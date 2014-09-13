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

        .sidebar {
            background-color: #CCCCCC;
                border-left:5px solid #216242;
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
                border-left:5px solid #216242;
            }
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
                    <div id="DepositAccounts" class="acctHeader">
                        DEPOSIT ACCOUNTS
                        <hr style="margin-bottom: 0px; margin-top: 0px;" />
                    </div>
                        <div id="depControl">
                            <asp:Repeater ID="depRepeater" runat="server">
                                <ItemTemplate>
                                    <uctrl:Account ID="depAccount" runat="server" ShowPaymentDue="false" ShowLastPayment="false"></uctrl:Account>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    <div id="InvestmentAccounts" class="acctHeader">
                        INVESTMENT ACCOUNTS
                        <hr style="margin-bottom: 0px; margin-top: 0px;" />
                    </div>
                        <div id="invControl">
<%--                            <asp:Repeater ID="invRepeater" runat="server">
                                <ItemTemplate>
                                    <uctrl:Account ID="invAccount" runat="server" ShowPaymentDue="false" ShowLastPayment="false">
                                    </uctrl>
                                </ItemTemplate>
                            </asp:Repeater>--%>
                        </div>

                    <div id="CreditAccounts" class="acctHeader">
                        CREDIT ACCOUNTS
                        <hr style="margin-bottom: 0px; margin-top: 0px;" />
                    </div>
<%--                        <div id="creControl">
                            <asp:Repeater ID="creRepeater" runat="server">
                                <ItemTemplate>
                                    <uctrl:Account ID="creAccount" runat="server" ShowPaymentDue="true" ShowLastPayment="true">
                                    </uctrl>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>--%>

                    <div id="LoanAccounts" class="acctHeader">
                        LOAN ACCOUNTS
                        <hr style="margin-bottom: 0px; margin-top: 0px;" />
                    </div>
<%--                        <div id="loanControl">
                            <asp:Repeater ID="loanRepeater" runat="server">
                                <ItemTemplate>
                                    <uctrl:Account ID="loanAccount" runat="server" ShowPaymentDue="true" ShowLastPayment="true">
                                    </uctrl>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>--%>
                </div>

                <div class="col-md-3 sidebar">
                    Sidebar
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
