Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports LinqDB.TABLE
Imports KioskPayment.Org.Mentalis.Files

Namespace Engine
    Public Class PaymentSlipENG
        Public Function CreatePaymentSlip(lnq As TsPaymentTransactionLinqDB) As String
            Dim ret As String = Application.StartupPath & "\PaymentSlip\TmpPayment_" & lnq.ACCOUNT_NO & ".pdf"
            Try
                Dim pgSize As New iTextSharp.text.Rectangle(400, 400)
                Dim doc As New Document(pgSize, 10, 10, 42, 35)
                Dim writer As PdfWriter = PdfWriter.GetInstance(doc, New FileStream(ret, FileMode.Create))
                doc.Open()


                'DTAC LOGO
                Dim imgLogo As String = Application.StartupPath & "\Images\LogoDTAC.jpg"
                Dim img As Image = Image.GetInstance(imgLogo)
                'img.ScalePercent(10.0F)
                img.Alignment = 1
                doc.Add(img)

                'Time Header
                Dim contentFont As Font = FontFactory.GetFont("Tahoma", 10, iTextSharp.text.Font.NORMAL)
                Dim hTime As New Paragraph("วันที่   เวลา   สาขา   LOCATION")
                hTime.Alignment = Element.ALIGN_CENTER
                doc.Add(hTime)

                'Time Content
                Dim TransTime As String = lnq.TRANSACTION_DATE.ToString("dd/MM/yy HH:mm", New Globalization.CultureInfo("en-US"))
                Dim cTime As New Paragraph(TransTime & " " & FunctionENG.GetConfig("BranchEN", "", Nothing) & "   " & FunctionENG.GetConfig("LocationCode", "", Nothing), contentFont)
                doc.Add(cTime)


                doc.Add(New Paragraph("ลำดับที่:" & lnq.ID.ToString, contentFont))
                doc.Add(New Paragraph("Account No:" & lnq.ACCOUNT_NO, contentFont))
                doc.Add(New Paragraph("Account Name:" & lnq.ACCOUNT_NAME, contentFont))
                doc.Add(New Paragraph("Amount:" & lnq.AMOUNT, contentFont))

                If lnq.PAYMENT_TYPE = "1" Then
                    doc.Add(New Paragraph("CASH", contentFont))
                Else
                    doc.Add(New Paragraph("CREDIT CARD:XXXX-XXXX-XXXX" & Right(lnq.CREDIT_CARD_NO, 4), contentFont))
                End If

                Dim tnk As New Paragraph("THANK YOU", contentFont)
                tnk.Alignment = Element.ALIGN_CENTER
                doc.Add(tnk)

                doc.Close()
            Catch ex As Exception
                ret = ""
            End Try
            Return ret
        End Function

        Public Function CreatePaymentTransaction(dt As DataTable, PaymentType As String) As TsPaymentTransactionLinqDB
            Dim lnq As New LinqDB.TABLE.TsPaymentTransactionLinqDB
            If dt.Rows.Count > 0 Then
                lnq.GetDataByPK(Convert.ToDouble(dt.Rows(0)("id")), Nothing)
                If lnq.ID > 0 Then
                    lnq.PAYMENT_TYPE = PaymentType
                    If Convert.IsDBNull(dt.Rows(0)("customer_image_byte")) = False Then
                        lnq.CUSTOMER_IMAGE_BYTE = dt.Rows(0)("customer_image_byte")
                    Else
                        lnq.CUSTOMER_IMAGE_BYTE = Nothing
                    End If

                    Dim PaySlip As String = CreatePaymentSlip(lnq)
                    If PaySlip.Trim <> "" Then
                        lnq.PAYMENT_SLIP_BYTE = File.ReadAllBytes(PaySlip)
                    End If
                End If
            End If
            Return lnq
        End Function

    End Class
End Namespace
