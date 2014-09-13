Public Class AccountLine
    Inherits System.Web.UI.UserControl
    Private Class TransactionQuickInfo
        Public TransDesc As String
        Public TransDate As Date
        Public TransAmount As Decimal
        Public Sub New(cDesc As String, dDate As Date, nAmt As Decimal)
            TransDesc = cDesc
            TransDate = dDate
            TransAmount = nAmt
        End Sub
    End Class
    Dim Transactions As System.Collections.ObjectModel.Collection(Of TransactionQuickInfo)
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
            Decimal.TryParse(lblBalance.Text, nBalance)
            Return nBalance
        End Get
        Set(value As Decimal)
            lblBalance.Text = value.ToString
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
            lblDueDate.Text = value.ToString
        End Set
    End Property
    Public Property PaymentDueAmount As Decimal
        Get
            Dim nDue As Decimal
            Decimal.TryParse(lblDueAmount.Text, nDue)
            Return nDue
        End Get
        Set(value As Decimal)
            lblDueAmount.Text = value.ToString
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
            lblLastPaymentDate.Text = value.ToString
        End Set
    End Property
    Public Property LastPaymentAmount As Decimal
        Get
            Dim nDue As Decimal
            Decimal.TryParse(lblLastPaymentAmount.Text, nDue)
            Return nDue
        End Get
        Set(value As Decimal)
            lblLastPaymentAmount.Text = value.ToString
        End Set
    End Property
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub lnkAccountName_Click(sender As Object, e As EventArgs) Handles lnkAccountName.Click
        Session("Account") = hfAccountNum.Value
        Response.Redirect("~/Forms/Menu/Transactions.aspx")
    End Sub

    Public Sub SetUpTransactions(ParamArray drTransactions() As DataRow)
        Transactions = New System.Collections.ObjectModel.Collection(Of TransactionQuickInfo)
        For Each drTrans As DataRow In drTransactions
            Dim objTrans As New TransactionQuickInfo(drTrans.Item("cTransDesc"), CDate(drTrans.Item("dPostDt")), CDec(drTrans.Item("nTransAmt")))
            Transactions.Add(objTrans)
        Next
    End Sub

    Private Sub lnkViewTen_Click(sender As Object, e As EventArgs) Handles lnkViewTen.Click
        ShowTransactions()
    End Sub
    Private Sub ShowTransactions()

    End Sub
End Class