using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net;

namespace Visor_de_Documentos
{
    public partial class PagsDocenteIndividual : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargaSedes();
                CargaDatos();
            }
        }
        string usuario = ""; int Nivel = 0;
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
            if (TextBox1.Text.Length > 0)
            {
                bool nopermitido = false;
                for (int c = 1; c < TextBox1.Text.Length; c++)
                {
                    string caracter = TextBox1.Text[c].ToString();
                    if (caracter == "=" || caracter == "'" || caracter == "-")
                        nopermitido = true;
                }
                if (nopermitido)
                { TextBox1.Text = ""; }
                else
                {
                    string cadena = "Select * from catedraticos where [nombre y apellidos del catedrático] COLLATE Latin1_General_CI_AI LIKE '%" +
                        TextBox1.Text + "%' order by [nombre y apellidos del catedrático]";
                    int opcion = Convert.ToInt32(DropDownList1.SelectedValue.ToString());
                    DataTable dt = Models.Conex.Consulta2(cadena, opcion);
                    if (dt.Rows.Count == 0)
                    { mensaje("No se localizan docentes con estas credenciales");
                        Response.Redirect("PagsDocenteIndividual.aspx");
                    }
                    else
                    {
                        ListBox1.Items.Clear();
                        ListBox1.Items.Add("Seleccione un catedrático");
                        lbResultado.Text = "Números de docentes localizados: " + dt.Rows.Count.ToString();
                        ListBox1.DataValueField = "codigo_catedratico";
                        ListBox1.DataTextField = "nombre y apellidos del catedrático";
                        ListBox1.DataSource = dt;
                        ListBox1.DataBind();

                    }
                }
            }
            this.TextBox2.Text = "";
            LimpiarEtiquetas();
            GridView4.DataSource = null; GridView4.DataBind();
            GridView5.DataSource = null; GridView5.DataBind();
            GridView6.DataSource = null; GridView6.DataBind();
            GridView7.DataSource = null; GridView7.DataBind();
            GridView8.DataSource = null; GridView8.DataBind();
            Image1.ImageUrl = "";
        }

        private void mensaje(string mensa )
        {
            string script = $"alert('{mensa}');";
            ClientScript.RegisterStartupScript(this.GetType(), "Alerta", script, true);
            
        }
        protected void Button2_Click(object sender, EventArgs e)
        {

            try
            {
                string cadena = "Select * from catedraticos where codigo_catedratico=" + TextBox2.Text;
                int opcion = Convert.ToInt32(DropDownList1.SelectedValue.ToString());
                System.Web.HttpContext.Current.Session["opcion"] = opcion;

                DataTable dt = Models.Conex.Consulta2(cadena, opcion);
                if (dt.Rows.Count == 0)
                {
                    mensaje("No se localizan docentes con estas credenciales");
                    Response.Redirect("PagsDocenteIndividual.aspx");
                }
                else
                {
                    string dato = "'" + dt.Rows[0]["nombre y apellidos del catedrático"] + "'";
                    System.Web.HttpContext.Current.Session["dato"] = dato;
                    System.Web.HttpContext.Current.Session["nombre"] = dt.Rows[0]["nombre y apellidos del catedrático"];
                    LimpiarEtiquetas();
                    CargaCuadriculas(1, dato);
                    CargaCuadriculas(2, dato);
                    CargaCuadriculas(3, dato);
                    CargaCuadriculas(4, dato);
                    CargaCuadriculas(5, dato);
                    CargaCuadriculas(6, dato);
                    CargaCuadriculas(7, dato);
                    CargaCuadriculas(8, dato);
                    cargaFoto();
                    ImgBtnPrint.Visible = true;
                }
                this.TextBox1.Text = "";
                this.ListBox1.Items.Clear();
            }
            catch 
            {
                Button2.Focus();
            }
        }

        protected void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string dato = "'" + ListBox1.SelectedItem.ToString() + "'";
            int opcion = Convert.ToInt32(DropDownList1.SelectedValue.ToString());
            System.Web.HttpContext.Current.Session["opcion"] = opcion;
            System.Web.HttpContext.Current.Session["dato"] = dato;
            System.Web.HttpContext.Current.Session["nombre"] = ListBox1.SelectedItem.ToString();
            LimpiarEtiquetas();
            CargaCuadriculas(1,dato);
            CargaCuadriculas(2,dato);
            CargaCuadriculas(3,dato);
            CargaCuadriculas(4,dato);
            CargaCuadriculas(5,dato);
            CargaCuadriculas(6,dato);
            CargaCuadriculas(7,dato);
            CargaCuadriculas(8,dato);
            cargaFoto();
            
        }

        private void CargaCuadriculas(int dato, string datin)
        {
            
            string cadena = "";
            switch (dato)
            {
                case 1:
                     cadena = "Select * from VistaDocentesEmpresa where [nombre y apellidos del catedrático] = " + datin;
                    break;
                case 2:
                    cadena = "Select * from VistaDocentesSalud where [nombre y apellidos del catedrático] = " + datin;
                    
                    break;
                case 3:
                    cadena = "Select * from VistaDocentesEnfermedades where [nombre y apellidos del catedrático] = " + datin;
                    
                    break;
                case 4:
                    cadena = "Select [nombre y apellidos del catedrático],Colegio, NoColegiado, convert(nvarchar,vencimientocolegiado,103) as VencimientoColegiado from VistaDocentesColegiado where [nombre y apellidos del catedrático] = " + datin;
                    
                    break;
                case 5:
                    cadena = "Select * from VistaDocentesTitulos where [nombre y apellidos del catedrático] =" + datin;
                    
                    break;
                case 6:
                    cadena = "Select * from VistaDocentesIdiomas where docente = " + datin;
                    
                    break;
                case 7:
                    cadena = "select codigo_catedratico,cedula, celular,telefono, email,pais,E_Civil, convert(nvarchar, fecha_nac, 103) as Nacimiento,nit, " +
                               "convert(nvarchar, FechaIngreso, 103) as Ingreso, " +
                               " (select CoumnLingDescripcion from IneComunidadLing where ComunLingId= idcomunidad) as comunidad, " +
                               " isNull(cuentaindustrial,'Sin Dato') as cuentaindustrial, ISNULL(Residencia, 'Sin Dato') as Residencia from catedraticos " +
                               "where [nombre y apellidos del catedrático] = " + datin;
                    break;
                case 8:
                    //cadena = "select asignatura,seccion,semestre,[Nombre de la carrera] from Vista_Asignacion_Curso_Profesor " +
                    //           " where ano = " + ddAnio.Text + " and SemestreCursado = " + 
                    //           ddSemestre.Text + " and [nombre y apellidos del catedrático] = " + datin;
                    cadena = "select asignatura,  seccion, carrera, semestre, count(asignatura) as Períodos from TablaConComunes(" + datin 
                        + ") group by asignatura,seccion, carrera, semestre order by carrera, seccion, semestre";
                    System.Web.HttpContext.Current.Session["anio"] = ddAnio.Text;
                    System.Web.HttpContext.Current.Session["semestre"] = ddSemestre.Text;
                    break;
                default:
                    break;
            }
            int opcion = Convert.ToInt32(DropDownList1.SelectedValue.ToString());

            DataTable dt = Models.Conex.Consulta2(cadena, opcion);

            switch (dato)
            {
                case 1:
                    GridView1.DataSource = dt; GridView1.DataBind(); datosLaborales(dt); break;
                case 2:
                    GridView2.DataSource = dt; GridView2.DataBind(); datosMedicos(dt); break;
                case 3:
                    GridView3.DataSource = dt; GridView3.DataBind(); datosSalud(dt); break;
                case 4:
                    if (dt.Rows.Count > 0)
                    {
                        LabelColCol.Text = dt.Rows[0]["Colegio"].ToString();
                        LabelColNum.Text = dt.Rows[0]["NoColegiado"].ToString();
                        LabelColVig.Text = dt.Rows[0]["VencimientoColegiado"].ToString();
                    }
                    else
                    {
                        LabelColCol.Text = ""; LabelColNum.Text = ""; LabelColVig.Text = "";
                    }
                    /*GridView4.DataSource = dt; GridView4.DataBind();*/
                    break;
                case 5:
                    foreach (DataRow fila in dt.Rows)
                    {
                        if ((int)fila["IdNivel"] == 1 || (int)fila["IdNivel"] == 2) LabelTitUni.Text += " " + fila["Descripcion"].ToString();
                        else if ((int)fila["IdNivel"] == 3) LabelTitMae.Text += " " + fila["Descripcion"].ToString();
                        else if ((int)fila["IdNivel"] == 4) LabelTitDoc.Text += " " + fila["Descripcion"].ToString();
                        else if ((int)fila["IdNivel"] == 5) LabelTitDip.Text += " " + fila["Descripcion"].ToString();
                    }
                    /*GridView5.DataSource = dt; GridView5.DataBind();*/
                    break;
                case 6:
                    GridView6.DataSource = dt; GridView6.DataBind(); break;
                case 7:
                    GridView7.DataSource = dt; Label12.Text = dt.Rows[0]["codigo_catedratico"].ToString();  GridView7.DataBind(); datosPersonales(dt); break;
                case 8:
                    GridView8.DataSource = dt; GridView8.DataBind(); break;
                default:
                    break;
            }
            
        }

        private void datosPersonales(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["Residencia"] != null)
                    LabelDomicilio.Text = "" + dt.Rows[0]["Residencia"];
                if (dt.Rows[0]["cedula"] != null)
                    LabelDpi.Text = "DPI: " + dt.Rows[0]["cedula"];
                if (dt.Rows[0]["Nit"] != null)
                    LabelNit.Text = "NIT: " + dt.Rows[0]["Nit"];
                if (dt.Rows[0]["celular"] != null)
                    LabelMovil.Text = dt.Rows[0]["celular"].ToString();//LabelMovil.Text = "Móvil: " + dt.Rows[0]["celular"];
                if (dt.Rows[0]["Telefono"] != null)
                    LabelTelefono.Text = dt.Rows[0]["Telefono"].ToString();//LabelTelefono.Text = "Teléfono: " + dt.Rows[0]["Telefono"];
                if (dt.Rows[0]["email"] != null)
                    LabelEmail.Text = dt.Rows[0]["email"].ToString();//LabelEmail.Text = "Email: " + dt.Rows[0]["email"];
                if (dt.Rows[0]["pais"] != null)
                { LabelPais.Text = dt.Rows[0]["pais"].ToString(); LabelLugarNac.Text = dt.Rows[0]["pais"].ToString(); }//LabelPais.Text = "Pais: " + dt.Rows[0]["pais"];
                if (dt.Rows[0]["E_civil"] != null)
                    LabelEstadoCivil.Text = dt.Rows[0]["E_civil"].ToString();//LabelEstadoCivil.Text = "Estado Civil: " + dt.Rows[0]["E_civil"];
                if (dt.Rows[0]["Nacimiento"] != null)
                    LabelFechaNacimiento.Text = dt.Rows[0]["Nacimiento"].ToString();//LabelFechaNacimiento.Text = "Fecha Nacimiento: " + dt.Rows[0]["Nacimiento"];
                if (dt.Rows[0]["Ingreso"] != null)
                    LabelFechaIngreso.Text = "" + dt.Rows[0]["Ingreso"];//LabelFechaIngreso.Text = "Fecha Ingreso: " + dt.Rows[0]["Ingreso"];
                if (dt.Rows[0]["Comunidad"] != null)
                    LabelComunidad.Text = "Comunidad: " + dt.Rows[0]["Comunidad"];
                if (dt.Rows[0]["CuentaIndustrial"] != null)
                    LabelCuentaIndustrial.Text = "Cuenta BI: " + dt.Rows[0]["CuentaIndustrial"];
            }
            /*if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["cedula"] != null)
                    LabelDpi.Text = "DPI: " + dt.Rows[0]["cedula"];
                if (dt.Rows[0]["Nit"] != null)
                    LabelNit.Text = "NIT: " + dt.Rows[0]["Nit"];
                if (dt.Rows[0]["celular"] != null)
                    LabelMovil.Text = "Móvil: " + dt.Rows[0]["celular"];
                if (dt.Rows[0]["Telefono"] != null)
                    LabelTelefono.Text = "Teléfono: " + dt.Rows[0]["Telefono"];
                if (dt.Rows[0]["email"] != null)
                    LabelEmail.Text = "Email: " + dt.Rows[0]["email"];
                if (dt.Rows[0]["pais"] != null)
                    LabelPais.Text = "Pais: " + dt.Rows[0]["pais"];
                if (dt.Rows[0]["E_civil"] != null)
                    LabelEstadoCivil.Text = "Estado Civil: " + dt.Rows[0]["E_civil"];
                if (dt.Rows[0]["Nacimiento"] != null)
                    LabelFechaNacimiento.Text = "Fecha Nacimiento: " + dt.Rows[0]["Nacimiento"];
                if (dt.Rows[0]["Ingreso"] != null)
                    LabelFechaIngreso.Text = "Fecha Ingreso: " + dt.Rows[0]["Ingreso"];
                if (dt.Rows[0]["Comunidad"] != null)
                    LabelComunidad.Text = "Comunidad: " + dt.Rows[0]["Comunidad"];
                if (dt.Rows[0]["CuentaIndustrial"] != null)
                    LabelCuentaIndustrial.Text = "Cuenta BI: " + dt.Rows[0]["CuentaIndustrial"];
            }*/
        }

        private void datosMedicos(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["telefonoemergencia"] != null)
                    LabelEmergencia.Text = "Teléfono de Emergencia: " + dt.Rows[0]["telefonoemergencia"];
                if (dt.Rows[0]["contactoemergencia"] != null)
                    LabelContacto.Text = "Contacto: " + dt.Rows[0]["contactoemergencia"];
                if (dt.Rows[0]["relacioncontacto"] != null)
                    LabelRelacion.Text = "Relación: " + dt.Rows[0]["relacioncontacto"];
                if (dt.Rows[0]["Medico"] != null)
                    LabelMedico.Text = "Médico: " + dt.Rows[0]["Medico"];
                if (dt.Rows[0]["TelefonoMedico"] != null)
                    LabelTelefonoMedico.Text = "Teléfono Médico: " + dt.Rows[0]["TelefonoMedico"];
                if (dt.Rows[0]["grupo"] != null)
                    LabelGrupoSanguineo.Text = "Grupo Sanguíneo: " + dt.Rows[0]["grupo"];
                if (dt.Rows[0]["NoSeguroMedico"] != null)
                    LabelSeguroMedico.Text = "Seguro Médico: " + dt.Rows[0]["NoSeguroMedico"];
                if (dt.Rows[0]["EmpresaSeguroMedico"] != null)
                    LabelEmpresaSeguro.Text = "Empresa Seguro: " + dt.Rows[0]["EmpresaSeguroMedico"];
                if (dt.Rows[0]["AlergicoMedicamentos"] != null)
                    LabelAlergias.Text = "Alergias: " + dt.Rows[0]["AlergicoMedicamentos"];
            }
            
        }

        private void datosSalud(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["EnfermedadCronica"] != null)
                    LabelEnfermedad.Text = "Enfermedad Crónica: " + dt.Rows[0]["EnfermedadCronica"];
                if (dt.Rows[0]["Tratamiento"] != null)
                    LabelTratamiento.Text = "Tratamiento: " + dt.Rows[0]["Tratamiento"];
            }
        }
        private void datosLaborales(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["Empresa"] != null)
                    LabelEmpresa.Text = " " + dt.Rows[0]["Empresa"];
                if (dt.Rows[0]["Direccion"] != null)
                    LabelDireccion.Text = " " + dt.Rows[0]["Direccion"];
                if (dt.Rows[0]["telefono"] != null)
                    LabelTelefonoLabora.Text = " " + dt.Rows[0]["telefono"];
                if (dt.Rows[0]["email"] != null)
                    LabelEmailLabora.Text = " " + dt.Rows[0]["email"];
                if (dt.Rows[0]["igss"] != null)
                    LabelIGSS.Text = " No: " + dt.Rows[0]["igss"];
            }
        }

        private void LimpiarEtiquetas()
        {
            
            LabelDpi.Text = "DPI: ";
            LabelNit.Text = "NIT: ";
            LabelMovil.Text = "Móvil: ";
            LabelTelefono.Text = "Teléfono: " ;
            LabelEmail.Text = "Email: ";
            LabelPais.Text = "Pais: ";
            LabelEstadoCivil.Text = "Estado Civil: ";
            LabelFechaNacimiento.Text = "Fecha Nacimiento: ";
            LabelFechaIngreso.Text = "Fecha Ingreso: "; 
            LabelComunidad.Text = "Comunidad: ";            
            LabelCuentaIndustrial.Text = "Cuenta BI: ";

            
            LabelEmergencia.Text = "Teléfono de Emergencia: ";
            LabelContacto.Text = "Contacto: ";
            LabelRelacion.Text = "Relación: ";
            LabelMedico.Text = "Médico: ";
            LabelTelefonoMedico.Text = "Teléfono Médico: ";
            LabelGrupoSanguineo.Text = "Grupo Sanguíneo: ";
            LabelSeguroMedico.Text = "Seguro Médico: ";
            LabelEmpresaSeguro.Text = "Empresa Seguro: ";
            LabelAlergias.Text = "Alergias: ";

            
            LabelEnfermedad.Text = "Enfermedad Crónica: ";
            LabelTratamiento.Text = "Tratamiento: ";

            LabelEmpresa.Text = "";//"Empresa: ";
            LabelDireccion.Text = "";// "Dirección: ";
            LabelTelefonoLabora.Text = "";//"Teléfono: ";
            LabelEmailLabora.Text = "";//"Email: ";
            LabelIGSS.Text = "IGSS: ";

            LabelTitMae.Text = "";
            LabelTitDip.Text = "";
            LabelTitDoc.Text = "";
            LabelTitUni.Text = "";

            LabelNombreApe.Text = "";
        }

        private void cargaFoto()
        {
            string cadena = "";
            int opcion = Convert.ToInt32(DropDownList1.SelectedValue.ToString());
            if (opcion == 1)
            {cadena = "https://secretaria.mesoamericana.edu.gt/fcu/" + Label12.Text + ".jpg"; 
                Image1.ImageUrl = cadena;
            }

            if (opcion == 2)
            {
                cadena = "https://academico.umes.edu.gt/fcu/" + Label12.Text + ".jpg";
                Image1.ImageUrl = cadena;
                
            }
            System.Web.HttpContext.Current.Session["codigo"] = Label12.Text;

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
        }

        protected void btNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("PagsDocenteIndividual.aspx");
        }
    }
}