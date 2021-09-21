<%@ Page Title="" Language="C#" MasterPageFile="~/LoggedIn.Master" AutoEventWireup="true" CodeBehind="NewAccountType.aspx.cs" Inherits="DanielProject.NewAccountType" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContentPlaceHolder" runat="server">
        <div id="login-container" style="margin-top: 15%;">
        <a href="Home.aspx">
            <img src="DPAYv1.png" class="loginlogo"></a>
        <span class="signuptext">SSN:</span> <span id="required">*</span><asp:TextBox ID="TextBoxSignUpSSN" runat="server"></asp:TextBox>
        <div id="WrongSSN" runat="server" style="color: red">Incorrect SSN. Please try again.</div>
        <span class="signuptext">Account Type:</span><span id="required">*</span>
        <asp:DropDownList ID="DropdownSignUpAccountType" runat="server">
            <asp:ListItem></asp:ListItem>
            <asp:ListItem>Checkings</asp:ListItem>
            <asp:ListItem>Savings</asp:ListItem>
        </asp:DropDownList>
        <asp:CheckBox ID="CheckBox1" runat="server" />
        <span class="signuptext">By adding a new account type you agree to our Terms & Privacy.</span><span id="required">*</span>
        <asp:Button class="login-button" runat="server" OnClick="NewAccountTypeEventMethod" Text="Confirm" Width="100%" />
    </div>
</asp:Content>
