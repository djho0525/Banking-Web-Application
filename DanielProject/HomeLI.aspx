<%@ Page Title="" Language="C#" MasterPageFile="~/LoggedIn.Master" AutoEventWireup="true" CodeBehind="HomeLI.aspx.cs" Inherits="DanielProject.HomeLI" %>

<%@ MasterType VirtualPath="~/LoggedIn.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentPlaceHolder" runat="server">
    <title>Dpay: Account Summary</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContentPlaceHolder" runat="server">
    <div id="WelcomeBack" runat="server" style="margin-top: 100px; color: gold; font-family: Verdana; font-size: 50px; margin-left: 5%"></div>

    <div id ="CheckingsBalance" runat="server">
        <div class="DisplayBalanceCheckings" runat="server" OnClick="LoadCheckingsAccountMethod" style="margin-top: 5%;">
        <asp:Button class="DisplayBalanceCheckingsButton" runat="server" OnClick="LoadCheckingsAccountMethod" Text="Checkings Account"/>
        <div id="CurrentBalanceHeader" runat="server" style="font-size: 20px; margin-top: 2.5%; margin-bottom: 10px;">Current Checkings Balance</div>
        <div id="CurrentBalanceCheckings" runat="server"></div>
        </div>
    </div>
    
    <div id ="SavingsBalance" runat="server">
        <div class="DisplayBalanceSavings" runat="server"  OnClick="LoadSavingsAccountMethod" style="margin-top: 2.5%;">
        <asp:Button class="DisplayBalanceSavingsButton" runat="server" OnClick="LoadSavingsAccountMethod" Text="Savings Account"/>
        <div class="CurrentBalanceHeader" runat="server" style="font-size: 20px; margin-top: 2.5%; margin-bottom: 10px;">Current Savings Balance</div>
        <div id="CurrentBalanceSavings" runat="server"></div>
        </div>
    </div>

    <div id ="AddNewAccountTypeButton" runat="server">
        <asp:Button class="button6" runat="server" OnClick="LoadAddNewAccountTypeMethod" Text="Add a New Checkings or Savings Account" Width="500px" style="margin-left: 5%;" />
    </div>

    <div id="DisplayRequests">
        <asp:GridView ID="CurrentBalanceGrid" AutoGenerateColumns="false" runat="server" Style="margin-top: 100px">
            <Columns>
                <asp:BoundField DataField="sender" HeaderText="From" />
                <asp:BoundField DataField="amount" HeaderText="Amount" />
                <asp:BoundField DataField="note" HeaderText="Note or Message" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
