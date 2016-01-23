Namespace Engine
    Public Class FunctionENG
        Public Shared Function GetConfig(ConfigName As String, DefaultValue As String, trans As SqlClient.SqlTransaction) As String
            Dim ret As String = ""
            Try
                Dim lnq As New LinqDB.TABLE.CfConfigurationLinqDB
                lnq.ChkDataByCONFIG_NAME(ConfigName, trans)
                If lnq.ID > 0 Then
                    ret = lnq.CONFIG_VALUE
                Else
                    ret = DefaultValue
                End If
                lnq = Nothing
            Catch ex As Exception
                ret = DefaultValue
            End Try
            
            Return ret
        End Function

        Public Shared Sub PlaySound(FormName As String)
            Dim dt As New DataTable
            Dim lnq As New LinqDB.TABLE.MsSoundConfigLinqDB
            dt = lnq.GetDataList("form_name='" & FormName & "'", "", Nothing)
            If dt.Rows.Count > 0 Then
                My.Computer.Audio.Play(Application.StartupPath & "\Sound\" & dt.Rows(0)("mp3_filename"))
            End If
            dt.Dispose()
            lnq = Nothing
        End Sub
    End Class
    Public Enum FormLanguage
        TH = 1
        EN = 2
    End Enum
    Public Enum ServiceType
        BillPayment = 1
        TopUpHappy = 2
        MoneyTransfer = 3
    End Enum
    Public Enum PaymentType
        Cash = 1
        CreditCard = 2
    End Enum
End Namespace

