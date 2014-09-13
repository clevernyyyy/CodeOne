Public Class Dashboard
    Inherits System.Web.UI.Page

    Dim nUserID As Integer = 4
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim dt As DataTable
            Dim lsDeposit As New List(Of DataRow)
            Dim dr = From d As DataRow In dt.Rows
                     Where d.Item("cAccountName") = "Checking" Or d.Item("cAccountName") = "Savings"
                     Select d
            If dr.Count > 0 Then
                lsDeposit = dr.ToList
                depRepeater.DataSource = lsDeposit
                depRepeater.DataBind()
            End If

        End If
    End Sub

    Private Sub depRepeater_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles depRepeater.ItemDataBound
        Dim oItem As DataRow = e.Item.DataItem
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim ctrlAccount As AccountLine = DirectCast(FindControl("depAccount"), AccountLine)
            ctrlAccount.AccountName = oItem.Item("cAccountName")
            ctrlAccount.AccountNumber = oItem.Item("cAccountNum")
            ctrlAccount.CurrentBalance = oItem.Item("nAccountBalance")

        End If
    End Sub
End Class