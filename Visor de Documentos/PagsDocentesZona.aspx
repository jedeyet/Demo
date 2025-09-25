<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PagsDocentesZona.aspx.cs" Inherits="Visor_de_Documentos.PagsDocentesZona" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderC" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form278" runat="server">
        <div class="card mb-6 text-center">
                <div class="card-body">
                    <h5 class="card-title mb-3">Zonas al día por Sede y Carrera</h5>   <div class="text-start">   
        <br />

        <asp:Label ID="Label1" runat="server" Text="Sede"></asp:Label>
        <asp:DropDownList ID="DropDownList1" runat="server" class ="btn btn-outline-success dropdown-toggle mb-1" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="true" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
            <asp:ListItem Value="1">Quetzaltenango</asp:ListItem>
            <asp:ListItem Value="2">Guatemala</asp:ListItem>
            <%--<asp:ListItem Value="3">Cobán</asp:ListItem>
            <asp:ListItem Value="4">Teologado</asp:ListItem>
            <asp:ListItem Value="5">Izabal</asp:ListItem>--%>
        </asp:DropDownList>
        <br />
        <asp:Label ID="Label2" runat="server" Text="Año"></asp:Label>
        <asp:DropDownList ID="ddAnio" runat="server" class ="btn btn-outline-success dropdown-toggle mb-1" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="true">
        </asp:DropDownList>
        &nbsp;
        <asp:Label ID="Label3" runat="server" Text="Ciclo"></asp:Label>
        <asp:DropDownList ID="ddSemestre" runat="server" class ="btn btn-outline-success dropdown-toggle mb-1" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="true">
        </asp:DropDownList>
        &nbsp;<br />
        <asp:Label ID="Label4" runat="server" Text="Carrera"></asp:Label>
        &nbsp;<asp:DropDownList ID="ddCarrera" runat="server" class ="btn btn-outline-success dropdown-toggle mb-1" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="true" Width="500" OnSelectedIndexChanged="ddCarrera_SelectedIndexChanged1">
        </asp:DropDownList>
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Ver Asignaturas" class="btn btn-success mb-1" />
        &nbsp;<asp:ImageButton ID="imgbutExc" runat="server"  ImageUrl="~/Imagenes/excel2.jpg" OnClick="imgbutExc_Click" ToolTip="Traslada a Excel los resultados del filtro" Visible="False" class ="btn btn-icon btn-icon-end btn-tertiary mb-1"/>                        
        &nbsp;<asp:Button ID="btNuevo" runat="server" Text="Nuevo" Width="113px" class="btn btn-danger mb-1" OnClick="btNuevo_Click" Height="41px" />
       
        <br />
        <br />
        <asp:Label ID="lbResultado" runat="server"></asp:Label>

        <asp:GridView ID="GridView1"  runat="server" HorizontalAlign="Center"  class="table table-striped" Height="69px" AutoGenerateColumns="False" >
            <Columns>
                <asp:TemplateField HeaderText="Codigo" FooterStyle-HorizontalAlign ="Center" HeaderStyle-VerticalAlign="Middle" Visible="False"  >
                     <ItemTemplate>
                        <asp:Label ID="lbCodigo" runat="server"   Text='<%# DataBinder.Eval(Container, "DataItem.Codigo_Asignacion_Curso_Profesor") %>'></asp:Label>
                    </ItemTemplate>
        <FooterStyle HorizontalAlign="Center"></FooterStyle>
        <HeaderStyle VerticalAlign="Middle"></HeaderStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Ciclo" FooterStyle-HorizontalAlign ="Center" HeaderStyle-VerticalAlign="Middle">
                     <ItemTemplate>
                        <asp:Label ID="lbCiclo" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Semestre") %>'></asp:Label>
                    </ItemTemplate>

<FooterStyle HorizontalAlign="Center"></FooterStyle>

<HeaderStyle VerticalAlign="Middle"></HeaderStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Asignatura" FooterStyle-HorizontalAlign ="Center" HeaderStyle-VerticalAlign="Middle">
                     <ItemTemplate>
                        <asp:Label ID="lbAsignatura" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Asignatura") %>'></asp:Label>
                    </ItemTemplate>
<FooterStyle HorizontalAlign="Center"></FooterStyle>
<HeaderStyle VerticalAlign="Middle"></HeaderStyle>

<HeaderStyle VerticalAlign="Middle"></HeaderStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Sección" FooterStyle-HorizontalAlign ="Center" HeaderStyle-VerticalAlign="Middle">
                     <ItemTemplate>
                        <asp:Label ID="lbseccion" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.seccion") %>'></asp:Label>
                    </ItemTemplate>
<FooterStyle HorizontalAlign="Center"></FooterStyle>
<HeaderStyle VerticalAlign="Middle"></HeaderStyle>

                </asp:TemplateField>
                <asp:TemplateField HeaderText="Docente" FooterStyle-HorizontalAlign ="Center" HeaderStyle-VerticalAlign="Middle">
                     <ItemTemplate>
                        <asp:Label ID="lbDocente" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Nombre y apellidos del catedrático") %>'></asp:Label>
                    </ItemTemplate>

<FooterStyle HorizontalAlign="Center"></FooterStyle>

<HeaderStyle VerticalAlign="Middle"></HeaderStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Zona" FooterStyle-HorizontalAlign ="Center" HeaderStyle-VerticalAlign="Middle">
                     <ItemTemplate>
                        <asp:Label ID="lbZona" runat="server" ></asp:Label>
                    </ItemTemplate>

<FooterStyle HorizontalAlign="Center"></FooterStyle>
<HeaderStyle VerticalAlign="Middle"></HeaderStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="P1" FooterStyle-HorizontalAlign ="Center" HeaderStyle-VerticalAlign="Middle">
                     <ItemTemplate>
                        <asp:Label ID="lbp1" runat="server" ></asp:Label>
                    </ItemTemplate>

<FooterStyle HorizontalAlign="Center"></FooterStyle>
<HeaderStyle VerticalAlign="Middle"></HeaderStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="P2" FooterStyle-HorizontalAlign ="Center" HeaderStyle-VerticalAlign="Middle">
                     <ItemTemplate>
                        <asp:Label ID="lbp2" runat="server" ></asp:Label>
                    </ItemTemplate>

<FooterStyle HorizontalAlign="Center"></FooterStyle> <HeaderStyle VerticalAlign="Middle"></HeaderStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="P3" FooterStyle-HorizontalAlign ="Center" HeaderStyle-VerticalAlign="Middle">
                     <ItemTemplate> <asp:Label ID="lbp3" runat="server" ></asp:Label> </ItemTemplate>

            <FooterStyle HorizontalAlign="Center"></FooterStyle> <HeaderStyle VerticalAlign="Middle"></HeaderStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="L1" FooterStyle-HorizontalAlign ="Center" HeaderStyle-VerticalAlign="Middle">
                     <ItemTemplate> <asp:Label ID="lbl1" runat="server" ></asp:Label> </ItemTemplate>

                    <FooterStyle HorizontalAlign="Center"></FooterStyle> <HeaderStyle VerticalAlign="Middle"></HeaderStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="L2" FooterStyle-HorizontalAlign ="Center" HeaderStyle-VerticalAlign="Middle">
                     <ItemTemplate> <asp:Label ID="lbl2" runat="server" ></asp:Label> </ItemTemplate>

            <FooterStyle HorizontalAlign="Center"></FooterStyle> <HeaderStyle VerticalAlign="Middle"></HeaderStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="L3" FooterStyle-HorizontalAlign ="Center" HeaderStyle-VerticalAlign="Middle">
                     <ItemTemplate> <asp:Label ID="lbl3" runat="server" ></asp:Label> </ItemTemplate>

            <FooterStyle HorizontalAlign="Center"></FooterStyle> <HeaderStyle VerticalAlign="Middle"></HeaderStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="L4" FooterStyle-HorizontalAlign ="Center" HeaderStyle-VerticalAlign="Middle">
                     <ItemTemplate> <asp:Label ID="lbl4" runat="server" ></asp:Label> </ItemTemplate>

            <FooterStyle HorizontalAlign="Center"></FooterStyle> <HeaderStyle VerticalAlign="Middle"></HeaderStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="L5" FooterStyle-HorizontalAlign ="Center" HeaderStyle-VerticalAlign="Middle">
                     <ItemTemplate> <asp:Label ID="lbl5" runat="server" ></asp:Label> </ItemTemplate>

            <FooterStyle HorizontalAlign="Center"></FooterStyle> <HeaderStyle VerticalAlign="Middle"></HeaderStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="L6" FooterStyle-HorizontalAlign ="Center" HeaderStyle-VerticalAlign="Middle">
                     <ItemTemplate> <asp:Label ID="lbl6" runat="server" ></asp:Label> </ItemTemplate>

            <FooterStyle HorizontalAlign="Center"></FooterStyle> <HeaderStyle VerticalAlign="Middle"></HeaderStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="L7" FooterStyle-HorizontalAlign ="Center" HeaderStyle-VerticalAlign="Middle">
                     <ItemTemplate> <asp:Label ID="lbl7" runat="server" ></asp:Label> </ItemTemplate>

            <FooterStyle HorizontalAlign="Center"></FooterStyle> <HeaderStyle VerticalAlign="Middle"></HeaderStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="L8" FooterStyle-HorizontalAlign ="Center" HeaderStyle-VerticalAlign="Middle">
                     <ItemTemplate> <asp:Label ID="lbl8" runat="server" ></asp:Label> </ItemTemplate>

            <FooterStyle HorizontalAlign="Center"></FooterStyle> <HeaderStyle VerticalAlign="Middle"></HeaderStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="L9" FooterStyle-HorizontalAlign ="Center" HeaderStyle-VerticalAlign="Middle">
                     <ItemTemplate> <asp:Label ID="lbl9" runat="server" ></asp:Label> </ItemTemplate>

            <FooterStyle HorizontalAlign="Center"></FooterStyle> <HeaderStyle VerticalAlign="Middle"></HeaderStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="L10" FooterStyle-HorizontalAlign ="Center" HeaderStyle-VerticalAlign="Middle">
                     <ItemTemplate> <asp:Label ID="lbl10" runat="server" ></asp:Label> </ItemTemplate>




<FooterStyle HorizontalAlign="Center"></FooterStyle>

<HeaderStyle VerticalAlign="Middle"></HeaderStyle>
                </asp:TemplateField>
            </Columns>
            </asp:GridView>
                    </div>
            </div>
         </div>
         
    </form>
</asp:Content>
