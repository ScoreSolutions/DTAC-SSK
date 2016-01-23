Imports KioskPayment.Engine
Public Class frmTopUpSum
    Public WriteOnly Property SummaryMoneyAmount As Double
        Set(value As Double)
            lblAmount.Text = value.ToString("#,##0.00")
        End Set
    End Property
    Public WriteOnly Property SummaryMobileNo As String
        Set(value As String)
            lblMobileNo.Text = value
        End Set
    End Property
    Private Sub frmTopUpSum_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            pbOK.Image = GetOKButtonImg
            pbCancel.Image = GetCancelButtonImg
            pbMobileNo.Image = GetTxtMobileNo
            pbAmount.Image = GetTxtMoneyTotal
            Me.BackgroundImage = _frmTopUpSum
        Catch ex As Exception

        End Try
    End Sub

    Private Sub pbOK_Click(sender As Object, e As EventArgs) Handles pbOK.Click
        TotalMoneyAmt += Convert.ToDouble(lblAmount.Text.Replace(",", ""))
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub pbCancel_Click(sender As Object, e As EventArgs) Handles pbCancel.Click
        'TotalMoneyAmt += Convert.ToDouble(lblAmount.Text.Replace(",", ""))
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
End Class