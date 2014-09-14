<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="IncomeExpense.ascx.vb" Inherits="CodeOne.IncomeExpense" %>
<head>
    <title></title>
  <link rel="stylesheet" href="//code.jquery.com/ui/1.11.1/themes/smoothness/jquery-ui.css">
  <script src="//code.jquery.com/jquery-1.10.2.js"></script>
  <script src="//code.jquery.com/ui/1.11.1/jquery-ui.js"></script>

  <script>
  $(function() {
      $("#dtpStart").datepicker();
      $("#dtpEnd").datepicker();
  });
  </script>
</head>

<form>
    <div id="bdgtControl" class="rowClass">
        <asp:HiddenField ID ="hfBudgetID" runat="server" />
        <asp:HiddenField ID="hfID" runat="server" />
        <asp:HiddenField ID="hfType" runat="server" />
        <div id="headings" style="display:inline-block;">
            <span class="shortGuy">Type:</span>
            <span style="margin-left:83px;">Description:</span>
            <span style="margin-left:138px;">Frequency:</span>
            <span style="margin-left:48px;">Amount:</span>
            <span style="margin-left:67px;">Date Start:</span>
            <span style="margin-left:53px;">Date End:</span>
        </div>
        <br />
        <div id="ctrls" style="display:inline-block;">
            <asp:DropDownList ID="ddlType"  CssClass="shortGuy" runat="server" />
            <span id="name" style="margin-left:25px;">
                <asp:TextBox ID="txtName" CssClass="longGuy" runat="server" />
            </span>
            <span id="freq" style="margin-left:25px;">
                <asp:dropdownlist ID="ddlFrequency" runat="server" CssClass="shortGuy" />
            </span>
            <span id="amt" style="margin-left:25px;">
                <asp:TextBox ID="txtAmount" runat="server" CssClass="shortGuy" />
            </span>        
            <span id="start" style="margin-left:25px;">
                <input type="text" class="shortGuy" id="dtpStart">
            </span>
            <span id="end" style="margin-left:25px;">
                <input type="text"  class="shortGuy" id="dtpEnd">
            </span>
        </div>
    </div>
</form>