Public Class Login
    Inherits System.Web.UI.UserControl
    Public ReadOnly Property Username As String
        Get
            'Return txtUserName.Text
            Return inputUser.Value
        End Get
    End Property
    Public ReadOnly Property Password As String
        Get
            'Return txtPassword.Text
            Return inputPassword.Value
        End Get
    End Property
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack Then
            If (Convert.ToString(Request.Form("__EVENTARGUMENT")) = "Cancel") Then
                Session.Clear()
                Response.Redirect("~/Default.aspx?Cancel=True")
            ElseIf (Convert.ToString(Request.Form("__EVENTARGUMENT")) = "Login") Then
                If SignIn(inputUser.Value, inputPassword.Value) Then
                    Response.Redirect("~/Forms/Menu/Dashboard.aspx")
                End If
            End If
        End If

        AddRegJS(lbRegister)
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

#Region "JavaScript"
    Private Sub AddRegJS(ByVal lb As LinkButton)
        Dim strJava As String = ""

        strJava = "javascript:OpenRegister();"
        lb.Attributes.Add("onclick", strJava)
    End Sub
#End Region

End Class