<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmConfigPort
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
        Me.DGVPortList = New System.Windows.Forms.DataGridView
        Me.HostIP = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.HostName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Port = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.id = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Delete = New System.Windows.Forms.DataGridViewButtonColumn
        Me.btnAdd = New System.Windows.Forms.Button
        Me.txtHostName = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtPortNumber = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtHostIP = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.chkSun = New System.Windows.Forms.CheckBox
        Me.chkMon = New System.Windows.Forms.CheckBox
        Me.chkTue = New System.Windows.Forms.CheckBox
        Me.chkWed = New System.Windows.Forms.CheckBox
        Me.chkThu = New System.Windows.Forms.CheckBox
        Me.chkFri = New System.Windows.Forms.CheckBox
        Me.chkSat = New System.Windows.Forms.CheckBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.chkAllDayEvent = New System.Windows.Forms.CheckBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.dpAlarmTimeFrom = New System.Windows.Forms.DateTimePicker
        Me.dpAlarmTimeTo = New System.Windows.Forms.DateTimePicker
        CType(Me.DGVPortList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DGVPortList
        '
        Me.DGVPortList.AllowUserToAddRows = False
        Me.DGVPortList.AllowUserToDeleteRows = False
        Me.DGVPortList.AllowUserToResizeColumns = False
        Me.DGVPortList.AllowUserToResizeRows = False
        Me.DGVPortList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVPortList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.HostIP, Me.HostName, Me.Port, Me.id, Me.Delete})
        Me.DGVPortList.Location = New System.Drawing.Point(12, 102)
        Me.DGVPortList.Name = "DGVPortList"
        Me.DGVPortList.RowHeadersVisible = False
        Me.DGVPortList.RowTemplate.Height = 30
        Me.DGVPortList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.DGVPortList.Size = New System.Drawing.Size(562, 557)
        Me.DGVPortList.TabIndex = 0
        '
        'HostIP
        '
        Me.HostIP.DataPropertyName = "HostIP"
        Me.HostIP.HeaderText = "Host IP"
        Me.HostIP.Name = "HostIP"
        Me.HostIP.ReadOnly = True
        '
        'HostName
        '
        Me.HostName.DataPropertyName = "HostName"
        Me.HostName.HeaderText = "Host Name"
        Me.HostName.Name = "HostName"
        Me.HostName.ReadOnly = True
        '
        'Port
        '
        Me.Port.DataPropertyName = "PortNumber"
        Me.Port.HeaderText = "Port"
        Me.Port.Name = "Port"
        Me.Port.ReadOnly = True
        '
        'id
        '
        Me.id.DataPropertyName = "id"
        Me.id.HeaderText = "id"
        Me.id.Name = "id"
        Me.id.ReadOnly = True
        Me.id.Visible = False
        '
        'Delete
        '
        Me.Delete.HeaderText = "Delete"
        Me.Delete.Name = "Delete"
        Me.Delete.ReadOnly = True
        Me.Delete.Text = "Delete"
        Me.Delete.UseColumnTextForButtonValue = True
        Me.Delete.Width = 80
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(492, 15)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(82, 71)
        Me.btnAdd.TabIndex = 1
        Me.btnAdd.Text = "Add"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'txtHostName
        '
        Me.txtHostName.Location = New System.Drawing.Point(70, 17)
        Me.txtHostName.Name = "txtHostName"
        Me.txtHostName.Size = New System.Drawing.Size(120, 20)
        Me.txtHostName.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(208, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(42, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Host IP"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(401, 20)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(26, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Port"
        '
        'txtPortNumber
        '
        Me.txtPortNumber.Location = New System.Drawing.Point(433, 17)
        Me.txtPortNumber.Name = "txtPortNumber"
        Me.txtPortNumber.Size = New System.Drawing.Size(40, 20)
        Me.txtPortNumber.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(9, 19)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(60, 13)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Host Name"
        '
        'txtHostIP
        '
        Me.txtHostIP.Location = New System.Drawing.Point(256, 17)
        Me.txtHostIP.Name = "txtHostIP"
        Me.txtHostIP.Size = New System.Drawing.Size(120, 20)
        Me.txtHostIP.TabIndex = 6
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(9, 48)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(59, 13)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Alarm Date"
        '
        'chkSun
        '
        Me.chkSun.AutoSize = True
        Me.chkSun.Checked = True
        Me.chkSun.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkSun.Location = New System.Drawing.Point(70, 48)
        Me.chkSun.Name = "chkSun"
        Me.chkSun.Size = New System.Drawing.Size(45, 17)
        Me.chkSun.TabIndex = 9
        Me.chkSun.Text = "Sun"
        Me.chkSun.UseVisualStyleBackColor = True
        '
        'chkMon
        '
        Me.chkMon.AutoSize = True
        Me.chkMon.Checked = True
        Me.chkMon.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkMon.Location = New System.Drawing.Point(120, 48)
        Me.chkMon.Name = "chkMon"
        Me.chkMon.Size = New System.Drawing.Size(47, 17)
        Me.chkMon.TabIndex = 10
        Me.chkMon.Text = "Mon"
        Me.chkMon.UseVisualStyleBackColor = True
        '
        'chkTue
        '
        Me.chkTue.AutoSize = True
        Me.chkTue.Checked = True
        Me.chkTue.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkTue.Location = New System.Drawing.Point(172, 48)
        Me.chkTue.Name = "chkTue"
        Me.chkTue.Size = New System.Drawing.Size(45, 17)
        Me.chkTue.TabIndex = 11
        Me.chkTue.Text = "Tue"
        Me.chkTue.UseVisualStyleBackColor = True
        '
        'chkWed
        '
        Me.chkWed.AutoSize = True
        Me.chkWed.Checked = True
        Me.chkWed.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkWed.Location = New System.Drawing.Point(222, 48)
        Me.chkWed.Name = "chkWed"
        Me.chkWed.Size = New System.Drawing.Size(49, 17)
        Me.chkWed.TabIndex = 12
        Me.chkWed.Text = "Wed"
        Me.chkWed.UseVisualStyleBackColor = True
        '
        'chkThu
        '
        Me.chkThu.AutoSize = True
        Me.chkThu.Checked = True
        Me.chkThu.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkThu.Location = New System.Drawing.Point(273, 48)
        Me.chkThu.Name = "chkThu"
        Me.chkThu.Size = New System.Drawing.Size(45, 17)
        Me.chkThu.TabIndex = 13
        Me.chkThu.Text = "Thu"
        Me.chkThu.UseVisualStyleBackColor = True
        '
        'chkFri
        '
        Me.chkFri.AutoSize = True
        Me.chkFri.Checked = True
        Me.chkFri.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkFri.Location = New System.Drawing.Point(323, 48)
        Me.chkFri.Name = "chkFri"
        Me.chkFri.Size = New System.Drawing.Size(37, 17)
        Me.chkFri.TabIndex = 14
        Me.chkFri.Text = "Fri"
        Me.chkFri.UseVisualStyleBackColor = True
        '
        'chkSat
        '
        Me.chkSat.AutoSize = True
        Me.chkSat.Checked = True
        Me.chkSat.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkSat.Location = New System.Drawing.Point(373, 48)
        Me.chkSat.Name = "chkSat"
        Me.chkSat.Size = New System.Drawing.Size(42, 17)
        Me.chkSat.TabIndex = 15
        Me.chkSat.Text = "Sat"
        Me.chkSat.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(9, 73)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(59, 13)
        Me.Label5.TabIndex = 16
        Me.Label5.Text = "Alarm Time"
        '
        'chkAllDayEvent
        '
        Me.chkAllDayEvent.AutoSize = True
        Me.chkAllDayEvent.Checked = True
        Me.chkAllDayEvent.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAllDayEvent.Location = New System.Drawing.Point(211, 73)
        Me.chkAllDayEvent.Name = "chkAllDayEvent"
        Me.chkAllDayEvent.Size = New System.Drawing.Size(90, 17)
        Me.chkAllDayEvent.TabIndex = 17
        Me.chkAllDayEvent.Text = "All Day Event"
        Me.chkAllDayEvent.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(121, 73)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(16, 13)
        Me.Label6.TabIndex = 19
        Me.Label6.Text = "to"
        '
        'dpAlarmTimeFrom
        '
        Me.dpAlarmTimeFrom.Enabled = False
        Me.dpAlarmTimeFrom.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.dpAlarmTimeFrom.Location = New System.Drawing.Point(70, 70)
        Me.dpAlarmTimeFrom.Name = "dpAlarmTimeFrom"
        Me.dpAlarmTimeFrom.ShowUpDown = True
        Me.dpAlarmTimeFrom.Size = New System.Drawing.Size(49, 20)
        Me.dpAlarmTimeFrom.TabIndex = 21
        Me.dpAlarmTimeFrom.Value = New Date(2013, 12, 24, 11, 14, 0, 0)
        '
        'dpAlarmTimeTo
        '
        Me.dpAlarmTimeTo.Enabled = False
        Me.dpAlarmTimeTo.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.dpAlarmTimeTo.Location = New System.Drawing.Point(141, 70)
        Me.dpAlarmTimeTo.Name = "dpAlarmTimeTo"
        Me.dpAlarmTimeTo.ShowUpDown = True
        Me.dpAlarmTimeTo.Size = New System.Drawing.Size(49, 20)
        Me.dpAlarmTimeTo.TabIndex = 22
        Me.dpAlarmTimeTo.Value = New Date(2013, 12, 24, 11, 14, 0, 0)
        '
        'frmConfigPort
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(586, 671)
        Me.Controls.Add(Me.dpAlarmTimeTo)
        Me.Controls.Add(Me.dpAlarmTimeFrom)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.chkAllDayEvent)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.chkSat)
        Me.Controls.Add(Me.chkFri)
        Me.Controls.Add(Me.chkThu)
        Me.Controls.Add(Me.chkWed)
        Me.Controls.Add(Me.chkTue)
        Me.Controls.Add(Me.chkMon)
        Me.Controls.Add(Me.chkSun)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtHostIP)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtPortNumber)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtHostName)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.DGVPortList)
        Me.Name = "frmConfigPort"
        Me.Text = "Config Monitor Port"
        CType(Me.DGVPortList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DGVPortList As System.Windows.Forms.DataGridView
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents txtHostName As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtPortNumber As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtHostIP As System.Windows.Forms.TextBox
    Friend WithEvents HostIP As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents HostName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Port As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Delete As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents chkSun As System.Windows.Forms.CheckBox
    Friend WithEvents chkMon As System.Windows.Forms.CheckBox
    Friend WithEvents chkTue As System.Windows.Forms.CheckBox
    Friend WithEvents chkWed As System.Windows.Forms.CheckBox
    Friend WithEvents chkThu As System.Windows.Forms.CheckBox
    Friend WithEvents chkFri As System.Windows.Forms.CheckBox
    Friend WithEvents chkSat As System.Windows.Forms.CheckBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents chkAllDayEvent As System.Windows.Forms.CheckBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents dpAlarmTimeFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents dpAlarmTimeTo As System.Windows.Forms.DateTimePicker
End Class
