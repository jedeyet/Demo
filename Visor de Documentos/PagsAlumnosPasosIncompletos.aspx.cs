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


namespace Visor_de_Documentos
{
    public partial class PagsAlumnosPasosIncompletos : System.Web.UI.Page
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
            for (int x = 2000; x <= DateTime.Now.Year; x++)
                ddAnio.Items.Add(x.ToString());
            ddAnio.SelectedIndex = ddAnio.Items.Count - 1;
            ddSemestre.Items.Add("1");
            ddSemestre.Items.Add("2");
            if (DateTime.Now.Month >= 7) ddSemestre.SelectedIndex = 1; else ddSemestre.SelectedIndex = 0;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try { 
            int opcion = Convert.ToInt32(DropDownList1.SelectedValue.ToString());
            string anio = ddAnio.SelectedValue.ToString();
            string semestre = ddSemestre.SelectedValue.ToString();
            DataTable dt = new DataTable();
            string sede = "notasmesoxela"; string sedecaja = "cajaxela";
            switch (opcion)
                {
                case 2:
                    sede = "notasmesoguate"; sedecaja = "cajaguate";
                    break;
                case 3:
                    sede = "notasmesocoban"; sedecaja = "cajacoban";
                    break;
                case 4:
                    sede = "notasmesoteo"; sedecaja = "cajateo";
                    break;
                case 5:
                    sede = "notasmesoizabal"; sedecaja = "cajaizabal";
                    break;
                case 6:
                    sede = "notasmesoamat"; sedecaja = "cajaamat";
                    break;
            }

            string cadena = $@"
                    SELECT DISTINCT C.carnet
                    FROM {sedecaja}.dbo.Contrato C
                    INNER JOIN {sedecaja}.dbo.ConfiguraSemestres CS
                        ON C.id_configurasemestre = CS.id_configurasemestre
                    WHERE YEAR(C.fecha_inicio_contrato) = {anio}
                      AND CS.ciclo_anual = {semestre}
                      AND C.estado_contrato = 1
                      AND C.id_inscripcion > 0
                      AND NOT EXISTS (
                            SELECT 1
                            FROM {sede}.dbo.Inscripcion I
                            WHERE I.Numero_Carnet COLLATE DATABASE_DEFAULT
                                  = C.carnet COLLATE DATABASE_DEFAULT
                              AND I.ano = {anio}
                              AND I.SemestreAnual = {semestre}
                      )";
          
            dt = Models.Conex.Consulta2(cadena, opcion);
                if (dt.Rows.Count == 0)
                {
                    lbResultado.Text = "No se localizan alumnos con pasos incompletos";
                    lbResultado.Visible = true; GridView1.Visible = false;
                    imgbutExc.Visible = false;
                }
                else
                {

                    dt.Columns.Add("nombre", typeof(string));
                    dt.Columns.Add("carrera", typeof(string));

                    int numerofilas = dt.Rows.Count;
                    for (int i = 0; i < numerofilas; i++)
                    {

                        string carnet = dt.Rows[i][0].ToString();

                        string cadnom = "Select * from alumno where [número de carné]='" + carnet + "'";
                        DataTable dtnombre = Models.Conex.Consulta2(cadnom, opcion);
                        if (dtnombre.Rows.Count < 1)
                        {
                            dt.Rows[i]["nombre"] = "Estudiante sin traslado de almacenamiento temporal";
                            string cod = carnet.Substring(4, 2);
                            string cadCarr = "Select * from carrera where[Id Carrera] = " + cod;
                            DataTable dtCarr = Models.Conex.Consulta2(cadCarr, opcion);
                            dt.Rows[i]["carrera"] = dtCarr.Rows[0]["nombre de la carrera"].ToString();
                        }
                        else
                        {
                            dt.Rows[i]["nombre"] = dtnombre.Rows[0]["Nombre_completo"].ToString();
                            string cod = carnet.Substring(4, 2);
                            string cadCarr = "Select * from carrera where[Id Carrera] = " + cod;
                            DataTable dtCarr = Models.Conex.Consulta2(cadCarr, opcion);
                            dt.Rows[i]["carrera"] = dtCarr.Rows[0]["nombre de la carrera"].ToString();
                        }
                    }

                    DataView vista = new DataView(dt);
                    vista.Sort = "carrera asc, nombre asc";

                    imgbutExc.Visible = true;

                    GridView1.DataSource = vista;
                    GridView1.DataBind();
                    lbResultado.Visible = true; GridView1.Visible = true; lbResultado.Text = "Alumnos localizados: " + dt.Rows.Count.ToString(); 
                }
                
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Ocurrió un problema al consultar los datos.');</script>");
            }
        }
        
        protected void imgbutExc_Click(object sender, ImageClickEventArgs e)
        {
            ExportaExcel(GridView1);
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

        protected void btNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("PagsAlumnosPasosIncompletos.aspx");
        }
    }
}