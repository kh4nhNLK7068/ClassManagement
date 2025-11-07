<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="CreateClass.aspx.cs" Inherits="CreateClass" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content0" ContentPlaceHolderID="head" runat="Server">
    <link href="../../../styles/default.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <telerik:RadAjaxPanel runat="server" ID="ajaxPanel" LoadingPanelID="LoadingPanel">
        <telerik:RadAjaxLoadingPanel ID="LoadingPanel" runat="server" />

        <h2>
            <asp:Literal ID="lblFormTitle" runat="server" /></h2>
        <br />

        <div style="display: flex; flex-direction: column; gap: 12px; width: 350px;">

            <telerik:RadTextBox ID="txtName" runat="server" Label="Class Name:" Width="100%" />

            <telerik:RadDropDownList ID="ddlType" runat="server" Width="100%" Label="Class Type:">
                <Items>
                    <telerik:DropDownListItem Text="Basic" Value="Basic" />
                    <telerik:DropDownListItem Text="Advanced" Value="Advanced" />
                </Items>
            </telerik:RadDropDownList>

            <telerik:RadDropDownList ID="ddlSubject" runat="server" Width="100%" Label="Subject:" />

            <asp:CheckBoxList ID="chkSchedule" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" CssClass="schedule-list">
                <asp:ListItem Text="2" Value="2" />
                <asp:ListItem Text="3" Value="3" />
                <asp:ListItem Text="4" Value="4" />
                <asp:ListItem Text="5" Value="5" />
                <asp:ListItem Text="6" Value="6" />
                <asp:ListItem Text="7" Value="7" />
                <asp:ListItem Text="CN" Value="CN" />
            </asp:CheckBoxList>

            <telerik:RadTimePicker ID="tpStart" runat="server" Label="Start Time:" />
            <telerik:RadTimePicker ID="tpEnd" runat="server" Label="End Time:" />

            <telerik:RadDropDownList ID="ddlStatus" runat="server" Width="100%" Label="Satus:">
                <Items>
                    <telerik:DropDownListItem Text="In-process" Value="In-process" />
                    <telerik:DropDownListItem Text="Finished" Value="Finished" />
                    <telerik:DropDownListItem Text="Cancelled" Value="Cancelled" />
                </Items>
            </telerik:RadDropDownList>

            <div style="margin-top: 10px;">
                <telerik:RadButton ID="btnSave" runat="server" Text="SAVE" OnClick="btnSave_Click" Skin="Material" />
                <telerik:RadButton ID="btnCancel" runat="server" Text="CANCEL"
                    OnClientClicked="function(){ window.location='../Curriculum.aspx'; }"
                    Skin="Material" />
            </div>
        </div>
    </telerik:RadAjaxPanel>

</asp:Content>
