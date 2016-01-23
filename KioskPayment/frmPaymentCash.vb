Imports KioskPayment.Org.Mentalis.Files
Imports KioskPayment.Engine
Imports System.IO.Ports
Imports System.Text
Public Class frmPaymentCash
    Dim com As New SerialPort
    Public Shadows _msg As String
    Public Shared message As String
    Dim BillAmount As Integer = 0
    Dim CoinAmount As Integer = 0
    Dim Bank1 As Integer = 0
    Dim Bank2 As Integer = 0
    Dim Bank3 As Integer = 0
    Dim Bank4 As Integer = 0
    Dim Bank5 As Integer = 0
    Dim Total As Double = 0
    Dim arrResult As New ArrayList

    Public WriteOnly Property SetMoneyAmount As Double
        Set(value As Double)
            lblAmount.Text = value.ToString("#,##0.00")
        End Set
    End Property
    Dim CountWaitForCash As Integer = 0

    Private Sub frmPaymentCash_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Disable()
        com.Close()
    End Sub

    Private Sub frmPaymentCash_Load(sender As Object, e As EventArgs) Handles Me.Load
        FunctionENG.PlaySound("frmPaymentCash")
    End Sub


    Private Sub frmPaymentCash_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            Me.BackgroundImage = _frmPaymentCash
            Dim ini As New IniReader(Application.StartupPath & "\KioskPayment.ini")
            Total = CDbl(lblAmount.Text)
            com.PortName = "Com" & ini.ReadString("HwSetting", "Acceptor")
            com.BaudRate = "38400"
            com.DataBits = 8
            com.Parity = Parity.None
            'com.ReadTimeout = 10000
            com.Handshake = Handshake.XOnXOff
            AddHandler com.DataReceived, New SerialDataReceivedEventHandler(AddressOf port_DataReceived)

            Enable()
            'WaitTimeForPayment = 30

        Catch ex As Exception

        End Try
    End Sub

    Private Sub TimerRead_Tick(sender As Object, e As EventArgs) Handles TimerRead.Tick
        Read()
        If (CInt(BillAmount) + CInt(CoinAmount)) >= Total Then
            TimerRead.Enabled = False
            TimerWaitReturn.Enabled = False
            Dim frm As New frmPaymentCashSum
            frm.arrData = arrResult
            Disable()
            'com.Close()
            If frm.ShowDialog = Windows.Forms.DialogResult.OK Then
                Me.Close()
            End If

        End If
    End Sub
    Private Sub TimerWaitReturn_Tick(sender As Object, e As EventArgs) Handles TimerWaitReturn.Tick
        If CountWaitForCash < WaitTimeForPayment Then
            CountWaitForCash += 1
        Else
            CountWaitForCash = 0
            TimerWaitReturn.Enabled = False
            Dim ff As New frmSoryForReturn
            If ff.ShowDialog = Windows.Forms.DialogResult.OK Then
                TimerWaitReturn.Enabled = True
                Me.BringToFront()
            Else
                frmChangeLanguage.Show()
                Me.Close()
            End If
        End If
    End Sub
#Region "Command"

    Sub Clear()
        Dim bClear(4) As Byte
        bClear(0) = &H2
        bClear(1) = &H35
        bClear(2) = &H3
        bClear(3) = &H34
        If com.IsOpen = False Then com.Open()

        com.Write(bClear, 0, 4)
    End Sub

    Sub Enable()
        Dim bClear(4) As Byte
        bClear(0) = &H2
        bClear(1) = &H35
        bClear(2) = &H3
        bClear(3) = &H34

        Dim bEnable(4) As Byte
        bEnable(0) = &H2
        bEnable(1) = &H32
        bEnable(2) = &H3
        bEnable(3) = &H33
        Try
            If com.IsOpen = False Then com.Open()
            com.Write(bClear, 0, 4)
            com.Close()
            com.Open()
            com.Write(bEnable, 0, 4)
            com.Close()

        Catch ex As Exception

        End Try

    End Sub

    Sub Read()
        Try

            Dim bRead(4) As Byte
            bRead(0) = &H2
            bRead(1) = &H34
            bRead(2) = &H3
            bRead(3) = &H35

            AddHandler com.DataReceived, New SerialDataReceivedEventHandler(AddressOf port_DataReceived)
            If com.IsOpen = False Then com.Open()
            com.Write(bRead, 0, 4)
            Dim strData() As String = _msg.Split(" ")
            Try
                If _msg.Split(" ")(0) = "05" Then
                    arrResult = New ArrayList
                    Dim strBillAmount As String = ""
                    Dim strCoinAmount As String = ""
                    Dim intBank1 As String = ""
                    Dim intBank2 As String = ""
                    Dim intBank3 As String = ""
                    Dim intBank4 As String = ""
                    Dim intBank5 As String = ""

                    For i As Integer = 1 To 5
                        strBillAmount &= strData(i).Substring(1, 1)
                    Next

                    For i As Integer = 6 To 10
                        strCoinAmount &= strData(i).Substring(1, 1)
                    Next


                    For i As Integer = 11 To 13
                        intBank1 &= strData(i).Substring(1, 1)
                    Next

                    For i As Integer = 14 To 16
                        intBank2 &= strData(i).Substring(1, 1)
                    Next

                    For i As Integer = 17 To 19
                        intBank3 &= strData(i).Substring(1, 1)
                    Next

                    For i As Integer = 20 To 22
                        intBank4 &= strData(i).Substring(1, 1)
                    Next

                    For i As Integer = 23 To 25
                        intBank5 &= strData(i).Substring(1, 1)
                    Next
                    arrResult.Add(CInt(strBillAmount))
                    arrResult.Add(CInt(strCoinAmount))
                    arrResult.Add(CInt(intBank1))
                    arrResult.Add(CInt(intBank2))
                    arrResult.Add(CInt(intBank3))
                    arrResult.Add(CInt(intBank4))
                    arrResult.Add(CInt(intBank5))
                    If CInt(strBillAmount) > BillAmount Or CInt(strCoinAmount) > CoinAmount Then
                        'form.arrData = arrResult
                        lblAmount.Text = FormatCurrency((CDbl(Total) - (CInt(strBillAmount) + CInt(strCoinAmount))), 2)
                    End If

                    BillAmount = CInt(strBillAmount)
                    CoinAmount = CInt(strCoinAmount)
                    Bank1 = CInt(intBank1)
                    Bank2 = CInt(intBank2)
                    Bank3 = CInt(intBank3)
                    Bank4 = CInt(intBank4)
                    Bank5 = CInt(intBank5)



                End If
            Catch ex As Exception

            End Try

        Catch ex As Exception

        End Try
    End Sub

    Sub Reset()
        Dim bReset(4) As Byte
        bReset(0) = &H2
        bReset(1) = &H31
        bReset(2) = &H3
        bReset(3) = &H30
        If com.IsOpen = False Then com.Open()

        com.Write(bReset, 0, 4)
    End Sub

    Sub Disable()
        Dim bDisable(4) As Byte
        bDisable(0) = &H2
        bDisable(1) = &H33
        bDisable(2) = &H3
        bDisable(3) = &H32
        If com.IsOpen = True Then
            'com.Open()
            com.Write(bDisable, 0, 4)
        Else
            Try
                com.Open()
                com.Write(bDisable, 0, 4)

            Catch ex As Exception

            End Try

        End If



    End Sub
#End Region

    Private Sub port_DataReceived(ByVal sender As Object, ByVal e As SerialDataReceivedEventArgs)
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
    Private Sub DisplayData(ByVal msg As String)

        TextBox1.Invoke(New EventHandler(AddressOf DoDisplay))

    End Sub

#Region "DoDisplay"
    Private Sub DoDisplay(ByVal sender As Object, ByVal e As EventArgs)
        TextBox1.SelectedText = String.Empty

        TextBox1.AppendText(_msg)
        'TextBox1.ScrollToCaret()
    End Sub
#End Region


End Class