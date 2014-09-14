<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Categories.ascx.vb" Inherits="CodeOne.Categories" %>

<%@ Register Src="~/Controls/Transactions/Transaction.ascx" TagPrefix="uctrl" TagName="Transaction" %>

    <div id="divCoverage" class="col-2 col-sm-2 col-lg-2" >
        <div id="divOutline" runat="server" class="categories" >
            <asp:Label runat="server" ID="lblCategory" CssClass="smallfont" ></asp:Label>
                <div id="divRptTransactions">
                    <asp:Repeater ID="rptTrans" runat="server">
                        <ItemTemplate>
                            <div id="divTransactions" style="display:inline-block;" runat="server">
                                <uctrl:Transaction id="ctrlTransaction" runat="server"></uctrl:Transaction>
                            </div>
                        </ItemTemplate>
                     </asp:Repeater>
                </div>
                
        </div>
    </div >