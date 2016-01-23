Imports System.IO
Imports KioskPayment.Engine
Imports KioskPayment.Org.Mentalis.Files

Public Class frmChangeLanguage

#Region "Show VDO Screen Saver"


    Dim CountVDO = 0

    Public Property MainCurrentTime As String
        Get
            Return lblScreenSaverCurrentTime.Text
        End Get
        Set(value As String)
            lblScreenSaverCurrentTime.Text = value
        End Set
    End Property

    Public Property MainCurrentFile As String
        Get
            Return lblScreenSaverCurrentFile.Text
        End Get
        Set(value As String)
            lblScreenSaverCurrentFile.Text = value
        End Set
    End Property


    Private Sub TimerScreenSaver_Tick(sender As Object, e As EventArgs) Handles TimerScreenSaver.Tick
        Try
            If CountVDO < WaitTimeScreenSaver Then
                CountVDO = CountVDO + 1
            Else
                pbThai.Enabled = False
                pbEng.Enabled = False
                If Directory.Exists(Application.StartupPath & "\VDO") = True Then
                    TimerScreenSaver.Enabled = False
                    If frmScreenSaver.ShowDialog = Windows.Forms.DialogResult.Yes Then
                        frmScreenSaver.Close()
                        CountVDO = 0
                        TimerScreenSaver.Enabled = True
                    End If
                End If
                pbThai.Enabled = True
                pbEng.Enabled = True
            End If
        Catch ex As Exception : End Try
    End Sub
#End Region

    Private Sub frmChangeLanguage_Click(sender As Object, e As EventArgs) Handles Me.Click
        CountVDO = 0
    End Sub

    Private Sub frmChangeLanguage_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            LoadAllDbConfig()
            LoadAllButton()
            LoadAllBGImage()
            CreateNewPaymentList()

            Me.BackgroundImage = _frmChangeLanguage
            Me.BackgroundImageLayout = ImageLayout.Stretch
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ClearAllData()
        TotalMoneyAmt = 0
    End Sub


    Private Sub LanguageButtonClick(Lang As FormLanguage)
        CountVDO = 0
        TimerScreenSaver.Enabled = False
        Me.Hide()
        _Language = Lang
        Dim ff As New frmInputMobileNo
        Engine.FunctionENG.PlaySound("frmInputMobileNo")
        If ff.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Dim frm As New frmSelectPaymentType
            'frm.MdiParent = frmMDIParent
            frm.MobileNo = ff.txtMobileNo.Text
            _MobileNo = ff.txtMobileNo.Text
            frm.Show()
        Else
            Me.Show()
            TimerScreenSaver.Enabled = True
        End If
    End Sub

    Private Sub frmChangeLanguage_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        FunctionENG.PlaySound("frmChangeLanguage")
    End Sub

    Private Sub frmChangeLanguage_VisibleChanged(sender As Object, e As EventArgs) Handles Me.VisibleChanged
        If Me.Visible = True Then
            TimerScreenSaver.Enabled = True
        End If
    End Sub

    Private Sub lblExit_Click(sender As Object, e As EventArgs) Handles lblExit.Click
        Application.Exit()
    End Sub

    Private Sub pbThai_Click(sender As Object, e As EventArgs) Handles pbThai.Click
        LanguageButtonClick(FormLanguage.TH)
    End Sub

    Private Sub pbEng_Click(sender As Object, e As EventArgs) Handles pbEng.Click
        LanguageButtonClick(FormLanguage.EN)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'My.Computer.Audio.Play("D:\MyProject\DTAC-SSK\KioskPayment\bin\Debug\Sound\กรุณากรอกจำนวนเงินที่ท่านต้องการโอน.wav")
    End Sub

    Private Sub TimerCheckAlarm_Tick(sender As Object, e As EventArgs) Handles TimerCheckAlarm.Tick
        If Engine.CountMoneyENG.CheckAlarmBankNote = True Then
            Dim frm As New frmOutOfService
            frm.ShowDialog()
        End If
    End Sub
End Class