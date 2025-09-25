using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Visor_de_Documentos
{
    public partial class PagsAlumnosEntrevista : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                txCarnet.Focus();  
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string cadena = "select carnet, (select nombre_completo from AlumnoTemporal as t2 where t2.carnet_temporal=carnet) " +
                "as Nombre, (select pensum_asignado from AlumnoTemporal as t2 where t2.carnet_temporal=carnet) " +
                "as Pensum, entrevista, Fecha, Entrevistado from Alumno_Entrevista where carnet = '" + txCarnet.Text + "'";
            DataTable dtinterview = Models.Conex.Consulta2(cadena, 2);
            if (dtinterview.Rows.Count == 0)
            { lbnombre.Text = "No se localizan coincidencias";
                btRegistrar.Visible = false; }
            else if (Convert.ToBoolean(dtinterview.Rows[0]["Entrevistado"].ToString())==true)
            {
                lbnombre.Text = dtinterview.Rows[0]["Nombre"].ToString();
                lbFecha.Text = "Estudiante ya entrevistado en la fecha " + dtinterview.Rows[0]["fecha"].ToString();
            }
            else
            {
                lbPensum.Text = dtinterview.Rows[0]["Pensum"].ToString();
                btRegistrar.Visible = true;
            }
        }

        protected void btRegistrar_Click(object sender, EventArgs e)
        {
            string actualiza = "Update alumno_entrevista set fecha = getdate(), entrevistado=1 where carnet='" + txCarnet.Text + "'";
            DataTable dtinterview = Models.Conex.Consulta2(actualiza, 2);
            lbFecha.Text = "La entrevista se ha registrado exitosamente";
            RegistraCursos();
            btRegistrar.Visible = false;
        }

        private void RegistraCursos()
        {
             
        }

        protected void btNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("PagsAlumnosEntrevista.aspx");
        }
    }
}