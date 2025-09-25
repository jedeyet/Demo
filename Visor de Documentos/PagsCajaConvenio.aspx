<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PagsCajaConvenio.aspx.cs" Inherits="Visor_de_Documentos.PagsCajaConvenio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderC" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form1" runat="server">
            <div class="card mb-6 text-center">
                <div class="card-body">
                    <h5 class="card-title mb-3">Convenios de pago</h5>                            
                        <div class="text-start">                          
                            <label for="DropDownList1" class="form-label">Sede</label>                                                
                            
                            <asp:DropDownList ID="DropDownList1" runat="server" class ="btn btn-outline-success dropdown-toggle mb-1" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="true" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                                <asp:ListItem Value="1">Quetzaltenango</asp:ListItem>
                                <asp:ListItem Value="2">Guatemala</asp:ListItem>
                                <asp:ListItem Value="3">Cobán</asp:ListItem>
                                <asp:ListItem Value="4">Teologado</asp:ListItem>
                                <asp:ListItem Value="5">Izabal</asp:ListItem>
                             </asp:DropDownList>                             
                            <br />
                            <asp:Label ID="Label2" runat="server" Text="Tipo de busqueda"></asp:Label>
                            <asp:DropDownList ID="ddTipo" runat="server" class ="btn btn-outline-success dropdown-toggle mb-1" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="true" AutoPostBack="True" OnSelectedIndexChanged="ddTipo_SelectedIndexChanged">
                            </asp:DropDownList>                        
                            <br />
                            <asp:Label ID="Label3" runat="server" Text="Estado"></asp:Label>
                            <asp:DropDownList ID="ddEstado" runat="server" class ="btn btn-outline-success dropdown-toggle mb-1" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="true">
                            </asp:DropDownList>                        
                            <br />
                             
                            
                            <div>
 
  
</div>

        <asp:Label ID="Label5" runat="server" Text="Carnet del estudiante"></asp:Label>
    <asp:TextBox ID="txCarnet" runat="server" CssClass="form-control" MaxLength="11" Width="155px" Enabled="False"></asp:TextBox>
                            <br />
                            <br />
                            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Ver reporte" class="btn btn-success mb-1" Width="249px"  />                         
                            <br />
                            <br />
                            <asp:Panel runat="server" DefaultButton="Button2">
                            <asp:Label ID="Label1" runat="server" Text="Introduzca parte del nombre del estudiante"></asp:Label>                        
                        <asp:TextBox ID="TextBox1" runat="server" Width="209px" class="btn btn-outline-success dropdown-toggle mb-1" onkeypress="return EnterEvent(event)"></asp:TextBox>                             
                        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Buscar Estudiante" class="btn btn-dark mb-1" />
                                </asp:Panel>
                            <asp:Label ID="Label4" runat="server"></asp:Label>                        
                        <asp:DropDownList ID="ListBox1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ListBox1_SelectedIndexChanged" class ="btn btn-outline-success dropdown-toggle mb-1" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="true" AppendDataBoundItems="True" >
                            <asp:ListItem>--- Seleccione al Estudiante ---</asp:ListItem>
                        </asp:DropDownList>                        
    
                            <br />
                            <br />
                            <asp:ImageButton ID="imgbutExc" runat="server"  ImageUrl="~/Imagenes/excel2.jpg" OnClick="imgbutExc_Click" ToolTip="Traslada a Excel los resultados del filtro" Visible="False" class ="btn btn-icon btn-icon-end btn-tertiary mb-1"/>                        
                            &nbsp;<asp:Button ID="btNuevo" runat="server" Text="Nuevo" Width="113px" class="btn btn-danger mb-1" OnClick="btNuevo_Click" Height="41px" />
       
                            <br />
                            <asp:Label ID="lbResultado" runat="server"></asp:Label>
                        </div>            
             </div>
      </div>

    <div class="data-table-responsive-wrapper">        
    <asp:GridView ID="GridView1" runat="server"  HorizontalAlign="Center" class="table table-striped table-sm">
    </asp:GridView>
        </div>
           
    </form>
</asp:Content>
