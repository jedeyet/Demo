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
    public partial class PagsAlumnosRetirados : System.Web.UI.Page
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
            for (int x = 2000; x <= DateTime.Now.Year ; x++)
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
            string cadena = " select  [número de carné] as Carnet, Nombre_Completo As Alumno,[Nombre de la carrera] As Carrera,SemestreActual as Ciclo, convert(nvarchar,Fecha_Retiro,103) as fecha, Observaciones " +
            " from vista_inscripcion where ano = " + ddAnio.Text + " and SemestreAnual = " + ddSemestre.Text +
            " and retirado = 'SI' order by nombre_completo";
            

            dt = Models.Conex.Consulta2(cadena, opcion);
            if (dt.Rows.Count == 0)
            {
                lbResultado.Text = "No se localizan alumnos retirados";
                lbResultado.Visible = true; GridView1.Visible = false;
                imgbutExc.Visible = false;
            }
            else
            {
                imgbutExc.Visible = true;
                GridView1.DataSource = dt;
                GridView1.DataBind();
                cadena = "select * from vista_inscripcion where retirado = 'SI' and ano = " + ddAnio.Text + " and SemestreAnual = " + ddSemestre.Text ;
                dt = Models.Conex.Consulta2(cadena, opcion);
                lbResultado.Visible = true; GridView1.Visible = true; lbResultado.Text = "Alumnos retirados: " + dt.Rows.Count.ToString();
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
            Response.Redirect("PagsAlumnosRetirados.aspx");
        }
    }
}