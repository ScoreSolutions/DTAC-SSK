Imports System.Data

Partial Class UserControls_cmbAutoComplete
    Inherits System.Web.UI.UserControl

    Public Event SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    Dim _isDefaultValue As Boolean = True
    Dim _DefaultValue As String = "0"
    Dim _DefaultDisplay As String = "เลือก"

    Public Sub dfCombo()
        lblValidText.Text = "กรุณาเลือกขอมูล"
    End Sub
    Public Sub clearDfCombo()
        lblValidText.Text = ""
    End Sub

    Public Overrides ReadOnly Property ClientID() As String
        Get
            Return cmbCombo.ClientID
        End Get
    End Property

    Public Property IsDefaultValue() As Boolean
        Get
            Return _isDefaultValue

        End Get
        Set(ByVal value As Boolean)
            _isDefaultValue = value

        End Set
    End Property
    Public Property DefaultValue() As String
        Get
            Return _DefaultValue
        End Get
        Set(ByVal value As String)
            _DefaultValue = value
        End Set
    End Property
    Public Property DefaultDisplay() As String
        Get
            Return _DefaultDisplay
        End Get
        Set(ByVal value As String)
            _DefaultDisplay = value
        End Set
    End Property

    Public Property SelectedValue() As String
        Get
            Return cmbCombo.SelectedValue
        End Get
        Set(ByVal value As String)
            cmbCombo.SelectedValue = value
        End Set
    End Property
    Public ReadOnly Property SelectedText() As String
        Get
            Return cmbCombo.SelectedItem.Text
        End Get
    End Property
    Public Property AutoPosBack() As Boolean
        Get
            Return cmbCombo.AutoPostBack
        End Get
        Set(ByVal value As Boolean)
            cmbCombo.AutoPostBack = value
        End Set
    End Property
    Public Property Width() As Unit
        Get
            Return cmbCombo.Width
        End Get
        Set(ByVal value As Unit)
            cmbCombo.Width = value
        End Set
    End Property
    Public Property Enabled() As Boolean
        Get
            Return cmbCombo.Enabled
        End Get
        Set(ByVal value As Boolean)
            cmbCombo.Enabled = value
        End Set
    End Property
    Public WriteOnly Property ValidateText() As String
        Set(ByVal value As String)
            lblValidText.Text = value
        End Set
    End Property

    Public Overloads ReadOnly Property Attributes() As AttributeCollection
        Get
            Return cmbCombo.Attributes
        End Get
    End Property

    Public Sub clearval()
        cmbCombo.SelectedIndex = 0
    End Sub
    Public Sub SetItemList(ByVal itemText As String, ByVal itemValue As String)
        Dim lst As New ListItem(itemText, itemValue)
        cmbCombo.Items.Add(lst)
    End Sub
    Public Sub SetItemList(ByVal dt As DataTable, ByVal fldText As String, ByVal fldValue As String)
        ClearCombo()
        If dt.Rows.Count > 0 Then
            If _isDefaultValue = True Then
                Dim dr As DataRow = dt.NewRow
                dr(fldText) = _DefaultDisplay
                dr(fldValue) = _DefaultValue

                dt.Rows.InsertAt(dr, 0)
            End If
            cmbCombo.DataValueField = fldValue
            cmbCombo.DataTextField = fldText
            cmbCombo.DataSource = dt
            cmbCombo.DataBind()
        Else
            SetDefaultItem()
        End If
    End Sub
    Public Sub ClearCombo()
        cmbCombo.Items.Clear()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetDefaultItem()
    End Sub

    Private Sub cmbCombo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCombo.SelectedIndexChanged
        RaiseEvent SelectedIndexChanged(sender, e)
    End Sub
    Private Sub SetDefaultItem()
        If _isDefaultValue = True And cmbCombo.Items.Count = 0 Then
            Dim lst As New ListItem(_DefaultDisplay, _DefaultValue)
            cmbCombo.Items.Insert(0, lst)
        End If
    End Sub
End Class
