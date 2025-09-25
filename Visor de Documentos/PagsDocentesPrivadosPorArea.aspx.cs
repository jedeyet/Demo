using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Configuration;

namespace Visor_de_Documentos
{
    public partial class PagsDocentesPrivadosPorArea : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargaSedes();
                CargaDatos();
                CargaCarreras();
            }
        }
        string usuario = ""; int Nivel = 0;

        private void CargaDatos()
        {
            for (int x = 2019; x <= DateTime.Now.Year; x++)
                ddAnio.Items.Add(x.ToString());
            ddAnio.SelectedIndex = ddAnio.Items.Count - 1;
            ddConvocatoria.Items.Add("1");
            ddConvocatoria.Items.Add("2");

            usuario = System.Web.HttpContext.Current.Session["usuario"].ToString().Trim();


            string cad = "select * from Privado_Fase";
            DataTable dt = new DataTable();
            dt = Models.Conex.Consulta(cad);

            ddFase.DataSource = dt;
            ddFase.DataValueField = "id_Fase";
            ddFase.DataTextField = "Fase";
            ddFase.DataBind();




            if (DateTime.Now.Month >= 7) ddConvocatoria.SelectedIndex = 1; else ddConvocatoria.SelectedIndex = 0;
        }
        private void CargaSedes()
        {
            usuario = System.Web.HttpContext.Current.Session["usuario"].ToString().Trim();
            DropDownList1.Items.Clear();
            string cad = "select * from AdminsSedesPermisos where usuario='" + usuario + "'";
            DataTable dt = new DataTable();
            dt = Models.Conex.Consulta(cad);
            System.Web.HttpContext.Current.Session["Nivel"] = Convert.ToInt16(dt.Rows[0]["Nivel"].ToString());
            DropDownList1.DataSource = dt;
            DropDownList1.DataValueField = "idSede";
            DropDownList1.DataTextField = "Sede";
            DropDownList1.DataBind();
        }
        private void CargaCarreras()
        {
            usuario = System.Web.HttpContext.Current.Session["usuario"].ToString().Trim();
            Nivel = Convert.ToInt16(System.Web.HttpContext.Current.Session["Nivel"].ToString());
            int opcion = Convert.ToInt32(DropDownList1.SelectedValue.ToString());
            int opcioncarrera = 1;
            DataTable dt = new DataTable();
            string cadena = "";
            string activas = " where estado = 'Activa' ";
            if (opcion > 1) activas = " where estado = 'True' ";
            if (Nivel == 0)
            {
                cadena = "select [id Carrera], [Nombre de la carrera] from carrera " + activas + " order by  [Nombre de la carrera]";
                opcioncarrera = opcion;
            }
            else
            {
                cadena = "select * from vistaAdminsCarreras where usuario = '" + usuario + "' and idsede=" + opcion.ToString();
                opcioncarrera = 1;
            }
            dt = Models.Conex.Consulta2(cadena, opcioncarrera);
            ddCarrera.DataValueField = "id carrera";
            ddCarrera.DataTextField = "Nombre de la carrera";
            ddCarrera.DataSource = dt;
            ddCarrera.DataBind();
        }

        private void oculta(bool opcion)
        {
            imgbutExc.Visible = opcion;
            GridView1.Visible = opcion;

        }

        private void Nivelin()
        {
            int opcion = Convert.ToInt32(DropDownList1.SelectedValue.ToString());
            string usu = System.Web.HttpContext.Current.Session["usuario"].ToString().Trim();
            string cad = "select * from AdminsSedesPermisos where usuario='" + usu + "' and idsede=" + opcion.ToString();
            //Label5.Text = cad;
            DataTable dt = new DataTable();
            dt = Models.Conex.Consulta(cad);
            System.Web.HttpContext.Current.Session["Nivel"] = Convert.ToInt16(dt.Rows[0]["Nivel"].ToString());
            //Label5.Text = System.Web.HttpContext.Current.Session["Nivel"].ToString();
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Nivelin();
            CargaCarreras();
            oculta(false); lbResultado.Text = "";
        }

        private void ExportaExcel(GridView grid)
        {
            HttpResponse ResponsePage = Response;
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page pageToRender = new Page();
            HtmlForm form = new HtmlForm();
            form.Controls.Add(grid);
            pageToRender.Controls.Add(form);
            ResponsePage.Clear();
            ResponsePage.Buffer = true;
            ResponsePage.ContentType = "application/vnd.ms-excel";
            string namereport = "Informe.xls";
            ResponsePage.AddHeader("Content-Disposition", "attachment;filename=" + namereport);
            ResponsePage.Charset = "UTF-8";
            ResponsePage.ContentEncoding = Encoding.Default;
            pageToRender.RenderControl(htw);
            ResponsePage.Write(sw.ToString());
            ResponsePage.End();
        }
        protected void imgbutExc_Click(object sender, ImageClickEventArgs e)
        {
            ExportaExcel(GridView1);
        }

        protected void ddCarrera_SelectedIndexChanged(object sender, EventArgs e)
        {
            oculta(false);
            lbResultado.Text = "";
        }
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the currently selected row using the SelectedRow property.
            GridViewRow row = GridView1.SelectedRow;

            // Display the first name from the selected row.
            // In this example, the third column (index 2) contains
            // the first name.
            Session["c"] = row.Cells[1].Text;
            Session["carr"] = Convert.ToInt32(DropDownList1.SelectedValue.ToString());

            // Response.Redirect("PagsDocenteIndividualLista.aspx");

            ClientScript.RegisterStartupScript(this.Page.GetType(), "", "window.open('PagsDocenteIndividualLista.aspx','Graph','height=600,width=600');", true);



        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            usuario = System.Web.HttpContext.Current.Session["usuario"].ToString().Trim();
            int opcion = Convert.ToInt32(DropDownList1.SelectedValue.ToString());
            string fase = ddFase.SelectedValue.ToString();
            string convocatoria = ddConvocatoria.SelectedValue.ToString();
            string idcarrera = ddCarrera.SelectedValue.ToString();
            string anio = ddAnio.SelectedValue.ToString();
            string cadena = "SELECT ID_Area, Area +': ' + Catedrático AS Area FROM Vista_Privado_Carrera " +
                      "WHERE [Id Carrera] = " + idcarrera + " AND ID_Fase = " + fase + " AND Convocatoria = " +
                      convocatoria + " And Año = " + anio;
            DataTable dt = new DataTable();
            dt = Models.Conex.Consulta(cadena);

            if (dt.Rows.Count == 0)
            { lbResultado.Text = "No hay áreas registradas de privados con esta combinación de datos "; oculta(false); oculta1(false); }

            else
            {
                lbResultado.Text = "Números de alumnos localizados: " + dt.Rows.Count.ToString();
                ddArea.DataSource = dt;
                ddArea.DataValueField = "id_Area";
                ddArea.DataTextField = "Area";
                ddArea.DataBind();
                oculta1(true);
                
            }

        }
        private void oculta1(bool op)
        {
            Label5.Visible = op; ddArea.Visible = op; Button3.Visible = op;
        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            usuario = System.Web.HttpContext.Current.Session["usuario"].ToString().Trim();
            int opcion = Convert.ToInt32(DropDownList1.SelectedValue.ToString());
            string cadena = "SELECT  Carnet, Alumno, Punteo FROM  Vista_Privados_Punteo " +
                      "WHERE Id_Area = " + ddArea.SelectedValue.ToString();
            DataTable dt = new DataTable();
            dt = Models.Conex.Consulta(cadena);
            GridView1.DataSource = dt; GridView1.DataBind();
            oculta(true);
        }


        protected void btNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("PagsDocentesPrivadosPorArea.aspx");
        }
    }
}