Public Class PieGraph
    Inherits System.Web.UI.UserControl


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim lst As New List(Of String)
        lst.Add("1")
        lst.Add("2")
        lst.Add("32")
        rptGraphs.DataSource = lst
        rptGraphs.DataBind()
    End Sub

    Private Sub AddJavaScript()
        'ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "Account #" & Me.AccountNum.ToString, "javascript:LoadChart(" & Me.AccountNum.ToString & ");", True)

    End Sub

    Private Sub rptGraphs_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptGraphs.ItemDataBound
        Dim oItem As RepeaterItem = e.Item
        If oItem.ItemType = ListItemType.AlternatingItem Or oItem.ItemType = ListItemType.Item Then
            Dim strAccountNum As String = oItem.DataItem
            Dim hfAccountNum As HiddenField = DirectCast(oItem.FindControl("hfAccountNum"), HiddenField)
            Dim lblAccountNum As Label = oItem.FindControl("lblAccountNum")
            lblAccountNum.Text = strAccountNum
            hfAccountNum.Value = strAccountNum
        End If
    End Sub
End Class