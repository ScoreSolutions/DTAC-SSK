<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTransferCustomerInfo
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTransferCustomerInfo))
        Me.lblCustomerName = New System.Windows.Forms.Label()
        Me.lblTotal_eWalletAmt = New System.Windows.Forms.Label()
        Me.pbCancel = New System.Windows.Forms.PictureBox()
        Me.pbOK = New System.Windows.Forms.PictureBox()
        Me.lblMobileNo = New System.Windows.Forms.Label()
        CType(Me.pbCancel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbOK, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblCustomerName
        '
        Me.lblCustomerName.AutoSize = True
        Me.lblCustomerName.BackColor = System.Drawing.Color.Transparent
        Me.lblCustomerName.Font = New System.Drawing.Font("Microsoft Sans Serif", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblCustomerName.Location = New System.Drawing.Point(618, 201)
        Me.lblCustomerName.Name = "lblCustomerName"
        Me.lblCustomerName.Size = New System.Drawing.Size(56, 33)
        Me.lblCustomerName.TabIndex = 0
        Me.lblCustomerName.Text = "คุณ"
        '
        'lblTotal_eWalletAmt
        '
        Me.lblTotal_eWalletAmt.AutoSize = True
        Me.lblTotal_eWalletAmt.BackColor = System.Drawing.Color.Transparent
        Me.lblTotal_eWalletAmt.Font = New System.Drawing.Font("Microsoft Sans Serif", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblTotal_eWalletAmt.Location = New System.Drawing.Point(618, 473)
        Me.lblTotal_eWalletAmt.Name = "lblTotal_eWalletAmt"
        Me.lblTotal_eWalletAmt.Size = New System.Drawing.Size(261, 33)
        Me.lblTotal_eWalletAmt.TabIndex = 1
        Me.lblTotal_eWalletAmt.Text = "Total_eWalletAmt"
        '
        'pbCancel
        '
        Me.pbCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pbCancel.BackColor = System.Drawing.Color.Transparent
        Me.pbCancel.Location = New System.Drawing.Point(812, 652)
        Me.pbCancel.Name = "pbCancel"
        Me.pbCancel.Size = New System.Drawing.Size(184, 79)
        Me.pbCancel.TabIndex = 4
        Me.pbCancel.TabStop = False
        '
        'pbOK
        '
        Me.pbOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pbOK.BackColor = System.Drawing.Color.Transparent
        Me.pbOK.Location = New System.Drawing.Point(596, 652)
        Me.pbOK.Name = "pbOK"
        Me.pbOK.Size = New System.Drawing.Size(184, 79)
        Me.pbOK.TabIndex = 3
        Me.pbOK.TabStop = False
        '
        'lblMobileNo
        '
        Me.lblMobileNo.AutoSize = True
        Me.lblMobileNo.BackColor = System.Drawing.Color.Transparent
        Me.lblMobileNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblMobileNo.Location = New System.Drawing.Point(618, 339)
        Me.lblMobileNo.Name = "lblMobileNo"
        Me.lblMobileNo.Size = New System.Drawing.Size(146, 33)
        Me.lblMobileNo.TabIndex = 5
        Me.lblMobileNo.Text = "MobileNo"
        '
        'frmTransferCustomerInfo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1024, 768)
        Me.Controls.Add(Me.lblMobileNo)
        Me.Controls.Add(Me.pbCancel)
        Me.Controls.Add(Me.pbOK)
        Me.Controls.Add(Me.lblTotal_eWalletAmt)
        Me.Controls.Add(Me.lblCustomerName)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmTransferCustomerInfo"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmTransferCustomerInfo"
        CType(Me.pbCancel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbOK, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblCustomerName As System.Windows.Forms.Label
    Friend WithEvents lblTotal_eWalletAmt As System.Windows.Forms.Label
    Friend WithEvents pbCancel As System.Windows.Forms.PictureBox
    Friend WithEvents pbOK As System.Windows.Forms.PictureBox
    Friend WithEvents lblMobileNo As System.Windows.Forms.Label
End Class
