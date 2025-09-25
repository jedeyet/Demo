<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PagsAlumnosReporteDetallado.aspx.cs" Inherits="Visor_de_Documentos.PagsAlumnosReporteDetallado" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderC" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form1" runat="server">
         <div class="card mb-6 text-center">
                <div class="card-body">
                    <h5 class="card-title mb-3">Reporte detallado de docentes <asp:Label ID="lbNivel" runat="server" Text="Label" Visible="False"></asp:Label>
                    </h5>   <div class="text-start">   
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
        <asp:DropDownList ID="ddAnio" runat="server" class ="btn btn-outline-success dropdown-toggle mb-1" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="true" >
        </asp:DropDownList>
        &nbsp;
        <asp:Label ID="Label3" runat="server" Text="Ciclo"></asp:Label>
        <asp:DropDownList ID="ddSemestre" runat="server" class ="btn btn-outline-success dropdown-toggle mb-1" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="true">
        </asp:DropDownList>
        <br />
        <br />
        &nbsp;&nbsp;
        <asp:RadioButtonList ID="RadioButtonList1" runat="server" class ="btn btn-outline-success  mb-1" AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" RepeatDirection="Horizontal">
            <asp:ListItem>Facultad</asp:ListItem>
            <asp:ListItem Selected="True">Carrera</asp:ListItem>
        </asp:RadioButtonList>
        <br />
        <br />
        <asp:Label ID="Label4" runat="server" Text="Facultad" Visible="False"></asp:Label>
        <asp:DropDownList ID="ddFacultad" runat="server" class ="btn btn-outline-success dropdown-toggle mb-1" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="true" OnSelectedIndexChanged="ddFacultad_SelectedIndexChanged" Visible="False">
            <asp:ListItem Value="1">Quetzaltenango</asp:ListItem>
            <asp:ListItem Value="2">Guatemala</asp:ListItem>
            <asp:ListItem Value="3">Cobán</asp:ListItem>
            <asp:ListItem Value="4">Teologado</asp:ListItem>
            <asp:ListItem Value="5">Izabal</asp:ListItem>
        </asp:DropDownList>
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label6" runat="server" Text="Carrera"></asp:Label>
        &nbsp;
        <asp:DropDownList ID="ddCarrera" runat="server" class ="btn btn-outline-success dropdown-toggle mb-1" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="true" OnSelectedIndexChanged="ddFacultad_SelectedIndexChanged">
            <asp:ListItem Value="1">Quetzaltenango</asp:ListItem>
            <asp:ListItem Value="2">Guatemala</asp:ListItem>
            <asp:ListItem Value="3">Cobán</asp:ListItem>
            <asp:ListItem Value="4">Teologado</asp:ListItem>
            <asp:ListItem Value="5">Izabal</asp:ListItem>
        </asp:DropDownList>
        &nbsp;<asp:Button ID="btNuevo" runat="server" Text="Nuevo" Width="113px" class="btn btn-danger mb-1" OnClick="btNuevo_Click" Height="36px" />
       
        <br />
        <br />
        
       
            <div class ="row">
                        <asp:Label ID="Label7" runat="server" Text="Seleccione los campos que desea incluya el reporte" Font-Bold="True"></asp:Label>
            </div>
            <div class ="row">
                        <asp:Label ID="Label8" runat="server" Text="Campos personales y académicos" Font-Bold="True"></asp:Label>
            </div>
              
            <div class="border m-4 d-flex justify-content-left" ">
                    
                  <div class ="row">
                        <div class ="col-md-3">
                            <asp:CheckBox ID="chDPI" runat="server"  Text="DPI" class ="btn btn-outline-success dropdown-toggle mb-1" />
                        </div>
                      <div class ="col-md-3">
                            <asp:CheckBox ID="Chnaci" runat="server"  Text="Nacimiento" class ="btn btn-outline-success dropdown-toggle mb-1" />
                        </div>
                      <div class ="col-md-3">
                           <asp:CheckBox ID="chDir" runat="server"  Text="Dirección" class ="btn btn-outline-success dropdown-toggle mb-1"/> 
                        </div>
                      <div class ="col-md-3">
                            <asp:CheckBox ID="chtel" runat="server"  Text="Teléfono" class ="btn btn-outline-success dropdown-toggle mb-1"/>
                        </div>
                      <div class ="col-md-3">
                            <asp:CheckBox ID="chema" runat="server"  Text="e-mail" class ="btn btn-outline-success dropdown-toggle mb-1"/>
                        </div>
                      <div class ="col-md-3">
                            <asp:CheckBox ID="chnac" runat="server"  Text="Nacionalidad" class ="btn btn-outline-success dropdown-toggle mb-1"/>
                        </div>
                      <div class ="col-md-3">
                            <asp:CheckBox ID="chusu" runat="server"  Text="Usuario" class ="btn btn-outline-success dropdown-toggle mb-1"/>
                        </div>
                      <div class ="col-md-3">
                             <asp:CheckBox ID="chpen" runat="server"  Text="Pensum" class ="btn btn-outline-success dropdown-toggle mb-1"/>
                        </div>
                      <div class ="col-md-3">
                             <asp:CheckBox ID="Chjor" runat="server"  Text="Jornada" class ="btn btn-outline-success dropdown-toggle mb-1"/>
                        </div>

                    </div>
                </div>
         
            <div class ="row">
            <asp:Label ID="Label9" runat="server" Text="Localización " Font-Bold="True"></asp:Label>
            </div>

            <div class="border m-4 d-flex justify-content-left" ">
                <div class ="row">
                     <div class ="col-md-4">
                      <asp:CheckBox ID="chdep" runat="server" Text="Departamento" class ="btn btn-outline-success dropdown-toggle mb-1"/> 
                     </div>
                      <div class ="col-md-4">
                            <asp:CheckBox ID="chmun" runat="server"  Text="Municipio  " class ="btn btn-outline-success dropdown-toggle mb-1"/>
                      </div>
                      <div class ="col-md-4">
                            <asp:CheckBox ID="chcom" runat="server"  Text="Comunidad  " class ="btn btn-outline-success dropdown-toggle mb-1"/>
                      </div>
                </div>
            </div>


   
            <div class ="row">
            <asp:Label ID="Label10" runat="server" Text="Contactos de emergencia, enfermedades y medicamentos" Font-Bold="True"></asp:Label>
            </div>

            <div class="border m-4 d-flex justify-content-left" ">
                <div class ="row">
                      <div class ="col-md-3">
                            <asp:CheckBox ID="chcon" runat="server"  Text="Contacto" class ="btn btn-outline-success dropdown-toggle mb-1"/>
                        </div>
                      <div class ="col-md-3">
                             <asp:CheckBox ID="chtelc" runat="server"  Text="Tel. Contacto" class ="btn btn-outline-success dropdown-toggle mb-1"/>
                        </div>
                      <div class ="col-md-3">
                             <asp:CheckBox ID="chenf" runat="server"  Text="Enfermedades" class ="btn btn-outline-success dropdown-toggle mb-1"/>
                        </div>
                      <div class ="col-md-3">
                            <asp:CheckBox ID="chmed" runat="server"  Text="Medicamentos" class ="btn btn-outline-success dropdown-toggle mb-1"/>
                        </div>
                      
                </div>
            </div> 
      
        <br />
        <asp:Button ID="Button1" runat="server" class="btn btn-success mb-1" OnClick="Button1_Click" Text="Ver Listado" />
          &nbsp;<asp:TextBox ID="TextBox1" runat="server" Width="1037px" Visible="false"></asp:TextBox>
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
