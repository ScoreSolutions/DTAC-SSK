<%@ Page Language="VB" MasterPageFile="~/TemplateMaster.master" AutoEventWireup="false" EnableEventValidation="false"
    CodeFile="frmMSTSoftwareDTACSetupKiosk.aspx.vb" Inherits="frmMSTSoftwareDTACSetupKiosk"  %>

<%@ Register Assembly="DotNetSources.Web.UI" Namespace="DotNetSources.Web.UI" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="MycustomDG" namespace="MycustomDG" tagprefix="Saifi" %>
<%@ Register src="UserControls/ctlBrowseFile.ascx" tagname="ctlBrowseFile" tagprefix="uc1" %>
<%@ Register src="UserControls/ctlShopSelected.ascx" tagname="ctlShopSelected" tagprefix="uc2" %>
<%@ Register src="UserControls/ctlBrowseFileStream.ascx" tagname="ctlBrowseFileStream" tagprefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="maincontent" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
        <tr>
            <td class="tableHeader">
                <asp:Label ID="lblScreenName" runat="server" Text="Kiosk Setup"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="height: 19px" align="left" >
                <table style="width: 100%;">
                    <tr>
                        <td class="dvInnerHeader">
                            Capture Image
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 216px; height: 27px" class="dvtCellLabel">
                                        Camera Index :
                                    </td>
                                    <td style="width: 231px; height: 27px" class="dvtCellInfo" >
                                        <asp:TextBox ID="txtCameraIndex" runat="server" Height="20px" class="TextView"
                                                OnKeyPress="txtKeyPress(event)" onKeyDown="CheckKeyBackSpace(event)" ></asp:TextBox>
                                        <asp:NumericUpDownExtender ID="numtxtCameraIndex" runat="server"
                                            Enabled="True" Maximum="1200" Minimum="0"  RefValues="" 
                                            ServiceDownMethod="" ServiceDownPath="" 
                                            ServiceUpMethod="" Tag="1" TargetButtonDownID="" TargetButtonUpID="" TargetControlID="txtCameraIndex"
                                            Width="60" >
                                        </asp:NumericUpDownExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="dvtCellLabel">
                                        Capture Image Width :
                                    </td>
                                    <td class="dvtCellInfo" >
                                        <asp:TextBox ID="txtCaptureWidth" runat="server" Height="20px" class="detailedViewTextBox"
                                                OnKeyPress="ChkMinusInt(this,event)" onKeyDown="CheckKeyNumber(event)"  ></asp:TextBox>
                                        
                                        Pixel
                                    </td>
                                </tr>
                                <tr>
                                    <td class="dvtCellLabel">
                                        Capture Image Height :
                                    </td>
                                    <td class="dvtCellInfo" >
                                        <asp:TextBox ID="txtCaptureHeight" runat="server" Height="20px" class="detailedViewTextBox"
                                                OnKeyPress="ChkMinusInt(this,event)" onKeyDown="CheckKeyNumber(event)"  ></asp:TextBox>
                                        Pixel
                                    </td>
                                </tr>
                                <tr>
                                    <td class="dvtCellLabel">
                                        Image Resolution :
                                    </td>
                                    <td class="dvtCellInfo" >
                                        <asp:TextBox ID="txtCaptureResolution" runat="server" Height="20px" class="TextView"
                                                OnKeyPress="txtKeyPress(event)" onKeyDown="CheckKeyBackSpace(event)" ></asp:TextBox>
                                        DPI
                                    </td>
                                </tr>
                                <tr>
                                    <td class="dvtCellLabel">
                                        Enable Function :
                                    </td>
                                    <td  class="dvtCellInfo" >
                                        <asp:CheckBox ID="chkCaptureEnable" runat="server" Text="Enable" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="dvInnerHeader">
                            Wait Time
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 216px; height: 27px" class="dvtCellLabel">
                                        Wait Time ScreenSaver :
                                    </td>
                                    <td style="width: 231px; height: 27px" class="dvtCellInfo" >
                                        <asp:TextBox ID="txtWaitTimeScreenSaver" runat="server" Height="20px" class="TextView"
                                                OnKeyPress="txtKeyPress(event)" onKeyDown="CheckKeyBackSpace(event)" ></asp:TextBox>
                                        <asp:NumericUpDownExtender ID="NumericUpDownExtender1" runat="server"
                                            Enabled="True" Maximum="1200" Minimum="0"  RefValues="" 
                                            ServiceDownMethod="" ServiceDownPath="" 
                                            ServiceUpMethod="" Tag="1" TargetButtonDownID="" TargetButtonUpID="" TargetControlID="txtWaitTimeScreenSaver"
                                            Width="60" >
                                        </asp:NumericUpDownExtender>
                                        Sec
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 216px; height: 27px" class="dvtCellLabel">
                                        Wait Time Session Expired :
                                    </td>
                                    <td style="width: 231px; height: 27px" class="dvtCellInfo" >
                                        <asp:TextBox ID="txtWaitTimeSessionExpired" runat="server" Height="20px" class="TextView"
                                                OnKeyPress="txtKeyPress(event)" onKeyDown="CheckKeyBackSpace(event)" ></asp:TextBox>
                                        <asp:NumericUpDownExtender ID="NumericUpDownExtender2" runat="server"
                                            Enabled="True" Maximum="1200" Minimum="0"  RefValues="" 
                                            ServiceDownMethod="" ServiceDownPath="" 
                                            ServiceUpMethod="" Tag="1" TargetButtonDownID="" TargetButtonUpID="" TargetControlID="txtWaitTimeSessionExpired"
                                            Width="60" >
                                        </asp:NumericUpDownExtender>
                                        Sec
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 216px; height: 27px" class="dvtCellLabel">
                                        Wait Time for Payment :
                                    </td>
                                    <td style="width: 231px; height: 27px" class="dvtCellInfo" >
                                        <asp:TextBox ID="txtWaitTimeForPayment" runat="server" Height="20px" class="TextView"
                                                OnKeyPress="txtKeyPress(event)" onKeyDown="CheckKeyBackSpace(event)" ></asp:TextBox>
                                        <asp:NumericUpDownExtender ID="NumericUpDownExtender3" runat="server"
                                            Enabled="True" Maximum="1200" Minimum="0"  RefValues="" 
                                            ServiceDownMethod="" ServiceDownPath="" 
                                            ServiceUpMethod="" Tag="1" TargetButtonDownID="" TargetButtonUpID="" TargetControlID="txtWaitTimeForPayment"
                                            Width="60" >
                                        </asp:NumericUpDownExtender>
                                        Sec
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 216px; height: 27px" class="dvtCellLabel">
                                        Wait Time Thank You :
                                    </td>
                                    <td style="width: 231px; height: 27px" class="dvtCellInfo" >
                                        <asp:TextBox ID="txtWaitTimeThankYou" runat="server" Height="20px" class="TextView"
                                                OnKeyPress="txtKeyPress(event)" onKeyDown="CheckKeyBackSpace(event)" ></asp:TextBox>
                                        <asp:NumericUpDownExtender ID="NumericUpDownExtender4" runat="server"
                                            Enabled="True" Maximum="1200" Minimum="0"  RefValues="" 
                                            ServiceDownMethod="" ServiceDownPath="" 
                                            ServiceUpMethod="" Tag="1" TargetButtonDownID="" TargetButtonUpID="" TargetControlID="txtWaitTimeThankYou"
                                            Width="60" >
                                        </asp:NumericUpDownExtender>
                                        Sec
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="dvInnerHeader">
                            Branch Information
                        </td>
                    </tr>
                    <tr>
                        <td >
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 216px; height: 27px" class="dvtCellLabel">
                                        Branch Name Thai :
                                    </td>
                                    <td style="width: 231px; height: 27px" class="dvtCellInfo" >
                                        <asp:TextBox ID="txtBranchTH" runat="server" Height="20px"  ></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 216px; height: 27px" class="dvtCellLabel">
                                        Branch Name Eng :
                                    </td>
                                    <td style="width: 231px; height: 27px" class="dvtCellInfo" >
                                        <asp:TextBox ID="txtBranchEN" runat="server" Height="20px"  ></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 216px; height: 27px" class="dvtCellLabel">
                                        Branch Code :
                                    </td>
                                    <td style="width: 231px; height: 27px" class="dvtCellInfo" >
                                        <asp:TextBox ID="txtBranchCode" runat="server" Height="20px" ></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 216px; height: 27px" class="dvtCellLabel">
                                        Location Code :
                                    </td>
                                    <td style="width: 231px; height: 27px" class="dvtCellInfo" >
                                        <asp:TextBox ID="txtLocationCode" runat="server" Height="20px"  ></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 216px; height: 27px" class="dvtCellLabel">
                                        Location Name :
                                    </td>
                                    <td style="width: 231px; height: 27px" class="dvtCellInfo" >
                                        <asp:TextBox ID="txtLocationName" runat="server" Height="20px"  ></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr><td>&nbsp;</td></tr>
                    <tr>
                        <td style="height: 24px">
                            <table style="width: 100%; height: 57px;">
                                <tr>
                                    <td style="width: 100px">
                                        &nbsp;
                                    </td>
                                    <td style="width: 260px">
                                        <asp:Button ID="btn_save" runat="server" Text="Save" />
                                        <asp:Button ID="btn_clear" runat="server" Text="Clear" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
