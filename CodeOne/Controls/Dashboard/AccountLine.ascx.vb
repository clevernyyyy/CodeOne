Public Class AccountLine
    Inherits System.Web.UI.UserControl
    Private Class TransactionQuickInfo
        Public Property TransDesc As String
        Public Property TransDate As Date
        Public Property TransAmount As Decimal
        Public Sub New(cDesc As String, dDate As Date, nAmt As Decimal)
            TransDesc = cDesc
            TransDate = dDate
            TransAmount = nAmt
        End Sub
    End Class
    Dim Transactions As List(Of TransactionQuickInfo)
    Dim dtTransactions As DataTable
    Public Property AccountName As String
        Get
            Return lnkAccountName.text
        End Get
        Set(value As String)
            lnkAccountName.text = value
        End Set
    End Property
    Public Property AccountNumber As String
        Get
            Return hfAccountNum.Value
        End Get
        Set(value As String)
            hfAccountNum.Value = value
        End Set
    End Property
    Public Property CurrentBalance As Decimal
        Get
            Dim nBalance As Decimal
            Decimal.TryParse(Replace(lblBalance.Text, "$", ""), nBalance)
            Return nBalance
        End Get
        Set(value As Decimal)
            lblBalance.Text = "$" + value.ToString
        End Set
    End Property
    Public WriteOnly Property ShowPaymentDue As Boolean
        Set(value As Boolean)
            divDue.Visible = value

        End Set
    End Property
    Public Property PaymentDueDate As Date
        Get
            Dim dPayment As Date
            Date.TryParse(lblDueDate.Text, dPayment)
            Return dPayment
        End Get
        Set(value As Date)
            lblDueDate.Text = value.ToShortDateString
        End Set
    End Property
    Public Property PaymentDueAmount As Decimal
        Get
            Dim nDue As Decimal
            Decimal.TryParse(Replace(lblDueAmount.Text, "$", ""), nDue)
            Return nDue
        End Get
        Set(value As Decimal)
            lblDueAmount.Text = "$" + value.ToString
        End Set
    End Property
    Public WriteOnly Property ShowLastPayment As Boolean
        Set(value As Boolean)
            divLast.Visible = value
        End Set
    End Property
    Public Property LastPaymentDate As Date
        Get
            Dim dPayment As Date
            Date.TryParse(lblLastPaymentDate.Text, dPayment)
            Return dPayment
        End Get
        Set(value As Date)
            lblLastPaymentDate.Text = value.ToShortDateString
        End Set
    End Property
    Public Property LastPaymentAmount As Decimal
        Get
            Dim nDue As Decimal
            Decimal.TryParse(Replace(lblLastPaymentAmount.Text, "$", ""), nDue)
            Return nDue
        End Get
        Set(value As Decimal)
            lblLastPaymentAmount.Text = "$" + value.ToString
        End Set
    End Property
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddRegJS(lnkViewTen)
    End Sub

    Private Sub lnkAccountName_Click(sender As Object, e As EventArgs) Handles lnkAccountName.Click
        Session("Account") = hfAccountNum.Value
        Response.Redirect("~/Forms/Menu/Transactions.aspx")
    End Sub

    Public Sub SetUpTransactions(ParamArray drTransactions() As DataRow)
        Transactions = New List(Of TransactionQuickInfo)
        For Each drTrans As DataRow In drTransactions
            Dim objTrans As New TransactionQuickInfo(drTrans.Item("cTransDesc"), CDate(drTrans.Item("dPostDt")), CDec(drTrans.Item("nTransAmt")))
            Transactions.Add(objTrans)
        Next
        LoadTransactions()
    End Sub



#Region "Last 10 Transactions"
#Region "Data Retrieval"

    Private Sub LoadTransactions()
        Dim dt As New DataTable
        LibraryFunctions.AddColumnsToDataTable(dt, "TransDesc", "TransDate", "TransAmount")
        LibraryFunctions.ObejctToDataTable(dt, Transactions)
        dvgPack.DataSource = dt
        dvgPack.DataBind()

    End Sub

    Private Sub LoadCategories()
        Dim dt As DataTable = Nothing

        Dim cmd = SqlCommand("Data.usp_Get_Categories")

        dt = FillDataTable(cmd)

        'rptCategories.DataSource = dt
        'rptCategories.DataBind()
    End Sub
#End Region
#Region "Search Grid Stuff"
    Private Sub dvgPack_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles dvgPack.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Height = 25
            Dim d As DateTime = e.Row.Cells(0).Text
            e.Row.Cells(0).Text = d.ToShortDateString
            ''This code allows the gridview to be selectable
            e.Row.Attributes.Add("onmouseover", "this.style.cursor='pointer'; this.style.backgroundColor='#E0EECA';")
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='';")
            'e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(sender, "Select$" + e.Row.RowIndex.ToString))
        End If
    End Sub

    Protected Sub dgvPack_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs)
        Dim dv As New DataView(Session("dtSearch"))
        If Session("strSortedBy") IsNot Nothing AndAlso Session("strSortedBy").contains("ASC") Then
            dv.Sort = e.SortExpression & " DESC"
        ElseIf Session("strSortedBy") IsNot Nothing AndAlso Session("strSortedBy").contains("DESC") Then
            dv.Sort = e.SortExpression & " ASC"
        Else
            dv.Sort = e.SortExpression & " ASC"
        End If
        HttpContext.Current.Session("strSortedBy") = dv.Sort.ToString
        dvgPack.DataSource = dv
        dvgPack.DataBind()
    End Sub
#End Region
#End Region

#Region "JavaScript"
    Private Sub AddRegJS(ByVal lb As LinkButton)
        Dim strJava As String = ""

        strJava = "javascript:showGrid;"
        'lb.Attributes.Add("onclick", strJava)
    End Sub
#End Region
End Class