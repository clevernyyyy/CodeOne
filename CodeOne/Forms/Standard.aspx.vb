Imports System.Data.SqlClient
Public Class Standard
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ''''' The entire purpose of this page is for demo use only '''''
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        If Not IsPostBack Then
            LoadCategories()
            LoadTransactions()
        End If
    End Sub

    Private Sub LoadTransactions()
        Dim dt As DataTable = Nothing

        Dim cmd = SqlCommand("Data.usp_Get_AccountDetail")

        'Hard Coded for Demo
        cmd.Parameters.AddWithValue("@nAccountNum", 2)

        dt = FillDataTable(cmd)

        rptTrans.DataSource = dt
        rptTrans.DataBind()
    End Sub

    Private Sub rptTrans_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptTrans.ItemDataBound
        Dim dDate As Date = e.Item.DataItem.Item("dPostDt")
        Dim intAmount As Integer = e.Item.DataItem.Item("nTranAmt")
        Dim strDetail As String = e.Item.DataItem.Item("cTransDesc")
        Dim strCat As String = e.Item.DataItem.Item("cCategory")
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim ctrl As Transaction = DirectCast(e.Item.FindControl("ctrlTransaction"), Transaction)
            ctrl.PostDate = dDate
            ctrl.Amount = intAmount
            ctrl.Detail = strDetail
            ctrl.Category = strCat
        End If
    End Sub

#Region "Data Retrieval"
    Private Sub LoadCategories()
        Dim dt As DataTable = Nothing

        Dim cmd = SqlCommand("Data.usp_Get_Categories")

        dt = FillDataTable(cmd)

        rptCategories.DataSource = dt
        rptCategories.DataBind()
    End Sub

    Private Sub rptCategories_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptCategories.ItemDataBound
        Dim strCategory As String = e.Item.DataItem.Item("cCategory")
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim ctrl As Category = DirectCast(e.Item.FindControl("ctrlCategory"), Category)
            ctrl.Category = strCategory
        End If
    End Sub
#End Region

End Class