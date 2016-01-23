Imports System.IO
Imports System.Xml
'Imports Engine.ConnectDB
Imports CenLinqDB.Common.Utilities
Namespace InfoClass
    Public Class CPUInfo
        Inherits HardwareInfo
        Public Function GetCPUMonitorFile() As DataTable
            Dim ret As New DataTable
            ret.Columns.Add("ServerName")
            ret.Columns.Add("CPUPercentUsage")
            ret.Columns.Add("CPUConfigFileName")
            ret.Columns.Add("LastMonitorTime", GetType(DateTime))

            Dim ini As New DRMonitorManagement.Org.Mentalis.Files.IniReader(Application.StartupPath & "\Config.ini")
            ini.Section = "Setting"
            Dim FMMonitorFolder As String = ini.ReadString("FMMonitorFolder")
            If Directory.Exists(FMMonitorFolder) = False Then
                Directory.CreateDirectory(FMMonitorFolder)
            End If

            For Each f As String In IO.Directory.GetFiles(FMMonitorFolder, "*CPU_PROCESS.xml")
                Dim fInfo As New FileInfo(f)
                Dim ff() As String = Split(fInfo.Name, "_")
                Dim ServerName As String = ff(0)

                Dim xml As New XmlDocument
                xml.Load(f)
                Dim CPUPercent As Xml.XmlNodeList = xml.GetElementsByTagName("PercentUsage")
                If CPUPercent.Item(0).InnerXml <> "" Then
                    Dim dr As DataRow = ret.NewRow
                    dr("ServerName") = ServerName
                    dr("CPUPercentUsage") = CPUPercent.Item(0).InnerText
                    dr("CPUConfigFileName") = ini.ReadString("FMConfigFolder") & ServerName & "_CONFIG_CPU_PROCESS.xml"
                    dr("LastMonitorTime") = fInfo.LastWriteTime
                    ret.Rows.Add(dr)
                End If
                xml = Nothing
                fInfo = Nothing

                File.Delete(f)
            Next
            ini = Nothing

            Return ret
        End Function

        Public Sub CreateCPUPendingAlarm(ByVal conf As Engine.Config.CPUConfigENG, ByVal Severity As String, ByVal AlarmValue As Integer, ByVal AlarmDesc As String, ByVal FlagClear As String, ByVal AlarmMethod As String)
            MyBase.CreatePendingAlarm("CPU", conf.ServerName, conf.IPAddress, Severity, AlarmValue, AlarmDesc, FlagClear, AlarmMethod, conf.AlarmType, conf.ServerName & "_CPU_PROCESS")
        End Sub

        Public Function GetCPUPendingAlarm(ByVal ServerName As String, ByVal Severity As String) As DataTable
            Return MyBase.GetPendingAlarm("CPU", ServerName, Severity)
        End Function

        Public Sub DeleteCPUPendingAlarm(ByVal ServerName As String)
            MyBase.DeletePendingAlarm("CPU", ServerName)
        End Sub


    End Class
End Namespace

