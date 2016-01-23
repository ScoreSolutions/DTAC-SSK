<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmConfigHW
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
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtShopName = New System.Windows.Forms.TextBox
        Me.txtServerName = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtIPAddress = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.CPU = New System.Windows.Forms.TabPage
        Me.Label20 = New System.Windows.Forms.Label
        Me.txtIntervalMinite_CPU = New System.Windows.Forms.TextBox
        Me.Label21 = New System.Windows.Forms.Label
        Me.chkActive_CPU = New System.Windows.Forms.CheckBox
        Me.btnSaveCPU = New System.Windows.Forms.Button
        Me.SeverityCPU = New FaultManagement.SeverityControl
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtRepeateCheck_CPU = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtAlarmType_CPU = New System.Windows.Forms.TextBox
        Me.RAM = New System.Windows.Forms.TabPage
        Me.Label22 = New System.Windows.Forms.Label
        Me.txtIntervalMinite_RAM = New System.Windows.Forms.TextBox
        Me.Label23 = New System.Windows.Forms.Label
        Me.chkActive_RAM = New System.Windows.Forms.CheckBox
        Me.btnSaveRAM = New System.Windows.Forms.Button
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.txtRepeateCheck_RAM = New System.Windows.Forms.TextBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.txtAlarmType_RAM = New System.Windows.Forms.TextBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.SeverityRAM = New FaultManagement.SeverityControl
        Me.HDD = New System.Windows.Forms.TabPage
        Me.Label24 = New System.Windows.Forms.Label
        Me.txtIntervalMinite_HDD = New System.Windows.Forms.TextBox
        Me.Label25 = New System.Windows.Forms.Label
        Me.chkActive_HDD = New System.Windows.Forms.CheckBox
        Me.btnSaveHDD = New System.Windows.Forms.Button
        Me.Label19 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.txtRepeateCheck_HDD = New System.Windows.Forms.TextBox
        Me.Label17 = New System.Windows.Forms.Label
        Me.txtAlarmType_HDD = New System.Windows.Forms.TextBox
        Me.Label18 = New System.Windows.Forms.Label
        Me.TabHDD = New System.Windows.Forms.TabControl
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.TextBox4 = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.TextBox5 = New System.Windows.Forms.TextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.SeverityControl2 = New FaultManagement.SeverityControl
        Me.TabControl1.SuspendLayout()
        Me.CPU.SuspendLayout()
        Me.RAM.SuspendLayout()
        Me.HDD.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(36, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(69, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Shop Name :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(31, 43)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(75, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Server Name :"
        '
        'txtShopName
        '
        Me.txtShopName.Location = New System.Drawing.Point(110, 11)
        Me.txtShopName.Name = "txtShopName"
        Me.txtShopName.ReadOnly = True
        Me.txtShopName.Size = New System.Drawing.Size(197, 20)
        Me.txtShopName.TabIndex = 3
        '
        'txtServerName
        '
        Me.txtServerName.Location = New System.Drawing.Point(110, 37)
        Me.txtServerName.Name = "txtServerName"
        Me.txtServerName.ReadOnly = True
        Me.txtServerName.Size = New System.Drawing.Size(197, 20)
        Me.txtServerName.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(323, 43)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(64, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "IP Address :"
        '
        'txtIPAddress
        '
        Me.txtIPAddress.Location = New System.Drawing.Point(393, 36)
        Me.txtIPAddress.Name = "txtIPAddress"
        Me.txtIPAddress.ReadOnly = True
        Me.txtIPAddress.Size = New System.Drawing.Size(160, 20)
        Me.txtIPAddress.TabIndex = 6
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(39, 26)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(66, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Alarm Type :"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.CPU)
        Me.TabControl1.Controls.Add(Me.RAM)
        Me.TabControl1.Controls.Add(Me.HDD)
        Me.TabControl1.Location = New System.Drawing.Point(34, 74)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(519, 497)
        Me.TabControl1.TabIndex = 8
        '
        'CPU
        '
        Me.CPU.BackColor = System.Drawing.Color.Transparent
        Me.CPU.Controls.Add(Me.Label20)
        Me.CPU.Controls.Add(Me.txtIntervalMinite_CPU)
        Me.CPU.Controls.Add(Me.Label21)
        Me.CPU.Controls.Add(Me.chkActive_CPU)
        Me.CPU.Controls.Add(Me.btnSaveCPU)
        Me.CPU.Controls.Add(Me.SeverityCPU)
        Me.CPU.Controls.Add(Me.Label7)
        Me.CPU.Controls.Add(Me.Label6)
        Me.CPU.Controls.Add(Me.txtRepeateCheck_CPU)
        Me.CPU.Controls.Add(Me.Label5)
        Me.CPU.Controls.Add(Me.txtAlarmType_CPU)
        Me.CPU.Controls.Add(Me.Label4)
        Me.CPU.Location = New System.Drawing.Point(4, 22)
        Me.CPU.Name = "CPU"
        Me.CPU.Padding = New System.Windows.Forms.Padding(3)
        Me.CPU.Size = New System.Drawing.Size(511, 471)
        Me.CPU.TabIndex = 0
        Me.CPU.Text = "CPU"
        Me.CPU.UseVisualStyleBackColor = True
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(387, 359)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(23, 13)
        Me.Label20.TabIndex = 30
        Me.Label20.Text = "min"
        '
        'txtIntervalMinite_CPU
        '
        Me.txtIntervalMinite_CPU.Location = New System.Drawing.Point(302, 356)
        Me.txtIntervalMinite_CPU.MaxLength = 3
        Me.txtIntervalMinite_CPU.Name = "txtIntervalMinite_CPU"
        Me.txtIntervalMinite_CPU.Size = New System.Drawing.Size(79, 20)
        Me.txtIntervalMinite_CPU.TabIndex = 27
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(219, 359)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(77, 13)
        Me.Label21.TabIndex = 29
        Me.Label21.Text = "Interval Minute"
        '
        'chkActive_CPU
        '
        Me.chkActive_CPU.AutoSize = True
        Me.chkActive_CPU.Checked = True
        Me.chkActive_CPU.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkActive_CPU.Location = New System.Drawing.Point(118, 356)
        Me.chkActive_CPU.Name = "chkActive_CPU"
        Me.chkActive_CPU.Size = New System.Drawing.Size(56, 17)
        Me.chkActive_CPU.TabIndex = 28
        Me.chkActive_CPU.Text = "Active"
        Me.chkActive_CPU.UseVisualStyleBackColor = True
        '
        'btnSaveCPU
        '
        Me.btnSaveCPU.Location = New System.Drawing.Point(225, 423)
        Me.btnSaveCPU.Name = "btnSaveCPU"
        Me.btnSaveCPU.Size = New System.Drawing.Size(75, 23)
        Me.btnSaveCPU.TabIndex = 26
        Me.btnSaveCPU.Text = "Save"
        Me.btnSaveCPU.UseVisualStyleBackColor = True
        '
        'SeverityCPU
        '
        Me.SeverityCPU.Location = New System.Drawing.Point(108, 52)
        Me.SeverityCPU.Name = "SeverityCPU"
        Me.SeverityCPU.Size = New System.Drawing.Size(306, 333)
        Me.SeverityCPU.TabIndex = 13
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(25, 59)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(80, 13)
        Me.Label7.TabIndex = 12
        Me.Label7.Text = "Alarm Severity :"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(358, 30)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(26, 13)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "time"
        '
        'txtRepeateCheck_CPU
        '
        Me.txtRepeateCheck_CPU.Location = New System.Drawing.Point(306, 26)
        Me.txtRepeateCheck_CPU.MaxLength = 3
        Me.txtRepeateCheck_CPU.Name = "txtRepeateCheck_CPU"
        Me.txtRepeateCheck_CPU.Size = New System.Drawing.Size(49, 20)
        Me.txtRepeateCheck_CPU.TabIndex = 9
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(212, 29)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(82, 13)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Repeat Check :"
        '
        'txtAlarmType_CPU
        '
        Me.txtAlarmType_CPU.Location = New System.Drawing.Point(118, 23)
        Me.txtAlarmType_CPU.Name = "txtAlarmType_CPU"
        Me.txtAlarmType_CPU.ReadOnly = True
        Me.txtAlarmType_CPU.Size = New System.Drawing.Size(77, 20)
        Me.txtAlarmType_CPU.TabIndex = 9
        Me.txtAlarmType_CPU.Text = "Fault"
        '
        'RAM
        '
        Me.RAM.BackColor = System.Drawing.Color.Transparent
        Me.RAM.Controls.Add(Me.Label22)
        Me.RAM.Controls.Add(Me.txtIntervalMinite_RAM)
        Me.RAM.Controls.Add(Me.Label23)
        Me.RAM.Controls.Add(Me.chkActive_RAM)
        Me.RAM.Controls.Add(Me.btnSaveRAM)
        Me.RAM.Controls.Add(Me.Label12)
        Me.RAM.Controls.Add(Me.Label13)
        Me.RAM.Controls.Add(Me.txtRepeateCheck_RAM)
        Me.RAM.Controls.Add(Me.Label14)
        Me.RAM.Controls.Add(Me.txtAlarmType_RAM)
        Me.RAM.Controls.Add(Me.Label15)
        Me.RAM.Controls.Add(Me.SeverityRAM)
        Me.RAM.Location = New System.Drawing.Point(4, 22)
        Me.RAM.Name = "RAM"
        Me.RAM.Padding = New System.Windows.Forms.Padding(3)
        Me.RAM.Size = New System.Drawing.Size(511, 471)
        Me.RAM.TabIndex = 1
        Me.RAM.Text = "RAM"
        Me.RAM.UseVisualStyleBackColor = True
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(387, 359)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(23, 13)
        Me.Label22.TabIndex = 34
        Me.Label22.Text = "min"
        '
        'txtIntervalMinite_RAM
        '
        Me.txtIntervalMinite_RAM.Location = New System.Drawing.Point(302, 356)
        Me.txtIntervalMinite_RAM.MaxLength = 3
        Me.txtIntervalMinite_RAM.Name = "txtIntervalMinite_RAM"
        Me.txtIntervalMinite_RAM.Size = New System.Drawing.Size(79, 20)
        Me.txtIntervalMinite_RAM.TabIndex = 31
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(219, 359)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(77, 13)
        Me.Label23.TabIndex = 33
        Me.Label23.Text = "Interval Minute"
        '
        'chkActive_RAM
        '
        Me.chkActive_RAM.AutoSize = True
        Me.chkActive_RAM.Checked = True
        Me.chkActive_RAM.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkActive_RAM.Location = New System.Drawing.Point(118, 356)
        Me.chkActive_RAM.Name = "chkActive_RAM"
        Me.chkActive_RAM.Size = New System.Drawing.Size(56, 17)
        Me.chkActive_RAM.TabIndex = 32
        Me.chkActive_RAM.Text = "Active"
        Me.chkActive_RAM.UseVisualStyleBackColor = True
        '
        'btnSaveRAM
        '
        Me.btnSaveRAM.Location = New System.Drawing.Point(225, 423)
        Me.btnSaveRAM.Name = "btnSaveRAM"
        Me.btnSaveRAM.Size = New System.Drawing.Size(75, 23)
        Me.btnSaveRAM.TabIndex = 26
        Me.btnSaveRAM.Text = "Save"
        Me.btnSaveRAM.UseVisualStyleBackColor = True
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(25, 59)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(80, 13)
        Me.Label12.TabIndex = 19
        Me.Label12.Text = "Alarm Severity :"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(358, 30)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(26, 13)
        Me.Label13.TabIndex = 18
        Me.Label13.Text = "time"
        '
        'txtRepeateCheck_RAM
        '
        Me.txtRepeateCheck_RAM.Location = New System.Drawing.Point(306, 26)
        Me.txtRepeateCheck_RAM.MaxLength = 3
        Me.txtRepeateCheck_RAM.Name = "txtRepeateCheck_RAM"
        Me.txtRepeateCheck_RAM.Size = New System.Drawing.Size(49, 20)
        Me.txtRepeateCheck_RAM.TabIndex = 15
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(212, 29)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(82, 13)
        Me.Label14.TabIndex = 17
        Me.Label14.Text = "Repeat Check :"
        '
        'txtAlarmType_RAM
        '
        Me.txtAlarmType_RAM.Location = New System.Drawing.Point(118, 23)
        Me.txtAlarmType_RAM.Name = "txtAlarmType_RAM"
        Me.txtAlarmType_RAM.ReadOnly = True
        Me.txtAlarmType_RAM.Size = New System.Drawing.Size(77, 20)
        Me.txtAlarmType_RAM.TabIndex = 16
        Me.txtAlarmType_RAM.Text = "Fault"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(39, 26)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(66, 13)
        Me.Label15.TabIndex = 14
        Me.Label15.Text = "Alarm Type :"
        '
        'SeverityRAM
        '
        Me.SeverityRAM.Location = New System.Drawing.Point(108, 52)
        Me.SeverityRAM.Name = "SeverityRAM"
        Me.SeverityRAM.Size = New System.Drawing.Size(306, 333)
        Me.SeverityRAM.TabIndex = 20
        '
        'HDD
        '
        Me.HDD.BackColor = System.Drawing.Color.Transparent
        Me.HDD.Controls.Add(Me.Label24)
        Me.HDD.Controls.Add(Me.txtIntervalMinite_HDD)
        Me.HDD.Controls.Add(Me.Label25)
        Me.HDD.Controls.Add(Me.chkActive_HDD)
        Me.HDD.Controls.Add(Me.btnSaveHDD)
        Me.HDD.Controls.Add(Me.Label19)
        Me.HDD.Controls.Add(Me.Label16)
        Me.HDD.Controls.Add(Me.txtRepeateCheck_HDD)
        Me.HDD.Controls.Add(Me.Label17)
        Me.HDD.Controls.Add(Me.txtAlarmType_HDD)
        Me.HDD.Controls.Add(Me.Label18)
        Me.HDD.Controls.Add(Me.TabHDD)
        Me.HDD.Location = New System.Drawing.Point(4, 22)
        Me.HDD.Name = "HDD"
        Me.HDD.Size = New System.Drawing.Size(511, 471)
        Me.HDD.TabIndex = 2
        Me.HDD.Text = "HDD"
        Me.HDD.UseVisualStyleBackColor = True
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(423, 407)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(23, 13)
        Me.Label24.TabIndex = 34
        Me.Label24.Text = "min"
        '
        'txtIntervalMinite_HDD
        '
        Me.txtIntervalMinite_HDD.Location = New System.Drawing.Point(338, 404)
        Me.txtIntervalMinite_HDD.MaxLength = 3
        Me.txtIntervalMinite_HDD.Name = "txtIntervalMinite_HDD"
        Me.txtIntervalMinite_HDD.Size = New System.Drawing.Size(79, 20)
        Me.txtIntervalMinite_HDD.TabIndex = 31
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(255, 407)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(77, 13)
        Me.Label25.TabIndex = 33
        Me.Label25.Text = "Interval Minute"
        '
        'chkActive_HDD
        '
        Me.chkActive_HDD.AutoSize = True
        Me.chkActive_HDD.Checked = True
        Me.chkActive_HDD.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkActive_HDD.Location = New System.Drawing.Point(154, 404)
        Me.chkActive_HDD.Name = "chkActive_HDD"
        Me.chkActive_HDD.Size = New System.Drawing.Size(56, 17)
        Me.chkActive_HDD.TabIndex = 32
        Me.chkActive_HDD.Text = "Active"
        Me.chkActive_HDD.UseVisualStyleBackColor = True
        '
        'btnSaveHDD
        '
        Me.btnSaveHDD.Location = New System.Drawing.Point(237, 437)
        Me.btnSaveHDD.Name = "btnSaveHDD"
        Me.btnSaveHDD.Size = New System.Drawing.Size(75, 23)
        Me.btnSaveHDD.TabIndex = 25
        Me.btnSaveHDD.Text = "Save"
        Me.btnSaveHDD.UseVisualStyleBackColor = True
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(25, 56)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(80, 13)
        Me.Label19.TabIndex = 24
        Me.Label19.Text = "Alarm Severity :"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(358, 30)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(26, 13)
        Me.Label16.TabIndex = 23
        Me.Label16.Text = "time"
        '
        'txtRepeateCheck_HDD
        '
        Me.txtRepeateCheck_HDD.Location = New System.Drawing.Point(306, 26)
        Me.txtRepeateCheck_HDD.MaxLength = 3
        Me.txtRepeateCheck_HDD.Name = "txtRepeateCheck_HDD"
        Me.txtRepeateCheck_HDD.Size = New System.Drawing.Size(49, 20)
        Me.txtRepeateCheck_HDD.TabIndex = 20
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(212, 29)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(82, 13)
        Me.Label17.TabIndex = 22
        Me.Label17.Text = "Repeat Check :"
        '
        'txtAlarmType_HDD
        '
        Me.txtAlarmType_HDD.Location = New System.Drawing.Point(118, 23)
        Me.txtAlarmType_HDD.Name = "txtAlarmType_HDD"
        Me.txtAlarmType_HDD.ReadOnly = True
        Me.txtAlarmType_HDD.Size = New System.Drawing.Size(77, 20)
        Me.txtAlarmType_HDD.TabIndex = 21
        Me.txtAlarmType_HDD.Text = "Fault"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(39, 26)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(66, 13)
        Me.Label18.TabIndex = 19
        Me.Label18.Text = "Alarm Type :"
        '
        'TabHDD
        '
        Me.TabHDD.Location = New System.Drawing.Point(118, 54)
        Me.TabHDD.Name = "TabHDD"
        Me.TabHDD.SelectedIndex = 0
        Me.TabHDD.Size = New System.Drawing.Size(374, 348)
        Me.TabHDD.TabIndex = 0
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(25, 59)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(77, 13)
        Me.Label8.TabIndex = 12
        Me.Label8.Text = "Alam Severity :"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(358, 30)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(26, 13)
        Me.Label9.TabIndex = 11
        Me.Label9.Text = "time"
        '
        'TextBox4
        '
        Me.TextBox4.Location = New System.Drawing.Point(306, 26)
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New System.Drawing.Size(49, 20)
        Me.TextBox4.TabIndex = 9
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(212, 29)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(88, 13)
        Me.Label10.TabIndex = 10
        Me.Label10.Text = "Repeate Check :"
        '
        'TextBox5
        '
        Me.TextBox5.Location = New System.Drawing.Point(118, 23)
        Me.TextBox5.Name = "TextBox5"
        Me.TextBox5.ReadOnly = True
        Me.TextBox5.Size = New System.Drawing.Size(77, 20)
        Me.TextBox5.TabIndex = 9
        Me.TextBox5.Text = "Fault"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(39, 26)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(63, 13)
        Me.Label11.TabIndex = 7
        Me.Label11.Text = "Alam Type :"
        '
        'SeverityControl2
        '
        Me.SeverityControl2.Location = New System.Drawing.Point(108, 52)
        Me.SeverityControl2.Name = "SeverityControl2"
        Me.SeverityControl2.Size = New System.Drawing.Size(306, 333)
        Me.SeverityControl2.TabIndex = 13
        '
        'frmConfigHW
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.SeaGreen
        Me.ClientSize = New System.Drawing.Size(588, 590)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtServerName)
        Me.Controls.Add(Me.txtIPAddress)
        Me.Controls.Add(Me.txtShopName)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label3)
        Me.Name = "frmConfigHW"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Config"
        Me.TabControl1.ResumeLayout(False)
        Me.CPU.ResumeLayout(False)
        Me.CPU.PerformLayout()
        Me.RAM.ResumeLayout(False)
        Me.RAM.PerformLayout()
        Me.HDD.ResumeLayout(False)
        Me.HDD.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtShopName As System.Windows.Forms.TextBox
    Friend WithEvents txtServerName As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtIPAddress As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents CPU As System.Windows.Forms.TabPage
    Friend WithEvents RAM As System.Windows.Forms.TabPage
    Friend WithEvents txtAlarmType_CPU As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtRepeateCheck_CPU As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents SeverityCPU As FaultManagement.SeverityControl
    Friend WithEvents HDD As System.Windows.Forms.TabPage
    Friend WithEvents SeverityRAM As FaultManagement.SeverityControl
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtRepeateCheck_RAM As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtAlarmType_RAM As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents SeverityControl2 As FaultManagement.SeverityControl
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents TextBox4 As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents TextBox5 As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtRepeateCheck_HDD As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtAlarmType_HDD As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents TabHDD As System.Windows.Forms.TabControl
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents btnSaveHDD As System.Windows.Forms.Button
    Friend WithEvents btnSaveCPU As System.Windows.Forms.Button
    Friend WithEvents btnSaveRAM As System.Windows.Forms.Button
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents txtIntervalMinite_CPU As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents chkActive_CPU As System.Windows.Forms.CheckBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents txtIntervalMinite_RAM As System.Windows.Forms.TextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents chkActive_RAM As System.Windows.Forms.CheckBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents txtIntervalMinite_HDD As System.Windows.Forms.TextBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents chkActive_HDD As System.Windows.Forms.CheckBox

End Class
