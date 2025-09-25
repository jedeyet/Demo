<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PagsDocenteVerDocumentos.aspx.cs" Inherits="Visor_de_Documentos.PagsDocenteVerDocumentos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderC" runat="server">
   
    <script src="Scripts/Default.aspx.js" type="text/javascript"></script>
    <style type="text/css">
        .embed-container {
            position: relative;
            padding-bottom: 56.25%;
            height: 0;
            overflow: hidden;
        } 
        .embed-container iframe {
            position: absolute;
            top:0;
            left: 0;
            width: 100%;
            height: 100%;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form1" runat="server">
            <div class="card card-default">
               <div class="card card-header">
                    <h3>Expediente Digital</h3>
                </div>
                <div class="card card-body">
                        <div class ="row">
                            <div class ="col-md-3">                                 
                                    <Label>Sede</Label> 
                            </div>
                            <div class ="col-md-6 mb-1">
                                    <asp:DropDownList ID="ddlSede" runat="server" CssClass="btn btn-outline-success dropdown-toggle mb-1" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="true">
                                        <asp:ListItem Value="1">Quetzaltenango</asp:ListItem>
                                        <asp:ListItem Value="2">Guatemala</asp:ListItem>
                                       <%-- <asp:ListItem Value="3">Cobán</asp:ListItem>
                                        <asp:ListItem Value="4">Teologado</asp:ListItem>
                                        <asp:ListItem Value="5">Izabal</asp:ListItem>--%>
                                    </asp:DropDownList>
                            </div>          
                         </div>
                        <div class ="row">                            
                            <div class ="col-md-3">                                    
                                    <Label>Introduzca parte del nombre del docente</Label>                                    
                            </div>
                            <div class ="col-md-6 mb-1">                                    
                                <asp:TextBox ID="txtCatedratico" runat="server" AutoPostBack="True" CssClass="form-control" ></asp:TextBox>                                    
                            </div>
                        </div>
                        <div class ="row">                            
                            <div class ="col-md-12 mb-5">
                                <asp:Button ID="btnBuscaProfesores" runat="server" class="btn btn-square btn-dark" Text="Buscar Profesores" OnClick="btnBuscaProfesores_Click"/>
                            </div>
                        </div>                                    
                        <div class ="row">
                            <div class ="col-md-12">
                                <h5>Seleccione a un profesor y el documento que quiere ver</h5>
                            </div>
                        </div>
                        <div class ="row">
                             <div class ="col-md-3">                                    
                                    <Label>Profesores encontrados</Label>                                      
                            </div>
                            <div class ="col-md-6 mb-1">
                                <asp:DropDownList ID="lbxProfesores" class="btn btn-outline-primary dropdown-toggle" runat="server" >
                                    <asp:ListItem>--- Seleccione Profesor ---</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class ="row">
                             <div class ="col-md-3">                                    
                                    <Label>Seleccione el documento a visualizar</Label>                                    
                            </div>
                            <div class ="col-md-6">                                                                          
                                    <asp:DropDownList CssClass="btn btn-outline-success dropdown-toggle mb-1" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="true" ID="ddlDocumentos" runat="server">
                                        <asp:ListItem Value="Curr">Currículo</asp:ListItem>
                                        <asp:ListItem Value="Cole">Constancia Colegiado</asp:ListItem>
                                        <asp:ListItem Value="Dpi">DPI</asp:ListItem>
                                        <asp:ListItem Value="Apen">Antecedentes Penales</asp:ListItem>
                                        <asp:ListItem Value="Apol">Antecedentes Policíacos</asp:ListItem>
                                        <asp:ListItem Value="Rtu">RTU</asp:ListItem>
                                        <asp:ListItem Value="Cons">Constancias Laborales</asp:ListItem>
                                        <asp:ListItem Value="Igss">Constancia de IGSS</asp:ListItem>
                                        <asp:ListItem Value="Tsal">Tarjeta de Salud</asp:ListItem>
                                        <asp:ListItem Value="Tit">Títulos Universitarios</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                         </div>
                    <div class="card card-footer">                    
                        <div class ="col text-center">
                            <asp:Button ID="ButtonSubir" runat="server" class="btn btn-square btn-success" Text="Visualizar Documento" OnClick="ButtonVer_Click"/>
                    &nbsp;<asp:Button ID="btNuevo" runat="server" Text="Nuevo" Width="113px" class="btn btn-danger mb-1" OnClick="btNuevo_Click" Height="41px" />
       
                    </div>    
                </div>
            </div>
         </div>    
    <div class ="row">
        <div class ="col-md-12">
            <div class="embed-container">
                <iframe id="pdf" runat="server" height="600" width="450" allowfullscreen ="true" ></iframe>            
            </div>                            
        </div>
    </div>
              
         
  </form>
</asp:Content>
