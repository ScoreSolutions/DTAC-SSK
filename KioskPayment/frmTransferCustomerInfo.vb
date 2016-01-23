Public Class frmTransferCustomerInfo

    Private Sub frmTransferCustomerInfo_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            'pbOK.Image = GetOKButtonImg
            'pbCancel.Image = GetCancelButtonImg
            Me.BackgroundImage = _frmTransferCustomerInfo
        Catch ex As Exception

        End Try

    End Sub

    Private Sub pbOK_Click(sender As Object, e As EventArgs) Handles pbOK.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub pbCancel_Click(sender As Object, e As EventArgs) Handles pbCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
End Class