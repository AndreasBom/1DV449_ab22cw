<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResultAvailable.aspx.cs" Inherits="scraper.Views.ResultAvailable" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h2>Zekes Resturang</h2>
        <p>Det fanns lediga tider på <%=Session["day"]%> kl <%=Session["time"]%> efter filmen <%=Session["name"]%>.</p>
        <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="Default.aspx">Tillbaka</asp:LinkButton>
    </div>
    </form>
</body>
</html>
