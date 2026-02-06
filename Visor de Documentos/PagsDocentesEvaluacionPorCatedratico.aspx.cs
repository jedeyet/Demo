using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.DataVisualization.Charting;
using System.Net;


namespace Visor_de_Documentos
{
    public partial class PagsDocentesEvaluacionPorCatedratico : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargaSedes();
                CargaDatos();
                Chart1.ImageLocation = Server.MapPath("~/TempImageFiles/");
                Chart1.ImageStorageMode = System.Web.UI.DataVisualization.Charting.ImageStorageMode.UseImageLocation;
            }
        }
        string usuario = "";  
        private void CargaDatos()
        {
            for (int x = DateTime.Now.Year; x >= 2002; x--)
                ddAnio.Items.Add(x.ToString());
            ddAnio.SelectedIndex = 0;
            ddSemestre.Items.Add("1");
            ddSemestre.Items.Add("2");
            if (DateTime.Now.Month >= 7) ddSemestre.SelectedIndex = 1; else ddSemestre.SelectedIndex = 0;

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
        protected void Button1_Click(object sender, EventArgs e)
        {
           
                    string cadena = "select docente from vista_evaluaciones_19 where ano = " + ddAnio.SelectedValue.ToString() + "and semestreEvaluacion= "  + 
                        ddSemestre.SelectedValue.ToString() +  " GROUP BY docente order by docente";
                    int opcion = Convert.ToInt32(DropDownList1.SelectedValue.ToString());
                    DataTable dt = Models.Conex.Consulta2(cadena, opcion);
            if (dt.Rows.Count == 0)
            { 
                lbResultado.Text = "No se localizan docentes evaluados en este año y semestre anual";
                oculta(false);
            }
            else
            {
                oculta(true);oculta1(false);oculta2(false);
                ddDocente.DataValueField = "docente";
                ddDocente.DataTextField = "docente";
                ddDocente.DataSource = dt;
                ddDocente.DataBind();
               // lbResultado.Text = "Docentes localizados: " + dt.Rows.Count.ToString();
            }
                
        }

        private void oculta (bool opcion)
        {
            Label6.Visible = opcion;  Button2.Visible= opcion; ddDocente.Visible = opcion;
             
        }

        private void oculta1(bool opcion)
        {
            
            Label8.Visible = opcion; lbConteo.Visible = opcion;
            Label7.Visible = opcion; lbDocente.Visible = opcion;
        }

        private void oculta2(bool opcion)
        {

            gridDominio.Visible = opcion;  Chart1.Visible= opcion;
            GridPreguntas.Visible = opcion; GridComentarios.Visible = opcion;
        }


        protected void ddDocente_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comentarios()
        {
            int opcion = Convert.ToInt32(DropDownList1.SelectedValue.ToString());
            String cadena = "select Comen_Pos as Positivos, Comen_Neg as Negativos from vista_Evaluacion_Encabezado_19  where docente = '" + ddDocente.SelectedValue.ToString() + "' and anio = " + ddAnio.SelectedValue.ToString() +
                    " and semestre = " + ddSemestre.SelectedValue.ToString() + "and (comen_pos<>'' or comen_Neg<>'')";
            DataTable dt = new DataTable();
            dt = Models.Conex.Consulta2(cadena, opcion);
            GridComentarios.DataSource = dt;
            GridComentarios.DataBind();
        }

        private void Colores2()
        {
            for (int x = 0; x < gridDominio.Rows.Count; x++)
            {
                GridViewRow fila = gridDominio.Rows[x];
                var promedio = fila.FindControl("lbProm1") as Label;
                promedio.Font.Bold = true;
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
                promedio.Text = prom.ToString("N2");
            }
        }

        private void Colores3()
        {
            for (int x = 0; x < GridPreguntas.Rows.Count; x++)
            {
                GridViewRow fila = GridPreguntas.Rows[x];
                var promedio = fila.FindControl("lbProm") as Label;
                promedio.Font.Bold = true;
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
                promedio.Text = prom.ToString("N2");
            }
        }



        protected void Button2_Click(object sender, EventArgs e)
        {
            if (ddDocente.SelectedIndex >= 0)
            {
               
                int opcion = Convert.ToInt32(DropDownList1.SelectedValue.ToString());
                string domin = "SELECT  ID_Dominio, dominio,AVG(Punteo) AS promedio  " +
                    " FROM Vista_Evaluaciones_19 WHERE  docente = '" + ddDocente.SelectedValue.ToString() + "' and ano = " + ddAnio.SelectedValue.ToString() + 
                    " and semestreEvaluacion = " + ddSemestre.SelectedValue.ToString() + " GROUP BY ID_Dominio,dominio order by id_dominio";
                Session["Domin"] = domin;
                DataTable dtdomin = new DataTable();
                dtdomin = Models.Conex.Consulta2(domin, opcion);

                ListBox1.Items.Clear();
                for (int i = 0; i < 6; i++)
                {
                    ListBox1.Items.Add(dtdomin.Rows[i]["promedio"].ToString());
                }
                gridDominio.DataSource = dtdomin;
                gridDominio.DataBind();
                Colores2();

                string cuantos = "Select * from vista_Evaluacion_Encabezado_19 WHERE  docente = '" + ddDocente.SelectedValue.ToString() + "' and anio = " + ddAnio.SelectedValue.ToString() +
                    " and semestre = " + ddSemestre.SelectedValue.ToString();
                DataTable dtcuantos = new DataTable();
                dtcuantos = Models.Conex.Consulta2(cuantos, opcion);
                lbConteo.Text = (dtcuantos.Rows.Count).ToString();


                string curso = "Select * from Vista_Evaluaciones_19 WHERE  docente = '" + ddDocente.SelectedValue.ToString() + "' and ano = " + ddAnio.SelectedValue.ToString() +
                    " and semestreEvaluacion = " + ddSemestre.SelectedValue.ToString();
                DataTable dtCurso = new DataTable();
                dtCurso = Models.Conex.Consulta2(curso, opcion);
                lbDocente.Text = dtCurso.Rows[0]["Docente"].ToString();
                
                    
                Chart1.DataSource = dtdomin;
                Chart1.Series["Series1"].XValueMember = "ID_Dominio";
                Chart1.Series["Series1"].YValueMembers = "Promedio";
                Chart1.DataBind();


                for (int i = 1; i <= dtdomin.Rows.Count; i++)
                {
                    CustomLabel lbl = new CustomLabel();
                    lbl.Text = dtdomin.Rows[i - 1]["ID_Dominio"].ToString(); //'AbreviacionArea
                    lbl.FromPosition = i - 0.5;
                    lbl.ToPosition = i + 0.5;
                    Chart1.ChartAreas[0].AxisX.CustomLabels.Add(lbl);
                }

                int a = 0;
                foreach (var recorre in Chart1.Series["Series1"].Points)
                {
                    ListBox1.SelectedIndex = a;
                    double prom = Convert.ToDouble(ListBox1.Text);
                    if (prom >= 3)
                        recorre.Color = System.Drawing.Color.Green;
                    if (prom >= 2 && prom < 3)
                        recorre.Color = System.Drawing.Color.Yellow;
                    if (prom < 2)
                        recorre.Color = System.Drawing.Color.Red;
                    a++;
                }


                string cadena = "select id_dominio, id_pregunta,pregunta,AVG(Punteo) AS promedio, COUNT(ID_Pregunta) " +
                " AS conteo from vista_evaluaciones_19 WHERE docente = '" + ddDocente.SelectedValue.ToString() + "' and ano = " + ddAnio.SelectedValue.ToString() +
                    " and semestreEvaluacion = " + ddSemestre.SelectedValue.ToString() + " GROUP BY ID_Dominio, ID_Pregunta,pregunta order by id_pregunta";
                DataTable dt2 = new DataTable();
                dt2 = Models.Conex.Consulta2(cadena, opcion);
                GridPreguntas.DataSource = dt2;
                GridPreguntas.DataBind();
                comentarios();
                Colores3();
                oculta1(true);
                oculta2(true);
            }
        }

        protected void ddAnio_SelectedIndexChanged(object sender, EventArgs e)
        {
            oculta(false); oculta1(false);oculta2(false);
        }

        protected void ddSemestre_SelectedIndexChanged(object sender, EventArgs e)
        {
            oculta(false); oculta1(false); oculta2(false);
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            oculta(false); oculta1(false); oculta2(false);
        }

        protected void btNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("PagsDocentesEvaluacionPorCatedratico.aspx");
        }
    }
}