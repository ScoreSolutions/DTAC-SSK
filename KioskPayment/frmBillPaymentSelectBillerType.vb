Imports LinqDB.TABLE
Public Class frmBillPaymentSelectBillerType
    Dim _BillerTypeID As Long = 0
    Dim _BillerTypeName As String = ""
    Dim dt As New DataTable

    Public ReadOnly Property BillerTypeID As Long
        Get
            Return _BillerTypeID
        End Get
    End Property
    Public ReadOnly Property BillerTypeName As String
        Get
            Return _BillerTypeName
        End Get
    End Property

    Private Sub frmBillPaymentSelectBillerType_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        dt.Dispose()
    End Sub

    Private Sub frmBillPaymentSelectBillerType_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Dim lnq As New MsBillerTypeLinqDB
        dt = lnq.GetDataList("active_status='Y'", "biller_type_name", Nothing)
        If dt.Rows.Count > 0 Then
            Dim Font As Font = New Font("Microsoft Sans Serif", 24, FontStyle.Bold, GraphicsUnit.Pixel)
            Dim bgImg As Image = Image.FromFile(Application.StartupPath & "\Images\Button\btnBillPaymentSelectBillerType.jpg")
            For Each dr As DataRow In dt.Rows
                Dim btn As New Button
                btn.Width = 300
                btn.Height = 50 'GetBtnTopupMoney.Height
                btn.BackgroundImage = bgImg
                btn.BackgroundImageLayout = ImageLayout.Stretch
                btn.Text = dr("biller_type_name")
                btn.Name = dr("id")
                btn.Font = Font
                btn.ForeColor = Color.White
                btn.FlatStyle = FlatStyle.Flat
                flp1.Controls.Add(btn)
                AddHandler btn.Click, AddressOf ClickBillerType
            Next
        End If

        lnq = Nothing
    End Sub

    Private Sub ClickBillerType(sender As Object, e As EventArgs)
        Dim btn As Button = sender
        _BillerTypeID = btn.Name
        _BillerTypeName = btn.Text
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub
End Class