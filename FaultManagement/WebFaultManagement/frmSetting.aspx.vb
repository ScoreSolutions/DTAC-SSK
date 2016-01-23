Imports Engine.Common
Imports Engine.Config
Imports System.Data
Imports System.IO
Imports Telerik.Web.UI

Partial Class frmSetting
    Inherits System.Web.UI.Page

#Region "Initail"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            PopulateDll()
            PopulateDataCPU()
            PopulateDataRAM()
            PopulateDataHDD()
        End If
    End Sub



    Private Sub PopulateDll()
        ddlShopName.DataSource = FunctionEng.GetActiveShop
        ddlShopName.DataTextField = "Shop_Name_Th"
        ddlShopName.DataValueField = "id"
        ddlShopName.DataBind()
        PopulateDrive()
    End Sub

    Private Sub PopulateDrive()
        Dim drDrive As DataRow
        Dim dtDrive As New DataTable
        dtDrive.Columns.Add("Name")

        Dim array As String() = {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"}
        For i As Integer = 0 To array.Length - 1
            drDrive = dtDrive.NewRow
            drDrive("Name") = array(i)
            dtDrive.Rows.Add(drDrive)
        Next


        RadComboBox_Drive.DataSource = dtDrive
        RadComboBox_Drive.DataTextField = "Name"
        RadComboBox_Drive.DataValueField = "Name"
        RadComboBox_Drive.DataBind()

        'DefaultValue
        lblText_OutDrive.Text = ""
        Dim DefaultValue As New ArrayList
        Dim pathxml As String = Server.MapPath("~/FileSetting/" & ddlShopName.SelectedValue & "_Config_HDD_Process.xml")
        Dim ds As New DataSet()
        Try
            If File.Exists(pathxml) = True Then
                Dim dtDriveName As New DataTable
                dtDriveName.Columns.Add("Name", GetType(String))
                Dim drDriveName As DataRow
                ds.ReadXml(pathxml)
                Dim dtxml As DataTable = ds.Tables(3)
                If dtxml.Columns.Count > 4 Then
                    For i As Integer = 4 To dtxml.Columns.Count - 1
                        DefaultValue.Add(dtxml.Columns.Item(i).ColumnName.Replace("_Id", "") & "")
                        lblText_OutDrive.Text &= IIf(lblText_OutDrive.Text = "", dtxml.Columns.Item(i).ColumnName.Replace("_Id", ""), "," & dtxml.Columns.Item(i).ColumnName.Replace("_Id", ""))
                    Next
                End If

                If Not IsNothing(DefaultValue) Then
                    Dim checkedItems As ArrayList = DefaultValue
                    For Each item As RadComboBoxItem In RadComboBox_Drive.Items
                        Dim cbSelect As CheckBox = DirectCast(item.FindControl("cbSelect_InDrive"), CheckBox)


                        For i2 As Integer = 0 To checkedItems.Count - 1
                            If cbSelect.Text = checkedItems.Item(i2) Then
                                cbSelect.Checked = True
                            End If
                        Next

                    Next
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub SetDefaultDrive()
        lblText_OutDrive.Text = ""
        For Each item As RadComboBoxItem In RadComboBox_Drive.Items
            Dim cbSelect As CheckBox = DirectCast(item.FindControl("cbSelect_InDrive"), CheckBox)
            If cbSelect.Checked Then
                lblText_OutDrive.Text &= IIf(lblText_OutDrive.Text = "", cbSelect.Text, "," & cbSelect.Text)
            End If
        Next

    End Sub

    Private Sub PopulateDataCPU()
        Dim pathxml As String = Server.MapPath("~/FileSetting/" & ddlShopName.SelectedValue & "_Config_CPU_Process.xml")
        Dim ds As New DataSet()
        Try
            If File.Exists(pathxml) = False Then

                CType(ctlAramServity1.FindControl("txtReplaceTime"), TextBox).Text = ""
                CType(ctlAramServity1.FindControl("txtInterval"), TextBox).Text = ""
                CType(ctlAramServity1.FindControl("txtInterval"), TextBox).Text = ""
                'CType(ctlAramServity1.FindControl("ckbActive"), CheckBox).Checked = False
                CType(ctlAramServity1.FindControl("txtMinor"), TextBox).Text = ""
                'CType(ctlAramServity1.FindControl("rdoMinorMethod"), RadioButtonList).SelectedIndex = -1
                CType(ctlAramServity1.FindControl("txtMajor"), TextBox).Text = ""
                'CType(ctlAramServity1.FindControl("rdoMajorMethod"), RadioButtonList).SelectedIndex = -1
                CType(ctlAramServity1.FindControl("txtCritical"), TextBox).Text = ""
                'CType(ctlAramServity1.FindControl("rdoCriticalMethod"), RadioButtonList).SelectedIndex = -1

                Exit Sub
            End If
            ds.ReadXml(pathxml)
            Dim dvLevel1 As DataView = ds.Tables(0).DefaultView
            If dvLevel1.Count > 0 Then
                txtServerName.Text = dvLevel1(0)("ServerName")
                txtIpAddress.Text = dvLevel1(0)("IPAddress")
                CType(ctlAramServity1.FindControl("txtReplaceTime"), TextBox).Text = dvLevel1(0)("RepeateCheck")
                CType(ctlAramServity1.FindControl("txtInterval"), TextBox).Text = dvLevel1(0)("IntervalMinute")
                If dvLevel1(0)("Active") = "Y" Then
                    CType(ctlAramServity1.FindControl("ckbActive"), CheckBox).Checked = True
                End If
            End If

            Dim dvLevel2 As DataView = ds.Tables(1).DefaultView
            dvLevel2.RowFilter = "Severity='Minor'"
            If dvLevel2.Count > 0 Then
                CType(ctlAramServity1.FindControl("txtMinor"), TextBox).Text = dvLevel2(0)("ValueOver")
                CType(ctlAramServity1.FindControl("rdoMinorMethod"), RadioButtonList).SelectedValue = dvLevel2(0)("AlarmMethod")
            End If

            dvLevel2.RowFilter = "Severity='Major'"
            If dvLevel2.Count > 0 Then
                CType(ctlAramServity1.FindControl("txtMajor"), TextBox).Text = dvLevel2(0)("ValueOver")
                CType(ctlAramServity1.FindControl("rdoMajorMethod"), RadioButtonList).SelectedValue = dvLevel2(0)("AlarmMethod")
            End If

            dvLevel2.RowFilter = "Severity='Critical'"
            If dvLevel2.Count > 0 Then
                CType(ctlAramServity1.FindControl("txtCritical"), TextBox).Text = dvLevel2(0)("ValueOver")
                CType(ctlAramServity1.FindControl("rdoCriticalMethod"), RadioButtonList).SelectedValue = dvLevel2(0)("AlarmMethod")
            End If


            dvLevel2.RowFilter = ""
            'GridView1.DataSource = ds.Tables(1)
            ' GridView1.DataBind()
            ' Dim dt As DataTable = ds.Tables(0)

            ds.Dispose()
            dvLevel1.Dispose()
            dvLevel2.Dispose()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub PopulateDataRAM()
        Dim pathxml As String = Server.MapPath("~/FileSetting/" & ddlShopName.SelectedValue & "_Config_RAM_Process.xml")
        Dim ds As New DataSet()
        Try
            If File.Exists(pathxml) = False Then

                CType(ctlAramServity2.FindControl("txtReplaceTime"), TextBox).Text = ""
                CType(ctlAramServity2.FindControl("txtInterval"), TextBox).Text = ""
                CType(ctlAramServity2.FindControl("txtReplaceTime"), TextBox).Text = ""
                CType(ctlAramServity2.FindControl("txtInterval"), TextBox).Text = ""
                'CType(ctlAramServity2.FindControl("ckbActive"), CheckBox).Checked = False
                CType(ctlAramServity2.FindControl("txtMinor"), TextBox).Text = ""
                'CType(ctlAramServity2.FindControl("rdoMinorMethod"), RadioButtonList).SelectedIndex = -1
                CType(ctlAramServity2.FindControl("txtMajor"), TextBox).Text = ""
                ' CType(ctlAramServity2.FindControl("rdoMajorMethod"), RadioButtonList).SelectedIndex = -1
                CType(ctlAramServity2.FindControl("txtCritical"), TextBox).Text = ""
                ' CType(ctlAramServity2.FindControl("rdoCriticalMethod"), RadioButtonList).SelectedIndex = -1

                Exit Sub
            End If
            ds.ReadXml(pathxml)
            Dim dvLevel1 As DataView = ds.Tables(0).DefaultView
            If dvLevel1.Count > 0 Then
                txtServerName.Text = dvLevel1(0)("ServerName")
                txtIpAddress.Text = dvLevel1(0)("IPAddress")
                CType(ctlAramServity2.FindControl("txtReplaceTime"), TextBox).Text = dvLevel1(0)("RepeateCheck")
                CType(ctlAramServity2.FindControl("txtInterval"), TextBox).Text = dvLevel1(0)("IntervalMinute")
                If dvLevel1(0)("Active") = "Y" Then
                    CType(ctlAramServity2.FindControl("ckbActive"), CheckBox).Checked = True
                End If

            End If

            Dim dvLevel2 As DataView = ds.Tables(1).DefaultView
            dvLevel2.RowFilter = "Severity='Minor'"
            If dvLevel2.Count > 0 Then
                CType(ctlAramServity2.FindControl("txtMinor"), TextBox).Text = dvLevel2(0)("ValueOver")
                CType(ctlAramServity2.FindControl("rdoMinorMethod"), RadioButtonList).SelectedValue = dvLevel2(0)("AlarmMethod")
            End If

            dvLevel2.RowFilter = "Severity='Major'"
            If dvLevel2.Count > 0 Then
                CType(ctlAramServity2.FindControl("txtMajor"), TextBox).Text = dvLevel2(0)("ValueOver")
                CType(ctlAramServity2.FindControl("rdoMajorMethod"), RadioButtonList).SelectedValue = dvLevel2(0)("AlarmMethod")
            End If

            dvLevel2.RowFilter = "Severity='Critical'"
            If dvLevel2.Count > 0 Then
                CType(ctlAramServity2.FindControl("txtCritical"), TextBox).Text = dvLevel2(0)("ValueOver")
                CType(ctlAramServity2.FindControl("rdoCriticalMethod"), RadioButtonList).SelectedValue = dvLevel2(0)("AlarmMethod")
            End If

            ds.Dispose()
            dvLevel1.Dispose()
            dvLevel2.Dispose()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub PopulateDataHDD()
        Dim pathxml As String = Server.MapPath("~/FileSetting/" & ddlShopName.SelectedValue & "_Config_HDD_Process.xml")
        Dim ds As New DataSet()
        Try
            If File.Exists(pathxml) = False Then

                CType(ctlAramServity3.FindControl("txtReplaceTime"), TextBox).Text = ""
                CType(ctlAramServity3.FindControl("txtInterval"), TextBox).Text = ""
                CType(ctlAramServity3.FindControl("txtReplaceTime"), TextBox).Text = ""
                CType(ctlAramServity3.FindControl("txtInterval"), TextBox).Text = ""
                ' CType(ctlAramServity3.FindControl("ckbActive"), CheckBox).Checked = False
                CType(ctlAramServity3.FindControl("txtMinor"), TextBox).Text = ""
                ' CType(ctlAramServity3.FindControl("rdoMinorMethod"), RadioButtonList).SelectedIndex = -1
                CType(ctlAramServity3.FindControl("txtMajor"), TextBox).Text = ""
                'CType(ctlAramServity3.FindControl("rdoMajorMethod"), RadioButtonList).SelectedIndex = -1
                CType(ctlAramServity3.FindControl("txtCritical"), TextBox).Text = ""
                ' CType(ctlAramServity3.FindControl("rdoCriticalMethod"), RadioButtonList).SelectedIndex = -1
                PopulateDrive()
                Exit Sub
            End If
            ds.ReadXml(pathxml)
            Dim dvLevel1 As DataView = ds.Tables(0).DefaultView
            If dvLevel1.Count > 0 Then
                txtServerName.Text = dvLevel1(0)("ServerName")
                txtIpAddress.Text = dvLevel1(0)("IPAddress")
                CType(ctlAramServity3.FindControl("txtReplaceTime"), TextBox).Text = dvLevel1(0)("RepeateCheck")
                CType(ctlAramServity3.FindControl("txtInterval"), TextBox).Text = dvLevel1(0)("IntervalMinute")
                If dvLevel1(0)("Active") = "Y" Then
                    CType(ctlAramServity3.FindControl("ckbActive"), CheckBox).Checked = True
                End If

            End If

            Dim dvLevel2 As DataView = ds.Tables(3).DefaultView
            dvLevel2.RowFilter = "Severity='Minor'"
            If dvLevel2.Count > 0 Then
                CType(ctlAramServity3.FindControl("txtMinor"), TextBox).Text = dvLevel2(0)("ValueOver")
                CType(ctlAramServity3.FindControl("rdoMinorMethod"), RadioButtonList).SelectedValue = dvLevel2(0)("AlarmMethod")
            End If

            dvLevel2.RowFilter = "Severity='Major'"
            If dvLevel2.Count > 0 Then
                CType(ctlAramServity3.FindControl("txtMajor"), TextBox).Text = dvLevel2(0)("ValueOver")
                CType(ctlAramServity3.FindControl("rdoMajorMethod"), RadioButtonList).SelectedValue = dvLevel2(0)("AlarmMethod")
            End If

            dvLevel2.RowFilter = "Severity='Critical'"
            If dvLevel2.Count > 0 Then
                CType(ctlAramServity3.FindControl("txtCritical"), TextBox).Text = dvLevel2(0)("ValueOver")
                CType(ctlAramServity3.FindControl("rdoCriticalMethod"), RadioButtonList).SelectedValue = dvLevel2(0)("AlarmMethod")
            End If



            ' dvLevel2.RowFilter = ""
            Dim dtDriveName As New DataTable
            dtDriveName.Columns.Add("Name", GetType(String))
            Dim drDriveName As DataRow
            Dim dtxml As DataTable = ds.Tables(3)
            If dtxml.Columns.Count > 4 Then
                For i As Integer = 4 To dtxml.Columns.Count - 1
                    drDriveName = dtDriveName.NewRow
                    drDriveName("Name") = dtxml.Columns.Item(i).ColumnName.Replace("_Id", "")
                    dtDriveName.Rows.Add(drDriveName)
                Next
            End If
            Mydg1.DataSource = dtDriveName
            Mydg1.DataBind()
            ' Dim dt As DataTable = ds.Tables(0)

            ds.Dispose()
            'dvLevel1.Dispose()
            ' dvLevel2.Dispose()

        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region "Fuction"
    Private Function SaveCPU() As Boolean
        Try
            ' Dim AlamType As String = CType(ctlAramServity1.FindControl("txtAlamType"), TextBox).Text
            Dim RepeateCheck As Integer = CType(ctlAramServity1.FindControl("txtReplaceTime"), TextBox).Text
            Dim MinorSeverity As Engine.Config.CPUConfigENG.AlarmSeverity
            With MinorSeverity
                .AlarmCode = "ALM_LOG_CPU_MINOR"
                .Severity = "MINOR"
                .ValueOver = CType(ctlAramServity1.FindControl("txtMinor"), TextBox).Text
                .AlarmMethod = CType(ctlAramServity1.FindControl("rdoMinorMethod"), RadioButtonList).SelectedValue
            End With
            Dim MajorSeverity As Engine.Config.CPUConfigENG.AlarmSeverity
            With MajorSeverity
                .AlarmCode = "ALM_LOG_CPU_MAJOR"
                .Severity = "MAJOR"
                .ValueOver = CType(ctlAramServity1.FindControl("txtMajor"), TextBox).Text
                .AlarmMethod = CType(ctlAramServity1.FindControl("rdoMajorMethod"), RadioButtonList).SelectedValue
            End With
            Dim CriticalSeverity As Engine.Config.CPUConfigENG.AlarmSeverity
            With CriticalSeverity
                .AlarmCode = "ALM_LOG_CPU_CRITICAL"
                .Severity = "CRITICAL"
                .ValueOver = CType(ctlAramServity1.FindControl("txtCritical"), TextBox).Text
                .AlarmMethod = CType(ctlAramServity1.FindControl("rdoCriticalMethod"), RadioButtonList).SelectedValue
            End With
            Dim IntervalMinute As Integer = CType(ctlAramServity1.FindControl("txtInterval"), TextBox).Text
            Dim Active As String = IIf(CType(ctlAramServity1.FindControl("ckbActive"), CheckBox).Checked = True, "Y", "N")
            Dim CPUFileNameSendToWebServie As String = txtServerName.Text & "_CONFIG_CPU_PROCESS.xml"
            Dim CPUFileNameSendToHost As String = ddlShopName.SelectedValue & "_CONFIG_CPU_PROCESS.xml"

            '  Dim  MinorSeverity As AlarmSeverity, ByVal MajorSeverity As AlarmSeverity, ByVal CriticalSeverity As AlarmSeverity, ByVal IntervalMinute As Int16, ByVal Active As String
            Dim clsCPUConfigENG As New CPUConfigENG
            With clsCPUConfigENG
                Dim ff As New IO.FileInfo(Server.MapPath("~/FileSetting/" & CPUFileNameSendToWebServie))
                If Directory.Exists(ff.Directory.FullName) = False Then
                    Directory.CreateDirectory(ff.Directory.FullName)
                End If

                .SaveCPUConfigWeb(ddlShopName.SelectedItem.Text, _
                                  txtServerName.Text, _
                                  txtIpAddress.Text, _
                                  RepeateCheck, _
                                  MinorSeverity, _
                                  MajorSeverity, _
                                  CriticalSeverity, _
                                  IntervalMinute, _
                                  Active, "Y").Save(Server.MapPath("~/FileSetting/" & CPUFileNameSendToWebServie))
                FileCopy(Server.MapPath("~/FileSetting/" & CPUFileNameSendToWebServie), Server.MapPath("~/FileSetting/" & CPUFileNameSendToHost))

            End With
            clsCPUConfigENG = Nothing

            Dim bytes As Byte() = System.IO.File.ReadAllBytes(Server.MapPath("~/FileSetting/" & CPUFileNameSendToWebServie))
            Dim clsFaultManagementService As New FaultManagementService.FaultManagementService
            clsFaultManagementService.Url = Config.GetFaultManagementServiceURL
            clsFaultManagementService.SendConfigFileToDC(bytes, CPUFileNameSendToWebServie, txtServerName.Text)
            clsFaultManagementService.Dispose()


            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function




    Private Function SaveRAM() As Boolean
        Try
            ' Dim AlamType As String = CType(ctlAramServity1.FindControl("txtAlamType"), TextBox).Text
            Dim RepeateCheck As Integer = CType(ctlAramServity2.FindControl("txtReplaceTime"), TextBox).Text
            Dim MinorSeverity As Engine.Config.CPUConfigENG.AlarmSeverity
            With MinorSeverity
                .AlarmCode = "ALM_LOG_RAM_MINOR"
                .Severity = "MINOR"
                .ValueOver = CType(ctlAramServity2.FindControl("txtMinor"), TextBox).Text
                .AlarmMethod = CType(ctlAramServity2.FindControl("rdoMinorMethod"), RadioButtonList).SelectedValue
            End With
            Dim MajorSeverity As Engine.Config.CPUConfigENG.AlarmSeverity
            With MajorSeverity
                .AlarmCode = "ALM_LOG_RAM_MAJOR"
                .Severity = "MAJOR"
                .ValueOver = CType(ctlAramServity2.FindControl("txtMajor"), TextBox).Text
                .AlarmMethod = CType(ctlAramServity2.FindControl("rdoMajorMethod"), RadioButtonList).SelectedValue
            End With
            Dim CriticalSeverity As Engine.Config.CPUConfigENG.AlarmSeverity
            With CriticalSeverity
                .AlarmCode = "ALM_LOG_RAM_CRITICAL"
                .Severity = "CRITICAL"
                .ValueOver = CType(ctlAramServity2.FindControl("txtCritical"), TextBox).Text
                .AlarmMethod = CType(ctlAramServity2.FindControl("rdoCriticalMethod"), RadioButtonList).SelectedValue
            End With
            Dim IntervalMinute As Integer = CType(ctlAramServity2.FindControl("txtInterval"), TextBox).Text
            Dim Active As String = IIf(CType(ctlAramServity2.FindControl("ckbActive"), CheckBox).Checked = True, "Y", "N")
            Dim CPUFileNameSendToWebServie As String = txtServerName.Text & "_CONFIG_RAM_PROCESS.xml"
            Dim CPUFileNameSendToHost As String = ddlShopName.SelectedValue & "_CONFIG_RAM_PROCESS.xml"

            Dim ff As New IO.FileInfo(Server.MapPath("~/FileSetting/" & CPUFileNameSendToWebServie))
            If Directory.Exists(ff.Directory.FullName) = False Then
                Directory.CreateDirectory(ff.Directory.FullName)
            End If
            Dim clsRamConfigENG As New RamConfigENG
            With clsRamConfigENG
                .SaveRamConfigWeb(ddlShopName.SelectedItem.Text, _
                                  txtServerName.Text, _
                                  txtIpAddress.Text, _
                                  RepeateCheck, _
                                  MinorSeverity, _
                                  MajorSeverity, _
                                  CriticalSeverity, _
                                  IntervalMinute, _
                                  Active, "Y").Save(Server.MapPath("~/FileSetting/" & CPUFileNameSendToWebServie))

                FileCopy(Server.MapPath("~/FileSetting/" & CPUFileNameSendToWebServie), Server.MapPath("~/FileSetting/" & CPUFileNameSendToHost))

            End With
            clsRamConfigENG = Nothing

            Dim bytes As Byte() = System.IO.File.ReadAllBytes(Server.MapPath("~/FileSetting/" & CPUFileNameSendToWebServie))
            Dim clsFaultManagementService As New FaultManagementService.FaultManagementService
            clsFaultManagementService.Url = Config.GetFaultManagementServiceURL
            clsFaultManagementService.SendConfigFileToDC(bytes, CPUFileNameSendToWebServie, txtServerName.Text)
            clsFaultManagementService.Dispose()


            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function SaveHDD() As Boolean
        Try
            ' Dim AlamType As String = CType(ctlAramServity1.FindControl("txtAlamType"), TextBox).Text
            Dim RepeateCheck As Integer = CType(ctlAramServity3.FindControl("txtReplaceTime"), TextBox).Text
            Dim MinorSeverity As Engine.Config.CPUConfigENG.AlarmSeverity
            With MinorSeverity
                .AlarmCode = "ALM_LOG_HD_MINOR"
                .Severity = "MINOR"
                .ValueOver = CType(ctlAramServity3.FindControl("txtMinor"), TextBox).Text
                .AlarmMethod = CType(ctlAramServity3.FindControl("rdoMinorMethod"), RadioButtonList).SelectedValue
            End With
            Dim MajorSeverity As Engine.Config.CPUConfigENG.AlarmSeverity
            With MajorSeverity
                .AlarmCode = "ALM_LOG_HD_MAJOR"
                .Severity = "MAJOR"
                .ValueOver = CType(ctlAramServity3.FindControl("txtMajor"), TextBox).Text
                .AlarmMethod = CType(ctlAramServity3.FindControl("rdoMajorMethod"), RadioButtonList).SelectedValue
            End With
            Dim CriticalSeverity As Engine.Config.CPUConfigENG.AlarmSeverity
            With CriticalSeverity
                .AlarmCode = "ALM_LOG_HD_CRITICAL"
                .Severity = "CRITICAL"
                .ValueOver = CType(ctlAramServity3.FindControl("txtCritical"), TextBox).Text
                .AlarmMethod = CType(ctlAramServity3.FindControl("rdoCriticalMethod"), RadioButtonList).SelectedValue
            End With
            Dim IntervalMinute As Integer = CType(ctlAramServity3.FindControl("txtInterval"), TextBox).Text
            Dim Active As String = IIf(CType(ctlAramServity3.FindControl("ckbActive"), CheckBox).Checked = True, "Y", "N")
            Dim CPUFileNameSendToWebServie As String = txtServerName.Text & "_CONFIG_HDD_PROCESS.xml"
            Dim CPUFileNameSendToHost As String = ddlShopName.SelectedValue & "_CONFIG_HDD_PROCESS.xml"

            '  Dim  MinorSeverity As AlarmSeverity, ByVal MajorSeverity As AlarmSeverity, ByVal CriticalSeverity As AlarmSeverity, ByVal IntervalMinute As Int16, ByVal Active As String
            Dim drDriveInfo As DataRow
            Dim dtDriveInfo As New DataTable
            dtDriveInfo.Columns.Add("DriveLetter", GetType(String))
            dtDriveInfo.Columns.Add("MinorSeverity", GetType(Engine.Config.HDDConfigENG.AlarmSeverity))
            dtDriveInfo.Columns.Add("MajorSeverity", GetType(Engine.Config.HDDConfigENG.AlarmSeverity))
            dtDriveInfo.Columns.Add("CriticalSeverity", GetType(Engine.Config.HDDConfigENG.AlarmSeverity))

            Dim arrayDrive As String() = lblText_OutDrive.Text.Split(",")

            For Each item As RadComboBoxItem In RadComboBox_Drive.Items
                Dim cbSelect As CheckBox = DirectCast(item.FindControl("cbSelect_InDrive"), CheckBox)
                If cbSelect.Checked Then
                    drDriveInfo = dtDriveInfo.NewRow
                    drDriveInfo("DriveLetter") = cbSelect.Text
                    drDriveInfo("MinorSeverity") = MinorSeverity
                    drDriveInfo("MajorSeverity") = MajorSeverity
                    drDriveInfo("CriticalSeverity") = CriticalSeverity
                    dtDriveInfo.Rows.Add(drDriveInfo)
                End If

            Next

            Dim ff As New IO.FileInfo(Server.MapPath("~/FileSetting/" & CPUFileNameSendToWebServie))
            If Directory.Exists(ff.Directory.FullName) = False Then
                Directory.CreateDirectory(ff.Directory.FullName)
            End If
            Dim clsHDDConfigENG As New HDDConfigENG
            With clsHDDConfigENG
                .SaveHDDConfigWeb(ddlShopName.SelectedItem.Text, _
                                  txtServerName.Text, _
                                  txtIpAddress.Text, _
                                  dtDriveInfo, _
                                  RepeateCheck, _
                                  IntervalMinute, _
                                  Active, "Y").Save(Server.MapPath("~/FileSetting/" & CPUFileNameSendToWebServie))

                FileCopy(Server.MapPath("~/FileSetting/" & CPUFileNameSendToWebServie), Server.MapPath("~/FileSetting/" & CPUFileNameSendToHost))

            End With
            clsHDDConfigENG = Nothing

            Dim bytes As Byte() = System.IO.File.ReadAllBytes(Server.MapPath("~/FileSetting/" & CPUFileNameSendToWebServie))
            Dim clsFaultManagementService As New FaultManagementService.FaultManagementService
            clsFaultManagementService.Url = Config.GetFaultManagementServiceURL
            clsFaultManagementService.SendConfigFileToDC(bytes, CPUFileNameSendToWebServie, txtServerName.Text)
            clsFaultManagementService.Dispose()


            'Dim pathxml As String = Server.MapPath("~/FileSetting/" & CPUFileNameSendToHost)
            'Dim ds As New DataSet
            'GridView1.DataSource = ds.ReadXml(pathxml)
            'GridView1.DataBind()

            PopulateDrive()

            Return True
        Catch ex As Exception
            FunctionEng.CreateErrorLog(ex.Message & vbNewLine & ex.StackTrace)
            Return False
        End Try
    End Function

#End Region

#Region "Event Handle"
    Protected Sub ddlShopName_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlShopName.SelectedIndexChanged
        txtServerName.Text = ""
        txtIpAddress.Text = ""
        PopulateDataCPU()
        PopulateDataRAM()
        PopulateDataHDD()
        PopulateDrive()
    End Sub


    Protected Sub btnSaveCPU_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveCPU.Click
        If txtServerName.Text = "" Then
            Config.ShowAlert("Please Input Server Name !", Me)
            Exit Sub
        End If

        If txtIpAddress.Text = "" Then
            Config.ShowAlert("Please Input IP Address !", Me)
            Exit Sub
        End If

        Dim strAlert As String = ctlAramServity1.GetAlertInputData
        If strAlert <> "" Then
            Config.ShowAlert(strAlert, Me)
            Exit Sub
        End If

        If SaveCPU() Then
            Config.ShowAlert("Save Data Complete", Me)
        Else
            Config.ShowAlert("Save Data Falied", Me)
        End If
    End Sub

    Protected Sub btnSaveRAM_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveRAM.Click
        If txtServerName.Text = "" Then
            Config.ShowAlert("Please Input Server Name !", Me)
            Exit Sub
        End If

        If txtIpAddress.Text = "" Then
            Config.ShowAlert("Please Input IP Address !", Me)
            Exit Sub
        End If

        Dim strAlert As String = ctlAramServity2.GetAlertInputData
        If strAlert <> "" Then
            Config.ShowAlert(strAlert, Me)
            Exit Sub
        End If

        If SaveRAM() Then
            Config.ShowAlert("Save Data Complete", Me)
        Else
            Config.ShowAlert("Save Data Falied", Me)
        End If
    End Sub

    Protected Sub btnSaveHDD_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveHDD.Click
        If txtServerName.Text = "" Then
            Config.ShowAlert("Please Input Server Name !", Me)
            SetDefaultDrive()

            Exit Sub
        End If

        If txtIpAddress.Text = "" Then
            Config.ShowAlert("Please Input IP Address !", Me)
            SetDefaultDrive()

            Exit Sub
        End If
        Dim checKSelectDrive As Boolean = False
        For Each item As RadComboBoxItem In RadComboBox_Drive.Items
            Dim cbSelect As CheckBox = DirectCast(item.FindControl("cbSelect_InDrive"), CheckBox)
            If cbSelect.Checked Then
                checKSelectDrive = True
                Exit For
            End If
        Next
        If checKSelectDrive = False Then
            Config.ShowAlert("Please Select Drive Later!", Me)
            SetDefaultDrive()
            Exit Sub
        End If

        Dim strAlert As String = ctlAramServity3.GetAlertInputData
        If strAlert <> "" Then
            Config.ShowAlert(strAlert, Me)
            SetDefaultDrive()

            Exit Sub
        End If

        If SaveHDD() Then
            Config.ShowAlert("Save Data Complete", Me)
        Else
            Config.ShowAlert("Save Data Falied", Me)
        End If
    End Sub



#End Region

#Region "Grid"
    'Protected Sub Mydg1_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles Mydg1.EditCommand
    '    If e.CommandName = "Edit" Then
    '        Dim lbl_Name As Label = DirectCast(e.Item.FindControl("lbl_Name"), Label)
    '        ddlDriveLater.SelectedValue = lbl_Name.Text
    '    End If

    'End Sub
#End Region




End Class
