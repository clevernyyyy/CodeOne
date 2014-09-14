Public Class Categories
    Inherits System.Web.UI.UserControl
    Public Property Category As String
        Get
            Return lblCategory.Text
        End Get
        Set(value As String)
            lblCategory.Text = value
        End Set
    End Property
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadTransactions()
    End Sub

    Private Sub LoadTransactions()
        Dim dt As DataTable = Nothing

        Dim cmd = SqlCommand("Data.usp_Get_CategoryTransactions")
        cmd.Parameters.AddWithValue("@nAccountNum", Session("Account"))
        cmd.Parameters.AddWithValue("@cCategory", Category)

        dt = FillDataTable(cmd)

        rptTrans.DataSource = dt
        rptTrans.DataBind()
    End Sub

    Private Sub rptTrans_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptTrans.ItemDataBound
        Dim dDate As Date = e.Item.DataItem.Item("dPostDt")
        Dim intAmount As Integer = e.Item.DataItem.Item("nTranAmt")
        Dim strDetail As String = e.Item.DataItem.Item("cTransDesc")
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim ctrl As Transaction = DirectCast(e.Item.FindControl("ctrlTransaction"), Transaction)
            ctrl.PostDate = dDate
            ctrl.Amount = intAmount
            ctrl.Detail = strDetail
        End If
    End Sub
End Class