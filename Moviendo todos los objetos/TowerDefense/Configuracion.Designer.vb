<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Configuracion
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.cbxRes = New System.Windows.Forms.ComboBox()
        Me.chkWindowed = New System.Windows.Forms.CheckBox()
        Me.lblNativeRes = New System.Windows.Forms.Label()
        Me.tbMovPantalla = New System.Windows.Forms.TrackBar()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblMovPantalla = New System.Windows.Forms.Label()
        Me.tbVolumenSonido = New System.Windows.Forms.TrackBar()
        Me.lblVolumenSonido = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.tbMovPantalla, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tbVolumenSonido, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(277, 274)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 29)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.Location = New System.Drawing.Point(3, 3)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 23)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "Aceptar"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(76, 3)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancelar"
        '
        'cbxRes
        '
        Me.cbxRes.FormattingEnabled = True
        Me.cbxRes.Location = New System.Drawing.Point(12, 12)
        Me.cbxRes.Name = "cbxRes"
        Me.cbxRes.Size = New System.Drawing.Size(121, 21)
        Me.cbxRes.TabIndex = 8
        '
        'chkWindowed
        '
        Me.chkWindowed.AutoSize = True
        Me.chkWindowed.Location = New System.Drawing.Point(21, 39)
        Me.chkWindowed.Name = "chkWindowed"
        Me.chkWindowed.Size = New System.Drawing.Size(77, 17)
        Me.chkWindowed.TabIndex = 9
        Me.chkWindowed.Text = "Windowed"
        Me.chkWindowed.UseVisualStyleBackColor = True
        '
        'lblNativeRes
        '
        Me.lblNativeRes.AutoSize = True
        Me.lblNativeRes.Location = New System.Drawing.Point(182, 20)
        Me.lblNativeRes.Name = "lblNativeRes"
        Me.lblNativeRes.Size = New System.Drawing.Size(55, 13)
        Me.lblNativeRes.TabIndex = 10
        Me.lblNativeRes.Text = "Native res"
        '
        'tbMovPantalla
        '
        Me.tbMovPantalla.Location = New System.Drawing.Point(21, 103)
        Me.tbMovPantalla.Maximum = 100
        Me.tbMovPantalla.Minimum = 10
        Me.tbMovPantalla.Name = "tbMovPantalla"
        Me.tbMovPantalla.Size = New System.Drawing.Size(172, 45)
        Me.tbMovPantalla.TabIndex = 11
        Me.tbMovPantalla.TickFrequency = 10
        Me.tbMovPantalla.Value = 10
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(18, 87)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(150, 13)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "Velocidad movimiento pantalla"
        '
        'lblMovPantalla
        '
        Me.lblMovPantalla.AutoSize = True
        Me.lblMovPantalla.Location = New System.Drawing.Point(198, 103)
        Me.lblMovPantalla.Name = "lblMovPantalla"
        Me.lblMovPantalla.Size = New System.Drawing.Size(39, 13)
        Me.lblMovPantalla.TabIndex = 13
        Me.lblMovPantalla.Text = "Label2"
        '
        'tbVolumenSonido
        '
        Me.tbVolumenSonido.Location = New System.Drawing.Point(21, 154)
        Me.tbVolumenSonido.Maximum = 500
        Me.tbVolumenSonido.Minimum = 10
        Me.tbVolumenSonido.Name = "tbVolumenSonido"
        Me.tbVolumenSonido.Size = New System.Drawing.Size(172, 45)
        Me.tbVolumenSonido.TabIndex = 14
        Me.tbVolumenSonido.TickFrequency = 20
        Me.tbVolumenSonido.Value = 10
        '
        'lblVolumenSonido
        '
        Me.lblVolumenSonido.AutoSize = True
        Me.lblVolumenSonido.Location = New System.Drawing.Point(199, 154)
        Me.lblVolumenSonido.Name = "lblVolumenSonido"
        Me.lblVolumenSonido.Size = New System.Drawing.Size(39, 13)
        Me.lblVolumenSonido.TabIndex = 15
        Me.lblVolumenSonido.Text = "Label2"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(18, 135)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(82, 13)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "Volumen sonido"
        '
        'Configuracion
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(435, 315)
        Me.Controls.Add(Me.lblVolumenSonido)
        Me.Controls.Add(Me.tbVolumenSonido)
        Me.Controls.Add(Me.lblMovPantalla)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.tbMovPantalla)
        Me.Controls.Add(Me.lblNativeRes)
        Me.Controls.Add(Me.chkWindowed)
        Me.Controls.Add(Me.cbxRes)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Configuracion"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Configuracion"
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.tbMovPantalla, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tbVolumenSonido, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents cbxRes As System.Windows.Forms.ComboBox
    Friend WithEvents chkWindowed As System.Windows.Forms.CheckBox
    Friend WithEvents lblNativeRes As System.Windows.Forms.Label
    Friend WithEvents tbMovPantalla As System.Windows.Forms.TrackBar
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblMovPantalla As System.Windows.Forms.Label
    Friend WithEvents tbVolumenSonido As TrackBar
    Friend WithEvents lblVolumenSonido As Label
    Friend WithEvents Label2 As Label
End Class
