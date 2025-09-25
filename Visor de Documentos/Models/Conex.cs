using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace Visor_de_Documentos.Models
{
    public class Conex
    {
        public static SqlConnection Con_1 = new SqlConnection("Data Source=GP4;Initial Catalog=NOTASMESO; User ID=Local; Password='L3ct0rL0c4l'");
        public static SqlConnection Con_X = new SqlConnection("Data Source=GP4;Initial Catalog=NOTASMESO; User ID=Local; Password='L3ct0rL0c4l'");
        public static SqlConnection Con_G = new SqlConnection("Data Source = Academico.umes.edu.gt; Initial Catalog = NOTASMESO; User ID = Local; Password='LectorUm3sLoc4l'");
        public static SqlConnection Con_C = new SqlConnection("Data Source = Academico.umes.edu.gt; Initial Catalog = NOTASMESOCOBAN; User ID = Local; Password='LectorUm3sLoc4l'");
        public static SqlConnection Con_T = new SqlConnection("Data Source = Academico.umes.edu.gt; Initial Catalog = NOTASMESOTEO; User ID = Local; Password='LectorUm3sLoc4l'");
        public static SqlConnection Con_I = new SqlConnection("Data Source = Academico.umes.edu.gt; Initial Catalog = NOTASMESOIZABAL; User ID = Local; Password='LectorUm3sLoc4l'");
        public static SqlConnection Con_A = new SqlConnection("Data Source = Academico.umes.edu.gt; Initial Catalog = NOTASMESOamat; User ID = Local; Password='LectorUm3sLoc4l'");

        public static SqlConnection ConF_1 = new SqlConnection("Data Source=GP4;Initial Catalog=Caja; User ID=Local; Password='L3ct0rL0c4l'");
        public static SqlConnection ConF_X = new SqlConnection("Data Source=GP4;Initial Catalog=Caja; User ID=Local; Password='L3ct0rL0c4l'");
        public static SqlConnection ConF_G = new SqlConnection("Data Source = Academico.umes.edu.gt; Initial Catalog = Caja; User ID = Local; Password='LectorUm3sLoc4l'");
        public static SqlConnection ConF_C = new SqlConnection("Data Source = Academico.umes.edu.gt; Initial Catalog = cajaCOBAN; User ID = Local; Password='LectorUm3sLoc4l'");
        public static SqlConnection ConF_T = new SqlConnection("Data Source = Academico.umes.edu.gt; Initial Catalog = cajaTEO; User ID = Local; Password='LectorUm3sLoc4l'");
        public static SqlConnection ConF_I = new SqlConnection("Data Source = Academico.umes.edu.gt; Initial Catalog = cajaIZABAL; User ID = Local; Password='LectorUm3sLoc4l'");
        public static SqlConnection ConF_A = new SqlConnection("Data Source = Academico.umes.edu.gt; Initial Catalog = cajaAmat; User ID = Local; Password='LectorUm3sLoc4l'");



        public static DataTable Consulta(string cadena)
        {
            SqlDataAdapter tabla = new SqlDataAdapter(cadena, Models.Conex.Con_1);  
            DataTable dato = new DataTable();
            tabla.Fill(dato);
            return dato;
        }

        public static SqlConnection conexmoney (int NoConexion)
        {
            SqlConnection conex = new SqlConnection();
            if (NoConexion == 1) conex = ConF_X;
            if (NoConexion == 2) conex = ConF_G;
            if (NoConexion == 3) conex = ConF_C;
            if (NoConexion == 4) conex = ConF_T;
            if (NoConexion == 5) conex = ConF_I;
            return conex;
        }

        public static DataTable Consulta2(string cadena, int NoConexion)
        {

            SqlConnection conex = new SqlConnection();
            if (NoConexion == 1) conex = Con_X;
            if (NoConexion == 2) conex = Con_G;
            if (NoConexion == 3) conex = Con_C;
            if (NoConexion == 4) conex = Con_T;
            if (NoConexion == 5) conex = Con_I;
            if (NoConexion == 7) conex = Con_A;

            SqlDataAdapter tabla = new SqlDataAdapter(cadena, conex);
            DataTable dato = new DataTable();
            tabla.Fill(dato);
            return dato;
        }

        public static DataTable ConsultaCaja(string cadena, int NoConexion)
        {

            SqlConnection conex = new SqlConnection();
            if (NoConexion == 1) conex = ConF_X;
            if (NoConexion == 2) conex = ConF_G;
            if (NoConexion == 3) conex = ConF_C;
            if (NoConexion == 4) conex = ConF_T;
            if (NoConexion == 5) conex = ConF_I;

            SqlDataAdapter tabla = new SqlDataAdapter(cadena, conex);
            DataTable dato = new DataTable();
            tabla.Fill(dato);
            return dato;
        }
    }
}