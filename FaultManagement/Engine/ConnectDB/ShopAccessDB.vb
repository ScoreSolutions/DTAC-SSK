Imports System.Windows.Forms
Imports System.Data.OleDb

Namespace ConnectDB
    Public Class ShopAccessDB
        Shared conn As OleDbConnection
        Shared connString As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & Application.StartupPath & "\FaultMngDB.mdb;"

        Public Shared Function ExecuteNonQuery(ByVal sql As String) As Integer
            Dim ret As Integer = 0
            Try
                conn = New OleDbConnection(connString)
                conn.Open()

                Dim cmd As New OleDbCommand(sql, conn)
                cmd.CommandType = CommandType.Text
                cmd.CommandTimeout = 240
                ret = cmd.ExecuteNonQuery
            Catch ex As Exception
            Finally
                conn.Close()
            End Try
            Return ret
        End Function

        Public Shared Function GetDataTable(ByVal sql As String) As DataTable
            Dim ret As New DataTable
            Try
                conn = New OleDbConnection(connString)
                conn.Open()
                Dim da As New OleDbDataAdapter(sql, conn)
                da.Fill(ret)
                da.Dispose()
                conn.Close()
            Catch ex As Exception

            End Try
            Return ret
        End Function
    End Class
End Namespace