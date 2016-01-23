
Partial Class UserControls_txtTime
    Inherits System.Web.UI.UserControl

    Dim _CssType As TextboxType = TextboxType.TextBox
    Dim _IsNotNull As Boolean

    Public Property CssType() As TextboxType
        Get
            Return _CssType
        End Get
        Set(ByVal value As TextboxType)
            _CssType = value
        End Set
    End Property
    Public Property TimeText() As String
        Get
            Return TextBox1.Text
        End Get
        Set(ByVal value As String)
            TextBox1.Text = value
        End Set
    End Property
    Public Property IsNotNull() As Boolean
        Get
            Return Label1.Visible
        End Get
        Set(ByVal value As Boolean)
            Label1.Visible = value
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

    Public Enum TextboxType
        TextBox = 1
        TextView = 2
    End Enum
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TextBox1.Attributes.Add("OnMouseUp", "this.select();")
        If _CssType = TextboxType.TextBox Then
            TextBox1.CssClass = "TextBox"
        Else
            TextBox1.CssClass = "TextView"
            TextBox1.Attributes.Add("OnKeyPress", "txtKeyPress();")
            TextBox1.Attributes.Add("onKeyDown", "CheckKeyBackSpace();")
        End If
    End Sub
End Class
