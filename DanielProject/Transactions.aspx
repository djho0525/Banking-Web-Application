<%@ Page Title="" Language="C#" MasterPageFile="~/LoggedIn.Master" AutoEventWireup="True" CodeBehind="Transactions.aspx.cs" Inherits="DanielProject.Transactions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentPlaceHolder" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContentPlaceHolder" runat="server">

    <br />
    <!--
        <asp:DataList ID="DataList2" runat="server" CellPadding="4" ForeColor="#333333">
                <AlternatingItemStyle BackColor="White" />
				<FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
				<HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
				<ItemStyle BackColor="#E3EAEB" />
                <ItemTemplate>  
                Transaction Name:
                <asp:Label ID="nameLabel" runat="server" DataField="transactionName" />  
                <br />  
                Transaction Amount:
                <asp:Label ID="emailLabel" runat="server" DataField="amount"/>  
                <br />  
                Transaction Date & Time:
                <asp:Label ID="contactLabel" runat="server" DataField="transactionTime"/>  
                <br />  
                <br />  
            </ItemTemplate>  
                <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
    </asp:DataList>
        -->
        <div id="DisplayRequests">
        <!--<asp:GridView ID="DataList1" AutoGenerateColumns="false" runat="server" Style="margin-top: 100px">
            <Columns>
                <asp:BoundField DataField="transactionName" HeaderText="Transaction Name" />
                <asp:BoundField DataField="amount" HeaderText="Transaction Amount" />
                <asp:BoundField DataField="transactionTime" HeaderText="Transaction Time" />
            </Columns>
        </asp:GridView>-->

	<div>
    <asp:DataList ID="DataList3" runat="server" Width="100%" ForeColor="Gold" BorderColor="Gray" BorderWidth="1px" BorderStyle="Solid">

        <AlternatingItemStyle BackColor="Gray" />

        <FooterStyle BackColor="Gray" ForeColor="White" Font-Bold="True" Font-Size="Smaller"/>
        <HeaderStyle BackColor="Goldenrod" Font-Size="Smaller" Font-Bold="True" ForeColor="White" />

        <HeaderTemplate>
            Transaction History
        </HeaderTemplate>

        <ItemStyle BackColor="Gray" ForeColor="Gray" Font-Size="XX-Small"/>

        <ItemTemplate>
            <tr>
                <td>
                    <asp:Label id ="Label1" runat="server" Font-Bold="true" Text ='<%#Eval("transactionName")%>'></asp:Label><span style="display:inline-block; width: 15px;"></span><asp:Label id ="Label3" runat="server" Text ='<%#Eval("transactionTime")%>' ></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label id ="Label2" runat="server" Text ='<%#Eval("displayAmount") %>'></asp:Label>
                </td>
            </tr>

        </ItemTemplate>

	    <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />

    </asp:DataList>
        <br />
        <asp:Button class="button8" ID="PrevPageButton" runat="server" Text="Previous Page" OnClick="PreviousPage"/>
        <asp:Label ID="pageNumLabel" runat="server" Text="Page#"></asp:Label>
        <asp:Button class="button8" ID="NextPageButton" runat="server" Text="Next Page" OnClick="NextPage" />
        <!--<asp:Button class="button8" runat="server" Text="Show more" OnClick="ShowMoreResults" />-->
    </div>
    </div>

    <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical">
            <AlternatingRowStyle BackColor="White" />
            <FooterStyle BackColor="#CCCC99" />
            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
            <RowStyle BackColor="#F7F7DE" />
            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#FBFBF2" />
            <SortedAscendingHeaderStyle BackColor="#848384" />
            <SortedDescendingCellStyle BackColor="#EAEAD3" />
            <SortedDescendingHeaderStyle BackColor="#575357" />

    </asp:GridView>

</asp:Content>
