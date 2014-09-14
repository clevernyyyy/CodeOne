Public Class Budgets
    Inherits System.Web.UI.Page
    Private Class MonthlyExpenseQuickInfo
        Public Property Category As String
        Public Property Amount As Decimal
        Public Sub New(cCategory As String, nAmt As Decimal)
            Category = cCategory
            Amount = nAmt
        End Sub
    End Class

    Dim MonthlyExpense As List(Of MonthlyExpenseQuickInfo)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If ddlCategories.SelectedIndex < 0 Then
            LoadCategories()
        End If
    End Sub

    Private Sub LoadCategories()
        Dim dt As DataTable = Nothing

        Dim cmd = SqlCommand("Data.usp_Get_Categories")

        dt = FillDataTable(cmd)

        ddlCategories.DataSource = dt
        ddlCategories.DataTextField = "cCategory"
        ddlCategories.DataValueField = "cCategory"
        ddlCategories.DataBind()
    End Sub
    Protected Sub ddlCategories_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlCategories.SelectedIndexChanged
        If Session("MonthlyExpense") IsNot Nothing Then
            MonthlyExpense = Session("MonthlyExpense")
        Else
            MonthlyExpense = New List(Of MonthlyExpenseQuickInfo)
        End If
        'Dim test As DropDownList = CType(sender.FindControl("ddlCategories"), DropDownList)
        Dim objMonthlyExpense As New MonthlyExpenseQuickInfo(ddlCategories.SelectedValue, 0)
        MonthlyExpense.Add(objMonthlyExpense)
        LoadMonthlyExpense()
        panelMonthlyExpense.Update()
    End Sub

    Private Sub rptMonthlyExpenses_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptMonthlyExpenses.ItemDataBound
        Dim cCategory As String = e.Item.DataItem.Item("Category")
        Dim intAmount As Integer = e.Item.DataItem.Item("Amount")
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim ctrl As MonthlyExpense = DirectCast(e.Item.FindControl("ctrlMonthlyExpense"), MonthlyExpense)
            ctrl.Category = cCategory
            ctrl.Amount = intAmount
        End If
    End Sub

    Private Sub LoadMonthlyExpense()
        Dim dt As New DataTable
        LibraryFunctions.AddColumnsToDataTable(dt, "Category", "Amount")
        LibraryFunctions.ObejctToDataTable(dt, MonthlyExpense)
        rptMonthlyExpenses.DataSource = dt
        rptMonthlyExpenses.DataBind()

        Session("MonthlyExpense") = MonthlyExpense
    End Sub
End Class