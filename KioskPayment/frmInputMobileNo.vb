Public Class frmInputMobileNo

    Private Sub AddMobileNo(pbDigit As String)
        If txtMobileNo.Text.Length < 10 Then
            txtMobileNo.Text += pbDigit
        End If
    End Sub

    Private Sub pb1_Click(sender As Object, e As EventArgs) Handles pb1.Click
        AddMobileNo("1")
    End Sub

    Private Sub pb2_Click(sender As Object, e As EventArgs) Handles pb2.Click
        AddMobileNo("2")
    End Sub

    Private Sub pb3_Click(sender As Object, e As EventArgs) Handles pb3.Click
        AddMobileNo("3")
    End Sub

    Private Sub pb4_Click(sender As Object, e As EventArgs) Handles pb4.Click
        AddMobileNo("4")
    End Sub

    Private Sub pb5_Click(sender As Object, e As EventArgs) Handles pb5.Click
        AddMobileNo("5")
    End Sub

    Private Sub pb6_Click(sender As Object, e As EventArgs) Handles pb6.Click
        AddMobileNo("6")
    End Sub

    Private Sub pb7_Click(sender As Object, e As EventArgs) Handles pb7.Click
        AddMobileNo("7")
    End Sub

    Private Sub pb8_Click(sender As Object, e As EventArgs) Handles pb8.Click
        AddMobileNo("8")
    End Sub

    Private Sub pb9_Click(sender As Object, e As EventArgs) Handles pb9.Click
        AddMobileNo("9")
    End Sub

    Private Sub pb0_Click(sender As Object, e As EventArgs) Handles pb0.Click
        AddMobileNo("0")
    End Sub

    Private Sub pbOK_Click(sender As Object, e As EventArgs) Handles pbOK.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub pbCancel_Click(sender As Object, e As EventArgs) Handles pbCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmInputMobileNo_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            If Me.BackgroundImage Is Nothing Then
                Me.BackgroundImage = _frmInputMobileNo
            End If
        Catch ex As Exception

        End Try


    End Sub

    Private Sub pbDelete_Click(sender As Object, e As EventArgs) Handles pbDelete.Click
        If txtMobileNo.Text.Length > 0 Then
            txtMobileNo.Text = txtMobileNo.Text.Substring(0, txtMobileNo.Text.Length - 1)
        End If
    End Sub

    Private Sub txtMobileNo_GotFocus(sender As Object, e As EventArgs) Handles txtMobileNo.GotFocus
        HideCaret(txtMobileNo.Handle)
    End Sub
End Class