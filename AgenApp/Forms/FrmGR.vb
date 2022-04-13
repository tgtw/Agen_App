Imports System.Data.SqlClient

Public Class FrmGR

    Public GR_NO As String = ""
    Public EditOrDisplay As String = ""

    Dim TEMPRECV As String = ""
    Dim KDSUPL As String = ""
    Dim NMSUPL As String = ""
    Dim SaveOkay As Boolean = False
    Dim MATDOC As String = ""
    Dim PODATE As String
    Dim NO As Integer = 1
    Dim OVERDELTOL As Integer = 0
    Dim UNLIMITED As String = ""

    Private Sub PRINT_GR_NOTE()

        Dim SLOCINFO As String = ""

        Query = "SELECT  TRANGRH.GRNO, TRANGRH.GRDATE, TRANGRH.PONO, TRANGRH.PODATE, TRANGRH.NMSUPL, TRANGRH.DELVNO, TRANGRH.NOTES," & _
                "        TRANGRD.ITEMNO, TRANGRD.KDPROD, TRANGRD.NMPROD, TRANGRD.QTYRECV1, TRANGRD.QTYRECV2, TRANGRD.UNIT, TRANGRD.SLOC " & _
                "FROM    TRANGRD INNER JOIN TRANGRH ON TRANGRD.GRNO = TRANGRH.GRNO " & _
                "WHERE   TRANGRH.GRNO = '" & MATDOC & "'"

        DS = New DataSet
        DA = New SqlDataAdapter(Query, Conn)
        DA.Fill(DS, "GR")

        SLOCINFO = DS.Tables("GR").Rows(0)("SLOC")
        SLOCINFO = SLOCINFO & "-" & SLOC_INFO(SLOCINFO)

        Dim PrintControl As New ClassPrintReport

        PrintControl.CetakReport("RptGRNote.rdlc", "DataSet1", Query, SLOCINFO, 0)

    End Sub


    Private Sub BAPI_GOODSMVT_CREATE()

        'Call LogToSAP(True)

        'If SAPConStatus = False Then Exit Sub

        SaveOkay = False

        MATDOC = ""


        Dim oFuncCtrl As Object
        Dim oTheFunc As Object
        Dim oFuncCommit As Object

        Dim oHEADER As Object
        Dim oCODE As Object
        Dim oITEM As Object
        Dim oHEADRET As Object
        Dim oRETURN As Object
        Dim oMESS As String = ""


        oFuncCtrl = CreateObject("SAP.Functions")
        oFuncCtrl.Connection = SAPConn

        oTheFunc = oFuncCtrl.Add("BAPI_GOODSMVT_CREATE")

        '===================
        ' IMPORT PARAMETERS 
        '===================

        oHEADER = oTheFunc.Exports.Item("GOODSMVT_HEADER")
        oHEADER.Value("PSTNG_DATE") = Format(dtpPostDate.Value, "yyyyMMdd")
        oHEADER.Value("DOC_DATE") = Format(dtpRecvDate.Value, "yyyyMMdd")
        oHEADER.Value("REF_DOC_NO") = Trim(txtDelNote.Text)

        oCODE = oTheFunc.Exports.Item("GOODSMVT_CODE")
        oCODE.Value("GM_CODE") = "01"


        '========
        ' TABLES
        '========

        oITEM = oTheFunc.tables.Item("GOODSMVT_ITEM")

        Dim i As Integer = 0

        For n = 1 To DGV.Rows.Count

            If DGV.Rows(n - 1).Cells(8).Value = True Then

                oITEM.Rows.Add()
                i = i + 1

                oITEM.Value(i, "MATERIAL") = DGV.Rows(n - 1).Cells(1).Value
                oITEM.Value(i, "PLANT") = PLANT
                oITEM.Value(i, "STGE_LOC") = SLOC
                oITEM.Value(i, "MOVE_TYPE") = "101"
                oITEM.Value(i, "VENDOR") = Format(Val(KDSUPL), "0000000000")
                oITEM.Value(i, "ENTRY_QNT") = DGV.Rows(n - 1).Cells(6).Value
                oITEM.Value(i, "ENTRY_UOM") = DGV.Rows(n - 1).Cells(4).Value
                oITEM.Value(i, "PO_NUMBER") = Format(Val(txtPO.Text), "0000000000")
                oITEM.Value(i, "PO_ITEM") = DGV.Rows(n - 1).Cells(0).Value
                oITEM.Value(i, "ITEM_TEXT") = lblDelv.Text
                oITEM.Value(i, "MVT_IND") = "B"

            End If

        Next n


        If oTheFunc.Call Then

            oHEADRET = oTheFunc.Imports.Item("GOODSMVT_HEADRET")

            MATDOC = oHEADRET.Value("MAT_DOC")

            If Trim(MATDOC) <> "" Then

                oFuncCommit = oFuncCtrl.Add("BAPI_TRANSACTION_COMMIT")

                If oFuncCommit.Call Then

                    MessageBox.Show("Material document no. " & MATDOC & " created !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    SaveOkay = True

                End If

            Else

                oRETURN = oTheFunc.tables.Item("RETURN")
                oMESS = oRETURN.Value(1, "MESSAGE")

                MessageBox.Show(oMESS, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)


            End If

        End If


        oFuncCtrl = Nothing
        oTheFunc = Nothing
        oFuncCommit = Nothing

        oHEADER = Nothing
        oCODE = Nothing
        oITEM = Nothing
        oHEADER = Nothing

    End Sub

    Private Sub BAPI_PO_GETDETAIL()

        'Call LogToSAP(True)

        'If SAPConStatus = False Then Exit Sub

        SaveOkay = False

        Dim oFuncCtrl As Object
        Dim oTheFunc As Object

        Dim oPURCHASEORDER As Object
        Dim oITEMS As Object
        Dim oPO_HEADER As Object
        Dim oPO_ITEMS As Object

        Dim Result As Boolean
        Dim DELSTATUS As String = ""


        oFuncCtrl = CreateObject("SAP.Functions")
        oFuncCtrl.Connection = SAPConn

        oTheFunc = oFuncCtrl.Add("BAPI_PO_GETDETAIL")

        '===================
        ' IMPORT PARAMETERS 
        '===================

        oPURCHASEORDER = oTheFunc.Exports.Item("PURCHASEORDER")
        oPURCHASEORDER.Value = Format(Val(txtPO.Text), "0000000000")

        oITEMS = oTheFunc.Exports.Item("ITEMS")
        oITEMS.Value = "X"


        '===================
        ' EXPORT PARAMETERS
        '===================

        oPO_HEADER = oTheFunc.Imports.Item("PO_HEADER")


        '========
        ' TABLES
        '========

        oPO_ITEMS = oTheFunc.tables.Item("PO_ITEMS")
        oPO_ITEMS.Rows.RemoveAll()


        Result = oTheFunc.Call

        If Result = True Then

            PODATE = oPO_HEADER.value("DOC_DATE")

            For n = 1 To oPO_ITEMS.RowCount

                DGV.Rows.Add()

                OVERDELTOL = oPO_ITEMS.Rows(n)("OVERDELTOL")
                UNLIMITED = oPO_ITEMS.Rows(n)("UNLIMITED")
                DELSTATUS = oPO_ITEMS.Rows(n)("DELETE_IND")

                If Trim(DELSTATUS) = "" Then

                    DGV.Rows(n - 1).Cells(0).Value = oPO_ITEMS.Rows(n)("PO_ITEM")
                    DGV.Rows(n - 1).Cells(1).Value = oPO_ITEMS.Rows(n)("MATERIAL")
                    DGV.Rows(n - 1).Cells(2).Value = oPO_ITEMS.Rows(n)("SHORT_TEXT")
                    DGV.Rows(n - 1).Cells(3).Value = oPO_ITEMS.Rows(n)("QUANTITY")
                    DGV.Rows(n - 1).Cells(4).Value = oPO_ITEMS.Rows(n)("UNIT")
                    DGV.Rows(n - 1).Cells(7).Value = SLOC
                    DGV.Rows(n - 1).Cells(9).Value = OVERDELTOL
                    DGV.Rows(n - 1).Cells(10).Value = UNLIMITED

                    DGV.Rows(n - 1).Cells(0).Style.BackColor = Color.FromArgb(225, 225, 225)
                    DGV.Rows(n - 1).Cells(1).Style.BackColor = Color.FromArgb(225, 225, 225)
                    DGV.Rows(n - 1).Cells(2).Style.BackColor = Color.FromArgb(225, 225, 225)
                    DGV.Rows(n - 1).Cells(3).Style.BackColor = Color.FromArgb(225, 225, 225)
                    DGV.Rows(n - 1).Cells(4).Style.BackColor = Color.FromArgb(225, 225, 225)

                End If

            Next

        End If


        oFuncCtrl = Nothing
        oTheFunc = Nothing

        oPURCHASEORDER = Nothing
        oITEMS = Nothing
        oPO_ITEMS = Nothing


    End Sub

    Private Sub DELIVERY_SCHEDULE()

        'Call LogToSAP(True)

        'If SAPConStatus = False Then Exit Sub

        SaveOkay = False

        Dim objRfcFunc As Object
        Dim objQueryTab As Object
        Dim objOptTab As Object
        Dim objFldTab As Object
        Dim objDatTab As Object
        Dim objDatRec As Object
        Dim SAP_RFC As Object

        Dim PO_QTY As Double = 0
        Dim GR_QTY As Double = 0


        SAP_RFC = CreateObject("SAP.Functions")
        SAP_RFC.Connection = SAPConn


        For n = 0 To DGV.Rows.Count - 1

            objRfcFunc = SAP_RFC.Add("RFC_READ_TABLE")

            objQueryTab = objRfcFunc.Exports("QUERY_TABLE")
            objQueryTab.Value = "EKET"  ' Scheduling agreement

            objOptTab = objRfcFunc.Tables("OPTIONS")
            objFldTab = objRfcFunc.Tables("FIELDS")
            objDatTab = objRfcFunc.Tables("DATA")

            objOptTab.FreeTable()

            objOptTab.Rows.Add()
            objOptTab(objOptTab.rowcount, "TEXT") = "EBELN = '" + Format(Val(txtPO.Text), "0000000000") + "' AND "
            objOptTab.Rows.Add()
            objOptTab(objOptTab.rowcount, "TEXT") = "EBELP = '" + DGV.Rows(n).Cells(0).Value + "'"

            objFldTab.FreeTable()

            objFldTab.Rows.Add()
            objFldTab(objFldTab.rowcount, "FIELDNAME") = "MENGE"  ' Scheduled Qty
            objFldTab.Rows.Add()
            objFldTab(objFldTab.rowcount, "FIELDNAME") = "WEMNG"  ' Delivered Qty

            If objRfcFunc.Call Then

                If objDatTab.rowcount > 0 Then

                    Dim i As Integer = 0

                    For Each objDatRec In objDatTab.Rows
                        i = i + 1
                        PO_QTY = Mid(objDatTab.Rows(i)("WA"), objFldTab(1, "OFFSET") + 1, objFldTab(1, "LENGTH")) / 1000
                        GR_QTY = Mid(objDatTab.Rows(i)("WA"), objFldTab(2, "OFFSET") + 1, objFldTab(2, "LENGTH")) / 1000
                    Next

                End If

            End If

            DGV.Rows(n).Cells(3).Value = (PO_QTY - GR_QTY)

        Next


        'objFldTab.Rows.RemoveAll()
        'objOptTab.Rows.RemoveAll()
        'objDatTab.Rows.RemoveAll()

        objRfcFunc = Nothing
        objQueryTab = Nothing
        objOptTab = Nothing
        objFldTab = Nothing
        objDatTab = Nothing
        SAP_RFC = Nothing


        '// HAPUS YG SDH DELIVERY COMPLETE

        For n = 0 To DGV.Rows.Count - 1
            If n = DGV.Rows.Count Then Exit For
            If DGV.Rows(n).Cells(3).Value <= 0 Then
                Me.DGV.Rows.RemoveAt(n)
                n = n - 1
            End If
        Next


    End Sub

    Private Sub BATAL()

        txtPO.Text = ""
        lblDelv.Text = ""
        lblVendor.Text = ""
        dtpRecvDate.Value = Format(Now, "dd/MM/yyyy")
        dtpPostDate.Value = Format(Now, "dd/MM/yyyy")
        txtDelNote.Text = ""

        DGV.Rows.Clear()

        txtPO.Enabled = True
        btnFindPO.Enabled = True
        dtpPostDate.Enabled = True

        txtPO.Select()

        If GR_NO = "" Then

            Query = "DELETE FROM [" & TEMPRECV & "] "

            CMD = New SqlCommand
            CMD.Connection = Conn
            CMD.CommandText = Query
            CMD.ExecuteNonQuery()

        End If

    End Sub


    Private Sub FrmGR_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Call LOCAL_CONNECT()

        Call HOST_CONNECT()

        Call GET_PERIOD()

        DGV.BorderStyle = BorderStyle.Fixed3D
        'DGV.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249)
        'DGV.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
        DGV.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise
        DGV.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke
        DGV.BackgroundColor = Color.White

        DGV.EnableHeadersVisualStyles = False
        'DGV.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        DGV.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72)
        DGV.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        DGV.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72)
        'DGV.RowHeadersVisible = False

        DGV.Columns(0).HeaderText = "NO"
        DGV.Columns(1).HeaderText = "PRODUCT #"
        DGV.Columns(2).HeaderText = "DESCRIPTION"
        DGV.Columns(3).HeaderText = "ORDERED"
        DGV.Columns(4).HeaderText = "UNIT"
        DGV.Columns(5).HeaderText = "RECV. IN ROLL/PCS"
        DGV.Columns(6).HeaderText = "RECV. IN YDS/MTR"
        DGV.Columns(7).HeaderText = "LOCATION"

        DGV.Columns(0).Width = 50
        DGV.Columns(1).Width = 145
        DGV.Columns(2).Width = 300
        DGV.Columns(3).Width = 90
        DGV.Columns(4).Width = 70
        DGV.Columns(5).Width = 90
        DGV.Columns(6).Width = 90
        DGV.Columns(7).Width = 80
        DGV.Columns(8).Width = 35
        DGV.Columns(9).Width = 0
        DGV.Columns(10).Width = 0

        DGV.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

        DGV.Columns(0).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DGV.Columns(1).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DGV.Columns(2).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DGV.Columns(3).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DGV.Columns(4).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DGV.Columns(5).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DGV.Columns(6).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DGV.Columns(7).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

        DGV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        DGV.ColumnHeadersHeight = 40

        DGV.Columns(3).DefaultCellStyle.Format = "N2"
        DGV.Columns(5).DefaultCellStyle.Format = "N0"
        DGV.Columns(6).DefaultCellStyle.Format = "N2"

        DGV.Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        DGV.Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DGV.Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DGV.Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        Column8.Items.Add(SLOC)


        If GR_NO = "" Then

            '=====================
            '  CREATE TEMP TABLE
            '=====================

Ulang:
            TEMPRECV = "TMPGR" & Format(NO, "00")

            Try

                Query = "SELECT * FROM [" & TEMPRECV & "] "

                DS = New DataSet
                DA = New SqlDataAdapter(Query, Conn)
                DA.Fill(DS, "GR")

                If DS.Tables("GR").Rows.Count > 0 Then
                    NO = NO + 1
                    GoTo Ulang
                End If

            Catch ex As Exception

                Query = "SELECT * INTO [" & TEMPRECV & "] FROM [STOCKBCD] WHERE 1=2 "

                CMD = New SqlCommand
                CMD.Connection = Conn
                CMD.CommandText = Query
                CMD.ExecuteNonQuery()

                GoTo Ulang

            End Try

            Call BATAL()

        Else

            Query = "SELECT * FROM [TRANGRH] WHERE [GRNO]='" & GR_NO & "'"

            DS = New DataSet
            DA = New SqlDataAdapter(Query, Conn)
            DA.Fill(DS, "GR_H")

            If DS.Tables("GR_H").Rows.Count = 1 Then

                txtPO.Text = Trim(DS.Tables("GR_H").Rows(0)("pono"))
                lblDelv.Text = Trim(DS.Tables("GR_H").Rows(0)("delvno"))
                lblVendor.Text = Trim(DS.Tables("GR_H").Rows(0)("nmsupl"))
                dtpRecvDate.Value = Trim(DS.Tables("GR_H").Rows(0)("grdate"))
                dtpPostDate.Value = Trim(DS.Tables("GR_H").Rows(0)("podate"))
                txtDelNote.Text = Trim(DS.Tables("GR_H").Rows(0)("notes"))

                txtPO.Enabled = IIf(EditOrDisplay = "Edit", True, False)
                btnFindPO.Enabled = IIf(EditOrDisplay = "Edit", True, False)
                dtpRecvDate.Enabled = IIf(EditOrDisplay = "Edit", True, False)
                dtpPostDate.Enabled = IIf(EditOrDisplay = "Edit", True, False)
                txtDelNote.Enabled = IIf(EditOrDisplay = "Edit", True, False)


                Query = "SELECT * FROM [TRANGRD] WHERE [GRNO]='" & GR_NO & "' ORDER BY [ItemNo] "

                DS = New DataSet
                DA = New SqlDataAdapter(Query, Conn)
                DA.Fill(DS, "GR_D")

                For n = 0 To DS.Tables("GR_D").Rows.Count - 1

                    n = DGV.Rows.Add()
                    DGV.Rows(n).Cells(0).Value = Val(DS.Tables("GR_D").Rows(n)("itemno"))
                    DGV.Rows(n).Cells(1).Value = DS.Tables("GR_D").Rows(n)("kdprod")
                    DGV.Rows(n).Cells(2).Value = DS.Tables("GR_D").Rows(n)("nmprod")
                    DGV.Rows(n).Cells(3).Value = DS.Tables("GR_D").Rows(n)("qtyorder")
                    DGV.Rows(n).Cells(4).Value = DS.Tables("GR_D").Rows(n)("unit")
                    DGV.Rows(n).Cells(5).Value = DS.Tables("GR_D").Rows(n)("qtyrecv1")
                    DGV.Rows(n).Cells(6).Value = DS.Tables("GR_D").Rows(n)("qtyrecv2")
                    DGV.Rows(n).Cells(7).Value = DS.Tables("GR_D").Rows(n)("sloc")

                Next n

                For n = 0 To 8
                    DGV.Columns(n).ReadOnly = IIf(EditOrDisplay = "Edit", False, True)
                Next n


                btnBarcode.Enabled = IIf(EditOrDisplay = "Edit", True, False)
                btnGRDelv.Enabled = IIf(EditOrDisplay = "Edit", True, False)
                btnSave.Enabled = IIf(EditOrDisplay = "Edit", True, False)
                btnCancel.Enabled = IIf(EditOrDisplay = "Edit", True, False)


            End If

        End If




    End Sub

    Private Sub txtPO_GotFocus(sender As Object, e As EventArgs) Handles txtPO.GotFocus
        txtPO.BackColor = Color.FromArgb(254, 240, 158)
    End Sub

    Private Sub txtPO_KeyDown(sender As Object, e As KeyEventArgs) Handles txtPO.KeyDown
        If e.KeyCode = Keys.Enter Then
            Call btnFindPO_Click(txtPO, e)
        End If
    End Sub

    Private Sub txtPO_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPO.KeyPress
        If Not Char.IsNumber(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtPO_LostFocus(sender As Object, e As EventArgs) Handles txtPO.LostFocus
        txtPO.BackColor = Color.White
    End Sub

    Private Sub btnFindPO_Click(sender As Object, e As EventArgs) Handles btnFindPO.Click

        If Trim(txtPO.Text) = "" Then
            txtPO.Select()
            Exit Sub
        End If


        '=============
        '  PO HEADER  
        '=============

        Query = "SELECT nopo,kdsupl,nmsupl FROM [TRANPOH] WHERE [NoPO]='" & txtPO.Text & "'"

        DS = New DataSet
        DA = New SqlDataAdapter(Query, Conn)
        DA.Fill(DS, "POH")

        If DS.Tables("POH").Rows.Count <> 1 Then

            MsgBox("PO " & txtPO.Text & " does not exist ! ", vbCritical, "Error")

            txtPO.Text = ""
            txtPO.Select()
            Exit Sub

        End If

        Me.Cursor = Cursors.WaitCursor


        KDSUPL = DS.Tables("POH").Rows(0)("kdsupl")
        NMSUPL = DS.Tables("POH").Rows(0)("nmsupl")

        lblVendor.Text = NMSUPL

        Call BAPI_PO_GETDETAIL()

        Call DELIVERY_SCHEDULE()

        '=============
        '  PO DETAIL
        '=============

        'Query = "SELECT itemno,kdprod,nmprod,nmwarna,(qty-received) AS qty,unit FROM [TRANPOD] WHERE  [NoPO]='" & txtPO.Text & "'"

        'DS = New DataSet
        'DA = New SqlDataAdapter(Query, Conn)
        'DA.Fill(DS, "POD")

        'Dim n As Integer

        'For i = 1 To DS.Tables("POD").Rows.Count

        '    n = DGV.Rows.Add()

        '    DGV.Rows(n).Cells(0).Value = i
        '    DGV.Rows(n).Cells(1).Value = DS.Tables("POD").Rows(0)("kdprod")
        '    DGV.Rows(n).Cells(2).Value = DS.Tables("POD").Rows(0)("nmprod")
        '    DGV.Rows(n).Cells(3).Value = DS.Tables("POD").Rows(0)("qty")
        '    DGV.Rows(n).Cells(4).Value = DS.Tables("POD").Rows(0)("unit")
        '    DGV.Rows(n).Cells(7).Value = SLOC

        '    DGV.Rows(n).Cells(0).Style.BackColor = Color.FromArgb(225, 225, 225)
        '    DGV.Rows(n).Cells(1).Style.BackColor = Color.FromArgb(225, 225, 225)
        '    DGV.Rows(n).Cells(2).Style.BackColor = Color.FromArgb(225, 225, 225)
        '    DGV.Rows(n).Cells(3).Style.BackColor = Color.FromArgb(225, 225, 225)
        '    DGV.Rows(n).Cells(4).Style.BackColor = Color.FromArgb(225, 225, 225)

        'Next

        'DGV.CurrentCell = DGV(5, 0)


        btnFindPO.Enabled = False
        txtPO.Enabled = False


        If InStr(UCase(lblVendor.Text), "TRISULA TEXTILE") > 0 Then

            For i = 0 To DGV.RowCount - 1
                DGV.Rows(i).Cells(5).ReadOnly = True
                DGV.Rows(i).Cells(6).ReadOnly = True
            Next

        End If


        dtpRecvDate.Select()

        Me.Cursor = Cursors.Default


    End Sub

    Private Sub txtDelNote_GotFocus(sender As Object, e As EventArgs) Handles txtDelNote.GotFocus
        txtDelNote.BackColor = Color.FromArgb(254, 240, 158)
    End Sub

    Private Sub txtDelNote_KeyDown(sender As Object, e As KeyEventArgs) Handles txtDelNote.KeyDown
        If e.KeyCode = Keys.Enter Then
            DGV.Select()
        End If
    End Sub

    Private Sub txtDelNote_LostFocus(sender As Object, e As EventArgs) Handles txtDelNote.LostFocus
        txtDelNote.BackColor = Color.White
    End Sub

    Private Sub txtDelNote_TextChanged(sender As Object, e As EventArgs) Handles txtDelNote.TextChanged

    End Sub

    Private Sub dtpRecvDate_KeyDown(sender As Object, e As KeyEventArgs) Handles dtpRecvDate.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtDelNote.Select()
        End If
    End Sub

    Private Sub DGV_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DGV.CellEndEdit

        Dim Col As Integer = e.ColumnIndex
        Dim Row As Integer = e.RowIndex

        Select Case Col

            Case Is = 5

                DGV.Rows(Row).Cells(Col).Value = DGV.Rows(Row).Cells(Col).Value * 1

                If e.RowIndex < DGV.Rows.Count - 1 Then
                    SendKeys.Send("{up}")
                End If


            Case Is = 6

                DGV.Rows(Row).Cells(Col).Value = DGV.Rows(Row).Cells(Col).Value * 1

                If e.RowIndex < DGV.Rows.Count - 1 Then
                    SendKeys.Send("{up}")
                End If

        End Select





    End Sub

    Private Sub DGV_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles DGV.EditingControlShowing

        Dim Row As Integer = DGV.CurrentCell.RowIndex
        Dim Col As Integer = DGV.CurrentCell.ColumnIndex


        '// uppercase textbox

        If TypeOf e.Control Is TextBox Then
            DirectCast(e.Control, TextBox).CharacterCasing = CharacterCasing.Upper
        End If

        '// numeric textbox

        If Col >= 5 And Col <= 6 Then
            AddHandler CType(e.Control, TextBox).KeyPress, AddressOf TextBoxNum_keyPress
        End If

    End Sub

    Private Sub TextBoxNum_keyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs)

        Dim Col As Integer = DGV.CurrentCell.ColumnIndex
        Dim Row As Integer = DGV.CurrentCell.RowIndex

        If Col >= 5 Or Col <= 6 Then

            If Not (Char.IsDigit(CChar(CStr(e.KeyChar))) Or e.KeyChar = Chr(8)) And Not e.KeyChar = "," Then
                e.Handled = True
            End If

        End If

    End Sub

    Private Sub DGV_KeyDown(sender As Object, e As KeyEventArgs) Handles DGV.KeyDown
        ' Disable enter key to move to next row
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click

        If DGV.Rows.Count > 1 And GR_NO = "" Then
            If MessageBox.Show("Do you really want to cancel this transaction ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.No Then
                Exit Sub
            End If

        End If

        If GR_NO = "" Then
            Call BATAL()
        End If

        SaveStatus = False

        Me.Dispose()

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click

        Try
            If MessageBox.Show("Do you really want to cancel this transaction ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Yes Then
                Call BATAL()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        '// check posting period

        Dim CurrPeriod As Long = Val(CurYearPeriode & CurMonPeriode)
        Dim PrevPeriod As Long = Val(PrevYearPeriode & PrevMonPeriode)

        Dim PostDate As Long = Val(Mid(Format(dtpPostDate.Value, "yyyyMMdd"), 1, 6))

        'If PostDate < PrevPeriod Or PostDate > CurrPeriod Then
        '    MsgBox("Posting only possible in periods " & PrevMonPeriode & "-" & PrevYearPeriode & " and " & CurMonPeriode & "-" & CurYearPeriode & " ! ", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Warning")
        '    dtpPostDate.Focus()
        '    Exit Sub
        'End If


        If DGV.Rows.Count <= 0 Then
            MessageBox.Show("Please enter purchase order !", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtPO.Select()
            Exit Sub
        End If


        '// check received quantity if exceeded from ordered quantity

        Dim OrderQty As Double = 0
        Dim RecQtyPc As Integer = 0
        Dim RecQtyYd As Double = 0
        Dim n As Integer = 0

        For n = 0 To DGV.Rows.Count - 1
            If DGV.Rows(n).Cells(8).Value = True Then
                Exit For
            End If
        Next


        If n > DGV.Rows.Count - 1 Then
            MessageBox.Show("You have no flagged any items as OK !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            DGV.Select()
            DGV.CurrentCell = DGV(8, 0)
            Exit Sub
        End If


        For n = 0 To DGV.Rows.Count - 1

            If DGV.Rows(n).Cells(8).Value = True Then

                OrderQty = DGV.Rows(n).Cells(3).Value
                RecQtyPc = DGV.Rows(n).Cells(5).Value
                RecQtyYd = DGV.Rows(n).Cells(6).Value
                OVERDELTOL = DGV.Rows(n).Cells(9).Value
                UNLIMITED = DGV.Rows(n).Cells(10).Value

                'If RecQtyPc = 0 Then
                '    MessageBox.Show("Please enter received quantity in roll/piece !", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                '    DGV.Select()
                '    DGV.CurrentCell = DGV(5, n)
                '    Exit Sub
                'End If

                If RecQtyYd = 0 Then
                    MessageBox.Show("Please enter received quantity in meter/yard !", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    DGV.Select()
                    DGV.CurrentCell = DGV(6, n)
                    Exit Sub
                End If

                If UNLIMITED <> "X" Then

                    If (RecQtyYd - OrderQty) > Math.Round(OrderQty * OVERDELTOL / 100, 2) Then  '  15%
                        MessageBox.Show("Item " & Trim(Str(n + 1)) & " received quantity exceeded " & Trim(Str(RecQtyYd - OrderQty)) & " Yds", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        DGV.Select()
                        DGV.CurrentCell = DGV(6, n)
                        Exit Sub
                    End If

                End If

            End If

        Next


        If MessageBox.Show("Save this transaction ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        Me.Cursor = Cursors.WaitCursor



        If PostDate < PrevPeriod Or PostDate > CurrPeriod Then

            Query = "SELECT TOP 1 [GRNO] FROM [TRANGRH] WHERE [GRNO] LIKE '" & "M" & Format(Now, "yyyyMM") & "%' ORDER BY [GRNO] DESC "

            DS = New DataSet
            DA = New SqlDataAdapter(Query, Conn)
            DA.Fill(DS, "GR")

            If DS.Tables("GR").Rows.Count = 0 Then
                MATDOC = "M" & Format(Now, "yyyyMM") & "001"
            Else
                MATDOC = DS.Tables("GR").Rows(0)("GRNO")
                MATDOC = Mid(MATDOC, 1, 7) & Format(Val(Mid(MATDOC, 8, 3)) + 1, "000")
            End If

            SaveOkay = True

        Else
            Call BAPI_GOODSMVT_CREATE()
        End If


        If SaveOkay Then

            Try

                Call LOCAL_CONNECT()


                Query = "UPDATE [" & TEMPRECV & "] SET [GR]='" & MATDOC & "'"

                CMD = New SqlCommand
                CMD.Connection = Conn
                CMD.CommandText = Query
                CMD.ExecuteNonQuery()


                Query = "INSERT INTO [TRANGRH] (grno, grdate, pono, podate, kdsupl, nmsupl, delvno, notes) " & _
                        "VALUES ('" & MATDOC & "','" & DATECHAR(dtpRecvDate.Value) & "','" & txtPO.Text & "'," & _
                                "'" & CHARDATE(PODATE) & "','" & KDSUPL & "','" & PETIK(NMSUPL) & "'," & _
                                "'" & lblDelv.Text & "','" & txtDelNote.Text & "')"

                CMD = New SqlCommand
                CMD.Connection = Conn
                CMD.CommandText = Query
                CMD.ExecuteNonQuery()


                For n = 0 To DGV.Rows.Count - 1

                    If DGV.Rows(n).Cells(8).Value = True Then

                        Query = "INSERT INTO [TRANGRD] (grno, itemno, kdprod, nmprod, qtyorder, unit, qtyrecv1, qtyrecv2, sloc) " & _
                                "VALUES ('" & MATDOC & "','" & DGV.Rows(n).Cells(0).Value & "','" & DGV.Rows(n).Cells(1).Value & "'," & _
                                        "'" & PETIK(DGV.Rows(n).Cells(2).Value) & "'," & Replace(DGV.Rows(n).Cells(3).Value, ",", ".") & "," & _
                                        "'" & DGV.Rows(n).Cells(4).Value & "'," & DGV.Rows(n).Cells(5).Value & "," & _
                                        " " & Replace(DGV.Rows(n).Cells(6).Value, ",", ".") & ",'" & DGV.Rows(n).Cells(7).Value & "')"

                        CMD = New SqlCommand
                        CMD.Connection = Conn
                        CMD.CommandText = Query
                        CMD.ExecuteNonQuery()


                        '// UPDATE STOCK

                        Query = "UPDATE [MASPROD] " & _
                                   "SET [stock1] = [stock1] + " & DGV.Rows(n).Cells(5).Value & "," & _
                                      " [stock2] = [stock2] + " & Replace(DGV.Rows(n).Cells(6).Value, ",", ".") & " " & _
                                 "WHERE [kdprod] = '" & DGV.Rows(n).Cells(1).Value & "'"

                        CMD = New SqlCommand
                        CMD.Connection = Conn
                        CMD.CommandText = Query
                        CMD.ExecuteNonQuery()


                        '// UPDATE PO DETAIL

                        Query = "UPDATE [TRANPOD] " & _
                                   "SET [received] = [received] + " & Replace(DGV.Rows(n).Cells(6).Value, ",", ".") & " " & _
                                 "WHERE [nopo]='" & txtPO.Text & "' AND [itemno]='" & DGV.Rows(n).Cells(0).Value & "'"

                        CMD = New SqlCommand
                        CMD.Connection = Conn
                        CMD.CommandText = Query
                        CMD.ExecuteNonQuery()

                    End If

                Next

                '=================
                ' UPDATE BAR CODE
                '=================

                Query = "INSERT INTO [STOCKBCD] SELECT * FROM [" & TEMPRECV & "] "      ' WHERE [po]='" & txtPO.Text & "'"

                CMD = New SqlCommand
                CMD.Connection = Conn
                CMD.CommandText = Query
                CMD.ExecuteNonQuery()       '<==== ERROR


                '============
                ' STOCK CARD
                '============

                Query = "SELECT * FROM [" & TEMPRECV & "] "

                DS = New DataSet
                DA = New SqlDataAdapter(Query, Conn)
                DA.Fill(DS, "RECVBAR")

                For i = 1 To DS.Tables("RECVBAR").Rows.Count

                    Query = "INSERT INTO [STOCKCARD] (postdate,po,barcode,corak,warna,grade,partai,lebar,yard,meter,unit,sloc,gr) " & _
                            "VALUES ('" & DATECHAR(dtpRecvDate.Value) & "','" & txtPO.Text & "'," & _
                                    "'" & DS.Tables("RECVBAR").Rows(i - 1)("BARCODE") & "','" & DS.Tables("RECVBAR").Rows(i - 1)("CORAK") & "'," & _
                                    "'" & DS.Tables("RECVBAR").Rows(i - 1)("WARNA") & "','" & DS.Tables("RECVBAR").Rows(i - 1)("GRADE") & "'," & _
                                    "'" & DS.Tables("RECVBAR").Rows(i - 1)("PARTAI") & "','" & DS.Tables("RECVBAR").Rows(i - 1)("LEBAR") & "'," & _
                                    " " & Replace(DS.Tables("RECVBAR").Rows(i - 1)("YARD"), ",", ".") & "," & _
                                    " " & Replace(DS.Tables("RECVBAR").Rows(i - 1)("METER"), ",", ".") & "," & _
                                    "'" & DS.Tables("RECVBAR").Rows(i - 1)("UNIT") & "','" & DS.Tables("RECVBAR").Rows(i - 1)("SLOC") & "'," & _
                                    "'" & DS.Tables("RECVBAR").Rows(i - 1)("GR") & "')"

                    CMD = New SqlCommand
                    CMD.Connection = Conn
                    CMD.CommandText = Query
                    CMD.ExecuteNonQuery()

                Next


                Me.Cursor = Cursors.Default


                '=====================
                ' PRINT GOODS RECEIPT
                '=====================

                If MessageBox.Show("Print Goods Receipt Note ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = vbYes Then
                    Call PRINT_GR_NOTE()
                End If


                Call BATAL()

                'SaveStatus = True
                'Me.Dispose()


            Catch ex As Exception
                Me.Cursor = Cursors.Default
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try


        Else
            Me.Cursor = Cursors.Default
            MessageBox.Show("Could not save the document to SAP ! ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If



    End Sub

    Private Sub btnBarcode_Click_1(sender As Object, e As EventArgs) Handles btnBarcode.Click

        If Trim(txtPO.Text) = "" Then
            MessageBox.Show("Please enter Purchase Order first !", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtPO.Select()
            Exit Sub
        End If

        If DGV.Rows.Count = 0 Then
            MessageBox.Show("No item found !", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtPO.Select()
            Exit Sub
        End If


        '// LOAD BARCODE FORM //

        With FrmGRBarcode
            .TMPGRBCD = TEMPRECV
            .lblPO.Text = txtPO.Text
            .ShowDialog()
        End With


        Try
            Query = "SELECT corak,COUNT(corak) AS roll,SUM(yard) AS yard,SUM(meter) AS meter FROM [" & TEMPRECV & "] GROUP BY [corak] "

            ' "WHERE [po]='" & txtPO.Text & "' 

            DS = New DataSet
            DA = New SqlDataAdapter(Query, Conn)
            DA.Fill(DS, "BARCODE")

            For n = 1 To DS.Tables("BARCODE").Rows.Count

                For i = 1 To DGV.Rows.Count

                    If DGV.Rows(i - 1).Cells(1).Value = DS.Tables("BARCODE").Rows(n - 1)("corak") Then

                        DGV.Rows(i - 1).Cells(5).Value = DS.Tables("BARCODE").Rows(n - 1)("roll")

                        If DGV.Rows(i - 1).Cells(4).Value = "YD" Then
                            DGV.Rows(i - 1).Cells(6).Value = DS.Tables("BARCODE").Rows(n - 1)("yard")
                        Else
                            DGV.Rows(i - 1).Cells(6).Value = DS.Tables("BARCODE").Rows(n - 1)("meter")
                        End If

                        DGV.Rows(i - 1).Cells(8).Value = True

                        DGV.CurrentCell = DGV(5, i - 1)

                    End If

                Next

            Next

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub btnGRDelv_Click(sender As Object, e As EventArgs) Handles btnGRDelv.Click

        If Trim(txtPO.Text) = "" Then
            MessageBox.Show("Please enter Purchase Order first !", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtPO.Select()
            Exit Sub
        End If

        If DGV.Rows.Count = 0 Then
            MessageBox.Show("No item found !", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtPO.Select()
            Exit Sub
        End If


        With FrmGRDelivery
            .TMPFILEGR = TEMPRECV
            .lblPO.Text = txtPO.Text
            '.txtDelvNo.Text = lblDelv.Text
            .ShowDialog()
        End With



        Dim ada As Boolean = False


        For n = 1 To DGV.Rows.Count
            DGV.Rows(n - 1).Cells(5).Value = ""
            DGV.Rows(n - 1).Cells(6).Value = ""
        Next


        Try

            Query = "SELECT corak, COUNT(corak) AS roll, SUM(x) AS yd_x FROM [" & TEMPRECV & "] " & _
                    "WHERE  [po]='" & txtPO.Text & "' AND [x]>0 GROUP BY [corak] "

            DS = New DataSet
            DA = New SqlDataAdapter(Query, Conn)
            DA.Fill(DS, "BARCODE")

            For n = 1 To DS.Tables("BARCODE").Rows.Count

                For i = 1 To DGV.Rows.Count

                    If DGV.Rows(i - 1).Cells(1).Value = Replace(DS.Tables("BARCODE").Rows(n - 1)("corak"), ".B.", ".X.") Then

                        DGV.Rows(i - 1).Cells(5).Value = DS.Tables("BARCODE").Rows(n - 1)("roll")
                        DGV.Rows(i - 1).Cells(6).Value = DS.Tables("BARCODE").Rows(n - 1)("yd_x")
                        DGV.Rows(i - 1).Cells(8).Value = True

                        DGV.CurrentCell = DGV(5, i - 1)

                    End If

                Next
            Next



            Query = "SELECT corak, COUNT(corak) AS roll, SUM(yard-x) AS yard, SUM(meter) AS meter FROM [" & TEMPRECV & "] " & _
                    "WHERE  [po]='" & txtPO.Text & "' GROUP BY [corak] "

            DS = New DataSet
            DA = New SqlDataAdapter(Query, Conn)
            DA.Fill(DS, "BARCODE")

            For n = 1 To DS.Tables("BARCODE").Rows.Count

                For i = 1 To DGV.Rows.Count

                    If DGV.Rows(i - 1).Cells(1).Value = DS.Tables("BARCODE").Rows(n - 1)("corak") Then

                        ada = True

                        DGV.Rows(i - 1).Cells(5).Value = DS.Tables("BARCODE").Rows(n - 1)("roll")

                        If DGV.Rows(i - 1).Cells(4).Value = "YD" Then
                            DGV.Rows(i - 1).Cells(6).Value = DS.Tables("BARCODE").Rows(n - 1)("yard")
                        Else
                            DGV.Rows(i - 1).Cells(6).Value = DS.Tables("BARCODE").Rows(n - 1)("meter")
                        End If

                        DGV.Rows(i - 1).Cells(8).Value = True

                        DGV.CurrentCell = DGV(5, i - 1)

                    End If

                Next

            Next


            ' GR date agen == delivery date TTI

            If ada = True Then

                Query = "SELECT sj, tglsj FROM [" & TEMPRECV & "] WHERE [po]='" & txtPO.Text & "' GROUP BY [sj], [tglsj] "

                DS = New DataSet
                DA = New SqlDataAdapter(Query, Conn)
                DA.Fill(DS, "BARCODE")

                If DS.Tables("BARCODE").Rows.Count > 0 Then
                    lblDelv.Text = DS.Tables("BARCODE").Rows(0)("sj")                                   ' delivery no   TTI
                    dtpPostDate.Value = Format(DS.Tables("BARCODE").Rows(0)("tglsj"), "dd/MM/yyyy")     ' dalivery date TTI
                    dtpRecvDate.Value = dtpPostDate.Value
                End If

            End If



        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnCreateBarcode.Click

        If Trim(txtPO.Text) = "" Then
            MessageBox.Show("Please enter Purchase Order first !", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtPO.Select()
            Exit Sub
        End If

        If DGV.Rows.Count = 0 Then
            MessageBox.Show("No item found !", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtPO.Select()
            Exit Sub
        End If


        With FrmGRNewBarcode
            .TMPGRBCD = TEMPRECV
            .ShowDialog()
        End With


        Try
            Query = "SELECT corak,COUNT(corak) AS roll,SUM(yard) AS yard,SUM(meter) AS meter FROM [" & TEMPRECV & "] GROUP BY [corak] "


            DS = New DataSet
            DA = New SqlDataAdapter(Query, Conn)
            DA.Fill(DS, "BARCODE")

            For n = 0 To DS.Tables("BARCODE").Rows.Count - 1

                For i = 0 To DGV.Rows.Count - 1

                    If DGV.Rows(i).Cells(1).Value = DS.Tables("BARCODE").Rows(n)("corak") Then

                        DGV.Rows(i).Cells(5).Value = DS.Tables("BARCODE").Rows(n)("roll")

                        If DGV.Rows(i).Cells(4).Value = "YD" Then
                            DGV.Rows(i).Cells(6).Value = DS.Tables("BARCODE").Rows(n)("yard")
                        Else
                            DGV.Rows(i).Cells(6).Value = DS.Tables("BARCODE").Rows(n)("meter")
                        End If

                        DGV.Rows(i).Cells(8).Value = True

                        DGV.CurrentCell = DGV(5, i)

                    End If

                Next

            Next

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try


    End Sub

    Private Sub txtPO_TextChanged(sender As Object, e As EventArgs) Handles txtPO.TextChanged

    End Sub
End Class