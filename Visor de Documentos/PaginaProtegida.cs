using System;
using System.Web;
using System.Web.UI;
namespace Visor_de_Documentos
{
    public class PaginaProtegida : Page
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // 🚪 Verificar si la sesión está activa
            if (Session["Usuario"] == null)
            {
                Response.Redirect("~/Login.aspx", true);
                return;
            }

            // 🛡️ Evitar cache para que no se pueda usar el botón "Atrás"
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
        }
    }
}