<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PagsDocentesHistorial.aspx.cs" Inherits="Visor_de_Documentos.PagsDocentesHistorial" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderC" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form111" runat="server">
         <div class="card mb-6 text-center">
                <div class="card-body">
                    <h5 class="card-title mb-3">Historial de cursos por docentes, sede y carrera</h5>                            
                        <div class="text-start">   
       
                            <asp:Label ID="Label5" runat="server" Text="Docentes por Sede y Carrera" CssClass="bg-gray-dark" Font-Bold="True"></asp:Label>
        <br />
        <asp:Label ID="Label1" runat="server" Text="Sede"></asp:Label>
        <asp:DropDownList ID="DropDownList1" runat="server" class ="btn btn-outline-success dropdown-toggle mb-1" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="true" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
            <asp:ListItem Value="1">Quetzaltenango</asp:ListItem>
            <asp:ListItem Value="2">Guatemala</asp:ListItem>
            <asp:ListItem Value="3">Cobán</asp:ListItem>
            <asp:ListItem Value="4">Teologado</asp:ListItem>
            <asp:ListItem Value="5">Izabal</asp:ListItem>
            <asp:ListItem Value="6">Amatitlán</asp:ListItem>
        </asp:DropDownList>
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click"  class="btn btn-success mb-1" Text="Ver Docentes" Width="188px"  />
        &nbsp; 
        <br />
        <asp:Label ID="Label6" runat="server" Text="Docente" Visible="False"></asp:Label>
        <asp:DropDownList ID="ddDocente" runat="server" class ="btn btn-outline-success dropdown-toggle mb-1" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="true" Width="500" AutoPostBack="True" OnSelectedIndexChanged="ddDocente_SelectedIndexChanged" Visible="False">
        </asp:DropDownList>
        <br />
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click"  class="btn btn-success mb-1" Text="Ver Historial" Width="188px" Visible="False"  />
        &nbsp;<asp:Button ID="btNuevo" runat="server" Text="Nuevo" Width="113px" class="btn btn-danger mb-1" OnClick="btNuevo_Click" Height="41px" />
       
        <br />
        <asp:Label ID="lbResultado" runat="server"></asp:Label>
        <asp:GridView ID="GridView1" runat="server" HorizontalAlign="Center" class="table table-striped" >
        </asp:GridView>
        <asp:ImageButton ID="imgbutExc" runat="server"  ImageUrl="~/Imagenes/excel2.jpg" OnClick="imgbutExc_Click" ToolTip="Traslada a Excel los resultados del filtro" Visible="False" class ="btn btn-icon btn-icon-end btn-tertiary mb-1"/>                        
                            </div></div></div>
    </form>

</asp:Content>
