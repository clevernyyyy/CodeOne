Public Module ObjectDataRetrieval

#Region "Package"
    Public Function GetPackage() As Package
        Dim pkg As Package
        If HttpContext.Current.Session("Package") Is Nothing Then
            pkg = New Package
            HttpContext.Current.Session("Package") = pkg
        End If
        pkg = HttpContext.Current.Session("Package")
        Return pkg
    End Function

    Public Sub ClearPackage()
        HttpContext.Current.Session("Package") = Nothing
    End Sub
#End Region

#Region "UserPackage"
    Public Function GetUserPackage()
        Dim Userpkg As UserPackage
        If HttpContext.Current.Session("UserPackage") Is Nothing Then
            Userpkg = New UserPackage
            HttpContext.Current.Session("UserPackage") = Userpkg
        End If
        Userpkg = HttpContext.Current.Session("UserPackage")
        Return Userpkg
    End Function

    Public Sub ClearUserPackage()
        HttpContext.Current.Session("UserPackage") = Nothing
    End Sub
#End Region

End Module
