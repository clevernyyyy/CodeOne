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
    Private Property BudgetID As Integer
    Dim MonthlyExpense As List(Of MonthlyExpenseQuickInfo)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If ddlCategories.SelectedIndex < 0 Then
            LoadBudgetID()
            LoadIncomes()
            LoadCategories()
        End If
    End Sub
    Private Sub LoadBudgetID()
        Dim objUser As User = HttpContext.Current.Session("User")
        Dim cmd = SqlCommand("Budget.usp_Get_BudgetID")
        cmd.Parameters.AddWithValue("@nUserID", objUser.UserID)
        BudgetID = ExecScalar(cmd, Enumerations.enmType._Integer)
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
    Private Sub LoadIncomes()
        Dim dt As DataTable = FillDataTable("Budget.usp_Get_Income", (New Connection).NewCnn, "@nBudgetID", BudgetID.ToString, "dtIncomes")
        Session(dt.TableName) = dt
        AddNewIncome(Nothing, Nothing)
    End Sub
    Private Sub AddNewIncome(sender As Object, e As System.EventArgs) Handles btnAddIncome.click
        Dim dtIncomes As DataTable = Session("dtIncomes")
        dtIncomes.Clear()
        For Each ri As RepeaterItem In rptIncomes.Items
            If ri.ItemType = ListItemType.AlternatingItem Or ri.ItemType = ListItemType.Item Then
                Dim ctrlIncome As IncomeExpense = ri.FindControl("ctrlIncome")
                Dim dr As DataRow = dtIncomes.NewRow
                dr.Item("nIncomeID") = ctrlIncome.IEID
                dr.Item("cIncomeType") = ctrlIncome.Type
                dr.Item("cIncomeName") = ctrlIncome.Name
                dr.Item("nIncomeFrequency") = ctrlIncome.Frequency
                dr.Item("nIncomeAmount") = ctrlIncome.Amount
                dr.Item("dIncomeStart") = ctrlIncome.dStart
                dr.Item("dIncomeEnd") = ctrlIncome.dEnd
                dtIncomes.Rows.Add(dr)
            End If
        Next
        Dim drBlank As DataRow = dtIncomes.NewRow
        dtIncomes.Rows.Add(drBlank)
        SetRepeater(rptIncomes, dtIncomes)
    End Sub
    Private Sub rptIncomes_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptIncomes.ItemDataBound
        Dim oItem As RepeaterItem = e.Item
        If oItem.ItemType = ListItemType.AlternatingItem Or oItem.ItemType = ListItemType.Item Then
            Dim dr As DataRowView = oItem.DataItem
            If Not IsDBNull(dr.Item("nBudgetID")) Then
                Dim ctrlIncome As IncomeExpense = oItem.FindControl("ctrlIncome")
                ctrlIncome.BudgetID = BudgetID
                ctrlIncome.IEID = dr.Item("nIncomeID")
                ctrlIncome.Type = dr.Item("cIncomeType")
                ctrlIncome.Name = dr.Item("cIncomeName")
                ctrlIncome.Frequency = dr.Item("nIncomeFrequency")
                ctrlIncome.Amount = dr.Item("nIncomeAmount")
                ctrlIncome.dStart = dr.Item("dIncomeStart")
                ctrlIncome.dEnd = dr.Item("dIncomeEnd")
            End If
        End If
    End Sub
    Private Sub SetRepeater(rpt As Repeater, dsSource As DataTable)
        Session(dsSource.TableName) = dsSource
        rpt.DataSource = Nothing
        rpt.DataSource = dsSource
        rpt.DataBind()
    End Sub
End Class