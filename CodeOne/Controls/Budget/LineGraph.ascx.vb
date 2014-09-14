Public Class LineGraph
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objUser As User = HttpContext.Current.Session("User")
        hfLineGraphUserId.Value = objUser.UserID
    End Sub

End Class