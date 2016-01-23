Imports Microsoft.Web.Administration
Imports System.DirectoryServices
Imports Engine.ConnectDB

Namespace InfoClass
    Public Class FileListInfo
        Inherits SoftwareInfo

#Region "File Size Info"

        Public Sub CreateFileSizePendingAlarm(ByVal conf As Engine.Config.FileConfigENG, ByVal Severity As String, ByVal AlarmDesc As String, ByVal FlagClear As String, ByVal AlarmMethod As String, ByVal FileName As String)
            Dim Sql As String = "insert into FileSizePendingAlarm (ServerName,SysLocation,HostIP,FileName,"
            Sql += " AlarmType,AlarmName,Severity,AlarmDesc,FlagClear,AlarmMethod,CreateDate)"
            Sql += " values('" & conf.ServerName & "','" & conf.ServerName & "_" & FileName & "','" & conf.IPAddress & "','" & FileName & "',"
            Sql += " '" & conf.AlarmType & "','ALM_DATAFILESIZE_" & Severity & "','" & Severity & "','" & AlarmDesc & "','" & FlagClear & "','" & AlarmMethod & "',Now())"

            ShopAccessDB.ExecuteNonQuery(Sql)
        End Sub

        Public Function GetFileSizePendingAlarm(ByVal ServerName As String, ByVal Severity As String, ByVal FileName As String) As DataTable
            Dim dt As New DataTable
            dt = ShopAccessDB.GetDataTable("select * from FileSizePendingAlarm where ServerName='" & ServerName & "' and Severity='" & Severity & "' and FileName='" & FileName & "'")
            Return dt
        End Function

        Public Sub DeleteFileSizePendingAlarm(ByVal ServerName As String, ByVal FileName As String)
            Dim sql As String = "delete from FileSizePendingAlarm where ServerName='" & ServerName & "' and FileName='" & FileName & "' "
            ShopAccessDB.ExecuteNonQuery(sql)
        End Sub
#End Region

#Region "File Lost Info"
        Public Sub CreateFileLostPendingAlarm(ByVal conf As Engine.Config.FileConfigENG, ByVal Severity As String, ByVal AlarmDesc As String, ByVal FlagClear As String, ByVal AlarmMethod As String, ByVal FileName As String)
            Dim Sql As String = "insert into FileLostPendingAlarm (ServerName,SysLocation,HostIP,FileName,"
            Sql += " AlarmType,AlarmName,Severity,AlarmDesc,FlagClear,AlarmMethod,CreateDate)"
            Sql += " values('" & conf.ServerName & "','" & conf.ServerName & "_" & FileName & "','" & conf.IPAddress & "','" & FileName & "',"
            Sql += " '" & conf.AlarmType & "','ALM_DATAFILESIZE_" & Severity & "','" & Severity & "','" & AlarmDesc & "','" & FlagClear & "','" & AlarmMethod & "',Now())"

            ShopAccessDB.ExecuteNonQuery(Sql)
        End Sub

        Public Function GetFileLostPendingAlarm(ByVal ServerName As String, ByVal Severity As String, ByVal FileName As String) As DataTable
            Dim dt As New DataTable
            dt = ShopAccessDB.GetDataTable("select * from FileLostPendingAlarm where ServerName='" & ServerName & "' and Severity='" & Severity & "' and FileName='" & FileName & "'")
            Return dt
        End Function

        Public Sub DeleteFileLostPendingAlarm(ByVal ServerName As String, ByVal FileName As String)
            Dim sql As String = "delete from FileLostPendingAlarm where ServerName='" & ServerName & "' and FileName='" & FileName & "' "
            ShopAccessDB.ExecuteNonQuery(sql)
        End Sub
#End Region
    End Class
End Namespace

