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
    public partial class PagsAlumnosReporteDetallado : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargaSedes();
                CargaDatos();
                CargaFacultades();
                CargaCarreras();
                carga();
            }
        }
        string usuario = ""; int Nivel = 0;
        private void Nivelin()
        {
            int opcion = Convert.ToInt32(DropDownList1.SelectedValue.ToString());
            string usu = System.Web.HttpContext.Current.Session["usuario"].ToString().Trim();
            string cad = "select * from AdminsSedesPermisos where usuario='" + usu + "' and idsede=" + opcion.ToString();
            //Label5.Text = cad;
            DataTable dt = new DataTable();
            dt = Models.Conex.Consulta(cad);
            lbNivel.Text = dt.Rows[0]["Nivel"].ToString();
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
            Nivelin();
            if (Convert.ToInt16(lbNivel.Text)!=0)
            {
                RadioButtonList1.Items[0].Enabled = false;
            }
        }


        private void carga()
        {

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

            int opcion = Convert.ToInt32(DropDownList1.SelectedValue.ToString());
            string anio = ddAnio.SelectedValue.ToString();
            string semestre = ddSemestre.SelectedValue.ToString();

            string dato = "";
            if (RadioButtonList1.SelectedIndex == 0)
                dato = " and facultad='" + ddFacultad.SelectedItem.Text + "' ";
            if (RadioButtonList1.SelectedIndex == 1)
                dato = " and [Carrera]='" + ddCarrera.SelectedItem.Text + "' ";

            string cadena = "";
            cadena = "Select  Carnet, Estudiante,";
            if (chDPI.Checked) cadena += "(select  DPI from alumno as T2 where T2.[número de carné]= V1.carnet) As DPI,";
            if (Chnaci.Checked) cadena += "(select  convert(nvarchar,nacimiento,103) from alumno as T2 where T2.[número de carné]= V1.carnet) As Nacimiento,";
            if (chDir.Checked) cadena += "(select  Dirección from alumno as T2 where T2.[número de carné]= V1.carnet) As Dirección,";
            if (chtel.Checked) cadena += "(select  Teléfono from alumno as T2 where T2.[número de carné]= V1.carnet) As Teléfono,";
            if (chema.Checked) cadena += "(select  email from alumno as T2 where T2.[número de carné]= V1.carnet) As email,";
            if (chnac.Checked) cadena += "(select  nacionalidad from alumno as T2 where T2.[número de carné]= V1.carnet) As Nacionalidad,";
            if (chusu.Checked) cadena += "(select  usuario from alumno as T2 where T2.[número de carné]= V1.carnet) As usuario,";
            if (chpen.Checked) cadena += "(select  pensum from alumno as T2 where T2.[número de carné]= V1.carnet) As Pensum,";
            if (Chjor.Checked) cadena += "(select  jornada from alumno as T2 where T2.[número de carné]= V1.carnet) As Jornada,";
            if (chdep.Checked) cadena += "(select  departamento from Guate_Dep as TD where TD.Id_Depto= V1.Departamento) As Departamento,";
            if (chmun.Checked) cadena += "(select  Municipio from Guate_Muns as TM where TM.Id_muns= V1.ciudad) As Municipio,";
            if (chcom.Checked) cadena += "(select  coumnlingDescripcion from inecomunidadling as TI where TI.comunlingid= V1.comunlingid) As Comunidad,";
            //Alumno_Emergencia
            if (chcon.Checked) cadena += "(select  contacto1 from alumno_emergencia as TE where TE.numero_carne= V1.Carnet) As Contacto,";
            if (chtelc.Checked) cadena += "(select  telefono1 from alumno_emergencia as TE where TE.numero_carne= V1.Carnet) As Tel_Contacto,";
            if (chenf.Checked) cadena += "(select  alergia from alumno_emergencia as TE where TE.numero_carne= V1.Carnet) As Enfermedades,";
            if (chmed.Checked) cadena += "(select  medicamentos from alumno_emergencia as TE where TE.numero_carne= V1.Carnet) As Medicamentos,";

            cadena = cadena.Substring(0, cadena.Length - 1);

            string filtro = " from Vista_alumno as V1 " + 
                " where año=" + anio.ToString() + " and Semestre=" + semestre + dato +
                " order by Estudiante";
            cadena += filtro;
            TextBox1.Text = cadena;

            DataTable dt = new DataTable();

            dt = Models.Conex.Consulta2(cadena, opcion);
            if (dt.Rows.Count == 0)
            {
                lbResultado.Text = "No se localizan alumnos activos"; oculta(false);
            }
            else
            {
                lbResultado.Text = "Números de alumnos localizados: " + dt.Rows.Count.ToString();
                GridView1.DataSource = dt;
                GridView1.DataBind();
                oculta(true);
            }

        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RadioButtonList1.SelectedIndex == 0)
            {
                Label6.Visible = false; ddCarrera.Visible = false;
                Label4.Visible = true; ddFacultad.Visible = true;
                CargaFacultades();

            }
            else if (RadioButtonList1.SelectedIndex == 1)
            {
                Label6.Visible = true; ddCarrera.Visible = true;
                Label4.Visible = false; ddFacultad.Visible = false;
                CargaCarreras();
            }

        }

        
       protected void btNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("PagsAlumnosReporteDetallado.aspx");
        }
    }
}