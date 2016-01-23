Imports System.IO
Imports System.Xml
'Imports Engine.ConnectDB
Imports CenLinqDB.Common.Utilities

Namespace InfoClass
    Public Class HDDInfo
        Inherits HardwareInfo

        Public Function GetHDDMonitorFile() As DataTable
            Dim ret As New DataTable
            ret.Columns.Add("ServerName")
            ret.Columns.Add("DriveLetter")
            ret.Columns.Add("HDDTotalSize", GetType(Double))
            ret.Columns.Add("HDDPercentUsage", GetType(Double))
            ret.Columns.Add("HDDConfigFileName")
            ret.Columns.Add("LastMonitorTime", GetType(DateTime))

            Dim ini As New DRMonitorManagement.Org.Mentalis.Files.IniReader(Application.StartupPath & "\Config.ini")
            ini.Section = "Setting"
            Dim FMMonitorFolder As String = ini.ReadString("FMMonitorFolder")
            If Directory.Exists(FMMonitorFolder) = False Then
                Directory.CreateDirectory(FMMonitorFolder)
            End If

            For Each f As String In IO.Directory.GetFiles(FMMonitorFolder, "*HDD_PROCESS.xml")
                Try
                    Dim fInfo As New FileInfo(f)
                    Dim ff() As String = Split(fInfo.Name, "_")
                    Dim ServerName As String = ff(0)
                    Dim xml As New XmlDocument
                    xml.Load(f)
                    Dim DriveInfoNode As Xml.XmlNodeList = xml.GetElementsByTagName("DriveInfo")
                    For Each DriveLetter As XmlElement In DriveInfoNode.Item(0).ChildNodes
                        Dim dr As DataRow = ret.NewRow
                        dr("ServerName") = ServerName
                        dr("DriveLetter") = DriveLetter.Name
                        dr("HDDTotalSize") = DriveLetter.GetElementsByTagName("TotalSizeGB").Item(0).InnerText
                        dr("HDDPercentUsage") = DriveLetter.GetElementsByTagName("PercentUsage").Item(0).InnerText
                        dr("HDDConfigFileName") = ini.ReadString("FMConfigFolder") & ServerName & "_CONFIG_HDD_PROCESS.xml"
                        dr("LastMonitorTime") = fInfo.LastWriteTime
                        ret.Rows.Add(dr)
                    Next
                    xml = Nothing
                    fInfo = Nothing
                    File.Delete(f)
                Catch ex As Exception

                End Try
            Next
            ini = Nothing

            Return ret
        End Function

        Public Sub CreateHDDPendingAlarm(ByVal conf As Engine.Config.HDDConfigENG, ByVal Severity As String, ByVal AlarmValue As Double, ByVal AlarmDesc As String, ByVal FlagClear As String, ByVal AlarmMethod As String, ByVal DriveLetter As String)
            'Dim Sql As String = "insert into HDDPendingAlarm (ServerName,SysLocation,HostIP,"
            'Sql += " AlarmType,AlarmName,Severity,AlarmValue,AlarmDesc,FlagClear,AlarmMethod,CreateDate, DriveLetter)"
            'Sql += " values('" & conf.ServerName & "','" & conf.ServerName & "_HDD','" & conf.IPAddress & "',"
            'Sql += " '" & conf.AlarmType & "','" & conf.ServerName & "_HardDisk_" & DriveLetter & "','" & Severity & "','" & AlarmValue & "','" & AlarmDesc & "','" & FlagClear & "','" & AlarmMethod & "',Now(),'" & DriveLetter & "')"

            'QisFaultDB.ExecuteNonQuery(Sql)
            MyBase.CreatePendingAlarm("HardDisk_" & DriveLetter, conf.ServerName, conf.IPAddress, Severity, AlarmValue, AlarmDesc, FlagClear, AlarmMethod, conf.AlarmType, conf.ServerName & "_HardDisk_" & DriveLetter)
        End Sub

        Public Function GetHDDPendingAlarm(ByVal ServerName As String, ByVal Severity As String, ByVal DriveLetter As String) As DataTable
            'Dim dt As New DataTable
            'dt = QisFaultDB.GetDataTable("select * from HDDPendingAlarm where ServerName='" & ServerName & "' and Severity='" & Severity & "' and DriveLetter='" & DriveLetter & "'")
            'Return dt

            Return MyBase.GetPendingAlarm("HardDisk_" & DriveLetter, ServerName, Severity)
        End Function

        Public Sub DeleteHDDPendingAlarm(ByVal ServerName As String, ByVal DriveLetter As String)
            'Dim sql As String = "delete from HDDPendingAlarm where ServerName='" & ServerName & "' and DriveLetter='" & DriveLetter & "' "
            'QisFaultDB.ExecuteNonQuery(sql)
            MyBase.DeletePendingAlarm("HardDisk_" & DriveLetter, ServerName)
        End Sub
    End Class
End Namespace

