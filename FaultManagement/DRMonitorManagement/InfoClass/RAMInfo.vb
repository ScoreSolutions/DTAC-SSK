Imports System.IO
Imports System.Xml
'Imports Engine.ConnectDB
Imports CenLinqDB.Common.Utilities

Namespace InfoClass
    Public Class RAMInfo
        Inherits HardwareInfo

        Public Function GetRAMMonitorFile() As DataTable
            Dim ret As New DataTable
            ret.Columns.Add("ServerName")
            ret.Columns.Add("RAMPercentUsage", GetType(Double))
            ret.Columns.Add("RAMConfigFileName")
            ret.Columns.Add("LastMonitorTime", GetType(DateTime))

            Dim ini As New DRMonitorManagement.Org.Mentalis.Files.IniReader(Application.StartupPath & "\Config.ini")
            ini.Section = "Setting"
            Dim FMMonitorFolder As String = ini.ReadString("FMMonitorFolder")
            If Directory.Exists(FMMonitorFolder) = False Then
                Directory.CreateDirectory(FMMonitorFolder)
            End If

            For Each f As String In IO.Directory.GetFiles(FMMonitorFolder, "*RAM_PROCESS.xml")
                Dim fInfo As New FileInfo(f)
                Dim ff() As String = Split(fInfo.Name, "_")
                Dim ServerName As String = ff(0)

                Dim xml As New XmlDocument
                xml.Load(f)
                Dim RAMPercent As Xml.XmlNodeList = xml.GetElementsByTagName("PercentUsageGB")
                If RAMPercent.Item(0).InnerXml <> "" Then
                    Dim dr As DataRow = ret.NewRow
                    dr("ServerName") = ServerName
                    dr("RAMPercentUsage") = RAMPercent.Item(0).InnerText
                    dr("RAMConfigFileName") = ini.ReadString("FMConfigFolder") & ServerName & "_CONFIG_RAM_PROCESS.xml"
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

        Public Sub CreateRAMPendingAlarm(ByVal conf As Engine.Config.RamConfigENG, ByVal Severity As String, ByVal AlarmValue As Double, ByVal AlarmDesc As String, ByVal FlagClear As String, ByVal AlarmMethod As String)
            MyBase.CreatePendingAlarm("RAM", conf.ServerName, conf.IPAddress, Severity, AlarmValue, AlarmDesc, FlagClear, AlarmMethod, conf.AlarmType, conf.ServerName & "_RAM_PROCESS")
        End Sub

        Public Function GetRAMPendingAlarm(ByVal ServerName As String, ByVal Severity As String) As DataTable
            Return MyBase.GetPendingAlarm("RAM", ServerName, Severity)
        End Function

        Public Sub DeleteRAMPendingAlarm(ByVal ServerName As String)
            MyBase.DeletePendingAlarm("RAM", ServerName)
        End Sub
    End Class
End Namespace

