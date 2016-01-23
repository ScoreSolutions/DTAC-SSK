Imports System.Xml
Namespace Info
    Public Class MonitorComputerInfoENG
        Public Function MonitorComputerInfo() As XDocument
            Dim ret As New XDocument()
            Dim ParentNode As New XElement("MonitorComputerInfo")
            ParentNode.Add(New XElement("ComName", Environment.MachineName))
            ParentNode.Add(New XElement("IPAddress", Engine.Common.FunctionEng.GetIPAddress))
            ParentNode.Add(New XElement("ShopName", Engine.Common.FunctionEng.GetShopConfig("s_name_en")))

            'CPU PercentUsage
            Dim cEng As New CPUInfoENG
            Dim CPUInfo As New XElement("CPUInfo")
            CPUInfo.Add(New XElement("PercentUsage", cEng.PercentUsage))
            ParentNode.Add(CPUInfo)
            cEng = Nothing

            'Ram Info
            Dim rEng As New RamInfoENG
            Dim RamInfo As New XElement("RamInfo")
            RamInfo.Add(New XElement("AvailablePhysicalMemoryGB", rEng.AvailablePhysicalMemoryGB))
            RamInfo.Add(New XElement("TotalPhysicalMemoryGB", rEng.TotalPhysicalMemoryGB))
            ParentNode.Add(RamInfo)
            rEng = Nothing

            'Drive Info
            Dim dEng As New DriveInfoENG
            ParentNode.Add(dEng.GetDriveInfoXML)
            dEng = Nothing

            ParentNode.Add(New XElement("CurrentTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", New Globalization.CultureInfo("en-US"))))
            ret.Add(ParentNode)
            'ret.Save("D:\MonitorComputerInfo.xml")
            Return ret
        End Function

        Public Sub M()
            Try
                
            Catch ex As Exception

            End Try
            
        End Sub
    End Class
End Namespace

