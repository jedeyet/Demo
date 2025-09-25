using System;
using System.Web;
using System.IO;

namespace Visor_de_Documentos.Pages
{
    public partial class webFormJPG : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Aqui le pasas el stream para que escriba el contenido del PDF
                //Mostrar(File.Open("F:/PDFS/200708131/Egreso.pdf", FileMode.Open));
                string carn = Session["car"].ToString();                
                Mostrar(File.Open("F:/PIC/" + carn + ".jpg", FileMode.Open));        

            }

        }

        public void Mostrar(Stream stream)
        {
            var context = HttpContext.Current;
            context.Response.Clear();


            //Aqui esta el truco, donde le decimos a al response que
            //escriba el contenido del archivo con el array de bytes obtenido del stream
            context.Response.BinaryWrite(GetBytes(stream));
            context.Response.ContentType = "image/JPEG";

            // obligamos que termine la Ejecucion del recurso
            stream.Close();
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