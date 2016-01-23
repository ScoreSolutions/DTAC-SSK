Imports KioskPayment.Org.Mentalis.Files
Imports System.IO
Imports KioskPayment.Engine
Imports System.IO.Ports
Imports System.Text

Public Class frmBillPaymentScanBarcode
    Public Shared com As New SerialPort()
    Public Shared _msg As String
    Public Shared data As String
    Public Shared strResultData As String
    Public Shared Timer_data As Timer

    Dim _BillerID As Long = 0
    Public WriteOnly Property BillerID As Long
        Set(value As Long)
            _BillerID = value
        End Set
    End Property

#Region "Barcode Scanner"
    Public Sub Innitial(ByVal strComportName As String)
        com.PortName = strComportName
        com.BaudRate = "9600"
        com.DataBits = 8
        com.Parity = Parity.None
        com.Handshake = Handshake.XOnXOff
        AddHandler com.DataReceived, New SerialDataReceivedEventHandler(AddressOf port_DataReceived)
    End Sub

    Private Sub port_DataReceived(sender As Object, e As SerialDataReceivedEventArgs)
        'determine the mode the user selected (binary/string)

        'user chose binary
        'retrieve number of bytes in the buffer
        Dim bytes As Integer = com.BytesToRead
        'create a byte array to hold the awaiting data
        Dim comBuffer As Byte() = New Byte(bytes - 1) {}
        'read the data and store it
        com.Read(comBuffer, 0, bytes)
        data = Encoding.ASCII.GetString(comBuffer)

        'display the data to the user
        _msg = ByteToHex(comBuffer) + "" + Environment.NewLine + ""
        DisplayData(ByteToHex(comBuffer) + "" + Environment.NewLine + "")


    End Sub


#Region "DoDisplay"
    Private Sub DoDisplay(ByVal sender As Object, ByVal e As EventArgs)
        TextBox4.SelectedText = String.Empty
        TextBox4.AppendText(data)
        TextBox4.ScrollToCaret()
        If TextBox4.Text <> "" Then CheckBarcode() Else TextBox4.Text = ""
    End Sub

    Private Sub DisplayData(ByVal msg As String)
        Try
            TextBox4.Invoke(New EventHandler(AddressOf DoDisplay))

        Catch ex As Exception

        End Try
    End Sub

    Private Function ByteToHex(ByVal comByte As Byte()) As String
        'create a new StringBuilder object
        Dim builder As New StringBuilder(comByte.Length * 3)
        'loop through each byte in the array
        For Each data As Byte In comByte
            builder.Append(Convert.ToString(data, 16).PadLeft(2, "0"c).PadRight(3, " "c))
            'convert the byte to a string and add to the stringbuilder
        Next
        'return the converted value
        Return builder.ToString().ToUpper()
    End Function
#End Region

    Public Function ConnectComport() As Boolean
        Try
            If Not com.IsOpen Then com.Open()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function DisconnectComport() As Boolean
        Try
            If com.IsOpen Then com.Close()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

#Region "Command"
    Public Sub Enable()
        If Not com.IsOpen Then com.Open()
        Dim bb(1) As Byte
        bb(0) = &HA0
        com.Write(bb, 0, 1)
    End Sub
    Public Sub Disable()
        If Not com.IsOpen Then com.Open()
        Dim bb(1) As Byte
        bb(0) = &H55
        com.Write(bb, 0, 1)
    End Sub

    Public Sub Reboot()
        If Not com.IsOpen Then com.Open()
        Dim bb(1) As Byte
        bb(0) = &HA5
        com.Write(bb, 0, 1)
    End Sub

#End Region

#End Region

    Private Sub txtBarcode_GotFocus(sender As Object, e As EventArgs) Handles txtBarcode.GotFocus
        HideCaret(txtBarcode.Handle)
    End Sub

    Private Sub txtBarcode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtBarcode.KeyPress
        'CountVDO = 0
        If e.KeyChar = Chr(13) Then
            'Dim dt As DataTable = GetPaymentTransactionByAccountNo(txtBarcode.Text)
            Dim BillerBarcode As String = Split(txtBarcode.Text, " ")(0)
            Dim bLnq As New LinqDB.TABLE.MsBillerLinqDB
            Dim bDt As DataTable = bLnq.GetDataList("CHARINDEX(barcode_start_digit,'" & BillerBarcode & "')>0", "", Nothing)
            If bDt.Rows.Count > 0 Then
                If bDt.Rows(0)("INHOUSE_BILLER").ToString = "N" Then
                    'Non Dtac Biller
                    Dim ff As New frmInputMoneyAmount
                    If ff.ShowDialog = Windows.Forms.DialogResult.OK Then
                        Dim frmBillSum As New frmBillPaymentSum
                        frmBillSum.SummaryCompanyName = bDt.Rows(0)("BILLER_NAME").ToString
                        frmBillSum.SummaryMoneyAmt = Convert.ToDouble(ff.txtMoneyAmount.Text).ToString("#,##0.00")
                        frmBillSum.pbBillerLogo.BackgroundImage = Image.FromFile(Application.StartupPath & bDt.Rows(0)("IMAGE_LOGO").ToString)
                        If frmBillSum.ShowDialog = Windows.Forms.DialogResult.OK Then
                            TotalMoneyAmt += Convert.ToDouble(ff.txtMoneyAmount.Text.Replace(",", ""))

                            AddPaymentListData(_MobileNo, _PaymentType, ServiceType.BillPayment, BillerBarcode, bDt.Rows(0)("BILLER_NAME").ToString, Convert.ToDouble(ff.txtMoneyAmount.Text).ToString("#,##0.00"))
                            Dim frmCon As New frmConfirmToOtherService
                            If frmCon.DialogResult = Windows.Forms.DialogResult.OK Then
                                'frmSelectService.MdiParent = frmMDIParent
                                frmSelectService.Show()
                            Else
                                If _PaymentType = PaymentType.Cash Then
                                    Dim frm As New frmPaymentCash
                                    'frm.MdiParent = frmMDIParent
                                    frm.SetMoneyAmount = TotalMoneyAmt
                                    frm.Show()
                                ElseIf _PaymentType = PaymentType.CreditCard Then
                                    Dim frm As New frmPaymentCredit
                                    'frm.MdiParent = frmMDIParent
                                    frm.TotalAmount = TotalMoneyAmt
                                    frm.Show()
                                End If
                            End If
                            'BarcodeScanner.Disable()
                            'BarcodeScanner.DisconnectComport()
                            Me.Close()
                        Else
                            frmSelectService.Show()
                            'BarcodeScanner.Disable()
                            'BarcodeScanner.DisconnectComport()
                            Me.Close()
                        End If
                    End If
                Else
                    'Dtac Biller
                    Dim mBLnq As New LinqDB.TABLE.TmpDeptLinqDB
                    mBLnq.ChkDataByBARCODE(txtBarcode.Text, Nothing)
                    If mBLnq.ID > 0 Then
                        Dim frmBillSum As New frmBillPaymentSum
                        frmBillSum.SummaryCompanyName = bDt.Rows(0)("BILLER_NAME").ToString
                        frmBillSum.SummaryMoneyAmt = mBLnq.AMOUNT
                        frmBillSum.pbBillerLogo.BackgroundImage = Image.FromFile(Application.StartupPath & bDt.Rows(0)("IMAGE_LOGO").ToString)
                        If frmBillSum.ShowDialog = Windows.Forms.DialogResult.OK Then
                            TotalMoneyAmt += mBLnq.AMOUNT

                            AddPaymentListData(_MobileNo, _PaymentType, ServiceType.BillPayment, BillerBarcode, bDt.Rows(0)("BILLER_NAME").ToString, mBLnq.AMOUNT)
                            Dim frmCon As New frmConfirmToOtherService
                            If frmCon.ShowDialog = Windows.Forms.DialogResult.OK Then
                                'frmSelectService.MdiParent = frmMDIParent
                                frmSelectService.Show()
                            Else
                                If _PaymentType = PaymentType.Cash Then
                                    Dim frm As New frmPaymentCash
                                    'frm.MdiParent = frmMDIParent
                                    frm.SetMoneyAmount = TotalMoneyAmt
                                    frm.Show()
                                ElseIf _PaymentType = PaymentType.CreditCard Then
                                    Dim frm As New frmPaymentCredit
                                    'frm.MdiParent = frmMDIParent
                                    frm.TotalAmount = TotalMoneyAmt
                                    frm.Show()
                                End If
                            End If
                            'BarcodeScanner.Disable()
                            'BarcodeScanner.DisconnectComport()
                            Me.Close()
                        Else
                            frmSelectService.Show()
                            'BarcodeScanner.Disable()
                            'BarcodeScanner.DisconnectComport()
                            Me.Close()
                        End If
                    End If
                    mBLnq = Nothing
                End If
            End If
            bLnq = Nothing
            txtBarcode.Text = ""
            txtBarcode.Focus()
        End If
    End Sub

    'Private Function GetPaymentTransactionByAccountNo(AccountNO As String) As DataTable
    '    Dim dt As New DataTable
    '    Dim lnq As New LinqDB.TABLE.TsPaymentTransactionLinqDB
    '    dt = lnq.GetDataList("account_no='" & AccountNO & "' and payment_type='0'", "transaction_date", Nothing)
    '    lnq = Nothing
    '    Return dt
    'End Function

    Private Sub frmScanBarcode_Click(sender As Object, e As EventArgs) Handles Me.Click
        'CountVDO = 0
        txtBarcode.Text = ""
        txtBarcode.Focus()
    End Sub

    Private Sub frmBillPaymentScanBarcode_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Disable()
        DisconnectComport()
    End Sub

    Private Sub frmScanBarcode_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            Me.BackgroundImage = _frmBillPaymentScanBarcodeBG
            pcBarcodeGIF.Image = _frmBillPaymentScanBarcodeScan
            pbInputMobile.Image = GetBtnInputMobileBarcode

            Dim lnq As New LinqDB.TABLE.MsBillerLinqDB
            lnq.GetDataByPK(_BillerID, Nothing)
            If lnq.ID > 0 Then
                Try
                    pbBillerLogo.BackgroundImage = Image.FromFile(Application.StartupPath & lnq.IMAGE_LOGO)
                    pbBillerLogo.BackgroundImageLayout = ImageLayout.Center
                Catch ex As Exception

                End Try
            End If
            lnq = Nothing


            Dim ini As New IniReader(Application.StartupPath & "\KioskPayment.ini")

            ini.ReadString("HwSetting", "BarcodeScannerComport")
            Innitial("Com" & ini.ReadString("HwSetting", "BarcodeScannerComport"))
            If ConnectComport() Then Enable()
        Catch ex As Exception

        End Try
    End Sub


    Private Sub frmScanBarcode_VisibleChanged(sender As Object, e As EventArgs) Handles Me.VisibleChanged
        If Me.Visible = True Then
            txtBarcode.Text = ""
            txtBarcode.Focus()
        End If
    End Sub

    Private Sub lblExit_Click(sender As Object, e As EventArgs) Handles lblExit.Click
        BarcodeScanner.Disable()
        BarcodeScanner.DisconnectComport()
        Application.Exit()
    End Sub

    Private Sub pcBackground_Click(sender As Object, e As EventArgs)
        'CountVDO = 0
        txtBarcode.Text = ""
        txtBarcode.Focus()
    End Sub

    Private Sub pcBarcodeGIF_Click(sender As Object, e As EventArgs) Handles pcBarcodeGIF.Click
        'CountVDO = 0
        txtBarcode.Text = ""
        txtBarcode.Focus()
    End Sub

    Private Sub pbInputMobile_Click(sender As Object, e As EventArgs) Handles pbInputMobile.Click
        Dim frm As New frmInputMobileNo
        frm.BackgroundImage = _frmInputMobileNoBarcode
        If frm.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim ff As New frmInputMoneyAmount
            If ff.ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim frmBillSum As New frmBillPaymentSum
                'frmBillSum.BackgroundImage = Image.FromFile(Application.StartupPath & "\Images\BillerLogo\LogoDTAC.jpg")
                frmBillSum.SummaryCompanyName = "บริษัท ดึแทค ไตรเน็ต จำกัด"
                frmBillSum.SummaryMoneyAmt = Convert.ToDouble(ff.txtMoneyAmount.Text).ToString("#,##0.00")
                frmBillSum.pbBillerLogo.BackgroundImage = Image.FromFile(Application.StartupPath & "\Images\BillerLogo\LogoDTAC.jpg")
                If frmBillSum.ShowDialog = Windows.Forms.DialogResult.OK Then
                    AddPaymentListData(_MobileNo, _PaymentType, ServiceType.MoneyTransfer, frm.txtMobileNo.Text, frmBillSum.lblCompanyName.Text, Convert.ToDouble(ff.txtMoneyAmount.Text).ToString("#,##0.00"))

                    Dim cFrm As New frmConfirmToOtherService
                    If cFrm.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                        If _PaymentType = PaymentType.Cash Then
                            Dim cafrm As New frmPaymentCash
                            'cafrm.MdiParent = frmMDIParent
                            cafrm.SetMoneyAmount = TotalMoneyAmt
                            cafrm.Show()
                        ElseIf _PaymentType = PaymentType.CreditCard Then
                            Dim crfrm As New frmPaymentCredit
                            'crfrm.MdiParent = frmMDIParent
                            crfrm.TotalAmount = TotalMoneyAmt
                            crfrm.Show()
                        End If

                        Me.Close()
                    Else
                        'frmSelectService.MdiParent = frmMDIParent
                        frmSelectService.Show()
                        Me.Close()
                    End If
                End If

                Me.Close()
            End If
        End If
    End Sub

    Sub CheckBarcode()
        'MessageBox.Show("Check")
        'Dim dt As DataTable = GetPaymentTransactionByAccountNo(txtBarcode.Text)
        Dim BillerBarcode As String = TextBox4.Text.Replace(" ", "").Replace(Chr(13), "")
        TextBox4.Text = ""
        Dim bLnq As New LinqDB.TABLE.MsBillerLinqDB
        Dim bDt As DataTable = bLnq.GetDataList("CHARINDEX(barcode_start_digit,'" & BillerBarcode & "')>0", "", Nothing)
        If bDt.Rows.Count > 0 Then
            If bDt.Rows(0)("INHOUSE_BILLER").ToString = "N" Then
                'MessageBox.Show("Non Dtac")
                'Non Dtac Biller
                Dim ff As New frmInputMoneyAmount
                If ff.ShowDialog = Windows.Forms.DialogResult.OK Then
                    Dim frmBillSum As New frmBillPaymentSum
                    frmBillSum.SummaryCompanyName = bDt.Rows(0)("BILLER_NAME").ToString
                    frmBillSum.SummaryMoneyAmt = Convert.ToDouble(ff.txtMoneyAmount.Text).ToString("#,##0.00")
                    frmBillSum.pbBillerLogo.BackgroundImage = Image.FromFile(Application.StartupPath & bDt.Rows(0)("IMAGE_LOGO").ToString)
                    If frmBillSum.ShowDialog = Windows.Forms.DialogResult.OK Then
                        TotalMoneyAmt += Convert.ToDouble(ff.txtMoneyAmount.Text.Replace(",", ""))

                        AddPaymentListData(_MobileNo, _PaymentType, ServiceType.BillPayment, BillerBarcode, bDt.Rows(0)("BILLER_NAME").ToString, Convert.ToDouble(ff.txtMoneyAmount.Text).ToString("#,##0.00"))
                        Dim frmCon As New frmConfirmToOtherService
                        'frmCon.MdiParent = frmMDIParent
                        If frmCon.DialogResult = Windows.Forms.DialogResult.OK Then
                            frmSelectService.Show()
                        Else
                            If _PaymentType = PaymentType.Cash Then
                                Dim frm As New frmPaymentCash
                                'frm.MdiParent = frmMDIParent
                                frm.SetMoneyAmount = TotalMoneyAmt
                                frm.Show()
                            ElseIf _PaymentType = PaymentType.CreditCard Then
                                Dim frm As New frmPaymentCredit
                                'frm.MdiParent = frmMDIParent
                                frm.TotalAmount = TotalMoneyAmt
                                frm.Show()
                            End If
                        End If
                        'BarcodeScanner.Disable()
                        'BarcodeScanner.DisconnectComport()
                        Me.Close()
                    Else
                        'frmSelectService.MdiParent = frmMDIParent
                        frmSelectService.Show()
                        'BarcodeScanner.Disable()
                        'BarcodeScanner.DisconnectComport()
                        Me.Close()
                    End If
                End If
            Else
                'Dtac Biller
                'MessageBox.Show("Dtac")
                Dim mBLnq As New LinqDB.TABLE.TmpDeptLinqDB
                mBLnq.ChkDataByBARCODE(BillerBarcode, Nothing)
               
                If mBLnq.ID > 0 Then
                    Dim frmBillSum As New frmBillPaymentSum
                    frmBillSum.SummaryCompanyName = bDt.Rows(0)("BILLER_NAME").ToString
                    frmBillSum.SummaryMoneyAmt = mBLnq.AMOUNT
                    frmBillSum.pbBillerLogo.BackgroundImage = Image.FromFile(Application.StartupPath & bDt.Rows(0)("IMAGE_LOGO").ToString)
                    If frmBillSum.ShowDialog = Windows.Forms.DialogResult.OK Then
                        TotalMoneyAmt += mBLnq.AMOUNT

                        AddPaymentListData(_MobileNo, _PaymentType, ServiceType.BillPayment, BillerBarcode, bDt.Rows(0)("BILLER_NAME").ToString, mBLnq.AMOUNT)
                        Dim frmCon As New frmConfirmToOtherService
                        If frmCon.ShowDialog = Windows.Forms.DialogResult.OK Then
                            frmSelectService.Show()
                        Else
                            If _PaymentType = PaymentType.Cash Then
                                Dim frm As New frmPaymentCash
                                'frm.MdiParent = frmMDIParent
                                frm.SetMoneyAmount = TotalMoneyAmt
                                frm.Show()
                            ElseIf _PaymentType = PaymentType.CreditCard Then
                                Dim frm As New frmPaymentCredit
                                'frm.MdiParent = frmMDIParent
                                frm.TotalAmount = TotalMoneyAmt
                                frm.Show()
                            End If
                        End If
                        'BarcodeScanner.Disable()
                        'BarcodeScanner.DisconnectComport()
                        Me.Close()
                    Else
                        'frmSelectService.MdiParent = frmMDIParent
                        frmSelectService.Show()
                        'BarcodeScanner.Disable()
                        'BarcodeScanner.DisconnectComport()
                        Me.Close()
                    End If
                End If
                mBLnq = Nothing
            End If
        End If
        bLnq = Nothing
        TextBox4.Text = ""
        TextBox4.Focus()
    End Sub
End Class
