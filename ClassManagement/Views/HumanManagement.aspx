<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="HumanManagement.aspx.cs" Inherits="HumanManagement" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content0" ContentPlaceHolderID="head" runat="Server">
    <link href="../../styles/default.css" rel="stylesheet" />
    <style type="text/css">
        .MyImageButton {
            cursor: pointer;
            cursor: hand;
        }

        .EditFormHeader td {
            font-size: 14px;
            padding: 4px !important;
            color: #0066cc;
        }

        .page-title {
            margin-bottom: 20px;
            color: #333;
        }

        .tab-content {
            padding: 20px;
            background: white;
            border: 1px solid #dee2e6;
            border-top: none;
            min-height: 400px;
        }

        /* Custom tab styling */
        .RadTabStrip {
            margin-bottom: 0 !important;
        }

        .rtsLevel1 {
            border-bottom: 2px solid #007bff !important;
        }

        .rtsLink {
            padding: 12px 24px !important;
            font-size: 14px !important;
            font-weight: 500 !important;
        }

        .rtsSelected .rtsLink {
            background-color: #007bff !important;
            color: white !important;
            border-color: #007bff !important;
        }

        .grid-container {
            margin-top: 0;
        }

            .grid-container h3 {
                margin-top: 0;
                margin-bottom: 15px;
                color: #333;
            }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <h2 class="page-title">Human Management</h2>

    <!-- TabStrip -->
    <telerik:RadTabStrip ID="RadTabStrip1" runat="server"
        MultiPageID="RadMultiPage1"
        SelectedIndex="0"
        Skin="Default">
        <Tabs>
            <telerik:RadTab Text="Teachers" Selected="true" />
            <telerik:RadTab Text="Students" />
        </Tabs>
    </telerik:RadTabStrip>

    <!-- MultiPage Content -->
    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0" CssClass="tab-content">

        <!-- Tab 1: Students -->
        <telerik:RadPageView ID="RadPageView1" runat="server">
            <div class="grid-container">
                <h3>Students Management</h3>

                <telerik:RadGrid ID="RadGrid1" GridLines="None" runat="server"
                    AllowAutomaticDeletes="True"
                    AllowSorting="True"
                    AllowAutomaticInserts="True"
                    PageSize="10"
                    AllowAutomaticUpdates="True"
                    AllowMultiRowEdit="False"
                    AllowPaging="True"
                    OnNeedDataSource="RadGrid1_NeedDataSource"
                    OnItemUpdated="RadGrid1_ItemUpdated"
                    AllowFilteringByColumn="True"
                    OnItemDeleted="RadGrid1_ItemDeleted"
                    OnItemInserted="RadGrid1_ItemInserted"
                    OnDataBound="RadGrid1_DataBound"
                    Skin="Bootstrap">

                    <PagerStyle Mode="NextPrevAndNumeric" />

                    <MasterTableView Width="100%"
                        CommandItemDisplay="Top"
                        DataKeyNames="ID"
                        HorizontalAlign="NotSet"
                        EditMode="EditForms"
                        AllowFilteringByColumn="True"
                        TableLayout="Fixed"
                        AutoGenerateColumns="False">

                        <Columns>
                            <telerik:GridTemplateColumn HeaderText="Full Name" UniqueName="FullName">
                                <ItemTemplate>
                                    <%# Eval("FullName") %>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtFullName" runat="server" Text='<%# Bind("FullName") %>' Width="100%" />
                                </EditItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="Username" HeaderText="Username" UniqueName="Username" />

                            <telerik:GridTemplateColumn HeaderText="Date of Birth" UniqueName="DateOfBirth">
                                <ItemTemplate>
                                    <%# Eval("DateOfBirth", "{0:dd-MM-yyyy}") %>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <telerik:RadDatePicker ID="dpDateOfBirth" runat="server"
                                        DbSelectedDate='<%# Bind("DateOfBirth") %>'
                                        DateInput-DateFormat="dd-MM-yyyy"
                                        Width="100%">
                                    </telerik:RadDatePicker>
                                </EditItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="City" UniqueName="CityLive">
                                <ItemTemplate>
                                    <%# Eval("CityLive") %>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtCity" runat="server" Text='<%# Bind("CityLive") %>' Width="100%" />
                                </EditItemTemplate>
                            </telerik:GridTemplateColumn>

                            <%--<telerik:GridBoundColumn DataField="Active" HeaderText="Active" />--%>
                            <telerik:GridTemplateColumn HeaderText="Active" UniqueName="Active">
                                <ItemTemplate>
                                    <telerik:RadSwitch ID="switchStatus" runat="server"
                                        OnText="Active" OffText="Inactive"
                                        AutoPostBack="false"
                                        Checked='<%# Convert.ToBoolean(Eval("Active")) %>' />
                                </ItemTemplate>

                                <EditItemTemplate>
                                    <telerik:RadSwitch ID="switchStatusEdit" runat="server"
                                        OnText="Active" OffText="Inactive"
                                        AutoPostBack="false"
                                        Checked='<%# Bind("Active") %>' />
                                </EditItemTemplate>
                            </telerik:GridTemplateColumn>
                            <%--<telerik:GridCheckBoxColumn DataField="Active" HeaderText="Active" UniqueName="Active" />--%>

                            <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn">
                                <ItemStyle Width="50px" CssClass="MyImageButton" HorizontalAlign="Center" />
                                <HeaderStyle Width="50px" HorizontalAlign="Center" />
                            </telerik:GridEditCommandColumn>

                            <telerik:GridButtonColumn
                                ConfirmText="Delete this teacher?"
                                ConfirmDialogType="RadWindow"
                                ConfirmTitle="Delete"
                                ButtonType="ImageButton"
                                CommandName="Delete"
                                Text="Delete"
                                UniqueName="DeleteColumn">
                                <ItemStyle Width="60px" HorizontalAlign="Center" CssClass="MyImageButton" />
                                <HeaderStyle Width="60px" />
                            </telerik:GridButtonColumn>
                        </Columns>

                        <EditFormSettings>
                            <FormTableItemStyle Wrap="False"></FormTableItemStyle>
                            <FormCaptionStyle CssClass="EditFormHeader"></FormCaptionStyle>
                            <FormMainTableStyle GridLines="None" CellSpacing="0" CellPadding="3"
                                BackColor="White" Width="100%" />
                            <FormTableAlternatingItemStyle Wrap="False"></FormTableAlternatingItemStyle>
                            <EditColumn ButtonType="ImageButton" UniqueName="EditCommandColumn1"
                                CancelText="Cancel edit" />
                            <FormTableButtonRowStyle HorizontalAlign="Right"
                                CssClass="EditFormButtonRow"></FormTableButtonRowStyle>
                        </EditFormSettings>
                    </MasterTableView>

                    <ClientSettings AllowColumnsReorder="true" AllowColumnHide="true">
                        <Selecting AllowRowSelect="true" />
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                    </ClientSettings>
                </telerik:RadGrid>

                <telerik:RadWindowManager ID="RadWindowManager1" runat="server" />
            </div>
        </telerik:RadPageView>

        <!-- Tab 2: Teachers -->
        <telerik:RadPageView ID="RadPageView2" runat="server">
            <div class="grid-container">
                <h3>Teachers Management</h3>

                <telerik:RadGrid ID="RadGrid2" GridLines="None" runat="server"
                    AllowAutomaticDeletes="True"
                    AllowSorting="True"
                    AllowAutomaticInserts="True"
                    PageSize="10"
                    AllowAutomaticUpdates="True"
                    AllowMultiRowEdit="False"
                    AllowPaging="True"
                    OnNeedDataSource="RadGrid2_NeedDataSource"
                    OnItemUpdated="RadGrid2_ItemUpdated"
                    AllowFilteringByColumn="True"
                    OnItemDeleted="RadGrid2_ItemDeleted"
                    OnItemInserted="RadGrid2_ItemInserted"
                    OnDataBound="RadGrid2_DataBound"
                    Skin="Bootstrap">

                    <PagerStyle Mode="NextPrevAndNumeric" />

                    <MasterTableView Width="100%"
                        CommandItemDisplay="Top"
                        DataKeyNames="ID"
                        HorizontalAlign="NotSet"
                        EditMode="EditForms"
                        AllowFilteringByColumn="True"
                        TableLayout="Fixed"
                        AutoGenerateColumns="False">

                        <Columns>
                            <telerik:GridTemplateColumn HeaderText="Full Name" UniqueName="FullName">
                                <ItemTemplate>
                                    <%# Eval("FullName") %>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtFullName" runat="server" Text='<%# Bind("FullName") %>' Width="100%" />
                                </EditItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="Username" HeaderText="Username" UniqueName="Username" />

                            <telerik:GridTemplateColumn HeaderText="Date of Birth" UniqueName="DateOfBirth">
                                <ItemTemplate>
                                    <%# Eval("DateOfBirth", "{0:dd-MM-yyyy}") %>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <telerik:RadDatePicker ID="dpDateOfBirth" runat="server"
                                        DbSelectedDate='<%# Bind("DateOfBirth") %>'
                                        DateInput-DateFormat="dd-MM-yyyy"
                                        Width="100%">
                                    </telerik:RadDatePicker>
                                </EditItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="City" UniqueName="CityLive">
                                <ItemTemplate>
                                    <%# Eval("CityLive") %>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtCity" runat="server" Text='<%# Bind("CityLive") %>' Width="100%" />
                                </EditItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridCheckBoxColumn DataField="Active" HeaderText="Active" UniqueName="Active" />

                            <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn">
                                <ItemStyle CssClass="MyImageButton" Width="50px" HorizontalAlign="Center" />
                                <HeaderStyle Width="50px" HorizontalAlign="Center" />
                            </telerik:GridEditCommandColumn>

                            <telerik:GridButtonColumn
                                ConfirmText="Delete this student?"
                                ConfirmDialogType="RadWindow"
                                ConfirmTitle="Delete"
                                ButtonType="ImageButton"
                                CommandName="Delete"
                                Text="Delete"
                                UniqueName="DeleteColumn">
                                <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" Width="60px" />
                                <HeaderStyle Width="60px" HorizontalAlign="Center" />
                            </telerik:GridButtonColumn>
                        </Columns>

                        <EditFormSettings>
                            <FormTableItemStyle Wrap="False"></FormTableItemStyle>
                            <FormCaptionStyle CssClass="EditFormHeader"></FormCaptionStyle>
                            <FormMainTableStyle GridLines="None" CellSpacing="0" CellPadding="3"
                                BackColor="White" Width="100%" />
                            <FormTableAlternatingItemStyle Wrap="False"></FormTableAlternatingItemStyle>
                            <EditColumn ButtonType="ImageButton" UniqueName="EditCommandColumn1"
                                CancelText="Cancel edit" />
                            <FormTableButtonRowStyle HorizontalAlign="Right"
                                CssClass="EditFormButtonRow"></FormTableButtonRowStyle>
                        </EditFormSettings>
                    </MasterTableView>

                    <ClientSettings AllowColumnsReorder="true" AllowColumnHide="true">
                        <Selecting AllowRowSelect="true" />
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                    </ClientSettings>
                </telerik:RadGrid>

                <telerik:RadWindowManager ID="RadWindowManager2" runat="server" />
            </div>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>
