<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AlumnosIndividual.aspx.cs" Inherits="Visor_de_Documentos.AlumnosIndividual" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderC" runat="server">    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%--<link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="Scripts/bootstrap.bundle.min.js"></script>--%>
    <form id="form2111" runat="server">
        <div class="card mb-6 text-center">
                <div class="card-body">
                    <h5 class="card-title mb-3">Búsqueda de Alumnos por sede<asp:Label ID="lbNivel" runat="server" Text="Label"></asp:Label>
                    </h5>   
                    <div class="text-start">   
                        <div class="d-flex align-items-center flex-column mb-2">        
                            <asp:Label ID="Label1" runat="server" Text="Sede"></asp:Label>&nbsp;
                            <asp:DropDownList ID="DropDownList1" runat="server" class ="btn btn-outline-success dropdown-toggle mb-1" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" >
                                <asp:ListItem Value="1">Quetzaltenango</asp:ListItem>
                                <asp:ListItem Value="2">Guatemala</asp:ListItem>
                                <asp:ListItem Value="3">Cobán</asp:ListItem>
                                <asp:ListItem Value="4">Teologado</asp:ListItem>
                                <asp:ListItem Value="5">Izabal</asp:ListItem>
                            </asp:DropDownList>
                            <asp:Label ID="Label12" runat="server" Text="Label"></asp:Label>        
                            <br />
                            <asp:Label ID="Label13" runat="server" Text="Si conoce el número de carnet, introduzcalo aquí"></asp:Label>
                            <asp:TextBox ID="TextBox2" runat="server" AutoPostBack="True" Width="173px" MaxLength="10" class="btn btn-outline-success dropdown-toggle mb-1" ></asp:TextBox>
                            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Buscar" class="btn btn-dark mb-1"/>        
                            <br />
                            <asp:Label ID="Label2" runat="server" Text="Si no conoce el No. de carné, introduzca una parte del nombre del estudiante"></asp:Label>        
                            <asp:TextBox ID="TextBox1" runat="server" AutoPostBack="True" Width="209px" class="btn btn-outline-success dropdown-toggle mb-1" ></asp:TextBox>
                            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Buscar" class="btn btn-success mb-1" style="left: 0px; top: 0px; width: 165px" />                            
                            <br />
                            <asp:Label ID="lbResultado" runat="server"></asp:Label>                            
                            <asp:ListBox ID="ListBox1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ListBox1_SelectedIndexChanged" class ="btn btn-outline-success dropdown-toggle mb-1" Visible="False" Width="533px"></asp:ListBox>                            
                           
                            <asp:Button ID="btNuevo" runat="server" Text="Nuevo" Width="113px" class="btn btn-danger mb-1" OnClick="btNuevo_Click" Height="36px" />
                            <%--<asp:Button ID="btnAbrirModal" runat="server" Text="Mostrar Modal" OnClick="btnAbrirModal_Click" CssClass="btn btn-warning" />--%>
                    </div>
                 </div>
        </div>
    </div>
        <script>
            var myApp = new function () {
                this.printDiv = function () {

                    var div = document.getElementById('printableArea');
                    var win = window.open('', '', 'height=700,width=700');
                    win.document.write(div.outerHTML);
                    win.document.close();
                    win.print();
                    win.close();
                    return true;
                }
            }
        </script>
    <div id="printableArea">
    <div class="row">       
        <div class="col-xl-12 mb-5">
             <h4><asp:Label ID="Label25" runat="server" class="small-title" Text="Información General"></asp:Label></h4>
            <button class="btn btn-icon btn-icon-only btn-foreground-alternate shadow datatable-print" onclick='myApp.printDiv()' data-bs-delay="0" data-bs-toggle="tooltip" data-bs-placement="top" title="Print" type="button">
                <i data-acorn-icon="print"></i>
            </button>
            <div class="card h-100-card">
                 <div class="card-body">
                     <div class="d-flex align-items-center flex-column mb-4">
                        <div class="d-flex align-items-center flex-column">
                              <div class="sw-13 position-relative mb-3">                                
                                 <asp:Image ID="Image1" class="img-fluid rounded-xl" runat="server" />
                                 <div id='test'></div>
                              </div>      
                         </div>
                        <div>                                  
                             <div><asp:Label ID="Label11" runat="server" Text="Carnet: " ></asp:Label>
                                   <asp:Label ID="lbCarnet" runat="server" Text="Label" class="fw-bold"></asp:Label> </div>
                             <div><asp:Label ID="Label7" runat="server" Text="Nombre: " ></asp:Label>
                                  <asp:Label ID="lbNombre" runat="server" Text="Label" class="fw-bold" ></asp:Label></div>
                             <div><asp:Label ID="Label4" runat="server" Text="CUI: " ></asp:Label>
                                 <asp:Label ID="lbCUI" runat="server" Text="Label" class="fw-bold"></asp:Label></div>
                             <div><asp:Label ID="Label5" runat="server" Text="Dirección: " ></asp:Label>
                                  <asp:Label ID="lbDireccion" runat="server" Text="Label" class="fw-bold"></asp:Label></div>
                             <div><asp:Label ID="Label6" runat="server" Text="Teléfono: "  ></asp:Label>
                                 <asp:Label ID="lbTel" runat="server" Text="Label: " class="fw-bold" ></asp:Label></div>
                             <div><asp:Label ID="Label8" runat="server" Text="Email: "  ></asp:Label>
                                  <asp:Label ID="lbEmail" runat="server" Text="Label" class="fw-bold" ></asp:Label></div>
                             <div><asp:Label ID="Label9" runat="server" Text="País: " ></asp:Label>
                                  <asp:Label ID="lbPais" runat="server" Text="Label"  class="fw-bold"></asp:Label></div>
                             <div><asp:Label ID="Label10" runat="server" Text="Estado Civil: "  ></asp:Label>
                                  <asp:Label ID="lbEst" runat="server" Text="Label" class="fw-bold"></asp:Label></div>
                             <div><asp:Label ID="Label14" runat="server" Text="Nacimiento: "  ></asp:Label>
                                  <asp:Label ID="lbNac" runat="server" Text="Label" class="fw-bold" ></asp:Label></div>
                             <div><asp:Label ID="Label15" runat="server" Text="Municipio: "  ></asp:Label>
                                  <asp:Label ID="lbMunicipio" runat="server" Text="Label" class="fw-bold" ></asp:Label></div>
                             <div><asp:Label ID="Label16" runat="server" Text="Departamento: " ></asp:Label>
                                  <asp:Label ID="lbDepartamento" runat="server" Text="Label" class="fw-bold" ></asp:Label></div>
                             <div><asp:Label ID="Label17" runat="server" Text="Comunidad: " ></asp:Label>
                                  <asp:Label ID="lbComunidad" runat="server" Text="Label" class="fw-bold" ></asp:Label></div>
                             <div><asp:Label ID="Label18" runat="server" Text="Pensum: " ></asp:Label>
                                  <asp:Label ID="lbPensum" runat="server" Text="Label" class="fw-bold" ></asp:Label></div>
                             <div><asp:Label ID="Label19" runat="server" Text="Contacto 1: " ></asp:Label>
                                 <asp:Label ID="lbCon1" runat="server" Text="Label" class="fw-bold" ></asp:Label></div>
                             <div><asp:Label ID="Label20" runat="server" Text="Contacto 2: " ></asp:Label>
                                  <asp:Label ID="lbCon2" runat="server" Text="Label" class="fw-bold" ></asp:Label></div>
                             <div><asp:Label ID="Label21" runat="server" Text="Problemas médicos (alergias u otros): " ></asp:Label>
                                  <asp:Label ID="lbMedicos" runat="server" Text="Label" class="fw-bold" ></asp:Label></div>
                             <div><asp:Label ID="Label22" runat="server" Text="Tratamiento: " ></asp:Label>
                                  <asp:Label ID="lbTratamiento" runat="server" Text="Label" class="fw-bold"></asp:Label></div>                            
                        </div>                         
                </div>
            </div>
        </div>
      </div>
   </div>
 </div> 
        
<%--<div class="modal fade" id="myModal" tabindex="-1" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="myModalLabel">Título del Popup</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        Aquí puedes colocar el contenido de tu popup.
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                        <button type="button" class="btn btn-primary">Guardar cambios</button>
                    </div>
                </div>
            </div>
    </div>--%>

</form>
</asp:Content>
