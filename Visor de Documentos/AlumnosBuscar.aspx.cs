using System;
using System.Data;
using System.Web.UI;
using Visor_de_Documentos.Models;  

namespace Visor_de_Documentos
{
    public partial class AlumnosBuscar : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null ||
                (Session["usuario"].ToString().Trim().ToLower() != "pjfr" &&
                 Session["usuario"].ToString().Trim().ToLower() != "jdeyet"))
            {
                Response.Redirect("~/NoAutorizado.aspx");
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string dato = txtBusqueda.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(dato))
            {
                litMensaje.Text = Mensaje("Por favor, ingrese un dato para buscar.", "warning");
                gvResultados.DataSource = null;
                gvResultados.DataBind();
                return;
            }

            DataTable resultados = new DataTable();
            resultados.Columns.Add("Sede");
            resultados.Columns.Add("Carnet");
            resultados.Columns.Add("Nombre");
            resultados.Columns.Add("DPI");
            resultados.Columns.Add("Teléfono");
            resultados.Columns.Add("Correo Personal");
            resultados.Columns.Add("Usuario");
            string anio = DateTime.Now.Year.ToString();
            int ciclo = (DateTime.Now.Month >= 7) ? 2 : 1;
            resultados.Columns.Add($"Inscrito {anio}-{ciclo}");

            int[] sedes = new int[] { 1 , 2, 3, 4, 5, 7 }; 
            foreach (int sede in sedes)
            {
                try
                {
                    
                    string consulta = $@"
                        SELECT TOP 10
                            a.[Número de carné] AS Carnet,
                            a.Nombre_Completo,
                            a.[No de Cédula o Pasaporte] AS DPI,
                            a.Teléfono,
                            a.Email,
                            a.Usuario,
                            (
                                SELECT COUNT(*) FROM inscripcion i 
                                WHERE i.Numero_Carnet = a.[Número de carné] 
                                  AND i.ano = '{anio}' AND i.SemestreAnual = '{ciclo}'
                            ) AS Inscrito
                        FROM alumno a
                        WHERE 
                            a.[Número de carné] = '{dato}'
                            OR a.[No de Cédula o Pasaporte] LIKE '%{dato}%'
                            OR a.Nombre_Completo COLLATE Latin1_General_CI_AI LIKE '%{dato}%'";
                    //litMensaje.Text += $"<pre style='font-size:12px;color:#555'>{consulta}</pre>";
                   
                    DataTable temp = Conex.Consulta2(consulta, sede);
                    litMensaje.Text += $"<div>→ Sede {SedeNombre(sede)}: {temp.Rows.Count} filas encontradas</div>";

                    foreach (DataRow fila in temp.Rows)
                    {
                        resultados.Rows.Add(
                            SedeNombre(sede),
                            fila["Carnet"],
                            fila["Nombre_Completo"],
                            fila["DPI"],
                            fila["Teléfono"],
                            fila["Email"],
                            fila["Usuario"],
                            Convert.ToInt32(fila["Inscrito"]) > 0 ? "Sí" : "No"
                        );
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al consultar sede {sede}: {ex.Message}");
                }
            }

            if (resultados.Rows.Count == 0)
            {
                litMensaje.Text = Mensaje("No se encontraron coincidencias en ninguna sede", "danger");
            }
            else
            {
                litMensaje.Text = Mensaje("Resultados encontrados.", "success");
            }

            gvResultados.DataSource = resultados;
            gvResultados.DataBind();
        }

        private string SedeNombre(int cod)
        {
            switch (cod)
            {
                case 1: return "Xela";
                case 2: return "Guate";
                case 3: return "Cobán";
                case 4: return "Teo";
                case 5: return "Izabal";
                case 7: return "Amatitlán";
                default: return "Desconocida";
            }
        }


        private string Mensaje(string texto, string tipo)
        {
            // Bootstrap 3.x compatible
            return $"<div class='alert alert-{tipo} alert-dismissable'>" +
                   $"<button type='button' class='close' data-dismiss='alert' aria-hidden='true'>&times;</button>" +
                   $"{texto}</div>";
        }
    }
}
