Imports System.Data
Imports System.Text
Imports System.Web.Services
Imports System.Data.SqlClient
Imports System.Configuration
Public Class Pie
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    <WebMethod()> _
    Public Shared Function GetChart(nAccountNum As String) As String
        Dim dt As DataTable = FillDataTable("Data.SpendingGraphs", (New Connection).NewCnn, "@nAccountNum", nAccountNum)
        Dim sb As New StringBuilder
        sb.Append("[")
        For Each dr As DataRow In dt.Rows
            sb.Append("{")
            System.Threading.Thread.Sleep(50)
            Dim color As String = String.Format("#{0:X6}", New Random().Next(&H1000000))
            sb.Append(String.Format("text :'{0}', value:{1}, color: '{2}'", dr.Item("cTransDesc").trim, dr.Item("nSpent"), color))
            sb.Append("},")
        Next
        If sb.Length > 1 Then
            sb = sb.Remove(sb.Length - 1, 1)
        End If
        sb.Append("]")
        Return sb.ToString
    End Function

End Class