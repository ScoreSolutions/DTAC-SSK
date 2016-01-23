<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBillPaymentScanBarcode
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBillPaymentScanBarcode))
        Me.txtBarcode = New System.Windows.Forms.TextBox()
        Me.lblScreenSaverCurrentFile = New System.Windows.Forms.Label()
        Me.lblScreenSaverCurrentTime = New System.Windows.Forms.Label()
        Me.pcBarcodeGIF = New System.Windows.Forms.PictureBox()
        Me.lblExit = New System.Windows.Forms.Label()
        Me.pbInputMobile = New System.Windows.Forms.PictureBox()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.pbBillerLogo = New System.Windows.Forms.PictureBox()
        CType(Me.pcBarcodeGIF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbInputMobile, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbBillerLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtBarcode
        '
        Me.txtBarcode.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtBarcode.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtBarcode.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.txtBarcode.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtBarcode.HideSelection = False
        Me.txtBarcode.Location = New System.Drawing.Point(473, 197)
        Me.txtBarcode.Name = "txtBarcode"
        Me.txtBarcode.Size = New System.Drawing.Size(273, 13)
        Me.txtBarcode.TabIndex = 1
        '
        'lblScreenSaverCurrentFile
        '
        Me.lblScreenSaverCurrentFile.AutoSize = True
        Me.lblScreenSaverCurrentFile.Location = New System.Drawing.Point(366, 173)
        Me.lblScreenSaverCurrentFile.Name = "lblScreenSaverCurrentFile"
        Me.lblScreenSaverCurrentFile.Size = New System.Drawing.Size(0, 13)
        Me.lblScreenSaverCurrentFile.TabIndex = 7
        '
        'lblScreenSaverCurrentTime
        '
        Me.lblScreenSaverCurrentTime.AutoSize = True
        Me.lblScreenSaverCurrentTime.Location = New System.Drawing.Point(366, 149)
        Me.lblScreenSaverCurrentTime.Name = "lblScreenSaverCurrentTime"
        Me.lblScreenSaverCurrentTime.Size = New System.Drawing.Size(0, 13)
        Me.lblScreenSaverCurrentTime.TabIndex = 6
        '
        'pcBarcodeGIF
        '
        Me.pcBarcodeGIF.Location = New System.Drawing.Point(473, 122)
        Me.pcBarcodeGIF.Name = "pcBarcodeGIF"
        Me.pcBarcodeGIF.Size = New System.Drawing.Size(409, 193)
        Me.pcBarcodeGIF.TabIndex = 10
        Me.pcBarcodeGIF.TabStop = False
        '
        'lblExit
        '
        Me.lblExit.AutoSize = True
        Me.lblExit.BackColor = System.Drawing.Color.White
        Me.lblExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblExit.ForeColor = System.Drawing.Color.White
        Me.lblExit.Location = New System.Drawing.Point(995, 5)
        Me.lblExit.Name = "lblExit"
        Me.lblExit.Size = New System.Drawing.Size(24, 24)
        Me.lblExit.TabIndex = 9
        Me.lblExit.Text = "E"
        '
        'pbInputMobile
        '
        Me.pbInputMobile.Location = New System.Drawing.Point(323, 525)
        Me.pbInputMobile.Name = "pbInputMobile"
        Me.pbInputMobile.Size = New System.Drawing.Size(559, 124)
        Me.pbInputMobile.TabIndex = 12
        Me.pbInputMobile.TabStop = False
        '
        'TextBox4
        '
        Me.TextBox4.Location = New System.Drawing.Point(511, 122)
        Me.TextBox4.Multiline = True
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New System.Drawing.Size(183, 137)
        Me.TextBox4.TabIndex = 13
        '
        'pbBillerLogo
        '
        Me.pbBillerLogo.BackColor = System.Drawing.Color.Transparent
        Me.pbBillerLogo.Location = New System.Drawing.Point(39, 52)
        Me.pbBillerLogo.Name = "pbBillerLogo"
        Me.pbBillerLogo.Size = New System.Drawing.Size(246, 207)
        Me.pbBillerLogo.TabIndex = 14
        Me.pbBillerLogo.TabStop = False
        '
        'frmBillPaymentScanBarcode
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1024, 768)
        Me.Controls.Add(Me.pbBillerLogo)
        Me.Controls.Add(Me.pbInputMobile)
        Me.Controls.Add(Me.lblExit)
        Me.Controls.Add(Me.lblScreenSaverCurrentFile)
        Me.Controls.Add(Me.lblScreenSaverCurrentTime)
        Me.Controls.Add(Me.pcBarcodeGIF)
        Me.Controls.Add(Me.TextBox4)
        Me.Controls.Add(Me.txtBarcode)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmBillPaymentScanBarcode"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmScanbarcode"
        CType(Me.pcBarcodeGIF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbInputMobile, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbBillerLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblScreenSaverCurrentFile As System.Windows.Forms.Label
    Friend WithEvents lblScreenSaverCurrentTime As System.Windows.Forms.Label
    Friend WithEvents pcBarcodeGIF As System.Windows.Forms.PictureBox
    Friend WithEvents lblExit As System.Windows.Forms.Label
    Friend WithEvents pbInputMobile As System.Windows.Forms.PictureBox
    Public WithEvents txtBarcode As System.Windows.Forms.TextBox
    Friend WithEvents TextBox4 As System.Windows.Forms.TextBox
    Friend WithEvents pbBillerLogo As System.Windows.Forms.PictureBox
End Class
