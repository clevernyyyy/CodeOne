<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Standard.aspx.vb" Inherits="CodeOne.Standard" %>

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
    <body>

    <div class="container">
        <h1>Transactions</h1>
        <div id="playground" style="border: 1px solid darkgreen; -moz-border-radius: 15px; border-radius: 15px;">
            <div id="categories" style="display:block;">
                <h3 class="cursor" style="margin-left:25px;">Categories <a id="expand" style="text-decoration:none; color:darkgreen;" href="#">+</a>
                </h3>
                <div id="divHideCategory" runat="server">
                    <div id="divRepCategory" style="display: none">
                        <asp:Repeater runat="server" ID="rptCategories">
                            <ItemTemplate>
                                <uctrl:Category ID="ctrlCategory" runat="server"></uctrl:Category>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
            <br />
            <div id="transactions" style="margin-top:50px;">
                <br />
                <asp:Repeater ID="rptTrans" runat="server">
                    <ItemTemplate>
                        <div id="divTransactions" runat="server">
                            <uctrl:Transaction ID="ctrlTransaction" runat="server"></uctrl:Transaction>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>

      </div>
    <!--/.container-->
    <footer>
        <p>&copy; Team A/S/L - 2014</p>
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

