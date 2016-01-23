Imports Engine.Configuration
Imports System.Data
Imports System.Drawing
Imports LinqDB.TABLE

Partial Class frmMSTDTACBiller
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            SetServiceList()
            SetCmdBillerType()
            txt_search.Attributes.Add("onkeypress", "return clickButton(event,'" + CmdSearch.ClientID + "') ")

            Config.SaveTransLog("คลิกเมนู : Biller", "WebConfig.frmMSTDTACBiller.aspx.Page_Load", Config.GetLoginHistoryID)
        End If
    End Sub

    Private Sub SetCmdBillerType()
        Dim lnq As New MsBillerTypeLinqDB
        Dim dt As New DataTable
        dt = lnq.GetDataList("active_status='Y'", "biller_type_name", Nothing)
        If dt.Rows.Count > 0 Then
            cmbBillerType.SetItemList(dt, "biller_type_name", "id")
        End If
        dt.Dispose()
        lnq = Nothing
    End Sub
    Private Sub SetServiceList()
        Dim eng As New MasterENG
        Dim dt As New DataTable
        dt = eng.GetBillerAllList("1=1")
        If dt.Rows.Count > 0 Then
            dgvService.DataSource = dt
            dgvService.DataBind()
        End If
        dt.Dispose()
        eng = Nothing
    End Sub

    Protected Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdSave.Click
        If Validation() = True Then
            Dim p As New MsBillerLinqDB
            Dim trans As New LinqDB.ConnectDB.TransactionDB
            p.ID = txt_id.Text
            p.GetDataByPK(txt_id.Text, trans.Trans)
            p.MS_BILLER_TYPE_ID = cmbBillerType.SelectedValue
            p.BILLER_CODE = txtBillerCode.Text
            p.BILLER_NAME = txtBillerName.Text
            'p.IMAGE_LOGO = 
            p.ACTIVE_STATUS = IIf(chk_active.Checked, "Y", "N")


            Dim uPara As CenParaDB.Common.LoginSessionPara = Engine.Common.LoginENG.GetLoginSessionPara
            Dim ret As Boolean = False
            If p.ID > 0 Then
                ret = p.UpdateByPK(uPara.USERNAME, trans.Trans)
            Else
                ret = p.InsertData(uPara.USERNAME, trans.Trans)
            End If

            If ret = True Then
                trans.CommitTransaction()

                If ctlBrowseFile1.HasFile = True Then
                    Dim WebConfigUploadPath As String = System.Configuration.ConfigurationSettings.AppSettings("WebConfigUploadPath").ToString
                    ctlBrowseFile1.SaveFile(WebConfigUploadPath, p.ID & "_" & ctlBrowseFile1.TmpFileUploadPara.FileName)
                End If

                Config.ShowAlert("Save Complete.", Me)
                SetServiceList()
            Else
                trans.RollbackTransaction()
                Config.ShowAlert(p.ErrorMessage, Me)
            End If

            Config.SaveTransLog("บันทึกข้อมูล : Biller", "WebConfig.frmMSTDTACBiller.aspx.CmdSave_Click", Config.GetLoginHistoryID)

        End If
    End Sub

    Private Function Validation() As Boolean
        Dim ret As Boolean = True
        'If txt_item_code.Text.Trim = "" Then
        '    ret = False
        '    Config.ShowAlert("Please input Item Code", Me, txt_item_code.ClientID)
        'ElseIf txt_item_name_english.Text.Trim = "" Then
        '    Config.ShowAlert("Please input Item Name in English", Me, txt_item_name_english.ClientID)
        '    ret = False
        'ElseIf txt_item_name_thai.Text.Trim = "" Then
        '    Config.ShowAlert("Please input Item Name in Thai", Me, txt_item_name_thai.ClientID)
        '    ret = False
        'ElseIf txt_appointment_queue_no_min.Text.Trim = "" Then
        '    Config.ShowAlert("Please input Appointment Queue No Min", Me, txt_appointment_queue_no_min.ClientID)
        '    ret = False
        'ElseIf txt_appointment_queue_no_max.Text.Trim = "" Then
        '    Config.ShowAlert("Please input Appointment Queue No Max", Me, txt_appointment_queue_no_max.ClientID)
        '    ret = False

        'ElseIf txt_item_order.Text.Trim = "" Then
        '    Config.ShowAlert("Please input Item Order", Me, txt_item_order.ClientID)
        '    ret = False
        'ElseIf txt_queue.Text.Trim = "" Then
        '    Config.ShowAlert("Please input Text Queue", Me, txt_queue.ClientID)
        '    ret = False
        'ElseIf txt_standard_handing_time.Text.Trim = "" Then
        '    Config.ShowAlert("Please input Standard Handling time", Me, txt_standard_handing_time.ClientID)
        '    ret = False
        '    'ElseIf txt_colorcode.Text = "" Then
        '    '    Config.ShowAlert("Please Select Color !!", Me, txt_colorcode.ClientID)
        '    '    ret = False
        'ElseIf txt_standard_waiting_time.Text.Trim = "" Then
        '    Config.ShowAlert("Please input Standard Waiting time", Me, txt_standard_waiting_time.ClientID)
        '    ret = False
        'ElseIf txt_standard_waiting_time.Text < 0 Then
        '    Config.ShowAlert("Standard Waiting time < 0,Please Try Again !!", Me, txt_standard_waiting_time.ClientID)
        '    ret = False
        'ElseIf txt_standard_handing_time.Text < 0 Then
        '    Config.ShowAlert("Standard Handling time < 0,Please Try Again !!'", Me, txt_standard_handing_time.ClientID)
        '    ret = False
        'ElseIf txt_appointment_queue_no_min.Text >= txt_appointment_queue_no_max.Text Then
        '    Config.ShowAlert("Max Appointment Queue No < Min Appointment Queue No ,Please Try Again !!", Me, txt_appointment_queue_no_min.ClientID)
        '    ret = False
        'ElseIf txt_item_order.Text < 0 Then
        '    Config.ShowAlert("Item Order < 0,Please Try Again !!", Me, txt_item_order.ClientID)
        '    ret = False
        'Else
        '    Dim eng As New MasterENG
        '    If eng.CheckDuplicateMasterServiceByItemCode(txt_id.Text, txt_item_code.Text) = True Then
        '        Config.ShowAlert("Item Code is duplicate", Me, txt_item_code.ClientID)
        '        ret = False
        '    ElseIf eng.CheckDuplicateMasterServiceByItemNameEng(txt_id.Text, txt_item_name_english.Text) = True Then
        '        Config.ShowAlert("Item Name English is duplicate", Me, txt_item_name_english.ClientID)
        '        ret = False
        '    ElseIf eng.CheckDuplicateMasterServiceByItemNameTH(txt_id.Text, txt_item_name_thai.Text) = True Then
        '        Config.ShowAlert("Item Name Thai is duplicate", Me, txt_item_name_thai.ClientID)
        '        ret = False
        '    ElseIf eng.CheckDuplicateMasterServiceByTextQueue(txt_id.Text, txt_queue.Text) = True Then
        '        Config.ShowAlert("Text Queue is duplicate", Me, txt_queue.ClientID)
        '        ret = False
        '    ElseIf eng.CheckDuplicateMasterServiceByItemOrder(txt_id.Text, txt_item_order.Text) = True Then
        '        Config.ShowAlert("Item Order is duplicate", Me, txt_item_order.ClientID)
        '        ret = False
        '    End If
        '    eng = Nothing
        'End If

        Return ret
    End Function


    Private Sub ClearData()
        'txt_id.Text = "0"
        'txt_item_code.Text = ""
        'txt_item_name_english.Text = ""
        'txt_item_name_thai.Text = ""
        'txt_standard_handing_time.Text = ""
        'txt_standard_waiting_time.Text = ""
        'txt_item_order.Text = ""
        'txt_queue.Text = ""
        'txt_colorcode.Text = ""
        'txt_color.BackColor = Color.White
        'chk_active.Checked = True
        'txt_appointment_queue_no_max.Text = ""
        'txt_appointment_queue_no_min.Text = ""
        'txt_item_code.Enabled = True
    End Sub

    Protected Sub CmdClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdClear.Click
        ClearData()
        Config.SaveTransLog("เคลียร์ข้อมูล : Biller", "WebConfig.frmMSTDTACBiller.aspx.CmdClear_Click", Config.GetLoginHistoryID)

    End Sub

    Protected Sub CmdSearch_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSearch.Click
        dgvService.CurrentPageIndex = 0

        Dim eng As New MasterENG
        Dim dt As New DataTable
        Dim wh As String = "1=1"
        If txt_search.Text.Trim <> "" Then
            wh = " biller_code like '%" & txt_search.Text.Trim & "%'"
            wh += " or biller_name like '%" & txt_search.Text.Trim & "%'"
            'wh += " or item_name_th like '%" & txt_search.Text.Trim & "%'"
        End If
        dt = eng.GetBillerAllList(wh)
        dgvService.DataSource = dt
        dgvService.DataBind()

        dt.Dispose()
        eng = Nothing

        Config.SaveTransLog("ค้นหาข้อมูล : Biller", "WebConfig.frmMSTDTACBiller.aspx.CmdSearch_Click", Config.GetLoginHistoryID)

    End Sub

    Protected Sub dgvService_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgvService.EditCommand
        If e.CommandName = "Edit" Then
            Dim lbl_id As Label = DirectCast(e.Item.FindControl("lbl_id"), Label)

            Dim p As New LinqDB.TABLE.MsBillerLinqDB
            'Dim eng As New MasterENG
            p = p.GetDataByPK(lbl_id.Text, Nothing) 'eng.GetMasterServicePara(lbl_id.Text)
            If p.ID <> 0 Then
                txt_id.Text = p.ID
                txtBillerCode.Text = p.BILLER_CODE
                txtBillerName.Text = p.BILLER_NAME
                cmbBillerType.SelectedValue = p.MS_BILLER_TYPE_ID
                chk_active.Checked = IIf(p.ACTIVE_STATUS = "Y", True, False)
            End If
            p = Nothing
            SetServiceList()
        End If
    End Sub

    Protected Sub dgvService_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgvService.PageIndexChanged
        dgvService.CurrentPageIndex = e.NewPageIndex
        SetServiceList()
    End Sub
End Class
