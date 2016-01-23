<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlBranchSelected.ascx.vb" Inherits="UserControls_ctlBranchSelected" %>
<script type="text/javascript">
    function OnCheckBoxCheckChanged(evt) {
        var src = window.event != window.undefined ? window.event.srcElement : evt.target;
        var isChkBoxClick = (src.tagName.toLowerCase() == "input" && src.type == "checkbox");
        if (isChkBoxClick) {
            var parentTable = GetParentByTagName("table", src);
            var nxtSibling = parentTable.nextSibling;
            
            if (nxtSibling && nxtSibling.nodeType == 1)//check if nxt sibling is not null & is an element node
            {
                if (nxtSibling.tagName.toLowerCase() == "div") //if node has children
                {
                    //check or uncheck children at all levels
                    CheckUncheckChildren(parentTable.nextSibling, src.checked);
                }
            }
            //check or uncheck parents at all levels
            
            CheckUncheckParents(src, src.checked);
        }
    }
    function CheckUncheckChildren(childContainer, check) {
        var childChkBoxes = childContainer.getElementsByTagName("input");
        var childChkBoxCount = childChkBoxes.length;
        for (var i = 0; i < childChkBoxCount; i++) {
            childChkBoxes[i].checked = check;
        }
    }
    function CheckUncheckParents(srcChild, check) {
        var parentDiv = GetParentByTagName("div", srcChild);
        var parentNodeTable = parentDiv.previousSibling;

        if (parentNodeTable) {
            var checkUncheckSwitch;

            if (check) //checkbox checked
            {
                var isAllSiblingsChecked = AreAllSiblingsChecked(srcChild);
                if (isAllSiblingsChecked)
                    checkUncheckSwitch = true;
                else
                    return; //do not need to check parent if any(one or more) child not checked
            }
            else //checkbox unchecked
            {
                checkUncheckSwitch = false;
            }

            var inpElemsInParentTable = parentNodeTable.getElementsByTagName("input");
            if (inpElemsInParentTable.length > 0) {
                var parentNodeChkBox = inpElemsInParentTable[0];
                parentNodeChkBox.checked = checkUncheckSwitch;
                //do the same recursively
                CheckUncheckParents(parentNodeChkBox, checkUncheckSwitch);
            }
        }
    }
    function AreAllSiblingsChecked(chkBox) {
        var parentDiv = GetParentByTagName("div", chkBox);
        var childCount = parentDiv.childNodes.length;
        for (var i = 0; i < childCount; i++) {
            if (parentDiv.childNodes[i].nodeType == 1) //check if the child node is an element node
            {
                if (parentDiv.childNodes[i].tagName.toLowerCase() == "table") {
                    var prevChkBox = parentDiv.childNodes[i].getElementsByTagName("input")[0];
                    //if any of sibling nodes are not checked, return false
                    if (!prevChkBox.checked) {
                        return false;
                    }
                }
            }
        }
        return true;
    }

   
</script>

<tr>
    <td class="dvInnerHeader">
        Branch
    </td>
</tr>
<tr>
    <td >
        <asp:UpdatePanel ID="pnlBranch" runat="server" >
            <ContentTemplate>
                <asp:TextBox ID="txtCheck1Shop" runat="server" Text="N" Visible="false"></asp:TextBox>

                <asp:Panel runat="server" ScrollBars="Vertical" Width="80%" ID="Panel1">
                    <script type="text/javascript">
                        function postbackOnCheck() {
                            var o = window.event.srcElement;
                            if (o.tagName == 'INPUT' && o.type == 'checkbox' && o.name != null && o.name.indexOf('CheckBox') > -1)
                            { __doPostBack("", ""); }
                        }
                     </script>
                    <table width="100%">
                        <tr>
                            <td valign="top">
                                <asp:TreeView ID="TreeViewRegion" runat="server" CssClass="Treeview"
                                    NodeStyle-ChildNodesPadding="1px" onclick="javascript:postbackOnCheck()" 
                                    OnTreeNodeCheckChanged="TreeViewRegion_TreeNodeCheckChanged" 
                                    Height="150px" SkipLinkText="" Width="300px" 
                                    BackColor="White"                                                     
                                    BorderColor="Silver" 
                                   >
                                    <NodeStyle CssClass="Treeview"  />
                                    <ParentNodeStyle CssClass="Treeview" />
                                    <RootNodeStyle CssClass="Treeview" />
                                    <SelectedNodeStyle CssClass="Treeview" />
                                  
                                </asp:TreeView>
                            </td>
                            <td valign="top">
                                <asp:TreeView ID="TreeViewBranch" runat="server" CssClass="Treeview" 
                                    NodeStyle-ChildNodesPadding="1px" onclick="javascript:postbackOnCheck();"  
                                    OnTreeNodeCheckChanged="TreeViewBranch_TreeNodeCheckChanged" Height="150px"  
                                    SkipLinkText="" Width="300px" BackColor="White" BorderColor="Silver" >
                                    <NodeStyle CssClass="Treeview" />
                                    <ParentNodeStyle CssClass="Treeview"  />
                                    <RootNodeStyle CssClass="Treeview"  />
                                    <SelectedNodeStyle CssClass="Treeview"  />
                                </asp:TreeView>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </td>
</tr>
<tr><td>&nbsp;</td></tr>