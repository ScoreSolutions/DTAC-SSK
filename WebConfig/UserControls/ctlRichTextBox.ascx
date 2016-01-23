<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlRichTextBox.ascx.vb" Inherits="UserControls_ctlRichTextBox" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc1" %>
<cc1:Editor  runat="server" ID="editor1" Height="300" AutoFocus="false" Width="100%" DocumentCssPath="myCustomEditorDocument.css" >

</cc1:Editor >
<asp:Label ID="Label1" runat="server" Text="*" ForeColor="Red"></asp:Label>
<asp:Label ID="lblValidText" runat="server" ForeColor="Red"></asp:Label>