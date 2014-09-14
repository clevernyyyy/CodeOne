﻿Imports System
Imports System.Web.UI
Imports System.Collections
Imports System.Collections.Specialized
Public Class SiteMaster
    Inherits MasterPage
    Private Const AntiXsrfTokenKey As String = "__AntiXsrfToken"
    Private Const AntiXsrfUserNameKey As String = "__AntiXsrfUserName"
    Private _antiXsrfTokenValue As String

    Protected Sub Page_Init(sender As Object, e As EventArgs)
        ' The code below helps to protect against XSRF attacks
        Dim requestCookie = Request.Cookies(AntiXsrfTokenKey)
        Dim requestCookieGuidValue As Guid
        If requestCookie IsNot Nothing AndAlso Guid.TryParse(requestCookie.Value, requestCookieGuidValue) Then
            ' Use the Anti-XSRF token from the cookie
            _antiXsrfTokenValue = requestCookie.Value
            Page.ViewStateUserKey = _antiXsrfTokenValue
        Else
            ' Generate a new Anti-XSRF token and save to the cookie
            _antiXsrfTokenValue = Guid.NewGuid().ToString("N")
            Page.ViewStateUserKey = _antiXsrfTokenValue

            Dim responseCookie = New HttpCookie(AntiXsrfTokenKey) With { _
                 .HttpOnly = True, _
                 .Value = _antiXsrfTokenValue _
            }
            If FormsAuthentication.RequireSSL AndAlso Request.IsSecureConnection Then
                responseCookie.Secure = True
            End If
            Response.Cookies.[Set](responseCookie)
        End If

        AddHandler Page.PreLoad, AddressOf master_Page_PreLoad
    End Sub

    Protected Sub master_Page_PreLoad(sender As Object, e As EventArgs)
        If Not IsPostBack Then
            ' Set Anti-XSRF token
            ViewState(AntiXsrfTokenKey) = Page.ViewStateUserKey
            ViewState(AntiXsrfUserNameKey) = If(Context.User.Identity.Name, [String].Empty)
        Else
            ' Validate the Anti-XSRF token
            If DirectCast(ViewState(AntiXsrfTokenKey), String) <> _antiXsrfTokenValue OrElse DirectCast(ViewState(AntiXsrfUserNameKey), String) <> (If(Context.User.Identity.Name, [String].Empty)) Then
                Throw New InvalidOperationException("Validation of Anti-XSRF token failed.")
            End If
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If Request.QueryString.Count = 2 Then
                SignIn(Request.QueryString.Item(0), Request.QueryString.Item(1))
            'Response.Redirect("~/Default.aspx")
            Response.Redirect("~/Forms/Menu/Dashboard.aspx")
                Response.Redirect(Request.Path)
            End If
            If (Convert.ToString(Request.Form("__EVENTARGUMENT")) = "LogIn") Then
                If SignIn(txtUserName.Value, txtPassword.Value) Then
                    Response.Redirect("~/Forms/Menu/Dashboard.aspx")
                End If
            End If
    End Sub

    Protected Sub Unnamed_LoggingOut(sender As Object, e As LoginCancelEventArgs)
        Context.GetOwinContext().Authentication.SignOut()
    End Sub

    Protected Sub LogOut_Click(sender As Object, e As EventArgs)
        If HttpContext.Current.Session("User") IsNot Nothing Then
            Session.Clear()
        End If
        Response.Redirect("~/Default.aspx")
    End Sub

    Public Function SignIn(userName As String, passWord As String) As Boolean
        Dim blnSuccess As Boolean = False
        Dim objUser As User
        If ValidateUser(userName, passWord, objUser) Then
            Session("User") = objUser
            blnSuccess = True
        End If
        Return blnSuccess
    End Function

    'Private Sub btnSignIn_ServerClick(sender As Object, e As System.EventArgs) Handles btnSignIn.ServerClick

    '    SignIn()

    'End Sub

   Protected Sub RaisePostBackEvent(source As IPostBackEventHandler, eventArgument As String)
	    'call the RaisePostBack event 
	    'MyBase.RaisePostBackEvent(source, eventArgument)

			  '  'do some logic
	    If source = "" Then
	    End If
    End Sub


    'Private Sub btnSubmit_ServerClick(sender As Object, e As EventArgs) Handles btnSubmit.ServerClick
    '    SignIn()
    'End Sub


End Class