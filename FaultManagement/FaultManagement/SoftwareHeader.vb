Public Class SoftwareHeader
    Private Sub SWHeader_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtShopName.Text = Engine.Common.FunctionEng.GetShopConfig("s_name_en")
        txtServerName.Text = Environment.MachineName
        txtServerIP.Text = Engine.Common.FunctionEng.GetIPAddress
    End Sub
End Class
