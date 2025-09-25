<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PagsDocentesActasNoEnviadas.aspx.cs" Inherits="Visor_de_Documentos.PagsDocentesActasNoEnviadas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderC" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <form id="form278" runat="server">
            
            <div class="card mb-6 text-center">
                <div class="card-body">
                    <h5 class="card-title mb-3">Zonas o finales:  Enviado o no Enviados</h5>                            
                        <div class="text-start">      
      
            <br />
        <asp:Label ID="Label1" runat="server" Text="Sede"></asp:Label>
        <asp:DropDownList ID="DropDownList1" runat="server" class ="btn btn-outline-success dropdown-toggle mb-1" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="true" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
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
        <asp:Label ID="Label4" runat="server" Text="Carrera"></asp:Label>
        &nbsp;<asp:DropDownList ID="ddCarrera" runat="server" class ="btn btn-outline-success dropdown-toggle mb-1" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="true" Width="500" AutoPostBack="True" OnSelectedIndexChanged="ddCarrera_SelectedIndexChanged">
        </asp:DropDownList>
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Zonas enviadas" class="btn btn-dark mb-1" Width="188px" BackColor="#003300" />
        &nbsp;<asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Zonas  NO enviadas" class="btn btn-success mb-1" Width="188px" />
        &nbsp; <asp:Button ID="Button5" runat="server" OnClick="Button5_Click" Text="Todas Zonas  NO enviadas" class="btn btn-success mb-1" Width="205px" />
        &nbsp; <asp:Button ID="Button7" runat="server" OnClick="Button7_Click" Text="Actas No firmadas por Carrera" class="btn-gradient-secondary"  Width="205px" />
        <br />
        <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Finales Enviados" class="btn btn-dark mb-1" Width="188px" BackColor="#003300" />
        &nbsp;<asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="Finales NO enviados" class="btn btn-success mb-1" Width="188px" />
        &nbsp; <asp:Button ID="Button6" runat="server" OnClick="Button6_Click" Text="Todos Finales NO enviados" class="btn btn-success mb-1" Width="205px" />
        &nbsp; <asp:Button ID="Button8" runat="server" OnClick="Button8_Click" Text="Todas las actas NO firmadas" class="btn-gradient-secondary" Width="205px" />
        &nbsp;                        
                    <asp:Button ID="btNuevo" runat="server" Text="Nuevo" Width="113px" class="btn btn-danger mb-1" OnClick="btNuevo_Click" Height="41px" />
       
        <br />
        <asp:Label ID="lbResultado" runat="server"></asp:Label>
        &nbsp; 
        <asp:ImageButton ID="imgbutExc" runat="server"  ImageUrl="~/Imagenes/excel2.jpg" OnClick="imgbutExc_Click" ToolTip="Traslada a Excel los resultados del filtro" Visible="False" class ="btn btn-icon btn-icon-end btn-tertiary mb-1"/>                        
        <asp:GridView ID="GridView1" runat="server" class="table table-striped" HorizontalAlign="Center"  >
        </asp:GridView>
        </div>            
             </div>
      </div>
    </form>
    </asp:Content>
