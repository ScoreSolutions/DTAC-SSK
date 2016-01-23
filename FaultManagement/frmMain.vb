Imports System.Data
Imports Engine.Common
Public Class frmMain

    Private Sub frmMain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        

    End Sub


    Dim CPUInterval As Integer = 0
    Dim CPULastMonitorTime As DateTime = DateTime.Now
    Dim CPURepeateCheckCritical As Integer = 0
    Dim CPURepeateCheckMajor As Integer = 0
    Dim CPURepeateCheckMinor As Integer = 0

    Private Sub tmCPUMonitor_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmCPUMonitor.Tick
        If DateAdd(DateInterval.Minute, CPUInterval, CPULastMonitorTime) < DateTime.Now Then
            Dim conf As New Engine.Config.CPUConfigENG
            conf.GetConfigFromXML(Application.StartupPath & "\Config\" & Environment.MachineName & "_CONFIG_CPU_PROCESS.xml")


            Dim inf As New Engine.Info.CPUInfoENG
            If inf.PercentUsage > conf.SeverityCritical.ValueOver Then
                CPURepeateCheckMajor = 0
                CPURepeateCheckMinor = 0
                CPURepeateCheckCritical += 1
                If CPURepeateCheckCritical >= conf.RepeateCheck Then
                    'Send Alarm Critical
                End If
            ElseIf inf.PercentUsage > conf.SeverityMajor.ValueOver Then
                CPURepeateCheckCritical = 0
                CPURepeateCheckMinor = 0
                CPURepeateCheckMajor += 1
                If CPURepeateCheckMajor >= conf.RepeateCheck Then
                    'Send Alarm Major
                End If
            ElseIf inf.PercentUsage > conf.SeverityMinor.ValueOver Then
                CPURepeateCheckCritical = 0
                CPURepeateCheckMajor = 0
                CPURepeateCheckMinor += 1
                If CPURepeateCheckMinor >= conf.RepeateCheck Then
                    'Send Alarm Minor
                End If
            End If

            CPUInterval = conf.IntervalMinute
            CPULastMonitorTime = DateTime.Now
            conf = Nothing
        End If
    End Sub
End Class