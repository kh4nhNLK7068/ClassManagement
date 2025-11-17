<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangePasswordForm.aspx.cs" Inherits="ChangePasswordForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" />
    <link href="Styles.css" rel="stylesheet" type="text/css" />
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
        <script type="text/javascript">
        //Put your JavaScript code here.
        </script>

        <div id="changePasswordModal"
            style="display: none; position: fixed; inset: 0; z-index: 9999; display: flex; align-items: center; justify-content: center;">

            <div style="width: 420px; border-radius: 10px; padding: 25px; box-shadow: 0 4px 15px rgba(0,0,0,0.3); position: relative;">

                <!-- Close button -->
                <span onclick="closeModal()"
                    style="position: absolute; top: 10px; right: 15px; cursor: pointer; font-size: 20px;">&times;
                </span>

                <h3 style="margin-top: 0; text-align: center; font-weight: bold;">Change Password</h3>

                <div style="display: flex; flex-direction: column; gap: 15px;">

                    <div>
                        <label>Current password:</label><br />
                        <asp:TextBox ID="txtBox1" runat="server" TextMode="Password"
                            Style="width: 100%; padding: 8px; border: 1px solid #ccc; border-radius: 5px;" />
                    </div>

                    <div>
                        <label>New password:</label><br />
                        <asp:TextBox ID="TextBox2" runat="server" TextMode="Password"
                            Style="width: 100%; padding: 8px; border: 1px solid #ccc; border-radius: 5px;" />
                    </div>

                    <div>
                        <label>Confirm password:</label><br />
                        <asp:TextBox ID="TextBox3" runat="server" TextMode="Password"
                            Style="width: 100%; padding: 8px; border: 1px solid #ccc; border-radius: 5px;" />
                    </div>

                    <div style="text-align: center; margin-top: 10px;">
                        <telerik:RadButton RenderMode="Lightweight" ID="RadButton5"
                            runat="server" Width="140" Height="32" Text="Confirm" Skin="Default" />
                    </div>

                    <asp:Label ID="lblResult" runat="server"
                        Style="color: green; text-align: center; display: block;" />
                </div>

            </div>
        </div>

    </form>
</body>
</html>
