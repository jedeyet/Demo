<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PagsAlumnosEntrevista.aspx.cs" Inherits="Visor_de_Documentos.PagsAlumnosEntrevista" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderC" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form1" runat="server">
        <div class="card mb-6 text-center">
                <div class="card-body">
                    <h5 class="card-title mb-3">Registro de entrevistas a estudiantes</h5>
                    <div class="text-start">   
        
                <br />
        <br />
        <asp:Label ID="Label2" runat="server" Text="Carnet del estudiante"></asp:Label>
        <asp:TextBox ID="txCarnet" runat="server" MaxLength="11" Width="155px"></asp:TextBox>
        &nbsp;<asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Buscar" class="btn btn-success mb-1" Width="113px"   />
        &nbsp; <asp:Button ID="btNuevo" runat="server" Text="Nuevo" Width="113px" class="btn btn-danger mb-1" OnClick="btNuevo_Click" Height="41px" />
       
                        &nbsp;
        <asp:Label ID="lbPensum" runat="server" CssClass="alert-inverse" BackColor="White" Font-Bold="True" ForeColor="#003300" Visible="False"></asp:Label>
       
                        <br />
        <asp:Label ID="lbnombre" runat="server" CssClass="alert-inverse" BackColor="White" Font-Bold="True" ForeColor="#003300"></asp:Label>
                        <br />
        <asp:Label ID="lbFecha" runat="server" CssClass="alert-inverse" BackColor="White" Font-Bold="True" ForeColor="#003300"></asp:Label>
                        <br />
           
                        <asp:Button ID="btRegistrar" runat="server" OnClick="btRegistrar_Click" Text="Registrar entrevista" class="btn btn-success mb-1" Width="151px" Visible="False"   />
                        <br />
                        <br />
                        <br />
                        <br />
       
        <br />
        <br />
        &nbsp;<br />
        &nbsp;<br />
        <br />
        <br />
           
        &nbsp;&nbsp;
           
                        <br />
           
        <br />
                           </div>

         </div>
         </div>  
    </form>
</asp:Content>

