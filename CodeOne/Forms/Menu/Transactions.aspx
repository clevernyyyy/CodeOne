<%@ Page Title="Transactions" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Transactions.aspx.vb" Inherits="CodeOne.Transactions" %>

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
    <script type="text/javascript" src="/Scripts/jquery-draggable.min.js"></script>
    <script type="text/javascript" src="/Scripts/jquery-ui-draggable.min.js"></script>
    <script type="text/javascript" src="/Scripts/site_scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="/Scripts/site_scripts/modernizr-2.6.2.js"></script>

    <script>
        $(init);

        function init() {
            $('#Retrieve_GridViewContainer').draggable({
                cursor: 'move'
                
            });
        }
    </script>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
     html, body, .container-fluid, .row {
	height: 100%;
}

.sidebar {
  background-color: #CCCCCC;
}

@media (min-width: 992px) {
  .sidebar {
    position: fixed;
    top: 0;
    right: 0;
    bottom: 0;
    z-index: 1000;
    display: block;
    background-color: #CCCCCC;
  }
}

    </style>
    <body>
    
    <div class="container">
      <div class="col-md-9 col-md-offset-3 content">

      <div class="row row-offcanvas row-offcanvas-right">
          <!-- Transactions Gridview -->
        <div id="Retrieve" class="centered">
       
        <div class="rowClassSpace">
            &nbsp;</div>
            <asp:ScriptManager runat="server"></asp:ScriptManager>
            <div id="Retrieve_GridViewContainer" class="gridViewContainer">
                <asp:GridView ID="dvgPack" runat="server" CssClass="table table-hover table-striped table-bordered table-condensed" 
                    AutoGenerateColumns="false"
                    OnSorting="dgvPack_Sorting" AllowSorting="true" CellPadding="3" TabIndex="4"
                    PageSize="10" AllowPaging="true" PagerSettings-Position="TopAndBottom" PagerStyle-HorizontalAlign="Center">
                    <HeaderStyle ForeColor="Green" Font-Underline="false" BorderColor="Black"/>
                    <Columns>
                        <%--0--%><asp:BoundField DataField="cCategory" HeaderText="CATEGORY" SortExpression="cCategory"
                            ItemStyle-Width="200" HeaderStyle-CssClass="centered" ItemStyle-CssClass="left"/>
                        <%--1--%><asp:BoundField DataField="dPostDt" HeaderText="POST DATE" DataFormatString="{0:d}"
                            ItemStyle-Width="200"  HeaderStyle-CssClass="centered"  />
                        <%--2--%><asp:BoundField DataField="cDebitCredit" HeaderText="DEBIT/CREDIT" 
                            SortExpression="cDebitCredit" ItemStyle-Width="60"  HeaderStyle-CssClass="centered" />
                        <%--3--%><asp:BoundField DataField="cTransDesc" HeaderText="TRANSACTION" 
                            SortExpression="cTransDesc" ItemStyle-Width="120"  HeaderStyle-CssClass="centered" />
                        <%--4--%><asp:BoundField DataField="cTranDetailDesc" HeaderText="TRANSACTION DESCRIPTION" 
                            SortExpression="cTranDetailDesc" ItemStyle-Width="120"  HeaderStyle-CssClass="centered" />
                        <%--5--%><asp:BoundField DataField="nTranAmt" HeaderText="AMOUNT" 
                            SortExpression="nTranAmt" ItemStyle-Width="60"  HeaderStyle-CssClass="centered" />
                        <%-- HeaderStyle-CssClass="nodisplay" ItemStyle-CssClass="nodisplay" />--%>
                    </Columns>
                    <PagerStyle CssClass="pager" />
                    <PagerTemplate>
<%--                        <uctrl:CustomButton ID="btnPrev" Text="<<" Width="75px" runat="server" CausesValidation="true"
                            OnClick="dgvPackPrev_Click" />--%>
                        <span style="display: inline-block; text-align: center; font-weight: bold;">Page
                            <asp:DropDownList ID="dgvPackDDL" AutoPostBack="true" OnSelectedIndexChanged="dgvPackDDL_SelectedIndexChanged"
                                runat="server" />
                            out of
                            <asp:Label ID="lblPages" runat="server"></asp:Label>
                        </span>
<%--                        <uctrl:CustomButton ID="btnNext" Text=">>" Width="75px" runat="server" CausesValidation="true"
                            OnClick="dgvPackNext_Click" />--%>
                    </PagerTemplate>
                </asp:GridView>
            </div>
        </div>
      </div>
      </div>

       <div class="col-md-3 sidebar">
          Sidebar
        </div>

      <hr>

      <footer>
        <p>&copy; Team A/S/L - 2014</p>
      </footer>
    
    </div><!--/.container-->

    <!-- Bootstrap core JavaScript
    ================================================== -->
    <!-- Placed at the end of the document so the pages load faster -->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>
    <!-- IE10 viewport hack for Surface/desktop Windows 8 bug -->
    <script src="/Scripts/site_scripts/ie10-viewport-bug-workaround.js"></script>

    <script src="/Scripts/site_scripts/offcanvas.js"></script>
  </body>
</asp:Content>

