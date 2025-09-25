using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Visor_de_Documentos.Models;

namespace Visor_de_Documentos
{
    public class DocumentoEscaneado
    {        
        [Display(Name = "Nombre del Documento")]
        public string Documento { get; set; }

        public DateTime? Fecha { get; set; }

        [Browsable(false)]
        public string Alumno { get; set; }

      
    }

    public partial class PagsReporteEscaneosPorAlumno : System.Web.UI.Page
    {
        protected void Limpiar()
        {
            GridReporte.DataSource = null;
            GridReporte.DataBind();            
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnReporte_Click(object sender, EventArgs e)
        {
            string carne = TextBoxCarne.Text;

            if (DropDownListSede.SelectedValue == "1")
            {

                NOTASMESOEntitiesEscaneos DB = new NOTASMESOEntitiesEscaneos();

                List<DocumentoEscaneado> documentosEscaneados = (from r in DB.View_EscaneosDetalle
                                                                 where r.Número_de_carné == carne
                                                                 select new DocumentoEscaneado
                                                                 {
                                                                     Documento = r.nombrearchivo,
                                                                     Alumno = r.Nombre_Completo,
                                                                     Fecha = r.Fecha,
                                                                 }).OrderBy(r => r.Documento)
                                           .ToList();


                for (int i = 0; i < documentosEscaneados.Count; i++)
                {
                    string archivo = documentosEscaneados[i].Documento;
                    int arroba = (archivo.IndexOf("@") + 1);
                    int guion = (archivo.IndexOf("-"));

                    //dejar solo el nombre
                    string nombre = archivo.Normalize();
                    nombre = nombre.Substring(arroba, guion - arroba);
                    nombre = nombre.Replace("_", " ");
                    documentosEscaneados[i].Documento = nombre;

                }

                if (documentosEscaneados.Count > 0)
                {
                    lblAlumno.Text = documentosEscaneados.First().Alumno;
                    GridReporte.AutoGenerateColumns = true;
                    GridReporte.DataSource = documentosEscaneados;
                    GridReporte.DataBind();
                }
                else
                    lblAlumno.Text = "No se han escaneado documentos de ese alumno. O no es de la sede seleccionada";




            }
            else if (DropDownListSede.SelectedValue == "2")
            {
                NOTASMESOEntitiesEscaneosGuate DB = new NOTASMESOEntitiesEscaneosGuate();

                List<DocumentoEscaneado> documentosEscaneados = (from r in DB.View_EscaneosDetalle
                                           where r.Número_de_carné == carne
                                           select new DocumentoEscaneado
                                           { Documento = r.nombrearchivo,
                                             Alumno = r.Nombre_Completo,
                                             Fecha = r.Fecha,
                                           }).OrderBy(r => r.Documento)
                                           .ToList();
                

                for (int i = 0; i < documentosEscaneados.Count; i++)
                {
                    string archivo = documentosEscaneados[i].Documento;
                    int arroba = (archivo.IndexOf("@") + 1);
                    int guion = (archivo.IndexOf("-"));

                    //dejar solo el nombre
                    string nombre = archivo.Normalize();
                    nombre = nombre.Substring(arroba, guion - arroba);
                    nombre = nombre.Replace("_", " ");
                    documentosEscaneados[i].Documento = nombre;

                }

                if (documentosEscaneados.Count > 0)
                {
                    lblAlumno.Text = documentosEscaneados.First().Alumno;
                    GridReporte.AutoGenerateColumns = true;
                    GridReporte.DataSource = documentosEscaneados;
                    GridReporte.DataBind();
                }
                else
                    lblAlumno.Text = "No se han escaneado documentos de ese alumno. O no es de la sede seleccionada";


            }
            else if (DropDownListSede.SelectedValue == "3")
            {
                NOTASMESOCOBANEntitiesEscaneos DB = new NOTASMESOCOBANEntitiesEscaneos();

                List<DocumentoEscaneado> documentosEscaneados = (from r in DB.View_EscaneosDetalle
                                                                 where r.Número_de_carné == carne
                                                                 select new DocumentoEscaneado
                                                                 {
                                                                     Documento = r.nombrearchivo,
                                                                     Alumno = r.Nombre_Completo,
                                                                     Fecha = r.Fecha,
                                                                 }).OrderBy(r => r.Documento)
                                           .ToList();


                for (int i = 0; i < documentosEscaneados.Count; i++)
                {
                    string archivo = documentosEscaneados[i].Documento;
                    int arroba = (archivo.IndexOf("@") + 1);
                    int guion = (archivo.IndexOf("-"));

                    //dejar solo el nombre
                    string nombre = archivo.Normalize();
                    nombre = nombre.Substring(arroba, guion - arroba);
                    nombre = nombre.Replace("_", " ");
                    documentosEscaneados[i].Documento = nombre;

                }

                if (documentosEscaneados.Count > 0)
                {
                    lblAlumno.Text = documentosEscaneados.First().Alumno;
                    GridReporte.AutoGenerateColumns = true;
                    GridReporte.DataSource = documentosEscaneados;
                    GridReporte.DataBind();
                }
                else
                    lblAlumno.Text = "No se han escaneado documentos de ese alumno. O no es de la sede seleccionada";

            }
            else if (DropDownListSede.SelectedValue == "4")
            {
                NOTASMESOTEOEntitiesEscaneo DB = new NOTASMESOTEOEntitiesEscaneo();

                List<DocumentoEscaneado> documentosEscaneados = (from r in DB.View_EscaneosDetalle
                                                                 where r.Número_de_carné == carne
                                                                 select new DocumentoEscaneado
                                                                 {
                                                                     Documento = r.nombrearchivo,
                                                                     Alumno = r.Nombre_Completo,
                                                                     Fecha = r.Fecha,
                                                                 }).OrderBy(r => r.Documento)
                                           .ToList();


                for (int i = 0; i < documentosEscaneados.Count; i++)
                {
                    string archivo = documentosEscaneados[i].Documento;
                    int arroba = (archivo.IndexOf("@") + 1);
                    int guion = (archivo.IndexOf("-"));

                    //dejar solo el nombre
                    string nombre = archivo.Normalize();
                    nombre = nombre.Substring(arroba, guion - arroba);
                    nombre = nombre.Replace("_", " ");
                    documentosEscaneados[i].Documento = nombre;

                }

                if (documentosEscaneados.Count > 0)
                {
                    lblAlumno.Text = documentosEscaneados.First().Alumno;
                    GridReporte.AutoGenerateColumns = true;
                    GridReporte.DataSource = documentosEscaneados;
                    GridReporte.DataBind();
                }
                else
                    lblAlumno.Text = "No se han escaneado documentos de ese alumno. O no es de la sede seleccionada";

            }
            else if (DropDownListSede.SelectedValue == "5")
            {
                NOTASMESOIZABALEntitiesEscaneos DB = new NOTASMESOIZABALEntitiesEscaneos();

                List<DocumentoEscaneado> documentosEscaneados = (from r in DB.View_EscaneosDetalle
                                                                 where r.Número_de_carné == carne
                                                                 select new DocumentoEscaneado
                                                                 {
                                                                     Documento = r.nombrearchivo,
                                                                     Alumno = r.Nombre_Completo,
                                                                     Fecha = r.Fecha,
                                                                 }).OrderBy(r => r.Documento)
                                           .ToList();


                for (int i = 0; i < documentosEscaneados.Count; i++)
                {
                    string archivo = documentosEscaneados[i].Documento;
                    int arroba = (archivo.IndexOf("@") + 1);
                    int guion = (archivo.IndexOf("-"));

                    //dejar solo el nombre
                    string nombre = archivo.Normalize();
                    nombre = nombre.Substring(arroba, guion - arroba);
                    nombre = nombre.Replace("_", " ");
                    documentosEscaneados[i].Documento = nombre;

                }

                if (documentosEscaneados.Count > 0)
                {
                    lblAlumno.Text = documentosEscaneados.First().Alumno;
                    GridReporte.AutoGenerateColumns = true;
                    GridReporte.DataSource = documentosEscaneados;
                    GridReporte.DataBind();
                }
                else
                    lblAlumno.Text = "No se han escaneado documentos de ese alumno. O no es de la sede seleccionada";

            }
            else if (DropDownListSede.SelectedValue == "6")
            {
                NOTASMESOAMATITTLANEntitiesEscaneos DB = new NOTASMESOAMATITTLANEntitiesEscaneos();

                List<DocumentoEscaneado> documentosEscaneados = (from r in DB.View_EscaneosDetalle
                                                                 where r.Número_de_carné == carne
                                                                 select new DocumentoEscaneado
                                                                 {
                                                                     Documento = r.nombrearchivo,
                                                                     Alumno = r.Nombre_Completo,
                                                                     Fecha = r.Fecha,
                                                                 }).OrderBy(r => r.Documento)
                                           .ToList();


                for (int i = 0; i < documentosEscaneados.Count; i++)
                {
                    string archivo = documentosEscaneados[i].Documento;
                    int arroba = (archivo.IndexOf("@") + 1);
                    int guion = (archivo.IndexOf("-"));

                    //dejar solo el nombre
                    string nombre = archivo.Normalize();
                    nombre = nombre.Substring(arroba, guion - arroba);
                    nombre = nombre.Replace("_", " ");
                    documentosEscaneados[i].Documento = nombre;

                }

                if (documentosEscaneados.Count > 0)
                {
                    lblAlumno.Text = documentosEscaneados.First().Alumno;
                    GridReporte.AutoGenerateColumns = true;
                    GridReporte.DataSource = documentosEscaneados;
                    GridReporte.DataBind();
                }
                else
                    lblAlumno.Text = "No se han escaneado documentos de ese alumno. O no es de la sede seleccionada";

            }

        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Limpiar();
        }

        protected void btNuevo_Click(object sender, EventArgs e)
        {
            Limpiar();
        }
    }
}