Public Class MonthlyExpense
    Inherits System.Web.UI.UserControl

    Public Property Category As String
        Get
            Return lblCategory.Text
        End Get
        Set(value As String)
            lblCategory.Text = value
        End Set
    End Property

    Public Property Amount As Integer
        Get
            Return txtAmount.Text
        End Get
        Set(value As Integer)
            txtAmount.Text = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

End Class