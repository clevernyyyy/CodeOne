<%@ Page Title="Acct Dashboard" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Dashboard.aspx.vb" Inherits="CodeOne.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="CodeOne Test Site">
    <meta name="author" content="Team A/S/L">
    <link rel="shortcut icon" href="">
    <title>Acct Dashboard</title>

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
    <script type="text/javascript" src="/Scripts/site_scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="/Scripts/site_scripts/modernizr-2.6.2.js"></script>

    <link href="../Styles/site_css/bootstrap.css" rel="stylesheet">


    <!-- Controls -->
    <%@ Register Src="~/Controls/Dashboard/AccountLine.ascx" TagPrefix="uctrl" TagName="Account" %>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        html, body, .container-fluid, .row {
            height: 100%;
        }
    </style>
    <body>
        <div class="container-fluid mainbody">
            <div class="row">
                <div class="col-md-9  content">
                    <br />
                    <div class="minitron">
                        <h1>Welcome!
                            <img src="../../Styles/config/img/logo.png" alt="logo" style="float: right" />
                        </h1>
                        <p>We're excited to be help you FinanceLogically.<sup>&copy</sup></p>
                    </div>
                    <div class="fullHeader">
                        Accounts
                    </div>
                    <div class="fullHeader">
                        <asp:linkButton id="lnkGraphs" runat="server" text="Graphs!"></asp:linkButton>
                    </div>
                    <br />
                    <div id="headerDep" style="margin-bottom:10px;">
                        <span id="DepositAccounts" class="acctHeader">
                            DEPOSIT ACCOUNTS
                        </span>
                        <span id="balDep" class="balHeader">
                            BALANCE
                        </span>
                    </div>
                    <br />
                    <div id="divDeposits">
                    <hr style="margin-bottom:0px; margin-top:0px; margin-left:30px; border-color:lightslategray;" />
                            <asp:Repeater ID="repDeposits" runat="server">
                                <ItemTemplate>
                                    <uctrl:Account ID="uctrlAccount" runat="server" ShowPaymentDue="false" ShowLastPayment="false"></uctrl:Account>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    <div id="headerInv" style="margin-bottom:10px;">
                        <span id="InvestmentAccounts" class="acctHeader">
                            INVESTMENT ACCOUNTS
                        </span>
                        <span id="balInv" class="balHeader">
                            BALANCE
                        </span>
                    </div>
                    <br />
                    <hr style="margin-bottom:0px; margin-top:0px; margin-left:30px; border-color:lightslategray;" />
                        <div id="divInvestments">
                            <asp:Repeater ID="repInvestments" runat="server">
                                <ItemTemplate>
                                    <uctrl:Account ID="uctrlAccount" runat="server" ShowPaymentDue="false" ShowLastPayment="false"></uctrl:Account>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    <div id="headerCred" style="margin-bottom:25px;">
                        <span id="CreditAccounts" class="acctHeader">
                            CREDIT ACCOUNTS
                        </span>
                        <span id="balCred" class="balHeader">
                            BALANCE
                        </span>
                        <span id="PayDueCred" class="dateHeader">
                            PAYMENT DUE
                        </span>
                        <span id="lastPayCred" class="dateHeader">
                            LAST PAYMENT
                        </span>
                    </div>
                    <br />
                    <hr style="margin-bottom:0px; margin-top:0px; margin-left:30px; border-color:lightslategray;" />

                        <div id="divCredits">
                            <asp:Repeater ID="repCredits" runat="server">
                                <ItemTemplate>
                                    <uctrl:Account ID="uctrlAccount" runat="server" ShowPaymentDue="true" ShowLastPayment="true"></uctrl:Account>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                  
                    <div id="headerLoan" style="margin-bottom:25px;">
                        <span id="LoanAccounts" class="acctHeader">
                            LOAN ACCOUNTS
                        </span>
                        <span id="balLoan" class="balHeader">
                            BALANCE
                        </span>
                        <%--<span id="PayDueLoan" class="dateHeader">
                            PAYMENT DUE
                        </span>--%>
                        <span id="lastPayLoan" class="dateHeader">
                            LAST PAYMENT
                        </span>
                    </div>
                    <br />
                    <hr style="margin-bottom:0px; margin-top:0px; margin-left:30px; border-color:lightslategray;" />
                        <div id="divLoans">
                            <asp:Repeater ID="repLoans" runat="server">
                                <ItemTemplate>
                                    <uctrl:Account ID="uctrlAccount" runat="server" ShowPaymentDue="false" ShowLastPayment="true"></uctrl:Account>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                </div>

                </div>

            </div>

            <hr>

            <footer class="footer">
                <p style="margin-left:30px; font-weight:500; font-family:'Microsoft JhengHei'; margin-bottom:0px; color:#216242;">&copy; Team ASL - 2014</p>
                <p style="margin-left:30px; font-weight:500; font-family:'Microsoft JhengHei'; margin-top:0px; margin-bottom:0px; color:#216242;">CodeOne Hackathon - FNBO</p>
            </footer>

        </div>
        <!--/.container-->

    <!-- Bootstrap core JavaScript
    ================================================== -->
    <!-- Placed at the end of the document so the pages load faster -->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>
    <!-- IE10 viewport hack for Surface/desktop Windows 8 bug -->
    <script src="/Scripts/site_scripts/ie10-viewport-bug-workaround.js"></script>

    <script src="/Scripts/site_scripts/offcanvas.js"></script>
    </body>
</asp:Content>
