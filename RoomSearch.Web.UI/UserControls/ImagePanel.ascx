<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ImagePanel.ascx.cs" Inherits="RoomSearch.Web.UI.ImagePanel" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<div>
    <telerik:RadAjaxManager EnableAJAX="true" runat="server" ID="ImagePanelAjaxManager">
            <AjaxSettings>
               <%-- <telerik:AjaxSetting AjaxControlID="btnDelete">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="btnDelete" />
                        <telerik:AjaxUpdatedControl ControlID="imgImage" />
                    </UpdatedControls>
                </telerik:AjaxSetting>--%>
            </AjaxSettings>
        </telerik:RadAjaxManager>
           
<table>
    <tr>
        <td>
            <asp:Image ID="imgImage" runat="server" Width="400px" style="margin-top:5px; margin-left:5px; margin-right:5px; margin-bottom:5px" />
        </td>
        <td align="right" valign="top">
            <asp:ImageButton ID="btnDelete" runat="server" Width="20" ToolTip="Xóa hình này" ImageUrl="~/Styles/Images/delete.png"
                OnClick="btnDelete_Click"/>
        </td>
    </tr>
</table>
</div>
