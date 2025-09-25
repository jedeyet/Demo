using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


namespace Visor_de_Documentos
{
    public partial class PagsDocenteCarrera : System.Web.UI.Page
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
        string usuario = "";   int Nivel = 0;

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
            Nivel  = Convert.ToInt16(System.Web.HttpContext.Current.Session["Nivel"].ToString());
            int opcion = Convert.ToInt32(DropDownList1.SelectedValue.ToString());
            int opcioncarrera = 1;
            DataTable dt = new DataTable();
            string cadena = "";
            

            if (Nivel == 0)
            {
                cadena = "select [id Carrera], [Nombre de la carrera] from carrera where estado='Activa' or estado='True'  order by  [Nombre de la carrera]";
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
        DataTable dt = new DataTable();
        string cadena = "select [Nombre y apellidos del catedrático],count(*) as Cursos,email "  
           + "from Vista_Asignacion_Curso_Profesor where ano = " + ddAnio.Text + " and SemestreCursado = " + ddSemestre.Text   
           + " and[Nombre y apellidos del catedrático] not like 'aas%' and [id carrera] = " + ddCarrera.SelectedValue.ToString()  
           + " group by[Nombre y apellidos del catedrático], email order by[Nombre y apellidos del catedrático]";
        dt = Models.Conex.Consulta2(cadena, opcion);
        if (dt.Rows.Count == 0) lbResultado.Text = "No se localizan docentes activos";
        else lbResultado.Text = "Números de docentes localizados: " + dt.Rows.Count.ToString();
        GridView1.DataSource = dt;
        GridView1.DataBind();
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
            lbResultado.Text = "";
            GridView1.DataSource = "";
            GridView1.DataBind();
        }

        protected void btNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("PagsDocenteCarrera.aspx");
        }

        protected void ddCarrera_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}