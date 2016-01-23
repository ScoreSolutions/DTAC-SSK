Imports System.IO
Imports System.Xml
Imports System.Data

Namespace Config
    Public Class HDDConfigENG
        Inherits ConfigENG

        Dim _ShopName As String = ""
        Dim _ServerName As String = ""
        Dim _IPAddress As String = ""
        Dim _RepeateCheck As Int16 = 0
        Dim _ConfigType As String = ""
        Dim _AlarmType As String = ""
        Dim _SeverityAlarmDT As New DataTable
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
        Public ReadOnly Property SeverityAlarmDT() As DataTable
            Get
                Return _SeverityAlarmDT
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


        Public Sub GetConfigFromXML(ByVal HDDXMLFile As String)
            If File.Exists(HDDXMLFile) = True Then
                Dim xml As New XmlDocument
                xml.Load(HDDXMLFile)

                Dim HDDNode As Xml.XmlNodeList = xml.GetElementsByTagName("HDD")
                If HDDNode.Item(0).InnerXml <> "" Then
                    _SeverityAlarmDT.Columns.Add("DriveLetter")
                    _SeverityAlarmDT.Columns.Add("MinorSeverity", GetType(Engine.Config.ConfigENG.AlarmSeverity))
                    _SeverityAlarmDT.Columns.Add("MajorSeverity", GetType(Engine.Config.ConfigENG.AlarmSeverity))
                    _SeverityAlarmDT.Columns.Add("CriticalSeverity", GetType(Engine.Config.ConfigENG.AlarmSeverity))

                    If HDDNode.Item(0).SelectSingleNode("ShopName") IsNot Nothing Then
                        _ShopName = HDDNode.Item(0).SelectSingleNode("ShopName").InnerText
                    End If
                    _ServerName = HDDNode.Item(0).SelectSingleNode("ServerName").InnerText
                    _IPAddress = HDDNode.Item(0).SelectSingleNode("IPAddress").InnerText
                    _RepeateCheck = HDDNode.Item(0).SelectSingleNode("RepeateCheck").InnerText
                    _ConfigType = HDDNode.Item(0).SelectSingleNode("ConfigType").InnerText
                    _AlarmType = HDDNode.Item(0).SelectSingleNode("AlarmType").InnerText

                    Dim DriveInfoNode As Xml.XmlNodeList = xml.GetElementsByTagName("DriveInfo")
                    For Each DriveLetter As XmlElement In DriveInfoNode.Item(0).ChildNodes
                        Dim AlarmSeverityNode As Xml.XmlNodeList = DriveLetter.GetElementsByTagName("AlarmSeverity")

                        Dim TmpDr As DataRow = _SeverityAlarmDT.NewRow
                        TmpDr("DriveLetter") = DriveLetter.Name
                        For Each asNode As Xml.XmlNode In AlarmSeverityNode
                            If asNode.Item("Severity").InnerText.ToUpper = "MINOR" Then
                                Dim _SeverityMinor As ConfigENG.AlarmSeverity
                                _SeverityMinor.AlarmCode = asNode.Item("AlarmCode").InnerText
                                _SeverityMinor.Severity = asNode.Item("Severity").InnerText
                                _SeverityMinor.ValueOver = asNode.Item("ValueOver").InnerText
                                _SeverityMinor.AlarmMethod = asNode.Item("AlarmMethod").InnerText
                                TmpDr("MinorSeverity") = _SeverityMinor

                            ElseIf asNode.Item("Severity").InnerText.ToUpper = "MAJOR" Then
                                Dim _SeverityMajor As ConfigENG.AlarmSeverity
                                _SeverityMajor.AlarmCode = asNode.Item("AlarmCode").InnerText
                                _SeverityMajor.Severity = asNode.Item("Severity").InnerText
                                _SeverityMajor.ValueOver = asNode.Item("ValueOver").InnerText
                                _SeverityMajor.AlarmMethod = asNode.Item("AlarmMethod").InnerText
                                TmpDr("MajorSeverity") = _SeverityMajor

                            ElseIf asNode.Item("Severity").InnerText.ToUpper = "CRITICAL" Then
                                Dim _SeverityCritical As ConfigENG.AlarmSeverity
                                _SeverityCritical.AlarmCode = asNode.Item("AlarmCode").InnerText
                                _SeverityCritical.Severity = asNode.Item("Severity").InnerText
                                _SeverityCritical.ValueOver = asNode.Item("ValueOver").InnerText
                                _SeverityCritical.AlarmMethod = asNode.Item("AlarmMethod").InnerText
                                TmpDr("CriticalSeverity") = _SeverityCritical
                            End If
                        Next
                        _SeverityAlarmDT.Rows.Add(TmpDr)
                    Next
                    _IntervalMinute = HDDNode.Item(0).SelectSingleNode("IntervalMinute").InnerText
                    _Active = HDDNode.Item(0).SelectSingleNode("Active").InnerText
                    _CreateDate = HDDNode.Item(0).SelectSingleNode("CreateDate").InnerText


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
                    dr("AlarmName") = "ALM_LOG_HDD_PROCESS"
                    dr("HostIP") = _IPAddress
                    dr("HostName") = _ServerName
                    dr("ConfigType") = _ConfigType
                    dr("AlarmType") = _AlarmType
                    dr("sysLocation") = _ServerName & "_HDD"
                    dr("Type") = "Hardware"
                    dr("Status") = IIf(_Active = "Y", "Active", "Inactive")
                    dr("XMLFileName") = HDDXMLFile
                    _ConfigList.Rows.Add(dr)
                End If
                xml = Nothing
            End If
        End Sub

        Public Function SaveHDDConfig(ByVal dt As DataTable, ByVal RepeateCheck As Integer, ByVal IntervalMinute As Integer, ByVal Active As String, ByVal IsShopInstall As String) As XDocument
            Dim ret As New XDocument
            Dim ConfigNode As New XElement("Config")
            Dim ConfigDetailNode As New XElement("HDD")

            If IsShopInstall = "Y" Then
                ConfigDetailNode.Add(New XElement("ShopName", Engine.Common.FunctionEng.GetShopConfig("s_name_en")))   'Get Datefrom Shop DB
            End If
            ConfigDetailNode.Add(New XElement("ServerName", Environment.MachineName))
            ConfigDetailNode.Add(New XElement("IPAddress", Common.FunctionEng.GetIPAddress))
            ConfigDetailNode.Add(New XElement("RepeateCheck", RepeateCheck))
            ConfigDetailNode.Add(New XElement("ConfigType", "HardDisk"))
            ConfigDetailNode.Add(New XElement("AlarmType", "Fault"))

            Dim DriveInfo As New XElement("DriveInfo")
            For Each dr As DataRow In dt.Rows
                Dim DriveLetterNode As New XElement(dr("DriveLetter").ToString)
                Dim MinorSeverity As AlarmSeverity = DirectCast(dr("MinorSeverity"), AlarmSeverity)
                Dim MinorSeverityNode As New XElement("AlarmSeverity")
                MinorSeverityNode.Add(New XElement("AlarmCode", MinorSeverity.AlarmCode))
                MinorSeverityNode.Add(New XElement("Severity", MinorSeverity.Severity))
                MinorSeverityNode.Add(New XElement("ValueOver", MinorSeverity.ValueOver))
                MinorSeverityNode.Add(New XElement("AlarmMethod", MinorSeverity.AlarmMethod))
                DriveLetterNode.Add(MinorSeverityNode)

                Dim MajorSeverity As AlarmSeverity = DirectCast(dr("MajorSeverity"), AlarmSeverity)
                Dim MajorSeverityNode As New XElement("AlarmSeverity")
                MajorSeverityNode.Add(New XElement("AlarmCode", MajorSeverity.AlarmCode))
                MajorSeverityNode.Add(New XElement("Severity", MajorSeverity.Severity))
                MajorSeverityNode.Add(New XElement("ValueOver", MajorSeverity.ValueOver))
                MajorSeverityNode.Add(New XElement("AlarmMethod", MajorSeverity.AlarmMethod))
                DriveLetterNode.Add(MajorSeverityNode)

                Dim CriticalSeverity As AlarmSeverity = DirectCast(dr("CriticalSeverity"), AlarmSeverity)
                Dim CriticalSeverityNode As New XElement("AlarmSeverity")
                CriticalSeverityNode.Add(New XElement("AlarmCode", CriticalSeverity.AlarmCode))
                CriticalSeverityNode.Add(New XElement("Severity", CriticalSeverity.Severity))
                CriticalSeverityNode.Add(New XElement("ValueOver", CriticalSeverity.ValueOver))
                CriticalSeverityNode.Add(New XElement("AlarmMethod", CriticalSeverity.AlarmMethod))
                DriveLetterNode.Add(CriticalSeverityNode)

                DriveInfo.Add(DriveLetterNode)
            Next
            ConfigDetailNode.Add(DriveInfo)
            ConfigDetailNode.Add(New XElement("IntervalMinute", IntervalMinute))
            ConfigDetailNode.Add(New XElement("Active", Active))
            ConfigDetailNode.Add(New XElement("CreateDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", New Globalization.CultureInfo("en-US"))))

            ConfigNode.Add(ConfigDetailNode)
            ret.Add(ConfigNode)
            Return ret
        End Function

        Public Function SaveHDDConfigWeb(ByVal ShopName As String, ByVal ServerName As String, ByVal IPAddress As String, ByVal dt As DataTable, ByVal RepeateCheck As Integer, ByVal IntervalMinute As Integer, ByVal Active As String, ByVal IsShopInstall As String) As XDocument
            Dim ret As New XDocument
            Dim ConfigNode As New XElement("Config")
            Dim ConfigDetailNode As New XElement("HDD")

            If IsShopInstall = "Y" Then
                ConfigDetailNode.Add(New XElement("ShopName", ShopName))
            End If
            ConfigDetailNode.Add(New XElement("ServerName", ServerName))
            ConfigDetailNode.Add(New XElement("IPAddress", IPAddress))
            ConfigDetailNode.Add(New XElement("RepeateCheck", RepeateCheck))
            ConfigDetailNode.Add(New XElement("ConfigType", "HardDisk"))
            ConfigDetailNode.Add(New XElement("AlarmType", "Fault"))

            Dim DriveInfo As New XElement("DriveInfo")
            For Each dr As DataRow In dt.Rows
                Dim DriveLetterNode As New XElement(dr("DriveLetter").ToString)
                Dim MinorSeverity As AlarmSeverity = DirectCast(dr("MinorSeverity"), AlarmSeverity)
                Dim MinorSeverityNode As New XElement("AlarmSeverity")
                MinorSeverityNode.Add(New XElement("AlarmCode", MinorSeverity.AlarmCode))
                MinorSeverityNode.Add(New XElement("Severity", MinorSeverity.Severity))
                MinorSeverityNode.Add(New XElement("ValueOver", MinorSeverity.ValueOver))
                MinorSeverityNode.Add(New XElement("AlarmMethod", MinorSeverity.AlarmMethod))
                DriveLetterNode.Add(MinorSeverityNode)

                Dim MajorSeverity As AlarmSeverity = DirectCast(dr("MajorSeverity"), AlarmSeverity)
                Dim MajorSeverityNode As New XElement("AlarmSeverity")
                MajorSeverityNode.Add(New XElement("AlarmCode", MajorSeverity.AlarmCode))
                MajorSeverityNode.Add(New XElement("Severity", MajorSeverity.Severity))
                MajorSeverityNode.Add(New XElement("ValueOver", MajorSeverity.ValueOver))
                MajorSeverityNode.Add(New XElement("AlarmMethod", MajorSeverity.AlarmMethod))
                DriveLetterNode.Add(MajorSeverityNode)

                Dim CriticalSeverity As AlarmSeverity = DirectCast(dr("CriticalSeverity"), AlarmSeverity)
                Dim CriticalSeverityNode As New XElement("AlarmSeverity")
                CriticalSeverityNode.Add(New XElement("AlarmCode", CriticalSeverity.AlarmCode))
                CriticalSeverityNode.Add(New XElement("Severity", CriticalSeverity.Severity))
                CriticalSeverityNode.Add(New XElement("ValueOver", CriticalSeverity.ValueOver))
                CriticalSeverityNode.Add(New XElement("AlarmMethod", CriticalSeverity.AlarmMethod))
                DriveLetterNode.Add(CriticalSeverityNode)

                DriveInfo.Add(DriveLetterNode)
            Next
            ConfigDetailNode.Add(DriveInfo)
            ConfigDetailNode.Add(New XElement("IntervalMinute", IntervalMinute))
            ConfigDetailNode.Add(New XElement("Active", Active))
            ConfigDetailNode.Add(New XElement("CreateDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", New Globalization.CultureInfo("en-US"))))

            ConfigNode.Add(ConfigDetailNode)
            ret.Add(ConfigNode)
            Return ret
        End Function
    End Class
End Namespace

