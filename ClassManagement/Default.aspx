<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ClassManagement.Default" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content0" ContentPlaceHolderID="head" Runat="Server">
    <link href="styles/default.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <telerik:RadPageLayout runat="server" ID="RadPageLayout1">
        <Rows>
            <telerik:LayoutRow>
                <Columns>
                    <telerik:LayoutColumn CssClass="jumbotron">
                        <h1>Class Management System</h1>
                    </telerik:LayoutColumn>
                </Columns>
            </telerik:LayoutRow>
            <telerik:LayoutRow>
                <Columns>
                    <telerik:LayoutColumn HiddenMd="true" HiddenSm="true" HiddenXs="true">
                        </br>Class Management System is a classroom management web application built with ASP.NET Web Forms (Telerik UI). The system supports multiple user roles (IT, Teacher, Student) with management, statistics, and visual data display functions via Dashboard charts.
                    </telerik:LayoutColumn>
                </Columns>
            </telerik:LayoutRow>
        </Rows>
    </telerik:RadPageLayout>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <telerik:RadPageLayout runat="server" ID="Content1">
        <Rows>
            <telerik:LayoutRow>
                <Columns>
                    <telerik:LayoutColumn Span="4" SpanMd="12" SpanSm="12" HiddenXs="true">
                        <h4>Class Management System.</h4>
                        <p><strong>Class Management System</strong> is a classroom management web application built with ASP.NET Web Forms (Telerik UI). The system supports multiple user roles (IT, Teacher, Student) with management, statistics, and visual data display functions via Dashboard charts.</p>
                        <p>...</p>
                        <telerik:RadButton runat="server" ID="RadButton1" Text="Button" ButtonType="SkinnedButton"></telerik:RadButton>
                    </telerik:LayoutColumn>

                    <telerik:LayoutColumn Span="4" SpanMd="12" SpanSm="12" HiddenXs="true">
                        <h4>Class Management System.</h4>
                        <p><strong>Class Management System</strong> is a classroom management web application built with ASP.NET Web Forms (Telerik UI). The system supports multiple user roles (IT, Teacher, Student) with management, statistics, and visual data display functions via Dashboard charts.</p>
                        <p>...</p>
                        <telerik:RadButton runat="server" ID="RadButton2" Text="Button" ButtonType="SkinnedButton"></telerik:RadButton>
                    </telerik:LayoutColumn>

                    <telerik:LayoutColumn Span="4" SpanMd="12" SpanSm="12" HiddenXs="true">
                        <h4>Class Management System.</h4>
                        <p><strong>Class Management System</strong> is a classroom management web application built with ASP.NET Web Forms (Telerik UI). The system supports multiple user roles (IT, Teacher, Student) with management, statistics, and visual data display functions via Dashboard charts.</p>
                        <p>...</p>
                        <telerik:RadButton runat="server" ID="RadButton3" Text="Button" ButtonType="SkinnedButton"></telerik:RadButton>
                    </telerik:LayoutColumn>
                </Columns>
            </telerik:LayoutRow>
        </Rows>
    </telerik:RadPageLayout>
</asp:Content>
