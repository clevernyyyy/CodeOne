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
        LoadBudgetID()
        If ddlCategories.SelectedIndex < 0 Then
            LoadIncomes()
            LoadExpenses()
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
        Dim cCategory As String = e.Item.DataItem.Item("cCategory")
        Dim intAmount As Integer = e.Item.DataItem.Item("nAmount")
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim ctrl As MonthlyExpense = DirectCast(e.Item.FindControl("ctrlMonthlyExpense"), MonthlyExpense)
            ctrl.Category = cCategory
            ctrl.Amount = intAmount
        End If
    End Sub
    Private Sub SaveMonthlyExpense()
        Dim dt As DataTable = Session("MonthlyExpense")
        dt.Clear()
        For Each ri As RepeaterItem In rptMonthlyExpenses.Items
            If ri.ItemType = ListItemType.AlternatingItem Or ri.ItemType = ListItemType.Item Then
                Dim ctrl As MonthlyExpense = ri.FindControl("ctrlMonthlyExpense")
                If ctrl.Amount <> 0 Then
                    Dim dr As DataRow = dt.NewRow
                    dr.Item("nBudgetID") = BudgetID
                    dr.Item("cCategory") = ctrl.Category
                    dr.Item("nAmount") = ctrl.Amount
                    dt.Rows.Add(dr)
                End If
            End If
        Next
        Dim cmd = SqlCommand("Budget.usp_BulkUpsert_MonthlyExpenses")
        cmd.Parameters.AddWithValue("@nBudgetID", BudgetID)
        Dim tvp As SqlClient.SqlParameter = cmd.Parameters.AddWithValue("@Expenses", dt)
        tvp.SqlDbType = SqlDbType.Structured
        tvp.TypeName = "Budget.MonthlyExpenses_Type"
        ExecNonQuery(cmd)
    End Sub
    Private Sub LoadMonthlyExpense()
        Dim dt As DataTable = FillDataTable("Budget.usp_Get_MonthlyExpenses", (New Connection).NewCnn, "@nBudgetID", BudgetID.ToString, "MonthlyExpense")
        Session(dt.TableName) = dt
        SetRepeater(rptMonthlyExpenses, dt)
    End Sub
    Private Sub LoadIncomes()
        Dim dt As DataTable = FillDataTable("Budget.usp_Get_Income", (New Connection).NewCnn, "@nBudgetID", BudgetID.ToString, "dtIncomes")
        Session(dt.TableName) = dt
        dt.Rows.Add(dt.NewRow)
        SetRepeater(rptIncomes, dt)
    End Sub
    Private Sub AddNewIncome(sender As Object, e As System.EventArgs) Handles btnAddIncome.click
        Dim dtIncomes As DataTable = Session("dtIncomes")
        dtIncomes.Clear()
        For Each ri As RepeaterItem In rptIncomes.Items
            If ri.ItemType = ListItemType.AlternatingItem Or ri.ItemType = ListItemType.Item Then
                Dim ctrlIncome As IncomeExpense = ri.FindControl("ctrlIncome")
                Dim dr As DataRow = dtIncomes.NewRow
                dr.Item("nBudgetId") = BudgetID
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
        If dtIncomes.Rows.Count > 0 Then SaveIncomes()
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
    Private Sub LoadExpenses()
        Dim dt As DataTable = FillDataTable("Budget.usp_Get_Expenses", (New Connection).NewCnn, "@nBudgetID", BudgetID.ToString, "dtExpenses")
        Session(dt.TableName) = dt
        AddNewExpense(Nothing, Nothing)
    End Sub
    Private Sub AddNewExpense(sender As Object, e As System.EventArgs) Handles btnAddExpense.Click
        Dim dtExpenses As DataTable = Session("dtExpenses")
        dtExpenses.Clear()
        For Each ri As RepeaterItem In rptExpenses.Items
            If ri.ItemType = ListItemType.AlternatingItem Or ri.ItemType = ListItemType.Item Then
                Dim ctrlExpense As IncomeExpense = ri.FindControl("ctrlExpense")
                Dim dr As DataRow = dtExpenses.NewRow
                dr.Item("nBudgetId") = BudgetID
                dr.Item("nExpenseID") = ctrlExpense.IEID
                dr.Item("cExpenseType") = ctrlExpense.Type
                dr.Item("cExpenseName") = ctrlExpense.Name
                dr.Item("nExpenseFrequency") = ctrlExpense.Frequency
                dr.Item("nExpenseAmount") = ctrlExpense.Amount
                dr.Item("dExpenseStart") = ctrlExpense.dStart
                dr.Item("dExpenseEnd") = ctrlExpense.dEnd
                dtExpenses.Rows.Add(dr)
            End If
        Next
        If dtExpenses.Rows.Count > 0 Then SaveExpenses()
        Dim drBlank As DataRow = dtExpenses.NewRow
        dtExpenses.Rows.Add(drBlank)
        SetRepeater(rptExpenses, dtExpenses)
    End Sub
    Private Sub rptExpenses_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptExpenses.ItemDataBound
        Dim oItem As RepeaterItem = e.Item
        If oItem.ItemType = ListItemType.AlternatingItem Or oItem.ItemType = ListItemType.Item Then
            Dim dr As DataRowView = oItem.DataItem
            If Not IsDBNull(dr.Item("nBudgetID")) Then
                Dim ctrlExpense As IncomeExpense = oItem.FindControl("ctrlExpense")
                ctrlExpense.BudgetID = BudgetID
                ctrlExpense.IEID = dr.Item("nExpenseID")
                ctrlExpense.Type = dr.Item("cExpenseType")
                ctrlExpense.Name = dr.Item("cExpenseName")
                ctrlExpense.Frequency = dr.Item("nExpenseFrequency")
                ctrlExpense.Amount = dr.Item("nExpenseAmount")
                ctrlExpense.dStart = dr.Item("dExpenseStart")
                ctrlExpense.dEnd = dr.Item("dExpenseEnd")
            End If
        End If
    End Sub
    Private Sub SetRepeater(rpt As Repeater, dsSource As DataTable)
        Session(dsSource.TableName) = dsSource
        rpt.DataSource = Nothing
        rpt.DataSource = dsSource
        rpt.DataBind()
    End Sub
    Private Sub SaveIncomes()
        Dim dt As DataTable = Session("dtIncomes")
        Dim cmd = SqlCommand("Budget.usp_BulkUpsert_Income")
        cmd.Parameters.AddWithValue("@nBudgetID", BudgetID)
        Dim tvp As SqlClient.SqlParameter = cmd.Parameters.AddWithValue("@Incomes", dt)
        tvp.SqlDbType = SqlDbType.Structured
        tvp.TypeName = "Budget.Incomes_Type"
        ExecNonQuery(cmd)
    End Sub
    Private Sub SaveExpenses()
        Dim dt As DataTable = Session("dtExpenses")
        Dim cmd = SqlCommand("Budget.usp_BulkUpsert_Expense")
        cmd.Parameters.AddWithValue("@nBudgetID", BudgetID)
        Dim tvp As SqlClient.SqlParameter = cmd.Parameters.AddWithValue("@Expenses", dt)
        tvp.SqlDbType = SqlDbType.Structured
        tvp.TypeName = "Budget.Expenses_Type"

        cmd.ExecuteNonQuery()

    End Sub
End Class