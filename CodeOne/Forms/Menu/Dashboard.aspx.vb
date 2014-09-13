Public Class Dashboard
    Inherits System.Web.UI.Page

    Dim nUserID As Integer = 4
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim dt As DataTable = FillDataTable("Data.usp_Get_UserAccounts", (New Connection).NewCnn, "@nUserID", nUserID)
            Dim lsDeposit As New List(Of DataRow)
            Dim dr = From d As DataRow In dt.Rows
                     Where d.Item("cAccountName").trim = "Checking" Or d.Item("cAccountName").trim = "Savings"
                     Select d
            If dr.Count > 0 Then
                lsDeposit = dr.ToList
                depRepeater.DataSource = lsDeposit
                depRepeater.DataBind()
            End If

        End If
    End Sub

    Private Sub depRepeater_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles depRepeater.ItemDataBound
        Dim oItem As RepeaterItem = e.Item
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim dr As DataRow = oItem.DataItem
            Dim ctrlAccount As AccountLine = DirectCast(oItem.FindControl("depAccount"), AccountLine)
            ctrlAccount.AccountName = "First National " + dr.Item("cAccountName").ToString().Trim()
            ctrlAccount.AccountNumber = dr.Item("cAccountNum")
            ctrlAccount.CurrentBalance = dr.Item("nAccountBalance")

        End If
    End Sub
End Class