Imports System.Data.SqlClient
Imports System.Data
Partial Class MasterLogin
    Inherits System.Web.UI.MasterPage

    Sub showError(ByVal msg As String, Optional ByVal isError As Boolean = True)
        If isError Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "JQMsg", "$.prompt('" & msg & "',{ buttons: { Ok: false},prefix:'jqir',overlayspeed: 'fast',opacity: 0.7 });", True)
        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "JQMsg", "$.prompt('" & msg & "',{ buttons: { Ok: false},prefix:'jqi',overlayspeed: 'fast',opacity: 0.7 });", True)
        End If

        'lblError.Text = msg
        'lblError.Visible = True
    End Sub

End Class

