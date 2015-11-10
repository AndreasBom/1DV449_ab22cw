<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="scraper.Views.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="Label1" runat="server" Text="Ange url: "></asp:Label>
        <asp:TextBox ID="TextBoxUrl" runat="server" Text="http://localhost:8888/"></asp:TextBox>
        <asp:Button ID="ButtonSubmit" runat="server" Text="Start!" OnClick="ButtonSubmit_OnClick"/>
    </div>
        <div>
            <asp:Repeater ID="RepeaterResult" runat="server" ItemType="System.String">
                <HeaderTemplate>
                    
                </HeaderTemplate>
                <ItemTemplate>
                    <%# Container.DataItem.ToString()%>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </form>
</body>
</html>
