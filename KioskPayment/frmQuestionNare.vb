Imports System.Security.Policy

Public Class frmQuestionNare

    Private Sub frmQuestionNare_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        WebBrowser1.Width = 1024
        WebBrowser1.Navigate("http://192.168.1.167/D_public/LM_Questionaire.aspx")
        pbOK.Image = GetOKButtonImg
        pbCancel.Image = GetCancelButtonImg
    End Sub

    Private Sub pbCancel_Click(sender As Object, e As EventArgs) Handles pbCancel.Click
        frmChangeLanguage.Show()
        Me.Close()
    End Sub
End Class