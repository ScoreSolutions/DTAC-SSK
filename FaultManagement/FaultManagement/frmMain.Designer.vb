﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
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
        Me.components = New System.ComponentModel.Container
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.DGVLog = New System.Windows.Forms.DataGridView
        Me.id = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.AlarmCode = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ServerName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.SpecificProblem = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.HostIP = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.SysLocation = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.AlarmQty = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.CreateDate = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.UpdateDate = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Severity = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.DGVConfigSetting = New System.Windows.Forms.DataGridView
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.btnAddPortConfig = New System.Windows.Forms.Button
        Me.btnAddSwConfig = New System.Windows.Forms.Button
        Me.BtnHWConfig = New System.Windows.Forms.Button
        Me.tmListAlarm = New System.Windows.Forms.Timer(Me.components)
        Me.tmListConfigData = New System.Windows.Forms.Timer(Me.components)
        Me.NotifyIcon1 = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.tmMonitor = New System.Windows.Forms.Timer(Me.components)
        Me.tmCheckAlive = New System.Windows.Forms.Timer(Me.components)
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.DGVLog, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        CType(Me.DGVConfigSetting, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Font = New System.Drawing.Font("Cordia New", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1104, 652)
        Me.TabControl1.TabIndex = 1
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.DGVLog)
        Me.TabPage1.Location = New System.Drawing.Point(4, 35)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(1096, 613)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Processes"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'DGVLog
        '
        Me.DGVLog.AllowUserToAddRows = False
        Me.DGVLog.AllowUserToDeleteRows = False
        Me.DGVLog.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.DGVLog.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.DGVLog.BorderStyle = System.Windows.Forms.BorderStyle.None
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Cordia New", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGVLog.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DGVLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVLog.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.id, Me.AlarmCode, Me.ServerName, Me.SpecificProblem, Me.HostIP, Me.SysLocation, Me.AlarmQty, Me.CreateDate, Me.UpdateDate, Me.Severity})
        Me.DGVLog.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGVLog.Location = New System.Drawing.Point(3, 3)
        Me.DGVLog.Name = "DGVLog"
        Me.DGVLog.RowHeadersVisible = False
        Me.DGVLog.Size = New System.Drawing.Size(1090, 607)
        Me.DGVLog.TabIndex = 2
        '
        'id
        '
        Me.id.DataPropertyName = "id"
        Me.id.HeaderText = "id"
        Me.id.Name = "id"
        Me.id.ReadOnly = True
        Me.id.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.id.Visible = False
        Me.id.Width = 29
        '
        'AlarmCode
        '
        Me.AlarmCode.DataPropertyName = "AlarmCode"
        Me.AlarmCode.HeaderText = "AlarmCode"
        Me.AlarmCode.Name = "AlarmCode"
        Me.AlarmCode.ReadOnly = True
        Me.AlarmCode.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'ServerName
        '
        Me.ServerName.DataPropertyName = "ServerName"
        Me.ServerName.HeaderText = "HostName"
        Me.ServerName.Name = "ServerName"
        Me.ServerName.ReadOnly = True
        Me.ServerName.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.ServerName.Width = 96
        '
        'SpecificProblem
        '
        Me.SpecificProblem.DataPropertyName = "SpecificProblem"
        Me.SpecificProblem.HeaderText = "Description"
        Me.SpecificProblem.Name = "SpecificProblem"
        Me.SpecificProblem.ReadOnly = True
        Me.SpecificProblem.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.SpecificProblem.Width = 101
        '
        'HostIP
        '
        Me.HostIP.DataPropertyName = "HostIP"
        Me.HostIP.HeaderText = "Host IP"
        Me.HostIP.Name = "HostIP"
        Me.HostIP.ReadOnly = True
        Me.HostIP.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.HostIP.Width = 76
        '
        'SysLocation
        '
        Me.SysLocation.DataPropertyName = "SysLocation"
        Me.SysLocation.HeaderText = "SysLocation"
        Me.SysLocation.Name = "SysLocation"
        Me.SysLocation.ReadOnly = True
        Me.SysLocation.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.SysLocation.Width = 104
        '
        'AlarmQty
        '
        Me.AlarmQty.DataPropertyName = "AlarmQty"
        Me.AlarmQty.HeaderText = "No. of Update"
        Me.AlarmQty.Name = "AlarmQty"
        Me.AlarmQty.ReadOnly = True
        Me.AlarmQty.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.AlarmQty.Width = 114
        '
        'CreateDate
        '
        Me.CreateDate.DataPropertyName = "CreateDate"
        Me.CreateDate.HeaderText = "Create Date"
        Me.CreateDate.Name = "CreateDate"
        Me.CreateDate.ReadOnly = True
        Me.CreateDate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.CreateDate.Width = 103
        '
        'UpdateDate
        '
        Me.UpdateDate.DataPropertyName = "UpdateDate"
        Me.UpdateDate.HeaderText = "Update Date"
        Me.UpdateDate.Name = "UpdateDate"
        Me.UpdateDate.ReadOnly = True
        Me.UpdateDate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.UpdateDate.Width = 108
        '
        'Severity
        '
        Me.Severity.DataPropertyName = "Severity"
        Me.Severity.HeaderText = "Severity"
        Me.Severity.Name = "Severity"
        Me.Severity.ReadOnly = True
        Me.Severity.Visible = False
        Me.Severity.Width = 79
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.DGVConfigSetting)
        Me.TabPage2.Controls.Add(Me.Panel1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 35)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(1096, 613)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Config Setting"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'DGVConfigSetting
        '
        Me.DGVConfigSetting.AllowUserToAddRows = False
        Me.DGVConfigSetting.AllowUserToDeleteRows = False
        Me.DGVConfigSetting.AllowUserToResizeRows = False
        Me.DGVConfigSetting.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DGVConfigSetting.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.DGVConfigSetting.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Cordia New", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGVConfigSetting.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.DGVConfigSetting.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVConfigSetting.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGVConfigSetting.Location = New System.Drawing.Point(3, 49)
        Me.DGVConfigSetting.MultiSelect = False
        Me.DGVConfigSetting.Name = "DGVConfigSetting"
        Me.DGVConfigSetting.ReadOnly = True
        Me.DGVConfigSetting.RowHeadersVisible = False
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter
        Me.DGVConfigSetting.RowsDefaultCellStyle = DataGridViewCellStyle3
        Me.DGVConfigSetting.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DGVConfigSetting.Size = New System.Drawing.Size(1090, 561)
        Me.DGVConfigSetting.TabIndex = 1
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.Control
        Me.Panel1.Controls.Add(Me.btnAddPortConfig)
        Me.Panel1.Controls.Add(Me.btnAddSwConfig)
        Me.Panel1.Controls.Add(Me.BtnHWConfig)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(3, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1090, 46)
        Me.Panel1.TabIndex = 0
        '
        'btnAddPortConfig
        '
        Me.btnAddPortConfig.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAddPortConfig.Location = New System.Drawing.Point(977, 6)
        Me.btnAddPortConfig.Name = "btnAddPortConfig"
        Me.btnAddPortConfig.Size = New System.Drawing.Size(108, 37)
        Me.btnAddPortConfig.TabIndex = 2
        Me.btnAddPortConfig.Text = "Add Port Config"
        Me.btnAddPortConfig.UseVisualStyleBackColor = True
        '
        'btnAddSwConfig
        '
        Me.btnAddSwConfig.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAddSwConfig.Location = New System.Drawing.Point(865, 6)
        Me.btnAddSwConfig.Name = "btnAddSwConfig"
        Me.btnAddSwConfig.Size = New System.Drawing.Size(108, 37)
        Me.btnAddSwConfig.TabIndex = 1
        Me.btnAddSwConfig.Text = "Add SW Config"
        Me.btnAddSwConfig.UseVisualStyleBackColor = True
        '
        'BtnHWConfig
        '
        Me.BtnHWConfig.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnHWConfig.Location = New System.Drawing.Point(752, 6)
        Me.BtnHWConfig.Name = "BtnHWConfig"
        Me.BtnHWConfig.Size = New System.Drawing.Size(108, 37)
        Me.BtnHWConfig.TabIndex = 0
        Me.BtnHWConfig.Text = "Add HW Config"
        Me.BtnHWConfig.UseVisualStyleBackColor = True
        '
        'tmListAlarm
        '
        Me.tmListAlarm.Enabled = True
        Me.tmListAlarm.Interval = 60000
        '
        'tmListConfigData
        '
        Me.tmListConfigData.Enabled = True
        Me.tmListConfigData.Interval = 60000
        '
        'NotifyIcon1
        '
        Me.NotifyIcon1.Icon = CType(resources.GetObject("NotifyIcon1.Icon"), System.Drawing.Icon)
        Me.NotifyIcon1.Text = "Fault Management"
        Me.NotifyIcon1.Visible = True
        '
        'tmMonitor
        '
        Me.tmMonitor.Enabled = True
        Me.tmMonitor.Interval = 60000
        '
        'tmCheckAlive
        '
        Me.tmCheckAlive.Enabled = True
        Me.tmCheckAlive.Interval = 60000
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1104, 652)
        Me.Controls.Add(Me.TabControl1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Fault Management"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        CType(Me.DGVLog, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        CType(Me.DGVConfigSetting, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Private WithEvents DGVConfigSetting As System.Windows.Forms.DataGridView
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents BtnHWConfig As System.Windows.Forms.Button
    Friend WithEvents btnAddSwConfig As System.Windows.Forms.Button
    Friend WithEvents tmListAlarm As System.Windows.Forms.Timer
    Friend WithEvents tmListConfigData As System.Windows.Forms.Timer
    Friend WithEvents DGVLog As System.Windows.Forms.DataGridView
    Friend WithEvents id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AlarmCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ServerName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SpecificProblem As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents HostIP As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SysLocation As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AlarmQty As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CreateDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents UpdateDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Severity As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnAddPortConfig As System.Windows.Forms.Button
    Friend WithEvents NotifyIcon1 As System.Windows.Forms.NotifyIcon
    Friend WithEvents tmMonitor As System.Windows.Forms.Timer
    Friend WithEvents tmCheckAlive As System.Windows.Forms.Timer
End Class
