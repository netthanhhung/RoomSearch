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
    <div id="fb-root"></div>
    <script>        (function (d, s, id) {
            var js, fjs = d.getElementsByTagName(s)[0];
            if (d.getElementById(id)) return;
            js = d.createElement(s); js.id = id;
            js.src = "//connect.facebook.net/en_GB/sdk.js#xfbml=1&version=v2.4";
            fjs.parentNode.insertBefore(js, fjs);
        } (document, 'script', 'facebook-jssdk'));</script>

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
                    <td style="width: 160px">
                        <asp:Label ID="lblLink" runat="server" Text="Link :"></asp:Label>
                    </td>
                    <td colspan="4">
                        <asp:LinkButton ID="btnLink" runat="server" ForeColor="#965EFF" Font-Underline="true" OnClick="OnBtnLinkClicked"/>
                    </td>
                    <td>
                        <%--<asp:Label ID="lblFBShare" runat="server" Text="Link :"></asp:Label>--%>
                        <div class="fb-like" data-href="https://www.facebook.com/pages/Timphongcomvntim-phong-tro/830240707047791" data-layout="standard" data-action="like" data-show-faces="true" data-share="true"></div>
                    </td>

                </tr>
                
                 <tr>
                    <td style="width: 160px">
                        <asp:Label ID="lblGender" runat="server" Text="Tìm :"></asp:Label>
                    </td>
                    <td>
                        <asp:RadioButton ID="radMale" runat="server" Checked="true" Text="Nam" GroupName="Gender" />
                        <asp:RadioButton ID="radFemale" runat="server" Checked="false" Text="Nữ" GroupName="Gender" />
                    </td>
                    <td colspan="4">
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblRealestate" runat="server" Text="Loại nhà/đất:"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadComboBox ID="cbbRealestateType" runat="server" Skin="WebBlue" Height="155px"
                            Width="254px">
                        </telerik:RadComboBox>
                    </td>
                    <td colspan="4">
                    </td>
                </tr>
                <tr>
                    <td style="width: 170px">
                        <asp:Label ID="lblPersonName" runat="server" Text="Tên người đăng(*) :"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtPersonName" runat="server" Width="250px">
                        </telerik:RadTextBox>
                    </td>
                    <td></td>                    
                    <td>
                        <asp:Label ID="lblRoomType" runat="server" Text="Loại phòng :"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadComboBox ID="cbbRoomType" runat="server" Skin="WebBlue" Height="155px"
                            Width="254px">
                        </telerik:RadComboBox>
                    </td>
                    <td></td>                    
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblPhone" runat="server" Text="Số điện thoại(*) :"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtPhoneNumber" runat="server" Width="250px">
                        </telerik:RadTextBox>
                    </td>
                    <td></td>                    
                    <td>
                        <asp:Label ID="lblAvailableRooms" runat="server" Text="Số phòng trống :"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadNumericTextBox ID="txtAvailableRooms" runat="server" Width="50px" Type="Number"
                            Skin="WebBlue" NumberFormat-DecimalDigits="0" NumberFormat-PositivePattern="n"
                            Value="1" EnabledStyle-HorizontalAlign="Right" NumberFormat-GroupSeparator=""
                            BorderStyle="Solid" BorderColor="#A8BEDA" BorderWidth="1" />
                    </td>                
                    <td></td>                    
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblEmail" runat="server" Text="Email :"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtEmail" runat="server" Width="250px">
                        </telerik:RadTextBox>
                    </td>
                    <td></td>                    
                    <td>
                        <asp:Label ID="lblMeterSquare" runat="server" Text="Phòng rộng :"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadNumericTextBox ID="txtMeterSQuare" runat="server" Width="50px" Type="Number"
                            Skin="WebBlue" NumberFormat-DecimalDigits="1" NumberFormat-PositivePattern="n"
                            Value="10.0" EnabledStyle-HorizontalAlign="Right" NumberFormat-GroupSeparator=""
                            BorderStyle="Solid" BorderColor="#A8BEDA" BorderWidth="1" />
                        <asp:Label runat="server" ID="lblMeterQuare" Text="mét vuông (m2)"></asp:Label>
                    </td>
                    <td></td>                    
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCity" runat="server" Text="Thành Phố :"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadComboBox ID="cbbCity" runat="server" Skin="WebBlue" Height="155px"
                            Width="254px" OnClientSelectedIndexChanged="onDropDownCity_ClientIndexChanged">
                        </telerik:RadComboBox>
                    </td>
                    <td></td>                    
                    <td>
                        <asp:Label ID="lblPrice" runat="server" Text="Giá thuê :"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadNumericTextBox ID="txtPrice" runat="server" Width="50px" Type="Number"
                            Skin="WebBlue" NumberFormat-DecimalDigits="1" NumberFormat-PositivePattern="n"
                            Value="2.0" EnabledStyle-HorizontalAlign="Right" NumberFormat-GroupSeparator=""
                            BorderStyle="Solid" BorderColor="#A8BEDA" BorderWidth="1" />
                        <asp:Label runat="server" ID="lblUnit" Text="triệu đồng 1 tháng"></asp:Label>
                    </td>
                    <td></td>                    
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblDistrict" runat="server" Text="Quận / Huyện :"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadComboBox ID="cbbDistrict" runat="server" Skin="WebBlue" Height="155px"
                            Width="254px">
                        </telerik:RadComboBox>
                    </td>
                    <td></td>                    
                    <td>
                        <asp:Label ID="lblPostDate" runat="server" Text="Ngày đăng :"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadDatePicker ID="datPostDate" runat="server" Width="100px" 
                            Skin="WebBlue" EnabledStyle-HorizontalAlign="Right" NumberFormat-GroupSeparator=""
                            BorderStyle="Solid" BorderColor="#A8BEDA" BorderWidth="1" />
                    </td>
                    <td></td>                    
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblAddress" runat="server" Text="Địa chỉ (số và tên đường)(*) :"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtAddress" runat="server" Width="250px">
                        </telerik:RadTextBox>
                    </td>
                    <td colspan="4">
                    </td>
                </tr>

                <tr>                    
                    <td style="width: 100px">
                        <asp:Label ID="lblDescription" runat="server" Text="Mô tả thêm :"></asp:Label>
                    </td>
                    <td rowspan="11" colspan="5">
                        <%--<telerik:RadTextBox ID="txtDescription" runat="server" Width="1000px" Rows="15" TextMode="MultiLine" 
                            BackColor="White" Font-Size="Small">
                        </telerik:RadTextBox>--%>
                        <asp:TextBox ID="txtDescription" runat="server" Width="1000px" TextMode="MultiLine" Rows="15"
                            BackColor="White" Font-Size="Small">
                        </asp:TextBox>
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
                        <asp:Panel ID="panelImage" runat="server" ScrollBars="Auto" Direction="LeftToRight" Width="1250px"></asp:Panel>    
                        <%--<div id="divImage" runat="server" dir="ltr" style="width:1024px; display:table"  ></div>--%>
                    </td>
                </tr>                
                <tr>
                    <td>
                        <telerik:RadUpload runat="server" ID="radUploadMulti" MaxFileSize="1000000" InputSize="66" Width="800px"
                            ControlObjectsVisibility="AddButton" MaxFileInputsCount="10" InitialFileInputsCount="3"
                            Skin="WebBlue" Localization-Add="Thêm hình" Localization-Select="Chọn hình" />
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
