<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SoftwareHeader
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.txtAlamSeverity = New System.Windows.Forms.TextBox
        Me.txtAlamType = New System.Windows.Forms.TextBox
        Me.txtServerIP = New System.Windows.Forms.TextBox
        Me.txtServerName = New System.Windows.Forms.TextBox
        Me.txtShopName = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'txtAlamSeverity
        '
        Me.txtAlamSeverity.Location = New System.Drawing.Point(434, 56)
        Me.txtAlamSeverity.Margin = New System.Windows.Forms.Padding(3, 6, 3, 6)
        Me.txtAlamSeverity.Name = "txtAlamSeverity"
        Me.txtAlamSeverity.ReadOnly = True
        Me.txtAlamSeverity.Size = New System.Drawing.Size(201, 34)
        Me.txtAlamSeverity.TabIndex = 43
        Me.txtAlamSeverity.Text = "Critical"
        '
        'txtAlamType
        '
        Me.txtAlamType.Location = New System.Drawing.Point(112, 56)
        Me.txtAlamType.Margin = New System.Windows.Forms.Padding(3, 6, 3, 6)
        Me.txtAlamType.Name = "txtAlamType"
        Me.txtAlamType.ReadOnly = True
        Me.txtAlamType.Size = New System.Drawing.Size(201, 34)
        Me.txtAlamType.TabIndex = 42
        Me.txtAlamType.Text = "Fault"
        '
        'txtServerIP
        '
        Me.txtServerIP.Location = New System.Drawing.Point(728, 8)
        Me.txtServerIP.Margin = New System.Windows.Forms.Padding(3, 6, 3, 6)
        Me.txtServerIP.Name = "txtServerIP"
        Me.txtServerIP.ReadOnly = True
        Me.txtServerIP.Size = New System.Drawing.Size(201, 34)
        Me.txtServerIP.TabIndex = 41
        '
        'txtServerName
        '
        Me.txtServerName.Location = New System.Drawing.Point(434, 10)
        Me.txtServerName.Margin = New System.Windows.Forms.Padding(3, 6, 3, 6)
        Me.txtServerName.Name = "txtServerName"
        Me.txtServerName.ReadOnly = True
        Me.txtServerName.Size = New System.Drawing.Size(201, 34)
        Me.txtServerName.TabIndex = 40
        '
        'txtShopName
        '
        Me.txtShopName.Location = New System.Drawing.Point(112, 10)
        Me.txtShopName.Margin = New System.Windows.Forms.Padding(3, 6, 3, 6)
        Me.txtShopName.Name = "txtShopName"
        Me.txtShopName.ReadOnly = True
        Me.txtShopName.Size = New System.Drawing.Size(201, 34)
        Me.txtShopName.TabIndex = 39
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(326, 64)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(91, 26)
        Me.Label6.TabIndex = 38
        Me.Label6.Text = "Alam Severity :"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(17, 66)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(76, 26)
        Me.Label7.TabIndex = 37
        Me.Label7.Text = "Alam Type :"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(644, 10)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(67, 26)
        Me.Label8.TabIndex = 36
        Me.Label8.Text = "Server IP :"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(326, 10)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(90, 26)
        Me.Label9.TabIndex = 35
        Me.Label9.Text = "Server Name :"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(6, 10)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(85, 26)
        Me.Label10.TabIndex = 34
        Me.Label10.Text = "Shop Name :"
        '
        'SoftwareHeader
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 26.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.txtAlamSeverity)
        Me.Controls.Add(Me.txtAlamType)
        Me.Controls.Add(Me.txtServerIP)
        Me.Controls.Add(Me.txtServerName)
        Me.Controls.Add(Me.txtShopName)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label10)
        Me.Font = New System.Drawing.Font("Cordia New", 14.25!)
        Me.Margin = New System.Windows.Forms.Padding(3, 6, 3, 6)
        Me.Name = "SoftwareHeader"
        Me.Size = New System.Drawing.Size(933, 102)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtAlamSeverity As System.Windows.Forms.TextBox
    Friend WithEvents txtAlamType As System.Windows.Forms.TextBox
    Friend WithEvents txtServerIP As System.Windows.Forms.TextBox
    Friend WithEvents txtServerName As System.Windows.Forms.TextBox
    Friend WithEvents txtShopName As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label

End Class
