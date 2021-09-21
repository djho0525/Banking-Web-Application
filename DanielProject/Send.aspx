<%@ Page Title="" Language="C#" MasterPageFile="~/LoggedIn.Master" AutoEventWireup="true" CodeBehind="Send.aspx.cs" Inherits="DanielProject.Send" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentPlaceHolder" runat="server">
    <title>Dpay: Send Money</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContentPlaceHolder" runat="server">
    <div id="AddMoney" style="margin-top: 15%;">
        <a href="HomeLI.aspx">
            <img src="DPAYv1.png" class="loginlogo"></a>
        <asp:TextBox ID="senderAccountNumber" runat="server" placeholder="Your Account Number" style="margin-bottom: 10px" Width="100%"></asp:TextBox>
        <div id="InvalidUser" runat="server" style="color: red; margin-top: -5px ;margin-bottom: 5px">Invalid user. Please try again.</div>
        <asp:TextBox ID="TextBoxPayEmailUsername" runat="server" placeholder="Recipient Email or Username" Width="100%"></asp:TextBox>
        <asp:TextBox ID="recipientAccountNumber" runat="server" placeholder="Recipient Account Number" style="margin-bottom: 10px" Width="100%"></asp:TextBox>
        <asp:TextBox ID="TextBoxTransactionName" runat="server" placeholder="Transaction Description or Name" style="margin-bottom: 10px" Width="100%"></asp:TextBox>
        <span id="DollarSign" style ="margin-top:5px">$</span><asp:TextBox ID="TextBoxSendMoney" type="number" min="0.00" step="0.01" runat="server" placeholder="0.00" style="margin-top:5px"></asp:TextBox>
        <div id="InvalidAmount" runat="server" style="color: red; margin-top: 5px">Invalid amount. Please enter a different amount.</div>
        <div id="NotEnoughMoney" runat="server" style="color: red; margin-top: 5px">Not enough money! Please add enter a different amount.</div>
        <div id="PayMoneySuccess" runat="server" style="color: limegreen; margin-top: 5px">Success! Money sent.</div>
        <asp:Button class="addmoneybutton" runat="server" OnClick="PayMoneyMethod" Text="Send" Width="100%" />
    </div>
</asp:Content>
