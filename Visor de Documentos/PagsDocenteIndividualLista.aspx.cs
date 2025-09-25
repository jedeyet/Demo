using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Configuration;


namespace Visor_de_Documentos
{
    public partial class PagsDocenteIndividualLista : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargaDatos();
                
            }
        }

        private void CargaDatos()
        {
            string cadena = "Select * from catedraticos where codigo_catedratico=" + Session["c"];
            DataTable dt = new DataTable();
            int opcion = Convert.ToInt16(Session["carr"]);
            dt = Models.Conex.Consulta2(cadena,   opcion);
            lbNombre.Text = dt.Rows[0]["Nombre y apellidos del catedrático"].ToString();
            lbCUI.Text = dt.Rows[0]["cedula"].ToString();
            lbMovil.Text = dt.Rows[0]["celular"].ToString();
            lbTel.Text = dt.Rows[0]["telefono"].ToString();
            lbPais.Text = dt.Rows[0]["pais"].ToString();
            lbEmail.Text = dt.Rows[0]["Email"].ToString();
            lbEst.Text = dt.Rows[0]["E_Civil"].ToString();
            DateTime nacimiento = Convert.ToDateTime(dt.Rows[0]["Fecha_Nac"].ToString());
            lbNac.Text = nacimiento.ToShortDateString();
            lbNIT.Text = dt.Rows[0]["nit"].ToString();
            lbCta.Text = dt.Rows[0]["cuentaindustrial"].ToString();

            cadena = "Select * from VistaDocentesColegiado where [nombre y apellidos del catedrático]='" + lbNombre.Text + "'";
            dt.Clear();
            dt = Models.Conex.Consulta2(cadena, opcion);
            if (dt.Rows.Count<1)
            {
                lbCol.Text = "No se ha ingresado"; lbVen.Text = "No se ha ingresado"; lbDanger.Visible = false;
            }
            else
            {
                DateTime vencimiento = Convert.ToDateTime(dt.Rows[0]["VencimientoColegiado"].ToString());
                lbCol.Text = dt.Rows[0]["NoColegiado"].ToString(); lbVen.Text = vencimiento.ToShortDateString();
                TimeSpan resta = vencimiento - DateTime.Now;
                int dias = resta.Days;  
                if (dias<30 && dias>=0) 
                { lbDanger.Visible = true; lbDanger.Text = "Días para vencer: " + dias.ToString(); }
                else if (dias<0)
                { lbDanger.Visible = true; lbDanger.Text = "Días vencido: " + dias.ToString(); }
            }
        }
    }
}