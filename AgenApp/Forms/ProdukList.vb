Imports System.Data.SqlClient
Imports System.ComponentModel   'For Sorting
Imports System.Threading

Public Class FrmProdukList

    Dim SAPPostOK As Boolean
    Dim MATDOC As String = ""


    Private Sub FrmProdukList_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Call LogToServer()

        Me.WindowState = FormWindowState.Maximized

        With DGView
            .MultiSelect = False
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnCount = 11
            .RowCount = 0

            .Columns.Item(0).HeaderText = "NO TRANS"
            .Columns.Item(1).HeaderText = "ISSUE DATE"
            .Columns.Item(2).HeaderText = "PROD. ORDER"
            .Columns.Item(3).HeaderText = "MATERIAL"
            .Columns.Item(4).HeaderText = "DESCRIPTION"
            .Columns.Item(5).HeaderText = "PLANT"
            .Columns.Item(6).HeaderText = "SLOC"
            .Columns.Item(7).HeaderText = "BATCH"
            .Columns.Item(8).HeaderText = "ISSUE QTY"
            .Columns.Item(9).HeaderText = "UNIT"
            .Columns.Item(10).HeaderText = "SAP DOC"
            .Columns(0).Visible = False
            .Columns(1).Width = 95
            .Columns(2).Width = 95
            .Columns(3).Width = 110
            .Columns(4).Width = 200
            .Columns(5).Width = 60
            .Columns(6).Width = 60
            .Columns(7).Width = 60
            .Columns(8).Width = 100
            .Columns(9).Width = 60
            .Columns(10).Width = 95

            '.Columns(11).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(8).ValueType = GetType(Double)
            .Columns(8).DefaultCellStyle.Format = "N2"


        End With

        dtpIssue.Value = DateValue(Format(Now, "yyyy-MM") & "-01")
        dtpIssue2.Value = Now

        Call btnLoad_Click(sender, e)


    End Sub

    Private Sub FrmProdukList_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        Panel1.Left = Me.Width - Panel1.Width - 17
        Panel1.Top = Me.Height - Panel1.Height - 48
    End Sub

    Private Sub btnLoad_Click(sender As Object, e As EventArgs) Handles btnLoad.Click

        Me.Cursor = Cursors.WaitCursor

        DGView.RowCount = 0

        Query = "SELECT notrans,issuedate,prodorder,material,materialdesc,plant,sloc,batch,issueqty,unit,matdoc FROM [SZ_GoodsIssue] " & _
                 "WHERE [IssueDate] >= '" & Format(dtpIssue.Value, "yyyy-MM-dd") & "' " & _
                   "AND [IssueDate] <= '" & Format(dtpIssue2.Value, "yyyy-MM-dd") & "' " & _
                   "AND [Material] LIKE '%" & Trim(txtMaterial.Text) & "%' " & _
                   "AND [ProdOrder] LIKE '%" & Trim(txtProdOrder.Text) & "%' " & _
                   "AND [MatDocCancel] Is Null " & _
                 "ORDER BY IssueDate, ProdOrder "

        DS = New DataSet
        DA = New SqlDataAdapter(Query, Conn)
        DA.Fill(DS, "GI")

        Dim i As Long = 0

        If DS.Tables("GI").Rows.Count > 0 Then

            For Each DR As DataRow In DS.Tables("GI").Rows

                DGView.Rows.Add(DS.Tables("GI").Rows(i)(0), _
                                Format(DS.Tables("GI").Rows(i)(1), "dd/MM/yyyy"), _
                                DS.Tables("GI").Rows(i)(2), _
                                DS.Tables("GI").Rows(i)(3), _
                                DS.Tables("GI").Rows(i)(4), _
                                DS.Tables("GI").Rows(i)(5), _
                                DS.Tables("GI").Rows(i)(6), _
                                DS.Tables("GI").Rows(i)(7), _
                                DS.Tables("GI").Rows(i)(8), _
                                DS.Tables("GI").Rows(i)(9), _
                                DS.Tables("GI").Rows(i)(10))

                i = i + 1

            Next

        End If


        Query = "SELECT SUM(issueqty) FROM [SZ_GoodsIssue] " & _
                 "WHERE [IssueDate] >= '" & Format(dtpIssue.Value, "yyyy-MM-dd") & "' " & _
                   "AND [IssueDate] <= '" & Format(dtpIssue2.Value, "yyyy-MM-dd") & "' " & _
                   "AND [Material] LIKE '%" & Trim(txtMaterial.Text) & "%' " & _
                   "AND [ProdOrder] LIKE '%" & Trim(txtProdOrder.Text) & "%' " & _
                   "AND [MatDocCancel] Is Null " 

        DS = New DataSet
        DA = New SqlDataAdapter(Query, Conn)
        DA.Fill(DS, "GI")

        If Not IsDBNull(DS.Tables("GI").Rows(0)(0)) Then

            DGView.Rows.Add("", "", "", "", "", "", "", "", _
                            DS.Tables("GI").Rows(0)(0))

            Dim b As Integer = i

            For c = 0 To 10
                DGView.Item(c, b).Style.BackColor = Color.LightCyan
            Next

        End If

        Me.Cursor = Cursors.Default


    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click

        SaveStatus = False

        SAPDOC = ""

        FrmIssue_Detail.ShowDialog()

        If SaveStatus Then btnLoad_Click(sender, e)

    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click

        If DGView.RowCount = 0 Then Exit Sub

        Dim NOTRX As Long = DGView.SelectedRows.Item(0).Cells(0).Value
        Dim POSTDATE As Date = DGView.SelectedRows.Item(0).Cells(1).Value
        Dim PROORDER As String = Trim(DGView.SelectedRows.Item(0).Cells(2).Value)
        Dim MAT As String = Trim(DGView.SelectedRows.Item(0).Cells(3).Value)
        Dim DESC As String = Trim(DGView.SelectedRows.Item(0).Cells(4).Value)
        PLANT = Trim(DGView.SelectedRows.Item(0).Cells(5).Value)
        SLOC = Trim(DGView.SelectedRows.Item(0).Cells(6).Value)
        Dim BATCH As String = Trim(DGView.SelectedRows.Item(0).Cells(7).Value)
        Dim QTT As Double = DGView.SelectedRows.Item(0).Cells(8).Value
        Dim UOM As String = Trim(DGView.SelectedRows.Item(0).Cells(9).Value)
        Dim MATDOC As String = DGView.SelectedRows.Item(0).Cells(10).Value


        If MsgBox("Transaksi Goods Issue ini akan di cancel ? " & vbCrLf & vbCrLf & _
                  "-Posting Date" & Chr(9) & ": " & Format(POSTDATE, "dd.MM.yyyy") & vbCrLf & _
                  "-Prod. Order" & Chr(9) & ": " & PROORDER & vbCrLf & _
                  "-Material" & Chr(9) & Chr(9) & ": " & MAT & vbCrLf & _
                  "-Description" & Chr(9) & ": " & DESC & vbCrLf & _
                  "-Batch" & Chr(9) & Chr(9) & ": " & BATCH & vbCrLf & _
                  "-Quantity" & Chr(9) & Chr(9) & ": " & Format(QTT, "#,##0.00") & " " & UOM, _
                  vbQuestion + vbYesNo + vbDefaultButton2, "Confirmation") = vbNo Then
            Exit Sub
        End If

        '=========================================================================================================================

        Call LogToSAP()

        If SAPConStatus = False Then Exit Sub

        SAPPostOK = False

        If IsNumeric(Mid(PROORDER, 1, 1)) Then
            PROORDER = "0000" & PROORDER
        End If


        Dim oFuncCtrl As Object
        Dim oTheFunc As Object
        Dim oHeader As Object
        Dim oCode As Object
        Dim oItems As Object
        Dim oRetMess As Object
        Dim returnFunc As Boolean = False


        oFuncCtrl = CreateObject("SAP.Functions")
        oFuncCtrl.Connection = SAPConn

        oTheFunc = oFuncCtrl.Add("BAPI_GOODSMVT_CREATE")

        oHeader = oTheFunc.Exports.Item("GOODSMVT_HEADER")
        oCode = oTheFunc.Exports.Item("GOODSMVT_CODE")

        oItems = oTheFunc.Tables.Item("GOODSMVT_ITEM")

        oHeader.Value("PSTNG_DATE") = Format(POSTDATE, "yyyy/MM/dd")
        oHeader.Value("DOC_DATE") = Format(POSTDATE, "yyyy/MM/dd")
        oHeader.Value("HEADER_TXT") = Format(Now, "dd/MM/yyyy hh:mm:ss")

        oCode.Value("GM_CODE") = "03"

        Dim n As Integer = 0

        n = n + 1
        oItems.Rows.Add()
        oItems.Value(n, "MATERIAL") = MAT
        oItems.Value(n, "PLANT") = PLANT
        oItems.Value(n, "STGE_LOC") = SLOC
        oItems.Value(n, "BATCH") = BATCH
        oItems.Value(n, "ORDERID") = PROORDER
        oItems.Value(n, "MOVE_TYPE") = "262"
        oItems.Value(n, "ENTRY_QNT") = QTT
        oItems.Value(n, "ENTRY_UOM") = UOM
        oItems.Value(n, "WITHDRAWN") = QTT


        returnFunc = oTheFunc.Call

        oRetMess = oTheFunc.Imports("GOODSMVT_HEADRET")

        MATDOC = oRetMess.Value("MAT_DOC")

        oRetMess = oTheFunc.tables.Item("RETURN")

        If MATDOC = "" Or oRetMess Is Nothing Then
            MsgBox(oRetMess.Value(1, "MESSAGE"))
        Else
            oTheFunc = Nothing
            oTheFunc = oFuncCtrl.Add("BAPI_TRANSACTION_COMMIT")

            returnFunc = oTheFunc.Call

            MsgBox("Material Document No : " & MATDOC & " Created ! ")

            SAPPostOK = True
        End If

        oFuncCtrl = Nothing
        oTheFunc = Nothing
        oHeader = Nothing
        oCode = Nothing
        oItems = Nothing
        oRetMess = Nothing


        If SAPPostOK Then

            Query = "UPDATE [SZ_GoodsIssue] SET MatDocCancel='" & MATDOC & "' WHERE NoTrans=" & NOTRX & ""

            CMD = New SqlCommand
            CMD.Connection = Conn
            CMD.CommandText = Query
            CMD.ExecuteNonQuery()

        End If


        Me.Cursor = Cursors.Default


        Call btnLoad_Click(sender, e)



        '=========================================================================================================================
        'Call LogToSAP()

        'If SAPConStatus = False Then Exit Sub

        'Me.Cursor = Cursors.WaitCursor

        'SAPPostOK = False

        'Dim oFuncCtrl As Object
        'Dim oTheFunc As Object
        'Dim oMatDoc As Object
        'Dim oPostDate As Object
        'Dim oRetMess As Object
        'Dim returnFunc As Boolean = False
        'Dim CancelMatDoc As String = ""

        'oFuncCtrl = CreateObject("SAP.Functions")
        'oFuncCtrl.Connection = SAPConn

        'oTheFunc = oFuncCtrl.Add("BAPI_GOODSMVT_CANCEL")

        'oMatDoc = oTheFunc.Exports.Item("MATERIALDOCUMENT")
        'oMatDoc.Value = MATDOC

        'oPostDate = oTheFunc.Exports.Item("GOODSMVT_PSTNG_DATE")
        'oPostDate.Value = Format(POSTDATE, "yyyy/MM/dd")

        'returnFunc = oTheFunc.Call

        'oRetMess = oTheFunc.Imports("GOODSMVT_HEADRET")
        'CancelMatDoc = oRetMess.Value("MAT_DOC")

        'oRetMess = oTheFunc.tables.Item("RETURN")


        'If CancelMatDoc = "" Or oRetMess Is Nothing Then
        '    MsgBox(oRetMess.Value(1, "MESSAGE"))
        'Else
        '    oTheFunc = Nothing
        '    oTheFunc = oFuncCtrl.Add("BAPI_TRANSACTION_COMMIT")

        '    returnFunc = oTheFunc.Call

        '    SAPPostOK = True

        'End If

        'oFuncCtrl = Nothing
        'oTheFunc = Nothing
        'oRetMess = Nothing
        'oMatDoc = Nothing
        'oPostDate = Nothing
        '=========================================================================================================================



    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click

        SaveStatus = False

        SAPDOC = DGView.SelectedRows.Item(0).Cells(10).Value

        FrmIssue_Detail.ShowDialog()

        If SaveStatus Then btnLoad_Click(sender, e)

    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

End Class