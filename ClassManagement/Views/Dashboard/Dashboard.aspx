<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Dashboard" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content0" ContentPlaceHolderID="head" runat="Server">
    <link href="../../styles/default.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h2 style="margin-bottom: 20px; color: #333;">Dashboard</h2>
    <telerik:RadHtmlChart runat="server" ID="RadHtmlChart1" Width="700px" Height="450px" Style="display: inline-block; vertical-align: top;">
        <ChartTitle Text="Active status of student" />
        <PlotArea>
            <Series>
                <telerik:PieSeries DataFieldY="Value" NameField="Browser" ExplodeField="Explode"
                    StartAngle="90">
                    <LabelsAppearance Position="OutsideEnd" DataFormatString="{0:N0}" />
                    <TooltipsAppearance DataFormatString="{0:N0} students" />
                </telerik:PieSeries>
            </Series>
        </PlotArea>
        <Legend>
            <Appearance Position="Right" />
        </Legend>
    </telerik:RadHtmlChart>

    <telerik:RadHtmlChart runat="server" ID="RadHtmlChart2" Width="700px" Height="450px" Style="display: inline-block; vertical-align: top;">
        <ChartTitle Text="Active status of teacher" />
        <PlotArea>
            <Series>
                <telerik:PieSeries DataFieldY="Value" NameField="Browser" ExplodeField="Explode"
                    StartAngle="90">
                    <LabelsAppearance Position="OutsideEnd" DataFormatString="{0:N0}" />
                    <TooltipsAppearance DataFormatString="{0:N0} teachers" />
                </telerik:PieSeries>
            </Series>
        </PlotArea>
        <Legend>
            <Appearance Position="Right" />
        </Legend>
    </telerik:RadHtmlChart>

    <telerik:RadHtmlChart runat="server" ID="RadHtmlChart3" Width="700px" Height="450px">
        <ChartTitle Text="Number of scheduled classes" />
        <PlotArea>
            <Series>
                <telerik:PieSeries DataFieldY="Value" NameField="Browser" ExplodeField="Explode"
                    StartAngle="90">
                    <LabelsAppearance Position="OutsideEnd" DataFormatString="{0:N0}" />
                    <TooltipsAppearance DataFormatString="{0:N0} classes" />
                </telerik:PieSeries>
            </Series>
        </PlotArea>
        <Legend>
            <Appearance Position="Right" />
        </Legend>
    </telerik:RadHtmlChart>

    <telerik:RadHtmlChart runat="server" ID="RadHtmlChartBar" Width="100%" Height="500px" Style="margin: 0 auto;">
        <ChartTitle Text="Total number of students in classes" />
        <PlotArea>
            <Series>
                <telerik:ColumnSeries DataFieldY="TotalStudent" Name="Total Students">
                    <LabelsAppearance DataFormatString="{0}" Position="OutsideEnd" />
                    <TooltipsAppearance DataFormatString="{0} students" />
                </telerik:ColumnSeries>
            </Series>
            <XAxis DataLabelsField="Name">
                <TitleAppearance Text="Class name" />
                <LabelsAppearance RotationAngle="45" Step="1" />
                <MajorGridLines Visible="false" />
                <MinorGridLines Visible="false" />
            </XAxis>
            <YAxis>
                <TitleAppearance Text="Total Students" />
            </YAxis>
        </PlotArea>
        <Legend>
            <Appearance Position="Bottom" />
        </Legend>
    </telerik:RadHtmlChart>

</asp:Content>
