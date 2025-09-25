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
    public partial class pagsAlumnosInscritos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargaDatos();
            }
        }

        private void CargaDatos()
        {
            int anioActual = DateTime.Now.Year;
            int mesActual = DateTime.Now.Month;

            // 📌 Si es julio (7) o después, ya incluir el próximo año en la lista
            int anioMaximo = (mesActual >= 7) ? anioActual + 1 : anioActual;

            // Llenar dropdown de años
            ddAnio.Items.Clear();
            for (int x = 2000; x <= anioMaximo; x++)
                ddAnio.Items.Add(x.ToString());

            // Llenar dropdown de semestres
            ddSemestre.Items.Clear();
            ddSemestre.Items.Add("1");
            ddSemestre.Items.Add("2");

            // 📌 Selección automática
            if (mesActual <= 6)
            {
                // Enero-Junio → Ciclo 1 del año actual
                ddSemestre.SelectedIndex = 0;
                ddAnio.SelectedIndex = ddAnio.Items.Count - 1;
            }
            else if (mesActual == 7 || mesActual == 8)
            {
                // Julio y Agosto → todavía ciclo 2 del año actual
                ddSemestre.SelectedIndex = 1;
                ddAnio.SelectedIndex = ddAnio.Items.Count - 2;
            }
            else // Septiembre-Diciembre
            {
                
                ddSemestre.SelectedIndex = 0; // Ciclo 1
                ddAnio.SelectedIndex = ddAnio.Items.Count - 1; // Último año en lista (el próximo)
            }
        }



        protected void Button1_Click(object sender, EventArgs e)
        {
            int opcion = Convert.ToInt32(DropDownList1.SelectedValue.ToString());
            string anio = ddAnio.SelectedValue.ToString();
            string semestre = ddSemestre.SelectedValue.ToString();
            DataTable dt = new DataTable();
            string cadena = " select  [Nombre de la carrera],SemestreActual, seccion,count(*) as Alumnos " +
            "from vista_inscripcion where ano = " + ddAnio.Text + " and SemestreAnual = " + ddSemestre.Text +
            " group by[Nombre de la carrera], SemestreActual, seccion " +
            " order by[Nombre de la carrera], SemestreActual, seccion";
            
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
             Response.Redirect("pagsAlumnosInscritos.aspx");
        }
    }
}