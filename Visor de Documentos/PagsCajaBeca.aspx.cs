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
    public partial class PagsCajaBeca : System.Web.UI.Page
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
        }

        private void CargaDatos()
        {
            ddTipo.Items.Add("Pregrado");
            ddTipo.Items.Add("Maestría");
            ddTipo.Items.Add("Por alumno");
            RadioButtonListCiclo.Items.Add("1");
            RadioButtonListCiclo.Items.Add("2");
            RadioButtonListCiclo.Items.Add("3");
            RadioButtonListCiclo.Items.Add("4");
            RadioButtonListCiclo.ClearSelection();
            RadioButtonListCiclo.Items[0].Enabled = true;
            RadioButtonListCiclo.Items[1].Enabled = true;
            RadioButtonListCiclo.Items[2].Enabled = false;
            RadioButtonListCiclo.Items[3].Enabled = false;
        }

        private string prepararParaSql(string _cadena)
        {
            string cadena = "";
            int numero = 0;
            try
            {
                numero = Convert.ToInt32(_cadena);
                cadena = _cadena;
            }
            catch (Exception err)
            {
                cadena = DateTime.Now.Year.ToString();
                this.txAnio.Text = cadena;
            }            
            return cadena;
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            int opcion = Convert.ToInt32(DropDownList1.SelectedValue.ToString());
            string anio = prepararParaSql(txAnio.Text);
            string semestre = RadioButtonListCiclo.SelectedValue; 
            DataTable dt = new DataTable();
            string cadena = "";       
            //if(this.DropDownList1.SelectedItem.Text.ToUpper().Contains("QUETZALTENANGO"))
            {
                switch (ddTipo.SelectedIndex)
                {
                    case 0:
                        if (semestre == "")
                        {
                            RadioButtonListCiclo.Items[0].Selected = true;
                            semestre = RadioButtonListCiclo.SelectedValue;
                        }
                        cadena = "SELECT Año, Codigo_Carrera, Codigo_facultad, [Nombre de la carrera], Nombre_Completo, [Número de carné], Seccion, SemestreActual, facultad, nombre_cuota as TipoBeca " +
                        "FROM dbo.f_ListadoBecas(" + anio + ", " + semestre + ") AS f_ListadoBecas_1 WHERE ([Nombre de la carrera] NOT LIKE 'Maestr%') order by facultad, [nombre de la carrera], semestreActual";
                        break;
                    case 1:
                        if (semestre == "")
                        {
                            RadioButtonListCiclo.Items[0].Selected = true;
                            semestre = RadioButtonListCiclo.SelectedValue;
                        }
                        cadena = "SELECT Año, Codigo_Carrera, Codigo_facultad, [Nombre de la carrera], Nombre_Completo, [Número de carné], Seccion, SemestreActual, facultad, nombre_cuota as TipoBeca " +
                        "FROM dbo.f_ListadoBecas(" + anio + ", " + semestre + ") AS f_ListadoBecas_1 WHERE ([Nombre de la carrera] LIKE 'Maestr%') order by facultad, [nombre de la carrera], semestreActual";
                        break;
                    case 2:
                        cadena = "SELECT [Número de carné], Nombre_Completo, SemestreActual, Año, Seccion, Codigo_Carrera, [Nombre de la carrera], Codigo_facultad, facultad, nombre_cuota as TipoBeca " +
                            "FROM dbo.f_ListadoBecas(" + anio + ", 1) AS f_ListadoBecas_4 WHERE([Número de carné] = '" + prepararParaSql(txCarnet.Text) + "') UNION ALL " +
                            "SELECT [Número de carné], Nombre_Completo, SemestreActual, Año, Seccion, Codigo_Carrera, [Nombre de la carrera], Codigo_facultad, facultad, nombre_cuota as TipoBeca " +
                            "FROM dbo.f_ListadoBecas(" + anio + ", 2) AS f_ListadoBecas_3 WHERE([Número de carné] = '" + prepararParaSql(txCarnet.Text) + "') UNION ALL " +
                            "SELECT [Número de carné], Nombre_Completo, SemestreActual, Año, Seccion, Codigo_Carrera, [Nombre de la carrera], Codigo_facultad, facultad, nombre_cuota as TipoBeca " +
                            "FROM dbo.f_ListadoBecas(" + anio + ", 3) AS f_ListadoBecas_2 WHERE([Número de carné] = '" + prepararParaSql(txCarnet.Text) + "') UNION ALL " +
                            "SELECT [Número de carné], Nombre_Completo, SemestreActual, Año, Seccion, Codigo_Carrera, [Nombre de la carrera], Codigo_facultad, facultad, nombre_cuota as TipoBeca " +
                            "FROM dbo.f_ListadoBecas(" + anio + ", 4) AS f_ListadoBecas_1 WHERE([Número de carné] = '" + prepararParaSql(txCarnet.Text) + "') ORDER BY SemestreActual";
                        break;
                }

                dt = Models.Conex.ConsultaCaja(cadena, opcion);
            }
            
            if (dt.Rows.Count == 0)
            {
                lbResultado.Text = "No se localizan datos para el carné "+txCarnet.Text; oculta(false);
            }
            else
            {
                lbResultado.Text = "Números de becas localizados: " + dt.Rows.Count.ToString();
                GridView1.DataSource = dt;
                GridView1.DataBind();
                oculta(true);
            }
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

        protected void btNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("PagsCajaBeca.aspx");
        }

        protected void ddTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddTipo.SelectedItem != null)
            {
                if(ddTipo.SelectedIndex==0)
                {
                    RadioButtonListCiclo.Enabled = true;
                    RadioButtonListCiclo.ClearSelection();
                    RadioButtonListCiclo.Items[0].Enabled= true;
                    RadioButtonListCiclo.Items[1].Enabled = true;
                    RadioButtonListCiclo.Items[2].Enabled = false;
                    RadioButtonListCiclo.Items[3].Enabled = false;
                    this.txCarnet.Enabled = false;
                    this.txCarnet.Text = "";
                    this.TextBox1.Enabled = false;
                    this.ListBox1.Enabled=false;
                    this.ListBox1.Items.Clear();
                    this.Button2.Enabled = false;
                    this.GridView1.DataSource = null;
                    this.GridView1.DataBind();
                    this.lbResultado.Text = "";
                }
                else
                {
                    if (ddTipo.SelectedIndex == 1)
                    {
                        RadioButtonListCiclo.Enabled = true;
                        RadioButtonListCiclo.ClearSelection();
                        RadioButtonListCiclo.Items[0].Enabled = true;
                        RadioButtonListCiclo.Items[1].Enabled = true;
                        RadioButtonListCiclo.Items[2].Enabled = true;
                        RadioButtonListCiclo.Items[3].Enabled = true;
                        this.txCarnet.Enabled = false;
                        this.txCarnet.Text = "";
                        this.TextBox1.Enabled = false;
                        this.ListBox1.Enabled = false;
                        this.ListBox1.Items.Clear();
                        this.Button2.Enabled = false;
                        this.GridView1.DataSource = null;
                        this.GridView1.DataBind();
                        this.lbResultado.Text = "";
                    }
                    else
                    {
                        RadioButtonListCiclo.Enabled = false;
                        RadioButtonListCiclo.ClearSelection();
                        this.txCarnet.Enabled = true;
                        this.txCarnet.Text = "";
                        this.TextBox1.Enabled = true;
                        this.ListBox1.Enabled = true;
                        this.ListBox1.Items.Clear();
                        this.Button2.Enabled = true;
                        this.GridView1.DataSource = null;
                        this.GridView1.DataBind();
                        this.lbResultado.Text = "";
                    }
                }
                
            }
        }

        protected void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string anio = prepararParaSql(txAnio.Text);
            int opcion = Convert.ToInt32(DropDownList1.SelectedValue.ToString());
            string carne = ListBox1.SelectedValue.ToString();
            DataTable dt = new DataTable();
            string cadena = "SELECT [Número de carné], Nombre_Completo, SemestreActual, Año, Seccion, Codigo_Carrera, [Nombre de la carrera], Codigo_facultad, facultad, nombre_cuota as TipoBeca " +
                            "FROM dbo.f_ListadoBecas(" + anio + ", 1) AS f_ListadoBecas_4 WHERE([Número de carné] = '" + carne + "') UNION ALL " +
                            "SELECT [Número de carné], Nombre_Completo, SemestreActual, Año, Seccion, Codigo_Carrera, [Nombre de la carrera], Codigo_facultad, facultad, nombre_cuota as TipoBeca " +
                            "FROM dbo.f_ListadoBecas(" + anio + ", 2) AS f_ListadoBecas_3 WHERE([Número de carné] = '" + carne + "') UNION ALL " +
                            "SELECT [Número de carné], Nombre_Completo, SemestreActual, Año, Seccion, Codigo_Carrera, [Nombre de la carrera], Codigo_facultad, facultad, nombre_cuota as TipoBeca " +
                            "FROM dbo.f_ListadoBecas(" + anio + ", 3) AS f_ListadoBecas_2 WHERE([Número de carné] = '" + carne + "') UNION ALL " +
                            "SELECT [Número de carné], Nombre_Completo, SemestreActual, Año, Seccion, Codigo_Carrera, [Nombre de la carrera], Codigo_facultad, facultad, nombre_cuota as TipoBeca " +
                            "FROM dbo.f_ListadoBecas(" + anio + ", 4) AS f_ListadoBecas_1 WHERE([Número de carné] = '" + carne + "') ORDER BY SemestreActual";

            /*string dato = "'" + ListBox1.SelectedItem.ToString() + "'";
            int opcion = Convert.ToInt32(DropDownList1.SelectedValue.ToString());
            System.Web.HttpContext.Current.Session["opcion"] = opcion;
            System.Web.HttpContext.Current.Session["dato"] = dato;
            System.Web.HttpContext.Current.Session["nombre"] = ListBox1.SelectedItem.ToString();*/

            dt = Models.Conex.ConsultaCaja(cadena, opcion);
            if (dt.Rows.Count == 0)
            {
                lbResultado.Text = "No se localizan datos para el carné " + txCarnet.Text; oculta(false);
            }
            else
            {
                lbResultado.Text = "Números de becas localizados: " + dt.Rows.Count.ToString();
                GridView1.DataSource = dt;
                GridView1.DataBind();
                oculta(true);
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (TextBox1.Text.Length > 0)
            {
                bool nopermitido = false;
                for (int c = 1; c < TextBox1.Text.Length; c++)
                {
                    string caracter = TextBox1.Text[c].ToString();
                    if (caracter == "=" || caracter == "'" || caracter == "-")
                        nopermitido = true;
                }
                if (nopermitido)
                { TextBox1.Text = ""; }
                else
                {
                    string cadena = "Select * from alumno where Nombre_Completo COLLATE Latin1_General_CI_AI LIKE '%" +
                        TextBox1.Text + "%' order by Nombre_Completo";
                    int opcion = Convert.ToInt32(DropDownList1.SelectedValue.ToString());
                    DataTable dt = Models.Conex.Consulta2(cadena, opcion);
                    if (dt.Rows.Count == 0)
                        lbResultado.Text = "No se localizan estudiantes con estas credenciales";
                    else
                    {
                        ListBox1.Items.Clear();
                        ListBox1.Items.Add("Seleccione un estudiante");
                        lbResultado.Text = "Números de estudiantes localizados: " + dt.Rows.Count.ToString();
                        ListBox1.DataValueField = "número de carné";
                        ListBox1.DataTextField = "nombre_completo";
                        ListBox1.DataSource = dt;
                        ListBox1.DataBind();

                    }
                }
            }
        }
    }

}