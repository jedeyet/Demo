using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net;

namespace Visor_de_Documentos
{
    public partial class PagsDocentesActas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargaSedes();
                CargaDatos();

            }
        }
        string usuario = "";  
        private void CargaDatos()
        {
            for (int x = 2002; x <= DateTime.Now.Year; x++)
                ddAnio.Items.Add(x.ToString());
            ddAnio.SelectedIndex = ddAnio.Items.Count - 1;
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
                    string cadena = "Select * from catedraticos where [nombre y apellidos del catedrático] COLLATE Latin1_General_CI_AI LIKE '%" +
                        TextBox1.Text + "%' order by [nombre y apellidos del catedrático]";
                    int opcion = Convert.ToInt32(DropDownList1.SelectedValue.ToString());
                    DataTable dt = Models.Conex.Consulta2(cadena, opcion);
                    if (dt.Rows.Count == 0)
                        lbResultado.Text = "No se localizan docentes con estas credenciales";
                    else
                    {
                        ListBox1.Items.Clear();
                        ListBox1.Items.Add("Seleccione un catedrático");
                        lbResultado.Text = "Números de docentes localizados: " + dt.Rows.Count.ToString();
                        ListBox1.DataValueField = "codigo_catedratico";
                        ListBox1.DataTextField = "nombre y apellidos del catedrático";
                        ListBox1.DataSource = dt;
                        ListBox1.DataBind();

                    }
                }
            }

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                string cadena = "Select * from catedraticos where codigo_catedratico=" + TextBox2.Text;
                int opcion = Convert.ToInt32(DropDownList1.SelectedValue.ToString());


                DataTable dt = Models.Conex.Consulta2(cadena, opcion);
                if (dt.Rows.Count == 0)
                    lbResultado.Text = "No se localizan docentes con estas credenciales";
                else
                {
                    string dato = "'" + dt.Rows[0]["nombre y apellidos del catedrático"] + "'";
                    Label12.Text = dt.Rows[0]["codigo_catedratico"].ToString();
                    CargaCuadriculas();
                }
            }
            catch
            { Button1.Focus(); }
        }

        protected void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int opcion = Convert.ToInt32(DropDownList1.SelectedValue.ToString());
            Label12.Text = ListBox1.SelectedValue.ToString();
            CargaCuadriculas();

        }

        private void CargaCuadriculas()
        {

            string cadena = "select Codigo_Asignacion_Curso_Profesor as Codigo, asignatura as Asignatura, seccion as Sección, " +
            "[Nombre de la carrera] as Carrera, Numero_Acta, zona as Zona,iif(zona = 'NO', 'NO', convert(nvarchar, fecha_zona, 103)) As FechaZona," +
            "finales as Finales,iif(Finales = 'NO', 'NO', convert(nvarchar, Fecha_Finales, 103)) as FechaFinal,RegistroFinal as FirmaActa " +
            "from Vista_Asignacion_Curso_Profesor where ano = " + ddAnio.Text + " and SemestreCursado = " + ddSemestre.Text + " and Codigo_Catedratico = " +
            Label12.Text;
            int opcion = Convert.ToInt32(DropDownList1.SelectedValue.ToString());
            DataTable dt = Models.Conex.Consulta2(cadena, opcion);
            GridView1.DataSource = dt; GridView1.DataBind();
            cargaFoto();
        }
        private void cargaFoto()
        {
            string cadena = "";
            int opcion = Convert.ToInt32(DropDownList1.SelectedValue.ToString());
            if (opcion == 1)
            {
                cadena = "https://secretaria.mesoamericana.edu.gt/fcu/" + Label12.Text + ".jpg";
                Image1.ImageUrl = cadena;
            }

            if (opcion == 2)
            {
                cadena = "https://academico.umes.edu.gt/fcu/" + Label12.Text + ".jpg";
                Image1.ImageUrl = cadena;

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
        }

        protected void btNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("PagsDocentesActas.aspx");
        }
    }
}