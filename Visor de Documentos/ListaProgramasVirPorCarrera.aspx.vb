Imports System.Runtime.InteropServices.Marshal
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports Microsoft.VisualBasic
Imports System
Partial Class ListarProgramasVirtuales
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' Session("idu") = 7
        If Not (Me.IsPostBack) Then
            ' If (Session("idu") <> 0) Then
            Dim conexion As SqlConnection = New SqlConnection()
            Dim comando As SqlCommand = New SqlCommand()
            Dim datos As New DataSet
            Dim adaptador As SqlDataAdapter
            Dim cadena As String
            conexion.ConnectionString = Visor_de_Documentos.Models.Conex.Con_X.ConnectionString;
            For x As Integer = 2002 To Now.Year
                ddlAnio.Items.Add(x)
            Next
            ddlAnio.SelectedIndex = ddlAnio.Items.Count - 1
            Using (conexion)
                conexion.Open()
                'LLena el Drop DownList de Año

                'LLena el Drop DownList Carreras
                cadena = "Select [nombre de la carrera] as ncarrera, [id carrera] as idcarrera from Carrera  "
                cadena &= "Where Estado='Activa' order by [nombre de la carrera]"
                comando.Dispose()
                comando.Parameters.Clear()
                comando.CommandType = CommandType.Text
                comando.CommandText = cadena
                comando.Connection = conexion
                adaptador = New SqlDataAdapter(comando)
                datos.Dispose()
                datos = New DataSet("Carreras")
                adaptador.Fill(datos, "Carreras")
                If datos.Tables(0).Rows.Count > 0 Then
                    Me.ddlCarrera.DataSource = datos.Tables("Carreras")
                    Me.ddlCarrera.DataTextField = "ncarrera"
                    Me.ddlCarrera.DataValueField = "idcarrera"
                    Me.ddlCarrera.DataBind()
                End If
                'LLena el Drop DownList Listado de Programas Virtuales
                comando.Dispose()
                comando.Parameters.Clear()
                cadena = "SELECT VACP.Codigo_Asignacion_Curso_Profesor, Asignatura + ' '+ACP.Seccion as CursoCarr, [Nombre y apellidos del catedrático], ACP.Ano, " &
                    "ACP.Seccion, ACP.SemestreCursado, ACP.Programa_Virtual, ACP.Entrego_pv "
                cadena &= "FROM Vista_Asignacion_Curso_Profesor as VACP Inner Join Asignacion_Curso_profesor as ACP "
                cadena &= "On ACP.codigo_asignacion_curso_profesor = VACP.codigo_asignacion_curso_profesor "
                cadena &= "WHERE (ACP.Ano = @anio) and vacp.Entrego_pv=1 AND (ACP.SemestreCursado = @semestreact) and (VACP.codigo_asignacion_curso_profesor like '@Carr')"
                cadena &= " order by VACP.Asignatura"
                comando.CommandType = CommandType.Text
                comando.CommandText = cadena
                comando.Connection = conexion
                comando.Parameters.AddWithValue("@anio", Me.ddlAnio.SelectedValue)
                comando.Parameters.AddWithValue("@semestreact", 2)
                Dim AuxCarr As String
                AuxCarr = Me.ddlCarrera.SelectedValue.ToString() & "%"
                comando.Parameters.AddWithValue("@carr", Me.ddlCarrera.SelectedValue.ToString() & "%")
                adaptador.Dispose()
                adaptador = New SqlDataAdapter(comando)
                datos.Dispose()
                datos = New DataSet("PV")
                adaptador.Fill(datos, "PV")
                If datos.Tables(0).Rows.Count > 0 Then
                    Me.ddlListaPV.DataSource = datos.Tables("PV")
                    Me.ddlListaPV.DataTextField = "CursoCarr"
                    Me.ddlListaPV.DataValueField = "Codigo_Asignacion_Curso_Profesor"
                    Me.ddlListaPV.DataBind()
                End If

            End Using
            Llena()
            ' End If 'Fin del if que compara con la variables de session            
        End If 'Fin del IsPostBack  
    End Sub

    Protected Sub ddlListaPV_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlListaPV.SelectedIndexChanged
        Dim conexion As SqlConnection = New SqlConnection()
        Dim comando As SqlCommand = New SqlCommand()
        Dim Lector As SqlDataReader

        Dim cadena As String
        Me.lblMensaje.Text = ""
        conexion.ConnectionString = Visor_de_Documentos.Models.Conex.Con_X.ConnectionString;
        Using (conexion)
            conexion.Open()
            cadena = "Select Programa_Virtual From Asignacion_Curso_profesor Where Codigo_Asignacion_Curso_Profesor = @CACP"
            comando = New SqlCommand(cadena, conexion)
            comando.Parameters.AddWithValue("@CACP", Me.ddlListaPV.SelectedValue)
            Lector = comando.ExecuteReader()
            If Lector.Read() And Not Lector.IsDBNull(0) Then

                lblContenido.Text = Lector.GetString(0)

            Else

                lblContenido.Text = ""
            End If
        End Using
    End Sub

    Protected Sub ddlCarrera_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCarrera.SelectedIndexChanged
        Me.lblMensaje.Text = ""
        Llena()
    End Sub
    Protected Sub Llena()
        Dim conexion As SqlConnection = New SqlConnection()
        Dim comando As SqlCommand = New SqlCommand()
        Dim datos As New DataSet
        Dim adaptador As SqlDataAdapter

        Dim cadena, apcad, AuxCarr As String
        conexion.ConnectionString = Visor_de_Documentos.Models.Conex.Con_X.ConnectionString;
        apcad = ""
        Using (conexion)
            conexion.Open()
            'AuxCarr = Me.ddlCarrera.SelectedValue.ToString() & "%"
            'NUEVA LINEA
            AuxCarr = Me.ddlCarrera.SelectedValue.ToString()
            'If Me.ddlCarrera.SelectedValue = 1 Then
            '    apcad = "and ([Nombre de la carrera] like '%Licenciatura en Administración de Empresas%')"
            'Else
            '    If Me.ddlCarrera.SelectedValue = 10 Then
            '        apcad = "and ([Nombre de la carrera] like '%Profesorado de Enseñanza Media en Computación%')"
            '    Else
            '        If Me.ddlCarrera.SelectedValue = 2 Then
            '            apcad = "and ([Nombre de la carrera] like '%Licenciatura en Arquitectura%')"
            '        Else
            '            apcad = " and (VACP.codigo_asignacion_curso_profesor like @carr)"
            '        End If
            '    End If
            'End If
            'NUEVA LINEA
            apcad = " and (VACP.[id carrera]= @carr)"
            If (Me.rblSemestre.SelectedValue = 0) Then
                cadena = "SELECT VACP.Codigo_Asignacion_Curso_Profesor, Asignatura + ' '+ACP.Seccion as CursoCarr, [Nombre y apellidos del catedrático], ACP.Ano, ACP.Seccion, ACP.SemestreCursado, ACP.Programa_Virtual, ACP.Entrego_pv "
                cadena &= "FROM Vista_Asignacion_Curso_Profesor as VACP Inner Join Asignacion_Curso_profesor as ACP "
                cadena &= "On ACP.codigo_asignacion_curso_profesor = VACP.codigo_asignacion_curso_profesor "
                cadena &= "WHERE (ACP.Ano = @anio) and vacp.Entrego_pv=1 " ' and (VACP.codigo_asignacion_curso_profesor like @carr)"
                cadena &= apcad & "order by vacp.asignatura"
                comando = New SqlCommand(cadena, conexion)
                comando.Parameters.AddWithValue("@anio", Me.ddlAnio.SelectedValue)
            Else
                cadena = "SELECT VACP.Codigo_Asignacion_Curso_Profesor, Asignatura + ' '+ACP.Seccion as CursoCarr, [Nombre y apellidos del catedrático], ACP.Ano, ACP.Seccion, ACP.SemestreCursado, ACP.Programa_Virtual, ACP.Entrego_pv "
                cadena &= "FROM Vista_Asignacion_Curso_Profesor as VACP Inner Join Asignacion_Curso_profesor as ACP "
                cadena &= "On ACP.codigo_asignacion_curso_profesor = VACP.codigo_asignacion_curso_profesor "
                cadena &= "WHERE (ACP.Ano = @anio) and vacp.Entrego_pv=1 AND (ACP.SemestreCursado = @semestreact)  " 'and (VACP.codigo_asignacion_curso_profesor like @carr)"
                cadena &= apcad & " order by vacp.asignatura"
                comando = New SqlCommand(cadena, conexion)
                comando.Parameters.AddWithValue("@anio", Me.ddlAnio.SelectedValue)
                comando.Parameters.AddWithValue("@semestreact", Me.rblSemestre.SelectedValue)
            End If

            comando.Parameters.AddWithValue("@carr", AuxCarr)
            'If Me.ddlCarrera.SelectedValue > 1 Then
            '    comando.Parameters.AddWithValue("@carr", AuxCarr)
            'End If
            adaptador = New SqlDataAdapter(comando)
            adaptador.Fill(datos)
            If datos.Tables(0).Rows.Count > 0 Then
                Me.ddlListaPV.DataSource = datos.Tables(0)
                Me.ddlListaPV.DataTextField = "CursoCarr"
                Me.ddlListaPV.DataValueField = "Codigo_Asignacion_Curso_Profesor"
                Me.ddlListaPV.DataBind()
                cadena = "Select Programa_Virtual From Asignacion_Curso_profesor Where Codigo_Asignacion_Curso_Profesor = @CACP"

                comando.Dispose()
                comando.Parameters.Clear()
                comando.CommandType = CommandType.Text
                comando.CommandText = cadena
                comando.Connection = conexion
                comando.Parameters.AddWithValue("@CACP", Me.ddlListaPV.SelectedValue)
                Dim Lector As SqlDataReader
                Lector = comando.ExecuteReader()
                If Lector.Read() And Not Lector.IsDBNull(0) Then

                    lblContenido.Text = Lector.GetString(0)
                    'Me.edtPV.Content = Lector.GetString(0)
                Else

                    lblContenido.Text = ""
                End If
            Else
                Me.ddlListaPV.Items.Clear()

                lblContenido.Text = ""
                'Me.edtPV.Content = ""
                Me.lblMensaje.Text = "NO existen Programas para esta Carrera y Semestre"
                Response.Write("<script>alert('NO existen Programas para esta Carrera y Semestre')</script>")
            End If
        End Using
    End Sub

    Protected Sub rblSemestre_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rblSemestre.SelectedIndexChanged
        Me.lblMensaje.Text = ""
        Llena()
    End Sub

    Protected Sub ddlAnio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlAnio.SelectedIndexChanged
        Me.lblMensaje.Text = ""
        Llena()
    End Sub
End Class
