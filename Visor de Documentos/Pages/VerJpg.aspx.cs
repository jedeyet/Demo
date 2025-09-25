using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;


namespace Visor_de_Documentos.Pages
{
    public partial class VerJPG : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void BtVerJPG_Click(object sender, EventArgs e)
        {
            HtmlControl frame1 = (HtmlControl)Page.Master.FindControl("MainContent").FindControl("jpg");
            /*Esto ya no va
            string cadena1 = "200708131" + " / " + "Egreso";
            string cadena1 = "file:///F:/PIC/" + TextBox1.Text.Trim() + "/" + DropDownList1.Text.Trim() + ".pdf";
            */

            frame1.Attributes["src"] = "webFormJPG.aspx";
            Session["car"] = this.TextBox1.Text.Trim();            
        }
    }
}