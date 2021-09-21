<%@ Page Title="" Language="C#" MasterPageFile="~/NotLoggedIn.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="DanielProject.Login" %>

<%@ MasterType VirtualPath="~/NotLoggedIn.Master" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentPlaceHolder" runat="server">
    <title>Log In to Your Dpay Account</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContentPlaceHolder" runat="server">
    <div id="login-container" style="margin-top: 15%;">
        <a href="Home.aspx">
            <img src="DPAYv1.png" class="loginlogo"></a>
        <asp:TextBox ID="TextBoxEmailUsername" runat="server" placeholder="Email or Username"></asp:TextBox>
        <div id="NoSuchEmail" runat="server" style="color: red">Email does not exist. Please sign up.</div>
        <div id="NoSuchUser" runat="server" style="color: red">Username was not found. Please sign up.</div>
        <asp:TextBox ID="TextBoxPassword" runat="server" TextMode="password" placeholder="Password"></asp:TextBox>
        <div id="WrongPassword" runat="server" style="color: red">Incorrect Password. Please try again.</div>
        <asp:Button class="login-button" runat="server" OnClick="LoginEventMethod" Text="Log In" Width="100%" />
        <asp:Button class="signup-button" runat="server" OnClick="LoadSignupMethod" Text="Sign Up" Width="100%" />
    </div>
</asp:Content>
