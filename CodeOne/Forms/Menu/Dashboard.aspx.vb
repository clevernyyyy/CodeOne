Imports System.Data
Imports System.Text
Imports System.Web.Services
Imports System.Data.SqlClient
Imports System.Configuration
Public Class Dashboard
    Inherits System.Web.UI.Page

    Dim nUserID As Integer = 0
    Dim dtQL As DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If HttpContext.Current.Session("User") IsNot Nothing Then
            Dim objUser As User = HttpContext.Current.Session("User")
            nUserID = objUser.UserID
        Else
            Dim objUser As New User("Peter", "Peter", "", "Peter", 0, 4)
            nUserID = objUser.UserID
        End If
        If Not IsPostBack Then
            'This is for the gridview
            If nUserID > 0 Then
                Dim dt As DataTable = FillDataTable("Data.usp_Get_UserAccounts", (New Connection).NewCnn, "@nUserID", nUserID.ToString)
                dtQL = FillDataTable("Data.usp_Get_Accounts_QuickLook", (New Connection).NewCnn, "@nUserID", nUserID.ToString)

                FillRepeater(repDeposits, dt, "Checking", "Savings")
                FillRepeater(repInvestments, dt, "CD", "IRA")
                FillRepeater(repCredits, dt, "CREDIT CARD")
                FillRepeater(repLoans, dt, "Auto Loan", "Mortgage")
                SendToGraphs()
            End If
        End If
    End Sub

#Region "GridViewRegion"
    Private Sub FillRepeater(rptRepeater As Repeater, dtAccounts As DataTable, ParamArray AccountTypes() As String)
        Dim lsAccounts As New List(Of DataRow)
        Dim dr = From d As DataRow In dtAccounts.Rows
                 Where AccountTypes.Contains(d.Item("cAccountName").Trim) Or (AccountTypes.Contains("CC") And d.Item("cAccountName").Trim = "CREDIT CARD")
                 Select d
        If dr.Count > 0 Then
            lsAccounts = dr.ToList
            rptRepeater.DataSource = lsAccounts
            rptRepeater.DataBind()
        Else
            rptRepeater.Visible = False
            'Select Case rptRepeater.ID
            '    Case "repDeposits"
            '    Case "repInvestments"
            '    Case "repCredits"
            '    Case "repLoans"
            'End Select
        End If
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
    Private Sub repCredits_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles repCredits.ItemDataBound
        Dim oItem As RepeaterItem = e.Item
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim dr As DataRow = oItem.DataItem
            Dim ctrlAccount As AccountLine = DirectCast(oItem.FindControl("uctrlAccount"), AccountLine)
            ctrlAccount.AccountName = dr.Item("cAccountName").ToString().Trim() & " - " & dr.Item("cAccountNum")
            ctrlAccount.AccountNumber = dr.Item("nAccountNum")
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
    <WebMethod()> _
    Public Shared Function GetChart(nID As String, strType As String) As String
        Dim cValueColumn As String, cTextColumn As String, cProcName As String
        If strType = "Account" Then
            cTextColumn = "cTransDesc"
            cValueColumn = "nSpent"
            cProcName = "Data.SpendingGraphs"
        ElseIf strType = "User" Then
            cTextColumn = "cDbCrCd"
            cValueColumn = "nTotal"
            cProcName = "Data.IncomeVersusExpenses"
        End If
        Dim dt As DataTable = FillDataTable(cProcName, (New Connection).NewCnn, IIf(strType = "User", "@nUserID", "@nAccountNum"), nID)
        Dim sb As New StringBuilder
        sb.Append("[")
        For Each dr As DataRow In dt.Rows
            sb.Append("{")
            System.Threading.Thread.Sleep(50)
            Dim color As String = String.Format("#{0:X6}", New Random().Next(&H1000000))
            Dim cText As String, cValue As String
            cText = dr.Item(cTextColumn)
            If cText = "C" Then
                cText = dr.Item("cSubProdCd") + " - Income"
            ElseIf cText = "D" Then
                cText = dr.Item("cSubProdCd") + " - Expenses"
            End If
            cValue = dr.Item(cValueColumn)
            sb.Append(String.Format("text :'{0}', value:{1}, color: '{2}'", cText, cValue, color))
            sb.Append("},")
        Next
        If sb.Length > 1 Then
            sb = sb.Remove(sb.Length - 1, 1)
        End If
        sb.Append("]")
        Return sb.ToString
    End Function

    Private Sub SendToGraphs()
        Dim dt As DataTable = FillDataTable("Data.usp_Get_UserAccounts", (New Connection).NewCnn, "@nUserID", nUserID.ToString)
        Dim accts = From dr As DataRow In dt.Rows
                    Select dr.Item("nAccountNum")


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
    End Sub
#End Region

End Class