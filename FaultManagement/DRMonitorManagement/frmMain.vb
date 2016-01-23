Imports System.Data
Imports Engine.Common
Imports System.Net
Imports System.Net.Sockets

Public Class frmMain
    Private Sub MonitorCPU()
        Dim eng As New InfoClass.CPUInfo
        Dim cpuDT As New DataTable  'CPU Usage All Shop From Monitor XML File
        cpuDT = eng.GetCPUMonitorFile()
        If cpuDT.Rows.Count > 0 Then
            For i As Integer = 0 To cpuDT.Rows.Count - 1
                Dim cpuDr As DataRow = cpuDT.Rows(i)
                Dim conf As New Engine.Config.CPUConfigENG
                conf.GetConfigFromXML(cpuDr("CPUConfigFileName"))

                If conf.Active = "Y" Then
                    Dim awDT As New DataTable
                    awDT = eng.GetAlarmWaitingClear(conf.ServerName, "CPU", "Alarm")
                    If Convert.ToDouble(cpuDr("CPUPercentUsage")) > conf.SeverityCritical.ValueOver Then
                        Dim Desc As String = "CPU on " & conf.ServerName & " usage over " & conf.SeverityCritical.ValueOver & " percent"
                        ProcessCPUAlarm(awDT, conf, "CRITICAL", eng, Convert.ToDouble(cpuDr("CPUPercentUsage")), Desc, conf.SeverityCritical)
                    ElseIf Convert.ToDouble(cpuDr("CPUPercentUsage")) > conf.SeverityMajor.ValueOver Then
                        Dim Desc As String = "CPU on " & conf.ServerName & " usage over " & conf.SeverityMajor.ValueOver & " percent"
                        ProcessCPUAlarm(awDT, conf, "MAJOR", eng, Convert.ToDouble(cpuDr("CPUPercentUsage")), Desc, conf.SeverityMajor)
                    ElseIf Convert.ToDouble(cpuDr("CPUPercentUsage")) > conf.SeverityMinor.ValueOver Then
                        Dim Desc As String = "CPU on " & conf.ServerName & " usage over " & conf.SeverityMinor.ValueOver & " percent"
                        ProcessCPUAlarm(awDT, conf, "MINOR", eng, Convert.ToDouble(cpuDr("CPUPercentUsage")), Desc, conf.SeverityMinor)
                    ElseIf Convert.ToDouble(cpuDr("CPUPercentUsage")) <= conf.SeverityMinor.ValueOver Then
                        If awDT.Rows.Count > 0 Then
                            Dim dr As DataRow = awDT.Rows(0)
                            Dim hw As New InfoClass.HardwareInfo
                            hw.SendClearAlarm(conf.ServerName, conf.ServerName & "_CPU", conf.IPAddress, conf.AlarmType, conf.ServerName & "_CPU_PROCESS", dr("Severity"), cpuDr("CPUPercentUsage"), "ALM_LOG_CPU_" & dr("Severity") & " is Clear", dr("AlarmMethod"), "CPU", "")
                            hw = Nothing
                        End If
                    End If
                    awDT.Dispose()
                End If
            Next
        End If
        cpuDT.Dispose()
        eng = Nothing
    End Sub

    Private Sub ProcessCPUAlarm(ByVal awDT As DataTable, ByVal conf As Engine.Config.CPUConfigENG, ByVal CheckSeverity As String, ByVal eng As InfoClass.CPUInfo, ByVal CPUPercentUsage As Double, ByVal AlarmDesc As String, ByVal ConfigSeverity As Engine.Config.ConfigENG.AlarmSeverity)
        awDT.DefaultView.RowFilter = "Severity<>'" & CheckSeverity & "'"
        If awDT.DefaultView.Count > 0 Then
            For Each awDR As DataRowView In awDT.DefaultView
                eng.SendClearAlarm(conf.ServerName, conf.ServerName & "_CPU", conf.IPAddress, conf.AlarmType, conf.ServerName & "_CPU_PROCESS", awDR("Severity"), CPUPercentUsage, "ALM_LOG_CPU_" & awDR("Severity") & " is Clear", awDR("AlarmMethod"), "CPU", "")
            Next
        End If

        Dim dt As New DataTable
        dt = eng.GetCPUPendingAlarm(conf.ServerName, CheckSeverity)
        If dt.Rows.Count < (Convert.ToInt16(conf.RepeateCheck) - 1) Then
            eng.CreateCPUPendingAlarm(conf, CheckSeverity, CPUPercentUsage, AlarmDesc, "1", ConfigSeverity.AlarmMethod)
        Else
            Dim SpecificProblem As String = conf.ServerName & " CPU Usage is " & CPUPercentUsage & "% over than " & ConfigSeverity.ValueOver & "% " & ConfigSeverity.Severity
            awDT.DefaultView.RowFilter = "Severity='" & CheckSeverity & "'"
            If awDT.DefaultView.Count = 0 Then
                If eng.SendAlarm(conf.ServerName, conf.ServerName & "_CPU", conf.IPAddress, conf.AlarmType, conf.ServerName & "_CPU_PROCESS", CheckSeverity, CPUPercentUsage, AlarmDesc, ConfigSeverity.AlarmMethod, "CPU") = True Then
                    If eng.CreateAlarmWaitingClear(conf.ServerName, conf.IPAddress, CheckSeverity, CPUPercentUsage, ConfigSeverity.AlarmMethod, "CPU", AlarmDesc, SpecificProblem, ConfigSeverity.AlarmCode, conf.ServerName & "_CPU") > 0 Then
                        eng.DeleteCPUPendingAlarm(conf.ServerName)
                    End If
                End If
            Else
                If eng.UpdateAlarmWaitingClear(conf.ServerName, conf.IPAddress, CheckSeverity, CPUPercentUsage, ConfigSeverity.AlarmMethod, "CPU", AlarmDesc, SpecificProblem, awDT.DefaultView(0)("id")) = True Then
                    eng.DeleteCPUPendingAlarm(conf.ServerName)
                End If
            End If
            awDT.DefaultView.RowFilter = ""
        End If
        dt.Dispose()
    End Sub

    Private Sub MonitorRam()
        Dim eng As New InfoClass.RAMInfo
        Dim RAMDT As New DataTable  'RAM Usage All Shop From Monitor XML File
        RAMDT = eng.GetRAMMonitorFile()
        If RAMDT.Rows.Count > 0 Then
            For i As Integer = 0 To RAMDT.Rows.Count - 1
                Dim RAMDr As DataRow = RAMDT.Rows(i)
                Dim conf As New Engine.Config.RamConfigENG
                conf.GetConfigFromXML(RAMDr("RAMConfigFileName"))

                If conf.Active = "Y" Then
                    Dim awDT As New DataTable
                    awDT = eng.GetAlarmWaitingClear(conf.ServerName, "RAM", "Alarm")
                    If Convert.ToDouble(RAMDr("RAMPercentUsage")) > conf.SeverityCritical.ValueOver Then
                        Dim Desc As String = "RAM on " & conf.ServerName & " usage over " & conf.SeverityCritical.ValueOver & " percent"
                        ProcessRAMAlarm(awDT, conf, "CRITICAL", eng, Convert.ToDouble(RAMDr("RAMPercentUsage")), Desc, conf.SeverityCritical)
                    ElseIf Convert.ToDouble(RAMDr("RAMPercentUsage")) > conf.SeverityMajor.ValueOver Then
                        Dim Desc As String = "RAM on " & conf.ServerName & " usage over " & conf.SeverityMajor.ValueOver & " percent"
                        ProcessRAMAlarm(awDT, conf, "MAJOR", eng, Convert.ToDouble(RAMDr("RAMPercentUsage")), Desc, conf.SeverityMajor)
                    ElseIf Convert.ToDouble(RAMDr("RAMPercentUsage")) > conf.SeverityMinor.ValueOver Then
                        Dim Desc As String = "RAM on " & conf.ServerName & " usage over " & conf.SeverityMinor.ValueOver & " percent"
                        ProcessRAMAlarm(awDT, conf, "MINOR", eng, Convert.ToDouble(RAMDr("RAMPercentUsage")), Desc, conf.SeverityMinor)
                    ElseIf Convert.ToDouble(RAMDr("RAMPercentUsage")) <= conf.SeverityMinor.ValueOver Then
                        If awDT.Rows.Count > 0 Then
                            Dim dr As DataRow = awDT.Rows(0)
                            Dim hw As New InfoClass.HardwareInfo
                            hw.SendClearAlarm(conf.ServerName, conf.ServerName & "_RAM", conf.IPAddress, conf.AlarmType, conf.ServerName & "_RAM_PROCESS", dr("Severity"), RAMDr("RAMPercentUsage"), "ALM_LOG_RAM_" & dr("Severity") & " is Clear", dr("AlarmMethod"), "RAM", "")
                            hw = Nothing
                        End If
                    End If
                    awDT.Dispose()
                End If
            Next
        End If
        RAMDT.Dispose()
        eng = Nothing
    End Sub

    Private Sub ProcessRAMAlarm(ByVal awDT As DataTable, ByVal conf As Engine.Config.RamConfigENG, ByVal CheckSeverity As String, ByVal eng As InfoClass.RAMInfo, ByVal RAMPercentUsage As Double, ByVal AlarmDesc As String, ByVal ConfigSeverity As Engine.Config.ConfigENG.AlarmSeverity)
        awDT.DefaultView.RowFilter = "Severity<>'" & CheckSeverity & "'"
        If awDT.DefaultView.Count > 0 Then
            For Each awDR As DataRowView In awDT.DefaultView
                eng.SendClearAlarm(conf.ServerName, conf.ServerName & "_RAM", conf.IPAddress, conf.AlarmType, conf.ServerName & "_RAM_PROCESS", awDR("Severity"), RAMPercentUsage, "ALM_LOG_RAM_" & awDR("Severity") & " is Clear", awDR("AlarmMethod"), "RAM", "")
            Next
        End If

        Dim dt As New DataTable
        dt = eng.GetRAMPendingAlarm(conf.ServerName, CheckSeverity)
        If dt.Rows.Count < (Convert.ToInt16(conf.RepeateCheck) - 1) Then
            eng.CreateRAMPendingAlarm(conf, CheckSeverity, RAMPercentUsage, AlarmDesc, "1", ConfigSeverity.AlarmMethod)
        Else
            Dim SpecificProblem As String = conf.ServerName & " RAM Usage is " & RAMPercentUsage & "% over than " & ConfigSeverity.ValueOver & "% " & ConfigSeverity.Severity
            awDT.DefaultView.RowFilter = "Severity='" & CheckSeverity & "'"
            If awDT.DefaultView.Count = 0 Then
                If eng.CreateAlarmWaitingClear(conf.ServerName, conf.IPAddress, CheckSeverity, RAMPercentUsage, ConfigSeverity.AlarmMethod, "RAM", AlarmDesc, SpecificProblem, ConfigSeverity.AlarmCode, conf.ServerName & "_RAM") > 0 Then
                    eng.DeleteRAMPendingAlarm(conf.ServerName)
                End If
                eng.SendAlarm(conf.ServerName, conf.ServerName & "_RAM", conf.IPAddress, conf.AlarmType, conf.ServerName & "_RAM_PROCESS", CheckSeverity, RAMPercentUsage, AlarmDesc, ConfigSeverity.AlarmMethod, "RAM")
            Else
                If eng.UpdateAlarmWaitingClear(conf.ServerName, conf.IPAddress, CheckSeverity, RAMPercentUsage, ConfigSeverity.AlarmMethod, "RAM", AlarmDesc, SpecificProblem, awDT.DefaultView(0)("id")) = True Then
                    eng.DeleteRAMPendingAlarm(conf.ServerName)
                End If
            End If
            awDT.DefaultView.RowFilter = ""
        End If
        dt.Dispose()
    End Sub

    Private Sub MonitorHDD()
        Dim eng As New InfoClass.HDDInfo
        Dim HddDT As New DataTable  'HDD Usage All Shop From Monitor XML File
        HddDT = eng.GetHDDMonitorFile()
        If HddDT.Rows.Count > 0 Then
            For i As Integer = 0 To HddDT.Rows.Count - 1
                Dim HDDDr As DataRow = HddDT.Rows(i)
                Dim DriveLetter As String = HDDDr("DriveLetter")
                Dim ServerName As String = HDDDr("ServerName")
                Dim conf As New Engine.Config.HDDConfigENG
                conf.GetConfigFromXML(HDDDr("HDDConfigFileName"))

                If conf.Active = "Y" Then
                    For Each Drive As DataRow In conf.SeverityAlarmDT.Rows
                        If Drive("DriveLetter") = DriveLetter And conf.ServerName = ServerName Then
                            Dim awDT As New DataTable
                            awDT = eng.GetAlarmWaitingClear(conf.ServerName, "HardDisk_" & DriveLetter, "Alarm")

                            Dim SeverityCritical As Engine.Config.ConfigENG.AlarmSeverity = DirectCast(Drive("CriticalSeverity"), Engine.Config.ConfigENG.AlarmSeverity)
                            Dim SeverityMajor As Engine.Config.ConfigENG.AlarmSeverity = DirectCast(Drive("MajorSeverity"), Engine.Config.ConfigENG.AlarmSeverity)
                            Dim SeverityMinor As Engine.Config.ConfigENG.AlarmSeverity = DirectCast(Drive("MinorSeverity"), Engine.Config.ConfigENG.AlarmSeverity)

                            If Convert.ToDouble(HDDDr("HDDPercentUsage")) > SeverityCritical.ValueOver Then
                                Dim Desc As String = "Hard Disk Drive " & DriveLetter & " on " & conf.ServerName & " usage over " & SeverityCritical.ValueOver & " percent"
                                ProcessHDDAlarm(awDT, conf, "CRITICAL", eng, Convert.ToDouble(HDDDr("HDDPercentUsage")), Desc, SeverityCritical, DriveLetter)
                            ElseIf Convert.ToDouble(HDDDr("HDDPercentUsage")) > SeverityMajor.ValueOver Then
                                Dim Desc As String = "Hard Disk  Drive " & DriveLetter & " on " & conf.ServerName & " usage over " & SeverityMajor.ValueOver & " percent"
                                ProcessHDDAlarm(awDT, conf, "MAJOR", eng, Convert.ToDouble(HDDDr("HDDPercentUsage")), Desc, SeverityMajor, DriveLetter)
                            ElseIf Convert.ToDouble(HDDDr("HDDPercentUsage")) > SeverityMinor.ValueOver Then
                                Dim Desc As String = "Hard Disk  Drive " & DriveLetter & " on " & conf.ServerName & " usage over " & SeverityMinor.ValueOver & " percent"
                                ProcessHDDAlarm(awDT, conf, "MINOR", eng, Convert.ToDouble(HDDDr("HDDPercentUsage")), Desc, SeverityMinor, DriveLetter)
                            ElseIf Convert.ToDouble(HDDDr("HDDPercentUsage")) <= SeverityMinor.ValueOver Then
                                If awDT.Rows.Count > 0 Then
                                    Dim dr As DataRow = awDT.Rows(0)
                                    Dim hw As New InfoClass.HardwareInfo
                                    hw.SendClearAlarm(conf.ServerName, conf.ServerName & "_HardDisk_" & DriveLetter, conf.IPAddress, conf.AlarmType, conf.ServerName & "_HardDisk_PROCESS", dr("Severity"), HDDDr("HDDPercentUsage"), "ALM_LOG_HardDisk_" & dr("Severity") & " is Clear", dr("AlarmMethod"), "HardDisk_" & DriveLetter, "")
                                    hw = Nothing
                                End If
                            End If
                            awDT.Dispose()
                        End If
                    Next
                End If
            Next
        End If
        HddDT.Dispose()
    End Sub

    Private Sub ProcessHDDAlarm(ByVal awDT As DataTable, ByVal conf As Engine.Config.HDDConfigENG, ByVal CheckSeverity As String, ByVal eng As InfoClass.HDDInfo, ByVal HDDPercentUsage As Double, ByVal AlarmDesc As String, ByVal ConfigSeverity As Engine.Config.ConfigENG.AlarmSeverity, ByVal DriveLetter As String)
        awDT.DefaultView.RowFilter = "Severity<>'" & CheckSeverity & "'"
        If awDT.DefaultView.Count > 0 Then
            For Each awDR As DataRowView In awDT.DefaultView
                eng.SendClearAlarm(conf.ServerName, conf.ServerName & "_HardDisk_" & DriveLetter, conf.IPAddress, conf.AlarmType, conf.ServerName & "_HardDisk_" & DriveLetter, awDR("Severity"), HDDPercentUsage, "ALM_LOG_HardDisk_" & awDR("Severity") & " is Clear", awDR("AlarmMethod"), "HardDisk_" & DriveLetter, "")
            Next
        End If

        Dim dt As New DataTable
        dt = eng.GetHDDPendingAlarm(conf.ServerName, CheckSeverity, DriveLetter)
        If dt.Rows.Count < (Convert.ToInt16(conf.RepeateCheck) - 1) Then
            eng.CreateHDDPendingAlarm(conf, CheckSeverity, HDDPercentUsage, AlarmDesc, "1", ConfigSeverity.AlarmMethod, DriveLetter)
        Else
            Dim SpecificProblem As String = conf.ServerName & "_HardDisk_" & DriveLetter & " Usage is " & HDDPercentUsage & "% over than " & ConfigSeverity.ValueOver & "% " & ConfigSeverity.Severity
            awDT.DefaultView.RowFilter = "Severity='" & CheckSeverity & "'"

            If awDT.DefaultView.Count = 0 Then
                If eng.CreateAlarmWaitingClear(conf.ServerName, conf.IPAddress, CheckSeverity, HDDPercentUsage, ConfigSeverity.AlarmMethod, "HardDisk_" & DriveLetter, AlarmDesc, SpecificProblem, ConfigSeverity.AlarmCode, conf.ServerName & "_HardDisk_" & DriveLetter) > 0 Then
                    eng.DeleteHDDPendingAlarm(conf.ServerName, DriveLetter)
                End If
                eng.SendAlarm(conf.ServerName, conf.ServerName & "_HardDisk_" & DriveLetter, conf.IPAddress, conf.AlarmType, conf.ServerName & "_HardDisk_" & DriveLetter, CheckSeverity, HDDPercentUsage, AlarmDesc, ConfigSeverity.AlarmMethod, "HardDisk_" & DriveLetter)
            Else
                If eng.UpdateAlarmWaitingClear(conf.ServerName, conf.IPAddress, CheckSeverity, HDDPercentUsage, ConfigSeverity.AlarmMethod, "HardDisk_" & DriveLetter, AlarmDesc, SpecificProblem, awDT.DefaultView(0)("id")) = True Then
                    eng.DeleteHDDPendingAlarm(conf.ServerName, DriveLetter)
                End If
            End If
            awDT.DefaultView.RowFilter = ""
        End If
        dt.Dispose()
    End Sub

    Private Sub MonitorPort()
        Dim eng As New InfoClass.HardwareInfo
        Dim cfg As New Engine.Config.PortConfigENG
        Dim dt As New DataTable
        dt = cfg.GetConfigPortList("")
        If dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                If Convert.ToBoolean(dr("CheckAlarmWithTimeConfig")) = True Then
                    Dim ServerName As String = dr("HostName")
                    Dim IPAddress As String = dr("HostIP")
                    Dim PortNumber As String = dr("PortNumber")
                    Dim awDT As New DataTable
                    awDT = eng.GetAlarmWaitingClear(ServerName, "PORT_" & PortNumber, "Alarm")
                    If TelnetPort(IPAddress, PortNumber) = False Then
                        Dim SpecificProblem As String = "PORT" & PortNumber & " process on " & ServerName & " not connect"
                        awDT.DefaultView.RowFilter = "Severity='CRITICAL'"

                        If awDT.DefaultView.Count = 0 Then
                            If eng.SendAlarm(ServerName, ServerName & "_PORT_" & PortNumber, IPAddress, "Fault", "ALM_PORT" & PortNumber & "_Process_Down", "CRITICAL", 0, SpecificProblem, "SNMP", "PORT_" & PortNumber) = True Then
                                eng.CreateAlarmWaitingClear(ServerName, IPAddress, "CRITICAL", 0, "SNMP", "PORT_" & PortNumber, SpecificProblem, SpecificProblem, "ALM_PORT" & PortNumber & "_Process_Down", ServerName & "_PORT_" & PortNumber)
                            End If
                        Else
                            eng.UpdateAlarmWaitingClear(ServerName, IPAddress, "CRITICAL", 0, "SNMP", "PORT_" & PortNumber, SpecificProblem, SpecificProblem, awDT.DefaultView(0)("id"))
                        End If
                        awDT.DefaultView.RowFilter = ""
                    Else
                        awDT.DefaultView.RowFilter = "Severity='CRITICAL'"
                        If awDT.DefaultView.Count > 0 Then
                            eng.SendClearAlarm(ServerName, ServerName & "_PORT_" & PortNumber, IPAddress, "Fault", ServerName & "_PORT_" & PortNumber, "CRITICAL", 0, "ALM_PORT_" & PortNumber & " is Clear", "SNMP", "PORT_" & PortNumber, "")
                        End If
                        awDT.DefaultView.RowFilter = ""
                    End If
                    awDT.Dispose()
                End If
            Next
        End If
        dt.Dispose()
        cfg = Nothing
        eng = Nothing
    End Sub

    Private Function TelnetPort(ByVal PIPAddress As String, ByVal PPort As String) As Boolean
        Dim ret As Boolean = False
        Dim remoteIPAddress As IPAddress
        Dim ep As IPEndPoint
        Dim tnSocket As Socket

        ' Get the IP Address and the Port and create an IPEndpoint (ep)
        remoteIPAddress = IPAddress.Parse(PIPAddress.Trim)
        ep = New IPEndPoint(remoteIPAddress, CType(PPort.Trim, Integer))
        ' Set the socket up (type etc)
        tnSocket = New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
        Try
            ' Connect
            tnSocket.Connect(ep)
            If tnSocket.Connected = True Then
                ret = True
            End If
        Catch oEX As SocketException
            ' error
            ' You will need to do error cleanup here e.g killing the socket
            ' and exiting the procedure.
            Return False
        End Try

        ' Cleanup
        remoteIPAddress = Nothing
        ep = Nothing
        tnSocket = Nothing

        Return ret
    End Function

    Private Sub MonitorIamAlive()
        Dim vDateNow As DateTime = DateTime.Now
        Dim dt As New DataTable
        Dim eng As New Engine.Config.ConfigENG
        dt = eng.GetIamAliveList()
        If dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                Dim hng As New InfoClass.HardwareInfo
                Dim hDt As DataTable = hng.GetAlarmWaitingClear(dr("ServerName"), "I_AM_ALIVE_" & dr("ServerName"), "Alarm")

                Dim NextAliveTime As DateTime = Convert.ToDateTime(dr("next_alive_time"))
                If vDateNow > NextAliveTime Then
                    'ถ้าเวลาปัจจุบันมากกว่า Next Alive Time แสดงว่า FaultManagement ไม่ทำงาน ให้ทำการส่ง Alarm
                    If hDt.Rows.Count = 0 Then
                        'Send Alarm
                        hng.CreateAlarmWaitingClear(dr("ServerName"), dr("HostIP"), "CRITICAL", 0, "SNMP", "I_AM_ALIVE_" & dr("ServerName"), "Fault Management Process on " & dr("ServerName") & " is not Alive", "Fault Management Process on " & dr("ServerName") & " is not Alive", "I_AM_ALIVE_ALARM", "I_AM_ALIVE_" & dr("ServerName"))
                        hng.SendAlarm(dr("ServerName"), "I_AM_ALIVE_" & dr("ServerName"), dr("HostIP"), "Fault", dr("ServerName") & "_FaultManagementNotAlive", "CRITICAL", 0, "Fault Management Process on " & dr("ServerName") & " is not Alive", "SNMP", "I_AM_ALIVE_" & dr("ServerName"))
                    Else
                        hng.UpdateAlarmWaitingClear(dr("ServerName"), dr("HostIP"), "CRITICAL", 0, "SNMP", "I_AM_ALIVE_" & dr("ServerName"), "Fault Management Process on " & dr("ServerName") & " is not Alive", "Fault Management Process on " & dr("ServerName") & " is not Alive", hDt.Rows(0)("id"))
                    End If
                Else
                    If hDt.Rows.Count > 0 Then
                        'Clear Alarm
                        hng.SendClearAlarm(dr("ServerName"), "I_AM_ALIVE_" & dr("ServerName"), dr("HostIP"), "Fault", dr("ServerName") & "_FaultManagementNotAlive", "CRITICAL", 0, "Fault Management Process on " & dr("ServerName") & " is not Alive", "SNMP", "I_AM_ALIVE_" & dr("ServerName"), "Fault Management Process on " & dr("ServerName") & " is not Alive")
                    End If
                End If
                hng = Nothing
                hDt.Dispose()
            Next
        End If
        dt.Dispose()
        eng = Nothing
    End Sub

    Private Sub tmListConfig_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmListConfig.Tick
        tmListConfig.Enabled = False
        SetListConfig()
        tmListConfig.Enabled = True
    End Sub

    Private Sub SetListConfig()
        Dim cfgDT As New DataTable
        Dim ini As New DRMonitorManagement.Org.Mentalis.Files.IniReader(Application.StartupPath & "\Config.ini")
        ini.Section = "Setting"

        If IO.Directory.Exists(ini.ReadString("FMConfigFolder")) = False Then
            IO.Directory.CreateDirectory(ini.ReadString("FMConfigFolder"))
        End If

        For Each f As String In IO.Directory.GetFiles(ini.ReadString("FMConfigFolder"), "*.xml")
            Dim fInfo As New IO.FileInfo(f)
            If InStr(fInfo.Name, "CPU") > 0 Then
                Dim cCPU As New Engine.Config.CPUConfigENG
                cCPU.GetConfigFromXML(f)
                Dim cDt As New DataTable
                cDt = cCPU.ConfigList
                If cDt.Rows.Count > 0 Then
                    cfgDT.Merge(cDt)
                End If
                cDt.Dispose()
                cCPU = Nothing
            End If
            If InStr(fInfo.Name, "RAM") > 0 Then
                Dim cRAM As New Engine.Config.RamConfigENG
                cRAM.GetConfigFromXML(f)
                Dim rDt As New DataTable
                rDt = cRAM.ConfigList
                If rDt.Rows.Count > 0 Then
                    cfgDT.Merge(rDt)
                End If
                rDt.Dispose()
                cRAM = Nothing
            End If
            If InStr(fInfo.Name, "HDD") > 0 Then
                Dim dHDD As New Engine.Config.HDDConfigENG
                dHDD.GetConfigFromXML(f)
                Dim dDt As New DataTable
                dDt = dHDD.ConfigList
                If dDt.Rows.Count > 0 Then
                    cfgDT.Merge(dDt)
                End If
                dDt.Dispose()
                dHDD = Nothing
            End If

            If InStr(fInfo.Name, "SERVICE") > 0 Then
                Dim SevDT As New Engine.Config.ServiceConfigENG
                SevDT.GetConfigFromXML(f)
                Dim dDt As New DataTable
                dDt = SevDT.ConfigList
                If dDt.Rows.Count > 0 Then
                    cfgDT.Merge(dDt)
                End If
                dDt.Dispose()
                SevDT = Nothing
            End If
            If InStr(fInfo.Name, "CONFIG_PROCESS") > 0 Then
                Dim pEng As New Engine.Config.ProcessConfigENG
                pEng.GetConfigFromXML(f)
                Dim dDt As New DataTable
                dDt = pEng.ConfigList
                If dDt.Rows.Count > 0 Then
                    cfgDT.Merge(dDt)
                End If
                dDt.Dispose()
                pEng = Nothing
            End If
            If InStr(fInfo.Name, "CONFIG_WEB") > 0 Then
                Dim wEng As New Engine.Config.WebConfigENG
                wEng.GetConfigFromXML(f)
                Dim dDt As New DataTable
                dDt = wEng.ConfigList
                If dDt.Rows.Count > 0 Then
                    cfgDT.Merge(dDt)
                End If
                dDt.Dispose()
                wEng = Nothing
            End If

            If InStr(fInfo.Name, "DATAFILESIZE") > 0 Then
                Dim fsEng As New Engine.Config.FileConfigENG
                fsEng.GetFileSizeConfigFromXML(f)
                Dim fsDt As New DataTable
                fsDt = fsEng.ConfigList
                If fsDt.Rows.Count > 0 Then
                    cfgDT.Merge(fsDt)
                End If
                fsDt.Dispose()
                fsEng = Nothing
            End If

            If InStr(fInfo.Name, "DATAFILELOST") > 0 Then
                'Dim FileLostConfigFile As String = Application.StartupPath & "\Config\" & Environment.MachineName & "_CONFIG_DATAFILELOST.xml"
                Dim flEng As New Engine.Config.FileConfigENG
                flEng.GetFileLostConfigFromXML(f)
                Dim flDt As New DataTable
                flDt = flEng.ConfigList
                If flDt.Rows.Count > 0 Then
                    cfgDT.Merge(flDt)
                End If
                flDt.Dispose()
                flEng = Nothing
            End If


            fInfo = Nothing
        Next

        If cfgDT.Rows.Count > 0 Then
            DGVConfigSetting.DataSource = cfgDT
        End If
    End Sub

    Private Sub frmMain_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        If Me.WindowState = FormWindowState.Minimized Then
            Me.Hide()
            NotifyIcon1.Visible = True
            NotifyIcon1.Text = "DR Monitor Management V" & getMyVersion()
        Else
            NotifyIcon1.Visible = False
        End If
    End Sub
    Private Function getMyVersion() As String
        Dim version As System.Version = System.Reflection.Assembly.GetExecutingAssembly.GetName().Version
        Return version.Major & "." & version.Minor & "." & version.Build & "." & version.Revision
    End Function

    Private Sub frmMain_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        SetListConfig()
        SetListProcessAlarm()
        Me.Text = "DR Monitor Management V" & getMyVersion()
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub SetListProcessAlarm()
        Dim dt As New DataTable
        Dim eng As New Engine.ConnectDB.AlarmDataENG
        dt = eng.GetAlarmData("", "Alarm", New Date(1, 1, 1), New Date(1, 1, 1))
        If dt.Rows.Count > 0 Then
            DGVLog.AutoGenerateColumns = False
            DGVLog.DataSource = dt

            For Each dgv As DataGridViewRow In DGVLog.Rows
                Dim dgStyle As New DataGridViewCellStyle
                If dgv.Cells("Severity").Value = "CRITICAL" Then
                    dgStyle.BackColor = Color.Red
                ElseIf dgv.Cells("Severity").Value = "MAJOR" Then
                    dgStyle.BackColor = Color.Orange
                ElseIf dgv.Cells("Severity").Value = "MINOR" Then
                    dgStyle.BackColor = Color.Yellow
                End If
                dgv.DefaultCellStyle = dgStyle
            Next
        Else
            DGVLog.DataSource = Nothing
        End If
        eng = Nothing
    End Sub

    Private Sub tmListProcessAlarm_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmListProcessAlarm.Tick
        tmListProcessAlarm.Enabled = False
        SetListProcessAlarm()
        Me.Text = "DR Monitor Management V" & getMyVersion() & "  Last Refresh Time :" & DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss", New Globalization.CultureInfo("en-US"))
        tmListProcessAlarm.Enabled = True
    End Sub

    Private Sub NotifyIcon1_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles NotifyIcon1.MouseDoubleClick
        Me.Show()
        Me.WindowState = FormWindowState.Normal
        NotifyIcon1.Visible = False
    End Sub

    Private Sub tmMonitor_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmMonitor.Tick
        tmMonitor.Enabled = False
        MonitorCPU()
        MonitorRam()
        MonitorHDD()
        MonitorPort()
        MonitorIamAlive()


        'CreateLogFile()
        tmMonitor.Enabled = True
    End Sub

    Public Shared Sub CreateLogFile()
        Dim ini As New DRMonitorManagement.Org.Mentalis.Files.IniReader(Application.StartupPath & "\Config.ini")
        ini.Section = "Setting"
        Dim FMMonitorFolder As String = ini.ReadString("FMMonitorFolder")
        If IO.Directory.Exists(FMMonitorFolder) = False Then
            IO.Directory.CreateDirectory(FMMonitorFolder)
        End If

        'Dim FILE_NAME As String = System.Windows.Forms.Application.StartupPath & "\" & DateTime.Now.ToString("yyyyyMMddHH") & ".sql"
        Dim FILE_NAME As String = FMMonitorFolder & "LastMonitorTime_" & DateTime.Now.ToString("yyyyMMddHH") & ".txt"
        Dim objWriter As New System.IO.StreamWriter(FILE_NAME, True)
        objWriter.WriteLine(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff") & Chr(13))
        objWriter.Close()

        ini = Nothing
    End Sub
End Class