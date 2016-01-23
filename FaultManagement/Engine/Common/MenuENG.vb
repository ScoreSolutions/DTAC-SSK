Namespace Common
    Public Class MenuENG
        Public Shared Function GetMenuList(ByVal ModuleID As Long) As DataTable
            Dim dt As New DataTable
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            If trans.Trans IsNot Nothing Then
                Dim lnq As New CenLinqDB.TABLE.SysmenuCenLinqDB
                dt = lnq.GetDataList("sysmodule_id = " & ModuleID & " and active='Y'", "order_seq", trans.Trans)
                trans.CommitTransaction()
                lnq = Nothing
            End If

            Return dt
        End Function

        Public Shared Function GetMenuList(ByVal ModuleID As Long, ByVal UserName As String) As DataTable
            Dim dt As New DataTable
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            If trans.Trans IsNot Nothing Then
                Dim lnq As New CenLinqDB.TABLE.SysmenuCenLinqDB
                Dim sql As String = "select distinct m.* from sysmenu m" & vbNewLine
                sql += " inner join TB_ROLE_SYSMENU rs on rs.sysmenu_id=m.id " & vbNewLine
                sql += " inner join TB_ROLE_USER ru on ru.tb_role_id=rs.tb_role_id" & vbNewLine
                sql += " inner join TB_USER u on u.id=ru.tb_user_id" & vbNewLine
                sql += " where u.username = '" & UserName & "'" & vbNewLine
                sql += " and m.sysmodule_id='" & ModuleID & "'" & vbNewLine
                sql += " and m.active='Y'" & vbNewLine
                sql += " order by m.order_seq" & vbNewLine

                dt = lnq.GetListBySql(sql, trans.Trans)
                trans.CommitTransaction()
                lnq = Nothing
            End If

            Return dt
        End Function

        Public Shared Function GetSubMenuList(ByVal MenuID As Long) As DataTable
            Dim dt As New DataTable
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            If trans.Trans IsNot Nothing Then
                Dim lnq As New CenLinqDB.TABLE.SysmenuCenLinqDB
                dt = lnq.GetDataList("ref_sysmenu_id = " & MenuID & " and active='Y'", "order_seq", trans.Trans)
                trans.CommitTransaction()
                lnq = Nothing
            End If
            Return dt
        End Function

        Public Shared Function GetMenuPara(ByVal MenuID As Long) As CenParaDB.TABLE.SysmenuCenParaDB
            Dim p As New CenParaDB.TABLE.SysmenuCenParaDB
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            If trans.Trans IsNot Nothing Then
                Dim lnq As New CenLinqDB.TABLE.SysmenuCenLinqDB
                p = lnq.GetParameter(MenuID, trans.Trans)
                trans.CommitTransaction()
                lnq = Nothing
            End If
            Return p
        End Function
    End Class
End Namespace

