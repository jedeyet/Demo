<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PagsDocentesContenidos.aspx.cs" Inherits="Visor_de_Documentos.PagsDocentesContenidos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderC" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form32A22" runat="server">
            <div class="card mb-6 text-center">
                <div class="card-body">
                    <h5 class="card-title mb-3">Docentes por sede y facultad</h5>                            
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
                            <asp:Label ID="Label2" runat="server" Text="Año"></asp:Label>
                            <asp:DropDownList ID="ddAnio" runat="server" class ="btn btn-outline-success dropdown-toggle mb-1" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="true">
                            </asp:DropDownList>                        
                            <br />
                            <asp:Label ID="Label3" runat="server" Text="Ciclo"></asp:Label>
                            <asp:DropDownList ID="ddSemestre" runat="server" class ="btn btn-outline-success dropdown-toggle mb-1" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="true">
                            </asp:DropDownList> 
                            <br />
                            <asp:Label ID="Label4" runat="server" Text="Facultad"></asp:Label>
                            <asp:DropDownList ID="ddFacultad" runat="server" class ="btn btn-outline-success dropdown-toggle mb-1" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="true" OnSelectedIndexChanged="ddFacultad_SelectedIndexChanged"></asp:DropDownList>                        
                            <br /> 
                            <br />
                            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Ver Docentes " class="btn btn-success mb-1" Width="251px"/>                                              
                            <asp:ImageButton ID="imgbutExc" runat="server"  ImageUrl="~/Imagenes/excel2.jpg" OnClick="imgbutExc_Click" ToolTip="Traslada a Excel los resultados del filtro" Visible="False" class ="btn btn-icon btn-icon-end btn-tertiary mb-1"/>                        
                            &nbsp;<asp:Button ID="btNuevo" runat="server" Text="Nuevo" Width="113px" class="btn btn-danger mb-1" OnClick="btNuevo_Click" Height="41px" />
                                  
                            <br />
                            <asp:Label ID="lbResultado" runat="server"></asp:Label>
                        </div>            
             </div>
      </div>
    <asp:GridView ID="GridView1" runat="server" HorizontalAlign="Center" class="table table-striped" AutoGenerateColumns="False"  >
            <Columns>
                <asp:TemplateField HeaderText="No.">
                    <ItemTemplate>
                        <asp:Label ID="lbNo" runat="server" Text=""></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Docente">
                    <ItemTemplate>
                        <asp:Label ID="lbdocente" runat="server" 
                            Text='<%# DataBinder.Eval(Container, "DataItem.Docente") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Asignatura">
                    <ItemTemplate>
                        <asp:Label ID="lbAsignatura" runat="server" 
                            Text='<%# DataBinder.Eval(Container, "DataItem.Asignatura") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Semestre">
                    <ItemTemplate>
                        <asp:Label ID="lbSemestre" runat="server" 
                            Text='<%# DataBinder.Eval(Container, "DataItem.Semestre") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Seccion">
                    <ItemTemplate>
                        <asp:Label ID="lbSeccion" runat="server" 
                            Text='<%# DataBinder.Eval(Container, "DataItem.Seccion") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Programa">
                    <ItemTemplate>
                        <asp:Label ID="lbPrograma" runat="server" 
                            Text='<%# DataBinder.Eval(Container, "DataItem.Programa") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Carrera">
                    <ItemTemplate>
                        <asp:Label ID="lbCarrera" runat="server" 
                            Text='<%# DataBinder.Eval(Container, "DataItem.Carrera") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
           
    </form>
</asp:Content>
