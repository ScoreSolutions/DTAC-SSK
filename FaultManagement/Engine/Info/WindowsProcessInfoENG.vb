Namespace Info
    Public Class WindowsProcessInfoENG

        Public Sub New(ByVal ProcessName As String)
            Dim p As Process() = Process.GetProcessesByName(ProcessName)
            Dim a As Process = p(0)
            Dim str As String = a.ProcessName

        End Sub
    End Class
End Namespace

