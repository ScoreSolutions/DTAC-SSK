<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlAramServity.ascx.vb"
    Inherits="Usercontrols_ctlAramServity" %>

<table style="width: 100%">

    <tr>
        <td width="20%" class="dvtCellInfo" >
            Alarm Type
        </td>
        <td width="30%" class="dvtCellInfo">
            <asp:Label ID="lblAlamType" runat="server" Text="Fault"></asp:Label>
        </td>
        <td width="20%" class="dvtCellInfo" >
            Repeat Check
        </td>
        <td width="30%" class="dvtCellInfo" >
            <asp:TextBox runat="server" Width="100px" ID="txtReplaceTime"></asp:TextBox>
            &nbsp;Time
        </td>
    </tr>
    <tr>
        <td  class="dvtCellInfo">
            Alarm Severity
        </td>
        <td colspan="3">
            <table style="width: 100%">
                <tr>
                    <td width="20%" class="dvtCellInfo">
                        Minor
                    </td>
                    <td  width="80%" class="dvtCellInfo">
                        Value Over
                        <asp:TextBox runat="server" Width="100px" ID="txtMinor"></asp:TextBox>
                        &nbsp;%
                    </td>
                </tr>
                <tr>
                    <td class="dvtCellInfo" >
                        Method
                    </td>
                    <td  width="80%" class="dvtCellInfo">
                        <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" 
                            ID="rdoMinorMethod">
                            <asp:ListItem Selected="True">SNMP</asp:ListItem>
                            <asp:ListItem>SMS</asp:ListItem>
                            <asp:ListItem>Email</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
        <td colspan="3">
            <table style="width: 100%">
                <tr>
                    <td width="20%" class="dvtCellInfo" >
                        Major
                    </td>
                    <td class="dvtCellInfo">
                        Value Over
                        <asp:TextBox runat="server" Width="100px" ID="txtMajor"></asp:TextBox>
                        &nbsp;%
                    </td>
                </tr>
                <tr>
                    <td class="dvtCellInfo">
                        Method
                    </td>
                    <td class="dvtCellInfo" >
                        <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" 
                            ID="rdoMajorMethod">
                            <asp:ListItem Selected="True">SNMP</asp:ListItem>
                            <asp:ListItem>SMS</asp:ListItem>
                            <asp:ListItem>Email</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
        <td colspan="3">
            <table style="width: 100%">
                <tr>
                    <td width="20%" class="dvtCellInfo" >
                        Critical
                    </td>
                    <td class="dvtCellInfo">
                        Value Over
                        <asp:TextBox runat="server" Width="100px" ID="txtCritical"></asp:TextBox>
                        &nbsp;%
                    </td>
                </tr>
                <tr>
                    <td class="dvtCellInfo">
                        Method
                    </td>
                    <td class="dvtCellInfo">
                        <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" 
                            ID="rdoCriticalMethod">
                            <asp:ListItem Selected="True">SNMP</asp:ListItem>
                            <asp:ListItem>SMS</asp:ListItem>
                            <asp:ListItem>Email</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="center" class="dvtCellInfo">
            <asp:CheckBox runat="server" Text="Active" ID="ckbActive" Checked="True"></asp:CheckBox>
        </td>
        <td colspan="3" class="dvtCellInfo" >
            Interval Minute
            <asp:TextBox runat="server" Width="100px" ID="txtInterval"></asp:TextBox>
            &nbsp;
        </td>
    </tr>
</table>
