<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PagsDocentesReporteDetallado.aspx.cs" Inherits="Visor_de_Documentos.PagsDocentesReporteDetallado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderC" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form1" runat="server">
        
         <div class="card mb-6 ">
                <div class="card-body">  <h5 class="card-title mb-3">Reporte detallado de docentes</h5>
                       <div class="text-start">   
        <br />

        <asp:Label ID="Label5" runat="server" Text="" Font-Bold="True"></asp:Label>
        <br />
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
        <br />
        <asp:Label ID="Label2" runat="server" Text="Año"></asp:Label>
        <asp:DropDownList ID="ddAnio" runat="server" class ="btn btn-outline-success dropdown-toggle mb-1" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="true">
        </asp:DropDownList>
        &nbsp;
        <asp:Label ID="Label3" runat="server" Text="Ciclo"></asp:Label>
        <asp:DropDownList ID="ddSemestre" runat="server" class ="btn btn-outline-success dropdown-toggle mb-1" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="true">
        </asp:DropDownList>
        <br />
        <br />
        &nbsp;&nbsp;
        <asp:RadioButtonList ID="RadioButtonList1" runat="server" class ="btn btn-outline-success dropdown-toggle mb-1" AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" RepeatDirection="Horizontal">
            <asp:ListItem Selected="True">Facultad</asp:ListItem>
            <asp:ListItem>Carrera</asp:ListItem>
        </asp:RadioButtonList>
        <br />
        <br />
        <asp:Label ID="Label4" runat="server" Text="Facultad"></asp:Label>
        <asp:DropDownList ID="ddFacultad" runat="server" class ="btn btn-outline-success dropdown-toggle mb-1" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="true" OnSelectedIndexChanged="ddFacultad_SelectedIndexChanged">
            <asp:ListItem Value="1">Quetzaltenango</asp:ListItem>
            <asp:ListItem Value="2">Guatemala</asp:ListItem>
            <asp:ListItem Value="3">Cobán</asp:ListItem>
            <asp:ListItem Value="4">Teologado</asp:ListItem>
            <asp:ListItem Value="5">Izabal</asp:ListItem>
        </asp:DropDownList>
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label6" runat="server" Text="Carrera" Visible="False"></asp:Label>
        &nbsp;
        <asp:DropDownList ID="ddCarrera" runat="server" class ="btn btn-outline-success dropdown-toggle mb-1" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="true" OnSelectedIndexChanged="ddFacultad_SelectedIndexChanged" Visible="False">
            <asp:ListItem Value="1">Quetzaltenango</asp:ListItem>
            <asp:ListItem Value="2">Guatemala</asp:ListItem>
            <asp:ListItem Value="3">Cobán</asp:ListItem>
            <asp:ListItem Value="4">Teologado</asp:ListItem>
            <asp:ListItem Value="5">Izabal</asp:ListItem>
        </asp:DropDownList>
        &nbsp;<asp:Button ID="btNuevo" runat="server" Text="Nuevo" Width="113px" class="btn btn-danger mb-1" OnClick="btNuevo_Click" Height="41px" />
       
        <br />
        <br />
        
        <div class="container">
            <div class ="row">
                        <asp:Label ID="Label7" runat="server" Text="Seleccione los campos que desea incluya el reporte" Font-Bold="True"></asp:Label>
            </div>
            <div class ="row">
                        <asp:Label ID="Label8" runat="server" Text="Campos personales" Font-Bold="True"></asp:Label>
            </div>
              
            <div class="border m-4 d-flex justify-content-left" ">
                    
                  <div class ="row">
                        <div class ="col-md-3">
                            <asp:CheckBox ID="chDPI" runat="server"  Text="DPI" class ="btn btn-outline-success dropdown-toggle mb-1" />
                        </div>
                      <div class ="col-md-3">
                           <asp:CheckBox ID="chCel" runat="server"  Text="Celular" class ="btn btn-outline-success dropdown-toggle mb-1"/> 
                        </div>
                      <div class ="col-md-3">
                            <asp:CheckBox ID="chema" runat="server"  Text="email" class ="btn btn-outline-success dropdown-toggle mb-1"/>
                        </div>
                      <div class ="col-md-3">
                            <asp:CheckBox ID="chpai" runat="server"  Text="Pais" class ="btn btn-outline-success dropdown-toggle mb-1"/>
                        </div>
                      <div class ="col-md-3">
                            <asp:CheckBox ID="chnac" runat="server"  Text="Nacimiento" class ="btn btn-outline-success dropdown-toggle mb-1"/>
                        </div>
                      <div class ="col-md-3">
                            <asp:CheckBox ID="chnit" runat="server"  Text="NIT" class ="btn btn-outline-success dropdown-toggle mb-1"/>
                        </div>
                      <div class ="col-md-3">
                             <asp:CheckBox ID="chemi" runat="server"  Text="email Institucional" class ="btn btn-outline-success dropdown-toggle mb-1"/>
                        </div>

                    </div>
                </div>
        </div>
            
        <div class ="row">
                        <asp:Label ID="Label9" runat="server" Text="Campos Laborales" Font-Bold="True"></asp:Label>
            </div>

            <div class="border m-4 d-flex justify-content-left" ">
                    
                  <div class ="row">
                        
                      <div class ="col-md-3">
                           <asp:CheckBox ID="chemt" runat="server" Text="email" class ="btn btn-outline-success dropdown-toggle mb-1"/> 
                        </div>
                      <div class ="col-md-3">
                            <asp:CheckBox ID="chtel" runat="server"  Text="Teléfono" class ="btn btn-outline-success dropdown-toggle mb-1"/>
                        </div>
                      <div class ="col-md-3">
                            <asp:CheckBox ID="chdir" runat="server"  Text="Dirección" class ="btn btn-outline-success dropdown-toggle mb-1"/>
                        </div>
                      <div class ="col-md-3">
                            <asp:CheckBox ID="chemp" runat="server"  Text="Empresa" class ="btn btn-outline-success dropdown-toggle mb-1"/>
                        </div>
                      <div class ="col-md-3">
                             <asp:CheckBox ID="chigss" runat="server"  Text="IGSS" class ="btn btn-outline-success dropdown-toggle mb-1"/>
                        </div>
                      <div class ="col-md-3">
                             <asp:CheckBox ID="chcol" runat="server"  Text="Colegiado" class ="btn btn-outline-success dropdown-toggle mb-1"/>
                        </div>
                      <div class ="col-md-3">
                            <asp:CheckBox ID="chven" runat="server"  Text="Vencimiento" class ="btn btn-outline-success dropdown-toggle mb-1"/>
                        </div>
                      

                    </div>
                </div>
      
        
             
        <br />
        <br />
        &nbsp;<asp:TextBox ID="TextBox1" runat="server" Width="1037px" Visible="False"></asp:TextBox>
        <br />
        <asp:Button ID="Button1" runat="server" class="btn btn-success mb-1" OnClick="Button1_Click" Text="Ver Listado" />
        <br />
        <br />
        <asp:GridView ID="GridView1" runat="server" HorizontalAlign="Center" class="table table-striped" >
        </asp:GridView>
        <br />
        <asp:ImageButton ID="imgbutExc" runat="server"  ImageUrl="~/Imagenes/excel2.jpg" OnClick="imgbutExc_Click" ToolTip="Traslada a Excel los resultados del filtro" Visible="False" class ="btn btn-icon btn-icon-end btn-tertiary mb-1"/>                        
        <br />
        <asp:Label ID="lbResultado" runat="server"></asp:Label>
                          </div>
                </div>
         </div>
        
    </form>
</asp:Content>

