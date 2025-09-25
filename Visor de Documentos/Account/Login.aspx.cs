using System;
using System.Linq;
using System.Web.Security;
using System.Data;
using System.Data.SqlClient;

namespace Visor_de_Documentos.Account
{

    public partial class Login : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            txtUsuario.Focus();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int entro = 0; //esta variable obtiene el resultado del parámetro de salida del
            //procedimiento almacenado LoginAdminSedes. 1 si se loguea, 0 si no lo hace. 
            SqlCommand Comando = new SqlCommand
            {
                CommandText = "LoginAdminSedes",
                CommandType = CommandType.StoredProcedure,
                Connection = Models.Conex.Con_X
            };
            Comando.Parameters.AddWithValue("@usuario", txtUsuario.Value);
            Comando.Parameters.AddWithValue("@password", txtPassword.Value);
            SqlParameter paramOut = Comando.Parameters.Add("@Result", SqlDbType.Int, int.MaxValue);
            paramOut.Direction = ParameterDirection.Output;
            Models.Conex.Con_X.Open();
            var salida = Comando.ExecuteReader();
            entro = (paramOut.Value != null && paramOut.Value != DBNull.Value) ? (int)paramOut.Value : 0;
            Models.Conex.Con_X.Close();
            if (entro == 1)
            {
                FormsAuthentication.SetAuthCookie(txtUsuario.Value, true);
                //        Response.Redirect("~/Default.aspx");
                Session["Movile"] = "NoMovil";
                if (Request.Browser.IsMobileDevice) Session["Movile"] = "Movil";
                FormsAuthentication.RedirectFromLoginPage(txtUsuario.Value, chkPersistCookie.Checked);
                System.Web.HttpContext.Current.Session["usuario"] = txtUsuario.Value.ToLower();
                Nivel();
                Registra();
            }
            else
                Response.Redirect("Login.aspx", true);
        }

       private void Nivel()
        {
            string cad = "select * from AdminSedes where usuario ='" + txtUsuario.Value.ToString() + "'";
            DataTable dt = new DataTable();
            dt = Models.Conex.Consulta2(cad, 1);
            System.Web.HttpContext.Current.Session["nivel"] = dt.Rows[0]["IdNivel"].ToString();
        }
        private void Registra()
        {
            Random n = new Random();
            
            string asig = "";
            for (int i = 0; i < 10; i++)
            {
                int num = n.Next(65, 90);
                asig += (char)num;
            }

            string cadena = "select nombre from VistaSedesAdminsDatos where Usuario='" + txtUsuario.Value + "' ";
            DataTable dz = new DataTable(); dz = Models.Conex.Consulta(cadena);
            Session["adm"] =  txtUsuario.Value + " | " + dz.Rows[0][0].ToString();
            Session["Asig"] = asig.ToString().Trim();
            SqlCommand Comando = new SqlCommand
            {
                CommandText = "Registro_Bitacora",
                CommandType = CommandType.StoredProcedure,
                Connection = Models.Conex.Con_X
            };
            Comando.Parameters.AddWithValue("@operacion", 78);
            Comando.Parameters.AddWithValue("@operador", -10);
            Comando.Parameters.AddWithValue("@Fecha", DateTime.Now.Date);
            Comando.Parameters.AddWithValue("@obser", Session["adm"]);
            Comando.Parameters.AddWithValue("@alumno", "");
            Comando.Parameters.AddWithValue("@catedratico", "");
            Comando.Parameters.AddWithValue("@asignatura", asig.ToString());
            Comando.Parameters.AddWithValue("@documento", "");
            Comando.Connection.Open();Comando.ExecuteNonQuery();Comando.Connection.Close();
        }

    }
}