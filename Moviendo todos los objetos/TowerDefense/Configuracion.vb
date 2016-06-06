Imports System.Windows.Forms

Public Class Configuracion
    Public resBox() As String = {"1024 x 768", "1280 x 900", "1400 x 900", "1680 x 1050", "1920 x 1080", "2560 x 1440"}
    Public resValueW() As Integer = {1024, 1280, 1400, 1680, 1920, 2560}
    Public resValueH() As Integer = {768, 900, 900, 1050, 1080, 1440}


    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        My.Settings.Res = cbxRes.SelectedIndex
        My.Settings.windowed = chkWindowed.Checked
        My.Settings.movPantalla = tbMovPantalla.Value
        My.Settings.volumenSonido = tbVolumenSonido.Value
        My.Settings.Save()
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub Configuracion_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lblNativeRes.Text = "Resolución nativa: " & Screen.PrimaryScreen.Bounds.Width & " x " & Screen.PrimaryScreen.Bounds.Height
        For i = 0 To UBound(resBox)
            'For Each resolucion As String In resBox
            If resValueW(i) <= Screen.PrimaryScreen.Bounds.Width Or resValueH(i) <= Screen.PrimaryScreen.Bounds.Height Then
                cbxRes.Items.Add(resBox(i))
            End If
        Next
            cbxRes.SelectedIndex = My.Settings.Res
        chkWindowed.Checked = My.Settings.windowed
        tbMovPantalla.Value = My.Settings.movPantalla
        tbVolumenSonido.Value = My.Settings.volumenSonido
        lblMovPantalla.Text = tbMovPantalla.Value
        lblVolumenSonido.Text = tbVolumenSonido.Value

    End Sub
    Public Function checkIndex(ByVal value As Integer, ByVal array As Integer()) As Integer
        Dim i As Integer
        Dim index As Integer = 15
        For i = 0 To Array.Length - 1
            If value = Array(i) Then
                index = i
                Exit For
            End If
        Next
        Return index
    End Function

    Private Sub tbMovPantalla_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbMovPantalla.Scroll
        lblMovPantalla.Text = tbMovPantalla.Value
    End Sub

    Private Sub tbVolumenSonido_Scroll(sender As Object, e As EventArgs) Handles tbVolumenSonido.Scroll
        lblVolumenSonido.Text = tbVolumenSonido.Value
    End Sub
End Class
