'Imports Engine.ConnectDB
Imports CenLinqDB.Common.Utilities

Namespace InfoClass
    Public Class HardwareInfo
        Public INIFile As String = Application.StartupPath & "\Config.ini"
        Public Function CreateAlarmWaitingClear(ByVal ServerName As String, ByVal IPAddress As String, ByVal Severity As String, ByVal AlarmValue As Double, ByVal AlarmMethod As String, ByVal AlarmActivity As String, ByVal AlarmDesc As String, ByVal SpecificProblem As String, ByVal AlarmCode As String, ByVal SysLocation As String) As Long
            Dim ret As Long = 0
            Dim sql As String = "insert into TB_ALARM_WAITING_CLEAR(ServerName,HostIP,AlarmActivity,"
            sql += " Severity,AlarmValue,AlarmMethod,FlagAlarm,CreateDate,UpdateDate,AlarmQty,SpecificProblem,"
            sql += " AlarmCode, SysLocation)"
            sql += " values('" & ServerName & "','" & IPAddress & "','" & AlarmActivity & "',"
            sql += " '" & Severity & "','" & AlarmValue & "','" & AlarmMethod & "','Alarm',getdate(),getdate(),1,'" & SpecificProblem & "',"
            sql += " '" & AlarmCode & "','" & SysLocation & "')"
            ret = QisFaultDB.ExecuteNonQuery(sql, INIFile)
            If ret > 0 Then
                ret = QisFaultDB.GetLastID("TB_ALARM_WAITING_CLEAR", INIFile)
            End If

            InsertAlarmLog(ServerName, IPAddress, AlarmActivity, Severity, AlarmValue, AlarmMethod, "Alarm", AlarmDesc, SpecificProblem, ret)

            Return ret
        End Function

        Public Function UpdateAlarmWaitingClear(ByVal ServerName As String, ByVal IPAddress As String, ByVal Severity As String, ByVal AlarmValue As Double, ByVal AlarmMethod As String, ByVal AlarmActivity As String, ByVal AlarmDesc As String, ByVal SpecificProblem As String, ByVal AlarmWaitingClearID As Long) As Boolean
            Dim sql As String = "update TB_ALARM_WAITING_CLEAR set AlarmQty = AlarmQty + 1,UpdateDate=getdate(),SpecificProblem='" & SpecificProblem & "' where id=" & AlarmWaitingClearID
            Dim ret As Long = QisFaultDB.ExecuteNonQuery(sql, INIFile)
            If ret > 0 Then
                InsertAlarmLog(ServerName, IPAddress, AlarmActivity, Severity, AlarmValue, AlarmMethod, "Alarm", AlarmDesc, SpecificProblem, AlarmWaitingClearID)
            End If

            Return (ret > 0)
        End Function

        Public Function GetAlarmWaitingClear(ByVal ServerName As String, ByVal AlarmActivity As String, ByVal FlagAlarm As String) As DataTable
            Dim ret As New DataTable
            Dim sql As String = "select * from TB_ALARM_WAITING_CLEAR where ServerName='" & ServerName & "' and AlarmActivity='" & AlarmActivity & "' and FlagAlarm='" & FlagAlarm & "'"
            ret = QisFaultDB.GetDataTable(sql, INIFile)
            Return ret
        End Function

        Public Function GetAllAlarmWaitingClear(ByVal AlarmActivity As String) As DataTable
            Dim ret As New DataTable
            Dim sql As String = "select * from TB_ALARM_WAITING_CLEAR where  AlarmActivity like '%" & AlarmActivity & "%' and FlagAlarm='Alarm'"
            ret = QisFaultDB.GetDataTable(sql, INIFile)
            Return ret
        End Function

        Public Function SendAlarm(ByVal ServerName As String, ByVal SysLocation As String, ByVal HostIP As String, ByVal AlarmType As String, ByVal AlarmName As String, ByVal Severity As String, ByVal AlarmValue As String, ByVal AlarmDesc As String, ByVal AlarmMethod As String, ByVal AlarmActivity As String) As Boolean
            Dim ret As Boolean = False
            Try

                Dim gw As New Engine.GeteWay.GateWayServiceENG
                If gw.SendToSNMP(SysLocation, HostIP, ServerName, AlarmType, AlarmName, Severity, AlarmValue, AlarmDesc, "0", AlarmMethod, Engine.Common.FunctionEng.GetQisDBConfig("SNMP_ALARM_URL1"), Engine.Common.FunctionEng.GetQisDBConfig("SNMP_ALARM_URL2")) = True Then
                    ret = True
                End If
                gw = Nothing

            Catch ex As Exception

            End Try
            

            Return ret
        End Function

        Public Function SendClearAlarm(ByVal ServerName As String, ByVal SysLocation As String, ByVal HostIP As String, ByVal AlarmType As String, ByVal AlarmName As String, ByVal Severity As String, ByVal AlarmValue As String, ByVal AlarmDesc As String, ByVal AlarmMethod As String, ByVal AlarmActivity As String, ByVal SpecificProblem As String) As Boolean
            Dim ret As Boolean = False

            Dim dt As DataTable = QisFaultDB.GetDataTable("select top 1 id from TB_ALARM_WAITING_CLEAR where ServerName='" & ServerName & "' and AlarmActivity='" & AlarmActivity & "' and Severity='" & Severity & "' and FlagAlarm='Alarm'", INIFile)
            If dt.Rows.Count > 0 Then
                Dim sql As String = "update TB_ALARM_WAITING_CLEAR set FlagAlarm='Clear', ClearDate=getdate() where id=" & dt.Rows(0)("id")
                If QisFaultDB.ExecuteNonQuery(sql, INIFile) > 0 Then
                    ret = True
                    InsertAlarmLog(ServerName, HostIP, AlarmActivity, Severity, AlarmValue, AlarmMethod, "Clear", AlarmDesc, SpecificProblem, dt.Rows(0)("id"))
                End If
            End If
            dt.Dispose()

            If ret = True Then
                Dim gw As New Engine.GeteWay.GateWayServiceENG
                gw.SendToSNMP(SysLocation, HostIP, ServerName, AlarmType, AlarmName, Severity, AlarmValue, AlarmDesc, "1", AlarmMethod, Engine.Common.FunctionEng.GetQisDBConfig("SNMP_ALARM_URL1"), Engine.Common.FunctionEng.GetQisDBConfig("SNMP_ALARM_URL2"))
                gw = Nothing
            End If

            Return ret
        End Function

        Private Sub InsertAlarmLog(ByVal ServerName As String, ByVal HostIP As String, ByVal AlarmActivity As String, ByVal Severity As String, ByVal CurrentValue As String, ByVal AlarmMethod As String, ByVal FlagAlarm As String, ByVal AlarmDesc As String, ByVal SpecificPloblem As String, ByVal AlarmWaitingClearID As Integer)
            Dim sql As String = "insert into TB_ALARM_LOG (CreateDate,ServerName,HostIP,AlarmActivity,Severity,"
            sql += " CurrentValue,AlarmMethod,FlagAlarm,AlarmDesc,SpecificProblem,AlarmWaitingClearID)"
            sql += " values(getdate(),'" & ServerName & "','" & HostIP & "','" & AlarmActivity & "','" & Severity & "',"
            sql += " '" & CurrentValue & "','" & AlarmMethod & "','" & FlagAlarm & "','" & AlarmDesc & "','" & SpecificPloblem & "','" & AlarmWaitingClearID & "')"

            QisFaultDB.ExecuteNonQuery(sql, INIFile)
        End Sub

        Public Sub CreatePendingAlarm(ByVal AlarmActivity As String, ByVal ServerName As String, ByVal IPAddress As String, ByVal Severity As String, ByVal AlarmValue As Double, ByVal AlarmDesc As String, ByVal FlagClear As String, ByVal AlarmMethod As String, ByVal AlarmType As String, ByVal AlarmName As String)
            Dim Sql As String = "insert into TB_ACTIVITY_PENDING_ALARM (AlarmActivity,ServerName,SysLocation,HostIP,"
            Sql += " AlarmType,AlarmName,Severity,AlarmValue,AlarmDesc,FlagClear,AlarmMethod,CreateDate)"
            Sql += " values('" & AlarmActivity & "','" & ServerName & "','" & ServerName & "_" & AlarmActivity & "','" & IPAddress & "',"
            Sql += " '" & AlarmType & "','" & AlarmName & "','" & Severity & "','" & AlarmValue & "','" & AlarmDesc & "','" & FlagClear & "','" & AlarmMethod & "',getdate())"

            QisFaultDB.ExecuteNonQuery(Sql, INIFile)
        End Sub

        Public Function GetPendingAlarm(ByVal AlarmActivity As String, ByVal ServerName As String, ByVal Severity As String) As DataTable
            Dim dt As New DataTable
            dt = QisFaultDB.GetDataTable("select * from TB_ACTIVITY_PENDING_ALARM where AlarmActivity='" & AlarmActivity & "' and ServerName='" & ServerName & "' and Severity='" & Severity & "'", INIFile)
            Return dt
        End Function

        Public Sub DeletePendingAlarm(ByVal AlarmActivity As String, ByVal ServerName As String)
            Dim sql As String = "delete from TB_ACTIVITY_PENDING_ALARM where ServerName='" & ServerName & "' and AlarmActivity='" & AlarmActivity & "' "
            QisFaultDB.ExecuteNonQuery(sql, INIFile)
        End Sub
    End Class
End Namespace

