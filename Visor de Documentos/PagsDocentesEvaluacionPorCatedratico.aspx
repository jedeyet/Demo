<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PagsDocentesEvaluacionPorCatedratico.aspx.cs" Inherits="Visor_de_Documentos.PagsDocentesEvaluacionPorCatedratico" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderC" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <form id="form278" runat="server">
         <div class="card mb-6 text-center">
               
             <div class="card-body">
                    <h5 class="card-title mb-3">Evaluaciones por docente (Todas)</h5>                            
                        <div class="text-start">   
        <br />
        <asp:Label ID="Label1" runat="server" Text="Sede"></asp:Label>
        <asp:DropDownList ID="DropDownList1" runat="server" class ="btn btn-outline-success dropdown-toggle mb-1" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="true" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" >
            <asp:ListItem Value="1">Quetzaltenango</asp:ListItem>
            <asp:ListItem Value="2">Guatemala</asp:ListItem>
            <asp:ListItem Value="3">Cobán</asp:ListItem>
            <asp:ListItem Value="4">Teologado</asp:ListItem>
            <asp:ListItem Value="5">Izabal</asp:ListItem>
        </asp:DropDownList>
        <br />
        <asp:Label ID="Label9" runat="server" Text="Año"></asp:Label>
        <asp:DropDownList ID="ddAnio" runat="server" class ="btn btn-outline-success dropdown-toggle mb-1" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="true" AutoPostBack="True" OnSelectedIndexChanged="ddAnio_SelectedIndexChanged">
        </asp:DropDownList>
        <asp:Label ID="Label10" runat="server" Text="Ciclo"></asp:Label>
        <asp:DropDownList ID="ddSemestre" runat="server" class ="btn btn-outline-success dropdown-toggle mb-1" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="true" AutoPostBack="True" OnSelectedIndexChanged="ddSemestre_SelectedIndexChanged">
        </asp:DropDownList>
        <br />
        &nbsp;
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Ver docentes evaluados" class="btn btn-success mb-1" Font-Bold="True" ForeColor="White" />
        <br />
        <br />
        <asp:Label ID="Label6" runat="server" Text="Docente" Visible="False"></asp:Label>
        &nbsp;<asp:DropDownList ID="ddDocente" runat="server" class ="btn btn-outline-success dropdown-toggle mb-1" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="true" Width="500" Visible="False" OnSelectedIndexChanged="ddDocente_SelectedIndexChanged">
        </asp:DropDownList>
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Ver" class="btn btn-success mb-1" Visible="False"   />
        &nbsp;<asp:Button ID="btNuevo" runat="server" Text="Nuevo" Width="113px" class="btn btn-danger mb-1" OnClick="btNuevo_Click" Height="41px" />
       
        <br />
            <asp:Label ID="lbResultado" runat="server" Font-Bold="True" ForeColor="Blue"></asp:Label>
        <br />
        &nbsp;<br />
        <asp:Label ID="Label8" runat="server" Text="Número de alumnos que evaluaron " Visible="False"></asp:Label>
        <asp:Label ID="lbConteo" runat="server" Visible="False"></asp:Label>
        <br />
        <asp:Label ID="Label7" runat="server" Text="Docente" Visible="False"></asp:Label>
        &nbsp;<asp:Label ID="lbDocente" runat="server" Visible="False"></asp:Label>
        <br />




        <asp:GridView ID="gridDominio" runat="server"  AutoGenerateColumns="False" HorizontalAlign="Center" class="table table-striped" Visible="False" >
            <Columns>
                    <asp:TemplateField HeaderText="No. Dominio">
                        <ItemTemplate>
                            <asp:Label ID="lbIdPreg1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID_Dominio") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Dominio">
                        <ItemTemplate>
                            <asp:Label ID="lgPreg1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Dominio") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Promedio">
                        <ItemTemplate>
                            <asp:Label ID="lbProm1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Promedio")%>' DataFormatString="{0:F2}"></asp:Label>
                        </ItemTemplate>
                        
                    </asp:TemplateField>
                </Columns>
        </asp:GridView>
        <asp:Chart ID="Chart1" runat="server" Visible="False">
            <Series>
                <asp:Series Name="Series1">
                </asp:Series>
            </Series>
            <ChartAreas>
                <asp:ChartArea Name="ChartArea1">
                </asp:ChartArea>
            </ChartAreas>
        </asp:Chart>
        <br />
        <asp:GridView ID="GridPreguntas" runat="server"  AutoGenerateColumns="False" HorizontalAlign="Center" class="table table-striped" Visible="False" >
            <Columns>
                <asp:TemplateField HeaderText="No.">
                        <ItemTemplate>
                            <asp:Label ID="lbIdPreg" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.id_pregunta") %>'></asp:Label>
                        </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Pregunta">
                        <ItemTemplate>
                            <asp:Label ID="lgPreg" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.pregunta") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Promedio">
                        <ItemTemplate>
                            <asp:Label ID="lbProm" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.promedio") %>'></asp:Label>
                        </ItemTemplate>
                       
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Conteo" Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="lbConteo0" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.conteo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
        </asp:GridView>
        <br />




        <asp:GridView ID="GridComentarios" runat="server"  AutoGenerateColumns="False" HorizontalAlign="Center" class="table table-striped" Visible="False" >
            <Columns>
                    <asp:TemplateField HeaderText="Positivos">
                        <ItemTemplate>
                            <asp:Label ID="lbPos0" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Positivos") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle BackColor="White" Font-Bold="True" ForeColor="Black" />
                        <ItemStyle BackColor="White" Font-Bold="True" ForeColor="Black" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Por Mejorar">
                        <ItemTemplate>
                            <asp:Label ID="lbNeg0" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Negativos") %>'></asp:Label>
                        </ItemTemplate>
                        <ControlStyle Font-Bold="True" />
                        <FooterStyle BackColor="White" Font-Bold="True" ForeColor="Red" />
                        <HeaderStyle BackColor="White" Font-Bold="True" ForeColor="Red" />
                        <ItemStyle BackColor="White" Font-Bold="True" ForeColor="Red" />
                    </asp:TemplateField>
                </Columns>
        </asp:GridView>
          <asp:ListBox ID="ListBox1" runat="server" Visible="False"></asp:ListBox>
        <br />
                            </div></div></div>
    </form>
</asp:Content>
