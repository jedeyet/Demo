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
    public partial class PagsDocentesContratos : System.Web.UI.Page
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

        private void Listado(int opcion)
        {
            
            
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                int opcion = Convert.ToInt32(DropDownList1.SelectedValue.ToString());
                string anio = ddAnio.SelectedValue.ToString();
                string semestre = ddSemestre.SelectedValue.ToString();
                string cadena = "select codigo_catedratico, [Nombre y apellidos del catedrático], " +
                 "(select fechafirmado from db_owner.Contratos B where B.idcatedratico=A.Codigo_Catedratico and B.ano = " + anio +
                " And B.idfacultad=" + ddFacultad.SelectedValue.ToString() + ") as Firmado" +
                " from Vista_Asignacion_Curso_Profesor A where ano = " + anio + " And SemestreCursado= " + semestre +
                " And codigo_facultad = " + ddFacultad.SelectedValue.ToString() + " And [Nombre y apellidos del catedrático] Not Like 'equiv%' " +
                "and [Nombre y apellidos del catedrático] " +
                " not Like '%sufic%' group by [Codigo_Catedratico],[Nombre y apellidos del catedrático] " +
                " order by firmado desc, [Nombre y apellidos del catedrático] ";
               

                DataTable dt = new DataTable();
                dt.Clear();
                dt = Models.Conex.Consulta2(cadena, opcion);
                if (dt.Rows.Count == 0)
                {
                    lbResultado.Text = "No se localizan docentes"; oculta(false);
                }
                else
                {

                    DataTable Dt2 = new DataTable();
                    Dt2.Columns.Add(new DataColumn("Codigo", typeof(string)));
                    Dt2.Columns.Add(new DataColumn("Docente", typeof(string)));
                    Dt2.Columns.Add(new DataColumn("Fecha", typeof(string)));

                    DataRow dr;
                    for (int x = 0; x < dt.Rows.Count; x++)
                    {
                        string firmado = dt.Rows[x]["Firmado"].ToString();
                        if (DateTime.TryParse(firmado, out _))
                        {
                            dr = Dt2.NewRow();
                            dr["Codigo"] = dt.Rows[x]["codigo_catedratico"];
                            dr["Docente"] = dt.Rows[x]["Nombre y apellidos del catedrático"];
                            dr["Fecha"] = dt.Rows[x]["firmado"];
                            Dt2.Rows.Add(dr);
                        }
                    }


                   // 
                    GridView1.DataSource = Dt2;
                    GridView1.DataBind();
                    lbResultado.Text = "Números de docentes localizados: " + Dt2.Rows.Count.ToString();

                    //for (int i = 0; i < Dt2.Rows.count; i++)
                    //{
                    //    GridViewRow fila = GridView1.Rows[i];
                    //    Label No = fila.FindControl("lbNo") as Label;

                    //    No.Text = (i + 1).ToString();

                    //}
                    oculta(true);
                }
            }
            catch
            {
                Button2.Focus();
            }
        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            try
            {
                int opcion = Convert.ToInt32(DropDownList1.SelectedValue.ToString());
                string anio = ddAnio.SelectedValue.ToString();
                string semestre = ddSemestre.SelectedValue.ToString();
                string cadena = "select codigo_catedratico, [Nombre y apellidos del catedrático], " +
                 "(select fechafirmado from db_owner.Contratos B where B.idcatedratico=A.Codigo_Catedratico and B.ano = " + anio +
                " And B.idfacultad=" + ddFacultad.SelectedValue.ToString() + ") as Firmado" +
                " from Vista_Asignacion_Curso_Profesor A where ano = " + anio + " And SemestreCursado= " + semestre +
                " And codigo_facultad = " + ddFacultad.SelectedValue.ToString() + " And [Nombre y apellidos del catedrático] Not Like 'equiv%' " +
                "and [Nombre y apellidos del catedrático] " +
                " not Like '%sufic%' group by [Codigo_Catedratico],[Nombre y apellidos del catedrático] " +
                " order by firmado desc, [Nombre y apellidos del catedrático] ";


                DataTable dt = new DataTable();
                dt.Clear();
                dt = Models.Conex.Consulta2(cadena, opcion);
                if (dt.Rows.Count == 0)
                {
                    lbResultado.Text = "No se localizan docentes"; oculta(false);
                }
                else
                {

                    DataTable Dt3 = new DataTable();
                    Dt3.Columns.Add(new DataColumn("Codigo", typeof(string)));
                    Dt3.Columns.Add(new DataColumn("Docente", typeof(string)));
                    Dt3.Columns.Add(new DataColumn("Fecha", typeof(string)));

                    DataRow dr;
                    for (int x = 0; x < dt.Rows.Count; x++)
                    {
                        string firmado = dt.Rows[x]["Firmado"].ToString();
                        if (!DateTime.TryParse(firmado, out _))
                        {
                            dr = Dt3.NewRow();
                            dr["Codigo"] = dt.Rows[x]["codigo_catedratico"];
                            dr["Docente"] = dt.Rows[x]["Nombre y apellidos del catedrático"];
                            Dt3.Rows.Add(dr);
                        }
                    }

                    GridView1.DataSource = Dt3;
                    GridView1.DataBind();
                    lbResultado.Text = "Números de docentes localizados sin firma de contrato: " + Dt3.Rows.Count.ToString();
                    //for (int i = 0; i < Dt2.Rows.count; i++)
                    //{
                    //    GridViewRow fila = GridView1.Rows[i];
                    //    Label No = fila.FindControl("lbNo") as Label;

                    //    No.Text = (i + 1).ToString();

                    //}
                    oculta(true);
                }
            }
            catch
            {
                Button3.Focus();
            }
        }
        protected void btNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("PagsDocentesContratos.aspx");
        }

        
    }
}