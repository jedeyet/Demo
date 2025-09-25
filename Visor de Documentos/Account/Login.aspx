<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Visor_de_Documentos.Account.Login" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
   <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1"/>
   <%--<meta name="description" content="Bootstrap Admin App + jQuery"/>
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
   <link rel="stylesheet" href="../app/css/app.css" id="maincss"/>    --%>

    <meta name="description" content="Bootstrap Admin App + jQuery"/>
   <meta name="keywords" content=""/>
   <title>Entrar al sistema</title>

    <link href="MisEstilos.css" rel="stylesheet" type="text/css" />

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
                  
               </a>
            </div>
            <div class="panel-body">
               

           <%-- <img src="UMES2.jpg" class="img-responsive"/>--%>

 
               <p class="text-center pv">CREDENCIALES DE ACCESO.</p>
                <form id="form1" runat="server">
                  <div class="form-group has-feedback">
                      <%--<asp:TextBox ID="txtUsuario"  placeholder="Número de usuario" class="form-control" runat="server"></asp:TextBox>--%>
                     <input id="txtUsuario" type="text" autocomplete="off" required class="form-control" runat="server"/>
                     <span class= "fa fa-user form-control-feedback text-muted"></span>
                  </div>
                  <div class="form-group has-feedback">
                      <%--<asp:input ID="txtPassword"  placeholder="Contraseña" class="form-control" runat="server"></asp:input>--%>
                     <input id="txtPassword" type="password" placeholder="Password" required class="form-control" runat="server"/>
                     <span class="fa fa-lock form-control-feedback text-muted"></span>
                  </div>
                  <div class="clearfix">
                     <div class="checkbox c-checkbox pull-left mt0">
                        <label>
                             <ASP:CheckBox id="chkPersistCookie" runat="server" autopostback="false" />
                           <%--<input type="checkbox" value="" name="chkPersistCookie" runat="server" autopostback="false">--%>
                           <span class="fa fa-check"></span>Rercordarme</label>
                     </div>
                    <%-- <div class="pull-right"><a href="Creacion.aspx" class="text-muted">Crear usuario</a>
                     </div>--%>
                  </div>
                    <asp:Button ID="Button1" runat="server" Text="Login" class="btn btn-oldstyle btn-circle text-bold" OnClick="Button1_Click" Font-Bold="True"   ForeColor="White" Font-Size="Larger" Height="58px" Width="282px" BackColor="#006600" />
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
            <span>Login</span>
           
         </div>
      </div>
   </div>
       <!-- =============== VENDOR SCRIPTS ===============-->
   <!-- MODERNIZR -->
   <script src="../vendor/modernizr/modernizr.js"></script>

   <!-- JQUERY (única versión estable) -->
   <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>

   <!-- BOOTSTRAP (versión estable de la rama 3) -->
   <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>

   <!-- OTROS VENDORS -->
   <script src="../vendor/jQuery-Storage-API/jquery.storageapi.js"></script>
   <script src="../vendor/jquery.easing/js/jquery.easing.js"></script>
   <script src="../vendor/animo.js/animo.js"></script>
   <script src="../vendor/slimScroll/jquery.slimscroll.min.js"></script>
   <script src="../vendor/screenfull/dist/screenfull.js"></script>
   <script src="../vendor/jquery-localize-i18n/dist/jquery.localize.js"></script>

   <!-- APP SCRIPT -->
   <script src="../app/js/app.js"></script>
   <link rel="stylesheet" type="text/css" href="../vendor/MisEstilos/MisEstilos.css"/>
</body>
</html>

