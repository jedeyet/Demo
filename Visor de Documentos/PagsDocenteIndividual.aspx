<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PagsDocenteIndividual.aspx.cs" Inherits="Visor_de_Documentos.PagsDocenteIndividual" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderC" runat="server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form111" runat="server">
      <div class="card mb-6 text-center">
      <div class="card-body">
             <h5 class="card-title mb-2">Datos del Docente</h5>                            
               <div class="text-start">
                <%--<div class="d-flex align-items-center flex-column mb-2">--%>
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
                   <br />
                        <asp:Label ID="Label10" runat="server" Text="Ciclo"></asp:Label>
                        <asp:DropDownList ID="ddSemestre" runat="server" class ="btn btn-outline-success dropdown-toggle mb-1" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="true" >
                        </asp:DropDownList> 
                   <br />
                   <asp:Panel runat="server" DefaultButton="Button2">
                        <asp:Label ID="Label13" runat="server" Text="Introduzca código del docente"></asp:Label>
                        <asp:TextBox ID="TextBox2" runat="server" Width="209px" class="btn btn-outline-success dropdown-toggle mb-1"></asp:TextBox>
                        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Buscar por código" class="btn btn-dark mb-1" />                        
                       </asp:Panel>
                   <br />
                   <asp:Panel runat="server" DefaultButton="Button1">
                        <asp:Label ID="Label2" runat="server" Text="Introduzca parte del nombre del docente"></asp:Label>                        
                        <asp:TextBox ID="TextBox1" runat="server" Width="209px" class="btn btn-outline-success dropdown-toggle mb-1"></asp:TextBox>                             
                        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Buscar Docentes" class="btn btn-dark mb-1" />                        
                       </asp:Panel>
                        <asp:Label ID="lbResultado" runat="server"></asp:Label>                        
                        <asp:DropDownList ID="ListBox1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ListBox1_SelectedIndexChanged" class ="btn btn-outline-success dropdown-toggle mb-1" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="true" AppendDataBoundItems="True" >
                            <asp:ListItem>--- Seleccione al Catedrático ---</asp:ListItem>
                        </asp:DropDownList>                        
                   <br />
                 &nbsp;<asp:Button ID="btNuevo" runat="server" Text="Nuevo" Width="113px" class="btn btn-danger mb-1" OnClick="btNuevo_Click" Height="41px" />
       
                 <%--</div>--%>
            </div>
         </div>
    </div>
<script src="src/js/print.min.js"></script>
        <link rel="stylesheet" type="text/css" href="src/css/print.min.css">
    <script>
        /*var myApp = new function () {
            this.printDiv = function () {
                
                var div = document.getElementById('printableArea');
                var win = window.open('', '', 'height=700,width=700');
                win.document.write(div.outerHTML);
                win.document.close();
                win.print();
                win.close();
                return true;
            }
        }*/

    </script>
        <button class="btn btn-icon btn-icon-only btn-foreground-alternate shadow datatable-print" onclick="printJS({ printable: 'printableArea', type: 'html',  documentTitle: 'Datos UMES', css: ['./vendor/animate.css/animate.min.css', './src/font/CS-Interface/style.css', './src/css/vendor/bootstrap.min.css', './src/css/vendor/OverlayScrollbars.min.css', './src/css/styles.css', './src/css/main.css'], style: ['@page { size: Legal; margin: 0mm;} body {margin: 0;} canvas {height: 1000;}'], targetStyles: ['*'] })" <%--onclick='myApp.printDiv()'--%> data-bs-delay="0" data-bs-toggle="tooltip" data-bs-placement="top" title="Print" type="button">
                <i data-acorn-icon="print"></i>
            </button>
<div id="printableArea">

    <!-- Vertical Alignment Start -->
                <section class="scroll-section" id="verticalAlignment">                  
                  <div class="card mb-5">
                    <div class="card-body">
                      <table class="table align-middle table-sm">
                        <thead>
                          <tr>
                            <th scope="col" class="w-25"></th>
                            <th scope="col" class="w-25"></th>
                            <th scope="col" class="w-25"></th>
                            <th scope="col" class="w-25"></th>
                          </tr>
                        </thead>
                        <tbody>
                          <tr bgcolor="#1ca35e">
                            <td>
                              <asp:Image ID="Image1" class="img-fluid rounded-xl  h-100" runat="server" />
                            </td>
                            <td>
                              <h2 class="mb-0 pb-0 display-4"><asp:Label ID="LabelNombreApe" runat="server"></asp:Label></h2>
                            </td>
                            <td>                              
                            </td>
                            <td>
                              <img src="Imagenes/LogoU.png" alt="logo" class="card-img card-img-horizontal left-bottom sw-11 h-50">
                            </td>
                          </tr>
                          <tr>                            
                            <td colspan="4" class="table-active"><h3 class="small-title">DATOS DOCENTES</h3></td>
                          </tr>
                          <tr>
                            <th scope="row" align="left"><h4 class="small-title">CONTACTO</h4></th>
                            <td colspan="3"></td>                            
                          </tr>
                          <tr>
                            <th scope="row" align="left">Dirección Domicilio:</th>
                            <td colspan="3" align ="left"><asp:Label ID="LabelDomicilio" runat="server" ></asp:Label></td>                            
                          </tr>
                          <tr>
                            <th scope="row" align="left">Teléfono de Domicilio:</th>
                            <td colspan="3" align ="left"><asp:Label ID="LabelTelefono" runat="server" ></asp:Label></td>                            
                          </tr>
                          <tr>
                            <th scope="row" align="left">Celular:</th>
                            <td colspan="3" align ="left"><asp:Label ID="LabelMovil" runat="server"></asp:Label></td>                            
                          </tr>
                          <tr>
                            <th scope="row" align="left">E-mail:</th>
                            <td colspan="3" align ="left"><asp:Label ID="LabelEmail" runat="server" ></asp:Label></td>                            
                          </tr>
                          <tr>
                            <td colspan="4" align="left">
                            <table class="table mb-0 table-sm">
                                <thead>
                                  <tr>
                                    <th scope="col"><div class="text-primary mb-1">Fecha Nacimiento</div></th>
                                    <th scope="col"><div class="text-primary mb-1">Nacionalidad</div></th>
                                    <th scope="col"><div class="text-primary mb-1">Lugar de nacimiento</div></th>
                                  </tr>
                                </thead>
                                <tbody>
                                  <tr>
                                    <th scope="row"><asp:Label ID='LabelFechaNacimiento' runat='server' ></asp:Label></th>
                                    <td><asp:Label ID="LabelPais" runat="server" ></asp:Label></td>
                                    <td><asp:Label ID="LabelLugarNac" runat="server" ></asp:Label></td>
                                  </tr>                                  
                                </tbody>
                              </table>
                              </td>
                          </tr>                          
                          <tr>
                            <td colspan="4" align="left">
                            <table class="table mb-0 table-sm">
                                <thead>
                                  <tr>
                                    <th scope="col"><div class="text-primary mb-1">No. DPI</div></th>
                                    <th scope="col"><div class="text-primary mb-1">No. NIT</div></th>
                                    <th scope="col"><div class="text-primary mb-1">Estado Civil</div></th>
                                  </tr>
                                </thead>
                                <tbody>
                                  <tr>
                                    <th scope="row"><asp:Label ID="LabelDpi" runat="server"></asp:Label></th>
                                    <td><asp:Label ID="LabelNit" runat="server"></asp:Label></td>
                                    <td><asp:Label ID="LabelEstadoCivil" runat="server" ></asp:Label></td>
                                  </tr>                                  
                                </tbody>
                              </table>
                              </td>
                          </tr>
                          <tr>
                            <th scope="row" align="left"><h4 class="small-title">DATOS LABORALES</h4></th>
                            <td colspan="3"></td>                            
                          </tr>
                          <tr>
                            <th scope="row">Nombre de Empresa:</th>
                            <td colspan="3" align="left"><asp:Label ID="LabelEmpresa" runat="server" ></asp:Label></td>                            
                          </tr>
                          <tr>
                            <th scope="row">Dirección de Empresa:</th>
                            <td colspan="3" align ="left"><asp:Label ID="LabelDireccion" runat="server" ></asp:Label></td>                            
                          </tr>
                          <tr>
                            <th scope="row">Teléfono de Empresa:</th>
                            <td colspan="3" align ="left"><asp:Label ID="LabelTelefonoLabora" runat="server" ></asp:Label></td>                            
                          </tr>
                          <tr>
                            <th scope="row">E-mail de Empresa:</th>
                            <td colspan="3" align ="left"><asp:Label ID="LabelEmailLabora" runat="server" ></asp:Label></td>                            
                          </tr>
                          <tr>
                            <th scope="row" align="left"><h4 class="small-title">DATOS ACADÉMICOS</h4></th>
                            <td colspan="3"></td>                            
                          </tr>
                          <tr>
                            <th scope="row">Título Universitario:</th>
                            <td colspan="3" align="left"><asp:Label ID="LabelTitUni" runat="server"></asp:Label></td>                            
                          </tr>
                          <tr>
                            <th scope="row">Título Maestría:</th>
                            <td colspan="3" align ="left"><asp:Label ID="LabelTitMae" runat="server"></asp:Label></td>                            
                          </tr>
                          <tr>
                            <th scope="row">Título Doctorado:</th>
                            <td colspan="3" align ="left"><asp:Label ID="LabelTitDoc" runat="server"></asp:Label></td>                            
                          </tr>
                          <tr>
                            <th scope="row">Título Diplomado:</th>
                            <td colspan="3" align ="left"><asp:Label ID="LabelTitDip" runat="server"></asp:Label></td>                            
                          </tr>
                          <tr>
                            <td colspan="4">
                              <table class="table mb-0 table-sm">
                                <thead>
                                  <tr>
                                    <th scope="col"><div class="text-primary mb-1">No. Colegiado</div></th>
                                    <th scope="col"><div class="text-primary mb-1">Colegio</div></th>
                                    <th scope="col"><div class="text-primary mb-1">Vigencia</div></th>
                                  </tr>
                                </thead>
                                <tbody>
                                  <tr>
                                    <th scope="row"><asp:Label ID='LabelColNum' runat='server' ></asp:Label></th>
                                    <td><asp:Label ID="LabelColCol" runat="server" ></asp:Label></td>
                                    <td><asp:Label ID="LabelColVig" runat="server" ></asp:Label></td>
                                  </tr>                                  
                                </tbody>
                              </table>
                            </td>
                          </tr>
                          <tr>
                            <td scope="row" align="left"><h4 class="small-title">DE LA UNIVERSIDAD</h4></td>
                            <td colspan="3"></td>                            
                          </tr>
                          <tr>
                            <th colspan="3" align="left">Fecha en que ingresó a trabajar para la Universidad:</th>
                            <td  align="left"><asp:Label ID="LabelFechaIngreso" runat="server" ></asp:Label></td>                            
                          </tr>
                          <tr>
                            <td colspan="3" align="left"><h4 class="small-title">CURSOS ASIGNADOS EN EL SIGUIENTE SEMESTRE</h4></td>
                            <td></td>                            
                          </tr>
                          <tr>
                            <td colspan="4">
                                <asp:GridView ID="GridView8" runat="server"  class="table table-striped table-sm" AutoGenerateColumns="false"  >
                        <Columns>
                            <asp:TemplateField HeaderText="Asignatura" FooterStyle-HorizontalAlign ="Center" HeaderStyle-VerticalAlign="Middle">
                                 <ItemTemplate>
                                    <asp:Label ID="lbasignatura" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Asignatura") %>'></asp:Label>                                
                                 </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Seccion" FooterStyle-HorizontalAlign ="Center" HeaderStyle-VerticalAlign="Middle">
                                 <ItemTemplate>
                                    <asp:Label ID="lbseccion" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.seccion") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ciclo" FooterStyle-HorizontalAlign ="Center" HeaderStyle-VerticalAlign="Middle">
                                 <ItemTemplate>
                                    <asp:Label ID="lbCiclo" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.semestre") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Carrera" FooterStyle-HorizontalAlign ="Center" HeaderStyle-VerticalAlign="Middle">
                                 <ItemTemplate>
                                    <asp:Label ID="lbCarrera" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Carrera") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Períodos" FooterStyle-HorizontalAlign ="Center" HeaderStyle-VerticalAlign="Middle">
                                 <ItemTemplate>
                                    <asp:Label ID="lbPeridodos" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Períodos") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            </Columns>
                    </asp:GridView>  
                            </td>                            
                          </tr>

                        </tbody>
                      </table>
                    </div>
                  </div>
                </section>
                <!-- Vertical Alignment End -->

</div>
<!--
    <div class="row">
        <div class="col-xl-4 mb-5">
                <h4> <asp:Label ID="Label7" runat="server" class="small-title" Text="Información General"></asp:Label> </h4>            
            
                <div class="card h-100-card">
                    <div class="card-body">
                      <div class="d-flex align-items-center flex-column mb-4">
                        <div class="d-flex align-items-center flex-column">
                              <div class="sw-13 position-relative mb-3">
                                <asp:Image ID="Image2" class="img-fluid rounded-xl" runat="server" />
                                  <div id='test'></div>
                              </div>      
                         </div>
                              <div >                                                                    
                                  <div></div> 
                                  <div></div> 
                                  <div></div> 
                                  <div></div> 
                                  <div></div> 
                                  <div></div> 
                                  <div></div> 
                                  <div></div> 
                                  <div></div> 
                                  <div><asp:Label ID="LabelComunidad" runat="server" ></asp:Label></div> 
                                  <div><asp:Label ID="LabelCuentaIndustrial" runat="server" ></asp:Label></div>                           
                              </div>
                        </div>
                     </div>
                  </div>
        </div>
        <div class="col-xl-4 mb-5">
            <h4><asp:Label ID="Label4" class="small-title" runat="server" Text="Información Médica"></asp:Label></h4>             
             <div class="card h-50-card">
                 <div class="card-body">
                      <div class="d-flex flex-column mb-4">
                           <div><asp:Label ID="LabelEmergencia" runat="server" Text="Teléfono Emergencia: "></asp:Label></div> 
                          <div><asp:Label ID="LabelContacto" runat="server" Text="Contacto: "></asp:Label></div> 
                          <div><asp:Label ID="LabelRelacion" runat="server" Text="Relación Contacto: "></asp:Label></div> 
                          <div><asp:Label ID="LabelMedico" runat="server" Text="Médico: "></asp:Label></div> 
                          <div><asp:Label ID="LabelTelefonoMedico" runat="server" Text="Tel. Médico: "></asp:Label></div> 
                          <div><asp:Label ID="LabelGrupoSanguineo" runat="server" Text="Grupo Sanguíneo: "></asp:Label></div> 
                          <div><asp:Label ID="LabelSeguroMedico" runat="server" Text="No. Seguro Médico: "></asp:Label></div> 
                          <div><asp:Label ID="LabelEmpresaSeguro" runat="server" Text="Empresa Seguro: "></asp:Label></div> 
                          <div><asp:Label ID="LabelAlergias" runat="server" Text="Alérgico A: "></asp:Label></div>
                          <div><asp:Label ID="LabelEnfermedad" runat="server" Text="Enfermedad: "></asp:Label></div>
                          <div><asp:Label ID="LabelTratamiento" runat="server" Text="IGSS: "></asp:Label></div>
                      </div>
                 </div>
             </div>
        </div>
         <div class="col-xl-4 mb-5">
             <h4><asp:Label ID="Label3" runat="server" class="small-title" Text="Información Laboral"></asp:Label></h4>
             <div class="card h-50-card">
                 <div class="card-body">
                      <div class="d-flex  flex-column mb-4">
                           <div></div> 
                          <div></div> 
                          <div></div> 
                          <div></div> 
                          <div><asp:Label ID="LabelIGSS" runat="server" Text="IGSS: "></asp:Label></div>                           
                          
                      </div>
                 </div>
             </div>
        </div>
    </div> 
    <div style="break-after:page"></div>
    <div class=newpage>    
    <div class="row">
        <div class="col-xl-4 mb-5">
             <h4><asp:Label ID="Label5" runat="server"  Text="Colegiados"></asp:Label></h4>
            <div class="card h-20-card">
                 <div class="card-body">                      
                            <asp:GridView ID="GridView4" runat="server" HorizontalAlign="Center" class="table table-striped" AutoGenerateColumns="false" >
                                <Columns>
                                    <asp:TemplateField HeaderText="Colegio Profesional">
                                            <ItemTemplate>
                                            <asp:Label ID="lbcolegio" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.colegio") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Colegiado #">
                                            <ItemTemplate>
                                            <asp:Label ID="lbNoColegiado" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NoColegiado") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Vencimiento">
                                            <ItemTemplate>
                                            <asp:Label ID="lbColVencimiento" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VencimientoColegiado") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    </Columns>
                            </asp:GridView>        
                    </div>                    
                </div>
        </div>
        <div class="col-xl-4 mb-5">        
            <h4><asp:Label ID="Label11" runat="server" class="small-title" Text="Títulos"></asp:Label></h4>
            <div class="card h-20-card">
                 <div class="card-body">    
                      <asp:GridView ID="GridView5" runat="server" class="table table-striped"  AutoGenerateColumns="false"  >
                            <Columns>
                            <asp:TemplateField HeaderText="Título Profesional">
                                 <ItemTemplate>
                                    <asp:Label ID="lbtitulo" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.descripcion") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            </Columns>
                     </asp:GridView>
                  </div>
            </div>
        </div>
        <div class="col-xl-4 mb-5">        
            <h4><asp:Label ID="Label6" runat="server" class="small-title" Text="Idiomas que domina"></asp:Label></h4>
            <div class="card h-20-card">
                 <div class="card-body">    
                         <asp:GridView ID="GridView6" runat="server" class="table table-striped"   AutoGenerateColumns="false"  >
                            <Columns>
                                <asp:TemplateField HeaderText="Idiomas" FooterStyle-HorizontalAlign ="Center" HeaderStyle-VerticalAlign="Middle">
                                     <ItemTemplate>
                                        <asp:Label ID="lbidioma" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.idioma") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                </Columns>
                        </asp:GridView>            
                  </div>
            </div>
        </div>
    </div>
         
    
    <div class="row">
        <div class="col-xl-12 mb-5">
             <h4><asp:Label ID="Label8" runat="server" class="small-title" Text="Cursos asignados en el presente semestre"></asp:Label></h4>
            <div class="card h-100-card">
                 <div class="card-body">
                          
                 </div>
            </div>
        </div>
    </div>
    </div>
           
            <br />
        
         <asp:GridView ID="GridView7" runat="server"  class="table table-striped" AutoGenerateColumns="False" Visible="False">
                                    <Columns>
                                        <asp:TemplateField HeaderText="DPI">
                                             <ItemTemplate>
                                                <asp:Label ID="lbdpi" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cedula") %>'></asp:Label>
                                            </ItemTemplate>
                                             <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Móvil">
                                             <ItemTemplate>
                                                <asp:Label ID="lbMovil" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.celular") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Teléfono">
                                             <ItemTemplate>
                                                <asp:Label ID="lbtel2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Telefono") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Email">
                                             <ItemTemplate>
                                                <asp:Label ID="lbemail2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.email") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="País">
                                             <ItemTemplate>
                                                <asp:Label ID="lbpais2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.pais") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Estado Civil">
                                             <ItemTemplate>
                                                <asp:Label ID="lbestaCiv" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.E_Civil") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nacimiento">
                                             <ItemTemplate>
                                                <asp:Label ID="lbNac" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Nacimiento") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nit">
                                             <ItemTemplate>
                                                <asp:Label ID="lbNit" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Nit") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ingresó el">
                                             <ItemTemplate>
                                                <asp:Label ID="lbNac" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Ingreso") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Comunidad">
                                             <ItemTemplate>
                                                <asp:Label ID="lbIngreso2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Comunidad") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cta. Industrial">
                                             <ItemTemplate>
                                                <asp:Label ID="lbInd" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CuentaIndustrial") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
         <asp:GridView ID="GridView2" runat="server" HorizontalAlign="Center" class="table table-striped" AutoGenerateColumns="false" Visible="False">
                    <Columns>
                        <asp:TemplateField HeaderText="Tel. Emergencia">
                             <ItemTemplate>
                                <asp:Label ID="lbmedico" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.telefonoemergencia") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Contacto">
                             <ItemTemplate>
                                <asp:Label ID="lbmedico" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.contactoemergencia") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Relación">
                             <ItemTemplate>
                                <asp:Label ID="lbmedico" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.relacioncontacto") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Médico">
                             <ItemTemplate>
                                <asp:Label ID="lbmedico" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Medico") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tel. Médico">
                             <ItemTemplate>
                                <asp:Label ID="lbtelmedico" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TelefonoMedico") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Grupo Sanguíneo">
                             <ItemTemplate>
                                <asp:Label ID="lbgrupo" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.grupo") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="No. Seguro Médico">
                             <ItemTemplate>
                                <asp:Label ID="lbNoseguro" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NoSeguroMedico") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Empresa Seg.">
                             <ItemTemplate>
                                <asp:Label ID="lbempresaSeg" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.EmpresaSeguroMedico") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Alérgico a">
                             <ItemTemplate>
                                <asp:Label ID="lbalergico" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AlergicoMedicamentos") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
            </asp:GridView>        
          <br />
                <asp:GridView ID="GridView1"  runat="server" HorizontalAlign="Center"  class="table table-striped"  AutoGenerateColumns="false" Visible="False" >
                    <Columns>
                        <asp:TemplateField HeaderText="Empresa" FooterStyle-HorizontalAlign ="Center" HeaderStyle-VerticalAlign="Middle">
                             <ItemTemplate>
                                <asp:Label ID="lbEmpresa" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Empresa") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Dirección" FooterStyle-HorizontalAlign ="Center" HeaderStyle-VerticalAlign="Middle">
                             <ItemTemplate>
                                <asp:Label ID="lbDireccion" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Direccion") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Teléfono" FooterStyle-HorizontalAlign ="Center" HeaderStyle-VerticalAlign="Middle">
                             <ItemTemplate>
                                <asp:Label ID="lbTelefono" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.telefono") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Email" FooterStyle-HorizontalAlign ="Center" HeaderStyle-VerticalAlign="Middle">
                             <ItemTemplate>
                                <asp:Label ID="lbemail" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.email") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="IGSS" FooterStyle-HorizontalAlign ="Center" HeaderStyle-VerticalAlign="Middle">
                             <ItemTemplate>
                                <asp:Label ID="lbigss" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.igss") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <br />
          <asp:GridView ID="GridView3" runat="server" HorizontalAlign="Center" CssClass="bg-green-dark" AutoGenerateColumns="False" Visible="False" >
            <Columns>
                <asp:TemplateField HeaderText="Enfermedad">
                     <ItemTemplate>
                        <asp:Label ID="lbEnfCron" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.EnfermedadCronica") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Tratamiento">
                     <ItemTemplate>
                        <asp:Label ID="lbtratamiento" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Tratamiento") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                </Columns>
        </asp:GridView>
            <asp:HyperLink ID="ImgBtnPrint" runat="server" ImageUrl="~/Imagenes/iconoImpresion1.JPG" NavigateUrl="~/PagsDocenteIndividualimpresion.aspx" Target="_blank" Visible="False">HyperLink</asp:HyperLink>
            <br />
        -->
    </form>
</asp:Content>
