Imports System.Linq
Module BudgetTargetFunctions
    Public Function GetPopularTargetComps(nUserID As Integer) As DataSet
        Dim ds As New DataSet
        Dim dtTargets As DataTable = FillDataTable("Budget.usp_Get_TargetData", (New Connection).NewCnn, "TargetData")
        ds.Tables.Add(dtTargets)
        Dim strCategories() As String = {"Restaurants/Bars", "Grocery", "Gas", "Recreation"}
        For Each StrCat In strCategories
            Dim cmd As New SqlClient.SqlCommand("Data.usp_Get_UserExpenses_ByCategory", (New Connection).NewCnn)
            cmd.Parameters.AddWithValue("@nUserID", nUserID)
            cmd.Parameters.AddWithValue("@cCategory", StrCat)
            Dim dt As DataTable = FillDataTable(cmd, StrCat)
            ds.Tables.Add(dt)
        Next
        Return ds
    End Function
    Public Function CompareBudgetTargetData(ByVal dsBudgetData As DataSet) As DataSet
        Dim dtTarget As DataTable = dsBudgetData.Tables("TargetData").Copy
        Dim dtFamilyInfo As DataTable = dsBudgetData.Tables("FamilyInfo").Copy
        Dim nAdults As Decimal = dtFamilyInfo.Rows(0).Item("nAdults")
        Dim nChildren As Decimal = dtFamilyInfo.Rows(0).Item("nChildren")
        dsBudgetData.Tables.Remove("TargetData")
        dsBudgetData.Tables.Remove("FamilyInfo")
        Dim dtTargetGraph As New DataTable
        dtTargetGraph.Columns.Add("cCategory")
        dtTargetGraph.Columns.Add("nExpenses")
        Dim dtUserGraph As New DataTable
        dtUserGraph.Columns.Add("cCategory")
        dtUserGraph.Columns.Add("nExpenses")

        For Each dt As DataTable In dsBudgetData.Tables

            Dim drTarget As DataRow = dtTarget.NewRow()
            drTarget.Item("cCategory") = dt.TableName
            Dim drUser As DataRow = dtUserGraph.NewRow()
            Dim total As Decimal = (From drs In dt.Rows Select CDec(drs.Item("nExpenses"))).Sum()
            drUser.Item("cCategory") = dt.TableName
            drUser.Item("nExpenses") = total
            Select Case dt.TableName
                Case "Restaurants/Bars"
                    Dim drAdult As DataRow = dtTarget.Select("cRule = 'Dining Adult'")(0)
                    Dim drChild As DataRow = dtTarget.Select("cRule = 'Dining Child'")(0)
                    drTarget.Item("nExpenses") = (drAdult.Item("nExpenses") * nAdults) + (drChild.Item("nExpenses") * nChildren)
                Case "Grocery"
                    Dim drAdult As DataRow = dtTarget.Select("cRule = 'Grocery Adult'")(0)
                    Dim drChild As DataRow = dtTarget.Select("cRule = 'Grocery Child'")(0)
                    drTarget.Item("nExpenses") = (drAdult.Item("nExpenses") * nAdults) + (drChild.Item("nExpenses") * nChildren)
                Case "Gas"
                    Dim drGas As DataRow = dtTarget.Select("cRule = 'Auto/Gas'")(0)
                    drTarget.Item("nExpenses") = drGas.Item("nExpenses") * nAdults
                Case "Recreation"
                    Dim drRecreation As DataRow = dtTarget.Select("cRule = 'Recreation'")(0)
                    drTarget.Item("nExpenses") = (drRecreation.Item("nExpenses") * nAdults) + (drRecreation.Item("nExpenses") * nChildren / 2)
            End Select
            dtTargetGraph.Rows.Add(drTarget)
        Next
        Dim ds As New DataSet
        ds.Tables.Add(dtTargetGraph)
        ds.Tables.Add(dtUserGraph)
        Return ds
    End Function
End Module
