using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Visor_de_Documentos.Models;

namespace Visor_de_Documentos.Escaneos
{
    class Reporte
    {
        string carne;
        string nombre;
        string carrera;
        string fecha;

        public string Carne { get => carne; set => carne = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Carrera { get => carrera; set => carrera = value; }
        public string Fecha { get => fecha; set => fecha = value; }
    }
    public partial class PagsReporteEscaneos : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            int sede = Convert.ToInt16(DropDownListSede.SelectedValue);


            string connString = "";

            if (sede > 0)
            {
                if (sede == 1)
                    connString = UtilidadesDB.CadenaConexion("SQL_CONN_NOTASMESOXELA");
                else
                    connString = UtilidadesDB.CadenaConexion("SQL_CONN_NOTASMESOGUATE");

                SqlDataSourceAdminsEscaneo.ConnectionString = connString;
                SqlDataSourceAdminsEscaneoGuate.ConnectionString = connString;
            }
            
        }

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
                                  Carrera = r.Nombre_de_la_carrera,
                                  Fecha = r.Fecha.ToString()
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

                    var repG = from r in DBG.View_EscaneosGeneral
                               where ((r.IdAdmin == idadmin) && (r.Fecha >= FechaI && r.Fecha <= FechaF))
                               orderby r.Fecha, r.Carne, r.Nombre_de_la_carrera
                               select new Reporte
                               {
                                   Carne = r.Carne,
                                   Nombre = r.Nombre_Completo,
                                   Carrera = r.Nombre_de_la_carrera,
                                   Fecha = r.Fecha.ToString()
                               };

                    List<Reporte> listareporG = new List<Reporte>();
                    int cuantosG = repG.ToList().Count;
                    listareporG = repG.ToList();


                    NOTASMESOCOBANEntitiesEscaneos DBC = new NOTASMESOCOBANEntitiesEscaneos();

                    var repC = from r in DBC.View_EscaneosGeneral
                               where ((r.IdAdmin == idadmin) && (r.Fecha >= FechaI && r.Fecha <= FechaF))
                               orderby r.Fecha, r.Carne, r.Nombre_de_la_carrera
                               select new Reporte
                               {
                                   Carne = r.Carne,
                                   Nombre = r.Nombre_Completo,
                                   Carrera = r.Nombre_de_la_carrera,
                                   Fecha = r.Fecha.ToString()
                               };

                    List<Reporte> listareporC = new List<Reporte>();
                    int cuantosC = repC.ToList().Count;
                    listareporC = repC.ToList();

                    NOTASMESOTEOEntitiesEscaneo DBT = new NOTASMESOTEOEntitiesEscaneo();


                    var repT = from r in DBT.View_EscaneosGeneral
                               where ((r.IdAdmin == idadmin) && (r.Fecha >= FechaI && r.Fecha <= FechaF))
                               orderby r.Fecha, r.Carne, r.Nombre_de_la_carrera
                               select new Reporte
                               {
                                   Carne = r.Carne,
                                   Nombre = r.Nombre_Completo,
                                   Carrera = r.Nombre_de_la_carrera,
                                   Fecha = r.Fecha.ToString()
                               };

                    List<Reporte> listareporT = new List<Reporte>();
                    int cuantosT = repT.ToList().Count;
                    listareporT = repT.ToList();

                    NOTASMESOIZABALEntitiesEscaneos DBI = new NOTASMESOIZABALEntitiesEscaneos();

                    var repI = from r in DBI.View_EscaneosGeneral
                               where ((r.IdAdmin == idadmin) && (r.Fecha >= FechaI && r.Fecha <= FechaF))
                               orderby r.Fecha, r.Carne, r.Nombre_de_la_carrera
                               select new Reporte
                               {
                                   Carne = r.Carne,
                                   Nombre = r.Nombre_Completo,
                                   Carrera = r.Nombre_de_la_carrera,
                                   Fecha = r.Fecha.ToString()
                               };

                    List<Reporte> listareporI = new List<Reporte>();
                    int cuantosI = repI.ToList().Count;
                    listareporI = repI.ToList();

                    NOTASMESOAMATITTLANEntitiesEscaneos DBA = new NOTASMESOAMATITTLANEntitiesEscaneos();
                    var repA = from r in DBA.View_EscaneosGeneral
                               where ((r.IdAdmin == idadmin) && (r.Fecha >= FechaI && r.Fecha <= FechaF))
                               orderby r.Fecha, r.Carne, r.Nombre_de_la_carrera
                               select new Reporte
                               {
                                   Carne = r.Carne,
                                   Nombre = r.Nombre_Completo,
                                   Carrera = r.Nombre_de_la_carrera,
                                   Fecha = r.Fecha.ToString()
                               };

                    List<Reporte> listareporA = new List<Reporte>();
                    int cuantosA = repA.ToList().Count;
                    listareporI = repA.ToList();



                    GridViewGuate.DataSource = listareporG;
                    GridViewGuate.DataBind();
                    GridViewCoban.DataSource = listareporC;
                    GridViewCoban.DataBind();
                    GridViewTeo.DataSource = listareporT;
                    GridViewTeo.DataBind();
                    GridViewIzabal.DataSource = listareporI;
                    GridViewIzabal.DataBind();
                    GridViewAmatitlan.DataSource = listareporA;
                    GridViewAmatitlan.DataBind();

                    total = cuantosG + cuantosC + cuantosT + cuantosI + cuantosA;

                   

                }
                lblNoExpedientes.Text = "No. de Expedientes: " + (total + cuantos);

            }
            catch
            {
                btnReporte.Focus();
            }
           
        }

        protected void btNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("PagsReporteEscaneos.aspx");
        }

        protected void TextBox2_TextChanged(object sender, EventArgs e)
        {
           
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
    }
}