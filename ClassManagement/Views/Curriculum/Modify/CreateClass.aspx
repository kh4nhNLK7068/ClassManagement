<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="CreateClass.aspx.cs" Inherits="CreateClass" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content0" ContentPlaceHolderID="head" runat="Server">
    <link href="../../../styles/default.css" rel="stylesheet" />
    <style>
        .add-student-section {
            padding: 15px;
            background: #f8f9fa;
            border-radius: 4px;
            margin-bottom: 10px;
        }
        .search-result-item {
            padding: 8px 12px;
            cursor: pointer;
            border-bottom: 1px solid #eee;
        }
        .search-result-item:hover {
            background-color: #e9ecef;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h2>
        <asp:Literal ID="lblFormTitle" runat="server" /></h2>
    <br />

    <div class="content" style="display: flex;">
        <!-- Left form -->
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

            <div id="subjectField" runat="server" style="display: flex; flex-direction: column;">
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
                <telerik:RadButton ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" Skin="Material" Style="background-color: #337ab7; color: white;" />
                <telerik:RadButton ID="btnCancel" runat="server" Text="Cancel"
                    OnClientClicked="function(){ window.location='../Curriculum.aspx'; }"
                    Skin="Material" />
            </div>
        </div>

        <!-- Rigt Grid with Add Student custom-->
        <div style="flex: 1; padding: 10px 15px;">
            <!-- Custom Add Student Section -->
            <div id="addStudentSection" runat="server" class="add-student-section">
                <h4 style="margin-top: 0;">Add Student to Class</h4>
                <div style="display: flex; gap: 10px; align-items: flex-start;">
                    <div style="flex: 1; position: relative;">
                        <telerik:RadTextBox 
                            ID="txtSearchStudent" 
                            runat="server" 
                            Width="100%" 
                            EmptyMessage="Type student name to search..."
                            AutoPostBack="true"
                            OnTextChanged="txtSearchStudent_TextChanged">
                        </telerik:RadTextBox>
                        
                        <!-- Search Results Dropdown -->
                        <asp:Panel ID="pnlSearchResults" runat="server" 
                            Style="position: absolute; top: 100%; left: 0; right: 0; 
                                   background: white; border: 1px solid #ccc; 
                                   max-height: 300px; overflow-y: auto; 
                                   z-index: 1000; display: none;"
                            CssClass="search-results-panel">
                            <asp:Repeater ID="rptSearchResults" runat="server">
                                <ItemTemplate>
                                    <div class="search-result-item">
                                        <asp:LinkButton 
                                            ID="btnSelectStudent" 
                                            runat="server"
                                            CommandArgument='<%# Eval("ID") %>'
                                            OnClick="btnSelectStudent_Click"
                                            Style="text-decoration: none; color: inherit; display: block;">
                                            <strong><%# Eval("FullName") %></strong><br />
                                            <small>DoB: <%# Eval("DoB", "{0:dd-MM-yyyy}") %> | City: <%# Eval("CityLive") %></small>
                                        </asp:LinkButton>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </asp:Panel>
                    </div>
                    <telerik:RadButton 
                        ID="btnAddStudent" 
                        runat="server" 
                        Text="Add" 
                        OnClick="btnAddStudent_Click"
                        Skin="Material" 
                        Style="background-color: #28a745; color: white;" />
                </div>
                
                <!-- Selected Student Info -->
                <asp:Panel ID="pnlSelectedStudent" runat="server" Visible="false" 
                    Style="margin-top: 10px; padding: 10px; background: white; border-radius: 4px;">
                    <asp:HiddenField ID="hfSelectedStudentId" runat="server" />
                    <strong>Selected:</strong> <asp:Label ID="lblSelectedStudent" runat="server" />
                </asp:Panel>
            </div>

            <!-- Student Grid -->
            <telerik:RadGrid ID="RadGridStudent" runat="server" 
                AllowPaging="true" 
                AllowSorting="true"
                PageSize="10" 
                PagerStyle-PageButtonCount="5" 
                AutoGenerateColumns="False"
                OnNeedDataSource="RadGridStudent_NeedDataSource"
                OnDeleteCommand="RadGridStudent_DeleteCommand"
                EnableViewState="true">
                
                <MasterTableView AutoGenerateColumns="False"
                    AllowFilteringByColumn="true" 
                    TableLayout="Fixed"
                    DataKeyNames="ID">
                    <Columns>
                        <telerik:GridBoundColumn DataField="FullName" HeaderText="Full name" />
                        <telerik:GridBoundColumn DataField="DoB" HeaderText="Birthday" DataFormatString="{0:dd-MM-yyyy}" />
                        <telerik:GridBoundColumn DataField="CityLive" HeaderText="City" />
                        <telerik:GridBoundColumn DataField="Status" HeaderText="Status" />
                        <telerik:GridButtonColumn 
                            CommandName="Delete" 
                            Text="Remove" 
                            UniqueName="DeleteColumn"
                            ButtonType="LinkButton"
                            ConfirmText="Remove this student from class?" />
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </div>
    </div>

    <script type="text/javascript">
        function showSearchResults() {
            var panel = document.querySelector('.search-results-panel');
            if (panel) panel.style.display = 'block';
        }
        
        function hideSearchResults() {
            setTimeout(function() {
                var panel = document.querySelector('.search-results-panel');
                if (panel) panel.style.display = 'none';
            }, 200);
        }
        
        // Hide results when clicking outside
        document.addEventListener('click', function(e) {
            var searchBox = document.getElementById('<%= txtSearchStudent.ClientID %>');
            var panel = document.querySelector('.search-results-panel');
            if (panel && searchBox && !searchBox.contains(e.target) && !panel.contains(e.target)) {
                panel.style.display = 'none';
            }
        });
    </script>
</asp:Content>