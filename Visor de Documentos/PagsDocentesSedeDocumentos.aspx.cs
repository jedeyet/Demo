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
    public partial class PagsDocentesSedeDocumentos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargaSedes();
                
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
       


        protected void Button1_Click(object sender, EventArgs e)
        {
            filtrar("facultad,[Nombre y apellidos del catedrático]");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            filtrar("[Nombre y apellidos del catedrático]");
        }

        private void filtrar(string orden)
        {
            int opcion = Convert.ToInt32(DropDownList1.SelectedValue.ToString());

            DataTable dt = new DataTable();
            string cadena = "select [Codigo_Catedratico],[Nombre y apellidos del catedrático], " +
                "(select tipodocumento from CatedraticosDocumentos as T2 where t2.idCatedratico = t1.Codigo_Catedratico and t2.TipoDocumento like '%Apen%') as A_Penales, " +
                "(select tipodocumento from CatedraticosDocumentos as T2 where t2.idCatedratico = t1.Codigo_Catedratico and t2.TipoDocumento like '%Apol%') as A_Policiacos, " +
                "(select tipodocumento from CatedraticosDocumentos as T2 where t2.idCatedratico = t1.Codigo_Catedratico and t2.TipoDocumento like '%cole%') as Colegiado, " +
                "(select tipodocumento from CatedraticosDocumentos as T2 where t2.idCatedratico = t1.Codigo_Catedratico and t2.TipoDocumento like '%Cons%') as C_Laborales, " +
                "(select tipodocumento from CatedraticosDocumentos as T2 where t2.idCatedratico = t1.Codigo_Catedratico and t2.TipoDocumento like '%curr%') as Curriculum, " +
                "(select tipodocumento from CatedraticosDocumentos as T2 where t2.idCatedratico = t1.Codigo_Catedratico and t2.TipoDocumento like '%DPI%') as DPI, " +
                "(select tipodocumento from CatedraticosDocumentos as T2 where t2.idCatedratico = t1.Codigo_Catedratico and t2.TipoDocumento like '%igss%') as IGSS, " +
                "(select tipodocumento from CatedraticosDocumentos as T2 where t2.idCatedratico = t1.Codigo_Catedratico and t2.TipoDocumento like '%rtu%') as RTU, " +
                "(select tipodocumento from CatedraticosDocumentos as T2 where t2.idCatedratico = t1.Codigo_Catedratico and t2.TipoDocumento like '%tsal%') as Salud," +
                "(select tipodocumento from CatedraticosDocumentos as T2 where t2.idCatedratico = t1.Codigo_Catedratico and t2.TipoDocumento like '%Tit%') as Títulos, " +
                "facultad as Facultad from Vista_Asignacion_Curso_Profesor as T1  " +
                "where ano = " + DateTime.Now.Year.ToString() + " and[Nombre y apellidos del catedrático] not like 'aas%' and[Nombre y apellidos del catedrático] not like 'equiv%'  " +
                "group by[Codigo_Catedratico],[Nombre y apellidos del catedrático],facultad " +
                "order by " + orden;

            dt = Models.Conex.Consulta2(cadena, opcion);
            if (dt.Rows.Count == 0)
            {
                lbResultado.Text = "No se localizan docentes activos"; oculta(false);
            }
            else
            {
                lbResultado.Text = "Números de docentes localizados: " + dt.Rows.Count.ToString();
                GridView1.DataSource = dt;
                GridView1.DataBind();
                oculta(true);
            }
        }





        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
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

        protected void btNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("PagsDocentesSedeDocumentos.aspx");
        }
    }

}