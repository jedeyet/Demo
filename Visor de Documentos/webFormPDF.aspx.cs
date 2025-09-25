using System;
using System.Web;
using System.IO;

namespace Visor_de_Documentos.Pages
{
    public partial class webFormPDF : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {                
                
               
                try
                {
                    string archivo = Session["archivo"].ToString();
                   // Response.Write("<script>alert('" + archivo + "')</script>");

                    Mostrar(File.Open(archivo, FileMode.Open));
                }
                catch (FileNotFoundException err)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "ex", "alert('" + err.Message + "');", true);
                }
               
                
            }

        }

        public void Mostrar(Stream stream)
        {
            var context = HttpContext.Current;
            context.Response.Clear();


            //Aqui esta el truco, donde le decimos a al response que
            //escriba el contenido del archivo con el array de bytes obtenido del stream
            context.Response.BinaryWrite(GetBytes(stream));
            context.Response.ContentType = "Application/pdf";

            // obligamos que termine la Ejecucion del recurso
            stream.Close();
            string archivo = Session["archivo"].ToString();
            File.Delete(archivo);
            context.Response.End();
           

        }
        public byte[] GetBytes(Stream src)
        {
            src.Position = 0;

            int size = int.Parse(src.Length.ToString());
            byte[] buffer = new byte[size];
            src.Read(buffer, 0, size);
            return buffer;
        }


    }
}