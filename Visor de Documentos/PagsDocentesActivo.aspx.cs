using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Configuration;

namespace Visor_de_Documentos
{
    public partial class PagsDocentesActivo : System.Web.UI.Page
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
        string usuario = ""; int Nivel = 0;
        
        private void CargaDatos()
        {
            for (int x = 2002; x <= DateTime.Now.Year; x++)
                ddAnio.Items.Add(x.ToString());
            ddAnio.SelectedIndex = ddAnio.Items.Count - 1;
            ddSemestre.Items.Add("1");
            ddSemestre.Items.Add("2");

            for (int i = 1; i <=8; i++)
            {
                ddSem.Items.Add(i.ToString());
            }
         
            if (DateTime.Now.Month >= 7) ddSemestre.SelectedIndex = 1; else ddSemestre.SelectedIndex = 0;
        }
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
        private void CargaCarreras()
        {
            usuario = System.Web.HttpContext.Current.Session["usuario"].ToString().Trim();
            Nivel = Convert.ToInt16(System.Web.HttpContext.Current.Session["Nivel"].ToString());
            int opcion = Convert.ToInt32(DropDownList1.SelectedValue.ToString());
            string acti = " where Estado  = 'Activa'";
            if (opcion > 1) acti = " where Estado = True ";
            int opcioncarrera = 1;
            DataTable dt = new DataTable();
            string cadena = "";
            if (Nivel == 0)
            {
                cadena = "select [id Carrera], [Nombre de la carrera] from carrera where estado='Activa' or estado = 'True' order by  [Nombre de la carrera]";
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

        private void oculta(bool opcion)
        {
            imgbutExc.Visible = opcion;
            GridView1.Visible = opcion;

        }
        
        protected void Button1_Click(object sender, EventArgs e) //ver a todos los docentes
        {
            int opcion = Convert.ToInt32(DropDownList1.SelectedValue.ToString());
             
           
            string cadena = "select Codigo_Catedratico, [Nombre y apellidos del catedrático], " +
                " (select NoColegiado from VistaDocentesColegiado as t3 where t3.[Nombre y apellidos del catedrático] = " +
                " t1.[Nombre y apellidos del catedrático]) as Colegiado" +
                " from Vista_Asignacion_Curso_Profesor as t1 where [Nombre y apellidos del catedrático] not like 'equiv%' " +   
                " and[Nombre y apellidos del catedrático] not like 'sufi%' and[Nombre y apellidos del catedrático] not like 'aas%' " +
                " and[Id Carrera] = " + ddCarrera.SelectedValue.ToString()  + " group by[Nombre y apellidos del catedrático], " +
                " Codigo_Catedratico, email  order by[Nombre y apellidos del catedrático]";

            DataTable dt = Models.Conex.Consulta2(cadena, opcion);
            if (dt.Rows.Count == 0)
            { lbResultado.Text = "No se localizan docentes "; oculta(false); }

            else
            {
                lbResultado.Text = "Números de docentes localizados: " + dt.Rows.Count.ToString();
                GridView1.DataSource = dt;
                GridView1.DataBind(); oculta(true);
            }
        }

        protected void Button2_Click(object sender, EventArgs e)  //docentes activos
        {
            int opcion = Convert.ToInt32(DropDownList1.SelectedValue.ToString());
            string anio = ddAnio.SelectedValue.ToString();
            string semestre = ddSemestre.SelectedValue.ToString();
            string activo = "";

            string cadena = "select Codigo_Catedratico, [Nombre y apellidos del catedrático], " +
                " (select NoColegiado from VistaDocentesColegiado as t3 where t3.[Nombre y apellidos del catedrático] = t1.[Nombre y apellidos del catedrático]) as Colegiado," +
                " (select count(ano) from vista_asignacion_curso_profesor as t2 where t2.Codigo_Catedratico = t1.Codigo_Catedratico " +
                "and ano = " + anio + " and semestrecursado=" + semestre + " and [Id Carrera]= " + ddCarrera.SelectedValue.ToString() + 
                ") as Cursos from Vista_Asignacion_Curso_Profesor as t1 where [Nombre y apellidos del catedrático] not like 'equiv%' " +
                " and[Nombre y apellidos del catedrático] not like 'sufi%' and[Nombre y apellidos del catedrático] not like 'aas%' " +
                " and ano = " + ddAnio.Text + " and semestrecursado= " + ddSemestre.Text +
                " and[Id Carrera] = " + ddCarrera.SelectedValue.ToString() + activo + " group by[Nombre y apellidos del catedrático], " +
                " Codigo_Catedratico, email  order by[Nombre y apellidos del catedrático]";

            DataTable dt = Models.Conex.Consulta2(cadena, opcion);
            if (dt.Rows.Count == 0)
            { lbResultado.Text = "No se localizan docentes activos"; oculta(false); }

            else
            {
                lbResultado.Text = "Números de docentes localizados: " + dt.Rows.Count.ToString();
                GridView1.DataSource = dt;
                GridView1.DataBind(); oculta(true);
            }


        }

        protected void Button3_Click(object sender, EventArgs e) //docentes inactivos
        {
            int opcion = Convert.ToInt32(DropDownList1.SelectedValue.ToString());
            string cadena = "select Codigo_Catedratico, [Nombre y apellidos del catedrático],  " +
            "(select NoColegiado from VistaDocentesColegiado as t3  where t3.[Nombre y apellidos del catedrático] = " +
            "t1.[Nombre y apellidos del catedrático]) as Colegiado from Vista_Asignacion_Curso_Profesor as t1 where " +
            "[Nombre y apellidos del catedrático] not like 'equiv%' and[Nombre y apellidos del catedrático] not like 'sufi%' " +
            "and[Nombre y apellidos del catedrático] not like 'aas%' and[Id Carrera] = " + ddCarrera.SelectedValue.ToString() +
            "and(select count(ano) from vista_asignacion_curso_profesor as t2 where t2.Codigo_Catedratico = t1.Codigo_Catedratico and " +
            "ano = " + ddAnio.Text + " and SemestreCursado = " + ddSemestre.Text + ") = 0 group by[Nombre y apellidos del catedrático], " +
            " Codigo_Catedratico, email  order by[Nombre y apellidos del catedrático]";
            DataTable dt = Models.Conex.Consulta2(cadena, opcion);
            
            if (dt.Rows.Count == 0)
            { lbResultado.Text = "No se localizan docentes inactivos"; oculta(false); }

            else
            {
                lbResultado.Text = "Números de docentes localizados: " + dt.Rows.Count.ToString();
                GridView1.DataSource = dt;
                GridView1.DataBind(); oculta(true);
            }
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
            oculta(false); lbResultado.Text = "";
        }

        private void ExportaExcel(GridView grid)
        {
            HttpResponse ResponsePage = Response;
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page pageToRender = new Page();
            HtmlForm form = new HtmlForm();
            form.Controls.Add(grid);
            pageToRender.Controls.Add(form);
            ResponsePage.Clear();
            ResponsePage.Buffer = true;
            ResponsePage.ContentType = "application/vnd.ms-excel";
            string namereport = "Informe.xls";
            ResponsePage.AddHeader("Content-Disposition", "attachment;filename=" + namereport);
            ResponsePage.Charset = "UTF-8";
            ResponsePage.ContentEncoding = Encoding.Default;
            pageToRender.RenderControl(htw);
            ResponsePage.Write(sw.ToString());
            ResponsePage.End();
        }
        protected void imgbutExc_Click(object sender, ImageClickEventArgs e)
        {
            ExportaExcel(GridView1);
        }

        protected void ddCarrera_SelectedIndexChanged(object sender, EventArgs e)
        {
            oculta(false);
            lbResultado.Text = "";
            string car = ddCarrera.SelectedItem.Text;  string carrera = car.Substring(0, 5);
            if (carrera == "Maest")
            { Label3.Visible = false; ddSemestre.Visible = false; Label5.Visible = true;  ddSem.Visible = true; }
            else
            { Label3.Visible = true; ddSemestre.Visible = true; Label5.Visible = false; ddSem.Visible = false; }
        }
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the currently selected row using the SelectedRow property.
            GridViewRow row = GridView1.SelectedRow;

            // Display the first name from the selected row.
            // In this example, the third column (index 2) contains
            // the first name.
            Session["c"] = row.Cells[1].Text;
            Session["carr"] = Convert.ToInt32(DropDownList1.SelectedValue.ToString());

            // Response.Redirect("PagsDocenteIndividualLista.aspx");

            ClientScript.RegisterStartupScript(this.Page.GetType(), "", "window.open('PagsDocenteIndividualLista.aspx','Graph','height=600,width=600');", true);



        }

        protected void btNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("PagsDocentesActivo.aspx");
        }
    }
}