Imports System.Data
Imports System.Text
Imports System.Web.Services
Imports System.Data.SqlClient
Imports System.Configuration
Public Class BudgetTargets
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Dim objUser As User = HttpContext.Current.Session("User")
        'hfUserId.Value = objUser.UserID
        hfUserId.Value = 4
    End Sub
    <WebMethod()> _
    Public Shared Function GetChart(nUserId As String) As String
        Dim ds1 As DataSet = GetPopularTargetComps(CInt(nUserId))
        Dim ds As DataSet = CompareBudgetTargetData(ds1)
        Dim sb As New StringBuilder
        Dim lstCategories As List(Of String)
        Dim cats = From dr As DataRow In ds.Tables(0).Rows
                   Select CStr(dr.Item("cCategory"))

        lstCategories = cats.ToList()
        sb.Append("labels: [")
        Dim cCats As String = ""
        For Each strcat As String In lstCategories
            cCats &= "'" & strcat & "',"
        Next
        sb.Append(cCats.Trim(",") & "],datasets:[")
        For Each dt As DataTable In ds.Tables
            sb.Append("{")
            System.Threading.Thread.Sleep(50)
            Dim color As String = String.Format("{0:X3},{0:X3},{0:X3}", New Random().Next(0, 256))
            sb.Append(String.Format("label:'{0}',fillColor:'rgba({1},0.2)',strokeColor:'rgba({1},1)',pointColor:'rgba({1},1)',pointStrokeColor:'#fff',pointHighlightfill:'#fff',pointHighlightStroke:'rgba({1},1)',data:[", dt.TableName, color))
            Dim cData As String = ""
            For Each dr As DataRow In dt.Rows
                cData &= dr.Item("nExpenses") & ", "
            Next
            cData = cData.Trim(",")
            sb.Append(cData & "]")
            sb.Append("},")
        Next
        If sb.Length > 1 Then
            sb = sb.Remove(sb.Length - 1, 1)
        End If
        sb.Append("]")
        Return sb.ToString
    End Function

End Class