<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PagsReporteEscaneos.aspx.cs" Inherits="Visor_de_Documentos.Escaneos.PagsReporteEscaneos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderC" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form1" runat="server">
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
        Seleccione la fecha inicial</span>
            <span> 
        <br />
        <asp:TextBox ID="TextBox1" runat="server" TextMode="Date"></asp:TextBox>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
        Seleccione la fecha final 
        <br />
        <asp:TextBox ID="TextBox2" runat="server" TextMode="Date" OnTextChanged="TextBox2_TextChanged"></asp:TextBox>
        <br />
        </span>
                    
            <span class="text">Seleccione a la persona</span>  <br />          
            <asp:DropDownList ID="ddlAdmin" runat="server" DataTextField="Nombre" DataValueField="ID_Admin">
            </asp:DropDownList>
            <asp:SqlDataSource ID="SqlDataSourceAdminsEscaneo" runat="server" ConnectionString="<%$ ConnectionStrings:NOTASMESOConnectionString %>" SelectCommand="SELECT DISTINCT Admins.ID_Admin, { fn CONCAT(Admins.Apellidos, { fn CONCAT(', ', Admins.Nombres) }) } AS Nombre FROM Admins INNER JOIN Escaneos ON Admins.ID_Admin = Escaneos.IdAdmin ORDER BY Nombre"></asp:SqlDataSource>
                    
            
            
            
            <asp:SqlDataSource ID="SqlDataSourceAdminsEscaneoGuate" runat="server" ConnectionString="<%$ ConnectionStrings:NOTASMESOConnectionReporteEscaneoAdminsGuate %>" ProviderName="<%$ ConnectionStrings:NOTASMESOConnectionReporteEscaneoAdminsGuate.ProviderName %>" SelectCommand="SELECT DISTINCT Admins.ID_Admin, { fn CONCAT(Admins.Apellidos, { fn CONCAT(', ', Admins.Nombres) }) } AS Nombre FROM Admins INNER JOIN Escaneos ON Admins.ID_Admin = Escaneos.IdAdmin ORDER BY Nombre"></asp:SqlDataSource>
                    
            
            
            
            <asp:Button ID="btnReporte" runat="server" Text="Ver Reporte" CssClass="mb-sm btn btn-success" OnClick="btnReporte_Click"   />
            <strong> <asp:Label ID="lblNoExpedientes" runat="server" CssClass="auto-style7"></asp:Label> 
            <span> <br />
        <asp:Button ID="btNuevo" runat="server" Text="Nuevo" Width="113px" class="btn btn-danger mb-1" OnClick="btNuevo_Click" Height="41px" />
       
        </span>
                    
                         
        </strong>
        <br />
        <br />
        <span>Quetzaltenango</span>
        <asp:GridView ID="grdReporte" runat="server"  EmptyDataText="Sede Xela NO hay datos que mostrar." class ="table table-striped table-sm" >            
        </asp:GridView>     
        <span>Guatemala</span>
        <asp:GridView ID="GridViewGuate" runat="server"  EmptyDataText="Sede Guatemala NO hay datos que mostrar." class ="table table-striped" >            
        </asp:GridView>
        <span>Cobán</span>
        <asp:GridView ID="GridViewCoban" runat="server"  EmptyDataText="Sede Cobán NO hay datos que mostrar." class ="table table-striped" >            
        </asp:GridView>
        <span>CENSES</span>
        <asp:GridView ID="GridViewTeo" runat="server"  EmptyDataText="Sede CENSES NO hay datos que mostrar." class ="table table-striped" >            
        </asp:GridView>
        <span>Izabal</span>
        <asp:GridView ID="GridViewIzabal" runat="server"  EmptyDataText="Sede Izabal NO hay datos que mostrar." class ="table table-striped" >            
        </asp:GridView>
        <span>Amatitlán</span>
        <asp:GridView ID="GridViewAmatitlan" runat="server"  EmptyDataText="Sede Amatitlán NO hay datos que mostrar." class ="table table-striped" >            
        </asp:GridView>
    </form>
</asp:Content>
