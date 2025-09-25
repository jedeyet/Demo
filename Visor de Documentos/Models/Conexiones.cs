using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;


namespace Visor_de_Documentos.Models
{
    public class Conexiones
    {
        public static SqlConnection Con_1 = new SqlConnection("Data Source=GP4;Initial Catalog=NOTASMESO; User ID=Local; Password='L3ct0rL0c4l'");

        public static DataTable Consulta(string cadena)
        {
            SqlDataAdapter tabla = new SqlDataAdapter(cadena, Models.Conexiones.Con_1);
            DataTable dato = new DataTable();
            tabla.Fill(dato);
            return dato;
        }
    }
}