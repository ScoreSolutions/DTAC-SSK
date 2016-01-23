Imports System
Imports System.Data
Imports System.Data.OleDb

Partial Class frmMSTSoftwareDTACSetupKiosk
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ClearTextBox()
            Binddata()
            'txt_date.Text = DateTime.Now.ToString("dd/MM/yyyy", New Globalization.CultureInfo("en-US"))
            Config.SaveTransLog("คลิกเมนู : Software Setup >> Kiosk", "AisWebConfig.frmMSTSoftwareSetupKiosk.aspx.Page_Load", Config.GetLoginHistoryID)
        End If
    End Sub

    Private Sub ClearTextBox()
        'txtSoonestArrivalAppointment.Text = ""
        'txtLatestArrivalAppointment.Text = ""
        'txtMaxServiceAppointment.Text = ""
        'txtPreBookingPeriod.Text = ""
        'txtCancelBeforeReservationTime.Text = ""
        'txtMaximumServicePerTime.Text = ""
        'txt_Length_of_Mobile_No.Text = ""
        'txtNoPrint.Text = ""
        'txtRefreshWT.Text = ""
        'txt_Allowable1.Text = ""
        'txt_Allowable2.Text = ""
        'txt_Allowable3.Text = ""
        'txt_Allowable4.Text = ""
    End Sub

    Protected Sub btn_clear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_clear.Click
        ClearTextBox()
        Config.SaveTransLog("เคลียร์ข้อมูล : Software Setup >> Kiosk", "AisWebConfig.frmMSTSoftwareSetupKiosk.aspx.btn_clear_Click", Config.GetLoginHistoryID)
    End Sub

    Protected Sub btn_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_save.Click
        
        Dim vDateSchedule As DateTime = DateTime.Now
        Dim uPara As CenParaDB.Common.UserProfilePara = Config.GetLogOnUser
        Dim ret As Boolean = False
        ret = SavedataNow()
        uPara = Nothing
        If ret = True Then
            Config.ShowAlert("Save Complete", Me)
        End If
        Config.SaveTransLog("บันทึกข้อมูล : Software Setup >> Kiosk", "AisWebConfig.frmMSTSoftwareSetupKiosk.aspx.btn_save_Click", Config.GetLoginHistoryID)

    End Sub

    

    'Private Function SaveShopFileSchedule(ByVal ShopFileByteID As Long, ByVal EventDate As DateTime, ByVal LoginName As String, ByVal trans As CenLinqDB.Common.Utilities.TransactionDB) As Boolean
    '    Dim ret As Boolean = False
    '    Dim dt As New DataTable
    '    dt = ctlShopSelected1.SelectedShop
    '    If dt.Rows.Count > 0 Then
    '        For Each dr As DataRow In dt.Rows
    '            Dim eng As New Engine.Configuration.ShopEventScheduleSettingENG
    '            Dim p As New CenParaDB.TABLE.TbShopFileScheduleCenParaDB
    '            p.SHOP_ID = dr("id")
    '            p.EVENT_DATE = EventDate
    '            p.TRANSFER_STATUS = "1"
    '            p.TB_SHOP_FILE_BYTE_ID = ShopFileByteID

    '            ret = eng.SaveShopFileSchedule(p, LoginName, trans)
    '            If ret = False Then
    '                Config.ShowAlert(eng.ErrMessage, Me)
    '                eng = Nothing
    '                p = Nothing
    '                Exit For
    '            End If
    '            eng = Nothing
    '            p = Nothing
    '        Next
    '    End If

    '    Return ret
    'End Function

    'Private Function SaveDataSchedule(ByVal uPara As CenParaDB.Common.UserProfilePara) As Boolean
    '    Dim ret As Boolean = False
    '    If Validation() = True Then
    '        Dim dt As New DataTable
    '        dt = ctlShopSelected1.SelectedShop
    '        If dt.Rows.Count > 0 Then
    '            For Each dr As DataRow In dt.Rows
    '                Dim ShopID As String = dr("id")

    '                Dim p As New CenParaDB.TABLE.TbCfgSwSchedKioskCenParaDB
    '                p.SHOP_ID = ShopID
    '                p.K_BEFORE = txtSoonestArrivalAppointment.Text.Trim
    '                p.K_LATE = txtLatestArrivalAppointment.Text.Trim
    '                p.K_MAX_APPOINTMENT = txtMaxServiceAppointment.Text.Trim
    '                p.K_BEFORE_APP = txtPreBookingPeriod.Text.Trim
    '                p.K_CANCEL = txtCancelBeforeReservationTime.Text.Trim
    '                p.K_DISABLE = txtNoPrint.Text.Trim
    '                p.K_SERVE = txtMaximumServicePerTime.Text.Trim
    '                p.K_REFRESH = txtRefreshWT.Text.Trim
    '                p.K_LEN = txt_Length_of_Mobile_No.Text.Trim
    '                p.K_MOBILE1 = txt_Allowable1.Text.Trim
    '                p.K_MOBILE2 = txt_Allowable2.Text.Trim
    '                p.K_MOBILE3 = txt_Allowable3.Text.Trim
    '                p.K_MOBILE4 = txt_Allowable4.Text.Trim
    '                p.EVENT_STATUS = "1"

    '                Dim eng As New Engine.Configuration.ShopEventScheduleSettingENG
    '                ret = eng.SaveCfgSchedKiosk(p, uPara.USERNAME)
    '                eng = Nothing
    '            Next
    '        End If
    '    End If

    '    Return ret
    'End Function

    Private Function SavedataNow() As Boolean
        Dim ret As Boolean = False
        If Validation() = True Then
            Dim ShowError As String = ""


            Dim eng As New ShopSettingENG
            ret = eng.SaveShopTbSetting(txtCameraIndex.Text.Trim, "CaptureCameraIndex")
            If ret = False Then
                ShowError += eng.ErrorMessage
            End If

            ret = eng.SaveShopTbSetting(txtCaptureWidth.Text.Trim, "CaptureWidth")
            If ret = False Then
                ShowError += eng.ErrorMessage
            End If

            ret = eng.SaveShopTbSetting(txtCaptureHeight.Text.Trim, "CaptureHeight")
            If ret = False Then
                ShowError += eng.ErrorMessage
            End If

            ret = eng.SaveShopTbSetting(txtCaptureResolution.Text.Trim, "CaptureResolution")
            If ret = False Then
                ShowError += eng.ErrorMessage
            End If

            ret = eng.SaveShopTbSetting(IIf(chkCaptureEnable.Checked, "Y", "N"), "CaptureEnable")
            If ret = False Then
                ShowError += eng.ErrorMessage
            End If

            ret = eng.SaveShopTbSetting(txtWaitTimeScreenSaver.Text.Trim, "WaitTimeScreenSaver")
            If ret = False Then
                ShowError += eng.ErrorMessage
            End If

            ret = eng.SaveShopTbSetting(txtWaitTimeSessionExpired.Text.Trim, "WaitTimeSessionExpired")
            If ret = False Then
                ShowError += eng.ErrorMessage
            End If

            ret = eng.SaveShopTbSetting(txtWaitTimeThankYou.Text.Trim, "WaitTimeThankYou")
            If ret = False Then
                ShowError += eng.ErrorMessage
            End If


            ret = eng.SaveShopTbSetting(txtWaitTimeForPayment.Text.Trim, "WaitTimeForPayment")
            If ret = False Then
                ShowError += eng.ErrorMessage
            End If

            ret = eng.SaveShopTbSetting(txtBranchTH.Text.Trim, "BranchTH")
            If ret = False Then
                ShowError += eng.ErrorMessage
            End If

            ret = eng.SaveShopTbSetting(txtBranchEN.Text.Trim, "BranchEN")
            If ret = False Then
                ShowError += eng.ErrorMessage
            End If

            ret = eng.SaveShopTbSetting(txtBranchCode.Text.Trim, "BranchCode")
            If ret = False Then
                ShowError += eng.ErrorMessage
            End If

            ret = eng.SaveShopTbSetting(txtLocationCode.Text.Trim, "LocationCode")
            If ret = False Then
                ShowError += eng.ErrorMessage
            End If

            ret = eng.SaveShopTbSetting(txtLocationName.Text.Trim, "LocationName")
            If ret = False Then
                ShowError += eng.ErrorMessage
            End If

            If ShowError.Trim <> "" Then
                Config.ShowAlert(ShowError, Me)
                ret = False
            End If
        End If

        Return ret
    End Function

    

    Private Function Validation() As Boolean
        Dim ret As Boolean = True
        'If txtSoonestArrivalAppointment.Text.Trim = "" Then
        '    ret = False
        '    Config.ShowAlert("Please input Soonest Arrival for Appointment", Me, txtSoonestArrivalAppointment.ClientID)
        'ElseIf txtLatestArrivalAppointment.Text.Trim = "" Then
        '    ret = False
        '    Config.ShowAlert("Please input Latest Arrival for Appointment ", Me, txtLatestArrivalAppointment.ClientID)
        'ElseIf txtMaxServiceAppointment.Text.Trim = "" Then
        '    ret = False
        '    Config.ShowAlert("Please input Maximum Services Appointment per time  ", Me, txtMaxServiceAppointment.ClientID)
        'ElseIf txtPreBookingPeriod.Text.Trim = "" Then
        '    ret = False
        '    Config.ShowAlert("Please input Pre-booking Period   ", Me, txtPreBookingPeriod.ClientID)
        'ElseIf txtCancelBeforeReservationTime.Text.Trim = "" Then
        '    ret = False
        '    Config.ShowAlert("Please input Cancel before Reservation Time ", Me, txtCancelBeforeReservationTime.ClientID)
        'ElseIf txtNoPrint.Text.Trim = "" Then
        '    ret = False
        '    Config.ShowAlert("Please input No Print is W.T if more than ", Me, txtNoPrint.ClientID)
        'ElseIf txtMaximumServicePerTime.Text.Trim = "" Then
        '    ret = False
        '    Config.ShowAlert("Please input Maximum Services chosen per Time ", Me, txtMaximumServicePerTime.ClientID)
        'ElseIf txtRefreshWT.Text.Trim = "" Then
        '    ret = False
        '    Config.ShowAlert("Please input Refresh W.T every ", Me, txtRefreshWT.ClientID)
        'End If

        Return ret
    End Function

    Private Sub Binddata()
        Dim eng As New ShopSettingENG
        Dim dt As New DataTable
        dt = eng.GetCfConfiguration()
        If dt.Rows.Count > 0 Then
            FillData(txtCameraIndex, "CaptureCameraIndex", dt)
            FillData(txtCaptureWidth, "CaptureWidth", dt)
            FillData(txtCaptureHeight, "CaptureHeight", dt)
            FillData(txtCaptureResolution, "CaptureResolution", dt)

            dt.DefaultView.RowFilter = "config_name = 'CaptureEnable'"
            If dt.DefaultView.Count > 0 Then
                chkCaptureEnable.Checked = IIf(dt.DefaultView(0)("config_value").ToString = "Y", True, False)
            End If
            dt.DefaultView.RowFilter = ""

            FillData(txtWaitTimeScreenSaver, "WaitTimeScreenSaver", dt)
            FillData(txtWaitTimeSessionExpired, "WaitTimeSessionExpired", dt)
            FillData(txtWaitTimeThankYou, "WaitTimeThankYou", dt)
            FillData(txtWaitTimeForPayment, "WaitTimeForPayment", dt)
            FillData(txtBranchTH, "BranchTH", dt)
            FillData(txtBranchEN, "BranchEN", dt)
            FillData(txtBranchCode, "BranchCode", dt)
            FillData(txtLocationCode, "LocationCode", dt)
            FillData(txtLocationName, "LocationName", dt)
        End If
        dt.Dispose()
        eng = Nothing
    End Sub

    Private Sub FillData(ByVal txtBox As TextBox, ByVal ConfigName As String, ByVal dt As DataTable)
        If dt.Rows.Count > 0 Then
            dt.DefaultView.RowFilter = "config_name = '" & ConfigName & "'"
            If dt.DefaultView.Count > 0 Then
                txtBox.Text = dt.DefaultView(0)("config_value")
            Else
                Config.ShowAlert("Config value not found : " & ConfigName, Me)
            End If
        End If
    End Sub




End Class
