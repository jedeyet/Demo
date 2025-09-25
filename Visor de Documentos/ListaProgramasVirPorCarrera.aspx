<%@ Page Title="" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="false" CodeFile="ListaProgramasVirPorCarrera.aspx.vb" Inherits="ListarProgramasVirtuales" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderC" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 270px;
        }
        .auto-style2 {
            height: 42px;
        }
        .auto-style3 {
            width: 270px;
            height: 42px;
        }
    </style>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <form id="form1" runat="server">
        <div align = left>
         Listado de Programas Virtuales por Carrera<br />
        
        <table class="auto-style6">
            <tr>
                <td colspan="2">
        
                        <asp:RadioButtonList ID="rblSemestre" runat="server" AutoPostBack="True" RepeatDirection="Horizontal" class ="btn btn-outline-success  mb-1" >
                            <asp:ListItem Selected="True" Value="0">Ambos</asp:ListItem>
                            <asp:ListItem Value="1">Impar</asp:ListItem>
                            <asp:ListItem Value="2">Par</asp:ListItem>
                        </asp:RadioButtonList>
                        </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="lblContenido3" runat="server">Año</asp:Label></td>
                <td class="auto-style3">
                        <asp:DropDownList ID="ddlAnio" runat="server" AutoPostBack="True" class ="btn btn-outline-success dropdown-toggle mb-1" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="true">
                        </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="auto-style4">
                    <asp:Label ID="lblContenido1" runat="server">Carrera</asp:Label>&nbsp;</td>
                <td class="auto-style1">
                        <asp:DropDownList ID="ddlCarrera" runat="server" AutoPostBack="True" class ="btn btn-outline-success dropdown-toggle mb-1" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="true">
                        </asp:DropDownList>
        
                </td>
            </tr>
            
                 
                    <asp:Label ID="lblContenido2" runat="server">Programa</asp:Label> 
                
        
                
        
            <tr>
                <td class="auto-style4">
                    <asp:Label ID="lblContenido4" runat="server">Asignatura</asp:Label></td>
                <td class="auto-style1">
        <asp:DropDownList ID="ddlListaPV" runat="server" AutoPostBack="True" class ="btn btn-outline-success dropdown-toggle mb-1" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="true">
        </asp:DropDownList>
        
                </td>
            </tr>
            
                 
                                    
        
        </table>
        <br />
        <asp:Label ID="lblMensaje" runat="server" Font-Size="14pt" ForeColor="#FF0000"></asp:Label>
        <br />
                    <asp:Label ID="lblContenido" runat="server"></asp:Label><br />
        <br />        
        <br />
        <br />
        <br />
    </div>
    
    </form>
</asp:Content>
