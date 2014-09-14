Imports System.Data.SqlClient

Public Class Transactions
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            'LoadTransactions(Session("Account"))
            LoadTransactions(2)
        End If
    End Sub

#Region "Data Retrieval"

    Private Sub LoadTransactions(ByVal nAccountNum, Optional ByVal lSetFocusToTop = False)
        Dim dt As DataTable = Nothing

        Dim cmd = SqlCommand("Data.usp_Get_AccountDetail")

        With cmd.Parameters
            .AddWithValue("@nAccountNum", nAccountNum)
        End With
        dt = FillDataTable(cmd)

        dvgPack.DataSource = dt
        dvgPack.DataBind()

        If Session("dtSearch") IsNot Nothing Then
            Session("dtSearch").Clear()
        End If
        Session("dtSearch") = dt

        If lSetFocusToTop Then
            Dim pageList As DropDownList = CType(dvgPack.TopPagerRow.FindControl("dgvPackDDL"), DropDownList)
            If Not pageList Is Nothing Then
                ScriptManager.GetCurrent(Page).SetFocus(pageList.ClientID)
            End If
        End If
    End Sub

#End Region

#Region "Search Grid Stuff"
    Private Sub dvgPack_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles dvgPack.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Height = 25
            Dim d As DateTime = e.Row.Cells(1).Text
            e.Row.Cells(1).Text = d.ToShortDateString
            ''This code allows the gridview to be selectable
            e.Row.Attributes.Add("onmouseover", "this.style.cursor='pointer'; this.style.backgroundColor='#333';")
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

    Protected Sub dgvPackDDL_SelectedIndexChanged(ByVal pageList As DropDownList, ByVal e As EventArgs)
        ' Set the PageIndex property to display that page selected by the user.
        dvgPack.PageIndex = pageList.SelectedIndex
        LoadTransactions(Session("Account"), True)
    End Sub

    Protected Sub dgvPackPrev_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' Set the PageIndex property to display that page selected by the user.
        dvgPack.PageIndex -= 1
        LoadTransactions(Session("Account"), True)
    End Sub

    Protected Sub dgvPackNext_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' Set the PageIndex property to display that page selected by the user.

        dvgPack.PageIndex += 1
        LoadTransactions(Session("Account"), True)
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
End Class