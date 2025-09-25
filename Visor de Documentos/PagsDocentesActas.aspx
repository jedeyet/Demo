<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PagsDocentesActas.aspx.cs" Inherits="Visor_de_Documentos.PagsDocentesActas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderC" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form111" runat="server">
      
         <div class="card mb-6 ">
      <div class="card-body">
             <h5 class="card-title mb-2">Datos del Docente</h5>                            
               <div class="text-start">
                <%--<div class="d-flex align-items-left flex-column mb-2">--%>
                    <br />
                        <asp:Label ID="Label1" runat="server" Text="Sede"></asp:Label>
                        <asp:DropDownList ID="DropDownList1" runat="server" class ="btn btn-outline-success dropdown-toggle mb-1" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" >
                            <asp:ListItem Value="1">Quetzaltenango</asp:ListItem>
                            <asp:ListItem Value="2">Guatemala</asp:ListItem>
                            <asp:ListItem Value="3">Cobán</asp:ListItem>
                            <asp:ListItem Value="4">Teologado</asp:ListItem>
                            <asp:ListItem Value="5">Izabal</asp:ListItem>
                        </asp:DropDownList>
                    <br />
                        <asp:Label ID="Label12" runat="server" Text="Label" Visible="False"></asp:Label>                        
                        <asp:Label ID="Label9" runat="server" Text="Año"></asp:Label>
                        <asp:DropDownList ID="ddAnio" runat="server" class ="btn btn-outline-success dropdown-toggle mb-1" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="true" >
                        </asp:DropDownList>
                        <asp:Label ID="Label10" runat="server" Text="Ciclo"></asp:Label>
                        <asp:DropDownList ID="ddSemestre" runat="server" class ="btn btn-outline-success dropdown-toggle mb-1" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="true" >
                        </asp:DropDownList>                        
                        <br />
                        <asp:Label ID="Label13" runat="server" Text="Introduzca código del docente"></asp:Label>
                        <asp:TextBox ID="TextBox2" runat="server" Width="209px" class="btn btn-outline-success dropdown-toggle mb-1"></asp:TextBox>
                        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Buscar por código" class="btn btn-dark mb-1" />                        
                        <br />
                        <asp:Label ID="Label2" runat="server" Text="Introduzca parte del nombre del docente"></asp:Label>                        
                        <asp:TextBox ID="TextBox1" runat="server" Width="209px" class="btn btn-outline-success dropdown-toggle mb-1"></asp:TextBox>                             
                        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Buscar Docentes" class="btn btn-dark mb-1" />                        
                        <br />
                        <asp:Label ID="lbResultado" runat="server"></asp:Label>                        
                        <asp:DropDownList ID="ListBox1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ListBox1_SelectedIndexChanged" class ="btn btn-outline-success dropdown-toggle mb-1" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="true" AppendDataBoundItems="True" >
                            <asp:ListItem>--- Seleccione al Catedrático ---</asp:ListItem>
                        </asp:DropDownList>                        
                        &nbsp;<asp:Button ID="btNuevo" runat="server" Text="Nuevo" Width="113px" class="btn btn-danger mb-1" OnClick="btNuevo_Click" Height="41px" />
       
                        <br />
                    <asp:Image ID="Image1" class="img-fluid rounded-xl" runat="server" />
                                  <div id='test'><asp:Label ID="Label5" runat="server"  Text="Datos del docente"></asp:Label>
                                      <br />    
                    <asp:GridView ID="GridView1" runat="server" HorizontalAlign="Center" class="table table-striped">
                     </asp:GridView>
                 </div>
            <%--</div>--%>
         </div>
    </div>
 </div>
    

    </form>
</asp:Content>
