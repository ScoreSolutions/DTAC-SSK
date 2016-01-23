Imports System.Data

Partial Class _frmWelcomePage
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim uPara As CenParaDB.Common.UserProfilePara = Engine.Common.LoginENG.GetLogOnUser()
        If uPara.LOGIN_HISTORY_ID <> 0 Then
            lblUser.Text = "ยินดีต้อนรับคุณ " & uPara.USER_PARA.FNAME & " " & uPara.USER_PARA.LNAME & " เข้าสู่ระบบ"
            uPara = Nothing
        Else
            Response.Redirect("frmLogin.aspx")
        End If
    End Sub
End Class
