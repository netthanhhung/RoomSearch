<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="Site.master" CodeBehind="SearchRoomPage.aspx.cs"
    Inherits="RoomSearch.Web.UI.SearchRoomPage" Title="Tìm Phòng Trọ" %>

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
                        <telerik:AjaxUpdatedControl ControlID="gridRoomResult" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        
        <div id="divCondition" runat="server">
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblCity" runat="server" Text="Thành Phố :"></asp:Label>
                    </td>
                    <td colspan="3">
                        <telerik:RadComboBox ID="cbbCity" runat="server" Skin="Office2007" Height="155px"
                            Width="254px" OnClientSelectedIndexChanged="onDropDownCity_ClientIndexChanged">
                        </telerik:RadComboBox>
                    </td>

                    <td style="width:10px"></td>
                    <td>
                        <asp:Label ID="lblDistrict" runat="server" Text="Quận / Huyện :"></asp:Label>
                    </td>
                    <td colspan="3">
                        <telerik:RadComboBox ID="cbbDistrict" runat="server" Skin="Office2007" Height="155px"
                            Width="254px">
                        </telerik:RadComboBox>
                    </td>

                    <td style="width:10px"></td>
                    <td>
                        <asp:Label ID="lblRoomType" runat="server" Text="Loại phòng :"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadComboBox ID="cbbRoomType" runat="server" Skin="Office2007" Height="155px"
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
                            Value="2.0" EnabledStyle-HorizontalAlign="Right" NumberFormat-GroupSeparator=""
                            BorderStyle="Solid" BorderColor="#A8BEDA" BorderWidth="1" />
                        <asp:Label runat="server" ID="lblUnitFrom" Text="(triệu)"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblPriceTo" runat="server" Text="Đến :"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadNumericTextBox ID="txtPriceTo" runat="server" Width="50px" Type="Number"
                            Skin="Office2007" NumberFormat-DecimalDigits="1" NumberFormat-PositivePattern="n"
                            Value="2.0" EnabledStyle-HorizontalAlign="Right" NumberFormat-GroupSeparator=""
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
                    <td></td>
                    <td></td>
                    <td align="left">
                            <asp:Button runat="server" ID="btnSearch" Text="Tìm Phòng" CssClass="flatButton" Width="100"
                                OnClick="OnBtnSearch_Clicked" CausesValidation="true"/>&nbsp;
                        </td>
                </tr>
            </table>
        </div>
        <div id="divResult" runat="server">
            <div>
                <telerik:RadGrid ID="gridRoomResult" GridLines="None" Skin="Office2007" AllowMultiRowSelection="False"
                    EnableAjaxSkinRendering="true" runat="server" AllowPaging="True" AllowSorting="True" AllowCustomPaging="True"
                    PageSize="20" Width="100%" AutoGenerateColumns="false" OnPageSizeChanged="OnGridRoomResult_PageSizeChanged"
                    OnNeedDataSource="OnGridRoomResultNeedDataSource" OnItemDataBound="OnGridRoomResultItemDataBound" 
                    OnSortCommand="OnGridRoomResultSortCommand"
                    >
                    <PagerStyle Mode="NextPrevNumericAndAdvanced"></PagerStyle>
                    <MasterTableView DataKeyNames="PostId" DataMember="PersonName" AllowMultiColumnSorting="True"
                        Width="100%" EditMode="PopUp">
                        <Columns>
                            <telerik:GridBoundColumn UniqueName="DateUpdated" SortExpression="DateUpdated" HeaderText="Ngày đăng tin" DataFormatString="{}{0:dd-MM-yyyy}"
                                DataField="City">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="District" SortExpression="District" HeaderText="Quận"
                                DataField="District">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="Address" SortExpression="Address" HeaderText="Địa chỉ"
                                DataField="Address">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="Description" SortExpression="Description" HeaderText="Mô tả"
                                DataField="Description">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="Price" SortExpression="Price" HeaderText="Price" DataFormatString="{}{0:C2}"
                                DataField="Price">
                            </telerik:GridBoundColumn>
                            
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </div>

        </div>
    </div>  
</asp:Content>
