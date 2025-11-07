<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="CreateSubject.aspx.cs" Inherits="CreateSubject" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content0" ContentPlaceHolderID="head" runat="Server">
    <link href="../../../styles/default.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <telerik:RadAjaxPanel runat="server" ID="ajaxPanel" LoadingPanelID="LoadingPanel">
        <telerik:RadAjaxLoadingPanel ID="LoadingPanel" runat="server" />

        <h2><asp:Literal ID="lblFormTitle" runat="server" /></h2>
        <br />

        <div style="display: flex; flex-direction: column; gap: 12px; width: 350px;">

            <telerik:RadTextBox ID="txtName" runat="server" Width="100%" Label="Subject Name:" />

            <telerik:RadTextBox ID="txtDescription" runat="server" Label="Description:"
                TextMode="MultiLine" Rows="4" Width="100%" />

            <telerik:RadTextBox ID="ddlType" runat="server" Label="Type:"
                TextMode="MultiLine" Rows="2" Width="100%" />

            <telerik:RadSwitch ID="switchStatus" runat="server" OnText="Active" OffText="Inactive" AutoPostBack="false" />

            <div style="margin-top: 10px;">
                <telerik:RadButton ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" Skin="Material" />
                <telerik:RadButton ID="btnCancel" runat="server" Text="Cancel"
                    OnClientClicked="function(){ window.location='../Curriculum.aspx'; }"
                    Skin="Material" />
            </div>
        </div>

    </telerik:RadAjaxPanel>

</asp:Content>