<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="PieGraph.ascx.vb" Inherits="CodeOne.PieGraph" %>

<script src="../../Scripts/WebForms/PieChart.js" type="text/javascript" ></script>

<asp:Repeater ID="rptGraphs" runat="server">
    <ItemTemplate>
        <div id="divGraph" style="display:inline-block;" runat="server">
            <asp:hiddenField ID="hfPieAccountNum" runat="server" />
            <asp:hiddenField ID="hfGraphCat" runat="server" />
            <asp:Label ID="lblAccountNum" runat="server" />
            <div id="dvChart" runat="server">
            </div>
            <div id="dvLegend" runat="server">
            </div>
        </div>
    </ItemTemplate>
</asp:Repeater>