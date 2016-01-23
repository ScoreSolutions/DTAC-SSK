<%@ Page Title="" Language="VB" MasterPageFile="~/TemplateMaster.master" AutoEventWireup="false" CodeFile="frmMSTDTACBiller.aspx.vb" Inherits="frmMSTDTACBiller" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="DotNetSources.Web.UI" Namespace="DotNetSources.Web.UI" TagPrefix="cc1" %>
<%@ Register Assembly="MycustomDG" Namespace="MycustomDG" TagPrefix="Saifi" %>
<%@ Register src="UserControls/ctlBrowseFile.ascx" tagname="ctlBrowseFile" tagprefix="uc1" %>
<%@ Register src="UserControls/cmbComboBox.ascx" tagname="cmbComboBox" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="maincontent" Runat="Server">
    <table style="width: 900px" border="0" cellpadding="0" cellspacing="0" >
        <tr>
            <td class="tableHeader">
                <asp:Label ID="Label2" runat="server" Text="Biller"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="height: 30px; text-align: center">
                <cc1:customupdateprogress id="progress" runat="server" progressimage="~/images/progress.gif"  />
            </td>
        </tr>
        <tr>
            <td class="dvInnerHeader">
                Configuration
            </td>
        </tr>
        <tr>
            <td>
                <table style="width:900px;">
                    <tr>
                        <td style="height: 27px; width: 201px;" class="dvtCellLabel">* Biller Code :</td>
                        <td style="width: 19px" class="dvtCellInfo">
                            <asp:TextBox ID="txtBillerCode" runat="server" MaxLength="20"
                                Width="150px" Height="20px"></asp:TextBox>
                            
                        </td>
                        <td style="width: 196px; height: 27px" class="dvtCellLabel">&nbsp;</td>
                        <td class="dvtCellInfo" style="width: 150px;">&nbsp;</td>
                        <td class="dvtCellInfo" style="width: 150px;">&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="height: 27px; width: 201px;" class="dvtCellLabel">
                            * Biller Name :
                        </td>
                        <td style="width: 19px" class="dvtCellInfo" colspan="3" >
                            <asp:TextBox ID="txtBillerName" runat="server"  MaxLength="50" 
                                Width="150px" Height="20px"></asp:TextBox>
                            <asp:TextBox ID="txt_id" runat="server" BorderStyle="None" ForeColor="White" Visible="False" Text="0" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 27px; width: 201px;" class="dvtCellLabel">
                            * Biller Type :
                        </td>
                        <td style="width: 19px" class="dvtCellInfo" colspan="3" 
                            <uc2:cmbComboBox ID="cmbBillerType" runat="server" Width="250px" />
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 27px; width: 201px;" class="dvtCellLabel">
                            * Image Logo :
                        </td>
                        <td style="width: 19px" class="dvtCellInfo" colspan="3" >
                            <uc1:ctlBrowseFile ID="ctlBrowseFile1" runat="server" VisibleUploadButton="false" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 201px; height: 27px" class="dvtCellLabel">
                             &nbsp;
                        </td>
                        <td style="width: 19px" class="dvtCellInfo">
                            <asp:CheckBox ID="chk_active" runat="server" Text="Active" Checked="true" />
                        </td>
                        <td style="width: 196px; height: 27px" class="dvtCellLabel">
                            &nbsp;
                        </td>
                        <td style="width: 150px; height: 27px" class="dvtCellLabel">
                            &nbsp;
                        </td>
                        <td style="width: 150px; height: 27px" class="dvtCellLabel">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td colspan="3">
                            <asp:Button ID="CmdSave" runat="server" Text="Save" Width="60px" />&nbsp;
                            <asp:Button ID="CmdClear" runat="server" Text="Clear" Width="60px" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr><td colspan='2'>&nbsp;</td></tr>
        <tr><td colspan='2'>&nbsp;</td></tr>
        <tr>
            <td colspan='2'>
                <table cellpadding="0" cellspacing="0" style="width: 100%;">
                    <tr>
                        <td style="height: 14px; width: 100%; text-align: right;" class="dvtCellInfo" align="right">
                            <asp:Panel ID="Panel3" runat="server" Width="100%" Height="20px" >
                                <asp:TextBox ID="txt_search" runat="server" class="detailedViewTextBox" MaxLength="100"
                                    onblur="this.className='detailedViewTextBox'" onfocus="this.className='detailedViewTextBoxOn'"
                                    Width="150px" Height="20px"></asp:TextBox>
                                <asp:ImageButton ID="CmdSearch" runat="server" Style="margin-left: 0px" Width="16px"
                                    ImageUrl="~/images/search_lense.png" />
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 14px; width: 675px; text-align: right;" class="dvtCellInfo">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 14px; width: 675px; text-align: right;" class="dvtCellInfo">
                            <saifi:mydg id="dgvService" runat="server" allowpaging="true" autogeneratecolumns="False"
                                    cssclass="Grid_inq" imagefirst="/imgs/nav_left.gif" imagelast="/imgs/nav_right.gif"
                                    imagenext="/imgs/bulletr.gif" imageprevious="/imgs/bulletl.gif" showfirstandlastimage="False"
                                    showpreviousandnextimage="False" width="900px">
                                <AlternatingItemStyle CssClass="Grid_inqAltItem" />
                                <ItemStyle CssClass="Grid_inqItem" />
                                <PagerStyle Mode="NumericPages" CssClass="Grid_inqPager"></PagerStyle>
                                <Columns>
                                    <asp:TemplateColumn HeaderText="Code" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="70px">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_id" runat="server" Style="text-align: left; width: 100%;" Visible="false"
                                                Text='<%#DataBinder.Eval(Container.DataItem, "id")%>'></asp:Label>
                                            <asp:Label ID="lbl_item_code" runat="server" Style="text-align: center; width: 100%;"
                                                Text='<%#DataBinder.Eval(Container.DataItem, "biller_code")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="70px" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200px">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_item_name" runat="server" Style="text-align: left; width: 100%;"
                                                Text='<%#DataBinder.Eval(Container.DataItem, "biller_name")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="200px" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Active" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="70px">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ChkActie" runat="server" Enabled="false" Checked='<%#DataBinder.Eval(Container.DataItem, "active_status") = "Y" %>' />
                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit_images.jpg"
                                                CommandName="Edit" ToolTip="Edit" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="70px" />
                                    </asp:TemplateColumn>
                                </Columns>
                                <HeaderStyle CssClass="Grid_inqHeader" />
                            </saifi:mydg>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr><td colspan='2'>&nbsp;</td></tr>
        <tr><td colspan='2'>&nbsp;</td></tr>
</table>
</asp:Content>

