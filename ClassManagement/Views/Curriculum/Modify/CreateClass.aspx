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

        <div style="display: flex; flex-direction: column; gap: 14px; width: 420px; padding: 10px 0;">

            <div style="display: flex;">
                <asp:Label AssociatedControlID="txtName" runat="server" Text="Class Name:"
                    Style="font-weight: 600; margin-bottom: 4px; padding-right: 16px; white-space: nowrap;" />
                <telerik:RadTextBox ID="txtName" runat="server" Width="100%" Style="font-weight: 600;" />
            </div>

            <div style="display: flex; flex-direction: column;">
                <asp:Label AssociatedControlID="ddlType" runat="server" Text="Class Type:"
                    Style="font-weight: 600; margin-bottom: 4px;" />
                <telerik:RadDropDownList ID="ddlType" runat="server" Width="100%">
                    <Items>
                        <telerik:DropDownListItem Text="Basic" Value="Basic" />
                        <telerik:DropDownListItem Text="Advanced" Value="Advanced" />
                    </Items>
                </telerik:RadDropDownList>
            </div>

            <div style="display: flex; flex-direction: column;">
                <asp:Label AssociatedControlID="ddlSubject" runat="server" Text="Subject:" Style="font-weight: 600; margin-bottom: 4px;" />
                <telerik:RadDropDownList ID="ddlSubject" runat="server" Width="100%" Label="Subject:" />
            </div>

            <div style="display: flex; flex-direction: column;">
                <asp:Label AssociatedControlID="autoSchedule" runat="server" Text="Schedule:" Style="font-weight: 600; margin-bottom: 4px;" />
                <telerik:RadAutoCompleteBox ID="autoSchedule" runat="server" Width="100%" InputType="Token">
                    <Entries>
                        <telerik:AutoCompleteBoxEntry Text="2" Value="2" />
                        <telerik:AutoCompleteBoxEntry Text="3" Value="3" />
                        <telerik:AutoCompleteBoxEntry Text="4" Value="4" />
                        <telerik:AutoCompleteBoxEntry Text="5" Value="5" />
                        <telerik:AutoCompleteBoxEntry Text="6" Value="6" />
                        <telerik:AutoCompleteBoxEntry Text="7" Value="7" />
                        <telerik:AutoCompleteBoxEntry Text="CN" Value="CN" />
                    </Entries>
                </telerik:RadAutoCompleteBox>
            </div>

            <div style="display: flex; gap: 12px; align-items: center;">
                <div style="flex: 1;">
                    <asp:Label AssociatedControlID="tpStart" runat="server" Text="Start:" />
                    <telerik:RadTimePicker ID="tpStart" runat="server" Label="Start Time:" Width="100%" />
                </div>
                <div style="flex: 1;">
                    <asp:Label AssociatedControlID="tpEnd" runat="server" Text="End:" />

                    <telerik:RadTimePicker ID="tpEnd" runat="server" Label="End Time:" Width="100%" />
                </div>
            </div>

            <div id="statusField" runat="server" style="display: flex; flex-direction: column;">
                <asp:Label AssociatedControlID="ddlStatus" runat="server" Text="Status:" Style="font-weight: 600; margin-bottom: 4px;" />
                <telerik:RadDropDownList ID="ddlStatus" runat="server" Width="100%">
                    <Items>
                        <telerik:DropDownListItem Text="In-process" Value="In-process" />
                        <telerik:DropDownListItem Text="Finished" Value="Finished" />
                        <telerik:DropDownListItem Text="Cancelled" Value="Cancelled" />
                    </Items>
                </telerik:RadDropDownList>
            </div>

            <div style="margin-top: 10px;">
                <telerik:RadButton ID="btnSave" runat="server" Text="SAVE" OnClick="btnSave_Click" Skin="Material" Style="background-color: #337ab7; color: white;"/>
                <telerik:RadButton ID="btnCancel" runat="server" Text="CANCEL"
                    OnClientClicked="function(){ window.location='../Curriculum.aspx'; }"
                    Skin="Material" />
            </div>
        </div>
    </telerik:RadAjaxPanel>

</asp:Content>
