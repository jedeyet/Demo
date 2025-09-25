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
    public partial class PagsAlumnosFinales : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!Page.IsPostBack)
                {
                    CargaSedes();
                    CargaCarreras();
                    CargaDatos();

                }
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
            for (int x = 2024; x <= DateTime.Now.Year; x++)
                ddAnio.Items.Add(x.ToString());
            ddAnio.SelectedIndex = ddAnio.Items.Count - 1;




            for (byte a = 65; a <= 90; a++)
            {
                ddSeccion.Items.Add(((char)a).ToString());
                if (a == 90)
                {
                    for (byte b = 65; b <= 90; b++)
                        ddSeccion.Items.Add("A" + ((char)b).ToString());
                }
            }

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
                cadena = cadena = "select [id Carrera], [Nombre de la carrera] from carrera where (estado = 'activa' or estado = 'True') order by  [Nombre de la carrera]";
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
            llenaSemestres();
        }

        private void llenaSemestres()
        {
            int opcion = Convert.ToInt32(DropDownList1.SelectedValue.ToString());
            string rangos = "select * from carrera where [id carrera]=" + ddCarrera.SelectedValue.ToString();
            DataTable rangosDt = new DataTable();
            rangosDt = Models.Conex.Consulta2(rangos, opcion);
            lbinicio.Text = rangosDt.Rows[0]["semestre_inicia"].ToString();
            lbFin.Text = rangosDt.Rows[0]["semestre_finaliza"].ToString();
            int inicio = Convert.ToInt16(lbinicio.Text);
            int fin = Convert.ToInt16(lbFin.Text);
            ddSemestre.Items.Clear();
            for (int i = inicio; i <= fin; i++)
            {
                ddSemestre.Items.Add(i.ToString());
            }

        }

        protected void ddCarrera_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenaSemestres();
            oculta(false); oculta1(false); lbResultado.Visible = false;
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
            oculta(false); oculta1(false);  lbResultado.Visible = false;
        }

        private void oculta(bool opcion)
        {
            Label6.Visible = opcion; Button1.Visible = opcion; ddAsignatura.Visible = opcion;

        }

        private void oculta1(bool opcion)
        {

             
            Label7.Visible = opcion; lbDocente.Visible = opcion;
           //lbResultado.Visible = opcion;
            GridCursos.Visible = false; imgbutExc.Visible = opcion;
        }

        protected void btProceder_Click(object sender, EventArgs e)
        {
            int opcion = Convert.ToInt32(DropDownList1.SelectedValue.ToString());
            string cadena = "select codigo_asignatura,asignatura from asignatura where carrera =" +
                    ddCarrera.SelectedValue + " and semestre = " + ddSemestre.Text + " and yanoseda= 0 ";
            DataTable dt = new DataTable();
            dt = Models.Conex.Consulta2(cadena, opcion);
            if (dt.Rows.Count > 0)
            {
                ddAsignatura.DataValueField = "codigo_asignatura";
                ddAsignatura.DataTextField = "asignatura";
                ddAsignatura.DataSource = dt;
                ddAsignatura.DataBind();
                lbResultado.Text = "Asignaturas localizadas: " + dt.Rows.Count.ToString();
                oculta(true); lbResultado.Visible = true;

            }
            else
            {
                lbResultado.Text = "No se localizan asignaturas evaluadas";
                oculta(false); oculta1(false);  

            }
        }

        protected void ddAnio_SelectedIndexChanged(object sender, EventArgs e)
        {
            oculta(false); oculta1(false); lbResultado.Visible = false;
        }

        protected void ddSemestre_SelectedIndexChanged(object sender, EventArgs e)
        {
            oculta(false); oculta1(false); lbResultado.Visible = false;
        }

        protected void ddSeccion_SelectedIndexChanged(object sender, EventArgs e)
        {
            //oculta(false); oculta1(false);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int opcion = Convert.ToInt32(DropDownList1.SelectedValue.ToString());
            string cadena = "select ROW_NUMBER() OVER(ORDER BY Nombre_Completo ASC) as No, [Número de carné], " +
                " Nombre_Completo,Zona, [Examen final], [Nota final],semestre_Cursado as Semestre, " +
                "Seccion from vista_notas where Codigo_Acta = '" + ddAsignatura.SelectedValue.ToString() +
                ddAnio.SelectedValue.ToString() + ddSeccion.SelectedValue.ToString() + "'";
            DataTable dt = new DataTable();
            dt = Models.Conex.Consulta2(cadena, opcion);
            if (dt.Rows.Count == 0)
            {
                lbResultado.Text = "No se localizan estudiantes registrados en esta asignatura";
                oculta1(false); imgbutExc.Visible = false;

            }
            else
            {
                GridCursos.DataSource = dt;
                GridCursos.DataBind();
                GridCursos.Visible = true; imgbutExc.Visible = true;
                string cad = "select top(1) [Nombre y apellidos del catedrático] from Vista_Notas where Codigo_Acta = '" +
                    ddAsignatura.SelectedValue.ToString() + ddAnio.SelectedValue.ToString() + ddSeccion.SelectedValue.ToString() + "'";

                DataTable dt2 = new DataTable(); dt2 = Models.Conex.Consulta2(cad, opcion);
                lbDocente.Text = dt2.Rows[0][0].ToString(); Label7.Visible = true; lbDocente.Visible = true;
            }

        }

        protected void btNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("PagsAlumnosFinales.aspx");
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
            ExportaExcel(GridCursos);
        }
    }
}