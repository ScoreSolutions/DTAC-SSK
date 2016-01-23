Imports System.Xml
Imports System.IO

Namespace Config
    Public Class ServiceConfigENG
        Inherits ConfigENG

        Dim _ShopName As String = ""
        Dim _ServerName As String = ""
        Dim _IPAddress As String = ""
        Dim _AlarmType As String = ""
        Dim _Severity As String = ""
        Dim _AlarmMethod As String = ""
        Dim _IntervalMinute As Integer = 0
        Dim _RepeateCheck As Integer = 0
        Dim _CreateDate As String = ""
        Dim _ConfigServiceList As New DataTable
        Dim _ConfigList As New DataTable

        Dim _AlamTimeFrom As String = ""
        Dim _AlamTimeTo As String = ""
        Dim _AllDayEvent As String = ""
        Dim _AlamDateList As New DataTable
        Dim _CheckAlarmWithTimeConfig As Boolean = False

        Public ReadOnly Property ShopName() As String
            Get
                Return _ShopName.Trim
            End Get
        End Property
        Public ReadOnly Property ServerName() As String
            Get
                Return _ServerName.Trim
            End Get
        End Property
        Public ReadOnly Property IPAddress() As String
            Get
                Return _IPAddress.Trim
            End Get
        End Property
        Public ReadOnly Property AlarmType() As String
            Get
                Return _AlarmType.Trim
            End Get
        End Property
        Public ReadOnly Property Severity() As String
            Get
                Return _Severity.Trim
            End Get
        End Property
        Public ReadOnly Property AlarmMethod() As String
            Get
                Return _AlarmMethod.Trim
            End Get
        End Property
        Public ReadOnly Property IntervalMinute() As Integer
            Get
                Return _IntervalMinute
            End Get
        End Property
        Public ReadOnly Property RepeateCheck() As Integer
            Get
                Return _RepeateCheck
            End Get
        End Property
        Public ReadOnly Property CreateDate() As String
            Get
                Return _CreateDate.Trim
            End Get
        End Property
        Public ReadOnly Property ConfigServiceList() As DataTable
            Get
                Return _ConfigServiceList
            End Get
        End Property
        Public ReadOnly Property ConfigList() As DataTable
            Get
                Return _ConfigList
            End Get
        End Property

        Public ReadOnly Property AlamTimeFrom() As String
            Get
                Return _AlamTimeFrom
            End Get
        End Property

        Public ReadOnly Property AlamTimeTo() As String
            Get
                Return _AlamTimeTo
            End Get
        End Property

        Public ReadOnly Property AllDayEvent() As String
            Get
                Return _AllDayEvent
            End Get
        End Property

        Public ReadOnly Property AlamDateList() As DataTable
            Get
                Return _AlamDateList
            End Get
        End Property

        Public Sub GetConfigFromXML(ByVal ServiceXMLFile As String)
            If File.Exists(ServiceXMLFile) = True Then
                Dim xml As New XmlDocument
                xml.Load(ServiceXMLFile)

                Dim ServiceNode As Xml.XmlNodeList = xml.GetElementsByTagName("Service")
                If ServiceNode.Item(0).InnerXml <> "" Then
                    _ConfigList.Columns.Add("AlarmName")
                    _ConfigList.Columns.Add("HostIP")
                    _ConfigList.Columns.Add("HostName")
                    _ConfigList.Columns.Add("ConfigType")
                    _ConfigList.Columns.Add("AlarmType")
                    _ConfigList.Columns.Add("sysLocation")
                    _ConfigList.Columns.Add("Type")
                    _ConfigList.Columns.Add("Status")
                    _ConfigList.Columns.Add("XMLFileName")

                    _ConfigServiceList.Columns.Add("ServiceName")
                    _ConfigServiceList.Columns.Add("AlarmCode")
                    _ConfigServiceList.Columns.Add("ProcessName")
                    _ConfigServiceList.Columns.Add("AlarmMethod")
                    _ConfigServiceList.Columns.Add("Active")

                    If ServiceNode.Item(0).SelectSingleNode("ShopName") IsNot Nothing Then
                        _ShopName = ServiceNode.Item(0).SelectSingleNode("ShopName").InnerText
                    End If
                    _AlamDateList.Columns.Add("Sunday")
                    _AlamDateList.Columns.Add("Monday")
                    _AlamDateList.Columns.Add("Tuesday")
                    _AlamDateList.Columns.Add("Wednesday")
                    _AlamDateList.Columns.Add("Thursday")
                    _AlamDateList.Columns.Add("Friday")
                    _AlamDateList.Columns.Add("Saturday")

                    _ServerName = ServiceNode.Item(0).SelectSingleNode("ServerName").InnerText
                    _IPAddress = ServiceNode.Item(0).SelectSingleNode("IPAddress").InnerText
                    _AlarmType = ServiceNode.Item(0).SelectSingleNode("AlarmType").InnerText
                    _Severity = ServiceNode.Item(0).SelectSingleNode("AlarmSeverity").InnerText
                    _AlarmMethod = ServiceNode.Item(0).SelectSingleNode("AlarmMethod").InnerText
                    _IntervalMinute = ServiceNode.Item(0).SelectSingleNode("IntervalMinute").InnerText
                    _RepeateCheck = ServiceNode.Item(0).SelectSingleNode("RepeateCheck").InnerText

                    Dim AlamDateListNode As Xml.XmlNodeList = xml.GetElementsByTagName("AlamDate")
                    For Each alNode As Xml.XmlNode In AlamDateListNode
                        Dim aDr As DataRow = _AlamDateList.NewRow
                        aDr("Sunday") = alNode.Item("Sunday").InnerText
                        aDr("Monday") = alNode.Item("Monday").InnerText
                        aDr("Tuesday") = alNode.Item("Tuesday").InnerText
                        aDr("Wednesday") = alNode.Item("Wednesday").InnerText
                        aDr("Thursday") = alNode.Item("Thursday").InnerText
                        aDr("Friday") = alNode.Item("Friday").InnerText
                        aDr("Saturday") = alNode.Item("Saturday").InnerText
                        _AlamDateList.Rows.Add(aDr)
                    Next

                    _AlamTimeFrom = ServiceNode.Item(0).SelectSingleNode("AlamTimeFrom").InnerText
                    _AlamTimeTo = ServiceNode.Item(0).SelectSingleNode("AlamTimeTo").InnerText
                    _AllDayEvent = ServiceNode.Item(0).SelectSingleNode("AllDayEvent").InnerText

                    Dim ServiceListNode As Xml.XmlNodeList = xml.GetElementsByTagName("ServiceList")
                    For Each sNode As Xml.XmlNode In ServiceListNode
                        Dim sDr As DataRow = _ConfigServiceList.NewRow
                        sDr("ServiceName") = sNode.Item("ServiceName").InnerText
                        sDr("AlarmCode") = sNode.Item("AlarmCode").InnerText
                        sDr("ProcessName") = sNode.Item("ProcessName").InnerText
                        sDr("AlarmMethod") = _AlarmMethod
                        sDr("Active") = sNode.Item("Active").InnerText
                        _ConfigServiceList.Rows.Add(sDr)

                        Dim dr As DataRow = _ConfigList.NewRow
                        dr("AlarmName") = "ALM_" & sNode.Item("ServiceName").InnerText & "_PROCESS_Down"
                        dr("HostIP") = _IPAddress
                        dr("HostName") = _ServerName
                        dr("ConfigType") = "Service"
                        dr("AlarmType") = _AlarmType
                        dr("sysLocation") = _ServerName & "_" & sNode.Item("ServiceName").InnerText & "_Process"
                        dr("Type") = "Software"
                        dr("Status") = IIf(sNode.Item("Active").InnerText = "Y", "Active", "Inactive")
                        dr("XMLFileName") = ServiceXMLFile
                        _ConfigList.Rows.Add(dr)
                    Next
                End If
                xml = Nothing
            End If
        End Sub

        Public Function SaveServiceConfig(ByVal stSWPara As SWPara, ByVal dtServiceList As DataTable, ByVal ConfigDetail As String, ByVal IsShopInstall As String) As XDocument
            Return SaveSWConfig(stSWPara, dtServiceList, ConfigDetail, IsShopInstall)
        End Function
    End Class
End Namespace

