Imports Microsoft.VisualBasic
Imports LinqDB.ConnectDB
Imports System.Data

Public Class ShopSettingENG
    Dim _err As String = ""

    Public ReadOnly Property ErrorMessage As String
        Get
            Return _err
        End Get
    End Property
    Public Function SaveShopTbSetting(ByVal ConfigValue As String, ByVal ConfigName As String) As Boolean
        Dim ret As Boolean = False
        Dim shTrans As New TransactionDB
        'shTrans = FunctionEng.GetShTransction(ShopID, "ShopSettingENG.SaveShopTbSetting")
        If shTrans.Trans IsNot Nothing Then
            Dim sLnq As New LinqDB.TABLE.CfConfigurationLinqDB
            Dim sql As String = "update CF_CONFIGURATION "
            sql += " set config_value = '" & ConfigValue & "',"
            sql += " update_date=getdate()"
            sql += " where config_name = '" & ConfigName & "'"
            ret = sLnq.UpdateBySql(sql, shTrans.Trans)

            If ret = True Then
                shTrans.CommitTransaction()
            Else
                shTrans.RollbackTransaction()
                _err = sLnq.ErrorMessage
            End If
            sLnq = Nothing
        End If

        Return ret
    End Function
    Public Function GetCfConfiguration() As DataTable
        Dim dt As New DataTable
        Dim shTrans As New TransactionDB
        'shTrans = FunctionEng.GetShTransction(ShopID, "ShopSettingENG.GetTbSetting")
        If shTrans.Trans IsNot Nothing Then
            Dim sLnq As New LinqDB.TABLE.CfConfigurationLinqDB
            dt = sLnq.GetDataList("1=1", "config_name", shTrans.Trans)
            sLnq = Nothing
        End If

        Return dt
    End Function
End Class
