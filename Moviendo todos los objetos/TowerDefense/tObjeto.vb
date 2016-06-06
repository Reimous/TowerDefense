Public Class tObjeto
    Public Sub New(ByVal tipo As String, Optional ByVal enUso As Boolean = False, Optional ByVal tiempo As Integer = 0)

        _tipo = tipo
        _enUso = enUso
        _tiempo = tiempo
        Try
            Select Case tipo
                Case "tecla"
                    _tiempo = 30
                Case "enemy"
                    _tiempo = 500
                Case "recargando"
                    _tiempo = 200
                Case "objetivo"
                    _tiempo = 100
                Case Else
                    _tiempo = 0
            End Select
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.OkOnly, String.Format("Class: {0}", Me.GetType().Name))
        End Try
    End Sub
    Private _tiempo As Integer
    Public Property tiempo() As Integer
        Get
            Return _tiempo
        End Get
        Set(ByVal value As Integer)
            _tiempo = value
        End Set
    End Property
    Private _tipo As String
    Public Property tipo() As String
        Get
            Return _tipo
        End Get
        Set(ByVal value As String)
            _tipo = value
        End Set
    End Property
    Private _enUso As Boolean
    Public Property enUso() As Boolean
        Get
            Return _enUso
        End Get
        Set(ByVal value As Boolean)
            _enUso = value
        End Set
    End Property
End Class
