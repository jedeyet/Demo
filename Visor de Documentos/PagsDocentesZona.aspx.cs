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
    public partial class PagsDocentesZona : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargaSedes();
                CargaDatos();
                CargaCarreras();
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

        private void CargaDatos()
        {
            for (int x = 2002; x <= DateTime.Now.Year; x++)
                ddAnio.Items.Add(x.ToString());
            ddAnio.SelectedIndex = ddAnio.Items.Count - 1;
            ddSemestre.Items.Add("1");
            ddSemestre.Items.Add("2");
            if (DateTime.Now.Month >= 7) ddSemestre.SelectedIndex = 1; else ddSemestre.SelectedIndex = 0;
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
                cadena = "select [id Carrera], [Nombre de la carrera] from carrera  where (estado = 'activa' or estado = 'True') order by  [Nombre de la carrera]";
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

    protected void Button1_Click(object sender, EventArgs e)
    {
        int opcion = Convert.ToInt32(DropDownList1.SelectedValue.ToString());
        string anio = ddAnio.SelectedValue.ToString();
        string semestre = ddSemestre.SelectedValue.ToString();
            string seccion = " and (seccion<>'Y' or seccion <>'Z')";
            if (opcion>=2) seccion = " and(seccion <> 'C' and seccion <> 'S')";
        string cadena = "select Codigo_Asignacion_Curso_Profesor,Semestre,asignatura,seccion,[Nombre y apellidos del catedrático] "
       + "from Vista_Asignacion_Curso_Profesor where ano = " + ddAnio.Text + " and SemestreCursado = " + ddSemestre.Text + seccion  
       + " and [Nombre y apellidos del catedrático] not like 'aas%' and[Nombre y apellidos del catedrático] not like 'equiv%' " 
       + " and asignatura not like '%(_)%' "
       + " and [id carrera] = " + ddCarrera.SelectedValue.ToString()
       + " order by semestre,asignatura";
        DataTable dt = Models.Conex.Consulta2(cadena, opcion);
            if (dt.Rows.Count == 0)
            { lbResultado.Text = "No se localizan docentes activos"; oculta(false); }
            else
            {
                lbResultado.Text = "Números de docentes localizados: " + dt.Rows.Count.ToString();
                GridView1.DataSource = dt;
                GridView1.DataBind();
                CargaZonas(); oculta(true);
            }
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargaCarreras(); oculta(false); lbResultado.Text = "";
    }

    private void CargaZonas()
    {
            int opcion = Convert.ToInt32(DropDownList1.SelectedValue.ToString());
            int ultimo = GridView1.Rows.Count;
            for (int c=0;c<ultimo; c++)
            {
                GridViewRow fila = GridView1.Rows[c];
                var codigo = fila.FindControl("lbcodigo") as Label;
                var zona = fila.FindControl("lbzona") as Label;
                var p1 = fila.FindControl("lbp1") as Label; var p2 = fila.FindControl("lbp2") as Label;
                var p3 = fila.FindControl("lbp3") as Label; var l1 = fila.FindControl("lbl1") as Label;
                var l2 = fila.FindControl("lbl2") as Label; var l3 = fila.FindControl("lbl3") as Label;
                var l4 = fila.FindControl("lbl4") as Label; var l5 = fila.FindControl("lbl5") as Label;
                var l6 = fila.FindControl("lbl6") as Label; var l7 = fila.FindControl("lbl7") as Label;
                var l8 = fila.FindControl("lbl8") as Label; var l9 = fila.FindControl("lbl9") as Label;
                var l10 = fila.FindControl("lbl10") as Label; 

                string cadena = "select top(1) zona_c,p1,p2,p3,l1,l2,l3,l4,l5,l6,l7,l8,l9,l10 from vista_zonas where codigo_acta='" + codigo.Text.ToString() + "' order by zona_c desc";
                DataTable dx = new DataTable();
                dx.Clear();
                dx = Models.Conex.Consulta2(cadena, opcion);
                if (dx.Rows.Count == 0)
                {
                    zona.Text = "Sin";
                    p1.Text = "Alu";
                    p2.Text = "mn";
                    p3.Text = "os";
                }
                else
                {
                    zona.Text = dx.Rows[0]["zona_c"].ToString();
                    p1.Text = dx.Rows[0]["p1"].ToString(); p2.Text = dx.Rows[0]["p2"].ToString();
                    p3.Text = dx.Rows[0]["p3"].ToString(); l1.Text = dx.Rows[0]["l1"].ToString();
                    l2.Text = dx.Rows[0]["l2"].ToString(); l3.Text = dx.Rows[0]["l3"].ToString();
                    l4.Text = dx.Rows[0]["l4"].ToString(); l5.Text = dx.Rows[0]["l5"].ToString();
                    l6.Text = dx.Rows[0]["l6"].ToString(); l7.Text = dx.Rows[0]["l7"].ToString();
                    l8.Text = dx.Rows[0]["l8"].ToString(); l9.Text = dx.Rows[0]["l9"].ToString();
                    l10.Text = dx.Rows[0]["l10"].ToString();
                    //if (opcion < 2)
                        //if (Convert.ToInt32(zona.Text) == 0) zona.BackColor = System.Drawing.Color.Red;
                    //else
                      //  if (Convert.ToInt32(zona.Text)==0)
                       // {
                         //   zona.BackColor = System.Drawing.Color.Red; 
                       // }
                }
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

        protected void ddCarrera_SelectedIndexChanged(object sender, EventArgs e)
        {
            oculta(false); lbResultado.Text = "";
        }

        private void oculta(bool opcion)
        {
            imgbutExc.Visible = opcion;
            GridView1.Visible = opcion;

        }

        protected void ddCarrera_SelectedIndexChanged1(object sender, EventArgs e)
        {
            oculta(false); lbResultado.Text = "";
        }

        protected void btNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("PagsDocentesZona.aspx");
        }
    }
}