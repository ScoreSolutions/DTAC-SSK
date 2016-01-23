Imports pCen = CenParaDB.TABLE
Imports System.IO
Imports System.Net
Imports Engine.Common
Imports paCen = CenParaDB.GateWay
Imports CenParaDB.GateWay
Imports ShLinqDB.TABLE
Imports ShParaDB.TABLE


Namespace GeteWay
    Public Class GateWayServiceENG
#Region "CustomerProfile"
        Public Function GetCustomerProfile(ByVal MobileNo As String, ByVal SessionID As String) As pCen.TbCustomerCenParaDB
            Dim para As New pCen.TbCustomerCenParaDB
            para = GetCustomerProfileFormGW(MobileNo, True, True, SessionID, "")
            If para.RESPONSE_MSG <> "" Then
                para = GetCustomerFromHQ(MobileNo)
            End If

            Return para
        End Function

        Public Function GetCustomerProfile(ByVal MobileNo As String) As pCen.TbCustomerCenParaDB
            Dim para As New pCen.TbCustomerCenParaDB
            para = GetCustomerProfileFormGW(MobileNo, False, False, "", "")
            If para.RESPONSE_MSG <> "" Then
                para = GetCustomerFromHQ(MobileNo)
            End If

            Return para
        End Function

        Private Function GetCustomerProfileFormGW(ByVal MobileNo As String, ByVal IsArBalance As Boolean, ByVal IsCampaign As Boolean, ByVal SessionID As String, ByVal ShopCode As String) As pCen.TbCustomerCenParaDB
            Dim ret As New pCen.TbCustomerCenParaDB
            'http://10.13.174.95/AISQ/interface/getmobileinfo.php?MOBILENO=0821156901&GETNEW=1&REQVAR=TITLE|NAME|CONTACT_EMAIL|LOCAL_LANGUAGE|MOBILE_SEGMENT|BIRTH_DATE|MOBILE_NO_STATUS|CUST_TYPE_PC|CUST_CLASS|REGISTER_DATE|SUB_NETWORK_TYPE
            Dim ReqVar As String = "TITLE|NAME|CONTACT_EMAIL|NATIONALITY_CODE|MOBILE_SEGMENT|BIRTH_DATE|MOBILE_NO_STATUS|GROUP_CODE|CUST_CLASS|REGISTER_DATE|CHURN|SUB_NETWORK_TYPE|ASSET_ID|BILL_CYCLE|ACCOUNT_NO|GROUP_CODE|CRM_SEGMENT|CORPORATE_TYPE|ACCOUNT_NUM|PROVINCE_CODE|NETWORK_TYPE|ID_CARD_NO|PASSWORD|CA_BLACKLIST|CONTRACT_PHONE|PA_GROUP|PAYMENT_TYPE|ACCOUNT_NUM2|REGION_CODE|LOCAL_LANGUAGE|PP_COS_ID|TPURCode|CA_NAME|CA_ID_CARD_NO|CLV_SEGMENT|REMARK|TUX_CODE"
            Dim InputPara As String = "MOBILENO=" & MobileNo & "&GETNEW=1&REQVAR=" & ReqVar
            Dim URL1 As String = FunctionEng.GetQisDBConfig("CustomerProfileURL1") & InputPara

            Try
                Dim webRequest As WebRequest
                Dim webresponse As WebResponse
                webRequest = webRequest.Create(URL1)
                webresponse = webRequest.GetResponse()
                ret = GetProfileMsg(webresponse, MobileNo)
                If FunctionEng.GetQisDBConfig("AGENT_SAVE_TRANS_LOG") = "Y" Then
                    FunctionEng.SaveTransLog("GateWayServiceENG.GetCustomerProfileFromGW", URL1)
                End If
                If IsArBalance = True Then
                    ret = GetArBalance(ret)
                End If
                If IsCampaign = True Then
                    ret = GetCampaign(ret, SessionID, ShopCode)
                End If
            Catch ex As Exception
                Dim URL2 As String = FunctionEng.GetQisDBConfig("CustomerProfileURL2") & InputPara
                Try
                    FunctionEng.SaveErrorLog("GateWayServiceENG.GetCustomerProfileFromGW", URL1 & " Exception : " & ex.Message)
                    Dim webRequest As WebRequest
                    Dim webresponse As WebResponse
                    webRequest = webRequest.Create(URL2)
                    webresponse = webRequest.GetResponse()
                    ret = GetProfileMsg(webresponse, MobileNo)
                    If FunctionEng.GetQisDBConfig("AGENT_SAVE_TRANS_LOG") = "Y" Then
                        FunctionEng.SaveTransLog("GateWayServiceENG.GetCustomerProfileFromGW", URL2)
                    End If

                    If IsArBalance = True Then
                        ret = GetArBalance(ret)
                    End If
                    If IsCampaign = True Then
                        ret = GetCampaign(ret, SessionID, ShopCode)
                    End If
                Catch ex1 As Exception
                    ret.RESPONSE_MSG = ex1.Message
                    FunctionEng.SaveErrorLog("GateWayServiceENG.GetCustomerProfileFromGW", URL2 & " Exception1 : " & ex1.Message)
                End Try
            End Try
            Return ret
        End Function

        Private Function GetProfileMsg(ByVal webresponse As WebResponse, ByVal MobileNo As String) As pCen.TbCustomerCenParaDB
            Dim ret As New pCen.TbCustomerCenParaDB
            Dim Stream As New StreamReader(webresponse.GetResponseStream(), System.Text.UnicodeEncoding.Default)
            If Stream.Peek <> -1 Then
                Dim tmp() As String = Split(Stream.ReadLine(), "#")

                'TITLE|NAME|CONTACT_EMAIL|NATIONALITY_CODE|MOBILE_SEGMENT|BIRTH_DATE|MOBILE_NO_STATUS|GROUP_CODE|CUST_CLASS|REGISTER_DATE|CHURN|SUB_NETWORK_TYPE|ASSET_ID|BILL_CYCLE|ACCOUNT_NO|GROUP_CODE|CRM_SEGMENT|CORPORATE_TYPE|ACCOUNT_NUM|PROVINCE_CODE|NETWORK_TYPE|ID_CARD_NO|PASSWORD|CA_BLACKLIST|CONTRACT_PHONE|PA_GROUP|PAYMENT_TYPE|ACCOUNT_NUM2|REGION_CODE|LOCAL_LANGUAGE|PP_COS_ID|TPURCode|CA_NAME|CA_ID_CARD_NO|CLV_SEGMENT|REMARK|TUX_CODE
                ret.MOBILE_NO = MobileNo
                ret.TITLE_NAME = tmp(0)
                If tmp(1).Trim <> "" Then
                    ret.F_NAME = tmp(1).Split(" ")(0)
                    ret.L_NAME = tmp(1).Split(" ")(1)
                End If
                ret.EMAIL = tmp(2)
                ret.PREFER_LANG = tmp(3)
                ret.SEGMENT_LEVEL = tmp(4)
                ret.BIRTH_DATE = tmp(5)
                ret.MOBILE_STATUS = tmp(6)
                ret.CATEGORY = MappingCustomer("GROUP_CODE", tmp(7))
                ret.CONTACT_CLASS = MappingCustomer("CUST_CLASS", tmp(8))
                ret.SERVICE_YEAR = CalServiceYear(tmp(9))
                ret.CHUM_SCORE = tmp(10)
                ret.NETWORK_TYPE = MappingNetworkType(tmp(20), tmp(11))
                ret.ASSET_ID = tmp(12)
                ret.CORPORATE_TYPE = MappingCorporateType(tmp(17))

                UpdateCustomerProfile(ret)

                For i As Integer = 13 To tmp.Length - 1
                    If ret.OTHER_INFO.Trim = "" Then
                        ret.OTHER_INFO = tmp(i)
                    Else
                        ret.OTHER_INFO += "#" & tmp(i)
                    End If
                Next
            Else
                ret.RESPONSE_MSG = "ข้อมูลไม่ถูกต้อง"
            End If
            Stream.Close()
            Stream = Nothing
            Return ret
        End Function

        Private Function MappingCorporateType(ByVal CorporateTypeCode As String) As String
            Dim ret As String = ""
            If CorporateTypeCode.Trim <> "" Then
                Dim lnq As New CenLinqDB.TABLE.TbCustomerCorporateTypeCenLinqDB
                lnq.ChkDataByCORPORATE_TYPE_CODE(CorporateTypeCode, Nothing)
                ret = lnq.CORPORATE_TYPE_NAME
                lnq = Nothing
            End If
            Return ret
        End Function

        Private Function MappingNetworkType(ByVal NetworkType As String, ByVal SubNetworkType As String) As String
            Dim ret As String = ""
            If NetworkType.Trim <> "" And SubNetworkType.Trim <> "" Then
                Dim lnq As New CenLinqDB.TABLE.TbCustomerNetworkTypeCenLinqDB
                lnq.ChkDataByNETWORK_TYPE_SUB_NETWORK_TYPE(NetworkType, SubNetworkType, Nothing)
                ret = lnq.DISPLAY_VALUE
                lnq = Nothing
            End If
            Return ret
        End Function

        Private Function MappingCustomer(ByVal FieldName As String, ByVal vGroupCode As String) As String
            Dim ret As String = ""
            If vGroupCode.Trim <> "" Then
                Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
                Dim lnq As New CenLinqDB.TABLE.TbCustomerMappingLinq
                Dim dt As DataTable = lnq.GetDataList("field_name='" & FieldName & "' and mapping_code='" & vGroupCode & "'", "", trans.Trans)
                If dt.Rows.Count > 0 Then
                    ret = dt.Rows(0)("display_value")
                End If
            End If
            Return ret
        End Function

        Private Sub UpdateCustomerProfile(ByVal p As pCen.TbCustomerCenParaDB)
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            If trans.Trans IsNot Nothing Then
                Dim lnq As New CenLinqDB.TABLE.TbCustomerCenLinqDB
                lnq.ChkDataByMOBILE_NO(p.MOBILE_NO, trans.Trans)
                lnq.TITLE_NAME = p.TITLE_NAME
                lnq.F_NAME = p.F_NAME
                lnq.L_NAME = p.L_NAME
                lnq.MOBILE_STATUS = p.MOBILE_STATUS
                lnq.EMAIL = p.EMAIL
                lnq.SEGMENT_LEVEL = p.SEGMENT_LEVEL
                lnq.BIRTH_DATE = p.BIRTH_DATE
                lnq.CATEGORY = p.CATEGORY
                lnq.ACCOUNT_BALANCE = p.ACCOUNT_BALANCE
                lnq.CONTACT_CLASS = p.CONTACT_CLASS
                lnq.SERVICE_YEAR = p.SERVICE_YEAR
                lnq.CHUM_SCORE = p.CHUM_SCORE
                lnq.PREFER_LANG = p.PREFER_LANG
                lnq.CAMPAIGN_CODE = p.CAMPAIGN_CODE
                lnq.CAMPAIGN_NAME = p.CAMPAIGN_NAME
                lnq.NETWORK_TYPE = p.NETWORK_TYPE
                lnq.CAMPAIGN_NAME_EN = p.CAMPAIGN_NAME_EN
                lnq.CAMPAIGN_DESC = p.CAMPAIGN_DESC
                lnq.CAMPAIGN_DESC_TH2 = p.CAMPAIGN_DESC_TH2
                lnq.CAMPAIGN_DESC_EN2 = p.CAMPAIGN_DESC_EN2
                lnq.CORPORATE_TYPE = p.CORPORATE_TYPE

                Dim ret As Boolean = False
                If lnq.ID <> 0 Then
                    ret = lnq.UpdateByPK("GateWayServiceENG.UpdateCustomerProfile", trans.Trans)
                Else
                    lnq.MOBILE_NO = p.MOBILE_NO
                    ret = lnq.InsertData("GateWayServiceENG.UpdateCustomerProfile", trans.Trans)
                End If
                If ret = True Then
                    trans.CommitTransaction()
                Else
                    trans.RollbackTransaction()
                End If
                lnq = Nothing
            End If
            p = Nothing
        End Sub

        Private Function CalServiceYear(ByVal RegisterDate As String) As String
            'RegisterDate Format = yyyyMMdd
            Dim ret As String = ""
            If RegisterDate.Trim <> "" Then
                Dim dd As String = Right(RegisterDate, 2)
                Dim mm As String = Mid(RegisterDate, 5, 2)
                Dim yy As Integer = Left(RegisterDate, 4)

                Dim RegDate As New Date(yy, mm, dd)
                Dim MonthDiff As Integer = DateDiff(DateInterval.Month, RegDate, Today)
                Dim YearQty As Int16 = MonthDiff \ 12
                Dim MonthQty As Int16 = MonthDiff Mod 12

                ret = YearQty & " Year " & MonthQty & " Month"
            End If
            Return ret
        End Function

        Private Function GetArBalance(ByVal para As pCen.TbCustomerCenParaDB) As pCen.TbCustomerCenParaDB
            Dim InputPara As String = "MOBILENO=" & para.MOBILE_NO
            Dim URL1 As String = FunctionEng.GetQisDBConfig("getArBalanceURL1") & InputPara

            Try
                Dim webRequest As WebRequest
                Dim webresponse As WebResponse
                webRequest = webRequest.Create(URL1)
                webresponse = webRequest.GetResponse()
                para = GetArBalanceMsg(webresponse, para)

                If FunctionEng.GetQisDBConfig("AGENT_SAVE_TRANS_LOG") = "Y" Then
                    FunctionEng.SaveTransLog("GateWayServiceENG.GetArBalance", URL1)
                End If
            Catch ex As Exception
                Dim URL2 As String = FunctionEng.GetQisDBConfig("getArBalanceURL2") & InputPara
                Try
                    FunctionEng.SaveErrorLog("GateWayServiceENG.GetArBalance", URL1 & " Exception : " & ex.Message)
                    Dim webRequest As WebRequest
                    Dim webresponse As WebResponse
                    webRequest = webRequest.Create(URL2)
                    webresponse = webRequest.GetResponse()
                    para = GetArBalanceMsg(webresponse, para)

                    If FunctionEng.GetQisDBConfig("AGENT_SAVE_TRANS_LOG") = "Y" Then
                        FunctionEng.SaveTransLog("GateWayServiceENG.GetArBalance", URL2)
                    End If
                Catch ex1 As Exception
                    para.RESPONSE_MSG = ex1.Message
                    FunctionEng.SaveErrorLog("GateWayServiceENG.GetArBalance", URL2 & " Exception1 : " & ex1.Message)
                End Try
            End Try
            Return para
        End Function

        Private Function GetArBalanceMsg(ByVal webresponse As WebResponse, ByVal para As pCen.TbCustomerCenParaDB) As pCen.TbCustomerCenParaDB
            Dim Stream As New StreamReader(webresponse.GetResponseStream(), System.Text.UnicodeEncoding.Default)
            If Stream.Peek <> -1 Then
                Dim tmp As String = Stream.ReadLine()
                If tmp.Trim <> "" Then
                    If IsNumeric(tmp) = True Then
                        para.ACCOUNT_BALANCE = Convert.ToDouble(tmp)
                    End If
                End If
            End If
            Stream.Close()
            Stream = Nothing
            Return para
        End Function

#Region "Get Customer Campaign"

        Public Function GetCampaignTest(ByVal AudienceIdValue As String, ByVal MobileNo As String, ByVal SessionID As String) As pCen.TbCustomerCenParaDB
            Dim para As New pCen.TbCustomerCenParaDB
            para.ASSET_ID = AudienceIdValue
            para.MOBILE_NO = MobileNo
            para.NETWORK_TYPE = "POST-PAID"

            Return GetCampaign(para, SessionID, "PK")

            'Dim ret As String = ""

            'Dim InputPara As String = "SESSIONID=" & SessionID & "&RELYONEXISTINGSESSION=false"
            'InputPara += "&INTERACTIVECHANNEL=Touchpoint_Interact"
            'InputPara += "&AUDIENCEID_NAME=SUBSCRIPTION_IDENTIFIER"
            'InputPara += "&AUDIENCEID_VALUE=" & AudienceIdValue
            'InputPara += "&AUDIENCEID_DATATYPE=string"
            'InputPara += "&AUDIENCELEVEL=SUBSCRIPTION_IDENTIFIER"
            'InputPara += "&PARAM1_NAME=MOBILE_NO"
            'InputPara += "&PARAM1_VALUE=" & MobileNo
            'InputPara += "&PARAM1_DATATYPE=string"
            'InputPara += "&PARAM2_NAME=MOBILE_STATUS"
            'InputPara += "&PARAM2_VALUE=Active"
            'InputPara += "&PARAM2_DATATYPE=string"
            'InputPara += "&DEBUGFLAG=true"

            'Dim URL1 As String = FunctionEng.GetQisDBConfig("InteractStartSessionURL1") & InputPara
            'Try
            '    Dim webRequest As WebRequest
            '    Dim webresponse As WebResponse
            '    webRequest = webRequest.Create(URL1)
            '    webresponse = webRequest.GetResponse()

            '    Dim Stream As New StreamReader(webresponse.GetResponseStream(), System.Text.UnicodeEncoding.Default)
            '    If Stream.Peek <> -1 Then
            '        Dim tmp As String = Stream.ReadLine()
            '        If tmp.Trim <> "" Then
            '            ret = tmp
            '        Else
            '            ret = "ข้อมูลไม่ถูกต้อง"
            '        End If
            '    Else
            '        ret = "ข้อมูลไม่ถูกต้อง"
            '    End If
            '    Stream.Close()
            '    Stream = Nothing
            '    FunctionEng.SaveTransLog("GateWayServiceENG.GetCampaign", URL1)

            '    Dim strList() As String = GetCampaignOffer(SessionID)
            '    If strList.Length > 0 Then
            '        For Each Str As String In strList
            '            Dim CampList() As String = Split(Str, "|")
            '            If CampList.Length = 8 Then
            '                GetCampaignPostEvent(SessionID, CampList(4), MobileNo, CampList(6), CampList(5), CampList(3), CampList(1), "PK")
            '            End If
            '        Next
            '    End If
            'Catch ex As Exception
            '    'Dim URL2 As String = FunctionEng.GetQisDBConfig("InteractStartSessionURL2") & InputPara
            '    'Try
            '    '    FunctionEng.SaveErrorLog("GateWayServiceENG.GetCampaign", URL1 & " Exception : " & ex.Message)
            '    '    Dim webRequest As WebRequest
            '    '    Dim webresponse As WebResponse
            '    '    webRequest = webRequest.Create(URL2)
            '    '    webresponse = webRequest.GetResponse()
            '    '    'para = GetCampaignMsg(webresponse, para)
            '    '    FunctionEng.SaveTransLog("GateWayServiceENG.GetCampaign", URL2)
            '    'Catch ex1 As Exception
            '    '    'para.RESPONSE_MSG = ex1.Message
            '    '    FunctionEng.SaveErrorLog("GateWayServiceENG.GetCampaign", URL2 & " Exception1 : " & ex1.Message)
            '    'End Try
            'End Try
            'Return ret
        End Function

        Public Function GetCampaign(ByVal para As pCen.TbCustomerCenParaDB, ByVal SessionID As String, ByVal ShopCode As String) As pCen.TbCustomerCenParaDB
            If GetCampaignStartSession(para, SessionID) = True Then
                Dim OfferMsg As String = ""
                Dim OfferResult() As String = GetCampaignOffer(SessionID)
                If OfferResult.Length > 0 Then
                    Dim campPara() As String = Split(OfferResult(0), "|")
                    para.CAMPAIGN_CODE = "สิทธิพิเศษ"
                    para.CAMPAIGN_NAME = ""  'Campaign ภาษาไทย
                    para.CAMPAIGN_NAME_EN = "Privilege"
                    para.CAMPAIGN_DESC = campPara(0)

                    For Each camp As String In OfferResult
                        If camp.Trim <> "" Then
                            Dim tmp() As String = Split(camp, "|")
                            If tmp.Length > 0 Then
                                GetCampaignPostEvent(SessionID, tmp(4), para.MOBILE_NO, tmp(6), tmp(5), tmp(3), tmp(1), ShopCode)
                            End If
                        End If
                    Next
                End If
            End If
            Return para
        End Function

        Private Sub GetCampaignPostEvent(ByVal SessionID As String, ByVal TreamentCode As String, ByVal MobileNo As String, ByVal ParentCode As String, ByVal ChildCode As String, ByVal Score As String, ByVal OfferCode As String, ByVal ShopCode As String)
            Dim InputPara As String = "SESSIONID=" & SessionID
            InputPara += "&EVENTNAME=offerView"
            InputPara += "&TREATMENTCODE=" & TreamentCode
            InputPara += "&MOBILENO=" & MobileNo
            InputPara += "&CHILDCODE=" & ChildCode
            InputPara += "&SCORE=" & Score
            InputPara += "&OFFERCODE=" & OfferCode
            InputPara += "&CHANNELCODE=QueueSystem"
            InputPara += "USERDEFINEDFIELDS=" & ShopCode

            Dim URL1 As String = FunctionEng.GetQisDBConfig("InteractPostEventURL1") & InputPara
            Try
                Dim webRequest As WebRequest
                Dim webresponse As WebResponse
                webRequest = webRequest.Create(URL1)
                webresponse = webRequest.GetResponse()
                GetCampaignPostEventResult(webresponse)
                If FunctionEng.GetQisDBConfig("AGENT_SAVE_TRANS_LOG") = "Y" Then
                    FunctionEng.SaveTransLog("GateWayServiceENG.GetCampaignPostEvent", URL1)
                End If
            Catch ex As Exception
                Dim URL2 As String = FunctionEng.GetQisDBConfig("InteractPostEventURL2") & InputPara
                Try
                    FunctionEng.SaveErrorLog("GateWayServiceENG.GetCampaignPostEvent", URL1 & " Exception : " & ex.Message)
                    Dim webRequest As WebRequest
                    Dim webresponse As WebResponse
                    webRequest = webRequest.Create(URL2)
                    webresponse = webRequest.GetResponse()
                    GetCampaignPostEventResult(webresponse)
                    If FunctionEng.GetQisDBConfig("AGENT_SAVE_TRANS_LOG") = "Y" Then
                        FunctionEng.SaveTransLog("GateWayServiceENG.GetCampaignPostEvent", URL2)
                    End If
                Catch ex1 As Exception
                    FunctionEng.SaveErrorLog("GateWayServiceENG.GetCampaignPostEvent", URL2 & " Exception1 : " & ex1.Message)
                End Try
            End Try
        End Sub

        Private Function GetCampaignPostEventResult(ByVal webresponse As WebResponse) As Boolean
            Dim ret As Boolean = False
            Dim Stream As New StreamReader(webresponse.GetResponseStream(), System.Text.UnicodeEncoding.Default)
            If Stream.Peek <> -1 Then
                Dim tmp As String = Stream.ReadLine()
                If tmp.Trim <> "" Then
                    Dim tmpRes() As String = Split(tmp, "|")
                    If tmpRes(0).Trim = "" Then
                        ret = True
                    End If
                Else
                    ret = False
                End If
            Else
                ret = False
            End If
            Stream.Close()
            Stream = Nothing
            Return ret
        End Function

        Private Function GetCampaignOffer(ByVal SessionID As String) As String()
            Dim ret() As String = {}
            Dim InputPara As String = "SESSIONID=" & SessionID
            InputPara += "&IPOINT=Recommend"
            InputPara += "&NUMBERREQUESTED=3"

            Dim URL1 As String = FunctionEng.GetQisDBConfig("InteractGetOfferURL1") & InputPara
            Try
                Dim webRequest As WebRequest
                Dim webresponse As WebResponse
                webRequest = webRequest.Create(URL1)
                webresponse = webRequest.GetResponse()
                ret = GetCampaignOfferResult(webresponse)
                If FunctionEng.GetQisDBConfig("AGENT_SAVE_TRANS_LOG") = "Y" Then
                    FunctionEng.SaveTransLog("GateWayServiceENG.GetCampaignOffer", URL1)
                End If
            Catch ex As Exception
                Dim URL2 As String = FunctionEng.GetQisDBConfig("InteractGetOfferURL2") & InputPara
                Try
                    FunctionEng.SaveErrorLog("GateWayServiceENG.GetCampaignOffer", URL1 & " Exception : " & ex.Message)
                    Dim webRequest As WebRequest
                    Dim webresponse As WebResponse
                    webRequest = webRequest.Create(URL2)
                    webresponse = webRequest.GetResponse()
                    ret = GetCampaignOfferResult(webresponse)

                    If FunctionEng.GetQisDBConfig("AGENT_SAVE_TRANS_LOG") = "Y" Then
                        FunctionEng.SaveTransLog("GateWayServiceENG.GetCampaignOffer", URL2)
                    End If
                Catch ex1 As Exception
                    FunctionEng.SaveErrorLog("GateWayServiceENG.GetCampaignOffer", URL2 & " Exception1 : " & ex1.Message)
                End Try
            End Try
            Return ret
        End Function

        Private Function GetCampaignOfferResult(ByVal webresponse As WebResponse) As String()
            Dim ret() As String = {}
            Dim Stream As New StreamReader(webresponse.GetResponseStream(), System.Text.UnicodeEncoding.Default)
            If Stream.Peek <> -1 Then
                Dim tmp As String = Stream.ReadLine()
                If tmp.Trim <> "" Then
                    ret = Split(tmp, "#")
                End If
            End If
            Stream.Close()
            Stream = Nothing
            Return ret
        End Function

        Private Function GetCampaignStartSession(ByVal para As pCen.TbCustomerCenParaDB, ByVal SessionID As String) As Boolean
            Dim ret As Boolean = False
            Dim AudienceIdValue As String = ""
            If para.NETWORK_TYPE.ToUpper.IndexOf("POST") > -1 Then
                AudienceIdValue = para.ASSET_ID
            Else
                AudienceIdValue = para.MOBILE_NO
            End If
            Dim InputPara As String = "SESSIONID=" & SessionID & "&RELYONEXISTINGSESSION=false"
            InputPara += "&INTERACTIVECHANNEL=Touchpoint_Interact"
            InputPara += "&AUDIENCEID_NAME=SUBSCRIPTION_IDENTIFIER"
            InputPara += "&AUDIENCEID_VALUE=" & AudienceIdValue
            InputPara += "&AUDIENCEID_DATATYPE=string"
            InputPara += "&AUDIENCELEVEL=SUBSCRIPTION_IDENTIFIER"
            InputPara += "&PARAM1_NAME=MOBILE_NO"
            InputPara += "&PARAM1_VALUE=" & para.MOBILE_NO
            InputPara += "&PARAM1_DATATYPE=string"
            InputPara += "&PARAM2_NAME=MOBILE_STATUS"
            InputPara += "&PARAM2_VALUE=Active"
            InputPara += "&PARAM2_DATATYPE=string"
            InputPara += "&DEBUGFLAG=true"

            Dim URL1 As String = FunctionEng.GetQisDBConfig("InteractStartSessionURL1") & InputPara
            Try
                Dim webRequest As WebRequest
                Dim webresponse As WebResponse
                webRequest = webRequest.Create(URL1)
                webresponse = webRequest.GetResponse()
                ret = GetCampaignStartSessionResult(webresponse)

                If FunctionEng.GetQisDBConfig("AGENT_SAVE_TRANS_LOG") = "Y" Then
                    FunctionEng.SaveTransLog("GateWayServiceENG.GetCampaignStartSession", URL1)
                End If
            Catch ex As Exception
                Dim URL2 As String = FunctionEng.GetQisDBConfig("InteractStartSessionURL2") & InputPara
                Try
                    FunctionEng.SaveErrorLog("GateWayServiceENG.GetCampaignStartSession", URL1 & " Exception : " & ex.Message)
                    Dim webRequest As WebRequest
                    Dim webresponse As WebResponse
                    webRequest = webRequest.Create(URL2)
                    webresponse = webRequest.GetResponse()
                    ret = GetCampaignStartSessionResult(webresponse)

                    If FunctionEng.GetQisDBConfig("AGENT_SAVE_TRANS_LOG") = "Y" Then
                        FunctionEng.SaveTransLog("GateWayServiceENG.GetCampaignStartSession", URL2)
                    End If
                Catch ex1 As Exception
                    para.RESPONSE_MSG = ex1.Message
                    FunctionEng.SaveErrorLog("GateWayServiceENG.GetCampaignStartSession", URL2 & " Exception1 : " & ex1.Message)
                End Try
            End Try
            Return ret
        End Function

        Private Function GetCampaignStartSessionResult(ByVal webresponse As WebResponse) As Boolean
            Dim ret As Boolean = False
            Dim Stream As New StreamReader(webresponse.GetResponseStream(), System.Text.UnicodeEncoding.Default)
            If Stream.Peek <> -1 Then
                Dim tmp As String = Stream.ReadLine()
                If tmp.Trim <> "" Then
                    Dim tmpRes() As String = Split(tmp, "|")
                    If tmpRes(0).Trim = "" Then
                        ret = True
                    End If
                Else
                    ret = False
                End If
            Else
                ret = False
            End If
            Stream.Close()
            Stream = Nothing
            Return ret
        End Function
#End Region

#Region "Get Customer From HQ DB"
        Private Function GetCustomerFromHQ(ByVal MobileNo As String) As pCen.TbCustomerCenParaDB
            Dim ret As New pCen.TbCustomerCenParaDB
            Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
            Dim lnq As New CenLinqDB.TABLE.TbCustomerCenLinqDB
            ret = lnq.GetParameterByMobileNo(MobileNo, trans.Trans)
            If lnq.HaveData = True Then
                If FunctionEng.GetQisDBConfig("AGENT_SAVE_TRANS_LOG") = "Y" Then
                    FunctionEng.SaveTransLog("MobileEng.GetCustomerFromHQ", "Get Data From HQ Mobile No : " & MobileNo)
                End If

                trans.CommitTransaction()
            Else
                trans.RollbackTransaction()
                ret.RESPONSE_MSG = lnq.ErrorMessage
                FunctionEng.SaveErrorLog("MobileEng.GetCustomerFromHQ", "Mobile No : " & MobileNo & "  " & lnq.ErrorMessage)
            End If

            Return ret
        End Function
#End Region
#End Region
#Region "LDAP"
        Public Function LDAPAuth(ByVal UserName As String, ByVal Pwd As String) As paCen.LDAPResponsePara
            Dim ret As New paCen.LDAPResponsePara
            Dim InputPara As String = "USERNAME=" & UserName & "&PWD=" & Pwd & "&PROJCODE=" & FunctionEng.GetQisDBConfig("q_ldap_proj_code")
            Dim LDAP_URL1 As String = FunctionEng.GetQisDBConfig("ldap_url1") & InputPara
            Dim LDAP_URL2 As String = FunctionEng.GetQisDBConfig("ldap_url2") & InputPara
            Try
                Dim webRequest As WebRequest
                Dim webresponse As WebResponse
                webRequest = webRequest.Create(LDAP_URL1)
                webresponse = webRequest.GetResponse()
                ret = GetLDAPMsg(webresponse)
            Catch ex As Exception
                Try
                    Dim webRequest As WebRequest
                    Dim webresponse As WebResponse
                    webRequest = webRequest.Create(LDAP_URL2)
                    webresponse = webRequest.GetResponse()
                    ret = GetLDAPMsg(webresponse)
                Catch ex1 As Exception
                    ret.RESULT = False
                    ret.RESPONSE_MSG = ex1.Message
                End Try
            End Try
            Return ret
        End Function

        Private Function GetLDAPMsg(ByVal webresponse As WebResponse) As paCen.LDAPResponsePara
            Dim ret As New paCen.LDAPResponsePara
            Dim Stream As New StreamReader(webresponse.GetResponseStream())
            If Stream.Peek <> -1 Then
                Dim tmp As String = Stream.ReadLine()
                If tmp = "AUTHENSUCCESS" Then
                    ret.RESULT = True
                Else
                    ret.RESULT = False
                    ret.RESPONSE_MSG = tmp
                End If
            End If
            Stream.Close()
            Stream = Nothing
            Return ret
        End Function
#End Region

#Region "Send SMS"
        Public Function SendSMS(ByVal MobileNo As String, ByVal Msg As String) As SMSResponsePara
            Dim ret As New SMSResponsePara
            Dim InputPara As String = "MOBILENO=" & MobileNo & "&SMSTXT=" & Msg
            Dim SMS_URL1 As String = FunctionEng.GetQisDBConfig("send_sms_url1") & InputPara
            Try
                Dim webRequest As WebRequest
                Dim webresponse As WebResponse
                webRequest = webRequest.Create(SMS_URL1)
                ret = GetSMSMsg(webRequest, webresponse, InputPara)
                If ret.RESULT = False Then
                    FunctionEng.SaveErrorLog("GateWayServiceENG.SendSMS", ret.ERROR_RESPONSE & " URL :" & SMS_URL1)
                Else
                    If FunctionEng.GetQisDBConfig("AGENT_SAVE_TRANS_LOG") = "Y" Then
                        FunctionEng.SaveTransLog("GateWayServiceENG.SendSMS", SMS_URL1)
                    End If
                End If
            Catch ex As Exception
                FunctionEng.SaveErrorLog("GateWayServiceENG.SendSMS", SMS_URL1 & " Exception :" & ex.Message)

                Dim SMS_URL2 As String = FunctionEng.GetQisDBConfig("send_sms_url2") & InputPara
                Try
                    Dim webRequest As WebRequest
                    Dim webresponse As WebResponse
                    webRequest = webRequest.Create(SMS_URL2)
                    ret = GetSMSMsg(webRequest, webresponse, InputPara)
                    If ret.RESULT = False Then
                        FunctionEng.SaveErrorLog("GateWayServiceENG.SendSMS", ret.ERROR_RESPONSE & " URL :" & SMS_URL2)
                    Else
                        If FunctionEng.GetQisDBConfig("AGENT_SAVE_TRANS_LOG") = "Y" Then
                            FunctionEng.SaveTransLog("GateWayServiceENG.SendSMS", SMS_URL2)
                        End If
                    End If
                Catch ex1 As Exception
                    ret.ERROR_RESPONSE = ex1.Message
                    FunctionEng.SaveErrorLog("GateWayServiceENG.SendSMS", SMS_URL2 & " Exception :" & ex1.Message)
                End Try

            End Try

            Return ret
        End Function

        Private Function GetSMSMsg(ByVal webrequest As WebRequest, ByVal webresponse As WebResponse, ByVal InputPara As String) As SMSResponsePara
            Dim ret As New SMSResponsePara
            webrequest.ContentType = "application/x-www-form-urlencoded"
            webrequest.Method = "POST"
            Dim thaiEnc As System.Text.Encoding = System.Text.Encoding.GetEncoding("iso-8859-11")
            Dim bytes() As Byte = thaiEnc.GetBytes(InputPara)
            Dim os As Stream = Nothing

            Try
                webrequest.ContentLength = bytes.Length
                os = webrequest.GetRequestStream()
                os.Write(bytes, 0, bytes.Length)
                webresponse = webrequest.GetResponse()
                Dim Stream As New StreamReader(webresponse.GetResponseStream())
                If Stream.Peek <> -1 Then
                    Dim tmp As String = Stream.ReadLine()
                    If tmp = "DONE" Then
                        ret.RESULT = True
                    Else
                        ret.RESULT = False
                        ret.ERROR_RESPONSE = tmp
                    End If
                End If
                Stream.Close()
                Stream = Nothing

            Catch ex As WebException
                ret.RESULT = False
                ret.ERROR_RESPONSE = ex.Message
            Finally
                If os IsNot Nothing Then
                    os.Close()
                End If
            End Try

            Return ret
        End Function
#End Region

#Region "ATSR Post Servey"
        Public Function SendATSR(ByVal MobileNo As String, ByVal QueueID As String, ByVal ShopCode As String, ByVal TemplateID As String) As Boolean
            Dim ret As Boolean = False
            Dim InputPara As String = "MOBILENO=" & MobileNo & "&QUNIQID=" & QueueID & "&BRANCHCODE=" & ShopCode & "&TEMPLATEID=" & TemplateID
            Dim URL1 As String = FunctionEng.GetQisDBConfig("ATSR_AddSurveyURL1") & InputPara
            Try
                Dim webRequest As WebRequest
                Dim webresponse As WebResponse
                webRequest = webRequest.Create(URL1)
                webresponse = webRequest.GetResponse()
                ret = SendATSRMsg(webresponse)
                If ret = False Then
                    FunctionEng.SaveErrorLog("GateWayServiceENG.SendATSR", "ATSR ERROR : " & _AtsrError & "   URL :" & URL1)
                Else
                    If FunctionEng.GetQisDBConfig("AGENT_SAVE_TRANS_LOG") = "Y" Then
                        FunctionEng.SaveTransLog("GateWayServiceENG.SendATSR", URL1)
                    End If
                End If
            Catch ex As Exception
                FunctionEng.SaveErrorLog("GateWayServiceENG.SendATSR", URL1 & " Exception :" & ex.Message)
                Dim URL2 As String = FunctionEng.GetQisDBConfig("ATSR_AddSurveyURL2") & InputPara
                Try
                    Dim webRequest As WebRequest
                    Dim webresponse As WebResponse
                    webRequest = webRequest.Create(URL2)
                    webresponse = webRequest.GetResponse()
                    ret = SendATSRMsg(webresponse)
                    If ret = False Then
                        FunctionEng.SaveErrorLog("GateWayServiceENG.SendATSR", "ATSR ERROR : " & _AtsrError & "   URL :" & URL2)
                    Else
                        If FunctionEng.GetQisDBConfig("AGENT_SAVE_TRANS_LOG") = "Y" Then
                            FunctionEng.SaveTransLog("GateWayServiceENG.SendATSR", URL2)
                        End If
                    End If
                Catch ex1 As Exception
                    FunctionEng.SaveErrorLog("GateWayServiceENG.SendATSR", URL2 & " Exception :" & ex1.Message)
                End Try
            End Try
            Return ret
        End Function

        Public Function CreateSiebelActivity(ByVal data As CenParaDB.CSI.SiebelReqPara) As CenParaDB.CSI.SiebelResponsePara
            Dim InputPara As String = ""
            InputPara += "SIEBEL_TYPE=" & data.SIEBEL_TYPE
            InputPara += "&SIEBEL_ACTIVITYCATEGORY=" & data.ACTIVITYCATEGORY
            InputPara += "&SIEBEL_ACTIVITYSUBCATEGORY=" & data.ACTIVITYSUBCATEGORY
            InputPara += "&SIEBEL_DESCRIPTION=" & data.DESCRIPTION
            InputPara += "&SIEBEL_STATUS=" & data.STATUS
            InputPara += "&SIEBEL_MOBILENO=" & data.MOBILE_NO

            Dim ret As New CenParaDB.CSI.SiebelResponsePara
            ret = CallWebService(InputPara)
            Return ret
        End Function

        Dim _err As String = ""
        Private Function CallWebService(ByVal InputPara As String) As CenParaDB.CSI.SiebelResponsePara
            Dim ret As New CenParaDB.CSI.SiebelResponsePara
            Dim SIEBEL_URL1 As String = FunctionEng.GetQisDBConfig("seibel_url1") & InputPara
            Dim webRequest As WebRequest
            Dim webresponse As WebResponse
            webRequest = webRequest.Create(SIEBEL_URL1)
            ret = GetSiebelMsg(webRequest, webresponse, InputPara)

            If ret.RESULT = False Then
                FunctionEng.SaveErrorLog("AIS-HQ-Agent.GateWayServiceENG.CallWebService", ret.ErrorMessage & " " & SIEBEL_URL1)
                Dim SIEBEL_URL2 As String = FunctionEng.GetQisDBConfig("seibel_url2") & InputPara
                webRequest = webRequest.Create(SIEBEL_URL2)
                ret = GetSiebelMsg(webRequest, webresponse, InputPara)

                If ret.RESULT = False Then
                    _err = ret.ErrorMessage
                    FunctionEng.SaveErrorLog("AIS-HQ-Agent.GateWayServiceENG.CallWebService", "AIS-HQ-Agent.GenSiebelENG.CallWebService : " & ret.ErrorMessage & " " & SIEBEL_URL2)
                End If
            End If
            Return ret
        End Function

        Private Function GetSiebelMsg(ByVal webrequest As WebRequest, ByVal webresponse As WebResponse, ByVal InputPara As String, Optional ByVal GenType As String = "CREATE") As CenParaDB.CSI.SiebelResponsePara
            Dim ret As New CenParaDB.CSI.SiebelResponsePara
            webrequest.ContentType = "application/x-www-form-urlencoded"
            webrequest.Method = "POST"
            Dim thaiEnc As System.Text.Encoding = System.Text.Encoding.GetEncoding("iso-8859-11")
            Dim bytes() As Byte = thaiEnc.GetBytes(InputPara)
            Dim os As Stream = Nothing

            Try
                webrequest.ContentLength = bytes.Length
                os = webrequest.GetRequestStream()
                os.Write(bytes, 0, bytes.Length)
                webresponse = webrequest.GetResponse()
                Dim Stream As New StreamReader(webresponse.GetResponseStream())
                If Stream.Peek <> -1 Then
                    Dim tmp As String = Stream.ReadLine()
                    If GenType = "UPDATE" Then
                        If tmp.IndexOf("SUCCESS") > -1 Then
                            ret.RESULT = True
                        Else
                            ret.RESULT = False
                            ret.ErrorMessage = tmp
                        End If
                    Else
                        If tmp.IndexOf("SUCCESS-") > -1 Then
                            ret.RESULT = True
                            ret.ACTIVITY_ID = tmp.Replace("SUCCESS-", "")
                        Else
                            ret.RESULT = False
                            ret.ErrorMessage = tmp
                        End If
                    End If
                End If
                Stream.Close()
                Stream = Nothing

            Catch ex As WebException
                ret.RESULT = False
                ret.ErrorMessage = ex.Message
            Finally
                If os IsNot Nothing Then
                    os.Close()
                End If
            End Try
            Return ret
        End Function

        Dim _AtsrError As String = ""
        Private Function SendATSRMsg(ByVal webresponse As WebResponse) As Boolean
            Dim ret As Boolean = False
            Dim Stream As New StreamReader(webresponse.GetResponseStream(), System.Text.UnicodeEncoding.Default)
            If Stream.Peek <> -1 Then
                Dim tmp As String = Stream.ReadLine()
                If tmp.Trim = "DONE" Then
                    ret = True
                Else
                    _AtsrError = tmp
                End If
            End If
            Stream.Close()
            Stream = Nothing
            Return ret
        End Function

        Public Function GetATSRSurveyResult(ByVal QUNIQID As String) As ATSRResultPara
            Dim ret As New ATSRResultPara
            Dim InputPara As String = "QUNIQID=" & QUNIQID
            Dim URL1 As String = FunctionEng.GetQisDBConfig("ATSR_GetSurveyResultURL1") & InputPara
            Try
                Dim webRequest As WebRequest
                Dim webresponse As WebResponse
                webRequest = webRequest.Create(URL1)
                webresponse = webRequest.GetResponse()
                ret = GetATSRSurverResultMsg(webresponse, ret)
                If ret.RESULT <> "DONE" Then
                    FunctionEng.SaveErrorLog("GateWayServiceENG.GetATSRSurveyResult", "URL :" & URL1)
                Else
                    If FunctionEng.GetQisDBConfig("AGENT_SAVE_TRANS_LOG") = "Y" Then
                        FunctionEng.SaveTransLog("GateWayServiceENG.GetATSRSurveyResult", URL1)
                    End If
                End If
            Catch ex As Exception
                FunctionEng.SaveErrorLog("GateWayServiceENG.GetATSRSurveyResult", URL1 & " Exception :" & ex.Message)
                Dim URL2 As String = FunctionEng.GetQisDBConfig("ATSR_GetSurveyResultURL2") & InputPara
                Try
                    Dim webRequest As WebRequest
                    Dim webresponse As WebResponse
                    webRequest = webRequest.Create(URL2)
                    webresponse = webRequest.GetResponse()
                    ret = GetATSRSurverResultMsg(webresponse, ret)
                    If ret.RESULT <> "DONE" Then
                        FunctionEng.SaveErrorLog("GateWayServiceENG.GetATSRSurveyResult", "URL :" & URL2)
                    Else
                        If FunctionEng.GetQisDBConfig("AGENT_SAVE_TRANS_LOG") = "Y" Then
                            FunctionEng.SaveTransLog("GateWayServiceENG.GetATSRSurveyResult", URL2)
                        End If
                    End If
                Catch ex1 As Exception
                    FunctionEng.SaveErrorLog("GateWayServiceENG.GetATSRSurveyResult", URL2 & " Exception :" & ex1.Message)
                End Try
            End Try
            Return ret
        End Function

        Private Function GetATSRSurverResultMsg(ByVal webresponse As WebResponse, ByVal ret As ATSRResultPara) As ATSRResultPara
            Dim Stream As New StreamReader(webresponse.GetResponseStream(), System.Text.UnicodeEncoding.Default)
            If Stream.Peek <> -1 Then
                Dim tmp() As String = Split(Stream.ReadLine(), "#")
                If tmp.Length > 0 Then
                    If tmp.Length = 2 Then
                        ret.RESULT = tmp(0)
                        ret.RESULT_STATE = tmp(1)
                    ElseIf tmp.Length > 0 Then
                        Dim retVal() As String = Split(tmp(2), "|")

                        ret.RESULT = tmp(0)
                        ret.RESULT_STATE = tmp(1)
                        ret.RESULT_VALUE = retVal(0)
                        ret.NPS_SCORE = tmp(3)
                        ret.ATSR_CALL_TIME = tmp(7)

                        If retVal.Length = 2 Then
                            ret.HAVE_LEAVE_VOICE = (retVal(1) = "Y")
                        End If
                    End If
                Else
                    ret.ERROR_MESSAGE = "Fail No return data"
                End If
            End If
            Stream.Close()
            Stream = Nothing
            Return ret
        End Function
#End Region

#Region "Send Email"
        Public Function SendEmail(ByVal EmailTo As String, ByVal MailSubject As String, ByVal msg As String) As Boolean
            'http://10.13.181.100/AISQ/interface/sendemail.php?ADDRTO=akkarawatp@scoresolutions.co.th&MAILSUB=Test From AIS ทดสอบการส่งเมล์&MAILMSG=Mail Message รายละเอียดอีเมล์
            Dim ret As Boolean = False

            Dim InputPara As String = "ADDRTO=" & EmailTo & "&MAILSUB=" & MailSubject & "&MAILMSG=" & msg
            Dim SMS_URL1 As String = FunctionEng.GetQisDBConfig("SendMailURL1") & InputPara
            Try
                Dim webRequest As WebRequest
                Dim webresponse As WebResponse
                webRequest = webRequest.Create(SMS_URL1)
                ret = GetEmailMsg(webRequest, webresponse, InputPara)
                If ret = False Then
                    FunctionEng.SaveErrorLog("GateWayServiceENG.SendEmail", "Send Email Fail" & " URL :" & SMS_URL1)
                Else
                    If FunctionEng.GetQisDBConfig("AGENT_SAVE_TRANS_LOG") = "Y" Then
                        FunctionEng.SaveTransLog("GateWayServiceENG.SendEmail", SMS_URL1)
                    End If
                End If
            Catch ex As Exception
                FunctionEng.SaveErrorLog("GateWayServiceENG.SendSMS", SMS_URL1 & " Exception :" & ex.Message)

                Dim SMS_URL2 As String = FunctionEng.GetQisDBConfig("SendMailURL2") & InputPara
                Try
                    Dim webRequest As WebRequest
                    Dim webresponse As WebResponse
                    webRequest = webRequest.Create(SMS_URL2)
                    ret = GetEmailMsg(webRequest, webresponse, InputPara)
                    If ret = False Then
                        FunctionEng.SaveErrorLog("GateWayServiceENG.SendEmail", "Send Email Fail" & " URL :" & SMS_URL2)
                    Else
                        If FunctionEng.GetQisDBConfig("AGENT_SAVE_TRANS_LOG") = "Y" Then
                            FunctionEng.SaveTransLog("GateWayServiceENG.SendEmail", SMS_URL2)
                        End If
                    End If
                Catch ex1 As Exception
                    FunctionEng.SaveErrorLog("GateWayServiceENG.SendEmail", SMS_URL2 & " Exception :" & ex1.Message)
                End Try
            End Try

            Return ret
        End Function
        Private Function GetEmailMsg(ByVal webrequest As WebRequest, ByVal webresponse As WebResponse, ByVal InputPara As String) As Boolean
            Dim ret As Boolean = False
            webrequest.ContentType = "application/x-www-form-urlencoded"
            webrequest.Method = "POST"
            Dim thaiEnc As System.Text.Encoding = System.Text.Encoding.GetEncoding("iso-8859-11")
            Dim bytes() As Byte = thaiEnc.GetBytes(InputPara)
            Dim os As Stream = Nothing

            Try
                webrequest.ContentLength = bytes.Length
                os = webrequest.GetRequestStream()
                os.Write(bytes, 0, bytes.Length)
                webresponse = webrequest.GetResponse()
                Dim Stream As New StreamReader(webresponse.GetResponseStream())
                If Stream.Peek <> -1 Then
                    Dim tmp As String = Stream.ReadLine()
                    If tmp.Trim = "DONE" Then
                        ret = True
                    Else
                        ret = False
                    End If
                End If
                Stream.Close()
                Stream = Nothing

            Catch ex As WebException
                ret = False
            Finally
                If os IsNot Nothing Then
                    os.Close()
                End If
            End Try
            Return ret
        End Function
#End Region

#Region "Fault Managemnet"
        Public Function SendToSNMP(ByVal SysLocation As String, ByVal HostIP As String, ByVal HostName As String, ByVal AlarmType As String, ByVal AlarmName As String, ByVal Severity As String, ByVal AlarmValue As String, ByVal Desc As String, ByVal FlagClear As String, ByVal AlarmMethod As String, ByVal AlarmURL1 As String, ByVal AlarmURL2 As String) As Boolean
            Dim ret As Boolean = False
            Dim InputPara As String = "Location=" & SysLocation.Trim & "&HostIP=" & HostIP.Trim & "&HostName=" & HostName.Trim & "&AlarmType=" & AlarmType.Trim & "&AlarmName=" & AlarmName.Trim & "&Severity=" & Severity.Trim & "&AlarmValue=" & AlarmValue.Trim & "&Desc=" & Desc.Trim & "&FlagClear=" & FlagClear.Trim & "&AlarmMethod=" & AlarmMethod.Trim.ToLower
            Dim URL1 As String = AlarmURL1 & InputPara
            Dim _ResErr As String = ""
            Try
                Dim webRequest As WebRequest
                Dim webresponse As WebResponse
                webRequest = webRequest.Create(New System.Uri(URL1))
                ret = GetAlarmMsg(webRequest, webresponse, InputPara, _ResErr)
                If ret = False Then
                    FunctionEng.SaveErrorLog("GateWayServiceENG.SendToSMTP", _ResErr & " URL :" & URL1)
                Else
                    FunctionEng.SaveTransLog("GateWayServiceENG.SendToSMTP", URL1)
                End If
            Catch ex As Exception
                FunctionEng.SaveErrorLog("GateWayServiceENG.SendToSMTP", URL1 & " Exception :" & ex.Message)
                _ResErr = ""
                Dim URL2 As String = AlarmURL2 & InputPara
                Try
                    Dim webRequest As WebRequest
                    Dim webresponse As WebResponse
                    webRequest = webRequest.Create(New System.Uri(URL2))
                    ret = GetAlarmMsg(webRequest, webresponse, InputPara, _ResErr)
                    If ret = False Then
                        FunctionEng.SaveErrorLog("GateWayServiceENG.SendToSNMP", _ResErr & " URL :" & URL2)
                    Else
                        FunctionEng.SaveTransLog("GateWayServiceENG.SendToSNMP", URL2)
                    End If
                Catch ex1 As Exception
                    FunctionEng.SaveErrorLog("GateWayServiceENG.SendToSNMP", URL2 & " Exception :" & ex1.Message)
                End Try
            End Try

            Return ret
        End Function

        Private Function GetAlarmMsg(ByVal webrequest As WebRequest, ByVal webresponse As WebResponse, ByVal InputPara As String, ByVal ErrDesc As String) As Boolean
            Dim ret As Boolean = False
            webrequest.ContentType = "application/x-www-form-urlencoded"
            webrequest.Method = "POST"
            Dim thaiEnc As System.Text.Encoding = System.Text.Encoding.GetEncoding("iso-8859-11")
            Dim bytes() As Byte = thaiEnc.GetBytes(InputPara)
            Dim os As Stream = Nothing

            Try
                webrequest.ContentLength = bytes.Length
                os = webrequest.GetRequestStream()
                os.Write(bytes, 0, bytes.Length)
                webresponse = webrequest.GetResponse()
                Dim Stream As New StreamReader(webresponse.GetResponseStream())
                If Stream.Peek <> -1 Then
                    Dim tmp As String = Stream.ReadLine()
                    If tmp.Trim = "DONE" Then
                        ret = True
                    Else
                        ret = False
                        ErrDesc = tmp
                    End If
                End If
                Stream.Close()
                Stream = Nothing

            Catch ex As WebException
                ret = False
                ErrDesc = ex.Message
            Finally
                If os IsNot Nothing Then
                    os.Close()
                End If
            End Try

            Return ret
        End Function
#End Region
    End Class
End Namespace
