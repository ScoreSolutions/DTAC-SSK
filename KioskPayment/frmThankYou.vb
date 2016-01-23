Imports KioskPayment.Org.Mentalis.Files
Imports KioskPayment.Engine
Public Class frmThankYou

    Dim WaitTimeThankYou As Integer = 5
    Dim CountTime As Integer = 0
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If CountTime < WaitTimeThankYou Then
            CountTime += 1
        Else
            Dim frm As New frmChangeLanguage
            'frm.MdiParent = frmMDIParent
            frm.Show()

            Me.Close()
        End If
    End Sub

    Private Sub frmThankYou_Load(sender As Object, e As EventArgs) Handles Me.Load
        WaitTimeThankYou = FunctionENG.GetConfig("WaitTimeThankYou", "5", Nothing) ' Convert.ToInt16(ini.ReadString("TimeThankYouToScanBarcode"))
    End Sub
End Class