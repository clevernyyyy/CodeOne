<%@ Page Title="Transactions" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Transactions.aspx.vb" Inherits="CodeOne.Transactions" %>

<%@ Register Src="~/Controls/Transactions/Transaction.ascx" TagPrefix="uctrl" TagName="Transaction" %>
<%@ Register Src="~/Controls/Transactions/Category.ascx" TagPrefix="uctrl" TagName="Category" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="CodeOne Test Site">
    <meta name="author" content="Team A/S/L">
    <link rel="shortcut icon" href="">
    <title>Transactions</title>

    <!-- Custom styles for this template -->
    <link type="text/css" rel="stylesheet" href="/Styles/bootstrap.min.css" />
    <link type="text/css" rel="stylesheet" href="https://code.jquery.com/ui/1.9.2/themes/base/jquery-ui.css" />
    <link type="text/css" rel="stylesheet" href="https://code.jquery.com/ui/1.10.1/themes/base/jquery-ui.css" />
    <link href="/Styles/site_css/main.css" rel="stylesheet" />
    <link href="/Styles/site_css/icomoon.css" rel="stylesheet" />
    <link href="/Styles/site_css/animate-custom.css" rel="stylesheet" />
    <link href='http://fonts.googleapis.com/css?family=Lato:300,400,700,300italic,400italic'
        rel='stylesheet' type='text/css' />
    <link href='http://fonts.googleapis.com/css?family=Raleway:400,300,700' rel='stylesheet'
        type='text/css' />

    <!-- Scripts -->
    <script type="text/javascript" src="/Scripts/site_scripts/jquery-draggable.min.js"></script>
    <script type="text/javascript" src="/Scripts/site_scripts/jquery-ui-draggable.min.js"></script>

    <script type="text/javascript" src="/Scripts/site_scripts/jquery-1.10.2-droppable.js"></script>
    <script type="text/javascript" src="/Scripts/site_scripts/jquery-ui-droppable.js"></script>

    <script type="text/javascript" src="/Scripts/site_scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="/Scripts/site_scripts/modernizr-2.6.2.js"></script>
    <script type="text/javascript" src="/Scripts/WebForms/Menu/Transactions.js"></script>

    <script>
        $(init);

        function init() {
            $('#divRepCategory').find("div").draggable({
                cursor: 'move'
            });

            $('#Retrieve_GridViewContainer').find("td.left").draggable({ cursor: "move", snap: true });
            $('#Retrieve_GridViewContainer').find("td.left").droppable({
                drop: function (event, ui) {
                    $(this).html($(ui.draggable).html());
                }
            });
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        html, body, .container-fluid, .row {
            height: 100%;
        }
    </style>

    <div class="container" style="margin-left:40px;">
        <h1 style="margin-top:10px; color:darkgreen;">Transactions</h1>

         <div class="row row-offcanvas row-offcanvas-right">
              <!-- Transactions Gridview -->
            <div id="Retrieve" class="centered">
       
            <div class="rowClassSpace">
                &nbsp;</div>
                <asp:ScriptManager runat="server"></asp:ScriptManager>
                <div id="Retrieve_GridViewContainer" class="gridViewContainer">
                    <asp:GridView ID="dvgPack" runat="server" AllowDrop = True CssClass="table table-hover table-striped table-bordered tabBoard" 
                        AutoGenerateColumns="false"
                        OnSorting="dgvPack_Sorting" AllowSorting="true" CellPadding="3" TabIndex="6"
                        PageSize="10" AllowPaging="true" PagerSettings-Position="TopAndBottom" PagerStyle-HorizontalAlign="Center">
                        <HeaderStyle ForeColor="Green" Font-Underline="false" BorderColor="Black"/>
                        <Columns >
                            <asp:BoundField DataField="cCategory" HeaderText="CATEGORY" SortExpression="cCategory"
                                ItemStyle-Width="50" HeaderStyle-CssClass="centered" ItemStyle-CssClass="left"/>
                            <asp:BoundField DataField="dPostDt" HeaderText="POST DATE" DataFormatString="{0:d}"
                                SortExpression="dPostDt" ItemStyle-Width="50"  HeaderStyle-CssClass="centered"  />
                            <asp:BoundField DataField="cdebitCredit" HeaderText="DEBIT & CREDIT" 
                                SortExpression="cDebitCredit" ItemStyle-Width="25"  HeaderStyle-CssClass="centered"/>
                            <asp:BoundField DataField="cTransDesc" HeaderText="TRANSACTION" 
                                SortExpression="cTransDesc" ItemStyle-Width="200"  HeaderStyle-CssClass="centered" />
                            <asp:BoundField DataField="cTranDetailDesc" HeaderText="TRANSACTION DESCRIPTION" 
                                SortExpression="cTranDetailDesc" ItemStyle-Width="200"  HeaderStyle-CssClass="centered" />
                            <asp:BoundField DataField="nTranAmt" HeaderText="AMOUNT" 
                                SortExpression="nTranAmt" ItemStyle-Width="50"  HeaderStyle-CssClass="centered" />
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
           </div>
    <!--/.container-->
        <footer class="footer">
            <p style="margin-left:30px; font-weight:500; font-family:'Microsoft JhengHei'; margin-bottom:0px; color:#216242;">&copy; Team ASL - 2014</p>
            <p style="margin-left:30px; font-weight:500; font-family:'Microsoft JhengHei'; margin-top:0px; margin-bottom:0px; color:#216242;">CodeOne Hackathon - FNBO</p>
        </footer>


    <!-- Bootstrap core JavaScript
    ================================================== -->
    <!-- Placed at the end of the document so the pages load faster -->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>
    <!-- IE10 viewport hack for Surface/desktop Windows 8 bug -->
    <script src="/Scripts/site_scripts/ie10-viewport-bug-workaround.js"></script>

    <script src="/Scripts/site_scripts/offcanvas.js"></script>
    </body>
</asp:Content>

