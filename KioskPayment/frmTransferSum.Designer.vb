<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTransferSum
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTransferSum))
        Me.lblTransferAmt = New System.Windows.Forms.Label()
        Me.lblMobileNo = New System.Windows.Forms.Label()
        Me.pbCancel = New System.Windows.Forms.PictureBox()
        Me.pbOK = New System.Windows.Forms.PictureBox()
        Me.pbMobileNo = New System.Windows.Forms.PictureBox()
        Me.pbMoney = New System.Windows.Forms.PictureBox()
        CType(Me.pbCancel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbOK, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbMobileNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbMoney, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblTransferAmt
        '
        Me.lblTransferAmt.AutoSize = True
        Me.lblTransferAmt.Font = New System.Drawing.Font("Microsoft Sans Serif", 26.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblTransferAmt.Location = New System.Drawing.Point(664, 330)
        Me.lblTransferAmt.Name = "lblTransferAmt"
        Me.lblTransferAmt.Size = New System.Drawing.Size(165, 39)
        Me.lblTransferAmt.TabIndex = 2
        Me.lblTransferAmt.Text = "จำนวนเงิน"
        '
        'lblMobileNo
        '
        Me.lblMobileNo.AutoSize = True
        Me.lblMobileNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 26.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblMobileNo.Location = New System.Drawing.Point(664, 248)
        Me.lblMobileNo.Name = "lblMobileNo"
        Me.lblMobileNo.Size = New System.Drawing.Size(146, 39)
        Me.lblMobileNo.TabIndex = 3
        Me.lblMobileNo.Text = "เบอร์ผู้รับ"
        '
        'pbCancel
        '
        Me.pbCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pbCancel.BackColor = System.Drawing.Color.Transparent
        Me.pbCancel.Location = New System.Drawing.Point(785, 635)
        Me.pbCancel.Name = "pbCancel"
        Me.pbCancel.Size = New System.Drawing.Size(180, 80)
        Me.pbCancel.TabIndex = 6
        Me.pbCancel.TabStop = False
        '
        'pbOK
        '
        Me.pbOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pbOK.BackColor = System.Drawing.Color.Transparent
        Me.pbOK.Location = New System.Drawing.Point(571, 635)
        Me.pbOK.Name = "pbOK"
        Me.pbOK.Size = New System.Drawing.Size(176, 80)
        Me.pbOK.TabIndex = 5
        Me.pbOK.TabStop = False
        '
        'pbMobileNo
        '
        Me.pbMobileNo.Location = New System.Drawing.Point(455, 243)
        Me.pbMobileNo.Name = "pbMobileNo"
        Me.pbMobileNo.Size = New System.Drawing.Size(528, 48)
        Me.pbMobileNo.TabIndex = 7
        Me.pbMobileNo.TabStop = False
        '
        'pbMoney
        '
        Me.pbMoney.Location = New System.Drawing.Point(455, 325)
        Me.pbMoney.Name = "pbMoney"
        Me.pbMoney.Size = New System.Drawing.Size(528, 48)
        Me.pbMoney.TabIndex = 8
        Me.pbMoney.TabStop = False
        '
        'frmTransferSum
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1024, 768)
        Me.Controls.Add(Me.lblTransferAmt)
        Me.Controls.Add(Me.pbMoney)
        Me.Controls.Add(Me.lblMobileNo)
        Me.Controls.Add(Me.pbMobileNo)
        Me.Controls.Add(Me.pbCancel)
        Me.Controls.Add(Me.pbOK)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmTransferSum"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmTransferSum"
        CType(Me.pbCancel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbOK, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbMobileNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbMoney, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblTransferAmt As System.Windows.Forms.Label
    Friend WithEvents lblMobileNo As System.Windows.Forms.Label
    Friend WithEvents pbCancel As System.Windows.Forms.PictureBox
    Friend WithEvents pbOK As System.Windows.Forms.PictureBox
    Friend WithEvents pbMobileNo As System.Windows.Forms.PictureBox
    Friend WithEvents pbMoney As System.Windows.Forms.PictureBox
End Class
