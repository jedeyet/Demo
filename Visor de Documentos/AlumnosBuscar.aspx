<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AlumnosBuscar.aspx.cs" Inherits="Visor_de_Documentos.AlumnosBuscar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderC" runat="server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form1" runat="server">
        <asp:Literal ID="litMensaje" runat="server" />

        <div class="input-group mb-3">
            <asp:TextBox ID="txtBusqueda" runat="server" CssClass="form-control" placeholder="Carné, DPI o nombre"></asp:TextBox>
            <asp:Button ID="btnBuscar" runat="server" CssClass="btn btn-primary" Text="Buscar" OnClick="btnBuscar_Click" />
        </div>

        <asp:GridView ID="gvResultados" runat="server" CssClass="table table-bordered table-hover" AutoGenerateColumns="true" />
    </form>
</asp:Content>
