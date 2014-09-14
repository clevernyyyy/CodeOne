Imports System.Data
Imports System.Text
Imports System.Web.Services
Imports System.Data.SqlClient
Imports System.Configuration
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
#Region "Properties"
    Public Property AccountName As String
        Get
            Return lnkAccountName.Text
        End Get
        Set(value As String)
            lnkAccountName.Text = value
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

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim collGraphs As System.Collections.ObjectModel.Collection(Of KeyValuePair(Of String, Integer)) = Session("GraphData")
            Dim coll As New System.Collections.ObjectModel.Collection(Of KeyValuePair(Of String, Integer))
            Dim lShowViz As Boolean = False
            Dim kvpGraph = From kvps As KeyValuePair(Of String, Integer) In collGraphs
                           Where kvps.Value = hfAccountNum.Value And kvps.Key = "Account"
                           Select kvps

            If kvpGraph.Count > 0 Then
                For Each kvp In kvpGraph.ToList
                    coll.Add(kvp)
                Next
                ctrlPie.SetUpGraphData(coll)
                If ctrlPie.HasGraph Then lShowViz = True
            End If
            If lShowViz = False Then
                divider.Visible = False
                ancViewPie.Visible = False
            End If
        End If

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

    Private Sub LoadTransactions(Optional ByVal lSetFocusToTop = False)
        Dim dt As New DataTable
        'ancViewTen.ID &= hfAccountNum.Value
        ancViewTen.Attributes.Add("onClick", "ShowHideTransactions(this);")
        ancViewPie.Attributes.Add("onClick", "ShowHideGraph(this);")
        LibraryFunctions.AddColumnsToDataTable(dt, "TransDesc", "TransDate", "TransAmount")
        LibraryFunctions.ObejctToDataTable(dt, Transactions)
        dvgPack.DataSource = dt
        dvgPack.DataBind()

        If lSetFocusToTop Then
            Dim pageList As DropDownList = CType(dvgPack.TopPagerRow.FindControl("dgvPackDDL"), DropDownList)
            If Not pageList Is Nothing Then
                ScriptManager.GetCurrent(Page).SetFocus(pageList.ClientID)
            End If
        End If
    End Sub

    'Private Sub LoadCategories()
    '    Dim dt As DataTable = Nothing

    '    Dim cmd = SqlCommand("Data.usp_Get_Categories")

    '    dt = FillDataTable(cmd)

    '    'rptCategories.DataSource = dt
    '    'rptCategories.DataBind()
    'End Sub
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
        ElseIf e.Row.RowType = DataControlRowType.Pager Then
            ' Retrieve the DropDownList and Label controls from the row.
            Dim pageList As DropDownList = CType(e.Row.Cells(0).FindControl("dgvPackDDL"), DropDownList)
            For i As Integer = 0 To dvgPack.PageCount - 1

                ' Create a ListItem object to represent a page.
                Dim pageNumber As Integer = i + 1
                Dim item As ListItem = New ListItem(pageNumber.ToString())

                ' If the ListItem object matches the currently selected
                ' page, flag the ListItem object as being selected. Because
                ' the DropDownList control is recreated each time the pager
                ' row gets created, this will persist the selected item in
                ' the DropDownList control.   
                If i = dvgPack.PageIndex Then

                    item.Selected = True

                End If

                ' Add the ListItem object to the Items collection of the 
                ' DropDownList.
                pageList.Items.Add(item)
            Next

            'Fix up 'out of' label
            Dim lblPages As Label = CType(e.Row.Cells(0).FindControl("lblPages"), Label)
            lblPages.Text = dvgPack.PageCount

            'Hide nav buttons if necessary
            If dvgPack.PageIndex = 0 Then
                Dim btnPrev As Button = CType(e.Row.Cells(0).FindControl("btnPrev"), Button)
                btnPrev.Style.Item("visibility") = "hidden"
            ElseIf dvgPack.PageIndex >= dvgPack.PageCount - 1 Then
                Dim btnNext As Button = CType(e.Row.Cells(0).FindControl("btnNext"), Button)
                btnNext.Style.Item("visibility") = "hidden"
            End If
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
    Protected Sub dgvPackDDL_SelectedIndexChanged(ByVal pageList As DropDownList, ByVal e As EventArgs)
        ' Set the PageIndex property to display that page selected by the user.
        dvgPack.PageIndex = pageList.SelectedIndex
        LoadTransactions(True)
    End Sub

    Protected Sub dgvPackPrev_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' Set the PageIndex property to display that page selected by the user.
        dvgPack.PageIndex -= 1
        LoadTransactions(True)
    End Sub

    Protected Sub dgvPackNext_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' Set the PageIndex property to display that page selected by the user.

        dvgPack.PageIndex += 1
        LoadTransactions(True)
    End Sub
#End Region
#End Region


#Region "Mmmmm Pie"
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
#End Region

End Class