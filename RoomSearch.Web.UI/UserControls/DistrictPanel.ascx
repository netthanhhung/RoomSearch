<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DistrictPanel.ascx.cs"
    Inherits="RoomSearch.Web.UI.DistrictPanel" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<link rel="stylesheet" type="text/css" href="../Styles/CSS/appstyle.css" />
<script type="text/javascript" src="../Styles/JS/utils.js"></script>
<div>
    <div id="divMain" runat="server">
        <table>
            <tr>
                <td style="border:0px">
                    <asp:Label id="lblSearch" runat="server" Text="Hồ Chí Minh" ForeColor="#23dbdb"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="border:0px">
                    <asp:DataList id="dataListLinks" runat="server" RepeatDirection="Vertical" >
                       <ItemTemplate>
                          <asp:HyperLink ID="hyperLinkItem" runat="server" ForeColor="White"
                            NavigateUrl='<%# Eval("URL")%>' Text='<%# Eval("Name")%>' />
                       </ItemTemplate>
                    </asp:DataList>
                </td>
            </tr>
        </table>
    </div>
</div>
