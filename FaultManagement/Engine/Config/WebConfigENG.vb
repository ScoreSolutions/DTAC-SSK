Imports System.IO
Imports System.Xml

Namespace Config
    Public Class WebConfigENG
        Inherits ConfigENG

        Dim _ShopName As String = ""
        Dim _ServerName As String = ""
        Dim _IPAddress As String = ""
        Dim _AlarmType As String = ""
        Dim _AlarmMethod As String = ""
        Dim _Severity As String = ""
        Dim _IntervalMinute As Integer = 0
        Dim _RepeateCheck As Integer = 0
        Dim _CreateDate As String = ""
        Dim _ConfigWebList As New DataTable
        Dim _ConfigList As New DataTable

        Dim _AlamTimeFrom As String = ""
        Dim _AlamTimeTo As String = ""
        Dim _AllDayEvent As String = ""
        Dim _AlamDateList As New DataTable

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
        Public ReadOnly Property AlarmMethod() As String
            Get
                Return _AlarmMethod.Trim
            End Get
        End Property
        Public ReadOnly Property Severity() As String
            Get
                Return _Severity.Trim
            End Get
        End Property
        Public ReadOnly Property IntervalMinute() As String
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
        Public ReadOnly Property ConfigWebList() As DataTable
            Get
                Return _ConfigWebList
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

        Public Sub GetConfigFromXML(ByVal WebXMLFile As String)
            If File.Exists(WebXMLFile) = True Then
                Dim xml As New XmlDocument
                xml.Load(WebXMLFile)

                Dim ServiceNode As Xml.XmlNodeList = xml.GetElementsByTagName("WebApplication")
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


                    _ConfigWebList.Columns.Add("WebApplicationName")
                    _ConfigWebList.Columns.Add("AlarmCode")
                    _ConfigWebList.Columns.Add("ProcessName")
                    _ConfigWebList.Columns.Add("AlarmMethod")
                    _ConfigWebList.Columns.Add("Active")

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

                    _ServerName = ServiceNode.Item(0).SelectSingleNode("ServerName").InnerText
                    _IPAddress = ServiceNode.Item(0).SelectSingleNode("IPAddress").InnerText
                    _AlarmType = ServiceNode.Item(0).SelectSingleNode("AlarmType").InnerText
                    _Severity = ServiceNode.Item(0).SelectSingleNode("AlarmSeverity").InnerText
                    _AlarmMethod = ServiceNode.Item(0).SelectSingleNode("AlarmMethod").InnerText
                    _IntervalMinute = ServiceNode.Item(0).SelectSingleNode("IntervalMinute").InnerText
                    _RepeateCheck = ServiceNode.Item(0).SelectSingleNode("RepeateCheck").InnerText

                    Dim ServiceListNode As Xml.XmlNodeList = xml.GetElementsByTagName("WebList")
                    For Each sNode As Xml.XmlNode In ServiceListNode
                        Dim sDr As DataRow = _ConfigWebList.NewRow
                        sDr("WebApplicationName") = sNode.Item("WebApplicationName").InnerText
                        sDr("AlarmCode") = sNode.Item("AlarmCode").InnerText
                        sDr("ProcessName") = sNode.Item("ProcessName").InnerText
                        sDr("AlarmMethod") = _AlarmMethod
                        sDr("Active") = sNode.Item("Active").InnerText
                        _ConfigWebList.Rows.Add(sDr)

                        Dim dr As DataRow = _ConfigList.NewRow
                        dr("AlarmName") = "ALM_" & sNode.Item("WebApplicationName").InnerText & "_PROCESS_Down"
                        dr("HostIP") = _IPAddress
                        dr("HostName") = _ServerName
                        dr("ConfigType") = "WebApplication"
                        dr("AlarmType") = _AlarmType
                        dr("sysLocation") = _ServerName & "_" & sNode.Item("WebApplicationName").InnerText & "_Process"
                        dr("Type") = "Software"
                        dr("Status") = IIf(sNode.Item("Active").InnerText = "Y", "Active", "Inactive")
                        dr("XMLFileName") = WebXMLFile
                        _ConfigList.Rows.Add(dr)
                    Next
                End If
                xml = Nothing
            End If
        End Sub

        Public Function SaveWebConfig(ByVal stSWPara As SWPara, ByVal dtServiceList As DataTable, ByVal ConfigDetail As String, ByVal IsShopInstall As String) As XDocument
            Return SaveSWConfig(stSWPara, dtServiceList, ConfigDetail, IsShopInstall)
        End Function
    End Class
End Namespace


