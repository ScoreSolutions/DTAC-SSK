Public Class frmTopUpSelectMoney
    Dim _MobileNo As String
    Dim _Money As Integer
    Public WriteOnly Property MobileNo As String
        Set(value As String)
            _MobileNo = value
        End Set
    End Property
    Public ReadOnly Property Money As Integer
        Get
            Return _Money
        End Get
    End Property
    Private Sub AddMoneyButton()
        Dim dt As New DataTable
        Dim lnq As New LinqDB.TABLE.MsTopupMoneyLinqDB
        dt = lnq.GetDataList("active_status='Y'", "money_value", Nothing)
        If dt.Rows.Count > 0 Then
            Dim Font As Font = New Font("Microsoft Sans Serif", 24, FontStyle.Bold, GraphicsUnit.Pixel)
            For Each dr As DataRow In dt.Rows
                Dim btn As New Button
                btn.Image = GetBtnTopupMoney
                btn.Text = dr("money_value")
                btn.Name = "btnMoney_" & btn.Text
                btn.Width = 79 'GetBtnTopupMoney.Width
                btn.Height = 79 'GetBtnTopupMoney.Height
                btn.Font = Font
                btn.ForeColor = Color.White
                btn.FlatStyle = FlatStyle.Flat
                FlowLayoutPanel1.Controls.Add(btn)
                AddHandler btn.Click, AddressOf ClickMoney
            Next
        End If
        dt.Dispose()
    End Sub

    Private Sub ClickMoney(sender As Object, e As EventArgs)
        Me.DialogResult = Windows.Forms.DialogResult.OK
        _Money = Convert.ToInt16(sender.text)
        Me.Close()
    End Sub

    Private Sub frmTopUpSelectMoney_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            pbCancel.Image = GetCancelButtonImg

            Engine.FunctionENG.PlaySound("frmTopUpSelectMoney")
        Catch ex As Exception

        End Try
        AddMoneyButton()
    End Sub

    Private Sub pbCancel_Click(sender As Object, e As EventArgs) Handles pbCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
End Class