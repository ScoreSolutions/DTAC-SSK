Imports CenLinqDB.Common.Utilities
Namespace Common
    Public Class IniConfigEng
        Private ReadOnly Property INIFileName() As IniReader
            Get
                'Application.StartupPath = C:\Program Files\Common Files\Microsoft Shared\DevServer\9.0
                Dim INIFlie As String = "C:\Windows\QueueSharp.ini"
                Dim ini As New IniReader(INIFlie)
                ini.Section = "ServerConfig"
                Return ini
            End Get
        End Property

        Public Sub SetServerConfig(ByVal key As String, ByVal value As String)
            Dim ini As IniReader = INIFileName
            ini.Write("ServerConfig", key, value)
        End Sub

        Public Function GetServerConfig(ByVal key As String) As String
            Dim ini As IniReader = INIFileName
            Return ini.ReadString(key)
        End Function

    End Class
End Namespace

