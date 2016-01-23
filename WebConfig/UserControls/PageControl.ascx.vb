Imports System
Imports System.Data
Imports System.Configuration
Imports System.Collections
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports CenParaDB.Common.Utilities

Partial Class UserControls_PageControl
    Inherits System.Web.UI.UserControl

    Public Event PageChange(ByVal sender As Object, ByVal e As EventArgs)

    Public Property TotalPage() As String
        Get
            Return lblTotalPage.Text
        End Get
        Set(ByVal value As String)
            lblTotalPage.Text = value
        End Set
    End Property

    Dim gvMain As GridView = Nothing
    Public Sub SetMainGridView(ByVal gv As GridView)
        gvMain = gv
    End Sub

    Public Sub Update(ByVal rowCount As Long)
        If gvMain IsNot Nothing Then
            If gvMain.PageCount > 0 Then
                Me.Visible = True
                Dim curPage As Integer = gvMain.PageIndex
                lblTotalPage.Text = gvMain.PageCount.ToString(Constant.IntFormat)
                buildPageCombo(gvMain.PageCount)
                cmbPage.SelectedIndex = curPage
                lnbBack.Enabled = (curPage <> 0)
                lnbNext.Enabled = (curPage < gvMain.PageCount - 1)
                lblSummary.Text = "รายการที่ " & ((gvMain.PageIndex * gvMain.PageSize) + 1).ToString(Constant.IntFormat) & " - " & ((gvMain.PageIndex * gvMain.PageSize) + gvMain.Rows.Count).ToString(Constant.IntFormat) & " จาก " & rowCount.ToString(Constant.IntFormat) & " รายการ"
                txtPageIndex.Text = "0"
            Else
                Me.Visible = False
            End If
        End If
    End Sub

    Public Sub Update(ByVal curPage As Integer, ByVal total As String, ByVal pageSize As Integer)
        Dim pageCount As Integer = 0
        If gvMain IsNot Nothing Then
            pageCount = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(total) / Convert.ToDecimal(pageSize)))
            If pageCount > 0 Then
                Me.Visible = True
                lblTotalPage.Text = gvMain.PageCount.ToString(Constant.IntFormat)
                buildPageCombo(gvMain.PageCount)
                cmbPage.SelectedIndex = curPage
                lnbBack.Enabled = (curPage <> 0)
                lnbNext.Enabled = (curPage < gvMain.PageCount - 1)
                lblSummary.Text = "รายการที่ " & ((gvMain.PageIndex * gvMain.PageSize) + 1).ToString(Constant.IntFormat) & " - " & ((gvMain.PageIndex * gvMain.PageSize) + gvMain.Rows.Count).ToString(Constant.IntFormat) & " จาก " & total & " รายการ"
            Else
                Me.Visible = False
            End If
        End If
    End Sub

    Public Sub buildPageCombo(ByVal maxPage As Integer)
        cmbPage.Items.Clear()
        Dim i As Integer = 0
        For i = 0 To maxPage - 1
            Dim p As Integer = i + 1
            cmbPage.Items.Add(New ListItem(p.ToString(), p.ToString()))
        Next

    End Sub

    Public ReadOnly Property SelectPageIndex() As Integer
        Get
            Return cmbPage.SelectedIndex
        End Get
    End Property

    Protected Sub cmbPage_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPage.SelectedIndexChanged
        If txtPageIndex.Text = "0" Then
            txtPageIndex.Text = "1"
            RaiseEvent PageChange(Me, e)
        Else
            txtPageIndex.Text = "0"
        End If
    End Sub


    Protected Sub lnbNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnbNext.Click
        If txtPageIndex.Text = "0" Then
            txtPageIndex.Text = "1"
            If cmbPage.SelectedIndex < cmbPage.Items.Count - 1 Then
                cmbPage.SelectedIndex = cmbPage.SelectedIndex + 1
                RaiseEvent PageChange(Me, e)
            End If
        Else
            txtPageIndex.Text = "0"
        End If
    End Sub

    Protected Sub lnbBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnbBack.Click
        If txtPageIndex.Text = "0" Then
            txtPageIndex.Text = "1"
            cmbPage.SelectedIndex = cmbPage.SelectedIndex - 1
            RaiseEvent PageChange(Me, e)
        Else
            txtPageIndex.Text = "0"
        End If
    End Sub
End Class
