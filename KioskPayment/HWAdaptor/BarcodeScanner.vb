Imports System.IO.Ports
Imports System.Text
Public Class BarcodeScanner
    Public Shared com As New SerialPort()
    Public Shared _msg As String
    Public Shared data As String
    Public Shared strResultData As String

    Public Shared Sub Innitial(ByVal strComportName As String)
        com.PortName = strComportName
        com.BaudRate = "9600"
        com.DataBits = 8
        com.Parity = Parity.None
        com.Handshake = Handshake.XOnXOff
        AddHandler com.DataReceived, New SerialDataReceivedEventHandler(AddressOf port_DataReceived)
    End Sub

    Public Shared Sub port_DataReceived(ByVal sender As Object, ByVal e As SerialDataReceivedEventArgs)
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
    Public Shared Function DoDisplay()
        strResultData = String.Empty
        strResultData = data
        Return True
    End Function

    Public Shared Sub DisplayData(ByVal msg As String)

        strResultData = DoDisplay()

    End Sub

    Public Shared Function ByteToHex(ByVal comByte As Byte()) As String
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

#Region "Command"
    Public Shared Sub Enable()
        If Not com.IsOpen Then com.Open()
        Dim bb(1) As Byte
        bb(0) = &HA0
        com.Write(bb, 0, 1)
    End Sub
    Public Shared Sub Disable()
        If Not com.IsOpen Then com.Open()
        Dim bb(1) As Byte
        bb(0) = &H55
        com.Write(bb, 0, 1)
    End Sub

    Public Shared Sub Reboot()
        If Not com.IsOpen Then com.Open()
        Dim bb(1) As Byte
        bb(0) = &HA5
        com.Write(bb, 0, 1)
    End Sub

#End Region

    Public Shared Function Read() As String
        Return strResultData
    End Function
End Class


