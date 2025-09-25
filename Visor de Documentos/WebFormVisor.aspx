<%@ Page Title="Visor" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WebFormVisor.aspx.cs" Inherits="VisorDeDocumentos.WebFormVisor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderC" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

     <script type="text/Javascript" src='<%=ResolveUrl("~/Scripts/jquery-1.10.2.js")%>'></script>
        <script type="text/Javascript" src='<%=ResolveUrl("~/Scripts/jquery-1.10.2.min.js")%>'></script>

    <%--<div id="dialog" title="Dialog Title">

 <object  data="webFormPDF.aspx" width="100%" type="application/pdf">
    </object>


</div>--%>

  <%--  <object type="application/pdf" data="webFormPDF.aspx#toolbar=1&amp;navpanes=0&amp;scrollbar=1" 
width="900" height="500">
<param name="src" value="webFormPDF.aspx" />
<p style="text-align:center; width: 60%;">Adobe Reader no se encuentra o la versión no es compatible, 
utiliza el icono para ir a la página de descarga <br />
<a href="http://get.adobe.com/es/reader/" onclick="this.target='_blank'">
<img src="reader_icon_special.jpg" alt="Descargar Adobe Reader" 
width="32" height="32" style="border: none;" /></a></p>
</object>--%>

    <div id="Dialog2" title="Ifra Title">
 <iframe src= "webFormPDF.aspx"  type="application/pdf"  width="100%">
    </iframe>
</div>

   <%-- <script>
        $(document).ready(function () {
            $("#Dialog2").dialog();
        });
  </script>--%>

</asp:Content>
