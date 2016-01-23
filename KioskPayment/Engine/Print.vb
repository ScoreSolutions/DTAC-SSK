Imports System.Data
Imports System.Data.SqlClient
Imports PrinterClassDll
Imports System.IO
Imports System
Imports System.Runtime.InteropServices
Imports System.Resources
Imports KioskPayment.Org.Mentalis.Files
Imports System.Threading
Imports System.Drawing.Printing
Imports Microsoft.Win32
Imports LinqDB.ConnectDB

Module Print
    Public INIFName As String = Application.StartupPath & "\KioskPayment.ini"  'Parth ที่ใช้เก็บไฟล์ .ini
    Public CountVDO = 0
    Public MaxCountVDO As Int32 = 120
    Public Mobile As String = ""
    Public CustomerTypeID As Int32 = 0
    Public DefaultCustomerTypeID As Int32 = 0
    Public Campang As String = ""
    Public Campaign_TH As String = ""
    Public Campaign_EN As String = ""
    Public Campaign_Desc1_EN As String = ""
    Public Campaign_Desc2_EN As String = ""
    Public Campaign_Desc1_TH As String = ""
    Public Campaign_Desc2_TH As String = ""
    Public AssetID As String = ""
    Public NetworkType As String = ""
    Public Segment As String = ""
    Public CustomerName As String = ""
    Public Appointment As Boolean = False
    Public CustomerApp As Boolean = False
    Public Lang_TH As Boolean = True
    Public ConnecetionPrimaryDB As Boolean = True
    Public Conn As New SqlConnection
    Public fn5 As New Font("Tahoma", 5, FontStyle.Regular)
    Public fn7 As New Font("Tahoma", 7, FontStyle.Regular)
    Public fn8 As New Font("Tahoma", 8, FontStyle.Regular)
    Public fn10 As New Font("Tahoma", 10, FontStyle.Regular)
    Public fn11 As New Font("Tahoma", 11, FontStyle.Regular)
    Public fn12 As New Font("Tahoma", 12, FontStyle.Regular)
    Public fn13 As New Font("Tahoma", 13, FontStyle.Regular)
    Public fn14 As New Font("Tahoma", 14, FontStyle.Regular)
    Public fn16 As New Font("Tahoma", 16, FontStyle.Regular)
    Public fn8b As New Font("Tahoma", 8, FontStyle.Bold)
    Public fn9b As New Font("Tahoma", 9, FontStyle.Bold)
    Public fn10b As New Font("Tahoma", 10, FontStyle.Bold)
    Public fn11b As New Font("Tahoma", 11, FontStyle.Bold)
    Public fn12b As New Font("Tahoma", 12, FontStyle.Bold)
    Public fn13b As New Font("Tahoma", 13, FontStyle.Bold)
    Public fn14b As New Font("Tahoma", 14, FontStyle.Bold)
    Public fn16b As New Font("Tahoma", 16, FontStyle.Bold)
    Public fn24b As New Font("Tahoma", 24, FontStyle.Bold)
    Public fn36b As New Font("Tahoma", 36, FontStyle.Bold)
    Public fn42b As New Font("Tahoma", 42, FontStyle.Bold)
    Public lastPrintY As Integer = 0
#Region "Print"

    Sub PrintText(ByVal txt As String, ByVal fnt As Font, ByVal align As Int16, ByRef e As System.Drawing.Printing.PrintPageEventArgs)
        Dim w As Integer = e.Graphics.MeasureString(txt, fnt).Width + 8
        Dim h As Integer = e.Graphics.MeasureString(txt, fnt).Height - 10
        Dim x As Integer = 0
        Dim y As Integer = 0
        Dim brsh As New SolidBrush(Color.FromArgb(0, 0, 0))
        Select Case align
            Case 0 'Default, LEFT
                x = e.PageSettings.PrintableArea.Left
                y = e.PageSettings.PrintableArea.Top + lastPrintY
            Case 1 'CENTER
                x = e.PageSettings.PrintableArea.Width / 2 - w / 2
                y = e.PageSettings.PrintableArea.Top + lastPrintY
            Case 2 'RIGHT
                x = e.PageSettings.PrintableArea.Right - w
                y = e.PageSettings.PrintableArea.Top + lastPrintY
        End Select
        e.Graphics.DrawString(txt, fnt, brsh, x, y)
        'TextRenderer.DrawText(e.Graphics, txt, fnt, New Point(x, y), SystemColors.ControlText)
        lastPrintY = y + h
    End Sub

    Sub PrintTextFooter(ByVal txt As String, ByVal fnt As Font, ByVal align As Int16, ByRef e As System.Drawing.Printing.PrintPageEventArgs)
        Dim w As Integer = e.Graphics.MeasureString(txt, fnt).Width + 8
        Dim h As Integer = e.Graphics.MeasureString(txt, fnt).Height - 15
        Dim x As Integer = 0
        Dim y As Integer = 0
        Dim brsh As New SolidBrush(Color.FromArgb(0, 0, 0))
        Select Case align
            Case 0 'Default, LEFT
                x = e.PageSettings.PrintableArea.Left
                y = e.PageSettings.PrintableArea.Top + lastPrintY
            Case 1 'CENTER
                x = e.PageSettings.PrintableArea.Width / 2 - w / 2
                y = e.PageSettings.PrintableArea.Top + lastPrintY
            Case 2 'RIGHT
                x = e.PageSettings.PrintableArea.Right - w
                y = e.PageSettings.PrintableArea.Top + lastPrintY
        End Select
        e.Graphics.DrawString(txt, fnt, brsh, x, y)
        'TextRenderer.DrawText(e.Graphics, txt, fnt, New Point(x, y), SystemColors.ControlText)
        lastPrintY = y + h
    End Sub

    Sub Print2Text(ByVal txt As String, ByVal txt2 As String, ByVal fnt As Font, ByVal align As Int16, ByRef e As System.Drawing.Printing.PrintPageEventArgs)
        Dim w As Integer = e.Graphics.MeasureString(txt, fnt).Width + 8
        Dim h As Integer = e.Graphics.MeasureString(txt, fnt).Height - 10
        Dim w2 As Integer = e.Graphics.MeasureString(txt2, fnt).Width + 8
        Dim h2 As Integer = e.Graphics.MeasureString(txt2, fnt).Height - 10
        Dim x As Integer = 0
        Dim y As Integer = 0
        Dim x2 As Integer = 0
        Dim y2 As Integer = 0
        Dim brsh As New SolidBrush(Color.FromArgb(0, 0, 0))
        Select Case align
            Case 0 'Default, LEFT
                x = e.PageSettings.PrintableArea.Left
                y = e.PageSettings.PrintableArea.Top + lastPrintY
                x2 = e.PageSettings.PrintableArea.Right - w2
                y2 = e.PageSettings.PrintableArea.Top + lastPrintY
            Case 1 'CENTER
                x = e.PageSettings.PrintableArea.Width / 2 - w / 2
                y = e.PageSettings.PrintableArea.Top + lastPrintY
            Case 2 'RIGHT
                x = e.PageSettings.PrintableArea.Right - w
                y = e.PageSettings.PrintableArea.Top + lastPrintY
        End Select
        e.Graphics.DrawString(txt, fnt, brsh, x, y)
        e.Graphics.DrawString(txt2, fnt, brsh, x2, y2)
        'TextRenderer.DrawText(e.Graphics, txt, fnt, New Point(x, y), SystemColors.ControlText)
        lastPrintY = y + h
    End Sub
    Sub PrintImage(ByVal img As Image, ByVal align As Int16, ByRef e As System.Drawing.Printing.PrintPageEventArgs)
        Dim w As Integer = img.Width + 30
        Dim h As Integer = img.Height
        Dim x As Integer = 0
        Dim y As Integer = 0

        Select Case align
            Case 0 'Default, LEFT
                x = e.PageSettings.PrintableArea.Left
                y = e.PageSettings.PrintableArea.Top + lastPrintY
            Case 1 'CENTER
                x = e.PageSettings.PrintableArea.Width / 2 - w / 2
                y = e.PageSettings.PrintableArea.Top + lastPrintY
            Case 2 'RIGHT
                x = e.PageSettings.PrintableArea.Right - w
                y = e.PageSettings.PrintableArea.Top + lastPrintY
        End Select
        e.Graphics.DrawImage(img, x, y)

        lastPrintY = y + h
    End Sub

    Sub PrintTextImage(ByVal txt As String, ByVal fnt As Font, ByVal img As Image, ByVal align As Int16, ByRef e As System.Drawing.Printing.PrintPageEventArgs)
        Dim w As Integer = img.Width
        Dim h As Integer = img.Height
        Dim ini As New KioskPayment.Org.Mentalis.Files.IniReader(INIFName)
        ini.Section = "SETTING"
        'If ini.ReadString("PaperSize") = 60 Then w = 30 : h = 30
        Dim wt As Integer = e.Graphics.MeasureString(txt, fnt).Width
        Dim ht As Integer = e.Graphics.MeasureString(txt, fnt).Height
        Dim x As Integer = 0
        Dim y As Integer = 0
        Dim brsh As New SolidBrush(Color.FromArgb(0, 0, 0))
        Dim str As String = ""
        Select Case align
            Case 0 'Default, LEFT
                x = e.PageSettings.PrintableArea.Left
                y = e.PageSettings.PrintableArea.Top + lastPrintY
            Case 1 'CENTER
                x = (e.PageSettings.PrintableArea.Width / 2) - ((w + wt + 5) / 2)
                y = e.PageSettings.PrintableArea.Top + lastPrintY
            Case 2 'RIGHT
                x = e.PageSettings.PrintableArea.Right - (w + wt)
                y = e.PageSettings.PrintableArea.Top + lastPrintY
        End Select
        e.Graphics.DrawImage(img, x - 100, y, w, h)
        If InStr(txt, vbCrLf) Then
            Dim font As Font = fnt
            font = New Font("Tahoma", fnt.Size + 1, FontStyle.Bold)
            e.Graphics.DrawString(txt.Split(vbCrLf)(0), font, brsh, x + w + 5, (h - ht) / 2)
            e.Graphics.DrawString(txt.Split(vbCrLf)(1), fnt, brsh, x + w + 5, (((h - ht) / 2) / 2) + (ht / 3))
        Else
            e.Graphics.DrawString(txt, fnt, brsh, x + w + 5, (h - ht) / 2)
        End If
        lastPrintY = y + h
    End Sub

    Sub PrintImagewithText(ByVal txt As String, ByVal fnt As Font, ByVal img As Image, ByVal align As Int16, ByRef e As System.Drawing.Printing.PrintPageEventArgs)
        Dim w As Integer = 172
        Dim h As Integer = 92

        'If ini.ReadString("PaperSize") = 60 Then w = 30 : h = 30
        Dim wt As Integer = e.Graphics.MeasureString(txt, fnt).Width
        Dim ht As Integer = e.Graphics.MeasureString(txt, fnt).Height
        Dim x As Integer = 0
        Dim y As Integer = 0
        Dim brsh As New SolidBrush(Color.FromArgb(0, 0, 0))
        Dim str As String = ""
        Select Case align
            Case 0 'Default, LEFT
                x = e.PageSettings.PrintableArea.Left
                y = e.PageSettings.PrintableArea.Top + lastPrintY
            Case 1 'CENTER
                x = (e.PageSettings.PrintableArea.Width / 2) - ((w + wt + 5) / 2)
                y = e.PageSettings.PrintableArea.Top + lastPrintY
            Case 2 'RIGHT
                x = e.PageSettings.PrintableArea.Right - (w + wt)
                y = e.PageSettings.PrintableArea.Top + lastPrintY
        End Select
        e.Graphics.DrawImage(img, x + wt + 10, y, w, h)
        e.Graphics.DrawString(txt, fnt, brsh, x, y + h - 20)

        lastPrintY = y + h
    End Sub

    Public Sub SetUpFont()
        Dim ini As New KioskPayment.Org.Mentalis.Files.IniReader(INIFName)
        ini.Section = "SETTING"
        If ini.ReadString("PaperSize") = 60 Then
            fn8b = New Font("Tahoma", 6, FontStyle.Bold)
            fn10b = New Font("Tahoma", 8, FontStyle.Bold)
            'fn12b = New Font("Tahoma", 8, FontStyle.Bold)
        End If
    End Sub

    Public Enum Align
        Left
        Center
        Right
    End Enum

    Public Function CheckPrinter(ByVal pd As PrintDocument) As Boolean
        Dim ini As New KioskPayment.Org.Mentalis.Files.IniReader(INIFName)
        ini.Section = "HwSetting"
        pd.PrinterSettings.PrinterName = ini.ReadString("PrinterName")
        If pd.PrinterSettings.IsValid Then
            Return True
        Else
            Return False
        End If
    End Function

#End Region

    Private Enum PrinterStatus
        PrinterIdle = 3
        PrinterPrinting = 4
        PrinterWarmingUp = 5
    End Enum

    Private Function PrinterStatusToString(ByVal ps As PrinterStatus) As String
        Dim str As String = ""
        Select Case ps
            Case PrinterStatus.PrinterIdle
                Return "idle"
            Case PrinterStatus.PrinterPrinting
                Return "printing"
            Case PrinterStatus.PrinterWarmingUp
                Return "warmup"
            Case Else
                Return "unknown"
        End Select
        Return str
    End Function

    Public Function StringFromRight(ByVal strTmp As String, ByVal strLength As Integer) As String
        If (strLength > 0 And strTmp.Length >= strLength) Then
            Return strTmp.Substring(strTmp.Length - strLength, strLength)
        Else
            Return strTmp
        End If
    End Function

    Function FindDateNow() As Date
        Dim sql As String = ""
        Dim dDate As New DateTime
        dDate = SqlDB.GetDateNowFromDB(Nothing)
        Return dDate
    End Function
End Module
