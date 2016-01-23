Imports System.Web
Imports System.Web.Security
Imports System.Xml.Serialization
Imports System.IO
Imports System.Configuration
Imports CenParaDB.Common

Namespace Common
    Public Class LoginENG
        Dim _err As String = ""
        Dim _LOGIN_HISTORY_ID As Long = 0
        Public ReadOnly Property ErrorMessage() As String
            Get
                Return _err
            End Get
        End Property
        Public ReadOnly Property LOGIN_HISTORY_ID() As Long
            Get
                Return _LOGIN_HISTORY_ID
            End Get
        End Property

        Private Function CheckLDAP(ByVal UserName As String, ByVal PassWD As String) As CenParaDB.GateWay.LDAPResponsePara
            Dim gw As New Engine.GeteWay.GateWayServiceENG
            Dim para As New CenParaDB.GateWay.LDAPResponsePara
            para = gw.LDAPAuth(UserName, PassWD)
            If para.RESULT = True Then
                Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
                Dim lnq As New CenLinqDB.TABLE.TbUserCenLinqDB
                If lnq.ChkDataByWhere("username='" & UserName & "' and active_status='1'", trans.Trans) = True Then
                    lnq.PASSWORD = PassWD
                    If lnq.UpdateByPK(lnq.ID, trans.Trans) = True Then
                        trans.CommitTransaction()
                    Else
                        trans.RollbackTransaction()
                    End If
                Else
                    trans.RollbackTransaction()
                    para.RESULT = False
                End If
                lnq = Nothing
            End If
            gw = Nothing

            Return para
        End Function

        Public Function CSIGetLDAP(ByVal UserName As String, ByVal PassWD As String) As CenParaDB.GateWay.LDAPResponsePara
            Dim para As New CenParaDB.GateWay.LDAPResponsePara
            para = CheckLDAP(UserName, PassWD)
            If para.RESULT = False Then
                para.RESPONSE_MSG = "This user has not been configured to use the QIS-CSI System.<br/>Please contact your System Aministrator."
            End If
            Return para
        End Function

        Public Function CSITWGetLDAP(ByVal UserName As String, ByVal PassWD As String) As CenParaDB.GateWay.LDAPResponsePara
            Dim para As New CenParaDB.GateWay.LDAPResponsePara
            para = CheckLDAP(UserName, PassWD)
            If para.RESULT = False Then
                para.RESPONSE_MSG = "This user has not been configured to use the QIS-CSI TWZ System.<br/>Please contact your System Aministrator."
            End If
            Return para
        End Function

        Public Function ReportGetLDAP(ByVal UserName As String, ByVal PassWD As String) As CenParaDB.GateWay.LDAPResponsePara
            Dim gw As New Engine.GeteWay.GateWayServiceENG
            Dim para As New CenParaDB.GateWay.LDAPResponsePara
            para = gw.LDAPAuth(UserName, PassWD)
            If para.RESULT = True Then
                Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
                Dim lnq As New CenLinqDB.TABLE.TbUserCenLinqDB
                If lnq.ChkDataByWhere("username='" & UserName & "' and report_active='1'", trans.Trans) = True Then
                    lnq.PASSWORD = PassWD
                    If lnq.UpdateByPK(lnq.ID, trans.Trans) = True Then
                        trans.CommitTransaction()
                    Else
                        trans.RollbackTransaction()
                    End If
                Else
                    trans.RollbackTransaction()
                    para.RESULT = False
                    para.RESPONSE_MSG = "This user has not been configured to use the QIS-Report System.<br/>Please contact your System Aministrator."
                End If
                lnq = Nothing
            End If
            gw = Nothing

            Return para
        End Function

        Public Function WebConfigGetLDAP(ByVal UserName As String, ByVal PassWD As String) As CenParaDB.GateWay.LDAPResponsePara
            Dim para As New CenParaDB.GateWay.LDAPResponsePara
            para = CheckLDAP(UserName, PassWD)
            If para.RESULT = False Then
                para.RESPONSE_MSG = "This user has not been configured to use the QIS-Web Configuration System.<br/>Please contact your System Aministrator."
            End If
            Return para
        End Function

        Public Function WebFaultGetLDAP(ByVal UserName As String, ByVal PassWD As String) As CenParaDB.GateWay.LDAPResponsePara
            Dim para As New CenParaDB.GateWay.LDAPResponsePara
            para = CheckLDAP(UserName, PassWD)
            If para.RESULT = False Then
                para.RESPONSE_MSG = "This user has not been configured to use the QIS-Web Fault Management System.<br/>Please contact your System Aministrator."
            End If
            Return para
        End Function

        Public Function CheckUserProfile(ByVal UserName As String, ByVal PassWD As String) As Boolean
            Dim ret As Boolean = False
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            If trans.Trans IsNot Nothing Then
                Dim lnq As New CenLinqDB.TABLE.TbUserCenLinqDB
                If lnq.ChkDataByUSERNAME(UserName, trans.Trans) = True Then
                    If lnq.PASSWORD = PassWD Then
                        ret = True
                    Else
                        _err = "Invalid Password"
                    End If
                End If
                trans.CommitTransaction()
                lnq = Nothing
            Else
                _err = trans.ErrorMessage
                ret = False
            End If

            Return ret
        End Function

        Public Function GetLoginUserProfile(ByVal UserName As String) As CenParaDB.TABLE.TbUserCenParaDB
            Dim para As New CenParaDB.TABLE.TbUserCenParaDB
            Dim lnq As New CenLinqDB.TABLE.TbUserCenLinqDB
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            If trans.Trans IsNot Nothing Then
                If lnq.ChkDataByUSERNAME(UserName, trans.Trans) = True Then
                    para = lnq.GetParameter(lnq.ID, trans.Trans)
                End If
                trans.CommitTransaction()
            End If
            lnq = Nothing

            Return para
        End Function
        Public Sub SaveLoginHistory(ByVal UserName As String, ByVal req As HttpRequest, ByVal ModuleName As String)
            Dim trans As New Common.CenDbTrans
            Try
                Dim lnq As New CenLinqDB.TABLE.LoginHistoryCenLinqDB
                lnq.CUSTOMER_ID = UserName
                lnq.LOGIN_TIME = DateTime.Now
                lnq.SESSION_ID = HttpContext.Current.Session.SessionID
                Dim browser As String = " Type:" + req.Browser.Type + " Version:" + req.Browser.Version + " Browser:" + req.Browser.Browser
                lnq.IP_ADDRESS = req.UserHostAddress 'Get Client IP ADDRESS
                lnq.BROWSER = browser
                lnq.LOGIN_MODULE = ModuleName
                lnq.SERVER_URL = req.Url.AbsoluteUri

                Dim ret As Boolean = lnq.InsertData(UserName, trans.Trans)
                If ret = True Then
                    trans.CommitTransaction()
                    _LOGIN_HISTORY_ID = lnq.ID
                Else
                    trans.RollbackTransaction()
                    _err = lnq.ErrorMessage
                End If
                lnq = Nothing
            Catch ex As Exception
                trans.RollbackTransaction()
                _err = ex.Message
            End Try
        End Sub

        Public Shared Sub SaveLoginHistory(ByVal UserName As String, ByVal p As CenParaDB.TABLE.LoginHistoryCenParaDB)
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim lnq As New CenLinqDB.TABLE.LoginHistoryCenLinqDB
            Try
                If p.ID <> 0 Then
                    lnq.GetDataByPK(p.ID, trans.Trans)
                End If

                lnq.CUSTOMER_ID = p.CUSTOMER_ID
                lnq.LOGIN_TIME = p.LOGIN_TIME
                lnq.SESSION_ID = p.SESSION_ID
                lnq.IP_ADDRESS = p.IP_ADDRESS
                lnq.BROWSER = p.BROWSER
                lnq.LOGIN_MODULE = p.LOGIN_MODULE
                lnq.SERVER_URL = p.SERVER_URL
                lnq.LOGOUT_TIME = p.LOGOUT_TIME

                Dim ret As Boolean = False
                If lnq.ID <> 0 Then
                    ret = lnq.UpdateByPK(UserName, trans.Trans)
                Else
                    ret = lnq.InsertData(UserName, trans.Trans)
                End If

                If ret = True Then
                    trans.CommitTransaction()
                Else
                    trans.RollbackTransaction()
                    FunctionEng.SaveErrorLog(lnq.ID, "LoginENG.SaveLoginHistory", lnq.ErrorMessage)
                End If
                lnq = Nothing
            Catch ex As Exception
                trans.RollbackTransaction()
                FunctionEng.SaveErrorLog(lnq.ID, "LoginENG.SaveLoginHistory", lnq.ErrorMessage)
            End Try
        End Sub

        Public Shared Function GetLoginHistoryPara(ByVal LoginHisID As Long) As CenParaDB.TABLE.LoginHistoryCenParaDB
            Dim p As New CenParaDB.TABLE.LoginHistoryCenParaDB

            Dim l As New CenLinqDB.TABLE.LoginHistoryCenLinqDB
            p = l.GetParameter(LoginHisID, Nothing)
            l = Nothing
            Return p
        End Function

        Public Shared Function GetLoginSessionPara() As LoginSessionPara
            Dim tmp As New LoginSessionPara
            Try
                Dim id As FormsIdentity = HttpContext.Current.User.Identity
                Dim tik As FormsAuthenticationTicket = id.Ticket
                Dim sr As New XmlSerializer(GetType(LoginSessionPara))
                Using st As New MemoryStream(Convert.FromBase64String(tik.UserData))
                    tmp = sr.Deserialize(st)
                End Using
            Catch ex As Exception

            End Try
            
            Return tmp
        End Function

        Public Shared Function GetLogOnUser() As CenParaDB.Common.UserProfilePara
            Dim ret As New CenParaDB.Common.UserProfilePara
            Try
                Dim tmp As LoginSessionPara = GetLoginSessionPara()
                ret.LOGIN_HISTORY_ID = tmp.LOGIN_HISTORY_ID
                ret.USERNAME = tmp.USERNAME

                'Get UserProfile From Table in DB
                Dim UPara As New CenParaDB.TABLE.TbUserCenParaDB
                Dim eng As New Engine.Common.LoginENG
                UPara = eng.GetLoginUserProfile(ret.USERNAME)
                ret.USER_PARA = UPara
                eng = Nothing
                UPara = Nothing
                tmp = Nothing
            Catch ex As Exception

            End Try

            Return ret
        End Function

        Public Shared Sub LogOut(ByVal LoginHistoryID As Long)
            'Dim LoginHisID As Long = Config.GetLoginHistoryID
            Dim p As New CenParaDB.TABLE.LoginHistoryCenParaDB
            p = Engine.Common.LoginENG.GetLoginHistoryPara(LoginHistoryID)
            p.LOGOUT_TIME = DateTime.Now
            Engine.Common.LoginENG.SaveLoginHistory(p.CUSTOMER_ID, p)
            p = Nothing
        End Sub

        Public Function GetShopListByUser(ByVal UserName As String) As DataTable
            Dim dt As New DataTable
            If UserName.ToUpper <> "ADMIN" Then
                Dim sql As String = "select s.* "
                sql += " r.location_group, r.region_name_en"
                sql += " from tb_shop s"
                sql += " inner join tb_user_shop us on s.id=us.tb_shop_id"
                sql += " inner join tb_user u on u.id=us.tb_user_id"
                sql += " inner join tb_region r on r.id=s.region_id"
                sql += " where u.username = '" & UserName & "'"
                sql += " order by s.shop_name_en"
                dt = FunctionEng.ExecuteDataTable(sql)
            Else
                dt = FunctionEng.GetActiveShop()
            End If

            Return dt
        End Function

    End Class

End Namespace
