Imports FaultManagement.Org.Mentalis.Files
Imports System.IO
Public Class frmConfigHW

    Dim dir As String = Application.StartupPath & "\config\"
    Public IsAddConfig As Boolean = False

    Private Sub frmConfig_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Directory.Exists(dir) = False Then
            Directory.CreateDirectory(dir)
        End If

        Dim ini As New IniReader(Application.StartupPath & "\Config.ini")
        ini.Section = "Setting"
        Dim IsShopInstall As String = ini.ReadString("IsShopInstall")
        If IsShopInstall = "Y" Then
            txtShopName.Text = Engine.Common.FunctionEng.GetShopConfig("s_name_en")
        Else
            txtShopName.Text = ""
        End If

        txtServerName.Text = Environment.MachineName
        txtIPAddress.Text = Engine.Common.FunctionEng.GetIPAddress
    End Sub

    Public Sub GenHDDTab()
        Dim driveinfo As New Engine.Info.DriveInfoENG
        Dim dt As New DataTable
        dt = driveinfo.GetDriveInfoToDT
        If dt.Rows.Count > 0 Then
            TabHDD.TabPages.Clear()
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim _SeverityControl As New SeverityControl
                _SeverityControl.Name = "Severity_" & dt.Rows(i).Item(0)
                Dim TabPage As New System.Windows.Forms.TabPage
                TabPage.Text = dt.Rows(i).Item(0) & ""
                TabPage.Controls.Add(_SeverityControl)
                TabHDD.TabPages.Add(TabPage)
            Next
        End If
    End Sub

    Private Sub btnSaveCPU_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveCPU.Click
        Try

            If CInt(IIf(SeverityCPU.txtValueOver_Minor.Text = "", 0, SeverityCPU.txtValueOver_Minor.Text)) >= _
            CInt(IIf(SeverityCPU.txtValueOver_Major.Text = "", 0, SeverityCPU.txtValueOver_Major.Text)) Then
                MessageBox.Show("กรุณาระบุ  Minor Value Over น้อยกว่า  Major Value Over")
                Exit Sub
            End If

            If CInt(IIf(SeverityCPU.txtValueOver_Major.Text = "", 0, SeverityCPU.txtValueOver_Major.Text)) >= _
            CInt(IIf(SeverityCPU.txtValueOverCritical.Text = "", 0, SeverityCPU.txtValueOverCritical.Text)) Then
                MessageBox.Show("กรุณาระบุ  Major Value Over น้อยกว่า Critical Value Over")
                Exit Sub
            End If

            Dim eng As New Engine.Config.CPUConfigENG

            '== Minor ==
            Dim AlarmMethodMi As String = ""
            If SeverityCPU.rdSnmp_Minor.Checked Then
                AlarmMethodMi = "SNMP"
            End If
            If SeverityCPU.rdSMS_Minor.Checked Then
                AlarmMethodMi = "SMS"
            End If
            If SeverityCPU.rdEmail_Minor.Checked Then
                AlarmMethodMi = "EMAIL"
            End If

            Dim Mi As Engine.Config.CPUConfigENG.AlarmSeverity
            Mi.AlarmCode = "ALM_LOG_CPU_MINOR"
            Mi.AlarmMethod = AlarmMethodMi
            Mi.Severity = "MINOR"
            Mi.ValueOver = IIf(SeverityCPU.txtValueOver_Minor.Text = "", 0, SeverityCPU.txtValueOver_Minor.Text)
            '===========

            '== Major ==
            Dim AlarmMethodMj As String = ""
            If SeverityCPU.rdSnmp_Major.Checked Then
                AlarmMethodMj = "SNMP"
            End If
            If SeverityCPU.rdSMS_Major.Checked Then
                AlarmMethodMj = "SMS"
            End If
            If SeverityCPU.rdEmail_Major.Checked Then
                AlarmMethodMj = "EMAIL"
            End If

            Dim Mj As Engine.Config.CPUConfigENG.AlarmSeverity
            Mj.AlarmCode = "ALM_LOG_CPU_MAJOR"
            Mj.AlarmMethod = AlarmMethodMj
            Mj.Severity = "MAJOR"
            Mj.ValueOver = IIf(SeverityCPU.txtValueOver_Major.Text = "", 0, SeverityCPU.txtValueOver_Major.Text)
            '===========

            '== Critical ==
            Dim AlarmMethodCt As String = ""
            If SeverityCPU.rdSnmp_Critical.Checked Then
                AlarmMethodCt = "SNMP"
            End If
            If SeverityCPU.rdSMS_Critical.Checked Then
                AlarmMethodCt = "SMS"
            End If
            If SeverityCPU.rdEmail_Critical.Checked Then
                AlarmMethodCt = "EMAIL"
            End If

            Dim Ct As Engine.Config.CPUConfigENG.AlarmSeverity
            Ct.AlarmCode = "ALM_LOG_CPU_CRITICAL"
            Ct.AlarmMethod = AlarmMethodCt
            Ct.Severity = "CRITICAL"
            Ct.ValueOver = IIf(SeverityCPU.txtValueOverCritical.Text = "", 0, SeverityCPU.txtValueOverCritical.Text)
            '===========

            Dim RepeateCheck As Integer = IIf(txtRepeateCheck_CPU.Text = "", 0, txtRepeateCheck_CPU.Text)
            Dim IntervalMinite As Integer = IIf(txtIntervalMinite_CPU.Text = "", 0, txtIntervalMinite_CPU.Text)
            Dim Active As String = IIf(chkActive_CPU.Checked, "Y", "N")

            Dim ini As New IniReader(Application.StartupPath & "\Config.ini")
            ini.Section = "Setting"
            Dim CPUConfigFileName As String = txtServerName.Text & "_CONFIG_CPU_PROCESS.xml"
            Dim path As String = Application.StartupPath & "\config\" & CPUConfigFileName
            eng.SaveCPUConfig(RepeateCheck, Mi, Mj, Ct, IntervalMinite, Active, ini.ReadString("IsShopInstall")).Save(path)

            Try
                Dim ws As New FaultManagementService.FaultManagementService
                ws.Timeout = 10000
                ws.Url = ini.ReadString("DCWebserviceURL")
                ws.SendConfigFileToDC(IO.File.ReadAllBytes(path), CPUConfigFileName, Environment.MachineName)
                ws = Nothing

                MessageBox.Show("บันทึกข้อมูลเรียบร้อยแล้ว")
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            ini = Nothing
            frmMain.SetListConfigData()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try

    End Sub

    Private Sub btnSaveRAM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveRAM.Click
        Try

            If CInt(IIf(SeverityRAM.txtValueOver_Minor.Text = "", 0, SeverityRAM.txtValueOver_Minor.Text)) >= _
            CInt(IIf(SeverityRAM.txtValueOver_Major.Text = "", 0, SeverityRAM.txtValueOver_Major.Text)) Then
                MessageBox.Show("กรุณาระบุ  Minor Value Over น้อยกว่า  Major Value Over")
                Exit Sub
            End If

            If CInt(IIf(SeverityRAM.txtValueOver_Major.Text = "", 0, SeverityRAM.txtValueOver_Major.Text)) >= _
            CInt(IIf(SeverityRAM.txtValueOverCritical.Text = "", 0, SeverityRAM.txtValueOverCritical.Text)) Then
                MessageBox.Show("กรุณาระบุ  Major Value Over น้อยกว่า Critical Value Over")
                Exit Sub
            End If

            Dim eng As New Engine.Config.RamConfigENG

            '== Minor ==
            Dim AlarmMethodMi As String = ""
            If SeverityRAM.rdSnmp_Minor.Checked Then
                AlarmMethodMi = "SNMP"
            End If
            If SeverityRAM.rdSMS_Minor.Checked Then
                AlarmMethodMi = "SMS"
            End If
            If SeverityRAM.rdEmail_Minor.Checked Then
                AlarmMethodMi = "EMAIL"
            End If

            Dim Mi As Engine.Config.RamConfigENG.AlarmSeverity
            Mi.AlarmCode = "ALM_LOG_RAM_MINOR"
            Mi.AlarmMethod = AlarmMethodMi
            Mi.Severity = "MINOR"
            Mi.ValueOver = IIf(SeverityRAM.txtValueOver_Minor.Text = "", 0, SeverityRAM.txtValueOver_Minor.Text)
            '===========

            '== Major ==
            Dim AlarmMethodMj As String = ""
            If SeverityRAM.rdSnmp_Major.Checked Then
                AlarmMethodMj = "SNMP"
            End If
            If SeverityRAM.rdSMS_Major.Checked Then
                AlarmMethodMj = "SMS"
            End If
            If SeverityRAM.rdEmail_Major.Checked Then
                AlarmMethodMj = "EMAIL"
            End If

            Dim Mj As Engine.Config.RamConfigENG.AlarmSeverity
            Mj.AlarmCode = "ALM_LOG_RAM_MAJOR"
            Mj.AlarmMethod = AlarmMethodMj
            Mj.Severity = "MAJOR"
            Mj.ValueOver = IIf(SeverityRAM.txtValueOver_Major.Text = "", 0, SeverityRAM.txtValueOver_Major.Text)
            '===========

            '== Critical ==
            Dim AlarmMethodCt As String = ""
            If SeverityRAM.rdSnmp_Critical.Checked Then
                AlarmMethodCt = "SNMP"
            End If
            If SeverityRAM.rdSMS_Critical.Checked Then
                AlarmMethodCt = "SMS"
            End If
            If SeverityRAM.rdEmail_Critical.Checked Then
                AlarmMethodCt = "EMAIL"
            End If

            Dim Ct As Engine.Config.RamConfigENG.AlarmSeverity
            Ct.AlarmCode = "ALM_LOG_RAM_CRITICAL"
            Ct.AlarmMethod = AlarmMethodCt
            Ct.Severity = "CRITICAL"
            Ct.ValueOver = IIf(SeverityRAM.txtValueOverCritical.Text = "", 0, SeverityRAM.txtValueOverCritical.Text)
            '===========

            Dim RepeateCheck As Integer = IIf(txtRepeateCheck_RAM.Text = "", 0, txtRepeateCheck_RAM.Text)
            Dim IntervalMinite As Integer = IIf(txtIntervalMinite_RAM.Text = "", 0, txtIntervalMinite_RAM.Text)
            Dim Active As String = IIf(chkActive_RAM.Checked, "Y", "N")

            Dim ini As New IniReader(Application.StartupPath & "\Config.ini")
            ini.Section = "Setting"

            Dim RAMConfigFileName As String = txtServerName.Text & "_CONFIG_RAM_PROCESS.xml"
            Dim path As String = Application.StartupPath & "\Config\" & RAMConfigFileName
            eng.SaveRamConfig(RepeateCheck, Mi, Mj, Ct, IntervalMinite, Active, ini.ReadString("IsShopInstall")).Save(path)

            Try
                Dim ws As New FaultManagementService.FaultManagementService
                ws.Timeout = 10000
                ws.Url = ini.ReadString("DCWebserviceURL")
                ws.SendConfigFileToDC(IO.File.ReadAllBytes(path), RAMConfigFileName, Environment.MachineName)

                ws = Nothing
                MessageBox.Show("บันทีกข้อมูลเรียบร้อย")
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            ini = Nothing
            frmMain.SetListConfigData()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub btnSaveHDD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveHDD.Click
        Try

            Dim eng As New Engine.Config.HDDConfigENG
            Dim inf As New Engine.Info.DriveInfoENG
            Dim dt As DataTable = inf.GetDriveInfoToDT()
            If dt.Rows.Count > 0 Then
                dt.Columns.Add("MinorSeverity", GetType(Engine.Config.ConfigENG.AlarmSeverity))
                dt.Columns.Add("MajorSeverity", GetType(Engine.Config.ConfigENG.AlarmSeverity))
                dt.Columns.Add("CriticalSeverity", GetType(Engine.Config.ConfigENG.AlarmSeverity))

                For i As Integer = 0 To dt.Rows.Count - 1

                    Dim SeverityHDD As New SeverityControl
                    SeverityHDD = DirectCast(TabHDD.TabPages(i), System.Windows.Forms.TabPage).Controls.Item(0)

                    If CInt(IIf(SeverityHDD.txtValueOver_Minor.Text = "", 0, SeverityHDD.txtValueOver_Minor.Text)) >= _
                    CInt(IIf(SeverityHDD.txtValueOver_Major.Text = "", 0, SeverityHDD.txtValueOver_Major.Text)) Then
                        MessageBox.Show("กรุณาระบุ  Minor Value Over น้อยกว่า  Major Value Over : Drive " & TabHDD.TabPages(i).Text)
                        Exit Sub
                    End If

                    If CInt(IIf(SeverityHDD.txtValueOver_Major.Text = "", 0, SeverityHDD.txtValueOver_Major.Text)) >= _
                    CInt(IIf(SeverityHDD.txtValueOverCritical.Text = "", 0, SeverityHDD.txtValueOverCritical.Text)) Then
                        MessageBox.Show("กรุณาระบุ  Major Value Over น้อยกว่า Critical Value Over : Drive " & TabHDD.TabPages(i).Text)
                        Exit Sub
                    End If

                    '==== Minor ======
                    Dim AlarmMethodMi As String = ""
                    If SeverityHDD.rdSnmp_Minor.Checked Then
                        AlarmMethodMi = "SNMP"
                    End If
                    If SeverityHDD.rdSMS_Minor.Checked Then
                        AlarmMethodMi = "SMS"
                    End If
                    If SeverityHDD.rdEmail_Minor.Checked Then
                        AlarmMethodMi = "EMAIL"
                    End If

                    Dim MinorSeverity As Engine.Config.ConfigENG.AlarmSeverity
                    MinorSeverity.AlarmCode = "ALM_LOG_HD_MINOR"
                    MinorSeverity.AlarmMethod = AlarmMethodMi
                    MinorSeverity.Severity = "MINOR"
                    MinorSeverity.ValueOver = IIf(SeverityHDD.txtValueOver_Minor.Text = "", 0, SeverityHDD.txtValueOver_Minor.Text)
                    dt.Rows(i)("MinorSeverity") = MinorSeverity
                    '=================


                    '===== Major ========
                    Dim AlarmMethodMj As String = ""
                    If SeverityHDD.rdSnmp_Major.Checked Then
                        AlarmMethodMj = "SNMP"
                    End If
                    If SeverityHDD.rdSMS_Major.Checked Then
                        AlarmMethodMj = "SMS"
                    End If
                    If SeverityHDD.rdEmail_Major.Checked Then
                        AlarmMethodMj = "EMAIL"
                    End If

                    Dim MajorSeverity As Engine.Config.ConfigENG.AlarmSeverity
                    MajorSeverity.AlarmCode = "ALM_LOG_HD_MAJOR"
                    MajorSeverity.AlarmMethod = AlarmMethodMj
                    MajorSeverity.Severity = "MAJOR"
                    MajorSeverity.ValueOver = IIf(SeverityHDD.txtValueOver_Major.Text = "", 0, SeverityHDD.txtValueOver_Major.Text)
                    dt.Rows(i)("MajorSeverity") = MajorSeverity
                    '======================


                    '====== Critical ======
                    Dim AlarmMethodCt As String = ""
                    If SeverityHDD.rdSnmp_Critical.Checked Then
                        AlarmMethodCt = "SNMP"
                    End If
                    If SeverityHDD.rdSMS_Critical.Checked Then
                        AlarmMethodCt = "SMS"
                    End If
                    If SeverityHDD.rdEmail_Critical.Checked Then
                        AlarmMethodCt = "EMAIL"
                    End If

                    Dim CriticalSeverity As Engine.Config.ConfigENG.AlarmSeverity
                    CriticalSeverity.AlarmCode = "ALM_LOG_HD_CRITICAL"
                    CriticalSeverity.AlarmMethod = AlarmMethodCt
                    CriticalSeverity.Severity = "CRITICAL"
                    CriticalSeverity.ValueOver = IIf(SeverityHDD.txtValueOverCritical.Text = "", 0, SeverityHDD.txtValueOverCritical.Text)
                    dt.Rows(i)("CriticalSeverity") = CriticalSeverity
                    '=========================
                Next

                Dim RepeateCheck As Integer = IIf(txtRepeateCheck_HDD.Text = "", 0, txtRepeateCheck_HDD.Text)
                Dim IntervalMinite As Integer = IIf(txtIntervalMinite_HDD.Text = "", 0, txtIntervalMinite_HDD.Text)
                Dim Active As String = IIf(chkActive_HDD.Checked, "Y", "N")

                Dim ini As New IniReader(Application.StartupPath & "\Config.ini")
                ini.Section = "Setting"
                Dim HDDConfigFileName As String = txtServerName.Text & "_CONFIG_HDD_PROCESS.xml"
                Dim path As String = Application.StartupPath & "\config\" & HDDConfigFileName
                eng.SaveHDDConfig(dt, RepeateCheck, IntervalMinite, Active, ini.ReadString("IsShopInstall")).Save(path)

                Try
                    Dim ws As New FaultManagementService.FaultManagementService
                    ws.Timeout = 10000
                    ws.Url = ini.ReadString("DCWebserviceURL")
                    ws.SendConfigFileToDC(IO.File.ReadAllBytes(path), HDDConfigFileName, Environment.MachineName)
                    ws = Nothing
                    MessageBox.Show("บันทึกข้อมูลเรียบร้อย")
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try
                ini = Nothing
            End If
            frmMain.SetListConfigData()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub txtRepeateCheck_CPU_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtRepeateCheck_CPU.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If

    End Sub

    Private Sub txtRepeateCheck_RAM_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtRepeateCheck_RAM.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub txtRepeateCheck_HDD_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtRepeateCheck_HDD.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub txtIntervalMinite_CPU_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtIntervalMinite_CPU.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub txtIntervalMinite_RAM_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtIntervalMinite_RAM.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub txtIntervalMinite_HDD_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtIntervalMinite_HDD.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub


    Public Sub SetCPUData(ByVal XMLFileName As String)
        If InStr(XMLFileName, "CPU") > 0 Then
            TabControl1.SelectedTab = CPU
            Dim cfg As New Engine.Config.CPUConfigENG
            cfg.GetConfigFromXML(XMLFileName)

            txtShopName.Text = cfg.ShopName
            txtServerName.Text = cfg.ServerName
            txtIPAddress.Text = cfg.IPAddress
            txtAlarmType_CPU.Text = cfg.AlarmType
            txtRepeateCheck_CPU.Text = cfg.RepeateCheck
            SeverityCPU.txtValueOver_Minor.Text = cfg.SeverityMinor.ValueOver
            If cfg.SeverityMinor.AlarmMethod = "SNMP" Then
                SeverityCPU.rdSnmp_Minor.Checked = True
            ElseIf cfg.SeverityMinor.AlarmMethod = "SMS" Then
                SeverityCPU.rdSMS_Minor.Checked = True
            ElseIf cfg.SeverityMinor.AlarmMethod = "EMAIL" Then
                SeverityCPU.rdEmail_Minor.Checked = True
            End If

            SeverityCPU.txtValueOver_Major.Text = cfg.SeverityMajor.ValueOver
            If cfg.SeverityMajor.AlarmMethod = "SNMP" Then
                SeverityCPU.rdSnmp_Major.Checked = True
            ElseIf cfg.SeverityMajor.AlarmMethod = "SMS" Then
                SeverityCPU.rdSMS_Major.Checked = True
            ElseIf cfg.SeverityMajor.AlarmMethod = "EMAIL" Then
                SeverityCPU.rdEmail_Major.Checked = True
            End If

            SeverityCPU.txtValueOverCritical.Text = cfg.SeverityCritical.ValueOver
            If cfg.SeverityCritical.AlarmMethod = "SNMP" Then
                SeverityCPU.rdSnmp_Critical.Checked = True
            ElseIf cfg.SeverityCritical.AlarmMethod = "SMS" Then
                SeverityCPU.rdSMS_Critical.Checked = True
            ElseIf cfg.SeverityCritical.AlarmMethod = "EMAIL" Then
                SeverityCPU.rdEmail_Critical.Checked = True
            End If

            chkActive_CPU.Checked = (cfg.Active = "Y")
            txtIntervalMinite_CPU.Text = cfg.IntervalMinute
        End If
    End Sub

    Public Sub SetRAMData(ByVal XMLFileName As String)
        If InStr(XMLFileName, "RAM") > 0 Then
            TabControl1.SelectedTab = RAM
            Dim cfg As New Engine.Config.RamConfigENG
            cfg.GetConfigFromXML(XMLFileName)

            txtShopName.Text = cfg.ShopName
            txtServerName.Text = cfg.ServerName
            txtIPAddress.Text = cfg.IPAddress
            txtAlarmType_RAM.Text = cfg.AlarmType
            txtRepeateCheck_RAM.Text = cfg.RepeateCheck
            SeverityRAM.txtValueOver_Minor.Text = cfg.SeverityMinor.ValueOver
            If cfg.SeverityMinor.AlarmMethod = "SNMP" Then
                SeverityRAM.rdSnmp_Minor.Checked = True
            ElseIf cfg.SeverityMinor.AlarmMethod = "SMS" Then
                SeverityRAM.rdSMS_Minor.Checked = True
            ElseIf cfg.SeverityMinor.AlarmMethod = "EMAIL" Then
                SeverityRAM.rdEmail_Minor.Checked = True
            End If

            SeverityRAM.txtValueOver_Major.Text = cfg.SeverityMajor.ValueOver
            If cfg.SeverityMajor.AlarmMethod = "SNMP" Then
                SeverityRAM.rdSnmp_Major.Checked = True
            ElseIf cfg.SeverityMajor.AlarmMethod = "SMS" Then
                SeverityRAM.rdSMS_Major.Checked = True
            ElseIf cfg.SeverityMajor.AlarmMethod = "EMAIL" Then
                SeverityRAM.rdEmail_Major.Checked = True
            End If

            SeverityRAM.txtValueOverCritical.Text = cfg.SeverityCritical.ValueOver
            If cfg.SeverityCritical.AlarmMethod = "SNMP" Then
                SeverityRAM.rdSnmp_Critical.Checked = True
            ElseIf cfg.SeverityCritical.AlarmMethod = "SMS" Then
                SeverityRAM.rdSMS_Critical.Checked = True
            ElseIf cfg.SeverityCritical.AlarmMethod = "EMAIL" Then
                SeverityRAM.rdEmail_Critical.Checked = True
            End If

            chkActive_RAM.Checked = (cfg.Active = "Y")
            txtIntervalMinite_RAM.Text = cfg.IntervalMinute
        End If
    End Sub

    Public Sub SetHDDData(ByVal XMLFileName As String)
        If InStr(XMLFileName, "HDD") > 0 Then
            TabControl1.SelectedTab = HDD
            Dim cfg As New Engine.Config.HDDConfigENG
            cfg.GetConfigFromXML(XMLFileName)

            txtShopName.Text = cfg.ShopName
            txtServerName.Text = cfg.ServerName
            txtIPAddress.Text = cfg.IPAddress
            txtAlarmType_HDD.Text = cfg.AlarmType
            txtRepeateCheck_HDD.Text = cfg.RepeateCheck

            GenHDDTab()
            For Each dr As DataRow In cfg.SeverityAlarmDT.Rows
                For Each t As TabPage In TabHDD.TabPages
                    If dr("DriveLetter").ToString = t.Text Then
                        For Each c As Control In t.Controls.Find("Severity_" & t.Text, False)
                            Dim s As SeverityControl = c
                            Dim SeverityMinor As Engine.Config.ConfigENG.AlarmSeverity = DirectCast(dr("MinorSeverity"), Engine.Config.ConfigENG.AlarmSeverity)

                            s.txtValueOver_Minor.Text = SeverityMinor.ValueOver
                            If SeverityMinor.AlarmMethod = "SNMP" Then
                                s.rdSnmp_Minor.Checked = True
                            ElseIf SeverityMinor.AlarmMethod = "SMS" Then
                                s.rdSMS_Minor.Checked = True
                            ElseIf SeverityMinor.AlarmMethod = "EMAIL" Then
                                s.rdEmail_Minor.Checked = True
                            End If

                            Dim SeverityMajor As Engine.Config.ConfigENG.AlarmSeverity = DirectCast(dr("MajorSeverity"), Engine.Config.ConfigENG.AlarmSeverity)
                            s.txtValueOver_Major.Text = SeverityMajor.ValueOver
                            If SeverityMajor.AlarmMethod = "SNMP" Then
                                s.rdSnmp_Major.Checked = True
                            ElseIf SeverityMajor.AlarmMethod = "SMS" Then
                                s.rdSMS_Major.Checked = True
                            ElseIf SeverityMajor.AlarmMethod = "EMAIL" Then
                                s.rdEmail_Major.Checked = True
                            End If

                            Dim SeverityCritical As Engine.Config.ConfigENG.AlarmSeverity = DirectCast(dr("CriticalSeverity"), Engine.Config.ConfigENG.AlarmSeverity)
                            s.txtValueOverCritical.Text = SeverityCritical.ValueOver
                            If SeverityCritical.AlarmMethod = "SNMP" Then
                                s.rdSnmp_Critical.Checked = True
                            ElseIf SeverityCritical.AlarmMethod = "SMS" Then
                                s.rdSMS_Critical.Checked = True
                            ElseIf SeverityCritical.AlarmMethod = "EMAIL" Then
                                s.rdEmail_Critical.Checked = True
                            End If
                        Next
                    End If
                Next
            Next

            chkActive_HDD.Checked = (cfg.Active = "Y")
            txtIntervalMinite_HDD.Text = cfg.IntervalMinute
        End If
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        If IsAddConfig = True Then
            If TabControl1.SelectedTab Is HDD Then
                GenHDDTab()
            End If
        End If
    End Sub

   
End Class
