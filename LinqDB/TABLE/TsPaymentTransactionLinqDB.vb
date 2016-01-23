Imports System
Imports System.Data 
Imports System.Data.SQLClient
Imports System.Data.Linq 
Imports System.Data.Linq.Mapping 
Imports System.Linq 
Imports System.Linq.Expressions 
Imports DB = LinqDB.ConnectDB.SQLDB
Imports LinqDB.ConnectDB

Namespace TABLE
    'Represents a transaction for TS_PAYMENT_TRANSACTION table LinqDB.
    '[Create by  on May, 9 2014]
    Public Class TsPaymentTransactionLinqDB
        Public Sub TsPaymentTransactionLinqDB()

        End Sub
        ' TS_PAYMENT_TRANSACTION
        Const _tableName As String = "TS_PAYMENT_TRANSACTION"
        Dim _deletedRow As Int16 = 0

        'Set Common Property
        Dim _error As String = ""
        Dim _information As String = ""
        Dim _haveData As Boolean = False

        Public ReadOnly Property TableName As String
            Get
                Return _tableName
            End Get
        End Property
        Public ReadOnly Property ErrorMessage As String
            Get
                Return _error
            End Get
        End Property
        Public ReadOnly Property InfoMessage As String
            Get
                Return _information
            End Get
        End Property
        Public ReadOnly Property HaveData As Boolean
            Get
                Return _haveData
            End Get
        End Property


        'Generate Field List
        Dim _ID As Long = 0
        Dim _CREATE_BY As String = ""
        Dim _CREATE_DATE As DateTime = New DateTime(1, 1, 1)
        Dim _UPDATE_BY As String = ""
        Dim _UPDATE_DATE As System.Nullable(Of DateTime) = New DateTime(1, 1, 1)
        Dim _TRANSACTION_NO As String = ""
        Dim _TRANSACTION_DATE As DateTime = New DateTime(1, 1, 1)
        Dim _ACCOUNT_NO As String = ""
        Dim _ACCOUNT_NAME As String = ""
        Dim _AMOUNT As Double = 0
        Dim _PAYMENT_TYPE As Char = ""
        Dim _CREDIT_CARD_NO As String = ""
        Dim _CUSTOMER_SIGN_BYTE() As Byte
        Dim _CUSTOMER_IMAGE_BYTE() As Byte
        Dim _PAYMENT_SLIP_BYTE() As Byte

        'Generate Field Property 
        <Column(Storage:="_ID", DbType:="BigInt NOT NULL ", CanBeNull:=False)> _
        Public Property ID() As Long
            Get
                Return _ID
            End Get
            Set(ByVal value As Long)
                _ID = value
            End Set
        End Property
        <Column(Storage:="_CREATE_BY", DbType:="VarChar(100) NOT NULL ", CanBeNull:=False)> _
        Public Property CREATE_BY() As String
            Get
                Return _CREATE_BY
            End Get
            Set(ByVal value As String)
                _CREATE_BY = value
            End Set
        End Property
        <Column(Storage:="_CREATE_DATE", DbType:="DateTime NOT NULL ", CanBeNull:=False)> _
        Public Property CREATE_DATE() As DateTime
            Get
                Return _CREATE_DATE
            End Get
            Set(ByVal value As DateTime)
                _CREATE_DATE = value
            End Set
        End Property
        <Column(Storage:="_UPDATE_BY", DbType:="VarChar(100)")> _
        Public Property UPDATE_BY() As String
            Get
                Return _UPDATE_BY
            End Get
            Set(ByVal value As String)
                _UPDATE_BY = value
            End Set
        End Property
        <Column(Storage:="_UPDATE_DATE", DbType:="DateTime")> _
        Public Property UPDATE_DATE() As System.Nullable(Of DateTime)
            Get
                Return _UPDATE_DATE
            End Get
            Set(ByVal value As System.Nullable(Of DateTime))
                _UPDATE_DATE = value
            End Set
        End Property
        <Column(Storage:="_TRANSACTION_NO", DbType:="VarChar(50) NOT NULL ", CanBeNull:=False)> _
        Public Property TRANSACTION_NO() As String
            Get
                Return _TRANSACTION_NO
            End Get
            Set(ByVal value As String)
                _TRANSACTION_NO = value
            End Set
        End Property
        <Column(Storage:="_TRANSACTION_DATE", DbType:="DateTime NOT NULL ", CanBeNull:=False)> _
        Public Property TRANSACTION_DATE() As DateTime
            Get
                Return _TRANSACTION_DATE
            End Get
            Set(ByVal value As DateTime)
                _TRANSACTION_DATE = value
            End Set
        End Property
        <Column(Storage:="_ACCOUNT_NO", DbType:="VarChar(100) NOT NULL ", CanBeNull:=False)> _
        Public Property ACCOUNT_NO() As String
            Get
                Return _ACCOUNT_NO
            End Get
            Set(ByVal value As String)
                _ACCOUNT_NO = value
            End Set
        End Property
        <Column(Storage:="_ACCOUNT_NAME", DbType:="VarChar(100) NOT NULL ", CanBeNull:=False)> _
        Public Property ACCOUNT_NAME() As String
            Get
                Return _ACCOUNT_NAME
            End Get
            Set(ByVal value As String)
                _ACCOUNT_NAME = value
            End Set
        End Property
        <Column(Storage:="_AMOUNT", DbType:="Double NOT NULL ", CanBeNull:=False)> _
        Public Property AMOUNT() As Double
            Get
                Return _AMOUNT
            End Get
            Set(ByVal value As Double)
                _AMOUNT = value
            End Set
        End Property
        <Column(Storage:="_PAYMENT_TYPE", DbType:="Char(1) NOT NULL ", CanBeNull:=False)> _
        Public Property PAYMENT_TYPE() As Char
            Get
                Return _PAYMENT_TYPE
            End Get
            Set(ByVal value As Char)
                _PAYMENT_TYPE = value
            End Set
        End Property
        <Column(Storage:="_CREDIT_CARD_NO", DbType:="VarChar(50)")> _
        Public Property CREDIT_CARD_NO() As String
            Get
                Return _CREDIT_CARD_NO
            End Get
            Set(ByVal value As String)
                _CREDIT_CARD_NO = value
            End Set
        End Property
        <Column(Storage:="_CUSTOMER_SIGN_BYTE", DbType:="IMAGE")> _
        Public Property CUSTOMER_SIGN_BYTE() As Byte()
            Get
                Return _CUSTOMER_SIGN_BYTE
            End Get
            Set(ByVal value As Byte())
                _CUSTOMER_SIGN_BYTE = value
            End Set
        End Property
        <Column(Storage:="_CUSTOMER_IMAGE_BYTE", DbType:="IMAGE")> _
        Public Property CUSTOMER_IMAGE_BYTE() As Byte()
            Get
                Return _CUSTOMER_IMAGE_BYTE
            End Get
            Set(ByVal value As Byte())
                _CUSTOMER_IMAGE_BYTE = value
            End Set
        End Property
        <Column(Storage:="_PAYMENT_SLIP_BYTE", DbType:="IMAGE")> _
        Public Property PAYMENT_SLIP_BYTE() As Byte()
            Get
                Return _PAYMENT_SLIP_BYTE
            End Get
            Set(ByVal value As Byte())
                _PAYMENT_SLIP_BYTE = value
            End Set
        End Property


        'Clear All Data
        Private Sub ClearData()
            _ID = 0
            _CREATE_BY = ""
            _CREATE_DATE = New DateTime(1, 1, 1)
            _UPDATE_BY = ""
            _UPDATE_DATE = New DateTime(1, 1, 1)
            _TRANSACTION_NO = ""
            _TRANSACTION_DATE = New DateTime(1, 1, 1)
            _ACCOUNT_NO = ""
            _ACCOUNT_NAME = ""
            _AMOUNT = 0
            _PAYMENT_TYPE = ""
            _CREDIT_CARD_NO = ""
            _CUSTOMER_SIGN_BYTE = Nothing
            _CUSTOMER_IMAGE_BYTE = Nothing
            _PAYMENT_SLIP_BYTE = Nothing
        End Sub

        'Define Public Method 
        'Execute the select statement with the specified condition and return a System.Data.DataTable.
        '/// <param name=whereClause>The condition for execute select statement.</param>
        '/// <param name=orderBy>The fields for sort data.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>The System.Data.DataTable object for specified condition.</returns>
        Public Function GetDataList(whClause As String, orderBy As String, trans As SqlTransaction) As DataTable
            Return DB.ExecuteTable(SqlSelect & IIf(whClause = "", "", " WHERE " & whClause) & IIF(orderBy = "", "", " ORDER BY  " & orderBy), trans)
        End Function


        'Execute the select statement with the specified condition and return a System.Data.DataTable.
        '/// <param name=whereClause>The condition for execute select statement.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>The System.Data.DataTable object for specified condition.</returns>
        Public Function GetListBySql(Sql As String, trans As SqlTransaction) As DataTable
            Return DB.ExecuteTable(Sql, trans)
        End Function


        '/// Returns an indication whether the current data is inserted into TS_PAYMENT_TRANSACTION table successfully.
        '/// <param name=userID>The current user.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if insert data successfully; otherwise, false.</returns>
        Public Function InsertData(LoginName As String, trans As SqlTransaction) As Boolean
            If trans IsNot Nothing Then
                _ID = DB.GetNextID("ID", tableName, trans)
                _CREATE_BY = LoginName
                _CREATE_DATE = DateTime.Now
                Return doInsert(trans)
            Else
                _error = "Transaction Is not null"
                Return False
            End If
        End Function


        '/// Returns an indication whether the current data is updated to TS_PAYMENT_TRANSACTION table successfully.
        '/// <param name=userID>The current user.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if update data successfully; otherwise, false.</returns>
        Public Function UpdateByPK(LoginName As String, trans As SqlTransaction) As Boolean
            If trans IsNot Nothing Then
                _UPDATE_BY = LoginName
                _UPDATE_DATE = DateTime.Now
                Return doUpdate("ID = " & DB.SetDouble(_ID), trans)
            Else
                _error = "Transaction Is not null"
                Return False
            End If
        End Function


        '/// Returns an indication whether the current data is updated to TS_PAYMENT_TRANSACTION table successfully.
        '/// <returns>true if update data successfully; otherwise, false.</returns>
        Public Function UpdateBySql(Sql As String, trans As SqlTransaction) As Boolean
            If trans IsNot Nothing Then
                Return DB.ExecuteNonQuery(Sql, trans)
            Else
                _error = "Transaction Is not null"
                Return False
            End If
        End Function


        '/// Returns an indication whether the current data is deleted from TS_PAYMENT_TRANSACTION table successfully.
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if delete data successfully; otherwise, false.</returns>
        Public Function DeleteByPK(cID As Long, trans As SqlTransaction) As Boolean
            If trans IsNot Nothing Then
                Return doDelete("ID = " & DB.SetDouble(cID), trans)
            Else
                _error = "Transaction Is not null"
                Return False
            End If
        End Function


        '/// Returns an indication whether the record of TS_PAYMENT_TRANSACTION by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByPK(cID As Long, trans As SqlTransaction) As Boolean
            Return doChkData("ID = " & DB.SetDouble(cID), trans)
        End Function


        '/// Returns an indication whether the record and Mapping field to Data Class of TS_PAYMENT_TRANSACTION by specified id key is retrieved successfully.
        '/// <param name=cid>The id key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function GetDataByPK(cID As Long, trans As SqlTransaction) As TsPaymentTransactionLinqDB
            Return doGetData("ID = " & DB.SetDouble(cID), trans)
        End Function




        '/// Returns an indication whether the record of TS_PAYMENT_TRANSACTION by specified TRANSACTION_NO key is retrieved successfully.
        '/// <param name=cTRANSACTION_NO>The TRANSACTION_NO key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByTRANSACTION_NO(cTRANSACTION_NO As String, trans As SqlTransaction) As Boolean
            Return doChkData("TRANSACTION_NO = " & DB.SetString(cTRANSACTION_NO) & " ", trans)
        End Function

        '/// Returns an duplicate data record of TS_PAYMENT_TRANSACTION by specified TRANSACTION_NO key is retrieved successfully.
        '/// <param name=cTRANSACTION_NO>The TRANSACTION_NO key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDuplicateByTRANSACTION_NO(cTRANSACTION_NO As String, cid As Long, trans As SqlTransaction) As Boolean
            Return doChkData("TRANSACTION_NO = " & DB.SetString(cTRANSACTION_NO) & " " & " And id <> " & DB.SetDouble(cid) & " ", trans)
        End Function


        '/// Returns an indication whether the record of TS_PAYMENT_TRANSACTION by specified TRANSACTION_DATE key is retrieved successfully.
        '/// <param name=cTRANSACTION_DATE>The TRANSACTION_DATE key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByTRANSACTION_DATE(cTRANSACTION_DATE As DateTime, trans As SqlTransaction) As Boolean
            Return doChkData("TRANSACTION_DATE = " & DB.SetDateTime(cTRANSACTION_DATE) & " ", trans)
        End Function

        '/// Returns an duplicate data record of TS_PAYMENT_TRANSACTION by specified TRANSACTION_DATE key is retrieved successfully.
        '/// <param name=cTRANSACTION_DATE>The TRANSACTION_DATE key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDuplicateByTRANSACTION_DATE(cTRANSACTION_DATE As DateTime, cid As Long, trans As SqlTransaction) As Boolean
            Return doChkData("TRANSACTION_DATE = " & DB.SetDateTime(cTRANSACTION_DATE) & " " & " And id <> " & DB.SetDouble(cid) & " ", trans)
        End Function


        '/// Returns an indication whether the record of TS_PAYMENT_TRANSACTION by specified ACCOUNT_NO key is retrieved successfully.
        '/// <param name=cACCOUNT_NO>The ACCOUNT_NO key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByACCOUNT_NO(cACCOUNT_NO As String, trans As SqlTransaction) As Boolean
            Return doChkData("ACCOUNT_NO = " & DB.SetString(cACCOUNT_NO) & " ", trans)
        End Function

        '/// Returns an duplicate data record of TS_PAYMENT_TRANSACTION by specified ACCOUNT_NO key is retrieved successfully.
        '/// <param name=cACCOUNT_NO>The ACCOUNT_NO key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDuplicateByACCOUNT_NO(cACCOUNT_NO As String, cid As Long, trans As SqlTransaction) As Boolean
            Return doChkData("ACCOUNT_NO = " & DB.SetString(cACCOUNT_NO) & " " & " And id <> " & DB.SetDouble(cid) & " ", trans)
        End Function


        '/// Returns an indication whether the record of TS_PAYMENT_TRANSACTION by specified ACCOUNT_NAME key is retrieved successfully.
        '/// <param name=cACCOUNT_NAME>The ACCOUNT_NAME key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByACCOUNT_NAME(cACCOUNT_NAME As String, trans As SqlTransaction) As Boolean
            Return doChkData("ACCOUNT_NAME = " & DB.SetString(cACCOUNT_NAME) & " ", trans)
        End Function

        '/// Returns an duplicate data record of TS_PAYMENT_TRANSACTION by specified ACCOUNT_NAME key is retrieved successfully.
        '/// <param name=cACCOUNT_NAME>The ACCOUNT_NAME key.</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDuplicateByACCOUNT_NAME(cACCOUNT_NAME As String, cid As Long, trans As SqlTransaction) As Boolean
            Return doChkData("ACCOUNT_NAME = " & DB.SetString(cACCOUNT_NAME) & " " & " And id <> " & DB.SetDouble(cid) & " ", trans)
        End Function


        '/// Returns an indication whether the record of TS_PAYMENT_TRANSACTION by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Public Function ChkDataByWhere(whText As String, trans As SqlTransaction) As Boolean
            Return doChkData(whText, trans)
        End Function



        '/// Returns an indication whether the current data is inserted into TS_PAYMENT_TRANSACTION table successfully.
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if insert data successfully; otherwise, false.</returns>
        Private Function doInsert(trans As SqlTransaction) As Boolean
            Dim ret As Boolean = True
            If _haveData = False Then
                Try

                    Dim cmbParam(2) As SqlParameter
                    If _CUSTOMER_SIGN_BYTE IsNot Nothing Then
                        cmbParam(0) = New SqlParameter("@_CUSTOMER_SIGN_BYTE", SqlDbType.Image, _CUSTOMER_SIGN_BYTE.Length)
                        cmbParam(0).Value = _CUSTOMER_SIGN_BYTE
                    Else
                        cmbParam(0) = New SqlParameter("@_CUSTOMER_SIGN_BYTE", SqlDbType.Image)
                        cmbParam(0).Value = DBNull.value
                    End If
                    If _CUSTOMER_IMAGE_BYTE IsNot Nothing Then
                        cmbParam(1) = New SqlParameter("@_CUSTOMER_IMAGE_BYTE", SqlDbType.Image, _CUSTOMER_IMAGE_BYTE.Length)
                        cmbParam(1).Value = _CUSTOMER_IMAGE_BYTE
                    Else
                        cmbParam(1) = New SqlParameter("@_CUSTOMER_IMAGE_BYTE", SqlDbType.Image)
                        cmbParam(1).Value = DBNull.value
                    End If
                    If _PAYMENT_SLIP_BYTE IsNot Nothing Then
                        cmbParam(2) = New SqlParameter("@_PAYMENT_SLIP_BYTE", SqlDbType.Image, _PAYMENT_SLIP_BYTE.Length)
                        cmbParam(2).Value = _PAYMENT_SLIP_BYTE
                    Else
                        cmbParam(2) = New SqlParameter("@_PAYMENT_SLIP_BYTE", SqlDbType.Image)
                        cmbParam(2).Value = DBNull.value
                    End If
                    ret = (DB.ExecuteNonQuery(SqlInsert, trans, cmbParam) > 0)
                    If ret = False Then
                        _error = DB.ErrorMessage
                    Else
                        _haveData = True
                    End If
                    _information = MessageResources.MSGIN001
                Catch ex As ApplicationException
                    ret = False
                    _error = ex.Message & "ApplicationException :" & ex.ToString() & "### SQL:" & SqlInsert
                Catch ex As Exception
                    ret = False
                    _error = MessageResources.MSGEC101 & " Exception :" & ex.ToString() & "### SQL: " & SqlInsert
                End Try
            Else
                ret = False
                _error = MessageResources.MSGEN002 & "### SQL: " & SqlInsert
            End If

            Return ret
        End Function


        '/// Returns an indication whether the current data is updated to TS_PAYMENT_TRANSACTION table successfully.
        '/// <param name=whText>The condition specify the updating record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if update data successfully; otherwise, false.</returns>
        Private Function doUpdate(whText As String, trans As SqlTransaction) As Boolean
            Dim ret As Boolean = True
            Dim tmpWhere As String = " Where " & whText
            If _haveData = True Then
                If whText.Trim() <> "" Then
                    Try

                        Dim cmbParam(2) As SqlParameter
                        If _CUSTOMER_SIGN_BYTE IsNot Nothing Then
                            cmbParam(0) = New SqlParameter("@_CUSTOMER_SIGN_BYTE", SqlDbType.Image, _CUSTOMER_SIGN_BYTE.Length)
                            cmbParam(0).Value = _CUSTOMER_SIGN_BYTE
                        Else
                            cmbParam(0) = New SqlParameter("@_CUSTOMER_SIGN_BYTE", SqlDbType.Image)
                            cmbParam(0).Value = DBNull.value
                        End If
                        If _CUSTOMER_IMAGE_BYTE IsNot Nothing Then
                            cmbParam(1) = New SqlParameter("@_CUSTOMER_IMAGE_BYTE", SqlDbType.Image, _CUSTOMER_IMAGE_BYTE.Length)
                            cmbParam(1).Value = _CUSTOMER_IMAGE_BYTE
                        Else
                            cmbParam(1) = New SqlParameter("@_CUSTOMER_IMAGE_BYTE", SqlDbType.Image)
                            cmbParam(1).Value = DBNull.value
                        End If
                        If _PAYMENT_SLIP_BYTE IsNot Nothing Then
                            cmbParam(2) = New SqlParameter("@_PAYMENT_SLIP_BYTE", SqlDbType.Image, _PAYMENT_SLIP_BYTE.Length)
                            cmbParam(2).Value = _PAYMENT_SLIP_BYTE
                        Else
                            cmbParam(2) = New SqlParameter("@_PAYMENT_SLIP_BYTE", SqlDbType.Image)
                            cmbParam(2).Value = DBNull.value
                        End If
                        ret = (DB.ExecuteNonQuery(SqlUpdate & tmpWhere, trans, cmbParam) > 0)
                        If ret = False Then
                            _error = DB.ErrorMessage
                        End If
                        _information = MessageResources.MSGIU001
                    Catch ex As ApplicationException
                        ret = False
                        _error = ex.Message & "ApplicationException :" & ex.ToString() & "### SQL:" & SqlUpdate & tmpWhere
                    Catch ex As Exception
                        ret = False
                        _error = MessageResources.MSGEC102 & " Exception :" & ex.ToString() & "### SQL: " & SqlUpdate & tmpWhere
                    End Try
                Else
                    ret = False
                    _error = MessageResources.MSGEU003 & "### SQL: " & SqlUpdate & tmpWhere
                End If
            Else
                ret = False
                _error = MessageResources.MSGEU002 & "### SQL: " & SqlUpdate & tmpWhere
            End If

            Return ret
        End Function


        '/// Returns an indication whether the current data is deleted from TS_PAYMENT_TRANSACTION table successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if delete data successfully; otherwise, false.</returns>
        Private Function doDelete(whText As String, trans As SqlTransaction) As Boolean
            Dim ret As Boolean = True
            Dim tmpWhere As String = " Where " & whText
            If doChkData(whText, trans) = True Then
                If whText.Trim() <> "" Then
                    Try
                        ret = (DB.ExecuteNonQuery(SqlDelete & tmpWhere, trans) > 0)
                        If ret = False Then
                            _error = MessageResources.MSGED001
                        End If
                        _information = MessageResources.MSGID001
                    Catch ex As ApplicationException
                        ret = False
                        _error = ex.Message & "ApplicationException :" & ex.ToString() & "### SQL:" & SqlDelete & tmpWhere
                    Catch ex As Exception
                        ret = False
                        _error = MessageResources.MSGEC103 & " Exception :" & ex.ToString() & "### SQL: " & SqlDelete & tmpWhere
                    End Try
                Else
                    ret = False
                    _error = MessageResources.MSGED003 & "### SQL: " & SqlDelete & tmpWhere
                End If
            Else
                ret = False
                _error = MessageResources.MSGEU002 & "### SQL: " & SqlDelete & tmpWhere
            End If

            Return ret
        End Function


        '/// Returns an indication whether the record of TS_PAYMENT_TRANSACTION by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Private Function doChkData(whText As String, trans As SqlTransaction) As Boolean
            Dim ret As Boolean = True
            Dim tmpWhere As String = " WHERE " & whText
            ClearData()
            _haveData = False
            If whText.Trim() <> "" Then
                Dim Rdr As SQLDataReader
                Try
                    Rdr = DB.ExecuteReader(SqlSelect() & tmpWhere, trans)
                    If Rdr.Read() Then
                        _haveData = True
                        If Convert.IsDBNull(Rdr("id")) = False Then _id = Convert.ToInt64(Rdr("id"))
                        If Convert.IsDBNull(Rdr("create_by")) = False Then _create_by = Rdr("create_by").ToString()
                        If Convert.IsDBNull(Rdr("create_date")) = False Then _create_date = Convert.ToDateTime(Rdr("create_date"))
                        If Convert.IsDBNull(Rdr("update_by")) = False Then _update_by = Rdr("update_by").ToString()
                        If Convert.IsDBNull(Rdr("update_date")) = False Then _update_date = Convert.ToDateTime(Rdr("update_date"))
                        If Convert.IsDBNull(Rdr("transaction_no")) = False Then _transaction_no = Rdr("transaction_no").ToString()
                        If Convert.IsDBNull(Rdr("transaction_date")) = False Then _transaction_date = Convert.ToDateTime(Rdr("transaction_date"))
                        If Convert.IsDBNull(Rdr("account_no")) = False Then _account_no = Rdr("account_no").ToString()
                        If Convert.IsDBNull(Rdr("account_name")) = False Then _account_name = Rdr("account_name").ToString()
                        If Convert.IsDBNull(Rdr("amount")) = False Then _amount = Convert.ToDouble(Rdr("amount"))
                        If Convert.IsDBNull(Rdr("payment_type")) = False Then _payment_type = Rdr("payment_type").ToString()
                        If Convert.IsDBNull(Rdr("credit_card_no")) = False Then _credit_card_no = Rdr("credit_card_no").ToString()
                        If Convert.IsDBNull(Rdr("customer_sign_byte")) = False Then _customer_sign_byte = CType(Rdr("customer_sign_byte"), Byte())
                        If Convert.IsDBNull(Rdr("customer_image_byte")) = False Then _customer_image_byte = CType(Rdr("customer_image_byte"), Byte())
                        If Convert.IsDBNull(Rdr("payment_slip_byte")) = False Then _payment_slip_byte = CType(Rdr("payment_slip_byte"), Byte())
                    Else
                        ret = False
                        _error = MessageResources.MSGEV002
                    End If

                    Rdr.Close()
                Catch ex As Exception
                    ex.ToString()
                    ret = False
                    _error = MessageResources.MSGEC104 & " #### " & ex.ToString()
                End Try
            Else
                ret = False
                _error = MessageResources.MSGEV001
            End If

            Return ret
        End Function


        '/// Returns an indication whether the record of TS_PAYMENT_TRANSACTION by specified condition is retrieved successfully.
        '/// <param name=whText>The condition specify the deleting record(s).</param>
        '/// <param name=trans>The System.Data.SQLClient.SQLTransaction used by this System.Data.SQLClient.SQLCommand.</param>
        '/// <returns>true if data is retrieved successfully; otherwise, false.</returns>
        Private Function doGetData(whText As String, trans As SqlTransaction) As TsPaymentTransactionLinqDB
            ClearData()
            _haveData = False
            If whText.Trim() <> "" Then
                Dim tmpWhere As String = " WHERE " & whText
                Dim Rdr As SQLDataReader
                Try
                    Rdr = DB.ExecuteReader(SqlSelect() & tmpWhere, trans)
                    If Rdr.Read() Then
                        _haveData = True
                        If Convert.IsDBNull(Rdr("id")) = False Then _id = Convert.ToInt64(Rdr("id"))
                        If Convert.IsDBNull(Rdr("create_by")) = False Then _create_by = Rdr("create_by").ToString()
                        If Convert.IsDBNull(Rdr("create_date")) = False Then _create_date = Convert.ToDateTime(Rdr("create_date"))
                        If Convert.IsDBNull(Rdr("update_by")) = False Then _update_by = Rdr("update_by").ToString()
                        If Convert.IsDBNull(Rdr("update_date")) = False Then _update_date = Convert.ToDateTime(Rdr("update_date"))
                        If Convert.IsDBNull(Rdr("transaction_no")) = False Then _transaction_no = Rdr("transaction_no").ToString()
                        If Convert.IsDBNull(Rdr("transaction_date")) = False Then _transaction_date = Convert.ToDateTime(Rdr("transaction_date"))
                        If Convert.IsDBNull(Rdr("account_no")) = False Then _account_no = Rdr("account_no").ToString()
                        If Convert.IsDBNull(Rdr("account_name")) = False Then _account_name = Rdr("account_name").ToString()
                        If Convert.IsDBNull(Rdr("amount")) = False Then _amount = Convert.ToDouble(Rdr("amount"))
                        If Convert.IsDBNull(Rdr("payment_type")) = False Then _payment_type = Rdr("payment_type").ToString()
                        If Convert.IsDBNull(Rdr("credit_card_no")) = False Then _credit_card_no = Rdr("credit_card_no").ToString()
                        If Convert.IsDBNull(Rdr("customer_sign_byte")) = False Then _customer_sign_byte = CType(Rdr("customer_sign_byte"), Byte())
                        If Convert.IsDBNull(Rdr("customer_image_byte")) = False Then _customer_image_byte = CType(Rdr("customer_image_byte"), Byte())
                        If Convert.IsDBNull(Rdr("payment_slip_byte")) = False Then _payment_slip_byte = CType(Rdr("payment_slip_byte"), Byte())
                    Else
                        _error = MessageResources.MSGEV002
                    End If

                    Rdr.Close()
                Catch ex As Exception
                    ex.ToString()
                    _error = MessageResources.MSGEC104 & " #### " & ex.ToString()
                End Try
            Else
                _error = MessageResources.MSGEV001
            End If
            Return Me
        End Function



        ' SQL Statements


        'Get Insert Statement for table TS_PAYMENT_TRANSACTION
        Private ReadOnly Property SqlInsert() As String
            Get
                Dim Sql As String = ""
                Sql += "INSERT INTO " & tableName & " (ID, CREATE_BY, CREATE_DATE, UPDATE_BY, UPDATE_DATE, TRANSACTION_NO, TRANSACTION_DATE, ACCOUNT_NO, ACCOUNT_NAME, AMOUNT, PAYMENT_TYPE, CREDIT_CARD_NO, CUSTOMER_SIGN_BYTE, CUSTOMER_IMAGE_BYTE, PAYMENT_SLIP_BYTE)"
                Sql += " VALUES("
                sql += DB.SetDouble(_ID) & ", "
                sql += DB.SetString(_CREATE_BY) & ", "
                sql += DB.SetDateTime(_CREATE_DATE) & ", "
                sql += DB.SetString(_UPDATE_BY) & ", "
                sql += DB.SetDateTime(_UPDATE_DATE) & ", "
                sql += DB.SetString(_TRANSACTION_NO) & ", "
                sql += DB.SetDateTime(_TRANSACTION_DATE) & ", "
                sql += DB.SetString(_ACCOUNT_NO) & ", "
                sql += DB.SetString(_ACCOUNT_NAME) & ", "
                sql += DB.SetDouble(_AMOUNT) & ", "
                sql += DB.SetString(_PAYMENT_TYPE) & ", "
                sql += DB.SetString(_CREDIT_CARD_NO) & ", "
                sql += "@_CUSTOMER_SIGN_BYTE" & ", "
                sql += "@_CUSTOMER_IMAGE_BYTE" & ", "
                sql += "@_PAYMENT_SLIP_BYTE"
                sql += ")"
                Return sql
            End Get
        End Property


        'Get update statement form table TS_PAYMENT_TRANSACTION
        Private ReadOnly Property SqlUpdate() As String
            Get
                Dim Sql As String = ""
                Sql += "UPDATE " & tableName & " SET "
                Sql += "CREATE_BY = " & DB.SetString(_CREATE_BY) & ", "
                Sql += "CREATE_DATE = " & DB.SetDateTime(_CREATE_DATE) & ", "
                Sql += "UPDATE_BY = " & DB.SetString(_UPDATE_BY) & ", "
                Sql += "UPDATE_DATE = " & DB.SetDateTime(_UPDATE_DATE) & ", "
                Sql += "TRANSACTION_NO = " & DB.SetString(_TRANSACTION_NO) & ", "
                Sql += "TRANSACTION_DATE = " & DB.SetDateTime(_TRANSACTION_DATE) & ", "
                Sql += "ACCOUNT_NO = " & DB.SetString(_ACCOUNT_NO) & ", "
                Sql += "ACCOUNT_NAME = " & DB.SetString(_ACCOUNT_NAME) & ", "
                Sql += "AMOUNT = " & DB.SetDouble(_AMOUNT) & ", "
                Sql += "PAYMENT_TYPE = " & DB.SetString(_PAYMENT_TYPE) & ", "
                Sql += "CREDIT_CARD_NO = " & DB.SetString(_CREDIT_CARD_NO) & ", "
                Sql += "CUSTOMER_SIGN_BYTE = " & "@_CUSTOMER_SIGN_BYTE" & ", "
                Sql += "CUSTOMER_IMAGE_BYTE = " & "@_CUSTOMER_IMAGE_BYTE" & ", "
                Sql += "PAYMENT_SLIP_BYTE = " & "@_PAYMENT_SLIP_BYTE" + ""
                Return Sql
            End Get
        End Property


        'Get Delete Record in table TS_PAYMENT_TRANSACTION
        Private ReadOnly Property SqlDelete() As String
            Get
                Dim Sql As String = "DELETE FROM " & tableName
                Return Sql
            End Get
        End Property


        'Get Select Statement for table TS_PAYMENT_TRANSACTION
        Private ReadOnly Property SqlSelect() As String
            Get
                Dim Sql As String = "SELECT ID, CREATE_BY, CREATE_DATE, UPDATE_BY, UPDATE_DATE, TRANSACTION_NO, TRANSACTION_DATE, ACCOUNT_NO, ACCOUNT_NAME, AMOUNT, PAYMENT_TYPE, CREDIT_CARD_NO, CUSTOMER_SIGN_BYTE, CUSTOMER_IMAGE_BYTE, PAYMENT_SLIP_BYTE FROM " & tableName
                Return Sql
            End Get
        End Property


    End Class
End Namespace
