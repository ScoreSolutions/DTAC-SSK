Imports System.IO
Imports System.Xml


Namespace Config
    Public Class RamConfigENG
        Inherits ConfigENG

        Dim _ShopName As String = ""
        Dim _ServerName As String = ""
        Dim _IPAddress As String = ""
        Dim _RepeateCheck As Int16 = 0
        Dim _ConfigType As String = ""
        Dim _AlarmType As String = ""
        Dim _SeverityMinor As AlarmSeverity
        Dim _SeverityMajor As AlarmSeverity
        Dim _SeverityCritical As AlarmSeverity
        Dim _IntervalMinute As Int16 = 0
        Dim _Active As String = ""
        Dim _CreateDate As String = ""
        Dim _ConfigList As New DataTable

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

        Public ReadOnly Property RepeateCheck() As Int16
            Get
                Return _RepeateCheck
            End Get
        End Property
        Public ReadOnly Property ConfigType() As String
            Get
                Return _ConfigType.Trim
            End Get
        End Property
        Public ReadOnly Property AlarmType() As String
            Get
                Return _AlarmType.Trim
            End Get
        End Property
        Public ReadOnly Property SeverityMinor() As AlarmSeverity
            Get
                Return _SeverityMinor
            End Get
        End Property
        Public ReadOnly Property SeverityMajor() As AlarmSeverity
            Get
                Return _SeverityMajor
            End Get
        End Property
        Public ReadOnly Property SeverityCritical() As AlarmSeverity
            Get
                Return _SeverityCritical
            End Get
        End Property
        Public ReadOnly Property IntervalMinute() As Int16
            Get
                Return _IntervalMinute
            End Get
        End Property
        Public ReadOnly Property Active() As String
            Get
                Return _Active.Trim
            End Get
        End Property
        Public ReadOnly Property CreateDate() As String
            Get
                Return _CreateDate.Trim
            End Get
        End Property
        Public ReadOnly Property ConfigList() As DataTable
            Get
                Return _ConfigList
            End Get
        End Property


        Public Sub GetConfigFromXML(ByVal RAMXMLFile As String)
            If File.Exists(RAMXMLFile) = True Then
                Dim xml As New XmlDocument
                xml.Load(RAMXMLFile)

                Dim RAMNode As Xml.XmlNodeList = xml.GetElementsByTagName("RAM")
                If RAMNode.Item(0).InnerXml <> "" Then
                    If RAMNode.Item(0).SelectSingleNode("ShopName") IsNot Nothing Then
                        _ShopName = RAMNode.Item(0).SelectSingleNode("ShopName").InnerText
                    End If
                    _ServerName = RAMNode.Item(0).SelectSingleNode("ServerName").InnerText
                    _IPAddress = RAMNode.Item(0).SelectSingleNode("IPAddress").InnerText
                    _RepeateCheck = RAMNode.Item(0).SelectSingleNode("RepeateCheck").InnerText
                    _ConfigType = RAMNode.Item(0).SelectSingleNode("ConfigType").InnerText
                    _AlarmType = RAMNode.Item(0).SelectSingleNode("AlarmType").InnerText

                    Dim AlarmSeverityNode As Xml.XmlNodeList = xml.GetElementsByTagName("AlarmSeverity")
                    For Each asNode As Xml.XmlNode In AlarmSeverityNode
                        If asNode.Item("Severity").InnerText.ToUpper = "MINOR" Then
                            _SeverityMinor.AlarmCode = asNode.Item("AlarmCode").InnerText
                            _SeverityMinor.Severity = asNode.Item("Severity").InnerText
                            _SeverityMinor.ValueOver = asNode.Item("ValueOver").InnerText
                            _SeverityMinor.AlarmMethod = asNode.Item("AlarmMethod").InnerText
                        ElseIf asNode.Item("Severity").InnerText.ToUpper = "MAJOR" Then
                            _SeverityMajor.AlarmCode = asNode.Item("AlarmCode").InnerText
                            _SeverityMajor.Severity = asNode.Item("Severity").InnerText
                            _SeverityMajor.ValueOver = asNode.Item("ValueOver").InnerText
                            _SeverityMajor.AlarmMethod = asNode.Item("AlarmMethod").InnerText
                        ElseIf asNode.Item("Severity").InnerText.ToUpper = "CRITICAL" Then
                            _SeverityCritical.AlarmCode = asNode.Item("AlarmCode").InnerText
                            _SeverityCritical.Severity = asNode.Item("Severity").InnerText
                            _SeverityCritical.ValueOver = asNode.Item("ValueOver").InnerText
                            _SeverityCritical.AlarmMethod = asNode.Item("AlarmMethod").InnerText
                        End If
                    Next
                    _IntervalMinute = RAMNode.Item(0).SelectSingleNode("IntervalMinute").InnerText
                    _Active = RAMNode.Item(0).SelectSingleNode("Active").InnerText
                    _CreateDate = RAMNode.Item(0).SelectSingleNode("CreateDate").InnerText


                    _ConfigList.Columns.Add("AlarmName")
                    _ConfigList.Columns.Add("HostIP")
                    _ConfigList.Columns.Add("HostName")
                    _ConfigList.Columns.Add("ConfigType")
                    _ConfigList.Columns.Add("AlarmType")
                    _ConfigList.Columns.Add("sysLocation")
                    _ConfigList.Columns.Add("Type")
                    _ConfigList.Columns.Add("Status")
                    _ConfigList.Columns.Add("XMLFileName")
                    Dim dr As DataRow = _ConfigList.NewRow
                    dr("AlarmName") = "ALM_LOG_RAM_PROCESS"
                    dr("HostIP") = _IPAddress
                    dr("HostName") = _ServerName
                    dr("ConfigType") = _ConfigType
                    dr("AlarmType") = _AlarmType
                    dr("sysLocation") = _ServerName & "_RAM"
                    dr("Type") = "Hardware"
                    dr("Status") = IIf(_Active = "Y", "Active", "Inactive")
                    dr("XMLFileName") = RAMXMLFile
                    _ConfigList.Rows.Add(dr)
                End If
                xml = Nothing
            End If
        End Sub

        Public Function SaveRamConfig(ByVal RepeateCheck As Integer, ByVal MinorSeverity As AlarmSeverity, ByVal MajorSeverity As AlarmSeverity, ByVal CriticalSeverity As AlarmSeverity, ByVal IntervalMinute As Int16, ByVal Active As String, ByVal IsShopInstall As String) As XDocument
            Return SaveConfig(RepeateCheck, "RAM", MinorSeverity, MajorSeverity, CriticalSeverity, IntervalMinute, Active, IsShopInstall)
        End Function

        Public Function SaveRamConfigWeb(ByVal ShopName As String, ByVal ServerName As String, ByVal IPAddress As String, ByVal RepeateCheck As Integer, ByVal MinorSeverity As AlarmSeverity, ByVal MajorSeverity As AlarmSeverity, ByVal CriticalSeverity As AlarmSeverity, ByVal IntervalMinute As Int16, ByVal Active As String, ByVal IsShopInstall As String) As XDocument
            Return SaveConfigWeb(ShopName, ServerName, IPAddress, RepeateCheck, "RAM", MinorSeverity, MajorSeverity, CriticalSeverity, IntervalMinute, Active, IsShopInstall)
        End Function
    End Class
End Namespace

