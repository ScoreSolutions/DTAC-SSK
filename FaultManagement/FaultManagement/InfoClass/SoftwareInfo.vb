Imports Engine.ConnectDB
Namespace InfoClass
    Public Class SoftwareInfo
#Region "Send Alarm Information"
        Public Function CreateAlarmWaitingClear(ByVal ServerName As String, ByVal IPAddress As String, ByVal Severity As String, ByVal AlarmValue As Double, ByVal AlarmMethod As String, ByVal AlarmActivity As String, ByVal AlarmDesc As String, ByVal AlarmCode As String, ByVal SysLocation As String) As Integer
            Dim ret As Long = 0
            Dim sql As String = "insert into AlarmWaitingClear(ServerName,HostIP,AlarmActivity,"
            sql += " Severity,AlarmValue,AlarmMethod,FlagAlarm,CreateDate,UpdateDate,AlarmQty,SpecificProblem,"
            sql += " AlarmCode, SysLocation)"
            sql += " values('" & ServerName & "','" & IPAddress & "','" & AlarmActivity & "',"
            sql += " '" & Severity & "','" & AlarmValue & "','" & AlarmMethod & "','Alarm',Now(),Now(),1,'" & AlarmDesc & "',"
            sql += " '" & AlarmCode & "','" & SysLocation & "')"
            ret = ShopAccessDB.ExecuteNonQuery(sql)
            If ret > 0 Then
                Dim sMx As String = "select max(id) as id from AlarmWaitingClear"
                Dim dt As DataTable = ShopAccessDB.GetDataTable(sMx)
                If dt.Rows.Count > 0 Then
                    ret = Convert.ToInt64(dt.Rows(0)("id"))
                Else
                    ret = 1
                End If

                InsertAlarmLog(ServerName, IPAddress, AlarmActivity, Severity, AlarmValue, AlarmMethod, "Alarm", AlarmDesc, AlarmDesc, ret)

                Try
                    Dim ini As New FaultManagement.Org.Mentalis.Files.IniReader(Application.StartupPath & "\Config.ini")
                    ini.Section = "Setting"
                    Dim ws As New FaultManagementService.FaultManagementService
                    ws.Timeout = 20000
                    ws.Url = ini.ReadString("DCWebserviceURL")

                    sql = "insert into TB_ALARM_WAITING_CLEAR(ServerName,HostIP,AlarmActivity, "
                    sql += " Severity,AlarmValue,AlarmMethod,FlagAlarm,CreateDate,UpdateDate,"
                    sql += " AlarmQty,SpecificProblem, AlarmCode, SysLocation) "
                    sql += " values('" & ServerName & "','" & IPAddress & "','" & AlarmActivity & "',"
                    sql += " '" & Severity & "','" & AlarmValue & "','" & AlarmMethod & "','Alarm',getdate(),getdate(),1,'" & AlarmDesc & "',"
                    sql += " '" & AlarmCode & "','" & SysLocation & "')"
                    Dim RefDC As Long = ws.InserAlarmWaitingClear(sql)
                    If RefDC > 0 Then
                        ShopAccessDB.ExecuteNonQuery("update AlarmWaitingClear set RefDCAlarmWaitingClearID=" & RefDC & " where id=" & ret)
                    End If
                    ini = Nothing
                    ws = Nothing
                Catch ex As Exception
                    CreateLogFile(ex.Message & vbNewLine & ex.StackTrace)
                End Try
            End If
            Return ret
        End Function
        Public Shared Sub CreateLogFile(ByVal TextMsg As String)
            'Dim FILE_NAME As String = System.Windows.Forms.Application.StartupPath & "\" & DateTime.Now.ToString("yyyyyMMddHH") & ".sql"
            Dim FILE_NAME As String = System.Windows.Forms.Application.StartupPath & "\SoftwareInfo_" & DateTime.Now.ToString("yyyyMMddHH") & ".txt"
            Dim objWriter As New System.IO.StreamWriter(FILE_NAME, True)
            objWriter.WriteLine(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff") & " " & TextMsg & Chr(13) & Chr(13))
            objWriter.Close()
        End Sub

        Public Function UpdateAlarmWaitingClear(ByVal ServerName As String, ByVal IPAddress As String, ByVal Severity As String, ByVal AlarmValue As Double, ByVal AlarmMethod As String, ByVal AlarmActivity As String, ByVal AlarmDesc As String, ByVal AlarmWaitingClearID As Long) As Boolean
            Dim sql As String = "update AlarmWaitingClear set AlarmQty = AlarmQty + 1,UpdateDate=Now() where id=" & AlarmWaitingClearID
            Dim ret As Long = ShopAccessDB.ExecuteNonQuery(sql)
            If ret > 0 Then
                InsertAlarmLog(ServerName, IPAddress, AlarmActivity, Severity, AlarmValue, AlarmMethod, "Alarm", AlarmDesc, AlarmDesc, AlarmWaitingClearID)
                Try
                    Dim dt As DataTable = ShopAccessDB.GetDataTable("select RefDCAlarmWaitingClearID from AlarmWaitingClear where id=" & AlarmWaitingClearID)
                    If dt.Rows.Count > 0 Then
                        Dim ini As New FaultManagement.Org.Mentalis.Files.IniReader(Application.StartupPath & "\Config.ini")
                        ini.Section = "Setting"

                        Dim ws As New FaultManagementService.FaultManagementService
                        ws.Timeout = 20000
                        ws.Url = ini.ReadString("DCWebserviceURL")
                        ws.UpdateAlarmWaitingClear(dt.Rows(0)("RefDCAlarmWaitingClearID"))

                        ws = Nothing
                        ini = Nothing
                    End If
                    dt.Dispose()
                Catch ex As Exception

                End Try
            End If

            Return (ret > 0)
        End Function

        Public Function GetAlarmWaitingClear(ByVal ServerName As String, ByVal AlarmActivity As String, ByVal FlagAlarm As String) As DataTable
            Dim ret As New DataTable
            Dim sql As String = "select * from AlarmWaitingClear where ServerName='" & ServerName & "' and AlarmActivity='" & AlarmActivity & "' and FlagAlarm='" & FlagAlarm & "'"
            ret = ShopAccessDB.GetDataTable(sql)
            Return ret
        End Function

        Public Function SendAlarm(ByVal ServerName As String, ByVal SysLocation As String, ByVal HostIP As String, ByVal AlarmType As String, ByVal AlarmName As String, ByVal Severity As String, ByVal AlarmValue As String, ByVal AlarmDesc As String, ByVal AlarmMethod As String, ByVal AlarmActivity As String) As Boolean
            Dim ret As Boolean = False
            Try
                Dim ini As New Org.Mentalis.Files.IniReader(Application.StartupPath & "\Config.ini")
                ini.Section = "Setting"
                Dim ws As New FaultManagementService.FaultManagementService
                ws.Timeout = 2000
                ws.Url = ini.ReadString("DCWebserviceURL")
                Dim AlarmUrl() As String = Split(ws.GetAlarmURL, "###")

                Dim gw As New Engine.GeteWay.GateWayServiceENG
                If gw.SendToSNMP(SysLocation, HostIP, ServerName, AlarmType, AlarmName, Severity, AlarmValue, AlarmDesc, "0", AlarmMethod, AlarmUrl(0), AlarmUrl(1)) = True Then
                    ret = True
                End If
                gw = Nothing
                ws = Nothing
                ini = Nothing
            Catch ex As Exception

            End Try
            

            Return ret
        End Function

        Public Function SendClearAlarm(ByVal ServerName As String, ByVal SysLocation As String, ByVal HostIP As String, ByVal AlarmType As String, ByVal AlarmName As String, ByVal Severity As String, ByVal AlarmValue As String, ByVal AlarmDesc As String, ByVal AlarmMethod As String, ByVal AlarmActivity As String, ByVal SpecificProblem As String) As Boolean
            Dim ret As Boolean = False

            Dim dt As DataTable = ShopAccessDB.GetDataTable("select top 1 id,RefDCAlarmWaitingClearID from AlarmWaitingClear where ServerName='" & ServerName & "' and AlarmActivity='" & AlarmActivity & "' and Severity='" & Severity & "' and FlagAlarm='Alarm'")
            If dt.Rows.Count > 0 Then
                Dim sql As String = "update AlarmWaitingClear set FlagAlarm='Clear', ClearDate=Now() where id=" & dt.Rows(0)("id")
                If ShopAccessDB.ExecuteNonQuery(sql) > 0 Then
                    InsertAlarmLog(ServerName, HostIP, AlarmActivity, Severity, AlarmValue, AlarmMethod, "Clear", AlarmDesc, SpecificProblem, dt.Rows(0)("id"))
                    Try
                        Dim ini As New FaultManagement.Org.Mentalis.Files.IniReader(Application.StartupPath & "\Config.ini")
                        ini.Section = "Setting"

                        Dim ws As New FaultManagementService.FaultManagementService
                        ws.Timeout = 20000
                        ws.Url = ini.ReadString("DCWebserviceURL")

                        Dim IsClear As Boolean = False
                        If ws.SendClearAlarm(dt.Rows(0)("RefDCAlarmWaitingClearID")) = True Then
                            IsClear = True
                        End If
                        ws = Nothing
                        ini = Nothing
                    Catch ex As Exception

                    End Try
                    ret = True
                End If
            End If
            dt.Dispose()

            If ret = True Then
                Dim AlarmUrl() As String = {}
                Try
                    Dim ini As New Org.Mentalis.Files.IniReader(Application.StartupPath & "\Config.ini")
                    ini.Section = "Setting"
                    Dim ws As New FaultManagementService.FaultManagementService
                    ws.Timeout = 2000
                    ws.Url = ini.ReadString("DCWebserviceURL")
                    AlarmUrl = Split(ws.GetAlarmURL, "###")
                Catch ex As Exception

                End Try

                If AlarmUrl.Length = 2 Then
                    Dim gw As New Engine.GeteWay.GateWayServiceENG
                    gw.SendToSNMP(SysLocation, HostIP, ServerName, AlarmType, AlarmName, Severity, AlarmValue, AlarmDesc, "1", AlarmMethod, AlarmUrl(0), AlarmUrl(1))
                    gw = Nothing
                End If
            End If

            Return ret
        End Function

        Private Sub InsertAlarmLog(ByVal ServerName As String, ByVal HostIP As String, ByVal AlarmActivity As String, ByVal Severity As String, ByVal CurrentValue As String, ByVal AlarmMethod As String, ByVal FlagAlarm As String, ByVal AlarmDesc As String, ByVal SpecificPloblem As String, ByVal AlarmWaitingClearID As Integer)
            Dim sql As String = "insert into AlarmLog (CreateDate,ServerName,HostIP,AlarmActivity,Severity,"
            sql += " CurrentValue,AlarmMethod,FlagAlarm,AlarmDesc,SpecificPloblem,AlarmWaitingClearID)"
            sql += " values(Now(),'" & ServerName & "','" & HostIP & "','" & AlarmActivity & "','" & Severity & "',"
            sql += " '" & CurrentValue & "','" & AlarmMethod & "','" & FlagAlarm & "','" & AlarmDesc & "','" & SpecificPloblem & "','" & AlarmWaitingClearID & "')"

            ShopAccessDB.ExecuteNonQuery(sql)
        End Sub
#End Region
    End Class

End Namespace
