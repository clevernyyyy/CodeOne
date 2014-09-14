Public Class PieGraph
    Inherits System.Web.UI.UserControl

    Public Property HasGraph As Boolean
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Dim dicGraphs As New System.Collections.ObjectModel.Collection(Of KeyValuePair(Of String, Integer))

        'dicGraphs.Add(New KeyValuePair(Of String, Integer)("Account", 1))
        'dicGraphs.Add(New KeyValuePair(Of String, Integer)("Account", 32))
        'dicGraphs.Add(New KeyValuePair(Of String, Integer)("User", 5))
        'rptGraphs.DataSource = dicGraphs
        'rptGraphs.DataBind()
    End Sub
    Public Sub SetUpGraphData(collGraphs As System.Collections.ObjectModel.Collection(Of KeyValuePair(Of String, Integer)))
        Dim ds As New DataSet
        HasGraph = False
        For Each kvp As KeyValuePair(Of String, Integer) In collGraphs
            Dim cValueColumn As String, cTextColumn As String, cProcName As String
            Dim strType As String = kvp.Key
            Dim nID As Integer = kvp.Value
            If strType = "Account" Then
                cTextColumn = "cTransDesc"
                cValueColumn = "nSpent"
                cProcName = "Data.SpendingGraphs"
            ElseIf strType = "User" Then
                cTextColumn = "cDbCrCd"
                cValueColumn = "nTotal"
                cProcName = "Data.IncomeVersusExpenses"
            End If
            Dim dt As DataTable = FillDataTable(cProcName, (New Connection).NewCnn, IIf(strType = "User", "@nUserID", "@nAccountNum"), nID.ToString)
            If dt.Rows.Count > 0 Then
                ds.Tables.Add(dt)
            End If
        Next
        If ds.Tables.Count > 0 Then
            HasGraph = True
            rptGraphs.DataSource = collGraphs
            rptGraphs.DataBind()

        End If

    End Sub

    Private Sub AddJavaScript()
        'ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "Account #" & Me.AccountNum.ToString, "javascript:LoadChart(" & Me.AccountNum.ToString & ");", True)

    End Sub

    Private Sub rptGraphs_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptGraphs.ItemDataBound
        Dim oItem As RepeaterItem = e.Item
        If oItem.ItemType = ListItemType.AlternatingItem Or oItem.ItemType = ListItemType.Item Then
            Dim kvp As KeyValuePair(Of String, Integer) = oItem.DataItem
            Dim hfAccountNum As HiddenField = DirectCast(oItem.FindControl("hfPieAccountNum"), HiddenField)
            Dim hfGraphCat As HiddenField = oItem.FindControl("hfGraphCat")
            Dim dvChart As HtmlGenericControl = oItem.FindControl("dvChart")
            Dim dvLegend As HtmlGenericControl = oItem.FindControl("dvLegend")
            hfAccountNum.Value = kvp.Value
            hfGraphCat.Value = kvp.Key
            dvChart.ID &= "_" & kvp.Value
            dvLegend.ID &= "_" & kvp.Value
        End If
    End Sub
End Class