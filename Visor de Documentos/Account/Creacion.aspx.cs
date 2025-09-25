using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;

namespace Visor_de_Documentos.Account
{
    public partial class Creacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        
        private void RegistraFecha()
        {
            string cadena = "Update adminsedespermitidos set fecharegistrado=getdate() where email ='" + txCorreo.Value + "'";
            DataTable dt = Models.Conex.Consulta2(cadena,1);
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            Label1.Visible = false;
            string cadena = "Select * from AdminSedespermitidos where email = '" + txCorreo.Value + "'";
            DataTable dt = Models.Conex.Consulta2(cadena,1);
            if (dt.Rows.Count < 1)
            { Label1.Text = "Error... El correo no está registrado"; Label1.Visible = true; }
            else
            {
                string cadena2 = "Select * from AdminSedespermitidos where email = '" + txCorreo.Value + "' and fecharegistrado is not null";
                DataTable dt2 = Models.Conex.Consulta2(cadena2, 1);
                if (dt2.Rows.Count > 0)
                { Label1.Text = "Esta cuenta ya fue creada"; Label1.Visible = true; }
                else
                if (txtPassword.Value != txPassword2.Value)
                { Label1.Text = "Las contraseñas no coinciden"; Label1.Visible = true; }
                else
                {
                    if (txtPassword.Value.Length < 8)
                    { Label1.Text = "Contraseña muy insegura, debe ser de 8 o más caracteres"; Label1.Visible = true; }
                    else
                    {
                        SqlCommand Comando = new SqlCommand();
                        Comando.CommandText = "IngresarAdminSede";
                        Comando.CommandType = CommandType.StoredProcedure;
                        Comando.Connection = Models.Conex.Con_X;
                        Comando.Parameters.AddWithValue("@usuario", txtUsuario.Value);
                        Comando.Parameters.AddWithValue("@password", txtPassword.Value);
                        Comando.Parameters.AddWithValue("@email", txCorreo.Value);
                        Models.Conex.Con_X.Open();
                        Comando.ExecuteNonQuery();
                        Models.Conex.Con_X.Close();
                        RegistraFecha();
                        Response.Redirect("Login.aspx", true);
                    }
                }
            }
        }
    }
}