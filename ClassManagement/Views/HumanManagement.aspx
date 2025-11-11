<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HumanManagement.aspx.cs" Inherits="HumanManagement" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Human Management</title>
    <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" />
    <style type="text/css">
        body {
            margin: 0;
            padding: 20px;
            font-family: Arial, sans-serif;
        }

        .MyImageButton {
            cursor: pointer;
            cursor: hand;
        }

        .EditFormHeader td {
            font-size: 14px;
            padding: 4px !important;
            color: #0066cc;
        }

        .page-header {
            background-color: #f8f9fa;
            padding: 20px;
            margin-bottom: 20px;
            border-bottom: 2px solid #dee2e6;
        }

        .page-header h2 {
            margin: 0;
            color: #333;
        }

        .tab-content {
            padding: 20px;
            background: white;
            border: 1px solid #dee2e6;
            border-top: none;
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
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js" />
            </Scripts>
        </telerik:RadScriptManager>

        <div class="page-header">
            <h2>HUMAN MANAGEMENT</h2>
        </div>

        <!-- TabStrip -->
        <telerik:RadTabStrip ID="RadTabStrip1" runat="server" 
            MultiPageID="RadMultiPage1"
            SelectedIndex="0"
            Skin="Default">
            <Tabs>
                <telerik:RadTab Text="Students" Selected="true" />
                <telerik:RadTab Text="Teachers" />
            </Tabs>
        </telerik:RadTabStrip>

        <!-- MultiPage Content -->
        <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0" CssClass="tab-content">
            
            <!-- Tab 1: Students -->
            <telerik:RadPageView ID="RadPageView1" runat="server">
                <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
                    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" />
                    
                    <h3 style="margin-top: 0;">Student Information</h3>
                    
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
                            DataKeyNames=""
                            HorizontalAlign="NotSet" 
                            EditMode="EditForms">
                            
                            <Columns>
                                <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn">
                                    <ItemStyle CssClass="MyImageButton" />
                                </telerik:GridEditCommandColumn>
                                
                                <telerik:GridButtonColumn 
                                    ConfirmText="Delete this student?" 
                                    ConfirmDialogType="RadWindow"
                                    ConfirmTitle="Delete" 
                                    ButtonType="ImageButton" 
                                    CommandName="Delete" 
                                    Text="Delete"
                                    UniqueName="DeleteColumn">
                                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
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
                    </telerik:RadGrid>
                    
                    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" />
                </telerik:RadAjaxPanel>
            </telerik:RadPageView>

            <!-- Tab 2: Teachers -->
            <telerik:RadPageView ID="RadPageView2" runat="server">
                <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" LoadingPanelID="RadAjaxLoadingPanel2">
                    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel2" runat="server" />
                    
                    <h3 style="margin-top: 0;">Teacher Information</h3>
                    
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
                            DataKeyNames=""
                            HorizontalAlign="NotSet" 
                            EditMode="EditForms">
                            
                            <Columns>
                                <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn">
                                    <ItemStyle CssClass="MyImageButton" />
                                </telerik:GridEditCommandColumn>
                                
                                <telerik:GridButtonColumn 
                                    ConfirmText="Delete this teacher?" 
                                    ConfirmDialogType="RadWindow"
                                    ConfirmTitle="Delete" 
                                    ButtonType="ImageButton" 
                                    CommandName="Delete" 
                                    Text="Delete"
                                    UniqueName="DeleteColumn">
                                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
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
                    </telerik:RadGrid>
                    
                    <telerik:RadWindowManager ID="RadWindowManager2" runat="server" />
                </telerik:RadAjaxPanel>
            </telerik:RadPageView>
        </telerik:RadMultiPage>
    </form>
</body>
</html>