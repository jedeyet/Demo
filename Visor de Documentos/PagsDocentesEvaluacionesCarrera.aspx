<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PagsDocentesEvaluacionesCarrera.aspx.cs" Inherits="Visor_de_Documentos.PagsDocentesEvaluacionesCarrera" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderC" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form278" runat="server">
         
        <div class="card mb-6 text-center">
                <div class="card-body">
                    <h5 class="card-title mb-3">Evaluaciones por asignatura</h5>                            
                        <div class="text-start">   
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
        &nbsp;<asp:DropDownList ID="ddCarrera" runat="server" class ="btn btn-outline-success dropdown-toggle mb-1" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="true" Width="500">
        </asp:DropDownList>
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Ver Asignaturas" class="btn btn-success mb-1" />
        &nbsp;<asp:Button ID="btNuevo" runat="server" Text="Nuevo" Width="113px" class="btn btn-danger mb-1" OnClick="btNuevo_Click" Height="41px" />
       
        <br />
        <br />
        <asp:Label ID="lbResultado" runat="server"></asp:Label>
        <asp:GridView ID="GridView1" runat="server" HorizontalAlign="Center" class="table table-striped" >
        </asp:GridView>
                            </div></div></div>
    </form>

</asp:Content>
