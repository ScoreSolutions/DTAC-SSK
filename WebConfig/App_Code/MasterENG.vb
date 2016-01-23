Imports Microsoft.VisualBasic
Imports System.Data
Imports LinqDB.ConnectDB
Public Class MasterENG
    Public Function GetBillerAllList(wh As String) As DataTable
        Dim dt As New DataTable
        Dim trans As New TransactionDB
        If trans IsNot Nothing Then
            Dim lnq As New LinqDB.TABLE.MsBillerLinqDB
            dt = lnq.GetDataList(wh, "biller_name", trans.Trans)
            trans.CommitTransaction()
            lnq = Nothing
        End If
        Return dt
    End Function
    Public Function GetTopupMoneyList(wh As String) As DataTable
        Dim dt As New DataTable
        Dim trans As New TransactionDB
        If trans IsNot Nothing Then
            Dim lnq As New LinqDB.TABLE.MsTopupMoneyLinqDB
            dt = lnq.GetDataList(wh, "money_value", trans.Trans)
            trans.CommitTransaction()
            lnq = Nothing
        End If
        Return dt
    End Function
    
End Class
