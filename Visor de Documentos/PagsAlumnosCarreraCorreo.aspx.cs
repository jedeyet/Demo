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
    public partial class PagsAlumnosCarreraCorreo : Visor_de_Documentos.PaginaProtegida
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
            string acti = " where Estado  = 'Activa'";
            if (opcion > 1) acti = " where Estado = 'True' ";

            if (Nivel == 0)
            {
                cadena = "select [id Carrera], [Nombre de la carrera] from carrera " + acti + " order by  [Nombre de la carrera]";
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

        protected void Button1_Click(object sender, EventArgs e)
        {

            int opcion = Convert.ToInt32(DropDownList1.SelectedValue.ToString());
            string anio = ddAnio.SelectedValue.ToString();
            string semestre = ddSemestre.SelectedValue.ToString();
            DataTable dt = new DataTable();
            string cadena = "select  [Número de carné] AS Carnet, Nombre_Completo AS Nombre, Usuario + '@umes.edu.gt' AS Correo, SemestreActual AS Ciclo, " +
                "semestreAnual AS Semestre  FROM Vista_Alumnos_Inscripcion_Usuarios WHERE Ano = " + anio + " AND Codigo_Carrera = " +
                ddCarrera.SelectedValue.ToString() + "  AND SemestreAnual = " + semestre + "ORDER BY Ciclo, Nombre";

            dt = Models.Conex.Consulta2(cadena, opcion);
            if (dt.Rows.Count == 0)
            {
                lbResultado.Text = "No se localizan alumnos activos"; imgbutExc.Visible = false;
            }
            else
            {
                TextBox1.Text = ""; TextBox2.Text = ""; TextBox3.Text = ""; imgbutExc.Visible = true;
                TextBox1.Visible = true; Button2.Visible = true; Button3.Visible = true;
                lbResultado.Text = "Números de alumnos localizados: " + dt.Rows.Count.ToString();
                GridView1.DataSource = dt;
                GridView1.DataBind();
                string coma = "";
                string puntoycoma = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    coma += dt.Rows[i]["Correo"].ToString() + ", ";
                    puntoycoma += dt.Rows[i]["Correo"].ToString() + "; ";
                }
                coma = coma.Substring(0, coma.Length - 2);
                puntoycoma = puntoycoma.Substring(0, puntoycoma.Length - 2);
                TextBox1.Text = coma; TextBox2.Text = coma; TextBox3.Text = puntoycoma;
                TextBox1.Visible = true; lbResultado.Visible = true;

            }
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
        }

        protected void btNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("PagsAlumnosCarreraCorreo.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            TextBox1.Text = TextBox2.Text;
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            TextBox1.Text = TextBox3.Text;
        }

        protected void ddCarrera_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextBox1.Visible = false; Button2.Visible = false; Button3.Visible = false; GridView1.DataSource = ""; GridView1.DataBind();
            lbResultado.Visible = false; imgbutExc.Visible = false;
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
    }
}