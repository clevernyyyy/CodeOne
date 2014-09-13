Public Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User") Is Nothing Then
            hUser.Value = ctrlLogin.Username
            hPass.Value = ctrlLogin.Password
        End If
        If Not IsPostBack() Then
            If Not CheckLogin() And Request.QueryString("Cancel") <> "True" Then
                'Option for automatically popping up if we decide that would be best
                'OpenLoginDialog()
            Else
                ctrlLogin.Visible = False
                LoadUserLvlDisplay()    'Access Control
            End If
        ElseIf IsPostBack AndAlso _
            Convert.ToString(Request.Form("__EVENTARGUMENT")) = "Login" Then
            'Popup once button is hit
            OpenLoginDialog()
        Else
            CheckLogin()
        End If
    End Sub

    Private Function CheckLogin() As Boolean
        'Only for testing, I imagine this will be in a class/module eventually
        If HttpContext.Current.Session("User") Is Nothing Then
            Dim strUserName As String = hUser.Value
            Dim strPassword As String = hPass.Value
            If strUserName <> "" And strPassword <> "" Then
                ctrlLogin.SignIn(strUserName, strPassword)
            End If
        End If
        Return HttpContext.Current.Session("User") IsNot Nothing
    End Function

    Private Sub OpenLoginDialog()
        Dim strJava As String = "OpenLoginDialog();"

        ScriptManager.RegisterStartupScript(Me, Me.GetType, "OpenLoginDialog", strJava, True)

    End Sub

    Private Sub LoadUserLvlDisplay()
        'Only show the user things they have access for

    End Sub

End Class