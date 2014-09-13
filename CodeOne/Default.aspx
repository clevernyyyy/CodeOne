<%@ Page Title="Home Page" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.vb" Inherits="CodeOne._Default" %>


<%@ Register Src="~/Controls/Login/Login.ascx" TagPrefix="uctrl" TagName="Log" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="Login Page">
    <meta name="author" content="Adam Schaal">
    <link rel="shortcut icon" href="">
    <title>Login</title>

    <!-- Custom styles for this template -->
    <link type="text/css" rel="stylesheet" href="/Styles/site_css/bootstrap.min.css" />
    <link type="text/css" rel="stylesheet" href="https://code.jquery.com/ui/1.9.2/themes/base/jquery-ui.css" />
    <link type="text/css" rel="stylesheet" href="https://code.jquery.com/ui/1.10.1/themes/base/jquery-ui.css" />
    <link href="/Styles/site_css/main.css" rel="stylesheet" />
    <link href="/Styles/site_css/icomoon.css" rel="stylesheet" />
    <link href="/Styles/site_css/animate-custom.css" rel="stylesheet" />
    <link href='http://fonts.googleapis.com/css?family=Lato:300,400,700,300italic,400italic'
        rel='stylesheet' type='text/css' />
    <link href='http://fonts.googleapis.com/css?family=Raleway:400,300,700' rel='stylesheet'
        type='text/css' />
    <link href='/Styles/site_css/custom.css' rel='stylesheet' type='text/css' />

    <!-- Scripts -->
    <script type="text/javascript" src="/Scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="/Scripts/modernizr-2.6.2.js"></script>
    <script type="text/javascript" src="/Scripts/Login/Login.js?cachebreak=08162014"></script>
    <script type="text/javascript" src="/Scripts/holder.js"></script>

    <!-- favicon -->
    <link rel="shortcut icon" href="/img/favicon.ico?v=2" type="image/x-icon" />
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>FinanceLogically</h1>
        <p class="lead">FinanceLogically is a web application to demonstrate logical spending, logical savings, and logical budgeting.</p>
        <p>
        <button type="button" class="btn btn-success btn-lg" id="btnLogin" runat="server" onclick="__doPostBack('btnLogin','Login');">Login &raquo;</button>

            
        <a href="https://www.firstnational.com/site/personal/" class="btn btn-success btn-lg">Learn more &raquo;</a></p>
    </div>

    <div class="row">
        <div class="col-md-1">
            </div>
        <div class="col-md-2">
            <img src="\Styles\assets\site\img\personal\promo-huskersdebit.jpg" class="img-thumbnail" alt="">
        </div>
        <div class="col-md-1">
            </div>
        <div class="col-md-4">
            <img src="\Styles\assets\site\img\personal\mainstage-mobile-postlaunch.jpg" class="img-thumbnail" alt="">
        </div>
        <div class="col-md-1">
            </div>
        <div class="col-md-2">
            <img src="\Styles\assets\site\img\personal\promo-artsarben.jpg" class="img-thumbnail" alt="">
        </div>
    </div>
    
    <div id="divLoginOpen" style="display:none">
        <uctrl:Log ID="ctrlLogin" runat="server" />
        <asp:HiddenField ID="hUser" runat="server" />
        <asp:HiddenField ID="hPass" runat="server" />
    </div>
    <!-- Bootstrap core JavaScript
    ================================================== -->
    <!-- Placed at the end of the document so the pages load faster -->
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>
    <script type="text/javascript" src="../Styles/site_css/bootstrap.min.js"></script>
    <script type="text/javascript" src="../Styles/site_css/docs.min.js"></script>
</asp:Content>
