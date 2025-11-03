<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Dashboard" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content0" ContentPlaceHolderID="head" Runat="Server">
    <link href="../../styles/default.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <telerik:RadHtmlChart runat="server" ID="PieChartBrowserUsage" Width="700px" Height="450px">
        <ChartTitle Text="Browser Usage for October 2021" />
        <PlotArea>
            <Series>
                <telerik:PieSeries DataFieldY="Value" NameField="Browser" ExplodeField="Explode" 
                                   StartAngle="90">
                    <LabelsAppearance Position="OutsideEnd" DataFormatString="{0} %" />
                    <TooltipsAppearance DataFormatString="{0} %" />
                </telerik:PieSeries>
            </Series>
        </PlotArea>
        <Legend>
            <Appearance Position="Right" />
        </Legend>
    </telerik:RadHtmlChart>
</asp:Content>
