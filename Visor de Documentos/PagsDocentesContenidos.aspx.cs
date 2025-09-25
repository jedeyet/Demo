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
    public partial class PagsDocentesContenidos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargaSedes();
                CargaDatos();
                CargaFacultades();
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

        private void CargaFacultades()
        {
            int opcion = Convert.ToInt32(DropDownList1.SelectedValue.ToString());
            DataTable dt = new DataTable();
            string cadena = "select * from Facultad";
            dt = Models.Conex.Consulta2(cadena, opcion);
            ddFacultad.DataValueField = "Codigo_facultad";
            ddFacultad.DataTextField = "Facultad";
            ddFacultad.DataSource = dt;
            ddFacultad.DataBind();
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



        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargaFacultades();
            oculta(false);
            lbResultado.Text = "";
        }

        private void oculta(bool opcion)
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

        protected void ddFacultad_SelectedIndexChanged(object sender, EventArgs e)
        {
            oculta(false); lbResultado.Text = "";
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                int opcion = Convert.ToInt32(DropDownList1.SelectedValue.ToString());
                string anio = ddAnio.SelectedValue.ToString();
                string semestre = ddSemestre.SelectedValue.ToString();
                DataTable dt = new DataTable();

                string rango = "'" + anio + "0701' and '" + anio + "1231'";
                if (Convert.ToInt16(semestre) == 1)
                {
                    rango = "'" + anio + "0101' and '" + anio + "0630'";
                }

                string cadena = "select [Nombre y apellidos del catedrático] as Docente,asignatura as Asignatura, semestre as Semestre," +
                    " seccion as Seccion,iif(entrego_pv=1,'Si','NO') as Programa,[Nombre de la carrera] as Carrera " +
                    " from Vista_Asignacion_Curso_Profesor where ano = " + anio +
                    " and SemestreCursado = " + semestre + " and facultad ='" + ddFacultad.SelectedItem.Text + "' " +
                    " order by entrego_pv,[Nombre de la carrera],semestre,seccion,[Nombre y apellidos del catedrático]";

                dt = Models.Conex.Consulta2(cadena, opcion);
                if (dt.Rows.Count == 0)
                {
                    lbResultado.Text = "No se localizan docentes"; oculta(false);
                }
                else
                {
                    lbResultado.Text = "Números de docentes localizados: " + dt.Rows.Count.ToString();
                    GridView1.DataSource = dt;
                    GridView1.DataBind();

                    for (int i = 0; i < GridView1.Rows.Count; i++)
                    {
                        GridViewRow fila = GridView1.Rows[i];
                        Label No = fila.FindControl("lbNo") as Label;
                        No.Text = (i + 1).ToString();
                    }
                    oculta(true);
                }
            }
            catch {
                Button2.Focus();
                }
        }

        protected void btNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("PagsDocentesContratos.aspx");
        }
    }
}