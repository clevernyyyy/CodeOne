<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="BudgetTargets.aspx.vb" Inherits="CodeOne.BudgetTargets" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../../Scripts/WebForms/Chart.js" type="text/javascript" ></script>
    <script src="../../Scripts/WebForms/LineGraph.js" type="text/javascript" ></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<asp:HiddenField ID="hfUserId" runat="server" />
    <div id="dvChart">

    </div>
</asp:Content>
