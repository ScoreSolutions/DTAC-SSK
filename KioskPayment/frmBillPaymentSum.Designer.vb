<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBillPaymentSum
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBillPaymentSum))
        Me.pbOK = New System.Windows.Forms.PictureBox()
        Me.pbCancel = New System.Windows.Forms.PictureBox()
        Me.lblCompanyName = New System.Windows.Forms.Label()
        Me.lblMoneyAmt = New System.Windows.Forms.Label()
        Me.pbPayTotal = New System.Windows.Forms.PictureBox()
        Me.pbBillerLogo = New System.Windows.Forms.PictureBox()
        CType(Me.pbOK, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbCancel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbPayTotal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbBillerLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pbOK
        '
        Me.pbOK.BackColor = System.Drawing.Color.Transparent
        Me.pbOK.Location = New System.Drawing.Point(575, 647)
        Me.pbOK.Name = "pbOK"
        Me.pbOK.Size = New System.Drawing.Size(177, 73)
        Me.pbOK.TabIndex = 1
        Me.pbOK.TabStop = False
        '
        'pbCancel
        '
        Me.pbCancel.BackColor = System.Drawing.Color.Transparent
        Me.pbCancel.Location = New System.Drawing.Point(790, 647)
        Me.pbCancel.Name = "pbCancel"
        Me.pbCancel.Size = New System.Drawing.Size(177, 73)
        Me.pbCancel.TabIndex = 2
        Me.pbCancel.TabStop = False
        '
        'lblCompanyName
        '
        Me.lblCompanyName.AutoSize = True
        Me.lblCompanyName.Location = New System.Drawing.Point(107, 67)
        Me.lblCompanyName.Name = "lblCompanyName"
        Me.lblCompanyName.Size = New System.Drawing.Size(13, 13)
        Me.lblCompanyName.TabIndex = 3
        Me.lblCompanyName.Text = "1"
        Me.lblCompanyName.Visible = False
        '
        'lblMoneyAmt
        '
        Me.lblMoneyAmt.AutoSize = True
        Me.lblMoneyAmt.BackColor = System.Drawing.Color.White
        Me.lblMoneyAmt.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblMoneyAmt.Location = New System.Drawing.Point(661, 276)
        Me.lblMoneyAmt.Name = "lblMoneyAmt"
        Me.lblMoneyAmt.Size = New System.Drawing.Size(30, 31)
        Me.lblMoneyAmt.TabIndex = 4
        Me.lblMoneyAmt.Text = "1"
        '
        'pbPayTotal
        '
        Me.pbPayTotal.Image = CType(resources.GetObject("pbPayTotal.Image"), System.Drawing.Image)
        Me.pbPayTotal.Location = New System.Drawing.Point(328, 269)
        Me.pbPayTotal.Name = "pbPayTotal"
        Me.pbPayTotal.Size = New System.Drawing.Size(528, 48)
        Me.pbPayTotal.TabIndex = 5
        Me.pbPayTotal.TabStop = False
        '
        'pbBillerLogo
        '
        Me.pbBillerLogo.BackColor = System.Drawing.Color.Transparent
        Me.pbBillerLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.pbBillerLogo.Location = New System.Drawing.Point(830, 0)
        Me.pbBillerLogo.Name = "pbBillerLogo"
        Me.pbBillerLogo.Size = New System.Drawing.Size(192, 138)
        Me.pbBillerLogo.TabIndex = 6
        Me.pbBillerLogo.TabStop = False
        '
        'frmBillPaymentSum
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1024, 768)
        Me.Controls.Add(Me.pbBillerLogo)
        Me.Controls.Add(Me.lblMoneyAmt)
        Me.Controls.Add(Me.pbPayTotal)
        Me.Controls.Add(Me.lblCompanyName)
        Me.Controls.Add(Me.pbCancel)
        Me.Controls.Add(Me.pbOK)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmBillPaymentSum"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmBillPaymentSum"
        CType(Me.pbOK, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbCancel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbPayTotal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbBillerLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pbOK As System.Windows.Forms.PictureBox
    Friend WithEvents pbCancel As System.Windows.Forms.PictureBox
    Friend WithEvents lblCompanyName As System.Windows.Forms.Label
    Friend WithEvents lblMoneyAmt As System.Windows.Forms.Label
    Friend WithEvents pbPayTotal As System.Windows.Forms.PictureBox
    Friend WithEvents pbBillerLogo As System.Windows.Forms.PictureBox
End Class
