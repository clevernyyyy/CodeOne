<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="BudgetTargets.aspx.vb" Inherits="CodeOne.BudgetTargets" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="/Scripts/site_scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="/Scripts/site_scripts/modernizr-2.6.2.js"></script>
    <script src="../../Scripts/WebForms/Chart.js" type="text/javascript" ></script>
    <script src="../../Scripts/WebForms/LineGraph.js" type="text/javascript" ></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<asp:HiddenField ID="hfUserId" runat="server" />
    <div id="_dvChart">

    </div>
    <div id="dvLegend">

    </div>

</asp:Content>
