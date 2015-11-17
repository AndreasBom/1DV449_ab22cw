<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="scraper.Default" %>
<%@ Import Namespace="scraper.Views" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <%-- Fulhack. Redirectar sidan till View/Default.aspx --%>
        <% Response.Redirect("Views/Default.aspx"); %>
    </div>
    </form>
</body>
</html>
