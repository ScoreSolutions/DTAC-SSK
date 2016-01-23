<%@ Page Title="" Language="VB" MasterPageFile="~/Template/MasterLogin.master" AutoEventWireup="false" CodeFile="frmLogin.aspx.vb" Inherits="frmLogin" %>
<%@ MasterType  virtualPath="~/Template/MasterLogin.master"%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <center>
            <br />
        <table width="400" border="0" align="center" cellpadding="0" cellspacing="0" class="formDialog">
          <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
          </tr>
          <tr>
            <td align="right">Username : </td>
            <td align="left">
                <asp:TextBox ID="txtUsername" runat="server" Width="171px" MaxLength="50"></asp:TextBox>
              </td>
          </tr>
          <tr>
            <td align="right">&nbsp;</td>
            <td align="left">&nbsp;</td>
          </tr>
          <tr>
            <td align="right">Password : </td>
            <td align="left">
                <asp:TextBox ID="txtPassword" runat="server" Width="171px" MaxLength="50" 
                    TextMode="Password"></asp:TextBox>
              </td>
          </tr>
          <tr>
            <td colspan="2" align="center" valign="middle">
                &nbsp;</td>
          </tr>
          <tr>
            <td colspan="2" align="center" valign="middle">
                <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btnGreen" Width="171px"/>
              </td>
          </tr>
          <tr>
            <td colspan="2" align="center" valign="middle">&nbsp;</td>
          </tr>
        </table>
    </center>
</asp:Content>

