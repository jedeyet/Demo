<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Academia.aspx.cs" Inherits="Visor_de_Documentos.Pages.Academia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderC" runat="server">
    <%--<asp:BoundField DataField="Asignatura" HeaderText="Asignatura" />
           <asp:BoundField DataField="Zona" HeaderText="Zona"  />
           <asp:BoundField DataField="Examen_Final" HeaderText="Final"  />   
           <asp:BoundField DataField="Nota_Final" HeaderText="Total"  />   --%> 
    <script src="../Scripts/Default.aspx.js" type="text/javascript"></script>

    
<link href="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/0.1.0/css/footable.min.css"
    rel="stylesheet" type="text/css" />
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/0.1.0/js/footable.min.js"></script>
<script type="text/javascript">
    $(function () {
        $('[id*=GridViewHpagos]').footable();
    });
</script>
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  
    
    
      <form id="form1" runat="server">
    <p>
        Carné del Alumno
        <asp:TextBox ID="txtCarne" runat="server" Width="228px"></asp:TextBox>
    </p>
          <p>
        <asp:Button CssClass="mb-sm btn btn-success" ID="btActuales" runat="server" OnClick="btActuales_Click" Text="Actuales" />
        <asp:Button CssClass="mb-sm btn btn-success" ID="btAprobados" runat="server" OnClick="btAprobados_Click" Text="Aprobados" />
        <asp:Button ID="btNoAprobados" runat="server" CssClass="mb-sm btn btn-success" OnClick="btNoAprobados_Click" Text="No Aprobados" />
        <asp:Button ID="btEquivalencias" runat="server" CssClass="mb-sm btn btn-success" OnClick="btEquivalencias_Click" Text="Equivalencias" />
        <asp:Button ID="btSuficiencias" runat="server" CssClass="mb-sm btn btn-success" OnClick="btSuficiencias_Click" Text="Suficiencias" />
        <asp:Button ID="EnBaseaPensum" runat="server" CssClass="mb-sm btn btn-success" OnClick="EnBaseaPensum_Click" Text="Pensum" />
    </p>
         
        
          <div class="col-md-6 col-md-offset-3">

             <asp:Panel ID="Panel1" runat="server" Width="476px" Visible="False">
                 <asp:Label ID="lbAn" runat="server" Text="Año" ></asp:Label>
                 <asp:DropDownList ID="ddAnio" runat="server">
                 </asp:DropDownList>
                 <asp:Label ID="LbSe" runat="server" Text="Semestre/Trimestre" ></asp:Label>
                 <asp:DropDownList ID="ddSem" runat="server">
                     <asp:ListItem Value="1">Enero - Junio</asp:ListItem>
                     <asp:ListItem Value="2">Julio - Noviembre</asp:ListItem>
                     <asp:ListItem Value="3">3er Trimestre</asp:ListItem>
                     <asp:ListItem Value="4">4to Trimestre</asp:ListItem>
                 </asp:DropDownList>
                 <asp:Button ID="btBuscar" runat="server" CssClass="bg-success" Text="Buscar" OnClick="btBuscar_Click" />
                 
          </asp:Panel>
              <asp:Label ID="lbPensum" runat="server" Text="Label" Visible="False"></asp:Label>
              <asp:Label ID="lbResultado" runat="server" CssClass="progress"></asp:Label>
            <br />
              <asp:Label ID="LbInfo" runat="server" CssClass="progress"></asp:Label>
           
        <asp:GridView ID="GridViewNotas"  runat="server" CssClass="footable" style="max-width: 700px" EmptyDataText="NO hay datos que mostrar.">
            <Columns>
                <asp:TemplateField HeaderText="No.">
                    <ItemTemplate>
                        <asp:Label ID="lbNum" runat="server" Text="Label"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <asp:GridView ID="GridViewActuales"  runat="server" CssClass="footable" style="max-width: 700px" EmptyDataText="NO hay datos que mostrar.">
            <Columns>
                <asp:TemplateField HeaderText="No.">
                    <ItemTemplate>
                        <asp:Label ID="lbNum" runat="server" Text="Label"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
           <%--<asp:BoundField DataField="Asignatura" HeaderText="Asignatura" />
           <asp:BoundField DataField="Zona" HeaderText="Zona"  />
           <asp:BoundField DataField="Examen_Final" HeaderText="Final"  />   
           <asp:BoundField DataField="Nota_Final" HeaderText="Total"  />   --%>
            </Columns>
        </asp:GridView>






       </div>

       <div class="col-md-6 col-md-offset-3"> 
       <asp:GridView ID="GridViewPensum"  runat="server" AutoGenerateColumns="False" CssClass="footable" style="max-width: 700px" EmptyDataText="NO hay datos que mostrar." Visible="False">
           <Columns>
               
               <asp:TemplateField HeaderText="No.">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lbNumero" runat="server"></asp:Label>
                    </ItemTemplate>
               </asp:TemplateField>

               <asp:TemplateField HeaderText="Asignatura">
                   <ItemTemplate>
                       <asp:Label ID="lbAsignatura" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.asignatura") %>'></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
               
               <asp:TemplateField HeaderText="Semestre">
                   <ItemTemplate>
                       <asp:Label ID="lbSemestre" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.semestre") %>'></asp:Label>
                       
                   </ItemTemplate>
               </asp:TemplateField>
               
               <asp:TemplateField HeaderText="Punteo">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lbPunteo" runat="server"></asp:Label>
                    </ItemTemplate>
               </asp:TemplateField>

               <asp:TemplateField HeaderText="Fecha">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lbFecha" runat="server"></asp:Label>
                    </ItemTemplate>
               </asp:TemplateField>

               <asp:TemplateField HeaderText="Tipo Nota">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lbTipo" runat="server"></asp:Label>
                    </ItemTemplate>
               </asp:TemplateField>

               
           </Columns>
         
       </asp:GridView>
      </div>

</form>
</asp:Content>
