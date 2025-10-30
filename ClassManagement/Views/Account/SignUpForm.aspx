<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignUpForm.aspx.cs" Inherits="ClassManagement.Views.Account.SignUpForm" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sign Up</title>
    <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" />
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server" />

        <div class="signup-container" style="width:400px;margin:50px auto;">
            <telerik:RadAjaxPanel runat="server">
                <h2>Sign Up</h2>

                <telerik:RadTextBox ID="txtUsername" runat="server" Label="Username:" Width="100%"></telerik:RadTextBox>
                <br />
                <telerik:RadTextBox ID="txtEmail" runat="server" Label="Email:" Width="100%"></telerik:RadTextBox>
                <br />
                <telerik:RadTextBox ID="txtPassword" runat="server" Label="Password:" TextMode="Password" Width="100%"></telerik:RadTextBox>
                <br />
                <telerik:RadTextBox ID="txtConfirm" runat="server" Label="Confirm Password:" TextMode="Password" Width="100%"></telerik:RadTextBox>
                <br />
                <telerik:RadButton ID="btnSignUp" runat="server" Text="Sign Up" OnClick="btnSignUp_Click" Width="100%"></telerik:RadButton>

                <telerik:RadLabel ID="lblMessage" runat="server" ForeColor="Red"></telerik:RadLabel>
            </telerik:RadAjaxPanel>
        </div>
    </form>
</body>
</html>
