
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
            Return TextBox1.AutoPostBack
        End Get
        Set(ByVal value As Boolean)
            TextBox1.AutoPostBack = value
        End Set
    End Property

    Public Property TxtBox() As TextBox
        Get
            Return TextBox1
        End Get
        Set(ByVal value As TextBox)
            TextBox1 = value
        End Set
    End Property
    Public Overrides ReadOnly Property ClientID() As String
        Get
            Return TextBox1.ClientID
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
            If TextBox1.Text.Trim() <> "" Then
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
        If TextBox1.Text.Trim() <> "" Then
            Return Date.ParseExact(TextBox1.Text, "dd/MM/yyyy", Nothing)
        Else
            Return New Date()
        End If
    End Function
    Private Sub SetDate(ByVal DateValue As DateTime)
        If DateValue.Year = 1 Then
            TextBox1.Text = ""
        Else
            TextBox1.Text = DateValue.ToString("dd/MM/yyyy")
        End If

    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            likA.Attributes.Add("onClick", "NewCssCal('" & TextBox1.ClientID & "','DDMMYYYY');")

            TextBox1.Attributes.Add("OnClick", "this.select();")
            TextBox1.Attributes.Add("OnKeyPress", "txtKeyPress(event);")
            TextBox1.Attributes.Add("onKeyDown", "CheckKeyBackSpace(event);")

            If _IsNotNull = False Then
                Label1.Visible = False
            Else
                Label1.Visible = True
            End If

            If _DefaultCurrentDate = True Then
                TextBox1.Text = DateTime.Today.ToString("dd/MM/yyyy")
            End If

        End If
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        RaiseEvent SelectedDateChanged(sender, e)
    End Sub

    Public Overloads ReadOnly Property Attributes() As AttributeCollection
        Get
            Return TextBox1.Attributes
        End Get
    End Property

    Public Overloads ReadOnly Property LinkAttributes() As AttributeCollection
        Get
            Return likA.Attributes
        End Get
    End Property
End Class
