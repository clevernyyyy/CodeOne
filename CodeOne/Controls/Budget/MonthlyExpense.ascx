<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="MonthlyExpense.ascx.vb" Inherits="CodeOne.MonthlyExpense" %>

<form>
    <div id="mnthExpenseControl" class="rowClass">
        <asp:HiddenField ID ="hfBudgetID" runat="server" />
        <asp:HiddenField ID="hfID" runat="server" />
        <asp:HiddenField ID="hfType" runat="server" />
        <%--<div id="headings" style="display:inline-block;">
            <span style="margin-left:0;">Category</span>
            <span style="margin-left:0;">Amount:</span>
        </div>--%>
        <br />
        <div id="ctrls" style="display:inline-block;">
            <asp:label ID="lblCategory" CssClass="shortGuy" runat="server" />
            <span id="amount" style="margin-left:15px;">
                <asp:TextBox ID="txtAmount" CssClass="shortGuy" runat="server" />
            </span>
        </div>
    </div>
</form>