<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPaymentReceipt
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPaymentReceipt))
        Me.pbOK = New System.Windows.Forms.PictureBox()
        Me.pbComplete = New System.Windows.Forms.PictureBox()
        CType(Me.pbOK, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbComplete, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pbOK
        '
        Me.pbOK.BackColor = System.Drawing.Color.Transparent
        Me.pbOK.Location = New System.Drawing.Point(368, 599)
        Me.pbOK.Name = "pbOK"
        Me.pbOK.Size = New System.Drawing.Size(285, 81)
        Me.pbOK.TabIndex = 0
        Me.pbOK.TabStop = False
        '
        'pbComplete
        '
        Me.pbComplete.BackColor = System.Drawing.Color.Transparent
        Me.pbComplete.Location = New System.Drawing.Point(694, 599)
        Me.pbComplete.Name = "pbComplete"
        Me.pbComplete.Size = New System.Drawing.Size(284, 81)
        Me.pbComplete.TabIndex = 1
        Me.pbComplete.TabStop = False
        '
        'frmPaymentReceipt
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1024, 768)
        Me.Controls.Add(Me.pbComplete)
        Me.Controls.Add(Me.pbOK)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmPaymentReceipt"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmPaymentReceipt"
        CType(Me.pbOK, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbComplete, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pbOK As System.Windows.Forms.PictureBox
    Friend WithEvents pbComplete As System.Windows.Forms.PictureBox
End Class
