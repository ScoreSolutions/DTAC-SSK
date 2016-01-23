Imports Cen = CenLinqDB.TABLE
Imports ShLinqDB.TABLE
Imports ShParaDb.TABLE
Imports ShLinqDB.Common.Utilities
Imports CenLinqDB.TABLE

Namespace Common
    Public Class FunctionEng
        Public Shared Function GetShopConfig(ByVal ParaName As String) As String
            Dim tmp As String = ""
            Try
                Dim ret As Boolean = False
                Dim lnq As New TbSettingShLinqDB
                Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB
                shTrans.CreateTransaction()
                If shTrans.Trans IsNot Nothing Then
                    ret = lnq.ChkDataByWhere("config_name = '" & ParaName & "'", shTrans.Trans)
                    shTrans.CommitTransaction()
                Else
                    shTrans.CreateTransaction()
                    If shTrans.Trans IsNot Nothing Then
                        ret = lnq.ChkDataByWhere("config_name = '" & ParaName & "'", shTrans.Trans)
                        shTrans.CommitTransaction()
                    End If
                End If

                If ret = True Then
                    tmp = lnq.CONFIG_VALUE
                End If
                'lnq = Nothing
            Catch ex As Exception
                FunctionEng.SaveErrorLog("FunctionEng.GetShopConfig", "Exception :" & ex.Message)
            End Try

            Return tmp
        End Function

        Public Shared Function GetQisDBConfig(ByVal ParaName As String) As String
            Dim ret As Boolean = False
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim lnq As New CenLinqDB.TABLE.SysconfigCenLinqDB

            ret = lnq.ChkDataByWhere("config_name = '" & ParaName & "'", trans.Trans)
            trans.CommitTransaction()
            Dim r As String = lnq.CONFIG_VALUE
            lnq = Nothing
            If ret = True Then
                Return r
            Else
                Return ""
            End If
        End Function

        Public Shared Function GetTbShopCenLinqDB(ByVal ShopID As Long) As Cen.TbShopCenLinqDB
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim lnq As New Cen.TbShopCenLinqDB
            lnq.GetDataByPK(ShopID, trans.Trans)
            lnq.SHOP_DB_SERVER = trans.conn.DataSource
            trans.CommitTransaction()

            Return lnq
        End Function

        Public Shared Function GetActiveShop() As DataTable
            'Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            'Dim lnq As New Cen.TbShopCenLinqDB
            'Dim dt As DataTable = lnq.GetDataList("active='Y'", "", trans.Trans)
            'trans.CommitTransaction()
            'lnq = Nothing

            Dim dt As DataTable = GetShopDtByWhere("active='Y'")
            Return dt
        End Function

        Public Shared Function GetShopDtByID(ByVal ShopID As Long) As DataTable
            'Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            'Dim lnq As New Cen.TbShopCenLinqDB
            Dim dt As DataTable = GetShopDtByWhere("id='" & ShopID & "'")
            'trans.CommitTransaction()
            'lnq = Nothing
            Return dt
        End Function

        Public Shared Function GetShopDtByWhere(ByVal wh As String) As DataTable
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim lnq As New Cen.TbShopCenLinqDB
            Dim dt As DataTable = lnq.GetDataList(wh, "", trans.Trans)
            trans.CommitTransaction()
            lnq = Nothing

            Return dt
        End Function

        'Public Shared Function GetShTransction(ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB, ByVal trans As CenLinqDB.Common.Utilities.TransactionDB, ByVal shLnq As CenLinqDB.TABLE.TbShopCenLinqDB, ByVal ClassName As String) As ShLinqDB.Common.Utilities.TransactionDB
        '    Dim retTrans As Boolean = False
        '    shTrans = New ShLinqDB.Common.Utilities.TransactionDB
        '    retTrans = shTrans.CreateTransaction(shLnq.MAIN_SERVERNAME, shLnq.MAIN_DB_USERID, shLnq.MAIN_DB_PWD, shLnq.MAIN_DB_NAME)
        '    If retTrans = False Then
        '        FunctionEng.SaveErrorLog("FunctionENG.GetShTransction", ClassName & " : Connect to DR Site " & shTrans.ErrorMessage & "#### :" & shLnq.SHOP_ABB)
        '        retTrans = shTrans.CreateTransaction(shLnq.SHOP_DR_SERVER, shLnq.SHOP_DR_USERID, shLnq.SHOP_DR_PWD, shLnq.SHOP_DR_NAME)
        '    End If

        '    Return shTrans
        'End Function

        'Public Shared Function GetShTransction(ByVal ShopID As Long, ByVal ClassName As String) As ShLinqDB.Common.Utilities.TransactionDB
        '    Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
        '    Dim shLnq As New CenLinqDB.TABLE.TbShopCenLinqDB
        '    shLnq = Engine.Common.FunctionEng.GetTbShopCenLinqDB(ShopID)
        '    Dim shTrans As New ShLinqDB.Common.Utilities.TransactionDB

        '    Dim retTrans As Boolean = False
        '    'retTrans = shTrans.CreateTransaction(shLnq.MAIN_SERVERNAME, shLnq.MAIN_DB_USERID, shLnq.MAIN_DB_PWD, shLnq.MAIN_DB_NAME)
        '    'If retTrans = False Then
        '    '    FunctionEng.SaveErrorLog("FunctionENG.GetShTransction", "Connect to DR Site")
        '    '    retTrans = shTrans.CreateTransaction(trans.conn.DataSource, shLnq.SHOP_DR_USERID, shLnq.SHOP_DR_PWD, shLnq.SHOP_DR_NAME)
        '    'End If
        '    shTrans = GetShTransction(shTrans, trans, shLnq, ClassName)
        '    trans.CommitTransaction()
        '    shLnq = Nothing

        '    Return shTrans
        'End Function


        Public Shared Function GetIPAddress() As String
            Dim oAddr As System.Net.IPAddress
            Dim sAddr As String
            With System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName())
                oAddr = New System.Net.IPAddress(.AddressList(0).Address)
                sAddr = oAddr.ToString
            End With
            GetIPAddress = sAddr
        End Function


        Public Shared Function SaveTransLog(ByVal ClassName As String, ByVal transDesc As String) As Boolean
            Dim ret As Boolean = False
            Dim lnq As New LogTransCenLinqDB
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB(True)
            lnq.LOGIN_HIS_ID = 0
            lnq.TRANS_DESC = transDesc
            lnq.TRANS_DATE = DateTime.Now

            ret = lnq.InsertData(ClassName, trans.Trans)
            If ret = True Then
                trans.CommitTransaction()
            Else
                trans.RollbackTransaction()
                SaveErrorLog(ClassName, lnq.ErrorMessage)
            End If
            lnq = Nothing

            Return ret
        End Function
        Public Shared Function SaveTransLog(ByVal LoginHisID As Long, ByVal ClassName As String, ByVal transDesc As String) As Boolean
            Dim ret As Boolean = False
            Dim lnq As New LogTransCenLinqDB
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB(True)
            lnq.LOGIN_HIS_ID = LoginHisID
            lnq.TRANS_DESC = transDesc
            lnq.TRANS_DATE = DateTime.Now

            ret = lnq.InsertData(ClassName, trans.Trans)
            If ret = True Then
                trans.CommitTransaction()
            Else
                trans.RollbackTransaction()
                SaveErrorLog(LoginHisID, ClassName, lnq.ErrorMessage)
            End If
            lnq = Nothing

            Return ret
        End Function

        Public Shared Function SaveErrorLog(ByVal ClassName As String, ByVal ErrorDesc As String) As Boolean
            Dim ret As Boolean = False
            Dim lnq As New LogErrorCenLinqDB
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB(True)
            lnq.LOGIN_HIS_ID = 0
            lnq.ERROR_DESC = ErrorDesc
            lnq.ERROR_TIME = DateTime.Now
            ret = lnq.InsertData(ClassName, trans.Trans)
            If ret = True Then
                trans.CommitTransaction()
            Else
                trans.RollbackTransaction()
            End If
            lnq = Nothing

            Return ret
        End Function

        Public Shared Function SaveErrorLog(ByVal LoginHisID As Long, ByVal ClassName As String, ByVal ErrorDesc As String) As Boolean
            Dim ret As Boolean = False
            Dim lnq As New LogErrorCenLinqDB
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB(True)
            lnq.LOGIN_HIS_ID = LoginHisID
            lnq.ERROR_DESC = ErrorDesc
            lnq.ERROR_TIME = DateTime.Now
            ret = lnq.InsertData(ClassName, trans.Trans)
            If ret = True Then
                trans.CommitTransaction()
            Else
                trans.RollbackTransaction()
            End If
            lnq = Nothing

            Return ret
        End Function

        Public Shared Function GetCustomerType(ByVal cID As Long, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As ShParaDB.TABLE.TbCustomertypeShParaDB
            Dim lnq As New TbCustomertypeShLinqDB
            Dim para As New TbCustomertypeShParaDB
            para = lnq.GetParameter(cID, shTrans.Trans)
            lnq = Nothing
            Return para
        End Function

        Public Shared Function GetUsers(ByVal cID As Long, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As ShParaDB.TABLE.TbUserShParaDB
            Dim lnq As New TbUserShLinqDB
            Dim para As New TbUserShParaDB
            para = lnq.GetParameter(cID, shTrans.Trans)
            lnq = Nothing
            Return para
        End Function

        Public Shared Function GetItem(ByVal cID As Long, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As ShParaDB.TABLE.TbItemShParaDB
            Dim lnq As New TbItemShLinqDB
            Dim para As New TbItemShParaDB
            para = lnq.GetParameter(cID, shTrans.Trans)
            lnq = Nothing
            Return para
        End Function

        Public Shared Function GetCounter(ByVal cID As Long, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As ShParaDB.TABLE.TbCounterShParaDB
            Dim lnq As New TbCounterShLinqDB
            Dim para As New TbCounterShParaDB
            para = lnq.GetParameter(cID, shTrans.Trans)
            lnq = Nothing
            Return para
        End Function

        Public Shared Function GetShopCustomer(ByVal MobileNo As String, ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As ShParaDB.TABLE.TbCustomerShParaDB
            Dim lnq As New TbCustomerShLinqDB
            Dim para As New TbCustomerShParaDB
            para = lnq.GetParameterByMobileNo(MobileNo, shTrans.Trans)
            lnq = Nothing
            Return para
        End Function

        Public Shared Function GetQisDBCustomer(ByVal MobileNo As String, ByVal trans As CenLinqDB.Common.Utilities.TransactionDB) As CenParaDB.TABLE.TbCustomerCenParaDB
            Dim lnq As New Cen.TbCustomerCenLinqDB
            Dim para As New CenParaDB.TABLE.TbCustomerCenParaDB
            para = lnq.GetParameterByMobileNo(MobileNo, trans.Trans)
            lnq = Nothing
            Return para
        End Function

        Public Shared Function GetShopService(ByVal shTrans As ShLinqDB.Common.Utilities.TransactionDB) As DataTable
            Dim lnq As New ShLinqDB.TABLE.TbItemShLinqDB
            Dim dt As New DataTable
            dt = lnq.GetDataList("active_status = '1'", "", shTrans.Trans)
            lnq = Nothing
            Return dt
        End Function

        Public Shared Sub CreateLogFile(ByVal TextMsg As String)
            'Dim FILE_NAME As String = System.Windows.Forms.Application.StartupPath & "\" & DateTime.Now.ToString("yyyyyMMddHH") & ".sql"
            Dim FILE_NAME As String = "C:\" & DateTime.Now.ToString("yyyyyMMddHH") & ".sql"
            Dim objWriter As New System.IO.StreamWriter(FILE_NAME, True)
            objWriter.WriteLine(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff") & " " & TextMsg & Chr(13) & Chr(13))
            objWriter.Close()
        End Sub

        Public Shared Sub CreateErrorLog(ByVal TextMsg As String)
            Dim LogFolder As String = "D:\WebFaultErrorLog"
            If IO.Directory.Exists(LogFolder) = False Then
                IO.Directory.CreateDirectory(LogFolder)
            End If
            Dim FILE_NAME As String = LogFolder & "\" & DateTime.Now.ToString("yyyyyMMddHH") & ".txt"
            Dim objWriter As New System.IO.StreamWriter(FILE_NAME, True)
            objWriter.WriteLine(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff") & " " & TextMsg & Chr(13) & Chr(13))
            objWriter.Close()
        End Sub

        Public Shared Function cStrToDateTime(ByVal StrDate As String, ByVal StrTime As String) As DateTime 'Convert วันที่จาก yyyy-MM-dd HH:mm:ss เป็น DateTime
            Dim ret As New Date(1, 1, 1)
            If StrDate.Trim <> "" Then
                Dim vDate() As String = Split(StrDate, "-")
                If StrTime.Trim <> "" Then
                    Dim vTime() As String = Split(StrTime, ":")
                    ret = New Date(vDate(0), vDate(1), vDate(2), vTime(0), vTime(1), vTime(2))
                Else
                    ret = New Date(vDate(0), vDate(1), vDate(2))
                End If
            End If
            Return ret
        End Function

        Public Shared Function GetTwLocationCenLinqDB(ByVal ShopID As Long) As Cen.TwLocationCenLinqDB
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim lnq As New Cen.TwLocationCenLinqDB
            lnq.GetDataByPK(ShopID, trans.Trans)
            'lnq.SHOP_DB_SERVER = trans.conn.DataSource
            trans.CommitTransaction()

            Return lnq
        End Function

        Public Shared Function ExecuteDataTable(ByVal sql As String) As DataTable
            Dim dt As New DataTable
            dt = SqlDB.ExecuteTable(sql)
            Return dt
        End Function

    End Class
End Namespace

