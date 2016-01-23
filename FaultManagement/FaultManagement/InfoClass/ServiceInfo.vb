Imports Engine.ConnectDB
Namespace InfoClass
    Public Class ServiceInfo
        Inherits SoftwareInfo

        Public Sub CreateServicePendingAlarm(ByVal conf As Engine.Config.ServiceConfigENG, ByVal Severity As String, ByVal AlarmDesc As String, ByVal FlagClear As String, ByVal AlarmMethod As String, ByVal ServiceName As String)
            Dim Sql As String = "insert into ServicePendingAlarm (ServerName,SysLocation,HostIP,"
            Sql += " AlarmType,AlarmName,Severity,AlarmDesc,FlagClear,AlarmMethod,CreateDate)"
            Sql += " values('" & conf.ServerName & "','" & conf.ServerName & "_" & ServiceName & "','" & conf.IPAddress & "',"
            Sql += " '" & conf.AlarmType & "','ALM_" & ServiceName & "_PROCESS_DOWN','" & Severity & "','" & AlarmDesc & "','" & FlagClear & "','" & AlarmMethod & "',Now())"

            ShopAccessDB.ExecuteNonQuery(Sql)
        End Sub

        Public Function GetServicePendingAlarm(ByVal ServerName As String, ByVal Severity As String) As DataTable
            Dim dt As New DataTable
            dt = ShopAccessDB.GetDataTable("select * from ServicePendingAlarm where ServerName='" & ServerName & "' and Severity='" & Severity & "'")
            Return dt
        End Function

        Public Sub DeleteServicePendingAlarm(ByVal ServerName As String)
            Dim sql As String = "delete from ServicePendingAlarm where ServerName='" & ServerName & "' "
            ShopAccessDB.ExecuteNonQuery(sql)
        End Sub
    End Class
End Namespace

