Imports System.Configuration
Imports System.Data

Partial Class TemplateMaster
    Inherits System.Web.UI.MasterPage
    Dim version As String = System.Configuration.ConfigurationManager.AppSettings("version")
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim uPara As New CenParaDB.Common.UserProfilePara
            uPara = Engine.Common.LoginENG.GetLogOnUser()
            If uPara.LOGIN_HISTORY_ID = 0 Then
                uPara = Nothing
                Session.RemoveAll()
                Me.Response.Redirect("frmLogin.aspx")
            Else
                'Label1.Text = uPara.USER_PARA.FNAME & " " & uPara.USER_PARA.LNAME  'Session("loginnm")
                CreateWebConfigMenu(uPara)
            End If
            Page.Title = version
            uPara = Nothing
        End If
    End Sub

    Protected Sub btnLogout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogout.Click
        Engine.Common.LoginENG.LogOut(Config.GetLoginHistoryID)

        Session.RemoveAll()

        Dim ck As New System.Web.HttpCookie(".AIS-WebConfig")
        ck.Expires = DateAdd(DateInterval.Minute, -1, DateTime.Now)
        HttpContext.Current.Response.Cookies.Add(ck)

        Me.Response.Redirect("frmLogin.aspx")
    End Sub

    Private Sub CreateWebConfigMenu(ByVal uPara As CenParaDB.Common.UserProfilePara)
        Dim ret As String = ""
        ret += "<div class='arrowlistmenu'> " & vbNewLine
        ret += "    <h3 class='menuheader expandable' style='background-image:url(images/MenuHeadBG.png); background-repeat: repeat-x;' >MENU</h3>" & vbNewLine
        ret += "    <ul class='categoryitems'>" & vbNewLine
        ret += "        <ul class='categoryitems'>" & vbNewLine
        ret += "            <a href='frmMSTSoftwareDTACSetupKiosk.aspx?rnd=" & DateTime.Now.Millisecond & "' >Kiosk Setup</a>" & vbNewLine
        ret += "        </ul>" & vbNewLine
        ret += "        <ul class='categoryitems'>" & vbNewLine
        ret += "            <a href='frmMSTDTACBiller.aspx?rnd=" & DateTime.Now.Millisecond & "' >Biller</a>" & vbNewLine
        ret += "        </ul>" & vbNewLine
        ret += "        <ul class='categoryitems'>" & vbNewLine
        ret += "            <a href='frmMSTDTACTopupMoney.aspx?rnd=" & DateTime.Now.Millisecond & "' >Top up Money</a>" & vbNewLine
        ret += "        </ul>" & vbNewLine
        ret += "    </ul>" & vbNewLine
        ret += "</div>" & vbNewLine
        lblMenu.Text = ret

        'Dim ret As String = ""
        'Dim dt As New DataTable
        'If uPara.USERNAME.ToUpper = "ADMIN" Then
        '    dt = Engine.Common.MenuENG.GetMenuList(3)
        'Else
        '    dt = Engine.Common.MenuENG.GetMenuList(3, uPara.USERNAME)
        'End If
        'If dt.Rows.Count > 0 Then
        '    dt.DefaultView.RowFilter = "ref_sysmenu_id=0"
        '    Dim mDt As New DataTable
        '    mDt = dt.DefaultView.ToTable

        '    ret += "<div class='arrowlistmenu'> " & vbNewLine
        '    For Each mDr As DataRow In mDt.Rows
        '        Dim ChildMenu As String = CreateSubMenu(dt, mDr("id"), 1)
        '        If ChildMenu.Trim <> "" Then
        '            ret += "    <h3 class='menuheader expandable' style='background-image:url(images/MenuHeadBG.png); background-repeat: repeat-x;' >" & mDr("menu_name_th") & "</h3>" & vbNewLine
        '            ret += "    <ul class='categoryitems'>" & vbNewLine
        '            ret += ChildMenu
        '            ret += "    </ul>" & vbNewLine
        '        End If
        '    Next
        '    ret += "</div>" & vbNewLine
        '    lblMenu.Text = ret
        '    mDt.Dispose()
        'End If
        'dt.Dispose()
    End Sub

    Private Function CreateSubMenu(ByVal dt As DataTable, ByVal RefMenuID As String, ByVal IndenLevel As Integer) As String
        Dim ret As String = ""
        dt.DefaultView.RowFilter = "ref_sysmenu_id='" & RefMenuID & "'"
        Dim dtS As DataView
        dtS = dt.DefaultView
        If dtS.Count > 0 Then
            For Each drS As DataRowView In dtS
                Dim TmpRet As String = ""
                Dim Space As String = ""
                For j As Integer = 1 To IndenLevel
                    Space += "&nbsp;&nbsp;"
                Next
                TmpRet = "    <ul class='categoryitems'>" & vbNewLine
                TmpRet += Space & " <a href='" & drS("menu_url") & "?rnd=" & DateTime.Now.Millisecond & "&MenuID=" & drS("id") & "' >" & drS("menu_name_en") & "</a>" & vbNewLine
                TmpRet += CreateSubMenu(dt, drS("id"), IndenLevel + 3)
                TmpRet += "    </ul>" & vbNewLine

                If drS("menu_url").ToString = "#" Then
                    'ถ้าเป็นเมนูที่ไม่มี Link ให้ดูว่ามีเมนูย่อยที่ไม่ได้กำหนดสิทธิ์หรือไม่
                    dt.DefaultView.RowFilter = "ref_sysmenu_id='" & drS("id") & "'"
                    If dt.DefaultView.Count = 0 Then
                        TmpRet = ""
                    End If
                End If

                ret += TmpRet
            Next
        End If

        Return ret
    End Function
End Class

