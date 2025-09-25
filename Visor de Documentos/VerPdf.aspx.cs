using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Linq;
using System.Data;

using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;

using System.Web.UI.HtmlControls;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Net;
using System.Net.Security;
using Visor_de_Documentos.Models;
using System.Web.UI.WebControls;

namespace Visor_de_Documentos.Pages
{
    public partial class VerPdf : Page
    {
        private class Archivos
        {
            string nombre;
            string id;

            public string Nombre { get => nombre; set => nombre = value; }
            public string Id { get => id; set => id = value; }
        }


        //El listado de archivos
        static IList<Google.Apis.Drive.v3.Data.File> files;


        //Para que acepte el certificado desde https
        protected void Page_Load(object sender, EventArgs e)
        {
            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(ValidateServerCertificate);
            
        }
        //hace que el certificado siempre sea valido
        public static bool ValidateServerCertificate(Object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        private static void SaveStream(System.IO.MemoryStream stream, string saveTo)
        {
            using (System.IO.FileStream file = new System.IO.FileStream(saveTo, System.IO.FileMode.Create, System.IO.FileAccess.Write))
            {
                stream.WriteTo(file);
            }
        }


        protected string KeyFile(int sede)
        {
            if (sede == 1)
                return "VisordeDocumentosRyP-7f82f28d5b1f.p12";
            else
                return "Visor de Documentos-5c7bec426d6c.p12";
        }

        protected string serviceAccount(int sede)
        {
            if (sede == 1)
                return "escritor@visor-de-documentos-186916.iam.gserviceaccount.com";
            else
                return "escritor@visor-de-documentos-197218.iam.gserviceaccount.com";            
        }
            

       

        protected void VerPdfAlumno(int sede)
        {
            
            try
            {
                //Autenticacion como servicio
                string[] scopes = new string[] { DriveService.Scope.Drive }; // Full access

                
                var keyFilePath3 = Server.MapPath(@KeyFile(sede));

                var serviceAccountEmail = serviceAccount(sede);


                //Que lea la llave desde el disco y no de el problema que no encuentra el archivo
                RSACryptoServiceProvider.UseMachineKeyStore = true;


                var certificate = new X509Certificate2(keyFilePath3, "notasecret", X509KeyStorageFlags.Exportable);

                var credential = new ServiceAccountCredential(new ServiceAccountCredential.Initializer(serviceAccountEmail)
                {
                    Scopes = scopes
                }.FromCertificate(certificate));

                var service = new DriveService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "Visor de Documentos",
                });

                //Id del documento extraido del combobox
                //var fileId = ddlDocumentos.SelectedItem.Value;
                var fileId = ddlDocumentos.SelectedValue;

                //Descargar el archivo con ese ID
                var request = service.Files.Get(fileId);

                //Cargarlo a memoria
                var stream = new System.IO.MemoryStream();
                //Carpeta temporal de descarga
                var temp = Server.MapPath(@"./temp");

                string archivotemp = "";
                // Response.Write("<script>alert('" + request.ToString() + "')</script>");
                //Descarga del Documento
                request.MediaDownloader.ProgressChanged += (Google.Apis.Download.IDownloadProgress progress) =>
                {
                    switch (progress.Status)
                    {
                        case Google.Apis.Download.DownloadStatus.Downloading:
                            {
                                Label1.Text = progress.BytesDownloaded.ToString();
                                Response.Write("<script>alert('En progreso')</script>");
                                break;
                            }
                        case Google.Apis.Download.DownloadStatus.Completed:
                            {


                                // Label1.Text = ("Download complete.");
                                //archivotemp = temp + "/" + files[ddlDocumentos.SelectedIndex].Name;
                                archivotemp = temp + "/" + ddlDocumentos.SelectedValue;
                                //  Response.Write("<script>alert('" + archivotemp + "')</script>");

                                SaveStream(stream, archivotemp);
                                break;
                            }
                        case Google.Apis.Download.DownloadStatus.Failed:
                            {
                                //Label1.Text = ("Download failed.");
                                //Response.Write("<script>alert('" + progress.Exception.InnerException.Message + "')</script>");
                                //Response.Write("<script>alert('" + progress.Exception.TargetSite.ToString() + "')</script>");
                                //Response.Write("<script>alert('" + progress.Exception.Message + "')</script>");
                                break;
                            }
                    }
                };

                request.Download(stream);

                //Mandar a llamar a WebFormPDF para mostrarlo en el frame1
                HtmlControl frame1 = (HtmlControl)Page.Master.FindControl("MainContent").FindControl("pdf");
                if (ddlDocumentos.Items.Count > 0)
                {
                    // Response.Write("<script>alert('" + archivotemp + "')</script>");
                    Session["archivo"] = archivotemp;
                    frame1.Attributes["src"] = "webFormPDF.aspx";
                }
            }
            catch (Exception e)
            {
                Label1.Text = e.Message;
                BtVerPdf.Focus();
            }

        }

        protected void BtVerPdf_Click(object sender, EventArgs e)
        {
            int sede = Convert.ToInt16(DropDownListSede.SelectedValue);

            VerPdfAlumno(sede);            
          
        }


        private void MostrarDocumento(string carne)
        {
            int sede = Convert.ToInt32(DropDownListSede.SelectedValue.ToString());

            if (sede == 1)
            {
                NOTASMESOEntitiesEscaneos DB = new NOTASMESOEntitiesEscaneos();


                var docs = from d in DB.Escaneos
                           where ((d.Carne == carne) && (d.nombrearchivo != null))
                           select d;


                if (docs.ToList().Count > 0)
                {
                    List<Archivos> nombres = new List<Archivos>();
                    foreach (var file in docs.ToList())
                    {
                        Archivos archtemp = new Archivos();
                        int arroba = (file.nombrearchivo.IndexOf("@") + 1);
                        int guion = (file.nombrearchivo.IndexOf("-"));
                        //dejar solo el nombre
                        string nombre = file.nombrearchivo.Normalize();
                        nombre = nombre.Substring(arroba, guion - arroba);
                        nombre = nombre.Replace("_", " ");

                        archtemp.Nombre = nombre;
                        archtemp.Id = file.idArchivo;
                        nombres.Add(archtemp);
                    }

                    nombres.Distinct().ToList().Sort((p, q) => string.Compare(p.Nombre, q.Nombre));

                    ddlDocumentos.DataValueField = "Id";
                    ddlDocumentos.DataTextField = "Nombre";
                    ddlDocumentos.DataSource = nombres;
                    ddlDocumentos.DataBind();


                    //NOTASMESOEntitiesReporteGeneralEscaneos DBa = new NOTASMESOEntitiesReporteGeneralEscaneos();

                    string alumnos = (from a in DB.View_EscaneosGeneral
                                      where (a.Carne == carne)
                                      select a.Nombre_Completo).FirstOrDefault();
                    Label2.Text = alumnos;
                    Label3.Visible = true; ddlDocumentos.Visible = true; BtVerPdf.Visible = true;
                }
                else
                {
                    ddlDocumentos.DataSource = null;
                    ddlDocumentos.DataBind();
                    Label3.Visible = false; ddlDocumentos.Visible = false; BtVerPdf.Visible = false;
                    Label2.Text = "Ese número de carné no posee archivos escaneados aún.";


                }
            }
            if (sede == 2)
            {
                NOTASMESOEntitiesEscaneosGuate DB = new NOTASMESOEntitiesEscaneosGuate();
                
                var docs = from d in DB.Escaneos
                           where ((d.Carne == carne) && (d.nombrearchivo != null))
                           select d;


                if (docs.ToList().Count > 0)
                {
                    List<Archivos> nombres = new List<Archivos>();
                    foreach (var file in docs.ToList())
                    {
                        Archivos archtemp = new Archivos();
                        int arroba = (file.nombrearchivo.IndexOf("@") + 1);
                        int guion = (file.nombrearchivo.IndexOf("-"));
                        //dejar solo el nombre
                        string nombre = file.nombrearchivo.Normalize();
                        nombre = nombre.Substring(arroba, guion - arroba);
                        nombre = nombre.Replace("_", " ");

                        archtemp.Nombre = nombre;
                        archtemp.Id = file.idArchivo;
                        nombres.Add(archtemp);
                    }

                    nombres.Distinct().ToList().Sort((p, q) => string.Compare(p.Nombre, q.Nombre));

                    ddlDocumentos.DataValueField = "Id";
                    ddlDocumentos.DataTextField = "Nombre";
                    ddlDocumentos.DataSource = nombres;
                    ddlDocumentos.DataBind();
                    
                    string alumnos = (from a in DB.View_EscaneosGeneral
                                      where (a.Carne == carne)
                                      select a.Nombre_Completo).FirstOrDefault();
                    Label2.Text = alumnos;
                    Label3.Visible = true; ddlDocumentos.Visible = true; BtVerPdf.Visible = true;
                }
                else
                {
                    ddlDocumentos.DataSource = null;
                    ddlDocumentos.DataBind();
                    Label3.Visible = false; ddlDocumentos.Visible = false; BtVerPdf.Visible = false;
                    Label2.Text = "Ese número de carné no posee archivos escaneados aún.";


                }
            }
            if (sede == 3)
            {
                NOTASMESOCOBANEntitiesEscaneos DB = new NOTASMESOCOBANEntitiesEscaneos();
                
                var docs = from d in DB.Escaneos
                           where ((d.Carne == carne) && (d.nombrearchivo != null))
                           select d;


                if (docs.ToList().Count > 0)
                {
                    List<Archivos> nombres = new List<Archivos>();
                    foreach (var file in docs.ToList())
                    {
                        Archivos archtemp = new Archivos();
                        int arroba = (file.nombrearchivo.IndexOf("@") + 1);
                        int guion = (file.nombrearchivo.IndexOf("-"));
                        //dejar solo el nombre
                        string nombre = file.nombrearchivo.Normalize();
                        nombre = nombre.Substring(arroba, guion - arroba);
                        nombre = nombre.Replace("_", " ");

                        archtemp.Nombre = nombre;
                        archtemp.Id = file.idArchivo;
                        nombres.Add(archtemp);
                    }

                    nombres.Distinct().ToList().Sort((p, q) => string.Compare(p.Nombre, q.Nombre));

                    ddlDocumentos.DataValueField = "Id";
                    ddlDocumentos.DataTextField = "Nombre";
                    ddlDocumentos.DataSource = nombres;
                    ddlDocumentos.DataBind();

                    
                    string alumnos = (from a in DB.View_EscaneosGeneral
                                      where (a.Carne == carne)
                                      select a.Nombre_Completo).FirstOrDefault();
                    Label2.Text = alumnos;
                    Label3.Visible = true; ddlDocumentos.Visible = true; BtVerPdf.Visible = true;
                }
                else
                {
                    ddlDocumentos.DataSource = null;
                    ddlDocumentos.DataBind();
                    Label3.Visible = false; ddlDocumentos.Visible = false; BtVerPdf.Visible = false;
                    Label2.Text = "Ese número de carné no posee archivos escaneados aún.";


                }
            }
            if (sede == 4)
            {
                NOTASMESOTEOEntitiesEscaneo DB = new NOTASMESOTEOEntitiesEscaneo();
                
                var docs = from d in DB.Escaneos
                           where ((d.Carne == carne) && (d.nombrearchivo != null))
                           select d;


                if (docs.ToList().Count > 0)
                {
                    List<Archivos> nombres = new List<Archivos>();
                    foreach (var file in docs.ToList())
                    {
                        Archivos archtemp = new Archivos();
                        int arroba = (file.nombrearchivo.IndexOf("@") + 1);
                        int guion = (file.nombrearchivo.IndexOf("-"));
                        //dejar solo el nombre
                        string nombre = file.nombrearchivo.Normalize();
                        nombre = nombre.Substring(arroba, guion - arroba);
                        nombre = nombre.Replace("_", " ");

                        archtemp.Nombre = nombre;
                        archtemp.Id = file.idArchivo;
                        nombres.Add(archtemp);
                    }

                    nombres.Distinct().ToList().Sort((p, q) => string.Compare(p.Nombre, q.Nombre));

                    ddlDocumentos.DataValueField = "Id";
                    ddlDocumentos.DataTextField = "Nombre";
                    ddlDocumentos.DataSource = nombres;
                    ddlDocumentos.DataBind();

                    
                    string alumnos = (from a in DB.View_EscaneosGeneral
                                      where (a.Carne == carne)
                                      select a.Nombre_Completo).FirstOrDefault();
                    Label2.Text = alumnos;
                    Label3.Visible = true; ddlDocumentos.Visible = true; BtVerPdf.Visible = true;
                }
                else
                {
                    ddlDocumentos.DataSource = null;
                    ddlDocumentos.DataBind();
                    Label3.Visible = false; ddlDocumentos.Visible = false; BtVerPdf.Visible = false;
                    Label2.Text = "Ese número de carné no posee archivos escaneados aún.";


                }
            }
            if (sede == 5)
            {
                NOTASMESOIZABALEntitiesEscaneos DB = new NOTASMESOIZABALEntitiesEscaneos();

                var docs = from d in DB.Escaneos
                           where ((d.Carne == carne) && (d.nombrearchivo != null))
                           select d;


                if (docs.ToList().Count > 0)
                {
                    List<Archivos> nombres = new List<Archivos>();
                    foreach (var file in docs.ToList())
                    {
                        Archivos archtemp = new Archivos();
                        int arroba = (file.nombrearchivo.IndexOf("@") + 1);
                        int guion = (file.nombrearchivo.IndexOf("-"));
                        //dejar solo el nombre
                        string nombre = file.nombrearchivo.Normalize();
                        nombre = nombre.Substring(arroba, guion - arroba);
                        nombre = nombre.Replace("_", " ");

                        archtemp.Nombre = nombre;
                        archtemp.Id = file.idArchivo;
                        nombres.Add(archtemp);
                    }

                    nombres.Distinct().ToList().Sort((p, q) => string.Compare(p.Nombre, q.Nombre));

                    ddlDocumentos.DataValueField = "Id";
                    ddlDocumentos.DataTextField = "Nombre";
                    ddlDocumentos.DataSource = nombres;
                    ddlDocumentos.DataBind();


                    string alumnos = (from a in DB.View_EscaneosGeneral
                                      where (a.Carne == carne)
                                      select a.Nombre_Completo).FirstOrDefault();
                    Label2.Text = alumnos;
                    Label3.Visible = true; ddlDocumentos.Visible = true; BtVerPdf.Visible = true;
                }
                else
                {
                    ddlDocumentos.DataSource = null;
                    ddlDocumentos.DataBind();
                    Label3.Visible = false; ddlDocumentos.Visible = false; BtVerPdf.Visible = false;
                    Label2.Text = "Ese número de carné no posee archivos escaneados aún.";


                }
            }
            if (sede == 6)
            {
                NOTASMESOAMATITTLANEntitiesEscaneos DB = new NOTASMESOAMATITTLANEntitiesEscaneos();

                var docs = from d in DB.Escaneos
                           where ((d.Carne == carne) && (d.nombrearchivo != null))
                           select d;


                if (docs.ToList().Count > 0)
                {
                    List<Archivos> nombres = new List<Archivos>();
                    foreach (var file in docs.ToList())
                    {
                        Archivos archtemp = new Archivos();
                        int arroba = (file.nombrearchivo.IndexOf("@") + 1);
                        int guion = (file.nombrearchivo.IndexOf("-"));
                        //dejar solo el nombre
                        string nombre = file.nombrearchivo.Normalize();
                        nombre = nombre.Substring(arroba, guion - arroba);
                        nombre = nombre.Replace("_", " ");

                        archtemp.Nombre = nombre;
                        archtemp.Id = file.idArchivo;
                        nombres.Add(archtemp);
                    }

                    nombres.Distinct().ToList().Sort((p, q) => string.Compare(p.Nombre, q.Nombre));

                    ddlDocumentos.DataValueField = "Id";
                    ddlDocumentos.DataTextField = "Nombre";
                    ddlDocumentos.DataSource = nombres;
                    ddlDocumentos.DataBind();


                    string alumnos = (from a in DB.View_EscaneosGeneral
                                      where (a.Carne == carne)
                                      select a.Nombre_Completo).FirstOrDefault();
                    Label2.Text = alumnos;
                    Label3.Visible = true; ddlDocumentos.Visible = true; BtVerPdf.Visible = true;
                }
                else
                {
                    ddlDocumentos.DataSource = null;
                    ddlDocumentos.DataBind();
                    Label3.Visible = false; ddlDocumentos.Visible = false; BtVerPdf.Visible = false;
                    Label2.Text = "Ese número de carné no posee archivos escaneados aún.";


                }
            }


        }


        private void Limpiar()
        {
            txtCarne.Text = "";
            Label2.Text = "";
            txNombre.Text = "";
            ddNombre.Items.Clear();
            ddlDocumentos.Items.Clear();            
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            try { 
                    MostrarDocumento(txtCarne.Text);
                 }
            catch
            {
                Button1.Focus();
            }


            if (txNombre.Text.Length > 0)
            {
                bool nopermitido = false;
                for (int c = 1; c < txNombre.Text.Length; c++)
                {
                    string caracter = txNombre.Text[c].ToString();
                    if (caracter == "=" || caracter == "'" || caracter == "-")
                        nopermitido = true;
                }
                if (nopermitido)
                { txNombre.Text = ""; }
                else
                {
                    int opcion = Convert.ToInt32(DropDownListSede.SelectedValue.ToString());

                    DataTable dt = new DataTable();
                    string cadena = "select  [número de carné], Nombre_completo + ' ' + [nombre de la carrera] As nombre_completo from VistaAlumnosCarrera where nombre_completo like '%" + txNombre.Text + "%' order by nombre_completo";
                    dt = Models.Conex.Consulta2(cadena, opcion);
                    if (dt.Rows.Count == 0)
                    { }//lbResultado.Text = "No se localizan coincidencias";
                    else
                    {
                        //lbResultado.Text = "Números de coincidencias: " + dt.Rows.Count.ToString();
                        ddNombre.DataValueField = "número de carné";
                        ddNombre.DataTextField = "nombre_completo";
                        ddNombre.DataSource = dt;
                        ddNombre.DataBind();
                        //lbnombre.Text = dt.Rows[0]["nombre_completo"].ToString();
                    }
                }
            }
        }

       


        protected void ddNombre_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCarne.Text = ddNombre.SelectedValue.ToString();
        }

        protected void btNuevo_Click(object sender, EventArgs e)
        {
            Limpiar();
        }


        private void buscaDatos()
        {

            if (txNombre.Text.Length > 0)
            {
                bool nopermitido = false;
                for (int c = 1; c < txNombre.Text.Length; c++)
                {
                    string caracter = txNombre.Text[c].ToString();
                    if (caracter == "=" || caracter == "'" || caracter == "-")
                        nopermitido = true;
                }
                if (nopermitido)
                { txNombre.Text = ""; }
                else
                {
                    int opcion =  Convert.ToInt32(DropDownListSede.SelectedValue.ToString());

                    DataTable dt = new DataTable();
                    string cadena = "select  [número de carné], Nombre_completo + ' ' + [nombre de la carrera] As nombre_completo from VistaAlumnosCarrera where nombre_completo like '%" + txNombre.Text + "%' order by nombre_completo";
                    dt = Models.Conex.Consulta2(cadena, opcion);
                    if (dt.Rows.Count == 0)
                    { }//lbResultado.Text = "No se localizan coincidencias";
                    else
                    {
                        //lbResultado.Text = "Números de coincidencias: " + dt.Rows.Count.ToString();
                        ddNombre.DataValueField = "número de carné";
                        ddNombre.DataTextField = "nombre_completo";
                        ddNombre.DataSource = dt;
                        ddNombre.DataBind();
                        //lbnombre.Text = dt.Rows[0]["nombre_completo"].ToString();
                        ddNombre.Items[0].Text = "-- Seleccione al Alumno ----";
                    }
                }
            }
        }


        protected void btCoincidencias_Click(object sender, EventArgs e)
        {
            buscaDatos();
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Limpiar();            
        }

        

        //protected void btCoincidencias_Click(object sender, EventArgs e)
        //{
        //    buscaDatos();
        //}

        //protected void ddNombre_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    txtCarne.Text = ddNombre.SelectedValue.ToString();
        //}

        //protected void btNuevo_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("VerPdf.aspx");
        //}
    }
}