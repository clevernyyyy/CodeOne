<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Transaction.ascx.vb" Inherits="CodeOne.Transaction" %>

<div id="TransactionControl" runat="server" >
    <div id="line" style="display:block">
        <span id="divDate" class="transDate transEl" > 
            <asp:Label ID="lblDate" runat="server"/>
        </span>
        <span id="divAmount" runat="server" class="transEl" style="border: 1px solid slategrey;">
            <asp:Label ID="lblAmount" runat="server" />
        </span>
        <span id="divCategory" runat="server" class="transEl" style="border: 1px solid slategrey;">
            <asp:Label ID="lblCategory" runat="server" />
        </span>
        <span id="divDetail" runat="server" class="transEl" style="border: 1px solid slategrey;">
            <asp:Label ID="lblDetail" runat="server" />
        </span>
    </div>
    <br />
</div>
<hr style="margin-bottom:0px; margin-top:0px; margin-left:30px; border-color:lightslategray;" />
    <br />