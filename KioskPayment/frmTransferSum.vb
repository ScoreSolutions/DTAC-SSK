Public Class frmTransferSum

    Public WriteOnly Property SummaryMobileNo As String
        Set(value As String)
            lblMobileNo.Text = value
        End Set
    End Property
    Public WriteOnly Property SummaryTransferAmount As String
        Set(value As String)
            lblTransferAmt.Text = value
        End Set
    End Property
    Private Sub pbOK_Click(sender As Object, e As EventArgs) Handles pbOK.Click
        TotalMoneyAmt += Convert.ToDouble(lblTransferAmt.Text.Replace(",", ""))
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub pbCancel_Click(sender As Object, e As EventArgs) Handles pbCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmTransferSum_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Me.BackgroundImage = _frmTransferSum
        pbMobileNo.Image = GetTxtMobileNo
        pbMoney.Image = GetTxtMoneyTotal
    End Sub
End Class