
Partial Class Usercontrols_ctlAramServity
    Inherits System.Web.UI.UserControl

    Public Function GetAlertInputData() As String
        If txtReplaceTime.Text = "" Then
            Return "Please Input Replace Check Time !"
        ElseIf Not IsNumeric(txtReplaceTime.Text) Then
            Return "Please Input Replace Check Is Numeric !"
        ElseIf txtMinor.Text = "" Then
            Return "Please Input Minor:Value Over !"
        ElseIf Not IsNumeric(txtMinor.Text) Then
            Return "Please Input Minor Value Over Is Numeric !"
        ElseIf rdoMinorMethod.SelectedIndex = -1 Then
            Return "Please Select Minor Method !"
        ElseIf txtMajor.Text = "" Then
            Return "Please Input Major Value Over !"
        ElseIf Not IsNumeric(txtMajor.Text) Then
            Return "Please Input Major Value Over Is Numeric !"
        ElseIf rdoMajorMethod.SelectedIndex = -1 Then
            Return "Please Select Major Method !"
        ElseIf txtCritical.Text = "" Then
            Return "Please Input Critical Value Over !"
        ElseIf Not IsNumeric(txtCritical.Text) Then
            Return "Please Input Critical Value Over Is Numeric !"
        ElseIf rdoCriticalMethod.SelectedIndex = -1 Then
            Return "Please Select Critical Method !"
        ElseIf txtInterval.Text = "" Then
            Return "Please Input Interval !"
        ElseIf Not IsNumeric(txtInterval.Text) Then
            Return "Please Input Interval Is Numeric !"
        Else
            Return ""
        End If
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            txtReplaceTime.Attributes.Add("onkeypress", "ChkMinusInt(this,event);")
            txtReplaceTime.Attributes.Add("onkeydown", "CheckKeyNumber(event);")
            txtMinor.Attributes.Add("onkeypress", "ChkMinusInt(this,event);")
            txtMinor.Attributes.Add("onkeydown", "CheckKeyNumber(event);")
            txtMajor.Attributes.Add("onkeypress", "ChkMinusInt(this,event);")
            txtMajor.Attributes.Add("onkeydown", "CheckKeyNumber(event);")
            txtCritical.Attributes.Add("onkeypress", "ChkMinusInt(this,event);")
            txtCritical.Attributes.Add("onkeydown", "CheckKeyNumber(event);")
            txtInterval.Attributes.Add("onkeypress", "ChkMinusInt(this,event);")
            txtInterval.Attributes.Add("onkeydown", "CheckKeyNumber(event);")
        End If
    End Sub
End Class
