<%@ Control Language="VB" AutoEventWireup="false" CodeFile="txtTime.ascx.vb" Inherits="UserControls_txtTime" %>

<asp:TextBox ID="TextBox1" runat="server" onkeypress="txtTime_OnKeyPress(this,event);" Width="40" CssClass="textbox" ></asp:TextBox>
<asp:Label ID="Label1" runat="server" ForeColor="Red" Text="*"></asp:Label>
