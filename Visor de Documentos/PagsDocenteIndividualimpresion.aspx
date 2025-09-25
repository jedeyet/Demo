<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PagsDocenteIndividualimpresion.aspx.cs" Inherits="Visor_de_Documentos.PagsDocenteIndividualimpresion" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        
        <div class="card mb-6 text-center">
      <div class="card-body">
             <h5 class="card-title mb-2">Datos del Docente</h5>                            
               <div class="text-start">
                <div class="d-flex align-items-center flex-column mb-2">
                 </div>
            </div>
         </div>
    </div>
    
    <div class="row">
        <div class="col-xl-4 mb-5">
                <h4> <asp:Label ID="Label7" runat="server" class="small-title" Text="Información General"></asp:Label> </h4>
                <div class="card h-100-card">
                    <div class="card-body">
                      <div class="d-flex align-items-center flex-column mb-4">
                        <div class="d-flex align-items-center flex-column">
                              <div class="sw-13 position-relative mb-3">
                                <asp:Image ID="Image1" class="img-fluid rounded-xl" runat="server" />
                                  <br />
                                  <asp:Label ID="LabelNombre" runat="server"></asp:Label>
                              </div>      
                         </div>
                              <div >                                  
                                  <div><asp:Label ID="LabelDpi" runat="server"></asp:Label></div> 
                                  <div><asp:Label ID="LabelNit" runat="server"></asp:Label></div> 
                                  <div><asp:Label ID="LabelMovil" runat="server"></asp:Label></div> 
                                  <div><asp:Label ID="LabelTelefono" runat="server" ></asp:Label></div> 
                                  <div><asp:Label ID="LabelEmail" runat="server" ></asp:Label></div> 
                                  <div><asp:Label ID="LabelPais" runat="server" ></asp:Label></div> 
                                  <div><asp:Label ID="LabelEstadoCivil" runat="server" ></asp:Label></div> 
                                  <div><asp:Label ID="LabelFechaNacimiento" runat="server" ></asp:Label></div> 
                                  <div><asp:Label ID="LabelFechaIngreso" runat="server" ></asp:Label></div> 
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
                           <div><asp:Label ID="LabelEmpresa" runat="server" Text="Empresa donde Labora: "></asp:Label></div> 
                          <div><asp:Label ID="LabelDireccion" runat="server" Text="Dirección: "></asp:Label></div> 
                          <div><asp:Label ID="LabelTelefonoLabora" runat="server" Text="Teléfono: "></asp:Label></div> 
                          <div><asp:Label ID="LabelEmailLabora" runat="server" Text="Email: "></asp:Label></div> 
                          <div><asp:Label ID="LabelIGSS" runat="server" Text="IGSS: "></asp:Label></div>                           
                          
                      </div>
                 </div>
             </div>
        </div>
    </div>                      
        
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
                     <asp:GridView ID="GridView8" runat="server"  class="table table-striped" AutoGenerateColumns="false"  >
                        <Columns>
                            <asp:TemplateField HeaderText="Asignatura" FooterStyle-HorizontalAlign ="Center" HeaderStyle-VerticalAlign="Middle">
                                 <ItemTemplate>
                                    <asp:Label ID="lbasignatura" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.asignatura") %>'></asp:Label>
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
                                    <asp:Label ID="lbCarrera" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Nombre de la carrera") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            </Columns>
                    </asp:GridView>       
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
            <br />


    </form>
</body>
</html>
