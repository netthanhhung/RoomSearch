<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="Site.master" CodeBehind="CreatePostPage.aspx.cs"
    Inherits="RoomSearch.Web.UI.PostRoomPage" Title="Đăng Tin Cho Thuê Phòng" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="contentDefault" ContentPlaceHolderID="centreContentPlaceHolder"
    runat="server">
    <link rel="stylesheet" type="text/css" href="Styles/CSS/appstyle.css" />
    <script type="text/javascript" src="Styles/JS/utils.js"></script>
    <telerik:RadScriptBlock runat="server" ID="scriptBlock">
        <script type="text/javascript" language="javascript">

            function onDropDownCity_ClientIndexChanged(sender, eventArgs) {
                var item = eventArgs.get_item();
                $find("<%= PostRoomAjaxManager.ClientID %>").ajaxRequest("RebindDistrictListByCity-" + item.get_value());
            }

        </script>
    </telerik:RadScriptBlock>
    <telerik:RadScriptManager ID="ScriptManager" runat="server" />
    <telerik:RadProgressManager ID="Radprogressmanager1" runat="server" />
    <div>
        <telerik:RadAjaxManager EnableAJAX="true" runat="server" ID="PostRoomAjaxManager"
            OnAjaxRequest="OnMyAjaxManagerAjaxRequest">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="cbbCity">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="cbbDistrict" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="btnSave">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl LoadingPanelID="pnlRadAjaxLoading" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="pnlRadAjaxLoading" runat="server" Height="75px"
            Width="75px" Transparency="50">
            <img alt="Loading..." src='<%= RadAjaxLoadingPanel.GetWebResourceUrl(Page, "Telerik.Web.UI.Skins.Default.Ajax.loading.gif") %>'
                style="border: 0;" />
        </telerik:RadAjaxLoadingPanel>
        <asp:ValidationSummary runat="server" ID="sumValid" ShowMessageBox="true" ShowSummary="false"
            ValidationGroup="PostRoomValidation" />
        <div id="divMain" runat="server">
            <table>
                <tr>
                    <td style="width: 160px">
                        <asp:Label ID="lblGender" runat="server" Text="Tìm :"></asp:Label>
                    </td>
                    <td>
                        <asp:RadioButton ID="radMale" runat="server" Checked="true" Text="Nam" GroupName="Gender" />
                        <asp:RadioButton ID="radFemale" runat="server" Checked="False" Text="Nữ" GroupName="Gender" />
                    </td>
                    <td colspan="3">
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblRealestate" runat="server" Text="Loại nhà/đất:"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadComboBox ID="cbbRealestateType" runat="server" Skin="Office2007" Height="155px"
                            Width="254px">
                        </telerik:RadComboBox>
                    </td>
                    <td colspan="3">
                    </td>
                </tr>
                <tr>
                    <td style="width: 160px">
                        <asp:Label ID="lblPersonName" runat="server" Text="Tên người đăng(*) :"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtPersonName" runat="server" Width="250px">
                        </telerik:RadTextBox>
                        <asp:RequiredFieldValidator runat="server" ID="rfvPersonName" ControlToValidate="txtPersonName"
                            ErrorMessage="Tên người đăng không hợp lệ." ValidationGroup="PostRoomValidation"
                            Display="None" EnableClientScript="true"></asp:RequiredFieldValidator>
                    </td>
                    <td style="width: 10px">
                    </td>
                    <td style="width: 100px">
                        <asp:Label ID="lblDescription" runat="server" Text="Mô tả thêm :"></asp:Label>
                    </td>
                    <td rowspan="11">
                        <telerik:RadTextBox ID="txtDescription" runat="server" Width="430px" Rows="15" TextMode="multiLine"
                            Skin="Office2007" BackColor="White">
                        </telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblPhone" runat="server" Text="Số điện thoại(*) :"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtPhoneNumber" runat="server" Width="250px">
                        </telerik:RadTextBox>
                        <asp:RequiredFieldValidator runat="server" ID="rfvPhoneNumber" ControlToValidate="txtPhoneNumber"
                            ErrorMessage="Số điện thoại không hợp lệ." ValidationGroup="PostRoomValidation"
                            Display="None" EnableClientScript="true"></asp:RequiredFieldValidator>
                    </td>
                    <td colspan="3">
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblEmail" runat="server" Text="Email :"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtEmail" runat="server" Width="250px">
                        </telerik:RadTextBox>
                        <asp:RegularExpressionValidator runat="server" ID="revEmail" ControlToValidate="txtEmail"
                            Display="none" ErrorMessage="Email không hợp lệ." ValidationGroup="PostRoomValidation"
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                    </td>
                    <td colspan="3">
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCity" runat="server" Text="Thành Phố :"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadComboBox ID="cbbCity" runat="server" Skin="Office2007" Height="155px"
                            Width="254px" OnClientSelectedIndexChanged="onDropDownCity_ClientIndexChanged">
                        </telerik:RadComboBox>
                    </td>
                    <td colspan="3">
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblDistrict" runat="server" Text="Quận / Huyện :"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadComboBox ID="cbbDistrict" runat="server" Skin="Office2007" Height="155px"
                            Width="254px">
                        </telerik:RadComboBox>
                    </td>
                    <td colspan="3">
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblAddress" runat="server" Text="Địa chỉ (số và tên đường)(*) :"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtAddress" runat="server" Width="250px">
                        </telerik:RadTextBox>
                        <asp:RequiredFieldValidator runat="server" ID="rfvAddress" ControlToValidate="txtAddress"
                            ErrorMessage="Địa chỉ không hợp lệ." ValidationGroup="PostRoomValidation" Display="None"
                            EnableClientScript="true"></asp:RequiredFieldValidator>
                    </td>
                    <td colspan="3">
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblRoomType" runat="server" Text="Loại phòng :"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadComboBox ID="cbbRoomType" runat="server" Skin="Office2007" Height="155px"
                            Width="254px">
                        </telerik:RadComboBox>
                    </td>
                    <td colspan="3">
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblAvailableRooms" runat="server" Text="Số phòng trống :"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadNumericTextBox ID="txtAvailableRooms" runat="server" Width="50px" Type="Number"
                            Skin="Office2007" NumberFormat-DecimalDigits="0" NumberFormat-PositivePattern="n"
                            Value="1" EnabledStyle-HorizontalAlign="Right" NumberFormat-GroupSeparator=""
                            BorderStyle="Solid" BorderColor="#A8BEDA" BorderWidth="1" />
                    </td>
                    <td colspan="3">
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblMeterSquare" runat="server" Text="Phòng rộng :"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadNumericTextBox ID="txtMeterSquare" runat="server" Width="50px" Type="Number"
                            Skin="Office2007" NumberFormat-DecimalDigits="1" NumberFormat-PositivePattern="n"
                            Value="4.0" EnabledStyle-HorizontalAlign="Right" NumberFormat-GroupSeparator=""
                            BorderStyle="Solid" BorderColor="#A8BEDA" BorderWidth="1" />
                        <asp:Label runat="server" ID="lblMeterUni" Text="mét vuông (m2)"></asp:Label>
                    </td>
                    <td colspan="3">
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblPrice" runat="server" Text="Giá thuê :"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadNumericTextBox ID="txtPrice" runat="server" Width="50px" Type="Number"
                            Skin="Office2007" NumberFormat-DecimalDigits="1" NumberFormat-PositivePattern="n"
                            Value="2.0" EnabledStyle-HorizontalAlign="Right" NumberFormat-GroupSeparator=""
                            BorderStyle="Solid" BorderColor="#A8BEDA" BorderWidth="1" />
                        <asp:Label runat="server" ID="lblUnit" Text="triệu đồng 1 tháng"></asp:Label>
                    </td>
                    <td colspan="3">
                    </td>
                </tr>
            </table>
            <div style="text-align: left; width: 100%">
                <table width="100%">
                    <tr>
                        <td>
                            <asp:Label ID="lblUploadImages" runat="server" Text="Hình ảnh :"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadUpload runat="server" ID="radUploadMulti" InputSize="66" Width="800px"
                                MaxFileSize="1000000" ControlObjectsVisibility="AddButton" MaxFileInputsCount="5"
                                InitialFileInputsCount="1" Skin="Office2007" Localization-Add="Thêm hình" Localization-Select="Chọn hình" />
                            <telerik:RadProgressArea runat="server" ID="radProgress" ProgressIndicators="TotalProgressBar, FilesCountBar, TimeEstimated, TransferSpeed">
                            </telerik:RadProgressArea>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="text-align: center; width: 100%">
                <table style="width: 100%">
                    <tr>
                        <td align="center" style="width: 100%">
                            <asp:Button runat="server" ID="btnSave" Text="Đăng Tin" CssClass="flatButton" Width="80"
                                OnClick="OnBtnSave_Clicked" CausesValidation="true" ValidationGroup="PostRoomValidation" />&nbsp;
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div id="divAfterPost" runat="server">
            <table style="width: 100%">
                <tr>
                    <td align="left">
                        <asp:Button runat="server" ID="btnContinuePost" Text="Đăng Tin Tiếp" CssClass="flatButton"
                            Width="100" OnClick="OnBtnContinuePost_Clicked" />&nbsp;
                    </td>
                    <td align="left">
                        <asp:Button runat="server" ID="btnSearchPost" Text="Tìm Phòng" CssClass="flatButton"
                            Width="80" OnClick="OnBtnSearchPost_Clicked" />&nbsp;
                    </td>
                    <td style="width: 100%">
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
