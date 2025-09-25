<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Creacion.aspx.cs" Inherits="Visor_de_Documentos.Account.Creacion" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
   <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1"/>
   <meta name="description" content="Bootstrap Admin App + jQuery"/>
   <meta name="keywords" content=""/>
   <title>Registrarse en el sistema</title>
   <!-- =============== VENDOR STYLES ===============-->
   <!-- FONT AWESOME-->
   <link rel="stylesheet" href="../vendor/fontawesome/css/font-awesome.min.css"/>
   <!-- SIMPLE LINE ICONS-->
   <link rel="stylesheet" href="../vendor/simple-line-icons/css/simple-line-icons.css"/>
   <!-- =============== BOOTSTRAP STYLES ===============-->
   <link rel="stylesheet" href="../app/css/bootstrap.css" id="bscss"/>
   <!-- =============== APP STYLES ===============-->
   <link rel="stylesheet" href="../app/css/app.css" id="maincss"/>    
</head>
<body>
        <div class="wrapper">
      <div class="block-center mt-xl wd-xl">
         <!-- START panel-->
         <div class="panel panel-dark panel-flat">
            <div class="panel-heading text-center">
               <a href="#">
                  <img src="Logo-Un-Color_icono.png" alt="Image" class="block-center img-rounded"/>
               </a>
            </div>
            <div class="panel-body">
               <p class="text-center pv">CREACION DE NUEVOS USUARIOS</p>
                <form id="form1" runat="server">
                  <div class="form-group has-feedback">
                      Ingrese usuario
                      <%--<asp:TextBox ID="txtUsuario"  placeholder="Número de usuario" class="form-control" runat="server"></asp:TextBox>--%>
                     <input id="txtUsuario" type="text" autocomplete="off" required class="form-control" runat="server"/>
                      Ingrese correo institucional
                     <input id="txCorreo" type="text" autocomplete="off" required class="form-control" runat="server"/>
                     <span class="fa fa-envelope form-control-feedback text-muted"></span>
                  </div>
                  <div class="form-group has-feedback">
                      <%--<asp:input ID="txtPassword"  placeholder="Contraseña" class="form-control" runat="server"></asp:input>--%>
                      Ingrese contraseña 
                      <input id="txtPassword" type="password" placeholder="Password" required class="form-control" runat="server"/>
                      Repita contraseña
                      <input id="txPassword2" type="password" placeholder="Password" required class="form-control" runat="server"/>
                     <span class="fa fa-lock form-control-feedback text-muted"></span>
                  </div>
                  <div class="clearfix">
                     <div class="checkbox c-checkbox pull-left mt0">
                        &nbsp;</div>
                      <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label>
                  </div>
                    <asp:Button ID="Button1" runat="server" Text="Crear" class="btn btn-block btn-primary mt-lg" OnClick="Button1_Click" />
                    <br />
                    <%--<button type="submit" class="btn btn-block btn-primary mt-lg">Login</button>--%>
                </form>
               </div>
         </div>
         <!-- END panel-->
         <div class="p-lg text-center">
            <span>&copy;</span>
            <span></span>
            <span>-</span>
            <span>Redes y Programas</span>
            <br/>
            <span>Creación de usuarios</span>
         </div>
      </div>
   </div>
   <!-- =============== VENDOR SCRIPTS ===============-->
   <!-- MODERNIZR-->
   <script src="../vendor/modernizr/modernizr.custom.js"></script>
   <!-- JQUERY-->
   <script src="../vendor/jquery/dist/jquery.js"></script>
   <!-- BOOTSTRAP-->
   <script src="../vendor/bootstrap/dist/js/bootstrap.js"></script>
   <!-- STORAGE API-->
   <script src="../vendor/jQuery-Storage-API/jquery.storageapi.js"></script>
   <!-- PARSLEY-->
   <script src="../vendor/parsleyjs/dist/parsley.min.js"></script>
   <!-- =============== APP SCRIPTS ===============-->
   <script src="js/app.js"></script>

</body>
</html>

