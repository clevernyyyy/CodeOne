<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="AccountLine.ascx.vb" Inherits="CodeOne.AccountLine" %>

<div id="AccountControl" runat="server" class="divAccts">
    <asp:LinkButton id="lnkAccountName" runat="server" CssClass="acctHeadings"></asp:LinkButton>
    <asp:HiddenField ID="hfAccountNum" runat="server" />
    <span id="divLast" runat="server">
        <asp:Label ID="lblLastPaymentDate" runat="server" />
        <asp:Label ID="lblLastPaymentAmount" runat="server" />
    </span>
    <span id="divDue" runat="server">
        <asp:Label ID="lblDueDate" runat="server" />
        <asp:Label ID="lblDueAmount" runat="server" />
    </span>
    <span id="balance" class="acctBalanceBak">
        <asp:Label ID="lblBalance" runat="server" CssClass="acctBalance"/>
    </span>
    <div>
        <asp:LinkButton ID="lnkViewTen" runat="server" CssClass="greenLink">Last 10 Transactions</asp:LinkButton>
    </div>
    <hr style="margin-bottom: 0px; margin-top: 10px;" />
</div>