<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPaymentCreditSign
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.pbPaymentCreditSign = New System.Windows.Forms.PictureBox()
        Me.pbCancel = New System.Windows.Forms.PictureBox()
        Me.pbOK = New System.Windows.Forms.PictureBox()
        Me.PrintPreviewControl1 = New System.Windows.Forms.PrintPreviewControl()
        Me.pd = New System.Drawing.Printing.PrintDocument()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.pcCapture = New System.Windows.Forms.PictureBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.pbDisplay = New System.Windows.Forms.PictureBox()
        CType(Me.pbPaymentCreditSign, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbCancel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbOK, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pcCapture, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbDisplay, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(421, 26)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(58, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "กรุณาลงชื่อ"
        '
        'pbPaymentCreditSign
        '
        Me.pbPaymentCreditSign.BackColor = System.Drawing.Color.White
        Me.pbPaymentCreditSign.Location = New System.Drawing.Point(546, 85)
        Me.pbPaymentCreditSign.Name = "pbPaymentCreditSign"
        Me.pbPaymentCreditSign.Size = New System.Drawing.Size(344, 228)
        Me.pbPaymentCreditSign.TabIndex = 1
        Me.pbPaymentCreditSign.TabStop = False
        '
        'pbCancel
        '
        Me.pbCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pbCancel.Location = New System.Drawing.Point(823, 668)
        Me.pbCancel.Name = "pbCancel"
        Me.pbCancel.Size = New System.Drawing.Size(172, 88)
        Me.pbCancel.TabIndex = 27
        Me.pbCancel.TabStop = False
        '
        'pbOK
        '
        Me.pbOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pbOK.Location = New System.Drawing.Point(645, 668)
        Me.pbOK.Name = "pbOK"
        Me.pbOK.Size = New System.Drawing.Size(172, 88)
        Me.pbOK.TabIndex = 26
        Me.pbOK.TabStop = False
        '
        'PrintPreviewControl1
        '
        Me.PrintPreviewControl1.AutoZoom = False
        Me.PrintPreviewControl1.BackColor = System.Drawing.SystemColors.Control
        Me.PrintPreviewControl1.Document = Me.pd
        Me.PrintPreviewControl1.Location = New System.Drawing.Point(44, 42)
        Me.PrintPreviewControl1.Name = "PrintPreviewControl1"
        Me.PrintPreviewControl1.Size = New System.Drawing.Size(448, 546)
        Me.PrintPreviewControl1.TabIndex = 28
        Me.PrintPreviewControl1.Zoom = 1.0R
        '
        'pd
        '
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label2.Location = New System.Drawing.Point(540, 42)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(130, 31)
        Me.Label2.TabIndex = 29
        Me.Label2.Text = "Signature"
        '
        'pcCapture
        '
        Me.pcCapture.Location = New System.Drawing.Point(546, 348)
        Me.pcCapture.Name = "pcCapture"
        Me.pcCapture.Size = New System.Drawing.Size(340, 220)
        Me.pcCapture.TabIndex = 30
        Me.pcCapture.TabStop = False
        Me.pcCapture.Visible = False
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(700, -1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(101, 40)
        Me.Label3.TabIndex = 31
        '
        'pbDisplay
        '
        Me.pbDisplay.Location = New System.Drawing.Point(546, 346)
        Me.pbDisplay.Name = "pbDisplay"
        Me.pbDisplay.Size = New System.Drawing.Size(340, 220)
        Me.pbDisplay.TabIndex = 32
        Me.pbDisplay.TabStop = False
        '
        'frmPaymentCreditSign
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1024, 768)
        Me.Controls.Add(Me.pbDisplay)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.pcCapture)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.PrintPreviewControl1)
        Me.Controls.Add(Me.pbCancel)
        Me.Controls.Add(Me.pbOK)
        Me.Controls.Add(Me.pbPaymentCreditSign)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmPaymentCreditSign"
        Me.Text = "frmPaymentCreditSign"
        CType(Me.pbPaymentCreditSign, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbCancel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbOK, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pcCapture, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbDisplay, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents pbPaymentCreditSign As System.Windows.Forms.PictureBox
    Friend WithEvents pbCancel As System.Windows.Forms.PictureBox
    Friend WithEvents pbOK As System.Windows.Forms.PictureBox
    Friend WithEvents PrintPreviewControl1 As System.Windows.Forms.PrintPreviewControl
    Friend WithEvents pd As System.Drawing.Printing.PrintDocument
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents pcCapture As System.Windows.Forms.PictureBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents pbDisplay As System.Windows.Forms.PictureBox
End Class
