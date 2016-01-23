<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOutOfService
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
        Me.pbEnd = New System.Windows.Forms.PictureBox()
        Me.lblOutOfService = New System.Windows.Forms.Label()
        CType(Me.pbEnd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pbEnd
        '
        Me.pbEnd.Location = New System.Drawing.Point(980, 3)
        Me.pbEnd.Name = "pbEnd"
        Me.pbEnd.Size = New System.Drawing.Size(41, 38)
        Me.pbEnd.TabIndex = 0
        Me.pbEnd.TabStop = False
        '
        'lblOutOfService
        '
        Me.lblOutOfService.AutoSize = True
        Me.lblOutOfService.Location = New System.Drawing.Point(559, 236)
        Me.lblOutOfService.Name = "lblOutOfService"
        Me.lblOutOfService.Size = New System.Drawing.Size(73, 13)
        Me.lblOutOfService.TabIndex = 1
        Me.lblOutOfService.Text = "Out of service"
        '
        'frmOutOfService
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1024, 768)
        Me.Controls.Add(Me.lblOutOfService)
        Me.Controls.Add(Me.pbEnd)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmOutOfService"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmOutOfService"
        CType(Me.pbEnd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pbEnd As System.Windows.Forms.PictureBox
    Friend WithEvents lblOutOfService As System.Windows.Forms.Label
End Class
