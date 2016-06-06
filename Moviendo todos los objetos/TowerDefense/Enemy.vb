Public Class Enemy
    Public Sub New(ByVal x As Single, ByVal y As Single, ByVal w As Integer, ByVal h As Integer, ByVal tipo As String, ByVal id As String, ByVal tBrush As TextureBrush, Optional ByVal mouseOver As Boolean = False)
        _puntoLocalizacion = New PointF(x, y)
        _size = New Size(w, h)
        _puntoLocalizacionCentrado = New PointF(_puntoLocalizacion.X + w / 2, _puntoLocalizacion.Y + h / 2)
        _tipo = tipo
        _id = id
        _rObjeto = New Rectangle(x, y, w, h)
        _tBrush = tBrush

        _mouseOver = mouseOver
        '_timerObjetivo = New tObjeto("objetivo")
        '_lMovimiento -= 5
        '_sID = "User"

        Try
            Select Case tipo
                Case "invader"
                    _vidaTotal = 200
                    _vida = _vidaTotal
                    _radioAtaque = 3
                    _rAtaque = New Rectangle(x - TowerDefense.medidaCuadro.Width / 1.5 * (_radioAtaque - 1), y - TowerDefense.medidaCuadro.Height / 2 * (_radioAtaque - 1), TowerDefense.medidaCuadro.Width * _radioAtaque, TowerDefense.medidaCuadro.Height * _radioAtaque)
                    '_daño = 2
                    _movConst = 10
                    _mov = _movConst
                    _esperaDisparando = 800
                    _esperaObjetivo = 0
                Case "shield"

                Case "cuadro"

                Case Else

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
            _puntoLocalizacionCentrado = New PointF(puntoLocalizacion.X + _size.Width / 2, puntoLocalizacion.Y + _size.Height / 2)
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
    Private _tBrush As TextureBrush
    Public Property tBrush() As TextureBrush
        Get
            Return _tBrush
        End Get
        Set(ByVal value As TextureBrush)
            _tBrush = value
        End Set
    End Property
    Private _puntoObjetivo As PointF
    Public Property puntoObjetivo() As PointF
        Get
            Return _puntoObjetivo
        End Get
        Set(ByVal value As PointF)
            _puntoObjetivo = value
        End Set
    End Property
    Private _distObjetivo As Single
    Public Property distObjetivo() As Single
        Get
            Return _distObjetivo
        End Get
        Set(ByVal value As Single)
            _distObjetivo = value
        End Set
    End Property
    Private _anguloObjetivo As Single
    Public Property anguloObjetivo() As Single
        Get
            Return _anguloObjetivo
        End Get
        Set(ByVal value As Single)
            _anguloObjetivo = value
        End Set
    End Property
    Private _idObjetivo As String
    Public Property idObjetivo() As String
        Get
            Return _idObjetivo
        End Get
        Set(ByVal value As String)
            _idObjetivo = value
        End Set
    End Property
    Private _vidaTotal As Integer
    Public Property vidaTotal() As Integer
        Get
            Return _vidaTotal
        End Get
        Set(ByVal value As Integer)
            _vidaTotal = value
        End Set
    End Property
    Private _vida As Integer
    Public Property vida() As Integer
        Get
            Return _vida
        End Get
        Set(ByVal value As Integer)
            _vida = value
        End Set
    End Property
    Private _radioAtaque As Integer
    Public Property radioAtaque() As Integer
        Get
            Return _radioAtaque
        End Get
        Set(ByVal value As Integer)
            _radioAtaque = value
        End Set
    End Property
    Private _rAtaque As Rectangle
    Public Property rAtaque() As Rectangle
        Get
            Return _rAtaque

        End Get
        Set(ByVal value As Rectangle)
            _rAtaque = value
            rObjetoUpdate()
        End Set
    End Property
    Private _daño As Integer
    Public Property daño() As Integer
        Get
            Return _daño
        End Get
        Set(ByVal value As Integer)
            _daño = value
        End Set
    End Property
    Private _idArea As String
    Public Property idArea()
        Get
            Return _idArea
        End Get
        Set(ByVal value)
            _idArea = value
        End Set
    End Property
    Private _esperaDisparando As Integer
    Public Property esperaDisparando() As Integer
        Get
            Return _esperaDisparando
        End Get
        Set(ByVal value As Integer)
            _esperaDisparando = value
        End Set
    End Property
    Private _timerDisparando As Single
    Public Property timerDisparando() As Single
        Get
            Return _timerDisparando
        End Get
        Set(ByVal value As Single)
            _timerDisparando = value
        End Set
    End Property
    Private _esperaObjetivo As Integer
    Public Property esperaObjetivo() As Integer
        Get
            Return _esperaObjetivo
        End Get
        Set(ByVal value As Integer)
            _esperaObjetivo = value
        End Set
    End Property
    Private _timerObjetivo As Single
    Public Property timerObjetivo() As Single
        Get
            Return _timerObjetivo
        End Get
        Set(ByVal value As Single)
            _timerObjetivo = value
        End Set
    End Property
    'Utilizo point en este caso para tener 2 valores de porcentaje. Uno respecto a la x y el otro a la y
    Private _porcPosRel As PointF
    Public Property porcPosRel() As PointF
        Get
            Return _porcPosRel
        End Get
        Set(ByVal value As PointF)
            _porcPosRel = value
        End Set
    End Property
    Private _movConst As Single
    Public Property movConst() As Single
        Get
            Return _movConst
        End Get
        Set(ByVal value As Single)
            _movConst = value
        End Set
    End Property
    Private _mov As Single
    Public Property mov() As Single
        Get
            Return _mov
        End Get
        Set(ByVal value As Single)
            _mov = value
        End Set
    End Property
    Private _movX As Single
    Public Property movX() As Single
        Get
            Return _movX
        End Get
        Set(ByVal value As Single)
            _movX = value
        End Set
    End Property
    Private _movY As Single
    Public Property movY() As Single
        Get
            Return _movY
        End Get
        Set(ByVal value As Single)
            _movY = value
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
    Private _eliminar As Boolean = False
    Public Property eliminar() As Boolean
        Get
            Return _eliminar
        End Get
        Set(ByVal value As Boolean)
            _eliminar = value
        End Set
    End Property


    Private Sub rObjetoUpdate()
        _rObjeto = New Rectangle(_puntoLocalizacion.X, _puntoLocalizacion.Y, size.Width, size.Height)
        _rAtaque = New Rectangle(_puntoLocalizacion.X - TowerDefense.medidaCuadro.Width / 1.5 * (_radioAtaque - 1), _puntoLocalizacion.Y - TowerDefense.medidaCuadro.Height / 2 * (_radioAtaque - 1), TowerDefense.medidaCuadro.Width * _radioAtaque, TowerDefense.medidaCuadro.Height * _radioAtaque)
    End Sub
End Class
