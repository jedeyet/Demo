using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net;
using Visor_de_Documentos.Helpers;

namespace Visor_de_Documentos
{
    public partial class AlumnosIndividual : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargaSedes(); CargaDatos();
            }
        }
        string usuario = "";
        private void CargaDatos()
        {
            oculta(false); Nivelin();
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
                    string cadena = "Select [Número de carné],nombre_completo + ' - ' + nomcorto as nombre_completo from Vista_Alumnos_Inscripcion_Usuarios " + 
                        "where nombre_completo COLLATE Latin1_General_CI_AI LIKE '%" +
                        TextBox1.Text + "%' group by [Número de carné],nombre_completo,NomCorto order by nombre_completo";
                    int opcion = Convert.ToInt32(DropDownList1.SelectedValue.ToString());
                    DataTable dt = Models.Conex.Consulta2(cadena, opcion);
                    if (dt.Rows.Count == 0)
                    {
                        mensaje("No se localizan alumnos con estas credenciales", "AlumnosIndividual");
                        lbResultado.Text = "No se localizan alumnos con estas credenciales"; ListBox1.Visible = false; oculta(false); }
                    else
                    {
                        lbResultado.Text = "Números de alumnos localizados: " + dt.Rows.Count.ToString();
                        ListBox1.DataValueField = "número de carné";
                        ListBox1.DataTextField = "nombre_completo";
                        ListBox1.DataSource = dt;
                        ListBox1.DataBind();
                        ListBox1.Visible = true;
                    }
                }
            }

        }
        private void mensaje(string mensa, string pagina)
        {
            string script = $"alert('{mensa}');";
            ClientScript.RegisterStartupScript(this.GetType(), "Alerta", script, true);
            Response.Redirect(pagina + ".aspx");
        }
        protected void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Label12.Text = ListBox1.SelectedValue.ToString();
            cargaFoto(Label12.Text);
            BuscaAlumno(Label12.Text);
        }

        private void cargaFoto(string carnet)
        {
            string cadena = "";
            int opcion = Convert.ToInt32(DropDownList1.SelectedValue.ToString());
            if (opcion == 1)
            {
                cadena = "https://secretaria.mesoamericana.edu.gt/fcu/" + carnet + ".jpg";
                Image1.ImageUrl = cadena;
            }

            if (opcion == 2)
            {
                cadena = "https://academico.umes.edu.gt/fcu/" + carnet + ".jpg";
                Image1.ImageUrl = cadena;

            }

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (TextBox2.Text.Length > 0)
            {
                bool nopermitido = false;
                for (int c = 1; c < TextBox2.Text.Length; c++)
                {
                    string caracter = TextBox2.Text[c].ToString();
                    if (caracter == "=" || caracter == "'" || caracter == "-")
                        nopermitido = true;
                }
                if (nopermitido)
                { TextBox2.Text = ""; }
                else
                {
                    string cadena = "Select * from alumno where [número de carné]='" + TextBox2.Text + "'";
                    int opcion = Convert.ToInt32(DropDownList1.SelectedValue.ToString());
                    DataTable dt = Models.Conex.Consulta2(cadena, opcion);
                    if (dt.Rows.Count == 0)
                    {
                        mensaje("No se localiza ningún estudiante con este carnet", "AlumnosIndividual");
                        oculta(false);
                        lbResultado.Text = "No se localiza ningún estudiante con este carnet";
                    }
                    else
                    {
                        oculta(true);
                       
                        if (lbNivel.Text == "1")
                        {

                        }
                        else
                        {
                            cargaFoto(TextBox2.Text);
                            BuscaAlumno(TextBox2.Text);
                        }
                    }
                }
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
            lbNivel.Text= dt.Rows[0]["Nivel"].ToString();
        }

        private void BuscaAlumno(string carnet)
        {
            string cadena = "SELECT  [Número de carné], [Fecha de Nacimiento], Nacionalidad,  " +
                "[No de Cédula o Pasaporte], [Estado Civil], [Dirección completa], Teléfono, Email, Nombre_Completo, Pensum_Asignado, " +
                "(select CoumnLingDescripcion from inecomunidadling as t2 where t2.ComunLingId=t1.comunlingid) as Comunidad," +
                "(select Municipio from Guate_Muns as t4 where t4.ID_Muns = t1.ciudad) as Municipio," +
                "(select departamento from Guate_Dep as t3 where t3.ID_Depto = t1.Departamento) as Departamento " +
                "FROM alumno  as t1 where [número de carné]='" + carnet + "'";
            
            int opcion = Convert.ToInt32(DropDownList1.SelectedValue.ToString());
            DataTable dt = Models.Conex.Consulta2(cadena, opcion);
            lbCarnet.Text = dt.Rows[0]["Número de carné"].ToString();
            lbNombre.Text = dt.Rows[0]["Nombre_completo"].ToString();
            lbCUI.Text = dt.Rows[0]["No de Cédula o pasaporte"].ToString();
            lbDireccion.Text = dt.Rows[0]["Dirección completa"].ToString();
            lbTel.Text = dt.Rows[0]["teléfono"].ToString();
            lbEmail.Text = dt.Rows[0]["email"].ToString();
            lbPais.Text = dt.Rows[0]["nacionalidad"].ToString();
            lbEst.Text = dt.Rows[0]["estado civil"].ToString();
            DateTime nacimiento = Convert.ToDateTime(dt.Rows[0]["fecha de nacimiento"].ToString());
            lbNac.Text = nacimiento.ToShortDateString();
            lbMunicipio.Text = dt.Rows[0]["Municipio"].ToString();
            lbDepartamento.Text = dt.Rows[0]["departamento"].ToString();
            lbComunidad.Text = dt.Rows[0]["comunidad"].ToString();
            lbPensum.Text = dt.Rows[0]["pensum_asignado"].ToString();


            string emergencia = "Select * from alumno_emergencia where numero_carne='" + carnet + "'";
            DataTable dtemer = new DataTable();
            dtemer = Models.Conex.Consulta2(emergencia, opcion);
            if (dtemer.Rows.Count > 0)
            {
                oculta(true);
                string contacto1 = "";
                if (dtemer.Rows[0]["contacto1"].ToString() == null)
                    contacto1 = "";
                else
                    contacto1 = dtemer.Rows[0]["contacto1"].ToString();

                if (dtemer.Rows[0]["telefono1"].ToString() == null)
                    contacto1 += "";
                else
                    contacto1 += ", Teléfono: " + dtemer.Rows[0]["telefono1"].ToString();
                lbCon1.Text = contacto1;


                string contacto2 = "";
                if (dtemer.Rows[0]["contacto2"].ToString().Length>5  && dtemer.Rows[0]["telefono2"].ToString().Length>5)
                    contacto2 = dtemer.Rows[0]["contacto2"].ToString() + ", Teléfono: " + dtemer.Rows[0]["telefono2"].ToString();
                lbCon2.Text = contacto2;



                string medicos = "";
                if (dtemer.Rows[0]["alergia"].ToString() == null)
                    medicos = "";
                else
                    medicos = dtemer.Rows[0]["alergia"].ToString();
                lbMedicos.Text = medicos;   

                string tratamiento = "";
                if (dtemer.Rows[0]["medicamentos"].ToString() == null)
                    tratamiento = "";
                else
                    tratamiento = dtemer.Rows[0]["medicamentos"].ToString();
                lbTratamiento.Text = tratamiento;

            }
            else
            {
                lbCon1.Text = null;  lbCon2.Text = null; lbMedicos.Text = null; lbTratamiento.Text = null;
            }
        }

        private void oculta (bool opcion)
        {
            Label4.Visible = opcion; Label5.Visible = opcion; Label6.Visible = opcion;  Label7.Visible= opcion; //Label3.Visible = opcion;
            Label8.Visible = opcion; Label9.Visible = opcion;   Label10.Visible = opcion;   Label11.Visible = opcion;   
            Label14.Visible = opcion; Label15.Visible = opcion; Label16.Visible = opcion; Label17.Visible = opcion;
            Label18.Visible = opcion; Label19.Visible = opcion; Label20.Visible = opcion; Label21.Visible = opcion; Label22.Visible = opcion;
            lbNombre.Visible = opcion; lbCUI.Visible = opcion; lbDireccion.Visible = opcion; lbTel.Visible = opcion;    lbEmail.Visible = opcion;   
            lbPais.Visible = opcion; lbNac.Visible = opcion;    lbEst.Visible = opcion; lbMunicipio.Visible = opcion;
            lbDepartamento.Visible = opcion; lbComunidad.Visible = opcion;  lbPensum.Visible = opcion; lbCon1.Visible = opcion;
            lbCon2.Visible = opcion; lbMedicos.Visible = opcion; lbTratamiento.Visible = opcion; lbCarnet.Visible = opcion;
            Image1.Visible = opcion;
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Nivelin();
        }

        protected void btNuevo_Click(object sender, EventArgs e)
        {
            string alerta = BootstrapAlertHelper.GetAlert("success", "¡Operación completada con éxito!");
             TextBox2.Text = alerta;
            //Response.Redirect("AlumnosIndividual.aspx");
        }

        protected void btnAbrirModal_Click(object sender, EventArgs e)
        {
            string script = "$('#myModal').modal('show');";
            ClientScript.RegisterStartupScript(this.GetType(), "ShowModal", script, true);
        }
    }
}