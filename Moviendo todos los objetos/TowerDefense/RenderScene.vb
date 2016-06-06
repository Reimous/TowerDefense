Imports System.Drawing.Drawing2D
Partial Public Class TowerDefense


    Private Sub renderScene()
        If ClientSize.Width > 0 Then 'Evitamos que pete cuando minimizamos
            backBuffer = New Bitmap(ClientSize.Width, ClientSize.Height)
            If Not paused Then
                'graphicsEscenario = Graphics.FromImage(backBuffer)

                graphicsCuadro = Graphics.FromImage(backBuffer)
                'For i = 0 To UBound(cuadro2) - 1
                '    cuadro2(i).graphics = Graphics.FromImage(backBuffer)
                'Next
                'If lBuilding IsNot Nothing Then
                '    'For i = 0 To UBound(building) - 1
                '    For Each bu As Objeto In lBuilding
                '        bu.graphics = Graphics.FromImage(backBuffer)
                '    Next
                'End If
                'For i = 0 To UBound(cilindro) - 1
                '    cilindro(i).graphics = Graphics.FromImage(backBuffer)
                'Next
                'For i = 0 To UBound(lEnemy) - 1
                'If lEnemy IsNot Nothing Then
                '    For Each en As Enemy In lEnemy
                '        en.graphics = Graphics.FromImage(backBuffer)
                '    Next
                'End If

                'If lUi IsNot Nothing Then
                '    For Each btn As UI In lUi
                '        If btn.visible Then
                '            btn.graphics = Graphics.FromImage(backBuffer)
                '        End If
                '    Next
                'End If

                pbSurface.Image = Nothing

                dibujaTablero()

                'graphicsEscenario.DrawImage(pbImage, 0, 0)
                'dibujaEscenario()

                'For i = 0 To UBound(cuadro2) - 1
                '    dibujaCuadro(cuadro2(i))
                'Next
                'For i = 0 To UBound(cilindro) - 1
                '    dibujaObjeto(cilindro(i))
                'Next

                If lBuilding IsNot Nothing Then
                    'For i = UBound(building) - 1 To 0 Step -1
                    For i = lBuilding.Count - 1 To 0 Step -1
                        'dibujaObjeto(building(i))
                        dibujaObjeto2(lBuilding(i))
                    Next
                End If
                If lEnemy IsNot Nothing Then
                    'For i = UBound(Enemy) - 1 To 0 Step -1
                    For i = lEnemy.Count - 1 To 0 Step -1
                        dibujaEnemy(lEnemy(i))
                    Next
                End If
                If lProy IsNot Nothing Then
                    For i = lProy.Count - 1 To 0 Step -1
                        dibujaProy(lProy(i))
                    Next
                End If
                dibujaEnPuntero()

                Dim g As Graphics = Graphics.FromImage(backBuffer)
                Dim rectCenter As Rectangle = New Rectangle(ClientSize.Width / 2, ClientSize.Height / 2, 15, 15)


                'g.DrawRectangle(Pens.Aqua, rectCenter)

                If lUI IsNot Nothing Then
                    For Each btn As UI In lUI
                        If btn.visible Then
                            dibujaUI(btn)
                        End If
                    Next
                End If


                tiempoTranscurrido()
            Else
                gamePaused()
            End If

            pbSurface.Image = backBuffer

        End If
    End Sub
    Private Sub gamePaused()
        Dim drawFont As New Font("Arial", 20)
        Dim drawBrush As New SolidBrush(Color.White)
        Dim g As Graphics = Graphics.FromImage(backBuffer)
        Dim x As Integer = 200
        Dim y As Integer = 200
        For Each bu As Objeto In lBuilding
            g.DrawString(bu.id & " AnguloObjetivo: " & bu.anguloObjetivo & " PObjetivo: " & bu.puntoObjetivo.X & _
                         ", " & bu.puntoObjetivo.Y & " PLocalizacion: " & bu.puntoLocalizacion.X & _
                         ", " & bu.puntoLocalizacion.Y, drawFont, drawBrush, x, y)
            y += 25
        Next
        For Each en As Enemy In lEnemy
            g.DrawString(en.id & " AnguloObjetivo: " & en.anguloObjetivo & " PuntoObjetivo: " & en.puntoObjetivo.X & _
                         ", " & en.puntoObjetivo.Y & "distObjetivo: " & en.distObjetivo, drawFont, drawBrush, x, y)
            y += 25
        Next
        g.Dispose()
    End Sub
    Private Sub tiempoTranscurrido()
        Dim drawFont As New Font("Arial", 16)
        Dim drawBrush As New SolidBrush(Color.White)
        Dim g As Graphics = Graphics.FromImage(backBuffer)
        g.DrawString("Time: " & tiempo, drawFont, drawBrush, ClientSize.Width - 120, ClientSize.Height - 30)
        g.Dispose()
    End Sub
    Private Sub dibujaTablero()

        Dim i As UShort = 0
        Dim fila As UShort = 0
        Dim columna As UShort = 0

        'brushSuelo.TranslateTransform(1, 1, MatrixOrder.Prepend) ' Curioso efecto, se mueve infinito

        graphicsCuadro.FillPolygon(brushSuelo, tab) 'Dibujamos el brush en todo el tablero
        graphicsCuadropath = New GraphicsPath

        'graphicsCuadro.DrawImage(pbImage, cuadro2(0).A.X, cuadro2(0).A.Y - medidaCuadro.Height / 2)

        While columna <= medidaTablero.Width - 1
            While fila <= medidaTablero.Height - 1

                If cuadro2(i).A.X <= ClientSize.Width And cuadro2(i).A.Y + medidaCuadro.Height >= 0 And cuadro2(i).A.Y <= ClientSize.Height + medidaCuadro.Height And cuadro2(i).A.X + medidaCuadro.Width >= 0 Then
                    If cuadricula Then
                        graphicsCuadropath.AddPolygon(cuadro2(i).polygon) 'Dibujamos los poligonos
                    End If

                    'graphicsCuadro.FillPolygon(brushSuelo, cuadro2(i).polygon) 'Dibujamos el brush en cada cuadro

                    If cuadro2(i).mouseOver Then
                        Dim semiTransBrush As New SolidBrush(Color.FromArgb(90, 255, 255, 255))
                        graphicsCuadro.FillPolygon(semiTransBrush, cuadro2(i).polygon)
                    End If
                End If
                i += 1
                fila += 1

            End While
            fila = 0
            columna += 1
        End While
        Dim whitePen As New Pen(Color.FromArgb(255, 255, 255, 255), 0.5)

        graphicsCuadro.DrawPath(whitePen, graphicsCuadropath)

        'Dim circle() As PointF
        'graphicsCuadro.DrawEllipse(Pens.Yellow, tab(0).X, tab(0).Y, 20, 20)
        'graphicsCuadro.DrawEllipse(Pens.Yellow, tab(1).X, tab(1).Y, 20, 20)
        'graphicsCuadro.DrawEllipse(Pens.Yellow, tab(2).X, tab(2).Y, 20, 20)
        'graphicsCuadro.DrawEllipse(Pens.Yellow, tab(3).X, tab(3).Y, 20, 20)


        graphicsCuadropath.Dispose()
        graphicsCuadro.Dispose()
    End Sub

    'Private Sub dibujaObjeto(ByVal objeto As Objeto)
    '    If objeto.x <= ClientSize.Width And objeto.y + objeto.h >= 0 And objeto.y <= ClientSize.Height And objeto.x + objeto.w >= 0 And Not objeto.acabado Then
    '        objeto.graphics.DrawImage(objeto.bitmapImage, objeto.rObjeto)
    '        objeto.graphics.Dispose()
    '    End If
    'End Sub
    Private Sub dibujaObjeto2(ByVal objeto As Objeto)
        If objeto.puntoLocalizacion.X <= ClientSize.Width And objeto.puntoLocalizacion.Y + objeto.size.Height >= 0 And objeto.puntoLocalizacion.Y <= ClientSize.Height And objeto.puntoLocalizacion.X + objeto.size.Width >= 0 Then

            Dim g As Graphics = Graphics.FromImage(backBuffer)

            Dim vidaPen As Pen
            Dim porcentaje As Single = (objeto.vida * 100) / objeto.vidaTotal
            If porcentaje >= 66 Then
                vidaPen = New Pen(Color.FromArgb(255, 0, 255, 0), 5)
            ElseIf porcentaje >= 33 Then
                vidaPen = New Pen(Color.FromArgb(255, 255, 255, 0), 5)
            Else
                vidaPen = New Pen(Color.FromArgb(255, 255, 0, 0), 5)
            End If




            If cuadroOverActual IsNot Nothing Then
                If objeto.cuadro = cuadroOverActual.id Then
                    'g.DrawRectangle(Pens.Blue, objeto.rObjeto)
                    g.DrawEllipse(Pens.Orange, objeto.rAtaque)
                End If
            End If

            g.FillRectangle(objeto.tBrush, objeto.rObjeto)
            If objeto.vida < objeto.vidaTotal Then
                g.DrawLine(vidaPen, objeto.puntoLocalizacion.X, objeto.puntoLocalizacion.Y - 5, objeto.puntoLocalizacion.X + objeto.size.Width * porcentaje / 100, objeto.puntoLocalizacion.Y - 5)
            End If

            g.Dispose()
        End If
    End Sub
    Private Sub dibujaEnemy(ByVal enem As Enemy)
        If enem.puntoLocalizacion.X <= ClientSize.Width And enem.puntoLocalizacion.Y + enem.size.Height >= 0 And enem.puntoLocalizacion.Y <= ClientSize.Height And enem.puntoLocalizacion.X + enem.size.Width >= 0 Then

            Dim g As Graphics = Graphics.FromImage(backBuffer)
            Dim vidaPen As Pen
            Dim porcentaje As Single = (enem.vida * 100) / enem.vidaTotal
            If porcentaje >= 66 Then
                vidaPen = New Pen(Color.FromArgb(255, 0, 255, 0), 5)
            ElseIf porcentaje >= 33 Then
                vidaPen = New Pen(Color.FromArgb(255, 255, 255, 0), 5)
            Else
                vidaPen = New Pen(Color.FromArgb(255, 255, 0, 0), 5)
            End If


            'g.DrawRectangle(Pens.Blue, enem.rObjeto)

            'g.DrawEllipse(Pens.Yellow, enem.rAtaque)


            g.FillRectangle(enem.tBrush, enem.rObjeto)


            If enem.vida < enem.vidaTotal Then
                g.DrawLine(vidaPen, enem.puntoLocalizacion.X, enem.puntoLocalizacion.Y - 5, enem.puntoLocalizacion.X + enem.size.Width * porcentaje / 100, enem.puntoLocalizacion.Y - 5)
            End If

            g.Dispose()
        End If
    End Sub
    Private Sub dibujaProy(ByVal proy As Proyectil)
        Dim g As Graphics = Graphics.FromImage(backBuffer)
        Dim brush As Brush
        'g.DrawRectangle(Pens.PeachPuff, proy.rObjeto)
        If proy.tipo = "invader" Then
            brush = Brushes.Yellow
        ElseIf proy.tipo = "building" Then
            brush = Brushes.Aqua
        Else
            brush = Brushes.Black
        End If
        g.FillRectangle(brush, proy.rObjeto)
        g.Dispose()
    End Sub
    Private Sub dibujaUI(ByVal btn As UI)
        Dim drawFont As New Font("Arial", 16)
        Dim drawBrush As New SolidBrush(Color.White)
        Dim g As Graphics = Graphics.FromImage(backBuffer)
        Dim stringFormat As New StringFormat()
        stringFormat.Alignment = StringAlignment.Center
        stringFormat.LineAlignment = StringAlignment.Center

        If btn.mouseOver Then
            g.FillRectangle(Brushes.Azure, btn.rObjeto)
            g.DrawString(btn.texto, drawFont, Brushes.Black, btn.puntoLocalizacionCentrado.X, btn.puntoLocalizacionCentrado.Y, stringFormat)
        Else
            g.DrawString(btn.texto, drawFont, drawBrush, btn.puntoLocalizacionCentrado.X, btn.puntoLocalizacionCentrado.Y, stringFormat)
        End If
        g.DrawRectangle(Pens.PeachPuff, btn.rObjeto)

        If btn.tBrush IsNot Nothing Then
            g.FillRectangle(btn.tBrush, btn.rObjeto)
        End If
        g.Dispose()
    End Sub
    Private Sub dibujaEnPuntero()
        If construcSel IsNot Nothing Then
            Dim g As Graphics = Graphics.FromImage(backBuffer)
            If construcSel = "Building" Then

                'brushBuilding(0).TranslateTransform(mloc.X - posMouseAnterior.X, mloc.Y - posMouseAnterior.Y, MatrixOrder.Prepend)

                g.FillRectangle(brushPuntero, mloc.X - medidaBuilding.Width / 2, mloc.Y - medidaBuilding.Height / 2, medidaBuilding.Width, medidaBuilding.Height)
            End If
        End If
    End Sub

    Private Sub escalaImagenObjeto(ByVal w As Integer, ByVal h As Integer, ByVal bitmapOriginal As Bitmap, ByRef brush As TextureBrush, ByVal wrap As WrapMode)
        ' Make a bitmap for the result.
        Dim bm_dest As New Bitmap(w, h)

        ' Make a Graphics object for the result Bitmap.
        Dim gr_dest As Graphics = Graphics.FromImage(bm_dest)

        ' Copy the source image into the destination bitmap.
        gr_dest.DrawImage(bitmapOriginal, 0, 0, bm_dest.Width, bm_dest.Height)
        brush = New TextureBrush(bm_dest)
        brush.WrapMode = wrap

        bm_dest.Dispose()
        gr_dest.Dispose()
    End Sub

    Private Sub dibujaEscenario()
        Dim srcRect As RectangleF = New Rectangle(0, 0, pbSurface.Width * scrollValue, pbSurface.Height * scrollValue)
        Dim dstRect As RectangleF = New RectangleF(0, 0, pbSurface.Width, pbSurface.Height)

        Dim bmp As New Bitmap(ClientSize.Width, ClientSize.Height)
        Using g As Graphics = Graphics.FromImage(bmp)
            g.DrawImage(pbImage, 0 + screenLeft, 0)
        End Using
        Using g2 As Graphics = Graphics.FromImage(bmp)
            g2.DrawImage(pbImage, pbImage.Width + screenLeft, 0)
        End Using
        Using g3 As Graphics = Graphics.FromImage(bmp)
            g3.DrawImage(pbImage, screenLeft - pbImage.Width, 0)
        End Using

        'graphicsEscenario.InterpolationMode = Drawing2D.InterpolationMode.NearestNeighbor
        'graphicsEscenario.DrawImage(bmp, dstRect, srcRect, GraphicsUnit.Pixel)
        'graphicsEscenario.DrawImage(bmp, 0, 0)
        ' Destruimos la imagen
        bmp.Dispose()
    End Sub
    'Private Sub dibujaObjeto(ByVal objeto As Objeto)

    '    If objeto.xObjeto <= ClientSize.Width And objeto.yObjeto + objeto.hObjeto >= 0 And objeto.yObjeto <= ClientSize.Height And objeto.xObjeto + objeto.wObjeto >= 0 Then
    '        Dim bmp As New Bitmap(objeto.wObjeto, objeto.hObjeto)
    '        Using g As Graphics = Graphics.FromImage(bmp)
    '            g.DrawImage(objeto.bitmapImage, 0, 0)
    '        End Using       
    '        'objeto.gObjeto.DrawImage(bmp, objeto.puntoLocalizacion)
    '        bmp.Dispose()
    '    End If
    'End Sub
    'Private Sub pbSurface_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles pbSurface.Paint
    '    'I'm calculating sf here, but it could be done in the Resize event so wouldn't have to be calculated every Paint Event

    '    'e.Graphics.ScaleTransform(sf, sf)               ' Scale our drawing up or down based on that scale factor

    '    e.Graphics.DrawImage(backBuffer, 0, 0)             ' Draw the background at 0,0 (so drawing and mouse coordinates start at 0,0)
    '    'e.Graphics.DrawImage(My.Resources.cilindroBuilding, mloc)          ' Draw the game piece at the current mouse location (follows the mouse around)
    '    'If Wash Then                                                        ' If the Wash flag is set
    '    '    Wash = False                                                      '   Make it a one-shot
    '    '    e.Graphics.ResetTransform()                                       '   Reset the scaling so we fill the form using the client Rectangle
    '    '    Using fb As New SolidBrush(Color.FromArgb(128, Color.LightGreen)) '   Create a semi-transparent brush ("Using" is an alternative to "Dispose")
    '    '        e.Graphics.FillRectangle(fb, ClientRectangle)                   '     Fill the form with that color
    '    '    End Using                                                         '   Instead of Using .. End Using, we could do fb.Dispose here
    '    'End If

    'End Sub
End Class
