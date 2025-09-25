<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Estudiantes3Perdidos.aspx.cs" Inherits="Visor_de_Documentos.Estudiantes3Perdidos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderC" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form1" runat="server">
        <asp:Label ID="Label1" runat="server" Text="Sede"></asp:Label>
        <asp:DropDownList ID="DropDownList1" runat="server" BackColor="#003300" Font-Bold="True" ForeColor="#CCFFFF">
            <asp:ListItem Value="1">Quetzaltenango</asp:ListItem>
            <asp:ListItem Value="2">Guatemala</asp:ListItem>
            <asp:ListItem Value="3">Cobán</asp:ListItem>
            <asp:ListItem Value="4">Teologado</asp:ListItem>
            <asp:ListItem Value="5">Izabal</asp:ListItem>
        </asp:DropDownList>
        <br />
        <asp:Label ID="Label2" runat="server" Text="Año"></asp:Label>
        <asp:DropDownList ID="ddAnio" runat="server" BackColor="#003300" Font-Bold="True" ForeColor="#CCFFFF">
        </asp:DropDownList>
        &nbsp;
        <asp:Label ID="Label3" runat="server" Text="Ciclo"></asp:Label>
        <asp:DropDownList ID="ddSemestre" runat="server" BackColor="#003300" Font-Bold="True" ForeColor="#CCFFFF">
        </asp:DropDownList>
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Ver alumnos" BackColor="#003300" Font-Bold="True" ForeColor="#CCFFFF" />
        &nbsp;<asp:Button ID="btNuevo" runat="server" Text="Nuevo" Width="113px" class="btn btn-danger mb-1" OnClick="btNuevo_Click" Height="36px" />
       
        <br />
        <asp:Label ID="lbResultado" runat="server"></asp:Label>
        <asp:GridView ID="GridView1" runat="server" HorizontalAlign="Center" class="table table-striped table-bordered table-hover" style="height: 163px">
        </asp:GridView>
    </form>
</asp:Content>
