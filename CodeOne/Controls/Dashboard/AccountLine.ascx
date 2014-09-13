<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="AccountLine.ascx.vb" Inherits="CodeOne.AccountLine" %>

<div id="AccountControl" runat="server" class="divAccts">
    <asp:LinkButton id="lnkAccountName" runat="server" CssClass="acctHeadings"></asp:LinkButton>
    <asp:HiddenField ID="hfAccountNum" runat="server" />
    <span id="divLast" runat="server">
        <asp:Label ID="lblLastPaymentDate" runat="server" />
        <asp:Label ID="lblLastPaymentAmount" runat="server" />
    </span>
    <span id="divDue" runat="server">
        <asp:Label ID="lblDueDate" runat="server" />
        <asp:Label ID="lblDueAmount" runat="server" />
    </span>
    <span id="balance" class="acctBalanceBak">
        <asp:Label ID="lblBalance" runat="server" CssClass="acctBalance"/>
    </span>
    <div>
        <asp:LinkButton ID="lnkViewTen" runat="server" CssClass="greenLink">Last 10 Transactions</asp:LinkButton>
    </div>
      <div class="row row-offcanvas row-offcanvas-right">
          <!-- Transactions Gridview -->
        <div id="Retrieve" class="centered">
       
        <div class="rowClassSpace">
            &nbsp;</div>
            <div id="Retrieve_GridViewContainer" class="gridViewContainer">
                <asp:GridView ID="dvgPack" runat="server" AllowDrop = True CssClass="table table-hover table-striped table-bordered table-condensed" 
                    AutoGenerateColumns="false"
                    OnSorting="dgvPack_Sorting" AllowSorting="true" CellPadding="3" TabIndex="6"
                    PageSize="10" AllowPaging="false" >
                    <HeaderStyle ForeColor="Green" Font-Underline="false" BorderColor="Black"/>
                    <Columns >
                        <%--0--%><%--<asp:BoundField DataField="cCategory" HeaderText="CATEGORY" SortExpression="cCategory"
                            ItemStyle-Width="50" HeaderStyle-CssClass="centered" ItemStyle-CssClass="left"/>--%>
                        <%--1--%><asp:BoundField DataField="TransDate" HeaderText="POST DATE" DataFormatString="{0:d}"
                            SortExpression="dPostDt" ItemStyle-Width="50"  HeaderStyle-CssClass="centered"  />
                        <%--2--%><%--<asp:BoundField DataField="cDebitCredit" HeaderText="DEBIT & CREDIT" 
                            SortExpression="cDebitCredit" ItemStyle-Width="25"  HeaderStyle-CssClass="centered"/>--%>
                        <%--3--%><asp:BoundField DataField="TransDesc" HeaderText="TRANSACTION" 
                            SortExpression="cTransDesc" ItemStyle-Width="200"  HeaderStyle-CssClass="centered" />
                        <%--4--%><%--<asp:BoundField DataField="cTranDetailDesc" HeaderText="TRANSACTION DESCRIPTION" 
                            SortExpression="cTranDetailDesc" ItemStyle-Width="200"  HeaderStyle-CssClass="centered" />--%>
                        <%--5--%><asp:BoundField DataField="TransAmount" HeaderText="AMOUNT" 
                            SortExpression="nTranAmt" ItemStyle-Width="50"  HeaderStyle-CssClass="centered" />
                        <%-- HeaderStyle-CssClass="nodisplay" ItemStyle-CssClass="nodisplay" />--%>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
      </div>
</div>