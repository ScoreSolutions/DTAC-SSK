Imports System.Data
Imports Engine.Common.FunctionEng
Partial Class UserControls_ctlBranchSelected
    Inherits System.Web.UI.UserControl

    Public Event TreeViewBranchChange(ByVal arrBranch As ArrayList)

    Public ReadOnly Property CheckedNodes() As TreeNodeCollection
        Get
            Return TreeViewBranch.CheckedNodes
        End Get
    End Property

    Public WriteOnly Property Check1Shop() As String
        Set(ByVal value As String)
            txtCheck1Shop.Text = value
        End Set
    End Property

    Public WriteOnly Property SelectShop() As String
        Set(ByVal value As String)
            Dim rDt As New DataTable
            rDt = Engine.Common.FunctionEng.GetShopRegion(value)
            If rDt.Rows.Count > 0 Then
                Dim Arr As New ArrayList
                Arr.Add(New ListItem(rDt.Rows(0)("region_name_en"), rDt.Rows(0)("region_id")))

                Session("Region") = Arr
                GenNode(Arr)
                For Each Node As TreeNode In TreeViewRegion.Nodes
                    For Each c As TreeNode In Node.ChildNodes
                        c.Checked = False
                        If c.Value = rDt.Rows(0)("region_id") Then
                            c.Checked = True
                        End If
                    Next
                Next

                For Each Node As TreeNode In TreeViewBranch.Nodes
                    For Each Parent As TreeNode In Node.ChildNodes
                        For Each Child As TreeNode In Parent.ChildNodes
                            If value = Child.Value Then
                                Child.Checked = True
                            End If
                        Next
                    Next
                Next
            End If
            rDt.Dispose()
        End Set
    End Property

    Sub bindRegion()
        Try
            Dim dt As New DataTable
            dt = Engine.Common.FunctionEng.ExecuteDataTable("select * from tb_region where active='Y'")
            If dt.Rows.Count > 0 Then
                Dim chkNodeAll As New TreeNode
                chkNodeAll.Text = "Select All Region"
                chkNodeAll.Value = "0"
                chkNodeAll.ShowCheckBox = True
                TreeViewRegion.Nodes.Clear()
                For Each Drow As DataRow In dt.Rows
                    Dim chkNodeShop As New TreeNode
                    chkNodeShop.Text = Drow.Item("Region_name_en")
                    chkNodeShop.Value = Drow.Item("ID")
                    chkNodeShop.SelectAction = TreeNodeSelectAction.Select
                    chkNodeShop.PopulateOnDemand = True
                    chkNodeShop.ShowCheckBox = True
                    chkNodeAll.ChildNodes.Add(chkNodeShop)
                Next
                dt.Dispose()
                chkNodeAll.Expanded = True
                TreeViewRegion.Nodes.Add(chkNodeAll)
                TreeViewRegion.DataBind()
            End If
        Catch ex As Exception

        End Try
    End Sub
    Function GenNode(ByVal ArrRegion As ArrayList) As Boolean
        Try
            Dim strSelected As String = ""
            For Each Node As TreeNode In TreeViewBranch.CheckedNodes()
                strSelected &= "|" & Node.Value & "|"
            Next
            ClearChildState()
            TreeViewBranch.Nodes.Clear()
            Dim uPara As CenParaDB.Common.UserProfilePara = Config.GetLogOnUser
            Dim lEng As New Engine.Common.LoginENG
            Dim shUserDT As New DataTable
            shUserDT = lEng.GetShopListByUser(uPara.USERNAME)

            For i As Integer = 0 To ArrRegion.Count - 1
                Dim chkRegion As New TreeNode
                chkRegion.Text = CType(ArrRegion.Item(i), System.Web.UI.WebControls.ListItem).Text
                chkRegion.Value = CType(ArrRegion.Item(i), System.Web.UI.WebControls.ListItem).Value
                chkRegion.ShowCheckBox = False


                Dim dtSi As New DataTable
                shUserDT.DefaultView.RowFilter = " Region_ID = '" & DirectCast(DirectCast(ArrRegion.Item(i), System.Object), System.Web.UI.WebControls.ListItem).Value & "'"
                dtSi = shUserDT.DefaultView.ToTable(True, "shop_size").Copy
                'dtSi = Engine.Common.FunctionEng.ExecuteDataTable("Select distinct Shop_Size From TB_SHOP Where active='Y' and Region_ID = '" & DirectCast(DirectCast(ArrRegion.Item(i), System.Object), System.Web.UI.WebControls.ListItem).Value & "'")
                If dtSi.Rows.Count > 0 Then
                    For Each DSize As DataRow In dtSi.Rows
                        Dim chkSizeShop As New TreeNode
                        chkSizeShop.Text = DSize.Item("Shop_Size")
                        chkSizeShop.Value = DSize.Item("Shop_Size")
                        chkSizeShop.ShowCheckBox = False
                        chkRegion.ChildNodes.Add(chkSizeShop)
                    Next
                End If


                Dim dtS As New DataTable
                dtS = lEng.GetShopListByUser(uPara.USERNAME)
                shUserDT.DefaultView.RowFilter = " Region_ID = '" & DirectCast(DirectCast(ArrRegion.Item(i), System.Object), System.Web.UI.WebControls.ListItem).Value & "'"
                'dtS = Engine.Common.FunctionEng.ExecuteDataTable("select * from TB_SHOP Where active='Y' and Region_ID = '" & DirectCast(DirectCast(ArrRegion.Item(i), System.Object), System.Web.UI.WebControls.ListItem).Value & "'")
                dtS = shUserDT.DefaultView.ToTable
                If dtS.Rows.Count > 0 Then
                    Dim strFind As String() = strSelected.Split("|")
                    For Each Drow As DataRow In dtS.Rows
                        Dim chkNodeShop As New TreeNode
                        chkNodeShop.Text = Drow.Item("shop_name_en")
                        chkNodeShop.Value = Drow("id")
                        chkNodeShop.SelectAction = TreeNodeSelectAction.Select
                        chkNodeShop.PopulateOnDemand = True
                        chkNodeShop.ShowCheckBox = True

                        If strFind.Contains(Drow.Item("shop_code")) Then chkNodeShop.Checked = True
                        For Each NodeSize As TreeNode In chkRegion.ChildNodes
                            If NodeSize.Value = Drow.Item("Shop_Size") Then
                                NodeSize.ChildNodes.Add(chkNodeShop)
                            End If
                        Next
                    Next
                End If
                dtS.Dispose()
                dtSi.Dispose()

                chkRegion.Expanded = True
                TreeViewBranch.Nodes.Add(chkRegion)
            Next
            shUserDT.Dispose()
            uPara = Nothing
            lEng = Nothing

            TreeViewBranch.DataBind()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Protected Sub TreeViewRegion_TreeNodeCheckChanged(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.TreeNodeEventArgs) 'Handles TreeViewRegion.TreeNodeCheckChanged
        Dim Arr As ArrayList = Session("Region")

        If e.Node.Checked = True Then
            If e.Node.Text <> "" And e.Node.Text IsNot Nothing Then
                If e.Node.Value = "0" Then
                    For Each Node As TreeNode In TreeViewRegion.Nodes
                        For Each Check As TreeNode In Node.ChildNodes
                            Check.Checked = True
                            If Arr.Contains(New ListItem(Check.Text, Check.Value)) = False And Check.Value <> "0" Then
                                Arr.Add(New ListItem(Check.Text, Check.Value))
                            End If
                        Next
                    Next
                Else
                    Arr.Add(New ListItem(e.Node.Text, e.Node.Value))
                End If
            End If
        Else
            If e.Node.Value = "0" Then
                For Each Node As TreeNode In TreeViewRegion.Nodes
                    For Each Check As TreeNode In Node.ChildNodes
                        Check.Checked = False
                        Arr.Remove(New ListItem(Check.Text, Check.Value))
                    Next
                Next
            Else
                Arr.Remove(New ListItem(e.Node.Text, e.Node.Value))
            End If
        End If
        Session("Region") = Arr
        GenNode(Arr)
    End Sub

    Protected Sub TreeViewRegion_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TreeViewRegion.SelectedNodeChanged
        Dim trv As TreeView = CType(sender, TreeView)
        If trv.SelectedNode.Checked = True Then trv.SelectedNode.Checked = False Else trv.SelectedNode.Checked = True
        TreeViewRegion_TreeNodeCheckChanged(sender, New System.Web.UI.WebControls.TreeNodeEventArgs(trv.SelectedNode))
    End Sub

    Function GetBranch() As ArrayList
        Dim arrList As New ArrayList
        For Each Node As TreeNode In TreeViewBranch.Nodes
            For Each Parent As TreeNode In Node.ChildNodes
                For Each Child As TreeNode In Parent.ChildNodes
                    If Child.Checked Then
                        arrList.Add(Child.Value & "|" & Child.Text)
                    End If
                Next
            Next
        Next
        Return arrList
    End Function

    Protected Sub TreeViewBranch_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TreeViewBranch.SelectedNodeChanged
        Dim trv As TreeView = CType(sender, TreeView)
        If trv.SelectedNode.Checked = True Then trv.SelectedNode.Checked = False Else trv.SelectedNode.Checked = True
        TreeViewBranch_TreeNodeCheckChanged(sender, New System.Web.UI.WebControls.TreeNodeEventArgs(trv.SelectedNode))

    End Sub

    Protected Sub TreeViewBranch_TreeNodeCheckChanged(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.TreeNodeEventArgs) 'Handles TreeViewBranch.TreeNodeCheckChanged
        If txtCheck1Shop.Text = "Y" Then
            Dim trv As TreeView = CType(sender, TreeView)
            For Each Node As TreeNode In trv.Nodes
                For Each N As TreeNode In Node.ChildNodes
                    For Each Check As TreeNode In N.ChildNodes
                        If e.Node.Value = Check.Value Then Check.Checked = True Else Check.Checked = False
                    Next
                Next
            Next
        End If

        RaiseEvent TreeViewBranchChange(GetBranch)
    End Sub

    Private Sub BindShop()
        Try
            Dim dtsize As New DataTable
            Dim dtshop As New DataTable
            dtsize = ExecuteDataTable("select isnull(shop_size,'S') as shop_size from TB_SHOP where active='Y' group by shop_size")
            TreeViewBranch.Nodes.Clear()
            If dtsize.Rows.Count > 0 Then
                Dim Allnode As New TreeNode
                Allnode.Text = "ALL Branch"
                Allnode.Value = "ALL"

                For node As Integer = 0 To dtsize.Rows.Count - 1
                    Dim tree As New TreeNode()
                    tree.Text = "Size " & dtsize.Rows(node).Item("shop_size").ToString.Trim
                    tree.Value = dtsize.Rows(node).Item("shop_size").ToString.Trim
                    tree.NavigateUrl = AppRelativeVirtualPath.ToString()
                    dtshop = ExecuteDataTable("select *,isnull(shop_size,'S') as shop_size1 from TB_SHOP where active='Y' and and isnull(shop_size,'S')='" & dtsize.Rows(node).Item("shop_size").ToString() & "'")
                    For parentnode As Integer = 0 To dtshop.Rows.Count - 1
                        Dim treeparent As New TreeNode()
                        treeparent.Text = dtshop.Rows(parentnode).Item("shop_name_en").ToString.Trim
                        treeparent.Value = dtshop.Rows(parentnode).Item("shop_code").ToString.Trim() & "," & _
                                            dtshop.Rows(parentnode).Item("shop_db_name").ToString.Trim() & "," & _
                                            dtshop.Rows(parentnode).Item("shop_db_userid").ToString.Trim() & "," & _
                                            dtshop.Rows(parentnode).Item("shop_db_pwd").ToString.Trim() & "," & _
                                            dtshop.Rows(parentnode).Item("shop_db_server").ToString.Trim() & "," & _
                                            dtshop.Rows(parentnode).Item("id").ToString.Trim()

                        treeparent.NavigateUrl = AppRelativeVirtualPath.ToString()
                        treeparent.SelectAction = TreeNodeSelectAction.Select
                        treeparent.Selected = True
                        treeparent.PopulateOnDemand = True
                        tree.ChildNodes.Add(treeparent)
                    Next
                    Allnode.ChildNodes.Add(tree)
                Next
            End If

        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Alert", "alert('BindShop Error !!,Please Check Function');", True)
        End Try
    End Sub



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            bindRegion()
            Dim Arr As New ArrayList
            Session("Region") = Arr
            GenNode(Arr)
        End If
    End Sub
    Public Sub ClearAllShop()
        bindRegion()
        Dim Arr As New ArrayList
        Session("Region") = Arr
        GenNode(Arr)
    End Sub
End Class
