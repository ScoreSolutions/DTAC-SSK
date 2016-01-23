<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPaySlip
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPaySlip))
        Me.pd = New System.Drawing.Printing.PrintDocument()
        Me.pbPrint = New System.Windows.Forms.PictureBox()
        Me.pbNonPrint = New System.Windows.Forms.PictureBox()
        CType(Me.pbPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbNonPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pd
        '
        '
        'pbPrint
        '
        Me.pbPrint.BackColor = System.Drawing.Color.Transparent
        Me.pbPrint.Location = New System.Drawing.Point(377, 385)
        Me.pbPrint.Name = "pbPrint"
        Me.pbPrint.Size = New System.Drawing.Size(285, 80)
        Me.pbPrint.TabIndex = 3
        Me.pbPrint.TabStop = False
        '
        'pbNonPrint
        '
        Me.pbNonPrint.BackColor = System.Drawing.Color.Transparent
        Me.pbNonPrint.Location = New System.Drawing.Point(702, 385)
        Me.pbNonPrint.Name = "pbNonPrint"
        Me.pbNonPrint.Size = New System.Drawing.Size(286, 80)
        Me.pbNonPrint.TabIndex = 4
        Me.pbNonPrint.TabStop = False
        '
        'frmPaySlip
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1024, 768)
        Me.Controls.Add(Me.pbNonPrint)
        Me.Controls.Add(Me.pbPrint)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmPaySlip"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmPaySlip"
        CType(Me.pbPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbNonPrint, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pd As System.Drawing.Printing.PrintDocument
    Friend WithEvents pbPrint As System.Windows.Forms.PictureBox
    Friend WithEvents pbNonPrint As System.Windows.Forms.PictureBox
End Class
