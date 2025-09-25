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
    public partial class PagsAlumnosDejaronMasDe2 : System.Web.UI.Page
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


        protected void Button1_Click(object sender, EventArgs e)
        {
            cargaCuadricula(1);
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargaCarreras();
            oculta(false);
            lbResultado.Text = "";
        }

        private void cargaCuadricula(int op)
        {
            
            int opcion = Convert.ToInt32(DropDownList1.SelectedValue.ToString());
            string anio = ddAnio.SelectedValue.ToString();
            string semestre = ddSemestre.SelectedValue.ToString();
            string ll = "";
            string carr = "";
            if (op==1) carr = " and [Id Carrera] = " + ddCarrera.SelectedValue.ToString();
            if (op == 2) ll = ", ";
            string cadena = "select [Número de carné] as Carnet,Nombre_Completo as Estudiante,count(*) as Perdidos,[nombre de la carrera] as Carrera  from vista_notas where gano = 0 and finales = 'si' and semestre = "
                + ddSemestre.Text + " and ano = " + ddAnio.Text + carr 
                + " group by[Número de carné], Nombre_Completo,[nombre de la carrera] having count(*)> 2 order by [nombre de la carrera],Nombre_Completo";
            DataTable dt = Models.Conex.Consulta2(cadena, opcion);
            if (dt.Rows.Count == 0)
            {
                lbResultado.Text = "No se localizan alumnos con más de dos cursos perdidos";
                oculta(false);
            }
            else
            {
                lbResultado.Text = "Alumnos con dos o más cursos perdidos: " + dt.Rows.Count.ToString();
                GridView1.DataSource = dt;
                GridView1.DataBind();
                oculta(true);
            }
        }

        private void oculta (bool opcion)
        {
            imgbutExc.Visible = opcion;
            GridView1.Visible = opcion;
            
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
            oculta(false); lbResultado.Text = "";
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            cargaCuadricula(2);
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("PagsAlumnosDejaronMasDe2.aspx");
        }

        protected void btNuevo_Click(object sender, EventArgs e)
        {
             Response.Redirect("PagsAlumnosDejaronMasDe2.aspx");
        }
    }
 
}