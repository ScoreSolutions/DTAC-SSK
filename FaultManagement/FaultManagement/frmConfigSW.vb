Imports FaultManagement.Org.Mentalis.Files
Imports System.IO
Public Class frmConfigSW
    Dim dir As String = Application.StartupPath & "\Config\ "
    'Dim _DefaultData As String = ""

    'Public WriteOnly Property DefaultData() As String
    '    Set(ByVal value As String)
    '        _DefaultData = value
    '    End Set
    'End Property
    Private Sub frmConfigSW_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim _DefaultData As String = txtDefaultData.Text
        If Directory.Exists(dir) = False Then
            Directory.CreateDirectory(dir)

        End If

        SetHeader()

        Dim xmlDoc As New Xml.XmlDocument
        xmlDoc.Load(Application.StartupPath & "\CONFIG_CHECKLIST.xml")

        SetService(xmlDoc)
        SetProcessService(xmlDoc)
        SetWebService(xmlDoc)

        If _DefaultData.Trim <> "" Then
            SetServiceData()
            SetProcessData()
            SetWebData()
            SetFileSizeData()
            SetFileLostData()
            If _DefaultData = "Service" Then
                TabControl1.SelectedTab = Service
            ElseIf _DefaultData = "Process" Then
                TabControl1.SelectedTab = Process
            ElseIf _DefaultData = "WebApplication" Then
                TabControl1.SelectedTab = Web
            ElseIf _DefaultData = "FileSize" Then
                TabControl1.SelectedTab = FileSize
            ElseIf _DefaultData = "FileConfigLost" Then
                TabControl1.SelectedTab = FileLost
            End If
        End If
        SetTimeFormat(dpAlamTimeFrom_Service)
        SetTimeFormat(dpAlamTimeTo_Service)
        SetTimeFormat(dpAlamTimeFrom_Process)
        SetTimeFormat(dpAlamTimeTo_Process)
        SetTimeFormat(dpAlamTimeFrom_Web)
        SetTimeFormat(dpAlamTimeTo_Web)
        SetTimeFormat(dpAlamTimeFrom_File)
        SetTimeFormat(dpAlamTimeTo_File)
        SetTimeFormat(dpAlamTimeFrom_Filelost)
        SetTimeFormat(dpAlamTimeTo_FileLost)
    End Sub

    Sub SetTimeFormat(ByVal dp As DateTimePicker)
        dp.Format = DateTimePickerFormat.Custom
        dp.CustomFormat = "HH:mm"
    End Sub
    Private Sub SetFileLostData()
        Dim FileLostConfigFile As String = Application.StartupPath & "\Config\" & Environment.MachineName & "_CONFIG_DATAFILELOST.xml"
        Dim eng As New Engine.Config.FileConfigENG
        eng.GetFileLostConfigFromXML(FileLostConfigFile)
        If eng.AlarmMethod = "SNMP" Then
            rdSnmp_FileLost.Checked = True
        ElseIf eng.AlarmMethod = "SMS" Then
            rdSMS_FileLost.Checked = True
        ElseIf eng.AlarmMethod = "EMAIL" Then
            rdEmail_FileLost.Checked = True
        End If
        txtIntervalMinute_FileLost.Text = eng.IntervalMinute
        txtRepeateCheck_FileLost.Text = eng.RepeateCheck
        Dim dtDate As New DataTable
        dtDate = eng.AlamDateList
        If dtDate.Rows.Count > 0 Then
            chkSun_FileLost.Checked = IIf(dtDate.Rows(0).Item("Sunday") & "" = "Y", True, False)
            chkMon_FileLost.Checked = IIf(dtDate.Rows(0).Item("Monday") & "" = "Y", True, False)
            chkTue_FileLost.Checked = IIf(dtDate.Rows(0).Item("Tuesday") & "" = "Y", True, False)
            chkWed_FileLost.Checked = IIf(dtDate.Rows(0).Item("Wednesday") & "" = "Y", True, False)
            chkThurs_FileLost.Checked = IIf(dtDate.Rows(0).Item("Thursday") & "" = "Y", True, False)
            chkFri_FileLost.Checked = IIf(dtDate.Rows(0).Item("Friday") & "" = "Y", True, False)
            chkSat_FileLost.Checked = IIf(dtDate.Rows(0).Item("Saturday") & "" = "Y", True, False)
        End If

        chkAllDayEvent_FileLost.Checked = IIf(eng.AllDayEvent = "Y", True, False)
        If chkAllDayEvent_FileLost.Checked = False Then
            dpAlamTimeFrom_Filelost.Value = CDate(Now.Date & " " & eng.AlamTimeFrom)
            dpAlamTimeTo_FileLost.Value = CDate(Now.Date & " " & eng.AlamTimeTo)
        End If

        dtDate.Dispose()
        Dim dt As New DataTable
        dt = eng.ConfigFileList
        If dt.Rows.Count > 0 Then
            DGVFileLost.AutoGenerateColumns = False
            DGVFileLost.DataSource = dt
        End If
        dt.Dispose()
        eng = Nothing
    End Sub
    Private Sub SetFileSizeData()
        Dim FileSizeConfigFile As String = Application.StartupPath & "\Config\" & Environment.MachineName & "_CONFIG_DATAFILESIZE.xml"
        Dim eng As New Engine.Config.FileConfigENG
        eng.GetFileSizeConfigFromXML(FileSizeConfigFile)
        If eng.AlarmMethod = "SNMP" Then
            rdSnmp_File.Checked = True
        ElseIf eng.AlarmMethod = "SMS" Then
            rdSMS_File.Checked = True
        ElseIf eng.AlarmMethod = "EMAIL" Then
            rdEmail_File.Checked = True
        End If
        txtIntervalMinute_File.Text = eng.IntervalMinute
        txtRepeateCheck_File.Text = eng.RepeateCheck

        Dim dtDate As New DataTable
        dtDate = eng.AlamDateList
        If dtDate.Rows.Count > 0 Then
            chkSun_File.Checked = IIf(dtDate.Rows(0).Item("Sunday") & "" = "Y", True, False)
            chkMon_File.Checked = IIf(dtDate.Rows(0).Item("Monday") & "" = "Y", True, False)
            chkTue_File.Checked = IIf(dtDate.Rows(0).Item("Tuesday") & "" = "Y", True, False)
            chkWed_File.Checked = IIf(dtDate.Rows(0).Item("Wednesday") & "" = "Y", True, False)
            chkThurs_File.Checked = IIf(dtDate.Rows(0).Item("Thursday") & "" = "Y", True, False)
            chkFri_File.Checked = IIf(dtDate.Rows(0).Item("Friday") & "" = "Y", True, False)
            chkSat_File.Checked = IIf(dtDate.Rows(0).Item("Saturday") & "" = "Y", True, False)
        End If

        chkAllDayEvent_File.Checked = IIf(eng.AllDayEvent = "Y", True, False)
        If chkAllDayEvent_File.Checked = False Then
            dpAlamTimeFrom_File.Value = CDate(Now.Date & " " & eng.AlamTimeFrom)
            dpAlamTimeTo_File.Value = CDate(Now.Date & " " & eng.AlamTimeTo)
        End If

        dtDate.Dispose()

        Dim dt As New DataTable
        dt = eng.ConfigFileList
        If dt.Rows.Count > 0 Then
            DGVFile.AutoGenerateColumns = False
            DGVFile.DataSource = dt
        End If
        dt.Dispose()
        eng = Nothing
    End Sub
    Private Sub SetWebData()
        Dim WebConfigFile As String = Application.StartupPath & "\Config\" & Environment.MachineName & "_CONFIG_WEB_PROCESS.xml"
        Dim eng As New Engine.Config.WebConfigENG
        eng.GetConfigFromXML(WebConfigFile)
        If eng.AlarmMethod = "SNMP" Then
            rdSnmp_Web.Checked = True
        ElseIf eng.AlarmMethod = "SMS" Then
            rdSMS_Web.Checked = True
        ElseIf eng.AlarmMethod = "EMAIL" Then
            rdEmail_Web.Checked = True
        End If
        txtIntervalTime_Web.Text = eng.IntervalMinute
        txtRepeateCheck_Web.Text = eng.RepeateCheck

        Dim dtDate As New DataTable
        dtDate = eng.AlamDateList
        If dtDate.Rows.Count > 0 Then
            chkSun_Web.Checked = IIf(dtDate.Rows(0).Item("Sunday") & "" = "Y", True, False)
            chkMon_Web.Checked = IIf(dtDate.Rows(0).Item("Monday") & "" = "Y", True, False)
            chkTue_Web.Checked = IIf(dtDate.Rows(0).Item("Tuesday") & "" = "Y", True, False)
            chkWed_Web.Checked = IIf(dtDate.Rows(0).Item("Wednesday") & "" = "Y", True, False)
            chkThurs_Web.Checked = IIf(dtDate.Rows(0).Item("Thursday") & "" = "Y", True, False)
            chkFri_Web.Checked = IIf(dtDate.Rows(0).Item("Friday") & "" = "Y", True, False)
            chkSat_Web.Checked = IIf(dtDate.Rows(0).Item("Saturday") & "" = "Y", True, False)
        End If

        chkAllDayEvent_Web.Checked = IIf(eng.AllDayEvent = "Y", True, False)
        If chkAllDayEvent_Web.Checked = False Then
            dpAlamTimeFrom_Web.Value = CDate(Now.Date & " " & eng.AlamTimeFrom)
            dpAlamTimeTo_Web.Value = CDate(Now.Date & " " & eng.AlamTimeTo)
        End If

        dtDate.Dispose()

        Dim dt As New DataTable
        dt = eng.ConfigWebList
        If dt.Rows.Count > 0 Then
            For Each grv As DataGridViewRow In DGVWeb.Rows
                dt.DefaultView.RowFilter = "WebApplicationName='" & grv.Cells("WebApplicationName").Value & "'"
                If dt.DefaultView.Count > 0 Then
                    grv.Cells("WebSelect").Value = "Y"
                    grv.Cells("WebActive").Value = dt.DefaultView(0)("Active")
                End If
            Next
        End If
        dt.Dispose()
        eng = Nothing
    End Sub
    Private Sub SetProcessData()
        Dim ProcessConfigFile As String = Application.StartupPath & "\Config\" & Environment.MachineName & "_CONFIG_PROCESS_PROCESS.xml"
        Dim eng As New Engine.Config.ProcessConfigENG
        eng.GetConfigFromXML(ProcessConfigFile)
        If eng.AlarmMethod = "SNMP" Then
            rdSnmp_Process.Checked = True
        ElseIf eng.AlarmMethod = "SMS" Then
            rdSMS_Process.Checked = True
        ElseIf eng.AlarmMethod = "EMAIL" Then
            rdEmail_Process.Checked = True
        End If
        txtIntervalTime_Process.Text = eng.IntervalMinute
        txtRepeateCheck_Process.Text = eng.RepeateCheck
      
        Dim dtDate As New DataTable
        dtDate = eng.AlamDateList
        If dtDate.Rows.Count > 0 Then
            chkSun_Process.Checked = IIf(dtDate.Rows(0).Item("Sunday") & "" = "Y", True, False)
            chkMon_Process.Checked = IIf(dtDate.Rows(0).Item("Monday") & "" = "Y", True, False)
            chkTue_Process.Checked = IIf(dtDate.Rows(0).Item("Tuesday") & "" = "Y", True, False)
            chkWed_Process.Checked = IIf(dtDate.Rows(0).Item("Wednesday") & "" = "Y", True, False)
            chkThurs_Process.Checked = IIf(dtDate.Rows(0).Item("Thursday") & "" = "Y", True, False)
            chkFri_Process.Checked = IIf(dtDate.Rows(0).Item("Friday") & "" = "Y", True, False)
            chkSat_Process.Checked = IIf(dtDate.Rows(0).Item("Saturday") & "" = "Y", True, False)
        End If

        chkAllDayEvent_Process.Checked = IIf(eng.AllDayEvent = "Y", True, False)
        If chkAllDayEvent_Process.Checked = False Then
            dpAlamTimeFrom_Process.Value = CDate(Now.Date & " " & eng.AlamTimeFrom)
            dpAlamTimeTo_Process.Value = CDate(Now.Date & " " & eng.AlamTimeTo)
        End If

        dtDate.Dispose()
        Dim dt As New DataTable
        dt = eng.ConfigProcessList
        If dt.Rows.Count > 0 Then
            For Each grv As DataGridViewRow In DGVProcess.Rows
                dt.DefaultView.RowFilter = "WindowProcessName='" & grv.Cells("WindowsProcessName").Value & "'"
                If dt.DefaultView.Count > 0 Then
                    grv.Cells("ProcessSelect").Value = "Y"
                    grv.Cells("ProcessActive").Value = dt.DefaultView(0)("Active")
                End If
            Next
        End If
        dt.Dispose()
        eng = Nothing
    End Sub
    Private Sub SetServiceData()
        Try
            Dim ServiceConfigFile As String = Application.StartupPath & "\Config\" & Environment.MachineName & "_CONFIG_SERVICE_PROCESS.xml"
            Dim eng As New Engine.Config.ServiceConfigENG
            eng.GetConfigFromXML(ServiceConfigFile)
            If eng.AlarmMethod = "SNMP" Then
                rdSnmp_Service.Checked = True
            ElseIf eng.AlarmMethod = "SMS" Then
                rdSMS_Service.Checked = True
            ElseIf eng.AlarmMethod = "EMAIL" Then
                rdEmail_Service.Checked = True
            End If
            txtIntervalTime_Service.Text = eng.IntervalMinute
            txtRepeateCheck_Service.Text = eng.RepeateCheck

            Dim dtDate As New DataTable
            dtDate = eng.AlamDateList
            If dtDate.Rows.Count > 0 Then
                chkSun_Service.Checked = IIf(dtDate.Rows(0).Item("Sunday") & "" = "Y", True, False)
                chkMon_Service.Checked = IIf(dtDate.Rows(0).Item("Monday") & "" = "Y", True, False)
                chkTue_Service.Checked = IIf(dtDate.Rows(0).Item("Tuesday") & "" = "Y", True, False)
                chkWen_Service.Checked = IIf(dtDate.Rows(0).Item("Wednesday") & "" = "Y", True, False)
                chkThurs_Service.Checked = IIf(dtDate.Rows(0).Item("Thursday") & "" = "Y", True, False)
                chkFri_Service.Checked = IIf(dtDate.Rows(0).Item("Friday") & "" = "Y", True, False)
                chkSat_Service.Checked = IIf(dtDate.Rows(0).Item("Saturday") & "" = "Y", True, False)
            End If

            chkAllDayEvent_Service.Checked = IIf(eng.AllDayEvent = "Y", True, False)
            If chkAllDayEvent_Service.Checked = False Then
                dpAlamTimeFrom_Service.Value = CDate(Now.Date & " " & eng.AlamTimeFrom)
                dpAlamTimeTo_Service.Value = CDate(Now.Date & " " & eng.AlamTimeTo)
            End If
            dtDate.Dispose()

            Dim dt As New DataTable
            dt = eng.ConfigServiceList
            If dt.Rows.Count > 0 Then
                For Each grv As DataGridViewRow In DGVService.Rows
                    dt.DefaultView.RowFilter = "ServiceName='" & grv.Cells("ServiceName").Value & "'"
                    If dt.DefaultView.Count > 0 Then
                        grv.Cells("chk").Value = "Y"
                        grv.Cells("Active").Value = dt.DefaultView(0)("Active")
                    End If
                Next
            End If
            dt.Dispose()
            eng = Nothing
        Catch ex As Exception
            MessageBox.Show("Get Data Error!! : " & ex.ToString())
        End Try
    End Sub

    Sub SetHeader()
        Dim ini As New IniReader(Application.StartupPath & "\Config.ini")
        ini.Section = "Setting"
        Dim IsShopInstall As String = ini.ReadString("IsShopInstall")
        If IsShopInstall = "Y" Then
            Dim ShopName As String = Engine.Common.FunctionEng.GetShopConfig("s_name_en")
            txtShopName_Service.Text = ShopName
            txtShopName_Process.Text = ShopName
            txtShopName_Web.Text = ShopName
            txtShopName_File.Text = ShopName
            txtShopName_FileLost.Text = ShopName
        Else
            txtShopName_Service.Text = ""
            txtShopName_Process.Text = ""
            txtShopName_Web.Text = ""
            txtShopName_File.Text = ""
            txtShopName_FileLost.Text = ""
        End If


        Dim ServerName As String = Environment.MachineName
        Dim IPAddress As String = Engine.Common.FunctionEng.GetIPAddress

        txtServerName_Service.Text = ServerName
        txtServerIP_Service.Text = IPAddress

        txtServerName_Process.Text = ServerName
        txtServerIP_Process.Text = IPAddress

        txtServerName_Web.Text = ServerName
        txtServerIP_Web.Text = IPAddress

        txtServerName_File.Text = ServerName
        txtServerIP_File.Text = IPAddress

        txtServerName_FileLost.Text = ServerName
        txtServerIP_FileLost.Text = IPAddress
    End Sub

    Sub SetService(ByVal nXml As Xml.XmlDocument)
        Dim dt As New DataTable
        With dt
            .Columns.Add(New DataColumn("Select", GetType(String)))
            .Columns.Add(New DataColumn("ServiceName", GetType(String)))
            .Columns.Add(New DataColumn("DisplayName", GetType(String)))
            .Columns.Add(New DataColumn("AlarmCode", GetType(String)))
            .Columns.Add(New DataColumn("ProcessName", GetType(String)))
            .Columns.Add(New DataColumn("Active", GetType(String)))
        End With

        Dim n As Xml.XmlNodeList = nXml.GetElementsByTagName("WindowService")
        For Each d As Xml.XmlNode In n.Item(0)
            Dim dr As DataRow = dt.NewRow
            dr("ServiceName") = d.Item("ServiceName").InnerText
            dr("DisplayName") = d.Item("DisplayName").InnerText
            dr("AlarmCode") = d.Item("AlarmCode").InnerText
            dr("ProcessName") = txtServerName_Service.Text & "_" & d.Item("ProcessName").InnerText
            dr("Select") = d.Item("Select").InnerText
            dr("Active") = d.Item("Active").InnerText
            dt.Rows.Add(dr)
        Next

        DGVService.AutoGenerateColumns = False
        DGVService.DataSource = dt
    End Sub

    Sub SetProcessService(ByVal nXml As Xml.XmlDocument)
        Dim dt As New DataTable
        With dt
            .Columns.Add(New DataColumn("Select", GetType(String)))
            .Columns.Add(New DataColumn("WindowProcessName", GetType(String)))
            .Columns.Add(New DataColumn("DisplayName", GetType(String)))
            .Columns.Add(New DataColumn("AlarmCode", GetType(String)))
            .Columns.Add(New DataColumn("ProcessName", GetType(String)))
            .Columns.Add(New DataColumn("Active", GetType(String)))
        End With

        Dim n As Xml.XmlNodeList = nXml.GetElementsByTagName("WindowProcess")
        For Each d As Xml.XmlNode In n.Item(0)
            Dim dr As DataRow = dt.NewRow
            dr("WindowProcessName") = d.Item("WindowProcessName").InnerText
            dr("DisplayName") = d.Item("DisplayName").InnerText
            dr("AlarmCode") = d.Item("AlarmCode").InnerText
            dr("ProcessName") = txtServerName_Service.Text & "_" & d.Item("ProcessName").InnerText
            dr("Select") = d.Item("Select").InnerText
            dr("Active") = d.Item("Active").InnerText
            dt.Rows.Add(dr)
        Next
        DGVProcess.AutoGenerateColumns = False
        DGVProcess.DataSource = dt
    End Sub

    Sub SetWebService(ByVal nXml As Xml.XmlDocument)
        Dim dt As New DataTable
        With dt
            .Columns.Add(New DataColumn("Select", GetType(String)))
            .Columns.Add(New DataColumn("WebApplicationName", GetType(String)))
            .Columns.Add(New DataColumn("DisplayName", GetType(String)))
            .Columns.Add(New DataColumn("AlarmCode", GetType(String)))
            .Columns.Add(New DataColumn("ProcessName", GetType(String)))
            .Columns.Add(New DataColumn("Active", GetType(String)))
        End With

        Dim n As Xml.XmlNodeList = nXml.GetElementsByTagName("WebApplication")
        For Each d As Xml.XmlNode In n.Item(0)
            Dim dr As DataRow = dt.NewRow
            dr("WebApplicationName") = d.Item("WebSiteName").InnerText
            dr("DisplayName") = d.Item("DisplayName").InnerText
            dr("AlarmCode") = d.Item("AlarmCode").InnerText
            dr("ProcessName") = txtServerName_Service.Text & "_" & d.Item("ProcessName").InnerText
            dr("Select") = d.Item("Select").InnerText
            dr("Active") = d.Item("Active").InnerText
            dt.Rows.Add(dr)
        Next

        DGVWeb.AutoGenerateColumns = False
        DGVWeb.DataSource = dt
    End Sub


    Private Sub btnSave_Service_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave_Service.Click
        Try
            If txtIntervalTime_Service.Text.Trim = "" Then
                MessageBox.Show("กรุณาระบุ Interval Time")
                Exit Sub
            End If
            If txtRepeateCheck_Service.Text.Trim = "" Then
                MessageBox.Show("กรุณาระบุ Repeate Check")
                Exit Sub
            End If
            If CInt(txtIntervalTime_Service.Text) = 0 Then
                MessageBox.Show("กรุณาระบุ Interval Time มากกว่า 0")
                Exit Sub
            End If
            If CInt(txtRepeateCheck_Service.Text) = 0 Then
                MessageBox.Show("กรุณาระบุ Repeate Check มากกว่า 0")
                Exit Sub
            End If

            Dim eng As New Engine.Config.ServiceConfigENG
            Dim stSWPara As New Engine.Config.ConfigENG.SWPara
            With stSWPara
                .ShopName = txtShopName_Service.Text
                .ServerName = txtServerName_Service.Text
                .IPAddress = txtServerIP_Service.Text
                .AlarmType = txtAlamType_Service.Text
                .AlarmSeverity = txtAlamSeverity_Service.Text
                .RepeateCheck = txtRepeateCheck_Service.Text
                .SunDay = IIf(chkSun_Service.Checked, "Y", "N")
                .Monday = IIf(chkMon_Service.Checked, "Y", "N")
                .Tuesday = IIf(chkTue_Service.Checked, "Y", "N")
                .Wednesday = IIf(chkWen_Service.Checked, "Y", "N")
                .Thursday = IIf(chkThurs_Service.Checked, "Y", "N")
                .Friday = IIf(chkFri_Service.Checked, "Y", "N")
                .Saturday = IIf(chkSat_Service.Checked, "Y", "N")

                If chkAllDayEvent_Service.Checked Then
                    .AlamTimeFrom = ""
                    .AlamTimeTo = ""
                Else
                    .AlamTimeFrom = dpAlamTimeFrom_Service.Value.ToString("HH:mm")
                    .AlamTimeTo = dpAlamTimeTo_Service.Value.ToString("HH:mm")
                End If
                .AllDayEvent = IIf(chkAllDayEvent_Service.Checked, "Y", "N")

                Dim AlarmMethod As String = ""
                If rdSnmp_Service.Checked Then
                    AlarmMethod = "SNMP"
                End If
                If rdSMS_Service.Checked Then
                    AlarmMethod = "SMS"
                End If
                If rdEmail_Service.Checked Then
                    AlarmMethod = "EMAIL"
                End If
                .AlarmMethod = AlarmMethod
                .IntervalMinute = txtIntervalTime_Service.Text
            End With

            Dim dtServiceList As New DataTable
            With dtServiceList
                .Columns.Add(New DataColumn("Select", GetType(String)))
                .Columns.Add(New DataColumn("ServiceName", GetType(String)))
                .Columns.Add(New DataColumn("AlarmCode", GetType(String)))
                .Columns.Add(New DataColumn("ProcessName", GetType(String)))
                .Columns.Add(New DataColumn("Active", GetType(String)))
            End With
            Dim dr As DataRow

            Dim isEmpty As Boolean = False
            For i As Integer = 0 To DGVService.Rows.Count - 1
                If DGVService.Rows(i).Cells("chk").Value = "Y" Then
                    Dim chk As String = DGVService.Rows(i).Cells("chk").Value
                    Dim ServiceName As String = ""
                    If Not DGVService.Rows(i).Cells("ServiceName").Value Is Nothing AndAlso DGVService.Rows(i).Cells("ServiceName").Value.ToString <> "" Then
                        ServiceName = DGVService.Rows(i).Cells("ServiceName").Value
                    End If

                    Dim AlarmCode As String = ""
                    If Not DGVService.Rows(i).Cells("AlamCode").Value Is Nothing AndAlso DGVService.Rows(i).Cells("AlamCode").Value.ToString <> "" Then
                        AlarmCode = DGVService.Rows(i).Cells("AlamCode").Value
                    End If

                    Dim ProcessName As String = ""
                    If Not DGVService.Rows(i).Cells("ProcessName").Value Is Nothing AndAlso DGVService.Rows(i).Cells("ProcessName").Value.ToString <> "" Then
                        ProcessName = DGVService.Rows(i).Cells("ProcessName").Value
                    End If

                    Dim Active As String = DGVService.Rows(i).Cells("Active").Value
                    If Trim(AlarmCode) = "" Or Trim(ProcessName) = "" Then
                        MessageBox.Show("กรุณาระบุข้อมูลให้ครบถ้วน")
                        Exit Sub
                    End If

                    dr = dtServiceList.NewRow
                    dr("Select") = chk
                    dr("ServiceName") = Trim(ServiceName)
                    dr("AlarmCode") = Trim(AlarmCode)
                    dr("ProcessName") = Trim(ProcessName)
                    dr("Active") = Active
                    dtServiceList.Rows.Add(dr)
                End If
            Next

            Dim ini As New IniReader(Application.StartupPath & "\Config.ini")
            ini.Section = "Setting"
            Dim ConfigFileName As String = txtServerName_Service.Text & "_CONFIG_SERVICE_PROCESS.xml"
            Dim path As String = Application.StartupPath & "\Config\" & ConfigFileName
            eng.SaveServiceConfig(stSWPara, dtServiceList, "Service", ini.ReadString("IsShopInstall")).Save(path)

            Try
                Dim ws As New FaultManagementService.FaultManagementService
                ws.Timeout = 10000
                ws.Url = ini.ReadString("DCWebserviceURL")
                ws.SendConfigFileToDC(IO.File.ReadAllBytes(path), ConfigFileName, Environment.MachineName)
                ws = Nothing
                MessageBox.Show("บันทีกข้อมูลเรียบร้อย")
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            ini = Nothing
            frmMain.SetListConfigData()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub btnSave_Process_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave_Process.Click
        Try
            If txtIntervalTime_Process.Text.Trim = "" Then
                MessageBox.Show("กรุณาระบุ Interval Time")
                Exit Sub
            End If
            If txtRepeateCheck_Process.Text.Trim = "" Then
                MessageBox.Show("กรุณาระบุ Repeate Check")
                Exit Sub
            End If
            If CInt(txtIntervalTime_Process.Text) = 0 Then
                MessageBox.Show("กรุณาระบุ Interval Time มากกว่า 0")
                Exit Sub
            End If
            If CInt(txtRepeateCheck_Process.Text) = 0 Then
                MessageBox.Show("กรุณาระบุ Repeate Check มากกว่า 0")
                Exit Sub
            End If

            Dim eng As New Engine.Config.ProcessConfigENG

            Dim ConfigFileName As String = txtServerName_Process.Text & "_CONFIG_PROCESS_PROCESS.xml"
            Dim path As String = Application.StartupPath & "\Config\" & ConfigFileName

            Dim stSWPara As New Engine.Config.ConfigENG.SWPara
            With stSWPara
                .ShopName = txtShopName_Process.Text
                .ServerName = txtServerName_Process.Text
                .IPAddress = txtServerIP_Process.Text
                .AlarmType = txtAlamType_Process.Text
                .AlarmSeverity = txtAlamSeverity_Process.Text
                .RepeateCheck = txtRepeateCheck_Process.Text
                .SunDay = IIf(chkSun_Process.Checked, "Y", "N")
                .Monday = IIf(chkMon_Process.Checked, "Y", "N")
                .Tuesday = IIf(chkTue_Process.Checked, "Y", "N")
                .Wednesday = IIf(chkWed_Process.Checked, "Y", "N")
                .Thursday = IIf(chkThurs_Process.Checked, "Y", "N")
                .Friday = IIf(chkFri_Process.Checked, "Y", "N")
                .Saturday = IIf(chkSat_Process.Checked, "Y", "N")

                If chkAllDayEvent_Process.Checked Then
                    .AlamTimeFrom = ""
                    .AlamTimeTo = ""
                Else
                    .AlamTimeFrom = dpAlamTimeFrom_Process.Value.ToString("HH:mm")
                    .AlamTimeTo = dpAlamTimeTo_Process.Value.ToString("HH:mm")
                End If
                .AllDayEvent = IIf(chkAllDayEvent_Process.Checked, "Y", "N")

                Dim AlarmMethod As String = ""
                If rdSnmp_Process.Checked Then
                    AlarmMethod = "SNMP"
                End If
                If rdSMS_Process.Checked Then
                    AlarmMethod = "SMS"
                End If
                If rdEmail_Process.Checked Then
                    AlarmMethod = "EMAIL"
                End If
                .AlarmMethod = AlarmMethod
                .IntervalMinute = txtIntervalTime_Process.Text
            End With

            Dim dtServiceList As New DataTable
            With dtServiceList
                .Columns.Add(New DataColumn("Select", GetType(String)))
                .Columns.Add(New DataColumn("WindowProcessName", GetType(String)))
                .Columns.Add(New DataColumn("AlarmCode", GetType(String)))
                .Columns.Add(New DataColumn("ProcessName", GetType(String)))
                .Columns.Add(New DataColumn("Active", GetType(String)))
            End With
            Dim dr As DataRow

            Dim isEmpty As Boolean = False
            For i As Integer = 0 To DGVProcess.Rows.Count - 1
                If DGVProcess.Rows(i).Cells("ProcessSelect").Value = "Y" Then
                    Dim chk As String = DGVProcess.Rows(i).Cells(0).Value
                    Dim WindowProcessName As String = ""
                    If Not DGVProcess.Rows(i).Cells(1).Value Is Nothing AndAlso DGVProcess.Rows(i).Cells(1).Value.ToString <> "" Then
                        WindowProcessName = DGVProcess.Rows(i).Cells("WindowsProcessName").Value
                    End If

                    Dim AlarmCode As String = ""
                    If Not DGVProcess.Rows(i).Cells("ProcessAlarmCode").Value Is Nothing AndAlso DGVProcess.Rows(i).Cells("ProcessAlarmCode").Value.ToString <> "" Then
                        AlarmCode = DGVProcess.Rows(i).Cells("ProcessAlarmCode").Value
                    End If

                    Dim ProcessName As String = ""
                    If Not DGVProcess.Rows(i).Cells("ProcessProcessName").Value Is Nothing AndAlso DGVProcess.Rows(i).Cells("ProcessProcessName").Value.ToString <> "" Then
                        ProcessName = DGVProcess.Rows(i).Cells("ProcessProcessName").Value
                    End If

                    Dim Active As String = DGVProcess.Rows(i).Cells("ProcessActive").Value

                    If Trim(AlarmCode) = "" Or Trim(ProcessName) = "" Then
                        MessageBox.Show("กรุณาระบุข้อมูลให้ครบถ้วน")
                        Exit Sub
                    End If

                    dr = dtServiceList.NewRow
                    dr("Select") = chk
                    dr("WindowProcessName") = Trim(WindowProcessName)
                    dr("AlarmCode") = Trim(AlarmCode)
                    dr("ProcessName") = Trim(ProcessName)
                    dr("Active") = Active
                    dtServiceList.Rows.Add(dr)
                End If
            Next

            Dim ini As New IniReader(Application.StartupPath & "\Config.ini")
            ini.Section = "Setting"
            eng.SaveProcessConfig(stSWPara, dtServiceList, "Process", ini.ReadString("IsShopInstall")).Save(path)

            Try
                
                Dim ws As New FaultManagementService.FaultManagementService
                ws.Timeout = 10000
                ws.Url = ini.ReadString("DCWebserviceURL")
                ws.SendConfigFileToDC(IO.File.ReadAllBytes(path), ConfigFileName, Environment.MachineName)
                ws = Nothing
                MessageBox.Show("บันทีกข้อมูลเรียบร้อย")
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            ini = Nothing
            frmMain.SetListConfigData()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

 
    Private Sub btnSave_Web_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave_Web.Click
        Try
            If txtIntervalTime_Web.Text.Trim = "" Then
                MessageBox.Show("กรุณาระบุ Interval Time")
                Exit Sub
            End If
            If txtRepeateCheck_Web.Text.Trim = "" Then
                MessageBox.Show("กรุณาระบุ Repeate Check")
                Exit Sub
            End If
            If CInt(txtIntervalTime_Web.Text) = 0 Then
                MessageBox.Show("กรุณาระบุ Interval Time มากกว่า 0")
                Exit Sub
            End If
            If CInt(txtRepeateCheck_Web.Text) = 0 Then
                MessageBox.Show("กรุณาระบุ Repeate Check มากกว่า 0")
                Exit Sub
            End If

            Dim eng As New Engine.Config.WebConfigENG

            Dim ConfigFileName As String = txtServerName_Web.Text & "_CONFIG_WEB_PROCESS.xml"
            Dim path As String = Application.StartupPath & "\Config\" & ConfigFileName

            Dim stSWPara As New Engine.Config.ConfigENG.SWPara
            With stSWPara
                .ShopName = txtShopName_Web.Text
                .ServerName = txtServerName_Web.Text
                .IPAddress = txtServerIP_Web.Text
                .AlarmType = txtAlamType_Web.Text
                .AlarmSeverity = txtAlamSeverity_Web.Text
                .RepeateCheck = txtRepeateCheck_Web.Text
                .SunDay = IIf(chkSun_Web.Checked, "Y", "N")
                .Monday = IIf(chkMon_Web.Checked, "Y", "N")
                .Tuesday = IIf(chkTue_Web.Checked, "Y", "N")
                .Wednesday = IIf(chkWed_Web.Checked, "Y", "N")
                .Thursday = IIf(chkThurs_Web.Checked, "Y", "N")
                .Friday = IIf(chkFri_Web.Checked, "Y", "N")
                .Saturday = IIf(chkSat_Web.Checked, "Y", "N")

                If chkAllDayEvent_Web.Checked Then
                    .AlamTimeFrom = ""
                    .AlamTimeTo = ""
                Else
                    .AlamTimeFrom = dpAlamTimeFrom_Web.Value.ToString("HH:mm")
                    .AlamTimeTo = dpAlamTimeTo_Web.Value.ToString("HH:mm")
                End If
                .AllDayEvent = IIf(chkAllDayEvent_Web.Checked, "Y", "N")


                Dim AlarmMethod As String = ""
                If rdSnmp_Web.Checked Then
                    AlarmMethod = "SNMP"
                End If
                If rdSMS_Web.Checked Then
                    AlarmMethod = "SMS"
                End If
                If rdEmail_Web.Checked Then
                    AlarmMethod = "EMAIL"
                End If
                .AlarmMethod = AlarmMethod
                .IntervalMinute = txtIntervalTime_Web.Text
            End With

            Dim dtServiceList As New DataTable
            With dtServiceList
                .Columns.Add(New DataColumn("Select", GetType(String)))
                .Columns.Add(New DataColumn("WebApplicationName", GetType(String)))
                .Columns.Add(New DataColumn("AlarmCode", GetType(String)))
                .Columns.Add(New DataColumn("ProcessName", GetType(String)))
                .Columns.Add(New DataColumn("Active", GetType(String)))
            End With
            Dim dr As DataRow

            Dim isEmpty As Boolean = False
            For i As Integer = 0 To DGVWeb.Rows.Count - 1
                If DGVWeb.Rows(i).Cells("WebSelect").Value = "Y" Then
                    Dim chk As String = DGVWeb.Rows(i).Cells("WebSelect").Value
                    Dim ServiceName As String = ""
                    If Not DGVWeb.Rows(i).Cells("WebApplicationName").Value Is Nothing AndAlso DGVWeb.Rows(i).Cells("WebApplicationName").Value.ToString <> "" Then
                        ServiceName = DGVWeb.Rows(i).Cells("WebApplicationName").Value
                    End If

                    Dim AlarmCode As String = ""
                    If Not DGVWeb.Rows(i).Cells("WebAlarmCode").Value Is Nothing AndAlso DGVWeb.Rows(i).Cells("WebAlarmCode").Value.ToString <> "" Then
                        AlarmCode = DGVWeb.Rows(i).Cells("WebAlarmCode").Value
                    End If

                    Dim ProcessName As String = ""
                    If Not DGVWeb.Rows(i).Cells("WebProcessName").Value Is Nothing AndAlso DGVWeb.Rows(i).Cells("WebProcessName").Value.ToString <> "" Then
                        ProcessName = DGVWeb.Rows(i).Cells("WebProcessName").Value
                    End If

                    Dim Active As String = DGVWeb.Rows(i).Cells("WebActive").Value

                    If Trim(AlarmCode) = "" Or Trim(ProcessName) = "" Then
                        MessageBox.Show("กรุณาระบุข้อมูลให้ครบถ้วน")
                        Exit Sub
                    End If

                    dr = dtServiceList.NewRow
                    dr("Select") = chk
                    dr("WebApplicationName") = Trim(ServiceName)
                    dr("AlarmCode") = Trim(AlarmCode)
                    dr("ProcessName") = Trim(ProcessName)
                    dr("Active") = Active
                    dtServiceList.Rows.Add(dr)
                End If
            Next

            Dim ini As New IniReader(Application.StartupPath & "\Config.ini")
            ini.Section = "Setting"
            eng.SaveWebConfig(stSWPara, dtServiceList, "WebApplication", ini.ReadString("IsShopInstall")).Save(path)

            Try
                Dim ws As New FaultManagementService.FaultManagementService
                ws.Timeout = 10000
                ws.Url = ini.ReadString("DCWebserviceURL")
                ws.SendConfigFileToDC(IO.File.ReadAllBytes(path), ConfigFileName, Environment.MachineName)
                ws = Nothing
                MessageBox.Show("บันทีกข้อมูลเรียบร้อย")
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            ini = Nothing
            frmMain.SetListConfigData()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnAddNewRow_File_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNewRow_File.Click
        Dim dt As New DataTable
        dt.Columns.Add("FileName")
        dt.Columns.Add("FileSizeMinor")
        dt.Columns.Add("FileSizeMajor")
        dt.Columns.Add("FileSizeCritical")
        dt.Columns.Add("Active")

        Dim dr As DataRow
        For Each item As DataGridViewRow In DGVFile.Rows
            dr = dt.NewRow
            dr("FileName") = item.Cells("FileSizeFileName").Value
            dr("FileSizeMinor") = item.Cells("FileSizeMinor").Value
            dr("FileSizeMajor") = item.Cells("FileSizeMajor").Value
            dr("FileSizeCritical") = item.Cells("FileSizeCritical").Value
            dr("Active") = item.Cells("FileSizeIsActive").Value
            dt.Rows.Add(dr)
        Next


        dr = dt.NewRow
        dr("FileName") = ""
        dr("FileSizeMinor") = 0
        dr("FileSizeMajor") = 0
        dr("FileSizeCritical") = 0
        dr("Active") = "Y"
        dt.Rows.Add(dr)
        DGVFile.DataSource = dt
        Dim grv As DataGridViewRow = DGVFile.Rows(DGVFile.Rows.Count - 1)
        grv.Cells("FileSizeIsActive").Value = "Y"

    End Sub

    Private Sub DGVFile_CellClick(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles DGVFile.CellClick
        If e.ColumnIndex = DGVFile.Columns("btn").Index AndAlso e.RowIndex >= 0 Then
            Dim dlg As New frmSelectFile
            If dlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
                DGVFile.Rows(e.RowIndex).Cells("FileSizeFileName").Value = dlg.FileSelect
            End If
        End If
        If e.ColumnIndex = DGVFile.Columns("btnDelete").Index AndAlso e.RowIndex >= 0 Then
            If MsgBox("ต้องการลบข้อมูลใช่หรือไม่?", MsgBoxStyle.YesNo, "Confirm Delete") = MsgBoxResult.Yes Then
                Dim dt As New DataTable
                dt = DGVFile.DataSource
                dt.Rows.RemoveAt(e.RowIndex)
                DGVFile.DataSource = dt
            End If
        End If
    End Sub

    Private Sub DGVFile_EditingControlShowing1(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles DGVFile.EditingControlShowing
        If DGVFile.CurrentCell.ColumnIndex > 1 And DGVFile.CurrentCell.ColumnIndex < 5 Then
            Dim txtedit As TextBox = DirectCast(e.Control, TextBox)
            AddHandler txtedit.KeyPress, AddressOf txtEdit_KeyPress
        End If
    End Sub

    Private Sub txtEdit_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs)
        If DGVFile.CurrentCell.ColumnIndex > 1 And DGVFile.CurrentCell.ColumnIndex < 5 Then
            If ("0123456789\b".IndexOf(e.KeyChar) = -1) Then
                If e.KeyChar <> Convert.ToChar(Keys.Back) Then
                    e.Handled = True
                End If
            End If
        End If
    End Sub

    Private Sub btnSave_File_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave_File.Click
        If txtIntervalMinute_File.Text.Trim = "" Then
            MessageBox.Show("กรุณาระบุ Interval Minute")
            txtIntervalMinute_File.Focus()
            Exit Sub
        End If
        If txtRepeateCheck_File.Text.Trim = "" Then
            MessageBox.Show("กรุณาระบุ Repeate Check")
            txtRepeateCheck_File.Focus()
            Exit Sub
        End If
        If CInt(txtIntervalMinute_File.Text) = 0 Then
            MessageBox.Show("กรุณาระบุ Interval Minute มากกว่า 0")
            txtIntervalMinute_File.Focus()
            Exit Sub
        End If
        If CInt(txtRepeateCheck_File.Text) = 0 Then
            MessageBox.Show("กรุณาระบุ Repeate Check  มากกว่า 0")
            txtRepeateCheck_File.Focus()
            Exit Sub
        End If
        If DGVFile.Rows.Count = 0 Then
            MessageBox.Show("กรุณาเลือกไฟล์")
            Exit Sub
        End If

        Dim stSWPara As New Engine.Config.ConfigENG.SWPara
        With stSWPara
            .ShopName = txtShopName_File.Text
            .ServerName = txtServerName_File.Text
            .IPAddress = txtServerIP_File.Text
            .AlarmType = txtAlamType_File.Text
            '.AlarmSeverity = txtAlamSeverity_File.Text
            .RepeateCheck = txtRepeateCheck_File.Text
            .SunDay = IIf(chkSun_File.Checked, "Y", "N")
            .Monday = IIf(chkMon_File.Checked, "Y", "N")
            .Tuesday = IIf(chkTue_File.Checked, "Y", "N")
            .Wednesday = IIf(chkWed_File.Checked, "Y", "N")
            .Thursday = IIf(chkThurs_File.Checked, "Y", "N")
            .Friday = IIf(chkFri_File.Checked, "Y", "N")
            .Saturday = IIf(chkSat_File.Checked, "Y", "N")

            If chkAllDayEvent_File.Checked Then
                .AlamTimeFrom = ""
                .AlamTimeTo = ""
            Else
                .AlamTimeFrom = dpAlamTimeFrom_File.Value.ToString("HH:mm")
                .AlamTimeTo = dpAlamTimeTo_File.Value.ToString("HH:mm")
            End If
            .AllDayEvent = IIf(chkAllDayEvent_File.Checked, "Y", "N")


            Dim AlarmMethod As String = ""
            If rdSnmp_File.Checked Then
                AlarmMethod = "SNMP"
            End If
            If rdSMS_File.Checked Then
                AlarmMethod = "SMS"
            End If
            If rdEmail_File.Checked Then
                AlarmMethod = "EMAIL"
            End If
            .AlarmMethod = AlarmMethod
            .IntervalMinute = txtIntervalMinute_File.Text
        End With

        Dim dt As New DataTable
        dt.Columns.Add("FileName")
        dt.Columns.Add("FileSizeMinor")
        dt.Columns.Add("FileSizeMajor")
        dt.Columns.Add("FileSizeCritical")
        dt.Columns.Add("Active")
        For Each grv As DataGridViewRow In DGVFile.Rows
            If grv.Cells("FileSizeFileName").Value = "" Then
                MessageBox.Show("กรุณาเลือกไฟล์")
                ShowMessageDGV("FileSizeFileName", grv, DGVFile)
                Exit Sub
            End If

            If IO.File.Exists(grv.Cells("FileSizeFileName").Value) = False Then
                MessageBox.Show("File not found " & grv.Cells("FileSizeFileName").Value)
                ShowMessageDGV("FileSizeFileName", grv, DGVFile)
                Exit Sub
            End If
            If grv.Cells("FileSizeMinor").Value.ToString = "" Then
                MessageBox.Show("กรุณาระบุ Minor(GB)")
                ShowMessageDGV("FileSizeMinor", grv, DGVFile)
                Exit Sub
            End If
            If grv.Cells("FileSizeMajor").Value.ToString = "" Then
                MessageBox.Show("กรุณาระบุ Major(GB)")
                ShowMessageDGV("FileSizeMajor", grv, DGVFile)
                Exit Sub
            End If
            If grv.Cells("FileSizeCritical").Value.ToString = "" Then
                MessageBox.Show("กรุณาระบุ Critical(GB)")
                ShowMessageDGV("FileSizeCritical", grv, DGVFile)
                Exit Sub
            End If

            If CInt(IIf(grv.Cells("FileSizeMinor").Value.ToString = "", 0, grv.Cells("FileSizeMinor").Value.ToString)) >= _
            CInt(IIf(grv.Cells("FileSizeMajor").Value.ToString = "", 0, grv.Cells("FileSizeMajor").Value.ToString)) Then
                MessageBox.Show("กรุณาระบุ  Minor Value Over น้อยกว่า  Major Value Over")
                ShowMessageDGV("FileSizeMinor", grv, DGVFile)
                Exit Sub
            End If

            If CInt(IIf(grv.Cells("FileSizeMajor").Value.ToString = "", 0, grv.Cells("FileSizeMajor").Value.ToString)) >= _
            CInt(IIf(grv.Cells("FileSizeCritical").Value.ToString = "", 0, grv.Cells("FileSizeCritical").Value.ToString)) Then
                MessageBox.Show("กรุณาระบุ  Major Value Over น้อยกว่า Critical Value Over")
                ShowMessageDGV("FileSizeMajor", grv, DGVFile)
                Exit Sub
            End If

            If IO.File.Exists(grv.Cells("FileSizeFileName").Value) = True Then
                Dim dr As DataRow = dt.NewRow
                dr("FileName") = grv.Cells("FileSizeFileName").Value
                dr("FileSizeMinor") = grv.Cells("FileSizeMinor").Value.ToString
                dr("FileSizeMajor") = grv.Cells("FileSizeMajor").Value.ToString
                dr("FileSizeCritical") = grv.Cells("FileSizeCritical").Value.ToString
                dr("Active") = grv.Cells("FileSizeIsActive").Value
                dt.Rows.Add(dr)
            End If
        Next

        Dim ini As New IniReader(Application.StartupPath & "\Config.ini")
        ini.Section = "Setting"
        Dim cfg As New Engine.Config.FileConfigENG
        Dim xmlFile As New XDocument
        xmlFile = cfg.SaveFileSizeConfig(stSWPara, dt, ini.ReadString("IsShopInstall"))
        cfg = Nothing

        Dim ConfigFileName As String = txtServerName_Web.Text & "_CONFIG_DATAFILESIZE.xml"
        Dim path As String = Application.StartupPath & "\Config\" & ConfigFileName
        xmlFile.Save(path)

        Try
            ini.Section = "Setting"
            Dim ws As New FaultManagementService.FaultManagementService
            ws.Timeout = 10000
            ws.Url = ini.ReadString("DCWebserviceURL")
            ws.SendConfigFileToDC(IO.File.ReadAllBytes(path), ConfigFileName, Environment.MachineName)
            ws = Nothing
            MessageBox.Show("บันทีกข้อมูลเรียบร้อย")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        frmMain.SetListConfigData()
        ini = Nothing
    End Sub

    Private Sub ShowMessageDGV(ByVal ColumnName As String, ByVal grv As DataGridViewRow, ByVal gvData As DataGridView)
        For Each gv As DataGridViewRow In gvData.Rows
            gv.Cells(ColumnName).Selected = False
        Next
        grv.Cells(ColumnName).Selected = True
    End Sub

    Private Sub btnAddRow_FileLost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddRow_FileLost.Click
        Dim dt As New DataTable
        dt.Columns.Add("FileName")
        dt.Columns.Add("Active")
        Dim dr As DataRow
        For Each item As DataGridViewRow In DGVFileLost.Rows
            dr = dt.NewRow
            dr("FileName") = item.Cells("FileLName").Value
            dr("Active") = item.Cells("FileLActive").Value
            dt.Rows.Add(dr)
        Next

        dr = dt.NewRow
        dr("FileName") = ""
        dr("Active") = "Y"
        dt.Rows.Add(dr)

        DGVFileLost.DataSource = dt

        Dim grv As DataGridViewRow = DGVFileLost.Rows(DGVFileLost.Rows.Count - 1)
        grv.Cells("FileLActive").Value = "Y"
    End Sub

    Private Sub btnSave_FileLost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave_FileLost.Click
        If txtIntervalMinute_FileLost.Text.Trim = "" Then
            MessageBox.Show("กรุณาระบุ Interval Minute")
            Exit Sub
        End If
        If txtRepeateCheck_FileLost.Text.Trim = "" Then
            MessageBox.Show("กรุณาระบุ Interval Minure")
            Exit Sub
        End If
        If CInt(txtIntervalMinute_FileLost.Text) = 0 Then
            MessageBox.Show("กรุณาระบุ Interval Minute มากกว่า 0")
            Exit Sub
        End If
        If CInt(txtRepeateCheck_FileLost.Text) = 0 Then
            MessageBox.Show("กรุณาระบุ Interval Minure มากกว่า 0")
            Exit Sub
        End If

        Dim dt As New DataTable
        dt.Columns.Add("FileName")
        dt.Columns.Add("Active")

        Dim stSWPara As New Engine.Config.ConfigENG.SWPara
        With stSWPara
            .ShopName = txtShopName_FileLost.Text
            .ServerName = txtServerName_FileLost.Text
            .IPAddress = txtServerIP_FileLost.Text
            .AlarmType = txtAlamType_FileLost.Text
            '.AlarmSeverity = txtAlamSeverity_File.Text
            .RepeateCheck = txtRepeateCheck_FileLost.Text
            .SunDay = IIf(chkSun_FileLost.Checked, "Y", "N")
            .Monday = IIf(chkMon_FileLost.Checked, "Y", "N")
            .Tuesday = IIf(chkTue_FileLost.Checked, "Y", "N")
            .Wednesday = IIf(chkWed_FileLost.Checked, "Y", "N")
            .Thursday = IIf(chkThurs_FileLost.Checked, "Y", "N")
            .Friday = IIf(chkFri_FileLost.Checked, "Y", "N")
            .Saturday = IIf(chkSat_FileLost.Checked, "Y", "N")

            If chkAllDayEvent_FileLost.Checked Then
                .AlamTimeFrom = ""
                .AlamTimeTo = ""
            Else
                .AlamTimeFrom = dpAlamTimeFrom_Filelost.Value.ToString("HH:mm")
                .AlamTimeTo = dpAlamTimeTo_FileLost.Value.ToString("HH:mm")
            End If
            .AllDayEvent = IIf(chkAllDayEvent_FileLost.Checked, "Y", "N")

            Dim AlarmMethod As String = ""
            If rdSnmp_FileLost.Checked Then
                AlarmMethod = "SNMP"
            End If
            If rdSMS_FileLost.Checked Then
                AlarmMethod = "SMS"
            End If
            If rdEmail_FileLost.Checked Then
                AlarmMethod = "EMAIL"
            End If
            .AlarmMethod = AlarmMethod
            .IntervalMinute = txtIntervalMinute_FileLost.Text
        End With

        For Each grv As DataGridViewRow In DGVFileLost.Rows
            If grv.Cells("FileLName").Value = "" Then
                MessageBox.Show("กรุณาเลือกไฟล์")
                ShowMessageDGV("FileLName", grv, DGVFileLost)
                Exit Sub
            End If

            'If IO.File.Exists(grv.Cells("FileLName").Value) = True Then
            Dim dr As DataRow = dt.NewRow
            dr("FileName") = grv.Cells("FileLName").Value
            dr("Active") = grv.Cells("FileLActive").Value
            dt.Rows.Add(dr)
            'End If
        Next


        Dim ini As New IniReader(Application.StartupPath & "\Config.ini")
        ini.Section = "Setting"
        Dim cfg As New Engine.Config.FileConfigENG
        Dim xmlFile As New XDocument
        xmlFile = cfg.SaveFileLostConfig(stSWPara, dt, ini.ReadString("IsShopInstall"))
        cfg = Nothing

        Dim ConfigFileName As String = txtServerName_Web.Text & "_CONFIG_DATAFILELOST.xml"
        Dim path As String = Application.StartupPath & "\Config\" & ConfigFileName
        xmlFile.Save(path)

        Try
            ini.Section = "Setting"
            Dim ws As New FaultManagementService.FaultManagementService
            ws.Timeout = 10000
            ws.Url = ini.ReadString("DCWebserviceURL")
            ws.SendConfigFileToDC(IO.File.ReadAllBytes(path), ConfigFileName, Environment.MachineName)
            ws = Nothing
            MessageBox.Show("บันทีกข้อมูลเรียบร้อย")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        ini = Nothing
        frmMain.SetListConfigData()
    End Sub

    Private Sub DGVFileLost_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGVFileLost.CellClick
        If e.ColumnIndex = DGVFileLost.Columns("SelectFileLost").Index AndAlso e.RowIndex >= 0 Then

            Dim dlg As New frmSelectFile
            If dlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
                DGVFileLost.Rows(e.RowIndex).Cells("FileLName").Value = dlg.FileSelect
            End If

        End If
        If e.ColumnIndex = DGVFileLost.Columns("btnDeleteFileLost").Index AndAlso e.RowIndex >= 0 Then
            If MsgBox("ต้องการลบข้อมูลใช่หรือไม่?", MsgBoxStyle.YesNo, "Confirm Delete") = MsgBoxResult.Yes Then
                Dim dt As New DataTable
                dt = DGVFileLost.DataSource
                dt.Rows.RemoveAt(e.RowIndex)
                DGVFileLost.DataSource = dt
            End If
        End If
    End Sub



#Region "__CheckKeyNumberOnly"
    Private Sub txtIntervalMinute_File_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtIntervalMinute_File.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub txtIntervalMinute_FileLost_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtIntervalMinute_FileLost.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub txtRepeateCheck_File_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtRepeateCheck_File.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub txtRepeateCheck_FileLost_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtRepeateCheck_FileLost.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub txtRepeateCheck_Service_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtRepeateCheck_Service.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub txtRepeateCheck_Process_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtRepeateCheck_Process.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub txtRepeateCheck_Web_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtRepeateCheck_Web.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub txtIntervalTime_Service_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtIntervalTime_Service.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub txtIntervalTime_Process_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtIntervalTime_Process.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub txtIntervalTime_Web_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtIntervalTime_Web.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub
#End Region

#Region "__AllDayEvent"
    Private Sub chkAllDayEvent_Service_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkAllDayEvent_Service.CheckedChanged
        If chkAllDayEvent_Service.Checked Then
            dpAlamTimeFrom_Service.Enabled = False
            dpAlamTimeTo_Service.Enabled = False
        Else
            dpAlamTimeFrom_Service.Enabled = True
            dpAlamTimeTo_Service.Enabled = True
        End If
    End Sub

    Private Sub chkAllDayEvent_Process_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkAllDayEvent_Process.CheckedChanged
        If chkAllDayEvent_Process.Checked Then
            dpAlamTimeFrom_Process.Enabled = False
            dpAlamTimeTo_Process.Enabled = False
        Else
            dpAlamTimeFrom_Process.Enabled = True
            dpAlamTimeTo_Process.Enabled = True
        End If
    End Sub

    Private Sub chkAllDayEvent_Web_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkAllDayEvent_Web.CheckedChanged
        If chkAllDayEvent_Web.Checked Then
            dpAlamTimeFrom_Web.Enabled = False
            dpAlamTimeTo_Web.Enabled = False
        Else
            dpAlamTimeFrom_Web.Enabled = True
            dpAlamTimeTo_Web.Enabled = True
        End If
    End Sub

    Private Sub chkAllDayEvent_File_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkAllDayEvent_File.CheckedChanged
        If chkAllDayEvent_File.Checked Then
            dpAlamTimeFrom_File.Enabled = False
            dpAlamTimeTo_File.Enabled = False
        Else
            dpAlamTimeFrom_File.Enabled = True
            dpAlamTimeTo_File.Enabled = True
        End If
    End Sub

    Private Sub chkAllDayEvent_FileLost_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkAllDayEvent_FileLost.CheckedChanged
        If chkAllDayEvent_FileLost.Checked Then
            dpAlamTimeFrom_Filelost.Enabled = False
            dpAlamTimeTo_FileLost.Enabled = False
        Else
            dpAlamTimeFrom_Filelost.Enabled = True
            dpAlamTimeTo_FileLost.Enabled = True
        End If
    End Sub
#End Region



End Class