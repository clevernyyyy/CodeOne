Imports System.Web.HttpContext
Imports System.Data.SqlClient
Imports CodeOne.Enumerations
Module MiscFunctions
#Region "Connection"
    Public Function SqlCommand(commandText As String) As SqlCommand
        Dim cmd As New SqlCommand
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = commandText
        cmd.Connection = (New Connection).NewCnn
        Return cmd
    End Function
#End Region
#Region "Login"
    Public Function ValidateUser(ByVal userName As String, ByVal passWord As String, ByRef objUser As User) As Boolean
        Dim lookupPassword As String

        lookupPassword = Nothing
        Dim drUser As DataRow
        ' Check for an invalid userName.
        ' userName  must not be set to nothing and must be between one and 50 characters.
        If ((userName Is Nothing)) Then
            System.Diagnostics.Trace.WriteLine("[ValidateUser] Input validation of userName failed.")
            Return False
        End If
        If ((userName.Length = 0) Or (userName.Length > 50)) Then
            System.Diagnostics.Trace.WriteLine("[ValidateUser] Input validation of userName failed.")
            Return False
        End If

        ' Check for invalid passWord.
        ' passWord must not be set to nothing and must be between one and 50 characters.
        If (passWord Is Nothing) Then
            System.Diagnostics.Trace.WriteLine("[ValidateUser] Input validation of passWord failed.")
            Return False
        End If
        If ((passWord.Length = 0) Or (passWord.Length > 50)) Then
            System.Diagnostics.Trace.WriteLine("[ValidateUser] Input validation of passWord failed.")
            Return False
        End If

        Try
            drUser = LoginUser(userName)

            If drUser IsNot Nothing Then
                lookupPassword = drUser.Item("cPWD")
            End If
        Catch ex As Exception
            ' Add error handling here for debugging.
            ' This error message should not be sent back to the caller.
            System.Diagnostics.Trace.WriteLine("[ValidateUser] Exception " & ex.Message)
        End Try

        ' If no password found, return false.
        If (lookupPassword Is Nothing) Then
            ' You could write failed login attempts here to the event log for additional security.
            'Session("LoginError") = "Username not found"
            Return False
        End If
        ' Compare lookupPassword and input passWord by using a case-sensitive comparison.
        Dim cHashPassword As String = FormsAuthentication.HashPasswordForStoringInConfigFile(passWord, "MD5")
        If (String.Compare(lookupPassword, cHashPassword, False) = 0) Then
            objUser = New User(drUser)
            Return True
        Else
            'Session("LoginError") = "Incorrect Password"
            objUser = Nothing
            Return False
        End If

    End Function

    Public Function LoginUser(pstrEmail As String) As DataRow
        Dim cmd As New SqlCommand("[Admin].usp_Login", (New Connection).NewCnn)
        cmd.Parameters.AddWithValue("@cEmail", pstrEmail)
        cmd.CommandType = CommandType.StoredProcedure
        Dim dt As New DataTable
        Dim da As New SqlDataAdapter(cmd)
        Try
            cmd.Connection.Open()
            da.Fill(dt)
        Catch ex As Exception
        Finally
            cmd.Connection.Close()
        End Try
        If dt.Rows.Count = 1 Then
            Return dt.Rows(0)
        Else
            Return Nothing
        End If
    End Function
#End Region

End Module
