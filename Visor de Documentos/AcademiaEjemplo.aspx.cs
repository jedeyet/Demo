using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace Visor_de_Documentos
{
    public partial class AcademiaEjemplo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int opcion = Convert.ToInt32(DropDownList1.SelectedValue.ToString());
            DataTable dt = new DataTable();
            string cadena = "Select top(10) [número de carné],nombre_completo,email from alumno where [número de carné] like '2021%'";
            dt = Models.Conex.Consulta2(cadena, opcion);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }



    }
}