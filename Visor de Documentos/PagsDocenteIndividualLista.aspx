<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PagsDocenteIndividualLista.aspx.cs" Inherits="Visor_de_Documentos.PagsDocenteIndividualLista" %>

<!DOCTYPE html>
<meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1">
    <meta name="description" content="Bootstrap Admin App + jQuery">
    <meta name="keywords" content="app, responsive, jquery, bootstrap, dashboard, admin">

    <!-- =============== VENDOR STYLES ===============-->
    <!-- FONT AWESOME-->
    <link rel="stylesheet" href="./vendor/fontawesome/css/font-awesome.min.css">
    <!-- SIMPLE LINE ICONS-->
    <link rel="stylesheet" href="./vendor/simple-line-icons/css/simple-line-icons.css">
    <!-- ANIMATE.CSS-->
    <link rel="stylesheet" href="./vendor/animate.css/animate.min.css">
    <!-- WHIRL (spinners)-->
    <link rel="stylesheet" href="./vendor/whirl/dist/whirl.css">
    <!-- =============== PAGE VENDOR STYLES ===============-->
    <!-- =============== BOOTSTRAP STYLES ===============-->
    <link rel="stylesheet" href="./app/css/bootstrap.css" id="bscss">
    <!-- =============== APP STYLES ===============-->
    <link rel="stylesheet" href="./app/css/app.css" id="maincss">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .auto-style2 {
            width: 152px;
        }
        .auto-style3 {
            width: 152px;
            height: 23px;
        }
        .auto-style4 {
            height: 23px;
        }
        .auto-style5 {
            width: 152px;
            height: 7px;
        }
        .auto-style6 {
            height: 7px;
        }
    </style>
</head>
<body class="m-0 vh-100 row justify-content-center align-items-center">
    <form id="form1" runat="server">
        
        <div class="row d-flex justify-content-center">
        <table class="table table-bordered table-striped text-center">
            <tr>
                <td class="auto-style6" colspan="2">
                   
                     <asp:Label ID="Label1" runat="server" Text="Información sobre Docentes" CssClas="text-info" CssClass="text-info" Font-Bold="True" ForeColor="#003300"></asp:Label>
            </tr>
            <tr>
                <td class="auto-style5">
                   
                    <asp:Label ID="lbNombre0" runat="server" Text="Nombre" CssClas="text-info" CssClass="text-info" Font-Bold="True" ForeColor="#003300"></asp:Label>
                  
                </td>
                <td class="auto-style6">
                    <asp:Label ID="lbNombre" runat="server" Text="Label" CssClas="text-info" CssClass="text-info" Font-Bold="True" ForeColor="#003300"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style3">
                    <asp:Label ID="Label2" runat="server" Text="CUI" CssClas="text-info" CssClass="text-info" Font-Bold="True" ForeColor="#003300"></asp:Label>
                </td>
                <td class="auto-style4">
                    <asp:Label ID="lbCUI" runat="server" Text="Label" CssClas="text-info" CssClass="text-info" Font-Bold="True" ForeColor="#003300"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="Label3" runat="server" Text="Móvil" CssClas="text-info" CssClass="text-info" Font-Bold="True" ForeColor="#003300"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lbMovil" runat="server" Text="Label" CssClas="text-info" CssClass="text-info" Font-Bold="True" ForeColor="#003300"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="Label4" runat="server" Text="Teléfono" CssClas="text-info" CssClass="text-info" Font-Bold="True" ForeColor="#003300"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lbTel" runat="server" Text="Label" CssClas="text-info" CssClass="text-info" Font-Bold="True" ForeColor="#003300"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style3">
                    <asp:Label ID="Label13" runat="server" Text="email" CssClas="text-info" CssClass="text-info" Font-Bold="True" ForeColor="#003300"></asp:Label>
                </td>
                <td class="auto-style4">
                    <asp:Label ID="lbEmail" runat="server" Text="Label" CssClas="text-info" CssClass="text-info" Font-Bold="True" ForeColor="#003300"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style3">
                    <asp:Label ID="Label5" runat="server" Text="País" CssClas="text-info" CssClass="text-info" Font-Bold="True" ForeColor="#003300"></asp:Label>
                </td>
                <td class="auto-style4">
                    <asp:Label ID="lbPais" runat="server" Text="Label" CssClas="text-info" CssClass="text-info" Font-Bold="True" ForeColor="#003300"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="Label6" runat="server" Text="Estado Civil" CssClass="text-info" Font-Bold="True" ForeColor="#003300"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lbEst" runat="server" Text="Label" CssClass="text-info" Font-Bold="True" ForeColor="#003300"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="Label7" runat="server" Text="Nacimiento" CssClass="text-info" Font-Bold="True" ForeColor="#003300"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lbNac" runat="server" Text="Label" CssClass="text-info" Font-Bold="True" ForeColor="#003300"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="Label8" runat="server" Text="NIT" CssClass="text-info" Font-Bold="True" ForeColor="#003300"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lbNIT" runat="server" Text="Label" CssClass="text-info" Font-Bold="True" ForeColor="#003300"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="Label9" runat="server" Text="Comunidad " CssClass="text-info" Font-Bold="True" ForeColor="#003300"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lbCom" runat="server" Text="Label" CssClass="text-info" Font-Bold="True" ForeColor="#003300"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="Label10" runat="server" Text="cta. Industrial" CssClass="text-info" Font-Bold="True" ForeColor="#003300"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lbCta" runat="server" Text="Label" CssClass="text-info" Font-Bold="True" ForeColor="#003300"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="Label11" runat="server" Text="Colegiado" CssClass="text-info" Font-Bold="True" ForeColor="#003300"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lbCol" runat="server" Text="Label" CssClass="text-info" Font-Bold="True" ForeColor="#003300"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="Label12" runat="server" Text="Vencimiento" CssClass="text-info" Font-Bold="True" ForeColor="#003300"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lbVen" runat="server" Text="Label" CssClass="text-info" Font-Bold="True" ForeColor="#003300"></asp:Label>
                &nbsp;
                    <asp:Label ID="lbDanger" runat="server" Text="Label" CssClass="text-danger" Font-Bold="True" Visible="False"></asp:Label>
                </td>
            </tr>
        </table>
        <div>
            <br />
            <br />
        </div>
      </div>
    </form>
</body>
</html>
