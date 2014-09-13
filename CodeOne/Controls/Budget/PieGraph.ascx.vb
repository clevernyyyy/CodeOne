Public Class PieGraph
    Inherits System.Web.UI.UserControl


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Dim dicGraphs As New System.Collections.ObjectModel.Collection(Of KeyValuePair(Of String, Integer))

        'dicGraphs.Add(New KeyValuePair(Of String, Integer)("Account", 1))
        'dicGraphs.Add(New KeyValuePair(Of String, Integer)("Account", 32))
        'dicGraphs.Add(New KeyValuePair(Of String, Integer)("User", 5))
        'rptGraphs.DataSource = dicGraphs
        'rptGraphs.DataBind()
    End Sub
    Public Sub SetUpGraphData(collGraphs As System.Collections.ObjectModel.Collection(Of KeyValuePair(Of String, Integer)))

        rptGraphs.DataSource = collGraphs
        rptGraphs.DataBind()
    End Sub
    Private Sub AddJavaScript()
        'ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "Account #" & Me.AccountNum.ToString, "javascript:LoadChart(" & Me.AccountNum.ToString & ");", True)

    End Sub

    Private Sub rptGraphs_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptGraphs.ItemDataBound
        Dim oItem As RepeaterItem = e.Item
        If oItem.ItemType = ListItemType.AlternatingItem Or oItem.ItemType = ListItemType.Item Then
            Dim kvp As KeyValuePair(Of String, Integer) = oItem.DataItem
            Dim hfAccountNum As HiddenField = DirectCast(oItem.FindControl("hfAccountNum"), HiddenField)
            Dim hfGraphCat As HiddenField = oItem.FindControl("hfGraphCat")
            hfAccountNum.Value = kvp.Value
            hfGraphCat.value = kvp.Key
        End If
    End Sub
End Class