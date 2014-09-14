Module BudgetTargetFunctions
    Public Function GetPopularTargetComps() As DataSet
        Dim ds As New DataSet
        Dim dtTargets As DataTable = FillDataTable("Budget.usp_Get_TargetData", (New Connection).NewCnn, "TargetData")

        Dim strCategories() As String = {"Restaurants/Bars","Grocery",}
    End Function
End Module
