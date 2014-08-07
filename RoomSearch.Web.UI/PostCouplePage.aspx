<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="Site.master" CodeBehind="PostCouplePage.aspx.cs"
    Inherits="RoomSearch.Web.UI.PostCouplePage" Title="Đăng Tin Ở Ghép" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="local" TagName="PostPanel" Src="~/UserControls/PostPanel.ascx" %>
<asp:Content ID="contentDefault" ContentPlaceHolderID="centreContentPlaceHolder"
    runat="server">

    
    <telerik:RadScriptManager ID="ScriptManager" runat="server" />
    <telerik:RadProgressManager ID="Radprogressmanager1" runat="server" />

    <div>
        <local:PostPanel ID="ucPostPanel" runat="server"/>
    </div>
</asp:Content>
