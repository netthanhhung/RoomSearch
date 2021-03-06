﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="Site.master" CodeBehind="NoPricePage.aspx.cs"
    Inherits="RoomSearch.Web.UI.NoPricePage" Title="Tìm Phòng Trọ" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="contentDefault" ContentPlaceHolderId="centreContentPlaceHolder" runat="server">
    <link rel="stylesheet" type="text/css" href="<%# ResolveUrl("~/Styles/CSS/appstyle.css")%>"   />
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
    <script type="text/javascript" src="<%# ResolveUrl("~/Styles/JS/utils.js")%>"  ></script>
    <telerik:RadScriptBlock runat="server" ID="scriptBlock">
        <script type="text/javascript" language="javascript">

            function onDropDownCity_ClientIndexChanged(sender, eventArgs) {
                var item = eventArgs.get_item();
                $find("<%= SearchRoomAjaxManager.ClientID %>").ajaxRequest("RebindDistrictListByCity-" + item.get_value());
            }

            function onDropDownRoomType_ClientIndexChanged(sender, eventArgs) {
                var item = eventArgs.get_item();
                var selectedRoomTypeId = item.get_value();
                var txtPriceFrom = $find("<%= txtPriceFrom.ClientID %>");
                var txtPriceTo = $find("<%= txtPriceTo.ClientID %>");
                if (selectedRoomTypeId == 1) {
                    txtPriceFrom.set_value(1);
                    txtPriceTo.set_value(3);
                } else {
                    txtPriceFrom.set_value(3);
                    txtPriceTo.set_value(10);
                }
            }

            function OnUserViewDetailsClientClicked(postId) {
                var radWindow = $find("<%= radWindowUser.ClientID %>")
                var url = "PostDetailPopup.aspx?Mode=Edit&PostId=" + postId;
                radWindow.setUrl(url);
                radWindow.show();

                return false;
            }

        </script>
    </telerik:RadScriptBlock>
    <telerik:RadScriptManager ID="ScriptManager" runat="server" />
    <%--<telerik:RadProgressManager ID="Radprogressmanager1" runat="server" />--%>
    
     <div>
        <telerik:RadAjaxManager EnableAJAX="true" runat="server" ID="SearchRoomAjaxManager"
            OnAjaxRequest="OnMyAjaxManagerAjaxRequest">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="cbbCity">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="cbbDistrict" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
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
                        <asp:Label ID="lblCity" runat="server" Text="Thành Phố :" Width="70px"></asp:Label>
                    </td>
                    <td colspan="3">
                        <telerik:RadComboBox ID="cbbCity" runat="server" Skin="WebBlue" Height="155px"
                            Width="230px" OnClientSelectedIndexChanged="onDropDownCity_ClientIndexChanged">
                        </telerik:RadComboBox>
                    </td>

                    <td style="width:10px"></td>
                    <td>
                        <asp:Label ID="lblDistrict" runat="server" Text="Quận / Huyện :" Width="90px"></asp:Label>
                    </td>
                    <td colspan="3">
                        <telerik:RadComboBox ID="cbbDistrict" runat="server" Skin="WebBlue" Height="155px"
                            Width="230px">
                        </telerik:RadComboBox>
                    </td>

                    <td style="width:10px"></td>
                    <td>
                        <asp:Label ID="lblRoomType" runat="server" Text="Loại phòng :" Width="70px"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadComboBox ID="cbbRoomType" runat="server" Skin="WebBlue" Height="155px"
                            Width="104px" OnClientSelectedIndexChanged="onDropDownRoomType_ClientIndexChanged">
                        </telerik:RadComboBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblPriceFrom" runat="server" Text="Giá từ :" Width="70px"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadNumericTextBox ID="txtPriceFrom" runat="server" Width="40px" Type="Number"
                            Skin="WebBlue" NumberFormat-DecimalDigits="1" NumberFormat-PositivePattern="n"
                            Value="1.0" EnabledStyle-HorizontalAlign="Right" NumberFormat-GroupSeparator=""
                            BorderStyle="Solid" BorderColor="#A8BEDA" BorderWidth="1" />
                        <asp:Label runat="server" ID="lblUnitFrom" Text="(triệu)"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblPriceTo" runat="server" Text="Đến :"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadNumericTextBox ID="txtPriceTo" runat="server" Width="40px" Type="Number"
                            Skin="WebBlue" NumberFormat-DecimalDigits="1" NumberFormat-PositivePattern="n"
                            Value="3.0" EnabledStyle-HorizontalAlign="Right" NumberFormat-GroupSeparator=""
                            BorderStyle="Solid" BorderColor="#A8BEDA" BorderWidth="1" />
                        <asp:Label runat="server" ID="lblUnitTo" Text="(triệu)"></asp:Label>
                    </td>

                    <td></td>
                    <td>
                        <asp:Label ID="lblDateFrom" runat="server" Text="Đăng từ ngày :" Width="90px"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadDatePicker ID="datDateFrom" runat="server" Width="95px"  Skin="WebBlue"  Calendar-CultureInfo="en-US">
                            <DateInput ID="dateInputCreationDate" runat="server" BackColor="White"
                                  DateFormat="dd/MM/yyyy"
                                  DisplayDateFormat="dd/MM/yyyy">
                            </DateInput>
                        </telerik:RadDatePicker> 
                    </td>
                    <td>
                        <asp:Label ID="lblDateTo" runat="server" Text="Đến ngày :" Width="60px"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadDatePicker ID="datDateTo" runat="server" Width="95px"  Skin="WebBlue"  Calendar-CultureInfo="en-US">
                            <DateInput ID="dateInput1" runat="server" BackColor="White"
                                  DateFormat="dd/MM/yyyy"
                                  DisplayDateFormat="dd/MM/yyyy">
                            </DateInput>
                        </telerik:RadDatePicker> 
                    </td>
                    
                    <td></td>
                    <td></td>
                    <td></td>
                    <td align="left">
                            <asp:Button runat="server" ID="btnSearch" Text="Tìm Phòng" CssClass="flatButton" Width="100"
                                OnClick="OnBtnSearch_Clicked" CausesValidation="true"/>&nbsp;
                        </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblKeywords" runat="server" Text="Theo từ khóa :" Width="90px"></asp:Label>
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtKeywords" runat="server" Width="225px" Wrap="true" MaxLength="50"
                            BackColor="White">
                        </asp:TextBox>
                    </td>
                    <td colspan="9">
                        <asp:Label ID="lblKeywordsDes" runat="server" Text=" (Tên đường hoặc tên chung cư hoặc khu vực ...)"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div id="divResult" runat="server" style="margin-top : 10px">
            <div>
                <telerik:RadGrid ID="gridRoomResult" GridLines="None" Skin="WebBlue" AllowMultiRowSelection="False" 
                    MasterTableView-NoDetailRecordsText="Không tìm thấy phòng nào phù hợp"
                    MasterTableView-NoMasterRecordsText="Không tìm thấy phòng nào phù hợp"
                    
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
                            <telerik:GridTemplateColumn UniqueName="DateUpdated" HeaderText="Ngày đăng tin">
                                <ItemTemplate>
                                    <asp:HyperLink ID="lnkPostDetails" runat="server" Text='<%#Eval("DateUpdated", "{0:dd/MM/yyyy}") %>' 
                                        NavigateUrl='<%# Eval("PostId","~/PostDetailPage.aspx?PostType=1&Mode=view&PostId={0}") %>'
                                        ForeColor="Blue" Font-Underline="true">  
                                    </asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle Width="95px"></HeaderStyle>
                            </telerik:GridTemplateColumn>

                            <telerik:GridBoundColumn UniqueName="District" SortExpression="District" HeaderText="Quận"
                                DataField="District"  HeaderStyle-Width="80px">
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
                                DataField="PriceString"  HeaderStyle-Width="70">
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
                    Title="Action" Height="650px" Width="1300px" >
                </telerik:RadWindow>
            </div>
            
                
        </div>
    </div>  
</asp:Content>
