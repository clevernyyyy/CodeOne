<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Transaction.ascx.vb" Inherits="CodeOne.Transaction" %>

<div id="TransactionControl" runat="server" >
    <span id="divDate" class="transDate">
        <asp:Label ID="lblDate" runat="server"/>
    </span>
    <span id="divAmount" runat="server" >
        <asp:Label ID="lblAmount" runat="server" />
    </span>
    <span id="divDetail" runat="server" >
        <asp:Label ID="lblDetail" runat="server" />
    </span>
</div>
<hr style="margin-bottom:0px; margin-top:0px; margin-left:30px; border-color:lightslategray;" />