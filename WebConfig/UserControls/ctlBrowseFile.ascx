<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlBrowseFile.ascx.vb" Inherits="UserControls_ctlBrowseFile" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<table border="0" cellpadding="0" cellspacing="0">
    <tr>
        <td>
            <cc1:AsyncFileUpload ID="FileBrowse" runat="server" Width="250px" Height ="24px" CssClass="" 
            FailedValidation="False" UploaderStyle="Traditional" />
        </td>
        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
        <td>
            <asp:Button ID="Button1" runat="server" Text="Upload"  Width="80px" />
        </td>
    </tr>
</table>

