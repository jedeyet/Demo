using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Antlr.Runtime.Tree;

namespace Visor_de_Documentos
{
    public partial class PagsAlumnosHistorial : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                txCarnet.Focus();  CargaSedes(); CargaDatos();
            }
        }
        string usuario = ""; int Nivel = 0;
        private void CargaDatos()
        {
           Nivelin();
        }
        private void Nivelin()
        {
            int opcion = Convert.ToInt32(DropDownList1.SelectedValue.ToString());
            string usu = System.Web.HttpContext.Current.Session["usuario"].ToString().Trim();
            string cad = "select * from AdminsSedesPermisos where usuario='" + usu + "' and idsede=" + opcion.ToString();
            //Label5.Text = cad;
            DataTable dt = new DataTable();
            dt = Models.Conex.Consulta(cad);
            lbNivel.Text = dt.Rows[0]["Nivel"].ToString();
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
        
        bool Carreras(int opcion)
        {
             
            string usu = System.Web.HttpContext.Current.Session["usuario"].ToString().Trim();
            string sedes = "Select * from VistaAdminSedes WHERE idSede = " + DropDownList1.SelectedValue.ToString() + " AND Usuario = '" + usu + "'";
            //DataTable dtsedes = Models.Conex.Consulta2(sedes, opcion);
            DataTable dtsedes = Models.Conex.Consulta2(sedes, 1);
            bool encontrado = false;
            int codi = Convert.ToInt16(txCarnet.Text.Substring(4, 2));
            for (int i = 0; i < dtsedes.Rows.Count; i++)
            {
                if (Convert.ToInt16(dtsedes.Rows[i][4].ToString()) == codi)
                {
                    encontrado = true;
                }
            }
            return encontrado;
        }
        
     
        
        private void Carga()
        {

            if (txCarnet.Text.Length > 0 && (txCarnet.Text.Length==9 || txCarnet.Text.Length == 10))
            {
                bool nopermitido = false;
                for (int c = 1; c < txCarnet.Text.Length; c++)
                {
                    string caracter = txCarnet.Text[c].ToString();
                    if (caracter == "=" || caracter == "'" || caracter == "-")
                        nopermitido = true;
                }
                if (nopermitido)
                { txCarnet.Text = ""; }
                else
                {
                    int opcion = Convert.ToInt32(DropDownList1.SelectedValue.ToString());
                    Nivelin();
                    bool encontrado = Carreras(opcion);
                    if (Convert.ToInt16(lbNivel.Text) == 0) encontrado = true;

                    if (encontrado == false)
                    {
                        mensaje("No tiene autorización a ver esta información", "");
                            }
                    else
                    {
                        DataTable dt = new DataTable();
                        string cadena = "";
                        cadena = "select asignatura, zona as Zona, [examen final] as Final, [nota final] as [Nota Final], semestre_cursado as Ciclo, seccion as Sección, " +
                        "convert(nvarchar,[Fecha aprobación], 103) as Aprobado from vista_notas where [número de carné] = '" + txCarnet.Text + "' order by semestre_cursado,asignatura";
                        dt = Models.Conex.Consulta2(cadena, opcion);
                        if (dt.Rows.Count == 0) { 
                            lbResultado.Text = "No se localizan cursos con este carnet"; lbnombre.Text = ""; Visualiza(false); 
                            GridCursos.Visible = false; GridCursos1.Visible = false; GridCursos2.Visible = false;
                            string mens = "No se localizan cursos con este carnet";
                            mensaje(mens, "PagsAlumnosHistorial");

                        }
                        else
                        {
                            lbResultado.Text = "Números de cursos localizados: " + dt.Rows.Count.ToString();
                            GridCursos1.Visible = false; GridCursos1.DataSource = ""; GridCursos1.DataBind();
                            GridCursos2.Visible = false; GridCursos2.DataSource = ""; GridCursos2.DataBind();
                            GridCursos.DataSource = dt;
                            GridCursos.DataBind();

                            DataTable dtnombre;
                            string nom = "select  [número de carné], Nombre_completo + ' ' + [nombre de la carrera] As nombre_completo from VistaAlumnosCarrera where [número de carné]='" +
                                txCarnet.Text + "'";
                            dtnombre = Models.Conex.Consulta2(nom, opcion);
                            lbnombre.Text = dtnombre.Rows[0]["nombre_completo"].ToString();
                            Visualiza(true); GridCursos.Visible = true;
                        }
                    }
                    
                }
            }
            else
            {
                lbResultado.Text = "Carnet no aceptado, rectifique por favor";
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            GridCursos1.Visible = false; GridCursos1.DataSource = ""; GridCursos1.DataBind(); GridCursos2.Visible = false;
            Carga();
        }

        private void Visualiza(bool opcion)
        {
            BtTodos.Visible = opcion; BtActualizacion.Visible = opcion; BtEquivalencias.Visible = opcion; BtGanados.Visible = opcion;
            BtPerdidos.Visible = opcion; BtPrivados.Visible = opcion; BtSuficiencias.Visible = opcion; BtConvenio.Visible = opcion; 
            BtDirigido.Visible = opcion; BtGraduandos.Visible = opcion; BtCierre.Visible = opcion; BtActuales.Visible = opcion;
            GridCursos2.Visible = opcion;
        }
        private void buscaDatos()
        {
            GridCursos1.Visible = false; GridCursos1.DataSource = ""; GridCursos1.DataBind(); GridCursos2.Visible = false;
            if (txNombre.Text.Length > 0)
            {
                bool nopermitido = false;
                for (int c = 1; c < txNombre.Text.Length; c++)
                {
                    string caracter = txNombre.Text[c].ToString();
                    if (caracter == "=" || caracter == "'" || caracter == "-")
                        nopermitido = true;
                }
                if (nopermitido)
                { txNombre.Text = ""; }
                else
                {
                    int opcion = Convert.ToInt32(DropDownList1.SelectedValue.ToString());
                    
                        DataTable dt = new DataTable();
                        string cadena = "select  [número de carné], Nombre_completo + ' ' + [nombre de la carrera] As nombre_completo from VistaAlumnosCarrera where nombre_completo like '%" + txNombre.Text + "%' order by nombre_completo";
                        dt = Models.Conex.Consulta2(cadena, opcion);
                    if (dt.Rows.Count == 0)
                    {
                        string mens = "No se localizan coincidencias";
                        mensaje(mens, "PagsAlumnosHistorial");
                        Response.Redirect("PagsAlumnosHistorial.aspx");
                    }
                    else
                    {
                        lbResultado.Text = "Números de coincidencias: " + dt.Rows.Count.ToString();
                        ddNombre.DataValueField = "número de carné";
                        ddNombre.DataTextField = "nombre_completo";
                        ddNombre.DataSource = dt;
                        ddNombre.DataBind();
                        //lbnombre.Text = dt.Rows[0]["nombre_completo"].ToString();
                    }
                }
            }
        }
        
        private void mensaje (string mensa, string pagina)
        {
            string script = $"alert('{mensa}');";
            ClientScript.RegisterStartupScript(this.GetType(), "Alerta", script, true);
          
        }
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Nivelin();
            
            Visualiza(false); lbResultado.Text = ""; lbnombre.Text = ""; GridCursos.Visible = false;
            GridCursos1.Visible = false; lbResultado2.Text = ""; GridCursos2.Visible = false;
        }
        protected void btCoincidencias_Click(object sender, EventArgs e)
        {
            buscaDatos();
        }

        protected void ddNombre_SelectedIndexChanged(object sender, EventArgs e)
        {
            Visualiza(false); lbResultado.Text = ""; lbnombre.Text = ""; GridCursos.Visible = false; 
            GridCursos1.Visible = false; lbResultado2.Text = ""; GridCursos2.Visible = false;
            txCarnet.Text = ddNombre.SelectedValue.ToString();
        }

        private void limpiacua()
        {
            GridCursos1.Visible = false; GridCursos1.DataSource = ""; GridCursos1.DataBind(); lbResultado2.Text = ""; 
            GridCursos2.DataSource = ""; GridCursos2.DataBind();
        }
        private void CargaCuadricula(int opcion)
        {
            limpiacua();
            string promediocad = "";
            int opc = Convert.ToInt32(DropDownList1.SelectedValue.ToString());
            string suficiencia = " and (seccion='Y' or seccion='Z') ";
            if (opc > 1) suficiencia = " and (seccion='C' or seccion='S') ";
            string dirigido = " and (seccion='W' or seccion='X') ";
            if (opc > 1) dirigido = " and (seccion='D' or seccion='E') ";
            string campos = "select asignatura as Asignatura, zona as Zona, [examen final] as Final, [nota final] as [Nota Final], semestre_cursado as Ciclo, seccion as Sección, " +
            "convert(nvarchar,[Fecha aprobación], 103) as Aprobado";

            if (opcion==2) campos = "select ROW_NUMBER() OVER(ORDER BY ano,semestre_cursado,asignatura) AS No, asignatura as Asignatura, zona as Zona, [examen final] as Final, [nota final] as " + 
                    "[Nota Final], [nombre y apellidos del catedrático] as Catedrático, semestre_cursado as Semestre, Ano as Año";
            
            string cadena = "";

            if (opcion == 1) cadena = campos + " from vista_notas where[número de carné] = '" + txCarnet.Text + "' and gano = 1 order by semestre_cursado,asignatura,ano";
            if (opcion == 2) cadena = campos + " from vista_notas where[número de carné] = '" + txCarnet.Text + "' and gano = 0 and finales = 'SI' order by semestre_cursado,asignatura,ano";
            if (opcion == 3) cadena = campos + " from vista_notas where[número de carné] = '" + txCarnet.Text + "' and [Nombre y apellidos del catedrático] like '%Equivalen%' order by semestre_cursado,asignatura,ano";
            if (opcion == 4) cadena = campos + " from vista_notas where[número de carné] = '" + txCarnet.Text + "' " + suficiencia + " order by semestre_cursado,asignatura,ano";
            if (opcion == 5) cadena = campos + " from vista_notas where[número de carné] = '" + txCarnet.Text + "' " + dirigido + "order by semestre_cursado,asignatura,ano";
            if (opcion == 6) cadena = campos + " from vista_notas where[número de carné] = '" + txCarnet.Text + "' and tiponota like 'actual%' order by semestre_cursado,asignatura,ano";
            string sem = "1";  if (DateTime.Now.Month >= 7) sem = "2";
            if (opcion == 7) cadena = campos + " from vista_notas where[número de carné] = '" + txCarnet.Text + "' and ano = " + DateTime.Now.Year.ToString() + " and semestre=" + sem + " order by semestre_cursado,asignatura,ano";
            
            DataTable dt = new DataTable(); dt.Clear();
            dt = Models.Conex.Consulta2(cadena, opc);
            if (dt.Rows.Count == 0)
            { lbResultado.Text = "No se localizan coincidencias"; GridCursos.Visible = false; }
            else
            {
                if (opcion == 7)
                {

                    var filasConFinal = dt.AsEnumerable()
                        .Where(row =>
                            !row.IsNull("Final") &&
                            !string.IsNullOrWhiteSpace(row["Final"].ToString()) &&
                            row["Final"].ToString().Trim() != "0" &&
                            row["Final"].ToString().Trim() != "-" &&
                            row["Final"].ToString().Trim() != " "
                        )
                        .ToList();

                    if (filasConFinal.Any())
                    {
                        var notasValidas = filasConFinal
                            .Where(row => !row.IsNull("Nota Final"))
                            .Select(row => Convert.ToDecimal(row["Nota Final"]));

                        if (notasValidas.Any())
                        {
                            decimal promedio = Math.Round(notasValidas.Average(), 2);

                            promediocad = " Promedio " + promedio.ToString() + " de " + notasValidas.Count().ToString() + " cursos con final subido";
                        }
                        else
                        {
                            promediocad = " No hay notas finales válidas.";
                        }
                    }
                    else
                    {
                        promediocad = " No hay finales registrados aún.";
                    }
                }
                lbResultado.Text = "Números de asignaturas: " + dt.Rows.Count.ToString() + " | " + promediocad;
                GridCursos.DataSource = dt;
                GridCursos.DataBind();
                GridCursos.Visible = true;
            }
            GridCursos1.Visible = false; lbResultado2.Text = ""; GridCursos2.Visible = false;

      


        }

        private void CargaPrivados()
        {
            //Privados teóricos o escritos
            limpiacua();
            int opc = Convert.ToInt32(DropDownList1.SelectedValue.ToString());
            string cadena = " select Fase, Convocatoria, Año, Round(SUM(Punteo),0) AS total FROM Vista_Privados_Punteo WHERE  "
                + "Carnet = '" + txCarnet.Text + "' and fase like 'Teórica%' and publicable = 1 GROUP BY Fase, Convocatoria, Año";
            DataTable dt = new DataTable(); dt.Clear();
            dt = Models.Conex.Consulta2(cadena, opc);
            if (dt.Rows.Count == 0)
            { lbResultado.Text = "No se localizan privados escritos"; GridCursos.Visible = false; }
            else
            {
                lbResultado.Text = "Números de privados escritos localizados: " + dt.Rows.Count.ToString();
                GridCursos.DataSource = dt;
                GridCursos.DataBind();
                GridCursos.Visible = true;
            }
            //Privados Orales
            string cadena1 = " select Fase, Convocatoria, Año, round(avg(Punteo),0) AS total FROM Vista_Privados_Punteo WHERE  Carnet = '"
            + txCarnet.Text + "' and fase like 'Oral%' and publicable = 1 GROUP BY Fase, Convocatoria, Año";
            DataTable dt1 = new DataTable(); dt1.Clear();
            dt1 = Models.Conex.Consulta2(cadena1, opc);
            if (dt1.Rows.Count == 0)
            { lbResultado2.Text = "No se localizan privados orales"; GridCursos2.Visible = false; }
            else
            {
                lbResultado2.Text = "Números de privados orales localizados: " + dt1.Rows.Count.ToString();
                GridCursos2.DataSource = dt1;
                GridCursos2.DataBind();
                GridCursos2.Visible = true;
            }
            if (lbResultado.Text == "No se localizan privados escritos" && lbResultado2.Text == "No se localizan privados orales")
            {
                lbResultado.Text = "No se localizan privados";
                lbResultado2.Text = "";
            }

        }


        protected void BtTodos_Click(object sender, EventArgs e)
        {
            
            Carga();
        }

        protected void BtGanados_Click(object sender, EventArgs e)
        {
            CargaCuadricula(1);
        }

        protected void BtPerdidos_Click(object sender, EventArgs e)
        {
            CargaCuadricula(2);
        }

        protected void BtEquivalencias_Click(object sender, EventArgs e)
        {
            CargaCuadricula(3);
        }

        protected void BtSuficiencias_Click(object sender, EventArgs e)
        {
            CargaCuadricula(4);
        }

        protected void BtDirigido_Click(object sender, EventArgs e)
        {
            CargaCuadricula(5);
        }

        protected void BtActualizacion_Click(object sender, EventArgs e)
        {
            CargaCuadricula(6);
        }

        protected void BtPrivados_Click(object sender, EventArgs e)
        {
            CargaPrivados();
        }

        protected void BtConvenio_Click(object sender, EventArgs e)
        {
            CargaConvenios();
        }

        private void CargaConvenios()
        {

            limpiacua();
            int opc = Convert.ToInt32(DropDownList1.SelectedValue.ToString());
            string cadena = "select Carnet,iif(vigente=0,'Cancelado','Vigente') as Vigencia,  " +
                "anio as Año, Semestre, convert(nvarchar, vencimiento, 103) as Vencimiento, " +
                "observaciones as Observaciones from Vista_Bloqueo where carnet = '" + txCarnet.Text +
                "' and tipo_bloqueo = 'Convenio de pagos pendiente' order by carnet,anio,semestre";
            DataTable dt = new DataTable(); dt.Clear();
            dt = Models.Conex.Consulta2(cadena, opc);
            if (dt.Rows.Count == 0)
            { lbResultado.Text = "No se localizan convenios de pago"; GridCursos.Visible = false; }
            else
            {
                lbResultado.Text = "Números de covenios localizados: " + dt.Rows.Count.ToString();
                GridCursos.DataSource = dt;
                GridCursos.DataBind();
                GridCursos.Visible = true;
            }
        }

        private void CargaGraduandos()
        {
            GridCursos1.Visible = false; GridCursos1.DataSource = ""; GridCursos1.DataBind(); GridCursos2.Visible = false;
            int opc = 1;
            string nom = "Select * from alumno where [número de carné]='" + txCarnet.Text + "'";
            DataTable dtnom = new DataTable();
            dtnom = Models.Conex.Consulta2(nom, opc);
            string noms = dtnom.Rows[0]["Nombre del alumno"].ToString().Trim() + " " + dtnom.Rows[0]["segundo nombre"].ToString().Trim();
            string apes = dtnom.Rows[0]["Primer apellido"].ToString().Trim() + " " + dtnom.Rows[0]["Segundo apellido"].ToString().Trim();

            string cadena = "select anio as Año, Registro, titulo as Título, Sede, Fecha, Acta, resolucion as Resolución, " + 
                "distincion as Distinción FROM Graduandos " +
                "WHERE (nombres = '" + noms + "' AND apellidos = '" + apes + "')";
            
            DataTable dt = new DataTable();
            dt = Models.Conex.Consulta2(cadena, opc);
            if (dt.Rows.Count == 0)
            { lbResultado.Text = "No se localizan registros de graduación"; GridCursos.Visible = false; }
            else
            {
                lbResultado.Text = "Registros de graduación localizados: " + dt.Rows.Count.ToString();
                GridCursos.DataSource = dt;
                GridCursos.DataBind();
                GridCursos.Visible = true;
            }
            GridCursos1.Visible = false; lbResultado2.Text = ""; GridCursos2.Visible = false;
        }

        protected void BtGraduandos_Click(object sender, EventArgs e)
        {
            CargaGraduandos();
        }

        protected void BtCierre_Click(object sender, EventArgs e)
        {
            int opc = Convert.ToInt32(DropDownList1.SelectedValue.ToString());
            string pensum = " select * from alumno where [número de carné]='" + txCarnet.Text + "'";
             DataTable dtpensum = Models.Conex.Consulta2(pensum, opc);
            string pen = dtpensum.Rows[0]["pensum_asignado"].ToString();
            int conteo = 0;
            string cadena = "select No_curso, Asignatura,semestre as Semestre, creditos as Créditos from Pensum_Asignaturas  as t1 where ID_Encabezado_Pensum = '" + pen + "' order by no_curso";
            DataTable dt = new DataTable();
            dt = Models.Conex.Consulta2(cadena, opc);
            decimal suma = 0; decimal ganados = 0;
            if (dt.Rows.Count == 0)
            { lbResultado.Text = "No se localizan coincidencias"; GridCursos.Visible = false; }
            else
            {
                GridCursos.Visible = false;
                lbResultado.Text = "Cierre de cursos: ";
                
                GridCursos1.DataSource = dt;
                GridCursos1.DataBind();
                GridCursos1.Visible = true;

                //llena un datatable con todas las notas del estudiante
                string cad = "SELECT     Nombre_Completo, [Nombre de la carrera], facultad, Departamento, " +
                "semestre_Cursado, Asignatura, MAX([Nota final]) AS [Nota final], " +
                "MAX([Fecha aprobación]) AS [fecha aprobación], [Créditos],tiponota  FROM Vista_Notas_Certificaciones " +
                "WHERE [Número de carné] = '" + txCarnet.Text + "' AND gano = 1  and semestre_cursado >0  " +
                "GROUP BY Asignatura, semestre_Cursado, Nombre_Completo, [Número de carné], TipoNota, " +
                "[Nombre de la carrera], facultad, Departamento, [Créditos],derecho_examen ORDER BY semestre_Cursado";


                DataTable dx = new DataTable();
                dx = Models.Conex.Consulta2(cad, opc);


                //for (int i = 0; i < GridCursos.Rows.Count; i++)
                //{
                //    GridViewRow fila = GridCursos.Rows[i];
                //    var punteo = fila.FindControl("label26") as Label; punteo.Text = "";
                //    var fecha = fila.FindControl("label27") as Label; fecha.Text = "";
                //    var tiponota = fila.FindControl("label28") as Label; tiponota.Text = "";
                //}
                
                for (int i = 0; i < GridCursos1.Rows.Count; i++)
                {
                    GridViewRow fila = GridCursos1.Rows[i];

                    Label dato = fila.FindControl("label23") as Label;
                    Label punteo = fila.FindControl("label26") as Label;
                    Label fecha = fila.FindControl("label27") as Label;
                    Label tiponota = fila.FindControl("label28") as Label;
                    bool enc = false;
                    for (int j = 0; j < dx.Rows.Count; j++)
                    {
                        string comparado = dx.Rows[j][5].ToString().Trim();
                        string asignatura = dato.Text.ToString().Trim();
                        if ( asignatura == comparado)
                        {
                            punteo.Text = dx.Rows[j][6].ToString();
                            fecha.Text = dx.Rows[j][7].ToString();
                            tiponota.Text = "";
                            tiponota.Text = dx.Rows[j][9].ToString();
                            enc = true;
                            decimal valor;

                            if (decimal.TryParse(punteo.Text, out valor) && valor != 0)
                            {
                                suma += valor; ganados++;
                            }

                        }
                    }

                    if (enc == false)
                    {
                        punteo.Text = "NO"; fecha.Text = "NO"; tiponota.Text = "NO";
                        punteo.ForeColor = System.Drawing.Color.Red; fecha.ForeColor = System.Drawing.Color.Red; tiponota.ForeColor = System.Drawing.Color.Red;
                        conteo++;
                    }
                }

            }
            decimal promedio = suma / ganados;
            string prom = " | Promedio cursos aprobados: " + promedio.ToString("F2");
            if (conteo > 0)
            {
                string dd = "Falta un curso";
                if (conteo > 2) dd = "Faltan " + conteo.ToString() + " cursos";
                lbResultado2.Text = dd + " para cierre de cursos" ;
                
            }
            else
            {
                lbResultado.Text = "CIERRE DE CURSOS COMPLETO " ;
                GridCursos.Visible = false; GridCursos2.Visible = false;
            }
            lbResultado.Text += prom;
        }

        protected void BtActuales_Click(object sender, EventArgs e)
        {
            CargaCuadricula(7);
        }

        protected void btNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("PagsAlumnosHistorial.aspx");
        }
    }
}