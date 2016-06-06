Imports System.Windows.Forms
Imports System.Threading
Imports System.Drawing.Drawing2D
Imports System.IO
Imports System.Text
Imports System.Diagnostics

Public Class TowerDefense
    Public Declare Function GetAsyncKeyState Lib "user32.dll" (ByVal vKey As Int32) As UShort
    Private Declare Function mciSendString Lib "winmm.dll" Alias "mciSendStringA" (ByVal lpstrCommand As String, ByVal lpstrReturnString As String, ByVal uReturnLength As Integer, ByVal hwndCallback As Integer) As Integer


#Region "Declaraciones"
    Dim timer As Stopwatch
    Dim backBuffer As Bitmap
    Dim graphicsEscenario, graphicsCuadro As Graphics
    Dim graphicsCuadropath As GraphicsPath
    Dim medidaBuilding, medidaCilindro, medidaCuadroConst, medidaBuildingConst, medidaCilindroConst, medidaEnemy, medidaEnemyConst, medidaMuro, medidaMuroConst As SizeF
    Public medidaCuadro As SizeF
    Dim posPantalla, posMouseAnterior As PointF
    Dim medidaTablero As Size
    Dim interval, startTick, screenLeft, detCol, detCuadro, tiempo, enemyCount, buildingCount As Integer
    Dim IsBeingPlayed, IsLooping, gameover, confIn, cuadricula, zooming, paused, shiftDown As Boolean
    'Dim escenario As Rectangle
    Dim pbImage, bitmapDestino As Bitmap


    Dim brushSuelo, brushEnemy, brushPuntero, brushBtn As TextureBrush

    'Dim esquinaSuperior, EsquinaInferior As Short 'Identifica el numero de cuadro correspondiente al de arriba de todo y al de abajo de todo del tablero
    'Dim pbShield() As Bitmap = {My.Resources.shield2RedEditCentrado, My.Resources.shield3RedEditCentrado, _
    '                            My.Resources.shield4RedEditCentrado, My.Resources.shield5RedEditCentrado, _
    '                            My.Resources.shield6RedEditCentrado, My.Resources.shield7RedCentrado}

    'Dim pbDefensaConst() As Bitmap = {My.Resources.arty, My.Resources.arty2, _
    '                            My.Resources.arty3, My.Resources.arty4, _
    '                            My.Resources.arty5, My.Resources.arty6, _
    '                            My.Resources.arty7, My.Resources.arty8}

    Dim brushBuilding, brushHierba As TextureBrush

    Dim resSF(0), divMed As Single

    Dim scrollValue As Single
    'Dim building(0), cilindro(0) As Objeto
    Dim lBuilding As List(Of Objeto)
    'Dim enemy(0) As Enemy
    Dim lEnemy As List(Of Enemy)
    Dim lUI As List(Of UI)
    Dim lProy As List(Of Proyectil)
    Dim cuadro2(0) As Cuadro

    Dim construcSel As String

    'Dim timerZoom As tObjeto
    Dim timerEnemy, timerPulsaTecla, timerObjetivo As tObjeto

    Dim sf As Single                        'Scale Factor used to maintain proportions and scaling of bitmaps
    'Dim PieceImage As New Bitmap(128, 128)  'Create a bitmap to act as a game piece.
    Dim mloc, tab() As PointF


    Dim cuadroOverActual, cuadroOverAnterior As Cuadro



#End Region
#Region "Eventos Form"""
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.DoubleBuffered = True
        'Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
        'Me.pbSurface.BackgroundImageLayout = ImageLayout.Tile

        configExtra()

        'pbImage = My.Resources.megaGrass

        detCol = 0

        scrollValue = 0

        'movMouse = 30

        divMed = 8
        medidaTablero = New Size(20, 20)

        tiempo = 0

        Me.ClientSize = New Size(Configuracion.resValueW(My.Settings.Res), Configuracion.resValueH(My.Settings.Res))
        'posPantalla = New PointF(0, ClientSize.Height / 2)
        posPantalla = New PointF(0, 0)

        medidaCuadro = New Size(700 / divMed, 389 / divMed)
        'medidaCuadro = New SizeF(350 / divMed, 195 / divMed)
        'medidaBuilding = New SizeF(700 / divMed, 525 / divMed)
        medidaBuilding = New Size(700 / divMed, 700 / divMed)
        medidaCilindro = New SizeF(61 / divMed, 76 / divMed)

        medidaEnemy = New SizeF(264 / divMed, 192 / divMed)

        medidaMuro = New SizeF(69, 149)

        medidaCuadroConst = medidaCuadro
        medidaCilindroConst = medidaCilindro
        medidaBuildingConst = medidaBuilding
        medidaEnemyConst = medidaEnemy
        medidaMuroConst = medidaMuro


        'hipTablero = Math.Sqrt(Math.Pow(medidaCuadro.Width / 2, 2) + Math.Pow(medidaCuadro.Height / 2, 2)) * medidaTablero.Width

        backBuffer = New Bitmap(ClientSize.Width, ClientSize.Height)

        'graphicsEscenario = Graphics.FromImage(backBuffer)
        'escenario = New Rectangle(0, 0, ClientSize.Width, ClientSize.Height)

        Me.CenterToScreen()
        Me.MaximizeBox = False

        interval = 16 '16 = 60fps 33 = 30fps
        timer = New Stopwatch()

        Timer1.Interval = interval
        gameover = False
        confIn = False


        screenLeft = 0
        sf = 1

        cuadricula = False

        timerPulsaTecla = New tObjeto("tecla")
        timerEnemy = New tObjeto("enemy")
        timerObjetivo = New tObjeto("objetivo")

        paused = False

        enemyCount = 0
        buildingCount = 0

        cuadroOverActual = New Cuadro(New PointF(0, 0), New PointF(0, 0), New PointF(0, 0), New PointF(0, 0), "", -1, 0)
        cuadroOverAnterior = New Cuadro(New PointF(0, 0), New PointF(0, 0), New PointF(0, 0), New PointF(0, 0), "", -1, 0)

        lBuilding = New List(Of Objeto)
        lEnemy = New List(Of Enemy)
        lUI = New List(Of UI)
        lProy = New List(Of Proyectil)



        iniUI()

        'Inicializo el sonido abriendo un archivo, sino no funciona
        mciSendString("close Sonido", Nothing, 0, 0)
        mciSendString("open " & """" & Application.StartupPath & "\sonidos\Efecto_energia_montando3.wav" & """" & " type mpegvideo alias Sonido", String.Empty, 0, 0)


        'escalaImagenObjeto(medidaCuadro.Width, medidaCuadro.Height, My.Resources.grass, brushSuelo, WrapMode.Tile)
        'escalaImagenObjeto2(medidaCuadro.Width, medidaCuadro.Height, My.Resources.grass, bitmapDestino)
        'brushSuelo.WrapMode = WrapMode.Clamp
        'escalaImagenObjeto(medidaBuilding.Width, medidaBuilding.Height, My.Resources.Building9RedCentrado, brushBuilding)
    End Sub

    Private Sub configExtra()
        Me.SetStyle(System.Windows.Forms.ControlStyles.AllPaintingInWmPaint, True) ' True is better
        Me.SetStyle(System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer, True) ' True is better
        ' Disable the on built PAINT event. We dont need it with a renderloop.
        ' The form will no longer refresh itself
        ' we will raise the paint event ourselves from our renderloop.
        Me.SetStyle(System.Windows.Forms.ControlStyles.UserPaint, False) ' False is better

    End Sub
    Private Sub iniUI()
        Dim pApareceEnemigo As New Point(0, 50)
        Dim btnApareceEnemLeft As New UI(pApareceEnemigo.X, pApareceEnemigo.Y, 100, 50, "apareceEnemigo", "apareceEnemLeft", "Left")
        btnApareceEnemLeft.visible = False
        lUI.Add(btnApareceEnemLeft)
        Dim btnApareceEnemRight As New UI(pApareceEnemigo.X + 100, pApareceEnemigo.Y, 100, 50, "apareceEnemigo", "apareceEnemRight", "Right")
        btnApareceEnemRight.visible = False
        lUI.Add(btnApareceEnemRight)
        Dim btnApareceEnemTop As New UI(pApareceEnemigo.X + 50, pApareceEnemigo.Y - 50, 100, 50, "apareceEnemigo", "apareceEnemUp", "Top")
        btnApareceEnemTop.visible = False
        lUI.Add(btnApareceEnemTop)
        Dim btnApareceEnemBot As New UI(pApareceEnemigo.X + 50, pApareceEnemigo.Y + 50, 100, 50, "apareceEnemigo", "apareceEnemDown", "Bot")
        btnApareceEnemBot.visible = False
        lUI.Add(btnApareceEnemBot)


        Dim btnSeleccionaConstruccion As New UI(50, ClientSize.Height - 150, 125, 100, "seleccionaConstruccion", "seleccionaConstruccionBu", "")
        escalaImagenObjeto(medidaBuildingConst.Width * 1.5, medidaBuildingConst.Height * 1.5, My.Resources.arty, brushBtn, WrapMode.Clamp)
        brushBtn.TranslateTransform(btnSeleccionaConstruccion.puntoLocalizacion.X, btnSeleccionaConstruccion.puntoLocalizacion.Y - 40)
        btnSeleccionaConstruccion.tBrush = brushBtn
        btnSeleccionaConstruccion.visible = True
        lUI.Add(btnSeleccionaConstruccion)

        Dim btnSeleccionaConstruccion2 As New UI(175, ClientSize.Height - 150, 125, 100, "seleccionaConstruccion", "seleccionaConstruccionMu", "")
        escalaImagenObjeto(medidaMuroConst.Width * 1, medidaBuildingConst.Height * 1, My.Resources.MuroIndividual, brushBtn, WrapMode.Clamp)
        brushBtn.TranslateTransform(btnSeleccionaConstruccion2.puntoLocalizacion.X + 20, btnSeleccionaConstruccion2.puntoLocalizacion.Y - 0)
        btnSeleccionaConstruccion2.tBrush = brushBtn
        btnSeleccionaConstruccion2.visible = True
        lUI.Add(btnSeleccionaConstruccion2)

    End Sub
    Private Sub TowerDefense_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        GameLoop()
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If GetAsyncKeyState(Convert.ToInt32(Keys.Escape)) Then
            End
        End If
        If Not paused Then
            If Not confIn Then
                If GetAsyncKeyState(Convert.ToInt32(Keys.F10)) Then
                    confIn = True
                    Dim ventana As Configuracion = New Configuracion
                    If (ventana.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
                        stopGame()
                        gestionWindow()
                        startGame()
                        'Invalidate() 'COMENTADO YA QUE CREO QUE ES INNECESARIO
                        confIn = False
                    Else
                        confIn = False
                    End If
                End If
            End If

            If GetAsyncKeyState(Convert.ToInt32(Keys.A)) And (tab(0).X < 0 Or tab(2).X < ClientSize.Width) Then
                mueveObjeto(My.Settings.movPantalla, 0)
            End If
            If GetAsyncKeyState(Convert.ToInt32(Keys.W)) And (tab(1).Y < 0 Or tab(3).Y < ClientSize.Height) Then
                mueveObjeto(0, My.Settings.movPantalla)
            End If
            If GetAsyncKeyState(Convert.ToInt32(Keys.D)) And (tab(2).X > ClientSize.Width Or tab(0).X > 0) Then
                mueveObjeto(-My.Settings.movPantalla, 0)
            End If
            If GetAsyncKeyState(Convert.ToInt32(Keys.S)) And (tab(3).Y > ClientSize.Height Or tab(1).Y > 0) Then
                mueveObjeto(0, -My.Settings.movPantalla)
            End If

            If GetAsyncKeyState(Keys.Add) And Not timerPulsaTecla.enUso Then
                zoomInOut(False)
                temporizador(timerPulsaTecla)
            End If
            If GetAsyncKeyState(Keys.Subtract) And Not timerPulsaTecla.enUso Then
                zoomInOut(True)
                If scrollValue <> 0 Then
                    temporizador(timerPulsaTecla)
                End If
            End If
            If GetAsyncKeyState(Keys.E) And Not timerEnemy.enUso Then
                Dim rnd As New Random
                apareceEnemigo(rnd.Next(0, 4))
                'temporizador(timerEnemy)
            End If
        End If
        If GetAsyncKeyState(Keys.P) And Not timerPulsaTecla.enUso Then
            If Not timerPulsaTecla.enUso Then
                temporizador(timerPulsaTecla)
                If paused Then
                    paused = False
                Else
                    paused = True
                End If
            End If
        End If
        If GetAsyncKeyState(1) Then
            If Not timerPulsaTecla.enUso Then
                temporizador(timerPulsaTecla)
                leftClick()
            End If
        ElseIf GetAsyncKeyState(2) Then
            rightClick()
        End If
        If GetAsyncKeyState(Keys.ShiftKey) Then
            shiftDown = True
        Else
            shiftDown = False
        End If
    End Sub
    
    Private Sub TowerDefense_MouseWheel(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseWheel
        If Not paused Then
            If e.Delta > 0 Then
                zoomInOut(False)
            Else
                zoomInOut(True)
            End If
        End If
    End Sub
    Private Sub zoomInOut(ByVal out As Boolean)
        'zooming = True
        If Not out Then
            scrollValue += 0.2
            gestionWindow()
        Else
            If scrollValue > 0.2 Then
                scrollValue -= 0.2
                gestionWindow()
            ElseIf scrollValue > 0 Then
                scrollValue = 0
                gestionWindow()
            End If
        End If
    End Sub


    Private Sub mueveObjeto(ByVal x As Single, ByVal y As Single)
        'For i = 0 To UBound(building) - 1
        If lBuilding IsNot Nothing Then
            For Each bu As Objeto In lBuilding
                bu.puntoLocalizacion = New PointF(bu.puntoLocalizacion.X + x, bu.puntoLocalizacion.Y + y)
                bu.tBrush.TranslateTransform(x, y, MatrixOrder.Prepend)
            Next
        End If
        'For i = 0 To UBound(cilindro) - 1
        '    cilindro(i).x += x
        '    cilindro(i).y += y
        'Next
        For i = 0 To UBound(cuadro2) - 1
            cuadro2(i).changePos(x, y)
        Next
        'For i = 0 To UBound(enemy) - 1
        If lEnemy IsNot Nothing Then
            For Each en As Enemy In lEnemy
                en.puntoLocalizacion = New PointF(en.puntoLocalizacion.X + x, en.puntoLocalizacion.Y + y)
                en.tBrush.TranslateTransform(x, y, MatrixOrder.Prepend)
            Next
        End If

        If lProy IsNot Nothing Then
            For Each proy As Proyectil In lProy
                proy.puntoLocalizacion = New PointF(proy.puntoLocalizacion.X + x, proy.puntoLocalizacion.Y + y)
            Next
        End If

        posPantalla.X += x
        posPantalla.Y += y
        brushSuelo.TranslateTransform(x, y, MatrixOrder.Prepend)
        tab = New PointF() {cuadro2(0).A, cuadro2(UBound(cuadro2) - medidaTablero.Height).B, cuadro2(UBound(cuadro2) - 1).C, cuadro2(medidaTablero.Width - 1).D}
        'tab2 = New Point() {New Point(0, 0), New Point(0, hipTablero), New Point(hipTablero, hipTablero), New Point(hipTablero, 0)}
        'btnUp.Location = New Point(CInt(tab(1).X), CInt(tab(1).Y))
        'btnDown.Location = New Point(CInt(tab(3).X), CInt(tab(3).Y))
        'btnLeft.Location = New Point(CInt(tab(0).X), CInt(tab(0).Y))
        'btnRight.Location = New Point(CInt(tab(2).X), CInt(tab(2).Y))


    End Sub
    Private Sub leftClick()
        If Not paused Then
            If cuadroOverActual IsNot Nothing And construcSel IsNot Nothing Then
                If Not cuadroOverActual.ocupado Then
                    Select construcSel
                        Case "Building"

                            escalaImagenObjeto(medidaBuilding.Width, medidaBuilding.Height, My.Resources.arty, brushBuilding, WrapMode.Clamp)


                            Dim tmpBuilding As New Objeto(cuadroOverActual.A.X, cuadroOverActual.A.Y - medidaBuilding.Height / 1.3, medidaBuilding.Width, medidaBuilding.Height, "building", "building" & buildingCount, brushBuilding, cuadroOverActual.id)

                            lBuilding.Add(tmpBuilding)
                            buildingCount += 1

                            Dim lastBuilding As Integer = lBuilding.Count - 1

                            lBuilding(lastBuilding).tBrush.TranslateTransform(lBuilding(lastBuilding).puntoLocalizacion.X, lBuilding(lastBuilding).puntoLocalizacion.Y, MatrixOrder.Prepend)

                            ''Inicializo el sonido abriendo un archivo, sino no funciona
                            'mciSendString("close Sonido", Nothing, 0, 0)
                            'mciSendString("open " & """" & Application.StartupPath & "\sonidos\Efecto_energia_montando3.wav" & """" & " type mpegvideo alias Sonido", String.Empty, 0, 0)

                            'sonido(Application.StartupPath & "\Efecto_energia_montando3.wav")
                            If Not shiftDown Then
                                construcSel = Nothing
                            End If

                            'Iteración para establecer que el cuadro esta ocupado por la estructura
                            For Each cuad As Cuadro In cuadro2
                                If cuadroOverActual.id = cuad.id Then
                                    cuad.ocupado = True
                                End If
                            Next

                    End Select
                End If
            Else
                For Each btn In lUI
                    If btn.mouseOver Then
                        funcionBtnUI(btn.id)
                    End If
                Next
            End If
        End If

    End Sub
    Private Sub rightClick()
        construcSel = Nothing
    End Sub
    Private Sub funcionBtnUI(ByVal id As String)

        Select Case id
            Case "apareceEnemLeft"
                apareceEnemigo(0)
            Case "apareceEnemRight"
                apareceEnemigo(2)
            Case "apareceEnemUp"
                apareceEnemigo(1)
            Case "apareceEnemDown"
                apareceEnemigo(3)
            Case "seleccionaConstruccionBu"
                'construccionSeleccionada("Building")
                construcSel = "Building"
                escalaImagenObjeto(medidaBuilding.Width, medidaBuilding.Height, My.Resources.arty, brushPuntero, WrapMode.Clamp)
                brushPuntero.TranslateTransform(mloc.X - medidaBuilding.Width / 2, mloc.Y - medidaBuilding.Height / 1.4, MatrixOrder.Prepend) 'En posición ratón
        End Select
    End Sub
    Private Sub TowerDefense_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        End
    End Sub
    Private Sub pbSurface_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbSurface.MouseMove
        If Not paused Then
            'Invalidate(New Rectangle(CInt(mloc.X * sf), CInt(mloc.Y * sf), CInt((sqrSize + 2) * sf), CInt((sqrSize + 2) * sf)))
            'mloc.X = CInt(e.X / sf)
            'mloc.Y = CInt(e.Y / sf)


            posMouseAnterior = New PointF(mloc.X, mloc.Y) 'Comentado para escribirlo cuando cambio de cuadro
            mloc.X = CSng(e.X)
            mloc.Y = CSng(e.Y)
            If brushPuntero IsNot Nothing Then 'And cuadroOverActual IsNot Nothing Then

                'If cuadroOverActual.id <> cuadroOverAnterior.id Then
                brushPuntero.TranslateTransform(mloc.X - posMouseAnterior.X, mloc.Y - posMouseAnterior.Y, MatrixOrder.Prepend) 'En posición ratón

                'brushPuntero.TranslateTransform(cuadroOverActual.A.X - cuadroOverAnterior.A.X, cuadroOverActual.A.Y - cuadroOverAnterior.A.Y, MatrixOrder.Prepend) 'En posición centro cuadroOverActual

                'End If
            End If
            'ToolStripLabel3.Text = "distancia centro X: " & ClientSize.Width / 2 - cuadroOverActual.A.X & " Y: " & ClientSize.Height / 2 - cuadroOverActual.A.Y

            If Not calcInUI() Then
                calcInCuadro2()
            End If

            'Invalidate(New Rectangle(CInt(mloc.X * sf), CInt(mloc.Y * sf), CInt((sqrSize + 2) * sf), CInt((sqrSize + 2) * sf)))
            'Invalidate() 'Comment out this, and uncomment the other two above to do partial area updates   COMENTADO YA QUE CREO QUE NO ES NECESARIO
        End If
    End Sub
    Private Function calcInUI()
        Dim dentro As Boolean = False
        If lUI IsNot Nothing Then
            For Each btn In lUI
                If btn.visible Then
                    If mloc.X >= btn.puntoLocalizacion.X And mloc.X <= btn.puntoLocalizacion.X + btn.size.Width _
                    And mloc.Y >= btn.puntoLocalizacion.Y And mloc.Y <= btn.puntoLocalizacion.Y + btn.size.Height Then
                        dentro = True
                        cuadroOverActual = Nothing
                        btn.mouseOver = True
                    Else
                        btn.mouseOver = False

                    End If
                End If
            Next
            
        End If
        Return dentro
    End Function

    Private Sub calcInCuadro2()
        Dim inicio As Integer
        Dim final As Integer
        Dim over As Boolean = False
        If cuadroOverActual Is Nothing Then
            inicio = UBound(cuadro2) - 1
            final = 0
        Else
            inicio = cuadroOverActual.indice + medidaTablero.Width * 2
            final = cuadroOverActual.indice - medidaTablero.Width * 2
            If final < 0 Then
                final = 0
            End If
            If inicio > UBound(cuadro2) - 1 Then
                inicio = UBound(cuadro2) - 1
            End If
        End If

        For i = inicio To final Step -1

            ''''''B
            'A'''''''''C
            ''''''D

            'If mloc.X <= ClientSize.Width / 2 And mloc.Y <= ClientSize.Height / 2 Then 'Parte superior izquierda
            'ElseIf mloc.X >= ClientSize.Width / 2 And mloc.Y <= ClientSize.Height / 2 Then 'Parte superior derecha
            'ElseIf mloc.X <= ClientSize.Width / 2 And mloc.Y >= ClientSize.Height / 2 Then 'Parte inferior izquierda
            'ElseIf mloc.X >= ClientSize.Width / 2 And mloc.Y >= ClientSize.Height / 2 Then 'Parte inferior derecha
            'End If

            If mloc.X > cuadro2(i).A.X And mloc.X < cuadro2(i).C.X And mloc.Y > cuadro2(i).B.Y And mloc.Y < cuadro2(i).D.Y Then
                Dim x1 As Single = cuadro2(i).A.X
                Dim y1 As Single = cuadro2(i).A.Y
                Dim x2 As Single = cuadro2(i).B.X
                Dim y2 As Single = cuadro2(i).B.Y
                Dim x3 As Single = cuadro2(i).D.X
                Dim y3 As Single = cuadro2(i).D.Y
                Dim x4 As Single = cuadro2(i).C.X
                Dim y4 As Single = cuadro2(i).C.Y
                over = True
                If mloc.X > x1 And mloc.X < x2 Then
                    If mloc.Y > ((y2 - y1) * ((mloc.X - x1) / (x2 - x1))) + y1 And mloc.Y < ((y3 - y1) * ((mloc.X - x1) / (x2 - x1))) + y1 Then
                        detCol = cuadro2(i).col
                        cuadro2(i).mouseOver = True
                        'If cuadroOverActual IsNot Nothing Then
                        '    cuadroOverAnterior = cuadroOverActual
                        '    posMouseAnterior = New PointF(mloc.X, mloc.Y)
                        'End If
                        cuadroOverActual = cuadro2(i)
                        'over = True
                        ToolStripLabel8.Text = "dentro"
                        ToolStripLabel10.Text = "cuadro: " & i
                        ToolStripLabel11.Text = "cuadro: " & cuadro2(i).id
                    Else
                        cuadro2(i).mouseOver = False
                    End If
                    ElseIf mloc.X > x2 And mloc.X < x4 Then
                        If mloc.Y > ((y4 - y2) * ((mloc.X - x2) / (x4 - x2))) + y2 And mloc.Y < ((y4 - y3) * ((mloc.X - x3) / (x4 - x3))) + y3 Then
                            detCol = cuadro2(i).col
                            cuadro2(i).mouseOver = True
                        'If cuadroOverActual IsNot Nothing Then
                        '    cuadroOverAnterior = cuadroOverActual
                        '    posMouseAnterior = New PointF(mloc.X, mloc.Y)
                        'End If
                            cuadroOverActual = cuadro2(i)
                            'over = True
                            ToolStripLabel8.Text = "dentro"
                            ToolStripLabel10.Text = "cuadro: " & i
                            ToolStripLabel11.Text = "cuadro: " & cuadro2(i).id
                        Else
                            cuadro2(i).mouseOver = False
                        End If
                    End If
            ElseIf cuadro2(i).mouseOver Then
                cuadro2(i).mouseOver = False

            End If
        Next
        If Not over Then
            cuadroOverActual = Nothing
            ToolStripLabel8.Text = "fuera"
        End If
    End Sub
    Private Sub btnUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        apareceEnemigo(1)
    End Sub
    Private Sub btnRight_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        apareceEnemigo(2)
    End Sub
    Private Sub btnLeft_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        apareceEnemigo(0)
    End Sub
    Private Sub btnDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        apareceEnemigo(3)
    End Sub
    Private Sub apareceEnemigo(ByVal esquina As Integer)
        'ReDim Preserve enemy(UBound(enemy) + 1)
        'Dim lastenemy As Integer = UBound(enemy) - 1
        'Dim lastenemy As Integer = lEnemy.Count

        escalaImagenObjeto(medidaEnemy.Width, medidaEnemy.Height, My.Resources.EnemyInvader1, brushEnemy, WrapMode.Clamp)

        'enemy(lastenemy) = New Enemy(tab(esquina).X, tab(esquina).Y, medidaEnemy.Width, medidaEnemy.Height, False, "invader", False, "invader" & lastenemy, brushEnemy)
        'enemy(lastenemy) = New Enemy(tab(esquina).X - medidaEnemy.Width / 2, tab(esquina).Y - medidaEnemy.Height / 2, medidaEnemy.Width, medidaEnemy.Height, False, "invader", False, "invader" & lastenemy, brushEnemy)
        'lEnemy(lastenemy) = New Enemy(tab(esquina).X - medidaEnemy.Width / 2, tab(esquina).Y - medidaEnemy.Height / 2, medidaEnemy.Width, medidaEnemy.Height, False, "invader", False, "invader" & lastenemy, brushEnemy)


        Dim tmpEnemy As New Enemy(tab(esquina).X - medidaEnemy.Width / 2, tab(esquina).Y - medidaEnemy.Height / 2, medidaEnemy.Width, medidaEnemy.Height, "invader", "invader" & enemyCount, brushEnemy)
        lEnemy.Add(tmpEnemy)
        enemyCount += 1

        Dim lastEnemy As Integer = lEnemy.Count - 1
        lEnemy(lastEnemy).tBrush.TranslateTransform(lEnemy(lastEnemy).puntoLocalizacion.X, lEnemy(lastEnemy).puntoLocalizacion.Y, MatrixOrder.Prepend)

    End Sub
    'Private Sub construccionSeleccionada(ByVal construccion As String)
    '    If construccion = "seleccionaBuilding" Then
    '        construcSel = construccion

    '    End If
    'End Sub
#End Region
#Region "Otros"
    Private Sub sonido(ByVal ruta As String)
        Dim sonidoT As New Thread(AddressOf procesaSonido)
        sonidoT.Start(ruta)
    End Sub
    Private Sub procesaSonido(ByVal ruta As String)
        mciSendString("close Sonido", Nothing, 0, 0)
        mciSendString("open " & """" & ruta & """" & " type mpegvideo alias Sonido", String.Empty, 0, 0)
        mciSendString("play Sonido", String.Empty, 0, 0)
        mciSendString("setaudio Sonido volume to " & My.Settings.volumenSonido, Nothing, 0, 0)
    End Sub
    Private Sub temporizador(ByVal tObjeto As tObjeto)
        Dim tTime As New Thread(AddressOf procesoTemporizador)
        tTime.Start(tObjeto)
    End Sub
    Private Sub procesoTemporizador(ByVal tObjeto As tObjeto)
        Dim tmpTimer As New Stopwatch

        tmpTimer.Start()
        Dim counter As Integer = 0
        tObjeto.enUso = True
        Do While (tObjeto.tiempo > counter)
            startTick = timer.ElapsedMilliseconds
            counter = CInt(tmpTimer.ElapsedMilliseconds)
            Do While timer.ElapsedMilliseconds - startTick < interval

            Loop
        Loop
        tObjeto.enUso = False
    End Sub

    Private Sub limpiaArrays(ByRef array() As Objeto)
        'Thread.Sleep(1000)
        ReDim Preserve array(UBound(array) - 1)
    End Sub
    'Private Sub procesaMontashield()
    '    Dim sh As Short = UBound(building) - 1
    '    'Dim cil As Integer = UBound(cilindro) - 1
    '    Dim ciclos As Short = 18
    '    cilindro(sh).bitmapImage = New Bitmap(My.Resources.cilindroShield)
    '    For i = 0 To pbShield.Length - 1
    '        building(sh).bitmapImage = New Bitmap(pbShield(i))
    '        If i <= 5 Then
    '            For k = 0 To ciclos
    '                cilindro(sh).y -= 1
    '                Thread.Sleep(10)
    '            Next
    '        End If
    '        If i = 4 Then
    '            cilindro(sh).bitmapImage = New Bitmap(My.Resources.puntaShield)
    '            cilindro(sh).y += 20
    '            ciclos = 40
    '        End If
    '    Next
    '    sonido(Application.StartupPath & "\Efecto_energia_montando4Edit2.wav")
    '    For i = 0 To 10
    '        building(sh).bitmapImage = New Bitmap(My.Resources.shield8RedCentrado)
    '        Thread.Sleep(20)
    '        building(sh).bitmapImage = New Bitmap(My.Resources.shield9RedCentrado)
    '        Thread.Sleep(10)
    '    Next
    '    cilindro(sh).acabado = True
    '    'limpiaArrays(cilindro)
    'End Sub
    Private Sub stopGame()
        timer.Stop()
    End Sub
    Private Sub startGame()
        timer.Start()
    End Sub
#End Region

End Class