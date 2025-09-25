<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Academico.aspx.cs" Inherits="Visor_de_Documentos.Pages.Academico" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderC" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  
    
    
      <form id="form1" runat="server">
    <p>
        <asp:Button CssClass="mb-sm btn btn-success" ID="btAprobados" runat="server" OnClick="btAprobados_Click" Text="Aprobados" />
        <asp:Button ID="btNoAprobados" runat="server" CssClass="mb-sm btn btn-success" OnClick="btNoAprobados_Click" Text="No Aprobados" />
        <asp:Button ID="btEquivalencias" runat="server" CssClass="mb-sm btn btn-success" OnClick="btEquivalencias_Click" Text="Equivalencias" />
    </p>
    <p>
        
        <br />
    </p>
    <p>
    </p>
        <div class="col-md-6 col-md-offset-3">
        <asp:GridView ID="GridView1"  runat="server" >
        </asp:GridView>
       </div>
</form>
</asp:Content>
