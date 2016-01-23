<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ctlUploadFile.ascx.vb"
    Inherits="UserControls_ctlUploadFile" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<script type="text/javascript">
    var i = 1;
    $(document).ready(function() {
        $("#addfile").click(function() {
            $("#dvfiles").append("<input name=" + i + "fu type=file /><a href=#>remove</a><br>");
            i++;
        });

        $("#dvfiles a").live('click', function() {
            $(this).prev("input[type=file]").remove();
            $(this).remove();
        });
    });

    $(document).submit(function() {
        var flag = true;
        $("#dvfiles input[type=file]").each(function() {
            if ($(this).val() == "") {
                $(this).css("background", "Red");
                flag = false;
            }
        });
        return flag;
    });        
</script>


<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate >

<asp:Button ID="Button1" runat="server" Text="Button" Width="0px" CssClass="zHidden" />
<asp:Image ID="Image3" runat="server" CssClass="CssLink" ImageUrl="~/Images/computer.gif" />
<asp:LinkButton ID="LinkButton2" runat="server" CssClass="CssLink">นำเข้าเอกสารจากเครื่องคอมพิวเตอร์</asp:LinkButton>
<div id="dvfiles">
</div>
<a id="addfile" href="#">Add file..</a><br />
<asp:Label ID="lblMessage" runat="server"></asp:Label>
<br />
<asp:Button ID="btnUpload" runat="server" OnClick="btnUpload_Click" Text="Upload" />
    </ContentTemplate>
          <Triggers>
                <asp:PostBackTrigger ControlID="btnUpload" />
                 </Triggers>
</asp:UpdatePanel>

