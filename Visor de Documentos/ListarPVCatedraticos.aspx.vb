Imports System.Data
Imports System.Data.SqlClient
Partial Class ListarProgramasVirtuales
    Inherits System.Web.UI.Page
    Dim usuario As String = ""
    Dim xela As String = conexion.ConnectionString = Visor_de_Documentos.Models.Conex.Con_X.ConnectionString;
    Dim guate As String = conexion.ConnectionString = Visor_de_Documentos.Models.Conex.Con_G.ConnectionString;
    Dim actual As String = conexion.ConnectionString = Visor_de_Documentos.Models.Conex.Con_X.ConnectionString;
    Private Sub CargaSedes()
        usuario = System.Web.HttpContext.Current.Session("usuario").ToString().Trim()
        DropDownList1.Items.Clear()
        Dim cad As String = "select * from AdminsSedesPermisos where usuario='" & usuario & "'"
        Dim dt As DataTable = New DataTable()
        dt = Consulta2(cad, 1)
        Session("Nivel") = Val(dt.Rows(0)("Nivel").ToString())
        DropDownList1.DataSource = dt
        DropDownList1.DataValueField = "idSede"
        DropDownList1.DataTextField = "Sede"
        DropDownList1.DataBind()
    End Sub

    Protected Sub LlenaCatedraticos()
        Dim conexion As SqlConnection = New SqlConnection()
        Dim comando As SqlCommand = New SqlCommand()
        Dim datos As New DataSet
        Dim adaptador As SqlDataAdapter
        Dim cadena As String

        conexion.ConnectionString = actual
        Using (conexion)
            conexion.Open()
            cadena = "Select codigo_catedratico, [Nombre y apellidos del catedrático] as Catedratico From vista_asignacion_curso_profesor " &
            " where(ano = @Anio And SemestreCursado = @SemestreActual and [Nombre y apellidos del catedrático] not like 'aasig%' " &
            " and [Nombre y apellidos del catedrático] not like 'equiv%' ) group by codigo_catedratico, [Nombre y apellidos del catedrático] order by catedratico "
            comando.Dispose()
            comando.CommandType = CommandType.Text
            comando.CommandText = cadena
            comando.Connection = conexion
            comando.Parameters.AddWithValue("@anio", Me.ddlAnio.SelectedValue)
            comando.Parameters.AddWithValue("@SemestreActual", Me.rblSemestre.SelectedValue)
            adaptador = New SqlDataAdapter(comando)
            adaptador.Fill(datos)
            If datos.Tables(0).Rows.Count > 0 Then
                Me.ddlCatedraticos.DataSource = datos.Tables(0)
                Me.ddlCatedraticos.DataTextField = "Catedratico"
                Me.ddlCatedraticos.DataValueField = "codigo_catedratico"
                Me.ddlCatedraticos.DataBind()

            End If
        End Using

    End Sub
    Protected Sub BuscaPV()
        Dim conexion As SqlConnection = New SqlConnection()
        Dim comando As SqlCommand = New SqlCommand()
        Dim datos As New DataSet
        Dim adaptador As SqlDataAdapter

        Dim cadena As String
        conexion.ConnectionString = actual

        Using (conexion)
            conexion.Open()
            'Llena el DropDownList de los programas virtuales enviados
            cadena = "SELECT VACP.Codigo_Asignacion_Curso_Profesor, Asignatura + ', ' + [Nombre de la carrera] + ' ' + ACP.Seccion as CursoCarr, [Nombre y apellidos del catedrático], ACP.Ano, ACP.Seccion, ACP.SemestreCursado, ACP.Programa_Virtual, ACP.Entrego_pv "
            cadena &= "FROM Vista_Asignacion_Curso_Profesor as VACP Inner Join Asignacion_Curso_profesor as ACP "
            cadena &= "On ACP.codigo_asignacion_curso_profesor = VACP.codigo_asignacion_curso_profesor "
            cadena &= "WHERE (ACP.Codigo_Catedratico = @codigocat) AND (ACP.Ano = @anio) AND (ACP.SemestreCursado = @semestreact) --and Entrego_pv=1"
            comando.Dispose()
            comando.CommandType = CommandType.Text
            comando.CommandText = cadena
            comando.Connection = conexion
            comando.Parameters.AddWithValue("@codigocat", Me.ddlCatedraticos.SelectedValue)
            comando.Parameters.AddWithValue("@anio", Me.ddlAnio.SelectedValue)
            comando.Parameters.AddWithValue("@semestreact", Me.rblSemestre.SelectedValue)
            adaptador = New SqlDataAdapter(comando)
            adaptador.Fill(datos)
            If datos.Tables(0).Rows.Count > 0 Then
                Me.ddlListaPV.DataSource = datos.Tables(0)
                Me.ddlListaPV.DataTextField = "CursoCarr"
                Me.ddlListaPV.DataValueField = "Codigo_Asignacion_Curso_Profesor"
                Me.ddlListaPV.DataBind()

                cadena = "Select Programa_Virtual From Asignacion_Curso_profesor Where Codigo_Asignacion_Curso_Profesor = @CACP and (Ano = @Anio) AND (SemestreCursado = @SemestreActual)"
                comando.Dispose()
                comando.Parameters.Clear()
                comando.CommandType = CommandType.Text
                comando.CommandText = cadena
                comando.Connection = conexion
                comando.Parameters.AddWithValue("@CACP", Me.ddlListaPV.SelectedValue)
                comando.Parameters.AddWithValue("@Anio", Me.ddlAnio.SelectedValue)
                comando.Parameters.AddWithValue("@SemestreActual", Me.rblSemestre.SelectedValue)
                Dim Lector As SqlDataReader
                Lector = comando.ExecuteReader()
                If Lector.Read() Then
                    'Me.lblContenido.Text = Lector.GetString(0)
                    If Not Lector.IsDBNull(0) Then
                        Me.lblContenido.Text = Lector.GetString(0)
                    Else
                        Me.lblContenido.Text = "NO ha enviado programa"
                    End If
                End If
            Else
                Me.lblMensaje.Text = "No hay programa enviados"
                Me.ddlListaPV.Items.Clear()
                Me.ddlListaPV.DataBind()
            End If
        End Using
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Carga()
    End Sub
    Sub Carga()
        'Session("idu") = 7
        ' If (Session("idu") <> 0) Then
        If Not (Me.IsPostBack) Then
            Dim conexion As SqlConnection = New SqlConnection()
            Dim comando As SqlCommand = New SqlCommand()
            Dim datos As New DataSet
            Dim adaptador As SqlDataAdapter

            Dim cadena As String
            If (DateTime.Now.Month >= 1 And DateTime.Now.Month <= 6) Then
                Me.rblSemestre.SelectedIndex = 0
            Else
                Me.rblSemestre.SelectedIndex = 1
            End If

            conexion.ConnectionString = actual
            Using (conexion)
                conexion.Open()
                'LLena el Drop DownList de Año
                For x As Integer = 2002 To Now.Year
                    Me.ddlAnio.Items.Add(x)
                Next
                ddlAnio.SelectedIndex = ddlAnio.Items.Count - 1
                'cadena = "Select Ano FROM Vista_Asignacion_Curso_Profesor Where Ano =" & Now.Year
                'comando = New SqlCommand(cadena, conexion)
                'adaptador = New SqlDataAdapter(comando)
                'datos = New DataSet("Anios")
                'adaptador.Fill(datos, "Anios")
                'If datos.Tables(0).Rows.Count > 0 Then
                '    Me.ddlAnio.DataSource = datos.Tables("Anios")
                '    Me.ddlAnio.DataTextField = "Ano"
                '    Me.ddlAnio.DataValueField = "Ano"
                '    Me.ddlAnio.DataBind()
                'End If
                'Me.FCKeditorListaPV.ToolbarSet = "Basic"
                'Me.FCKeditorListaPV.Height = 400
            End Using
            LlenaCatedraticos()
            BuscaPV()
            Cursos()
            '  End If 'Fin del IsPostBack  
        End If
    End Sub
    Protected Sub ddlListaPV_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlListaPV.SelectedIndexChanged

        Dim conexion As SqlConnection = New SqlConnection()
        Dim comando As SqlCommand = New SqlCommand()
        Dim Lector As SqlDataReader

        Dim cadena As String
        conexion.ConnectionString = actual
        Using (conexion)
            conexion.Open()
            cadena = "Select Programa_Virtual From Asignacion_Curso_profesor Where Codigo_Asignacion_Curso_Profesor = @CACP"
            comando = New SqlCommand(cadena, conexion)
            comando.Parameters.AddWithValue("@CACP", Me.ddlListaPV.SelectedValue)
            Lector = comando.ExecuteReader()
            If Lector.Read() Then
                'Me.FCKeditorListaPV.Value = Lector.GetString(0)
                If Not Lector.IsDBNull(0) Then
                    Me.lblContenido.Text = Lector.GetString(0)

                Else
                    Me.lblContenido.Text = "NO ha enviado programa!"
                End If
            End If
        End Using
    End Sub

    Protected Sub rblSemestre_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rblSemestre.SelectedIndexChanged
        LlenaCatedraticos()
        Me.lblMensaje.Text = ""
        'Me.FCKeditorListaPV.Value = ""        
        BuscaPV()
    End Sub

    Protected Sub ddlAnio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlAnio.SelectedIndexChanged
        LlenaCatedraticos()
        Me.lblMensaje.Text = ""
        'Me.FCKeditorListaPV.Value = ""
        'Me.edtPV.Content = ""
        BuscaPV()
    End Sub

    Protected Sub ddlCatedraticos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCatedraticos.SelectedIndexChanged
        Call Cursos()
        Me.BuscaPV()
    End Sub
    Sub Cursos()

        Dim conexion As New SqlConnection
        Dim comando As SqlCommand = New SqlCommand()
        conexion.ConnectionString = actual
        Using (conexion)
            conexion.Open()
            comando.Dispose()
            comando.CommandText = "progvirt"
            comando.CommandType = CommandType.StoredProcedure
            comando.Connection = conexion
            comando.Parameters.AddWithValue("@con", 1)
            comando.Parameters.AddWithValue("@cat", Me.ddlCatedraticos.SelectedValue)
            comando.Parameters.AddWithValue("@yea", Me.ddlAnio.SelectedValue)
            comando.Parameters.AddWithValue("@sem", Me.rblSemestre.SelectedValue)
            Dim Lector As SqlDataReader
            Lector = comando.ExecuteReader()
            If Lector.Read() Then
                'Me.lblContenido.Text = Lector.GetString(0)
                If Not Lector.IsDBNull(0) Then
                    Me.Label1.Text = "Cursos asignados en este semestre: " & Lector.GetInt32(0)
                Else
                    Me.Label2.Text = "No hay datos localizados"
                End If
            End If
        End Using

        Dim conexion1 As New SqlConnection
        Dim comando1 As SqlCommand = New SqlCommand()
        conexion1.ConnectionString = actual
        Using (conexion1)
            conexion1.Open()
            comando1.Dispose()
            comando1.CommandText = "progvirt"
            comando1.CommandType = CommandType.StoredProcedure
            comando1.Connection = conexion1
            comando1.Parameters.AddWithValue("@con", 2)
            comando1.Parameters.AddWithValue("@cat", Me.ddlCatedraticos.SelectedValue)
            comando1.Parameters.AddWithValue("@yea", Me.ddlAnio.SelectedValue)
            comando1.Parameters.AddWithValue("@sem", Me.rblSemestre.SelectedValue)
            Dim Lector2 As SqlDataReader
            Lector2 = comando1.ExecuteReader()
            If Lector2.Read() Then
                'Me.lblContenido.Text = Lector.GetString(0)
                If Not Lector2.IsDBNull(0) Then
                    Me.Label2.Text = "Programas enviados: " & Lector2.GetInt32(0)
                Else
                    Me.Label2.Text = "No hay datos localizados"
                End If
            End If
        End Using

    End Sub

    Public Function Consulta2(cadena As String, conexion As String) As DataTable
        Dim conex2 As SqlConnection = New SqlConnection(conexion)
        Dim tabla As New SqlDataAdapter(cadena, conex2)
        Dim dato As New DataTable()
        tabla.Fill(dato)
        Return dato
    End Function
    'Private Sub cargaDocentes()
    '    Dim opcion As Integer = Convert.ToInt32(DropDownList1.SelectedValue.ToString())
    '    Dim cadena As String = "select Codigo_Catedratico, [Nombre y apellidos del catedrático] from catedraticos where " &
    '                       "[nombre y apellidos del catedrático] not like 'equiv%' and [nombre y apellidos del catedrático] not like 'Aasig%' " &
    '                       "and [nombre y apellidos del catedrático] not like '%ordina%' order by [Nombre y apellidos del catedrático]"
    '    Dim dt As DataTable = New DataTable
    '    dt = Consulta2(cadena, opcion)

    '    ' Dim conex4 As SqlConnection = Consulta3(opcion)
    '    'Dim da As New SqlDataAdapter(cadena, conex4)

    '    ' ddlCatedraticos.DataValueField = "codigo_catedratico"
    '    ' ddlCatedraticos.DataTextField = "nombre y apellidos del catedrático"
    '    'ddlCatedraticos.DataSource = dt
    '    'ddlCatedraticos.DataBind()


    '    'lbResultado.Text = "Docentes localizados: " & dt.Rows.Count.ToString()
    'End Sub
    Private Sub Nivelin()
        Dim opcion As Integer = Val(DropDownList1.SelectedValue)
        Dim usu As String = System.Web.HttpContext.Current.Session("usuario").ToString().Trim()
        Dim cad As String = "select * from AdminsSedesPermisos where usuario='" & usu & "' and idsede=" & opcion.ToString()
        'Label5.Text = cad
        Dim dt As DataTable = New DataTable()
        dt = Consulta2(cad, actual)
        System.Web.HttpContext.Current.Session("Nivel") = Convert.ToInt16(dt.Rows(0)("Nivel").ToString())
        'Label5.Text = System.Web.HttpContext.Current.Session("Nivel").ToString()
    End Sub
    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Nivelin()
        If DropDownList1.SelectedIndex = 0 Then actual = xela
        If DropDownList1.SelectedIndex = 1 Then actual = guate
        Carga()
        'cargaDocentes()
    End Sub

    Protected Sub DropDownList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList1.SelectedIndexChanged

    End Sub
End Class
