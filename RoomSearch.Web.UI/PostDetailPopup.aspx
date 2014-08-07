<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PostDetailPopup.aspx.cs" Inherits="RoomSearch.Web.UI.PostDetailPopup" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="local" TagName="ImagePanel" Src="~/UserControls/ImagePanel.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="head" runat="server">
    <title>Chi tiết phòng</title>
    <link rel="stylesheet" type="text/css" href="Styles/CSS/appstyle.css" />
    <link rel="stylesheet" type="text/css" href="Styles/CSS/font-awesome.css"/>
    <link rel="stylesheet" type="text/css" href="Styles/CSS/menu.css"/>
    <script type="text/javascript" src="Styles/JS/utils.js"></script>
    <script type="text/javascript" language="javascript">

        function onDropDownCity_ClientIndexChanged(sender, eventArgs) {
            var item = eventArgs.get_item();
            $find("PostDetailsAjaxManager").ajaxRequest("RebindDistrictListByCity-" + item.get_value());
        }

        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow)
                oWindow = window.radWindow;
            else if (window.frameElement.radWindow)
                oWindow = window.frameElement.radWindow;
            return oWindow;
        }

        function OnBtnCancelClientClicked(sender, eventArgs) {
            var currentWindow = GetRadWindow();
            var isReload = "No";
            currentWindow.argument = isReload;
            currentWindow.close();
        }


        function OnBtnSaveClientClicked() {
            var currentWindow = GetRadWindow();
            var isReload = "Yes";
            currentWindow.argument = isReload;
            currentWindow.close();
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="ScriptManager" runat="server" />
    
    <div>     
        <telerik:RadAjaxManager EnableAJAX="true" runat="server" ID="PostDetailsAjaxManager"
            OnAjaxRequest="OnMyAjaxManagerAjaxRequest">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="cbbCity">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="cbbDistrict" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="panelImage">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="panelImage" />
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

         <table>
                <tr>
                    <td style="width: 170px">
                        <asp:Label ID="lblPersonName" runat="server" Text="Tên người đăng(*) :"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtPersonName" runat="server" Width="250px">
                        </telerik:RadTextBox>
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
                        <telerik:RadNumericTextBox ID="txtMeterSQuare" runat="server" Width="50px" Type="Number"
                            Skin="Office2007" NumberFormat-DecimalDigits="1" NumberFormat-PositivePattern="n"
                            Value="4.0" EnabledStyle-HorizontalAlign="Right" NumberFormat-GroupSeparator=""
                            BorderStyle="Solid" BorderColor="#A8BEDA" BorderWidth="1" />
                        <asp:Label runat="server" ID="lblMeterQuare" Text="mét vuông (m2)"></asp:Label>
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
         <div>
            <table>
                <tr>
                    <td >
                        <asp:Label ID="lblImages" runat="server" Text="Hình ảnh :" ></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>     
                        <asp:Panel ID="panelImage" runat="server" ScrollBars="Auto" Direction="LeftToRight" Width="950px"></asp:Panel>    
                        <%--<div id="divImage" runat="server" dir="ltr" style="width:1024px; display:table"  ></div>--%>
                    </td>
                </tr>                
                <tr>
                    <td>
                        <telerik:RadUpload runat="server" ID="radUploadMulti" MaxFileSize="1000000" InputSize="66" Width="800px"
                            ControlObjectsVisibility="AddButton" MaxFileInputsCount="5" InitialFileInputsCount="1"
                            Skin="Office2007" Localization-Add="Thêm hình" Localization-Select="Chọn hình" />
                    </td>
                </tr>
                <%--<tr>
                    <td>     
                        <local:ImagePanel ID="ucImagePanel" runat="server" OnDeleteClicked="OnImagePanel_DeleteClicked" />
                    </td>
                </tr> --%>
            </table>
         </div>
        <div style="text-align:center">
        
            <asp:Button runat="server" ID="btnSave" Text="Lưu" CssClass="flatButton" Width="60" OnClick="OnBtnSaveClicked"/>&nbsp;
            <asp:Button runat="server" ID="btnCancel" Text="Đóng" CssClass="flatButton" Width="60" OnClientClick="OnBtnCancelClientClicked()"/>
        </div>
    </div>
    </form>
</body>
</html>
