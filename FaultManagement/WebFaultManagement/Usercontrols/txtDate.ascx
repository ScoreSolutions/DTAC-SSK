<%@ Control Language="VB" AutoEventWireup="false" CodeFile="txtDate.ascx.vb" Inherits="UserControls_txtDate" %>
<asp:TextBox ID="TextBox1" runat="server" Width="80" CssClass="Csslbl"  ></asp:TextBox>
<a id="likA" runat="server" style="text-decoration:none" >
    <img src="../Images/calendarIcon.gif" width="19" height="19" border="0" 
    id="ImageButton1" runat="server" style="vertical-align:baseline;cursor:pointer;" />
</a>
<asp:Label ID="Label1" runat="server" Text="*" ForeColor="Red"></asp:Label>
<asp:Label ID="lblValidText" runat="server" Text="" ForeColor="Red"></asp:Label>
