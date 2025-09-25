<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PagsPagos.aspx.cs" Inherits="Visor_de_Documentos.PagsPagos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderC" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form1" runat="server">
         <div class="card mb-6 text-center">
                <div class="card-body">
                    <h5 class="card-title mb-3">Pagos realizados por estudiantes (por sede) <asp:Label ID="lbNivel" runat="server" Text="Label" Visible="False"></asp:Label>
                    </h5>   <div class="text-start">   

        
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
    <asp:TextBox ID="txCarnet" runat="server" MaxLength="11" Width="155px"></asp:TextBox>
        <br />
        <asp:Label ID="Label3" runat="server" Text="Parte del nombre del estudiante"></asp:Label>
    <asp:TextBox ID="txNombre" runat="server" MaxLength="20" Width="291px"></asp:TextBox>
        <asp:Button ID="btCoincidencias" runat="server" OnClick="btCoincidencias_Click" Text="Buscar Coincidencias" class="btn btn-success mb-1" Width="156px"  />
        <br />
        <asp:Label ID="Label4" runat="server" Text="Coincidencias" Visible="False"></asp:Label>
        &nbsp;<asp:DropDownList ID="ddNombre" runat="server" class ="btn btn-outline-success dropdown-toggle mb-1" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="true" AutoPostBack="True" OnSelectedIndexChanged="ddNombre_SelectedIndexChanged" Width="834px" AppendDataBoundItems="True" Visible="False">
        </asp:DropDownList>
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Ver pagos"  Width="113px" class="btn btn-success mb-1" />
        &nbsp;<asp:ImageButton ID="imgbutExc" runat="server"  ImageUrl="~/Imagenes/excel2.jpg" OnClick="imgbutExc_Click" ToolTip="Traslada a Excel los resultados del filtro" Visible="False" class ="btn btn-icon btn-icon-end btn-tertiary mb-1"/>                        
        &nbsp;
       
        <asp:Button ID="btNuevo" runat="server" Text="Nuevo" Width="113px" class="btn btn-danger mb-1" OnClick="btNuevo_Click" Height="36px" />
       
        <br />
        <asp:Label ID="lbnombre" runat="server" Font-Bold="True" ForeColor="#003300"></asp:Label>
        <br />




       
        <asp:GridView ID="gridEstado" runat="server" HorizontalAlign="Center" class="table table-striped" >
        </asp:GridView>
        <br />
        <asp:Label ID="lbConvenio" runat="server" Font-Bold="True" ForeColor="#003300"></asp:Label>
        <asp:GridView ID="GridConvenio" runat="server" HorizontalAlign="Center" class="table table-striped" >
        </asp:GridView>
        <br />
        <asp:Label ID="lbConvenio0" runat="server" CssClass="bg-green-dark"  Visible ="False">Suma de cuotas: </asp:Label>
        &nbsp;&nbsp;
        <asp:Label ID="lbMonto1" runat="server" CssClass="bg-green-dark" Visible="False">0.00</asp:Label>
        <br />
        <asp:Label ID="lbConvenio1" runat="server" CssClass="bg-green-dark" Visible="False">Suma de convenios:</asp:Label>
        &nbsp;<asp:Label ID="lbMonto2" runat="server" CssClass="bg-green-dark" Visible="False">0.00</asp:Label>
        <br />
        <asp:Label ID="lbConvenio2" runat="server" CssClass="bg-green-dark" Visible="False">total de la Deuda: </asp:Label>
        &nbsp;<asp:Label ID="lbTotal" runat="server" CssClass="bg-green-dark" Visible="False">0.00</asp:Label>
        <br />
        <br />
        <asp:Label ID="lbResultado" runat="server" Font-Bold="True" ForeColor="#003300"></asp:Label>
        <asp:GridView ID="GridView1" runat="server" HorizontalAlign="Center" class="table table-striped" >
        </asp:GridView>
                        </div>

         </div>
         </div>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
    </form>
</asp:Content>
