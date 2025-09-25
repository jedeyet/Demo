<%@ Page Title="" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="false" CodeFile="ListarPVCatedraticos.aspx.vb" Inherits="ListarProgramasVirtuales" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderC" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <form id="form1" runat="server">
  
        <span style="font-size: 20pt">Listado de Programas Virtuales Todos los Cátedraticos<br />
        </span>
        <br />
        <div style="text-align:left">
        <table class="auto-style6">
            <tr>
                <td colspan="2">
                    <asp:Label ID="Label3" runat="server" Text="Sede"></asp:Label>
                    <asp:DropDownList ID="DropDownList1" runat="server" class ="btn btn-outline-success dropdown-toggle mb-1" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="true"  AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                        <asp:ListItem Value="1">Quetzaltenango</asp:ListItem>
                        <asp:ListItem Value="2">Guatemala</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;<asp:Button ID="Button1" runat="server" OnClick="Button1_Click"  class="btn btn-success mb-1" Text="Ver Docentes" Width="188px"  />
                    <br />
                    <br />
                    <asp:RadioButtonList ID="rblSemestre" runat="server" AutoPostBack="True" RepeatDirection="Horizontal" class ="btn btn-outline-success  mb-1"
                        Width="267px" ForeColor="#0000CC">
                        <asp:ListItem Value="1">Enero - Junio</asp:ListItem>
                        <asp:ListItem Value="2">Julio - Noviembre</asp:ListItem>
                    </asp:RadioButtonList>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style4">
                    <asp:Label ID="lblContenido3" runat="server">Año</asp:Label></td>
                <td class="auto-style2">
                    <asp:DropDownList ID="ddlAnio" runat="server" AutoPostBack="True" class ="btn btn-outline-success dropdown-toggle mb-1" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="true" >
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="auto-style4">
                    <asp:Label ID="lblContenido1" runat="server">Docente</asp:Label>&nbsp;</td>
                <td class="auto-style2">
                    <asp:DropDownList ID="ddlCatedraticos" runat="server" AutoPostBack="True" class ="btn btn-outline-success dropdown-toggle mb-1" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="true">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="auto-style3">
                    <asp:Label ID="lblContenido2" runat="server">Asignatura</asp:Label></td>
                <td class="auto-style5">
        <asp:DropDownList ID="ddlListaPV" runat="server" AutoPostBack="True" class ="btn btn-outline-success dropdown-toggle mb-1" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="true">
        </asp:DropDownList>
                </td>
            </tr>
        </table>
      
        <br />
        <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="Blue"></asp:Label>
        <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="Maroon"></asp:Label>
        <br />
        <asp:Label ID="lblMensaje" runat="server" Font-Size="14pt" ForeColor="#FF0000"></asp:Label><br />
        <table style="border-right: black thin solid; border-top: black thin solid; border-left: black thin solid;
            width: 80%; border-bottom: black thin solid">
            <tr>
                <td colspan="3" style="text-align: justify">
                    <asp:Label ID="lblContenido" runat="server"></asp:Label></td>
            </tr>
        </table>
        <br />
        <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="~/Default.aspx" 
            Visible="False">Regresar al Menú Principal</asp:LinkButton><br />
        <br />
        <br />
      </div>
 
    </form>
</asp:Content>
