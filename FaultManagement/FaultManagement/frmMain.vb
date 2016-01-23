Imports System.Data
Imports Engine.Common
Imports System.IO
Public Class frmMain

#Region "Monitor Zone"
    Dim CPUInterval As Integer = 0
    Dim CPULastMonitorTime As DateTime = DateTime.Now
    Private Function CPUMonitor() As DataTable
        Dim ret As New DataTable
        If DateAdd(DateInterval.Minute, CPUInterval, CPULastMonitorTime) < DateTime.Now Then

            Dim CPUInfoFile As String = Application.StartupPath & "\MonitorProcess\" & Environment.MachineName & "_CPU_PROCESS.xml"
            If Directory.Exists(Application.StartupPath & "\MonitorProcess\") = False Then
                Directory.CreateDirectory(Application.StartupPath & "\MonitorProcess\")
            End If

            Dim conf As New Engine.Config.CPUConfigENG
            conf.GetConfigFromXML(Application.StartupPath & "\Config\" & Environment.MachineName & "_CONFIG_CPU_PROCESS.xml")
            If conf.Active = "Y" Then
                If IO.File.Exists(CPUInfoFile) = True Then
                    Dim BackupPath As String = Application.StartupPath & "\MonitorProcess\BackupCPUProcess\" & Today.Year & "\" & Today.Month.ToString.PadLeft(2, "0") & "\" & Today.Day.ToString.PadLeft(2, "0")
                    If IO.Directory.Exists(BackupPath) = False Then
                        IO.Directory.CreateDirectory(BackupPath)
                    End If
                    IO.File.Copy(CPUInfoFile, BackupPath & "\" & Environment.MachineName & "_CPU_PROCESS_" & DateTime.Now.ToString("yyyyMMdd_HHmmss") & ".xml")
                End If

                Dim inf As New Engine.Info.CPUInfoENG
                Dim CPUInfo As XElement = inf.GetCPUInfoXML()
                CPUInfo.Save(CPUInfoFile)
                inf = Nothing

                Try
                    Dim ini As New Org.Mentalis.Files.IniReader(Application.StartupPath & "\Config.ini")
                    ini.Section = "Setting"
                    Dim ws As New FaultManagementService.FaultManagementService
                    ws.Timeout = 2000
                    ws.Url = ini.ReadString("DCWebserviceURL")
                    ws.SendMonitorFileToDC(IO.File.ReadAllBytes(CPUInfoFile), New IO.FileInfo(CPUInfoFile).Name, Environment.MachineName)
                    ws = Nothing
                    ini = Nothing
                Catch ex As Exception

                End Try

                ret = conf.ConfigList

                CPUInterval = conf.IntervalMinute
                CPULastMonitorTime = DateTime.Now
            End If
            conf = Nothing
        End If

        Return ret
    End Function

    Dim RamInterval As Integer = 0
    Dim RamLastMonitorTime As DateTime = DateTime.Now
    Private Function RamMonitor() As DataTable
        Dim ret As New DataTable
        If DateAdd(DateInterval.Minute, RamInterval, RamLastMonitorTime) < DateTime.Now Then
            Dim RAMInfoFile As String = Application.StartupPath & "\MonitorProcess\" & Environment.MachineName & "_RAM_PROCESS.xml"
            If Directory.Exists(Application.StartupPath & "\MonitorProcess\") = False Then
                Directory.CreateDirectory(Application.StartupPath & "\MonitorProcess\")
            End If

            Dim conf As New Engine.Config.RamConfigENG
            conf.GetConfigFromXML(Application.StartupPath & "\Config\" & Environment.MachineName & "_CONFIG_RAM_PROCESS.xml")
            If conf.Active = "Y" Then
                If IO.File.Exists(RAMInfoFile) = True Then
                    Dim BackupPath As String = Application.StartupPath & "\MonitorProcess\BackupRAMProcess\" & Today.Year & "\" & Today.Month.ToString.PadLeft(2, "0") & "\" & Today.Day.ToString.PadLeft(2, "0")
                    If IO.Directory.Exists(BackupPath) = False Then
                        IO.Directory.CreateDirectory(BackupPath)
                    End If
                    IO.File.Copy(RAMInfoFile, BackupPath & "\" & Environment.MachineName & "_RAM_PROCESS_" & DateTime.Now.ToString("yyyyMMdd_HHmmss") & ".xml")
                End If

                Dim inf As New Engine.Info.RamInfoENG
                Dim RAMInfo As XElement = inf.GetRAMInfoXML
                RAMInfo.Save(RAMInfoFile)
                inf = Nothing

                Try
                    Dim ini As New Org.Mentalis.Files.IniReader(Application.StartupPath & "\Config.ini")
                    ini.Section = "Setting"
                    Dim ws As New FaultManagementService.FaultManagementService
                    ws.Url = ini.ReadString("DCWebserviceURL")
                    ws.Timeout = 2000
                    ws.SendMonitorFileToDC(IO.File.ReadAllBytes(RAMInfoFile), New IO.FileInfo(RAMInfoFile).Name, Environment.MachineName)
                    ws = Nothing
                    ini = Nothing
                Catch ex As Exception

                End Try

                ret = conf.ConfigList
                RamInterval = conf.IntervalMinute
                RamLastMonitorTime = DateTime.Now
            End If
            conf = Nothing
        End If
        Return ret
    End Function

    Dim HDDInterval As Integer = 0
    Dim HDDLastMonitorTime As DateTime = DateTime.Now
    Private Function HDDMonitor() As DataTable
        Dim ret As New DataTable
        If DateAdd(DateInterval.Minute, HDDInterval, HDDLastMonitorTime) < DateTime.Now Then
            Dim HDDInfoFile As String = Application.StartupPath & "\MonitorProcess\" & Environment.MachineName & "_HDD_PROCESS.xml"
            If Directory.Exists(Application.StartupPath & "\MonitorProcess\") = False Then
                Directory.CreateDirectory(Application.StartupPath & "\MonitorProcess\")
            End If
            Dim conf As New Engine.Config.HDDConfigENG
            conf.GetConfigFromXML(Application.StartupPath & "\Config\" & Environment.MachineName & "_CONFIG_HDD_PROCESS.xml")
            If conf.Active = "Y" Then
                If IO.File.Exists(HDDInfoFile) = True Then
                    Dim BackupPath As String = Application.StartupPath & "\MonitorProcess\BackupHDDProcess\" & Today.Year & "\" & Today.Month.ToString.PadLeft(2, "0") & "\" & Today.Day.ToString.PadLeft(2, "0")
                    If IO.Directory.Exists(BackupPath) = False Then
                        IO.Directory.CreateDirectory(BackupPath)
                    End If
                    IO.File.Copy(HDDInfoFile, BackupPath & "\" & Environment.MachineName & "_HDD_PROCESS_" & DateTime.Now.ToString("yyyyMMdd_HHmmss") & ".xml")
                End If

                Dim fInfo As New FileInfo(HDDInfoFile)
                If Directory.Exists(fInfo.DirectoryName) = False Then
                    Directory.CreateDirectory(fInfo.DirectoryName)
                End If
                fInfo = Nothing

                Dim inf As New Engine.Info.DriveInfoENG
                Dim HDDInfo As XElement = inf.GetDriveInfoXML
                HDDInfo.Save(HDDInfoFile)
                inf = Nothing

                Try
                    Dim ini As New Org.Mentalis.Files.IniReader(Application.StartupPath & "\Config.ini")
                    ini.Section = "Setting"
                    Dim ws As New FaultManagementService.FaultManagementService
                    ws.Timeout = 2000
                    ws.Url = ini.ReadString("DCWebserviceURL")
                    ws.SendMonitorFileToDC(IO.File.ReadAllBytes(HDDInfoFile), New IO.FileInfo(HDDInfoFile).Name, Environment.MachineName)
                    ws = Nothing
                    ini = Nothing
                Catch ex As Exception

                End Try
                ret = conf.ConfigList
                HDDInterval = conf.IntervalMinute
                HDDLastMonitorTime = DateTime.Now
            End If
            conf = Nothing
        End If
        Return ret
    End Function

    Dim ServiceInterval As Integer = 0
    Dim ServiceLastMonitorTime As DateTime = DateTime.Now
    Private Sub ServiceMonitor()
        If DateAdd(DateInterval.Minute, ServiceInterval, ServiceLastMonitorTime) < DateTime.Now Then
            Dim DeBug As String = "Start Service Monitor" & vbCrLf
            Dim eng As New InfoClass.ServiceInfo
            Dim ServiceConfigFile As String = Application.StartupPath & "\Config\" & Environment.MachineName & "_CONFIG_SERVICE_PROCESS.xml"
            If File.Exists(ServiceConfigFile) = True Then
                DeBug += "ServiceConfigFile : " & ServiceConfigFile & vbCrLf

                Dim conf As New Engine.Config.ServiceConfigENG
                conf.GetConfigFromXML(ServiceConfigFile)
                If conf.CheckAlarmWithTimeConfig(conf.AlamDateList, conf.AllDayEvent, conf.AlamTimeFrom, conf.AlamTimeTo) = True Then

                    DeBug += "CheckAlarmWithTimeConfig : " & vbCrLf
                    Dim dt As New DataTable
                    dt = conf.ConfigServiceList
                    If dt.Rows.Count > 0 Then
                        DeBug += "ConfigServiceList : " & dt.Rows.Count & vbCrLf
                        For Each dr As DataRow In dt.Rows
                            If dr("Active").ToString = "Y" Then
                                Dim awDt As New DataTable
                                awDt = eng.GetAlarmWaitingClear(Environment.MachineName, "SERVICE_" & dr("ServiceName"), "Alarm")
                                DeBug += "Active : " & dr("ServiceName") & " ### awDt :" & awDt.Rows.Count & vbCrLf
                                Dim ServiceInfo As New Engine.Info.WindowsServiceInfoENG(dr("ServiceName"))
                                DeBug += "ServiceInfo : " & dr("ServiceName") & "#### ServiceStatus :" & ServiceInfo.ServiceStatus & vbCrLf
                                If ServiceInfo.ServiceStatus <> "4" Then   'Started = 4
                                    Dim pDt As New DataTable
                                    pDt = eng.GetServicePendingAlarm(conf.ServerName, "CRITICAL")
                                    DeBug += "GetServicePendingAlarm : " & pDt.Rows.Count & " $$$ Repeat Check" & conf.RepeateCheck & vbCrLf
                                    Dim AlarmDesc As String = "The " & dr("ServiceName") & " on " & conf.ServerName & " has down"
                                    If pDt.Rows.Count < (conf.RepeateCheck - 1) Then
                                        eng.CreateServicePendingAlarm(conf, "CRITICAL", AlarmDesc, "1", conf.AlarmMethod, dr("ServiceName"))
                                        'ServiceInfo.StartService()
                                    Else
                                        DeBug += "Severity='CRITICAL' : " & vbCrLf
                                        awDt.DefaultView.RowFilter = "Severity='CRITICAL'"
                                        If awDt.DefaultView.Count = 0 Then
                                            DeBug += "awDt.DefaultView.Count : 0" & vbCrLf
                                            If eng.CreateAlarmWaitingClear(conf.ServerName, conf.IPAddress, "CRITICAL", 0, conf.AlarmMethod, "SERVICE_" & dr("ServiceName"), AlarmDesc, dr("AlarmCode"), conf.ServerName & "_" & dr("ServiceName")) > 0 Then
                                                eng.DeleteServicePendingAlarm(conf.ServerName)
                                            End If
                                            eng.SendAlarm(conf.ServerName, conf.ServerName & "_" & dr("ServiceName"), conf.IPAddress, conf.AlarmType, "ALM_" & dr("ServiceName") & "_PROCESS_DOWN", "CRITICAL", 0, AlarmDesc, conf.AlarmMethod, "SERVICE")
                                        Else
                                            DeBug += "awDt.DefaultView.Count : " & awDt.DefaultView.Count & vbCrLf
                                            If eng.UpdateAlarmWaitingClear(conf.ServerName, conf.IPAddress, "CRITICAL", 0, conf.AlarmMethod, "SERVICE_" & dr("ServiceName"), AlarmDesc, awDt.DefaultView(0)("id")) = True Then
                                                eng.DeleteServicePendingAlarm(conf.ServerName)
                                            End If
                                        End If

                                    End If
                                    pDt.Dispose()
                                Else
                                    If awDt.Rows.Count > 0 Then
                                        Dim sDr As DataRow = awDt.Rows(0)
                                        Dim sw As New InfoClass.SoftwareInfo
                                        sw.SendClearAlarm(conf.ServerName, conf.ServerName & "_" & dr("ServiceName"), conf.IPAddress, conf.AlarmType, "ALM_" & dr("ServiceName") & "_PROCESS_DOWN", "CRITICAL", 0, "ALM_LOG_SERVICE_DOWN is Clear", conf.AlarmMethod, "SERVICE_" & dr("ServiceName"), "")
                                        sw = Nothing
                                    End If
                                End If
                                awDt.Dispose()
                            End If
                        Next
                    End If
                    dt.Dispose()
                    ServiceInterval = conf.IntervalMinute
                    ServiceLastMonitorTime = DateTime.Now
                End If
                conf = Nothing
            End If
            eng = Nothing
            'MessageBox.Show(DeBug)
        End If
    End Sub

    Dim ProcessInterval As Integer = 0
    Dim ProcessLastMonitorTime As DateTime = DateTime.Now
    Private Sub ProcessMonitor()
        If DateAdd(DateInterval.Minute, ProcessInterval, ProcessLastMonitorTime) < DateTime.Now Then
            Dim eng As New InfoClass.ProcessInfo
            Dim ProcessConfigFile As String = Application.StartupPath & "\Config\" & Environment.MachineName & "_CONFIG_PROCESS_PROCESS.xml"
            If File.Exists(ProcessConfigFile) = True Then
                Dim conf As New Engine.Config.ProcessConfigENG
                conf.GetConfigFromXML(ProcessConfigFile)
                If conf.CheckAlarmWithTimeConfig(conf.AlamDateList, conf.AllDayEvent, conf.AlamTimeFrom, conf.AlamTimeTo) = True Then
                    Dim dt As New DataTable
                    dt = conf.ConfigProcessList
                    If dt.Rows.Count > 0 Then
                        For Each dr As DataRow In dt.Rows
                            If dr("Active").ToString = "Y" Then
                                Dim awDt As New DataTable
                                awDt = eng.GetAlarmWaitingClear(Environment.MachineName, "PROCESS_" & dr("WindowProcessName"), "Alarm")
                                If Process.GetProcessesByName(dr("WindowProcessName")).Length = 0 Then
                                    Dim pDt As New DataTable
                                    pDt = eng.GetProcessPendingAlarm(conf.ServerName, "CRITICAL")

                                    Dim AlarmDesc As String = "The " & dr("WindowProcessName") & " on " & conf.ServerName & " has down"
                                    If pDt.Rows.Count < (conf.RepeateCheck - 1) Then
                                        eng.CreateProcessPendingAlarm(conf, "CRITICAL", AlarmDesc, "1", conf.AlarmMethod, dr("WindowProcessName"))
                                    Else
                                        awDt.DefaultView.RowFilter = "Severity='CRITICAL'"
                                        If awDt.DefaultView.Count = 0 Then
                                            If eng.CreateAlarmWaitingClear(conf.ServerName, conf.IPAddress, "CRITICAL", 0, conf.AlarmMethod, "PROCESS_" & dr("WindowProcessName"), AlarmDesc, dr("AlarmCode"), conf.ServerName & "_" & dr("WindowProcessName")) > 0 Then
                                                eng.DeleteProcessPendingAlarm(conf.ServerName)
                                            End If
                                            eng.SendAlarm(conf.ServerName, conf.ServerName & "_" & dr("WindowProcessName"), conf.IPAddress, conf.AlarmType, "ALM_" & dr("WindowProcessName") & "_PROCESS_DOWN", "CRITICAL", 0, AlarmDesc, conf.AlarmMethod, "PROCESS")
                                        Else
                                            If eng.UpdateAlarmWaitingClear(conf.ServerName, conf.IPAddress, "CRITICAL", 0, conf.AlarmMethod, "PROCESS_" & dr("WindowProcessName"), AlarmDesc, awDt.DefaultView(0)("id")) = True Then
                                                eng.DeleteProcessPendingAlarm(conf.ServerName)
                                            End If
                                        End If
                                    End If
                                    pDt.Dispose()
                                    'End If
                                Else
                                    If awDt.Rows.Count > 0 Then
                                        Dim sDr As DataRow = awDt.Rows(0)
                                        Dim sw As New InfoClass.SoftwareInfo
                                        sw.SendClearAlarm(conf.ServerName, conf.ServerName & "_" & dr("WindowProcessName"), conf.IPAddress, conf.AlarmType, "ALM_" & dr("WindowProcessName") & "_PROCESS_DOWN", "CRITICAL", 0, "ALM_LOG_PROCESS_DOWN is Clear", conf.AlarmMethod, "PROCESS_" & dr("WindowProcessName"), "")
                                        sw = Nothing
                                    End If
                                End If
                                awDt.Dispose()
                            End If
                        Next
                    End If
                    dt.Dispose()
                    ProcessInterval = conf.IntervalMinute
                    ProcessLastMonitorTime = DateTime.Now
                End If
                conf = Nothing
            End If
            eng = Nothing
        End If
    End Sub

    Dim WebInterval As Integer = 0
    Dim WebLastMonitorTime As DateTime = DateTime.Now
    Private Sub WebMonitor()
        If DateAdd(DateInterval.Minute, WebInterval, WebLastMonitorTime) < DateTime.Now Then
            Dim eng As New InfoClass.WebInfo
            Dim WebConfigFile As String = Application.StartupPath & "\Config\" & Environment.MachineName & "_CONFIG_WEB_PROCESS.xml"
            If File.Exists(WebConfigFile) = True Then
                Dim conf As New Engine.Config.WebConfigENG
                conf.GetConfigFromXML(WebConfigFile)
                If conf.CheckAlarmWithTimeConfig(conf.AlamDateList, conf.AllDayEvent, conf.AlamTimeFrom, conf.AlamTimeTo) = True Then
                    Dim dt As New DataTable
                    dt = conf.ConfigWebList
                    If dt.Rows.Count > 0 Then
                        Dim wDt As New DataTable
                        wDt = GetWebApplicationList()

                        For Each dr As DataRow In dt.Rows
                            If dr("Active").ToString = "Y" Then
                                Dim awDt As New DataTable
                                awDt = eng.GetAlarmWaitingClear(Environment.MachineName, "WEB_" & dr("WebApplicationName"), "Alarm")

                                wDt.DefaultView.RowFilter = "WebApplicationName='" & dr("WebApplicationName") & "' and ApplicationStatus='Started'"
                                Dim wDv As DataView = wDt.DefaultView
                                If wDv.Count = 0 Then
                                    Dim pDt As New DataTable
                                    pDt = eng.GetWebPendingAlarm(conf.ServerName, "CRITICAL")

                                    Dim AlarmDesc As String = "The " & dr("WebApplicationName") & " Process on " & conf.ServerName & " has down"
                                    If pDt.Rows.Count < (conf.RepeateCheck - 1) Then
                                        eng.CreateWebPendingAlarm(conf, "CRITICAL", AlarmDesc, "1", conf.AlarmMethod, dr("WebApplicationName"))
                                    Else
                                        awDt.DefaultView.RowFilter = "Severity='CRITICAL'"
                                        If awDt.DefaultView.Count = 0 Then
                                            If eng.CreateAlarmWaitingClear(conf.ServerName, conf.IPAddress, "CRITICAL", 0, conf.AlarmMethod, "WEB_" & dr("WebApplicationName"), AlarmDesc, dr("AlarmCode"), conf.ServerName & "_" & dr("WebApplicationName")) > 0 Then
                                                eng.DeleteWebPendingAlarm(conf.ServerName)
                                            End If
                                            eng.SendAlarm(conf.ServerName, conf.ServerName & "_" & dr("WebApplicationName"), conf.IPAddress, conf.AlarmType, dr("AlarmCode"), "CRITICAL", 0, AlarmDesc, conf.AlarmMethod, "WEB")
                                        Else
                                            If eng.UpdateAlarmWaitingClear(conf.ServerName, conf.IPAddress, "CRITICAL", 0, conf.AlarmMethod, "WEB_" & dr("WebApplicationName"), AlarmDesc, awDt.DefaultView(0)("id")) = True Then
                                                eng.DeleteWebPendingAlarm(conf.ServerName)
                                            End If
                                        End If
                                    End If
                                    pDt.Dispose()
                                Else
                                    If awDt.Rows.Count > 0 Then
                                        Dim sDr As DataRow = awDt.Rows(0)
                                        Dim sw As New InfoClass.SoftwareInfo
                                        sw.SendClearAlarm(conf.ServerName, conf.ServerName & "_" & dr("WindowProcessName"), conf.IPAddress, conf.AlarmType, "ALM_" & dr("WindowProcessName") & "_PROCESS_DOWN", "CRITICAL", 0, "ALM_LOG_PROCESS_DOWN is Clear", conf.AlarmMethod, "PROCESS_" & dr("WindowProcessName"), "")
                                        sw = Nothing
                                    End If
                                End If
                                awDt.Dispose()
                            End If
                        Next
                        wDt.Dispose()
                    End If
                    dt.Dispose()
                    WebInterval = conf.IntervalMinute
                    WebLastMonitorTime = DateTime.Now
                End If
                conf = Nothing
            End If
            eng = Nothing
        End If
    End Sub

    Public Function GetWebApplicationList() As DataTable
        Dim dt As New DataTable
        dt.Columns.Add("SiteName")
        dt.Columns.Add("WebApplicationName")
        dt.Columns.Add("ApplicationPool")
        dt.Columns.Add("ApplicationStatus")
        Try
            Dim mgr As New Microsoft.Web.Administration.ServerManager()
            For Each WebSite As Microsoft.Web.Administration.Site In mgr.Sites
                For Each WebApp As Microsoft.Web.Administration.Application In WebSite.Applications
                    If Replace(WebApp.Path, "/", "") <> "" Then
                        Dim applState
                        applState = mgr.ApplicationPools(WebApp.ApplicationPoolName).State

                        Dim dr As DataRow = dt.NewRow
                        dr("SiteName") = WebSite.Name
                        dr("WebApplicationName") = Replace(WebApp.Path, "/", "")
                        dr("ApplicationPool") = Replace(WebApp.ApplicationPoolName, "/", "")
                        dr("ApplicationStatus") = applState
                        dt.Rows.Add(dr)
                    End If
                Next
            Next
        Catch ex As Exception

        End Try
        
        Return dt
    End Function


    Dim FileSizeInterval As Integer = 0
    Dim FileSizeLastMonitorTime As DateTime = DateTime.Now
    Private Sub FileSizeMonitor()
        If DateAdd(DateInterval.Minute, FileSizeInterval, FileSizeLastMonitorTime) < DateTime.Now Then
            Dim eng As New InfoClass.FileListInfo
            Dim FileSizeConfigFile As String = Application.StartupPath & "\Config\" & Environment.MachineName & "_CONFIG_DATAFILESIZE.xml"
            If File.Exists(FileSizeConfigFile) = True Then
                Dim conf As New Engine.Config.FileConfigENG
                conf.GetFileSizeConfigFromXML(FileSizeConfigFile)
                If conf.CheckAlarmWithTimeConfig(conf.AlamDateList, conf.AllDayEvent, conf.AlamTimeFrom, conf.AlamTimeTo) = True Then
                    Dim dt As New DataTable
                    dt = conf.ConfigFileList
                    If dt.Rows.Count > 0 Then
                        For Each dr As DataRow In dt.Rows
                            If dr("Active").ToString = "Y" Then
                                If File.Exists(dr("FileName")) = True Then
                                    Dim awDt As New DataTable
                                    awDt = eng.GetAlarmWaitingClear(Environment.MachineName, "FileSize_" & dr("FileName"), "Alarm")

                                    Dim ff As New FileInfo(dr("FileName"))
                                    Dim AlarmCode As String = conf.ServerName & "_DATAFILESIZE_" & ff.FullName & "_" & ff.Directory.Root.Name
                                    Dim CurrFileSize As Double = Convert.ToDouble(ff.Length / (1024 ^ 3))
                                    If CurrFileSize > Convert.ToDouble(dr("FileSizeCritical")) Then
                                        ProcessFileSizeAlarm(awDt, conf, "CRITICAL", eng, CurrFileSize, conf.AlarmMethod, Convert.ToDouble(dr("FileSizeCritical")), AlarmCode, ff)
                                    ElseIf CurrFileSize > Convert.ToDouble(dr("FileSizeMajor")) Then
                                        ProcessFileSizeAlarm(awDt, conf, "MAJOR", eng, CurrFileSize, conf.AlarmMethod, Convert.ToDouble(dr("FileSizeMajor")), AlarmCode, ff)
                                    ElseIf CurrFileSize > Convert.ToDouble(dr("FileSizeMinor")) Then
                                        ProcessFileSizeAlarm(awDt, conf, "MINOR", eng, CurrFileSize, conf.AlarmMethod, Convert.ToDouble(dr("FileSizeMinor")), AlarmCode, ff)
                                    ElseIf CurrFileSize <= Convert.ToDouble(dr("FileSizeMinor")) Then
                                        If awDt.Rows.Count > 0 Then
                                            Dim awDr As DataRow = awDt.Rows(0)
                                            Dim sw As New InfoClass.SoftwareInfo
                                            sw.SendClearAlarm(conf.ServerName, conf.ServerName & "_DATAFILESIZE_" & ff.FullName, conf.IPAddress, conf.AlarmType, conf.ServerName & "_DATAFILESIZE_", awDr("Severity"), CurrFileSize, "ALM_DATAFILESIZE_" & awDr("Severity") & " is Clear", conf.AlarmMethod, "FileSize_" & ff.FullName, "")
                                            sw = Nothing
                                        End If
                                    End If
                                    awDt.Dispose()
                                End If
                            End If
                        Next
                    End If
                    dt.Dispose()
                    FileSizeInterval = conf.IntervalMinute
                    FileSizeLastMonitorTime = DateTime.Now
                End If
                conf = Nothing
            End If
            eng = Nothing
        End If
    End Sub

    Private Sub ProcessFileSizeAlarm(ByVal awDT As DataTable, ByVal conf As Engine.Config.FileConfigENG, ByVal CheckSeverity As String, ByVal eng As InfoClass.FileListInfo, ByVal FileSizeUse As Double, ByVal AlarmMethod As String, ByVal ValueOver As Double, ByVal AlarmCode As String, ByVal ff As FileInfo)
        Dim AlarmDesc As String = "The DATAFILESIZE " & ff.FullName & " at " & ff.Directory.Root.Name & " on " & conf.ServerName & " usage over " & ValueOver
        Dim dt As New DataTable
        dt = eng.GetFileSizePendingAlarm(conf.ServerName, CheckSeverity, ff.FullName)
        If dt.Rows.Count < (Convert.ToInt16(conf.RepeateCheck) - 1) Then
            awDT.DefaultView.RowFilter = "Severity<>'" & CheckSeverity & "'"
            If awDT.DefaultView.Count > 0 Then
                For Each awDR As DataRowView In awDT.DefaultView
                    eng.SendClearAlarm(conf.ServerName, conf.ServerName & "_DATAFILESIZE_" & ff.FullName, conf.IPAddress, conf.AlarmType, conf.ServerName & "_DATAFILESIZE_" & ff.FullName, awDR("Severity"), FileSizeUse, "ALM_DATAFILESIZE_" & awDR("Severity") & " is Clear", awDR("AlarmMethod"), "FileSize_" & ff.FullName, "")
                Next
            End If
            eng.CreateFileSizePendingAlarm(conf, CheckSeverity, AlarmDesc, "1", AlarmMethod, ff.FullName)
        Else

            awDT.DefaultView.RowFilter = "Severity='" & CheckSeverity & "'"
            If awDT.DefaultView.Count = 0 Then
                If eng.CreateAlarmWaitingClear(conf.ServerName, conf.IPAddress, CheckSeverity, FileSizeUse, AlarmMethod, "FileSize_" & ff.FullName, AlarmDesc, AlarmCode, conf.ServerName & "_DATAFILESIZE_" & ff.FullName) > 0 Then
                    eng.DeleteFileSizePendingAlarm(conf.ServerName, ff.FullName)
                End If
                eng.SendAlarm(conf.ServerName, conf.ServerName & "_DATAFILESIZE_" & ff.FullName, conf.IPAddress, conf.AlarmType, conf.ServerName & "_DATAFILESIZE_" & ff.FullName, CheckSeverity, FileSizeUse, AlarmDesc, AlarmMethod, "FileSize_" & ff.FullName)
            Else
                If eng.UpdateAlarmWaitingClear(conf.ServerName, conf.IPAddress, CheckSeverity, FileSizeUse, AlarmMethod, "FileSize_" & ff.FullName, AlarmDesc, awDT.DefaultView(0)("id")) = True Then
                    eng.DeleteFileSizePendingAlarm(conf.ServerName, ff.FullName)
                End If
            End If
            awDT.DefaultView.RowFilter = ""
        End If
        dt.Dispose()
    End Sub


    Dim FileLostInterval As Integer = 0
    Dim FileLostLastMonitorTime As DateTime = DateTime.Now
    Private Sub FileLostMonitor()
        If DateAdd(DateInterval.Minute, FileLostInterval, FileLostLastMonitorTime) < DateTime.Now Then
            Dim eng As New InfoClass.FileListInfo
            Dim FileLostConfigFile As String = Application.StartupPath & "\Config\" & Environment.MachineName & "_CONFIG_DATAFILELOST.xml"
            If File.Exists(FileLostConfigFile) = True Then
                Dim conf As New Engine.Config.FileConfigENG
                conf.GetFileLostConfigFromXML(FileLostConfigFile)
                If conf.CheckAlarmWithTimeConfig(conf.AlamDateList, conf.AllDayEvent, conf.AlamTimeFrom, conf.AlamTimeTo) = True Then
                    Dim dt As New DataTable
                    dt = conf.ConfigFileList
                    If dt.Rows.Count > 0 Then
                        For Each dr As DataRow In dt.Rows
                            Dim FileName As String = dr("FileName").ToString
                            If dr("Active").ToString = "Y" Then
                                Dim awDt As New DataTable
                                awDt = eng.GetAlarmWaitingClear(Environment.MachineName, "FileLost_" & dr("FileName"), "Alarm")

                                If File.Exists(FileName) = False Then
                                    'Dim ff As New FileInfo(dr("FileName"))
                                    Dim AlarmCode As String = conf.ServerName & "_FILECONFIG_" & FileName & "_" & FileName.Substring(0, 2)
                                    Dim AlarmDesc As String = "The FILE CONFIG " & FileName & " at " & FileName.Substring(0, 2) & " on " & conf.ServerName & " is lost"
                                    Dim pdDt As New DataTable
                                    pdDt = eng.GetFileLostPendingAlarm(conf.ServerName, "CRITICAL", FileName)
                                    If pdDt.Rows.Count < (Convert.ToInt16(conf.RepeateCheck) - 1) Then
                                        awDt.DefaultView.RowFilter = "Severity<>'CRITICAL'"
                                        If awDt.DefaultView.Count > 0 Then
                                            For Each awDR As DataRowView In awDt.DefaultView
                                                eng.SendClearAlarm(conf.ServerName, conf.ServerName & "_FILECONFIG_" & FileName, conf.IPAddress, conf.AlarmType, conf.ServerName & "_FILECONFIG_" & FileName, awDR("Severity"), 0, "ALM_FILECONFIG_" & awDR("Severity") & " is Clear", awDR("AlarmMethod"), "FileLost_" & FileName, "")
                                            Next
                                        End If
                                        eng.CreateFileLostPendingAlarm(conf, "CRITICAL", AlarmDesc, "1", conf.AlarmMethod, FileName)
                                    Else
                                        awDt.DefaultView.RowFilter = "Severity='CRITICAL'"
                                        If awDt.DefaultView.Count = 0 Then
                                            If eng.CreateAlarmWaitingClear(conf.ServerName, conf.IPAddress, "CRITICAL", 0, conf.AlarmMethod, "FileLost_" & FileName, AlarmDesc, AlarmCode, conf.ServerName & "_FILECONFIG_" & FileName) > 0 Then
                                                eng.DeleteFileLostPendingAlarm(conf.ServerName, FileName)
                                            End If
                                            eng.SendAlarm(conf.ServerName, conf.ServerName & "_FILECONFIG_" & FileName, conf.IPAddress, conf.AlarmType, conf.ServerName & "_FILECONFIG_" & FileName, "CRITICAL", 0, AlarmDesc, conf.AlarmMethod, "FileLost_" & FileName)
                                        Else
                                        If eng.UpdateAlarmWaitingClear(conf.ServerName, conf.IPAddress, "CRITICAL", 0, conf.AlarmMethod, "FileLost_" & FileName, AlarmDesc, awDt.DefaultView(0)("id")) = True Then
                                            eng.DeleteFileLostPendingAlarm(conf.ServerName, FileName)
                                        End If
                                    End If
                                    awDt.DefaultView.RowFilter = ""
                                    End If
                                    pdDt.Dispose()
                                Else
                                    If awDt.Rows.Count > 0 Then
                                        Dim awDr As DataRow = awDt.Rows(0)
                                        Dim sw As New InfoClass.SoftwareInfo
                                        sw.SendClearAlarm(conf.ServerName, conf.ServerName & "_FILECONFIG_" & FileName, conf.IPAddress, conf.AlarmType, conf.ServerName & "_DATAFILESIZE_", awDr("Severity"), 0, "ALM_FILECONFIG_LOST is Clear", conf.AlarmMethod, "FileLost_" & FileName, "")
                                        sw = Nothing
                                    End If
                                End If
                                awDt.Dispose()
                            End If
                        Next
                    End If
                    dt.Dispose()
                    FileLostInterval = conf.IntervalMinute
                    FileLostLastMonitorTime = DateTime.Now
                End If
            End If
            eng = Nothing
        End If
    End Sub


    Dim ImaAliveInterval As Integer = 0
    Dim ImaAliveMonitorTime As DateTime = DateTime.Now
    Private Sub MonitorCheckAlive()
        If DateAdd(DateInterval.Minute, ImaAliveInterval, ImaAliveMonitorTime) < DateTime.Now Then
            Dim ini As New Org.Mentalis.Files.IniReader(Application.StartupPath & "\Config.ini")
            ini.Section = "ConfigAlive"

            Dim cfgIntervelMinute As Integer = IIf(ini.ReadString("IntervalMinute") = "", 0, ini.ReadString("IntervalMinute"))
            Dim AliveStartTime As String = ini.ReadString("StartTime")
            Dim AliveEndTime As String = ini.ReadString("EndTime")
            Dim vDateNow As DateTime = DateTime.Now
            Dim vTimeNow As String = vDateNow.ToString("HH:mm")
            If vTimeNow >= AliveStartTime And vTimeNow <= AliveEndTime Then
                ini.Section = "Setting"
                Try
                    Dim ws As New FaultManagementService.FaultManagementService
                    ws.Url = ini.ReadString("DCWebserviceURL")
                    ws.SendImAlive(Environment.MachineName, FunctionEng.GetIPAddress, cfgIntervelMinute, AliveStartTime, AliveEndTime, DateAdd(DateInterval.Minute, 3, vDateNow))
                    ws = Nothing
                Catch ex As Exception

                End Try
            End If
            ini = Nothing
            ImaAliveInterval = cfgIntervelMinute
            ImaAliveMonitorTime = DateTime.Now
        End If
    End Sub
#End Region

#Region "Config Zone"

    Public Sub SetListConfigData()
        Dim cfgDT As New DataTable
        Dim CPUConfigFile As String = Application.StartupPath & "\Config\" & Environment.MachineName & "_CONFIG_CPU_PROCESS.xml"
        Dim cCPU As New Engine.Config.CPUConfigENG
        cCPU.GetConfigFromXML(CPUConfigFile)
        Dim cDt As New DataTable
        cDt = cCPU.ConfigList
        If cDt.Rows.Count > 0 Then
            cfgDT.Merge(cDt)
        End If
        cDt.Dispose()
        cCPU = Nothing

        Dim RAMConfigFile As String = Application.StartupPath & "\Config\" & Environment.MachineName & "_CONFIG_RAM_PROCESS.xml"
        Dim cRAM As New Engine.Config.RamConfigENG
        cRAM.GetConfigFromXML(RAMConfigFile)
        Dim rDt As New DataTable
        rDt = cRAM.ConfigList
        If rDt.Rows.Count > 0 Then
            cfgDT.Merge(rDt)
        End If
        rDt.Dispose()
        cRAM = Nothing

        Dim HDDConfigFile As String = Application.StartupPath & "\Config\" & Environment.MachineName & "_CONFIG_HDD_PROCESS.xml"
        Dim dHDD As New Engine.Config.HDDConfigENG
        dHDD.GetConfigFromXML(HDDConfigFile)
        Dim dDt As New DataTable
        dDt = dHDD.ConfigList
        If dDt.Rows.Count > 0 Then
            cfgDT.Merge(dDt)
        End If
        dDt.Dispose()
        dHDD = Nothing


        Dim ServiceConfigFile As String = Application.StartupPath & "\Config\" & Environment.MachineName & "_CONFIG_SERVICE_PROCESS.xml"
        Dim SevEng As New Engine.Config.ServiceConfigENG
        SevEng.GetConfigFromXML(ServiceConfigFile)
        Dim seDt As New DataTable
        seDt = SevEng.ConfigList
        If seDt.Rows.Count > 0 Then
            cfgDT.Merge(seDt)
        End If
        seDt.Dispose()
        SevEng = Nothing


        Dim ProcessConfigFile As String = Application.StartupPath & "\Config\" & Environment.MachineName & "_CONFIG_PROCESS_PROCESS.xml"
        Dim pEng As New Engine.Config.ProcessConfigENG
        pEng.GetConfigFromXML(ProcessConfigFile)
        Dim pDt As New DataTable
        pDt = pEng.ConfigList
        If pDt.Rows.Count > 0 Then
            cfgDT.Merge(pDt)
        End If
        pDt.Dispose()
        pEng = Nothing

        Dim WebAppConfigFile As String = Application.StartupPath & "\Config\" & Environment.MachineName & "_CONFIG_WEB_PROCESS.xml"
        Dim wEng As New Engine.Config.WebConfigENG
        wEng.GetConfigFromXML(WebAppConfigFile)
        Dim wDt As New DataTable
        wDt = wEng.ConfigList
        If wDt.Rows.Count > 0 Then
            cfgDT.Merge(wDt)
        End If
        wDt.Dispose()
        wEng = Nothing

        Dim FileSizeConfigFile As String = Application.StartupPath & "\Config\" & Environment.MachineName & "_CONFIG_DATAFILESIZE.xml"
        Dim fsEng As New Engine.Config.FileConfigENG
        fsEng.GetFileSizeConfigFromXML(FileSizeConfigFile)
        Dim fsDt As New DataTable
        fsDt = fsEng.ConfigList
        If fsDt.Rows.Count > 0 Then
            cfgDT.Merge(fsDt)
        End If
        fsDt.Dispose()
        fsEng = Nothing

        Dim FileLostConfigFile As String = Application.StartupPath & "\Config\" & Environment.MachineName & "_CONFIG_DATAFILELOST.xml"
        Dim flEng As New Engine.Config.FileConfigENG
        flEng.GetFileLostConfigFromXML(FileLostConfigFile)
        Dim flDt As New DataTable
        flDt = flEng.ConfigList
        If flDt.Rows.Count > 0 Then
            cfgDT.Merge(flDt)
        End If
        flDt.Dispose()
        flEng = Nothing

        If cfgDT.Rows.Count > 0 Then
            DGVConfigSetting.Columns.Clear()
            DGVConfigSetting.DataSource = cfgDT

            Dim delCol As New DataGridViewButtonColumn
            delCol.Text = "Delete"
            delCol.UseColumnTextForButtonValue = True
            delCol.Width = 20
            DGVConfigSetting.Columns.Add(delCol)

            DGVConfigSetting.Columns("XMLFileName").Visible = False
        Else
            DGVConfigSetting.DataSource = Nothing
        End If
    End Sub

    Private Sub tmListConfigData_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmListConfigData.Tick
        tmListConfigData.Enabled = False
        LoadConfigFileFromDC()
        SetListConfigData()
        tmListConfigData.Enabled = True
    End Sub

    Private Sub DGVConfigSetting_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGVConfigSetting.CellContentClick
        SetEnableTimer(False)
        If e.RowIndex < 0 Then
            Exit Sub
        End If
        Dim grv As DataGridViewRow = DGVConfigSetting.Rows(e.RowIndex)
        If grv.Cells(e.ColumnIndex).Value = "Delete" Then
            Dim ini As New Org.Mentalis.Files.IniReader(Application.StartupPath & "\Config.ini")
            ini.Section = "Setting"

            Try
                Dim ws As New FaultManagementService.FaultManagementService
                ws.Timeout = 10000
                ws.Url = ini.ReadString("DCWebserviceURL")

                Dim dt As New DataTable
                dt = ws.GetAlarmByServerName(Environment.MachineName)
                If dt.Rows.Count > 0 Then
                    If grv.Cells("ConfigType").Value = "Window Process" Then
                        dt.DefaultView.RowFilter = "AlarmActivity like '%PROCESS%' and FlagAlarm='Alarm'"
                    ElseIf grv.Cells("ConfigType").Value = "WebApplication" Then
                        dt.DefaultView.RowFilter = "AlarmActivity like '%WEB%' and FlagAlarm='Alarm'"
                    Else
                        dt.DefaultView.RowFilter = "AlarmActivity like '%" & grv.Cells("ConfigType").Value & "%' and FlagAlarm='Alarm'"
                    End If

                    If dt.DefaultView.Count > 0 Then
                        MessageBox.Show("การ Config ของ " & grv.Cells("ConfigType").Value & " ยังคงมี Alarm ที่ยังไม่ Clear ไม่สามารถลบ Config ได้")
                        dt.Dispose()
                        ws = Nothing
                        ini = Nothing
                        SetEnableTimer(True)
                        SetListProcessAlarm()
                        Exit Sub
                    End If
                End If
                dt.Dispose()

                Dim Yes As Integer = MessageBox.Show("ยืนยันการลบ Config", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
                If Yes = 1 Then
                    Try
                        Dim XMLFile As New FileInfo(grv.Cells("XMLFileName").Value)
                        File.Delete(XMLFile.FullName)

                        Try
                            ws.DeleteConfigFile(XMLFile.Name, 1)
                        Catch ex As Exception
                            MessageBox.Show("FaultManagementService Error !!! " & ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly, False)
                        End Try
                        XMLFile = Nothing
                        SetListConfigData()
                        SetListProcessAlarm()
                    Catch ex As Exception
                        MessageBox.Show("Delete Error !!! " & ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly, False)
                    End Try
                End If
                ws = Nothing
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
        SetEnableTimer(True)
    End Sub
    Private Sub DGVConfigSetting_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGVConfigSetting.CellDoubleClick
        If e.RowIndex < 0 Then
            Exit Sub
        End If

        Try

            If DGVConfigSetting.Rows(e.RowIndex).Cells("XMLFileName").Value <> "" Then
                If IO.File.Exists(DGVConfigSetting.Rows(e.RowIndex).Cells("XMLFileName").Value) = True Then
                    SetEnableTimer(False)
                    If DGVConfigSetting.Rows(e.RowIndex).Cells("Type").Value = "Hardware" Then
                        Dim CPUConfigFile As String = Application.StartupPath & "\Config\" & Environment.MachineName & "_CONFIG_CPU_PROCESS.xml"
                        Dim RAMConfigFile As String = Application.StartupPath & "\Config\" & Environment.MachineName & "_CONFIG_RAM_PROCESS.xml"
                        Dim HDDConfigFile As String = Application.StartupPath & "\Config\" & Environment.MachineName & "_CONFIG_HDD_PROCESS.xml"
                        Dim frm As New frmConfigHW
                        If InStr(DGVConfigSetting.Rows(e.RowIndex).Cells("ConfigType").Value, "CPU") > 0 Then
                            frm.SetRAMData(RAMConfigFile)
                            frm.SetHDDData(HDDConfigFile)

                            frm.SetCPUData(CPUConfigFile)
                        ElseIf InStr(DGVConfigSetting.Rows(e.RowIndex).Cells("ConfigType").Value, "RAM") > 0 Then
                            frm.SetHDDData(HDDConfigFile)
                            frm.SetCPUData(CPUConfigFile)

                            frm.SetRAMData(RAMConfigFile)
                        ElseIf InStr(DGVConfigSetting.Rows(e.RowIndex).Cells("ConfigType").Value, "HardDisk") > 0 Then
                            frm.SetCPUData(CPUConfigFile)
                            frm.SetRAMData(RAMConfigFile)

                            frm.SetHDDData(HDDConfigFile)
                        End If
                        frm.ShowDialog()
                    ElseIf DGVConfigSetting.Rows(e.RowIndex).Cells("Type").Value = "Software" Then
                        Dim frm As New frmConfigSW
                        If DGVConfigSetting.Rows(e.RowIndex).Cells("ConfigType").Value = "Service" Then
                            frm.txtDefaultData.Text = "Service"
                        ElseIf DGVConfigSetting.Rows(e.RowIndex).Cells("ConfigType").Value = "Window Process" Then
                            frm.txtDefaultData.Text = "Process"
                        ElseIf DGVConfigSetting.Rows(e.RowIndex).Cells("ConfigType").Value = "WebApplication" Then
                            frm.txtDefaultData.Text = "WebApplication"
                        ElseIf DGVConfigSetting.Rows(e.RowIndex).Cells("ConfigType").Value = "FileSize" Then
                            frm.txtDefaultData.Text = "FileSize"
                        ElseIf DGVConfigSetting.Rows(e.RowIndex).Cells("ConfigType").Value = "FileConfigLost" Then
                            frm.txtDefaultData.Text = "FileConfigLost"
                        End If
                        frm.ShowDialog()
                    End If
                    SetEnableTimer(True)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Get Data Error !!! " & ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly, False)
        End Try
        
    End Sub

    Private Sub frmMain_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        If Me.WindowState = FormWindowState.Minimized Then
            Me.Hide()
            NotifyIcon1.Visible = True
            NotifyIcon1.Text = "Fault Management V" & getMyVersion()
        Else
            NotifyIcon1.Visible = False
        End If
    End Sub

    Private Function getMyVersion() As String
        Dim version As System.Version = System.Reflection.Assembly.GetExecutingAssembly.GetName().Version
        Return version.Major & "." & version.Minor & "." & version.Build & "." & version.Revision
    End Function

    Private Sub frmMain_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        LoadConfigFileFromDC()
        SetListConfigData()
        SetListProcessAlarm()
        Me.Text = "Fault Management V" & getMyVersion()
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub LoadConfigFileFromDC()
        Try
            Dim ini As New Org.Mentalis.Files.IniReader(Application.StartupPath & "\Config.ini")
            ini.Section = "Setting"

            Dim ConfigFolder As String = Application.StartupPath & "\Config\"

            Dim dt As New DataTable
            Dim ws As New FaultManagementService.FaultManagementService
            ws.Timeout = 20000
            ws.Url = ini.ReadString("DCWebserviceURL")
            dt = ws.LoadConfigFileFromDC(Environment.MachineName)
            If dt.Rows.Count > 0 Then

                If IO.Directory.Exists(ConfigFolder) = False Then
                    IO.Directory.CreateDirectory(ConfigFolder)
                End If

                For Each ff As String In Directory.GetFiles(ConfigFolder, "*.xml")
                    Try
                        File.SetAttributes(ff, FileAttributes.Normal)
                        File.Delete(ff)
                    Catch ex As Exception

                    End Try
                Next

                For Each dr As DataRow In dt.Rows
                    If IO.File.Exists(ConfigFolder & dr("FileName")) = True Then
                        IO.File.Delete(ConfigFolder & dr("FileName"))
                    End If

                    Dim FileByte As Byte() = DirectCast(dr("FileByte"), Byte())
                    If FileByte.Length > 0 Then
                        Dim fs As FileStream
                        fs = New FileStream(ConfigFolder & dr("FileName"), FileMode.CreateNew)
                        fs.Write(FileByte, 0, FileByte.Length)
                        fs.Close()
                    End If
                    FileByte = Nothing
                Next
                tmMonitor.Enabled = True
            Else
                'ถ้าไม่มีการ Config เลยก็ให้หยุด Monitor
                tmMonitor.Enabled = False
                DGVConfigSetting.DataSource = Nothing
                For Each f As String In Directory.GetFiles(ConfigFolder)
                    Try
                        File.SetAttributes(f, FileAttributes.Normal)
                        File.Delete(f)
                    Catch ex As Exception

                    End Try
                Next
            End If
            dt.Dispose()
            ws = Nothing
            ini = Nothing
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnAddSwConfig_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddSwConfig.Click
        SetEnableTimer(False)

        Dim frm As New frmConfigSW
        frm.txtDefaultData.Text = ""
        frm.txtDefaultData.Text = "Service"    'Default Tab Service
        frm.ShowDialog()
        SetEnableTimer(True)
    End Sub

    Private Sub SetEnableTimer(ByVal Status As Boolean)
        tmListConfigData.Enabled = Status
        tmListAlarm.Enabled = Status
        tmMonitor.Enabled = Status
        'tmCheckAlive.Enabled = Status
    End Sub

    Private Sub BtnHWConfig_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnHWConfig.Click
        SetEnableTimer(False)
        Dim frm As New frmConfigHW
        frm.IsAddConfig = True
        frm.ShowDialog()
        SetEnableTimer(True)
    End Sub
#End Region

    Private Sub tmMonitor_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmMonitor.Tick
        tmMonitor.Enabled = False
        CPUMonitor()
        RamMonitor()
        HDDMonitor()

        ServiceMonitor()
        ProcessMonitor()
        WebMonitor()
        FileSizeMonitor()
        FileLostMonitor()


        tmMonitor.Enabled = True
    End Sub


    Private Sub SetListProcessAlarm()
        Dim dt As New DataTable
        dt = GetAlarmData("", "Alarm", New Date(1, 1, 1), New Date(1, 1, 1))

        DGVLog.AutoGenerateColumns = False
        DGVLog.DataSource = dt

        If dt.Rows.Count > 0 Then
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
        End If
    End Sub

    Public Function GetAlarmData(ByVal ServerName As String, ByVal FlagAlarm As String, ByVal DateFrom As DateTime, ByVal DateTo As DateTime) As DataTable
        Dim dt As New DataTable
        'Dim sql As String = "select * from AlarmWaitingClear where 1=1"
        'If ServerName.Trim <> "" Then
        '    sql += " and ServerName like '%" & ServerName & "%'"
        'End If
        'If FlagAlarm.Trim <> "" Then
        '    sql += " and FlagAlarm = '" & FlagAlarm & "'"
        'End If
        'If DateFrom.Year <> 1 Then
        '    sql += " and Format(CreateDate, 'yyyy/MM/dd') >= '" & Format(DateFrom, "yyyy/MM/dd") & "'"
        'End If
        'If DateTo.Year <> 1 Then
        '    sql += " and Format(CreateDate, 'yyyy/MM/dd') <= '" & Format(DateTo, "yyyy/MM/dd") & "'"
        'End If
        'sql += " order by UpdateDate desc"

        'dt = Engine.ConnectDB.ShopAccessDB.GetDataTable(sql)

        Try
            Dim ini As New Org.Mentalis.Files.IniReader(Application.StartupPath & "\Config.ini")
            ini.Section = "Setting"
            Dim ws As New FaultManagementService.FaultManagementService
            ws.Timeout = 20000
            ws.Url = ini.ReadString("DCWebserviceURL")
            dt = ws.GetAlarmByServerName(Environment.MachineName)
            ws = Nothing
            ini = Nothing
        Catch ex As Exception

        End Try
        If dt.Rows.Count > 0 Then
            dt.DefaultView.Sort = "UpdateDate desc"
            dt = dt.DefaultView.ToTable
        End If

        Return dt
    End Function

    Private Sub tmListAlarm_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmListAlarm.Tick
        SetListProcessAlarm()
        Me.Text = "Fault Management V" & getMyVersion() & "  Last Refresh Time :" & DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss", New Globalization.CultureInfo("en-US"))
    End Sub

    Private Sub btnAddPortConfig_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddPortConfig.Click
        Dim frm As New frmConfigPort
        frm.StartPosition = FormStartPosition.CenterParent
        frm.ShowDialog()
    End Sub

    Private Sub NotifyIcon1_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles NotifyIcon1.MouseDoubleClick
        Me.Show()
        Me.WindowState = FormWindowState.Normal
        NotifyIcon1.Visible = False
    End Sub

    Private Sub DGVLog_Sorted(ByVal sender As Object, ByVal e As System.EventArgs) Handles DGVLog.Sorted
        SetListProcessAlarm()
    End Sub

    Private Sub tmCheckAlive_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmCheckAlive.Tick
        tmCheckAlive.Enabled = False
        MonitorCheckAlive()
        tmCheckAlive.Enabled = True
    End Sub
End Class