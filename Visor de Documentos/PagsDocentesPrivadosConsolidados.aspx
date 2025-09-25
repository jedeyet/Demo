<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PagsDocentesPrivadosConsolidados.aspx.cs" Inherits="Visor_de_Documentos.PagsDocentesPrivadosConsolidados" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderC" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form278" runat="server">
        
        <div class="card mb-6 text-center">
                <div class="card-body">
                    <h5 class="card-title mb-3">Privados Consolidados por Carrera, fasey convocatoria</h5>                            
                        <div class="text-start">      
        
        <br />
        <asp:Label ID="Label1" runat="server" Text="Sede"></asp:Label>
        <asp:DropDownList ID="DropDownList1" runat="server" class ="btn btn-outline-success dropdown-toggle mb-1" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="true"  AutoPostBack="True"  OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
            <asp:ListItem Value="1">Quetzaltenango</asp:ListItem>
            <asp:ListItem Value="2">Guatemala</asp:ListItem>
            <asp:ListItem Value="3">Cobán</asp:ListItem>
            <asp:ListItem Value="4">Teologado</asp:ListItem>
            <asp:ListItem Value="5">Izabal</asp:ListItem>
        </asp:DropDownList>
                            <br />
        <asp:Label ID="Label4" runat="server" Text="Carrera"></asp:Label>
                            <asp:DropDownList ID="ddCarrera" runat="server" class ="btn btn-outline-success dropdown-toggle mb-1" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="true"  Width="500" AutoPostBack="True" OnSelectedIndexChanged="ddCarrera_SelectedIndexChanged">
        </asp:DropDownList>
        <br />
        <asp:Label ID="Label2" runat="server" Text="Año"></asp:Label>
        <asp:DropDownList ID="ddAnio" runat="server" class ="btn btn-outline-success dropdown-toggle mb-1" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="true" >
        </asp:DropDownList>
        &nbsp;
        <asp:Label ID="Label3" runat="server" Text="Convocatoria"></asp:Label>
        <asp:DropDownList ID="ddConvocatoria" runat="server" class ="btn btn-outline-success dropdown-toggle mb-1" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="true" >
        </asp:DropDownList>
        <asp:Label ID="Label5" runat="server" Text="Fase"></asp:Label>
        
        <asp:DropDownList ID="ddFase" runat="server" class ="btn btn-outline-success dropdown-toggle mb-1" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="true" >
        </asp:DropDownList>
        
        <br />
        &nbsp;&nbsp; 
                            <br />
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Proceder" class="btn btn-success mb-1" Width="188px"  />
        &nbsp;<asp:ImageButton ID="imgbutExc" runat="server"  ImageUrl="~/Imagenes/excel2.jpg" OnClick="imgbutExc_Click" ToolTip="Traslada a Excel los resultados del filtro" Visible="False" class ="btn btn-icon btn-icon-end btn-tertiary mb-1"/>                        
                            &nbsp;<asp:Button ID="btNuevo" runat="server" Text="Nuevo" Width="113px" class="btn btn-danger mb-1" OnClick="btNuevo_Click" Height="41px" />
       
                            <br />
        <br />
        <asp:Label ID="lbResultado" runat="server"></asp:Label>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center" class="table table-striped">
                        <Columns>
                <asp:TemplateField HeaderText="No.">
                    <ItemTemplate>
                        <asp:Label ID="Num" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Num") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Alumno">
                    <ItemTemplate>
                        <asp:Label ID="Alumno" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Alumno") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Total">
                    <ItemTemplate>
                        <asp:Label ID="Total" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Total") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
                    </asp:GridView>
        </div></div></div>
    </form>
</asp:Content>
