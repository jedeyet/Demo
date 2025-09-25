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

namespace Visor_de_Documentos
{
    public partial class PagsAlumnosPromedios : System.Web.UI.Page
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

        private void CargaDatos()
        {
            for (int x = 2002; x <= DateTime.Now.Year; x++)
                ddAnio.Items.Add(x.ToString());
            ddAnio.SelectedIndex = ddAnio.Items.Count - 1;
            ddSemestre.Items.Add("1");
            ddSemestre.Items.Add("2");
            if (DateTime.Now.Month >= 7) ddSemestre.SelectedIndex = 1; else ddSemestre.SelectedIndex = 0;
        }



        private void CargaCarreras()
        {
            usuario = System.Web.HttpContext.Current.Session["usuario"].ToString().Trim();
            Nivel = Convert.ToInt16(System.Web.HttpContext.Current.Session["Nivel"].ToString());
            int opcion = Convert.ToInt32(DropDownList1.SelectedValue.ToString());
            int opcioncarrera = 1;
            DataTable dt = new DataTable();
            string cadena = "";


            if (Nivel == 0)
            {
                cadena = "select [id Carrera], [Nombre de la carrera] from carrera where estado = 'activa' or estado = 'true'  order by  [Nombre de la carrera]";
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

        private void Ver()
        {
            int opcion = Convert.ToInt32(DropDownList1.SelectedValue.ToString());
            string anio = ddAnio.SelectedValue.ToString();
            string semestre = ddSemestre.SelectedValue.ToString();
            DataTable dt = new DataTable();
            string cadena = "";
            if (opcion==1)
            cadena = "select [Número de carné],Nombre_Completo, " +
            " (select count(gano) from vista_notas t2 where t2.[Número de carné] = t1.[Número de carné] and " +
            " t2.gano = 1 and t2.Finales = 'SI') as Cursos, (select count(gano) from vista_notas t2 where " +
            " t2.[Número de carné] = t1.[Número de carné] and t2.gano = 0 and t2.Finales <> 'NO') " +
            " as Perdidos, (select avg([nota final]) from vista_notas t3 where t3.[Número de carné] = " +
            " t1.[Número de carné] and t3.gano = 1 and t3.Finales = 'SI' and t3.TipoNota<>'Equivalencia Externa') " + 
            "as Promedio from vista_inscripcion t1 where ano = " + anio + " and SemestreAnual = "  + semestre + 
            " and[Nombre de la carrera] = '" + ddCarrera.SelectedItem.Text + "' " + "order by promedio desc," + 
            "cursos desc";
            else
                cadena = "select [Número de carné],Nombre_Completo, " +
                " (select count(gano) from vista_notas t2 where t2.[Número de carné] = t1.[Número de carné] and " +
                " t2.gano = 1 and t2.Finales = 'SI') as Cursos, (select count(gano) from vista_notas t2 where " +
                " t2.[Número de carné] = t1.[Número de carné] and t2.gano = 0 and t2.Finales <> 'NO') " +
                " as Perdidos, (select avg(cast([nota final] as decimal)) from vista_notas t3 where t3.[Número de carné] = " +
                " t1.[Número de carné] and t3.gano = 1 and t3.Finales = 'SI' and t3.TipoNota<>'Equivalencia Externa') " +
                "as Promedio from vista_inscripcion t1 where ano = " + anio + " and SemestreAnual = " + semestre +
                " and[Nombre de la carrera] = '" + ddCarrera.SelectedItem.Text + "' " + "order by promedio desc," +
                "cursos desc";
            dt = Models.Conex.Consulta2(cadena, opcion);
            if (dt.Rows.Count == 0)
            {
                lbResultado.Text = "No se localizan alumnos con promedios en esta carrera"; 
                imgbutExc.Visible = false;
            }
            else
            {
                imgbutExc.Visible = true;
                lbResultado.Text = "Números de alumnos localizados: " + dt.Rows.Count.ToString();
                GridView1.DataSource = dt;
                GridView1.DataBind();
                lbResultado.Visible = true;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Ver();
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
            GridView1.DataSource = ""; GridView1.DataBind();
            lbResultado.Visible = false; imgbutExc.Visible = false;
        }

        protected void btNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("PagsAlumnosNohanEvaluado.aspx");
        }



        protected void imgbutExc_Click(object sender, ImageClickEventArgs e)
        {
            ExportaExcel(GridView1);
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

        protected void ddCarrera_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridView1.DataSource = ""; GridView1.DataBind();
            lbResultado.Visible = false; imgbutExc.Visible = false;
        }
    }
}