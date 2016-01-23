Namespace Info
    Public Class RamInfoENG
        Dim _AvailablePhysicalMemoryGB As Double = 0
        Dim _TotalPhysicalMemoryGB As Double = 0
        Dim _PercentUsageGB As Double = 0

        Public ReadOnly Property AvailablePhysicalMemoryGB() As Double
            Get
                Return _AvailablePhysicalMemoryGB
            End Get
        End Property

        Public ReadOnly Property TotalPhysicalMemoryGB() As Double
            Get
                Return _TotalPhysicalMemoryGB
            End Get
        End Property

        Public ReadOnly Property PercentUsageGB() As Double
            Get
                Return _PercentUsageGB
            End Get
        End Property

        Public Function GetRAMInfoXML() As XElement
            Dim RAMInfo As New XElement("RAMInfo")
            RAMInfo.Add(New XElement("AvailablePhysicalMemoryGB", _AvailablePhysicalMemoryGB))
            RAMInfo.Add(New XElement("TotalPhysicalMemoryGB", _TotalPhysicalMemoryGB))
            RAMInfo.Add(New XElement("PercentUsageGB", _PercentUsageGB))
            Return RAMInfo
        End Function

        Public Sub New()
            Dim ComInfo As New Devices.ComputerInfo
            _AvailablePhysicalMemoryGB = Math.Round((ComInfo.AvailablePhysicalMemory / (1024 ^ 3)), 2)
            _TotalPhysicalMemoryGB = Math.Round((ComInfo.TotalPhysicalMemory / (1024 ^ 3)), 2)
            _PercentUsageGB = Math.Round(((ComInfo.TotalPhysicalMemory - ComInfo.AvailablePhysicalMemory) / ComInfo.TotalPhysicalMemory) * 100, 2)
        End Sub
    End Class
End Namespace

