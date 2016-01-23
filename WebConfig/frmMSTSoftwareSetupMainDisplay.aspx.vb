Imports System
Imports System.Data
Imports System.Data.OleDb
Imports CenLinqDB.TABLE

Partial Class frmMSTSoftwareSetupMainDisplay
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            
            Dim uPara As CenParaDB.Common.UserProfilePara = Engine.Common.LoginENG.GetLogOnUser()
            If uPara.LOGIN_HISTORY_ID = 0 Then
                Session.RemoveAll()
                Me.Response.Redirect("frmlogin.aspx")
            Else
                'Binddata(Request("ShopID"))
                ClearTextBox()
                txt_date.Text = DateTime.Now.ToString("dd/MM/yyyy", New Globalization.CultureInfo("en-US"))
            End If
            uPara = Nothing
            Config.SaveTransLog("คลิกเมนู : Software Setup >> Main Display", "AisWebConfig.frmMSTSoftwareSetupMainDisplay.aspx.Page_Load", Config.GetLoginHistoryID)

        End If
    End Sub

    Private Sub ClearTextBox()
        lbl_upload4.Text = ""
        ctlBrowseFileVdo1.ClearFile()
        opt_now.Checked = False
        opt_schedule.Checked = True
    End Sub

    Protected Sub btn_clear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_clear.Click
        ClearTextBox()
        Config.SaveTransLog("เคลียร์ข้อมูล : Software Setup >> Main Display", "AisWebConfig.frmMSTSoftwareSetupMainDisplay.aspx.btn_clear_Click", Config.GetLoginHistoryID)
    End Sub

    Protected Sub btn_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_save.Click
        If ctlShopSelected1.SelectedShop.Rows.Count = 0 Then
            Config.ShowAlert("Please select Shop", Me)
            Exit Sub
        End If

        If opt_schedule.Checked Then
            If txt_date.Text <> "" Then
                If Engine.Common.FunctionEng.cStrToDate2(txt_date.Text) <= Now.Date Then
                    Config.ShowAlert("Date Schedule Less Than Today,Please Select Again !!", Me)
                    Exit Sub
                End If
            End If
        End If

        If ctlBrowseFileVdo1.HasFile = True Then
            If ctlBrowseFileVdo1.TmpFileUploadPara.FileExtent.ToUpper <> ".MOV" Then
                Config.ShowAlert("Video file Extention is .MOV only", Me)
                Exit Sub
            End If
        Else
            Config.ShowAlert("Please Select Video File", Me)
            Exit Sub
        End If

        Dim uPara As CenParaDB.Common.UserProfilePara = Config.GetLogOnUser
        Dim ret As Boolean = False
        Dim vDateSchedule As DateTime = DateTime.Now
        If opt_now.Checked = True Then
            ret = True 'SavedataNow()
        ElseIf opt_schedule.Checked = True Then
            DeleteCfg()
            ret = SaveDataSchedule(uPara)
            vDateSchedule = Engine.Common.FunctionEng.cStrToDate2(txt_date.Text)
        End If

        If ret = True Then
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            If ctlBrowseFileVdo1.HasFile = True Then
                Dim FileByteID As Long = SaveVDOFile(uPara.USERNAME, vDateSchedule, ctlBrowseFileVdo1, trans)
                If FileByteID > 0 Then
                    ret = SaveShopFileSchedule(FileByteID, vDateSchedule, uPara.USERNAME, trans)
                End If
            End If

            If ret = True Then
                trans.CommitTransaction()
            Else
                trans.RollbackTransaction()
            End If
        End If
        uPara = Nothing
        Config.SaveTransLog("บันทึกข้อมูล : Software Setup >> Main Display", "AisWebConfig.frmMSTSoftwareSetupMainDisplay.aspx.btn_save_Click", Config.GetLoginHistoryID)

        If ret = True Then
            Config.ShowAlert("Save Data Complete", Me)
        End If

    End Sub

    Private Function DeleteCfg() As Boolean

        Dim ret As Boolean = True
        Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
        Try
            Dim dt As New DataTable
            dt = ctlShopSelected1.SelectedShop
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    Dim shopid As String = dr("id")
                    Dim eng As New Engine.Configuration.ShopEventScheduleSettingENG
                    eng.DeleteCfgMainDisplay(shopid, trans)
                    eng.DeleteCfgShopFileScheduleMainDisplay(shopid, trans)
                Next
            End If

            trans.CommitTransaction()
        Catch ex As Exception
            trans.RollbackTransaction()
            ret = False
        End Try
        Return ret
    End Function

    Private Function SaveDataSchedule(ByVal uPara As CenParaDB.Common.UserProfilePara) As Boolean
        Dim ret As Boolean = False
        If Validation() = True Then
            Dim dt As New DataTable
            dt = ctlShopSelected1.SelectedShop
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    Dim p As New CenParaDB.TABLE.TbCfgSwSchedMaindisplayCenParaDB
                    p.SHOP_ID = dr("id")
                    p.EVENT_DATE = Engine.Common.FunctionEng.cStrToDate2(txt_date.Text)
                    p.EVENT_STATUS = "1"

                    Dim eng As New Engine.Configuration.ShopEventScheduleSettingENG
                    ret = eng.SaveCfgSchedMainDisplay(p, uPara.USERNAME)
                    If ret = False Then
                        Config.ShowAlert(eng.ErrMessage, Me)
                        eng = Nothing
                        Exit For
                    End If
                    eng = Nothing
                Next
            End If
            dt.Dispose()
        End If

        Return ret
    End Function

    'Private Function SavedataNow() As Boolean
    '    Dim ret As Boolean = False
    '    If Validation() = True Then
    '        Dim ShowError As String = ""

    '        Dim dt As New DataTable
    '        dt = ctlShopSelected1.SelectedShop
    '        If dt.Rows.Count > 0 Then
    '            For Each dr As DataRow In dt.Rows
    '                Dim eng As New Engine.Configuration.ShopSettingENG
    '                ret = eng.SaveShopTbSetting(dr("id"), txt_Retardation1.Text.Trim, "m_retardation_vdo")
    '                If ret = False Then
    '                    ShowError += eng.ErrorMessage
    '                End If
    '                eng = Nothing
    '            Next
    '        End If
    '        dt.Dispose()

    '        If ShowError.Trim <> "" Then
    '            Config.ShowAlert(ShowError, Me)
    '            ret = False
    '        End If
    '    End If

    '    Return ret
    'End Function

    Private Function Validation() As Boolean
        Dim ret As Boolean = True
        If ValidateVDOFile() = False Then
            ret = False
            Config.ShowAlert("Video file Extention is .MOV only ", Me)
        ElseIf opt_schedule.Checked = True Then
            If txt_date.Text.Trim = "" Then
                ret = False
                Config.ShowAlert("Please select event date ", Me)
            End If
        End If

        Return ret
    End Function

    Private Function ValidateVDOFile() As Boolean
        Dim ret As Boolean = True
        If ctlBrowseFileVdo1.HasFile = True Then
            If ctlBrowseFileVdo1.TmpFileUploadPara.FileExtent.ToUpper <> ".MOV" Then
                Return False
            End If
        End If
       

        Return True
    End Function

    Private Sub Binddata(ByVal ShopID As String)
        Dim eng As New Engine.Configuration.ShopSettingENG
        Dim dt As New DataTable
        dt = eng.GetTbSetting(ShopID)
        If dt.Rows.Count > 0 Then
            ' FillData(txt_Retardation1, "m_retardation_vdo", dt)
        End If
        dt.Dispose()
        eng = Nothing

        Dim mEng As New Engine.Configuration.MasterENG
        ctlShopSelected1.ClearSelectedShop()
        ctlShopSelected1.SetSelectedShop(mEng.GetShopList("sh.id='" & ShopID & "'"))
        mEng = Nothing
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


    Private Function SaveVDOFile(ByVal UserName As String, ByVal vDateNow As DateTime, ByVal FileUpload As UserControls_ctlBrowseFileStream, ByVal trans As CenLinqDB.Common.Utilities.TransactionDB) As Long
        Dim ret As Long = 0
        Try
            Dim EventDate As New Date(1, 1, 1)
            If opt_now.Checked = True Then
                EventDate = New Date(vDateNow.Year, vDateNow.Month, vDateNow.Day)
            ElseIf opt_schedule.Checked = True Then
                EventDate = Engine.Common.FunctionEng.cStrToDate2(txt_date.Text)
            End If

            Dim p As New CenParaDB.TABLE.TbShopFileByteCenParaDB
            p.TARGET_TYPE = "1"
            p.FOLDER_NAME = System.Configuration.ConfigurationManager.AppSettings("WebConfigUploadPath") & "\" & vDateNow.ToString("yyyyMMdd", New Globalization.CultureInfo("en-US"))
            p.FILE_NAME = DateTime.Now.ToString("yyyyMMddHHmmssfff", New Globalization.CultureInfo("en-US")) & FileUpload.TmpFileUploadPara.FileExtent
            'p.FILE_BYTE = FileUpload.TmpFileUploadPara.TmpFileByte
            p.CONVERT_STATUS = "Y"

            Dim eng As New Engine.Configuration.ShopEventScheduleSettingENG
            ret = eng.SaveShopFileByte(p, UserName, trans)
            eng = Nothing

            If ret > 0 Then
                If IO.Directory.Exists(p.FOLDER_NAME) = False Then
                    IO.Directory.CreateDirectory(p.FOLDER_NAME)
                End If
                FileUpload.SaveFile(p.FOLDER_NAME, p.FILE_NAME)
                FileUpload.ClearFile()
            End If
            p = Nothing
        Catch ex As Exception
            ret = 0
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Alert", "alert('Upload File Error !!," & ex.Message & "');", True)
        End Try

        Return ret
    End Function

    Private Function SaveShopFileSchedule(ByVal ShopFileByteID As Long, ByVal EventDate As DateTime, ByVal LoginName As String, ByVal trans As CenLinqDB.Common.Utilities.TransactionDB) As Boolean
        Dim ret As Boolean = False
        Dim dt As New DataTable
        dt = ctlShopSelected1.SelectedShop
        If dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                Dim eng As New Engine.Configuration.ShopEventScheduleSettingENG
                Dim p As New CenParaDB.TABLE.TbShopFileScheduleCenParaDB
                p.SHOP_ID = dr("id")
                p.EVENT_DATE = EventDate
                p.TRANSFER_STATUS = "1"
                p.TB_SHOP_FILE_BYTE_ID = ShopFileByteID
                ret = eng.SaveShopFileSchedule(p, LoginName, trans)
                If ret = False Then
                    Config.ShowAlert(eng.ErrMessage, Me)
                    eng = Nothing
                    Exit For
                End If
                eng = Nothing
                p = Nothing
            Next
        End If

        Return ret
    End Function




#Region "Tab VIEW"
    Private Function GetDataTabView(ByVal wh As String) As DataTable
        'Tab View
        Dim dt As New DataTable
        dt.Columns.Add("id")
        dt.Columns.Add("shop_size")
        dt.Columns.Add("shop_name")
        dt.Columns.Add("shop_code")
        dt.Columns.Add("main_retardation_video")

        Dim uPara As CenParaDB.Common.UserProfilePara = Config.GetLogOnUser
        Dim lEng As New Engine.Common.LoginENG
        Dim shDt As New DataTable
        shDt = lEng.GetShopListByUser(uPara.USERNAME)
        If shDt.Rows.Count > 0 Then
            For Each shDr As DataRow In shDt.Rows
                Dim shTrans As ShLinqDB.Common.Utilities.TransactionDB = Engine.Common.FunctionEng.GetShTransction(shDr("id"), "frmMSTSoftwareSetupMainDisplay_GetDataTabView")
                If shTrans.Trans IsNot Nothing Then
                    Dim RetardationVideo As String = Engine.Common.FunctionEng.GetShopConfig("m_retardation_vdo", shTrans)

                    Dim dr As DataRow = dt.NewRow
                    dr("id") = shDr("id")
                    dr("shop_size") = shDr("shop_size")
                    dr("shop_name") = shDr("shop_name_en")
                    dr("shop_code") = shDr("shop_code")
                    dr("main_retardation_video") = RetardationVideo
                    dt.Rows.Add(dr)
                End If
            Next
        End If

        If wh <> "" Then
            dt.DefaultView.RowFilter = wh
        End If
        shDt.Dispose()
        lEng = Nothing
        uPara = Nothing

        Return dt.DefaultView.ToTable
    End Function

    Private Sub BindTabView(ByVal wh As String)
        Dim dt As New DataTable
        dt = GetDataTabView(wh)
        If dt.DefaultView.Count > 0 Then
            dgvdetail.DataSource = dt.DefaultView.ToTable
            dgvdetail.DataBind()
        Else
            dgvdetail.DataSource = Nothing
            dgvdetail.DataBind()
            lblErrorMessage.Visible = True
        End If
        dt.Dispose()
    End Sub

    Protected Sub TabContainer1_ActiveTabChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabContainer1.ActiveTabChanged
        If TabContainer1.ActiveTabIndex = 1 Then
            BindTabView("1=1")
        End If
    End Sub

    Dim Valddshop_size As String = ""
    Dim Valddshop_name As String = ""
    Dim Valddmain_retardation_video As String = ""

#Region "TabView Filter Dropdownlist"
    Protected Sub ddshop_size_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim dd As DropDownList = DirectCast(sender, DropDownList)
            If dd.SelectedItem.Text <> "All" Then
                Valddshop_size = dd.SelectedValue.ToString
                BindTabView("shop_size = '" & dd.SelectedItem.Text & "'")
            Else
                Valddshop_size = ""
                BindTabView("")
            End If
            Config.SaveTransLog("ค้นหาข้อมูลตาม Shop Size : Software Setup >> Main Display", "AisWebConfig.frmMSTSoftwareSetupMainDisplay.aspx.ddshop_size_SelectedIndexChanged", Config.GetLoginHistoryID)

        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Alert", "alert('" & ex.Message & "');", True)
            Exit Sub
        End Try
    End Sub

    Protected Sub ddshop_name_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim dd As DropDownList = DirectCast(sender, DropDownList)
            If dd.SelectedItem.Text <> "All" Then
                Valddshop_name = dd.SelectedValue.ToString
                BindTabView("shop_name = '" & dd.SelectedItem.Text & "'")
            Else
                Valddshop_name = ""
                BindTabView("")
            End If
            Config.SaveTransLog("ค้นหาข้อมูลตาม Shop Name : Software Setup >> Main Display", "AisWebConfig.frmMSTSoftwareSetupMainDisplay.aspx.ddshop_name_SelectedIndexChanged", Config.GetLoginHistoryID)

        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Alert", "alert('" & ex.Message & "');", True)
            Exit Sub
        End Try
    End Sub

    Protected Sub ddmain_retardation_video_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim dd As DropDownList = DirectCast(sender, DropDownList)
        If dd.SelectedItem.Text <> "All" Then
            Valddmain_retardation_video = dd.SelectedValue.ToString
            BindTabView("main_retardation_video = '" & dd.SelectedItem.Text & "'")
        Else
            Valddmain_retardation_video = ""
            BindTabView("")
        End If
        Config.SaveTransLog("ค้นหาข้อมูลตามความหน่วงของไฟล์ video(second) : Software Setup >> Main Display", "AisWebConfig.frmMSTSoftwareSetupMainDisplay.aspx.ddmain_retardation_video_SelectedIndexChanged", Config.GetLoginHistoryID)

    End Sub

    Protected Sub lbl_shop_Name_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            TabContainer1.ActiveTabIndex = "0"
            Dim lbl_shop_Name As LinkButton = DirectCast(sender, LinkButton)
            Dim gvr As DataGridItem = DirectCast(lbl_shop_Name.NamingContainer, DataGridItem)
            Dim lblID As Label = gvr.FindControl("lbl_id")
            Binddata(lblID.Text)
            Config.SaveTransLog("เลือกข้อมูล - " & lbl_shop_Name.Text & " : Software Setup >> Main Display", "AisWebConfig.frmMSTSoftwareSetupMainDisplay.aspx.lbl_shop_Name_Click", Config.GetLoginHistoryID)

        Catch ex As Exception

        End Try
    End Sub
#End Region
    Protected Sub dgvdetail_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgvdetail.ItemCreated
        If e.Item.ItemType = DataControlRowType.Header Then
            Dim uPara As CenParaDB.Common.UserProfilePara = Config.GetLogOnUser
            Dim lEng As New Engine.Common.LoginENG
            Dim shDt As New DataTable
            shDt = lEng.GetShopListByUser(uPara.USERNAME)
            If shDt.Rows.Count > 0 Then
                'Shop Size
                BindDataToDropdownlist(shDt, DirectCast(e.Item.FindControl("ddshop_size"), DropDownList), "shop_size")
                If Valddshop_size <> "" Then
                    Dim ddl As New DropDownList
                    ddl = DirectCast(e.Item.FindControl("ddshop_size"), DropDownList)
                    If Not ddl Is Nothing Then
                        ddl.SelectedValue = Valddshop_size
                    End If
                End If


                'Shop Name
                BindDataToDropdownlist(shDt, DirectCast(e.Item.FindControl("ddshop_name"), DropDownList), "shop_name_en")
                If Valddshop_name <> "" Then
                    Dim ddl As New DropDownList
                    ddl = DirectCast(e.Item.FindControl("ddshop_name"), DropDownList)
                    If Not ddl Is Nothing Then
                        ddl.SelectedValue = Valddshop_name
                    End If
                End If


                Dim dt As New DataTable
                dt = GetDataTabView("")
                If dt.Rows.Count > 0 Then
                    BindDataToDropdownlist(dt, DirectCast(e.Item.FindControl("ddmain_retardation_video"), DropDownList), "main_retardation_video")
                    If Valddmain_retardation_video <> "" Then
                        Dim ddl As New DropDownList
                        ddl = DirectCast(e.Item.FindControl("ddmain_retardation_video"), DropDownList)
                        If Not ddl Is Nothing Then
                            ddl.SelectedValue = Valddmain_retardation_video
                        End If
                    End If
                End If
                dt.Dispose()
            End If
        End If
    End Sub

    Private Sub BindDataToDropdownlist(ByVal dt As DataTable, ByVal ddl As DropDownList, ByVal DataTxtFld As String)
        Dim dtDDl As New DataTable
        dtDDl = dt.DefaultView.ToTable(True, DataTxtFld)
        dtDDl.DefaultView.Sort = DataTxtFld

        If dtDDl.Rows.Count > 0 Then
            Dim dr As DataRow = dtDDl.NewRow
            dr(DataTxtFld) = "All"
            dtDDl.Rows.InsertAt(dr, 0)
            ddl.DataTextField = DataTxtFld
            ddl.DataSource = dtDDl
            ddl.DataBind()
        End If
      
        dtDDl.Dispose()
    End Sub
#End Region



End Class
