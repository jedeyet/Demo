using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web.UI.HtmlControls;


namespace Visor_de_Documentos
{
    public partial class PagsPagos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                limpia443();
                txCarnet.Focus();
                CargaSedes(); CargaDatos(); 
            }
        }
        string usuario = "";
        private void CargaDatos()
        {
            Nivelin();
        }
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



        private void limpia443()
        {
            lbConvenio0.Visible = false; lbConvenio1.Visible = false; lbConvenio2.Visible = false;
            lbMonto1.Visible = false;  lbMonto2.Visible = false;    lbTotal.Visible = false;
            gridEstado.DataSource = null;  GridConvenio.DataSource = null; gridEstado.Visible = false;  GridConvenio.Visible = false;
            GridView1.Visible = false;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            limpia443();
            if (txCarnet.Text.Length > 0)
            {
                bool nopermitido = false;
                for (int c = 1; c < txCarnet.Text.Length; c++)
                {
                    string caracter = txCarnet.Text[c].ToString();
                    if (caracter == "=" || caracter == "'" || caracter == "-")
                        nopermitido = true;
                }
                if (nopermitido)
                { txCarnet.Text = ""; }
                else
                {
                    int opcion = Convert.ToInt32(DropDownList1.SelectedValue.ToString());
                    DataTable dt = new DataTable();
                    string cadena = "SELECT  descripcion AS Descripción, CONVERT(nvarchar, fecha_recibo, 103) " +
                        "AS Fecha, anio_pago AS Año, pago AS Monto, nombre_mes AS Mes, iniciales_establecimiento AS " +
                        "Establecimiento, recibo_Numero as NumeroRecibo FROM VerHistorial WHERE carnet = '" + txCarnet.Text + "' " +
                        "ORDER BY YEAR(fecha_recibo) DESC, mes_pago DESC, Descripción";

                    dt = Models.Conex.ConsultaCaja(cadena, opcion);
                    if (dt.Rows.Count == 0) { lbResultado.Text = "No se localizan pagos con este carnet"; lbnombre.Text = ""; }
                    else
                    {

                        Nivelin();
                        bool encontrado = Carreras(opcion);
                        if (Convert.ToInt16(lbNivel.Text) == 0) encontrado = true;

                        if (encontrado == false)
                        { lbResultado.Text = "No tiene autorización a ver esta información"; }
                        else
                        {

                            lbResultado.Text = "Números de pagos localizados: " + dt.Rows.Count.ToString();
                            GridView1.DataSource = dt;
                            GridView1.DataBind();
                            GridView1.Visible = true;

                            DataTable dtnombre;
                            string nom = "select  [número de carné], Nombre_completo + ' ' + [nombre de la carrera] As nombre_completo from VistaAlumnosCarrera where [número de carné]='" + txCarnet.Text + "'";
                            dtnombre = Models.Conex.Consulta2(nom, opcion);
                            lbnombre.Text = dtnombre.Rows[0]["nombre_completo"].ToString();

                            int semi = 1; if (DateTime.Now.Month >= 7) semi = 2;
                            SqlCommand cmd = new SqlCommand();
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "Proc_CalculaCuota";
                            cmd.Connection = Models.Conex.conexmoney(opcion);
                            cmd.Parameters.AddWithValue("@carnet", txCarnet.Text);
                            cmd.Parameters.AddWithValue("@semestre_anual", semi);
                            cmd.Parameters.AddWithValue("@anio", DateTime.Now.Year);
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            DataTable dtpagos = new DataTable();
                            da.Fill(dtpagos);

                            SqlCommand cmd1 = new SqlCommand();
                            cmd1.CommandType = CommandType.StoredProcedure;
                            cmd1.CommandText = "Proc_Convenio";
                            cmd1.Connection = Models.Conex.conexmoney(opcion);
                            cmd1.Parameters.AddWithValue("@carnet", txCarnet.Text);
                            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                            DataTable dtpagos1 = new DataTable();
                            da1.Fill(dtpagos1);
                            double monto1 = 0; double monto2 = 0;
                            if (dtpagos.Rows.Count > 0)
                            {
                                gridEstado.DataSource = dtpagos; gridEstado.DataBind(); gridEstado.Visible = true;

                                //for (int c = 0; c < gridEstado.Rows.Count; c++)
                                //{
                                //    GridViewRow fila = gridEstado.Rows[c];
                                //    var cuota = fila.FindControl("lbCuota555") as Label;
                                //    monto1 += Convert.ToDouble(cuota.Text);
                                //}

                                foreach (GridViewRow row in gridEstado.Rows)
                                {

                                    //    if (row.Cells[2].Text.Length == 1)
                                    //        mon = 0;
                                    //else if (Convert.ToDouble(row.Cells[2].Text)>0)
                                    monto1 += Convert.ToDouble(row.Cells[2].Text);
                                }
                                if (monto1 > 0)
                                {
                                    lbMonto1.Text = monto1.ToString("C"); lbMonto1.Visible = true; lbConvenio0.Visible = true;
                                }

                            }
                            else
                            { gridEstado.DataSource = null; }

                            if (dtpagos1.Rows.Count > 0)
                            {
                                GridConvenio.DataSource = dtpagos1; GridConvenio.DataBind(); GridConvenio.Visible = true;
                                foreach (GridViewRow row in GridConvenio.Rows)
                                {
                                    monto2 += Convert.ToDouble(row.Cells[5].Text);
                                }
                                if (monto2 > 0)
                                {
                                    lbMonto2.Text = monto2.ToString("C"); lbMonto2.Visible = true; lbConvenio1.Visible = true;
                                }

                            }
                            else
                                GridConvenio.DataSource = null;


                            if ((monto1 > 0) || (monto2 > 0))
                            {
                                lbTotal.Text = (monto1 + monto2).ToString("C");
                                lbTotal.Visible = true; lbConvenio2.Visible = true;
                            }
                        }
                    }
                }
            }
        }

        bool Carreras(int opcion)
        {
            bool encontrado = false;
            string usu = System.Web.HttpContext.Current.Session["usuario"].ToString().Trim();
            string sedes = "Select * from VistaAdminSedes WHERE idSede = " + DropDownList1.SelectedValue.ToString() + " AND Usuario = '" + usu + "'";
            DataTable dtsedes = Models.Conex.Consulta(sedes);
            int codi = Convert.ToInt16(txCarnet.Text.Substring(4, 2));
            for (int i = 0; i < dtsedes.Rows.Count; i++)
            {
                if (Convert.ToInt16(dtsedes.Rows[i][4].ToString()) == codi)
                {
                    encontrado = true;
                }
            }
            return encontrado;
        }



        private void buscaDatos()
        {
            if (txNombre.Text.Length > 0)
            {
                bool nopermitido = false;
                for (int c = 1; c < txNombre.Text.Length; c++)
                {
                    string caracter = txNombre.Text[c].ToString();
                    if (caracter == "=" || caracter == "'" || caracter == "-")
                        nopermitido = true;
                }
                if (nopermitido)
                { txNombre.Text = ""; }
                else
                {
                    int opcion = Convert.ToInt32(DropDownList1.SelectedValue.ToString());
                    DataTable dt = new DataTable();
                    string cadena = "select  [número de carné], Nombre_completo + ' ' + [nombre de la carrera] As nombre_completo from VistaAlumnosCarrera where nombre_completo COLLATE Latin1_General_CI_AI Like '%" + 
                        txNombre.Text + "%' order by nombre_completo";
                    lbResultado.Text = opcion.ToString() + " " + cadena;
                    dt = Models.Conex.Consulta2(cadena, opcion);
                    if (dt.Rows.Count == 0)
                    { lbResultado.Text = "No se localizan coincidencias"; Label4.Visible = false; ddNombre.Visible = false; lbnombre.Text = ""; }
                    else
                    {
                        ddNombre.ClearSelection(); ddNombre.Items.Clear();
                        lbResultado.Text = "Números de coincidencias: " + dt.Rows.Count.ToString();
                        ddNombre.Items.Add("Seleccione un item par ver los pagos");
                        ddNombre.DataValueField = "número de carné";
                        ddNombre.DataTextField = "nombre_completo";
                        ddNombre.DataSource = dt;
                        ddNombre.DataBind();
                        Label4.Visible = true; ddNombre.Visible = true;
                        lbnombre.Text = "Registros localizados: " + dt.Rows.Count.ToString();    //[0]["nombre_completo"].ToString();
                    }
                }
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //CargaFacultades(); 
            //CargaSedes();
            oculta(false); lbResultado.Text = ""; lbnombre.Text = "";
                limpia443();
        }
        protected void btCoincidencias_Click(object sender, EventArgs e)
        {
            buscaDatos();

        }

        protected void ddNombre_SelectedIndexChanged(object sender, EventArgs e)
        {
            txCarnet.Text = ddNombre.SelectedValue.ToString();
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

        protected void ddCarrera_SelectedIndexChanged(object sender, EventArgs e)
        {
            oculta(false); lbResultado.Text = "";
        }

        private void oculta(bool opcion)
        {
            imgbutExc.Visible = opcion;
            GridView1.Visible = opcion;
            gridEstado.Visible = opcion;
        }

        protected void ddCarrera_SelectedIndexChanged1(object sender, EventArgs e)
        {
            oculta(false); lbResultado.Text = "";
        }

        protected void btNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("PagsPagos.aspx");
        }
    }
}