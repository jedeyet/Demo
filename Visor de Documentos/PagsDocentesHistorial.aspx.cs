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
    public partial class PagsDocentesHistorial : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargaSedes();
            }
        }
        string usuario = "";  
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            visualiza(false);imgbutExc.Visible = false;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            cargaDocentes();
            visualiza(true);
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
        private void cargaDocentes()
        {
            int opcion = Convert.ToInt32(DropDownList1.SelectedValue.ToString());
            string cadena = "select Codigo_Catedratico, [Nombre y apellidos del catedrático] from catedraticos  where " +
            "[nombre y apellidos del catedrático] not like 'equiv%' and [nombre y apellidos del catedrático] not like 'Aasig%'  " +
            " and [nombre y apellidos del catedrático] not like '%ordina%'  order by  [Nombre y apellidos del catedrático]";
            DataTable dt = new DataTable();
            dt = Models.Conex.Consulta2(cadena,opcion);
            ddDocente.DataValueField = "codigo_catedratico";
            ddDocente.DataTextField = "nombre y apellidos del catedrático";
            ddDocente.DataSource = dt;
            ddDocente.DataBind();
            lbResultado.Text = "Docentes localizados: " + dt.Rows.Count.ToString();

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            int opcion = Convert.ToInt32(DropDownList1.SelectedValue.ToString());
            string cadena = "select Codigo_Asignacion_Curso_Profesor as Codigo,asignatura, Numero_acta as Acta, " +
            " ano, semestre as Ciclo, [Nombre de la carrera] as Carrera from Vista_Asignacion_Curso_Profesor " +
            " where  Codigo_Catedratico = " + ddDocente.SelectedValue.ToString() + " order by ano,ciclo,asignatura";
            DataTable dt = new DataTable();
            dt = Models.Conex.Consulta2(cadena, opcion);
            if (dt.Rows.Count > 0)
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();
                visualiza(true);
                imgbutExc.Visible = true;
                lbResultado.Text = "No. de cursos localizados: " + dt.Rows.Count.ToString();
            }
            else
            { 
                imgbutExc.Visible=false;
                //visualiza(false); lbResultado.Text = "No se localizan coincidencias"; lbResultado.Visible = true; 
            }
        
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

        protected void ddDocente_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void visualiza (bool vis)
        {
            lbResultado.Visible = vis;  ddDocente.Visible = vis;    
            GridView1.Visible = vis;  Button2.Visible = vis;    
             
        }

        protected void btNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("PagsDocentesHistorial.aspx");
        }
    }
}