
Partial Class frmProcessAlam
    Inherits System.Web.UI.Page

    Dim INIFile As String = Server.MapPath("Config.ini")
#Region "Initail"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            PopulateGrid()
            PopulateGrid_History()
            lblRefreshTime.Text = DateTime.Now.ToString("HH:mm:ss")


            txtHost.Attributes.Add("onkeypress", "return clickButton(event,'" & btnSearch.ClientID & "')")
            txtAlamCode.Attributes.Add("onkeypress", "return clickButton(event,'" & btnSearch.ClientID & "')")
            txtType.Attributes.Add("onkeypress", "return clickButton(event,'" & btnSearch.ClientID & "')")
            txtsysLocation.Attributes.Add("onkeypress", "return clickButton(event,'" & btnSearch.ClientID & "')")
            txtDescription.Attributes.Add("onkeypress", "return clickButton(event,'" & btnSearch.ClientID & "')")
        End If
    End Sub

    Private Sub PopulateGrid()
        Dim clsAlarmDataENG As New Engine.ConnectDB.AlarmDataENG
        With clsAlarmDataENG
            Mydg1.DataSource = .GetAlarmData_More(txtHost.Text, _
                                             "Alarm", _
                                             txtDateFrom.DateValue, _
                                             txtDateTo.DateValue, _
                                             txtAlamCode.Text, _
                                             txtDescription.Text, _
                                             txtsysLocation.Text, _
                                             rdoSeverity.SelectedValue, _
                                             txtType.Text, INIFile)

            Mydg1.DataBind()


        End With
        clsAlarmDataENG = Nothing
    End Sub

    Private Sub PopulateGrid_History()
        Dim clsAlarmDataENG As New Engine.ConnectDB.AlarmDataENG
        With clsAlarmDataENG
            Mydg2.DataSource = .GetAlarmData_More(txtHost.Text, _
                                             "Clear", _
                                             txtDateFrom.DateValue, _
                                             txtDateTo.DateValue, _
                                             txtAlamCode.Text, _
                                             txtDescription.Text, _
                                             txtsysLocation.Text, _
                                             rdoSeverity.SelectedValue, _
                                             txtType.Text, INIFile)

            Mydg2.DataBind()


        End With
        clsAlarmDataENG = Nothing
    End Sub
#End Region

#Region "Event Handles"

    Protected Sub Mydg1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Mydg1.ItemDataBound
        Try
            Dim lbl_AlamMethod As Label = e.Item.FindControl("lbl_AlamMethod")
            Dim lbl_CreateDate As Label = e.Item.FindControl("lbl_CreateDate")
            Dim lbl_UpdateDate As Label = e.Item.FindControl("lbl_UpdateDate")
            Dim lbl_Severity As Label = e.Item.FindControl("lbl_Severity")
            If Not IsNothing(lbl_Severity) Then
                Select Case lbl_Severity.Text.ToLower
                    Case "minor"
                        e.Item.BackColor = Drawing.Color.Yellow
                    Case "major"
                        e.Item.BackColor = Drawing.Color.Orange
                    Case "critical"
                        e.Item.BackColor = Drawing.Color.Red
                End Select
            End If

            If Not IsNothing(lbl_CreateDate) Then
                lbl_CreateDate.Text = Format(e.Item.DataItem("CreateDate"), "dd/MM/yyyy HH:mm")
            End If
            If Not IsNothing(lbl_UpdateDate) Then
                lbl_UpdateDate.Text = Format(e.Item.DataItem("UpdateDate"), "dd/MM/yyyy HH:mm")
            End If
        Catch ex As Exception

        End Try

    End Sub
    Protected Sub Mydg1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Mydg1.PageIndexChanged
        Mydg1.CurrentPageIndex = e.NewPageIndex
        PopulateGrid()
    End Sub
    Protected Sub Mydg2_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Mydg2.ItemDataBound
        Try
            Dim lbl_AlamMethod2 As Label = e.Item.FindControl("lbl_AlamMethod2")
            Dim lbl_CreateDate2 As Label = e.Item.FindControl("lbl_CreateDate2")
            Dim lbl_UpdateDate2 As Label = e.Item.FindControl("lbl_UpdateDate2")
            If Not IsNothing(lbl_AlamMethod2) Then
                Select Case lbl_AlamMethod2.Text.ToLower
                    Case "minor"
                        e.Item.BackColor = Drawing.Color.Yellow
                    Case "major"
                        e.Item.BackColor = Drawing.Color.Orange
                    Case "critical"
                        e.Item.BackColor = Drawing.Color.Red
                End Select
            End If

            If Not IsNothing(lbl_CreateDate2) Then
                lbl_CreateDate2.Text = Format(e.Item.DataItem("CreateDate"), "dd/MM/yyyy HH:mm")
            End If
            If Not IsNothing(lbl_UpdateDate2) Then
                lbl_UpdateDate2.Text = Format(e.Item.DataItem("UpdateDate"), "dd/MM/yyyy HH:mm")
            End If
        Catch ex As Exception

        End Try

    End Sub
    Protected Sub Mydg2_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Mydg2.PageIndexChanged
        Mydg2.CurrentPageIndex = e.NewPageIndex
        PopulateGrid_History()
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        PopulateGrid()
        PopulateGrid_History()
    End Sub

    Protected Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        txtHost.Text = ""
        txtType.Text = ""
        txtDateFrom.TxtBox.Text = ""
        txtDateTo.TxtBox.Text = ""
        txtAlamCode.Text = ""
        txtDescription.Text = ""
        txtsysLocation.Text = ""
        rdoSeverity.SelectedIndex = -1
    End Sub
#End Region


    Protected Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If TabContainer1.ActiveTabIndex = 0 Then
            Dim RefreshTime As DateTime = Convert.ToDateTime(lblRefreshTime.Text)
            If DateAdd(DateInterval.Second, CInt(ddlShowAlarmInterval.SelectedValue), RefreshTime) < DateTime.Now Then
                PopulateGrid()
                lblRefreshTime.Text = DateTime.Now.ToString("HH:mm:ss")
            End If
        End If
    End Sub

    Protected Sub ddlShowAlarmInterval_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlShowAlarmInterval.SelectedIndexChanged
        Timer1.Interval = CInt(ddlShowAlarmInterval.SelectedValue) * 1000
    End Sub

    Protected Sub TabContainer1_ActiveTabChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabContainer1.ActiveTabChanged
        If TabContainer1.ActiveTabIndex = 0 Then
            Timer1.Enabled = True
        Else
            Timer1.Enabled = False
        End If
    End Sub
End Class
