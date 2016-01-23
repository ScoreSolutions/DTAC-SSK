'Imports Engine.ConnectDB
Imports CenLinqDB.Common.Utilities
Namespace Config
    Public Class PortConfigENG
        Inherits ConfigENG
        Dim _HostIP As String = ""
        Dim _HostName As String = ""
        Dim _Port As Long = 0

        Public Property HostIP() As String
            Get
                Return _HostIP.Trim
            End Get
            Set(ByVal value As String)
                _HostIP = value
            End Set
        End Property
        Public Property HostName() As String
            Get
                Return _HostName.Trim
            End Get
            Set(ByVal value As String)
                _HostName = value
            End Set
        End Property
        Public Property Port() As Long
            Get
                Return _Port
            End Get
            Set(ByVal value As Long)
                _Port = value
            End Set
        End Property

        Private Function CheckAlarmWithTimeConfig(ByVal dr As DataRow) As Boolean
            Dim ret As Boolean = False
            Dim vDateNow As DateTime = DateTime.Now
            Dim CaseDay As Integer = DatePart(DateInterval.Weekday, vDateNow)
            Select Case CaseDay
                Case 1
                    ret = (Convert.ToString(dr("AlarmSun")) = "Y")
                Case 2
                    ret = (Convert.ToString(dr("AlarmMon")) = "Y")
                Case 3
                    ret = (Convert.ToString(dr("AlarmTue")) = "Y")
                Case 4
                    ret = (Convert.ToString(dr("AlarmWed")) = "Y")
                Case 5
                    ret = (Convert.ToString(dr("AlarmThu")) = "Y")
                Case 6
                    ret = (Convert.ToString(dr("AlarmFri")) = "Y")
                Case 7
                    ret = (Convert.ToString(dr("AlarmSat")) = "Y")
            End Select

            If ret = True Then
                If Convert.ToString(dr("AllDayEvent")) = "N" Then
                    If Convert.ToString(dr("AlarmTimeFrom")) <> "" And Convert.ToString(dr("AlarmTimeTo")) <> "" Then
                        If Convert.ToString(dr("AlarmTimeFrom")) <= vDateNow.ToString("HH:mm") And vDateNow.ToString("HH:mm") <= Convert.ToString(dr("AlarmTimeTo")) Then
                            ret = True
                        Else
                            ret = False
                        End If
                    Else
                        ret = False
                    End If
                End If
            End If
            
            Return ret
        End Function
        
        Public Function GetConfigPortList(ByVal whText As String) As DataTable
            Dim sql As String = " select * from TB_CONFIG_PORT_LIST where 1=1 "
            Dim dt As New DataTable
            dt = QisFaultDB.GetDataTable(sql, INIFile)
            If dt.Rows.Count > 0 Then
                dt.Columns.Add("CheckAlarmWithTimeConfig", GetType(Boolean))
                For i As Integer = 0 To dt.Rows.Count - 1
                    dt.Rows(i)("CheckAlarmWithTimeConfig") = CheckAlarmWithTimeConfig(dt.Rows(i))
                Next
            End If

            dt.TableName = "GetConfigPortList"
            Return dt
        End Function
    End Class
End Namespace

