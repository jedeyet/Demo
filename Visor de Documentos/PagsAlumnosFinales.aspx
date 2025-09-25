<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PagsAlumnosFinales.aspx.cs" Inherits="Visor_de_Documentos.PagsAlumnosFinales" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderC" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <form id="form1aEb32" runat="server">
    <%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>
         
        <div class="card mb-6 text-center">
                <div class="card-body">
                    <h5 class="card-title mb-3">Notas finales por Sede y Carrera</h5>                            
                        <div class="text-start">   
        
        <br />
    <asp:Label ID="Label1" runat="server" Text="Sede"></asp:Label>
        <asp:DropDownList ID="DropDownList1" runat="server" class ="btn btn-outline-success dropdown-toggle mb-1" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="true" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
            <asp:ListItem Value="1">Quetzaltenango</asp:ListItem>
            <asp:ListItem Value="2">Guatemala</asp:ListItem>
        </asp:DropDownList>
        <br />
        <asp:Label ID="Label4" runat="server" Text="Carrera"></asp:Label>
        <asp:DropDownList ID="ddCarrera" runat="server" class ="btn btn-outline-success dropdown-toggle mb-1" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="true" Width="500" OnSelectedIndexChanged="ddCarrera_SelectedIndexChanged" AutoPostBack="True">
        </asp:DropDownList>
        <asp:Label ID="lbinicio" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lbFin" runat="server" Visible="False"></asp:Label>
        <br />
        <asp:Label ID="Label2" runat="server" Text="Año"></asp:Label>
        <asp:DropDownList ID="ddAnio" runat="server" class ="btn btn-outline-success dropdown-toggle mb-1" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="true" AutoPostBack="True" OnSelectedIndexChanged="ddAnio_SelectedIndexChanged">
        </asp:DropDownList>
        &nbsp;
        <asp:Label ID="Label3" runat="server" Text="Ciclo"></asp:Label>
        <asp:DropDownList ID="ddSemestre" runat="server" class ="btn btn-outline-success dropdown-toggle mb-1" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="true" AutoPostBack="True" OnSelectedIndexChanged="ddSemestre_SelectedIndexChanged">
        </asp:DropDownList>
        &nbsp;<asp:Label ID="Label5" runat="server" Text="Sección"></asp:Label>
        &nbsp;<asp:DropDownList ID="ddSeccion" runat="server" class ="btn btn-outline-success dropdown-toggle mb-1" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="true" AutoPostBack="True" OnSelectedIndexChanged="ddSeccion_SelectedIndexChanged">
        </asp:DropDownList>
        <br />
        &nbsp;&nbsp;
        <asp:Button ID="btProceder" runat="server" OnClick="btProceder_Click" Text="Proceder" class="btn btn-success mb-1" />




        <br />
        <br />
        <asp:Label ID="Label6" runat="server" Text="Asignatura" Visible="False"></asp:Label>
        &nbsp;<asp:DropDownList ID="ddAsignatura" runat="server" class ="btn btn-outline-success dropdown-toggle mb-1" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="true" Width="500" Visible="False">
        </asp:DropDownList>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Ver" class="btn btn-success mb-1" Visible="False" />




        &nbsp;<asp:ImageButton ID="imgbutExc" runat="server"  ImageUrl="~/Imagenes/excel2.jpg" OnClick="imgbutExc_Click" ToolTip="Traslada a Excel los resultados del filtro" Visible="False" class ="btn btn-icon btn-icon-end btn-tertiary mb-1"/>                        
                            <asp:Button ID="btNuevo" runat="server" Text="Nuevo" Width="113px" class="btn btn-danger mb-1" OnClick="btNuevo_Click" Height="41px" />
       



        <br />
        <asp:Label ID="lbResultado" runat="server"></asp:Label>
                            <br />
        <br />
        <asp:Label ID="Label7" runat="server" Text="Docente:" Visible="False"></asp:Label>
        &nbsp;<asp:Label ID="lbDocente" runat="server" Font-Bold="True"></asp:Label>
                            <br />
        <br />




        <asp:GridView ID="GridCursos" runat="server" HorizontalAlign="Center" class="table table-striped" >
        
        </asp:GridView>
        <br />




        <br />




        <br />
        <br />
        <br />
        <br />
        <br />



    </div></div></div>
    </form>

</asp:Content>
