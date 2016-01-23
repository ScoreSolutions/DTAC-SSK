Namespace Info
    Public Class WindowsServiceInfoENG
        Dim _ServiceName As String = ""
        Dim _ServiceType As String = ""
        Dim _ServiceStatus As String = ""
        Dim _ServiceCtl As System.ServiceProcess.ServiceController

        Public ReadOnly Property ServiceName() As String
            Get
                Return _ServiceName.Trim
            End Get
        End Property
        Public ReadOnly Property ServiceType() As String
            Get
                Return _ServiceType.Trim
            End Get
        End Property
        Public ReadOnly Property ServiceStatus() As String
            Get
                Return _ServiceStatus.Trim
            End Get
        End Property
        Public Sub StartService()
            _ServiceCtl.Start()
            _ServiceCtl.WaitForStatus(ServiceProcess.ServiceControllerStatus.Running, TimeSpan.FromMilliseconds(20000))
        End Sub

        Public Sub New(ByVal ServiceName As String)
            'Dim desc As String = "Windows Service Info :" & vbNewLine
            Try
                For Each s As System.ServiceProcess.ServiceController In System.ServiceProcess.ServiceController.GetServices
                    If s.ServiceName = ServiceName Then
                        _ServiceName = ServiceName
                        _ServiceStatus = s.Status
                        _ServiceType = s.ServiceType
                        _ServiceCtl = s
                    End If
                Next
            Catch ex As Exception
                CreateLogFile(ex.Message & vbCrLf & ex.StackTrace)
            End Try
            
        End Sub

        Public Shared Sub CreateLogFile(ByVal TextMsg As String)
            'Dim FILE_NAME As String = System.Windows.Forms.Application.StartupPath & "\" & DateTime.Now.ToString("yyyyyMMddHH") & ".sql"
            Dim FILE_NAME As String = System.Windows.Forms.Application.StartupPath & "\WindowsServiceInfoENG_" & DateTime.Now.ToString("yyyyyMMddHH") & ".txt"
            Dim objWriter As New System.IO.StreamWriter(FILE_NAME, True)
            objWriter.WriteLine(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff") & " " & TextMsg & Chr(13) & Chr(13))
            objWriter.Close()
        End Sub

        Public Shared Sub SetServiceMonitorInterval(ByVal IntervalMinute As Integer, ByVal ServiceName As String, ByVal FilePath As String)
            Dim xml As New XDocument
            xml.Add(New XElement("ServiceName", ServiceName))
            xml.Add(New XElement("IntervalMinute", IntervalMinute))
            xml.Add(New XElement("LastMonitorTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", New Globalization.CultureInfo("en-US"))))
            xml.Save(FilePath)
        End Sub
    End Class
End Namespace
