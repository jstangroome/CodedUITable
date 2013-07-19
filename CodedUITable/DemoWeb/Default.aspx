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
                <tr><td>Jack</td><td>03/04/1979</td><td>34</td></tr>
                <tr><td>James</td><td>05/06/1981</td><td>32</td></tr>
                <tr><td>Jim</td><td>07/08/1978</td><td>35</td></tr>
                <tr><td>Joe</td><td>09/10/1982</td><td>31</td></tr>
            </tbody>
        </table>
    </div>
    </form>
</body>
</html>
