Public Module Formatting

#Region "HTML / Control Helpers"
    <Runtime.CompilerServices.Extension()>
    Public Function ToStdDateString(ByRef dDate As Date) As String
        Return dDate.ToString("MM/dd/yyyy")
    End Function
    <Runtime.CompilerServices.Extension()>
    Public Function ToStdDateString(ByRef dDate As Nullable(Of Date)) As String
        If dDate.HasValue Then
            Return dDate.Value.ToString("MM/dd/yyyy")
        Else
            Return ""
        End If
    End Function
#End Region
End Module