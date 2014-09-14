Public Class Transaction
    Inherits System.Web.UI.UserControl

    Public Property PostDate As Date
        Get
            Return lblDate.Text
        End Get
        Set(value As Date)
            lblDate.Text = value
        End Set
    End Property

    Public Property Amount As Integer
        Get
            Return lblAmount.Text
        End Get
        Set(value As Integer)
            lblAmount.Text = value
        End Set
    End Property

    Public Property Detail As String
        Get
            Return lblDetail.Text
        End Get
        Set(value As String)
            lblDetail.Text = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

End Class