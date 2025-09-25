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
    public partial class PagsDocentesReporteDetallado : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
                if (!Page.IsPostBack)
                {
                    CargaSedes();
                    CargaDatos();
                    CargaFacultades();
                    CargaCarreras();
                }
            
            
        }
        string usuario = ""; int Nivel = 0;
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
        private void CargaSedes()
        {
            usuario = System.Web.HttpContext.Current.Session["usuario"].ToString().Trim();
            DropDownList1.Items.Clear();
            string cad = "select * from AdminsSedesPermisos where usuario='" + usuario + "'";
            DataTable dt = new DataTable();
            dt = Models.Conex.Consulta(cad);
            System.Web.HttpContext.Current.Session["Nivel"] = Convert.ToInt16(dt.Rows[0]["Nivel"].ToString());
            //Label5.Text = System.Web.HttpContext.Current.Session["Nivel"].ToString();
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
                cadena = "select [id Carrera], [Nombre de la carrera] from carrera order by  [Nombre de la carrera]";
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

        private void CargaDatos()
        {
            for (int x = 2002; x <= DateTime.Now.Year; x++)
                ddAnio.Items.Add(x.ToString());
            ddAnio.SelectedIndex = ddAnio.Items.Count - 1;
            ddSemestre.Items.Add("1");
            ddSemestre.Items.Add("2");
            if (DateTime.Now.Month >= 7) ddSemestre.SelectedIndex = 1; else ddSemestre.SelectedIndex = 0;
        }




        protected void ddFacultad_SelectedIndexChanged(object sender, EventArgs e)
        {
            oculta(false); lbResultado.Text = "";
        }

        private void oculta(bool opcion)
        {
            imgbutExc.Visible = opcion;
            GridView1.Visible = opcion;

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
            CargaFacultades();
            CargaCarreras();
            oculta(false);
            lbResultado.Text = "";
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


        protected void Button1_Click(object sender, EventArgs e)
        {
            
            VerCuadricula();
        }

        private void VerCuadricula()
        {
            try
            {
                int opcion = Convert.ToInt32(DropDownList1.SelectedValue.ToString());
                string anio = ddAnio.SelectedValue.ToString();
                string semestre = ddSemestre.SelectedValue.ToString();

                string dato = "";
                if (RadioButtonList1.SelectedIndex == 0)
                    dato = " and facultad='" + ddFacultad.SelectedItem.Text + "' ";
                if (RadioButtonList1.SelectedIndex == 1)
                    dato = " and [Nombre de la carrera]='" + ddCarrera.SelectedItem.Text + "' ";

                string cadena = "";
                cadena = "Select Codigo_Catedratico as Codigo, [Nombre y apellidos del catedrático] as Docente, ";
                if (chDPI.Checked) cadena += "(select  Cedula from catedraticos as T2 where T2.Codigo_Catedratico= V1.codigo_catedratico) As DPI,";
                if (chCel.Checked) cadena += "(select  Celular from catedraticos as T2 where T2.Codigo_Catedratico = V1.codigo_catedratico) As Celular,";
                if (chema.Checked) cadena += "(select  Email from catedraticos as T2 where T2.Codigo_Catedratico= V1.codigo_catedratico) As email,";
                if (chpai.Checked) cadena += "(select  Pais from catedraticos as T2 where T2.Codigo_Catedratico= V1.codigo_catedratico) As País,";
                if (chnac.Checked) cadena += "(select  convert(nvarchar,fecha_nac,103) from Catedraticos as T2 where T2.Codigo_Catedratico= V1.codigo_catedratico) As Nacimiento,";
                if (chnit.Checked) cadena += "(select  nit from catedraticos as T2 where T2.Codigo_Catedratico= V1.codigo_catedratico) As NIT,";
                if (chemi.Checked) cadena += "(select  Correo_Institucional from catedraticos as T2 where T2.Codigo_Catedratico= V1.codigo_catedratico) As emailInstitucional,";
                if (chemt.Checked) cadena += "(select  Correo_Institucional from catedraticos as T2 where T2.Codigo_Catedratico= V1.codigo_catedratico) As emailInstitucional,";
                if (chtel.Checked) cadena += "(select  telefono from Catedraticosempresas as T2 where T2.IdCatedratico= V1.codigo_catedratico) As Teléfono,";
                if (chdir.Checked) cadena += "(select  direccion from Catedraticosempresas as T2 where T2.IdCatedratico= V1.codigo_catedratico) As Direccion,";
                if (chemp.Checked) cadena += "(select  empresa from Catedraticosempresas as T2 where T2.IdCatedratico= V1.codigo_catedratico) As Empresa,";
                if (chigss.Checked) cadena += "(select  IGSS from Catedraticosempresas as T2 where T2.IdCatedratico= V1.codigo_catedratico) As IGSS,";
                if (chcol.Checked) cadena += "(select  Nocolegiado from CatedraticosColegios as T1 where T1.IdCatedratico= V1.codigo_catedratico) As Colegiado,";
                if (chven.Checked) cadena += "(select  convert(nvarchar,vencimientoColegiado,103) from CatedraticosColegios as T1 where T1.IdCatedratico= V1.codigo_catedratico) As Vencimiento,";

                cadena = cadena.Substring(0, cadena.Length - 1);

                string filtro = " from Vista_Asignacion_Curso_Profesor as V1 where [Nombre y apellidos del catedrático] not like 'equiv%' " +
                    "and ano=" + anio.ToString() + " and Semestrecursado=" + semestre + dato +
                    " group by Codigo_Catedratico,[Nombre y apellidos del catedrático] order by Docente";
                cadena += filtro;
                TextBox1.Text = cadena;

                DataTable dt = new DataTable();

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
            catch
            {
                Button1.Focus();
            }

        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RadioButtonList1.SelectedIndex==0)
            {
                Label6.Visible = false; ddCarrera.Visible = false;
                Label4.Visible = true; ddFacultad.Visible = true;
                
            }
            else if (RadioButtonList1.SelectedIndex==1)
            {
                Label6.Visible = true; ddCarrera.Visible = true;
                Label4.Visible = false; ddFacultad.Visible = false;
            }

        }

        protected void btNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("PagsDocentesReporteDetallado.aspx");
        }
    }
}