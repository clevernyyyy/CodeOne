Public Class Category
    Inherits System.Web.UI.UserControl

    Public Property Category As String
        Get
            Return lblCategoryText.Text
        End Get
        Set(value As String)
            lblCategoryText.Text = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

End Class