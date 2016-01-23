Namespace Info
    Public Class CPUInfoENG
        Dim _PercentUsage As Double = 0

        Public ReadOnly Property PercentUsage() As Double
            Get
                Return _PercentUsage
            End Get
        End Property

        Public Function GetCPUInfoXML() As XElement
            Dim CPUInfo As New XElement("CPUInfo")
            CPUInfo.Add(New XElement("PercentUsage", _PercentUsage))
            Return CPUInfo
        End Function

        Public Sub New()
            Dim moReturn As Management.ManagementObjectCollection
            Dim moSearch As Management.ManagementObjectSearcher
            Dim mo As Management.ManagementObject
            moSearch = New Management.ManagementObjectSearcher("Select * from Win32_Processor")
            moReturn = moSearch.Get
            For Each mo In moReturn
                _PercentUsage += Convert.ToDouble(mo("LoadPercentage"))
            Next
            moSearch.Dispose()
            moReturn.Dispose()
        End Sub
    End Class
End Namespace

