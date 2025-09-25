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
    public partial class PagsAlumnosInscripcionGenero : System.Web.UI.Page
    {
            protected void Page_Load(object sender, EventArgs e)
            {
                if (!Page.IsPostBack)
                {
                CargaSedes(); CargaDatos();
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
            for (int x = 2000; x <= DateTime.Now.Year; x++)
                ddAnio.Items.Add(x.ToString());
            ddAnio.SelectedIndex = ddAnio.Items.Count - 1;
            ddSemestre.Items.Add("1");
            ddSemestre.Items.Add("2");
            if (DateTime.Now.Month >= 7) ddSemestre.SelectedIndex = 1; else ddSemestre.SelectedIndex = 0;
        }



       

        protected void Button1_Click(object sender, EventArgs e)
            {
                int opcion = Convert.ToInt32(DropDownList1.SelectedValue.ToString());
                string anio = ddAnio.SelectedValue.ToString();
                string semestre = ddSemestre.SelectedValue.ToString();
                DataTable dt = new DataTable();
                string cadena = "select t1.[Nombre de la carrera],t1.SemestreActual as Ciclo, " +
                "(select count(*) from vista_inscripcion as t2 where t2.Sexo = 'femenino' and t2.SemestreActual = t1.SemestreActual " +
                "and t2.ano = t1.ano and t2.SemestreAnual = t1.semestreanual and t2.[Nombre de la carrera] = t1.[Nombre de la carrera]) as Femenino, " +
                "(select count(*) from vista_inscripcion as t2 where t2.Sexo = 'Masculino' and t2.SemestreActual = t1.SemestreActual and t2.ano = t1.ano and " +
                "t2.SemestreAnual = t1.semestreanual and t2.[Nombre de la carrera] = t1.[Nombre de la carrera] ) as Masculino," +
                " count(*) as Total from vista_inscripcion  as t1 where t1.ano = " + ddAnio.Text + " and t1.SemestreAnual = " + ddSemestre.Text + 
                " group by t1.[Nombre de la carrera],t1.ano,t1.SemestreAnual,t1.SemestreActual " +
                " order by t1.[Nombre de la carrera],t1.SemestreActual";
                dt = Models.Conex.Consulta2(cadena, opcion);
                if (dt.Rows.Count == 0)
                {
                    lbResultado.Text = "No se localizan alumnos inscritos";
                    lbResultado.Visible = true; GridView1.Visible = false;
                    imgbutExc.Visible = false;
            }
                else
                {
                    imgbutExc.Visible = true;
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                    cadena = "select * from vista_inscripcion where ano = " + ddAnio.Text + " and SemestreAnual = " + ddSemestre.Text;
                    dt = Models.Conex.Consulta2(cadena, opcion);
                    lbResultado.Visible = true; GridView1.Visible = true; lbResultado.Text = "Inscripciones localizadas: " + dt.Rows.Count.ToString();
                }
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

        protected void btNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("PagsAlumnosInscripcionGenero.aspx");
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
           
        }
    }
}