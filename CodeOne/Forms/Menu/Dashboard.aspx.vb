Public Class Dashboard
    Inherits System.Web.UI.Page

    Dim nUserID As Integer = 4
    Dim dtQL As DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim dt As DataTable = FillDataTable("Data.usp_Get_UserAccounts", (New Connection).NewCnn, "@nUserID", nUserID.ToString)
            dtQL = FillDataTable("Data.usp_Get_Accounts_QuickLook", (New Connection).NewCnn, "@nUserID", nUserID.ToString)
            FillRepeater(repDeposits, dt, "Checking", "Savings")
            FillRepeater(repInvestments, dt, "CD", "IRA")
            FillRepeater(repCredits, dt, "CREDIT CARD")
            FillRepeater(repLoans, dt, "Auto Loan", "Mortgage")

        End If
    End Sub
    Private Sub FillRepeater(rptRepeater As Repeater, dtAccounts As DataTable, ParamArray AccountTypes() As String)
        Dim lsAccounts As New List(Of DataRow)
        Dim dr = From d As DataRow In dtAccounts.Rows
                 Where AccountTypes.Contains(d.Item("cAccountName").Trim) Or (AccountTypes.Contains("CC") And d.Item("cAccountName").Trim = "CREDIT CARD")
                 Select d

        lsAccounts = dr.ToList
        rptRepeater.DataSource = lsAccounts
        rptRepeater.DataBind()
    End Sub
    Private Sub repDeposits_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles repDeposits.ItemDataBound, repInvestments.ItemDataBound
        Dim oItem As RepeaterItem = e.Item
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim dr As DataRow = oItem.DataItem
            Dim ctrlAccount As AccountLine = DirectCast(oItem.FindControl("uctrlAccount"), AccountLine)
            ctrlAccount.AccountName = "First National " + dr.Item("cAccountName").ToString().Trim()
            ctrlAccount.AccountNumber = dr.Item("cAccountNum")
            ctrlAccount.CurrentBalance = dr.Item("nAccountBalance")
            Dim drs() As DataRow = dtQL.Select("cAccountNum = '" & dr.Item("cAccountNum") & "'")
            ctrlAccount.SetUpTransactions(drs)
        End If
    End Sub

    Private Sub SendToGraphs()
        Dim dt As DataTable = FillDataTable("Data.usp_Get_UserAccounts", (New Connection).NewCnn, "@nUserID", nUserID.ToString)
        Dim accts = From dr As DataRow In dt.Rows
                    Select dr.Item("cAccountNum")


        Dim collKeys As New System.Collections.ObjectModel.Collection(Of KeyValuePair(Of String, Integer))
        collKeys.Add(New KeyValuePair(Of String, Integer)("User", nUserID))
        For Each acct As String In accts.ToList()
            Dim int As Integer
            Integer.TryParse(acct, int)
            If int > 0 Then
                Dim kvp As New KeyValuePair(Of String, Integer)("Account", int)
                collKeys.Add(kvp)
            End If
        Next
        Session("GraphData") = collKeys
        Response.Redirect("../Pie.aspx")
    End Sub

    Private Sub lnkGraphs_Click(sender As Object, e As EventArgs) Handles lnkGraphs.Click
        SendToGraphs()
    End Sub

    Private Sub repCredits_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles repCredits.ItemDataBound
        Dim oItem As RepeaterItem = e.Item
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim dr As DataRow = oItem.DataItem
            Dim ctrlAccount As AccountLine = DirectCast(oItem.FindControl("uctrlAccount"), AccountLine)
            ctrlAccount.AccountName = dr.Item("cAccountName").ToString().Trim() & " - " & dr.Item("cAccountNum")
            ctrlAccount.AccountNumber = dr.Item("cAccountNum")
            ctrlAccount.CurrentBalance = dr.Item("nAccountBalance")
            ctrlAccount.PaymentDueAmount = dr.Item("nPaymentDue")
            ctrlAccount.PaymentDueDate = dr.Item("dPaymentDue")
            ctrlAccount.LastPaymentAmount = dr.Item("nLastPayment")
            ctrlAccount.LastPaymentDate = dr.Item("dLastPayment")
            Dim drs() As DataRow = dtQL.Select("cAccountNum = '" & dr.Item("cAccountNum") & "'")
            ctrlAccount.SetUpTransactions(drs)
        End If
    End Sub

    Private Sub repLoans_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles repLoans.ItemDataBound
        Dim oItem As RepeaterItem = e.Item
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim dr As DataRow = oItem.DataItem
            Dim ctrlAccount As AccountLine = DirectCast(oItem.FindControl("uctrlAccount"), AccountLine)
            ctrlAccount.AccountName = dr.Item("cAccountName").ToString().Trim()
            ctrlAccount.AccountNumber = dr.Item("cAccountNum")
            ctrlAccount.CurrentBalance = dr.Item("nAccountBalance")
            ctrlAccount.LastPaymentAmount = dr.Item("nLastPayment")
            ctrlAccount.LastPaymentDate = dr.Item("dLastPayment")
            Dim drs() As DataRow = dtQL.Select("cAccountNum = '" & dr.Item("cAccountNum") & "'")
            ctrlAccount.SetUpTransactions(drs)
        End If
    End Sub
    Protected Sub LogOut_Click(sender As Object, e As EventArgs)
        Response.Redirect("~/Default.aspx")
    End Sub

End Class