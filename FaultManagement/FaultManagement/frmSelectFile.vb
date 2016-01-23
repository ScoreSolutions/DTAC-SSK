Public Class frmSelectFile

    Private Sub frmSelectFile_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim driveinfo As New Engine.Info.DriveInfoENG
            Dim dt As New DataTable
            dt = driveinfo.GetDriveInfoToDT
            If dt.Rows.Count > 0 Then
                cbDrive.DataSource = dt
                PopulateData(dt.Rows(0).Item(0).ToString & ":\")
                txtPath.Text = dt.Rows(0).Item(0).ToString & ":\"
            End If
            AddColumn()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Sub AddColumn()
        'lvDetail.Columns.Add("Name", 100, HorizontalAlignment.Left)
        lvDetail.Columns.Add("Date Modify", 150, HorizontalAlignment.Left)
        lvDetail.Columns.Add("Type", 150, HorizontalAlignment.Left)
        lvDetail.Columns.Add("Size", 100, HorizontalAlignment.Left)
    End Sub

    Private Sub PopulateData(ByVal dir As String)
        lvDetail.Items.Clear()
        Dim folder As String = String.Empty
        Try

            If dir.EndsWith("\") = False Then dir &= "\"
            Dim folders() As String = IO.Directory.GetDirectories(dir)
            If folders.Length <> 0 Then
                For Each folder In folders
                    Dim fldInfo As New System.IO.DirectoryInfo(folder)
                    Dim fldName As String = fldInfo.Name

                    Dim str(5) As String
                    Dim itm As ListViewItem
                    str(0) = fldName
                    str(1) = IO.File.GetLastWriteTime(folder)
                    str(2) = "File Folder"
                    str(3) = ""
                    itm = New ListViewItem(str)
                    itm.ImageIndex = 0
                    lvDetail.Items.Add(itm)


                Next

            End If

            Dim di As New IO.DirectoryInfo(dir)
            Dim AllfilInfo As IO.FileInfo() = di.GetFiles()
            For Each file In AllfilInfo
                Dim str(5) As String
                Dim itm As ListViewItem
                str(0) = file.Name
                str(1) = IO.File.GetLastWriteTime(file.DirectoryName)
                str(2) = file.Extension
                str(3) = (file.Length / (1024 ^ 3)).ToString("#,##0.00") & " GB"
                itm = New ListViewItem(str)
                lvDetail.Items.Add(itm)
            Next
        Catch ex As UnauthorizedAccessException
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub lvDetail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvDetail.Click
        Try
            Dim data As String = ""
            If lvDetail.SelectedItems.Count > 0 Then
                If lvDetail.SelectedItems(0).SubItems(2).Text = "File Folder" Then
                  
                Else
                    txtFileName.Text = lvDetail.SelectedItems(0).SubItems(0).Text
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub lvDetail_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvDetail.DoubleClick
        Try
            Dim data As String = ""
            If lvDetail.SelectedItems.Count > 0 Then
                If lvDetail.SelectedItems(0).SubItems(2).Text = "File Folder" Then
                    Dim directoryInfo As New IO.DirectoryInfo(txtPath.Text)
                    If txtPath.Text = directoryInfo.Root.Name Then
                        txtPath.Text &= lvDetail.SelectedItems(0).SubItems(0).Text
                    Else
                        txtPath.Text &= "\" & lvDetail.SelectedItems(0).SubItems(0).Text
                    End If

                    PopulateData(txtPath.Text)
                    txtFileName.Text = ""
                Else
                    txtFileName.Text = lvDetail.SelectedItems(0).SubItems(0).Text
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try

    End Sub

   
    Private Sub cbDrive_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbDrive.SelectedIndexChanged
        Try
            txtFileName.Text = ""
            PopulateData(cbDrive.SelectedValue & ":\")
            txtPath.Text = cbDrive.SelectedValue & ":\"
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try

    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try

            Dim directoryInfo As New IO.DirectoryInfo(txtPath.Text)
            If txtPath.Text = directoryInfo.Root.Name Then
                Exit Sub
            End If
            txtFileName.Text = ""
            txtPath.Text = IO.Directory.GetParent(txtPath.Text).ToString
            PopulateData(txtPath.Text)
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private _FileSelect As String = ""
    Property FileSelect() As String
        Get
            Dim path As String = txtPath.Text
            If path.EndsWith("\") = False Then path &= "\"

            Return path & txtFileName.Text
        End Get
        Set(ByVal value As String)
            _FileSelect = value
        End Set
    End Property

    Private Sub btnSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelect.Click
        If txtFileName.Text = "" Then
            Exit Sub
        Else
            Me.DialogResult = System.Windows.Forms.DialogResult.OK
            Me.Close()
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
End Class