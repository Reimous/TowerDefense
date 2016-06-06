Imports System.Drawing.Drawing2D
Imports System.Threading

Partial Public Class TowerDefense
    Private Sub creaCuadros2()
        Dim i As Integer = 0
        Dim fila As Integer = 0
        Dim columna As Integer = 0
        Dim tmpX As Single = posPantalla.X + (ClientSize.Width / 2 - medidaTablero.Width * (medidaCuadro.Width / 2))
        Dim tmpY As Single = posPantalla.Y + ClientSize.Height / 2
        Dim col = 1
        'Dim tmpBrush As TextureBrush = escalaImagenObjeto2(medidaCuadro.Width, medidaCuadro.Height, My.Resources.grass)

        While columna <= medidaTablero.Width - 1
            While fila <= medidaTablero.Height - 1
                ReDim Preserve cuadro2(UBound(cuadro2) + 1)

                cuadro2(i) = New Cuadro(New PointF(tmpX, tmpY), New PointF(tmpX + medidaCuadro.Width / 2, tmpY - medidaCuadro.Height / 2), New PointF(tmpX + medidaCuadro.Width, tmpY), New PointF(tmpX + medidaCuadro.Width / 2, tmpY + medidaCuadro.Height / 2), "c" & columna & "f" & fila, i, col)
                'Esto esta comentado porque si dibujo un brush por cada cuadrado va muy lento, al menos con el clamp y el translatetransform
                'cuadro2(i).tBrush = New TextureBrush(bitmapDestino)
                'cuadro2(i).tBrush.WrapMode = WrapMode.Clamp
                'cuadro2(i).tBrush.TranslateTransform(cuadro2(i).A.X, cuadro2(i).A.Y - medidaCuadro.Height / 2, MatrixOrder.Prepend)
                tmpY += medidaCuadro.Height / 2
                tmpX += medidaCuadro.Width / 2
                i += 1
                fila += 1
            End While

            If col = 1 Then
                col = 2
            Else
                col = 1
            End If
            fila = 0
            columna += 1

            tmpX = posPantalla.X + medidaCuadro.Width / 2 * columna + (ClientSize.Width / 2 - medidaTablero.Width * (medidaCuadro.Width / 2))
            tmpY = posPantalla.Y + ClientSize.Height / 2 - medidaCuadro.Height / 2 * columna
        End While

        ReDim Preserve cuadro2(UBound(cuadro2) + 1)
        cuadro2(i) = New Cuadro(New PointF(tmpX, tmpY), New PointF(tmpX + medidaCuadro.Width / 2, tmpY - medidaCuadro.Height / 2), New PointF(tmpX + medidaCuadro.Width, tmpY), New PointF(tmpX + medidaCuadro.Width / 2, tmpY + medidaCuadro.Height / 2), "c" & columna & "f" & fila, i, col)
        'tmpBrush.Dispose()
        tab = New PointF() {cuadro2(0).A, cuadro2(UBound(cuadro2) - medidaTablero.Height).B, cuadro2(UBound(cuadro2) - 1).C, cuadro2(medidaTablero.Width - 1).D}
        'tab2 = New Point() {New Point(0, 0), New Point(0, hipTablero), New Point(hipTablero, hipTablero), New Point(hipTablero, 0)}

        'btnUp.Location = New Point(CInt(tab(1).X), CInt(tab(1).Y))
        'btnDown.Location = New Point(CInt(tab(3).X), CInt(tab(3).Y))
        'btnLeft.Location = New Point(CInt(tab(0).X), CInt(tab(0).Y))
        'btnRight.Location = New Point(CInt(tab(2).X), CInt(tab(2).Y))
        'btnUp.Location = New Point(50, 10)
        'btnDown.Location = New Point(50, 90)
        'btnLeft.Location = New Point(10, 50)
        'btnRight.Location = New Point(100, 50)

    End Sub

    Private Sub GameLoop()

        timer.Start()
        Timer1.Start()
        gestionWindow()

        Do While (gameover = False)
            startTick = timer.ElapsedMilliseconds

            ToolStripLabel1.Text = "Cuadros: " & UBound(cuadro2)
            ToolStripLabel2.Text = "clientsize: " & ClientSize.Width & "x" & ClientSize.Height
            'ToolStripLabel3.Text = "pospantalla: " & posPantalla.X & " " & posPantalla.Y
            ToolStripLabel4.Text = "scrollValue: " & scrollValue
            ToolStripLabel5.Text = "medidaCuadro: " & medidaCuadro.Width
            ToolStripLabel6.Text = "mlocX: " & mloc.X & " mlocY: " & mloc.Y

            tiempo = CInt(timer.ElapsedMilliseconds / 1000)


            If Not paused Then

                logicaJuego()
            End If

            renderScene()

            Application.DoEvents()

            ' Forzamos una recolección de elementos no utilizados
            GC.Collect()
            GC.WaitForPendingFinalizers()

            Do While timer.ElapsedMilliseconds - startTick < interval

            Loop
        Loop

        Dim result As DialogResult
        result = MessageBox.Show("Volver a intentar?", "Game Ovaer", MessageBoxButtons.YesNo, MessageBoxIcon.None)
    End Sub

    Private Sub logicaJuego()

        proyMove()
        enemyAtack()
        buildingDef()
        eliminaObjetos()
        

    End Sub
    Private Sub enemyAtack()
        'For i = 0 To UBound(Enemy) - 1
        'For i = 0 To lEnemy.Count
        If lEnemy IsNot Nothing Then
            For Each en As Enemy In lEnemy
                'objetivoObject(en, lBuilding, medidaBuilding)
                objetivoEnemy(en)
                mueveEnemy(en)
                'dispara(en,lBuilding)
                disparaEnemy(en)
            Next
        End If
    End Sub
    Private Sub buildingDef()

        'For i = 0 To UBound(building) - 1
        If lBuilding IsNot Nothing Then
            For Each bu As Objeto In lBuilding
                'objetivoObject(bu, lEnemy, medidaEnemy)
                objetivoBuilding(bu)
                'apunta(bu)
                'dispara(bu,lEnemy)
                disparaBuilding(bu)
            Next
        End If
    End Sub
    Private Sub proyMove()
        If lProy IsNot Nothing Then
            'Dim i As Integer = 0
            'For Each proy As Proyectil In lProy
            'For i = 0 To lProy.Count - 1
            mueveProyectil()
            colisionProy()
            'i += 1
            'next
        End If
    End Sub
    Private Sub eliminaObjetos()
        lProy.RemoveAll(AddressOf eliminaProy)
        lEnemy.RemoveAll(AddressOf eliminaEn)
        lBuilding.RemoveAll(AddressOf eliminaBu)
    End Sub

    Private Shared Function eliminaProy(ByVal proy As Proyectil) As Boolean
        Return proy.eliminar
    End Function
    Private Shared Function eliminaEn(ByVal en As Enemy) As Boolean
        Return en.eliminar
    End Function
    Private Shared Function eliminaBu(ByVal bu As Objeto) As Boolean
        Return bu.eliminar
    End Function

    Private Sub objetivoEnemy(ByVal enem As Enemy)
        Dim distanciaMasCorta As Single
        'For i = 0 To UBound(objetivo) - 1
        If lBuilding IsNot Nothing Then
            Dim start As Boolean = True
            If Not compruebaObjetivoATiroEnemy(enem) Then
                'If Not enem.timerObjetivo.enUso Then

                'temporizador(enem.timerObjetivo)
                For Each obj As Objeto In lBuilding

                    Dim distanciaActualX As Single = Math.Abs((enem.puntoLocalizacionCentrado.X) / 2 - (obj.puntoLocalizacionCentrado.X) / 2)
                    Dim distanciaActualY As Single = Math.Abs((enem.puntoLocalizacionCentrado.Y) / 2 - (obj.puntoLocalizacionCentrado.Y) / 2)
                    Dim distanciaActual As Single = Math.Sqrt(Math.Pow(distanciaActualX, 2) + Math.Pow(distanciaActualY, 2))
                    If start Then
                        start = False
                        distanciaMasCorta = distanciaActual
                        enem.puntoObjetivo = New PointF(obj.puntoLocalizacionCentrado.X, obj.puntoLocalizacionCentrado.Y)
                        enem.distObjetivo = distanciaActual
                        'If enem.tipo = "invader" Then
                        enem.idObjetivo = obj.id
                        'End If
                    End If
                    If distanciaActual < distanciaMasCorta Then
                        enem.puntoObjetivo = New PointF(obj.puntoLocalizacionCentrado.X, obj.puntoLocalizacionCentrado.Y)
                        enem.distObjetivo = distanciaActual
                        enem.idObjetivo = obj.id
                    End If
                    distanciaMasCorta = Math.Min(distanciaMasCorta, distanciaActual)

                Next
            End If
            'End If
            'enem.puntoObjetivo = mloc
        End If

        'If enem.tipo = "invader" Then
        'If enem.idObjetivoAnterior <> enem.idObjetivo Then
        'Dim p As PointF = New PointF(enem.puntoLocalizacion.x + enem.w / 2, enem.puntoLocalizacion.y + enem.h / 2)


        enem.anguloObjetivo = Angle(enem.puntoLocalizacion, enem.puntoObjetivo) 'el objetivo de las defensas son los enemigos y viceversa
        'enem.anguloObjetivo = Angle(enem.puntoLocalizacion, mloc) 'el objetivo de las defensas y enemigos es el ratón

        'End If
        'End If
    End Sub
    Private Sub objetivoBuilding(ByVal objeto As Objeto)
        Dim distanciaMasCorta As Single
        'If objeto.tipo = "invader" Then
        '    objeto.idObjetivoAnterior = objeto.idObjetivo
        'End If
        'For i = 0 To UBound(objetivo) - 1
        'If Not objeto.timerObjetivo.enUso Then
        If lEnemy IsNot Nothing Then
            Dim start As Boolean = True
            If Not compruebaObjetivoATiroBuilding(objeto) Then


                For Each obj As Enemy In lEnemy

                    Dim distanciaActualX As Single = Math.Abs((objeto.puntoLocalizacionCentrado.X) / 2 - (obj.puntoLocalizacionCentrado.X) / 2)
                    Dim distanciaActualY As Single = Math.Abs((objeto.puntoLocalizacionCentrado.Y) / 2 - (obj.puntoLocalizacionCentrado.Y) / 2)
                    Dim distanciaActual As Single = Math.Sqrt(Math.Pow(distanciaActualX, 2) + Math.Pow(distanciaActualY, 2))
                    If start Then
                        start = False
                        distanciaMasCorta = distanciaActual
                        objeto.puntoObjetivo = New PointF(obj.puntoLocalizacionCentrado.X, obj.puntoLocalizacionCentrado.Y)
                        objeto.distObjetivo = distanciaActual
                        'If objeto.tipo = "invader" Then
                        objeto.idObjetivo = obj.id
                        'End If
                    End If
                    If distanciaActual < distanciaMasCorta Then
                        objeto.puntoObjetivo = New PointF(obj.puntoLocalizacionCentrado.X, obj.puntoLocalizacionCentrado.Y)
                        objeto.distObjetivo = distanciaActual
                        objeto.idObjetivo = obj.id
                    End If
                    distanciaMasCorta = Math.Min(distanciaMasCorta, distanciaActual)


                Next

            End If
        End If

        'If objeto.tipo = "invader" Then
        'If objeto.idObjetivoAnterior <> objeto.idObjetivo Then
        'Dim p As PointF = New PointF(objeto.puntoLocalizacion.x + objeto.w / 2, objeto.puntoLocalizacion.y + objeto.h / 2)

        objeto.anguloObjetivo = Angle(objeto.puntoLocalizacion, objeto.puntoObjetivo) 'el objetivo de las defensas son los enemigos y viceversa
        'objeto.anguloObjetivo = Angle(objeto.puntoLocalizacion, mloc) 'el objetivo de las defensas y enemigos es el ratón

        'temporizador(objeto.timerObjetivo)
        'End If
        'End If
        'End If
    End Sub
  
    Private Sub disparaBuilding(ByVal objeto As Objeto)
        'For i = 0 To UBound(objeto2) - 1
        If lEnemy IsNot Nothing Then
            'Dim i As Integer = 0
            For Each ob2 As Enemy In lEnemy

                If ob2.id = objeto.idObjetivo Then
                    'Dim puntoO1 As New Point(objeto.rObjeto.x + objeto.rAtaque.width / 2, objeto.rObjeto.y + objeto.rAtaque.height / 2)
                    Dim puntoO1 As New Point(objeto.puntoLocalizacionCentrado.X, objeto.puntoLocalizacionCentrado.Y)
                    Dim puntoO2 As New Point(ob2.puntoLocalizacionCentrado.X, ob2.puntoLocalizacionCentrado.Y)
                    Dim num1 As Single = Math.Pow(puntoO2.X - puntoO1.X, 2)
                    Dim num2 As Single = Math.Pow(puntoO2.Y - puntoO1.Y, 2)

                    Dim res As Single = num1 / Math.Pow(objeto.rAtaque.Width / 2, 2) + num2 / Math.Pow(objeto.rAtaque.Height / 2, 2)
                    If res <= 1 Then 'dentro
                        ob2.idArea = objeto.id
                        If timer.ElapsedMilliseconds > objeto.timerDisparando + objeto.esperaDisparando Then

                            apunta(objeto)
                            'Dim rnd As New Random
                            'Dim num As Integer
                            'num = rnd.Next(1, 2)

                            sonido(Application.StartupPath & "\sonidos\artyPlasma.wav")

                            'proy.anguloObjetivo = Angle(proy.puntoLocalizacion, proy.puntoObjetivo)
                            Dim proyectil As New Proyectil(objeto.puntoLocalizacionCentrado.X, objeto.puntoLocalizacionCentrado.Y, 5, 5, objeto.tipo, "proyectil", objeto.anguloObjetivo, objeto.idObjetivo, objeto.puntoObjetivo, Nothing)
                            lProy.Add(proyectil)

                            objeto.timerDisparando = timer.ElapsedMilliseconds
                            'ob2.vida -= objeto.daño
                            'If ob2.vida <= 0 Then
                            '    lEnemy.RemoveAt(i)
                            '    Exit Sub
                            'End If
                        End If
                    Else 'fuera

                    End If
                End If
                'i += 1
            Next
        End If
    End Sub
    Private Sub disparaEnemy(ByVal objeto As Enemy)
        'For i = 0 To UBound(objeto2) - 1
        If lBuilding IsNot Nothing Then
            'Dim i As Integer = 0
            For Each ob2 As Objeto In lBuilding

                If ob2.id = objeto.idObjetivo Then
                    'Dim puntoO1 As New Point(objeto.rObjeto.x + objeto.rAtaque.width / 2, objeto.rObjeto.y + objeto.rAtaque.height / 2)
                    Dim puntoO1 As New Point(objeto.puntoLocalizacionCentrado.X, objeto.puntoLocalizacionCentrado.Y)
                    Dim puntoO2 As New Point(ob2.puntoLocalizacionCentrado.X, ob2.puntoLocalizacionCentrado.Y)
                    Dim num1 As Single = Math.Pow(puntoO2.X - puntoO1.X, 2)
                    Dim num2 As Single = Math.Pow(puntoO2.Y - puntoO1.Y, 2)

                    Dim res As Single = num1 / Math.Pow(objeto.rAtaque.Width / 2, 2) + num2 / Math.Pow(objeto.rAtaque.Height / 2, 2)
                    If res <= 1 Then 'dentro
                        ob2.idArea = objeto.id
                        If timer.ElapsedMilliseconds > objeto.timerDisparando + objeto.esperaDisparando Then

                            objeto.mov = 0

                            sonido(Application.StartupPath & "\sonidos\short-laser.wav")
                            Dim proyectil As New Proyectil(objeto.puntoLocalizacionCentrado.X, objeto.puntoLocalizacionCentrado.Y, 3, 3, objeto.tipo, "proyectil", objeto.anguloObjetivo, objeto.idObjetivo, objeto.puntoObjetivo, Nothing)
                            lProy.Add(proyectil)

                            objeto.timerDisparando = timer.ElapsedMilliseconds
                            'ob2.vida -= objeto.daño
                            'If ob2.vida <= 0 Then
                            '    lBuilding.RemoveAt(i)
                            '    Exit Sub
                            'End If
                        End If
                    Else 'fuera

                        If objeto.mov = 0 Then
                            objeto.mov = objeto.movConst
                        End If

                    End If
                End If
                'i += 1
            Next
        End If
    End Sub

    Private Sub colisionProy()
        Dim sinObjetivo As Boolean = True
        If lProy IsNot Nothing Then
            For Each proy As Proyectil In lProy
                'Dim sinObjetivo As Boolean = True
                If proy.tipo = "building" Then
                    For Each en As Enemy In lEnemy

                        If proy.idObjetivo = en.id Then
                            sinObjetivo = False
                            If proy.puntoLocalizacionCentrado.X >= en.puntoLocalizacion.X And proy.puntoLocalizacionCentrado.X <= en.puntoLocalizacion.X + en.size.Width _
                               And proy.puntoLocalizacionCentrado.Y >= en.puntoLocalizacion.Y And proy.puntoLocalizacionCentrado.Y <= en.puntoLocalizacion.Y + en.size.Height Then
                                en.vida -= proy.daño
                                If en.vida <= 0 Then
                                    en.eliminar = True
                                End If
                                proy.eliminar = True

                            Else 'si no ha chocado actualizamos el punto objetivo para que le siga bien
                                proy.puntoObjetivo = en.puntoLocalizacionCentrado
                            End If

                        End If

                    Next
                ElseIf proy.tipo = "invader" Then
                    For Each bu As Objeto In lBuilding

                        If proy.idObjetivo = bu.id Then
                            sinObjetivo = False
                            Dim correcion As SizeF = New Size(medidaBuilding.Width / 4, medidaBuilding.Height / 4)
                            If proy.puntoLocalizacionCentrado.X >= bu.puntoLocalizacion.X + correcion.Width And proy.puntoLocalizacionCentrado.X <= bu.puntoLocalizacion.X + bu.size.Width - correcion.Width _
                               And proy.puntoLocalizacionCentrado.Y >= bu.puntoLocalizacion.Y + correcion.Height And proy.puntoLocalizacionCentrado.Y <= bu.puntoLocalizacion.Y + bu.size.Height - correcion.Height Then
                                bu.vida -= proy.daño
                                If bu.vida <= 0 Then
                                    bu.eliminar = True
                                    'Iteración para establecer que el cuadro esta ocupado por la estructura
                                    For Each cuad As Cuadro In cuadro2
                                        If bu.cuadro = cuad.id Then
                                            cuad.ocupado = False
                                        End If
                                    Next
                                End If
                                proy.eliminar = True
                            Else
                                proy.puntoObjetivo = bu.puntoLocalizacionCentrado
                            End If
                        End If

                    Next
                End If
                If sinObjetivo Then
                    proy.eliminar = True
                End If
            Next
        End If
    End Sub
  
   
    'Con esta funcion comprobamos si el objetivo de la defensa sigue estando a tiro y así no cambia de objetivo hasta que no muera o salga de rango
    Private Function compruebaObjetivoATiro(ByVal objeto As Object, ByVal objetivo As Object) As Boolean
        Dim objetivoAtiro As Boolean = False
        For Each obj As Object In objetivo
            If objeto.id = obj.idArea Then
                objetivoAtiro = True
                'ElseIf Not objeto.idObjetivo = Nothing Then
            End If
            Return objetivoAtiro
        Next
    End Function
    Private Function compruebaObjetivoATiroEnemy(ByVal enem As Enemy) As Boolean
        Dim objetivoAtiro As Boolean = False
        For Each obj As Objeto In lBuilding
            If enem.id = obj.idArea Then
                objetivoAtiro = True
                'ElseIf Not objeto.idObjetivo = Nothing Then
            End If
            Return objetivoAtiro
        Next
    End Function
    Private Function compruebaObjetivoATiroBuilding(ByVal objeto As Objeto) As Boolean
        Dim objetivoAtiro As Boolean = False
        For Each obj As Enemy In lEnemy
            If objeto.id = obj.idArea Then
                objetivoAtiro = True
                'ElseIf Not objeto.idObjetivo = Nothing Then
            End If
            Return objetivoAtiro
        Next
    End Function

    Private Sub mueveEnemy(ByVal enem As Enemy)

        If Not enem.mov = 0 Or lBuilding Is Nothing Then

            'Dim orientacion As Boolean = False
            For Each en As Enemy In lEnemy 'Comprobamos si el enemigo en cuestión esta colisionando con otro

                If enem.puntoLocalizacionCentrado.X >= en.puntoLocalizacion.X And enem.puntoLocalizacionCentrado.X <= en.puntoLocalizacion.X + en.size.Width _
                                   And enem.puntoLocalizacionCentrado.Y >= en.puntoLocalizacion.Y And enem.puntoLocalizacionCentrado.Y <= en.puntoLocalizacion.Y + en.size.Height And Not enem.id = en.id Then
                    'If orientacion Then
                    'orientacion = False
                    enem.anguloObjetivo += 0.5
                    'Else
                    'orientacion = True
                    'enem.anguloObjetivo -= 20
                    'End If
                End If
            Next

            enem.movX = Math.Cos(enem.anguloObjetivo) * enem.mov
            enem.movY = Math.Sin(enem.anguloObjetivo) * enem.mov
            'End If
          
            'ToolStripLabel12.Text = "angulo: " & enem.anguloObjetivo & " objetivo: " & enem.distObjetivo
            enem.puntoLocalizacion = New PointF(enem.puntoLocalizacion.X + enem.movX, enem.puntoLocalizacion.Y + enem.movY)

            enem.tBrush.TranslateTransform(enem.movX, enem.movY, MatrixOrder.Prepend)

            Dim distX As Single = Math.Abs(enem.puntoLocalizacion.X - tab(0).X)
            Dim distY As Single = Math.Abs(enem.puntoLocalizacion.Y - tab(1).Y)


            enem.porcPosRel = New PointF((distX * 100) / (medidaCuadro.Width * medidaTablero.Width), (distY * 100) / (medidaCuadro.Height * medidaTablero.Height))

        End If
    End Sub
    Private Sub mueveProyectil()
        If lProy IsNot Nothing Then
            For Each proy As Proyectil In lProy
                'If Not enem.mov = 0 Or lBuilding Is Nothing Then
                Dim angulo As Single = Angle(proy.puntoLocalizacion, proy.puntoObjetivo)

                'proy.movX = Math.Cos(proy.anguloObjetivo) * proy.mov
                'proy.movY = Math.Sin(proy.anguloObjetivo) * proy.mov
                proy.movX = Math.Cos(angulo) * proy.mov
                proy.movY = Math.Sin(angulo) * proy.mov
                'End If

                'ToolStripLabel12.Text = "angulo: " & proy.anguloObjetivo & " objetivo: " & proy.distObjetivo
                proy.puntoLocalizacion = New PointF(proy.puntoLocalizacion.X + proy.movX, proy.puntoLocalizacion.Y + proy.movY)

                'proy.tBrush.TranslateTransform(proy.movX, proy.movY, MatrixOrder.Prepend)

                Dim distX As Single = Math.Abs(proy.puntoLocalizacion.X - tab(0).X)
                Dim distY As Single = Math.Abs(proy.puntoLocalizacion.Y - tab(1).Y)



                proy.porcPosRel = New PointF((distX * 100) / (medidaCuadro.Width * medidaTablero.Width), (distY * 100) / (medidaCuadro.Height * medidaTablero.Height))


                'End If
            Next
        End If
    End Sub

    Private Sub apunta(ByVal objeto As Objeto)

        'REVISAR ESTA FUNCION YA QUE NO PARECE FUNCIONAR CORRECTAMENTE
        Dim modificado As Boolean = False
        'Dim angulo As Single = Angle(objeto.puntoLocalizacion, objeto.puntoObjetivo)
        'escalaImagenObjeto(medidaBuilding.Width, medidaBuilding.Height, My.Resources.Cañon1Final, brushBuilding(0), WrapMode.Clamp)
        Dim anguloActual As Single = objeto.anguloObjetivo * 180 / Math.PI
        Dim anguloAnterior As Single = objeto.anguloAnterior


        'Como al empezar el angulo anterior es 0 entonces si pones un enemigo por la derecha no gira el cañon
        If anguloActual <= 22 And anguloActual > -22 Then
            If anguloAnterior >= 22 Or anguloAnterior < -22 Then
                escalaImagenObjeto(medidaBuilding.Width, medidaBuilding.Height, My.Resources.arty8, brushBuilding, WrapMode.Clamp)
                objeto.tBrush = brushBuilding
                modificado = True
            End If
        ElseIf anguloActual <= -22 And anguloActual > -68 Then
            If anguloAnterior >= -22 Or anguloAnterior < -68 Then
                escalaImagenObjeto(medidaBuilding.Width, medidaBuilding.Height, My.Resources.arty7, brushBuilding, WrapMode.Clamp)
                objeto.tBrush = brushBuilding
                modificado = True
            End If
        ElseIf anguloActual <= -68 And anguloActual > -112 Then
            If anguloAnterior >= -68 Or anguloAnterior < -112 Then
                escalaImagenObjeto(medidaBuilding.Width, medidaBuilding.Height, My.Resources.arty6, brushBuilding, WrapMode.Clamp)
                objeto.tBrush = brushBuilding
                modificado = True
            End If
        ElseIf anguloActual <= -112 And anguloActual > -158 Then
            If anguloAnterior >= -112 Or anguloAnterior < -158 Then
                escalaImagenObjeto(medidaBuilding.Width, medidaBuilding.Height, My.Resources.arty5, brushBuilding, WrapMode.Clamp)
                objeto.tBrush = brushBuilding
                modificado = True
            End If
        ElseIf anguloActual <= -158 And anguloActual > -180 Or anguloActual <= 180 And anguloActual > 158 Then
            If anguloAnterior >= -158 Or anguloAnterior <= 158 Then
                escalaImagenObjeto(medidaBuilding.Width, medidaBuilding.Height, My.Resources.arty4, brushBuilding, WrapMode.Clamp)
                objeto.tBrush = brushBuilding
                modificado = True
            End If
        ElseIf anguloActual <= 158 And anguloActual > 112 Then
            If anguloAnterior >= 158 Or anguloAnterior < 112 Then
                escalaImagenObjeto(medidaBuilding.Width, medidaBuilding.Height, My.Resources.arty3, brushBuilding, WrapMode.Clamp)
                objeto.tBrush = brushBuilding
                modificado = True
            End If
        ElseIf anguloActual <= 112 And anguloActual > 68 Then
            If anguloAnterior >= 112 Or anguloAnterior < 68 Then
                escalaImagenObjeto(medidaBuilding.Width, medidaBuilding.Height, My.Resources.arty2, brushBuilding, WrapMode.Clamp)
                objeto.tBrush = brushBuilding
                modificado = True
            End If
        ElseIf anguloActual <= 68 And anguloActual > 22 Then
            If anguloAnterior >= 68 Or anguloAnterior < 22 Then
                escalaImagenObjeto(medidaBuilding.Width, medidaBuilding.Height, My.Resources.arty, brushBuilding, WrapMode.Clamp)
                objeto.tBrush = brushBuilding
                modificado = True
            End If
        End If

        objeto.anguloAnterior = anguloActual
        If modificado Then
            objeto.tBrush.TranslateTransform(objeto.puntoLocalizacion.X, objeto.puntoLocalizacion.Y, MatrixOrder.Prepend)
        End If
    End Sub
  
    'Public Function MoveBetweenPoints(ByVal point1 As Point, ByVal point2 As Point, ByVal percentage As Single) As Point
    '    Dim x As Integer
    '    Dim y As Integer

    '    x = point1.X * (1.0 - percentage) + point2.X * percentage
    '    y = point1.Y * (1.0 - percentage) + point2.Y * percentage

    '    Return New Point(x, y)
    '    'Return New Point(point1.X + Math.Abs(point2.X - point1.X) / 50, point1.Y + Angle(point1, point2) / 50)
    'End Function


    ''' <summary>
    ''' Calculates angle in radians between two points and x-axis.
    ''' </summary>
    Private Function Angle(ByVal p1 As PointF, ByVal p2 As PointF) As Single
        Dim deltaY As Single = p2.Y - p1.Y
        Dim deltaX As Single = p2.X - p1.X

        Return Math.Atan2(deltaY, deltaX) 'En grados* (180 / Math.PI)
    End Function
    Private Sub calcSF()
        Dim sfx As Single
        Dim sfy As Single
        ' Whichever is smaller is the one we'll use to maintain proportion and fill as much as possible
        For i = 0 To UBound(Configuracion.resValueW)
            If Configuracion.resValueW(i) <= Screen.PrimaryScreen.Bounds.Width Or Configuracion.resValueH(i) <= Screen.PrimaryScreen.Bounds.Height Then
                sfx = ClientSize.Width / Configuracion.resValueW(i) ' + scrollValue ' Get the ratio of the width change compared to our fixed size
                sfy = ClientSize.Height / Configuracion.resValueH(i) ' + scrollValue ' Get the ratio of the height change compared to our fixed size

                resSF(i) = Math.Min(sfx, sfy)
                ReDim Preserve resSF(UBound(resSF) + 1)
            End If
        Next
    End Sub
    Private Sub ajustaMedidas()
        'Dim tmpSF As Single
        If My.Settings.windowed Then
            'tmpSF = 1
            sf = 1
        Else
            'tmpSF = resSF(My.Settings.Res)
            sf = resSF(My.Settings.Res)
        End If

        medidaCuadro.Width = medidaCuadroConst.Width * (sf + scrollValue)
        medidaCuadro.Height = medidaCuadroConst.Height * (sf + scrollValue)
        medidaBuilding.Width = medidaBuildingConst.Width * (sf + scrollValue)
        medidaBuilding.Height = medidaBuildingConst.Height * (sf + scrollValue)
        'medidaCilindro.Width = medidaCilindroConst.Width * (sf + scrollValue)
        'medidaCilindro.Height = medidaCilindroConst.Height * (sf + scrollValue)
        medidaEnemy.Width = medidaEnemyConst.Width * (sf + scrollValue)
        medidaEnemy.Height = medidaEnemyConst.Height * (sf + scrollValue)


    End Sub
    Private Sub gestionWindow()

        If My.Settings.windowed Then

            Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
            ClientSize = New Size(Configuracion.resValueW(My.Settings.Res), Configuracion.resValueH(My.Settings.Res))
            Me.WindowState = FormWindowState.Normal
        Else
            ClientSize = New Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height)
            calcSF()
            Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
            Me.WindowState = FormWindowState.Maximized
        End If

        'Me.TopMost = True

        ajustaMedidas()
        ReDim Preserve cuadro2(UBound(cuadro2) - (UBound(cuadro2) + 1))
        creaCuadros2()
        ajustaObjetos()
        iniBrushes()
        'centrar()
    End Sub
    Private Sub iniBrushes()

        'escalaImagenObjeto(medidaBuilding.Width, medidaBuilding.Height, My.Resources.Artilery2Trans, brushBuilding(0), WrapMode.Clamp)
        'escalaImagenObjeto(medidaBuilding.Width, medidaBuilding.Height, My.Resources.Artilery2Trans, brushBuilding(1), WrapMode.Clamp)
        'escalaImagenObjeto(medidaBuilding.Width, medidaBuilding.Height, My.Resources.Artilery2Trans, brushBuilding(2), WrapMode.Clamp)
        'escalaImagenObjeto(medidaBuilding.Width, medidaBuilding.Height, My.Resources.Artilery2Trans, brushBuilding(3), WrapMode.Clamp)
        'escalaImagenObjeto(medidaBuilding.Width, medidaBuilding.Height, My.Resources.Artilery2Trans, brushBuilding(4), WrapMode.Clamp)
        'escalaImagenObjeto(medidaBuilding.Width, medidaBuilding.Height, My.Resources.Artilery2Trans, brushBuilding(5), WrapMode.Clamp)
        'escalaImagenObjeto(medidaBuilding.Width, medidaBuilding.Height, My.Resources.Artilery2Trans, brushBuilding(6), WrapMode.Clamp)
        'escalaImagenObjeto(medidaBuilding.Width, medidaBuilding.Height, My.Resources.arty, brushBuilding(7), WrapMode.Clamp)

    End Sub
    Private Sub centrar()
        mueveObjeto(ClientSize.Width / 2 - cuadroOverActual.A.X + medidaCuadro.Width / 2, ClientSize.Height / 2 - cuadroOverActual.A.Y)
    End Sub
    Private Sub ajustaObjetos()
        'For i = 0 To UBound(building) - 1
        If lBuilding IsNot Nothing Then
            For Each bu As Objeto In lBuilding
                escalaImagenObjeto(medidaBuilding.Width, medidaBuilding.Height, My.Resources.arty, brushBuilding, WrapMode.Clamp)
                'escalaImagenObjeto(medidaEnemy.Width, medidaEnemy.Height, My.Resources.EnemyInvader1, brushBuilding(0), WrapMode.Clamp)

                bu.size = New SizeF(medidaBuilding.Width, medidaBuilding.Height)
                For k = 0 To UBound(cuadro2) - 1
                    If cuadro2(k).id = bu.cuadro Then
                        bu.puntoLocalizacion = New PointF(cuadro2(k).A.X, cuadro2(k).A.Y - medidaBuilding.Height / 1.4)
                    End If
                Next
                bu.tBrush = brushBuilding
                bu.tBrush.TranslateTransform(bu.puntoLocalizacion.X, bu.puntoLocalizacion.Y, MatrixOrder.Prepend)
            Next
        End If
        escalaImagenObjeto(medidaCuadro.Width, medidaCuadro.Height, My.Resources.G000M800, brushSuelo, WrapMode.Tile)

        'For i = 0 To UBound(enemy) - 1
        'For i = 0 To lEnemy.Count
        If lEnemy IsNot Nothing Then
            For Each en As Enemy In lEnemy
                escalaImagenObjeto(medidaEnemy.Width, medidaEnemy.Height, My.Resources.EnemyInvader1, brushEnemy, WrapMode.Clamp)
                en.size = New SizeF(medidaEnemy.Width, medidaEnemy.Height)
                en.tBrush = brushEnemy
                en.puntoLocalizacion = New PointF(((en.porcPosRel.X * medidaCuadro.Width * medidaTablero.Width) / 100) + tab(0).X, ((en.porcPosRel.Y * medidaCuadro.Height * medidaTablero.Height) / 100) + tab(1).Y)
                en.tBrush.TranslateTransform(en.puntoLocalizacion.X, en.puntoLocalizacion.Y, MatrixOrder.Prepend)


                'en.mov = en.movConst * (sf + scrollValue) 'Provisional para que se adapte la velocidad segun el zoom
                'En este punto me planteo dejar el zoom de lado ya que no tiene mucho sentido en una aplicación 2D sin camara.
            Next
        End If

        If lProy IsNot Nothing Then
            For Each proy As Proyectil In lProy
                proy.puntoLocalizacion = New PointF(((proy.porcPosRel.X * medidaCuadro.Width * medidaTablero.Width) / 100) + tab(0).X, ((proy.porcPosRel.Y * medidaCuadro.Height * medidaTablero.Height) / 100) + tab(1).Y)
            Next
        End If

        If lUI IsNot Nothing Then
            For Each btn As UI In lUI
                If btn.tipo = "seleccionaConstruccion" Then
                    If btn.visible Then
                        Dim correcionBrush As Single = ClientSize.Height - 150 - btn.puntoLocalizacion.Y
                        btn.puntoLocalizacion = New Point(btn.puntoLocalizacion.X, ClientSize.Height - 150)
                        btn.tBrush.TranslateTransform(0, correcionBrush)
                    End If
                End If
            Next
        End If

        'Dim distX As Integer = Math.Abs(enem.x - tab(0).X)
        'Dim distY As Integer = Math.Abs(enem.y - tab(1).Y)


        'enem.porcPosRel = New Point((distX * 100) / (medidaCuadro.Width * medidaTablero.Width), (distY * 100) / (medidaCuadro.Height * medidaTablero.Height))


        'For i = 0 To UBound(cilindro) - 1
        '    cilindro(i).w = medidaCilindro.Width
        '    cilindro(i).h = medidaCilindro.Height
        'Next



    End Sub
End Class
