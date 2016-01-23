<%@ Control Language="VB" AutoEventWireup="false" CodeFile="txtDate.ascx.vb" Inherits="UserControls_txtDate"  %>

<table border="0" cellpadding="1" cellspacing="1" >
    <tr>
        <td width="80px">
            <asp:TextBox ID="txtdate" runat="server" AutoPostBack="false" Width="80"  ></asp:TextBox>
        </td>
        <td>
            <a href="#" onClick="NewCssCal('<%=txtdate.ClientID %>','DDMMYYYY')" style="text-decoration:none" >
                <img src="../Images/calendarIcon.gif" width="19" height="19" border="0" 
                id="ImageButton1" runat="server" style="vertical-align:baseline;" />
            </a>
        </td>
        <td>
            <asp:Label ID="Label1" runat="server" Text="*" ForeColor="Red"></asp:Label>
            <asp:Label ID="lblValidText" runat="server" Text="" ForeColor="Red"></asp:Label>
        </td>
    </tr>
</table>


