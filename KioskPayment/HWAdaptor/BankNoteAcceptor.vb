Imports System.IO.Ports
Imports System.Text
Imports System.Drawing
Public Class BankNoteAcceptor
    Public Shared com As New SerialPort
    Public Shared _msg As String
    Public Shared message As String
    Public Shared strResultData As String
    Private Shared Sub Innitial(ByVal strComportName As String)
        com.PortName = strComportName
        com.BaudRate = "38400"
        com.DataBits = 8
        com.Parity = Parity.None
        com.Handshake = Handshake.XOnXOff
        AddHandler com.DataReceived, New SerialDataReceivedEventHandler(AddressOf port_DataReceived)
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

    End Sub

#Region "DoDisplay"
    Private Shared Function DoDisplay()
        strResultData = String.Empty

        strResultData = _msg
        Return True
        'TextBox1.ScrollToCaret()
    End Function

    Private Shared Sub DisplayData(ByVal msg As String)

        strResultData = DoDisplay()

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

    Private Shared Function ConnectComport() As Boolean
        Try
            If Not com.IsOpen Then com.Open()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Shared Function DisconnectComport() As Boolean
        Try
            If com.IsOpen Then com.Close()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

#Region "Send Command Data"
    Private Shared Function Read() As ArrayList
        If com.IsOpen = False Then com.Open()
        Try
            Dim bRead(4) As Byte
            bRead(0) = &H2
            bRead(1) = &H34
            bRead(2) = &H3
            bRead(3) = &H35
            com.Write(bRead, 0, 4)

            Dim strData() As String = _msg.Split(" ")
            Try
                If _msg.Split(" ")(0) = "05" Then

                    Dim strBillAmount As String = String.Empty
                    Dim strCoinAmount As String = String.Empty
                    For i As Integer = 1 To 5
                        strBillAmount &= strData(i).Substring(1, 1)
                    Next

                    For i As Integer = 6 To 10
                        strCoinAmount &= strData(i).Substring(1, 1)
                    Next
                    Dim intBank1 As String = String.Empty
                    Dim intBank2 As String = String.Empty
                    Dim intBank3 As String = String.Empty
                    Dim intBank4 As String = String.Empty
                    Dim intBank5 As String = String.Empty
                    Dim arrResult As New ArrayList
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
                    Return arrResult
                End If
            Catch ex As Exception
                Return Nothing
            End Try
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Shared Function Reset() As Boolean
        If com.IsOpen = False Then
            Return False
        End If
        Try
            Dim bReset(4) As Byte
            bReset(0) = &H2
            bReset(1) = &H31
            bReset(2) = &H3
            bReset(3) = &H30
            com.Write(bReset, 0, 4)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Shared Function Enable() As Boolean
        If com.IsOpen = False Then
            Return False
        End If
        Try
            Dim bEnable(4) As Byte
            bEnable(0) = &H2
            bEnable(1) = &H32
            bEnable(2) = &H3
            bEnable(3) = &H33
            com.Write(bEnable, 0, 4)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Shared Function Disable() As Boolean
        If com.IsOpen = False Then
            Return False
        End If
        Try
            Dim bDisable(4) As Byte
            bDisable(0) = &H2
            bDisable(1) = &H33
            bDisable(2) = &H3
            bDisable(3) = &H32
            com.Write(bDisable, 0, 4)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Shared Function Clear() As Boolean
        If com.IsOpen = False Then
            Return False
        End If
        Try
            Dim bClear(4) As Byte
            bClear(0) = &H2
            bClear(1) = &H35
            bClear(2) = &H3
            bClear(3) = &H34
            com.Write(bClear, 0, 4)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
#End Region

End Class
