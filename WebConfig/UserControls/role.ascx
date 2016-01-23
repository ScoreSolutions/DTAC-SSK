<%@ Control Language="VB" AutoEventWireup="false" CodeFile="role.ascx.vb" Inherits="UserControls_role" %>
<style type="text/css">
    .shoplist
    {
        font-size: normal;
    }
    .formDialog
    {
        border-top-width: 1px;
        border-right-width: 1px;
        border-bottom-width: 1px;
        border-left-width: 1px;
        border-top-style: solid;
        border-right-style: solid;
        border-bottom-style: solid;
        border-left-style: solid;
        border-top-color: #eff5c5;
        border-right-color: #aec843;
        border-bottom-color: #97aa1c;
        border-left-color: #d5e03a;
        background-color: #B7D575;
        border-collapse: collapse;
        color: White;
        font-weight: bold;
        height: 25px;
    }
    .zHidden
    {
        display: none;
    }
    a:link
    {
        text-decoration: none;
        color: #79B241;
    }
</style>
<table>
    <tr>
        <td align="center">
            <asp:Label ID="lblHeader" runat="server" Text="Label"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
        <asp:Panel ID="panel1" runat="server" Height="400px" ScrollBars="Both"
    Width="400px">
    <asp:GridView ID="gvDataList" runat="server" AutoGenerateColumns="False" 
        CssClass="shoplist" Width="500px">
        <Columns>
            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="#" 
                ItemStyle-Width="5%">
                <itemtemplate>
                    <asp:CheckBox ID="ChkSelect" runat="server" />
                     <asp:Label ID="lblId" runat="server" Text='<%# Bind("id") %>' style="display:none"></asp:Label>
                </itemtemplate>
                <HeaderStyle HorizontalAlign="Center" />
                <itemstyle horizontalalign="center" width="5%" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="" ShowHeader="True"  HeaderStyle-HorizontalAlign="Center">
                <HeaderStyle Width="95%"  />
                <ItemStyle HorizontalAlign="Left" Width="95%" />
                <HeaderTemplate>
                    <asp:Label ID="lblHeaderText" runat="server" Text="Label"></asp:Label>
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>' Font-Size="8pt" ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="id">
            <ControlStyle CssClass="zHidden" />
            <FooterStyle CssClass="zHidden" />
            <HeaderStyle CssClass="zHidden" />
            <ItemStyle CssClass="zHidden" />
            </asp:BoundField>
        </Columns>
        <HeaderStyle CssClass="formDialog" />
        <AlternatingRowStyle BackColor="#DDDDDD" />
    </asp:GridView>
</asp:Panel>

        </td>
    </tr>
</table>


