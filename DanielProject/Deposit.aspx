<%@ Page Title="" Language="C#" MasterPageFile="~/LoggedIn.Master" AutoEventWireup="true" CodeBehind="Deposit.aspx.cs" Inherits="DanielProject.Deposit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentPlaceHolder" runat="server">
    <title>Dpay: Add Money to Your Dpay Account</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContentPlaceHolder" runat="server">
    <div id="AddMoney" style="margin-top:15%;">
                <a href="HomeLI.aspx"><img src="DPAYv1.png" class="loginlogo"></a>
                <span id ="DollarSign">$</span><asp:TextBox ID="TextBoxAddMoney"  type="number" min="0.00" step="0.01" runat="server" placeholder="0.00"></asp:TextBox>
                <div id="InvalidAmount" runat="server" style="color:red;margin-top:5px">Invalid amount. Please enter a different amount.</div>
                <div id="AddMoneySuccess" runat="server" style="color:limegreen;margin-top:5px">Success! Money added.</div>
                <asp:Button class="addmoneybutton" runat="server" OnClick="AddMoneyMethod" Text="Add Money" Width="100%" /> 
    </div>
</asp:Content>
