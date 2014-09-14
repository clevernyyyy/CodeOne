<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="AccountLine.ascx.vb" Inherits="CodeOne.AccountLine" %>

<!-- Scripts -->
<script type="text/javascript" src="/Scripts/WebForms/Menu/Dashboard.js"></script>
<script src="../../Scripts/WebForms/Chart.js"></script>

<!-- Controls -->
<%@ Register Src="~/Controls/Budget/PieGraph.ascx" TagPrefix="uctrl" TagName="Pie" %>

<div id="AccountControl" runat="server" class="divAccts">
    <asp:LinkButton id="lnkAccountName" runat="server" CssClass="acctHeadings"></asp:LinkButton>
    <asp:HiddenField ID="hfAccountNum" runat="server" />
    <span id="balance" class="acctBalanceBak">
        <asp:Label ID="lblBalance" runat="server" CssClass="acctBalance"/>
        <br />
        <asp:Label ID="lblFake" runat="server" CssClass="acctBalance" />
    </span>
    <span id="divDue" runat="server" class="acctDebBak">
        <asp:Label ID="lblDueAmount" runat="server"  CssClass="acctDeb" />
        <br />
        <asp:Label ID="lblDueDate" runat="server" CssClass="acctDeb" />
    </span>
    <span id="divLast" runat="server" class="acctDebBak">
        <asp:Label ID="lblLastPaymentAmount" runat="server"  CssClass="acctDeb"/>
        <br />
        <asp:Label ID="lblLastPaymentDate" runat="server"  CssClass="acctDeb"/>
    </span>
    <div>
        <a ID="ancViewTen"  class="greenLink pointer">Last 10 Transactions</a> 
        <a id="divider" class="greenLink" style="margin-left:5px; text-decoration:none;" runat="server">|</a>
        <a ID="ancViewPie"  class="greenLink pointer" style="margin-left:5px;" runat="server">Visualize Account Data</a> 
        <div class="rowClassSpace">
            &nbsp;</div>
          <div id="grid" style="display:none;" class="row row-offcanvas row-offcanvas-right">
              <!-- Transactions Gridview -->
            <div id="Retrieve" class="centered">
                <div id="Retrieve_GridViewContainer" class="gridViewContainer">
                    <asp:GridView ID="dvgPack" runat="server" AllowDrop="True" CssClass="table table-hover table-striped table-bordered table-condensed" 
                        AutoGenerateColumns="false"
                        OnSorting="dgvPack_Sorting" AllowSorting="true" CellPadding="3" TabIndex="6"
                        PageSize="10" AllowPaging="true" PagerSettings-Position="TopAndBottom" PagerStyle-HorizontalAlign="Center">
                        <HeaderStyle ForeColor="Green" Font-Underline="false" BorderColor="Black"/>
                        <Columns >
                            <%--1--%><asp:BoundField DataField="TransDate" HeaderText="POST DATE" DataFormatString="{0:d}"
                                SortExpression="TransDate" ItemStyle-Width="50" HeaderStyle-CssClass="centered"  />
                            <%--3--%><asp:BoundField DataField="TransDesc" HeaderText="TRANSACTION" 
                                SortExpression="TransDesc" ItemStyle-Width="200" HeaderStyle-CssClass="centered" DataFormatString="{0}" />
                            <%--5--%><asp:BoundField DataField="TransAmount" HeaderText="AMOUNT"
                                SortExpression="TransAmount" ItemStyle-Width="50" HeaderStyle-CssClass="centered" DataFormatString="${0:C}" />
                        </Columns>
                        <PagerStyle CssClass="pager" />
                        <PagerTemplate>
                            <asp:Button ID="btnPrev" Text="<<" Width="75px" runat="server"
                                OnClick="dgvPackPrev_Click" />
                            <span style="display: inline-block; text-align: center; font-weight: bold;">Page
                                <asp:DropDownList ID="dgvPackDDL" AutoPostBack="true" OnSelectedIndexChanged="dgvPackDDL_SelectedIndexChanged"
                                    runat="server" />
                                out of
                                <asp:Label ID="lblPages" runat="server"></asp:Label>
                            </span>
                            <asp:Button ID="btnNext" Text=">>" Width="75px" runat="server"
                                OnClick="dgvPackNext_Click" />
                        </PagerTemplate>
                    </asp:GridView>
                </div>
            </div>
          </div>
        <div id="pie" style="display:none; margin-left:15px;" class="row row-offcanvas row-offcanvas-right">
            <div id="pieContent">
                <uctrl:Pie ID="ctrlPie" runat="server" style="float:left;"/>
            </div>
        </div>
    </div>
</div>
<hr style="margin-bottom:0px; margin-top:0px; margin-left:30px; border-color:lightslategray;" />