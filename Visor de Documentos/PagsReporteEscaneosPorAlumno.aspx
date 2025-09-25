<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PagsReporteEscaneosPorAlumno.aspx.cs" Inherits="Visor_de_Documentos.PagsReporteEscaneosPorAlumno" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderC" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form2" runat="server">
        <span>Sede
        <asp:DropDownList ID="DropDownListSede" runat="server" class ="btn btn-outline-success dropdown-toggle mb-1" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="true"  AutoPostBack="True"  OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AppendDataBoundItems="True">
            <asp:ListItem>---- Seleccione la Sede ----</asp:ListItem>
            <asp:ListItem Value="1">Quetzaltenango</asp:ListItem>
            <asp:ListItem Value="2">Guatemala</asp:ListItem>
            <asp:ListItem Value="3">Cobán</asp:ListItem>
            <asp:ListItem Value="4">Teologado</asp:ListItem>
            <asp:ListItem Value="5">Izabal</asp:ListItem>
             <asp:ListItem Value="6">Amatitlán</asp:ListItem>
        </asp:DropDownList>
                            <br />
        Ingrese el Carné</span>
            <span> 
        <br />
        <asp:TextBox ID="TextBoxCarne" runat="server"></asp:TextBox>
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
        </span>
                    
            
            
            
            <asp:Button ID="btnReporte" runat="server" Text="Ver Reporte" CssClass="mb-sm btn btn-success" OnClick="btnReporte_Click"   />
            <strong> 
        <br />
        <br />
        <span> 
        <asp:Button ID="btNuevo" runat="server" Text="Nuevo" Width="113px" class="btn btn-danger mb-1" OnClick="btNuevo_Click" Height="41px" />
       
        </span>
                    
                         
        </strong>
        <br />
        <br />
            <strong> <asp:Label ID="lblAlumno" runat="server"></asp:Label> 
            <span> <br />
       
        </span>
                    
                         
        </strong>
        <br />
        Documentos Escaneados del Alumno<asp:GridView ID="GridReporte" runat="server"  EmptyDataText="NO hay datos que mostrar." class ="table table-striped" >            
        </asp:GridView>
    </form>
</asp:Content>
