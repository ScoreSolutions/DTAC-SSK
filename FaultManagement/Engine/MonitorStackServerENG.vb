Imports System.Management

Public Class MonitorStackServerENG
    Dim _err As String = ""

    Public ReadOnly Property ErrorMessage() As String
        Get
            Return _err
        End Get
    End Property


    Public Function SaveStackEmail(ByVal Para As CenParaDB.TABLE.TbStackServerEmailCenParaDB) As Boolean
        Dim ret As Boolean = False
        Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
        If trans.Trans IsNot Nothing Then
            Dim lnq As New CenLinqDB.TABLE.TbStackServerEmailCenLinqDB
            lnq.SERVER_NAME = Para.SERVER_NAME
            lnq.IP_ADDRESS = Para.IP_ADDRESS
            lnq.LOCATION = Para.LOCATION
            lnq.EMAIL_TO = Para.EMAIL_TO

            ret = lnq.InsertData("MonitorStackServerENG.SaveStackEmail", trans.Trans)
            If ret = True Then
                trans.CommitTransaction()
            Else
                trans.RollbackTransaction()
                _err = lnq.ErrorMessage
            End If
        Else
            _err = "Cannot Connect To Database"
        End If

        Return ret
    End Function

    Public Function GetStackEmail(ByVal ServerIP As String) As DataTable
        Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
        Dim dt As New DataTable
        If trans.Trans IsNot Nothing Then
            Dim lnq As New CenLinqDB.TABLE.TbStackServerEmailCenLinqDB
            dt = lnq.GetDataList("ip_address = '" & ServerIP & "'", "email_to", trans.Trans)
            trans.CommitTransaction()
        Else
            _err = "Cannot Connect To Database"
        End If

        Return dt
    End Function

    Public Function DeleteStackEmail(ByVal vID As Long) As Boolean
        Dim ret As Boolean = False
        Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
        If trans.Trans IsNot Nothing Then
            Dim lnq As New CenLinqDB.TABLE.TbStackServerEmailCenLinqDB
            lnq.DeleteByPK(vID, trans.Trans)
            trans.CommitTransaction()
            ret = True
        Else
            ret = False
            _err = "Cannot Connect To Database"
        End If

        Return ret
    End Function


    Public Function SaveStackSMS(ByVal Para As CenParaDB.TABLE.TbStackServerSmsCenParaDB) As Boolean
        Dim ret As Boolean = False
        Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
        If trans.Trans IsNot Nothing Then
            Dim lnq As New CenLinqDB.TABLE.TbStackServerSmsCenLinqDB
            lnq.SERVER_NAME = Para.SERVER_NAME
            lnq.IP_ADDRESS = Para.IP_ADDRESS
            lnq.LOCATION = Para.LOCATION
            lnq.SMS_TO = Para.SMS_TO

            ret = lnq.InsertData("MonitorStackServerENG.SaveStackSMS", trans.Trans)
            If ret = True Then
                trans.CommitTransaction()
            Else
                trans.RollbackTransaction()
                _err = lnq.ErrorMessage
            End If
        Else
            _err = "Cannot Connect To Database"
        End If

        Return ret
    End Function

    Public Function GetStackSMS(ByVal ServerIP As String) As DataTable
        Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
        Dim dt As New DataTable
        If trans.Trans IsNot Nothing Then
            Dim lnq As New CenLinqDB.TABLE.TbStackServerSmsCenLinqDB
            dt = lnq.GetDataList("ip_address = '" & ServerIP & "'", "sms_to", trans.Trans)
            trans.CommitTransaction()
        Else
            _err = "Cannot Connect To Database"
        End If

        Return dt
    End Function

    Public Function DeleteStackSms(ByVal vID As Long) As Boolean
        Dim ret As Boolean = False
        Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
        If trans.Trans IsNot Nothing Then
            Dim lnq As New CenLinqDB.TABLE.TbStackServerSmsCenLinqDB
            lnq.DeleteByPK(vID, trans.Trans)
            trans.CommitTransaction()
            ret = True
        Else
            ret = False
            _err = "Cannot Connect To Database"
        End If

        Return ret
    End Function

    Public Function SaveStackServerLog(ByVal Para As CenParaDB.TABLE.TbStackServerLogCenParaDB) As Boolean
        Dim ret As Boolean = False
        Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
        If trans.Trans IsNot Nothing Then
            Dim lnq As New CenLinqDB.TABLE.TbStackServerLogCenLinqDB
            lnq.SERVER_NAME = Para.SERVER_NAME
            lnq.IP_ADDRESS = Para.IP_ADDRESS
            lnq.LOCATION = Para.LOCATION
            lnq.LOG_DATE = DateTime.Now
            lnq.STACK_DESC = Para.STACK_DESC
            lnq.SEND_EMAIL = "N"
            lnq.SEND_SMS = "N"

            ret = lnq.InsertData("MonitorStackServerENG.SaveStackServerLog", trans.Trans)
            If ret = True Then
                trans.CommitTransaction()

                SendStackEmail(lnq.ID, Para.IP_ADDRESS, Para.STACK_DESC)
                SendStackSMS(lnq.ID, Para.IP_ADDRESS, Para.STACK_DESC)
            Else
                _err = lnq.ErrorMessage
                trans.RollbackTransaction()
            End If
        Else
            _err = "Cannot Connect To Database"
        End If

        Return ret
    End Function

    Private Function SendStackEmail(ByVal LogID As Long, ByVal ServerID As String, ByVal StackDesc As String) As Boolean
        Dim ret As Boolean = True
        Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
        If trans.Trans IsNot Nothing Then
            Dim dt As DataTable = GetStackEmail(ServerID)
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    Dim eng As New Engine.GeteWay.GateWayServiceENG
                    ret = eng.SendEmail(dr("email_to"), "Monitor Stack Server : Time :" & DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"), StackDesc)
                    If ret = False Then
                        Exit For
                    End If
                Next
                dt.Dispose()
                dt = Nothing

                If ret = True Then
                    Dim lnq As New CenLinqDB.TABLE.TbStackServerLogCenLinqDB
                    lnq.GetDataByPK(LogID, trans.Trans)
                    lnq.SEND_EMAIL = "Y"
                    ret = lnq.UpdateByPK("MonitorStackServerENG.SendStackEmail", trans.Trans)
                    If ret = True Then
                        trans.CommitTransaction()
                    Else
                        trans.RollbackTransaction()
                    End If
                End If
            End If
        Else
            ret = False
            _err = "Cannot Connect To Database"
        End If

        Return ret
    End Function

    Private Function SendStackSMS(ByVal LogID As Long, ByVal ServerID As String, ByVal StackDesc As String) As Boolean
        Dim ret As Boolean = True
        Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
        If trans.Trans IsNot Nothing Then
            Dim dt As DataTable = GetStackSMS(ServerID)
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    Dim eng As New Engine.GeteWay.GateWayServiceENG
                    Dim res As New CenParaDB.GateWay.SMSResponsePara
                    res = eng.SendSMS(dr("sms_to"), StackDesc)
                    ret = res.RESULT
                    If ret = False Then
                        _err = res.ERROR_RESPONSE
                        Exit For
                    End If
                Next
                dt.Dispose()
                dt = Nothing

                If ret = True Then
                    Dim lnq As New CenLinqDB.TABLE.TbStackServerLogCenLinqDB
                    lnq.GetDataByPK(LogID, trans.Trans)
                    lnq.SEND_SMS = "Y"
                    ret = lnq.UpdateByPK("MonitorStackServerENG.SendStackSMS", trans.Trans)

                    If ret = True Then
                        trans.CommitTransaction()
                    Else
                        trans.RollbackTransaction()
                    End If
                End If
            End If
        Else
            ret = False
            _err = "Cannot Connect To Database"
        End If

        Return ret
    End Function

    Private Function GetCPU() As String
        Dim ret As String = ""
        Dim moReturn As Management.ManagementObjectCollection
        Dim moSearch As Management.ManagementObjectSearcher
        Dim mo As Management.ManagementObject
        moSearch = New Management.ManagementObjectSearcher("Select * from Win32_Processor")
        moReturn = moSearch.Get
        For Each mo In moReturn
            Dim ProcessName As String = mo("Name")
            Dim PercentUsage As String = mo("LoadPercentage")
            ret += "CPU Usage : " & PercentUsage & " %" & vbNewLine
        Next
        moSearch.Dispose()
        moReturn.Dispose()
        Return ret
    End Function

    Private Function GetRam() As String
        Dim ret As String = ""
        Dim ComInfo As New Devices.ComputerInfo
        ret = "Available Physical Memory :" & (ComInfo.AvailablePhysicalMemory / 1024 / 1024 / 1024).ToString("#,##0.00") & " GB" & vbNewLine
        Return ret
    End Function

    Private Function GetHDD() As String
        Dim desc As String = " HDD Info :" & vbNewLine
        Dim drives As System.IO.DriveInfo() = System.IO.DriveInfo.GetDrives
        For Each dri As System.IO.DriveInfo In drives
            If dri.IsReady = True Then
                desc += "Drive Name : " & dri.Name & vbNewLine
                desc += "Volume Label : " & dri.VolumeLabel & vbNewLine
                desc += "Total Free Space : " & (dri.TotalFreeSpace / (1024 ^ 3)).ToString("#,##0.00") & " GB" & vbNewLine
                desc += "TotalSize : " & (dri.TotalSize / (1024 ^ 3)).ToString("#,##0.00") & " GB" & vbNewLine & vbNewLine
            End If
        Next

        Return desc
    End Function
    Private Function GetWindowsService() As String
        Dim desc As String = "Windows Service Info :" & vbNewLine

        For Each s As System.ServiceProcess.ServiceController In System.ServiceProcess.ServiceController.GetServices
            desc += "Service Name : " & s.ServiceName & vbNewLine
            desc += "Service Type : " & s.ServiceType & vbNewLine
            desc += "Status : " & s.Status & vbNewLine
        Next
        Return desc
    End Function
End Class
