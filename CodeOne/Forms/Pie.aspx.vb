Imports System.Data
Imports System.Text
Imports System.Web.Services
Imports System.Data.SqlClient
Imports System.Configuration
Public Class Pie
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim collGraphs As System.Collections.ObjectModel.Collection(Of KeyValuePair(Of String, Integer)) = Session("GraphData")
            ctrlPie.SetUpGraphData(collGraphs)
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

End Class