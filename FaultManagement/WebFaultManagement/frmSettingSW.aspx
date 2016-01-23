<%@ Page Title="" Language="VB" MasterPageFile="~/TemplateMaster.master" AutoEventWireup="false"
    CodeFile="frmSettingSW.aspx.vb" Inherits="frmSettingSW" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="Usercontrols/ctlAramServitySW.ascx" TagName="ctlAramServitySW"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="maincontent" runat="Server">
    <table style="width: 100%">
        <tr>
            <td align="left" class="dvInnerHeader">
                <asp:Label ID="lblScreenName" runat="server" Text="Config &gt;&gt; Software"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="100%">
                    <asp:TabPanel runat="server" HeaderText="Service" ID="TabPanel1">
                        <ContentTemplate>
                            <table style="width: 100%">
                                <tr>
                                    <td>
                                        <uc1:ctlAramServitySW ID="ctlAramServitySW1" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="btnSaveService" runat="server" Text="Save" Width="100px" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="Process">
                        <ContentTemplate>
                            <table style="width: 100%">
                                <tr>
                                    <td>
                                        <uc1:ctlAramServitySW ID="ctlAramServitySW2" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="btnSaveProcess" runat="server" Text="Save" Width="100px" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="TabPanel3" runat="server" HeaderText="Web">
                        <ContentTemplate>
                            <table style="width: 100%">
                                <tr>
                                    <td>
                                        <uc1:ctlAramServitySW ID="ctlAramServitySW3" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="btnSaveWeb" runat="server" Text="Save" Width="100px" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:TabPanel>
                </asp:TabContainer>
            </td>
        </tr>
    </table>
</asp:Content>
