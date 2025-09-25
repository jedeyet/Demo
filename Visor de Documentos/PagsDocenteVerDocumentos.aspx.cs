using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.SqlServer;
using System.Data.Linq.SqlClient;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using static Google.Apis.Drive.v3.DriveService;

namespace Visor_de_Documentos
{
    public partial class PagsDocenteVerDocumentos : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargaSedes();
                
            }
        }
        string usuario = ""; int Nivel = 0;
        private void CargaSedes()
        {
            usuario = System.Web.HttpContext.Current.Session["usuario"].ToString().Trim();
            ddlSede.Items.Clear();
            string cad = "select * from AdminsSedesPermisos where usuario='" + usuario + "'";
            DataTable dt = new DataTable();
            dt = Models.Conex.Consulta(cad);
            System.Web.HttpContext.Current.Session["Nivel"] = Convert.ToInt16(dt.Rows[0]["Nivel"].ToString());
            ddlSede.DataSource = dt;
            ddlSede.DataValueField = "idSede";
            ddlSede.DataTextField = "Sede";
            ddlSede.DataBind();
        }
        private static DriveService GetServiceGuate()
        {
            var tokenResponse = new TokenResponse
            {
                AccessToken = "ya29.a0ARrdaM_6YsVtx4c6Cws-kP4l-w11kCSYMVfODfyp7vxed_O-ZcS4TpXP7PItU3oEP4inMHV_h-EwRXqfqgHUmfX4yiFbFI0FRaU_luOtL5k3pNkb7xnAYFXo0wjMVuao4QTmIrtAQzRyNNRFdEiCUAEAqYKA",
                RefreshToken = "1//041AJq9rKxz2vCgYIARAAGAQSNwF-L9IrGWVjuGLtwg-eME6BTEluIRwq7I_IASwFXINZcd4Mqbpr6RD3GmndAmuESKE2k4UHk7k",
            };



            var applicationName = "DocumentosCatedraticosGuate";
            var username = "documentoscatguate@mesoamericana.edu.gt";


            var apiCodeFlow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = new ClientSecrets
                {
                    ClientId = "357520040888-e5mb1av5nhecacrlrtivc58nfje0ts19.apps.googleusercontent.com",
                    ClientSecret = "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"
                },
                Scopes = new[] { Scope.Drive },
                DataStore = new FileDataStore(applicationName)
            });


            var credential = new UserCredential(apiCodeFlow, username, tokenResponse);


            var service = new DriveService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = applicationName
            });

            return service;
        }
        private static DriveService GetServiceXela()
        {
            var tokenResponse = new TokenResponse
            {
                AccessToken = "ya29.a0ARrdaM_XuWbC3chELme0KWjcP3FNysh3KwCmF3ZI8pK5wTHp1c3yw7rKdscFrx8_2BMsseBshu6vuQPMThIn_lZxrXbcF4T-ZOaCamd-gnpPuRtM-OkgcOuS7AXxWCRdsyEa1_XT8Cj8B9XSwWm40wSC6r-X",
                RefreshToken = "1//047G6bX3L9U1_CgYIARAAGAQSNwF-L9IrplS9ZqEsioSu9PZdylHc9F4h99DHoTJMnqxSHZrLenNO2JVW4p1ashsDmdUjtkM2HNA",
            };


            var applicationName = "DocumentosCatedraticos";
            var username = "documentoscatxela @mesoamericana.edu.gt";


            var apiCodeFlow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = new ClientSecrets
                {
                    ClientId = "1084806953541-6lcsb4d6v0pp0k6672eo8fb3crpg56pa.apps.googleusercontent.com",
                    ClientSecret = "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"
                },
                Scopes = new[] { Scope.Drive },
                DataStore = new FileDataStore(applicationName)
            });


            var credential = new UserCredential(apiCodeFlow, username, tokenResponse);


            var service = new DriveService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = applicationName
            });

            return service;
        }

        private static void SaveStream(System.IO.MemoryStream stream, string saveTo)
        {
            using (System.IO.FileStream file = new System.IO.FileStream(saveTo, System.IO.FileMode.Create, System.IO.FileAccess.Write))
            {
                stream.WriteTo(file);
            }
        }


        protected void buscarXela()
        {
            NOTASMESOEntitiesCatedraticoXela DB = new NOTASMESOEntitiesCatedraticoXela();

            string nombre = txtCatedratico.Text;


            var profesores = from p in DB.Catedraticos
                             where ((p.Nombre_y_apellidos_del_catedrático.Contains(nombre)) || (p.Nombre_y_apellidos_del_catedrático.EndsWith(nombre)))
                             && ((p.Codigo_Catedratico != 666) && (p.Codigo_Catedratico != 275) && (p.Codigo_Catedratico != 397) && (p.Codigo_Catedratico != 399))
                             orderby p.Nombre_y_apellidos_del_catedrático
                             select new { nombre = p.Nombre_y_apellidos_del_catedrático, id = p.Codigo_Catedratico }; 


            lbxProfesores.DataValueField = "id";
            lbxProfesores.DataTextField = "nombre";
            lbxProfesores.DataSource = profesores.ToList();
            lbxProfesores.DataBind();

        }

        protected void buscarGuate()
        {
            string nombre = txtCatedratico.Text;

            using (SqlConnection conex = new SqlConnection("Data Source=Academico.umes.edu.gt;Initial Catalog=NOTASMESO; User ID=Local; Password='LectorUm3sLoc4l';"))
            {
                string sql = @"Select  Codigo_Catedratico, [Nombre y apellidos del catedrático] from Catedraticos "+

                    "Where ([Nombre y apellidos del catedrático] like '%' + @nombre + '%' " +
                    "AND (Codigo_Catedratico NOT IN (1, 10, 11, 12, 13, 14, 15, 16, 397, 398, 399, 400, 513, 9999, 8088))) " +
                    "order by [Nombre y apellidos del catedrático]";

                SqlCommand comando = new SqlCommand(sql, conex);
                comando.Parameters.AddWithValue("@nombre", nombre);

                conex.Open();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(comando);
                da.Fill(dt);

                lbxProfesores.DataValueField = "Codigo_Catedratico";
                lbxProfesores.DataTextField = "Nombre y apellidos del catedrático";
                lbxProfesores.DataSource = dt;
                lbxProfesores.DataBind();
            }

            
        }
        protected void btnBuscaProfesores_Click(object sender, EventArgs e)
        {
            if (ddlSede.SelectedValue == "1")
                buscarXela();

            if (ddlSede.SelectedValue == "2")
                buscarGuate();


        }

        protected void verXela()
        {
            try {
                DriveService service = GetServiceXela();

                NOTASMESOEntitiesCatedraticoXela DB = new NOTASMESOEntitiesCatedraticoXela();


                int idCatedratico = Convert.ToInt32(lbxProfesores.SelectedValue);
                string tipoDocumento = ddlDocumentos.SelectedValue;

                string fileId = (from d in DB.CatedraticosDocumentos
                                 where (d.idCatedratico == idCatedratico) && (d.TipoDocumento == tipoDocumento)
                                 select d.idDocumento).FirstOrDefault();

                if (fileId != null)
                {
                    var request = service.Files.Get(fileId);
                    var stream = new System.IO.MemoryStream();
                    var temp = Server.MapPath(@"./temp");
                    string archivotemp = "";

                    request.MediaDownloader.ProgressChanged += (Google.Apis.Download.IDownloadProgress progress) =>
                    {
                        switch (progress.Status)
                        {
                            case Google.Apis.Download.DownloadStatus.Downloading:
                                {
                                    //  Response.Write("<script>alert('En progreso')</script>");
                                    break;
                                }
                            case Google.Apis.Download.DownloadStatus.Completed:
                                {
                                    archivotemp = temp + "/" + ddlDocumentos.SelectedValue;
                                    SaveStream(stream, archivotemp);
                                    break;
                                }
                            case Google.Apis.Download.DownloadStatus.Failed:
                                {
                                    break;
                                }
                        }
                    };

                    request.Download(stream);

                    //Mandar a llamar a WebFormPDF para mostrarlo en el frame1
                    HtmlControl frame1 = (HtmlControl)Page.Master.FindControl("MainContent").FindControl("pdf");
                    if (ddlDocumentos.Items.Count > 0)
                    {
                        Session["archivo"] = archivotemp;
                        frame1.Attributes["src"] = "webFormPDF.aspx";
                    }
                }
                else
                    Response.Write("<script>alert('El catedrático no ha subido ese documento.')</script>");
            }
            catch
            {
                ButtonSubir.Focus();
            }
        }
        


        protected void verGuate()
        {
            try
            {
                DriveService service = GetServiceGuate();

                int idCatedratico = Convert.ToInt32(lbxProfesores.SelectedValue);
                string tipoDocumento = ddlDocumentos.SelectedValue;
                string fileId = "";
                DataTable dt = new DataTable();
                using (SqlConnection conex = new SqlConnection("Data Source=Academico.umes.edu.gt;Initial Catalog=NOTASMESO; User ID=Local; Password='LectorUm3sLoc4l';"))
                {

                    string sql = "Select  idDocumento from CatedraticosDocumentos " +
                                "Where idCatedratico = @idCatedratico and TipoDocumento = @tipoDocumento";

                    SqlCommand comando = new SqlCommand(sql, conex);
                    comando.Parameters.AddWithValue("@idCatedratico", idCatedratico);
                    comando.Parameters.AddWithValue("@tipoDocumento", tipoDocumento);

                    conex.Open();

                    SqlDataAdapter da = new SqlDataAdapter(comando);
                    da.Fill(dt);

                }
                if (dt.Rows.Count > 0)
                {
                    fileId = dt.Rows[0]["idDocumento"].ToString();
                    var request = service.Files.Get(fileId);
                    var stream = new System.IO.MemoryStream();
                    var temp = Server.MapPath(@"./temp");
                    string archivotemp = "";

                    request.MediaDownloader.ProgressChanged += (Google.Apis.Download.IDownloadProgress progress) =>
                    {
                        switch (progress.Status)
                        {
                            case Google.Apis.Download.DownloadStatus.Downloading:
                                {
                                    //  Response.Write("<script>alert('En progreso')</script>");
                                    break;
                                }
                            case Google.Apis.Download.DownloadStatus.Completed:
                                {
                                    archivotemp = temp + "/" + ddlDocumentos.SelectedValue;
                                    SaveStream(stream, archivotemp);
                                    break;
                                }
                            case Google.Apis.Download.DownloadStatus.Failed:
                                {
                                    break;
                                }
                        }
                    };

                    request.Download(stream);

                    //Mandar a llamar a WebFormPDF para mostrarlo en el frame1
                    HtmlControl frame1 = (HtmlControl)Page.Master.FindControl("MainContent").FindControl("pdf");
                    if (ddlDocumentos.Items.Count > 0)
                    {
                        Session["archivo"] = archivotemp;
                        frame1.Attributes["src"] = "webFormPDF.aspx";
                    }
                }
                else
                    Response.Write("<script>alert('El catedrático no ha subido ese documento.')</script>");
            }
            catch
            { ButtonSubir.Focus(); }

        }

        protected void ButtonVer_Click(object sender, EventArgs e)
        {

            if (lbxProfesores.SelectedItem == null)
                Response.Write("<script>alert('Debe seleccionar un catedrático!!!!')</script>");
            else
            {
                if (ddlSede.SelectedValue == "1")
                    verXela();

                if (ddlSede.SelectedValue == "2")
                    verGuate();
            }


        }
        protected void btNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("PagsDocenteVerDocumentos.aspx");
        }
    }
}