
Partial Class UserControls_txtAutoComplete
    Inherits System.Web.UI.UserControl

    '###############################
    '##### AutoComplete Property
    '###############################
    Public WriteOnly Property TableName() As String
        Set(ByVal value As String)
            txtTbName.Value = value
        End Set
    End Property

    Public WriteOnly Property FieldName() As String
        Set(ByVal value As String)
            txtFldName.Value = value
        End Set
    End Property



    '#####################
    '####### Textbox Property
    '####### Copy มาจาก UserControls.txtBos.ascx ถ้าจะแก้ไขอะไรให้แก้ทั้งสองไฟล์ด้วยนะ
    '#####################
    Public Event TextChange(ByVal sender As Object, ByVal e As System.EventArgs)

    Dim _IsNotNull As Boolean = False
    Dim _TextType As TextboxType = TextboxType.TextBox
    Dim _TextKey As TextKeyType = TextKeyType.TextChar
    Dim _TextAlign As TextboxAlign = TextboxAlign.AlignLeft

    Public Overrides ReadOnly Property ClientID() As String
        Get
            Return TxtBox.ClientID
        End Get
    End Property
    Public Property Text() As String
        Get
            Return TextBox1.Text
        End Get
        Set(ByVal value As String)
            TextBox1.Text = value
        End Set
    End Property

    Public Property Width() As Unit
        Get
            Return TextBox1.Width
        End Get
        Set(ByVal value As Unit)
            TextBox1.Width = value
        End Set
    End Property
    Public Property Height() As Unit
        Get
            Return TextBox1.Height
        End Get
        Set(ByVal value As Unit)
            TextBox1.Height = value
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

    Public Property AutoPosBack() As Boolean
        Get
            Return TextBox1.AutoPostBack
        End Get
        Set(ByVal value As Boolean)
            TextBox1.AutoPostBack = value
        End Set
    End Property
    Public Property MaxLength() As Integer
        Get
            Return TextBox1.MaxLength
        End Get
        Set(ByVal value As Integer)
            TextBox1.MaxLength = value
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
    Public Property TextMode() As TextBoxMode
        Get
            Return TextBox1.TextMode
        End Get
        Set(ByVal value As TextBoxMode)
            TextBox1.TextMode = value
        End Set
    End Property
    Public Property TabIndex() As Short
        Get
            Return TextBox1.TabIndex
        End Get
        Set(ByVal value As Short)
            TextBox1.TabIndex = value
        End Set
    End Property

    Public Property Enabled() As Boolean
        Get
            Return GetEnabled()
        End Get
        Set(ByVal value As Boolean)
            SetEnabled(value)
        End Set
    End Property
    Public Property TextType() As TextboxType
        Get
            Return _TextType
        End Get
        Set(ByVal value As TextboxType)
            _TextType = SetTextType(value)
        End Set
    End Property
    Public Property TextKey() As TextKeyType
        Get
            Return _TextKey
        End Get
        Set(ByVal value As TextKeyType)
            _TextKey = value
        End Set
    End Property
    Public Property TextAlign() As TextboxAlign
        Get
            Return _TextAlign
        End Get
        Set(ByVal value As TextboxAlign)
            _TextAlign = value
        End Set
    End Property

    Public WriteOnly Property ValidationText() As String
        Set(ByVal value As String)
            lblValidText.Text = value
        End Set
    End Property

    Public Overrides Sub Focus()
        TextBox1.Focus()
    End Sub

    Private Sub SetEnabled(ByVal value As Boolean)
        If value = False Then
            TextBox1.BackColor = Drawing.ColorTranslator.FromHtml("#F0F0F0")
            TextBox1.Enabled = False
        Else
            TextBox1.BackColor = Drawing.Color.White
            TextBox1.Enabled = True
        End If
    End Sub
    Private Function GetEnabled() As Boolean
        Return TextBox1.Enabled
    End Function

    Public Enum TextboxType
        TextBox = 1
        TextView = 2
    End Enum
    Public Enum TextKeyType
        TextChar = 1
        TextInt = 2
        TextDouble = 3
    End Enum

    Public Enum TextboxAlign
        AlignLeft = 1
        AlignRight = 2
        AlignCenter = 3
    End Enum


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.autoComplete1.ContextKey = txtTbName.Value & "#" & txtFldName.Value  'For Auto Complete Only

        If IsPostBack = False Then
            SetTextBoxStyle()
            TextBox1.Attributes.Add("OnFocus", "this.select();")
        End If

        If _IsNotNull = False Then
            Label1.Visible = False
        Else
            Label1.Visible = True
        End If
    End Sub

    Private Sub SetTextBoxStyle()

        Dim ret As String = ""
        If _TextType = TextboxType.TextBox Then
            TextBox1.CssClass = "TextBox"
            If _TextKey = TextKeyType.TextInt Then
                TextBox1.Attributes.Add("OnKeyPress", "ChkMinusInt(this,event);")
                TextBox1.Attributes.Add("onKeyDown", "CheckKeyNumber(event);")
            ElseIf _TextKey = TextKeyType.TextDouble Then
                TextBox1.Attributes.Add("OnKeyPress", "ChkMinusDbl(this,event);")
                TextBox1.Attributes.Add("onKeyDown", "CheckKeyNumber(event);")
            End If
        ElseIf _TextType = TextboxType.TextView Then
            TextBox1.CssClass = "TextView"
            TextBox1.Attributes.Add("OnKeyPress", "txtKeyPress(event);")
            TextBox1.Attributes.Add("onKeyDown", "CheckKeyBackSpace(event);")
            TextBox1.Attributes.Add("OnMouseDown", "ValidRightClick(event)")
        End If

        Dim vTextAlign As String = ""
        If _TextAlign = TextboxAlign.AlignRight Then
            vTextAlign += "text-align:right;"
        ElseIf _TextAlign = TextboxAlign.AlignCenter Then
            vTextAlign += "text-align:center;"
        Else
            vTextAlign += "text-align:left;"
        End If
        TextBox1.Attributes.Add("Style", vTextAlign)

    End Sub
    Private Function SetTextBoxAlign() As String
        Dim ret As String = ""
        If _TextAlign = TextboxAlign.AlignRight Then
            ret += "text-align:right;"
        ElseIf _TextAlign = TextboxAlign.AlignCenter Then
            ret += "text-align:center;"
        Else
            ret += "text-align:left;"
        End If
        Return ret
    End Function

    Protected Sub TextBox1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        RaiseEvent TextChange(sender, e)
    End Sub

    Private Function SetTextType(ByVal value As TextboxType) As TextboxType
        If value = TextboxType.TextBox Then
            TextBox1.CssClass = "TextBox"
        ElseIf value = TextboxType.TextView Then
            TextBox1.CssClass = "TextView"
        End If
        Return value
    End Function

    Public Overloads ReadOnly Property Attributes() As AttributeCollection
        Get
            Return TextBox1.Attributes
        End Get
    End Property
End Class
