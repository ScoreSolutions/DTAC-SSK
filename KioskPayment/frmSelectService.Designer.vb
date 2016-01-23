<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSelectService
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSelectService))
        Me.pbBillPayment = New System.Windows.Forms.PictureBox()
        Me.pbTopupHappy = New System.Windows.Forms.PictureBox()
        Me.pbMoneyTransfer = New System.Windows.Forms.PictureBox()
        Me.pbCancel = New System.Windows.Forms.PictureBox()
        CType(Me.pbBillPayment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbTopupHappy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbMoneyTransfer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbCancel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pbBillPayment
        '
        Me.pbBillPayment.BackColor = System.Drawing.Color.Transparent
        Me.pbBillPayment.Location = New System.Drawing.Point(407, 171)
        Me.pbBillPayment.Name = "pbBillPayment"
        Me.pbBillPayment.Size = New System.Drawing.Size(234, 232)
        Me.pbBillPayment.TabIndex = 3
        Me.pbBillPayment.TabStop = False
        '
        'pbTopupHappy
        '
        Me.pbTopupHappy.BackColor = System.Drawing.Color.Transparent
        Me.pbTopupHappy.Location = New System.Drawing.Point(705, 171)
        Me.pbTopupHappy.Name = "pbTopupHappy"
        Me.pbTopupHappy.Size = New System.Drawing.Size(234, 232)
        Me.pbTopupHappy.TabIndex = 4
        Me.pbTopupHappy.TabStop = False
        '
        'pbMoneyTransfer
        '
        Me.pbMoneyTransfer.BackColor = System.Drawing.Color.Transparent
        Me.pbMoneyTransfer.Location = New System.Drawing.Point(407, 461)
        Me.pbMoneyTransfer.Name = "pbMoneyTransfer"
        Me.pbMoneyTransfer.Size = New System.Drawing.Size(234, 239)
        Me.pbMoneyTransfer.TabIndex = 5
        Me.pbMoneyTransfer.TabStop = False
        '
        'pbCancel
        '
        Me.pbCancel.BackColor = System.Drawing.Color.Transparent
        Me.pbCancel.Location = New System.Drawing.Point(757, 620)
        Me.pbCancel.Name = "pbCancel"
        Me.pbCancel.Size = New System.Drawing.Size(182, 80)
        Me.pbCancel.TabIndex = 6
        Me.pbCancel.TabStop = False
        '
        'frmSelectService
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1024, 768)
        Me.Controls.Add(Me.pbCancel)
        Me.Controls.Add(Me.pbMoneyTransfer)
        Me.Controls.Add(Me.pbTopupHappy)
        Me.Controls.Add(Me.pbBillPayment)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmSelectService"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmSelectService"
        CType(Me.pbBillPayment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbTopupHappy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbMoneyTransfer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbCancel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pbBillPayment As System.Windows.Forms.PictureBox
    Friend WithEvents pbTopupHappy As System.Windows.Forms.PictureBox
    Friend WithEvents pbMoneyTransfer As System.Windows.Forms.PictureBox
    Friend WithEvents pbCancel As System.Windows.Forms.PictureBox
End Class
