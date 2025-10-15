using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using System.Data;
using System.Data.SqlClient;

namespace VisorDeDocumentos
{
    public partial class SiteMaster : MasterPage
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;

        protected void Page_Init(object sender, EventArgs e)
        {
            // El código siguiente ayuda a proteger frente a ataques XSRF
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Utilizar el token Anti-XSRF de la cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generar un nuevo token Anti-XSRF y guardarlo en la cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += master_Page_PreLoad;
        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Establecer token Anti-XSRF
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Validar el token Anti-XSRF
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Error de validación del token Anti-XSRF.");
                }
            }
        }

      
        protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // 1. Ocultar todo por defecto
                Docentes.Visible = false;
                Alumnos.Visible = false;
                Registro.Visible = false;
                Contabilidad.Visible = false;
                Comunicacion.Visible = false;
                Escaneos.Visible = false;
                Entrevistas.Visible = false;
                Alcanza.Visible = false;
                liBuscarAlumno.Visible = false;
                liReportes.Visible = false;
                RptAdmin.Visible = false;
                RptFin.Visible = false;

                // 2. Usuario actual en sesión
                string usuario = "";
                if (Session["usuario"] != null)
                    usuario = Session["usuario"].ToString().Trim().ToLower();

                // 3. Consulta de permisos en la base de datos
                string cadena = "SELECT Menu FROM AdminSedesPriv WHERE usuario = @usuario";
                using (SqlConnection con = new SqlConnection("Data Source=GP4;Initial Catalog=NOTASMESO;User ID=Local;Password=L3ct0rL0c4l"))
                using (SqlDataAdapter da = new SqlDataAdapter(cadena, con))
                {
                    da.SelectCommand.Parameters.AddWithValue("@usuario", usuario);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    foreach (DataRow row in dt.Rows)
                    {
                        string permiso = row["Menu"].ToString().Trim().ToUpper();

                        switch (permiso)
                        {
                            case "DOCENTES":
                                Docentes.Visible = true;
                                break;

                            case "ALUMNOS":
                                Alumnos.Visible = true;
                                break;

                            case "REGISTRO":
                                Registro.Visible = true;
                                break;

                            case "CONTABILIDAD":
                                Contabilidad.Visible = true;
                                break;

                            case "COMUNICACION":
                                Comunicacion.Visible = true;
                                break;

                            case "ESCANEO":
                                Escaneos.Visible = true;
                                break;

                            case "ENTREVISTAS":
                                Entrevistas.Visible = true;
                                break;

                            case "ALCANZA":
                                Alcanza.Visible = true;
                                break;

                            // === NUEVOS PERMISOS DE REPORTES ===
                            case "REPORTES":
                                liReportes.Visible = true;
                                break;

                            case "RPT_ADMIN":
                                liReportes.Visible = true;
                                RptAdmin.Visible = true;
                                break;

                            case "RPT_FIN":
                                liReportes.Visible = true;
                                RptFin.Visible = true;
                                break;
                        }
                    }
                }

                // 4. Excepciones especiales
                if (usuario == "pjfr" || usuario == "jdeyet")
                {
                    liBuscarAlumno.Visible = true;
                }
            }
        }
    }
}