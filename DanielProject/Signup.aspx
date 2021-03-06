<%@ Page Title="" Language="C#" MasterPageFile="~/NotLoggedIn.Master" AutoEventWireup="true" CodeBehind="Signup.aspx.cs" Inherits="DanielProject.Signup" %>

<%@ MasterType VirtualPath="~/NotLoggedIn.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentPlaceHolder" runat="server">
    <title>Sign up for a Dpay Account</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContentPlaceHolder" runat="server">
    <div id="login-container" style="margin-top: 15%;">
        <a href="Home.aspx">
            <img src="DPAYv1.png" class="loginlogo"></a>
        <span class="signuptext">Name:</span> <span id="required">*</span><asp:TextBox ID="TextBoxSignUpName" runat="server"></asp:TextBox>
        <span class="signuptext">Email:</span> <span id="required">*</span><asp:TextBox ID="TextBoxSignUpEmail" runat="server"></asp:TextBox>
        <span class="signuptext">SSN:</span> <span id="required">*</span><asp:TextBox ID="TextBoxSignUpSSN" runat="server"></asp:TextBox>
        <div id="InvalidEmail" runat="server" style="color: red">Please enter a valid email.</div>
        <div id="EmailUsed" runat="server" style="color: red">Email is already being used. Please enter another.</div>
        <span class="signuptext">Username:</span> <span id="required">*</span><asp:TextBox ID="TextBoxSignUpUsername" runat="server"></asp:TextBox>
        <div id="UsernameTaken" runat="server" style="color: red">Username is already taken. Please try another.</div>
        <span class="signuptext">Password:</span> <span id="required">*</span><asp:TextBox ID="TextBoxSignUpPassword" class="PWSignUpTextBox" TextMode="Password" runat="server"></asp:TextBox>
        <span class="signuptext">Confirm Password:</span> <span id="required">*</span><asp:TextBox ID="TextBoxSignUpConfirmPassword" class="PWSignUpTextBox" TextMode="Password" runat="server"></asp:TextBox>
        <div id="PasswordsDoNotMatch" runat="server" style="color: red">Passwords do not match. Please try again.</div>
        <span class="signuptext">Account Type:</span><span id="required">*</span>
        <asp:DropDownList ID="DropdownSignUpAccountType" runat="server">
            <asp:ListItem></asp:ListItem>
            <asp:ListItem>Checkings</asp:ListItem>
            <asp:ListItem>Savings</asp:ListItem>
        </asp:DropDownList>
        <asp:CheckBox ID="CheckBox1" runat="server" />
        <span class="signuptext">By creating an account you agree to our Terms & Privacy.</span><span id="required">*</span>
        <asp:Button class="login-button" runat="server" OnClick="SignUpEventMethod" Text="Sign Up" Width="100%" />
        <asp:Button class="signup-button" runat="server" OnClick="CancelEventMethod" Text="Cancel" Width="100%" />
    </div>
</asp:Content>
