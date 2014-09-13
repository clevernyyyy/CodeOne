<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="PieGraph.ascx.vb" Inherits="CodeOne.PieGraph" %>

<script src="../../Scripts/WebForms/PieChart.js" type="text/javascript" ></script>

<asp:Repeater ID="rptGraphs" runat="server">
    <ItemTemplate>
        <asp:hiddenField ID="hfAccountNum" runat="server" />
        <asp:Label ID="lblAccountNum" runat="server" />
        <div id="dvChart" runat="server">
        </div>
        <div id="dvLegend" runat="server">
        </div>
    </ItemTemplate>
</asp:Repeater>