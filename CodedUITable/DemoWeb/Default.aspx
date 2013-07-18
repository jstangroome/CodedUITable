<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DemoWeb.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>DemoWeb</title>
    <style>
        table, td { border: 1px solid; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>Welcome</h1>
        <p>Results:</p>
        <table id="theTable">
            <thead>
                <tr><th>Given Name</th><th>Date of Birth</th><th>Age</th></tr>
            </thead>
            <tbody>
                <tr><td>John</td><td>01/02/1980</td><td>33</td></tr>
            </tbody>
        </table>
    </div>
    </form>
</body>
</html>
