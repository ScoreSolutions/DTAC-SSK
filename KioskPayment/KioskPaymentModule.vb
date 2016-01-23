Imports KioskPayment.Engine
Module KioskPaymentModule

    Public Declare Function HideCaret Lib "user32.dll" (ByVal hWnd As IntPtr) As Boolean
    ' Private Declare Function ShowCaret Lib "user32.dll" (ByVal hWnd As IntPtr) As Boolean

    
    Public WaitTimeScreenSaver As Int32
    Public WaitTimeForPayment As Int32
    Public WaitTimeSessionExpired As Int32

    Public PaymentListDT As New DataTable
    Public TotalMoneyAmt As Double
    Public _Language As FormLanguage
    Public _PaymentType As PaymentType
    Public _MobileNo As String


    'GetformBackgroundImage
    Public _frmMDIParent As Image
    Public _frmChangeLanguage As Image
    Public _frmBillPaymentScanBarcodeBG As Image
    Public _frmBillPaymentScanBarcodeScan As Image
    Public _frmBillPaymentSum As Image
    Public _frmSelectPaymentType As Image
    Public _frmPaymentCash As Image
    Public _frmPaymentCashSum As Image
    Public _frmInputMobileNo As Image
    Public _frmInputMobileNoBarcode As Image
    Public _frmInputMobileNoTransfer As Image
    Public _frmPaymentCreditBG As Image
    Public _frmPaymentCreditCard As Image
    Public _frmTopUpSum As Image
    Public _frmPaymentReceipt As Image
    Public _frmTransferInsertIDCardBG As Image
    Public _frmTransferInsertIDCardIDCard As Image
    Public _frmTransferCustomerInfo As Image
    Public _frmTransferInputMoneyAmount As Image
    Public _frmTransferSum As Image
    Public _frmThankYou As Image
    Public _frmSoryForReturn As Image

    'Image Button
    Public GetOKButtonImg As Image
    Public GetCancelButtonImg As Image
    Public GetBtnCashButton As Image
    Public GetBtnCreditButton As Image
    Public GetBtnInputMobileBarcode As Image
    Public GetTxtPayTotal As Image
    Public GetBtnTopupMoney As Image
    Public GetTxtMobileNo As Image
    Public GetTxtMoneyTotal As Image

    Public _CameraIndex As Integer
    Public _CaptureWidth As Integer
    Public _CaptureHeight As Integer
    Public _CaptureResolutions As Integer
    Public _CaptureEnable As String

    Public Sub LoadAllBGImage()
        _frmMDIParent = Image.FromFile(Application.StartupPath & "\Images\Background\frmMDIParent.jpg")
        _frmChangeLanguage = Image.FromFile(Application.StartupPath & "\Images\Background\frmChangeLanguage.jpg")
        _frmBillPaymentScanBarcodeBG = Image.FromFile(Application.StartupPath & "\Images\Background\frmBillPaymentScanBarcodeBG.jpg")
        _frmBillPaymentScanBarcodeScan = Image.FromFile(Application.StartupPath & "\Images\Background\frmBillPaymentScanBarcodeScan.gif")
        _frmBillPaymentSum = Image.FromFile(Application.StartupPath & "\Images\Background\frmBillPaymentSum.jpg")
        _frmSelectPaymentType = Image.FromFile(Application.StartupPath & "\Images\Background\frmSelectPaymentTypeBG.jpg")
        _frmPaymentCash = Image.FromFile(Application.StartupPath & "\Images\Background\frmPaymentCashBG.jpg")
        _frmPaymentCashSum = Image.FromFile(Application.StartupPath & "\Images\Background\frmPaymentCashSumBG.jpg")
        _frmInputMobileNo = Image.FromFile(Application.StartupPath & "\Images\Background\frmInputMobileNo.jpg")
        _frmInputMobileNoBarcode = Image.FromFile(Application.StartupPath & "\Images\Background\frmInputMobileNoBarcode.jpg")
        _frmInputMobileNoTransfer = Image.FromFile(Application.StartupPath & "\Images\Background\frmInputMobileNoTransfer.jpg")
        _frmTransferInputMoneyAmount = Image.FromFile(Application.StartupPath & "\Images\Background\frmTransferInputMoneyAmount.jpg")
        _frmPaymentCreditBG = Image.FromFile(Application.StartupPath & "\Images\Background\frmPaymentCreditBG.jpg")
        _frmPaymentCreditCard = Image.FromFile(Application.StartupPath & "\Images\Background\frmPaymentCreditCard.gif")
        _frmTopUpSum = Image.FromFile(Application.StartupPath & "\Images\Background\frmTopUpSum.jpg")
        _frmPaymentReceipt = Image.FromFile(Application.StartupPath & "\Images\Background\frmPaymentReceipt.jpg")
        _frmTransferInsertIDCardBG = Image.FromFile(Application.StartupPath & "\Images\Background\frmTransferInsertIDCardBG.jpg")
        _frmTransferInsertIDCardIDCard = Image.FromFile(Application.StartupPath & "\Images\Background\frmTransferInsertIDCardIDCard.gif")
        _frmTransferCustomerInfo = Image.FromFile(Application.StartupPath & "\Images\Background\frmTransferCustomerInfo.jpg")
        _frmTransferSum = Image.FromFile(Application.StartupPath & "\Images\Background\frmTransferSum.jpg")
        _frmThankYou = Image.FromFile(Application.StartupPath & "\Images\Background\frmThankYou.jpg")
        _frmSoryForReturn = Image.FromFile(Application.StartupPath & "\Images\Background\frmSoryForReturn.jpg")
    End Sub
    Public Sub LoadAllButton()
        GetOKButtonImg = Image.FromFile(Application.StartupPath & "\Images\Button\ok_btn.jpg") 'GetButtonImageENG.GetButton(ImageButton.OK)
        GetCancelButtonImg = Image.FromFile(Application.StartupPath & "\Images\Button\cancel_btn.jpg") 'GetButtonImageENG.GetButton(ImageButton.Cancel)
        GetBtnCashButton = Image.FromFile(Application.StartupPath & "\Images\Button\cash_btn.jpg")
        GetBtnCreditButton = Image.FromFile(Application.StartupPath & "\Images\Button\credit_btn.jpg")
        GetBtnInputMobileBarcode = Image.FromFile(Application.StartupPath & "\Images\Button\btnMobileNoBarcode.jpg")
        GetTxtPayTotal = Image.FromFile(Application.StartupPath & "\Images\BillerLogo\txtPayTotal.jpg")
        GetBtnTopupMoney = Image.FromFile(Application.StartupPath & "\Images\Button\btnTopupMoney.jpg")
        GetTxtMoneyTotal = Image.FromFile(Application.StartupPath & "\Images\Background\txtMoney.jpg")
        GetTxtMobileNo = Image.FromFile(Application.StartupPath & "\Images\Background\txtMobileNo.jpg")
    End Sub
    Public Sub LoadAllDbConfig()
        WaitTimeScreenSaver = FunctionENG.GetConfig("WaitTimeScreenSaver", "20", Nothing)
        WaitTimeForPayment = FunctionENG.GetConfig("WaitTimeForPayment", "60", Nothing)
        WaitTimeSessionExpired = FunctionENG.GetConfig("WaitTimeSessionExpired", "10", Nothing)

        _CameraIndex = FunctionENG.GetConfig("CaptureCameraIndex", "0", Nothing)
        _CaptureHeight = FunctionENG.GetConfig("CapthreHeight", "480", Nothing)
        _CaptureWidth = FunctionENG.GetConfig("CaptureWidth", "640", Nothing)
        _CaptureResolutions = FunctionENG.GetConfig("CaptureResolutions", "300", Nothing)
        _CaptureEnable = FunctionENG.GetConfig("CaptureEnable", "N", Nothing)
    End Sub

    Public Sub AddPaymentListData(MobileNo As String, PaymentType As PaymentType, ServiceType As ServiceType, AccountNo As String, BillerName As String, Amount As Double)
        'PaymentListDT.Columns.Add("MobileNo")
        'PaymentListDT.Columns.Add("PaymentType")   'Cash/CreditCard
        'PaymentListDT.Columns.Add("ServiceType")
        'PaymentListDT.Columns.Add("AccountNo")
        'PaymentListDT.Columns.Add("BillerName")
        'PaymentListDT.Columns.Add("Amount")

        Dim dr As DataRow = PaymentListDT.NewRow
        dr("MobileNo") = MobileNo
        dr("PaymentType") = PaymentType
        dr("ServiceType") = ServiceType
        dr("AccountNo") = AccountNo
        dr("BillerName") = BillerName
        dr("Amount") = Amount
        PaymentListDT.Rows.Add(dr)

    End Sub

    Public Sub CreateNewPaymentList()
        PaymentListDT = New DataTable
        PaymentListDT.Columns.Add("MobileNo")
        PaymentListDT.Columns.Add("PaymentType")   'Cash/CreditCard
        PaymentListDT.Columns.Add("ServiceType")
        PaymentListDT.Columns.Add("AccountNo")
        PaymentListDT.Columns.Add("BillerName")
        PaymentListDT.Columns.Add("Amount")
    End Sub
End Module
