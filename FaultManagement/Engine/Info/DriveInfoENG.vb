Namespace Info
    Public Class DriveInfoENG
        Dim _DriveName As String = ""
        Dim _VolumnLabel As String = ""
        Dim _FreeSpaceGB As Long = 0
        Dim _TotalSizeGB As Long = 0
        Public ReadOnly Property DriveName() As String
            Get
                Return _DriveName.Trim
            End Get
        End Property
        Public ReadOnly Property VolumnLabel() As String
            Get
                Return _VolumnLabel.Trim
            End Get
        End Property
        Public ReadOnly Property FreeSpaceGB() As Long
            Get
                Return _FreeSpaceGB
            End Get
        End Property
        Public ReadOnly Property TotalSizeGB() As Long
            Get
                Return _TotalSizeGB
            End Get
        End Property

        Public Sub GetDriveInfoByDriveLetter(ByVal DriveLetter As String)
            Dim drives As System.IO.DriveInfo() = System.IO.DriveInfo.GetDrives
            For Each dri As System.IO.DriveInfo In drives
                If dri.IsReady = True Then
                    If dri.Name.StartsWith(DriveLetter) = True Then
                        _DriveName = dri.Name
                        _VolumnLabel = dri.VolumeLabel
                        _FreeSpaceGB = dri.TotalFreeSpace / (1024 ^ 3)
                        _TotalSizeGB = dri.TotalSize / (1027 ^ 3)
                    End If
                End If
            Next
        End Sub

        Public Function GetDriveInfoXML() As XElement
            Dim DriveInfo As New XElement("DriveInfo")
            Dim drives As System.IO.DriveInfo() = System.IO.DriveInfo.GetDrives
            If drives.Length > 0 Then
                For Each dri As System.IO.DriveInfo In drives
                    If dri.IsReady = True Then
                        Dim DriveLetter As New XElement(Left(dri.Name, 1))
                        DriveLetter.Add(New XElement("VolumnLabel", dri.VolumeLabel))
                        DriveLetter.Add(New XElement("FreeSpaceGB", Math.Round(dri.TotalFreeSpace / (1024 ^ 3), 2)))
                        DriveLetter.Add(New XElement("TotalSizeGB", Math.Round(dri.TotalSize / (1024 ^ 3), 2)))
                        DriveLetter.Add(New XElement("PercentUsage", Math.Round(((dri.TotalSize - dri.TotalFreeSpace) * 100) / dri.TotalSize, 2)))
                        DriveInfo.Add(DriveLetter)
                    End If
                Next
            End If

            Return DriveInfo
        End Function

        Public Function GetDriveInfoToDT() As DataTable
            Dim DriveInfo As New DataTable
            DriveInfo.Columns.Add("DriveLetter")
            DriveInfo.Columns.Add("VolumnLabel")
            DriveInfo.Columns.Add("FreeSpaceGB", GetType(Double))
            DriveInfo.Columns.Add("TotalSizeGB", GetType(Double))
            DriveInfo.Columns.Add("PercentUsage", GetType(Double))

            Dim drives As System.IO.DriveInfo() = System.IO.DriveInfo.GetDrives
            If drives.Length > 0 Then
                For Each dri As System.IO.DriveInfo In drives
                    If dri.IsReady = True Then
                        Dim dr As DataRow = DriveInfo.NewRow
                        dr("DriveLetter") = Left(dri.Name, 1)
                        dr("VolumnLabel") = dri.VolumeLabel
                        dr("FreeSpaceGB") = Math.Round(dri.TotalFreeSpace / (1024 ^ 3), 2)
                        dr("TotalSizeGB") = Math.Round(dri.TotalSize / (1024 ^ 3), 2)
                        dr("PercentUsage") = Math.Round(((dri.TotalSize - dri.TotalFreeSpace) * 100) / dri.TotalSize, 2)
                        DriveInfo.Rows.Add(dr)
                    End If
                Next
            End If

            Return DriveInfo
        End Function
    End Class
End Namespace

