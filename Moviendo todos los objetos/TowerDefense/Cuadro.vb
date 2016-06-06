Imports System.Drawing.Drawing2D

Public Class Cuadro
    Dim whitePen As New Pen(Color.FromArgb(255, 255, 255, 255), 2)
    'Dim redPen As New Pen(Color.FromArgb(255, 255, 0, 0), 10)
    Public Sub New(ByVal A As PointF, ByVal B As PointF, ByVal C As PointF, ByVal D As PointF, ByVal id As String, ByVal indice As Integer, ByVal col As Integer, Optional ByVal mouseOver As Boolean = False)
        _A = A
        _B = B
        _C = C
        _D = D
        _pen = whitePen
        '_w = w
        '_h = h
        _id = id
        _indice = indice
        _col = col
        _mouseOver = mouseOver
        _polygon = New PointF() {_A, _B, _C, _D}
        '_bitmapImage = My.Resources.texturaGrass
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
    Private _indice As Integer
    Public Property indice() As Integer
        Get
            Return _indice
        End Get
        Set(ByVal value As Integer)
            _indice = value
        End Set
    End Property
    Private _A As PointF
    Public Property A() As PointF
        Get
            Return _A
        End Get
        Set(ByVal value As PointF)
            _A = value
            updatePolygon()
        End Set
    End Property
    Private _B As PointF
    Public Property B() As PointF
        Get
            Return _B
        End Get
        Set(ByVal value As PointF)
            _B = value
            updatePolygon()
        End Set
    End Property
    Private _C As PointF
    Public Property C() As PointF
        Get
            Return _C
        End Get
        Set(ByVal value As PointF)
            _C = value
            updatePolygon()
        End Set
    End Property
    Private _D As PointF
    Public Property D() As PointF
        Get
            Return _D
        End Get
        Set(ByVal value As PointF)
            _D = value
            updatePolygon()
        End Set
    End Property
    Private _pen As Pen
    Public Property pen() As Pen
        Get
            Return _pen
        End Get
        Set(ByVal value As Pen)
            _pen = value
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
            'If _mouseOver Then
            '    _pen = redPen
            'Else
            '    _pen = whitePen
            'End If
        End Set
    End Property
    Private _col As Integer
    Public Property col() As Integer
        Get
            Return _col
        End Get
        Set(ByVal value As Integer)
            _col = value

        End Set
    End Property
    Private _polygon() As PointF
    Public Property polygon() As PointF()
        Get
            Return _polygon
        End Get
        Set(ByVal value As PointF())
            _polygon = value
        End Set
    End Property
    Private _ocupado As Boolean
    Public Property ocupado() As Boolean
        Get
            Return _ocupado
        End Get
        Set(ByVal value As Boolean)
            _ocupado = value
        End Set
    End Property
   
    Private Sub updatePolygon()
        _polygon = New PointF() {_A, _B, _C, _D}
    End Sub

    Public Sub changePos(ByVal xA As Single, ByVal yA As Single)
        _A.X += xA
        _A.Y += yA
        _B.X += xA
        _C.X += xA
        _D.X += xA
        _B.Y += yA
        _C.Y += yA
        _D.Y += yA
        updatePolygon()
    End Sub

End Class


