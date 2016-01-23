Imports KioskPayment.Org.Mentalis.Files
Imports KioskPayment.Engine
Public Class frmSoryForReturn
    Dim CountTimeReturn As Integer = 0
    Private Sub pbOK_Click(sender As Object, e As EventArgs) Handles pbOK.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub frmSoryForReturn_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Me.BackgroundImage = _frmSoryForReturn
            pbOK.Image = GetOKButtonImg 'GetButtonImageENG.GetButton(ImageButton.OK) 'Image.FromFile(Application.StartupPath & "\Images\ok_btn.jpg")
            pbCancel.Image = GetCancelButtonImg 'GetButtonImageENG.GetButton(ImageButton.Cancel) 'Image.FromFile(Application.StartupPath & "\Images\cancel_btn.jpg")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub pbCancel_Click(sender As Object, e As EventArgs) Handles pbCancel.Click
        frmPaymentCash.Close()
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub TimerReturnToScanbarcode_Tick(sender As Object, e As EventArgs) Handles TimerReturnToScanbarcode.Tick
        If CountTimeReturn < WaitTimeSessionExpired Then
            CountTimeReturn += 1
        Else
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
            Me.Close()
        End If
    End Sub
End Class