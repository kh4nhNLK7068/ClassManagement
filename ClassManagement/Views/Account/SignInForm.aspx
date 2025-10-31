<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignInForm.aspx.cs" Inherits="ClassManagement.Views.Account.SignInForm" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html>
<html xmlns='http://www.w3.org/1999/xhtml'>
<head runat="server">
    <title>CMS - Sign in</title>
    <link href="Styles.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form2" runat="server">
        <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />
        <div class="webTitle">
            <h1>CLASS MANAGEMENT SYSTEM</h1>
        </div>
        <div class="demo-container no-bg">
            <div class="signUpScenario">
                <div class="signUpHeader">
                    <div class="formTitle">Sign In</div>
                </div>
                <div class="signUpBody">
                    <div class="formElements">
                        <label for="txtUserName">User name:</label>
                        <asp:TextBox ID="txtUserName" runat="server" CssClass="itext"></asp:TextBox>
                    </div>
                    <div class="formElements">
                        <label for="txtPassword">Password:</label>
                        <asp:TextBox ID="txtPassword" runat="server" CssClass="itext" TextMode="Password"></asp:TextBox>
                    </div>
                    <div class="signUp">
                        <telerik:RadButton RenderMode="Lightweight" ID="RadButton5" runat="server" Width="112px" Height="28px" Text="Sign In" Skin="Default" OnClick="btnLogin_Click">
                        </telerik:RadButton>
                        <asp:Label ID="lblResult" runat="server" ForeColor="Green" />
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
