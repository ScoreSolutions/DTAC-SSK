<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlAramServitySW.ascx.vb"
    Inherits="Usercontrols_ctlAramServitySW" %>
<%@ Register Assembly="MycustomDG" Namespace="MycustomDG" TagPrefix="Saifi" %>
<table style="width: 100%">
    <tr>
        <td width="20%" class="dvtCellInfo">
            Shop Name
        </td>
        <td width="30%" class="dvtCellInfo">
            <asp:TextBox ID="txtShopName" runat="server" Width="150px"></asp:TextBox>
        </td>
        <td width="20%" class="dvtCellInfo">
            Server Name
        </td>
        <td width="30%" class="dvtCellInfo">
            <asp:TextBox ID="txtServerName" runat="server" Width="150px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td width="20%" class="dvtCellInfo">
            IP Address
        </td>
        <td width="30%" class="dvtCellInfo">
            <asp:TextBox ID="txtIPAddress" runat="server" Width="150px"></asp:TextBox><div>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtIpAddress"
                    Display="Dynamic" ErrorMessage="Incorrect IP Address format" ValidationExpression="^(\d|[1-9]\d|1\d\d|2([0-4]\d|5[0-5]))\.(\d|[1-9]\d|1\d\d|2([0-4]\d|5[0-5]))\.(\d|[1-9]\d|1\d\d|2([0-4]\d|5[0-5]))\.(\d|[1-9]\d|1\d\d|2([0-4]\d|5[0-5]))$"></asp:RegularExpressionValidator></div>
        </td>
        <td class="dvtCellInfo">
            Interval Minute
        </td>
        <td width="30%" class="dvtCellInfo">
            <asp:TextBox ID="txtIntervalMinute" runat="server" Width="150px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td width="20%" class="dvtCellInfo">
            Alarm Type
        </td>
        <td width="30%" class="dvtCellInfo">
            <asp:Label ID="lblAlamType" runat="server" Text="Fault"></asp:Label>
        </td>
        <td class="dvtCellInfo">
            Alarm Severity
        </td>
        <td width="30%" class="dvtCellInfo">
            <asp:Label ID="lblAlamSeverity" runat="server" Text="Critical"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="dvtCellInfo">
            Alarm Method
        </td>
        <td width="30%" class="dvtCellInfo">
            <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" ID="rdoMethod">
                <asp:ListItem Selected="True">SNMP</asp:ListItem>
                <asp:ListItem>SMS</asp:ListItem>
                <asp:ListItem>Email</asp:ListItem>
            </asp:RadioButtonList>
        </td>
        <td class="dvtCellInfo">
            &nbsp;
        </td>
        <td width="30%" class="dvtCellInfo">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="dvtCellInfo">
            Alarm Date</td>
        <td width="30%" class="dvtCellInfo">
            <asp:CheckBoxList ID="chkDateList" runat="server" 
                RepeatDirection="Horizontal">
                <asp:ListItem Selected="True">Sun</asp:ListItem>
                <asp:ListItem Selected="True">Mon</asp:ListItem>
                <asp:ListItem Selected="True">Tues</asp:ListItem>
                <asp:ListItem Selected="True">Wed</asp:ListItem>
                <asp:ListItem Selected="True">Thurs</asp:ListItem>
                <asp:ListItem Selected="True">Fri</asp:ListItem>
                <asp:ListItem Selected="True">Sat</asp:ListItem>
            </asp:CheckBoxList>
        </td>
        <td class="dvtCellInfo">
            &nbsp;</td>
        <td width="30%" class="dvtCellInfo">
            &nbsp;</td>
    </tr>
    <tr>
        <td class="dvtCellInfo">
            Alarm Time</td>
        <td width="30%" class="dvtCellInfo">
            <asp:TextBox ID="txtAlamTimeFrom" runat="server" Width="64px" ></asp:TextBox>

            To&nbsp;
            <asp:TextBox ID="txtAlamTimeTo" runat="server" Width="64px" ></asp:TextBox>

        </td>
        <td class="dvtCellInfo">
            <asp:CheckBox ID="chkAllDayEvent" runat="server" Text="All Day Event" 
                AutoPostBack="True" />
        </td>
        <td width="30%" class="dvtCellInfo">
            &nbsp;</td>
    </tr>
    <tr>
        <td class="dvtCellInfo">
            &nbsp;</td>
        <td width="30%" class="dvtCellInfo">
            <asp:RegularExpressionValidator ID="DateValidator" runat="server" ControlToValidate="txtAlamTimeFrom"
                ValidationExpression="^((([0]?[1-9]|1[0-2])(:)[0-5][0-9]((:)[0-5][0-9])?( )?(AM|am|aM|Am|PM|pm|pM|Pm))|(([0]?[0-9]|1[0-9]|2[0-3])(:)[0-5][0-9]((:)[0-5][0-9])?))$"
                ErrorMessage="Incorrect Time From"></asp:RegularExpressionValidator>
            &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtAlamTimeTo"
                ValidationExpression="^((([0]?[1-9]|1[0-2])(:)[0-5][0-9]((:)[0-5][0-9])?( )?(AM|am|aM|Am|PM|pm|pM|Pm))|(([0]?[0-9]|1[0-9]|2[0-3])(:)[0-5][0-9]((:)[0-5][0-9])?))$"
                ErrorMessage="Incorrect Time To"></asp:RegularExpressionValidator>
        </td>
        <td class="dvtCellInfo">
            &nbsp;</td>
        <td width="30%" class="dvtCellInfo">
            &nbsp;</td>
    </tr>
    <tr>
        <td class="dvtCellInfo" colspan="4">
            <asp:Label ID="lblItemList" runat="server"></asp:Label>
           
        </td>
    </tr>
    <tr>
        <td class="dvtCellInfo" colspan="4">
            <Saifi:MyDg ID="Mydg1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                CssClass="Grid_inq" ImageFirst="/imgs/nav_left.gif" ImageLast="/imgs/nav_right.gif"
                ImageNext="/imgs/bulletr.gif" ImagePrevious="/imgs/bulletl.gif" ShowFirstAndLastImage="False"
                ShowPreviousAndNextImage="False" Width="100%">
                <AlternatingItemStyle CssClass="Grid_inqAltItem" />
                <Columns>
                    <asp:TemplateColumn>
                        <ItemTemplate>
                            <asp:CheckBox ID="ckbSelect" runat="server" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn Visible="false" >
                        <ItemTemplate>
                            <asp:Label ID="lbl_ServiceName" runat="server" Style="text-align: left; width: 100%;"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Width="20%" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Service Name">
                        <HeaderTemplate>
                            <asp:Label ID="lblHeaderName" runat="server" Text="ABC123"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_DisplayName" runat="server" Style="text-align: left; width: 100%;" Text='<%#DataBinder.Eval(Container.DataItem, "DisplayName")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Width="20%" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Alarm Code">
                        <ItemTemplate>
                            <asp:Label ID="txtAlamCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "AlarmCode")%>' ></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Width="30%" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Process Name">
                        <ItemTemplate>
                            <asp:Label ID="txtProcessName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ProcessName")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Width="30%" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Active">
                        <ItemTemplate>
                            <asp:CheckBox ID="ckbActive" runat="server" />
                            <asp:Label ID="lbl_Active" runat="server" Visible="false" Style="text-align: left;
                                width: 100%;" Text='<%#DataBinder.Eval(Container.DataItem, "Active")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                    </asp:TemplateColumn>
                </Columns>
                <HeaderStyle CssClass="Grid_inqHeader" />
                <ItemStyle CssClass="Grid_inqItem" />
                <PagerStyle Mode="NumericPages" Visible="False" />
            </Saifi:MyDg>
        </td>
    </tr>
</table>
<asp:Label ID="lblKeepPara" runat="server" Visible="False"></asp:Label>

