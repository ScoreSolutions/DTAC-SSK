Imports System.Drawing.Printing
Imports System.IO
Imports LinqDB.ConnectDB
Imports KioskPayment.Org.Mentalis.Files
Imports KioskPayment.Engine
Public Class frmPaymentCreditSign
    Private _Previous As System.Nullable(Of Point) = Nothing
    Public PrintData As Print
    Dim CountTimeReturn As Integer = 0
    'Dim CaptureEnable As String = ""
    Dim imgFace As Image
    Public Structure Print
        Dim MerchantID As String
        Dim InvoiceNumber As String
        Dim TransactionID As String
        Dim Total As String
        Dim TotalText As String
        Dim dtData As DataTable
    End Structure

    Private Property WebCam As WebCam

    '-------- Format Data Table -------------
    'column 1 = ServiceName ชื่อ service
    'column 2 = No เลขที่บัญชี
    'column 3 = Amount ยอดรวมของ service
    'column 4 = Vat 
    'column 5 = Status 0=Dtac or Trinet , 1= biller offline
    '-------- Format Data Table -------------
    Private Sub pbPaymentCreditSign_MouseDown(sender As Object, e As MouseEventArgs) Handles pbPaymentCreditSign.MouseDown
        _Previous = e.Location
        pbPaymentCreditSign_MouseMove(sender, e)
    End Sub

    Private Sub pbPaymentCreditSign_MouseMove(sender As Object, e As MouseEventArgs) Handles pbPaymentCreditSign.MouseMove
        If _Previous IsNot Nothing Then
            If pbPaymentCreditSign.Image Is Nothing Then
                Dim bmp As New Bitmap(pbPaymentCreditSign.Width, pbPaymentCreditSign.Height)
                Using g As Graphics = Graphics.FromImage(bmp)
                    g.Clear(Color.White)
                End Using
                pbPaymentCreditSign.Image = bmp
            End If
            Using g As Graphics = Graphics.FromImage(pbPaymentCreditSign.Image)
                Dim p As New Pen(Brushes.Black, 3)
                g.DrawLine(p, _Previous.Value, e.Location)
                g.Save()
            End Using
            pbPaymentCreditSign.Invalidate()
            _Previous = e.Location
        End If
    End Sub

    Private Sub pbPaymentCreditSign_MouseUp(sender As Object, e As MouseEventArgs) Handles pbPaymentCreditSign.MouseUp
        _Previous = Nothing
        SaveImageCapture(PrintData.InvoiceNumber)
    End Sub

    Private Sub pbOK_Click(sender As Object, e As EventArgs) Handles pbOK.Click
        If pbPaymentCreditSign.Enabled = False Then
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
                If ff.ShowDialog = Windows.Forms.DialogResult.OK Then
                    CreateNewPaymentList()
                    frmSelectPaymentType.Show()
                Else
                    frmChangeLanguage.Show()
                End If
                Me.Close()
            Else
                frmThankYou.Show()
                Me.Close()
            End If
        End If
        Dim CustSign As String = Application.StartupPath & "\SignImage\TmpCustSign.jpg"
        If IO.Directory.Exists(Application.StartupPath & "\SignImage") = False Then
            IO.Directory.CreateDirectory(Application.StartupPath & "\SignImage")
        End If

        pbPaymentCreditSign.Image.Save(CustSign, System.Drawing.Imaging.ImageFormat.Jpeg)
        If IO.File.Exists(CustSign) = True Then

            Dim p As New PrintDocument
            p.PrintController = New Printing.StandardPrintController
            Dim ini As New KioskPayment.Org.Mentalis.Files.IniReader(Application.StartupPath & "\KioskPayment.ini")
            ini.Section = "HwSetting"
            p.PrinterSettings.PrinterName = ini.ReadString("PrinterName")
            AddHandler p.PrintPage, AddressOf pd_PrintPage
            lastPrintY = 0
            PrintPreviewControl1.Document = pd
            PrintPreviewControl1.Update()
            PrintPreviewControl1.Refresh()
            'Application.DoEvents()

            pbPaymentCreditSign.Enabled = False
            'Dim trans As New LinqDB.ConnectDB.TransactionDB
            '_lnq.CUSTOMER_SIGN_BYTE = IO.File.ReadAllBytes(CustSign)

            'If _lnq.UpdateByPK("KIOSK", trans.Trans) = True Then
            '    trans.CommitTransaction()
            'Else
            '    trans.RollbackTransaction()
            'End If

            Try
                If File.Exists(Application.StartupPath & "\CaptureImages\" & PrintData.InvoiceNumber & ".jpg") Then
                    pbDisplay.BackgroundImage = Image.FromFile(Application.StartupPath & "\CaptureImages\" & PrintData.InvoiceNumber & ".jpg")
                    pbDisplay.BackgroundImageLayout = ImageLayout.Stretch
                    pbDisplay.Refresh()
                End If
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Function Prints()
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
            'p.Print()
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

        'Try
        '    If File.Exists(Application.StartupPath & "\SignImage\TmpCustSign.jpg") Then
        '        PrintImagewithText("Signature :", fn10, Image.FromFile(Application.StartupPath & "\SignImage\TmpCustSign.jpg"), Align.Left, e)
        '    End If
        'Catch ex As Exception

        'End Try
        'PrintTextFooter("ขอบคุณที่ใช้บริการ", fn7, Align.Center, e)
        'PrintTextFooter("กรุณาตรวจสอบความถูกต้องทุกครั้งเพื่อความถูกต้อง", fn7, Align.Center, e)
        'PrintTextFooter("**โปรดเก็บเอกสารนี้ไว้เป็นหลักฐานการชำระเงิน**", fn7, Align.Center, e)
        'PrintTextFooter("มีปัญหาขัดข้องกรุณาติดต่อ 1678", fn7, Align.Center, e)
        'PrintTextFooter("เอกสารนี้เป็นเอกสารที่ออกโดยร้านค้า", fn7, Align.Center, e)
        'PrintTextFooter("เพื่อใช้เป็นเอกสารยืนยันการทำรายการของร้านค้าเอง", fn7, Align.Center, e)
        'PrintTextFooter("มิใช่เอกสารยืนยันการทำรายการของ บมจ. โทเทิ่ล แอ็คเซ็ส คอมมูนิเคชั้น", fn7, Align.Center, e)
        'Dim dDate As DateTime = SqlDB.GetDateNowFromDB(Nothing)

        PrintImage(Image.FromFile(imgLogo), Align.Right, e)

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
        Print2Text("ยอดรวม(Total): ", FormatCurrency(PrintData.Total, 2) & " บาท/THB", fn12b, Align.Left, e)
        'PrintText("(หนึ่งพันห้าบาทถ้วน)", fn10, Align.Right, e)

        Try
            If File.Exists(Application.StartupPath & "\SignImage\TmpCustSign.jpg") Then
                PrintImagewithText("Signature :", fn10, Image.FromFile(Application.StartupPath & "\SignImage\TmpCustSign.jpg"), Align.Left, e)
            End If
        Catch ex As Exception

        End Try
        PrintTextFooter("ขอบคุณที่ใช้บริการ", fn7, Align.Center, e)
        PrintTextFooter("กรุณาตรวจสอบความถูกต้องทุกครั้งเพื่อความถูกต้อง", fn7, Align.Center, e)
        PrintTextFooter("**โปรดเก็บเอกสารนี้ไว้เป็นหลักฐานการชำระเงิน**", fn7, Align.Center, e)
        PrintTextFooter("มีปัญหาขัดข้องกรุณาติดต่อ 1678", fn7, Align.Center, e)
        PrintTextFooter("เอกสารนี้เป็นเอกสารที่ออกโดยร้านค้า", fn7, Align.Center, e)
        PrintTextFooter("เพื่อใช้เป็นเอกสารยืนยันการทำรายการของร้านค้าเอง", fn7, Align.Center, e)
        PrintTextFooter("มิใช่เอกสารยืนยันการทำรายการของ บมจ. โทเทิ่ล แอ็คเซ็ส คอมมูนิเคชั้น", fn7, Align.Center, e)

        e.HasMorePages = False
    End Sub

    Private Sub frmPaymentCreditSign_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        PrintPreviewControl1.Dispose()
        WebCam.Stop()
        IO.File.SetAttributes(Application.StartupPath & "\SignImage\TmpCustSign.jpg", IO.FileAttributes.Normal)
        IO.File.Delete(Application.StartupPath & "\SignImage\TmpCustSign.jpg")

    End Sub

    Private Sub frmPaymentCreditSign_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        pbOK.Image = GetOKButtonImg
        pbCancel.Image = GetCancelButtonImg
    End Sub

    Private Sub SaveImageCapture(BillingNo As String)
        If _CaptureEnable = "Y" Then
            Try
                'Dim ConfigINI As New KioskPayment.Org.Mentalis.Files.IniReader(Application.StartupPath & "\KioskPayment.ini")
                'ConfigINI.Section = "CaptureImage"
                '======= บันทึกรูปภาพแบบปกติ =====
                Dim FileName As String = "Temp.jpg"
                Dim Path As String = GetCapturePath()
                Dim Path1 As String = Path & FileName
                pcCapture.Image.Save(Path1, System.Drawing.Imaging.ImageFormat.Jpeg)
                '======= บันทึกรูปภาพแบบปกติ =====

                '======= save convert to 300 dpi =====
                Dim path2 As String = GetCapturePath()
                Dim Resolution As String = KioskPaymentModule._CaptureResolutions
                path2 = path2 & BillingNo & ".jpg"
                Using bitmap As Bitmap = DirectCast(Image.FromFile(Path1), Bitmap)
                    Using newBitmap As New Bitmap(bitmap)
                        newBitmap.SetResolution(Resolution, Resolution)
                        newBitmap.Save(path2, System.Drawing.Imaging.ImageFormat.Jpeg)

                        imgFace = newBitmap
                    End Using
                End Using

                If File.Exists(Path1) Then
                    File.SetAttributes(Path1, FileAttributes.Normal)
                    File.Delete(Path1)
                End If
                '======= save convert to 300 dpi =====
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private ReadOnly Property GetCapturePath() As String
        Get
            Dim p As String = Application.StartupPath & "\CaptureImages\"
            If Directory.Exists(p) = False Then
                Directory.CreateDirectory(p)
            End If
            Return p
        End Get
    End Property

    Private ReadOnly Property GetCaptureBackupPath() As String
        Get
            Dim p As String = GetCapturePath & "Backup\"
            If Directory.Exists(p) = False Then
                Directory.CreateDirectory(p)
            End If
            Return p
        End Get
    End Property

    Private Sub frmPaymentCreditSign_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            'Dim ini As New IniReader(Application.StartupPath & "\KioskPayment.ini")
            'ini.Section = "CaptureImage"
            'CaptureEnable = FunctionENG.GetConfig("CaptureEnable", "N", Nothing) 'ini.ReadString("EnableCaptureImage")
            If _CaptureEnable = "Y" Then
                'Dim CamIndex As Integer = FunctionENG.GetConfig("CaptureCameraIndex", "0", Nothing) 'ini.ReadString("CameraIndex")
                'Dim CaptureHeight As Integer = FunctionENG.GetConfig("CaptureHeight", "480", Nothing) 'ini.ReadString("CaptureHeight")
                'Dim CaptureWidth As Integer = FunctionENG.GetConfig("CaptureWidth", "640", Nothing) 'ini.ReadString("CaptureWidth")

                pcCapture.Size = New Size(_CaptureWidth, _CaptureHeight)

                WebCam = New WebCam()
                WebCam.InitializeWebCam(pcCapture)
                WebCam.CaptureHeight(_CaptureHeight)
                WebCam.CaptureWidth(_CaptureWidth)
                WebCam.Start(_CameraIndex)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click
        Me.Close()
    End Sub
End Class