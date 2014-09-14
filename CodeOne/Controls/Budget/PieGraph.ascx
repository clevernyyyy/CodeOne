<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="PieGraph.ascx.vb" Inherits="CodeOne.PieGraph" %>

<%--<script src="../../Scripts/WebForms/PieChart.js" type="text/javascript" ></script>--%>

<asp:Repeater ID="rptGraphs" runat="server">
    <ItemTemplate>
        <div id="divGraph" style="display:inline-block;" runat="server">
            <asp:hiddenField ID="hfPieAccountNum" runat="server" />
            <asp:hiddenField ID="hfGraphCat" runat="server" />
            <asp:Label ID="lblAccountNum" runat="server" />
            <span runat="server" id="charts">
                <div id="dvChart" style="float:left;" runat="server">
                </div>
                <div id="dvLegend" style="float:right;" runat="server">
                </div>
            </span>
        </div>
    </ItemTemplate>
</asp:Repeater>