<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPaymentCredit
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPaymentCredit))
        Me.pbCreditCard = New System.Windows.Forms.PictureBox()
        Me.lblPayAmt = New System.Windows.Forms.Label()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.txtHex = New System.Windows.Forms.TextBox()
        Me.txtData = New System.Windows.Forms.TextBox()
        CType(Me.pbCreditCard, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pbCreditCard
        '
        Me.pbCreditCard.Location = New System.Drawing.Point(590, 212)
        Me.pbCreditCard.Name = "pbCreditCard"
        Me.pbCreditCard.Size = New System.Drawing.Size(190, 250)
        Me.pbCreditCard.TabIndex = 0
        Me.pbCreditCard.TabStop = False
        '
        'lblPayAmt
        '
        Me.lblPayAmt.AutoSize = True
        Me.lblPayAmt.BackColor = System.Drawing.Color.Transparent
        Me.lblPayAmt.Font = New System.Drawing.Font("Microsoft Sans Serif", 26.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblPayAmt.Location = New System.Drawing.Point(604, 65)
        Me.lblPayAmt.Name = "lblPayAmt"
        Me.lblPayAmt.Size = New System.Drawing.Size(126, 39)
        Me.lblPayAmt.TabIndex = 1
        Me.lblPayAmt.Text = "Label1"
        '
        'Timer1
        '
        '
        'txtHex
        '
        Me.txtHex.Location = New System.Drawing.Point(639, 186)
        Me.txtHex.Multiline = True
        Me.txtHex.Name = "txtHex"
        Me.txtHex.Size = New System.Drawing.Size(100, 20)
        Me.txtHex.TabIndex = 2
        Me.txtHex.Visible = False
        '
        'txtData
        '
        Me.txtData.Location = New System.Drawing.Point(639, 212)
        Me.txtData.Multiline = True
        Me.txtData.Name = "txtData"
        Me.txtData.Size = New System.Drawing.Size(100, 20)
        Me.txtData.TabIndex = 3
        Me.txtData.Visible = False
        '
        'frmPaymentCredit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1024, 768)
        Me.Controls.Add(Me.txtData)
        Me.Controls.Add(Me.txtHex)
        Me.Controls.Add(Me.lblPayAmt)
        Me.Controls.Add(Me.pbCreditCard)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmPaymentCredit"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmPaymentCredit"
        CType(Me.pbCreditCard, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pbCreditCard As System.Windows.Forms.PictureBox
    Friend WithEvents lblPayAmt As System.Windows.Forms.Label
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Public WithEvents txtHex As System.Windows.Forms.TextBox
    Public WithEvents txtData As System.Windows.Forms.TextBox
End Class
