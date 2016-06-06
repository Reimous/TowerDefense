Public Class UI
    Public Sub New(ByVal x As Single, ByVal y As Single, ByVal w As Single, ByVal h As Single, ByVal tipo As String, ByVal id As String, ByVal texto As String, Optional ByVal tBrush As TextureBrush = Nothing)
        _puntoLocalizacion = New PointF(x, y)
        _size = New Size(w, h)

        _puntoLocalizacionCentrado = New PointF(_puntoLocalizacion.X + w / 2, _puntoLocalizacion.Y + h / 2)
        _tipo = tipo
        _id = id
        _texto = texto
        _rObjeto = New Rectangle(x, y, w, h)
        _tBrush = tBrush
        _mouseOver = mouseOver


        Try
            Select Case tipo
                Case "apareceEnemigo"
                    '_bitmapImage = New Bitmap(My.Resources.cilindroShield)
                Case "shield"
                    '_bitmapImage = New Bitmap(My.Resources.shield9RedCentrado)
                Case "cuadro"
                    '_bitmapImage = New Bitmap(My.Resources.cuadroBlanco2)
                Case "building"

                Case Else
                    '_bitmapImage = New Bitmap(My.Resources.shield1RedCentrado)
            End Select
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.OkOnly, String.Format("Class: {0}", Me.GetType().Name))
            '_bitmapImage = New Bitmap(10, 10)
        End Try
    End Sub
    Private _id As String
    Public Property id() As String
        Get
            Return _id
        End Get
        Set(ByVal value As String)
            _id = value
        End Set
    End Property
    'Private _graphics As Graphics
    'Public Property graphics() As Graphics
    '    Get
    '        Return _graphics
    '    End Get
    '    Set(ByVal value As Graphics)
    '        _graphics = value
    '    End Set
    'End Property
    Private _size As SizeF
    Public Property size() As SizeF
        Get
            Return _size
        End Get
        Set(ByVal value As SizeF)
            _size = value
            rObjetoUpdate()
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
    Private _puntoLocalizacion As PointF = Nothing '-- where on the map is the user.
    Public Property puntoLocalizacion() As PointF
        Get
            Return _puntoLocalizacion
        End Get
        Set(ByVal value As PointF)
            _puntoLocalizacion = value
            _puntoLocalizacionCentrado = New PointF(_puntoLocalizacion.X + _size.Width / 2, _puntoLocalizacion.Y + _size.Height / 2)

            rObjetoUpdate()
        End Set
    End Property
    Private _puntoLocalizacionCentrado As PointF = Nothing
    Public Property puntoLocalizacionCentrado() As PointF
        Get
            Return _puntoLocalizacionCentrado
        End Get
        Set(ByVal value As PointF)
            _puntoLocalizacionCentrado = New PointF(_puntoLocalizacion.X + _size.Width / 2, _puntoLocalizacion.Y + _size.Height / 2)
        End Set
    End Property
    Private _rObjeto As Rectangle = Nothing
    Public Property rObjeto() As Rectangle
        Get
            Return _rObjeto
        End Get
        Set(ByVal value As Rectangle)
            _rObjeto = value
        End Set
    End Property
    Private _texto As String
    Public Property texto() As String
        Get
            Return _texto
        End Get
        Set(ByVal value As String)
            _texto = value
        End Set
    End Property
    Private _tBrush As TextureBrush
    Public Property tBrush() As TextureBrush
        Get
            Return _tBrush
        End Get
        Set(ByVal value As TextureBrush)
            _tBrush = value
        End Set
    End Property
    Private _mouseOver As Boolean = Nothing
    Public Property mouseOver() As Boolean
        Get
            Return _mouseOver
        End Get
        Set(ByVal value As Boolean)
            _mouseOver = value
        End Set
    End Property
    Private _visible As Boolean = Nothing
    Public Property visible() As Boolean
        Get
            Return _visible
        End Get
        Set(ByVal value As Boolean)
            _visible = value
        End Set
    End Property

    Private Sub rObjetoUpdate()
        _rObjeto = New Rectangle(_puntoLocalizacion.X, _puntoLocalizacion.Y, size.Width, size.Height)
    End Sub
End Class


