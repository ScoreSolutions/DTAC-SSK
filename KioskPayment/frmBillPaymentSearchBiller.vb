Imports LinqDB.TABLE
Public Class frmBillPaymentSearchBiller
    Dim _BillerTypeID As Long = 0
    Dim _BillerTypeName As String = ""
    Dim _BillerID As Long = 0
    Public WriteOnly Property BillerTypeID As Long
        Set(value As Long)
            _BillerTypeID = value
        End Set
    End Property
    Public WriteOnly Property BillerTypeName As String
        Set(value As String)
            _BillerTypeName = value
        End Set
    End Property
    Public ReadOnly Property BillerID As Long
        Get
            Return _BillerID
        End Get
    End Property

    Private Sub frmBillPaymentSearchBiller_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            pbCancel.BackgroundImage = GetCancelButtonImg
        Catch ex As Exception

        End Try

        Dim dt As New DataTable
        Dim lnq As New MsBillerLinqDB
        dt = lnq.GetDataList("ms_biller_type_id='" & _BillerTypeID & "'", "biller_name", Nothing)
        If dt.Rows.Count > 0 Then
            Dim Font As Font = New Font("Microsoft Sans Serif", 24, FontStyle.Bold, GraphicsUnit.Pixel)
            Dim bgImg As Image = Image.FromFile(Application.StartupPath & "\Images\Button\btnBillPaymentSelectBillerType.jpg")
            For Each dr As DataRow In dt.Rows
                Dim btn As New Button
                btn.Width = 300
                btn.Height = 50 'GetBtnTopupMoney.Height
                btn.BackgroundImage = bgImg
                btn.BackgroundImageLayout = ImageLayout.Stretch
                btn.Text = dr("biller_name")
                btn.Name = dr("id")
                btn.Font = Font
                'btn.ForeColor = Color.Black
                'btn.BackColor = Color.LightBlue
                btn.FlatStyle = FlatStyle.Flat
                flp1.Controls.Add(btn)
                AddHandler btn.Click, AddressOf ClickBiller
            Next
        End If
        dt.Dispose()
        lnq = Nothing
    End Sub

    Private Sub ClickBiller(sender As Object, e As EventArgs)
        Dim btn As Button = sender
        _BillerID = btn.Name
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub pbCancel_Click(sender As Object, e As EventArgs) Handles pbCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
End Class