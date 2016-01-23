<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTopUpSum
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTopUpSum))
        Me.pbCancel = New System.Windows.Forms.PictureBox()
        Me.pbOK = New System.Windows.Forms.PictureBox()
        Me.lblAmount = New System.Windows.Forms.Label()
        Me.lblMobileNo = New System.Windows.Forms.Label()
        Me.pbMobileNo = New System.Windows.Forms.PictureBox()
        Me.pbAmount = New System.Windows.Forms.PictureBox()
        CType(Me.pbCancel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbOK, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbMobileNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbAmount, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pbCancel
        '
        Me.pbCancel.Location = New System.Drawing.Point(729, 653)
        Me.pbCancel.Name = "pbCancel"
        Me.pbCancel.Size = New System.Drawing.Size(172, 88)
        Me.pbCancel.TabIndex = 4
        Me.pbCancel.TabStop = False
        '
        'pbOK
        '
        Me.pbOK.Location = New System.Drawing.Point(536, 653)
        Me.pbOK.Name = "pbOK"
        Me.pbOK.Size = New System.Drawing.Size(172, 88)
        Me.pbOK.TabIndex = 3
        Me.pbOK.TabStop = False
        '
        'lblAmount
        '
        Me.lblAmount.AutoSize = True
        Me.lblAmount.BackColor = System.Drawing.Color.Transparent
        Me.lblAmount.Font = New System.Drawing.Font("Microsoft Sans Serif", 27.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblAmount.Location = New System.Drawing.Point(595, 336)
        Me.lblAmount.Name = "lblAmount"
        Me.lblAmount.Size = New System.Drawing.Size(192, 42)
        Me.lblAmount.TabIndex = 5
        Me.lblAmount.Text = "lblAmount"
        '
        'lblMobileNo
        '
        Me.lblMobileNo.AutoSize = True
        Me.lblMobileNo.BackColor = System.Drawing.Color.Transparent
        Me.lblMobileNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 26.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblMobileNo.Location = New System.Drawing.Point(595, 249)
        Me.lblMobileNo.Name = "lblMobileNo"
        Me.lblMobileNo.Size = New System.Drawing.Size(209, 39)
        Me.lblMobileNo.TabIndex = 8
        Me.lblMobileNo.Text = "lblMobileNo"
        '
        'pbMobileNo
        '
        Me.pbMobileNo.BackColor = System.Drawing.Color.Transparent
        Me.pbMobileNo.Location = New System.Drawing.Point(373, 244)
        Me.pbMobileNo.Name = "pbMobileNo"
        Me.pbMobileNo.Size = New System.Drawing.Size(528, 48)
        Me.pbMobileNo.TabIndex = 9
        Me.pbMobileNo.TabStop = False
        '
        'pbAmount
        '
        Me.pbAmount.BackColor = System.Drawing.Color.Transparent
        Me.pbAmount.Location = New System.Drawing.Point(373, 333)
        Me.pbAmount.Name = "pbAmount"
        Me.pbAmount.Size = New System.Drawing.Size(528, 48)
        Me.pbAmount.TabIndex = 10
        Me.pbAmount.TabStop = False
        '
        'frmTopUpSum
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1024, 768)
        Me.Controls.Add(Me.lblMobileNo)
        Me.Controls.Add(Me.lblAmount)
        Me.Controls.Add(Me.pbAmount)
        Me.Controls.Add(Me.pbMobileNo)
        Me.Controls.Add(Me.pbCancel)
        Me.Controls.Add(Me.pbOK)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmTopUpSum"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmTopUpSum"
        CType(Me.pbCancel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbOK, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbMobileNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbAmount, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pbCancel As System.Windows.Forms.PictureBox
    Friend WithEvents pbOK As System.Windows.Forms.PictureBox
    Friend WithEvents lblAmount As System.Windows.Forms.Label
    Friend WithEvents lblMobileNo As System.Windows.Forms.Label
    Friend WithEvents pbMobileNo As System.Windows.Forms.PictureBox
    Friend WithEvents pbAmount As System.Windows.Forms.PictureBox
End Class
