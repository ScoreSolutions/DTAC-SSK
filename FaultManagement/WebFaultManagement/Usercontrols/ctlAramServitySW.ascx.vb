Imports System.Data

Partial Class Usercontrols_ctlAramServitySW
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            txtIntervalMinute.Attributes.Add("onkeypress", "ChkMinusInt(this,event);")
            txtIntervalMinute.Attributes.Add("onkeydown", "CheckKeyNumber(event);")
        End If
    End Sub

    Public Function GetAlertInputData() As String
        If txtShopName.Text = "" Then
            Return "Please Input Shop Name !"
        ElseIf txtServerName.Text = "" Then
            Return "Please Input Server Name!"
        ElseIf txtIPAddress.Text = "" Then
            Return "Please Input IP Address!"
        ElseIf txtIntervalMinute.Text = "" Then
            Return "Please Input Interval Minute !"
        ElseIf Not IsNumeric(txtIntervalMinute.Text) Then
            Return "Please Input Interval Minute Is Numeric !"
        ElseIf rdoMethod.SelectedIndex = -1 Then
            Return "Please Select Alam Method !"
        ElseIf checkSelectItemInGrid() = False Then
            Return "Please Select Service List !"
        ElseIf checkSelectItemInPutAlamCodeGrid() = False Then
            Return "Please Select Service List and Input Alam Code!"
        ElseIf checkSelectItemInPutProcessNameGrid() = False Then
            Return "Please Select Service List and Input Process Name!"
        Else
            Return ""
        End If
    End Function

    Private Function checkSelectItemInGrid() As Boolean
        For i As Integer = 0 To Mydg1.Items.Count - 1
            Dim ckbSelect As CheckBox = Mydg1.Items(i).FindControl("ckbSelect")
            If ckbSelect.Checked Then
                Return True
            End If
        Next
        Return False
    End Function

    Private Function checkSelectItemInPutAlamCodeGrid() As Boolean
        For i As Integer = 0 To Mydg1.Items.Count - 1
            Dim ckbSelect As CheckBox = Mydg1.Items(i).FindControl("ckbSelect")
            Dim txtAlamCode As Label = Mydg1.Items(i).FindControl("txtAlamCode")
            If ckbSelect.Checked And txtAlamCode.Text = "" Then
                Return False
            End If
        Next
        Return True
    End Function
    Private Function checkSelectItemInPutProcessNameGrid() As Boolean
        For i As Integer = 0 To Mydg1.Items.Count - 1
            Dim ckbSelect As CheckBox = Mydg1.Items(i).FindControl("ckbSelect")
            Dim txtProcessName As Label = Mydg1.Items(i).FindControl("txtProcessName")
            If ckbSelect.Checked And txtProcessName.Text = "" Then
                Return False
            End If
        Next
        Return True
    End Function

    Public Sub PopulateGrid(ByVal dtData As DataTable, ByVal Sw_Para As String)
        lblKeepPara.Text = Sw_Para
        Mydg1.DataSource = dtData
        Mydg1.DataBind()


        Select Case lblKeepPara.Text
            Case "Service"
                lblItemList.Text = "Service List"
            Case "Process"
                lblItemList.Text = "Window Process List"
            Case "Web"
                lblItemList.Text = "Web Application List"
        End Select
    End Sub

    Protected Sub Mydg1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Mydg1.ItemDataBound
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim ckbActive As CheckBox = e.Item.FindControl("ckbActive")
            Dim ckbSelect As CheckBox = e.Item.FindControl("ckbSelect")
            Dim txtAlamCode As Label = e.Item.FindControl("txtAlamCode")
            Dim txtProcessName As Label = e.Item.FindControl("txtProcessName")
            Dim lbl_Active As Label = e.Item.FindControl("lbl_Active")
            Dim lbl_DisplayName As Label = e.Item.FindControl("lbl_DisplayName")
            Dim lbl_ServiceName As Label = e.Item.FindControl("lbl_ServiceName")
            Select Case lblKeepPara.Text
                Case "Service"
                    lbl_ServiceName.Text = e.Item.DataItem("ServiceName")
                Case "Process"
                    lbl_ServiceName.Text = e.Item.DataItem("WindowProcessName")
                Case "Web"
                    lbl_ServiceName.Text = e.Item.DataItem("WebApplicationName")
            End Select
            If lbl_Active.Text = "Y" Then
                ckbActive.Checked = True
            End If
        End If

        If e.Item.ItemType = ListItemType.Header Then
            Dim lblHeaderName As Label = e.Item.FindControl("lblHeaderName")
            Select Case lblKeepPara.Text
                Case "Service"
                    lblHeaderName.Text = "Service Name"
                Case "Process"
                    lblHeaderName.Text = "Window Process Name"
                Case "Web"
                    lblHeaderName.Text = "Web Application Name"
            End Select
        End If
    End Sub

    Protected Sub chkAllDayEvent_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkAllDayEvent.CheckedChanged
        If chkAllDayEvent.Checked Then
            txtAlamTimeFrom.Enabled = False
            txtAlamTimeTo.Enabled = False
        Else
            txtAlamTimeFrom.Enabled = True
            txtAlamTimeTo.Enabled = True
        End If

    End Sub
End Class
