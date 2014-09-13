<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Pie.aspx.vb" Inherits="CodeOne.Pie" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../../Scripts/WebForms/Chart.js"></script>
<%@ Register Src="~/Controls/Budget/PieGraph.ascx" TagPrefix="uctrl" TagName="Pie" %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div>
        <uctrl:Pie ID="ctrlPie1" AccountNum="1" runat="server" style="float:left;"/>
    </div><div>
    <uctrl:Pie id="ctrlPie2" AccountNum="2" runat="server" style="float:right"/>
    </div>
</asp:Content>
