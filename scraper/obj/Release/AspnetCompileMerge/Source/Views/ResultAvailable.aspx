<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResultAvailable.aspx.cs" Inherits="scraper.Views.ResultAvailable" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Scraper -- Sökresultat</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css" integrity="sha512-dTfge/zgoMYpP7QbHy4gWMEGsbsdZeCXz7irItjcC3sPUFtf0kuFbDz/ixG7ArTxmDjLXDmezHubeNikyKGVyQ==" crossorigin="anonymous">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap-theme.min.css" integrity="sha384-aUGj/X2zp5rLCbBxumKTCw2Z50WgIr1vs/PFN4praOTvYXWlVyh2UtNUU0KAUhAX" crossorigin="anonymous">

    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js" integrity="sha512-K1qjQ+NcF2TYO/eI3M6v8EiNYZfA95pQumfvcVrTHtwQVDG+aHRqLi/ETn2uB+1JqwYqVG3LIvdm9lj6imS/pQ==" crossorigin="anonymous"></script>
</head>
<body>
    <div class="container" id="wrapper">
        <div class="page-header">
            <h1 style="text-align: center">Webbteknik II</h1>
            <h3 style="text-align: center">Labb 1</h3>
        </div>
        <div style="width: 60%; margin: auto"></div>
        <form id="form1" runat="server" style="text-align: center">
            <div style="">
                <h2>Zekes Resturang</h2>
                <asp:Repeater ID="RepeaterDinner" runat="server" ItemType="scraper.Models.Dinner">
                    <HeaderTemplate>
                        Följande tider fanns lediga efter filmen <%=Session["name"].ToString()%>
                        <ul style="list-style-position: inside; margin: 0; padding: 0;">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <li>
                            <%#DataBinder.Eval(Container.DataItem, "Time")%>-<%#((int)DataBinder.Eval(Container.DataItem, "Time")+2) %>
                        </li>
                    </ItemTemplate>
                    <FooterTemplate>
                        </ul>
                    </FooterTemplate>
                </asp:Repeater>

                <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="Default.aspx">Tillbaka</asp:LinkButton>
            </div>
        </form>
    </div>
</body>
</html>
