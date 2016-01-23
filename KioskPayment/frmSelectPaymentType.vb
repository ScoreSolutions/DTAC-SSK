Imports KioskPayment.Org.Mentalis.Files
Imports System.IO
Imports KioskPayment.Engine
Public Class frmSelectPaymentType

    Dim CountTimeReturn As Integer = 0
    Dim CaptureEnable As String = ""

    Dim _dt As New DataTable
    Dim _Language As FormLanguage
    Dim _MobileNo As String

    Public WriteOnly Property SetAccountData As DataTable
        Set(value As DataTable)
            _dt = value
        End Set
    End Property

    Public WriteOnly Property SetLanguage As FormLanguage
        Set(value As FormLanguage)
            _Language = value
        End Set
    End Property

    Public WriteOnly Property MobileNo As String
        Set(value As String)
            _MobileNo = value
        End Set
    End Property

    'Private Sub SaveImageCapture(BillingNo As String)
    '    If CaptureEnable = "Y" Then
    '        Try
    '            Dim ConfigINI As New IniReader(Application.StartupPath & "\KioskPayment.ini")
    '            ConfigINI.Section = "CaptureImage"
    '            '======= บันทึกรูปภาพแบบปกติ =====
    '            Dim FileName As String = "Temp.jpg"
    '            Dim Path As String = GetCapturePath
    '            Dim Path1 As String = Path & FileName
    '            pcCapture.Image.Save(Path1, System.Drawing.Imaging.ImageFormat.Jpeg)
    '            '======= บันทึกรูปภาพแบบปกติ =====

    '            '======= save convert to 300 dpi =====
    '            Dim path2 As String = GetCapturePath
    '            Dim Resolution As String = ConfigINI.ReadString("Resolution")
    '            path2 = path2 & BillingNo & ".jpg"
    '            Using bitmap As Bitmap = DirectCast(Image.FromFile(Path1), Bitmap)
    '                Using newBitmap As New Bitmap(bitmap)
    '                    newBitmap.SetResolution(Resolution, Resolution)
    '                    newBitmap.Save(path2, System.Drawing.Imaging.ImageFormat.Jpeg)

    '                    _dt.Rows(0)("customer_image_byte") = newBitmap
    '                End Using
    '            End Using

    '            If File.Exists(Path1) Then
    '                File.SetAttributes(Path1, FileAttributes.Normal)
    '                File.Delete(Path1)
    '            End If
    '            '======= save convert to 300 dpi =====
    '        Catch ex As Exception

    '        End Try
    '    End If
    'End Sub

    'Private ReadOnly Property GetCapturePath() As String
    '    Get
    '        Dim p As String = Application.StartupPath & "\CaptureImages\"
    '        If Directory.Exists(p) = False Then
    '            Directory.CreateDirectory(p)
    '        End If
    '        Return p
    '    End Get
    'End Property

    'Private ReadOnly Property GetCaptureBackupPath() As String
    '    Get
    '        Dim p As String = GetCapturePath & "Backup\"
    '        If Directory.Exists(p) = False Then
    '            Directory.CreateDirectory(p)
    '        End If
    '        Return p
    '    End Get
    'End Property

    'Dim WebCam As WebCam

    'Private Sub frmSelectPaymentType_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
    '    Try
    '        WebCam.Stop()
    '    Catch ex As Exception

    '    End Try
    'End Sub
    Private Sub frmSelectPaymentType_Load(sender As Object, e As EventArgs) Handles Me.Load
        If _dt.Rows.Count > 0 Then
            lblAccountNo.Text = _dt.Rows(0)("account_no")
            lblAccountName.Text = _dt.Rows(0)("account_name")
            lblAmount.Text = _dt.Rows(0)("amount")
            lblBillingNo.Text = _dt.Rows(0)("transaction_no")
        End If

        Try
            ''Dim ini As New IniReader(Application.StartupPath & "\KioskPayment.ini")
            ''ini.Section = "CaptureImage"
            'CaptureEnable = FunctionENG.GetConfig("CaptureEnable", "N", Nothing) 'ini.ReadString("EnableCaptureImage")
            'If CaptureEnable = "Y" Then
            '    Dim CamIndex As Integer = FunctionENG.GetConfig("CaptureCameraIndex", "0", Nothing) 'ini.ReadString("CameraIndex")
            '    Dim CaptureHeight As Integer = FunctionENG.GetConfig("CaptureHeight", "480", Nothing) 'ini.ReadString("CaptureHeight")
            '    Dim CaptureWidth As Integer = FunctionENG.GetConfig("CaptureWidth", "640", Nothing) 'ini.ReadString("CaptureWidth")

            '    pcCapture.Size = New Size(CaptureWidth, CaptureHeight)

            '    WebCam = New WebCam()
            '    WebCam.InitializeWebCam(pcCapture)
            '    WebCam.CaptureHeight(CaptureHeight)
            '    WebCam.CaptureWidth(CaptureWidth)
            '    WebCam.Start(CamIndex)
            'End If

            FunctionENG.PlaySound("frmSelectPaymentType")
        Catch ex As Exception

        End Try
        SetImageButton()
    End Sub

    Private Sub SetImageButton()
        Try
            Me.BackgroundImage = _frmSelectPaymentType
            pbCash.Image = GetBtnCashButton
            pbCreditCard.Image = GetBtnCreditButton
        Catch ex As Exception

        End Try
    End Sub


    Private Sub TimerReturnToScanbarcode_Tick(sender As Object, e As EventArgs) Handles TimerReturnToScanbarcode.Tick
        If CountTimeReturn < WaitTimeSessionExpired Then
            CountTimeReturn += 1
        Else
            CountTimeReturn = 0
            TimerReturnToScanbarcode.Enabled = False
            Dim ff As New frmSoryForReturn
            If ff.ShowDialog = Windows.Forms.DialogResult.OK Then
                TimerReturnToScanbarcode.Enabled = True
                Me.BringToFront()
            Else
                frmChangeLanguage.Show()
                Me.Close()
            End If
        End If
    End Sub

    Private Sub pbCash_Click(sender As Object, e As EventArgs) Handles pbCash.Click
        'SaveImageCapture(lblBillingNo.Text)
        Dim ff As New frmSelectService
        'ff.MdiParent = frmMDIParent
        _PaymentType = PaymentType.Cash
        ff.Show()
        Me.Close()
    End Sub

    Private Sub pbCreditCard_Click(sender As Object, e As EventArgs) Handles pbCreditCard.Click
        'SaveImageCapture(lblBillingNo.Text)
        Dim ff As New frmSelectService
        'ff.MdiParent = frmMDIParent
        _PaymentType = PaymentType.CreditCard
        ff.Show()
        Me.Close()
    End Sub

End Class