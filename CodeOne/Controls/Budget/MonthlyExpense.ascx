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
        <div id="ctrls">
            <div style="position:relative">
                <asp:label ID="lblCategory" CssClass="longGuy col-lg-4" runat="server" />
                <span id="amount" style="margin-left:15px;">
                    <asp:TextBox ID="txtAmount" CssClass="shortGuy  col-lg-4" runat="server" />
                </span>
            <br />
            </div>
        </div>
    </div>
</form>