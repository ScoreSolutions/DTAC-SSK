Imports LinqDB.TABLE
Imports KioskPayment.Org.Mentalis.Files
Imports System.IO.Ports
Imports System.Text

Public Class frmPaymentCredit

    Public Shared com As New SerialPort()
    Public Shared _continue As Boolean
    Public Shared _msg As String
    Public Shared data As String
    Public Shared sType As String
    Public Shared strResultData As String
    Public Shared strAmt As String ' strAmt = Amount 12 digit include 2 decimal
    Public Shared strInvoice As String ' Invoice Number
    Public WriteOnly Property TotalAmount As Double
        Set(value As Double)
            lblPayAmt.Text = value.ToString("#,##0.00")
        End Set
    End Property
    Private Sub pbOK_Click(sender As Object, e As EventArgs)

        Dim frmSign As New frmPaymentCreditSign
        Dim MerchantID As String = frmSign.PrintData.MerchantID
        Dim InvoiceNumber As String = frmSign.PrintData.InvoiceNumber
        Dim TransactionID As String = frmSign.PrintData.TransactionID
        Dim Total As String = frmSign.PrintData.Total
        Dim dtData As DataTable = frmSign.PrintData.dtData
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
        frmSign.PrintData.MerchantID = MerchantID
        frmSign.PrintData.InvoiceNumber = InvoiceNumber
        frmSign.PrintData.TransactionID = TransactionID
        frmSign.PrintData.Total = Total
        frmSign.PrintData.dtData = dtData
        frmSign.Show()
        Me.Close()
    End Sub

    Public Sub Innitial(ByVal strComportName As String)
        com.PortName = strComportName
        com.BaudRate = "9600"
        com.DataBits = 8
        com.Parity = Parity.None
        com.Handshake = Handshake.XOnXOff
        AddHandler com.DataReceived, New SerialDataReceivedEventHandler(AddressOf port_DataReceived)
    End Sub

    Private Sub frmPaymentCredit_Load(sender As Object, e As EventArgs) Handles Me.Load
        Engine.FunctionENG.PlaySound("frmPaymentCredit")
    End Sub
    Private Sub frmPaymentCredit_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            'pbOK.Image = GetOKButtonImg
            'pbCancel.Image = GetCancelButtonImg
            pbCreditCard.Image = _frmPaymentCreditCard
            Dim ini As New IniReader(Application.StartupPath & "\KioskPayment.ini")
            Innitial("Com" & ini.ReadString("HwSetting", "EDCComPort"))
            strAmt = lblPayAmt.Text.Replace(",", "").Replace(".", "").PadLeft(12, "000000000000")
            Sale(New Object, New EventArgs)
                Timer1.Interval = 10000
                Timer1.Enabled = True
                'EDC.Sale(lblPayAmt.Text.Replace(",", "").Replace(".", "").PadLeft(12, "000000000000"))



        Catch ex As Exception

        End Try
    End Sub


    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Try
            Dim arrData As ArrayList = ReadData()
            Dim strMessage As String = String.Empty
            If arrData IsNot Nothing Then
                Timer1.Enabled = False
                For i As Integer = 0 To arrData.Count - 1
                    strMessage &= arrData(i) & vbCrLf
                Next
                'MessageBox.Show(strMessage)
                pbOK_Click(New Object, New EventArgs)
            Else
                Timer1.Interval = 5000
                Timer1.Enabled = True
            End If
        Catch ex As Exception
            Timer1.Interval = 5000
            Timer1.Enabled = True
        End Try
    End Sub


    Private Function calculateLRC(ByVal s As String, ByVal offset As Integer, ByVal length As Integer)
        Dim b() As Byte = Encoding.ASCII.GetBytes(s)
        Dim l As Byte = calculateLRC(b, offset, length)
        Return l
    End Function

    Private Function calculateLRC(ByVal s As String)
        Dim l As Byte = calculateLRC(s, 0, s.Length)
    End Function

    Private Function calculateLRC(ByVal s() As Byte, ByVal offset As Integer, ByVal length As Integer)
        Dim lrc As Byte = &H0
        For i As Integer = offset To length - 1
            lrc = lrc Xor s(i)
        Next
        Return lrc
    End Function


    Private Sub port_DataReceived(sender As Object, e As SerialDataReceivedEventArgs)
        'determine the mode the user selected (binary/string)

        'user chose binary
        'retrieve number of bytes in the buffer
        Dim bytes As Integer = com.BytesToRead
        'create a byte array to hold the awaiting data
        Dim comBuffer As Byte() = New Byte(bytes - 1) {}
        'read the data and store it
        com.Read(comBuffer, 0, bytes)

        'display the data to the user
        _msg = ByteToHex(comBuffer)
        DisplayData(ByteToHex(comBuffer))
        Try

            Dim bResult() As Byte = New Byte(bytes - 1) {}
            Dim j As Integer = 0
            For i As Integer = 0 To comBuffer.Length - 1
                If comBuffer(i) = &H3 Or comBuffer(i) = &H17 Or comBuffer(i) = &H2 Or comBuffer(i) = &H1 Or comBuffer(i) = &H6 Then
                Else
                    bResult(j) = comBuffer(i)
                    j += 1
                End If
            Next

            data = Encoding.Default.GetString(bResult).Trim

        Catch ex As Exception

        End Try
    End Sub

    Public Sub txtHex_TextChanged(sender As Object, e As EventArgs) Handles txtHex.TextChanged
        Try
            If txtHex.Text.Replace(" ", "").Substring(0, 2) = "06" Then
                If sType = "Sale" Then
                    txtData.Text = String.Empty
                    Sale(strAmt)
                ElseIf sType = "Void" Then
                    txtData.Text = String.Empty
                    Invoice(New Object, New EventArgs)
                End If
                'ElseIf TextBox4.Text.Split(" ")(0) = "02" And TextBox4.Text.Split(" ")(1) = "30" Then
                '    MessageBox.Show(TextBox5.Text)
                '    If TextBox5.Text.Length > 15 Then TextBox5.Text = String.Empty
                'ElseIf TextBox4.Text.Split(" ")(0) = "02" And TextBox4.Text.Split(" ")(1) = "31" Then
                '    MessageBox.Show(TextBox5.Text)
                '    If TextBox5.Text.Length > 15 Then TextBox5.Text = String.Empty
            End If

        Catch ex As Exception

        End Try

    End Sub


#Region "DoDisplay"
    Private Sub DoDisplay(ByVal sender As Object, ByVal e As EventArgs)
        Try
            txtHex.SelectedText = String.Empty

            txtHex.AppendText(_msg)

            txtHex.ScrollToCaret()
            txtData.SelectedText = String.Empty
            txtData.AppendText(data)
            txtData.ScrollToCaret()
        Catch ex As Exception

        End Try
        
    End Sub

    Private Sub DisplayData(ByVal msg As String)
        Try
            txtHex.Invoke(New EventHandler(AddressOf DoDisplay))

        Catch ex As Exception

        End Try
    End Sub

    Public Function ByteToHex(ByVal comByte As Byte()) As String
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


    Private Sub Invoice(sender As Object, e As EventArgs)
        If Not com.IsOpen Then com.Open()
        Dim bb(1) As Byte
        bb(0) = &H6
        Dim message As String = "21000000000010000000037"
        Dim CheckSum As String = message.Length.ToString("000") & "21000000000010000000037"
        Dim data As String = "21000000000010000000037"

        Dim lrc As Byte = &H0
        Dim bData(28) As Byte
        Dim bCheck(26) As Byte
        bData(0) = &H2
        For i As Integer = 0 To CheckSum.Length - 1
            bCheck(i) = AscW(CheckSum(i))
        Next
        bCheck(bCheck.Length - 1) = &H3
        For j As Integer = 0 To bCheck.Length - 1
            bData(j + 1) = bCheck(j)
        Next
        calculateLRC(bCheck, 0, bCheck.Length)
        bData(bData.Length - 1) = calculateLRC(bCheck, 0, bCheck.Length)
        txtHex.Text = String.Empty
        txtData.Text = String.Empty
        com.Write(bData, 0, bData.Length)
    End Sub
#Region "Sale"
    Public Function Sale(ByVal Amt As String) 'Send Amount 12 digit include 2 decimal
        Try
            sType = "Sale"
            Dim strTranscode As String = "01000"
            Dim strData As String = strTranscode & Amt & "00" & "00"
            Dim CheckSum As String = strData.Length.ToString("000") & strData

            Dim lrc As Byte = &H0
            Dim bData(CheckSum.Length + 2) As Byte
            Dim bCheck(CheckSum.Length) As Byte
            bData(0) = &H2
            For i As Integer = 0 To CheckSum.Length - 1
                bCheck(i) = AscW(CheckSum(i))
            Next
            bCheck(bCheck.Length - 1) = &H3
            For j As Integer = 0 To bCheck.Length - 1
                bData(j + 1) = bCheck(j)
            Next
            bData(bData.Length - 1) = calculateLRC(bCheck, 0, bCheck.Length)
            txtHex.Text = String.Empty
            txtData.Text = String.Empty
            'strResultData = String.Empty
            com.Write(bData, 0, bData.Length)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Sub Sale(sender As Object, e As EventArgs)
        If Not com.IsOpen Then com.Open()
        Dim bb(1) As Byte
        bb(0) = &H6
        com.Write(bb, 0, 1)
        sType = "Sale"
    End Sub
#End Region

    Public Function ReadData() As ArrayList

        Try
            Dim intCnt As Integer = CInt(txtData.Text.Substring(0, 3))
            Dim strData As String = txtData.Text.Substring(0, intCnt + 3)
            Dim arrData As New ArrayList
            arrData.Add(strData.Substring(0, 3)) 'Length Data
            arrData.Add(strData.Substring(3, 5)) 'Transcode
            arrData.Add(strData.Substring(8, 3))    'Response Code
            arrData.Add(strData.Substring(11, 6))   'Approved Code
            arrData.Add(strData.Substring(17, 6))   'Invoice Number
            arrData.Add(strData.Substring(23, 12))  'Reference Code
            arrData.Add(strData.Substring(35, 8))   'Terminal ID
            arrData.Add(strData.Substring(43, 15))  'Merchant ID
            arrData.Add(strData.Substring(58, 6))   'Date
            arrData.Add(strData.Substring(64, 6))   'Time
            arrData.Add(strData.Substring(70, 2))   'Length Card Holder 
            arrData.Add(strData.Substring(72, CInt(arrData(10))))   'Card Holder Name
            arrData.Add(strData.Substring(72 + CInt(arrData(10)), 2))   'Length Card Number
            arrData.Add(strData.Substring(74 + CInt(arrData(10)), CInt(arrData(12))))   'Card Number
            arrData.Add(strData.Substring(74 + CInt(arrData(10)) + CInt(arrData(12)), 2))   'Length Card Type
            arrData.Add(strData.Substring(76 + CInt(arrData(10)) + CInt(arrData(12)), CInt(arrData(14))))   'Card Type
            arrData.Add(strData.Substring(76 + CInt(arrData(10)) + CInt(arrData(12)) + CInt(arrData(14)), 4))   'Expiry Date
            Return arrData
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
End Class