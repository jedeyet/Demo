using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Visor_de_Documentos
{
    public partial class Estudiantes3Perdidos : System.Web.UI.Page
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
            for (int x = 2002; x <= DateTime.Now.Year; x++)
                ddAnio.Items.Add(x.ToString());
            ddAnio.SelectedIndex = ddAnio.Items.Count - 1;
            ddSemestre.Items.Add("1");
            ddSemestre.Items.Add("2");
            if (DateTime.Now.Month >= 7) ddSemestre.SelectedIndex = 1; else ddSemestre.SelectedIndex = 0;
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            int opcion = Convert.ToInt32(DropDownList1.SelectedValue.ToString());
            string anio = ddAnio.SelectedValue.ToString();
            string semestre = ddSemestre.SelectedValue.ToString();
            DataTable dt = new DataTable();
            string cadena = "select [Número de carné] as Carnet, Nombre_Completo as Estudiante, [Nombre de la carrera] as Carrera, " +
                "semestre_Cursado as Ciclo,count(*) as Perdidos " +
              "from vista_notas where ano = " + anio + "and semestre = " + semestre + " and gano = 0 and finales = 'Si' " +
              "group by[Número de carné], Nombre_Completo, [Nombre de la carrera], semestre_Cursado having count(*)> 2 " +
              "order by[Nombre de la carrera], semestre_Cursado, Nombre_Completo";
            dt = Models.Conex.Consulta2(cadena, opcion);
            if (dt.Rows.Count == 0) lbResultado.Text = "No se localizan estudiantes con 3 o más cursos perdidos";
            else
            {
                lbResultado.Text = "Números de estudiantes localizados: " + dt.Rows.Count.ToString();
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
        }

        protected void btNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("Estudiantes3Perdidos.aspx");
        }
    }
}