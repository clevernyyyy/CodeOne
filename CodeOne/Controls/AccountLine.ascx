<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="AccountLine.ascx.vb" Inherits="CodeOne.AccountLine" %>
<asp:LinkButton id="lnkAccountName" runat="server"></asp:LinkButton>
<asp:HiddenField ID="hfAccountNum" runat="server" />
<div id="divLast" runat="server">
    <asp:Label ID="lblLastPaymentDate" runat="server" />
    <asp:Label ID="lblLastPaymentAmount" runat="server" />
</div>
<div id="divDue" runat="server">
    <asp:Label ID="lblDueDate" runat="server" />
    <asp:Label ID="lblDueAmount" runat="server" />
</div>
<asp:Label ID="lblBalance" runat="server" />