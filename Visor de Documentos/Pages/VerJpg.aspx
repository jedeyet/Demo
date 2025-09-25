<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VerJPG.aspx.cs" Inherits="Visor_de_Documentos.Pages.VerJPG" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderC" runat="server">
    <script src="../Scripts/jquery-1.10.2.min.js" type="text/javascript"></script>    
    <script src="../Scripts/Default.aspx.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form1" runat="server">
        <asp:Label ID="Label1" runat="server" Text="Carnet del alumno"></asp:Label>
        <asp:TextBox ID="TextBox1" runat="server">200708131</asp:TextBox>
        <br />
        <br />
        <asp:Button ID="BtVerJPG" runat="server" Text="Ver JPG" OnClick="BtVerJPG_Click" />
           <br />
     
        <div class="embed-container">
            <iframe id="jpg" runat="server" height="179" width="130" ></iframe> 
     </div>
    </form>
</asp:Content>
