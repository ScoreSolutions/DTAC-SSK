<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlShopSelected.ascx.vb" Inherits="UserControls_ctlShopSelected" %>
<asp:UpdatePanel ID="pnlUpdate1" runat="server" >
    <ContentTemplate>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td width="45%">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td align="center" class="dvtCellLabel" >
                                <asp:Label ID="lblHeaderLeft" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="pnlPanelLeft" runat="server" Height="300px" ScrollBars="Auto" Width="400px">
                                    <asp:GridView ID="gvShopLeft" runat="server" AutoGenerateColumns="False" 
                                        CssClass="shoplist" Width="96%">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="#" 
                                                ItemStyle-Width="10%">
                                                <itemtemplate>
                                                    <asp:CheckBox ID="ChkSelect" runat="server" />
                                                     <asp:Label ID="lblId" runat="server" Text='<%# Bind("id") %>' style="display:none"></asp:Label>
                                                </itemtemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <itemstyle horizontalalign="center" width="10%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="" ShowHeader="True"  HeaderStyle-HorizontalAlign="Center">
                                                <HeaderStyle Width="86%"  />
                                                <ItemStyle HorizontalAlign="Left" Width="86%" />
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblHeaderText" runat="server" Text="Shop Name"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblShopName" runat="server" Text='<%# Bind("shop_name_en") %>'></asp:Label>
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
                </td>
                <td  style="width:10%" valign="top" >
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr><td>&nbsp;</td></tr>
                        <tr><td>&nbsp;</td></tr>
                        <tr><td>&nbsp;</td></tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnAddUser" runat="server" Text="&lt;---" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnDeleteUser" runat="server" Text="---&gt;"  />
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="45%">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td align="center" class="dvtCellLabel">
                                <asp:Label ID="lblHeaderRight" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="pnlRight" runat="server" Height="300px" ScrollBars="Auto" Width="400px">
                                    <asp:GridView ID="gvShopRight" runat="server" AutoGenerateColumns="False" 
                                        CssClass="shoplist" Width="96%">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="#" 
                                                ItemStyle-Width="10%">
                                                <itemtemplate>
                                                    <asp:CheckBox ID="ChkSelect" runat="server" />
                                                     <asp:Label ID="lblId" runat="server" Text='<%# Bind("id") %>' style="display:none"></asp:Label>
                                                </itemtemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <itemstyle horizontalalign="center" width="10%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="" ShowHeader="True"  HeaderStyle-HorizontalAlign="Center">
                                                <HeaderStyle Width="86%"  />
                                                <ItemStyle HorizontalAlign="Left" Width="86%" />
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblHeaderText" runat="server" Text="Shop Name"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblShopName" runat="server" Text='<%# Bind("shop_name_en") %>'></asp:Label>
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
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>