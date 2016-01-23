Imports KioskPayment.Engine
Public Class frmBillPaymentSum

    Public WriteOnly Property SummaryCompanyName As String
        Set(value As String)
            lblCompanyName.Text = value
        End Set
    End Property
    Public WriteOnly Property SummaryMoneyAmt As String
        Set(value As String)
            lblMoneyAmt.Text = value
        End Set
    End Property

    Private Sub frmBillPaymentSum_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            'pbOK.Image = GetOKButtonImg
            'pbCancel.Image = GetCancelButtonImg
            pbPayTotal.Image = GetTxtPayTotal
        Catch ex As Exception

        End Try
    End Sub

    Private Sub pbOK_Click(sender As Object, e As EventArgs) Handles pbOK.Click
        TotalMoneyAmt += Convert.ToDouble(lblMoneyAmt.Text.Replace(",", ""))
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub pbCancel_Click(sender As Object, e As EventArgs) Handles pbCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
End Class