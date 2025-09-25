using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Visor_de_Documentos.Models;

namespace Visor_de_Documentos
{
    class Reporte
    {
        string carne;
        string nombre;        
        string documentos;        

        public string Carne { get => carne; set => carne = value; }
        public string Nombre { get => nombre; set => nombre = value; }        
        public string Documentos { get => documentos; set => documentos = value; }        
    }
    public partial class PagsReporteEscaneosDetallado : System.Web.UI.Page
    {
        protected void Limpiar()
        {
            grdReporte.DataSource = null;
            grdReporte.DataBind();

            GridViewGuate.DataSource = null;
            GridViewGuate.DataBind();
            GridViewCoban.DataSource = null;
            GridViewCoban.DataBind();
            GridViewTeo.DataSource = null;
            GridViewTeo.DataBind();
            GridViewIzabal.DataSource = null;
            GridViewIzabal.DataBind();
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnReporte_Click(object sender, EventArgs e)
        {
            Limpiar();
            try
            {
                int idadmin = Convert.ToInt16(ddlAdmin.SelectedValue);


                DateTime FechaI = Convert.ToDateTime(TextBox1.Text);
                DateTime FechaF = Convert.ToDateTime(TextBox2.Text);

                FechaF = FechaF.AddDays(1);


                int cuantos = 0;
                int total = 0;

                if (DropDownListSede.SelectedValue == "1")
                {
                    NOTASMESOEntitiesEscaneos DB = new NOTASMESOEntitiesEscaneos();

                    var rep = from r in DB.View_EscaneosGeneral
                              where ((r.IdAdmin == idadmin) && (r.Fecha >= FechaI && r.Fecha <= FechaF))
                              orderby r.Fecha, r.Carne, r.Nombre_de_la_carrera
                              select new Reporte
                              {
                                  Carne = r.Carne,
                                  Nombre = r.Nombre_Completo,                                 
                              };
                    List<Reporte> listarepor = new List<Reporte>();
                    cuantos = rep.ToList().Count;
                    listarepor = rep.ToList();

                    grdReporte.DataSource = listarepor;
                    grdReporte.DataBind();

                    lblNoExpedientes.Text = "No. de Expedientes: " + cuantos;
                }
                else
                {
                    NOTASMESOEntitiesEscaneosGuate DBG = new NOTASMESOEntitiesEscaneosGuate();

                    var listadoG = (from r in DBG.View_EscaneosDetalle
                                                           where ((r.IdAdmin == idadmin) && (r.Fecha >= FechaI && r.Fecha <= FechaF))
                                                           orderby r.Nombre_Completo, r.nombrearchivo
                                                           select r).ToList();

                    List<Reporte> reportesG = new List<Reporte>();
                    

                    for (int i = 0; i < listadoG.Count - 1; i++)
                    {
                        Reporte reporte = new Reporte();

                        reporte.Carne = listadoG[i].Número_de_carné;
                        reporte.Nombre = listadoG[i].Nombre_Completo;
                        
                        string documento = "";

                        int j = i;
                        while ((listadoG[i].Número_de_carné == listadoG[j].Número_de_carné) && (j < listadoG.Count))
                        {
                            string archivo = listadoG[j].nombrearchivo;
                            int arroba = (archivo.IndexOf("@") + 1);
                            int guion = (archivo.IndexOf("-"));

                            //dejar solo el nombre
                            string nombre = archivo.Normalize();
                            nombre = nombre.Substring(arroba, guion - arroba);
                            nombre = nombre.Replace("_", " ");
                            documento += nombre + ", ";
                            if (j < listadoG.Count - 1)
                                j++;
                            else
                                break;
                            
                        }
                        i = j - 1;
                       
                        documento = documento.Remove(documento.Count()-2);
                        reporte.Documentos = documento;

                        reportesG.Add(reporte);

                    }
                    

                   
                    //NOTASMESOCOBANEntitiesEscaneos DBC = new NOTASMESOCOBANEntitiesEscaneos();

                    //var repC = from r in DBC.View_EscaneosGeneral
                    //           where ((r.IdAdmin == idadmin) && (r.Fecha >= FechaI && r.Fecha <= FechaF))
                    //           orderby r.Fecha, r.Carne, r.Nombre_de_la_carrera
                    //           select new Reporte
                    //           {
                    //               Carne = r.Carne,
                    //               Nombre = r.Nombre_Completo,
                    //               Carrera = r.Nombre_de_la_carrera,
                    //               Fecha = r.Fecha
                    //           };

                    //List<Reporte> listareporC = new List<Reporte>();
                    //int cuantosC = repC.ToList().Count;
                    //listareporC = repC.ToList();

                    //var repT = from r in DBC.View_EscaneosGeneral
                    //           where ((r.IdAdmin == idadmin) && (r.Fecha >= FechaI && r.Fecha <= FechaF))
                    //           orderby r.Fecha, r.Carne, r.Nombre_de_la_carrera
                    //           select new Reporte
                    //           {
                    //               Carne = r.Carne,
                    //               Nombre = r.Nombre_Completo,
                    //               Carrera = r.Nombre_de_la_carrera,
                    //               Fecha = r.Fecha
                    //           };

                    //List<Reporte> listareporT = new List<Reporte>();
                    //int cuantosT = repT.ToList().Count;
                    //listareporT = repT.ToList();


                    //var repI = from r in DBC.View_EscaneosGeneral
                    //           where ((r.IdAdmin == idadmin) && (r.Fecha >= FechaI && r.Fecha <= FechaF))
                    //           orderby r.Fecha, r.Carne, r.Nombre_de_la_carrera
                    //           select new Reporte
                    //           {
                    //               Carne = r.Carne,
                    //               Nombre = r.Nombre_Completo,
                    //               Carrera = r.Nombre_de_la_carrera,
                    //               Fecha = r.Fecha
                    //           };

                    //List<Reporte> listareporI = new List<Reporte>();
                    //int cuantosI = repI.ToList().Count;
                    //listareporI = repI.ToList();



                    GridViewGuate.DataSource = reportesG;
                    GridViewGuate.DataBind();
                    //GridViewCoban.DataSource = listareporC;
                    //GridViewCoban.DataBind();
                    //GridViewTeo.DataSource = listareporT;
                    //GridViewTeo.DataBind();
                    //GridViewIzabal.DataSource = listareporI;
                    //GridViewIzabal.DataBind();

                    //total = cuantosG + cuantosC + cuantosT + cuantosI;



                }
                lblNoExpedientes.Text = "No. de Expedientes: " + (total + cuantos);

            }
            catch (Exception err)
            {
                lblNoExpedientes.Text += err.Message;
                btnReporte.Focus();
            }

        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Limpiar();
            if (DropDownListSede.SelectedValue == "1")
            {
                ddlAdmin.DataSource = SqlDataSourceAdminsEscaneo;
                ddlAdmin.DataBind();
            }
            else
            {
                ddlAdmin.DataSource = SqlDataSourceAdminsEscaneoGuate;
                ddlAdmin.DataBind();
            }
        }

        protected void btNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("PagsReporteEscaneos.aspx");
        }
    }
}