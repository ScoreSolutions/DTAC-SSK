Imports KioskPayment.Engine

Public Class frmSelectService

    Private Sub pbBillPayment_Click(sender As Object, e As EventArgs) Handles pbBillPayment.Click
        Dim frm As New frmBillPaymentSelectBillerType
        'frm.MdiParent = frmMDIParent
        If frm.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim fb As New frmBillPaymentSearchBiller
            fb.BillerTypeID = frm.BillerTypeID
            fb.BillerTypeName = frm.BillerTypeName

            If fb.ShowDialog() = Windows.Forms.DialogResult.OK Then
                Dim fs As New frmBillPaymentScanBarcode
                'fs.MdiParent = frmMDIParent
                fs.BillerID = fb.BillerID
                fs.Show()
                Me.Close()
            End If
        End If
    End Sub
    Private Sub pbCancel_Click(sender As Object, e As EventArgs) Handles pbCancel.Click
        Dim frm As New frmChangeLanguage
        'frm.MdiParent = frmMDIParent
        frm.Show()
        Me.Close()
    End Sub
    Private Sub pbMoneyTransfer_Click(sender As Object, e As EventArgs) Handles pbMoneyTransfer.Click
        Dim frm As New frmTransferInsertIDCard
        'frm.MdiParent = frmMDIParent
        If frm.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim fCus As New frmTransferCustomerInfo
            fCus.lblCustomerName.Text = "คุณอัครัวฒน์ พุทธจันทร์"
            fCus.lblMobileNo.Text = _MobileNo
            fCus.lblTotal_eWalletAmt.Text = "500"
            If fCus.ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim ff As New frmInputMobileNo
                ff.BackgroundImage = _frmInputMobileNoTransfer
                If ff.ShowDialog = Windows.Forms.DialogResult.OK Then
                    Dim fAmt As New frmTransferInputMoneyAmount
                    fAmt.lbleWalletAmt.Text = fCus.lblTotal_eWalletAmt.Text
                    If fAmt.ShowDialog = Windows.Forms.DialogResult.OK Then
                        Dim sTranAmt As New frmTransferSum
                        sTranAmt.SummaryMobileNo = ff.txtMobileNo.Text
                        sTranAmt.SummaryTransferAmount = fAmt.txtMoneyAmt.Text
                        If sTranAmt.ShowDialog = Windows.Forms.DialogResult.OK Then

                            'AddPaymentListData(ff.txtMobileNo.Text, _PaymentType, ServiceType.MoneyTransfer, ff.txtMobileNo.Text, "", Convert.ToInt16(fAmt.txtMoneyAmt.Text.Replace(",", "")))

                            Dim cFrm As New frmConfirmToOtherService
                            If cFrm.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                                'If _PaymentType = PaymentType.Cash Then
                                '    Dim cafrm As New frmPaymentCash
                                '    cafrm.SetMoneyAmount = TotalMoneyAmt
                                '    cafrm.Show()
                                'ElseIf _PaymentType = PaymentType.CreditCard Then
                                '    Dim crfrm As New frmPaymentCredit
                                '    crfrm.TotalAmount = TotalMoneyAmt
                                '    crfrm.Show()
                                'End If

                                CreateNewPaymentList()
                                Dim crfrm As New frmChangeLanguage
                                crfrm.Show()

                                Me.Close()
                            Else
                                FunctionENG.PlaySound("frmSelectService")
                            End If
                        End If
                    End If
                End If
            End If
        End If
    End Sub

   

    Private Sub pbTopupHappy_Click(sender As Object, e As EventArgs) Handles pbTopupHappy.Click
        Dim frm As New frmInputMobileNo
        'frm.MdiParent = frmMDIParent
        If frm.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim ff As New frmTopUpSelectMoney
            'ff.MobileNo = frm.txtMobileNo.Text
            If ff.ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim fSum As New frmTopUpSum
                fSum.SummaryMobileNo = frm.txtMobileNo.Text
                fSum.SummaryMoneyAmount = ff.Money
                If fSum.ShowDialog = Windows.Forms.DialogResult.OK Then

                    AddPaymentListData(_MobileNo, _PaymentType, ServiceType.TopUpHappy, frm.txtMobileNo.Text, "", ff.Money)

                    Dim cFrm As New frmConfirmToOtherService
                    If cFrm.ShowDialog = Windows.Forms.DialogResult.Cancel Then
                        If _PaymentType = PaymentType.Cash Then
                            Dim cafrm As New frmPaymentCash
                            cafrm.SetMoneyAmount = TotalMoneyAmt
                            cafrm.Show()
                        ElseIf _PaymentType = PaymentType.CreditCard Then
                            Dim crfrm As New frmPaymentCredit
                            crfrm.TotalAmount = TotalMoneyAmt
                            crfrm.Show()
                        End If

                        Me.Close()
                    Else
                        FunctionENG.PlaySound("frmSelectService")
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub frmSelectService_Load(sender As Object, e As EventArgs) Handles Me.Load
        FunctionENG.PlaySound("frmSelectService")
    End Sub
End Class