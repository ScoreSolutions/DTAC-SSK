Imports System.Data
Imports CenLinqDB.Common.Utilities

Namespace ConnectDB
    Public Class AlarmDataENG
        Dim INIFile As String = System.Windows.Forms.Application.StartupPath & "\Config.ini"

        Public Function GetAlarmData(ByVal ServerName As String, _
                                     ByVal FlagAlarm As String, _
                                     ByVal DateFrom As DateTime, _
                                     ByVal DateTo As DateTime) As DataTable
            Dim dt As New DataTable
            Dim sql As String = "select * from TB_ALARM_WAITING_CLEAR where 1=1"
            If ServerName.Trim <> "" Then
                sql += " and ServerName like '%" & ServerName & "%'"
            End If
            If FlagAlarm.Trim <> "" Then
                sql += " and FlagAlarm  = '" & FlagAlarm & "'"
            End If

            If DateFrom.Year <> 1 Then
                sql += " and convert(varchar(8),CreateDate, 112) >= '" & DateFrom.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & "'"
            End If
            If DateTo.Year <> 1 Then
                sql += " and convert(varchar(8),CreateDate, 112) <= '" & DateTo.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & "'"
            End If
            sql += " order by UpdateDate desc"

            dt = QisFaultDB.GetDataTable(sql, INIFile)
            Return dt
        End Function

        Public Function GetAlarmData_More(ByVal ServerName As String, _
                                     ByVal FlagAlarm As String, _
                                     ByVal DateFrom As DateTime, _
                                     ByVal DateTo As DateTime, _
                                     ByVal AlamCode As String, _
                                     ByVal Description As String, _
                                     ByVal sysLocation As String, _
                                     ByVal Severity As String, _
                                     ByVal Type As String, ByVal INIFile As String) As DataTable
            Dim dt As New DataTable
            Dim sql As String = "select * from TB_ALARM_WAITING_CLEAR where 1=1"
            If ServerName.Trim <> "" Then
                sql += " and ServerName like '%" & ServerName & "%'"
            End If

            If FlagAlarm.Trim <> "" Then
                sql += " and FlagAlarm = '" & FlagAlarm & "'"
            End If
            If Type.Trim <> "" Then
                sql += " and AlarmActivity like '%" & Type & "%'"
            End If
            If AlamCode.Trim <> "" Then
                sql += " and AlarmCode like '%" & AlamCode & "%'"
            End If
            If sysLocation.Trim <> "" Then
                sql += " and sysLocation like '%" & sysLocation & "%'"
            End If
            If Description.Trim <> "" Then
                sql += " and SpecificProblem like '%" & Description & "%'"
            End If
            If Severity.Trim <> "" Then
                sql += " and Severity = '" & Severity & "'"
            End If
            If DateFrom.Year <> 1 Then
                sql += " and convert(varchar(8),CreateDate, 112) >= '" & DateFrom.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & "'"
            End If
            If DateTo.Year <> 1 Then
                sql += " and convert(varchar(8),CreateDate, 112) <= '" & DateTo.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US")) & "'"
            End If
            sql += " order by UpdateDate desc"

            dt = QisFaultDB.GetDataTable(sql, INIFile)
            Return dt
        End Function
    End Class
End Namespace

