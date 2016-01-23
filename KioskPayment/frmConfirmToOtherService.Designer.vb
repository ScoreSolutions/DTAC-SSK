<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmConfirmToOtherService
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmConfirmToOtherService))
        Me.pbComplete = New System.Windows.Forms.PictureBox()
        Me.pbOK = New System.Windows.Forms.PictureBox()
        CType(Me.pbComplete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbOK, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pbComplete
        '
        Me.pbComplete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pbComplete.BackColor = System.Drawing.Color.Transparent
        Me.pbComplete.Location = New System.Drawing.Point(696, 523)
        Me.pbComplete.Name = "pbComplete"
        Me.pbComplete.Size = New System.Drawing.Size(285, 79)
        Me.pbComplete.TabIndex = 6
        Me.pbComplete.TabStop = False
        '
        'pbOK
        '
        Me.pbOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pbOK.BackColor = System.Drawing.Color.Transparent
        Me.pbOK.Location = New System.Drawing.Point(370, 523)
        Me.pbOK.Name = "pbOK"
        Me.pbOK.Size = New System.Drawing.Size(285, 79)
        Me.pbOK.TabIndex = 5
        Me.pbOK.TabStop = False
        '
        'frmConfirmToOtherService
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1024, 768)
        Me.Controls.Add(Me.pbComplete)
        Me.Controls.Add(Me.pbOK)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmConfirmToOtherService"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmConfirmToOtherService"
        CType(Me.pbComplete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbOK, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pbComplete As System.Windows.Forms.PictureBox
    Friend WithEvents pbOK As System.Windows.Forms.PictureBox
End Class
