Imports System.DirectoryServices
Imports Engine.ConnectDB
Namespace InfoClass
    Public Class WebInfo
        Inherits SoftwareInfo
        Public Sub CreateWebPendingAlarm(ByVal conf As Engine.Config.WebConfigENG, ByVal Severity As String, ByVal AlarmDesc As String, ByVal FlagClear As String, ByVal AlarmMethod As String, ByVal WebName As String)
            Dim Sql As String = "insert into WebPendingAlarm (ServerName,SysLocation,HostIP,"
            Sql += " AlarmType,AlarmName,Severity,AlarmDesc,FlagClear,AlarmMethod,CreateDate)"
            Sql += " values('" & conf.ServerName & "','" & conf.ServerName & "_" & WebName & "','" & conf.IPAddress & "',"
            Sql += " '" & conf.AlarmType & "','ALM_" & WebName & "_PROCESS_DOWN','" & Severity & "','" & AlarmDesc & "','" & FlagClear & "','" & AlarmMethod & "',Now())"

            ShopAccessDB.ExecuteNonQuery(Sql)
        End Sub

        Public Function GetWebPendingAlarm(ByVal ServerName As String, ByVal Severity As String) As DataTable
            Dim dt As New DataTable
            dt = ShopAccessDB.GetDataTable("select * from WebPendingAlarm where ServerName='" & ServerName & "' and Severity='" & Severity & "'")
            Return dt
        End Function

        Public Sub DeleteWebPendingAlarm(ByVal ServerName As String)
            Dim sql As String = "delete from WebPendingAlarm where ServerName='" & ServerName & "' "
            ShopAccessDB.ExecuteNonQuery(sql)
        End Sub
    End Class
End Namespace

