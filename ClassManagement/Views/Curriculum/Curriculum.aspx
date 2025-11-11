<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Curriculum.aspx.cs" Inherits="Curriculum" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content0" ContentPlaceHolderID="head" runat="Server">
    <link href="../../styles/default.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <h2 style="margin-bottom: 20px; color: #333; position: relative;">Curriculum Management</h2>

    <telerik:RadDropDownList ID="ddlCreateNew" runat="server" Width="150px" Style="position: absolute; padding-right: 20px; right: 0; top: 0;"
        DefaultMessage="Create new"
        AutoPostBack="true"
        OnSelectedIndexChanged="ddlCreateNew_SelectedIndexChanged">
        <Items>
            <telerik:DropDownListItem Text="Subject" Value="Subject" />
            <telerik:DropDownListItem Text="Class" Value="Class" />
        </Items>
    </telerik:RadDropDownList>

    <telerik:RadGrid ID="RadGridSubject" runat="server" AllowPaging="true" AllowSorting="true"
        PageSize="10" PagerStyle-PageButtonCount="5" AutoGenerateColumns="False"
        OnNeedDataSource="RadGridSubject_NeedDataSource"
        OnDetailTableDataBind="RadGridClass_DetailTableDataBind"
        OnItemCommand="RadGridSubject_ItemCommand">

        <MasterTableView AllowFilteringByColumn="true"
            TableLayout="Fixed"
            DataKeyNames="ID">
            <Columns>
                <telerik:GridTemplateColumn HeaderText="Subject Name">
                    <ItemTemplate>
                        <asp:LinkButton
                            ID="lnkSubject"
                            runat="server"
                            Text='<%# Eval("Name") %>'
                            CommandName="EditSubject"
                            CommandArgument='<%# Eval("ID") %>'
                            Style="text-decoration: none;" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="Description" HeaderText="Description" />
                <telerik:GridBoundColumn DataField="Type" HeaderText="Type" />
                <telerik:GridBoundColumn DataField="Status" HeaderText="Status" />
            </Columns>

            <DetailTables>
                <telerik:GridTableView Name="Classes" DataKeyNames="ID" Width="100%">
                    <Columns>
                        <telerik:GridTemplateColumn HeaderText="Class Name">
                            <ItemTemplate>
                                <asp:LinkButton
                                    ID="lnkClass"
                                    runat="server"
                                    Text='<%# Eval("Name") %>'
                                    CommandName="EditClass"
                                    CommandArgument='<%# Eval("ID") %>'
                                    Style="text-decoration: none;" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="Type" HeaderText="Type" />
                        <telerik:GridBoundColumn DataField="SubjectName" HeaderText="Subject" />
                        <telerik:GridBoundColumn DataField="ScheduledClass" HeaderText="Schedule" />
                        <telerik:GridBoundColumn DataField="TimeStart" HeaderText="Start" />
                        <telerik:GridBoundColumn DataField="TimeEnd" HeaderText="End" />
                        <telerik:GridBoundColumn DataField="TotalStudent" HeaderText="Total Student" />
                        <telerik:GridBoundColumn DataField="Status" HeaderText="Status" />
                    </Columns>
                </telerik:GridTableView>
            </DetailTables>
        </MasterTableView>
        <ClientSettings AllowColumnsReorder="true" AllowColumnHide="true" AllowDragToGroup="true">
            <Selecting AllowRowSelect="true" />
            <Scrolling AllowScroll="true" UseStaticHeaders="true" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>

