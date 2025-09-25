using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;


namespace Visor_de_Documentos
{
    public partial class PagsDocenteIndividualimpresion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //System.Web.HttpContext.Current.Session["datos"] = "47";

                string dato = System.Web.HttpContext.Current.Session["dato"].ToString();
                CargaDatos(dato);
                
            }
        }
        
        


        private void CargaDatos(string dato)
        {
            CargaCuadriculas(1, dato);
            CargaCuadriculas(2, dato);
            CargaCuadriculas(3, dato);
            CargaCuadriculas(4, dato);
            CargaCuadriculas(5, dato);
            CargaCuadriculas(6, dato);
            CargaCuadriculas(7, dato);
            CargaCuadriculas(8, dato);
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
                               " cuentaindustrial from catedraticos " +
                               "where [nombre y apellidos del catedrático] = " + datin;
                    break;
                case 8:
                    string ano = System.Web.HttpContext.Current.Session["anio"].ToString().Trim();  
                    string semix = System.Web.HttpContext.Current.Session["semestre"].ToString().Trim();
                    cadena = "select asignatura,seccion,semestre,[Nombre de la carrera] from Vista_Asignacion_Curso_Profesor " +
                               " where ano = " + ano + " and SemestreCursado = " +
                               semix + " and [nombre y apellidos del catedrático] = " + datin;
                    break;
                default:
                    break;
            }
            int codigo = Convert.ToInt32(System.Web.HttpContext.Current.Session["codigo"]);
            int opcion = Convert.ToInt32(System.Web.HttpContext.Current.Session["opcion"]);

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
                    GridView4.DataSource = dt; GridView4.DataBind(); break;
                case 5:
                    GridView5.DataSource = dt; GridView5.DataBind(); break;
                case 6:
                    GridView6.DataSource = dt; GridView6.DataBind(); break;
                case 7:
                    GridView7.DataSource = dt; GridView7.DataBind(); datosPersonales(dt); break;
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
            }
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
                    LabelEmpresa.Text = "Empresa: " + dt.Rows[0]["Empresa"];
                if (dt.Rows[0]["Direccion"] != null)
                    LabelDireccion.Text = "Dirección:" + dt.Rows[0]["Direccion"];
                if (dt.Rows[0]["telefono"] != null)
                    LabelTelefonoLabora.Text = "Teléfono: " + dt.Rows[0]["telefono"];
                if (dt.Rows[0]["email"] != null)
                    LabelEmailLabora.Text = "Email: " + dt.Rows[0]["email"];
                if (dt.Rows[0]["igss"] != null)
                    LabelIGSS.Text = "IGSS: " + dt.Rows[0]["igss"];
            }
        }

        private void cargaFoto()
        {
            string cadena = "";
            int codigo = Convert.ToInt32(System.Web.HttpContext.Current.Session["codigo"]);
            int opcion = Convert.ToInt32(System.Web.HttpContext.Current.Session["opcion"]);
            LabelNombre.Text = System.Web.HttpContext.Current.Session["nombre"].ToString();
            if (opcion == 1)
            {
                cadena = "http://secretaria.mesoamericana.edu.gt/fcu/" + codigo.ToString() + ".jpg";
                Image1.ImageUrl = cadena;
            }

            if (opcion == 2)
            {
                cadena = "http://academico.umes.edu.gt/fcu/" + codigo.ToString() + ".jpg";
                Image1.ImageUrl = cadena;

            }

        }


    }
}