<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Unauthorized.aspx.cs" Inherits="Unauthorized" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<!DOCTYPE html>
<html lang="vi">
<head runat="server">
    <meta charset="utf-8" />
    <title>Access denied</title>
    <style>
        body {
            margin: 0;
            font-family: "Segoe UI", sans-serif;
            background: linear-gradient(135deg, #667eea, #764ba2);
            color: #fff;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            overflow: hidden;
        }

        .container {
            text-align: center;
            background: rgba(255, 255, 255, 0.1);
            padding: 60px;
            border-radius: 15px;
            backdrop-filter: blur(10px);
            box-shadow: 0 8px 32px rgba(0, 0, 0, 0.2);
            animation: fadeIn 0.8s ease;
        }

        h1 {
            font-size: 64px;
            margin-bottom: 10px;
        }

        p {
            font-size: 18px;
            margin-bottom: 30px;
        }

        a.button {
            text-decoration: none;
            background: #fff;
            color: #4a4a4a;
            padding: 12px 24px;
            border-radius: 25px;
            transition: all 0.3s ease;
            font-weight: 600;
        }

        a.button:hover {
            background: #4a4a4a;
            color: #fff;
        }

        @keyframes fadeIn {
            from { opacity: 0; transform: translateY(20px); }
            to { opacity: 1; transform: translateY(0); }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h1>403</h1>
            <h2>Access denied</h2>
            <p>You do not have permission to access this page.</p>
            <a href="Default.aspx" class="button">Back to Home</a>
        </div>
    </form>
</body>
</html>