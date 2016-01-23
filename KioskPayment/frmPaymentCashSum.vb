Imports KioskPayment.Engine
Public Class frmPaymentCashSum
    Dim _dt As New DataTable
    Dim _arrData As New ArrayList

    Public WriteOnly Property SetPaymentList As DataTable
        Set(value As DataTable)
            _dt = value
        End Set
    End Property
    Public WriteOnly Property arrData As ArrayList
        Set(value As ArrayList)
            _arrData = value
        End Set
    End Property

    Private Sub frmSum_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.BackgroundImage = _frmPaymentCashSum
            SetBankNoteList()
          
        Catch ex As Exception

        End Try
    End Sub


    Private Sub SetBankNoteList()
        Dim BankAmt20 As Integer = 0
        Dim BankAmt50 As Integer = 0
        Dim BankAmt100 As Integer = 0
        Dim BankAmt500 As Integer = 0
        Dim BankAmt1000 As Integer = 0
        Try

            BankAmt20 = _arrData(2)
            BankAmt50 = _arrData(3)
            BankAmt100 = _arrData(4)
            BankAmt500 = _arrData(5)
            BankAmt1000 = _arrData(6)
            lblAmt20.Text = "20"
            lblMoney20.Text = BankAmt20 * 20

            lblAmt50.Text = "50"
            lblMoney50.Text = BankAmt50 * 50

            lblAmt100.Text = "100"
            lblMoney100.Text = BankAmt100 * 100

            lblAmt500.Text = "500"
            lblMoney500.Text = BankAmt500 * 500

            lblAmt1000.Text = "1000"
            lblMoney1000.Text = BankAmt1000 * 1000

        Catch ex As Exception
            lblAmt20.Text = "20"
            lblMoney20.Text = BankAmt20 * 20

            lblAmt50.Text = "50"
            lblMoney50.Text = BankAmt50 * 50

            lblAmt100.Text = "100"
            lblMoney100.Text = BankAmt100 * 100

            lblAmt500.Text = "500"
            lblMoney500.Text = BankAmt500 * 500

            lblAmt1000.Text = "1000"
            lblMoney1000.Text = BankAmt1000 * 1000
        End Try

        'Try

        '    'Dim MoneyTotal As Double = 0
        '    Dim bLnq As New LinqDB.TABLE.MsBankNoteLinqDB
        '    _dt = bLnq.GetDataList("active_status='Y'", "", Nothing)
        '    If _dt.Rows.Count > 0 Then
        '        _dt.Columns.Add("BankNoteImg", GetType(Image))
        '        _dt.Columns.Add("BankAmt", GetType(Integer))
        '        For i As Integer = 0 To _dt.Rows.Count - 1
        '            If IO.File.Exists(_dt.Rows(i)("bank_img")) = True Then
        '                _dt.Rows(i)("BankNoteImg") = Image.FromFile(_dt.Rows(i)("bank_img"))
        '            End If
        '            If Convert.ToInt16(_dt.Rows(i)("bank_value")) = 20 Then
        '                _dt.Rows(i)("BankAmt") = BankAmt20
        '            End If
        '            If Convert.ToInt16(_dt.Rows(i)("bank_value")) = 50 Then
        '                _dt.Rows(i)("BankAmt") = BankAmt50
        '            End If
        '            If Convert.ToInt16(_dt.Rows(i)("bank_value")) = 100 Then
        '                _dt.Rows(i)("BankAmt") = BankAmt100
        '            End If
        '            If Convert.ToInt16(_dt.Rows(i)("bank_value")) = 500 Then
        '                _dt.Rows(i)("BankAmt") = BankAmt500
        '            End If
        '            If Convert.ToInt16(_dt.Rows(i)("bank_value")) = 1000 Then
        '                _dt.Rows(i)("BankAmt") = BankAmt1000
        '            End If
        '        Next

        '        'DataGridView1.DataSource = _dt
        '    End If

        'Catch ex As Exception

        'End Try

        Try
            lblMoneyTotal.Text = (CInt(_arrData(0)) + CInt(_arrData(1))).ToString("#,##0.00")
        Catch ex As Exception

        End Try

    End Sub


    Private Sub pbOk_Click(sender As Object, e As EventArgs) Handles pbOk.Click
        Dim frm As New frmPaySlip
        Dim MerchantID As String = frm.PrintData.MerchantID
        Dim InvoiceNumber As String = frm.PrintData.InvoiceNumber
        Dim TransactionID As String = frm.PrintData.TransactionID
        Dim Total As String = frm.PrintData.Total
        Dim dtData As DataTable = frm.PrintData.dtData
        MerchantID = "11223344"
        InvoiceNumber = "2557052145687"
        TransactionID = "2014051254879"
        Total = TotalMoneyAmt
        'p.TotalText=
        dtData = New DataTable
        dtData.Columns.Add("ServiceName")
        dtData.Columns.Add("No")
        dtData.Columns.Add("Amount")
        dtData.Columns.Add("Vat")
        dtData.Columns.Add("status")

        Dim i As Integer = 0
        For Each dr As DataRow In PaymentListDT.Rows
            'dr("MobileNo") = MobileNo
            'dr("PaymentType") = PaymentType
            'dr("ServiceType") = ServiceType
            'dr("AccountNo") = AccountNo
            'dr("BillerName") = BillerName
            'dr("Amount") = Amount

            Dim lnq As New LinqDB.TABLE.MsBillerLinqDB
            Dim tmpDt As DataTable = lnq.GetDataList("biller_name='" & dr("BillerName") & "'", "", Nothing)

            Dim pDr As DataRow = dtData.NewRow
            pDr("ServiceName") = dr("ServiceType")
            pDr("No") = (i + 1)
            pDr("Amount") = dr("Amount")
            pDr("Vat") = (Convert.ToDouble(dr("Amount")) * 7) / 100

            Dim Status As String = ""
            If tmpDt.Rows.Count > 0 Then
                pDr("status") = IIf(tmpDt.Rows(0)("inhouse_biller").ToString = "Y", "0", "1")
            Else
                pDr("status") = "1"
            End If
            dtData.Rows.Add(pDr)
            tmpDt.Dispose()

            i += 1
        Next

        frm.PrintData.MerchantID = MerchantID
        frm.PrintData.InvoiceNumber = InvoiceNumber
        frm.PrintData.TransactionID = TransactionID
        frm.PrintData.Total = Total
        frm.PrintData.dtData = dtData

        If frm.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim ff As New frmPaymentReceipt
            Engine.FunctionENG.PlaySound("frmPaymentReceipt")
            If ff.ShowDialog = Windows.Forms.DialogResult.OK Then
                CreateNewPaymentList()
                frmSelectPaymentType.Show()
            Else

                frmChangeLanguage.Show()
            End If
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
        Else
            frmThankYou.Show()
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
        End If
    End Sub

    Private Sub pbAddMoney_Click(sender As Object, e As EventArgs) Handles pbAddMoney.Click
        'frmPaymentCash.Show()
        Dim ff As New frmPaymentCash
        ff.SetMoneyAmount = TotalMoneyAmt
        ff.Show()
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub


End Class