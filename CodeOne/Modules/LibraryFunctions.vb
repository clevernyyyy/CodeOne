Imports System.Data.SqlClient
Imports System.Web.HttpContext
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Text.RegularExpressions
Imports System.Web.Configuration

Public Module LibraryFunctions

    'Controls
#Region "Control Data Handling"
    Public Sub SetDataSource(ByVal pstrControl As RadioButtonList, _
    ByVal pdtStructure As DataTable, _
    ByVal pstrDataText As String, _
    ByVal pstrDataValue As String,
    Optional ByVal pstrDataTextFormatString As String = "")

        Try
            'Set DataSource of DropDownList
            pstrControl.SelectedValue = Nothing
            pstrControl.DataSource = Nothing
            pstrControl.DataValueField = pstrDataValue
            pstrControl.DataTextField = pstrDataText
            pstrControl.DataSource = pdtStructure
            If pstrDataTextFormatString <> "" Then
                pstrControl.DataTextFormatString = pstrDataTextFormatString
            End If
            pstrControl.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub SetDataSource(ByVal pstrControl As DropDownList, _
    ByVal pdtStructure As DataTable, _
    ByVal pstrDataText As String, _
    ByVal pstrDataValue As String,
    Optional ByVal pstrDataTextFormatString As String = "")

        Try
            'Set DataSource of DropDownList
            If Not pstrControl Is Nothing Then
                pstrControl.SelectedValue = Nothing
                pstrControl.DataSource = Nothing
                pstrControl.DataValueField = pstrDataValue
                pstrControl.DataTextField = pstrDataText
                pstrControl.DataSource = pdtStructure

                If pstrDataTextFormatString <> "" Then
                    pstrControl.DataTextFormatString = pstrDataTextFormatString
                End If
                pstrControl.DataBind()
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub SetDataSource(ByVal pstrControl As HtmlSelect, _
        ByVal pdtStructure As DataTable, _
        ByVal pstrDataText As String, _
        ByVal pstrDataValue As String)

        Try
            'Set DataSource of DropDownList
            pstrControl.Value = Nothing
            pstrControl.DataSource = Nothing
            pstrControl.DataValueField = pstrDataValue
            pstrControl.DataTextField = pstrDataText
            pstrControl.DataSource = pdtStructure
            pstrControl.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BindDataGrid(ByRef dgvGrid As GridView, ByVal dtTable As DataTable)
        With dgvGrid
            .DataSource = Nothing
            .DataSource = dtTable
            .DataBind()
        End With
    End Sub
    Public Sub BindDataGrid(ByRef dgvGrid As GridView, ByVal ds As DataSet)
        With dgvGrid
            .DataSource = Nothing
            .DataSource = ds
            .DataBind()
        End With
    End Sub
#End Region

#Region "Generic Database Functions"
    Public Function Command(ByVal pcProc As String,
                           ByVal cnn As SqlConnection,
                           Optional ByVal sqlTran As SqlTransaction = Nothing) As SqlCommand
        Dim cmd As New SqlCommand(pcProc)
        cmd.CommandType = CommandType.StoredProcedure
        If Not sqlTran Is Nothing Then
            cmd.Transaction = sqlTran
        End If
        cmd.Connection = cnn
        Return cmd
    End Function
    Private Function Convert(ByRef pnVal As Object, ByVal pConvert As Enumerations.enmType)
        If IsDBNull(pnVal) Then
            If pConvert = Enumerations.enmType._String Then
                pnVal = ""
            ElseIf pConvert = Enumerations.enmType._Boolean Then
                pnVal = False
            ElseIf pConvert = Enumerations.enmType._DateTime Then
                pnVal = "1/1/1900"
            Else
                pnVal = 0
            End If
        End If
        Select Case pConvert
            Case Enumerations.enmType._String
                Return CType(pnVal, String)
            Case Enumerations.enmType._Decimal
                Return CType(pnVal, Decimal)
            Case Enumerations.enmType._Integer
                Return CType(pnVal, Integer)
            Case Enumerations.enmType._Double
                Return CType(pnVal, Double)
            Case Enumerations.enmType._Boolean
                Return CType(pnVal, Boolean)
            Case Enumerations.enmType._DateTime
                Return CType(pnVal, DateTime)
        End Select
        Return ""
    End Function
    Public Sub CnnSqlOpen(ByVal gcnnSQL As SqlConnection)
        Try
            If gcnnSQL.State = ConnectionState.Closed Then gcnnSQL.Open()
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function FillDataTable(ByVal pcStoredProc As String,
                                  ByVal gcnnSQL As SqlConnection,
                                  ByVal psParameterName As String, ByVal pcParameterValue As String,
                                  Optional ByVal pcTableName As String = "DBTable") As DataTable
        Dim Command As New SqlCommand
        Command.CommandText = pcStoredProc
        Command.Connection = gcnnSQL

        With Command.Parameters
            .AddWithValue(psParameterName, pcParameterValue)
        End With
        Return FillDataTable(Command, pcTableName)
    End Function
    Public Function FillDataTable(ByVal pcStoredProc As String,
                                  ByVal gcnnSQL As SqlConnection,
                                  Optional ByVal pcTableName As String = "",
                                  Optional ByVal plFixNulls As Boolean = True) As DataTable

        Dim Command As New SqlCommand
        Command.Connection = gcnnSQL
        Command.CommandText = pcStoredProc

        Return FillDataTable(Command, pcTableName, plFixNulls)

    End Function
    Public Function FillDataTable(ByRef pCommand As SqlCommand,
                                  Optional ByVal pcTableName As String = "Table", Optional ByVal plFixNulls As Boolean = True) As DataTable
        Dim dt As New DataTable

        Dim daSql As New SqlDataAdapter

        pCommand.CommandType = CommandType.StoredProcedure

        daSql.SelectCommand = pCommand

        Try
            CnnSqlOpen(pCommand.Connection)
            daSql.Fill(dt)
        Catch sqlex As SqlException
            'Catch the foreign key merge error. Ignore it because it does nothing.
        Catch ex As Exception
            Throw
        Finally
            ''Only close the connection if there isn't a transaction associated with the command
            If pCommand.Transaction Is Nothing Then
                pCommand.Connection.Close()
            End If
            daSql.Dispose()
            pCommand.Dispose()
        End Try

        dt.TableName = pcTableName
        If plFixNulls Then
            FixNulls(dt)
        End If

        Return dt

    End Function
    Public Function FillDataSet(ByVal pcStoredProc As String, ByVal gcnnSQL As SqlConnection) As DataSet

        Dim Command As New SqlCommand
        Command.Connection = gcnnSQL
        Command.CommandText = pcStoredProc

        Return FillDataSet(Command)

    End Function
    Public Function FillDataSet(ByRef pCommand As SqlCommand) As DataSet

        Dim ds As New DataSet
        Dim daSql As New SqlDataAdapter

        pCommand.CommandType = CommandType.StoredProcedure

        daSql.SelectCommand = pCommand
        Try
            CnnSqlOpen(pCommand.Connection)
            daSql.Fill(ds)
        Catch ex As Exception
            Throw
        Finally
            If pCommand.Transaction Is Nothing Then
                pCommand.Connection.Close()
            End If
            daSql.Dispose()
            pCommand.Dispose()
        End Try

        Return ds

    End Function
    Public Function ExecNonQuery(ByRef pCommand As SqlCommand) As Boolean

        Dim lSaved As Boolean = False
        Dim cnnSQL As SqlConnection = (New Connection).NewCnn

        pCommand.CommandType = CommandType.StoredProcedure
        pCommand.Connection = cnnSQL

        Try
            CnnSqlOpen(pCommand.Connection)
            pCommand.ExecuteNonQuery()
            lSaved = True
        Catch sqlex As SqlException
            '
        Catch ex As Exception
            Throw
        Finally
            ''Only close the connection if there isn't a transaction associated with the command
            If pCommand.Transaction Is Nothing Then
                pCommand.Connection.Close()
            End If
            pCommand.Dispose()
        End Try

        Return lSaved
    End Function
    Public Function FillScalar(ByVal pCmd As SqlCommand, ByVal pcType As String)
        Dim ret
        Dim daSql As New SqlDataAdapter

        pCmd.CommandType = CommandType.StoredProcedure

        daSql.SelectCommand = pCmd
        Try
            CnnSqlOpen(pCmd.Connection)
            ret = pCmd.ExecuteScalar()
        Catch ex As Exception
            Throw
        Finally
            If pCmd.Transaction Is Nothing Then
                pCmd.Connection.Close()
            End If
            daSql.Dispose()
            pCmd.Dispose()
        End Try

        Return Convert(ret, pcType)
    End Function
    Public Function ExecScalar(ByVal pcStoredProc As String, ByVal gcnnSQL As SqlConnection, ByVal pcType As String)

        Dim Command As New SqlCommand
        Command.Connection = gcnnSQL
        Command.CommandText = pcStoredProc

        Return ExecScalar(Command, pcType)
    End Function
    Public Function ExecScalar(ByRef pCommand As SqlCommand, ByVal pcType As String)

        Dim ret
        Dim cnnSQL As SqlConnection = (New Connection).NewCnn

        pCommand.CommandType = CommandType.StoredProcedure
        pCommand.Connection = cnnSQL

        Try
            CnnSqlOpen(pCommand.Connection)
            ret = pCommand.ExecuteScalar()
        Catch ex As Exception
            Throw
        Finally
            If pCommand.Transaction Is Nothing Then
                pCommand.Connection.Close()
            End If
            pCommand.Dispose()
        End Try

        Return Convert(ret, pcType)
    End Function
#End Region

#Region "Data Handling"
    Public Sub AddColumnsToDataTable(ByRef dtTable As DataTable, ByVal ParamArray strColumns() As String)
        Try
            Dim dc As DataColumn
            Dim str As String
            For Each str In strColumns
                If Not dtTable.Columns.Contains(str) Then
                    Dim sType As System.Type = System.Type.GetType("System.String")
                    Select Case Strings.Left(str, 1)
                        Case "c", "m"
                            sType = System.Type.GetType("System.String")
                        Case "l"
                            sType = System.Type.GetType("System.Boolean")
                        Case "n"
                            sType = System.Type.GetType("System.Decimal")
                        Case "d"
                            sType = System.Type.GetType("System.DateTime")
                    End Select
                    dc = New DataColumn(str, sType)
                    dtTable.Columns.Add(dc)
                End If
            Next
            dtTable.AcceptChanges()
            LibraryFunctions.FixNulls(dtTable)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function SortTable(ByRef dtTable As DataTable, ByVal cSortCol As String) As DataTable
        Dim dv As New DataView(dtTable)
        dv.Sort = cSortCol
        Return dv.ToTable
    End Function
    Public Sub FixNulls(ByRef pdsSet As DataSet)
        Try
            For Each dtTable As DataTable In pdsSet.Tables
                FixNulls(dtTable)
            Next
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub FixNulls(ByRef pdtTable As DataTable)
        Try
            'Assign a default value to all current Null fields
            For Each dr As DataRow In pdtTable.Rows
                FixNulls(dr, pdtTable)
            Next
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub FixNulls(ByRef pdrRow As DataRow, ByRef pdtTable As DataTable)
        Try
            'Assign a default value to all current Null fields
            Dim myCol As DataColumn

            For Each myCol In pdtTable.Columns
                If IsDBNull(pdrRow.Item(myCol.ColumnName)) Then
                    Select Case Strings.Left(myCol.ColumnName, 1)
                        Case "c", "m"
                            pdrRow.Item(myCol.ColumnName) = ""
                        Case "l"
                            pdrRow.Item(myCol.ColumnName) = False
                        Case "n"
                            pdrRow.Item(myCol.ColumnName) = 0
                        Case "d"
                            If Not myCol.ColumnName = "dTimeOfLoss" Then
                                pdrRow.Item(myCol.ColumnName) = "01/01/1900"
                            Else
                                pdrRow.Item(myCol.ColumnName) = TimeSpan.Zero
                            End If
                        Case "i"
                            pdrRow.Item(myCol.ColumnName) = Guid.Empty
                    End Select
                End If
            Next
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Sub AddEmptyRow(ByRef dtTable As DataTable, ByRef nIndex As Integer)
        AddEmptyRow(dtTable)
        nIndex = dtTable.Rows.Count - 1
    End Sub
    Public Sub AddEmptyRow(ByRef dtTable As DataTable)

        If Not dtTable Is Nothing Then
            Dim dr As DataRow
            dr = dtTable.NewRow
            FixNulls(dr, dtTable)
            dtTable.Rows.Add(dr)
        End If
    End Sub
    Public Function AddEmptyRowToBegining(ByRef dtTable As DataTable, Optional ByVal cSortCol As String = "", Optional ByVal FixNulls As Boolean = True)
        Dim dr As DataRow = dtTable.NewRow

        dtTable.Rows.InsertAt(dr, 0)
        If FixNulls Then
            LibraryFunctions.FixNulls(dtTable)
        End If

        If cSortCol <> "" Then
            Return SortTable(dtTable, cSortCol)
        Else
            Return dtTable
        End If
    End Function
    Public Function NullableBool(ByVal plBool As Object)
        If IsDBNull(plBool) OrElse plBool Is Nothing Then
            Return DBNull.Value
        End If
        If plBool = False Then
            Return DBNull.Value
        End If
        Return plBool
    End Function
    Public Function NullableInt(ByVal pnInt As Object)
        If IsDBNull(pnInt) OrElse pnInt Is Nothing Then
            Return DBNull.Value
        End If
        If pnInt = 0 Then
            Return DBNull.Value
        End If
        Return pnInt
    End Function
    Public Function NullableDec(ByVal pnDec As Object)
        If IsDBNull(pnDec) OrElse pnDec Is Nothing Then
            Return DBNull.Value
        End If
        If pnDec = 0 Then
            Return DBNull.Value
        End If
        Return pnDec
    End Function
    Public Function NullableString(ByVal pcString As Object)
        If IsDBNull(pcString) OrElse pcString Is Nothing Then
            Return DBNull.Value
        End If
        If pcString.Trim = "" Then
            Return DBNull.Value
        End If
        Return pcString
    End Function
    Public Function NullableDate(ByVal pdDate As Object)
        If IsDBNull(pdDate) OrElse pdDate Is Nothing Then
            Return DBNull.Value
        End If
        'For some reason, instead of null or 1/1/1900, comes in with just a time, year = 1 will catch that case
        If CType(pdDate, DateTime).Year = 1 OrElse CType(pdDate, DateTime).Date = "1/1/1900" Then
            Return DBNull.Value
        End If
        Return pdDate
    End Function
    Public Function NullableField(Of T)(ByVal item As T) As T
        If IsDBNull(item) Then
            Return Nothing
        Else
            Return item
        End If
    End Function
    Public Sub CheckSQLCommand(ByVal objCommand As SqlCommand)
        Debug.WriteLine("CommandType: {0}", objCommand.CommandType.ToString)
        Debug.WriteLine("CommandText: {0}", objCommand.CommandText)

        For Each parameter As Object In objCommand.Parameters
            Dim strParameterValue As String = String.Format( _
                "{0, 11} Parameter: {1} ({2}) == '{3}' ({4})", _
                    parameter.Direction, _
                    parameter.ParameterName, _
                    parameter.DbType, _
                    parameter.Value, _
                    If(Not parameter.value Is Nothing, parameter.Value.GetType().Name, "Nothing"))
            Debug.WriteLine(strParameterValue)
        Next
    End Sub
    Public Sub CreateDataTable(ByVal pcStoredProcedure As String, ByVal pcParameterID As String, ByRef dt As DataTable)

        Dim proc As New SqlClient.SqlCommand(pcStoredProcedure, (New Connection).NewCnn)
        Dim params As Array = pcParameterID.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
        Dim i As Integer = 0
        For Each j As String In params
            Dim s As String = params([i])
            proc.Parameters.AddWithValue(Trim(s), 0)
            i += 1
        Next

        dt = LibraryFunctions.FillDataTable(proc, "dt")

        AddEmptyRow(dt)

    End Sub
#End Region

#Region "Object Conversions"
    Public Sub RowToObject(ByRef obj As Object, ByVal row As DataRow, ByVal ParamArray strIgnore() As String)
        Dim t As Type = obj.GetType
        Dim properties As System.Reflection.PropertyInfo() = t.GetProperties()

        For Each prop In properties
            Try
                If Not prop.GetSetMethod Is Nothing Then
                    If strIgnore.Length = 0 Then
                        prop.SetValue(obj, row.Item(prop.Name), Nothing)
                    Else
                        If Not strIgnore.Contains(prop.Name) Then
                            prop.SetValue(obj, row.Item(prop.Name), Nothing)
                        End If
                    End If
                End If
            Catch ex As Exception
                Throw
            End Try
        Next
    End Sub
    Public Sub ObejctToDataTable(ByRef dt As DataTable, ByVal objList As System.Collections.Generic.IEnumerable(Of Object))
        dt.Clear()

        If Not objList Is Nothing Then
            For Each obj In objList
                Dim dr As DataRow = dt.NewRow
                ObejctToDataRow(dr, obj)
                dt.Rows.Add(dr)
            Next
        End If
    End Sub
    Public Sub ObejctToDataRow(ByRef dr As DataRow, ByVal obj As Object)
        Dim t As Type = obj.GetType
        Dim properties As System.Reflection.PropertyInfo() = t.GetProperties()

        For Each col As DataColumn In dr.Table.Columns
            Try
                For Each prop In properties
                    Try
                        If prop.Name = col.ColumnName Then

                            dr.Item(col.ColumnName) = If(prop.GetValue(obj, Nothing), DBNull.Value)
                            Exit For
                        End If
                    Catch ex As Exception
                        Throw ex
                    End Try
                Next
            Catch ex As Exception
                Throw ex
            End Try
        Next
    End Sub
#End Region

#Region "String Formatting"
    Public Function FormatPolNum(ByVal strPolNum As String) As String
        Try
            ''Display Policy Number as 70APT000000-01
            If strPolNum.Trim.Length <= 11 Then
                Return strPolNum
            ElseIf Trim(strPolNum).Length = 13 Then
                Return Left(strPolNum, 11) & "-" & Right(strPolNum.Trim, 2)
            ElseIf Trim(strPolNum).Length = 14 Then
                Return Left(strPolNum, 11) & "-" & Right(strPolNum.Trim, 3)
            ElseIf Trim(strPolNum).Length = 15 Then
                Return Left(strPolNum, 11) & "-" & Right(strPolNum.Trim, 4)
            End If
        Catch ex As Exception
            Throw
        End Try
        Return strPolNum
    End Function
    Public Function UnFormatPolNum(ByVal strPolNum As String) As String
        ''Display Policy Number as 70APT000000-01
        If strPolNum.Trim = "" Then
            Return ""
        End If
        Try
            Return Strings.Replace(strPolNum, "-", "")
        Catch ex As Exception
            Throw
        End Try
        Return ""
    End Function
    Public Function FormatZip(ByVal strZip As String) As String
        Dim nReturn As String = ""

        If strZip.Length > 5 Then
            nReturn = CType(strZip.Substring(0, 5), String)
        Else
            nReturn = CType(strZip, String)
        End If
        Return nReturn
    End Function
    Public Function FormatPhone(ByVal strPhone As String) As String
        Try
            ''Unformat just in case
            If strPhone Is Nothing Then
                strPhone = ""
            End If

            If strPhone.Trim <> "" Then
                Dim blnAsterick As Boolean = False
                If InStr(strPhone, "*") > 0 Then
                    blnAsterick = True
                    strPhone = strPhone.Replace("*", "")
                End If
                strPhone = UnFormatPhone(strPhone)
                If strPhone.Trim.Length = 10 Then
                    strPhone = "(" & Mid(strPhone, 1, 3) & ") " & Mid(strPhone, 4, 3) & "-" & Mid(strPhone, 7, 4)
                ElseIf strPhone.Trim.Length = 7 Then
                    strPhone = Mid(strPhone, 1, 3) & "-" & Mid(strPhone, 4, 4)
                End If
                If blnAsterick Then
                    strPhone = strPhone & "*"
                End If
            End If
        Catch ex As Exception
            Throw
        End Try
        Return strPhone.Trim & " "
    End Function
    Public Function UnFormatPhone(ByVal strPhone As String, Optional ByVal lTextbox As Boolean = False) As String
        Try
            strPhone = strPhone.Replace("(", "")
            strPhone = strPhone.Replace(")", "")
            strPhone = strPhone.Replace("-", "")
            strPhone = strPhone.Replace("x", "")
            strPhone = strPhone.Replace(" ", "")
            If lTextbox = True Then
                strPhone = strPhone & "    "
            End If

        Catch ex As Exception
            Throw
        End Try
        Return strPhone
    End Function
    Public Function FormatDate(ByVal dptDate As DateTime, _
    Optional ByVal blnCheckDate As Boolean = False, _
    Optional ByVal blnYearOnly As Boolean = False, _
    Optional ByVal blnInclTime As Boolean = False, _
    Optional ByVal blnShowSeconds As Boolean = False) As String
        FormatDate = ""
        Try
            If blnCheckDate Then
                'dptDate = DateOnHolidayWeekend(dptDate, False, False)
            End If
            If blnInclTime Then
                If blnShowSeconds Then
                    FormatDate = dptDate.ToString
                Else
                    FormatDate = dptDate.ToString("g")
                End If
            ElseIf Not blnYearOnly Then
                FormatDate = dptDate.ToShortDateString
            Else
                FormatDate = "XX-XX-" & dptDate.Year.ToString
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function

#End Region

#Region "Type Conversion"
    Public Function ConvertInt(ByVal pcVal As String) As Integer
        If pcVal Is Nothing Then
            Return 0
        End If
        pcVal = Regex.Replace(pcVal, "[^0-9-]", "") 'Removes all non-integer charcters, except - 
        If pcVal.Trim = "" Then
            Return 0
        End If
        Dim nInt As Integer
        If Integer.TryParse(pcVal, nInt) Then
            Return nInt
        End If
        Return 0
    End Function
    Public Function ConvertShort(ByVal pcVal As String) As Short
        If pcVal Is Nothing Then
            Return 0
        End If
        pcVal = Regex.Replace(pcVal, "[^0-9-]", "") 'Removes all non-integer charcters, except -
        If pcVal.Trim = "" Then
            Return 0
        End If
        Dim nShort As Short
        If Short.TryParse(pcVal, nShort) Then
            Return nShort
        End If
        Return 0
    End Function
    Public Function ConvertDec(ByVal pcVal As String) As Decimal
        If pcVal Is Nothing Then
            Return 0
        End If
        pcVal = Regex.Replace(pcVal, "[^0-9-.]", "") 'Removes all non-integer charcters, except - and .
        If pcVal.Trim = "" Then
            Return 0
        End If
        Dim nDec As Decimal
        If Decimal.TryParse(pcVal, nDec) Then
            Return nDec
        End If
        Return 0
    End Function

#End Region

#Region "Validation"
    Public Function ValidPolNum(ByVal strPolNum As String) As Boolean
        If strPolNum.Trim.Length = 11 OrElse strPolNum.Trim.Length = 13 Then
            Return True
        End If
        Return False
    End Function
#End Region

#Region "Helpers"
    Public Sub SetDisplay(ByVal strDisplay As String, ByVal ParamArray cControls() As String)
        Dim cString As String

        For Each cString In cControls
            cString = strDisplay
        Next
    End Sub
    Public Sub SetDisplay(ByVal strDisplay As String, ByVal ParamArray Controls() As Control)
        'This is modified to use a variable list of controls
        Dim wc As Control

        For Each wc In Controls
            Dim strType As String = wc.GetType.ToString.ToLower
            If InStr(strType, "html") > 0 Then
                Dim htmlcontrol As HtmlControl = wc
                htmlcontrol.Style("display") = strDisplay
            ElseIf InStr(strType, "checkbox") > 0 Then
                Dim checkbox As Web.UI.WebControls.CheckBox = wc
                checkbox.Style("display") = strDisplay
            ElseIf InStr(strType, "label") > 0 Then
                Dim label As Web.UI.WebControls.Label = wc
                label.Style("display") = strDisplay
            ElseIf InStr(strType, "dropdownlist") > 0 Then
                Dim dropdownlist As Web.UI.WebControls.DropDownList = wc
                dropdownlist.Style("display") = strDisplay
            ElseIf InStr(strType, "textbox") > 0 Then
                Dim textbox As Web.UI.WebControls.TextBox = wc
                textbox.Style("display") = strDisplay
            ElseIf InStr(strType, "linkbutton") > 0 Then
                Dim linkbutton As Web.UI.WebControls.LinkButton = wc
                linkbutton.Style("display") = strDisplay
            ElseIf InStr(strType, "imagebutton") > 0 Then
                Dim imagebutton As Web.UI.WebControls.ImageButton = wc
                imagebutton.Style("display") = strDisplay
            ElseIf InStr(strType, "hyperlink") > 0 Then
                Dim link As Web.UI.WebControls.HyperLink = wc
                link.Style("display") = strDisplay
            ElseIf InStr(strType, "radiobuttonlist") > 0 Then
                Dim radiobutton As Web.UI.WebControls.RadioButtonList = wc
                radiobutton.Style("display") = strDisplay
            End If
        Next
    End Sub
    Public Sub SetVisibility(ByVal pblnValue As Boolean, ByVal ParamArray Controls() As Control)
        'This is modified to use a variable list of controls
        Dim wc As Control

        For Each wc In Controls
            wc.Visible = pblnValue
        Next
    End Sub
    Public Sub FormatDataTable(ByRef dt As DataTable, ByVal pcParam As String)

        For i = dt.Rows.Count - 1 To 0 Step -1
            If dt.Rows(i).Item(pcParam).ToString.Trim = "" Then
                dt.Rows.RemoveAt(i)
            ElseIf dt.Rows(i).Item(pcParam).ToString.Contains("  ") Then
                Replace(dt.Rows(i).Item(pcParam).ToString, "  ", " ")  'Basically accounting for no middle initial
            End If
        Next

        If dt.Rows.Count = 0 OrElse Not dt.Rows(0).Item(pcParam).Trim = "" Then
            LibraryFunctions.AddEmptyRowToBegining(dt)
        End If
    End Sub
    Public Function CleanText(ByVal cString As String)
        Dim cReturn As String

        cReturn = Replace(cString, "&", "")

        Return cReturn
    End Function
#End Region

#Region "WebSettings"
    Dim dtSettingsCache As DataTable
    Dim nMinutesToLive As Integer = 5
    Dim dTTL As DateTime
    Public Enum enmDatabase
        Issuance = 1
        Rating = 2
    End Enum

    Public Function GetConfigSetting(ByVal strSetting As String, Optional ByVal strSection As String = "appSettings", Optional ByVal lErrorIfNotExist As Boolean = True) As String
        Dim strValue As String = Nothing

        Try
            strValue = WebConfigurationManager.GetSection(strSection)(strSetting)

            Return strValue
        Catch ex As Exception
            If lErrorIfNotExist Then
                Throw New Exception("GetConfigSetting: Invalid setting requested - " & strSetting)
            Else
                Return Nothing
            End If
        End Try
    End Function
    '    ''' <summary>
    '    ''' Function for retrieving logged emails
    '    ''' </summary>
    '    ''' <param name="nQuoteID">Numeric User Identifier</param>
    '    ''' <returns>Datatable containing the quote's logged emails</returns>
    '    ''' <remarks></remarks>
    Public Function RetrieveEmailLog(ByVal pnQuoteID As Integer, ByVal pCNN As SqlConnection) As DataTable
        Dim dtEmailLog As New DataTable
        dtEmailLog = FillDataTable("[Save].[usp_Get_Log_Email]", pCNN, "@nQuoteID", pnQuoteID, "dtEmailLog")
        Return dtEmailLog
    End Function
#End Region


End Module
