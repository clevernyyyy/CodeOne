Imports System.Web.Security
Imports System.Data.SqlClient
Public Class Register
    Inherits System.Web.UI.UserControl
    Private cnn As SqlConnection = NewConnection(True)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("cErrorMessage") IsNot Nothing Then
            lblMsg.Text = Session("cErrorMessage").ToString
            Session.Remove("cErrorMessage")
        End If

        If IsPostBack Then
            If (Convert.ToString(Request.Form("__EVENTARGUMENT")) = "Register") Then
                Register()
            End If
        End If
    End Sub

    Private Sub Register()
        Dim cMsg As String = ""
        Dim lSuccess As Boolean = True
        If inputEmail.Value <> inputEmail2.Value Then
            cMsg &= "Emails do not match<br>"
            lSuccess = False
        End If
        If inputUserPass.Value <> inputUserPass2.Value Then
            cMsg &= "Passwords do not match<br>"
            lSuccess = False
        End If
        If UserExists(inputEmail.Value) Then
            lSuccess = False
            cMsg &= "User Already Exists"

        End If
        If lSuccess Then
            Try
                Dim cHashPass As String = FormsAuthentication.HashPasswordForStoringInConfigFile(inputUserPass.Value, "MD5")
                Dim cmd As New SqlCommand("[Admin].usp_Upsert_User")
                cmd.Connection = cnn
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@cEmail", inputEmail.Value)
                cmd.Parameters.AddWithValue("@cFName", inputFName.Value)
                cmd.Parameters.AddWithValue("@cMName", inputMName.Value)
                cmd.Parameters.AddWithValue("@cLName", inputLName.Value)
                cmd.Parameters.AddWithValue("@cPWD", cHashPass)
                cmd.Parameters.AddWithValue("@cNickName", inputNickName.Value)
                cnn.Open()
                cmd.ExecuteScalar()
                cMsg = "User Successfully Added"
            Catch ex As Exception
                cMsg = ex.Message
                lSuccess = False
            End Try
        End If
        If lSuccess Then
            Session("User") = New User(inputEmail.Value)
            Response.Redirect("~/Default.aspx")
        Else
            Session("cErrorMessage") = cMsg
        End If
    End Sub
    Private Function UserExists(cEmail As String) As Boolean
        Dim lExists As Boolean = True
        Try
            Dim cmd As New SqlCommand("[Admin].usp_UserExists")
            cmd.Connection = cnn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@cEmail", cEmail.Trim)
            cnn.Open()
            lExists = cmd.ExecuteScalar
        Catch ex As Exception
            lExists = True
        Finally
            cnn.Close()
        End Try
        Return lExists
    End Function

End Class