Imports System.Web
'Imports System.Web.Security
'Imports System.Web.UI
Imports System.Configuration
Imports CenParaDB.Common
Imports CenParaDB.Common.Utilities
Imports CenParaDB.TABLE
Imports System.Xml.Serialization
Imports System.IO
Imports System
Imports System.Data
Imports Engine.Common

Public Class Config
    Public Shared Function GetLogOnUser() As UserProfilePara

        Dim ret As New UserProfilePara
        Try
            'Dim id As FormsIdentity = HttpContext.Current.User.Identity
            'Dim tik As FormsAuthenticationTicket = id.Ticket
            'Dim sr As New XmlSerializer(GetType(UserProfilePara))
            'Dim st As New MemoryStream(Convert.FromBase64String(tik.UserData))
            'ret = sr.Deserialize(st)

            ''Get UserProfile From Table in DB
            'Dim UPara As New TbUserCenParaDB
            'Dim eng As New LoginENG
            'UPara = eng.GetLoginUserProfile(ret.USERNAME)
            'ret.USER_PARA = UPara

            ret = Engine.Common.LoginENG.GetLogOnUser
        Catch ex As Exception

        End Try

        Return ret
    End Function

    Public Shared Function GetLoginHistoryID() As Long
        Dim tmp As LoginSessionPara
        tmp = Engine.Common.LoginENG.GetLoginSessionPara()
        Dim ret As Long = tmp.LOGIN_HISTORY_ID
        tmp = Nothing
        Return ret
    End Function

    Public Shared Function GetWebConfigVersion() As String
        Return ConfigurationManager.AppSettings("Version").ToString()
    End Function

    Public Shared Sub ShowAlert(ByVal msg As String, ByVal frm As Page, ByVal ClientID As String)
        Dim popupScript As String = "<script language='javascript'> " & _
        " alert('" & msg & "'); " & _
        " document.getElementById('" & ClientID & "').select();" & _
        " </script>"
        ScriptManager.RegisterStartupScript(frm, GetType(String), "SetAlert1", popupScript, False)
    End Sub
    Public Shared Sub ShowAlert(ByVal msg As String, ByVal frm As Page)
        Dim popupScript As String = "<script language='javascript'> " & _
        " alert('" & msg & "'); " & _
        " </script>"
        ScriptManager.RegisterStartupScript(frm, GetType(String), "SetAlert1", popupScript, False)
    End Sub
    Public Shared Sub SaveTransLog(ByVal TransDesc As String, ByVal ClassName As String, ByVal LoginHisID As Long)
        Engine.Common.FunctionEng.SaveTransLog(LoginHisID, ClassName, TransDesc)
    End Sub
End Class
