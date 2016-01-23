<%@ Control Language="VB" AutoEventWireup="true" CodeFile="MultipleFileUpload.ascx.vb" Inherits="MultipleFileUpload" %>

<asp:Panel ID="pnlParent" runat="server" Width="300px" BorderColor="Black" BorderWidth="1px"
    BorderStyle="Solid">
    <asp:Panel ID="pnlFiles" runat="server" Width="300px" HorizontalAlign="Left">
        <asp:FileUpload ID="IpFile" runat="server" />
    </asp:Panel>
    <asp:Panel ID="pnlListBox" runat="server" Width="292px" BorderStyle="Inset">
    </asp:Panel>
    <asp:Panel ID="pnlButton" runat="server" Width="300px" HorizontalAlign="Right">
        <input id="btnAdd" onclick="javascript:Add();" style="width: 60px" type="button"
            runat="server" value="Add" />
        <input id="btnClear" onclick="javascript:Clear();" style="width: 60px" type="button"
            value="Clear" runat="server" />
        <asp:Button ID="btnUpload" OnClientClick="javascript:return DisableTop();" runat="server"
            Text="Upload" Width="60px" OnClick="btnUpload_Click" />
        <br />
        <asp:Label ID="lblCaption" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="XX-Small"
            ForeColor="Gray"></asp:Label>&nbsp;
    </asp:Panel>
</asp:Panel>
