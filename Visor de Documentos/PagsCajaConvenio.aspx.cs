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
    public partial class PagsCajaConvenio : System.Web.UI.Page
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
            ddTipo.Items.Add("Todos");
            ddTipo.Items.Add("Por alumno");            
            ddEstado.Items.Add("Cumplidos");
            ddEstado.Items.Add("Pendiente");
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
                cadena = "";
            }            
            return cadena;
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            int opcion = Convert.ToInt32(DropDownList1.SelectedValue.ToString());
            string anio = ddTipo.SelectedValue.ToString();
            string semestre = ddEstado.SelectedValue.ToString();
            DataTable dt = new DataTable();
            string cadena = "";
            switch (ddTipo.SelectedIndex)
            {
                case 0:
                    if (ddEstado.SelectedIndex == 0)
                        cadena = "SELECT carrera, carnet, Estudiante, Ingresado, Inicio, Concluye, numero_pagos, pagos_realizados, estado,  año, Cuota, [Teléfono(s)], Email, emailUMES, observacion  " +
                    "FROM View_InfoConvenioDePagos WHERE(estado = 'Cumplido') ORDER BY id_contrato_detalle";
                    else
                        cadena = "SELECT carrera, carnet, Estudiante, Ingresado, Inicio, Concluye, numero_pagos, pagos_realizados, estado,  año, Cuota, [Teléfono(s)], Email, emailUMES, observacion  " +
                    "FROM View_InfoConvenioDePagos WHERE(estado = 'Pendiente') ORDER BY id_contrato_detalle";
                    break;
                    case 1:
                    cadena = "SELECT carrera, carnet, Estudiante, Ingresado, Inicio, Concluye, numero_pagos, pagos_realizados, estado,  año, Cuota, [Teléfono(s)], Email, emailUMES, observacion  " +
                    "FROM View_InfoConvenioDePagos WHERE(carnet = '"+ prepararParaSql(txCarnet.Text) + "') ORDER BY id_contrato_detalle";
                    break;                
            }            
                
            
            dt = Models.Conex.ConsultaCaja(cadena, opcion);
            if (dt.Rows.Count == 0)
            {
                lbResultado.Text = "No se localizan convenios para el carné "+txCarnet.Text; oculta(false);
            }
            else
            {
                lbResultado.Text = "Números de convenios localizados: " + dt.Rows.Count.ToString();
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
            Response.Redirect("PagsCajaConvenio.aspx");
        }

        protected void ddTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddTipo.SelectedItem != null)
            {
                if(ddTipo.SelectedIndex== 1)
                {
                    ddEstado.Enabled = false;
                    this.txCarnet.Enabled = true;
                }
                else
                {
                    ddEstado.Enabled = true;
                    this.txCarnet.Enabled = false;
                }                    
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

        protected void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            int opcion = Convert.ToInt32(DropDownList1.SelectedValue.ToString());            
            string carne = ListBox1.SelectedValue.ToString();
            DataTable dt = new DataTable();
            string cadena = "";
            cadena = "SELECT carrera, carnet, Estudiante, Ingresado, Inicio, Concluye, numero_pagos, pagos_realizados, estado,  año, Cuota, [Teléfono(s)], Email, emailUMES, observacion  " +
                    "FROM View_InfoConvenioDePagos WHERE(carnet = '" + prepararParaSql(carne) + "') ORDER BY id_contrato_detalle";
            


            dt = Models.Conex.ConsultaCaja(cadena, opcion);
            if (dt.Rows.Count == 0)
            {
                lbResultado.Text = "No se localizan convenios para el carné " + txCarnet.Text; oculta(false);
            }
            else
            {
                lbResultado.Text = "Números de convenios localizados: " + dt.Rows.Count.ToString();
                GridView1.DataSource = dt;
                GridView1.DataBind();
                oculta(true);
            }

        }
    }

}