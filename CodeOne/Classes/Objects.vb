#Region "Packages"
<Serializable()> _
Public Class Package

    Sub New()

    End Sub
End Class

<Serializable()> _
Public Class UserPackage
    Public UserInformation As New UserInformation
End Class

#Region "UserInformation"
<Serializable()> _
Public Class UserInformation
    Public cAuthorization As String
    Public nUserID As Integer
    Public iUserIdentifier As String
    Public cUserName As String

End Class
#End Region

#Region "Error"
<Serializable()> _
Public Class ErrorGUID
    Public cErrorGUID As String
End Class

#End Region

#End Region
