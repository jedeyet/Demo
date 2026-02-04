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
        // ============================================================
        //   MÉTODO PARA OBTENER CONNECTION STRING DE VARIABLE DE ENTORNO
        // ============================================================
        private static string GetConnString(string envVar)
        {
            // Buscar en USER primero
            string valor = Environment.GetEnvironmentVariable(envVar, EnvironmentVariableTarget.User);

            // Si no existe, buscar en MACHINE
            if (string.IsNullOrEmpty(valor))
            {
                valor = Environment.GetEnvironmentVariable(envVar, EnvironmentVariableTarget.Machine);
            }

            if (string.IsNullOrEmpty(valor))
            {
                throw new Exception($"Variable de entorno '{envVar}' no encontrada. Por favor configúrela en el sistema.");
            }

            return valor;
        }

        // ============================================================
        //   CONEXIONES NOTASMESO (Académico)
        // ============================================================
        public static SqlConnection Con_1
        {
            get { return new SqlConnection(GetConnString("SQL_CONN_NOTASMESOXELA")); }
        }

        public static SqlConnection Con_X
        {
            get { return new SqlConnection(GetConnString("SQL_CONN_NOTASMESOXELA")); }
        }

        public static SqlConnection Con_G
        {
            get { return new SqlConnection(GetConnString("SQL_CONN_NOTASMESOGUATE")); }
        }

        public static SqlConnection Con_C
        {
            get { return new SqlConnection(GetConnString("SQL_CONN_NOTASMESOCOBAN")); }
        }

        public static SqlConnection Con_T
        {
            get { return new SqlConnection(GetConnString("SQL_CONN_NOTASMESOTEO")); }
        }

        public static SqlConnection Con_I
        {
            get { return new SqlConnection(GetConnString("SQL_CONN_NOTASMESOIZABAL")); }
        }

        public static SqlConnection Con_A
        {
            get { return new SqlConnection(GetConnString("SQL_CONN_NOTASMESOAMATITLAN")); }
        }

        public static SqlConnection Con_H
        {
            get { return new SqlConnection(GetConnString("SQL_CONN_NOTASMESOHONDURAS")); }
        }

        public static SqlConnection Con_S
        {
            get { return new SqlConnection(GetConnString("SQL_CONN_NOTASMESOSEMI")); }
        }


        // ============================================================
        //   CONEXIONES CAJA (Financiero - ConF_)
        // ============================================================
        public static SqlConnection ConF_1
        {
            get { return new SqlConnection(GetConnString("SQL_CONN_CAJAXELA")); }
        }

        public static SqlConnection ConF_X
        {
            get { return new SqlConnection(GetConnString("SQL_CONN_CAJAXELA")); }
        }

        public static SqlConnection ConF_G
        {
            get { return new SqlConnection(GetConnString("SQL_CONN_CAJAGUATE")); }
        }

        public static SqlConnection ConF_C
        {
            get { return new SqlConnection(GetConnString("SQL_CONN_CAJACOBAN")); }
        }

        public static SqlConnection ConF_T
        {
            get { return new SqlConnection(GetConnString("SQL_CONN_CAJATEO")); }
        }

        public static SqlConnection ConF_I
        {
            get { return new SqlConnection(GetConnString("SQL_CONN_CAJAIZABAL")); }
        }

        public static SqlConnection ConF_A
        {
            get { return new SqlConnection(GetConnString("SQL_CONN_CAJAAMATITLAN")); }
        }

        public static SqlConnection ConF_H
        {
            get { return new SqlConnection(GetConnString("SQL_CONN_CAJAHONDURAS")); }
        }

        public static SqlConnection ConF_S
        {
            get { return new SqlConnection(GetConnString("SQL_CONN_CAJASEMI")); }
        }



        // ============================================================
        //   MÉTODOS EXISTENTES (sin cambios)
        // ============================================================
        public static DataTable Consulta(string cadena)
        {
            SqlDataAdapter tabla = new SqlDataAdapter(cadena, Models.Conex.Con_1);
            DataTable dato = new DataTable();
            tabla.Fill(dato);
            return dato;
        }

        public static SqlConnection conexmoney(int NoConexion)
        {
            SqlConnection conex = new SqlConnection();
            if (NoConexion == 1) conex = ConF_X;
            if (NoConexion == 2) conex = ConF_G;
            if (NoConexion == 3) conex = ConF_C;
            if (NoConexion == 4) conex = ConF_T;
            if (NoConexion == 5) conex = ConF_I;
            if (NoConexion == 6) conex = ConF_A;
            if (NoConexion == 7) conex = ConF_H;
            if (NoConexion == 8) conex = ConF_S;
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
            if (NoConexion == 6) conex = Con_A;
            if (NoConexion == 7) conex = Con_H;
            if (NoConexion == 8) conex = Con_S;

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
            if (NoConexion == 6) conex = ConF_A;
            if (NoConexion == 7) conex = ConF_A;
            if (NoConexion == 8) conex = ConF_A;


            SqlDataAdapter tabla = new SqlDataAdapter(cadena, conex);
            DataTable dato = new DataTable();
            tabla.Fill(dato);
            return dato;
        }
    }
}



