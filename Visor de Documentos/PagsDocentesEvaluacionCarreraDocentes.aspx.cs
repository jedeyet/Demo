using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Data;
using System.Web.UI.HtmlControls;

namespace Visor_de_Documentos
{
    public partial class PagsDocentesEvaluacionCarreraDocentes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargaSedes();
                CargaCarreras();
                CargaDatos();

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
            for (int x = DateTime.Now.Year; x >= 2002; x--)
                ddAnio.Items.Add(x.ToString());
            ddAnio.SelectedIndex = ddAnio.Items.Count - 1;
            ddSemestre.Items.Add("1"); ddSemestre.Items.Add("2");
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
                cadena = cadena = "select [id Carrera], [Nombre de la carrera] from carrera where (estado = 'activa' or estado = 'True') order by  [Nombre de la carrera]";
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

        protected void btProceder_Click(object sender, EventArgs e)
        {
             
                int opcion = Convert.ToInt32(DropDownList1.SelectedValue.ToString());
            
            string domin = "select  t1.docente, (select round(avg(punteo), 2) from vista_evaluaciones_19 as t2 " + 
                " where t2.ano = " + ddAnio.Text + " and t2.SemestreEvaluacion = " + ddSemestre.Text + " and t2.docente = t1.docente " +
                " and t2.id_dominio = 1 and t2.[Id Carrera] = " + ddCarrera.SelectedValue + " group by t2.docente, t2.ID_Dominio) as Dominio1, " +  
                " (select round(avg(punteo), 2) from vista_evaluaciones_19 as t2 where t2.ano = " + ddAnio.Text +  
                " and t2.SemestreEvaluacion = " + ddSemestre.Text + " and t2.docente = t1.docente and t2.id_dominio = 2 " +
                " and t2.[Id Carrera] = " + ddCarrera.SelectedValue + " group by t2.docente, t2.ID_Dominio) as Dominio2, " +
                " (select round(avg(punteo), 2) from vista_evaluaciones_19 as t2 where t2.ano = " + ddAnio.Text + " and t2. SemestreEvaluacion =" +
                ddSemestre.SelectedValue + "and t2.docente = t1.docente and t2.id_dominio = 3 and t2.[Id Carrera]= " + ddCarrera.SelectedValue + 
                " group by t2.docente,t2.ID_Dominio) as Dominio3, (select round(avg(punteo), 2) from vista_evaluaciones_19 as t2 " + 
                " where t2.ano = " + ddAnio.Text + " and t2. SemestreEvaluacion = " + ddSemestre.Text + " and t2.docente = t1.docente " +
                " and t2.id_dominio = 4 and t2.[Id Carrera]= " + ddCarrera.SelectedValue + "group by t2.docente,t2.ID_Dominio) as Dominio4, " +
                " (select round(avg(punteo), 2) from vista_evaluaciones_19 as t2 where t2.ano = " + ddAnio.Text + "and t2. SemestreEvaluacion =" +
                ddSemestre.Text + "and t2.docente = t1.docente and t2.id_dominio = 5 and t2.[Id Carrera]= " + ddCarrera.SelectedValue  + 
                " group by t2.docente,t2.ID_Dominio) as Dominio5, (select round(avg(punteo), 2) from vista_evaluaciones_19 as t2 " + 
                " where t2.ano = " + ddAnio.Text + " and t2. SemestreEvaluacion = " + ddSemestre.Text + "and t2.docente = t1.docente " +
                " and t2.id_dominio = 6 and t2.[Id Carrera]= " + ddCarrera.SelectedValue + "group by t2.docente,t2.ID_Dominio) as Dominio6, " +
                " (select round(avg(punteo), 2) from vista_evaluaciones_19 as t2 where t2.ano = " + ddAnio.Text + "and t2. SemestreEvaluacion =" +
                ddSemestre.Text + "and t2.docente = t1.docente and t2.[Id Carrera]= " + ddCarrera.Text + "group by t2.docente,t2.[Id Carrera])" +
                " as promedio, (select (count(*)/25) from vista_evaluaciones_19 as t2 where t2.ano = " + ddAnio.Text + "and t2. SemestreEvaluacion=" + 
                ddSemestre.Text + " and t2.docente = t1.docente and t2.[Id Carrera]= " + ddCarrera.SelectedValue + " group by t2.docente,t2.[Id Carrera]) as Alumnos " +
                "from vista_evaluaciones_19 as t1 where t1.ano = " + ddAnio.Text + "and t1.SemestreEvaluacion = " + 
                ddSemestre.Text + " and t1.[Id Carrera]=" + ddCarrera.SelectedValue + " group by t1.docente order by t1.docente ";
             

            Session["Domin"] = domin;
            DataTable dtdomin = new DataTable();
            dtdomin = Models.Conex.Consulta2(domin, opcion);


            gridDominio.DataSource = dtdomin;
            gridDominio.DataBind();
            Colores2();

            if (dtdomin.Rows.Count == 0)
            { lbResultado.Text = "No se localizan docentes con esta combinación de opciones"; imgbutExc.Visible = false; }
            else
            { lbResultado.Text = ""; imgbutExc.Visible = true; }
        }

        private void Colores2()
        {
            for (int x = 0; x < gridDominio.Rows.Count; x++)
            {
                GridViewRow fila = gridDominio.Rows[x];
                var promedio = fila.FindControl("lbprom") as Label; promedio.Font.Bold = true;
                var dominio1 = fila.FindControl("lbdom1") as Label; dominio1.Font.Bold = true;
                var dominio2 = fila.FindControl("lbdom2") as Label; dominio1.Font.Bold = true;
                var dominio3 = fila.FindControl("lbdom3") as Label; dominio1.Font.Bold = true;
                var dominio4 = fila.FindControl("lbdom4") as Label; dominio1.Font.Bold = true;
                var dominio5 = fila.FindControl("lbdom5") as Label; dominio1.Font.Bold = true;
                var dominio6 = fila.FindControl("lbdom6") as Label; dominio1.Font.Bold = true;
                double domi1 = Convert.ToDouble(dominio1.Text); double domi2 = Convert.ToDouble(dominio2.Text);
                double domi3 = Convert.ToDouble(dominio3.Text); double domi4 = Convert.ToDouble(dominio4.Text);
                double domi5 = Convert.ToDouble(dominio5.Text); double domi6 = Convert.ToDouble(dominio6.Text);
                double prom = Convert.ToDouble(promedio.Text);
                if (prom >= 3)
                {
                    promedio.BackColor = System.Drawing.Color.Green; promedio.ForeColor = System.Drawing.Color.White;
                }
                if (prom >= 2 && prom < 3)
                {
                    promedio.BackColor = System.Drawing.Color.Yellow; promedio.ForeColor = System.Drawing.Color.Brown;
                }
                if (prom < 2)
                {
                    promedio.BackColor = System.Drawing.Color.Red; promedio.ForeColor = System.Drawing.Color.White;
                }
                promedio.Text = prom.ToString("N2"); dominio1.Text = domi1.ToString("N2"); dominio2.Text = domi2.ToString("N2");
                dominio3.Text = domi3.ToString("N2"); dominio4.Text = domi4.ToString("N2");
                dominio5.Text = domi5.ToString("N2"); dominio6.Text = domi6.ToString("N2");
            }
        }

        protected void ddCarrera_SelectedIndexChanged(object sender, EventArgs e)
        {
            oculta();
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Nivelin();
            CargaCarreras();
            oculta();
        }

        private void oculta()
        {
            gridDominio.DataSource = ""; gridDominio.DataBind(); lbResultado.Text = ""; imgbutExc.Visible = false;
        }
        protected void ddAnio_SelectedIndexChanged(object sender, EventArgs e)
        {
            oculta();
        }

        protected void ddSemestre_SelectedIndexChanged(object sender, EventArgs e)
        {
            oculta();
        }

        protected void btNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("PagsDocentesEvaluacionCarreraDocentes.aspx");
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
            ExportaExcel(gridDominio);
        }
    }
    
}