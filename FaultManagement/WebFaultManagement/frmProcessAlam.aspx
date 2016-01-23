<%@ Page Title="" Language="VB" MasterPageFile="~/TemplateMaster.master" AutoEventWireup="false"
    CodeFile="frmProcessAlam.aspx.vb" Inherits="frmProcessAlam" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="Usercontrols/ctlByDate.ascx" TagName="ctlByDate" TagPrefix="uc1" %>
<%@ Register Src="Usercontrols/txtDate.ascx" TagName="txtDate" TagPrefix="uc2" %>
<%@ Register Assembly="MycustomDG" Namespace="MycustomDG" TagPrefix="Saifi" %>
<asp:Content ID="Content1" ContentPlaceHolderID="maincontent" runat="Server"  >
    <table style="width: 100%" onkeypress="return clickButton(event,this)">
        <tr>
            <td align="left" class="dvInnerHeader">
                <asp:Label ID="lblScreenName" runat="server" 
                    Text="Config &gt;&gt;Process Alarm"></asp:Label>
            </td>
        </tr>
        <tr align="Center">
            <td>
                <table style="width: 75%">
                    <tr>
                        <td colspan="4">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td class="section_header" colspan="4">
                                        Filter
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td width="25%" class="dvtCellInfo">
                            Host
                        </td>
                        <td align="left" width="25%">
                            <asp:TextBox ID="txtHost" runat="server"></asp:TextBox>
                        </td>
                        <td width="25%" class="dvtCellInfo">
                            Type
                        </td>
                        <td align="left" width="25%">
                            <asp:TextBox ID="txtType" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="dvtCellInfo">
                            Alarm Code
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtAlamCode" runat="server"></asp:TextBox>
                        </td>
                        <td class="dvtCellInfo">
                            sysLocation
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtsysLocation" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="dvtCellInfo">
                            Description
                        </td>
                        <td align="left" colspan="3">
                            <asp:TextBox ID="txtDescription" runat="server" Width="250px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="dvtCellInfo">
                            Severity&nbsp;
                        </td>
                        <td align="left" colspan="3" class="dvtCellInfo">
                            <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" ID="rdoSeverity">
                                <asp:ListItem>Minor</asp:ListItem>
                                <asp:ListItem>Major</asp:ListItem>
                                <asp:ListItem>Critical</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td class="dvtCellInfo">
                            Date
                        </td>
                        <td align="left" colspan="3" width="75%">
                            <uc2:txtDate ID="txtDateFrom" runat="server" />
                            &nbsp;To&nbsp<uc2:txtDate ID="txtDateTo" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="dvtCellInfo">
                            Show Alarm Interval
                        </td>
                        <td align="left" colspan="3" width="75%">
                            <asp:DropDownList ID="ddlShowAlarmInterval" runat="server" Width="83px" AutoPostBack="true"  >
                                <asp:ListItem Text="5" Value="5" Selected></asp:ListItem>
                                <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                <asp:ListItem Text="30" Value="30"></asp:ListItem>
                            </asp:DropDownList>
                            Second
                            <asp:Label ID="lblRefreshTime" runat="server" CssClass="zHidden" ></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td align="center">
                            <asp:Button ID="btnSearch" runat="server" Text="Search" Width="100px" />
                            &nbsp;
                            <asp:Button ID="btnClear" runat="server" Text="Clear" Width="100px" />
                            
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Timer ID="Timer1" runat="server" Interval="5000">
                </asp:Timer>
                
            </td>
        </tr>
        <tr align="left">
            <td>
                <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" 
                    Width="100%" AutoPostBack="true" >
                    <asp:TabPanel runat="server" HeaderText="Show Alarm" ID="TabPanel1">
                        <HeaderTemplate>
                            Show Alarm
                        </HeaderTemplate>
                        <ContentTemplate>
                            <Saifi:MyDg ID="Mydg1" runat="server" AllowPaging="true" AutoGenerateColumns="False"
                                CssClass="Grid_inq" ImageFirst="/imgs/nav_left.gif" ImageLast="/imgs/nav_right.gif"
                                ImageNext="/imgs/bulletr.gif" ImagePrevious="/imgs/bulletl.gif" PageSize="10"
                                ShowFirstAndLastImage="False" ShowPreviousAndNextImage="False" Width="100%">
                                <Columns>
                                    <asp:TemplateColumn HeaderText="Alarm Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_AlamCode" runat="server" Style="text-align: left; width: 100%;"
                                                Text='<%#DataBinder.Eval(Container.DataItem, "AlarmCode")%>'></asp:Label>
                                            <asp:Label ID="lbl_AlamMethod" runat="server" Style="text-align: left; width: 100%;"
                                                Text='<%#DataBinder.Eval(Container.DataItem, "AlarmMethod")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="lbl_Severity" runat="server" Style="text-align: left; width: 100%;"
                                                Text='<%#DataBinder.Eval(Container.DataItem, "Severity")%>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="11%" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Host Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_ServerName" runat="server" Style="text-align: left; width: 100%;"
                                                Text='<%#DataBinder.Eval(Container.DataItem, "ServerName")%>'></asp:Label></ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="11%" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_SpecificProblem" runat="server" Style="text-align: left; width: 100%;"
                                                Text='<%#DataBinder.Eval(Container.DataItem, "SpecificProblem")%>'></asp:Label></ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="11%" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Host IP">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_HostIP" runat="server" Style="text-align: left; width: 100%;"
                                                Text='<%#DataBinder.Eval(Container.DataItem, "HostIP")%>'></asp:Label></ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="11%" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="sysLocation">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_SysLocation" runat="server" Style="text-align: left; width: 100%;"
                                                Text='<%#DataBinder.Eval(Container.DataItem, "SysLocation")%>'></asp:Label></ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="11%" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="No. Of Update">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_AlarmQty" runat="server" Style="text-align: left; width: 100%;"
                                                Text='<%#DataBinder.Eval(Container.DataItem, "AlarmQty")%>'></asp:Label></ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="11%" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Create Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_CreateDate" runat="server" Style="text-align: left; width: 100%;"
                                                Text='<%#DataBinder.Eval(Container.DataItem, "CreateDate")%>'></asp:Label></ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="11%" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Update Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_UpdateDate" runat="server" Style="text-align: left; width: 100%;"
                                                Text='<%#DataBinder.Eval(Container.DataItem, "UpdateDate")%>'></asp:Label></ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="11%" />
                                    </asp:TemplateColumn>
                                </Columns>
                                <HeaderStyle CssClass="Grid_inqHeader" />
                                <PagerStyle Mode="NumericPages" />
                            </Saifi:MyDg></ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel runat="server" HeaderText="Show History" ID="TabPanel2">
                        <HeaderTemplate>
                            Show History
                        </HeaderTemplate>
                        <ContentTemplate>
                            <Saifi:MyDg ID="Mydg2" runat="server" AllowPaging="true" AutoGenerateColumns="False"
                                CssClass="Grid_inq" ImageFirst="/imgs/nav_left.gif" ImageLast="/imgs/nav_right.gif"
                                ImageNext="/imgs/bulletr.gif" ImagePrevious="/imgs/bulletl.gif" PageSize="10"
                                ShowFirstAndLastImage="False" ShowPreviousAndNextImage="False" Width="100%">
                                <Columns>
                                    <asp:TemplateColumn HeaderText="Alarm Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_AlamCode2" runat="server" Style="text-align: left; width: 100%;"
                                                Text='<%#DataBinder.Eval(Container.DataItem, "AlarmCode")%>'></asp:Label><asp:Label
                                                    ID="lbl_AlamMethod2" runat="server" Style="text-align: left; width: 100%;" Text='<%#DataBinder.Eval(Container.DataItem, "AlarmMethod")%>'
                                                    Visible="false"></asp:Label></ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="11%" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Host Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_ServerName2" runat="server" Style="text-align: left; width: 100%;"
                                                Text='<%#DataBinder.Eval(Container.DataItem, "ServerName")%>'></asp:Label></ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="11%" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_SpecificProblem2" runat="server" Style="text-align: left; width: 100%;"
                                                Text='<%#DataBinder.Eval(Container.DataItem, "SpecificProblem")%>'></asp:Label></ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="11%" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Host IP">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_HostIP2" runat="server" Style="text-align: left; width: 100%;"
                                                Text='<%#DataBinder.Eval(Container.DataItem, "HostIP")%>'></asp:Label></ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="11%" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="sysLocation">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_SysLocation2" runat="server" Style="text-align: left; width: 100%;"
                                                Text='<%#DataBinder.Eval(Container.DataItem, "SysLocation")%>'></asp:Label></ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="11%" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="No. Of Update">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_AlarmQty2" runat="server" Style="text-align: left; width: 100%;"
                                                Text='<%#DataBinder.Eval(Container.DataItem, "AlarmQty")%>'></asp:Label></ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="11%" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Create Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_CreateDate2" runat="server" Style="text-align: left; width: 100%;"
                                                Text='<%#DataBinder.Eval(Container.DataItem, "CreateDate")%>'></asp:Label></ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="11%" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Update Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_UpdateDate2" runat="server" Style="text-align: left; width: 100%;"
                                                Text='<%#DataBinder.Eval(Container.DataItem, "UpdateDate")%>'></asp:Label></ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="11%" />
                                    </asp:TemplateColumn>
                                </Columns>
                                <HeaderStyle CssClass="Grid_inqHeader" />
                                <PagerStyle Mode="NumericPages" />
                            </Saifi:MyDg></ContentTemplate>
                    </asp:TabPanel>
                </asp:TabContainer>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
