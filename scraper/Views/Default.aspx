<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="scraper.Views.Default" %>
<%@ Import Namespace="System.ComponentModel" %>
<%@ Import Namespace="System.Net" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="Label1" runat="server" Text="Ange url: "></asp:Label>
        <asp:TextBox ID="TextBoxUrl" runat="server"></asp:TextBox>
        <asp:Button ID="ButtonSubmit" runat="server" Text="Start!" OnClick="ButtonSubmit_OnClick"/>
    </div>
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
                    <br/> 
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </form>
</body>
</html>
