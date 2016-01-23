Imports System.Data
Imports Engine.Config
Imports System.IO
Imports System


Partial Class frmSettingSW
    Inherits System.Web.UI.Page


#Region "Initail"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim xmlDoc As New Xml.XmlDocument
            xmlDoc.Load(Server.MapPath("CONFIG_CHECKLIST.xml"))

            PopulateService(xmlDoc)
            PopulateProcess(xmlDoc)
            PopulateWeb(xmlDoc)
            xmlDoc = Nothing
        End If

    End Sub

    Private Sub PopulateService(ByVal xmlDoc As System.Xml.XmlDocument)
        Dim dt As New DataTable
        With dt
            .Columns.Add(New DataColumn("Select", GetType(String)))
            .Columns.Add(New DataColumn("ServiceName", GetType(String)))
            .Columns.Add(New DataColumn("DisplayName", GetType(String)))
            .Columns.Add(New DataColumn("AlarmCode", GetType(String)))
            .Columns.Add(New DataColumn("ProcessName", GetType(String)))
            .Columns.Add(New DataColumn("Active", GetType(String)))
        End With

        Dim n As Xml.XmlNodeList = xmlDoc.GetElementsByTagName("WindowService")
        For Each d As Xml.XmlNode In n.Item(0)
            Dim dr As DataRow = dt.NewRow
            dr("ServiceName") = d.Item("ServiceName").InnerText
            dr("DisplayName") = d.Item("DisplayName").InnerText
            dr("AlarmCode") = d.Item("AlarmCode").InnerText
            dr("ProcessName") = "[ServerName]_" & d.Item("ProcessName").InnerText
            dr("Select") = d.Item("Select").InnerText
            dr("Active") = d.Item("Active").InnerText
            dt.Rows.Add(dr)
        Next

        ctlAramServitySW1.PopulateGrid(dt, "Service")

        'Dim strServerList As String() = {"IIS", "MSSQL", "MSQL Agent", "Filezilla"}
        'PopulateHeader("Service")

        'If CreateExistsDataTable(strServerList, "Service").Rows.Count = 0 Then
        '    Dim dtService As DataTable = CreateTable("Service")
        '    Dim drService As DataRow

        '    For i As Integer = 0 To strServerList.Length - 1
        '        drService = dtService.NewRow
        '        drService("ServiceName") = strServerList(i)
        '        drService("AlarmCode") = "" ' "ALM_" & strServerList(i) & "_Process_Down"
        '        drService("ProcessName") = "" '"[Hostname]_& strServerList(i) & "_Process"
        '        drService("Active") = "N"
        '        dtService.Rows.Add(drService)
        '    Next

        '    ctlAramServitySW1.PopulateGrid(dtService, "Service")
        'Else
        '    ctlAramServitySW1.PopulateGrid(CreateExistsDataTable(strServerList, "Service"), "Service")
        'End If


    End Sub

    Private Sub PopulateProcess(ByVal xmlDoc As System.Xml.XmlDocument)
        Dim dt As New DataTable
        With dt
            .Columns.Add(New DataColumn("Select", GetType(String)))
            .Columns.Add(New DataColumn("WindowProcessName", GetType(String)))
            .Columns.Add(New DataColumn("DisplayName", GetType(String)))
            .Columns.Add(New DataColumn("AlarmCode", GetType(String)))
            .Columns.Add(New DataColumn("ProcessName", GetType(String)))
            .Columns.Add(New DataColumn("Active", GetType(String)))
        End With

        Dim n As Xml.XmlNodeList = xmlDoc.GetElementsByTagName("WindowProcess")
        For Each d As Xml.XmlNode In n.Item(0)
            Dim dr As DataRow = dt.NewRow
            dr("WindowProcessName") = d.Item("WindowProcessName").InnerText
            dr("DisplayName") = d.Item("DisplayName").InnerText
            dr("AlarmCode") = d.Item("AlarmCode").InnerText
            dr("ProcessName") = "[ServerName]_" & d.Item("ProcessName").InnerText
            dr("Select") = d.Item("Select").InnerText
            dr("Active") = d.Item("Active").InnerText
            dt.Rows.Add(dr)
        Next
        ctlAramServitySW2.PopulateGrid(dt, "Process")

        'Dim strServerList As String() = {"DashBoard", "Send Agent", "SMS Notify", "Main Display", "Filezilla", "Unit Display"}

        'PopulateHeader("Process")

        'If CreateExistsDataTable(strServerList, "Process").Rows.Count = 0 Then
        '    Dim dtService As DataTable = CreateTable("Process")
        '    Dim drService As DataRow

        '    For i As Integer = 0 To strServerList.Length - 1
        '        drService = dtService.NewRow
        '        drService("WindowProcessName") = strServerList(i)
        '        drService("AlarmCode") = "" ' "ALM_" & strServerList(i) & "_Process_Down"
        '        drService("ProcessName") = "" '"[Hostname]_& strServerList(i) & "_Process"
        '        drService("Active") = "N"
        '        dtService.Rows.Add(drService)
        '    Next

        '    ctlAramServitySW2.PopulateGrid(dtService, "Process")
        'Else
        '    ctlAramServitySW2.PopulateGrid(CreateExistsDataTable(strServerList, "Process"), "Process")
        'End If


    End Sub

    Private Sub PopulateWeb(ByVal xmlDoc As System.Xml.XmlDocument)
        Dim dt As New DataTable
        With dt
            .Columns.Add(New DataColumn("Select", GetType(String)))
            .Columns.Add(New DataColumn("WebApplicationName", GetType(String)))
            .Columns.Add(New DataColumn("DisplayName", GetType(String)))
            .Columns.Add(New DataColumn("AlarmCode", GetType(String)))
            .Columns.Add(New DataColumn("ProcessName", GetType(String)))
            .Columns.Add(New DataColumn("Active", GetType(String)))
        End With

        Dim n As Xml.XmlNodeList = xmlDoc.GetElementsByTagName("WebApplication")
        For Each d As Xml.XmlNode In n.Item(0)
            Dim dr As DataRow = dt.NewRow
            dr("WebApplicationName") = d.Item("WebSiteName").InnerText
            dr("DisplayName") = d.Item("DisplayName").InnerText
            dr("AlarmCode") = d.Item("AlarmCode").InnerText
            dr("ProcessName") = "[ServerName]_" & d.Item("ProcessName").InnerText
            dr("Select") = d.Item("Select").InnerText
            dr("Active") = d.Item("Active").InnerText
            dt.Rows.Add(dr)
        Next

        ctlAramServitySW3.PopulateGrid(dt, "Web")

        'Dim strServerList As String() = {"ShopWebService", "ShopWebQM", "QisReport", "QisCSI", "QisQM", "QisWebAppointment", "QisWebConfiguration", "QisWebFalutManagement"}

        'PopulateHeader("Web")

        'If CreateExistsDataTable(strServerList, "Web").Rows.Count = 0 Then
        '    Dim dtService As DataTable = CreateTable("Web")
        '    Dim drService As DataRow

        '    For i As Integer = 0 To strServerList.Length - 1
        '        drService = dtService.NewRow
        '        drService("WebApplicationName") = strServerList(i)
        '        drService("AlarmCode") = "" ' "ALM_" & strServerList(i) & "_Process_Down"
        '        drService("ProcessName") = "" '"[Hostname]_& strServerList(i) & "_Process"
        '        drService("Active") = "N"
        '        dtService.Rows.Add(drService)
        '    Next

        '    ctlAramServitySW3.PopulateGrid(dtService, "Web")
        'Else
        '    ctlAramServitySW3.PopulateGrid(CreateExistsDataTable(strServerList, "Web"), "Web")
        'End If


    End Sub

    Private Sub PopulateHeader(ByVal SW_Para As String)
        Dim pathxml As String = Server.MapPath("~/FileSetting/" & "Config_" & SW_Para & "_Process.xml")
        Dim ds As New DataSet()
        Dim dvHeader As DataView
        Dim dvDetail As DataView
        Try
            If File.Exists(pathxml) = True Then
                ds.ReadXml(pathxml)
                dvHeader = ds.Tables(0).DefaultView
                If dvHeader.Count Then
                    Select Case SW_Para
                        Case "Service"
                            CType(ctlAramServitySW1.FindControl("txtShopName"), TextBox).Text = dvHeader(0)("ShopName")
                            CType(ctlAramServitySW1.FindControl("txtServerName"), TextBox).Text = dvHeader(0)("ServerName")
                            CType(ctlAramServitySW1.FindControl("txtIPAddress"), TextBox).Text = dvHeader(0)("IPAddress")
                            CType(ctlAramServitySW1.FindControl("rdoMethod"), RadioButtonList).SelectedValue = dvHeader(0)("AlarmMethod")
                            CType(ctlAramServitySW1.FindControl("txtIntervalMinute"), TextBox).Text = dvHeader(0)("IntervalMinute")

                        Case "Process"
                            CType(ctlAramServitySW2.FindControl("txtShopName"), TextBox).Text = dvHeader(0)("ShopName")
                            CType(ctlAramServitySW2.FindControl("txtServerName"), TextBox).Text = dvHeader(0)("ServerName")
                            CType(ctlAramServitySW2.FindControl("txtIPAddress"), TextBox).Text = dvHeader(0)("IPAddress")
                            CType(ctlAramServitySW2.FindControl("rdoMethod"), RadioButtonList).SelectedValue = dvHeader(0)("AlarmMethod")
                            CType(ctlAramServitySW2.FindControl("txtIntervalMinute"), TextBox).Text = dvHeader(0)("IntervalMinute")

                        Case "Web"
                            CType(ctlAramServitySW3.FindControl("txtShopName"), TextBox).Text = dvHeader(0)("ShopName")
                            CType(ctlAramServitySW3.FindControl("txtServerName"), TextBox).Text = dvHeader(0)("ServerName")
                            CType(ctlAramServitySW3.FindControl("txtIPAddress"), TextBox).Text = dvHeader(0)("IPAddress")
                            CType(ctlAramServitySW3.FindControl("rdoMethod"), RadioButtonList).SelectedValue = dvHeader(0)("AlarmMethod")
                            CType(ctlAramServitySW3.FindControl("txtIntervalMinute"), TextBox).Text = dvHeader(0)("IntervalMinute")

                    End Select
                End If
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Function CreateExistsDataTable(ByVal strServerList As String(), ByVal SW_Para As String) As DataTable
        Dim pathxml As String = Server.MapPath("~/FileSetting/" & "Config_" & SW_Para & "_Process.xml")
        Dim ds As New DataSet()
        Dim dtDetail As DataTable = CreateTable(SW_Para)
        Dim drDetail As DataRow
        Dim dvDetail As DataView
        Try
            If File.Exists(pathxml) = True Then
                ds.ReadXml(pathxml)
                dvDetail = ds.Tables(1).DefaultView
                For i As Integer = 0 To strServerList.Length - 1
                    Select Case SW_Para
                        Case "Service"
                            dvDetail.RowFilter = "ServiceName='" & strServerList(i) & "'"
                        Case "Process"
                            dvDetail.RowFilter = "WindowProcessName='" & strServerList(i) & "'"
                        Case "Web"
                            dvDetail.RowFilter = "WebApplicationName='" & strServerList(i) & "'"
                    End Select

                    If dvDetail.Count > 0 Then
                        drDetail = dtDetail.NewRow
                        Select Case SW_Para
                            Case "Service"
                                drDetail("ServiceName") = dvDetail(0)("ServiceName")
                            Case "Process"
                                drDetail("WindowProcessName") = dvDetail(0)("WindowProcessName")
                            Case "Web"
                                drDetail("WebApplicationName") = dvDetail(0)("WebApplicationName")
                        End Select



                        drDetail("AlarmCode") = dvDetail(0)("AlarmCode")
                        drDetail("ProcessName") = dvDetail(0)("ProcessName")
                        drDetail("Active") = dvDetail(0)("Active")
                        dtDetail.Rows.Add(drDetail)
                        'CType(ctlAramServitySW1.FindControl("txtShopName"), TextBox).Text = dvDetail(0)("ShopName")
                        'CType(ctlAramServitySW1.FindControl("txtServerName"), TextBox).Text = dvDetail(0)("ServerName")
                        'CType(ctlAramServitySW1.FindControl("txtIPAddress"), TextBox).Text = dvDetail(0)("IPAddress")
                        'CType(ctlAramServitySW1.FindControl("rdoMethod"), RadioButtonList).SelectedValue = dvDetail(0)("AlarmMethod")
                        'CType(ctlAramServitySW1.FindControl("txtIntervalMinute"), TextBox).Text = dvDetail(0)("IntervalMinute")
                    Else
                        drDetail = dtDetail.NewRow
                        Select Case SW_Para
                            Case "Service"
                                drDetail("ServiceName") = strServerList(i)
                            Case "Process"
                                drDetail("WindowProcessName") = strServerList(i)
                            Case "Web"
                                drDetail("WebApplicationName") = strServerList(i)
                        End Select
                        drDetail("AlarmCode") = ""
                        drDetail("ProcessName") = ""
                        drDetail("Active") = "N"
                        dtDetail.Rows.Add(drDetail)
                    End If
                Next



            End If

            Return dtDetail
        Catch ex As Exception
            Return CreateTable(SW_Para)
        End Try
    End Function
#End Region

#Region "Function"
    Private Function SaveService() As Boolean
        Try
            Dim strHeader As Engine.Config.ConfigENG.SWPara
            With strHeader
                .ShopName = CType(ctlAramServitySW1.FindControl("txtShopName"), TextBox).Text
                .ServerName = CType(ctlAramServitySW1.FindControl("txtServerName"), TextBox).Text
                .IPAddress = CType(ctlAramServitySW1.FindControl("txtIPAddress"), TextBox).Text
                .AlarmType = "Fault"
                .AlarmSeverity = "Critical"
                .AlarmMethod = CType(ctlAramServitySW1.FindControl("rdoMethod"), RadioButtonList).SelectedValue
                .IntervalMinute = CType(ctlAramServitySW1.FindControl("txtIntervalMinute"), TextBox).Text
                .SunDay = IIf(CType(ctlAramServitySW1.FindControl("chkDateList"), CheckBoxList).Items(0).Selected, "Y", "N")
                .Monday = IIf(CType(ctlAramServitySW1.FindControl("chkDateList"), CheckBoxList).Items(1).Selected, "Y", "N")
                .Tuesday = IIf(CType(ctlAramServitySW1.FindControl("chkDateList"), CheckBoxList).Items(2).Selected, "Y", "N")
                .Wednesday = IIf(CType(ctlAramServitySW1.FindControl("chkDateList"), CheckBoxList).Items(3).Selected, "Y", "N")
                .Thursday = IIf(CType(ctlAramServitySW1.FindControl("chkDateList"), CheckBoxList).Items(4).Selected, "Y", "N")
                .Friday = IIf(CType(ctlAramServitySW1.FindControl("chkDateList"), CheckBoxList).Items(5).Selected, "Y", "N")
                .Saturday = IIf(CType(ctlAramServitySW1.FindControl("chkDateList"), CheckBoxList).Items(6).Selected, "Y", "N")

                If CType(ctlAramServitySW1.FindControl("chkAllDayEvent"), CheckBox).Checked Then
                    .AlamTimeFrom = ""
                    .AlamTimeTo = ""
                Else
                    .AlamTimeFrom = CType(ctlAramServitySW1.FindControl("txtAlamTimeFrom"), TextBox).Text
                    .AlamTimeTo = CType(ctlAramServitySW1.FindControl("txtAlamTimeTo"), TextBox).Text
                End If
                .AllDayEvent = IIf(CType(ctlAramServitySW1.FindControl("chkAllDayEvent"), CheckBox).Checked, "Y", "N")
            End With


            Dim MyDGDetail As MycustomDG.MyDg = CType(ctlAramServitySW1.FindControl("Mydg1"), MycustomDG.MyDg)
            Dim DtData As DataTable = CreateTable("Service")
            Dim drData As DataRow

            For i As Integer = 0 To MyDGDetail.Items.Count - 1
                Dim ckbSelect As CheckBox = MyDGDetail.Items(i).FindControl("ckbSelect")
                If ckbSelect.Checked Then
                    Dim lbl_ServiceName As Label = MyDGDetail.Items(i).FindControl("lbl_ServiceName")
                    Dim txtAlamCode As Label = MyDGDetail.Items(i).FindControl("txtAlamCode")
                    Dim txtProcessName As Label = MyDGDetail.Items(i).FindControl("txtProcessName")
                    Dim ckbActive As CheckBox = MyDGDetail.Items(i).FindControl("ckbActive")

                    drData = DtData.NewRow
                    drData("ServiceName") = lbl_ServiceName.Text
                    drData("AlarmCode") = txtAlamCode.Text
                    drData("ProcessName") = txtProcessName.Text.Replace("[ServerName]", strHeader.ServerName)
                    drData("Active") = IIf(ckbActive.Checked = True, "Y", "N")
                    DtData.Rows.Add(drData)
                End If
            Next

            'Dim CPUFileNameSendToHost As String = 
            Dim CPUFileNameSendToWebServie As String = CType(ctlAramServitySW1.FindControl("txtServerName"), TextBox).Text.ToUpper & "_CONFIG_SERVICE_PROCESS.xml"

            If Directory.Exists(Server.MapPath("~/FileSetting/")) = False Then
                Directory.CreateDirectory(Server.MapPath("~/FileSetting/"))
            End If
            Dim clsServiceConfigENG As New ServiceConfigENG
            With clsServiceConfigENG
                .SaveServiceConfig(strHeader, DtData, "Service", "Y").Save(Server.MapPath("~/FileSetting/" & CPUFileNameSendToWebServie))
            End With
            clsServiceConfigENG = Nothing

            Dim bytes As Byte() = System.IO.File.ReadAllBytes(Server.MapPath("~/FileSetting/" & CPUFileNameSendToWebServie))
            Dim clsFaultManagementService As New FaultManagementService.FaultManagementService
            clsFaultManagementService.Url = Config.GetFaultManagementServiceURL
            If clsFaultManagementService.SendConfigFileToDC(bytes, CPUFileNameSendToWebServie, CType(ctlAramServitySW1.FindControl("txtServerName"), TextBox).Text) = True Then
                Try
                    File.Delete(Server.MapPath("~/FileSetting/" & CPUFileNameSendToWebServie))
                Catch ex As Exception

                End Try
            End If
            clsFaultManagementService.Dispose()
            bytes = Nothing


            Return True
        Catch ex As Exception
            Return False
        End Try


    End Function

    Private Function SaveProcess() As Boolean
        Try
            Dim strHeader As Engine.Config.ConfigENG.SWPara
            With strHeader
                .ShopName = CType(ctlAramServitySW2.FindControl("txtShopName"), TextBox).Text
                .ServerName = CType(ctlAramServitySW2.FindControl("txtServerName"), TextBox).Text
                .IPAddress = CType(ctlAramServitySW2.FindControl("txtIPAddress"), TextBox).Text
                .AlarmType = "Fault"
                .AlarmSeverity = "Critical"
                .AlarmMethod = CType(ctlAramServitySW2.FindControl("rdoMethod"), RadioButtonList).SelectedValue
                .IntervalMinute = CType(ctlAramServitySW2.FindControl("txtIntervalMinute"), TextBox).Text
                .AlarmMethod = CType(ctlAramServitySW2.FindControl("rdoMethod"), RadioButtonList).SelectedValue
                .IntervalMinute = CType(ctlAramServitySW2.FindControl("txtIntervalMinute"), TextBox).Text
                .SunDay = IIf(CType(ctlAramServitySW2.FindControl("chkDateList"), CheckBoxList).Items(0).Selected, "Y", "N")
                .Monday = IIf(CType(ctlAramServitySW2.FindControl("chkDateList"), CheckBoxList).Items(1).Selected, "Y", "N")
                .Tuesday = IIf(CType(ctlAramServitySW2.FindControl("chkDateList"), CheckBoxList).Items(2).Selected, "Y", "N")
                .Wednesday = IIf(CType(ctlAramServitySW2.FindControl("chkDateList"), CheckBoxList).Items(3).Selected, "Y", "N")
                .Thursday = IIf(CType(ctlAramServitySW2.FindControl("chkDateList"), CheckBoxList).Items(4).Selected, "Y", "N")
                .Friday = IIf(CType(ctlAramServitySW2.FindControl("chkDateList"), CheckBoxList).Items(5).Selected, "Y", "N")
                .Saturday = IIf(CType(ctlAramServitySW2.FindControl("chkDateList"), CheckBoxList).Items(6).Selected, "Y", "N")

                If CType(ctlAramServitySW2.FindControl("chkAllDayEvent"), CheckBox).Checked Then
                    .AlamTimeFrom = ""
                    .AlamTimeTo = ""
                Else
                    .AlamTimeFrom = CType(ctlAramServitySW2.FindControl("txtAlamTimeFrom"), TextBox).Text
                    .AlamTimeTo = CType(ctlAramServitySW2.FindControl("txtAlamTimeTo"), TextBox).Text
                End If
                .AllDayEvent = IIf(CType(ctlAramServitySW2.FindControl("chkAllDayEvent"), CheckBox).Checked, "Y", "N")
            End With


            Dim MyDGDetail As MycustomDG.MyDg = CType(ctlAramServitySW2.FindControl("Mydg1"), MycustomDG.MyDg)
            Dim DtData As DataTable = CreateTable("Process")
            Dim drData As DataRow

            For i As Integer = 0 To MyDGDetail.Items.Count - 1
                Dim ckbSelect As CheckBox = MyDGDetail.Items(i).FindControl("ckbSelect")
                If ckbSelect.Checked Then
                    Dim lbl_ServiceName As Label = MyDGDetail.Items(i).FindControl("lbl_ServiceName")
                    Dim txtAlamCode As Label = MyDGDetail.Items(i).FindControl("txtAlamCode")
                    Dim txtProcessName As Label = MyDGDetail.Items(i).FindControl("txtProcessName")
                    Dim ckbActive As CheckBox = MyDGDetail.Items(i).FindControl("ckbActive")

                    drData = DtData.NewRow
                    drData("WindowProcessName") = lbl_ServiceName.Text
                    drData("AlarmCode") = txtAlamCode.Text
                    drData("ProcessName") = txtProcessName.Text.Replace("[ServerName]", strHeader.ServerName)
                    drData("Active") = IIf(ckbActive.Checked = True, "Y", "N")
                    DtData.Rows.Add(drData)
                End If
            Next

            Dim CPUFileNameSendToWebServie As String = CType(ctlAramServitySW2.FindControl("txtServerName"), TextBox).Text.ToUpper & "_CONFIG_PROCESS_PROCESS.xml"
            'Dim CPUFileNameSendToHost As String = "Config_Process_Process.xml"

            If Directory.Exists(Server.MapPath("~/FileSetting/")) = False Then
                Directory.CreateDirectory(Server.MapPath("~/FileSetting/"))
            End If
            Dim clsProcessConfigENG As New ProcessConfigENG
            With clsProcessConfigENG
                .SaveProcessConfig(strHeader, DtData, "Process", "Y").Save(Server.MapPath("~/FileSetting/" & CPUFileNameSendToWebServie))
            End With
            clsProcessConfigENG = Nothing

            Dim bytes As Byte() = System.IO.File.ReadAllBytes(Server.MapPath("~/FileSetting/" & CPUFileNameSendToWebServie))
            Dim clsFaultManagementService As New FaultManagementService.FaultManagementService
            clsFaultManagementService.Url = Config.GetFaultManagementServiceURL
            If clsFaultManagementService.SendConfigFileToDC(bytes, CPUFileNameSendToWebServie, CType(ctlAramServitySW2.FindControl("txtServerName"), TextBox).Text) = True Then
                Try
                    File.Delete(Server.MapPath("~/FileSetting/" & CPUFileNameSendToWebServie))
                Catch ex As Exception

                End Try
            End If
            clsFaultManagementService.Dispose()

            Return True
        Catch ex As Exception
            Return False
        End Try


    End Function

    Private Function SaveWeb() As Boolean
        Try
            Dim strHeader As Engine.Config.ConfigENG.SWPara
            With strHeader
                .ShopName = CType(ctlAramServitySW3.FindControl("txtShopName"), TextBox).Text
                .ServerName = CType(ctlAramServitySW3.FindControl("txtServerName"), TextBox).Text
                .IPAddress = CType(ctlAramServitySW3.FindControl("txtIPAddress"), TextBox).Text
                .AlarmType = "Fault"
                .AlarmSeverity = "Critical"
                .AlarmMethod = CType(ctlAramServitySW3.FindControl("rdoMethod"), RadioButtonList).SelectedValue
                .IntervalMinute = CType(ctlAramServitySW3.FindControl("txtIntervalMinute"), TextBox).Text
                .AlarmMethod = CType(ctlAramServitySW3.FindControl("rdoMethod"), RadioButtonList).SelectedValue
                .IntervalMinute = CType(ctlAramServitySW3.FindControl("txtIntervalMinute"), TextBox).Text
                .SunDay = IIf(CType(ctlAramServitySW3.FindControl("chkDateList"), CheckBoxList).Items(0).Selected, "Y", "N")
                .Monday = IIf(CType(ctlAramServitySW3.FindControl("chkDateList"), CheckBoxList).Items(1).Selected, "Y", "N")
                .Tuesday = IIf(CType(ctlAramServitySW3.FindControl("chkDateList"), CheckBoxList).Items(2).Selected, "Y", "N")
                .Wednesday = IIf(CType(ctlAramServitySW3.FindControl("chkDateList"), CheckBoxList).Items(3).Selected, "Y", "N")
                .Thursday = IIf(CType(ctlAramServitySW3.FindControl("chkDateList"), CheckBoxList).Items(4).Selected, "Y", "N")
                .Friday = IIf(CType(ctlAramServitySW3.FindControl("chkDateList"), CheckBoxList).Items(5).Selected, "Y", "N")
                .Saturday = IIf(CType(ctlAramServitySW3.FindControl("chkDateList"), CheckBoxList).Items(6).Selected, "Y", "N")

                If CType(ctlAramServitySW3.FindControl("chkAllDayEvent"), CheckBox).Checked Then
                    .AlamTimeFrom = ""
                    .AlamTimeTo = ""
                Else
                    .AlamTimeFrom = CType(ctlAramServitySW3.FindControl("txtAlamTimeFrom"), TextBox).Text
                    .AlamTimeTo = CType(ctlAramServitySW3.FindControl("txtAlamTimeTo"), TextBox).Text
                End If
                .AllDayEvent = IIf(CType(ctlAramServitySW3.FindControl("chkAllDayEvent"), CheckBox).Checked, "Y", "N")
            End With


            Dim MyDGDetail As MycustomDG.MyDg = CType(ctlAramServitySW3.FindControl("Mydg1"), MycustomDG.MyDg)
            Dim DtData As DataTable = CreateTable("Web")
            Dim drData As DataRow

            For i As Integer = 0 To MyDGDetail.Items.Count - 1
                Dim ckbSelect As CheckBox = MyDGDetail.Items(i).FindControl("ckbSelect")
                If ckbSelect.Checked Then
                    Dim lbl_ServiceName As Label = MyDGDetail.Items(i).FindControl("lbl_ServiceName")
                    Dim txtAlamCode As Label = MyDGDetail.Items(i).FindControl("txtAlamCode")
                    Dim txtProcessName As Label = MyDGDetail.Items(i).FindControl("txtProcessName")
                    Dim ckbActive As CheckBox = MyDGDetail.Items(i).FindControl("ckbActive")

                    drData = DtData.NewRow
                    drData("WebApplicationName") = lbl_ServiceName.Text
                    drData("AlarmCode") = txtAlamCode.Text
                    drData("ProcessName") = txtProcessName.Text.Replace("[ServerName]", strHeader.ServerName)
                    drData("Active") = IIf(ckbActive.Checked = True, "Y", "N")
                    DtData.Rows.Add(drData)
                End If
            Next

            Dim CPUFileNameSendToWebServie As String = CType(ctlAramServitySW3.FindControl("txtServerName"), TextBox).Text.ToUpper & "_CONFIG_WEB_PROCESS.xml"

            If Directory.Exists(Server.MapPath("~/FileSetting/")) = False Then
                Directory.CreateDirectory(Server.MapPath("~/FileSetting/"))
            End If
            Dim clsWebConfigENG As New WebConfigENG
            With clsWebConfigENG
                .SaveWebConfig(strHeader, DtData, "WebApplication", "Y").Save(Server.MapPath("~/FileSetting/" & CPUFileNameSendToWebServie))
            End With
            clsWebConfigENG = Nothing

            Dim bytes As Byte() = System.IO.File.ReadAllBytes(Server.MapPath("~/FileSetting/" & CPUFileNameSendToWebServie))
            Dim clsFaultManagementService As New FaultManagementService.FaultManagementService
            clsFaultManagementService.Url = Config.GetFaultManagementServiceURL
            If clsFaultManagementService.SendConfigFileToDC(bytes, CPUFileNameSendToWebServie, CType(ctlAramServitySW3.FindControl("txtServerName"), TextBox).Text) = True Then
                Try
                    File.Delete(Server.MapPath("~/FileSetting/" & CPUFileNameSendToWebServie))
                Catch ex As Exception

                End Try
            End If
            clsFaultManagementService.Dispose()

            Return True
        Catch ex As Exception
            Return False
        End Try


    End Function

    Private Function CreateTable(ByVal SW_Para As String) As DataTable
        Dim DtData As New DataTable
        'DtData.Columns.Add("ServiceName", GetType(String))
        'DtData.Columns.Add("WindowProcessName", GetType(String))
        'DtData.Columns.Add("WebApplicationName", GetType(String))
        Select Case SW_Para
            Case "Service"
                DtData.Columns.Add("ServiceName", GetType(String))
            Case "Process"
                DtData.Columns.Add("WindowProcessName", GetType(String))
            Case "Web"
                DtData.Columns.Add("WebApplicationName", GetType(String))
        End Select
        DtData.Columns.Add("AlarmCode", GetType(String))
        DtData.Columns.Add("ProcessName", GetType(String))
        DtData.Columns.Add("Active", GetType(String))
        Return DtData
    End Function
#End Region

#Region "Event Handle"
    Protected Sub btnSaveService_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveService.Click
        Dim strAlert As String = ctlAramServitySW1.GetAlertInputData
        If strAlert <> "" Then
            Config.ShowAlert(strAlert, Me)
            Exit Sub
        End If

        If SaveService() Then
            Dim xmlDoc As New Xml.XmlDocument
            xmlDoc.Load(Server.MapPath("CONFIG_CHECKLIST.xml"))
            PopulateService(xmlDoc)
            Config.ShowAlert("Save Data Complete", Me)
            xmlDoc = Nothing
        Else
            Config.ShowAlert("Save Data Falied", Me)
        End If
    End Sub

    Protected Sub btnSaveProcess_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveProcess.Click
        Dim strAlert As String = ctlAramServitySW2.GetAlertInputData
        If strAlert <> "" Then
            Config.ShowAlert(strAlert, Me)
            Exit Sub
        End If

        If SaveProcess() Then
            Dim xmlDoc As New Xml.XmlDocument
            xmlDoc.Load(Server.MapPath("CONFIG_CHECKLIST.xml"))
            PopulateProcess(xmlDoc)
            xmlDoc = Nothing
            Config.ShowAlert("Save Data Complete", Me)
        Else
            Config.ShowAlert("Save Data Falied", Me)
        End If
    End Sub

    Protected Sub btnSaveWeb_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveWeb.Click
        Dim strAlert As String = ctlAramServitySW3.GetAlertInputData
        If strAlert <> "" Then
            Config.ShowAlert(strAlert, Me)
            Exit Sub
        End If

        If SaveWeb() Then
            Dim xmlDoc As New Xml.XmlDocument
            xmlDoc.Load(Server.MapPath("CONFIG_CHECKLIST.xml"))
            PopulateWeb(xmlDoc)
            xmlDoc = Nothing
            Config.ShowAlert("Save Data Complete", Me)
        Else
            Config.ShowAlert("Save Data Falied", Me)
        End If
    End Sub
#End Region



End Class
