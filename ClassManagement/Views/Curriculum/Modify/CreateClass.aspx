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

        <div class="content" style="display: flex;">
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
                    <asp:Label AssociatedControlID="cboSchedule" runat="server" Text="Schedule:" Style="font-weight: 600; margin-bottom: 4px;" />
                    <telerik:RadComboBox
                        ID="cboSchedule"
                        runat="server"
                        Width="100%"
                        CheckBoxes="true"
                        EnableCheckAllItemsCheckBox="false"
                        EmptyMessage="Select schedule...">
                        <Items>
                            <telerik:RadComboBoxItem Text="Mon" Value="2" />
                            <telerik:RadComboBoxItem Text="Tue" Value="3" />
                            <telerik:RadComboBoxItem Text="Wed" Value="4" />
                            <telerik:RadComboBoxItem Text="Thu" Value="5" />
                            <telerik:RadComboBoxItem Text="Fri" Value="6" />
                            <telerik:RadComboBoxItem Text="Sat" Value="7" />
                            <telerik:RadComboBoxItem Text="Sun" Value="CN" />
                        </Items>
                    </telerik:RadComboBox>
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
                    <telerik:RadButton ID="btnSave" runat="server" Text="SAVE" OnClick="btnSave_Click" Skin="Material" Style="background-color: #337ab7; color: white;" />
                    <telerik:RadButton ID="btnCancel" runat="server" Text="CANCEL"
                        OnClientClicked="function(){ window.location='../Curriculum.aspx'; }"
                        Skin="Material" />
                </div>
            </div>

            <telerik:RadGrid ID="RadGridStudent" runat="server" AllowPaging="true" AllowSorting="true"
                PageSize="10" AutoGenerateColumns="False"
                OnNeedDataSource="RadGridStudent_NeedDataSource"
                Style="flex: 1; padding: 10px 15px; border: none;">

                <MasterTableView DataKeyNames="ID" >
                    <Columns>
                        <telerik:GridBoundColumn DataField="Fullname" HeaderText="Full name" />
                        <telerik:GridBoundColumn DataField="DoB" HeaderText="Birthday" DataFormatString="{0:dd-MM-yyyy}" />
                        <telerik:GridBoundColumn DataField="CityLive" HeaderText="City" />
                        <telerik:GridBoundColumn DataField="Status" HeaderText="Status" />
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </div>
    </telerik:RadAjaxPanel>
</asp:Content>
