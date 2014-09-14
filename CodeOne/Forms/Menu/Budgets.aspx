<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Budgets.aspx.vb" Inherits="CodeOne.Budgets" %>

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
    <script type="text/javascript" src="/Scripts/WebForms/Menu/Budgets.js"></script>
    <script type="text/javascript" src="../../Scripts/WebForms/Chart.js"></script>

    <!--Styles-->
    <link href="../Styles/site_css/bootstrap.css" rel="stylesheet">
    <link href="../Styles/site_css/iCheck.css" rel="stylesheet">
    <link href="../Styles/site_css/custom.css" rel="stylesheet">

    <link href="../Styles/skins/square/green.css" rel="stylesheet">

    <!-- Controls -->
    <%@ Register Src="~/Controls/Budget/IncomeExpense.ascx" TagPrefix="uctrl" TagName="IncomeExpense" %>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        html, body, .container-fluid, .row {
            height: 100%;
        }
    </style>
    <body>
        <div class="">
            <div class="row">
                <div class="">
                    <br />
                    <div class="minitron">
                        <h1>Welcome!
                            <img src="../../Styles/config/img/logo.png" alt="logo" style="float: right" />
                        </h1>
                        <p>We're excited to be help you BudgetLogically.<span style="vertical-align: super; font-size: x-small;">&copy</span></p>
                    </div>
                </div>
            </div>
            <div style="margin-left: 10px;">
                <p class="pull-right visible-xs">
                    <button type="button" class="btn btn-primary btn-xs" data-toggle="offcanvas">
                        Toggle nav</button>
                </p>
                <div>
                    <h1 class="centered">Our Story</h1>
                    <hr />

                    <div class="panel-group" id="accordion">
                        <div class="panel panel-default">
                            <div class="panel-heading" data-toggle="collapse" data-parent="#accordion" href="#collapseOne">
                                <h4 class="panel-title">
                                    <a class="accordion-toggle">SET BUDGET
                                    </a>
                                </h4>
                            </div>
                            <div id="collapseOne" class="panel-collapse collapse in">
                                <div class="panel-body accordion-smallerfont">
                                    <span style="font-size: large; text-decoration: underline;">Step One:</span>
                                    <p>First, setup your known credits!</p>

                                    <span style="font-size: large;">INCOME:</span>
                                    <p>A common example of an income entry is a bi-weekly paycheck.  FinanceLogically can handle anything from an annuity payment to a simple check from grandma!</p>
                                    <uctrl:IncomeExpense ID="ctrlIncome" runat="server" />
                                    <br />
                                    <span style="font-size: large; text-decoration: underline;">Step Two:</span>
                                    <p>Next, fill out your known expenses!</p>
                                    <span style="font-size: large;">EXPENSES:</span>
                                    <p>A good example of a common expense would be your weekly grocery budget.  Or perhaps your car insurance bill.</p>
                                    <uctrl:IncomeExpense ID="ctrlExpenses" runat="server" />
                                </div>
                            </div>
                        </div>
                        <div class="panel panel-default">
                            <div class="panel-heading" data-toggle="collapse" data-parent="#accordion" href="#collapseTwo">
                                <h4 class="panel-title">
                                    <a class="accordion-toggle">WISH LIST
                                    </a>
                                </h4>
                            </div>
                            <div id="collapseTwo" class="panel-collapse collapse">
                                <div class="panel-body accordion-smallerfont">
                                    <div class="container">
                                        <div class="form-horizontal col-md-4">
                                            <div class="form-group">
                                                <div class="checkbox">
                                                    <label>
                                                        <input type="checkbox" value="Family Vacation US" />Family Vacation (USA)
                                                    </label>
                                                </div>
                                                <div class="checkbox">
                                                    <label>
                                                        <input type="checkbox" value="Family Vacation Int" />Family Vacation (International)
                                                    </label>
                                                </div>
                                                <div class="checkbox">
                                                    <label>
                                                        <input type="checkbox" value="College Savings" />College Savings
                                                    </label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-horizontal col-md-4">
                                            <div class="form-group">
                                                <div class="checkbox">
                                                    <label>
                                                        <input type="checkbox" value="Family Vacation US" />Family Vacation (USA)
                                                    </label>
                                                </div>
                                                <div class="checkbox">
                                                    <label>
                                                        <input type="checkbox" value="Family Vacation Int" />Family Vacation (International)
                                                    </label>
                                                </div>
                                                <div class="checkbox">
                                                    <label>
                                                        <input type="checkbox" value="College Savings" />College Savings
                                                    </label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-horizontal col-md-4">
                                            <div class="form-group">
                                                <div class="checkbox">
                                                    <label>
                                                        <input type="checkbox" value="Family Vacation US" />Family Vacation (USA)
                                                    </label>
                                                </div>
                                                <div class="checkbox">
                                                    <label>
                                                        <input type="checkbox" value="Family Vacation Int" />Family Vacation (International)
                                                    </label>
                                                </div>
                                                <div class="checkbox">
                                                    <label>
                                                        <input type="checkbox" value="College Savings" />College Savings
                                                    </label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="panel panel-default">
                            <div class="panel-heading" data-toggle="collapse" data-parent="#accordion" href="#collapseThree">
                                <h4 class="panel-title">
                                    <a class="accordion-toggle">BUDGET TARGETS
                                    </a>
                                </h4>
                            </div>
                            <div id="collapseThree" class="panel-collapse collapse">
                                <div class="panel-body accordion-smallerfont">
                                    <p>TEST</p>
                                </div>
                            </div>
                        </div>
                        <div class="panel panel-default">
                            <div class="panel-heading" data-toggle="collapse" data-parent="#accordion" href="#collapseFour">
                                <h4 class="panel-title">
                                    <a class="accordion-toggle">RETIREMENT PLANNING
                                    </a>
                                </h4>
                            </div>
                            <div id="collapseFour" class="panel-collapse collapse">
                                <div class="panel-body accordion-smallerfont">
                                    <p>TEST</p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <br />

            <hr style="position: static; margin-top: 500px;" />

            <footer class="footer">
                <p style="margin-left: 30px; font-weight: 500; font-family: 'Microsoft JhengHei'; margin-bottom: 0px; color: #216242;">&copy; Team ASL - 2014</p>
                <p style="margin-left: 30px; font-weight: 500; font-family: 'Microsoft JhengHei'; margin-top: 0px; margin-bottom: 0px; color: #216242;">CodeOne Hackathon - FNBO</p>
            </footer>

        </div>
        <!--/.container-->
        <script type="text/javascript" src="/Scripts/site_scripts/icheck.js"></script>


        <!-- Bootstrap core JavaScript
    ================================================== -->
        <!-- Placed at the end of the document so the pages load faster -->
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>
        <!-- IE10 viewport hack for Surface/desktop Windows 8 bug -->
        <script src="/Scripts/site_scripts/ie10-viewport-bug-workaround.js"></script>

        <script src="/Scripts/site_scripts/offcanvas.js"></script>
    </body>
</asp:Content>
