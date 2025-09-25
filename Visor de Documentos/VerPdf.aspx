<%@ Page  Title="Visor de Documentos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VerPdf.aspx.cs" Inherits="Visor_de_Documentos.Pages.VerPdf" %>

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

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form1" runat="server">

        <div class="card card-default">
               <div class="card card-header">
                    <h3>Expediente Digital</h3>
                    <p>
        <asp:Label ID="Label5" runat="server" Text="Sede"></asp:Label>
        <asp:DropDownList ID="DropDownListSede" runat="server" class ="btn btn-outline-success dropdown-toggle mb-1" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="true"  AutoPostBack="True"  OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
            <asp:ListItem Value="1">Quetzaltenango</asp:ListItem>
            <asp:ListItem Value="2">Guatemala</asp:ListItem>
            <asp:ListItem Value="3">Cobán</asp:ListItem>
            <asp:ListItem Value="4">Teologado</asp:ListItem>
            <asp:ListItem Value="5">Izabal</asp:ListItem>
            <asp:ListItem Value="6">Amatitlán</asp:ListItem>
        </asp:DropDownList>
                            </p>
                </div>
                <div class="card card-body">
                     <div class ="row">
                            <div class="col-md-2">   
                                <asp:Label ID="Label4" runat="server" Text="Parte del nombre del estudiante"></asp:Label><br />
                                <asp:TextBox ID="txNombre" runat="server" MaxLength="20" Width="291px"></asp:TextBox>
                                <asp:Button ID="btCoincidencias" runat="server" OnClick="btCoincidencias_Click" Text="Buscar Coincidencias" class="btn btn-success mb-1" Width="156px"  />
                                &nbsp; <asp:Button ID="btNuevo" runat="server" Text="Nueva Búsqueda" Width="169px" class="btn btn-danger mb-1" OnClick="btNuevo_Click" />
       
                                <br />
                                <br />
                                <br />
                                <asp:DropDownList ID="ddNombre" runat="server" class ="btn btn-outline-success dropdown-toggle mb-1" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="true" AutoPostBack="True" OnSelectedIndexChanged="ddNombre_SelectedIndexChanged" Width="839px" AppendDataBoundItems="True">
                                    <asp:ListItem>-- No hay Alumnos con esos datos --</asp:ListItem>
                                </asp:DropDownList>
                                <br />
                                <br />
                                
                       </div>
                         <div class ="row">
                            <div class ="col-md-2">
                                <asp:Label ID="Label1" runat="server" Text="Carnet del alumno"></asp:Label>
                                <asp:TextBox ID="txtCarne" runat="server"></asp:TextBox>
                                <asp:Button CssClass="mb-sm btn btn-success" ID="Button1" runat="server" OnClick="Button1_Click" Text="Buscar Dcoumentos Escaneados" />
                            </div>                            
                      </div>
                         </div>
                     <div class ="row">
                         <div class="col-md-2">   
                                <asp:Label ID="Label3" runat="server" Text="Documento a Visualizar" Visible="False"></asp:Label><br />
                             <asp:DropDownList CssClass="btn btn-outline-success dropdown-toggle mb-1" ID="ddlDocumentos" runat="server" Visible="False" >
                           </asp:DropDownList>
                            <asp:Button CssClass="mb-sm btn btn-success" ID="BtVerPdf" runat="server" Text="Ver Pdf" OnClick="BtVerPdf_Click" Visible="False" /> 
                             <br />
                             <asp:Label ID="Label2" runat="server"></asp:Label>
                            </div>
                    </div>
                     <div class="card card-footer">    
                             <div class ="row">
                                <div class ="col-md-3">   
                                    <h4>
                                        
                                    </h4>        
                                </div>
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
