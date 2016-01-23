
Partial Class UserControls_txtDate
    Inherits System.Web.UI.UserControl

    Public Delegate Sub SelectedDateEvent(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event SelectedDateChanged As SelectedDateEvent

    Dim _IsNotNull As Boolean = False
    Dim _DefaultCurrentDate As Boolean = False
    Public Property DefaultCurrentDate() As Boolean
        Get
            Return _DefaultCurrentDate
        End Get
        Set(ByVal value As Boolean)
            _DefaultCurrentDate = value
        End Set
    End Property
    Public Property AutoPosBack() As Boolean
        Get
            Return txtdate.AutoPostBack
        End Get
        Set(ByVal value As Boolean)
            txtdate.AutoPostBack = value
        End Set
    End Property
    Public WriteOnly Property ValidationText() As String
        Set(ByVal value As String)
            lblValidText.Text = value
        End Set
    End Property
    Public Property TxtBox() As TextBox
        Get
            Return txtdate
        End Get
        Set(ByVal value As TextBox)
            txtdate = value
        End Set
    End Property
    Public Overrides ReadOnly Property ClientID() As String
        Get
            Return txtdate.ClientID
        End Get
    End Property

    Public ReadOnly Property ImageClientID() As String
        Get
            Return ImageButton1.ClientID
        End Get
    End Property

    Public Property DateValue() As DateTime
        Get
            Return GetDate()
        End Get
        Set(ByVal value As DateTime)
            SetDate(value)
        End Set
    End Property

    Public Property VisibleImg() As Boolean
        Get
            Return ImageButton1.Visible
        End Get
        Set(ByVal value As Boolean)
            ImageButton1.Visible = value
        End Set
    End Property
    Public Property IsNotNull() As Boolean
        Get
            Return _IsNotNull
        End Get
        Set(ByVal value As Boolean)
            _IsNotNull = value
        End Set
    End Property
    Public WriteOnly Property ValidateText() As String
        Set(ByVal value As String)
            lblValidText.Text = value
        End Set
    End Property
    Public ReadOnly Property GetDateCondition() As String
        Get
            Dim ret As String = ""
            If txtdate.Text.Trim() <> "" Then
                If GetDate.Year > 2500 Then
                    ret = (GetDate.Year - 543) & GetDate().ToString("MMdd")
                Else
                    ret = GetDate.Year & GetDate().ToString("MMdd")
                End If
            End If

            Return ret
        End Get
    End Property

    Private Function GetDate() As Date
        If txtdate.Text.Trim() <> "" Then
            Return Date.ParseExact(txtdate.Text, "dd/MM/yyyy", Nothing)
        Else
            Return New Date(1, 1, 1)
        End If
    End Function
    Private Sub SetDate(ByVal DateValue As DateTime)
        If DateValue.Year = 1 Then
            txtdate.Text = ""
        Else
            txtdate.Text = DateValue.ToString("dd/MM/yyyy")
        End If

    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            txtdate.Attributes.Add("OnClick", "this.select();")
            txtdate.Attributes.Add("OnKeyPress", "txtKeyPress(event);")
            txtdate.Attributes.Add("onKeyDown", "CheckKeyBackSpace(event);")
            txtdate.Attributes.Add("OnMouseDown", "ValidRightClick(event)")

            If _IsNotNull = False Then
                Label1.Visible = False
            Else
                Label1.Visible = True
            End If

            If _DefaultCurrentDate = True Then
                txtdate.Text = DateTime.Today.ToString("dd/MM/yyyy")
            End If

        End If
    End Sub

    
    Protected Sub txtdate_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtdate.TextChanged
        RaiseEvent SelectedDateChanged(sender, e)

    End Sub
End Class
