<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="IncomeExpense.ascx.vb" Inherits="CodeOne.IncomeExpense" %>

<asp:HiddenField ID="hfBudgetID" runat="server" />
<asp:HiddenField ID="hfID" runat="server" />
<asp:HiddenField ID="hfType" runat="server" />
<asp:DropDownList ID="ddlType" runat="server" />
<asp:TextBox ID="txtName" runat="server" />
<asp:dropdownlist ID="ddlFrequency" runat="server" />
<asp:TextBox ID="txtAmount" runat="server" />
<asp:Calendar ID="dtpStart" runat="server" />
<asp:Calendar ID="dtpEnd" runat="server" />