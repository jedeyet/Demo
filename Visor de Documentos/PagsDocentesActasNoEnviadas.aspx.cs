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
    public partial class PagsDocentesActasNoEnviadas : System.Web.UI.Page
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
            for (int x = 2002; x <= DateTime.Now.Year; x++)
                ddAnio.Items.Add(x.ToString());
            ddAnio.SelectedIndex = ddAnio.Items.Count - 1;
            ddSemestre.Items.Add("1");
            ddSemestre.Items.Add("2");
            if (DateTime.Now.Month >= 7) ddSemestre.SelectedIndex = 1; else ddSemestre.SelectedIndex = 0;
            //Nivel = Convert.ToInt16(System.Web.HttpContext.Current.Session["nivel"].ToString().Trim());
           // Label1.Text = Nivel.ToString();
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

            if (Nivel == 0)
            {
                cadena = "select [id Carrera], [Nombre de la carrera] from carrera where estado='Activa' or estado='True'  order by  [Nombre de la carrera]";
                opcioncarrera = opcion;

            }
            else
            {

                cadena = "select * from vistaAdminsCarreras where usuario = '" + usuario + "' and idsede=" + opcion.ToString();
                opcioncarrera = 1; Button5.Visible = false;  Button6.Visible = false; Button7.Visible = false; Button8.Visible = false;
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
        protected void Button2_Click(object sender, EventArgs e)
        {
            cargaCuadricula(2);
        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            cargaCuadricula(3);
        }
        protected void Button4_Click(object sender, EventArgs e)
        {
            cargaCuadricula(4);
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            cargaCuadricula(5);
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            cargaCuadricula(6);
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
            oculta(false);
            lbResultado.Text = "";
        }

        private void cargaCuadricula(int opcio)
        {
            string complemento = ""; string fechas = "";
            if(opcio == 0) { complemento = " and RegistroFinal = 'NO' "; fechas = " finales as Finales "; }
            if (opcio == 1) { complemento = " and zona = 'SI' "; fechas = " zona as Zona, convert(nvarchar, Fecha_Zona, 103) as Fecha "; }
            if (opcio == 2) { complemento = " and zona = 'NO' "; fechas = " zona as Zona "; }
            if (opcio == 3) { complemento = " and finales = 'SI' "; fechas = " finales as Finales, convert(nvarchar, Fecha_Finales, 103) as Fecha "; }
            if (opcio == 4) { complemento = " and finales = 'NO' "; fechas = " finales as Finales "; }
            if (opcio == 5) { complemento = " and zona = 'NO' "; fechas = " zona as Zona"; }
            if (opcio == 6) { complemento = " and finales = 'NO' "; fechas = " finales as Finales "; }
            if (opcio == 7) { complemento = " and finales = 'NO' "; fechas = " finales as Finales "; }
            if (opcio == 8) { complemento = " and RegistroFinal = 'NO' "; fechas = " finales as Finales "; }


            int opcion = Convert.ToInt32(DropDownList1.SelectedValue.ToString());
            string carrerita = " and[Id Carrera] = " + ddCarrera.SelectedValue.ToString();
            if (opcio > 4) carrerita = "";
            string anio = ddAnio.SelectedValue.ToString();
            string semestre = ddSemestre.SelectedValue.ToString();
            string cadena = "select Asignatura,semestre as Semestre,seccion, [nombre de la carrera] as Carrera, [Nombre y apellidos del catedrático] as Docente, "
             + fechas  
             + ",numero_Acta as Acta from Vista_Asignacion_Curso_Profesor where ano = " + ddAnio.Text + " and semestrecursado = " + ddSemestre.Text + complemento
             + " and[Nombre y apellidos del catedrático] not like 'equi%' and asignatura not like '%(_)%' "
             + carrerita + " order by [nombre de la carrera],Semestre,asignatura";
            DataTable dt = Models.Conex.Consulta2(cadena, opcion);
            if (dt.Rows.Count == 0) { lbResultado.Text = "No se localizan actas"; oculta(false); }
            else
            {
                lbResultado.Text = "Números de actas localizadas: " + dt.Rows.Count.ToString();
                GridView1.DataSource = dt;
                GridView1.DataBind();
                oculta(true);
               
            }
        }

        private void oculta(bool opcion)
        {
            imgbutExc.Visible = opcion;
            GridView1.Visible = opcion;
        }

        protected void ddCarrera_SelectedIndexChanged(object sender, EventArgs e)
        {
            oculta(false);
            lbResultado.Text = "";
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

        protected void Button7_Click(object sender, EventArgs e)
        {
            cargaCuadricula(0);
        }

        protected void Button8_Click(object sender, EventArgs e)
        {
            cargaCuadricula(8);
        }

        protected void btNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("PagsDocentesActasNoEnviadas.aspx");
        }
    }
}