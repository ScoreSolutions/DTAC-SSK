Namespace Engine
    Public Class CountMoneyENG
        Public Shared Function UpdateCountMoney(Bank20Count As Integer, Bank50Count As Integer, Bank100Count As Integer, Bank500Count As Integer, Bank1000Count As Integer, CoinCount As Integer) As Boolean
            Dim ret As Boolean = True
            Dim trans As New LinqDB.ConnectDB.TransactionDB
            Dim lnq As New LinqDB.TABLE.CfConfigurationLinqDB

            If Bank20Count > 0 Then
                If lnq.ChkDataByCONFIG_NAME("BankNote20Count", trans.Trans) = True Then
                    lnq.CONFIG_VALUE += Bank20Count

                    ret = lnq.UpdateByPK("CountMoneyENG.BankNote20Count", trans.Trans)
                Else
                    ret = False
                End If
            End If
            If ret = True Then
                If Bank50Count > 0 Then
                    If lnq.ChkDataByCONFIG_NAME("BankNote50Count", trans.Trans) = True Then
                        lnq.CONFIG_VALUE += Bank50Count

                        ret = lnq.UpdateByPK("CountMoneyENG.BankNote50Count", trans.Trans)
                    Else
                        ret = False
                    End If
                End If
            End If
            If ret = True Then
                If Bank100Count > 0 Then
                    If lnq.ChkDataByCONFIG_NAME("BankNote100Count", trans.Trans) = True Then
                        lnq.CONFIG_VALUE += Bank100Count

                        ret = lnq.UpdateByPK("CountMoneyENG.BankNote100Count", trans.Trans)
                    Else
                        ret = False
                    End If
                End If
            End If
            If ret = True Then
                If Bank500Count > 0 Then
                    If lnq.ChkDataByCONFIG_NAME("BankNote500Count", trans.Trans) = True Then
                        lnq.CONFIG_VALUE += Bank500Count

                        ret = lnq.UpdateByPK("CountMoneyENG.BankNote500Count", trans.Trans)
                    Else
                        ret = False
                    End If
                End If
            End If
            If ret = True Then
                If Bank1000Count > 0 Then
                    If lnq.ChkDataByCONFIG_NAME("BankNote1000Count", trans.Trans) = True Then
                        lnq.CONFIG_VALUE += Bank1000Count

                        ret = lnq.UpdateByPK("CountMoneyENG.BankNote1000Count", trans.Trans)
                    Else
                        ret = False
                    End If
                End If
            End If
            If ret = True Then
                If CoinCount > 0 Then
                    If lnq.ChkDataByCONFIG_NAME("CoinCount", trans.Trans) = True Then
                        lnq.CONFIG_VALUE += CoinCount

                        ret = lnq.UpdateByPK("CountMoneyENG.CoinCount", trans.Trans)
                    Else
                        ret = False
                    End If
                End If
            End If
            If ret = True Then
                If lnq.ChkDataByCONFIG_NAME("BankNoteCount", trans.Trans) = True Then
                    lnq.CONFIG_VALUE += (Bank20Count + Bank50Count + Bank100Count + Bank500Count + Bank1000Count)

                    ret = lnq.UpdateByPK("CountMoneyENG.BankNoteCount", trans.Trans)
                Else
                    ret = False
                End If
            End If
            If ret = True Then
                trans.CommitTransaction()
            Else
                trans.RollbackTransaction()
            End If
            lnq = Nothing
            Return ret
        End Function

        Public Shared Function CheckAlarmBankNote() As Boolean
            'BankNoteCount
            'BankNoteAlarmCritical()
            Dim ret As Boolean = False
            Dim BankNoteCount As Integer = FunctionENG.GetConfig("BankNoteCount", "0", Nothing)
            Dim BankNoteAlarmCritical As Integer = FunctionENG.GetConfig("BankNoteAlarmCritical", "900", Nothing)
            If BankNoteCount > BankNoteAlarmCritical Then
                ret = True
            End If
            Return ret
        End Function
    End Class
End Namespace

