Imports System.IO
Imports CenParaDB.Common

Partial Class UserControls_ctlBrowseFile
    Inherits System.Web.UI.UserControl

    Public Event Upload(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Overrides Property ID() As String
        Get
            Return MyBase.ID
        End Get
        Set(ByVal value As String)
            MyBase.ID = value
        End Set
    End Property

    Public ReadOnly Property TmpFileUploadPara() As TmpFileUploadPara
        Get
            Dim TmpName As String = GetTempFileName()
            If File.Exists(TmpName) = True Then
                Dim FileProp As New FileInfo(TmpName)
                Dim fData As New TmpFileUploadPara
                fData.TmpFileByte = GetTempFileByte()
                fData.FileExtent = FileProp.Extension
                fData.FileName = FileProp.Name
                Return fData
            Else
                Return Nothing
            End If
        End Get
    End Property

    Public Property Width() As Unit
        Get
            Return FileBrowse.Width
        End Get
        Set(ByVal value As Unit)
            FileBrowse.Width = value
        End Set
    End Property

    Public ReadOnly Property HasFile() As Boolean
        Get
            Return File.Exists(GetTempFileName)
        End Get
    End Property

    Public Property VisibleUploadButton() As Boolean
        Get
            Return Button1.Visible
        End Get
        Set(ByVal value As Boolean)
            Button1.Visible = value
        End Set
    End Property

    Protected Sub FileBrowse_UploadedComplete(ByVal sender As Object, ByVal e As AjaxControlToolkit.AsyncFileUploadEventArgs) Handles FileBrowse.UploadedComplete
        If e.state = AjaxControlToolkit.AsyncFileUploadState.Success Then
            Dim FileProp As New FileInfo(FileBrowse.FileName)
            Dim fData As New TmpFileUploadPara
            'fData.TmpFileByte = ConvertStreamToByte(FileBrowse.FileContent)
            fData.TmpFileByte = FileBrowse.FileBytes
            fData.FileExtent = FileProp.Extension
            fData.FileName = FileProp.Name
            SaveTempFile(fData)

            'SaveTempFile(FileBrowse, FileProp.Extension)

        End If
    End Sub


    'Private Function ConvertStreamToByte(ByVal stream As System.IO.Stream) As Byte()
    '    Dim fileStream As FileStream = File.Create("D:\vdo3.mov", CInt(stream.Length))
    '    ' Initialize the bytes array with the stream length and then fill it with data
    '    'Dim bytesInStream As Byte() = New Byte(stream.Length - 1) {}
    '    'stream.Read(bytesInStream, 0, bytesInStream.Length)
    '    '' Use write method to write to the file specified above
    '    'fileStream.Write(bytesInStream, 0, bytesInStream.Length)


    '    Dim buffer As Byte() = New Byte(1024 - 1) {}
    '    Dim len As Integer = stream.Read(buffer, 0, buffer.Length)
    '    While len > 0
    '        Try
    '            fileStream.Write(buffer, 0, buffer.Length)
    '            len = stream.Read(buffer, 0, buffer.Length)
    '        Catch ex As Exception

    '        End Try

    '    End While
    '    fileStream.Close()
    'End Function

    Private Sub SaveTempFile(ByVal FileBrowse As AjaxControlToolkit.AsyncFileUpload, ByVal FileExtent As String)
        Dim TmpFileUpload As String = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings("TempUpload"))
        If Directory.Exists(TmpFileUpload) = False Then
            Directory.CreateDirectory(TmpFileUpload)
        End If

        Dim ClientIP As String = Request.UserHostAddress
        If Directory.Exists(TmpFileUpload & "/" & ClientIP) = False Then
            Directory.CreateDirectory(TmpFileUpload & "/" & ClientIP)
        End If

        'Dim fs As FileStream
        'Dim byteData() As Byte
        'byteData = fData.TmpFileByte

        Dim PathFile As String = GetTempPath()
        Dim FileName As String = PathFile & "\" & Config.GetLoginHistoryID & "_" & Me.Page.AppRelativeVirtualPath.Replace("~/", "").Replace(".aspx", "") & "_" & FileBrowse.ClientID & FileExtent
        If File.Exists(FileName) = True Then
            File.Delete(FileName)
        End If
        FileBrowse.SaveAs(FileName)

        'fs = New FileStream(FileName, FileMode.CreateNew)
        'fs.Write(byteData, 0, byteData.Length)
        'fs.Close()
    End Sub
    Private Sub SaveTempFile(ByVal fData As TmpFileUploadPara)
        Dim TmpFileUpload As String = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings("TempUpload"))
        If Directory.Exists(TmpFileUpload) = False Then
            Directory.CreateDirectory(TmpFileUpload)
        End If

        Dim ClientIP As String = Request.Url.Host
        If Directory.Exists(TmpFileUpload & "\" & ClientIP) = False Then
            Directory.CreateDirectory(TmpFileUpload & "\" & ClientIP)
        End If

        Dim fs As FileStream
        Dim byteData() As Byte
        byteData = fData.TmpFileByte

        Dim PathFile As String = GetTempPath()
        Dim FileName As String = PathFile & "\" & Config.GetLoginHistoryID & "_" & Me.Page.AppRelativeVirtualPath.Replace("~/", "").Replace(".aspx", "") & "_" & FileBrowse.ClientID & fData.FileExtent
        If File.Exists(FileName) = True Then
            File.Delete(FileName)
        End If

        fs = New FileStream(FileName, FileMode.CreateNew)
        fs.Write(byteData, 0, byteData.Length)
        fs.Close()
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        RaiseEvent Upload(sender, e)
    End Sub

    Public Sub ClearFile()
        Dim TempFile As String = GetTempFileName()
        If File.Exists(TempFile) Then
            File.Delete(TempFile)
        End If
    End Sub

    Private ReadOnly Property GetTempPath() As String
        Get
            Return Server.MapPath(System.Configuration.ConfigurationManager.AppSettings("TempUpload")) & "\" & Request.Url.Host
        End Get
    End Property

    Private Function GetTempFileByte() As Byte()
        Dim TempName As String = Config.GetLoginHistoryID & "_" & Me.Page.AppRelativeVirtualPath.Replace("~/", "").Replace(".aspx", "") & "_" & FileBrowse.ClientID

        Dim f() As Byte
        If Directory.Exists(GetTempPath) = True Then
            For Each fle As String In Directory.GetFiles(GetTempPath)
                If InStr(fle, TempName) > 0 Then
                    f = File.ReadAllBytes(fle)
                    Exit For
                End If
            Next
        End If

        Return f
    End Function

    Private Function GetTempFileName() As String
        Dim TempName As String = Config.GetLoginHistoryID & "_" & Me.Page.AppRelativeVirtualPath.Replace("~/", "").Replace(".aspx", "") & "_" & FileBrowse.ClientID

        Dim f As String = ""
        If Directory.Exists(GetTempPath) = True Then
            For Each fle As String In Directory.GetFiles(GetTempPath)
                If InStr(fle, TempName) > 0 Then
                    f = fle
                    Exit For
                End If
            Next
        End If
        Return f
    End Function


    Public Sub SaveFile(ByVal PathFile As String, ByVal fileName As String)
        Dim TempName As String = GetTempFileName()
        If TempName <> "" Then
            If PathFile.EndsWith("\") = False Then
                PathFile += "\"
            End If
            If Directory.Exists(PathFile) = False Then
                Directory.CreateDirectory(PathFile)
            End If

            File.SetAttributes(TempName, FileAttributes.Normal)
            File.Move(TempName, PathFile & fileName)
            ClearFile()
        End If
    End Sub
End Class
