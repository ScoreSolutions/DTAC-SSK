Imports System.IO.Ports
Imports System.Threading
Imports System.Text
Imports System.Windows.Forms

Public Class EDC
    Public Shared com As New SerialPort()
    Public Shared _continue As Boolean
    Public Shared _msg As String
    Public Shared data As String
    Public Shared sType As String
    Public Shared strResultData As String
    Public Shared Text As New TextBox()
    Public Shared TextData As New TextBox()
    Public Shared strAmt As String ' strAmt = Amount 12 digit include 2 decimal
    Public Shared strInvoice As String ' Invoice Number

    Public Shared Sub Innitial(ByVal strComportName As String)
        com.PortName = strComportName
        com.BaudRate = "9600"
        com.DataBits = 8
        com.Parity = Parity.None
        com.Handshake = Handshake.XOnXOff
        AddHandler com.DataReceived, New SerialDataReceivedEventHandler(AddressOf port_DataReceived)
        AddHandler frmPaymentCredit.txtHex.TextChanged, New EventHandler(AddressOf TextBox_TextChanged)
    End Sub

    Private Shared Sub port_DataReceived(ByVal sender As Object, ByVal e As SerialDataReceivedEventArgs)
        'determine the mode the user selected (binary/string)

        'user chose binary
        'retrieve number of bytes in the buffer
        Dim bytes As Integer = com.BytesToRead
        'create a byte array to hold the awaiting data
        Dim comBuffer As Byte() = New Byte(bytes - 1) {}
        'read the data and store it
        com.Read(comBuffer, 0, bytes)
        'display the data to the user
        _msg = ByteToHex(comBuffer) + "" + Environment.NewLine + ""
        DisplayData(ByteToHex(comBuffer) + "" + Environment.NewLine + "")
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

#Region "DoDisplay"
    Private Shared Sub DoDisplay()
        Try

            frmPaymentCredit.txtHex.SelectedText = String.Empty
            frmPaymentCredit.txtHex.AppendText(_msg)
            frmPaymentCredit.txtData.SelectedText = String.Empty
            frmPaymentCredit.txtData.AppendText(data)
            'strResultData &= TextData.Text

        Catch ex As Exception

        End Try
        'TextBox1.ScrollToCaret()
    End Sub

    Private Shared Sub DisplayData(ByVal msg As String)
        Try
            frmPaymentCredit.txtHex.Invoke(New EventHandler(AddressOf DoDisplay))
            'DoDisplay()
        Catch ex As Exception

        End Try

    End Sub

    Private Shared Sub TextBox_TextChanged(sender As Object, e As EventArgs)
        Try
            If frmPaymentCredit.txtHex.Text.Replace(" ", "").Substring(0, 2) = "06" Then
                If sType = "Sale" Then

                    Sale(strAmt)
                    frmPaymentCredit.txtHex.Text = String.Empty
                    frmPaymentCredit.txtData.Text = String.Empty
                    strResultData = String.Empty

                ElseIf sType = "Void" Then

                    Void(strAmt, strInvoice)


                End If
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Shared Function ByteToHex(ByVal comByte As Byte()) As String
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

    Public Shared Function ConnectComport() As Boolean
        Try
            If Not com.IsOpen Then com.Open()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Shared Function DisconnectComport() As Boolean
        Try
            If com.IsOpen Then com.Close()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

#Region "Checksum Xor"
    Public Shared Function calculateLRC(ByVal s As String, ByVal offset As Integer, ByVal length As Integer)
        Dim b() As Byte = Encoding.ASCII.GetBytes(s)
        Dim l As Byte = calculateLRC(b, offset, length)
        Return l
    End Function

    Public Shared Function calculateLRC(ByVal s As String)
        Dim l As Byte = calculateLRC(s, 0, s.Length)
    End Function

    Public Shared Function calculateLRC(ByVal s() As Byte, ByVal offset As Integer, ByVal length As Integer)
        Dim lrc As Byte = &H0
        For i As Integer = offset To length - 1
            lrc = lrc Xor s(i)
        Next
        Return lrc
    End Function
#End Region

#Region "Sale"
    Public Shared Function Sale(ByVal Amt As String) 'Send Amount 12 digit include 2 decimal
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
            frmPaymentCredit.txtHex.Text = String.Empty
            frmPaymentCredit.txtData.Text = String.Empty
            'strResultData = String.Empty
            com.Write(bData, 0, bData.Length)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Shared Sub Sale(sender As Object, e As EventArgs)
        If Not com.IsOpen Then com.Open()
        Dim bb(1) As Byte
        bb(0) = &H6
        com.Write(bb, 0, 1)
        sType = "Sale"
    End Sub
#End Region

#Region "Void"
    Public Shared Function Void(ByVal Amt As String, ByVal Invoice As String) 'Send Amount 12 digit include 2 decimal
        Try
            sType = "Void"
            Dim strTranscode As String = "21000"
            Dim strData As String = strTranscode & Amt & Invoice
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
            Text.Text = String.Empty
            TextData.Text = String.Empty
            com.Write(bData, 0, bData.Length)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Shared Sub Void(sender As Object, e As EventArgs)
        If Not com.IsOpen Then com.Open()
        Dim bb(1) As Byte
        bb(0) = &H6
        com.Write(bb, 0, 1)
        sType = "Void"
    End Sub
#End Region

#Region "Read Data From EDC"
    Public Shared Function ReadData() As ArrayList
        Dim intCnt As Integer = CInt(frmPaymentCredit.txtData.Text.Substring(0, 3))
        Dim strData As String = frmPaymentCredit.txtData.Text.Substring(0, intCnt + 3)
        Dim arrData As New ArrayList
        Try
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
#End Region

End Class
