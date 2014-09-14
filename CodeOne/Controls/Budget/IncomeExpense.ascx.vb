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
            Dim nIntReturn As Integer
            Integer.TryParse(hfID.Value, nIntReturn)
            Return nIntReturn
        End Get
        Set(value As Nullable(Of Integer))
            hfID.Value = value
        End Set
    End Property
    Dim _Type As String = ""
    Public Property Type As String
        Get
            Return _Type
        End Get
        Set(value As String)
            _Type = value
            If ddlType.DataSource IsNot Nothing Then
                ddlType.SelectedValue = value
            End If
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
    Dim _Frequency As Integer
    Public Property Frequency As Integer
        Get
                Return _Frequency
        End Get
        Set(value As Integer)
            _Frequency = value
            If ddlFrequency.DataSource IsNot Nothing Then
                ddlFrequency.SelectedValue = value
            End If
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
            Return dtpstart.Value
        End Get
        Set(value As Date)
            dtpstart.Value = value
        End Set
    End Property
    Public Property dEnd As Date
        Get
            Return dtpEnd.Value
        End Get
        Set(value As Date)
            dtpEnd.Value = value
        End Set
    End Property
#End Region
    Dim dtTypes As DataTable
    Dim dtFrequency As DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            dtTypes = FillDataTable("Budget.usp_GetTypes", (New Connection).NewCnn, "@cType", EI)
            ddlType.DataSource = dtTypes
            If EI = "I" Then
                ddlType.DataTextField = "cIncomeType"
                ddlType.DataValueField = "nIncomeType"
            Else
                ddlType.DataTextField = "cExpenseType"
                ddlType.DataValueField = "nExpenseType"
            End If
            ddlType.DataBind()
            If _Type <> "" Then ddlType.SelectedValue = _Type


            dtFrequency = FillDataTable("Budget.usp_GetFrequencies", (New Connection).NewCnn)
            ddlFrequency.DataSource = dtFrequency
            ddlFrequency.DataTextField = "cFrequency"
            ddlFrequency.DataValueField = "nFrequency"
            ddlFrequency.DataBind()
            If _Frequency <> 0 Then ddlFrequency.SelectedValue = _Frequency
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