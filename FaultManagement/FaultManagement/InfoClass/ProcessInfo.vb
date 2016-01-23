Imports Engine.ConnectDB
Namespace InfoClass
    Public Class ProcessInfo
        Inherits SoftwareInfo

        Public Sub CreateProcessPendingAlarm(ByVal conf As Engine.Config.ProcessConfigENG, ByVal Severity As String, ByVal AlarmDesc As String, ByVal FlagClear As String, ByVal AlarmMethod As String, ByVal ProcessName As String)
            Dim Sql As String = "insert into ProcessPendingAlarm (ServerName,SysLocation,HostIP,"
            Sql += " AlarmType,AlarmName,Severity,AlarmDesc,FlagClear,AlarmMethod,CreateDate)"
            Sql += " values('" & conf.ServerName & "','" & conf.ServerName & "_" & ProcessName & "','" & conf.IPAddress & "',"
            Sql += " '" & conf.AlarmType & "','ALM_" & ProcessName & "_PROCESS_DOWN','" & Severity & "','" & AlarmDesc & "','" & FlagClear & "','" & AlarmMethod & "',Now())"

            ShopAccessDB.ExecuteNonQuery(Sql)
        End Sub

        Public Function GetProcessPendingAlarm(ByVal ServerName As String, ByVal Severity As String) As DataTable
            Dim dt As New DataTable
            dt = ShopAccessDB.GetDataTable("select * from ProcessPendingAlarm where ServerName='" & ServerName & "' and Severity='" & Severity & "'")
            Return dt
        End Function

        Public Sub DeleteProcessPendingAlarm(ByVal ServerName As String)
            Dim sql As String = "delete from ProcessPendingAlarm where ServerName='" & ServerName & "' "
            ShopAccessDB.ExecuteNonQuery(sql)
        End Sub
    End Class

End Namespace

