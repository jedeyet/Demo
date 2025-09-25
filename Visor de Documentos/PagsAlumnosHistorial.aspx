<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PagsAlumnosHistorial.aspx.cs" Inherits="Visor_de_Documentos.PagsAlumnosHistorial" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderC" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form1" runat="server">
        <div class="card mb-6 text-center">
                <div class="card-body">
                    <h5 class="card-title mb-3">Historial de cursos de estudiantes <asp:Label ID="lbNivel" runat="server" Text="Label" Visible="False"></asp:Label>
                    </h5>   <div class="text-start">   
        
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
        <asp:Label ID="Label2" runat="server" Text="Carnet del estudiante"></asp:Label>
        <asp:TextBox ID="txCarnet" runat="server" onkeydown = "return ((event.keyCode >= 48 && event.keyCode <= 57) || event.keyCode==8)" MaxLength="11" Width="155px"></asp:TextBox>
        &nbsp;<asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Ver  historial" class="btn btn-success mb-1" Width="113px"   />
        &nbsp; <asp:Button ID="btNuevo" runat="server" Text="Nuevo" Width="113px" class="btn btn-danger mb-1" OnClick="btNuevo_Click" Height="41px" />
       
        <br />
        <asp:Label ID="Label3" runat="server" Text="Parte del nombre del estudiante"></asp:Label>
        <asp:TextBox ID="txNombre" runat="server" MaxLength="20" Width="291px"></asp:TextBox>
        <asp:Button ID="btCoincidencias" runat="server" OnClick="btCoincidencias_Click" Text="Buscar Coincidencias" class="btn btn-success mb-1" Width="156px"  />
        <br />
        <asp:Label ID="Label4" runat="server" Text="Coincidencias"></asp:Label>
        &nbsp;<asp:DropDownList ID="ddNombre" runat="server" class ="btn btn-outline-success dropdown-toggle mb-1" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="true" AutoPostBack="True" OnSelectedIndexChanged="ddNombre_SelectedIndexChanged" Width="839px">
        </asp:DropDownList>
        <br />
        &nbsp;<br />
        <asp:Label ID="lbnombre" runat="server" CssClass="alert-inverse" BackColor="White" Font-Bold="True" ForeColor="#003300"></asp:Label>
        <br />
        <br />
          <asp:Button ID="BtActuales" runat="server" OnClick="BtActuales_Click"  Text="Actuales" class="btn btn-success mb-1" Width="110px" Font-Bold="True" CssClass="bg-green-dark" Visible="False"  />
          <asp:Button ID="BtTodos" runat="server" Text="Todos" OnClick="BtTodos_Click" class="btn btn-success mb-1" Width="110px" Font-Bold="True" CssClass="bg-green-dark" Visible="false" />
          <asp:Button ID="BtGanados" runat="server" OnClick="BtGanados_Click"  Text="Ganados" class="btn btn-success mb-1" Width="110px" Font-Bold="True" CssClass="bg-green-dark" Visible="false"  />
          <asp:Button ID="BtPerdidos" runat="server" OnClick="BtPerdidos_Click"  Text="Perdidos" class="btn btn-success mb-1" Width="110px" Font-Bold="True" CssClass="bg-green-dark"  Visible="false"  />
          <asp:Button ID="BtEquivalencias" runat="server" OnClick="BtEquivalencias_Click"  Text="Equivalencias" class="btn btn-success mb-1" Width="110px" Font-Bold="True" CssClass="bg-green-dark" Visible="false"  />
          <asp:Button ID="BtSuficiencias" runat="server" OnClick="BtSuficiencias_Click"  Text="Suficiencias" class="btn btn-success mb-1" Width="110px" Font-Bold="True" CssClass="bg-green-dark"  Visible="false" />  
        <br />
          <asp:Button ID="BtDirigido" runat="server" OnClick="BtDirigido_Click"  Text="Dirigidos" class="btn btn-success mb-1" Width="110px" Font-Bold="True" CssClass="bg-green-dark"  Visible="false" />
          <asp:Button ID="BtActualizacion" runat="server" OnClick="BtActualizacion_Click"  Text="Actualización" class="btn btn-success mb-1" Width="110px"  Font-Bold="True" CssClass="bg-green-dark" Visible="false" />
          <asp:Button ID="BtCierre" runat="server" OnClick="BtCierre_Click"  Text="Cierre" class="btn btn-success mb-1" Width="110px" Font-Bold="True" CssClass="bg-green-dark"  Visible="false" />
          <asp:Button ID="BtConvenio" runat="server" OnClick="BtConvenio_Click"  Text="Convenios" class="btn btn-success mb-1" Width="110px" Font-Bold="True" CssClass="bg-green-dark"  Visible="False" />
          <asp:Button ID="BtPrivados" runat="server" OnClick="BtPrivados_Click"  Text="Privados" class="btn btn-success mb-1" Width="110px" Font-Bold="True" CssClass="bg-green-dark"  Visible="false" />
          <asp:Button ID="BtGraduandos" runat="server" OnClick="BtGraduandos_Click"  Text="Título(s)" class="btn btn-success mb-1" Width="110px" Font-Bold="True" CssClass="bg-green-dark"  Visible="false" />
        <br />
        <asp:Label ID="lbResultado" runat="server" CssClass="alert-inverse" BackColor="White" Font-Bold="True" ForeColor="#003300"></asp:Label>
        <asp:GridView ID="GridCursos" runat="server" HorizontalAlign="Center" class="table table-striped" >
        
        </asp:GridView>
           
        &nbsp;&nbsp;
           
        <asp:Label ID="lbResultado2" runat="server" CssClass="alert-inverse" BackColor="White" Font-Bold="True" ForeColor="#003300"></asp:Label>
        <asp:GridView ID="GridCursos1" runat="server" HorizontalAlign="Center" class="table table-striped" AutoGenerateColumns="False"  >
            <Columns>
                <asp:TemplateField HeaderText="No.">
                    <ItemTemplate>
                        <asp:Label ID="Label22" runat="server" 
                            Text='<%# DataBinder.Eval(Container, "DataItem.No_curso") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Asignatura">
                    <ItemTemplate>
                        <asp:Label ID="Label23" runat="server" 
                            Text='<%# DataBinder.Eval(Container, "DataItem.Asignatura") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Créditos">
                    <ItemTemplate>
                        <asp:Label ID="Label24" runat="server" 
                            Text='<%# DataBinder.Eval(Container, "DataItem.Créditos") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Semestre">
                    <ItemTemplate>
                        <asp:Label ID="Label25" runat="server" 
                            Text='<%# DataBinder.Eval(Container, "DataItem.Semestre") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Punteo">
                    <ItemTemplate>
                        <asp:Label ID="Label26" runat="server" Text="Label">
                            Text='<%# DataBinder.Eval(Container, "DataItem.semestre") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Fecha">
                    <ItemTemplate>
                        <asp:Label ID="Label27" runat="server" Text="Label">
                            Text='<%# DataBinder.Eval(Container, "DataItem.semestre") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Tipo Nota">
                    <ItemTemplate>
                        <asp:Label ID="Label28" runat="server" Text="Label">
                            Text='<%# DataBinder.Eval(Container, "DataItem.semestre") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
           
                        <br />
        <asp:GridView ID="GridCursos2" runat="server" HorizontalAlign="Center" class="table table-striped" >
        
        </asp:GridView>
           
        <br />
                           </div>

         </div>
         </div>  
    </form>
</asp:Content>
