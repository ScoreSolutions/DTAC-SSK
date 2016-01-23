<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSelectPaymentType
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
        Me.components = New System.ComponentModel.Container()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.pcCapture = New System.Windows.Forms.PictureBox()
        Me.lblBillingNo = New System.Windows.Forms.Label()
        Me.lblAmount = New System.Windows.Forms.Label()
        Me.lblAccountName = New System.Windows.Forms.Label()
        Me.lblAccountNo = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TimerReturnToScanbarcode = New System.Windows.Forms.Timer(Me.components)
        Me.pbCash = New System.Windows.Forms.PictureBox()
        Me.pbCreditCard = New System.Windows.Forms.PictureBox()
        CType(Me.pcCapture, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbCash, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbCreditCard, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(157, 138)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(129, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "กรุณาเลือกวิธีการชำระเงิน"
        Me.Label1.Visible = False
        '
        'pcCapture
        '
        Me.pcCapture.Location = New System.Drawing.Point(15, 47)
        Me.pcCapture.Name = "pcCapture"
        Me.pcCapture.Size = New System.Drawing.Size(75, 59)
        Me.pcCapture.TabIndex = 5
        Me.pcCapture.TabStop = False
        Me.pcCapture.Visible = False
        '
        'lblBillingNo
        '
        Me.lblBillingNo.AutoSize = True
        Me.lblBillingNo.Location = New System.Drawing.Point(12, 9)
        Me.lblBillingNo.Name = "lblBillingNo"
        Me.lblBillingNo.Size = New System.Drawing.Size(39, 13)
        Me.lblBillingNo.TabIndex = 6
        Me.lblBillingNo.Text = "Label2"
        Me.lblBillingNo.Visible = False
        '
        'lblAmount
        '
        Me.lblAmount.AutoSize = True
        Me.lblAmount.Location = New System.Drawing.Point(241, 85)
        Me.lblAmount.Name = "lblAmount"
        Me.lblAmount.Size = New System.Drawing.Size(0, 13)
        Me.lblAmount.TabIndex = 12
        Me.lblAmount.Visible = False
        '
        'lblAccountName
        '
        Me.lblAccountName.AutoSize = True
        Me.lblAccountName.Location = New System.Drawing.Point(241, 56)
        Me.lblAccountName.Name = "lblAccountName"
        Me.lblAccountName.Size = New System.Drawing.Size(0, 13)
        Me.lblAccountName.TabIndex = 11
        Me.lblAccountName.Visible = False
        '
        'lblAccountNo
        '
        Me.lblAccountNo.AutoSize = True
        Me.lblAccountNo.Location = New System.Drawing.Point(250, 26)
        Me.lblAccountNo.Name = "lblAccountNo"
        Me.lblAccountNo.Size = New System.Drawing.Size(0, 13)
        Me.lblAccountNo.TabIndex = 10
        Me.lblAccountNo.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(186, 85)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(49, 13)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Amount :"
        Me.Label3.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(151, 56)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(84, 13)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Account Name :"
        Me.Label2.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(165, 26)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(70, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Account No :"
        Me.Label4.Visible = False
        '
        'TimerReturnToScanbarcode
        '
        Me.TimerReturnToScanbarcode.Enabled = True
        Me.TimerReturnToScanbarcode.Interval = 1000
        '
        'pbCash
        '
        Me.pbCash.Location = New System.Drawing.Point(209, 263)
        Me.pbCash.Name = "pbCash"
        Me.pbCash.Size = New System.Drawing.Size(247, 244)
        Me.pbCash.TabIndex = 13
        Me.pbCash.TabStop = False
        '
        'pbCreditCard
        '
        Me.pbCreditCard.Location = New System.Drawing.Point(515, 263)
        Me.pbCreditCard.Name = "pbCreditCard"
        Me.pbCreditCard.Size = New System.Drawing.Size(247, 244)
        Me.pbCreditCard.TabIndex = 14
        Me.pbCreditCard.TabStop = False
        '
        'frmSelectPaymentType
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1024, 768)
        Me.Controls.Add(Me.pbCreditCard)
        Me.Controls.Add(Me.pbCash)
        Me.Controls.Add(Me.lblAmount)
        Me.Controls.Add(Me.lblAccountName)
        Me.Controls.Add(Me.lblAccountNo)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.lblBillingNo)
        Me.Controls.Add(Me.pcCapture)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmSelectPaymentType"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmSelectPaymentType"
        CType(Me.pcCapture, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbCash, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbCreditCard, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents pcCapture As System.Windows.Forms.PictureBox
    Friend WithEvents lblBillingNo As System.Windows.Forms.Label
    Friend WithEvents lblAmount As System.Windows.Forms.Label
    Friend WithEvents lblAccountName As System.Windows.Forms.Label
    Friend WithEvents lblAccountNo As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TimerReturnToScanbarcode As System.Windows.Forms.Timer
    Friend WithEvents pbCash As System.Windows.Forms.PictureBox
    Friend WithEvents pbCreditCard As System.Windows.Forms.PictureBox
End Class
