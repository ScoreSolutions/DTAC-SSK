Imports System
Imports System.Collections.Generic
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Text
Imports System.Windows.Forms

Namespace ConnectDB
    Public Class SqlDB
        Protected Shared ErrorConnectionString As String = MessageResources.MSGEC001
        Protected Shared ErrorConnection As String = MessageResources.MSGEC002
        Protected Shared ErrorSetCommandConnection As String = MessageResources.MSGEC003
        Protected Shared ErrorInvalidCommandType As String = MessageResources.MSGEC004
        Protected Shared ErrorDuplicateParameter As String = MessageResources.MSGEC006
        Protected Shared ErrorNullParameter As String = MessageResources.MSGEC005
        Protected Shared ErrorExecuteNonQuery As String = MessageResources.MSGEC010
        Protected Shared ErrorExecuteReader As String = MessageResources.MSGEC011
        Protected Shared ErrorExecuteTable As String = MessageResources.MSGEC012
        Protected Shared ErrorExecuteScalar As String = MessageResources.MSGEC013
        Protected Shared ErrorDatabaseOther As String = MessageResources.MSGEC901
        Protected Shared ErrorUndefined As String = MessageResources.MSGEC902

        Private Shared _err As String
        Public Shared ReadOnly Property ErrorMessage()
            Get
                Return _err
            End Get
        End Property

        Public Shared Function SetDouble(ByVal number As System.Nullable(Of Double)) As String
            Dim ret As String
            If Convert.IsDBNull(number) Then
                ret = "NULL"
            ElseIf number Is Nothing Then
                ret = "NULL"
            Else
                ret = number.ToString()
            End If
            Return ret
        End Function
        Public Shared Function SetDecimal(ByVal number As System.Nullable(Of Decimal)) As String
            Dim ret As String
            If Convert.IsDBNull(number) Then
                ret = "NULL"
            ElseIf number Is Nothing Then
                ret = "NULL"
            Else
                ret = number.ToString()
            End If
            Return ret
        End Function
        Public Shared Function SetString(ByVal str As String) As String
            Dim ret As String = ""
            If str Is Nothing Or str.Trim() = "" Then
                ret = "NULL"
            Else
                ret = Chr(39) & str.Trim().Replace("'", "''") & Chr(39)
            End If
            Return ret
        End Function
        Public Shared Function SetDate() As String
            Return SetDateTime(DateTime.Today)
        End Function
        Public Function SetDate(ByVal DateIn As DateTime) As String
            Return SetDateTime(DateIn)
        End Function
        Public Shared Function SetDateTime() As String
            Return SetDateTime(DateTime.Today)
        End Function
        Public Shared Function SetDateTime(ByVal DateIn As DateTime) As String
            Dim ret As String = ""
            If DateIn.Year = 1 Or Convert.IsDBNull(DateIn) Then
                ret = "NULL"
                'ElseIf DateIn.Year > 2500 Then
                '    Dim vYear As String = DateIn.Year - 543
                '    ret = "'" & vYear & "-" & DateIn.ToString("MM-dd HH:mm:ss") & "'"
            Else
                'ret = "'" & DateIn.Year & "-" & DateIn.ToString("MM-dd HH:mm:ss") & "'"
                ret = "'" & DateIn.ToString("yyyy-MM-dd HH:mm:ss.fff", New Globalization.CultureInfo("en-US")) & "'"
            End If
            Return ret
        End Function

        Public Shared Function GetExceptionMessage(ByVal ex As SqlException) As String
            Return String.Format(ErrorDatabaseOther, ex.ErrorCode.ToString(), ex.Message)
        End Function

        Private Shared ReadOnly Property INIFileName() As IniReader
            Get
                'Application.StartupPath = C:\Program Files\Common Files\Microsoft Shared\DevServer\9.0
                Dim INIFlie As String = "C:\Windows\DTAC-SSK.ini"
                Dim ini As New IniReader(INIFlie)
                ini.Section = "DbSetting"
                Return ini
            End Get
        End Property

        Public Shared Function GetStartupPath() As String
            Return Application.StartupPath
        End Function

        Public Shared ReadOnly Property ServerName() As String
            Get

                Dim ini As IniReader = INIFileName
                Return ini.ReadString("ServerName")
            End Get
        End Property
        Public Shared ReadOnly Property DatabaseName() As String
            Get
                Dim ini As IniReader = INIFileName
                Return ini.ReadString("DatabaseName")
            End Get
        End Property

        Public Shared ReadOnly Property UserID() As String
            Get
                Dim ini As IniReader = INIFileName
                Return ini.ReadString("UserID")
            End Get
        End Property

        Public Shared ReadOnly Property DbPwd() As String
            Get
                Dim ini As IniReader = INIFileName
                Return DeCripPwd(ini.ReadString("Password"))
            End Get
        End Property

        Protected Shared ReadOnly Property GetConnectionString() As String
            Get
                Try
                    Return "Data Source=" & ServerName() & ";Initial Catalog=" & DatabaseName() & ";User ID=" & UserID() & ";Password=" & DbPwd()
                Catch ex As Exception
                    Throw New ApplicationException(ErrorConnectionString, ex)
                End Try
            End Get
        End Property


        Public Shared Function GetConnection() As SqlConnection
            Dim conn As SqlConnection
            Try
                conn = New SqlConnection(GetConnectionString)
                conn.Open()
                Return conn
            Catch ex As ApplicationException
                'Throw ex
                _err = ex.Message
            Catch ex As SqlException
                Try
                    conn = New SqlConnection(GetConnectionString)
                    conn.Open()
                    Return conn
                Catch ex1 As SqlException
                    _err = ex1.Message
                End Try
            Catch ex As Exception
                _err = ex.Message
            End Try
        End Function

        Public Shared Function GetConnection(ByVal connString As String) As SqlConnection
            Dim conn As SqlConnection
            Try
                conn = New SqlConnection(connString)
                conn.Open()
                Return conn
            Catch ex As ApplicationException
                _err = ex.Message
            Catch ex As SqlException
                _err = ex.Message
            Catch ex As Exception
                _err = ex.Message
            End Try
        End Function


        Public Shared Function ChkConnection() As Boolean
            Dim ret As Boolean = False
            Dim conn As SqlConnection
            Try
                conn = New SqlConnection(GetConnectionString)
                conn.Open()

                ret = True
            Catch ex As ApplicationException
                _err = ex.Message
                ret = False
            Catch ex As SqlException
                Try
                    conn = New SqlConnection(GetConnectionString)
                    conn.Open()
                    ret = True
                Catch ex1 As SqlException
                    _err = ex1.Message
                    ret = False
                End Try
            Catch ex As Exception
                _err = ex.Message
                ret = False
            End Try

            Return ret
        End Function
        Public Shared Function GetNextID(ByVal fldName As String, ByVal tbName As String, ByVal trans As SqlTransaction) As Long
            Dim ret As Long
            Dim Sql As String = "select max(" & fldName & ") maxID from " & tbName
            Dim dt As DataTable = ExecuteTable(Sql, trans)
            If dt.Rows.Count > 0 Then
                If Convert.IsDBNull(dt.Rows(0)("maxID")) = False Then
                    ret = Convert.ToInt64(dt.Rows(0)("maxID").ToString()) + 1
                Else
                    ret = 1
                End If
            Else
                ret = 1
            End If
            Return ret
        End Function

        Public Shared Function GetLastID(ByVal tbName As String, ByVal trans As SqlTransaction) As Long
            Dim ret As Long
            Dim Sql As String = "select IDENT_CURRENT('" & tbName & "') lastID "
            Dim dt As DataTable = ExecuteTable(Sql, trans)
            If Convert.IsDBNull(dt.Rows(0)("lastID")) = False Then
                ret = Convert.ToInt64(dt.Rows(0)("lastID").ToString())
            Else
                ret = 1
            End If

            Return ret
        End Function

        Public Shared Function GetDateNow(ByVal rtType As String, ByVal trans As SqlTransaction) As String
            'rtType = D  ให้คืนค่าเป็นวันที่ในรูปแบบ yyyy-MM-dd
            'rtType = T  ให้คืนค่าเป็นเวลาปัจจุบัน hh:mm
            Dim ret As String
            Dim dt As DataTable = ExecuteTable("select convert(varchar(10),getdate(),120) date_now, CONVERT(varchar(5),getdate(),108) time_now", trans)
            If rtType = "D" Then
                ret = dt.Rows(0)("date_now")
            ElseIf rtType = "T" Then
                ret = dt.Rows(0)("time_now")
            Else
                ret = dt.Rows(0)("date_now") & " " & dt.Rows(0)("time_now")
            End If

            Return ret
        End Function

        Public Shared Function GetDateNowFromDB(ByVal trans As SqlTransaction) As Date
            Dim ret As String
            Dim dt As DataTable = ExecuteTable("select getdate() date_now", trans)
            ret = dt.Rows(0)("date_now")
            Return ret
        End Function

        Public Shared Function ExecuteTable(ByVal sql As String) As DataTable
            Return ExecuteTable(sql, Nothing, Nothing)
        End Function
        Public Shared Function ExecuteTable(ByVal sql As String, ByVal conn As SqlConnection) As DataTable
            Return ExecuteTable(sql, conn, Nothing)
        End Function
        Public Shared Function ExecuteTable(ByVal sql As String, ByVal trans As SqlTransaction) As DataTable
            Return ExecuteTable(sql, Nothing, trans)
        End Function
        Public Shared Function ExecuteTable(ByVal sql As String, ByVal conn As SqlConnection, ByVal trans As SqlTransaction) As DataTable
            Dim cmd As New SqlCommand
            Dim adapter As New SqlDataAdapter
            adapter.SelectCommand = cmd
            Dim LetClose As Boolean = False
            Dim dt As New DataTable
            Try
                If trans IsNot Nothing And conn Is Nothing Then
                    conn = trans.Connection
                ElseIf conn Is Nothing Then
                    conn = GetConnection()
                    LetClose = True
                End If

                BuildCommand(cmd, conn, trans, CommandType.Text, sql, Nothing)
                adapter.Fill(dt)
                adapter.Dispose()
                If LetClose = True Then
                    cmd.Dispose()
                    conn.Close()
                End If
            Catch ex As ApplicationException
                _err = sql & " $$$$ " & ex.Message
                adapter.Dispose()
            Catch ex As SqlException
                _err = sql & " $$$$ " & ex.Message
                adapter.Dispose()
            Catch ex As Exception
                _err = sql & " $$$$ " & ex.Message
                adapter.Dispose()
            End Try

            Return dt
        End Function

        Public Shared Function ExecuteReader(ByVal Sql As String) As SqlDataReader
            Return ExecuteReader(Sql, Nothing, Nothing)
        End Function
        Public Shared Function ExecuteReader(ByVal Sql As String, ByVal trans As SqlTransaction) As SqlDataReader
            Return ExecuteReader(Sql, Nothing, trans)
        End Function

        Public Shared Function ExecuteReader(ByVal Sql As String, ByVal conn As SqlConnection) As SqlDataReader
            Return ExecuteReader(Sql, conn, Nothing)
        End Function

        Public Shared Function ExecuteReader(ByVal Sql As String, ByVal conn As SqlConnection, ByVal trans As SqlTransaction) As SqlDataReader
            Dim command As New SqlCommand()
            Dim reader As SqlDataReader
            Dim LetClose As Boolean = False

            Try
                If trans IsNot Nothing And conn Is Nothing Then
                    conn = trans.Connection
                ElseIf conn Is Nothing Then
                    conn = GetConnection()
                    LetClose = True
                End If

                BuildCommand(command, conn, trans, CommandType.Text, Sql, Nothing)
                If LetClose = True Then
                    reader = command.ExecuteReader(CommandBehavior.CloseConnection)
                Else
                    reader = command.ExecuteReader()
                End If
            Catch ex As ApplicationException
                _err = Sql & " $$$$ " & ex.Message
            Catch ex As SqlException
                _err = Sql & " $$$$ " & ex.Message
            Catch ex As Exception
                _err = Sql & " $$$$ " & ex.Message
            End Try

            Return reader
        End Function


        Public Shared Function ExecuteNonQuery(ByVal Sql As String) As Integer
            Return ExecuteNonQuery(Sql, Nothing, Nothing)
        End Function
        Public Shared Function ExecuteNonQuery(ByVal Sql As String, ByVal conn As SqlConnection) As Integer
            Return ExecuteNonQuery(Sql, conn, Nothing, Nothing)
        End Function
        Public Shared Function ExecuteNonQuery(ByVal Sql As String, ByVal trans As SqlTransaction) As Integer
            Return ExecuteNonQuery(Sql, Nothing, trans, Nothing)
        End Function
        Public Shared Function ExecuteNonQuery(ByVal Sql As String, ByVal trans As SqlTransaction, ByVal cmdParms() As SqlParameter) As Integer
            Return ExecuteNonQuery(Sql, Nothing, trans, cmdParms)
        End Function
        Public Shared Function ExecuteNonQuery(ByVal Sql As String, ByVal conn As SqlConnection, ByVal trans As SqlTransaction, ByVal cmdParms() As SqlParameter) As Integer
            Dim retval As Integer
            Dim command As New SqlCommand

            Try
                If trans Is Nothing Then
                    conn = GetConnection()
                    BuildCommand(command, conn, trans, CommandType.Text, Sql, cmdParms)
                    retval = command.ExecuteNonQuery
                Else
                    If trans IsNot Nothing And conn Is Nothing Then
                        conn = trans.Connection
                    End If

                    BuildCommand(command, trans.Connection, trans, CommandType.Text, Sql, cmdParms)
                    retval = command.ExecuteNonQuery
                End If
            Catch ex As ApplicationException
                _err = Sql & " $$$$ " & ex.Message
            Catch ex As SqlException
                _err = Sql & " $$$$ " & ex.Message
            Catch ex As Exception
                _err = Sql & " $$$$ " & ex.Message
            End Try

            Return retval
        End Function

        Private Shared Sub BuildCommand(ByVal cmd As SqlCommand, ByVal conn As SqlConnection, ByVal trans As SqlTransaction, ByVal cmdType As CommandType, ByVal cmdText As String, ByVal cmdParms() As SqlParameter)
            If conn.State <> ConnectionState.Open Then
                Try
                    conn.Open()
                Catch ex As SqlException
                    _err = ex.Message
                Catch ex As ApplicationException
                    _err = ex.Message
                Catch ex As Exception
                    _err = ex.Message
                End Try
            End If

            Try
                cmd.Connection = conn
            Catch ex As Exception
                _err = ex.Message
            End Try
            cmd.CommandText = cmdText

            If trans IsNot Nothing Then
                cmd.Transaction = trans
            End If

            Try
                cmd.CommandType = cmdType
                cmd.CommandTimeout = 240
            Catch ex As ArgumentException
                _err = ex.Message
            End Try

            If cmdParms IsNot Nothing Then
                For Each parm As SqlParameter In cmdParms
                    Try
                        cmd.Parameters.Add(parm)
                    Catch ex As ArgumentNullException
                        _err = ex.Message
                    Catch ex As ArgumentException
                        _err = ex.Message
                    End Try
                Next
            End If
        End Sub

#Region " Encrypt/Decrypt "
        Private Shared EncryptionKey As String = "scoresolutions12"
        Public Shared Function EnCripPwd(ByVal passString As String) As String
            Dim AES As New System.Security.Cryptography.RijndaelManaged
            Dim Hash_AES As New System.Security.Cryptography.MD5CryptoServiceProvider
            Dim encrypted As String = ""
            Try
                Dim hash(31) As Byte
                Dim temp As Byte() = Hash_AES.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(EncryptionKey))
                Array.Copy(temp, 0, hash, 0, 16)
                Array.Copy(temp, 0, hash, 15, 16)
                AES.Key = hash
                AES.Mode = Security.Cryptography.CipherMode.ECB
                Dim DESEncrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateEncryptor
                Dim Buffer As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(passString)
                encrypted = Convert.ToBase64String(DESEncrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))

            Catch ex As Exception
            End Try

            Return encrypted
        End Function

        Public Shared Function DeCripPwd(ByVal passString As String) As String
            Dim AES As New System.Security.Cryptography.RijndaelManaged
            Dim Hash_AES As New System.Security.Cryptography.MD5CryptoServiceProvider
            Dim decrypted As String = ""
            Try
                Dim hash(31) As Byte
                Dim temp As Byte() = Hash_AES.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(EncryptionKey))
                Array.Copy(temp, 0, hash, 0, 16)
                Array.Copy(temp, 0, hash, 15, 16)
                AES.Key = hash
                AES.Mode = Security.Cryptography.CipherMode.ECB
                Dim DESDecrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateDecryptor
                Dim Buffer As Byte() = Convert.FromBase64String(passString)
                decrypted = System.Text.ASCIIEncoding.ASCII.GetString(DESDecrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))
            Catch ex As Exception
            End Try

            Return decrypted
        End Function
#End Region

    End Class


End Namespace

