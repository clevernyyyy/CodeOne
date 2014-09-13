Public Class IncomeExpense
    Inherits System.Web.UI.UserControl
#Region "Public Properties"
    Public Property EI As String
        Get
            Return hfType.Value
        End Get
        Set(value As String)
            hfType.Value = value
        End Set
    End Property
    Public Property BudgetID As Integer
        Get
            Return hfBudgetID.Value
        End Get
        Set(value As Integer)
            hfBudgetID.Value = value
        End Set
    End Property
    Public Property IEID As Nullable(Of Integer)
        Get
            Return hfID.Value
        End Get
        Set(value As Nullable(Of Integer))
            hfID.Value = value
        End Set
    End Property
    Public Property Type As String
        Get
            Return ddlType.SelectedValue
        End Get
        Set(value As String)
            ddlType.SelectedValue = value
        End Set
    End Property
    Public Property Name As String
        Get
            Return txtName.Text
        End Get
        Set(value As String)
            txtName.Text = value
        End Set
    End Property
    Public Property Frequency As Integer
        Get
            Return ddlFrequency.SelectedValue
        End Get
        Set(value As Integer)
            ddlFrequency.SelectedValue = value
        End Set
    End Property
    Public Property Amount As Integer
        Get
            Return txtAmount.Text
        End Get
        Set(value As Integer)
            txtAmount.Text = value
        End Set
    End Property
    Public Property dStart As Date
        Get
            Return dtpStart.SelectedDate
        End Get
        Set(value As Date)
            dtpStart.SelectedDate = value
        End Set
    End Property
    Public Property dEnd As Date
        Get
            Return dtpEnd.SelectedDate
        End Get
        Set(value As Date)
            dtpEnd.SelectedDate = value
        End Set
    End Property
#End Region
    Dim dtTypes As DataTable
    Dim dtFrequency As DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            dtTypes = FillDataTable("Budget.usp_GetTypes", (New Connection).NewCnn, "@cType", Type)
            ddlType.DataSource = dtTypes
            ddlType.DataBind()
            dtFrequency = FillDataTable("Budget.usp_GetFrequencies", (New Connection).NewCnn)
            ddlFrequency.DataSource = dtFrequency
            ddlFrequency.DataTextField = "cFrequency"
            ddlFrequency.DataValueField = "nFrequency"
            ddlFrequency.DataBind()
        End If
    End Sub
    Public Sub SaveToDataRow(ByRef dt As DataTable)
        Dim dr As DataRow = dt.NewRow
        Dim cIncomeExpense As String
        If EI = "I" Then cIncomeExpense = "Income" Else cIncomeExpense = "Expense"
        dr.Item("nBudgetID") = hfBudgetID.Value
        dr.Item("n" & cIncomeExpense & "ID") = IEID
        dr.Item("c" & cIncomeExpense & "Type") = Type
        dr.Item("c" & cIncomeExpense & "Name") = Name
        dr.Item("n" & cIncomeExpense & "Frequency") = Frequency
        dr.Item("n" & cIncomeExpense & "Amount") = Amount
        dr.Item("d" & cIncomeExpense & "Start") = dStart
        dr.Item("d" & cIncomeExpense & "End") = dEnd
        dt.Rows.Add(dr)
    End Sub

End Class