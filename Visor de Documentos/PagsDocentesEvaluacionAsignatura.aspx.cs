using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Data;
using System.Web.UI.DataVisualization.Charting;


namespace Visor_de_Documentos
{
    public partial class PagsDocentesEvaluacionAsignatura : System.Web.UI.Page
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
            for (int x = 2002; x <= DateTime.Now.Year; x++)
                ddAnio.Items.Add(x.ToString());
            ddAnio.SelectedIndex = ddAnio.Items.Count - 1;

           
            
            
            for (byte a = 65; a <= 90; a++)
            {
                 ddSeccion.Items.Add(((char)a).ToString());
                if (a==90)
                {
                    for (byte b = 65; b <= 90; b++)
                        ddSeccion.Items.Add("A"+((char)b).ToString());
                }
            }

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
            llenaSemestres();
        }

        private void llenaSemestres()
        {
            int opcion = Convert.ToInt32(DropDownList1.SelectedValue.ToString());
            string rangos = "select * from carrera where [id carrera]=" + ddCarrera.SelectedValue.ToString();
            DataTable rangosDt = new DataTable();
            rangosDt = Models.Conex.Consulta2(rangos, opcion);
            lbinicio.Text = rangosDt.Rows[0]["semestre_inicia"].ToString();
            lbFin.Text = rangosDt.Rows[0]["semestre_finaliza"].ToString();
            int inicio = Convert.ToInt16(lbinicio.Text);
            int fin = Convert.ToInt16(lbFin.Text);
            ddSemestre.Items.Clear();
            for (int i = inicio; i <= fin; i++)
            {
                ddSemestre.Items.Add(i.ToString());
            }

        }

        protected void ddCarrera_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenaSemestres();
            oculta(false); oculta1(false); oculta2(false);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (ddAsignatura.SelectedIndex>=0)
            {
                int opcion = Convert.ToInt32(DropDownList1.SelectedValue.ToString());
                
                string domin = "SELECT  ID_Dominio, dominio,round(AVG(Punteo),2) AS promedio  " +
                    " FROM Vista_Evaluaciones_19 WHERE  Codigo = '" + ddAsignatura.SelectedValue.ToString() +  "' GROUP BY ID_Dominio,dominio order by id_dominio";
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

                string curso = "Select * from Vista_Evaluaciones_19 WHERE  Codigo = '" + ddAsignatura.SelectedValue.ToString() + "'";
                DataTable dtCurso = new DataTable();    
                dtCurso = Models.Conex.Consulta2(curso, opcion);
                lbDocente.Text = dtCurso.Rows[0]["Docente"].ToString();
                lbConteo.Text = (dtCurso.Rows.Count / 25).ToString();

                Chart1.DataSource = dtdomin;
                Chart1.Series["Series1"].XValueMember = "ID_Dominio";
                Chart1.Series["Series1"].YValueMembers = "Promedio";
                Chart1.DataBind();
                

                for (int i = 1; i <= dtdomin.Rows.Count; i++)
                {
                    CustomLabel lbl = new CustomLabel();
                    lbl.Text = dtdomin.Rows[i-1]["ID_Dominio"].ToString(); //'AbreviacionArea
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
                    if (prom>=2 && prom<3)
                        recorre.Color = System.Drawing.Color.Yellow;
                    if (prom<2)
                        recorre.Color = System.Drawing.Color.Red;
                    a++;
                }

                string cadena = "select id_dominio, id_pregunta,pregunta,AVG(Punteo) AS promedio, COUNT(ID_Pregunta) " +
                " AS conteo from vista_evaluacion WHERE Codigo = '" + ddAsignatura.SelectedValue.ToString() + "' GROUP BY ID_Dominio, ID_Pregunta,pregunta order by id_pregunta";
                DataTable dt = new DataTable();
                dt = Models.Conex.Consulta2(cadena, opcion);
                cadena = "select id_dominio, id_pregunta,pregunta,AVG(Punteo) AS promedio, COUNT(ID_Pregunta) " +
                " AS conteo from vista_evaluaciones_19 WHERE Codigo = '" + ddAsignatura.SelectedValue.ToString() + "' GROUP BY ID_Dominio, ID_Pregunta,pregunta order by id_pregunta";
                DataTable dt2 = new DataTable();
                dt2 = Models.Conex.Consulta2(cadena, opcion);
                GridPreguntas.DataSource = dt2;
                GridPreguntas.DataBind();
                comentarios();
                Colores3();
                oculta1(true); oculta2(true);

            }
        }

        private void comentarios()
        {
            int opcion = Convert.ToInt32(DropDownList1.SelectedValue.ToString());
            string vista = " Evaluacion_Encabezado_19 ";
            if (opcion > 1) vista = "Evaluacion_Encabezado";
            String cadena = "select Comen_Pos as Positivos, Comen_Neg as Negativos from " + vista + " where curso = '" + ddAsignatura.SelectedValue.ToString() + "' and (comen_pos<>'' or comen_Neg<>'')";
            DataTable dt = new DataTable();
            dt = Models.Conex.Consulta2(cadena, opcion);
            GridComentarios.DataSource = dt;
            GridComentarios.DataBind();
        }

        private void Colores2()
        {
            for (int x=0;x<gridDominio.Rows.Count;x++)
            {
                GridViewRow fila = gridDominio.Rows[x];
                var promedio = fila.FindControl("lbProm1") as Label;
                promedio.Font.Bold = true;
                double prom = Convert.ToDouble(promedio.Text);
                if (prom>=3)
                {
                    promedio.BackColor = System.Drawing.Color.Green;   promedio.ForeColor = System.Drawing.Color.White;
                }
                if (prom>=2 && prom<3)
                {
                    promedio.BackColor = System.Drawing.Color.Yellow; promedio.ForeColor = System.Drawing.Color.Brown;
                }
                if (prom < 2 )
                {
                    promedio.BackColor = System.Drawing.Color.Red ; promedio.ForeColor = System.Drawing.Color.White;
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



        protected void btProceder_Click(object sender, EventArgs e)
        {
            int opcion = Convert.ToInt32(DropDownList1.SelectedValue.ToString());
            string cadena = "select codigo,asignatura from Vista_Evaluaciones_19 where [id carrera] =" +
                    ddCarrera.SelectedValue + " and semestre = " + ddSemestre.Text + " and ano = " + ddAnio.Text + " and seccion ='" +
                ddSeccion.Text + "' group by codigo,asignatura";
            DataTable dt = new DataTable();
            dt = Models.Conex.Consulta2(cadena, opcion);
            if (dt.Rows.Count > 0)
            {
                ddAsignatura.DataValueField = "codigo";
                ddAsignatura.DataTextField = "asignatura";
                ddAsignatura.DataSource = dt;
                ddAsignatura.DataBind();
                lbResultado.Text = "Asignaturas localizadas: " + dt.Rows.Count.ToString();
                oculta(true);lbResultado.Visible = true;
                
            }
            else
            {
                lbResultado.Text = "No se localizan asignaturas evaluadas";
                oculta(false); oculta1(false); oculta2(false);
                 
            }
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
            CargaCarreras();
            oculta(false); oculta1(false);oculta2(false);
        }

        protected void Chart1_Load(object sender, EventArgs e)
        {

        }

        private void oculta(bool opcion)
        {
            Label6.Visible = opcion; Button1.Visible = opcion; ddAsignatura.Visible = opcion;

        }

        private void oculta1(bool opcion)
        {

            Label8.Visible = opcion; lbConteo.Visible = opcion;
            Label7.Visible = opcion; lbDocente.Visible = opcion;
            lbResultado.Visible = opcion;
        }

        private void oculta2(bool opcion)
        {

            gridDominio.Visible = opcion; Chart1.Visible = opcion;
            GridPreguntas.Visible = opcion; GridComentarios.Visible = opcion;
        }

        protected void ddAnio_SelectedIndexChanged(object sender, EventArgs e)
        {
            oculta(false); oculta1(false); oculta2(false);
        }

        protected void ddSemestre_SelectedIndexChanged(object sender, EventArgs e)
        {
            oculta(false); oculta1(false); oculta2(false);
        }

        protected void ddSeccion_SelectedIndexChanged(object sender, EventArgs e)
        {
            oculta(false); oculta1(false); oculta2(false);
        }

        protected void btNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("PagsDocentesEvaluacionAsignatura.aspx");
        }
    }
}