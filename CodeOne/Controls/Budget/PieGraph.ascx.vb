Public Class PieGraph
    Inherits System.Web.UI.UserControl
    Public Property AccountNum As Integer
        Set(value As Integer)
            Me.hfAccountNum.Value = value
        End Set
        Get
            Return Me.hfAccountNum.Value
        End Get
    End Property
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddJavaScript()
    End Sub
    Private Sub AddJavaScript()
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "Account #" & Me.AccountNum.ToString, "javascript:LoadChart(" & Me.AccountNum & ");", True)
    End Sub
End Class