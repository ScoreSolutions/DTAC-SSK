Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports Engine.Common
Imports System.IO
Imports System.Data
Imports Cen = CenLinqDB.TABLE

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class EquipmentFileService
     Inherits System.Web.Services.WebService

    '<WebMethod()> _
    ' Public Function SendFileToDC(ByVal FileByte() As Byte, ByVal EventDate As String, ByVal ShopID As String, _
    '                              ByVal Target_Type As String, ByVal Mime_Type As String, ByVal LoginName As String) As Boolean
    '    Dim ret As Boolean = False
    '    Dim FloderName As String = Now.Year & Now.ToString("MMdd")
    '    Dim FileName As String = Now.Year & Now.ToString("MMddHHmmssfff") & Mime_Type
    '    Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
    '    Try
    '        If trans.Trans IsNot Nothing Then
    '            Dim arrShopID As String() = ShopID.Split(",")
    '            If arrShopID.Length > 0 Then
    '                Dim _err As String = ""
    '                For cntShop As Integer = 0 To arrShopID.Length - 1
    '                    Dim ObjTB_Shop_File_Schedule As New Cen.TbShopFileScheduleCenLinqDB
    '                    With ObjTB_Shop_File_Schedule
    '                        .EVENT_DATE = Engine.Common.FunctionEng.cStrToDate(EventDate)
    '                        .SHOP_ID = arrShopID(cntShop)
    '                        .TARGET_TYPE = Target_Type
    '                        .FOLDER_NAME = FloderName
    '                        .FILE_NAME = FileName

    '                        ret = .InsertData(LoginName, trans.Trans)
    '                        If ret = False Then
    '                            _err = .ErrorMessage
    '                            trans.RollbackTransaction()
    '                            ret = False
    '                            Exit For
    '                        End If
    '                    End With
    '                Next

    '                If ret = True Then
    '                    Dim path As String = System.Configuration.ConfigurationSettings.AppSettings("DirectorySendImage").ToString & FloderName & "\"
    '                    If Directory.Exists(path) = False Then
    '                        Directory.CreateDirectory(path)
    '                    End If

    '                    Dim fs As FileStream
    '                    If File.Exists(path & FileName) = True Then
    '                        File.Delete(path & FileName)
    '                    End If

    '                    fs = New FileStream(path & FileName, FileMode.CreateNew)
    '                    fs.Write(FileByte, 0, FileByte.Length)
    '                    fs.Close()

    '                    trans.CommitTransaction()
    '                    ret = True
    '                Else
    '                    trans.RollbackTransaction()
    '                    FunctionEng.SaveErrorLog("EquipmentFileService.SendFileToDC", _err)
    '                    ret = False
    '                End If
    '            End If
    '        End If
    '    Catch ex As Exception
    '        trans.RollbackTransaction()
    '        FunctionEng.SaveErrorLog("EquipmentFileService.SendFileToDC", "Exception : " & ex.Message)
    '        ret = False
    '    End Try
    '    Return ret
    'End Function


    Dim _err As String = ""

    '<WebMethod()> _
    'Public Function TestTransferByteData() As Byte()
    '    Dim _path As String = "D:\WebConfigUploadPath\20140203\20140202114511858.mov"

    '    Dim imagestream As FileStream = New FileStream(_path, FileMode.Open)
    '    Dim data() As Byte
    '    ReDim data(CInt(imagestream.Length))
    '    imagestream.Read(data, 0, CInt(imagestream.Length - 1))
    '    imagestream.Close()

    '    Return data
    'End Function

    <WebMethod()> _
    Public Function GetFileByteStreamFromDC(ByVal FilePath As String) As DataTable
        Dim ret As New DataTable
        ret.Columns.Add("CharData")
        Dim Stream As FileStream = File.OpenRead(FilePath)
        Dim buffer As Byte() = New Byte((1024 * 16) - 1) {}
        While Stream.Read(buffer, 0, buffer.Length)
            Try
                Dim dr As DataRow = ret.NewRow
                dr("CharData") = Convert.ToBase64String(buffer)
                ret.Rows.Add(dr)
            Catch ex As Exception

            End Try
        End While
        Stream.Close()

        ret.TableName = "TestTransfetDataTableData"
        Return ret
    End Function

    '<WebMethod()> _
    'Public Function TestTransferStringData() As String
    '    Dim _path As String = "D:\WebConfigUploadPath\20140203\20140202114511858.mov"
    '    Dim ret As New StringBuilder

    '    Dim b() As Byte = File.ReadAllBytes(_path)

    '    ret.Append(Convert.ToBase64String(b))

    '    'Dim imagestream As FileStream = New FileStream(_path, FileMode.Open)
    '    'Dim data() As Byte
    '    'ReDim data(CInt(imagestream.Length))
    '    'imagestream.Read(data, 0, CInt(imagestream.Length - 1))
    '    'imagestream.Close()

    '    Return ret.ToString
    'End Function


    '<WebMethod()> _
    'Public Function GetFileFromDC(ByVal EventDate As String, ByVal ShopAbb As String, ByVal TargetType As String) As DataTable
    '    Dim ret As New DataTable
    '    Dim tmpDT As New DataTable
    '    ret.Columns.Add("FileScheduleID", GetType(Long))
    '    ret.Columns.Add("Files", GetType(Byte()))
    '    Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
    '    Try
    '        If trans.Trans IsNot Nothing Then
    '            Dim wh As String = "select fs.id, fb.Folder_Name, fb.File_Name "
    '            wh += " from tb_shop_file_schedule fs"
    '            wh += " inner join tb_shop_file_byte fb on fb.id=fs.tb_shop_file_byte_id "
    '            wh += " where convert(varchar(10),fs.event_date,120)='" & EventDate & "'"
    '            wh += " and fs.Shop_ID In (Select ID From TB_SHOP Where Shop_Abb ='" & ShopAbb & "')"
    '            wh += " and fb.Target_Type = '" & TargetType & "'"
    '            wh += " and fs.transfer_status='1'"
    '            Dim ObjTB_Shop_File_Schedule As New Cen.TbShopFileScheduleCenLinqDB
    '            With ObjTB_Shop_File_Schedule
    '                tmpDT = .GetListBySql(wh, trans.Trans)
    '            End With
    '            trans.CommitTransaction()
    '        End If

    '        Dim dr As DataRow
    '        For cnt As Integer = 0 To tmpDT.Rows.Count - 1
    '            dr = ret.NewRow
    '            Dim _path As String
    '            _path = tmpDT.Rows(cnt).Item("Folder_Name") & "\" & tmpDT.Rows(cnt).Item("File_Name") & ""
    '            Dim imagestream As FileStream = New FileStream(_path, FileMode.Open)
    '            Dim data() As Byte
    '            ReDim data(CInt(imagestream.Length))
    '            imagestream.Read(data, 0, CInt(imagestream.Length - 1))
    '            imagestream.Close()

    '            dr("Files") = data
    '            dr("FileScheduleID") = tmpDT.Rows(cnt)("id")
    '            ret.Rows.Add(dr)
    '        Next
    '    Catch ex As Exception
    '        'ret = New DataTable
    '        Throw New Exception(ex.StackTrace)
    '    End Try
    '    ret.TableName = "GetFileFromDC"
    '    Return ret
    'End Function

    <WebMethod()> _
    Public Function GetFileInfoFromDC(ByVal EventDate As String, ByVal ShopAbb As String, ByVal TargetType As String) As DataTable
        Dim ret As New DataTable
        Dim trans As New CenLinqDB.Common.Utilities.TransactionDB
        Try
            If trans.Trans IsNot Nothing Then
                Dim wh As String = "select fs.id, fb.Folder_Name, fb.File_Name "
                wh += " from tb_shop_file_schedule fs"
                wh += " inner join tb_shop_file_byte fb on fb.id=fs.tb_shop_file_byte_id "
                wh += " where convert(varchar(10),fs.event_date,120)='" & EventDate & "'"
                wh += " and fs.Shop_ID In (Select ID From TB_SHOP Where Shop_Abb ='" & ShopAbb & "')"
                wh += " and fb.Target_Type = '" & TargetType & "'"
                wh += " and fs.transfer_status='1'"
                Dim ObjTB_Shop_File_Schedule As New Cen.TbShopFileScheduleCenLinqDB
                With ObjTB_Shop_File_Schedule
                    ret = .GetListBySql(wh, trans.Trans)
                End With
                trans.CommitTransaction()
            End If

            If ret.Rows.Count > 0 Then
                ret.Columns.Add("FileSize")
                For cnt As Integer = 0 To ret.Rows.Count - 1
                    Dim _path As String
                    _path = ret.Rows(cnt).Item("Folder_Name") & "\" & ret.Rows(cnt).Item("File_Name") & ""
                    Dim ff As New FileInfo(_path)
                    ret.Rows(cnt)("FileSize") = ff.Length
                    ff = Nothing
                Next
            End If

        Catch ex As Exception
            ret = New DataTable
        End Try
        ret.TableName = "GetFileInfoFromDC"
        Return ret
    End Function

    '<WebMethod()> _
    'Public Function GetFileByteStreamFromDC(ByVal FilePath As String) As Byte()
    '    Dim data() As Byte
    '    Try
    '        Dim imagestream As FileStream = New FileStream(FilePath, FileMode.Open)
    '        ReDim data(CInt(imagestream.Length))
    '        imagestream.Read(data, 0, CInt(imagestream.Length - 1))
    '        imagestream.Close()
    '    Catch ex As Exception

    '    End Try

    '    Return data
    'End Function

    <WebMethod()> _
    Public Function UpdateTransferStatus(ByVal FileScheduleID As String) As Boolean
        _err = ""
        Dim ret As Boolean = False

        Dim eng As New Engine.Configuration.ShopEventScheduleSettingENG
        ret = eng.UpdateFileScheduleTransferStatus(FileScheduleID, "2")
        If ret = False Then
            _err = eng.ErrMessage
        End If
        eng = Nothing
        
        Return ret
    End Function

    <WebMethod()> _
    Public Function ErrorMessage() As String
        Return _err
    End Function

    <WebMethod()> _
    Public Function GetBillerLogoFile() As DataTable
        Dim dt As New DataTable
        dt.Columns.Add("BillerID")
        dt.Columns.Add("FileName")
        dt.Columns.Add("FileByte", GetType(Byte()))

        For Each fle As String In Directory.GetFiles(System.Configuration.ConfigurationSettings.AppSettings("WebConfigUploadPath").ToString)
            Dim dr As DataRow = dt.NewRow
            Dim f As New FileInfo(fle)
            dr("BillerID") = Split(f.Name, "_")(0)
            dr("FileName") = fle
            dr("FileByte") = File.ReadAllBytes(fle)
            dt.Rows.Add(dr)
        Next
        dt.TableName = "GetBillerLogoFile"
        Return dt
    End Function

    <WebMethod()>
    Public Function DeleteBillerLogoFile(FileName As String) As Boolean
        Dim ret As Boolean = False
        Try
            'Dim TmpFile As String = System.Configuration.ConfigurationSettings.AppSettings("WebConfigUploadPath").ToString & "\" & FileName
            If File.Exists(FileName) = True Then
                File.SetAttributes(FileName, FileAttributes.Normal)
                File.Delete(FileName)
                ret = True
            Else
                ret = True
            End If
        Catch ex As Exception

        End Try
        Return ret
    End Function



End Class
