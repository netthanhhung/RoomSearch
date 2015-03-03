<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="Site.master" CodeBehind="SearchOldPostPage.aspx.cs"
    Inherits="RoomSearch.Web.UI.SearchOldPostPage" Title="Sửa Tin Đã Đăng" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="contentDefault" ContentPlaceHolderId="centreContentPlaceHolder" runat="server">
    <link rel="stylesheet" type="text/css" href="Styles/CSS/appstyle.css" />
    <script type="text/javascript">
        (function (i, s, o, g, r, a, m) {
            i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
                (i[r].q = i[r].q || []).push(arguments)
            }, i[r].l = 1 * new Date(); a = s.createElement(o),
                m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
        })(window, document, 'script', '//www.google-analytics.com/analytics.js', 'ga');

        ga('create', 'UA-55758160-1', 'auto');
        ga('send', 'pageview');

    </script>
    <script type="text/javascript" src="Styles/JS/utils.js"></script>
    <telerik:RadScriptBlock runat="server" ID="scriptBlock">
        <script type="text/javascript" language="javascript">

            function GetQueryStringByParameter(name) {
                name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
                var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
                results = regex.exec(location.search);
                return results == null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
            }

            function OnUserViewDetailsClientClicked(postId) {
                var radWindow = $find("<%= radWindowUser.ClientID %>")
                var postTypeId = GetQueryStringByParameter("PostType");
                var url = "PostDetailPopup.aspx?Mode=edit&PostId=" + postId + "&PostType=" + postTypeId;
                radWindow.argument = "No";
                radWindow.setUrl(url);
                radWindow.show();

                return false;
            }

            function onClientPostDetailWindowClosed(window) {
                if (window.argument != undefined && window.argument != null && window.argument != "") {
                    var isReload = window.argument;
                    if (isReload == "Yes") {
                        $find("<%= SearcRoomPostAjaxManager.ClientID %>").ajaxRequest("RebindSearchResults");
                    }
                }
            }

        </script>
    </telerik:RadScriptBlock>
    <telerik:RadScriptManager ID="ScriptManager" runat="server" />
    <%--<telerik:RadProgressManager ID="Radprogressmanager1" runat="server" />--%>
    
     <div>
        <telerik:RadAjaxManager EnableAJAX="true" runat="server" ID="SearcRoomPostAjaxManager"
            OnAjaxRequest="OnMyAjaxManagerAjaxRequest">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="btnSearch">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="gridRoomResult" LoadingPanelID="pnlRadAjaxLoading"/>
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        
        <telerik:RadAjaxLoadingPanel ID="pnlRadAjaxLoading" runat="server" Height="75px"
                Width="75px" Transparency="50">
                <img alt="Loading..." src='<%= RadAjaxLoadingPanel.GetWebResourceUrl(Page, "Telerik.Web.UI.Skins.Default.Ajax.loading.gif") %>'
                    style="border: 0;" />
        </telerik:RadAjaxLoadingPanel>

        <div id="divCondition" runat="server">
            <table>
                <tr>
                     <td>
                        <asp:Label ID="lblEmail" runat="server" Text="Tìm theo Email :"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtEmail" runat="server" Width="250px">
                        </telerik:RadTextBox>
                    </td>
                    
                    <td>
                        <asp:Label ID="lblPhone" runat="server" Text="Hoặc Số điện thoại :"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtPhoneNumber" runat="server" Width="250px">
                        </telerik:RadTextBox>
                    </td>

                    <td align="left">
                            <asp:Button runat="server" ID="btnSearch" Text="Tìm kiếm" CssClass="flatButton" Width="100"
                                OnClick="OnBtnSearch_Clicked" CausesValidation="true"/>&nbsp;
                        </td>
                </tr>
            </table>
        </div>
        <div id="divResult" runat="server" style="margin-top : 10px">
            <div>
                <telerik:RadGrid ID="gridRoomResult" GridLines="None" Skin="WebBlue" AllowMultiRowSelection="False" 
                    MasterTableView-NoDetailRecordsText="Không tìm thấy kết quả nào phù hợp"
                    MasterTableView-NoMasterRecordsText="Không tìm thấy kết quả nào phù hợp"
                    
                    EnableAjaxSkinRendering="true" runat="server" AllowPaging="True" AllowSorting="True" AllowCustomPaging="True"
                    PageSize="20" Width="100%" AutoGenerateColumns="false" OnPageSizeChanged="OnGridRoomResult_PageSizeChanged"
                    OnNeedDataSource="OnGridRoomResultNeedDataSource" OnItemDataBound="OnGridRoomResultItemDataBound" 
                    OnSortCommand="OnGridRoomResultSortCommand"
                    >
                    <PagerStyle Mode="NextPrevNumericAndAdvanced"></PagerStyle>
                    <MasterTableView DataKeyNames="PostId" DataMember="PersonName" AllowMultiColumnSorting="True"
                        Width="100%" EditMode="PopUp">
                        <Columns>
                            <%--<telerik:GridBoundColumn UniqueName="DateUpdated" SortExpression="DateUpdated" HeaderText="Ngày đăng tin" DataFormatString="{0:dd/MM/yyyy}"
                                DataField="DateUpdated" HeaderStyle-Width="100px">
                            </telerik:GridBoundColumn>--%>
                            <telerik:GridTemplateColumn UniqueName="DateUpdated" HeaderText="Ngày đăng tin" HeaderStyle-Width="100px">
                                <ItemTemplate>
                                    <asp:HyperLink ID="lnkPostDetails" runat="server" Text='<%#Eval("DateUpdated", "{0:dd/MM/yyyy}") %>' 
                                        NavigateUrl='<%# Eval("PostId","~/PostDetailPage.aspx?Mode=view&PostId={0}") %>'
                                        ForeColor="Blue" Font-Underline="true">  
                                    </asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle Width="80px"></HeaderStyle>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn UniqueName="District" SortExpression="District" HeaderText="Quận"
                                DataField="District"  HeaderStyle-Width="100px">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="Address" SortExpression="Address" HeaderText="Địa chỉ"
                                DataField="Address"  HeaderStyle-Width="200px">
                            </telerik:GridBoundColumn>
                            <%--<telerik:GridBoundColumn UniqueName="Description" SortExpression="Description" HeaderText="Mô tả"
                                DataField="ShortDescription" Tooltip="love">
                            </telerik:GridBoundColumn>--%>
                            <telerik:GridTemplateColumn UniqueName="Description" HeaderText="Mô tả" > 
                               <ItemTemplate> 
                                   <asp:Label ID="lblDescription" runat="server" Text='<%#Eval("ShortDescription") %>' ToolTip='<%#Eval("Description") %>' ></asp:Label> 
                               </ItemTemplate> 
                             </telerik:GridTemplateColumn> 

                            <telerik:GridBoundColumn UniqueName="Price" SortExpression="Price" HeaderText="Giá" DataFormatString="{0:C2}"
                                DataField="PriceString"  HeaderStyle-Width="80">
                            </telerik:GridBoundColumn>

                            <telerik:GridTemplateColumn UniqueName="ViewDetails">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkViewDetails" runat="server" Text="Xem chi tiết" CommandArgument='<%# Eval("PostId") %>' ForeColor="Blue" Font-Underline="true">  
                                    </asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle Width="80px"></HeaderStyle>
                            </telerik:GridTemplateColumn>
                            
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>

                <telerik:RadWindow runat="server" ID="radWindowUser" Skin="WebBlue" VisibleOnPageLoad="false" VisibleStatusbar="false"
                    Modal="true" OffsetElementID="offsetElement" Top="30" Left="30" NavigateUrl="PostDetailPopup.aspx"
                    Title="Action" Height="650px" Width="1300px" OnClientClose="onClientPostDetailWindowClosed">
                </telerik:RadWindow>
            </div>
            
                
        </div>
    </div>  
</asp:Content>
