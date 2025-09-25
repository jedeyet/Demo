<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PagsAlumnosPasosIncompletos.aspx.cs" Inherits="Visor_de_Documentos.PagsAlumnosPasosIncompletos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderC" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <form id="form1" runat="server">
             <div class="card mb-6 text-center">
                <div class="card-body">
                    <h5 class="card-title mb-3">Alumnos con pasos pendientes de completar para inscripción</h5>   <div class="text-start">   
       
        <br />
        <asp:Label ID="Label1" runat="server" Text="Sede"></asp:Label>
        <asp:DropDownList ID="DropDownList1" runat="server" class ="btn btn-outline-success dropdown-toggle mb-1" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="true" AutoPostBack="True">
            <asp:ListItem Value="1">Quetzaltenango</asp:ListItem>
            <asp:ListItem Value="2">Guatemala</asp:ListItem>
            <asp:ListItem Value="3">Cobán</asp:ListItem>
            <asp:ListItem Value="4">Teologado</asp:ListItem>
            <asp:ListItem Value="5">Izabal</asp:ListItem>
        </asp:DropDownList>
        <br />
        <asp:Label ID="Label2" runat="server" Text="Año"></asp:Label>
        <asp:DropDownList ID="ddAnio" runat="server" class ="btn btn-outline-success dropdown-toggle mb-1" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="true">
        </asp:DropDownList>
        &nbsp;
        <asp:Label ID="Label3" runat="server" Text="Ciclo"></asp:Label>
        <asp:DropDownList ID="ddSemestre" runat="server" class ="btn btn-outline-success dropdown-toggle mb-1" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="true">
        </asp:DropDownList>
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Ver alumnos" class="btn btn-success mb-1" />
        &nbsp;<asp:Button ID="btNuevo" runat="server" Text="Nuevo" Width="113px" class="btn btn-danger mb-1" OnClick="btNuevo_Click" Height="41px" />
       
        <br />
        <asp:Label ID="lbResultado" runat="server" Visible="False"></asp:Label>
        &nbsp;<asp:ImageButton ID="imgbutExc" runat="server"  ImageUrl="~/Imagenes/excel2.jpg" OnClick="imgbutExc_Click" ToolTip="Traslada a Excel los resultados del filtro" Visible="False" class ="btn btn-icon btn-icon-end btn-tertiary mb-1"/>                        
        <br />
        <asp:GridView ID="GridView1" runat="server" HorizontalAlign="Center" class="table table-striped" >
            
        </asp:GridView>
         </div>

         </div>
         </div>
    </form>
</asp:Content>
