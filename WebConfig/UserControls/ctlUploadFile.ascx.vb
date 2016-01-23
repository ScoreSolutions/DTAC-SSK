Imports System.Collections
Imports System.Configuration
Imports System.Data
Imports System.Linq
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Xml.Linq

Partial Class UserControls_ctlUploadFile
    Inherits System.Web.UI.UserControl
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)

    End Sub
    Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpload.Click
        Try
            Dim filecolln As HttpFileCollection = Request.Files
            If filecolln.Count > 0 Then
                For i As Integer = 0 To filecolln.Count - 1
                    Dim file As HttpPostedFile = filecolln(i)
                    If file.ContentLength > 0 Then
                        file.SaveAs(ConfigurationManager.AppSettings("FilePath") + System.IO.Path.GetFileName(file.FileName))
                    End If
                Next
                lblMessage.Text = "Uploaded Successfully!"
            Else
                lblMessage.Text = "No files selected!"

            End If
        Catch ex As Exception
            lblMessage.Text = ex.Message
        End Try
    End Sub

    Protected Sub LinkButton2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton2.Click

    End Sub
End Class
