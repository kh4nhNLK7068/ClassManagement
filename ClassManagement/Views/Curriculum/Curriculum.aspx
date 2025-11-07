<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Curriculum.aspx.cs" Inherits="Curriculum" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content0" ContentPlaceHolderID="head" runat="Server">
    <link href="../../styles/default.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <h2 style="margin-bottom: 20px; color: #333;">Subject List</h2>

    <telerik:RadGrid ID="RadGridSubject" runat="server" AllowPaging="true" AllowSorting="true"
        PageSize="10" AutoGenerateColumns="False"
        OnNeedDataSource="RadGridSubject_NeedDataSource"
        OnDetailTableDataBind="RadGridClass_DetailTableDataBind">

        <MasterTableView DataKeyNames="ID">
            <Columns>
                <telerik:GridBoundColumn DataField="Name" HeaderText="Subject Name" />
                <telerik:GridBoundColumn DataField="Description" HeaderText="Description" />
                <telerik:GridBoundColumn DataField="Type" HeaderText="Type" />
                <telerik:GridBoundColumn DataField="Status" HeaderText="Status" />
            </Columns>

            <DetailTables>
                <telerik:GridTableView Name="Classes" DataKeyNames="ID" Width="100%">
                    <Columns>
                        <telerik:GridBoundColumn DataField="Name" HeaderText="Class Name" />
                        <telerik:GridBoundColumn DataField="Type" HeaderText="Type" />
                        <telerik:GridBoundColumn DataField="SubjectName" HeaderText="Subject" />
                        <telerik:GridBoundColumn DataField="ScheduledClass" HeaderText="Schedule" />
                        <telerik:GridBoundColumn DataField="TimeStart" HeaderText="Start" />
                        <telerik:GridBoundColumn DataField="TimeEnd" HeaderText="End" />
                        <telerik:GridBoundColumn DataField="TotalStudent" HeaderText="Total Student" />
                        <telerik:GridBoundColumn DataField="Status" HeaderText="Status" />
                    </Columns>

                    <DetailTables>
                        <telerik:GridTableView Name="Students" DataKeyNames="ID" Width="100%">
                            <Columns>
                                <telerik:GridBoundColumn DataField="FullName" HeaderText="Student Name" />
                                <telerik:GridBoundColumn DataField="DoB" HeaderText="Birthday" DataFormatString="{0:dd-MM-yyyy}" />
                                <telerik:GridBoundColumn DataField="CityLive" HeaderText="City" />
                                <telerik:GridBoundColumn DataField="Status" HeaderText="Status" />
                            </Columns>
                        </telerik:GridTableView>
                    </DetailTables>
                </telerik:GridTableView>
            </DetailTables>

        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>

