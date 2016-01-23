<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlByDate.ascx.vb" Inherits="FormControls_ctlByDate" %>
<%@ Register src="../UserControls/txtDate.ascx" tagname="txtDate" tagprefix="uc1" %>
    <tr style="height: 30px">
        <td style="height: 28px" align="right">Date From :&nbsp;</td>
        <td style="height: 28px" align="left">
            <uc1:txtDate ID="txtDateFrom" runat="server" />
        </td>
        <td style="height: 28px" align="right">Date To :&nbsp;</td>
        <td style="height: 28px" align="left">
            <uc1:txtDate ID="txtDateTo" runat="server" />
        </td>
    </tr>
