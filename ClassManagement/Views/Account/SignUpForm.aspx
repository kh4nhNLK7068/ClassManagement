<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignUpForm.aspx.cs" Inherits="ClassManagement.Views.Account.SignUpForm" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html>
<html xmlns='http://www.w3.org/1999/xhtml'>
<head runat="server">
    <title>CMS - Add new user</title>
    <link href="Styles.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />
        <telerik:RadSkinManager ID="RadSkinManager1" runat="server" ShowChooser="true" />
        <div class="demo-container no-bg">
            <div class="signUpScenario">
                <div class="signUpHeader">
                    <div class="formTitle">Sign Up</div>
                    <div class="signButton">
                        <telerik:RadButton RenderMode="Lightweight" ID="ImageButton" runat="server" Width="85px" Height="28px" Text="Sign in" Skin="Default">
                            <Icon PrimaryIconCssClass="rbIcon rbNext" PrimaryIconLeft="5px" PrimaryIconTop="5px" />
                        </telerik:RadButton>
                    </div>
                </div>
                <div class="signUpBody">
                    <div class="formElements">
                        <label for="txtFullName">Full name:</label>
                        <asp:TextBox ID="txtFullName" runat="server" CssClass="itext"></asp:TextBox>
                    </div>
                    <%--<div class="formElements">
                        <label for="txtDateOfBirth">Date of birth:</label>
                        <%--<asp:TextBox ID="txtDateOfBirth" runat="server" CssClass="itext"></asp:TextBox>
                        <telerik:RadDatePicker RenderMode="Lightweight" ID="txtDateOfBirth" Width="50%" ClientEvents-OnDateSelected="OnDateSelected" runat="server">
                        </telerik:RadDatePicker>
                    </div>--%>
                    <div class="formElements">
                        <label for="txtDateOfBirth">Date of birth:</label>
                        <telerik:RadDatePicker
                            ID="txtDateOfBirth"
                            runat="server"
                            RenderMode="Lightweight"
                            Width="220px">
                            <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"></DateInput>
                            <Calendar runat="server"></Calendar>
                        </telerik:RadDatePicker>
                    </div>

                    <div class="formElements">
                        <label for="txtRole">Role:</label>
                        <telerik:RadButton RenderMode="Lightweight" ID="btnTeacher" runat="server"
                            ButtonType="ToggleButton" ToggleType="Radio" GroupName="Role"
                            Text="Teacher" Width="75px" Height="22px" Skin="Default" ForeColor="Black"
                            AutoPostBack="false">
                            <ToggleStates>
                                <telerik:RadButtonToggleState Text="Teacher" Selected="true"></telerik:RadButtonToggleState>
                                <telerik:RadButtonToggleState Text="Teacher"></telerik:RadButtonToggleState>
                            </ToggleStates>
                        </telerik:RadButton>

                        <telerik:RadButton RenderMode="Lightweight" ID="btnStudent" runat="server"
                            ButtonType="ToggleButton" ToggleType="Radio" GroupName="Role"
                            Text="Student" Width="75px" Height="22px" Skin="Default" ForeColor="Black"
                            AutoPostBack="false">
                            <ToggleStates>
                                <telerik:RadButtonToggleState Text="Student" Selected="false"></telerik:RadButtonToggleState>
                                <telerik:RadButtonToggleState Text="Student"></telerik:RadButtonToggleState>
                            </ToggleStates>
                        </telerik:RadButton>
                    </div>

                    <div class="formElements">
                        <label for="txtCityLive">City live:</label>
                        <asp:TextBox ID="txtCityLive" runat="server" CssClass="itext"></asp:TextBox>
                    </div>
                    <div class="subscription">
                        <div>
                            <telerik:RadButton RenderMode="Lightweight" ID="btnNewsletter" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                AutoPostBack="false" Width="38px" Height="22px" ToolTip="Subscribe to Newsletter" Skin="Default">
                                <Icon PrimaryIconCssClass="rbMail" PrimaryIconTop="3px" PrimaryIconLeft="10px"></Icon>
                                <ToggleStates>
                                    <telerik:RadButtonToggleState ImageUrl="images/smallBtnHov.png" Selected="true" IsBackgroundImage="true"></telerik:RadButtonToggleState>
                                    <telerik:RadButtonToggleState ImageUrl="images/smallBtn.png" HoveredImageUrl="images/smallBtnHov.png"
                                        IsBackgroundImage="true"></telerik:RadButtonToggleState>
                                </ToggleStates>
                            </telerik:RadButton>
                            <span>Subscribe to Newsletter</span>
                        </div>
                        <div style="padding-top: 5px">
                            <telerik:RadButton RenderMode="Lightweight" ID="btnRss" runat="server" ButtonType="ToggleButton" ToggleType="CheckBox"
                                AutoPostBack="false" Width="38px" Height="22px" ForeColor="Black" ToolTip="Subscribe to RSS Feeds" Skin="Default">
                                <Icon PrimaryIconCssClass="rbRSS" PrimaryIconTop="3px" PrimaryIconLeft="10px"></Icon>
                                <ToggleStates>
                                    <telerik:RadButtonToggleState ImageUrl="images/smallBtnHov.png" Selected="true" IsBackgroundImage="true"></telerik:RadButtonToggleState>
                                    <telerik:RadButtonToggleState ImageUrl="images/smallBtn.png" HoveredImageUrl="images/smallBtnHov.png"
                                        IsBackgroundImage="true"></telerik:RadButtonToggleState>
                                </ToggleStates>
                            </telerik:RadButton>
                            <span>Subscribe to RSS Feeds</span>
                        </div>
                    </div>
                    <div class="signUp">
                        <telerik:RadButton RenderMode="Lightweight" ID="RadButton5" runat="server" Width="112px" Height="28px" Text="Sign Up" Skin="Default" OnClick="btnCreateUser_Click">
                        </telerik:RadButton>
                        <telerik:RadButton RenderMode="Lightweight" ID="RadButton6" runat="server" Width="112px" Height="28px" Text="Cancel" Skin="Default">
                        </telerik:RadButton>
                        <asp:Label ID="lblResult" runat="server" ForeColor="Green" />
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
