<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PagsDocentesSedeDocumentos.aspx.cs" Inherits="Visor_de_Documentos.PagsDocentesSedeDocumentos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderC" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form1" runat="server">
         <div class="card mb-6 text-center">
                <div class="card-body">
                    <h5 class="card-title mb-3">Subida de Documentos por Docentes por Sede</h5>                            
                        <div class="text-start">   
                <br />
        <asp:Label ID="Label1" runat="server" Text="Sede"></asp:Label>
        <asp:DropDownList ID="DropDownList1" runat="server" class ="btn btn-outline-success dropdown-toggle mb-1" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="true" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" Width="153px">
            <asp:ListItem Value="1">Quetzaltenango</asp:ListItem>
            <asp:ListItem Value="2">Guatemala</asp:ListItem>
            <%--<asp:ListItem Value="3">Cobán</asp:ListItem>
            <asp:ListItem Value="4">Teologado</asp:ListItem>
            <asp:ListItem Value="5">Izabal</asp:ListItem>--%>
        </asp:DropDownList>
        <br /><br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Por Facultad" class="btn btn-success mb-1"  Height="41px" Width="164px" />
        &nbsp;<asp:Button ID="Button2" runat="server" OnClick="Button2_Click" class="btn btn-success mb-1" Text="Por orden alfabético" Height="41px" Width="164px" />
        <br />
        <asp:ImageButton ID="imgbutExc" runat="server"  ImageUrl="~/Imagenes/excel2.jpg" OnClick="imgbutExc_Click" ToolTip="Traslada a Excel los resultados del filtro" Visible="False" class ="btn btn-icon btn-icon-end btn-tertiary mb-1"/>                        
        &nbsp;<asp:Button ID="btNuevo" runat="server" Text="Nuevo" Width="113px" class="btn btn-danger mb-1" OnClick="btNuevo_Click" Height="41px" />
       
        <br />
        <asp:Label ID="lbResultado" runat="server"></asp:Label>
        <asp:GridView ID="GridView1" runat="server" HorizontalAlign="Center" class="table table-striped">
        </asp:GridView>
        </div>            
             </div>
      </div>
    </form>
</asp:Content>
