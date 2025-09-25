<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PagsDocentesSedeDocumentos.aspx.cs" Inherits="Visor_de_Documentos.PagsDocentesSedeDocumentos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderC" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form1" runat="server">
        <asp:Label ID="Label5" runat="server" Text="Subida de Documentos por Docentes por Sede" CssClass="bg-gray-dark" Font-Bold="True"></asp:Label>
        <br />
        <asp:Label ID="Label1" runat="server" Text="Sede"></asp:Label>
        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="bg-green-dark" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" Width="153px">
            <asp:ListItem Value="1">Quetzaltenango</asp:ListItem>
            <asp:ListItem Value="2">Guatemala</asp:ListItem>
            <asp:ListItem Value="3">Cobán</asp:ListItem>
            <asp:ListItem Value="4">Teologado</asp:ListItem>
            <asp:ListItem Value="5">Izabal</asp:ListItem>
        </asp:DropDownList>
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Por Facultad" CssClass="bg-primary-dark" BackColor="#003300" Height="41px" Width="164px" />
        &nbsp;<asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Por orden alfabético" CssClass="bg-primary-dark" BackColor="#003300" Height="42px" Width="164px" />
        <br />
        <asp:ImageButton ID="imgbutExc" runat="server" Height="25px" ImageUrl="~/Imagenes/excel2.jpg" OnClick="imgbutExc_Click" Width="33px" ToolTip="Traslada a Excel los resultados del filtro" Visible="False" />
        <br />
        <asp:Label ID="lbResultado" runat="server"></asp:Label>
        <asp:GridView ID="GridView1" runat="server" HorizontalAlign="Center" CssClass="bg-green-dark" >
        </asp:GridView>
    </form>
</asp:Content>
