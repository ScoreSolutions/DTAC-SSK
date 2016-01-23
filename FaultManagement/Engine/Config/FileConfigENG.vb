Imports System.Data
Imports System.Xml
Imports System.IO

Namespace Config
    Public Class FileConfigENG
        Inherits ConfigENG

        Dim _ShopName As String = ""
        Dim _ServerName As String = ""
        Dim _IPAddress As String = ""
        Dim _RepeateCheck As Int16 = 0
        Dim _ConfigType As String = ""
        Dim _AlarmType As String = ""
        Dim _AlarmMethod As String = ""
        Dim _IntervalMinute As Int16 = 0
        Dim _CreateDate As String = ""
        Dim _ConfigFileList As New DataTable
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
        Public ReadOnly Property AlarmMethod() As String
            Get
                Return _AlarmMethod.Trim
            End Get
        End Property
        Public ReadOnly Property IntervalMinute() As Int16
            Get
                Return _IntervalMinute
            End Get
        End Property
        Public ReadOnly Property CreateDate() As String
            Get
                Return _CreateDate.Trim
            End Get
        End Property
        Public ReadOnly Property ConfigFileList() As DataTable
            Get
                Return _ConfigFileList
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

#Region "File Size Config"
        Public Sub GetFileSizeConfigFromXML(ByVal FileSizeXMLFile As String)
            If File.Exists(FileSizeXMLFile) = True Then
                Dim xml As New XmlDocument
                xml.Load(FileSizeXMLFile)

                Dim FileSizeNode As Xml.XmlNodeList = xml.GetElementsByTagName("FileSize")
                If FileSizeNode.Item(0).InnerXml <> "" Then
                    _ConfigFileList.Columns.Add("FileName")
                    _ConfigFileList.Columns.Add("FileSizeMinor", GetType(Double))
                    _ConfigFileList.Columns.Add("FileSizeMajor", GetType(Double))
                    _ConfigFileList.Columns.Add("FileSizeCritical", GetType(Double))
                    _ConfigFileList.Columns.Add("Active")

                    If FileSizeNode.Item(0).SelectSingleNode("ShopName") IsNot Nothing Then
                        _ShopName = FileSizeNode.Item(0).SelectSingleNode("ShopName").InnerText
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
                    _AlamTimeFrom = FileSizeNode.Item(0).SelectSingleNode("AlamTimeFrom").InnerText
                    _AlamTimeTo = FileSizeNode.Item(0).SelectSingleNode("AlamTimeTo").InnerText
                    _AllDayEvent = FileSizeNode.Item(0).SelectSingleNode("AllDayEvent").InnerText

                    _ServerName = FileSizeNode.Item(0).SelectSingleNode("ServerName").InnerText
                    _IPAddress = FileSizeNode.Item(0).SelectSingleNode("IPAddress").InnerText
                    _RepeateCheck = FileSizeNode.Item(0).SelectSingleNode("RepeateCheck").InnerText
                    _ConfigType = FileSizeNode.Item(0).SelectSingleNode("ConfigType").InnerText
                    _AlarmType = FileSizeNode.Item(0).SelectSingleNode("AlarmType").InnerText
                    _AlarmMethod = FileSizeNode.Item(0).SelectSingleNode("AlarmMethod").InnerText
                    _IntervalMinute = FileSizeNode.Item(0).SelectSingleNode("IntervalMinute").InnerText
                    _CreateDate = FileSizeNode.Item(0).SelectSingleNode("CreateDate").InnerText

                    Dim FileListNode As Xml.XmlNodeList = xml.GetElementsByTagName("FileList")
                    For Each asNode As Xml.XmlNode In FileListNode
                        Dim fDr As DataRow = _ConfigFileList.NewRow
                        fDr("FileName") = asNode.Item("FileName").InnerText
                        fDr("FileSizeMinor") = asNode.Item("FileSizeMinor").InnerText
                        fDr("FileSizeMajor") = asNode.Item("FileSizeMajor").InnerText
                        fDr("FileSizeCritical") = asNode.Item("FileSizeCritical").InnerText
                        fDr("Active") = asNode.Item("Active").InnerText
                        _ConfigFileList.Rows.Add(fDr)
                    Next

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
                    dr("AlarmName") = "ALM_DATAFILESIZE_PROCESS"
                    dr("HostIP") = _IPAddress
                    dr("HostName") = _ServerName
                    dr("ConfigType") = _ConfigType
                    dr("AlarmType") = _AlarmType
                    dr("sysLocation") = _ServerName & "_FILE_SIZE"
                    dr("Type") = "Software"
                    dr("Status") = "Active"
                    dr("XMLFileName") = FileSizeXMLFile
                    _ConfigList.Rows.Add(dr)
                End If
                xml = Nothing
            End If
        End Sub

        Public Function SaveFileSizeConfig(ByVal stSWPara As SWPara, ByVal FileList As DataTable, ByVal IsShopInstall As String) As XDocument
            Dim ret As New XDocument
            Dim ConfigNode As New XElement("Config")
            Dim ConfigDetailNode As New XElement("FileSize")

            If IsShopInstall = "Y" Then
                ConfigDetailNode.Add(New XElement("ShopName", stSWPara.ShopName))   'Get Datefrom Shop DB
            Else
                ConfigDetailNode.Add(New XElement("ShopName", ""))
            End If
            ConfigDetailNode.Add(New XElement("ServerName", stSWPara.ServerName))
            ConfigDetailNode.Add(New XElement("IPAddress", stSWPara.IPAddress))
            ConfigDetailNode.Add(New XElement("ConfigType", "FileSize"))
            ConfigDetailNode.Add(New XElement("AlarmType", stSWPara.AlarmType))
            ConfigDetailNode.Add(New XElement("RepeateCheck", stSWPara.RepeateCheck))
            ConfigDetailNode.Add(New XElement("AlarmMethod", stSWPara.AlarmMethod))
            ConfigDetailNode.Add(New XElement("IntervalMinute", stSWPara.IntervalMinute))
            ConfigDetailNode.Add(New XElement("CreateDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", New Globalization.CultureInfo("en-US"))))
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

            If FileList.Rows.Count > 0 Then
                For Each dr As DataRow In FileList.Rows
                    Dim FileListNode As New XElement("FileList")
                    FileListNode.Add(New XElement("FileName", dr("FileName")))
                    FileListNode.Add(New XElement("FileSizeMinor", dr("FileSizeMinor")))
                    FileListNode.Add(New XElement("FileSizeMajor", dr("FileSizeMajor")))
                    FileListNode.Add(New XElement("FileSizeCritical", dr("FileSizeCritical")))
                    FileListNode.Add(New XElement("Active", dr("Active")))
                    ConfigDetailNode.Add(FileListNode)
                Next
            End If
            ConfigNode.Add(ConfigDetailNode)
            ret.Add(ConfigNode)
            Return ret
        End Function

#End Region
        Public Sub GetFileLostConfigFromXML(ByVal FileLostXMLFile As String)
            If File.Exists(FileLostXMLFile) = True Then
                Dim xml As New XmlDocument
                xml.Load(FileLostXMLFile)

                Dim FileLostNode As Xml.XmlNodeList = xml.GetElementsByTagName("FileLost")
                If FileLostNode.Item(0).InnerXml <> "" Then
                    _ConfigFileList.Columns.Add("FileName")
                    _ConfigFileList.Columns.Add("Active")

                    If FileLostNode.Item(0).SelectSingleNode("ShopName") IsNot Nothing Then
                        _ShopName = FileLostNode.Item(0).SelectSingleNode("ShopName").InnerText
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
                    _AlamTimeFrom = FileLostNode.Item(0).SelectSingleNode("AlamTimeFrom").InnerText
                    _AlamTimeTo = FileLostNode.Item(0).SelectSingleNode("AlamTimeTo").InnerText
                    _AllDayEvent = FileLostNode.Item(0).SelectSingleNode("AllDayEvent").InnerText

                    _ServerName = FileLostNode.Item(0).SelectSingleNode("ServerName").InnerText
                    _IPAddress = FileLostNode.Item(0).SelectSingleNode("IPAddress").InnerText
                    _RepeateCheck = FileLostNode.Item(0).SelectSingleNode("RepeateCheck").InnerText
                    _ConfigType = FileLostNode.Item(0).SelectSingleNode("ConfigType").InnerText
                    _AlarmType = FileLostNode.Item(0).SelectSingleNode("AlarmType").InnerText
                    _IntervalMinute = FileLostNode.Item(0).SelectSingleNode("IntervalMinute").InnerText
                    _AlarmMethod = FileLostNode.Item(0).SelectSingleNode("AlarmMethod").InnerText
                    _CreateDate = FileLostNode.Item(0).SelectSingleNode("CreateDate").InnerText

                    Dim FileListNode As Xml.XmlNodeList = xml.GetElementsByTagName("FileList")
                    For Each asNode As Xml.XmlNode In FileListNode
                        Dim fDr As DataRow = _ConfigFileList.NewRow
                        fDr("FileName") = asNode.Item("FileName").InnerText
                        fDr("Active") = asNode.Item("Active").InnerText
                        _ConfigFileList.Rows.Add(fDr)
                    Next

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
                    dr("AlarmName") = "ALM_FILECONFIG_PROCESS"
                    dr("HostIP") = _IPAddress
                    dr("HostName") = _ServerName
                    dr("ConfigType") = _ConfigType
                    dr("AlarmType") = _AlarmType
                    dr("sysLocation") = _ServerName & "_FILECONFIG"
                    dr("Type") = "Software"
                    dr("Status") = "Active"
                    dr("XMLFileName") = FileLostXMLFile
                    _ConfigList.Rows.Add(dr)
                End If
                xml = Nothing
            End If
        End Sub


        Public Function SaveFileLostConfig(ByVal stSWPara As SWPara, ByVal FileList As DataTable, ByVal IsShopInstall As String) As XDocument
            Dim ret As New XDocument
            Dim ConfigNode As New XElement("Config")
            Dim ConfigDetailNode As New XElement("FileLost")

            If IsShopInstall = "Y" Then
                ConfigDetailNode.Add(New XElement("ShopName", stSWPara.ShopName))   'Get Datefrom Shop DB
            End If
            ConfigDetailNode.Add(New XElement("ServerName", stSWPara.ServerName))
            ConfigDetailNode.Add(New XElement("IPAddress", stSWPara.IPAddress))
            ConfigDetailNode.Add(New XElement("ConfigType", "FileConfigLost"))
            ConfigDetailNode.Add(New XElement("AlarmType", stSWPara.AlarmType))
            ConfigDetailNode.Add(New XElement("RepeateCheck", stSWPara.RepeateCheck))
            ConfigDetailNode.Add(New XElement("AlarmMethod", stSWPara.AlarmMethod))
            ConfigDetailNode.Add(New XElement("IntervalMinute", stSWPara.IntervalMinute))
            ConfigDetailNode.Add(New XElement("CreateDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", New Globalization.CultureInfo("en-US"))))
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
            If FileList.Rows.Count > 0 Then
                For Each dr As DataRow In FileList.Rows
                    Dim FileListNode As New XElement("FileList")
                    FileListNode.Add(New XElement("FileName", dr("FileName")))
                    FileListNode.Add(New XElement("Active", dr("Active")))
                    ConfigDetailNode.Add(FileListNode)
                Next
            End If
            ConfigNode.Add(ConfigDetailNode)
            ret.Add(ConfigNode)
            Return ret
        End Function

    End Class
End Namespace

