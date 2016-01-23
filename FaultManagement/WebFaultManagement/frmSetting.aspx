<%@ Page Title="" Language="VB" MasterPageFile="~/TemplateMaster.master" AutoEventWireup="false"
    CodeFile="frmSetting.aspx.vb" Inherits="frmSetting" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/Usercontrols/ctlAramServity.ascx" TagName="ctlAramServity" TagPrefix="uc1" %>
<%@ Register Assembly="MycustomDG" Namespace="MycustomDG" TagPrefix="Saifi" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="maincontent" runat="Server">

    <script type="text/javascript">
        function stopPropagation_ComboBoxDrive(e) {
            e.cancelBubble = true;
            if (e.stopPropagation) {
                e.stopPropagation();
            }
        }

        function changing_ComboBoxDrive(item) {
            return false;
        }

        function CheckSelectDrive(RadComboBox) {
            var lblText;
            lblText = document.getElementById("<%= lblText_OutDrive.ClientID %>");

            var count = 0;
            var Name = '';
            var IDS = '';
            for (i = 0; i < document.forms[0].elements.length; i++) {
                if ((document.forms[0].elements[i].type == 'checkbox') && (document.forms[0].elements[i].name.indexOf(RadComboBox) > -1)) {

                    if (document.forms[0].elements[i].checked) {
                        count += 1;
                        IDS = document.forms[0].elements[i].id;
                        IDS = IDS.replace("cbSelect_InDrive", "lblText_InDrive");
                        if (document.getElementById(IDS) != null) {
                            // alert('Yes');
                            if (Name == '') {
                                Name += document.getElementById(IDS).innerText;
                            } else {
                                Name += ", " + document.getElementById(IDS).innerText;
                            }
                        } else {
                            // alert('No');
                            if (Name == '') {
                                Name += "Any";
                            } else {
                                Name += ", Any";
                            }
                        }
                        //Name += document.getElementById(IDS).innerText;  // or +'<br />' if inside HTML
                    }
                }
            }

            lblText.innerText = Name;
        }
    </script>

    <table style="width: 100%">
        <tr>
            <td align="left" class="dvInnerHeader">
                <asp:Label ID="lblScreenName" runat="server" Text="Config &gt;&gt; Hardware"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left" style="height: 18px">
                <table style="width: 100%">
                    <tr>
                        <td width="20%" class="dvtCellInfo">
                            Shop Name
                        </td>
                        <td width="80%" class="dvtCellInfo">
                            <asp:DropDownList ID="ddlShopName" runat="server" Width="200px" AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="dvtCellInfo">
                            Server Name
                        </td>
                        <td class="dvtCellInfo">
                            <asp:TextBox ID="txtServerName" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="dvtCellInfo">
                            IP Address
                        </td>
                        <td class="dvtCellInfo">
                            <asp:TextBox ID="txtIpAddress" runat="server" Width="200px"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtIpAddress"
                                Display="Dynamic" ErrorMessage="Incorrect IP Address format" ValidationExpression="^(\d|[1-9]\d|1\d\d|2([0-4]\d|5[0-5]))\.(\d|[1-9]\d|1\d\d|2([0-4]\d|5[0-5]))\.(\d|[1-9]\d|1\d\d|2([0-4]\d|5[0-5]))\.(\d|[1-9]\d|1\d\d|2([0-4]\d|5[0-5]))$"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="left">
            </td>
        </tr>
        <tr>
            <td align="left" style="margin-left: 40px">
                <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="100%">
                    <asp:TabPanel runat="server" HeaderText="CPU" ID="TabPanel1">
                        <HeaderTemplate>
                            CPU
                        </HeaderTemplate>
                        <ContentTemplate>
                            <table style="width: 100%">
                                <tr>
                                    <td>
                                        <uc1:ctlAramServity ID="ctlAramServity1" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="btnSaveCPU" runat="server" Text="Save" Width="100px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel runat="server" HeaderText="RAM" ID="TabPanel2">
                        <HeaderTemplate>
                            RAM
                        </HeaderTemplate>
                        <ContentTemplate>
                            <table style="width: 100%">
                                <tr>
                                    <td>
                                        <uc1:ctlAramServity ID="ctlAramServity2" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="btnSaveRAM" runat="server" Text="Save" Width="100px" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel runat="server" HeaderText="HDD" ID="TabPanel3">
                        <HeaderTemplate>
                            HDD
                        </HeaderTemplate>
                        <ContentTemplate>
                            <table style="width: 100%">
                                <tr>
                                    <td>
                                        <table style="width: 100%">
                                            <tr>
                                                <td width="20%" class="dvtCellInfo">
                                                    Drive Later
                                                </td>
                                                <td width="80%" class="dvtCellInfo">
                                                    <asp:Label ID="lblText_OutDrive" runat="server"></asp:Label><table cellpadding="0"
                                                        cellspacing="0" width="30%">
                                                        <tr>
                                                            <td style="border-style: none">
                                                                <telerik:RadComboBox ID="RadComboBox_Drive" runat="server" DataTextField="Name" DataValueField="Name"
                                                                    Skin="Hay" Filter="Contains" Height="150px" Width="30%" OnClientSelectedIndexChanging="changing_ComboBoxDrive"
                                                                    HighlightTemplatedItems="True">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="cbSelect_InDrive" runat="server" Text="<%# bind('Name') %>" onclick="CheckSelectDrive('RadComboBox_Drive');stopPropagation_ComboBoxDrive(event);" /><div
                                                                            style="display: none">
                                                                            <asp:Label ID="lblText_InDrive" runat="server" Text="<%# bind('Name') %>"></asp:Label></div>
                                                                    </ItemTemplate>
                                                                    <CollapseAnimation Duration="200" Type="OutQuint"></CollapseAnimation>
                                                                </telerik:RadComboBox>
                                                            </td>
                                                            <td style="border-style: none">
                                                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="RadComboBox_Drive"
                                                                    Enabled="False" ErrorMessage="**" Operator="NotEqual"></asp:CompareValidator>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <uc1:ctlAramServity ID="ctlAramServity3" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="btnSaveHDD" runat="server" Text="Save" Width="100px" />
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td align="center">
                                        <Saifi:MyDg ID="Mydg1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                            CssClass="Grid_inq" ImageFirst="/imgs/nav_left.gif" ImageLast="/imgs/nav_right.gif"
                                            ImageNext="/imgs/bulletr.gif" ImagePrevious="/imgs/bulletl.gif" ShowFirstAndLastImage="False"
                                            ShowPreviousAndNextImage="False" Width="500px">
                                            <AlternatingItemStyle CssClass="Grid_inqAltItem" />
                                            <Columns>
                                                <asp:TemplateColumn HeaderText="Drive Later">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Name" runat="server" Style="text-align: left; width: 100%;" Text='<%#DataBinder.Eval(Container.DataItem, "Name")%>'></asp:Label></ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="200px" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Edit" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnEdit" runat="server" CommandName="Edit" ImageUrl="~/images/edit_images.jpg"
                                                            ToolTip="Edit" /></ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                </asp:TemplateColumn>
                                            </Columns>
                                            <HeaderStyle CssClass="Grid_inqHeader" />
                                            <ItemStyle CssClass="Grid_inqItem" />
                                            <PagerStyle Mode="NumericPages" Visible="False" />
                                        </Saifi:MyDg>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:TabPanel>
                </asp:TabContainer>
            </td>
        </tr>
    </table>
    <%-- <asp:Button ID="Button1" runat="server" Text="Save" Width="100px" />--%>
</asp:Content>
