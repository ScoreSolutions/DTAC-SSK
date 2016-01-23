<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SeverityControl
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.rdEmail_Minor = New System.Windows.Forms.RadioButton
        Me.rdSMS_Minor = New System.Windows.Forms.RadioButton
        Me.rdSnmp_Minor = New System.Windows.Forms.RadioButton
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtValueOver_Minor = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.rdEmail_Major = New System.Windows.Forms.RadioButton
        Me.rdSMS_Major = New System.Windows.Forms.RadioButton
        Me.rdSnmp_Major = New System.Windows.Forms.RadioButton
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtValueOver_Major = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.rdEmail_Critical = New System.Windows.Forms.RadioButton
        Me.rdSMS_Critical = New System.Windows.Forms.RadioButton
        Me.rdSnmp_Critical = New System.Windows.Forms.RadioButton
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.txtValueOverCritical = New System.Windows.Forms.TextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rdEmail_Minor)
        Me.GroupBox1.Controls.Add(Me.rdSMS_Minor)
        Me.GroupBox1.Controls.Add(Me.rdSnmp_Minor)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtValueOver_Minor)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(8, 5)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(292, 90)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'rdEmail_Minor
        '
        Me.rdEmail_Minor.AutoSize = True
        Me.rdEmail_Minor.Location = New System.Drawing.Point(230, 50)
        Me.rdEmail_Minor.Name = "rdEmail_Minor"
        Me.rdEmail_Minor.Size = New System.Drawing.Size(50, 17)
        Me.rdEmail_Minor.TabIndex = 7
        Me.rdEmail_Minor.Text = "Email"
        Me.rdEmail_Minor.UseVisualStyleBackColor = True
        '
        'rdSMS_Minor
        '
        Me.rdSMS_Minor.AutoSize = True
        Me.rdSMS_Minor.Location = New System.Drawing.Point(160, 50)
        Me.rdSMS_Minor.Name = "rdSMS_Minor"
        Me.rdSMS_Minor.Size = New System.Drawing.Size(48, 17)
        Me.rdSMS_Minor.TabIndex = 6
        Me.rdSMS_Minor.Text = "SMS"
        Me.rdSMS_Minor.UseVisualStyleBackColor = True
        '
        'rdSnmp_Minor
        '
        Me.rdSnmp_Minor.AutoSize = True
        Me.rdSnmp_Minor.Checked = True
        Me.rdSnmp_Minor.Location = New System.Drawing.Point(79, 50)
        Me.rdSnmp_Minor.Name = "rdSnmp_Minor"
        Me.rdSnmp_Minor.Size = New System.Drawing.Size(52, 17)
        Me.rdSnmp_Minor.TabIndex = 5
        Me.rdSnmp_Minor.TabStop = True
        Me.rdSnmp_Minor.Text = "Snmp"
        Me.rdSnmp_Minor.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(8, 50)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(43, 13)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Method"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(227, 19)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(15, 13)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "%"
        '
        'txtValueOver_Minor
        '
        Me.txtValueOver_Minor.Location = New System.Drawing.Point(142, 16)
        Me.txtValueOver_Minor.MaxLength = 3
        Me.txtValueOver_Minor.Name = "txtValueOver_Minor"
        Me.txtValueOver_Minor.Size = New System.Drawing.Size(79, 20)
        Me.txtValueOver_Minor.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(76, 19)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(60, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Value Over"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(33, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Minor"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.rdEmail_Major)
        Me.GroupBox2.Controls.Add(Me.rdSMS_Major)
        Me.GroupBox2.Controls.Add(Me.rdSnmp_Major)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.txtValueOver_Major)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Location = New System.Drawing.Point(8, 103)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(292, 90)
        Me.GroupBox2.TabIndex = 8
        Me.GroupBox2.TabStop = False
        '
        'rdEmail_Major
        '
        Me.rdEmail_Major.AutoSize = True
        Me.rdEmail_Major.Location = New System.Drawing.Point(230, 50)
        Me.rdEmail_Major.Name = "rdEmail_Major"
        Me.rdEmail_Major.Size = New System.Drawing.Size(50, 17)
        Me.rdEmail_Major.TabIndex = 7
        Me.rdEmail_Major.Text = "Email"
        Me.rdEmail_Major.UseVisualStyleBackColor = True
        '
        'rdSMS_Major
        '
        Me.rdSMS_Major.AutoSize = True
        Me.rdSMS_Major.Location = New System.Drawing.Point(160, 50)
        Me.rdSMS_Major.Name = "rdSMS_Major"
        Me.rdSMS_Major.Size = New System.Drawing.Size(48, 17)
        Me.rdSMS_Major.TabIndex = 6
        Me.rdSMS_Major.Text = "SMS"
        Me.rdSMS_Major.UseVisualStyleBackColor = True
        '
        'rdSnmp_Major
        '
        Me.rdSnmp_Major.AutoSize = True
        Me.rdSnmp_Major.Checked = True
        Me.rdSnmp_Major.Location = New System.Drawing.Point(79, 50)
        Me.rdSnmp_Major.Name = "rdSnmp_Major"
        Me.rdSnmp_Major.Size = New System.Drawing.Size(52, 17)
        Me.rdSnmp_Major.TabIndex = 5
        Me.rdSnmp_Major.TabStop = True
        Me.rdSnmp_Major.Text = "Snmp"
        Me.rdSnmp_Major.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(8, 50)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(43, 13)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Method"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(227, 19)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(15, 13)
        Me.Label6.TabIndex = 3
        Me.Label6.Text = "%"
        '
        'txtValueOver_Major
        '
        Me.txtValueOver_Major.Location = New System.Drawing.Point(142, 16)
        Me.txtValueOver_Major.MaxLength = 3
        Me.txtValueOver_Major.Name = "txtValueOver_Major"
        Me.txtValueOver_Major.Size = New System.Drawing.Size(79, 20)
        Me.txtValueOver_Major.TabIndex = 2
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(76, 19)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(60, 13)
        Me.Label7.TabIndex = 1
        Me.Label7.Text = "Value Over"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(8, 19)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(33, 13)
        Me.Label8.TabIndex = 0
        Me.Label8.Text = "Major"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.rdEmail_Critical)
        Me.GroupBox3.Controls.Add(Me.rdSMS_Critical)
        Me.GroupBox3.Controls.Add(Me.rdSnmp_Critical)
        Me.GroupBox3.Controls.Add(Me.Label9)
        Me.GroupBox3.Controls.Add(Me.Label10)
        Me.GroupBox3.Controls.Add(Me.txtValueOverCritical)
        Me.GroupBox3.Controls.Add(Me.Label11)
        Me.GroupBox3.Controls.Add(Me.Label12)
        Me.GroupBox3.Location = New System.Drawing.Point(8, 201)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(292, 90)
        Me.GroupBox3.TabIndex = 9
        Me.GroupBox3.TabStop = False
        '
        'rdEmail_Critical
        '
        Me.rdEmail_Critical.AutoSize = True
        Me.rdEmail_Critical.Location = New System.Drawing.Point(230, 50)
        Me.rdEmail_Critical.Name = "rdEmail_Critical"
        Me.rdEmail_Critical.Size = New System.Drawing.Size(50, 17)
        Me.rdEmail_Critical.TabIndex = 7
        Me.rdEmail_Critical.Text = "Email"
        Me.rdEmail_Critical.UseVisualStyleBackColor = True
        '
        'rdSMS_Critical
        '
        Me.rdSMS_Critical.AutoSize = True
        Me.rdSMS_Critical.Location = New System.Drawing.Point(160, 50)
        Me.rdSMS_Critical.Name = "rdSMS_Critical"
        Me.rdSMS_Critical.Size = New System.Drawing.Size(48, 17)
        Me.rdSMS_Critical.TabIndex = 6
        Me.rdSMS_Critical.Text = "SMS"
        Me.rdSMS_Critical.UseVisualStyleBackColor = True
        '
        'rdSnmp_Critical
        '
        Me.rdSnmp_Critical.AutoSize = True
        Me.rdSnmp_Critical.Checked = True
        Me.rdSnmp_Critical.Location = New System.Drawing.Point(79, 50)
        Me.rdSnmp_Critical.Name = "rdSnmp_Critical"
        Me.rdSnmp_Critical.Size = New System.Drawing.Size(52, 17)
        Me.rdSnmp_Critical.TabIndex = 5
        Me.rdSnmp_Critical.TabStop = True
        Me.rdSnmp_Critical.Text = "Snmp"
        Me.rdSnmp_Critical.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(8, 50)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(43, 13)
        Me.Label9.TabIndex = 4
        Me.Label9.Text = "Method"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(227, 19)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(15, 13)
        Me.Label10.TabIndex = 3
        Me.Label10.Text = "%"
        '
        'txtValueOverCritical
        '
        Me.txtValueOverCritical.Location = New System.Drawing.Point(142, 16)
        Me.txtValueOverCritical.MaxLength = 3
        Me.txtValueOverCritical.Name = "txtValueOverCritical"
        Me.txtValueOverCritical.Size = New System.Drawing.Size(79, 20)
        Me.txtValueOverCritical.TabIndex = 2
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(76, 19)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(60, 13)
        Me.Label11.TabIndex = 1
        Me.Label11.Text = "Value Over"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(8, 19)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(38, 13)
        Me.Label12.TabIndex = 0
        Me.Label12.Text = "Critical"
        '
        'SeverityControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "SeverityControl"
        Me.Size = New System.Drawing.Size(306, 302)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtValueOver_Minor As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents rdEmail_Minor As System.Windows.Forms.RadioButton
    Friend WithEvents rdSMS_Minor As System.Windows.Forms.RadioButton
    Friend WithEvents rdSnmp_Minor As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents rdEmail_Major As System.Windows.Forms.RadioButton
    Friend WithEvents rdSMS_Major As System.Windows.Forms.RadioButton
    Friend WithEvents rdSnmp_Major As System.Windows.Forms.RadioButton
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtValueOver_Major As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents rdEmail_Critical As System.Windows.Forms.RadioButton
    Friend WithEvents rdSMS_Critical As System.Windows.Forms.RadioButton
    Friend WithEvents rdSnmp_Critical As System.Windows.Forms.RadioButton
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtValueOverCritical As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label

End Class
