<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PagsDocentesPromedio.aspx.cs" Inherits="Visor_de_Documentos.PagsDocentesPromedio" %>
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
            <asp:ListItem Value="3">Cobán</asp:ListItem>
            <asp:ListItem Value="4">Teologado</asp:ListItem>
            <asp:ListItem Value="5">Izabal</asp:ListItem>
        </asp:DropDownList>
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Ver Promedios" class="btn btn-success mb-1" Height="41px" Width="164px" />
        &nbsp;<br />
        <asp:ImageButton ID="imgbutExc" runat="server"  ImageUrl="~/Imagenes/excel2.jpg" OnClick="imgbutExc_Click" ToolTip="Traslada a Excel los resultados del filtro" Visible="False" class ="btn btn-icon btn-icon-end btn-tertiary mb-1"/>                        
        &nbsp;<asp:Button ID="btNuevo" runat="server" Text="Nuevo" Width="113px" class="btn btn-danger mb-1" OnClick="btNuevo_Click" Height="41px" />
       
        <br />
        <asp:Label ID="lbResultado" runat="server"></asp:Label>
        <asp:GridView ID="GridView1" runat="server" HorizontalAlign="Center" class="table table-striped" AutoGenerateColumns="False" >
            <Columns>
                <asp:TemplateField HeaderText="Carrera">
                        <ItemTemplate>
                            <asp:Label ID="lbCarrera" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Nombre de la Carrera") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Carnet">
                        <ItemTemplate>
                            <asp:Label ID="lbCarnet" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Número de carné") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                <asp:TemplateField HeaderText="Nombre">
                        <ItemTemplate>
                            <asp:Label ID="lbNombre" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Nombre_completo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Promedio">
                        <ItemTemplate>
                            <asp:Label ID="lbProm1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Promedio")%>' DataFormatString="{0:F2}"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
            </Columns>
        </asp:GridView>
                            </div></div></div>
    </form>
</asp:Content>
