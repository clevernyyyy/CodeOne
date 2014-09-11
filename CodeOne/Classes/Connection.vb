Imports System.Data.SqlClient
Public Class Connection
    Dim cnnStr As String = "server = 184.168.47.13; database = codelogically_survey; UID = codeOne; pwd = AC3sFa32;"
    Dim cnnLogin As String = "server = 184.168.47.21; database = codinglogical_login; UID = loginlogically; pwd = tj7B3J7kAC3sFa32;"
    Public ReadOnly Property Str As String
        Get
            Return cnnStr
        End Get
    End Property
    Public ReadOnly Property NewCnn As SqlConnection
        Get
            Return New SqlConnection(cnnStr)
        End Get
    End Property
    Public ReadOnly Property NewCnnLogin As SqlConnection
        Get
            Return New SqlConnection(cnnLogin)
        End Get
    End Property
    Public Sub New(cnnStr As String)
        Me.cnnStr = cnnStr
    End Sub
    Public Sub New()

    End Sub
End Class
