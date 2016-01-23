
Partial Class UserControls_ctlRichTextBox
    Inherits System.Web.UI.UserControl

    Dim _IsNotNull As Boolean = False

    Public Property Text() As String
        Get
            Return editor1.Content
        End Get
        Set(ByVal value As String)
            'editor1.Content = value
            editor1.Content = value
        End Set
    End Property

    Public Property Width() As Unit
        Get
            Return editor1.Width
        End Get
        Set(ByVal value As Unit)
            editor1.Width = value
        End Set
    End Property
    Public Property Height() As Unit
        Get
            Return editor1.Height
        End Get
        Set(ByVal value As Unit)
            editor1.Height = value
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

    'Public Property Enabled() As Boolean
    '    Get
    '        Return editor1.Enabled
    '    End Get
    '    Set(ByVal value As Boolean)
    '        editor1.Enabled = value
    '    End Set
    'End Property


    Public WriteOnly Property ValidationText() As String
        Set(ByVal value As String)
            lblValidText.Text = value
        End Set
    End Property


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            If _IsNotNull = False Then
                Label1.Visible = False
            Else
                Label1.Visible = True
            End If
        End If
    End Sub



End Class
