<%@ Page Title="" Language="C#" MasterPageFile="~/LoggedIn.Master" AutoEventWireup="true" CodeBehind="Request.aspx.cs" Inherits="DanielProject.Request" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentPlaceHolder" runat="server">
    <title>Dpay: Request Money</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContentPlaceHolder" runat="server">
        <div id="AddMoney" style="margin-top: 15%;">
        <a href="HomeLI.aspx">
            <img src="DPAYv1.png" class="loginlogo"></a>
        <asp:TextBox ID="TextBoxRequestEmailUsername" runat="server" placeholder="Email or Username"></asp:TextBox>
        <div id="InvalidUser" runat="server" style="color: red; margin-top: -5px ;margin-bottom: 5px">Invalid user. Please try again.</div>
        <span id="DollarSign">$</span><asp:TextBox ID="TextBoxAddMoney" type="number" min="0.00" step="0.01" runat="server" placeholder="0.00"></asp:TextBox>
        <div id="InvalidAmount" runat="server" style="color: red; margin-top: 5px">Invalid amount. Please enter a different amount.</div>
        <asp:TextBox ID="TextBoxRequestNote" runat="server" placeholder="Note or Message"></asp:TextBox>
        <div id="RequestMoneySuccess" runat="server" style="color: limegreen; margin-top: 5px">Success! Money requested.</div>
        <asp:Button class="addmoneybutton" runat="server" OnClick="RequestMoneyMethod" Text="Request" Width="100%" />
    </div>
</asp:Content>
