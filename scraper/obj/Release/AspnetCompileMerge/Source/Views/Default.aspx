<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="scraper.Views.Default" ViewStateMode="Disabled"%>

<%@ Import Namespace="System.Net" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Scraper</title>
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
        <div  style="width: 60%; margin: auto;">
            <form id="form1" runat="server" style="text-align: center">

                <div>
                    <asp:Label ID="Label1" runat="server" Text="Ange url: "></asp:Label>
                    <asp:TextBox ID="TextBoxUrl" runat="server"></asp:TextBox>
                    <asp:Button ID="ButtonSubmit" runat="server" Text="Start!" OnClick="ButtonSubmit_OnClick" />
                </div>
                <div>
                    <div>
                        <div>
                            <asp:Repeater ID="RepeaterResult" runat="server"
                                ItemType="scraper.Models.Movie">
                                <HeaderTemplate>
                                    <h2>Följande filmer hittades</h2>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    Filmen <span style="font-weight: bold"><%# DataBinder.Eval(Container.DataItem, "Name") %></span>
                                    klockan <%# DataBinder.Eval(Container.DataItem, "Time") %> 
                    på <%# DataBinder.Eval(Container.DataItem, "Day") %>
                                    <a href="?day=<%#WebUtility.UrlEncode(DataBinder.Eval(Container.DataItem, "Day").ToString())%>&time=<%#WebUtility.UrlEncode(DataBinder.Eval(Container.DataItem, "Time").ToString())%>&name=<%#WebUtility.UrlEncode(DataBinder.Eval(Container.DataItem, "Name").ToString()) %>">Välj denna och boka bord</a>
                                    <br />
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>
            </form>
        </div>
    </div>
</body>
</html>
