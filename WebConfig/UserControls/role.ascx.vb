Imports System.Data
Imports CenLinqDB
Imports Engine.Configuration

Partial Class UserControls_role
    Inherits System.Web.UI.UserControl
    Private _type As String = ""
    Private _header As String = ""
    Private _IsUserExist As Boolean = False
    Private _IsProgramIDExist As Boolean = False
    Private _roleid As String

#Region "Property"
    Public WriteOnly Property Type() As EnumType
        Set(ByVal value As EnumType)
            _type = value
        End Set
    End Property

    Public WriteOnly Property Header() As String
        Set(ByVal value As String)
            _header = value
        End Set
    End Property

    Public WriteOnly Property IsUserExist() As Boolean
        Set(ByVal value As Boolean)
            _IsUserExist = value
        End Set
    End Property

    Public WriteOnly Property IsProgramIDExist() As Boolean
        Set(ByVal value As Boolean)
            _IsProgramIDExist = value
        End Set
    End Property

    Public WriteOnly Property RoleID() As String
        Set(ByVal value As String)
            _roleid = value
        End Set
    End Property

    Public ReadOnly Property dtSelectList() As DataTable
        Get
            dtSelectList = getDataSelect()
        End Get
    End Property

#End Region '__Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            lblHeader.Text = _header
            SetDataList()
        End If
    End Sub

    Private Function getDataSelect() As DataTable
        Dim dt As New DataTable
        dt.Columns.Add("id", GetType(String))
        dt.Columns.Add("name", GetType(String))
        Dim dr As DataRow
        Dim chk As New CheckBox
        For Each row In gvDataList.Rows
            Dim lbl_id As Label = row.FindControl("lblID")
            Dim lbl_name As Label = row.FindControl("lblName")
            chk = row.FindControl("ChkSelect")
            If chk.Checked = True Then
                dr = dt.NewRow
                dr("id") = lbl_id.Text
                dr("name") = lbl_name.Text
                dt.Rows.Add(dr)
            End If
        Next

        Return dt
    End Function

    Public Sub SetDataList()

        Dim dt As New DataTable
        Dim eng As New RoleENG
        Select Case _type
            Case EnumType.User
                If _IsUserExist = True Then
                    dt = eng.GetUserList(" id  in (select tb_user_id from TB_ROLE_USER where tb_role_id ='" & _roleid & "') ")
                Else
                    dt = eng.GetUserList(" id not in (select tb_user_id from TB_ROLE_USER where tb_role_id ='" & _roleid & "' )")
                End If

            Case EnumType.ProgramID
                If _IsProgramIDExist = True Then
                    dt = eng.GetProgramList(" s.id  in (select sysmenu_id from TB_ROLE_SYSMENU where tb_role_id ='" & _roleid & "') ")
                Else
                    dt = eng.GetProgramList(" s.id not in (select sysmenu_id from TB_ROLE_SYSMENU where tb_role_id ='" & _roleid & "' )")
                End If
        End Select


        gvDataList.DataSource = dt
        gvDataList.DataBind()

        dt.Dispose()

    End Sub

    Protected Sub gvDataList_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvDataList.RowDataBound
        Dim lblHeaderText As Label = e.Row.FindControl("lblHeaderText")
        If Not IsNothing(lblHeaderText) Then
            lblHeaderText.Text = IIf(_type = "1", "User", "ProgramID")
        End If
    End Sub

    Public Enum EnumType
        User = 1
        ProgramID = 2
    End Enum

 
End Class
