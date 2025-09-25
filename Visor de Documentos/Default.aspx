<%@ Page Title="Home Page" Language="C#" MasterPageFile="Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="VisorDeDocumentos._Default"  ClientIDMode="Static" %>



<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeaderC">
    
    <script src="Scripts/Default.aspx.js" type="text/javascript"></script>    
</asp:Content>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

  

    <form id="form1" runat="server">
       
        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="XX-Large" Text="Sistema General de Gestión UMES"></asp:Label>
       
        </form>
    <div class="panel-body">
        
    <div class="row row-table text-center">  
         <img src="Imagenes/Portada Sistema.jpg" class="img-responsive" alt="istema General de Gestión UMES" />  
        <div class="col-xs-4">
        </div>
        <div class="col-xs-4">
           <div id ="ocultar">
           </div>
        </div>
        <div class="col-xs-4">
        </div>
                
    </div>
    </div>
                  

    

</asp:Content>

