using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using VisorDeDocumentos.Models;

namespace Visor_de_Documentos.Pages
{
    public partial class Academia : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["car"] != null)
                txtCarne.Text = Session["car"].ToString();
            

        }
        //Actuales es un modelo que se declara de alcance global en el formulario y está direccionado a Vista_Notas
        Models.Historial_NotasDataContext Actuales = new Models.Historial_NotasDataContext();

        protected void btActuales_Click(object sender, EventArgs e)
        {

            OcultaGrillas();
            int sem = 1;
            if (DateTime.Now.Month >= 7 && DateTime.Now.Month <= 11) sem = 2;
            VerCursos(DateTime.Now.Year, sem); //Llama a este procedimiento para mostrar si tiene cursos actuales asignados.
        }


        private void VerCursos(int anio, int sem)  //Despliega los cursos asignados en un año y semestre o trimestre (1 a 4) respectivos
        {
            var actuales = (from A in Actuales.Vista_Notas
                            where A.Número_de_carné == txtCarne.Text && A.ano == anio && A.semestre == sem
                            select new { Asignatura = A.Asignatura, Zona = A.Zona, Examen_Final = A.Examen_final, Nota_Final = A.Nota_final }).ToList();
            lbResultado.Text = "No se localizan coincidencias";
            if (actuales.Count > 0)
            {
                GridViewActuales.DataSource = actuales;
                GridViewActuales.DataBind();
                lbResultado.Text = "Registros localizados: " + actuales.Count.ToString();
                GridViewActuales.HeaderRow.TableSection = TableRowSection.TableHeader;
                Numerin(GridViewActuales, "lbNum");
                GridViewActuales.Visible = true;
                LlenaAnios();  //Llena el dropdownlist ddAnio
            }
            else
                GridViewActuales.Visible = false;
        }


        protected void LlenaAnios()  //Este procedimiento llena el combo de años (ddAnio) en los que un alumno ha estado inscrito
        {
            var meses = (from M in Actuales.Vista_Notas where M.Número_de_carné == txtCarne.Text orderby M.ano
                         group M by M.ano into grupo select grupo).ToList();
            if (meses.Count < 1)
                Panel1.Visible = false;
            else
            {
                foreach (var objeto in meses)
                {
                    ddAnio.Items.Add(objeto.Key.ToString());
                }
                Panel1.Visible = true;
            }
        }

        protected void btBuscar_Click(object sender, EventArgs e)  //Este botón llama también a VerCursos enviando un año y semestre seleccionados de los combos
        {
            int sem = Convert.ToInt16(ddSem.SelectedValue.ToString());
            int anio = Convert.ToInt16(ddAnio.SelectedValue.ToString());
            VerCursos(anio,sem);
        }


        protected void OcultaGrillas()  //Oculta todas las cuadriculas y el panel1
        { 
            GridViewActuales.Visible = false;
            GridViewNotas.Visible = false;
            GridViewPensum.Visible = false;
            Panel1.Visible = false;

        }
        protected void btAprobados_Click(object sender, EventArgs e)
        {
            carga(1);
        }

        protected void btNoAprobados_Click(object sender, EventArgs e)
        {
            carga(2);
        }

        protected void btEquivalencias_Click(object sender, EventArgs e)
        {
            carga(3);
        }

        protected void btSuficiencias_Click(object sender, EventArgs e)
        {
            carga(4);
        }


        private void Numerin(GridView Grid, string objeto)  //Este procedimiento llena la primer columna de una cuadricula con números de 1 en adelante.  Recibe 
            //el nombre de la cuadricula que se llenará y el nombre de la etiqueta que se encuentra en dicha cuadricula
        {
            for (int fila = 0; fila < Grid.Rows.Count; fila++)
            {
                Label Numero = (Label)Grid.Rows[fila].FindControl(objeto);
                Numero.Text = (fila + 1).ToString();
            }
        }

        private void carga(int opcion) //este procedimiento es llamado del segundo al quinto botón.  Se llama a un procedimiento almacenado que recibe como 
            //parámetros el número de carnet del alumno y un número entero para saber si se desea aprobados, no aprobados, equivalencias o suficiencias. 
        {
            OcultaGrillas();
            LbInfo.Visible = false;
            Models.ProcedimientosDataContext consultas = new Models.ProcedimientosDataContext();
            
            //Llama al procedimiento almacenado Ver_cursos_Alumnos, recibe como parámetros el carnet del alumno y un número para la consulta respectiva
            var datos = consultas.Ver_Cursos_Alumno(txtCarne.Text, opcion).ToList();
            lbResultado.Text = "No se localizan coincidencias";
            if (datos.Count > 0)
            {
                GridViewNotas.DataSource = datos;
                GridViewNotas.DataBind();
                GridViewNotas.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridViewNotas.Visible = true;
                lbResultado.Text = "Registros localizados: " + datos.Count.ToString();
                Numerin(GridViewNotas, "lbNum");
            }
            else
                GridViewNotas.Visible = false;
            
        }

        protected void EnBaseaPensum_Click(object sender, EventArgs e)  //Esta opción revisa el pensum asignado al alumno, curso a curso para saber si lo tiene
            //aprobado o no.   Busca la nota más alta por si el alumno ha cursado una misma materia varias veces, analiza si está ganado con 0. 
        {
            OcultaGrillas();
            GridViewPensum.Visible = true;
            LbInfo.Visible = true;
            //Modelo para ver los datos de un alumno.  dirigido a la tabla Alumno
            Models.DataAlumnoDataContext alumno = new Models.DataAlumnoDataContext();
            var consul = (from C in alumno.alumno where C.Número_de_carné  == txtCarne.Text select C).ToList();
            //Muestra el Pensum asignado en la etiqueta lbPensum
            lbPensum.Text = consul[0].Pensum_Asignado.ToString();

            //Llena la cuadricula con el pensum que tiene el alumno asignado

            //Modelo para conocer el pensum que tiene asignado el alumno  y llena la cuadricula GridviewPensum con los cursos respectivos
            Models.DataAsignaturasDataContext Asignaturas = new Models.DataAsignaturasDataContext();
            var LlenaPensum = (from P in Asignaturas.Pensum_Asignaturas where P.ID_Encabezado_Pensum == lbPensum.Text orderby P.No_curso select P);
            GridViewPensum.DataSource = LlenaPensum;
            GridViewPensum.DataBind();

            //Encunetra los punteos de los cursos aprobados
            //Modelo para ver el historial de notas de un alumno.  Dirigido a vista notas
            Models.Historial_NotasDataContext Pensum = new Models.Historial_NotasDataContext();
            int faltantes  = 0;  int aprobados = 0; double suma = 0; //contadores y acumulador para cursos faltantes, aprobados y suma de punteos (para promedio)
            int aprob_nota = 0;
            for (int fila = 0; fila < GridViewPensum.Rows.Count; fila++)
            {

                Label Numero = (Label)GridViewPensum.Rows[fila].FindControl("lbNumero");
                Numero.Text = (fila + 1).ToString();

                Label Asig = (Label)GridViewPensum.Rows[fila].FindControl("lbAsignatura");
                Label Fecha = (Label)GridViewPensum.Rows[fila].FindControl("lbFecha");
                Label Tipo = (Label)GridViewPensum.Rows[fila].FindControl("lbTipo");
                Label P = (Label)GridViewPensum.Rows[fila].FindControl("lbPunteo");

                //consulta para ver si se localiza el curso del pensum en los cursos aprobados por el alumno.  Vista notas
                var aprobado = (from A in Pensum.Vista_Notas where A.Número_de_carné == txtCarne.Text &&
                                A.Asignatura.ToString().Trim() == Asig.Text && A.gano == true orderby A.Nota_final descending select A).ToList();
                String nota = "No";
                if (aprobado.Count > 0)
                {
                    if (aprobado[0].Nota_final == 0)  //Cursos como seminario que se aprueban con 0 o equivalencias externas
                        nota = "AP";
                    else
                    {
                        nota = aprobado[0].Nota_final.ToString();
                        if (aprobado[0].TipoNota.ToString() == "Equivalencia Externa") //no se suma porque no tiene punteo para el cálculo del promedio
                        { }
                        else
                        { suma += Convert.ToDouble(aprobado[0].Nota_final); aprob_nota += 1; } //solo se suma si es un curso aprobado con punteo
                        aprobados += 1;
                    }
                    //se muestra la fecha de aprobación con formato dd/mm/yyyy
                    Fecha.Text = String.Format("{0:dd/MM/yyyy}", aprobado[0].Fecha_aprobación);
                    Tipo.Text = aprobado[0].TipoNota.ToString(); //controla las notas por equivalencia
                }
                else
                {
                    faltantes++;
                    P.BackColor = System.Drawing.Color.Red;
                    P.ForeColor = System.Drawing.Color.White;
                    P.Font.Bold = true; 
                }
                P.Text = nota;  //Envía a la etiqueta de nota el valor localizado. 
            }
            if (aprob_nota == 0)
                lbResultado.Text = "No hay cursos aprobados";
            else
                lbResultado.Text = "Promedio de cursos aprobados: " + (suma / aprob_nota).ToString("f");
             
            LbInfo.Text = "Cursos restantes para cerrar pensum: " + faltantes.ToString();
            if (aprobados== GridViewPensum.Rows.Count -1)  LbInfo.Text = "El alumno tiene el pensum cerrado";
        }

       
    }
}