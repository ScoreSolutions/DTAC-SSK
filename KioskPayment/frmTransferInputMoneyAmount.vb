Public Class frmTransferInputMoneyAmount


    Private Sub AddNumDigit(pbDigit As String)
        txtMoneyAmt.Text = txtMoneyAmt.Text.Replace(",", "")
        txtMoneyAmt.Text += pbDigit
        txtMoneyAmt.Text = Convert.ToDouble(txtMoneyAmt.Text).ToString("#,##0")
    End Sub
    Private Sub pb1_Click(sender As Object, e As EventArgs) Handles pb1.Click
        AddNumDigit("1")
    End Sub

    Private Sub pb2_Click(sender As Object, e As EventArgs) Handles pb2.Click
        AddNumDigit("2")
    End Sub

    Private Sub pb3_Click(sender As Object, e As EventArgs) Handles pb3.Click
        AddNumDigit("3")
    End Sub

    Private Sub pb4_Click(sender As Object, e As EventArgs) Handles pb4.Click
        AddNumDigit("4")
    End Sub

    Private Sub pb5_Click(sender As Object, e As EventArgs) Handles pb5.Click
        AddNumDigit("5")
    End Sub

    Private Sub pb6_Click(sender As Object, e As EventArgs) Handles pb6.Click
        AddNumDigit("6")
    End Sub

    Private Sub pb7_Click(sender As Object, e As EventArgs) Handles pb7.Click
        AddNumDigit("7")
    End Sub

    Private Sub pb8_Click(sender As Object, e As EventArgs) Handles pb8.Click
        AddNumDigit("8")
    End Sub

    Private Sub pb9_Click(sender As Object, e As EventArgs) Handles pb9.Click
        AddNumDigit("9")
    End Sub

    Private Sub pb0_Click(sender As Object, e As EventArgs) Handles pb0.Click
        AddNumDigit("0")
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