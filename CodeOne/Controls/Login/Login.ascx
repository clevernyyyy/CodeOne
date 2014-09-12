<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Login.ascx.vb" Inherits="CodeLogically.Login" %>

<%@ Register Src="~/Controls/Login/Register.ascx" TagPrefix="uctrl" TagName="Reg" %>

<!-- including jQuery Dialog UI here-->
<link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.9.2/themes/ui-darkness/jquery-ui.css" rel="stylesheet">
<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>
<script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.9.2/jquery-ui.min.js"></script>
<script type="text/javascript" src="js/dialog.js"></script>

 <!-- Custom styles for this control -->
 <link href="/Styles/Login/Login.css" rel="stylesheet" />

<div id="divLoginControl" >
<asp:ScriptManager runat="server"></asp:ScriptManager>
<asp:UpdatePanel runat="server" id="udpLogin">
<ContentTemplate>

    <asp:Label ID="lblLogin" runat="server" CssClass="questionText" 
        Text="Please Login!"></asp:Label>
    <br />
    <%--<asp:Label id="lblUser" runat="server" Text="User Name:">User Name:</asp:Label>--%>
<%--    <asp:TextBox ID="txtUserName" runat="server" Width="200px" style="text-align:left; 
        font-size:small;" CssClass="smallBox"></asp:TextBox>--%>
    <input type="text" id="inputUser" placeholder="UserName" runat="server" />
        <br />
<%--    <asp:Label id="lblPassword" runat="server" Text="Password:">Password:</asp:Label>--%>
<%--    <asp:TextBox ID="txtPassword" runat="server" Width="200px" style="text-align:left; 
        font-size:small;" CssClass="smallBox" TextMode="Password" ></asp:TextBox>--%>
        <input type="password" id="inputPassword" placeholder="Password" runat="server" />
    <br />

    <asp:Label ID="lblRegister" runat="server" Text="Don't have a login?  "></asp:Label>
    
    <asp:LinkButton ID="lbRegister" runat="server" Text="Register Now!" ForeColor="SlateBlue"></asp:LinkButton>
       
    <div id="divBottomLeft" class="bottomPopupLeft">        
        <button type="button" id="btnCancel" runat="server" onclick="__doPostBack('btnCancel','Cancel');">Cancel</button>
    </div>
    <div id="divBottomRight" class="bottomPopupRight">
        <button type="button" id="btnFinish" runat="server" onclick="__doPostBack('btnLogin','Login');">Login</button>
    </div>

      </ContentTemplate>
</asp:UpdatePanel>
</div>

    
<div class="none border" id="divRegisterControl" title="Help">
    <uctrl:Reg ID="Register" runat="server" />
</div>
