Imports System.IO
Public Class frmScreenSaver

    Private Sub frmVDO_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            lblCurrentTime.Text = frmChangeLanguage.MainCurrentTime
            lblCurrentFile.Text = frmChangeLanguage.MainCurrentFile
            Dim dblStartTime As Double = 0.0
            If lblCurrentTime.Text <> "0" AndAlso lblCurrentTime.Text <> "" Then
                If lblCurrentTime.Text.Length = 5 Then lblCurrentTime.Text = "00:" & lblCurrentTime.Text
                dblStartTime = GetSecFromTimeFormat(lblCurrentTime.Text)
            End If

            Dim myStack As New Stack
            myStack = GetFileFromDirectory(lblCurrentFile.Text)
            If myStack.Count = 0 Then Exit Sub

            TimerSetTime.Start()

            VDO.settings.setMode("loop", True)
            For i As Integer = 0 To myStack.Count - 1
                Dim file As String = myStack.Pop
                Dim songs = VDO.newMedia(file)
                VDO.currentPlaylist.appendItem(songs)
            Next

            VDO.Ctlcontrols.currentPosition = dblStartTime
            VDO.uiMode = "none"
            VDO.stretchToFit = True
            VDO.Ctlcontrols.play()

        Catch ex As Exception
        End Try

    End Sub

    Function GetFileFromDirectory(currentfile As String) As Stack
        Dim myStack As New Stack

        If Directory.Exists(Application.StartupPath & "\VDO") = True Then
            Dim di As New DirectoryInfo(Application.StartupPath & "\VDO")
            Dim files As FileSystemInfo() = di.GetFileSystemInfos
            Dim orderedFiles = files.OrderBy(Function(f) f.Name)

            Dim tmpStack1 As New Stack
            Dim tmpStack2 As New Stack
            Dim isMyFile As Boolean = False
            For Each f As FileSystemInfo In orderedFiles
                Dim File As String = f.FullName
                Dim fileType As String = f.Extension.ToLower
                If fileType <> ".mp4" And fileType <> ".wmv" And fileType <> ".flv" Then
                    Continue For
                End If
                If File = currentfile Then
                    tmpStack1.Push(File)
                    isMyFile = True
                Else
                    If isMyFile Then tmpStack1.Push(File) Else tmpStack2.Push(File)
                End If
            Next

            If tmpStack2.Count > 0 Then
                For i As Integer = 0 To tmpStack2.Count - 1
                    myStack.Push(tmpStack2.Pop)
                Next
            End If

            If tmpStack1.Count > 0 Then
                For i As Integer = 0 To tmpStack1.Count - 1
                    myStack.Push(tmpStack1.Pop)
                Next
            End If
        End If
        Return myStack
    End Function

    Public Shared Function GetSecFromTimeFormat(ByVal TimeFormat As String) As Double
        'แปลงเวลาในรูปแบบ HH:mm:ss ไปเป็นวินาที

        Dim ret As Double = 0
        If TimeFormat.Trim <> "" Then
            Dim tmp() As String = Split(TimeFormat, ":")
            Dim TimeSec As Integer = 0
            If CDbl(tmp(0)) > 0 Then
                TimeSec += (CDbl(tmp(0)) * 60 * 60)
            End If
            If Convert.ToInt64(tmp(1)) > 0 Then
                TimeSec += (CDbl(tmp(1)) * 60)
            End If
            ret = TimeSec + CDbl(tmp(2))
        End If
        Return ret
    End Function

    Private Sub frmScreenSaver_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseClick ', pb.MouseClick, pb1.MouseClick
        'TimerSetTime.Stop()
        'Me.DialogResult = Windows.Forms.DialogResult.Yes
        Me.Close()
    End Sub

    Private Sub frmVDO_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        TimerSetTime.Stop()
        frmChangeLanguage.lblScreenSaverCurrentTime.Text = Me.lblCurrentTime.Text
        frmChangeLanguage.lblScreenSaverCurrentFile.Text = Me.lblCurrentFile.Text

        Me.DialogResult = Windows.Forms.DialogResult.Yes
        'Me.Close()
    End Sub

    Private Sub TimerSetTime_Tick(sender As Object, e As EventArgs) Handles TimerSetTime.Tick
        lblCurrentTime.Text = VDO.Ctlcontrols.currentPositionString
        lblCurrentFile.Text = VDO.Ctlcontrols.currentItem.sourceURL
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

    Private Sub VDO_ClickEvent(sender As Object, e As AxWMPLib._WMPOCXEvents_ClickEvent) Handles VDO.ClickEvent
        Me.Close()
    End Sub

    Private Sub TimerCheckBillerLogoFile_Tick(sender As Object, e As EventArgs) Handles TimerCheckBillerLogoFile.Tick
        Try
            Dim ws As New FileService.EquipmentFileService
            ws.Url = "http://akkarawatp/webtms/EquipmentFileService.asmx?WSDL"
            ws.Timeout = 50000
            Dim dt As New DataTable
            dt = ws.GetBillerLogoFile
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    Dim trans As New LinqDB.ConnectDB.TransactionDB
                    Dim lnq As New LinqDB.TABLE.MsBillerLinqDB
                    lnq.GetDataByPK(dr("BillerID"), trans.Trans)
                    If lnq.ID > 0 Then
                        Dim LogoFile As String = Application.StartupPath & "\Images\BillerLogo\Logo" & lnq.BILLER_CODE & ".JPG"
                        If File.Exists(LogoFile) = True Then
                            Try
                                File.SetAttributes(LogoFile, FileAttributes.Normal)
                                File.Delete(LogoFile)
                            Catch ex As Exception

                            End Try
                        End If


                        Dim ImageLogoFile As String = "\Images\BillerLogo\Logo" & lnq.BILLER_CODE & ".JPG"
                        Dim FileByte() As Byte = dr("FileByte")
                        Dim fs As FileStream
                        fs = New FileStream(LogoFile, FileMode.CreateNew)
                        fs.Write(FileByte, 0, FileByte.Length)
                        fs.Close()

                        If ws.DeleteBillerLogoFile(dr("FileName")) = True Then
                            lnq.IMAGE_LOGO = ImageLogoFile

                            If lnq.UpdateByPK("TimerCheckBillerLogoFile", trans.Trans) = True Then
                                trans.CommitTransaction()
                            Else
                                trans.RollbackTransaction()
                            End If
                        End If
                    End If
                    lnq = Nothing
                Next
            End If
            dt.Dispose()
            ws = Nothing
        Catch ex As Exception

        End Try
    End Sub
End Class