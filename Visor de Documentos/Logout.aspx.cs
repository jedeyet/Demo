    using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace Visor_de_Documentos
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //HttpContext.Current.Session.Clear();
            //HttpContext.Current.Session.Abandon();
            //HttpContext.Current.Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
            Session.Clear();
            Session.Abandon();

            string cadena = "Update registro set alumno = '" + DateTime.Now.ToLongTimeString() + "' where observaciones = '" +
               Session["adm"] + "' and asignatura = '" + Session["Asig"] + "'";
            SqlDataAdapter da = new SqlDataAdapter(cadena, Models.Conex.Con_X);
            DataTable dt = new DataTable(); da.Fill(dt);
            if (Request.Cookies[".ASPXAUTH"] != null)
            {
                HttpCookie myCookie = new HttpCookie(".ASPXAUTH");
                myCookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(myCookie);
            }
            //FormsAuthentication.SignOut();
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            Response.Redirect("~/Account/Login.aspx");

        }
    }
}