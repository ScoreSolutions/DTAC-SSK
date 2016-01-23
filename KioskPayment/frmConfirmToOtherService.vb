Imports KioskPayment.Engine
Public Class frmConfirmToOtherService

    Private Sub pbOK_Click(sender As Object, e As EventArgs) Handles pbOK.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub pbComplete_Click(sender As Object, e As EventArgs) Handles pbComplete.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmConfirmToOtherService_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        FunctionENG.PlaySound("frmConfirmToOtherService")
    End Sub
End Class