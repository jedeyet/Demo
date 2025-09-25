<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PagsDocentesEvaluacionCarreraDocentes.aspx.cs" Inherits="Visor_de_Documentos.PagsDocentesEvaluacionCarreraDocentes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderC" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form1UAAsp" runat="server">
    <%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>
         
        <div class="card mb-6 text-center">
                <div class="card-body">
                    <h5 class="card-title mb-3">Evaluación a docentes (por Sede, Carrera, año, ciclo y docente)</h5>                            
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
        &nbsp;&nbsp;<br />
        &nbsp;&nbsp;
        <asp:Button ID="btProceder" runat="server" OnClick="btProceder_Click" Text="Proceder" class="btn btn-success mb-1" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:ImageButton ID="imgbutExc" runat="server"  ImageUrl="~/Imagenes/excel2.jpg" OnClick="imgbutExc_Click" ToolTip="Traslada a Excel los resultados del filtro" Visible="False" class ="btn btn-icon btn-icon-end btn-tertiary mb-1"/>                        
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:Button ID="btNuevo" runat="server" Text="Nuevo" Width="113px" class="btn btn-danger mb-1" OnClick="btNuevo_Click" Height="41px" />
        <br />
        <asp:Label ID="lbResultado" runat="server"></asp:Label>
        <br />
        &nbsp;&nbsp;<br />
        <br />
        &nbsp;<asp:GridView ID="gridDominio" runat="server"  AutoGenerateColumns="False" HorizontalAlign="Center" class="table table-striped" >
            <Columns>
                <asp:TemplateField HeaderText="Docente">
                        <ItemTemplate>
                            <asp:Label ID="lbdoc" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.docente") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Dominio 1">
                        <ItemTemplate>
                            <asp:Label ID="lbdom1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dominio1") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Dominio 2">
                        <ItemTemplate>
                            <asp:Label ID="lbdom2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dominio2") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Dominio 3">
                        <ItemTemplate>
                            <asp:Label ID="lbdom3" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dominio3")%>' ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                <asp:TemplateField HeaderText="Dominio 4">
                        <ItemTemplate>
                            <asp:Label ID="lbdom4" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dominio4") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                <asp:TemplateField HeaderText="Dominio 5">
                        <ItemTemplate>
                            <asp:Label ID="lbdom5" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dominio5") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                <asp:TemplateField HeaderText="Dominio 6">
                        <ItemTemplate>
                            <asp:Label ID="lbdom6" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dominio6") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                <asp:TemplateField HeaderText="Promedio">
                        <ItemTemplate>
                            <asp:Label ID="lbprom" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Promedio") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                <asp:TemplateField HeaderText="Alumnos">
                        <ItemTemplate>
                            <asp:Label ID="lbalu" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Alumnos") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
        </asp:GridView>
        <br />




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
