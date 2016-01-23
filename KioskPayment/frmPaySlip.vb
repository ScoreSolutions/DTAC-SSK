Imports System.Drawing.Printing
Imports KioskPayment.Org.Mentalis.Files
Imports System.Threading
Imports LinqDB.ConnectDB
Imports LinqDB.TABLE
Imports System.IO
Imports System.Text
Imports KioskPayment.Engine


Public Class frmPaySlip
    Dim _lnq As New LinqDB.TABLE.TsPaymentTransactionLinqDB
    Public PrintData As Print
    Dim t As Thread
    Dim lnq As TsPaymentTransactionLinqDB
    Dim PAYMENT_TYPE As String
    Public WriteOnly Property SetAccountDetail As LinqDB.TABLE.TsPaymentTransactionLinqDB
        Set(value As LinqDB.TABLE.TsPaymentTransactionLinqDB)
            _lnq = value
        End Set
    End Property


    Private _Previous As System.Nullable(Of Point) = Nothing

    Public Structure Print
        Dim MerchantID As String    'รหัส
        Dim InvoiceNumber As String
        Dim TransactionID As String
        Dim Total As String
        Dim TotalText As String
        Dim dtData As DataTable
    End Structure
    '-------- Format Data Table -------------
    'column 1 = ServiceName ชื่อ service
    'column 2 = No เลขที่บัญชี
    'column 3 = Amount ยอดรวมของ service
    'column 4 = Vat 
    'column 5 = Status 0=Dtac or Trinet , 1= biller offline
    '-------- Format Data Table -------------

#Region "Print"
    Private Sub Printreceive()

        If CheckPrinter(pd) = False Then Exit Sub
        'Dim strQuene As String = GenQueueNo(dDate.ToString("HH:mm"), Type)
        'Dim strQuene As String = GenQueueNo(Time, Type)
        'PrintQueue.QueueNo = strQuene
        'strCusType = Type
        PAYMENT_TYPE = "2"
        t = New Thread(AddressOf PrintQuene)
        t.IsBackground = True
        t.Priority = ThreadPriority.Highest
        t.Start()
        'showMsgBox("1", strQuene, "", "", New Object, New System.EventArgs)
        'CheckNotify(printHeight)
        't.Abort()
    End Sub

    Private Function PrintQuene()
        Try


            '************** Print ***************
            'PrintQueue.QueueNo = strQuene
            'PrintQueue.CountQueue = dt.Rows(0).Item("cntQuene")
            'printTicket(Queue, Mobile, CountQueue, ItemTime, ListItem)
            Dim p As New PrintDocument
            p.PrintController = New Printing.StandardPrintController
            Dim ini As New KioskPayment.Org.Mentalis.Files.IniReader(Application.StartupPath & "\KioskPayment.ini")
            'Dim pkCustomSize1 As New System.Drawing.Printing.PaperSize("Custom Paper Size", 400, 400)
            'p.PrinterSettings.DefaultPageSettings.PaperSize = pkCustomSize1


            ini.Section = "HwSetting"
            p.PrinterSettings.PrinterName = ini.ReadString("PrinterName")
            Dim dt As New DataTable
            Dim strPaymentType As String = ""
            'lnq = PaymentSlipENG.CreatePaymentTransaction(dt, strPaymentType)
            AddHandler p.PrintPage, AddressOf pd_PrintPage

            p.Print()
            lastPrintY = 0
            Dim strTime As String = ""

            'strTime = dt.Rows(0).Item(0)

            'strTime = Date.Now.Date & " " & strStartTime & ":01"

            '************************************

        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Private Sub pd_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles pd.PrintPage
        'SetUpFont()
        lastPrintY = 0

        Dim imgLogo As String = Application.StartupPath & "\Images\LogoDTAC.jpg"

        '------------------- Demo Data --------------------
        'PrintImage(Image.FromFile(imgLogo), Align.Right, e)
        'PrintText("ใบยืนยันรายการ/ใบรับชำระแทน", fn14, Align.Center, e)
        'PrintText("", fn5, Align.Center, e)
        'Dim TransTime As String = Date.Now.ToString("dd/MM/yy HH:mm", New Globalization.CultureInfo("en-US"))
        'PrintText("Date-Time: " & TransTime, fn10, Align.Left, e)
        'PrintText("รหัสตัวแทน: " & "55544455", fn10, Align.Left, e)
        'PrintText("เลขที่เอกสาร: " & "A020977788877000000001", fn10, Align.Left, e)
        'PrintText("Transaction ID: " & "84055", fn10, Align.Left, e)
        'PrintText("", fn5, Align.Center, e)
        'Print2Text("ชำระบริการ: ", "ถอนเงิน", fn10, Align.Left, e)
        'Print2Text("บัญชีมือถือ: ", "0850494803", fn10, Align.Left, e)
        'Print2Text("จำนวนเงิน(Amount): ", "1,000.00 บาท/THB", fn10, Align.Left, e)
        'Print2Text("ค่าธรรมเนียม VAT(Fee): ", "5.00 บาท/THB", fn10, Align.Left, e)
        'Print2Text("ยอดรวม(Total): ", "1,005.00 บาท/THB", fn10, Align.Left, e)
        'PrintText("(หนึ่งพันห้าบาทถ้วน)", fn10, Align.Right, e)

        'PrintTextFooter("ขอบคุณที่ใช้บริการ", fn7, Align.Center, e)
        'PrintTextFooter("กรุณาตรวจสอบความถูกต้องทุกครั้งเพื่อความถูกต้อง", fn7, Align.Center, e)
        'PrintTextFooter("**โปรดเก็บเอกสารนี้ไว้เป็นหลักฐานการชำระเงิน**", fn7, Align.Center, e)
        'PrintTextFooter("มีปัญหาขัดข้องกรุณาติดต่อ 1678", fn7, Align.Center, e)
        'PrintTextFooter("เอกสารนี้เป็นเอกสารที่ออกโดยร้านค้า", fn7, Align.Center, e)
        'PrintTextFooter("เพื่อใช้เป็นเอกสารยืนยันการทำรายการของร้านค้าเอง", fn7, Align.Center, e)
        'PrintTextFooter("มิใช่เอกสารยืนยันการทำรายการของ บมจ. โทเทิ่ล แอ็คเซ็ส คอมมูนิเคชั้น", fn7, Align.Center, e)
        '------------------- Demo Data --------------------

        PrintImage(Image.FromFile(imgLogo), Align.Right, e)
        PrintText("", fn5, Align.Center, e)
        PrintText("ใบยืนยันรายการ/ใบรับชำระแทน", fn14, Align.Center, e)
        PrintText("", fn5, Align.Center, e)
        Dim TransTime As String = Date.Now.ToString("dd/MM/yy HH:mm", New Globalization.CultureInfo("en-US"))
        PrintText("Date-Time: " & TransTime, fn10, Align.Left, e)
        PrintText("รหัสตัวแทน: " & PrintData.MerchantID, fn10, Align.Left, e)
        PrintText("เลขที่เอกสาร: " & PrintData.InvoiceNumber, fn10, Align.Left, e)
        PrintText("Transaction ID: " & PrintData.TransactionID, fn10, Align.Left, e)
        PrintText("", fn5, Align.Center, e)
        Dim dt As DataTable = PrintData.dtData
        For Each dr As DataRow In dt.Rows
            Print2Text("ชำระบริการ: ", dr.Item("ServiceName"), fn10, Align.Left, e)
            If dr.Item("Status") = "0" Then
                Print2Text("บัญชีมือถือ: ", dr.Item("No"), fn10, Align.Left, e)
            Else
                Print2Text("บัญชีเลขที่: ", dr.Item("No"), fn10, Align.Left, e)
            End If
            Print2Text("จำนวนเงิน(Amount): ", FormatCurrency((dr.Item("Amount")), 2) & " บาท/THB", fn10, Align.Left, e)
            Print2Text("ค่าธรรมเนียม VAT(Fee): ", FormatCurrency((dr.Item("Vat")), 2) & " บาท/THB", fn10, Align.Left, e)
        Next
        PrintText("", fn5, Align.Center, e)
        Print2Text("ยอดรวม(Total):", FormatCurrency(PrintData.Total, 2) & " บาท/THB", fn11b, Align.Left, e)
        'PrintText("(หนึ่งพันห้าบาทถ้วน)", fn10, Align.Right, e)
        PrintText("", fn5, Align.Center, e)
        PrintTextFooter("ขอบคุณที่ใช้บริการ", fn7, Align.Center, e)
        PrintTextFooter("กรุณาตรวจสอบความถูกต้องทุกครั้งเพื่อความถูกต้อง", fn7, Align.Center, e)
        PrintTextFooter("**โปรดเก็บเอกสารนี้ไว้เป็นหลักฐานการชำระเงิน**", fn7, Align.Center, e)
        PrintTextFooter("มีปัญหาขัดข้องกรุณาติดต่อ 1678", fn7, Align.Center, e)
        PrintTextFooter("เอกสารนี้เป็นเอกสารที่ออกโดยร้านค้า", fn7, Align.Center, e)
        PrintTextFooter("เพื่อใช้เป็นเอกสารยืนยันการทำรายการของร้านค้าเอง", fn7, Align.Center, e)
        PrintTextFooter("มิใช่เอกสารยืนยันการทำรายการของ บมจ. โทเทิ่ล แอ็คเซ็ส คอมมูนิเคชั้น", fn7, Align.Center, e)
        PrintText("", fn5, Align.Center, e)
        e.HasMorePages = False
    End Sub
#End Region

    Private Sub pbPrint_Click(sender As Object, e As EventArgs) Handles pbPrint.Click
        Printreceive()
        frmQuestionNare.Show()
        frmPaymentCash.Close()
        frmPaymentCashSum.Close()
        Me.Close()
    End Sub

    Private Sub pbNonPrint_Click(sender As Object, e As EventArgs) Handles pbNonPrint.Click
        frmQuestionNare.Show()
        frmPaymentCash.Close()
        frmPaymentCashSum.Close()
        Me.Close()
    End Sub
End Class