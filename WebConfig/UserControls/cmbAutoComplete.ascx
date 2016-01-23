<%@ Control Language="VB" AutoEventWireup="true" CodeFile="cmbAutoComplete.ascx.vb" Inherits="UserControls_cmbAutoComplete"  %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

        <cc1:ListSearchExtender id="LSE" runat="server"
            TargetControlID="cmbCombo"
            PromptText=""
            PromptCssClass="zHidden"
            PromptPosition="Bottom"
            QueryTimeout="0"
            QueryPattern="Contains"
            IsSorted="true"   />
            
        <asp:DropDownList ID="cmbCombo" runat="server" CssClass="zComboBox"  >
        </asp:DropDownList>
            <asp:Label ID="lblValidText" runat="server" ForeColor="Red" ></asp:Label>

