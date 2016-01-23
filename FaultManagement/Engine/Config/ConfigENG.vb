Imports CenLinqDB.Common.Utilities

Namespace Config
    Public Class ConfigENG
        Public INIFile As String = System.Windows.Forms.Application.StartupPath & "\Config.ini"
        Public Structure AlarmSeverity
            Dim AlarmCode As String
            Dim Severity As String
            Dim ValueOver As Double
            Dim AlarmMethod As String
        End Structure

        Public Structure SWPara
            Dim ShopName As String
            Dim ServerName As String
            Dim IPAddress As String
            Dim AlarmType As String
            Dim AlarmSeverity As String
            Dim AlarmMethod As String
            Dim IntervalMinute As String
            Dim RepeateCheck As Integer
            Dim SunDay As String
            Dim Monday As String
            Dim Tuesday As String
            Dim Wednesday As String
            Dim Thursday As String
            Dim Friday As String
            Dim Saturday As String
            Dim AlamTimeFrom As String
            Dim AlamTimeTo As String
            Dim AllDayEvent As String
        End Structure

        Protected Function SaveConfig(ByVal RepeateCheck As Integer, ByVal ConfigDetail As String, ByVal MinorSeverity As AlarmSeverity, ByVal MajorSeverity As AlarmSeverity, ByVal CriticalSeverity As AlarmSeverity, ByVal IntervalMinute As Int16, ByVal Active As String, ByVal IsShopInstall As String) As XDocument
            Dim ret As New XDocument
            Dim ConfigNode As New XElement("Config")
            Dim ConfigDetailNode As New XElement(ConfigDetail)

            If IsShopInstall = "Y" Then
                ConfigDetailNode.Add(New XElement("ShopName", Engine.Common.FunctionEng.GetShopConfig("s_name_en")))   'Get Datefrom Shop DB
            Else
                ConfigDetailNode.Add(New XElement("ShopName", ""))
            End If
            ConfigDetailNode.Add(New XElement("ServerName", Environment.MachineName))
            ConfigDetailNode.Add(New XElement("IPAddress", Common.FunctionEng.GetIPAddress))
            ConfigDetailNode.Add(New XElement("RepeateCheck", RepeateCheck))
            ConfigDetailNode.Add(New XElement("ConfigType", ConfigDetail))
            ConfigDetailNode.Add(New XElement("AlarmType", "Fault"))

            Dim MinorSeverityNode As New XElement("AlarmSeverity")
            MinorSeverityNode.Add(New XElement("AlarmCode", MinorSeverity.AlarmCode))
            MinorSeverityNode.Add(New XElement("Severity", MinorSeverity.Severity))
            MinorSeverityNode.Add(New XElement("ValueOver", MinorSeverity.ValueOver))
            MinorSeverityNode.Add(New XElement("AlarmMethod", MinorSeverity.AlarmMethod))
            ConfigDetailNode.Add(MinorSeverityNode)

            Dim MajorSeverityNode As New XElement("AlarmSeverity")
            MajorSeverityNode.Add(New XElement("AlarmCode", MajorSeverity.AlarmCode))
            MajorSeverityNode.Add(New XElement("Severity", MajorSeverity.Severity))
            MajorSeverityNode.Add(New XElement("ValueOver", MajorSeverity.ValueOver))
            MajorSeverityNode.Add(New XElement("AlarmMethod", MajorSeverity.AlarmMethod))
            ConfigDetailNode.Add(MajorSeverityNode)

            Dim CriticalSeverityNode As New XElement("AlarmSeverity")
            CriticalSeverityNode.Add(New XElement("AlarmCode", CriticalSeverity.AlarmCode))
            CriticalSeverityNode.Add(New XElement("Severity", CriticalSeverity.Severity))
            CriticalSeverityNode.Add(New XElement("ValueOver", CriticalSeverity.ValueOver))
            CriticalSeverityNode.Add(New XElement("AlarmMethod", CriticalSeverity.AlarmMethod))
            ConfigDetailNode.Add(CriticalSeverityNode)

            ConfigDetailNode.Add(New XElement("IntervalMinute", IntervalMinute))
            ConfigDetailNode.Add(New XElement("Active", Active))
            ConfigDetailNode.Add(New XElement("CreateDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", New Globalization.CultureInfo("en-US"))))

            ConfigNode.Add(ConfigDetailNode)
            ret.Add(ConfigNode)
            Return ret
        End Function

        Protected Function SaveSWConfig(ByVal stSWPara As SWPara, ByVal dtList As DataTable, ByVal ConfigDetail As String, ByVal IsShopInstall As String) As XDocument
            Dim ret As New XDocument
            Dim ConfigNode As New XElement("Config")
            Dim ConfigDetailNode As New XElement(ConfigDetail)

            If IsShopInstall = "Y" Then
                ConfigDetailNode.Add(New XElement("ShopName", stSWPara.ShopName))
            Else
                ConfigDetailNode.Add(New XElement("ShopName", ""))
            End If
            ConfigDetailNode.Add(New XElement("ServerName", stSWPara.ServerName))
            ConfigDetailNode.Add(New XElement("IPAddress", stSWPara.IPAddress))
            ConfigDetailNode.Add(New XElement("AlarmType", stSWPara.AlarmType))
            ConfigDetailNode.Add(New XElement("AlarmSeverity", stSWPara.AlarmSeverity))
            ConfigDetailNode.Add(New XElement("AlarmMethod", stSWPara.AlarmMethod))
            ConfigDetailNode.Add(New XElement("ConfigType", ConfigDetail))
            ConfigDetailNode.Add(New XElement("IntervalMinute", stSWPara.IntervalMinute))
            ConfigDetailNode.Add(New XElement("RepeateCheck", stSWPara.RepeateCheck))
            Dim AlamDateNode As New XElement("AlamDate")
            AlamDateNode.Add(New XElement("Sunday", stSWPara.SunDay))
            AlamDateNode.Add(New XElement("Monday", stSWPara.Monday))
            AlamDateNode.Add(New XElement("Tuesday", stSWPara.Tuesday))
            AlamDateNode.Add(New XElement("Wednesday", stSWPara.Wednesday))
            AlamDateNode.Add(New XElement("Thursday", stSWPara.Thursday))
            AlamDateNode.Add(New XElement("Friday", stSWPara.Friday))
            AlamDateNode.Add(New XElement("Saturday", stSWPara.Saturday))
            ConfigDetailNode.Add(AlamDateNode)
            ConfigDetailNode.Add(New XElement("AlamTimeFrom", stSWPara.AlamTimeFrom))
            ConfigDetailNode.Add(New XElement("AlamTimeTo", stSWPara.AlamTimeTo))
            ConfigDetailNode.Add(New XElement("AllDayEvent", stSWPara.AllDayEvent))

            If dtList.Rows.Count > 0 Then
                Dim SeverityName1 As String = ""
                Dim SeverityName2 As String = ""
                Select Case ConfigDetail.ToUpper
                    Case "SERVICE"
                        SeverityName1 = "ServiceList"
                        SeverityName2 = "ServiceName"
                    Case "PROCESS"
                        SeverityName1 = "ProcessList"
                        SeverityName2 = "WindowProcessName"
                    Case "WEBAPPLICATION"
                        SeverityName1 = "WebList"
                        SeverityName2 = "WebApplicationName"
                End Select

                For i As Integer = 0 To dtList.Rows.Count - 1
                    Dim SeverityNode As New XElement(SeverityName1)
                    SeverityNode.Add(New XElement(SeverityName2, dtList.Rows(i).Item(SeverityName2)))
                    SeverityNode.Add(New XElement("AlarmCode", dtList.Rows(i).Item("AlarmCode")))
                    SeverityNode.Add(New XElement("ProcessName", dtList.Rows(i).Item("ProcessName")))
                    'SeverityNode.Add(New XElement("IntervalMinute", dtList.Rows(i).Item("IntervalMinute")))
                    SeverityNode.Add(New XElement("Active", dtList.Rows(i).Item("Active")))
                    ConfigDetailNode.Add(SeverityNode)
                Next
            End If

            ConfigNode.Add(ConfigDetailNode)
            ret.Add(ConfigNode)
            Return ret
        End Function

        Protected Function SaveConfigWeb(ByVal ShopName As String, ByVal ServerName As String, ByVal IPAddress As String, ByVal RepeateCheck As Integer, ByVal ConfigDetail As String, ByVal MinorSeverity As AlarmSeverity, ByVal MajorSeverity As AlarmSeverity, ByVal CriticalSeverity As AlarmSeverity, ByVal IntervalMinute As Int16, ByVal Active As String, ByVal IsShopInstall As String) As XDocument
            Dim ret As New XDocument
            Dim ConfigNode As New XElement("Config")
            Dim ConfigDetailNode As New XElement(ConfigDetail)

            If IsShopInstall = "Y" Then
                ConfigDetailNode.Add(New XElement("ShopName", ShopName))
            Else
                ConfigDetailNode.Add(New XElement("ShopName", ""))
            End If
            ConfigDetailNode.Add(New XElement("ServerName", ServerName))
            ConfigDetailNode.Add(New XElement("IPAddress", IPAddress))
            ConfigDetailNode.Add(New XElement("RepeateCheck", RepeateCheck))
            ConfigDetailNode.Add(New XElement("ConfigType", ConfigDetail))
            ConfigDetailNode.Add(New XElement("AlarmType", "Fault"))

            Dim MinorSeverityNode As New XElement("AlarmSeverity")
            MinorSeverityNode.Add(New XElement("AlarmCode", MinorSeverity.AlarmCode))
            MinorSeverityNode.Add(New XElement("Severity", MinorSeverity.Severity))
            MinorSeverityNode.Add(New XElement("ValueOver", MinorSeverity.ValueOver))
            MinorSeverityNode.Add(New XElement("AlarmMethod", MinorSeverity.AlarmMethod))
            ConfigDetailNode.Add(MinorSeverityNode)

            Dim MajorSeverityNode As New XElement("AlarmSeverity")
            MajorSeverityNode.Add(New XElement("AlarmCode", MajorSeverity.AlarmCode))
            MajorSeverityNode.Add(New XElement("Severity", MajorSeverity.Severity))
            MajorSeverityNode.Add(New XElement("ValueOver", MajorSeverity.ValueOver))
            MajorSeverityNode.Add(New XElement("AlarmMethod", MajorSeverity.AlarmMethod))
            ConfigDetailNode.Add(MajorSeverityNode)

            Dim CriticalSeverityNode As New XElement("AlarmSeverity")
            CriticalSeverityNode.Add(New XElement("AlarmCode", CriticalSeverity.AlarmCode))
            CriticalSeverityNode.Add(New XElement("Severity", CriticalSeverity.Severity))
            CriticalSeverityNode.Add(New XElement("ValueOver", CriticalSeverity.ValueOver))
            CriticalSeverityNode.Add(New XElement("AlarmMethod", CriticalSeverity.AlarmMethod))
            ConfigDetailNode.Add(CriticalSeverityNode)

            ConfigDetailNode.Add(New XElement("IntervalMinute", IntervalMinute))
            ConfigDetailNode.Add(New XElement("Active", Active))
            ConfigDetailNode.Add(New XElement("CreateDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", New Globalization.CultureInfo("en-US"))))

            ConfigNode.Add(ConfigDetailNode)
            ret.Add(ConfigNode)
            Return ret
        End Function

        Public Function CheckAlarmWithTimeConfig(ByVal AlarmDateList As DataTable, ByVal AllDayEvent As String, ByVal AlarmTimeFrom As String, ByVal AlarmTimeTo As String) As Boolean
            Dim ret As Boolean = False
            If AlarmDateList.Rows.Count = 1 Then
                Dim dr As DataRow = AlarmDateList.Rows(0)

                Dim vDateNow As DateTime = DateTime.Now
                Dim CaseDay As Integer = DatePart(DateInterval.Weekday, vDateNow)
                Select Case CaseDay
                    Case 1
                        ret = (Convert.ToString(dr("Sunday")) = "Y")
                    Case 2
                        ret = (Convert.ToString(dr("Monday")) = "Y")
                    Case 3
                        ret = (Convert.ToString(dr("Tuesday")) = "Y")
                    Case 4
                        ret = (Convert.ToString(dr("Wednesday")) = "Y")
                    Case 5
                        ret = (Convert.ToString(dr("Thursday")) = "Y")
                    Case 6
                        ret = (Convert.ToString(dr("Friday")) = "Y")
                    Case 7
                        ret = (Convert.ToString(dr("Saturday")) = "Y")
                End Select

                If ret = True Then
                    If AllDayEvent = "N" Then
                        If AlarmTimeFrom <> "" And AlarmTimeTo <> "" Then
                            If AlarmTimeFrom <= vDateNow.ToString("HH:mm") And vDateNow.ToString("HH:mm") <= AlarmTimeTo Then
                                ret = True
                            Else
                                ret = False
                            End If
                        Else
                            ret = False
                        End If
                    End If
                End If
            End If
            Return ret
        End Function

        Public Function GetIamAliveList() As DataTable
            Dim dt As New DataTable
            dt = QisFaultDB.GetDataTable("select * from TB_IAM_ALIVE where next_alive_time is not null", INIFile)
            Return dt
        End Function
    End Class
End Namespace

