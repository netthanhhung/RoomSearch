<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="Site.master" CodeBehind="SearchHousePage.aspx.cs"
    Inherits="RoomSearch.Web.UI.SearchHousePage" Title="Tìm nhà đất" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="contentDefault" ContentPlaceHolderId="centreContentPlaceHolder" runat="server">
    <link rel="stylesheet" type="text/css" href="Styles/CSS/appstyle.css" />
    <script type="text/javascript" src="Styles/JS/utils.js"></script>
    <telerik:RadScriptBlock runat="server" ID="scriptBlock">
        <script type="text/javascript" language="javascript">

            function onDropDownCity_ClientIndexChanged(sender, eventArgs) {
                var item = eventArgs.get_item();
                $find("<%= SearchRoomAjaxManager.ClientID %>").ajaxRequest("RebindDistrictListByCity-" + item.get_value());
            }

            function OnUserViewDetailsClientClicked(postId) {
                var radWindow = $find("<%= radWindowUser.ClientID %>")
                var url = "PostDetailPopup.aspx?Mode=view&PostId=" + postId + "&PostType=3";
                radWindow.setUrl(url);
                radWindow.show();

                return false;
            }

        </script>
    </telerik:RadScriptBlock>
    <telerik:RadScriptManager ID="ScriptManager" runat="server" />
    <telerik:RadProgressManager ID="Radprogressmanager1" runat="server" />
    
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
                        <asp:Label ID="lblCity" runat="server" Text="Thành Phố :"></asp:Label>
                    </td>
                    <td colspan="3">
                        <telerik:RadComboBox ID="cbbCity" runat="server" Skin="Office2007" Height="155px"
                            Width="230px" OnClientSelectedIndexChanged="onDropDownCity_ClientIndexChanged">
                        </telerik:RadComboBox>
                    </td>

                    <td style="width:10px"></td>
                    <td>
                        <asp:Label ID="lblDistrict" runat="server" Text="Quận / Huyện :"></asp:Label>
                    </td>
                    <td colspan="3">
                        <telerik:RadComboBox ID="cbbDistrict" runat="server" Skin="Office2007" Height="155px"
                            Width="230px">
                        </telerik:RadComboBox>
                    </td>

                    <td style="width:10px"></td>
                    <td>
                        <asp:Label ID="lblRealestateType" runat="server" Text="Loại nhà/đất :"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadComboBox ID="cbbRealestateType" runat="server" Skin="Office2007" Height="155px"
                            Width="104px">
                        </telerik:RadComboBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblPriceFrom" runat="server" Text="Giá từ :"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadNumericTextBox ID="txtPriceFrom" runat="server" Width="50px" Type="Number"
                            Skin="Office2007" NumberFormat-DecimalDigits="1" NumberFormat-PositivePattern="n"
                            Value="500" EnabledStyle-HorizontalAlign="Right" NumberFormat-GroupSeparator=""
                            BorderStyle="Solid" BorderColor="#A8BEDA" BorderWidth="1" />
                        <asp:Label runat="server" ID="lblUnitFrom" Text="(triệu)"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblPriceTo" runat="server" Text="Đến :"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadNumericTextBox ID="txtPriceTo" runat="server" Width="50px" Type="Number"
                            Skin="Office2007" NumberFormat-DecimalDigits="1" NumberFormat-PositivePattern="n"
                            Value="1000" EnabledStyle-HorizontalAlign="Right" NumberFormat-GroupSeparator=""
                            BorderStyle="Solid" BorderColor="#A8BEDA" BorderWidth="1" />
                        <asp:Label runat="server" ID="lblUnitTo" Text="(triệu)"></asp:Label>
                    </td>

                    <td></td>
                    <td>
                        <asp:Label ID="lblDateFrom" runat="server" Text="Đăng từ ngày :"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadDatePicker ID="datDateFrom" runat="server" Width="95px"  Skin="Office2007"  Calendar-CultureInfo="en-US">
                            <DateInput ID="dateInputCreationDate" runat="server" BackColor="White"
                                  DateFormat="dd/MM/yyyy"
                                  DisplayDateFormat="dd/MM/yyyy">
                            </DateInput>
                        </telerik:RadDatePicker> 
                    </td>
                        <td>
                        <asp:Label ID="lblDateTo" runat="server" Text="Đến ngày :"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadDatePicker ID="datDateTo" runat="server" Width="95px"  Skin="Office2007"  Calendar-CultureInfo="en-US">
                            <DateInput ID="dateInput1" runat="server" BackColor="White"
                                  DateFormat="dd/MM/yyyy"
                                  DisplayDateFormat="dd/MM/yyyy">
                            </DateInput>
                        </telerik:RadDatePicker> 
                    </td>
                    
                    <td></td>
                    <td>
                        <asp:Label ID="lblGender" runat="server" Text="Tìm :"></asp:Label>
                    </td>
                    <td>
                        <asp:RadioButton ID="radMale" runat="server" Checked="false" Text="Mua" GroupName="Gender" />
                        <asp:RadioButton ID="radFemale" runat="server" Checked="true" Text="Bán" GroupName="Gender" />
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
                <telerik:RadGrid ID="gridRoomResult" GridLines="None" Skin="Office2007" AllowMultiRowSelection="False" 
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
                            <telerik:GridBoundColumn UniqueName="DateUpdated" SortExpression="DateUpdated" HeaderText="Ngày đăng tin" DataFormatString="{0:dd/MM/yyyy}"
                                DataField="DateUpdated" HeaderStyle-Width="100px">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="District" SortExpression="District" HeaderText="Quận"
                                DataField="District"  HeaderStyle-Width="100px">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="Address" SortExpression="Address" HeaderText="Địa chỉ"
                                DataField="Address"  HeaderStyle-Width="150px">
                            </telerik:GridBoundColumn>
                            <%--<telerik:GridBoundColumn UniqueName="Description" SortExpression="Description" HeaderText="Mô tả"
                                DataField="ShortDescription" Tooltip="love">
                            </telerik:GridBoundColumn>--%>
                            <telerik:GridTemplateColumn UniqueName="Description" HeaderText="Mô tả" > 
                               <ItemTemplate> 
                                   <asp:Label ID="lblDescription" runat="server" Text='<%#Eval("ShortDescription") %>' ToolTip='<%#Eval("Description") %>' ></asp:Label> 
                               </ItemTemplate> 
                             </telerik:GridTemplateColumn> 

                             <telerik:GridBoundColumn UniqueName="MeterSquare" SortExpression="MeterSquare" HeaderText="Diện tích(m2)" DataFormatString="{0:0}"
                                DataField="MeterSquare" ItemStyle-HorizontalAlign="Right"  HeaderStyle-Width="100">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="Price" SortExpression="Price" HeaderText="Giá" DataFormatString="{0:C2}"
                                DataField="PriceString" ItemStyle-HorizontalAlign="Right"  HeaderStyle-Width="80">
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

                <telerik:RadWindow runat="server" ID="radWindowUser" Skin="Office2007" VisibleOnPageLoad="false" VisibleStatusbar="false"
                    Modal="true" OffsetElementID="offsetElement" Top="30" Left="30" NavigateUrl="PostDetailPopup.aspx"
                    Title="Thong tin chi tiết nhà đất" Height="650px" Width="1024px" >
                </telerik:RadWindow>
            </div>
            
                
        </div>
    </div>  
</asp:Content>
