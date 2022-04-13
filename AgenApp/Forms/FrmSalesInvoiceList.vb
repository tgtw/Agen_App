Imports System.Data.SqlClient
Imports Microsoft.Reporting.WinForms


Public Class FrmSalesInvoiceList

    Dim Indeks As Integer = 0
    Dim SaveOkay As Boolean = False
    Dim DeliveryNo As String = ""
    Dim InvoiceNo As String = ""
    Dim InvoiceDate As String = ""
    Dim FilterBy As String = ""

    Private Sub BAPI_BILLINGDOC_CANCEL1()

        SaveOkay = False

        Dim oFuncCtrl As Object
        Dim oTheFunc As Object
        Dim oFuncCommit As Object

        Dim oBILLINGDOCUMENT As Object

        Dim oRETURN As Object
        Dim oSUCCESS As Object


        oFuncCtrl = CreateObject("SAP.Functions")
        oFuncCtrl.Connection = SAPConn

        oTheFunc = oFuncCtrl.Add("BAPI_BILLINGDOC_CANCEL1")

        oBILLINGDOCUMENT = oTheFunc.Exports.Item("BILLINGDOCUMENT")

        oRETURN = oTheFunc.tables.Item("RETURN")
        oSUCCESS = oTheFunc.tables.Item("SUCCESS")


        '===================
        ' IMPORT PARAMETERS 
        '===================

        oBILLINGDOCUMENT.Value = InvoiceNo


        If oTheFunc.Call Then

            oFuncCommit = oFuncCtrl.Add("BAPI_TRANSACTION_COMMIT")

            If oFuncCommit.Call Then

                For n = 1 To oSUCCESS.RowCount

                    InvoiceNo = oSUCCESS.Rows(n)("SUCCESS")

                    Exit For
                Next

                SaveOkay = True

            End If

        End If


    End Sub

    Private Sub BAPI_BILLINGDOC_CREATEMULTIPLE()

        'Call LogToSAP(True)

        'If SAPConStatus = False Then Exit Sub

        SaveOkay = False

        Dim oFuncCtrl As Object
        Dim oTheFunc As Object
        Dim oFuncCommit As Object

        Dim oBILLINGDATAIN As Object
        Dim oCONDITIONDATAIN As Object
        Dim oRETURN As Object
        Dim oSUCCESS As Object


        oFuncCtrl = CreateObject("SAP.Functions")
        oFuncCtrl.Connection = SAPConn

        oTheFunc = oFuncCtrl.Add("BAPI_BILLINGDOC_CREATEMULTIPLE")


        oBILLINGDATAIN = oTheFunc.tables.Item("BILLINGDATAIN")
        oCONDITIONDATAIN = oTheFunc.tables.Item("CONDITIONDATAIN")
        oRETURN = oTheFunc.tables.Item("RETURN")
        oSUCCESS = oTheFunc.tables.Item("SUCCESS")



        '========
        ' TABLES
        '========

        oBILLINGDATAIN.Rows.Add()
        oBILLINGDATAIN.Value(1, "REF_DOC") = DeliveryNo
        oBILLINGDATAIN.Value(1, "REF_DOC_CA") = "J"

        'oSUCCESS.Rows.Add()
        'oSUCCESS.Value(1, "REF_DOC") = DeliveryNo
        'oSUCCESS.Value(1, "BILL_DOC") = InvoiceNo


        'For n = 1 To DGV.Rows.Count

        '    If IsNothing(DGV.Rows(n - 1).Cells(1).Value) Then
        '        Exit For
        '    End If

        '    oCONDITIONDATAIN.Rows.Add()
        '    oCONDITIONDATAIN.Value(n, "DATA_INDEX") = n
        '    oCONDITIONDATAIN.Value(n, "COND_TYPE") = "ZGSP"
        '    oCONDITIONDATAIN.Value(n, "COND_VALUE") = DGV.Rows(n - 1).Cells(7).Value / 1000    ' SALES PRICE
        '    oCONDITIONDATAIN.Value(n, "COND_CURR") = "IDR"

        'Next


        If oTheFunc.Call Then

            oFuncCommit = oFuncCtrl.Add("BAPI_TRANSACTION_COMMIT")

            If oFuncCommit.Call Then

                For n = 1 To oSUCCESS.RowCount
                    InvoiceNo = oSUCCESS.Rows(n)("BILL_DOC")
                    Exit For
                Next

                If Trim(InvoiceNo) <> "" Then
                    SaveOkay = True
                End If

            End If

        End If



        oFuncCtrl = Nothing
        oTheFunc = Nothing
        oFuncCommit = Nothing
        oBILLINGDATAIN = Nothing
        oCONDITIONDATAIN = Nothing
        oRETURN = Nothing
        oSUCCESS = Nothing


    End Sub

    Private Sub CHANGE_ACTUAL_GI()

        'Call LogToSAP(True)

        'If SAPConStatus = False Then Exit Sub

        SaveOkay = False

        Dim oFuncCtrl As Object
        Dim objRfcFunc As Object
        Dim oTCODE As Object
        Dim oMODE As Object
        Dim oBT_DATA As Object
        Dim oL_ERRORS As Object
        Dim oMESS As Object
        Dim BDC_TABLE As Object

        Dim Pesan As String

    
        oFuncCtrl = CreateObject("SAP.Functions")
        oFuncCtrl.Connection = SAPConn

        objRfcFunc = oFuncCtrl.Add("RFC_CALL_TRANSACTION")

        objRfcFunc.exports("TRANCODE") = "VL02N"
        objRfcFunc.exports("UPDMODE") = "S"

        BDC_TABLE = objRfcFunc.Tables("BDCTABLE")

        Indeks = 0

        ADD_BDCDATA(BDC_TABLE, "SAPMV50A", "4004", "X", "", "")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "BDC_CURSOR", "LIKP-VBELN")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "BDC_OKCODE", "/00")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "LIKP-VBELN", DeliveryNo)

        ADD_BDCDATA(BDC_TABLE, "SAPMV50A", "1000", "X", "", "")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "BDC_OKCODE", "/00")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "BDC_CURSOR", "LIKP-WADAT_IST")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "LIKP-WADAT_IST", InvoiceDate)

        ADD_BDCDATA(BDC_TABLE, "SAPMV50A", "1000", "X", "", "")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "BDC_OKCODE", "=WABU_T")


        If objRfcFunc.Call = True Then

            oMESS = objRfcFunc.Imports("MESSG")
            Pesan = oMESS.Value("MSGTX")

            If InStr(Pesan, "saved") > 0 Then
                SaveOkay = True

            End If

            MessageBox.Show(Pesan, "SAP Message", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Else
            MsgBox(Err.Description, MsgBoxStyle.Critical, "Error")
        End If


        oFuncCtrl = Nothing
        objRfcFunc = Nothing
        oTCODE = Nothing
        oMODE = Nothing
        oBT_DATA = Nothing
        oL_ERRORS = Nothing
        oMESS = Nothing


    End Sub

    Private Sub POST_GOODS_ISSUE()

        'Call LogToSAP(True)

        'If SAPConStatus = False Then Exit Sub

        SaveOkay = False

        Dim oFuncCtrl As Object
        Dim objRfcFunc As Object
        Dim oTCODE As Object
        Dim oMODE As Object
        Dim oBT_DATA As Object
        Dim oL_ERRORS As Object
        Dim oMESS As Object
        Dim BDC_TABLE As Object

        Dim Pesan As String


        oFuncCtrl = CreateObject("SAP.Functions")
        oFuncCtrl.Connection = SAPConn

        objRfcFunc = oFuncCtrl.Add("RFC_CALL_TRANSACTION")

        objRfcFunc.exports("TRANCODE") = "VL02N"
        objRfcFunc.exports("UPDMODE") = "S"


        Indeks = 0

        BDC_TABLE = objRfcFunc.Tables("BDCTABLE")

        ADD_BDCDATA(BDC_TABLE, "SAPMV50A", "4004", "X", "", "")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "BDC_CURSOR", "LIKP-VBELN")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "BDC_OKCODE", "=WABU_T")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "LIKP-VBELN", DeliveryNo)


        If objRfcFunc.Call = True Then

            oMESS = objRfcFunc.Imports("MESSG")

            Pesan = oMESS.Value("MSGTX")

            If InStr(Pesan, "saved") > 0 Then
                SaveOkay = True
                MessageBox.Show(Pesan, "SAP Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Else
            MsgBox(Err.Description, MsgBoxStyle.Critical, "Error")
        End If


        oFuncCtrl = Nothing
        objRfcFunc = Nothing
        oTCODE = Nothing
        oMODE = Nothing
        oBT_DATA = Nothing
        oL_ERRORS = Nothing
        oMESS = Nothing


    End Sub

    Public Sub ADD_BDCDATA(BdcTable As Object, program As String, dynpro As String, dynbegin As String, fnam As String, fval As String)
        Indeks = Indeks + 1
        BdcTable.Rows.Add()
        BdcTable.Value(Indeks, "PROGRAM") = program ' Program Name
        BdcTable.Value(Indeks, "DYNPRO") = dynpro ' Dynpro Number
        BdcTable.Value(Indeks, "DYNBEGIN") = dynbegin ' X if a screen
        BdcTable.Value(Indeks, "FNAM") = fnam ' Field Name
        BdcTable.Value(Indeks, "FVAL") = fval ' Field Value
    End Sub

    Private Sub LOAD_DATA()

        Do Until DGV.Rows.Count = 0
            DGV.Rows.RemoveAt(0)
        Loop

        Dim n As Integer = 0
        Dim DPP As Single = 0
        Dim PPN As Single = 0
        Dim TOT As Single = 0

        Try

            Query = "SELECT  * FROM [TRANSLSH] " & _
                    "WHERE [SO_Date] BETWEEN '" & Format(FrmFilterSO.dtpDate1.Value, "yyyy-MM-dd") & "' AND " & _
                                            "'" & Format(FrmFilterSO.dtpDate2.Value, "yyyy-MM-dd") & "' "

            If Trim(txtFilter.Text) <> "" Then
                Query = Query & "AND [" & FilterBy & "] LIKE '%" & txtFilter.Text & "%' "
            End If

            Query = Query & "ORDER BY [so_no] "


            DS = New DataSet
            DA = New SqlDataAdapter(Query, Conn)
            DA.Fill(DS, "SALES")

            For n = 0 To DS.Tables("SALES").Rows.Count - 1

                If DS.Tables("SALES").Rows(n)("TAX") = "Included" Then

                    If DS.Tables("SALES").Rows(n)("SO_Date") < DateValue("2022-04-01") Then
                        DPP = Math.Round(DS.Tables("SALES").Rows(n)("TOTAL") * 100 / 110, 0)
                        PPN = Math.Round(DPP * 10 / 100, 0)
                    Else
                        DPP = Math.Round(DS.Tables("SALES").Rows(n)("TOTAL") * 100 / 111, 0)
                        PPN = Math.Round(DPP * 11 / 100, 0)
                    End If

                Else

                    DPP = DS.Tables("SALES").Rows(n)("TOTAL")

                    If DS.Tables("SALES").Rows(n)("SO_Date") < DateValue("2022-04-01") Then
                        PPN = Math.Round(DPP * 10 / 100, 0)
                    Else
                        PPN = Math.Round(DPP * 11 / 100, 0)
                    End If

                End If

                TOT = DPP + PPN

                DGV.Rows.Add()
                DGV.Rows(n).Cells(0).Value = DS.Tables("SALES").Rows(n)("SO_NO")
                DGV.Rows(n).Cells(1).Value = Format(DS.Tables("SALES").Rows(n)("SO_DATE"), "dd.MM.yyyy")
                DGV.Rows(n).Cells(2).Value = DS.Tables("SALES").Rows(n)("CUST_NO")
                DGV.Rows(n).Cells(3).Value = DS.Tables("SALES").Rows(n)("CUST_NAME")
                DGV.Rows(n).Cells(4).Value = DS.Tables("SALES").Rows(n)("DELV_NO")
                DGV.Rows(n).Cells(5).Value = DS.Tables("SALES").Rows(n)("INV_NO")
                DGV.Rows(n).Cells(6).Value = Format(Val(Replace(Replace(DS.Tables("SALES").Rows(n)("PAY_TERMS"), "Z", ""), "T", "")), "### d ")
                DGV.Rows(n).Cells(7).Value = DS.Tables("SALES").Rows(n)("TAX")
                DGV.Rows(n).Cells(8).Value = DPP
                DGV.Rows(n).Cells(9).Value = PPN
                DGV.Rows(n).Cells(10).Value = TOT

                'If DS.Tables("SALES").Rows(n)("UPLOAD") = 0 Then ' checkbox
                '    DGV.Item(11, n) = New DataGridViewCheckBoxCell
                '    DGV.Item(11, n).ReadOnly = False
                'Else
                '    DGV.Item(11, n) = New DataGridViewTextBoxCell
                '    DGV.Item(11, n).ReadOnly = True
                '    DGV.Item(11, n).Value = ""
                'End If

            Next

            Call AutoNumberRowsForGridView(DGV)

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            'Conn.Close()
            'DA.Dispose()
        End Try

    End Sub

    Private Sub FrmSalesList_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        MDIMain.ToolStrip.Visible = True
    End Sub

    Private Sub FrmSalesList_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Call Local_Connect()

        If ConnectStatus = False Then Exit Sub

        Call GET_PERIOD()

        Dim FromDate As Date = DateValue(CurYearPeriode & "-" & CurMonPeriode & "-01")

        With FrmFilterSO
            .Show()
            .txtNo1.Text = ""
            .txtNo2.Text = ""
            .dtpDate1.Value = FromDate
            .dtpDate2.Value = TglAkhirBulan(FromDate)
            .txtCust.Text = ""
            .Hide()
        End With



        '=====================
        ' DataGridView Design
        '=====================

        DGV.BorderStyle = BorderStyle.Fixed3D
        DGV.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249)
        DGV.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
        DGV.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise
        DGV.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke
        DGV.BackgroundColor = Color.White

        DGV.EnableHeadersVisualStyles = False
        DGV.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        DGV.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72)
        DGV.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        'DGV.RowHeadersVisible = False

        DGV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        DGV.ColumnHeadersHeight = 50

        'DGV.Columns(6).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
        DGV.Columns(8).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
        DGV.Columns(9).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
        DGV.Columns(10).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight

        DGV.Columns(8).HeaderText = "SUB TOTAL" & vbCrLf & "(DPP)"
        DGV.Columns(9).HeaderText = "TAX" & vbCrLf & "(PPN)"
        DGV.Columns(10).HeaderText = "TOTAL"

        FilterBy = "INV_NO"

        cboFilter.SelectedIndex = 0

        Call LOAD_DATA()


    End Sub

    Private Sub tsbExit_Click(sender As Object, e As EventArgs) Handles tsbExit.Click
        Me.Close()
    End Sub

    Private Sub FrmSalesList_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        On Error Resume Next
        txtFilter.Left = Me.Width - txtFilter.Width - 35
        cboFilter.Left = txtFilter.Left - cboFilter.Width - 5
        On Error GoTo 0
    End Sub

    Private Sub tsbUploadtoSAP_Click(sender As Object, e As EventArgs) Handles tsbUploadtoSAP.Click

        'Dim Ada As Boolean = False

        'For n = 0 To DGV.Rows.Count - 1
        '    If TypeOf (DGV.Rows(n).Cells(11)) Is DataGridViewCheckBoxCell Then
        '        If DGV.Rows(n).Cells(11).Value = True Then
        '            Ada = True
        '            Exit For
        '        End If
        '    End If
        'Next

        'If Ada = False Then Exit Sub


        InvoiceDate = DGV.CurrentRow.Cells(1).Value.ToString

        DeliveryNo = DGV.CurrentRow.Cells(4).Value.ToString

        If Trim(UCase(DeliveryNo)) = "CANCEL" Then
            MessageBox.Show("This item already cancelled !", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If


        If Trim(DGV.CurrentRow.Cells(5).Value.ToString) <> "" Then
            MessageBox.Show("This item already uploaded !", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If


        If MessageBox.Show("Create Sales Invoice ? ", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = vbNo Then
            Exit Sub
        End If


        Call CHANGE_ACTUAL_GI()

        If SaveOkay = True Then

            Call BAPI_BILLINGDOC_CREATEMULTIPLE()

            If SaveOkay Then

                Call Local_Connect()

                Query = "UPDATE [TRANSLSH] " & _
                        "SET    [UPLOAD]=1, [INV_NO]='" & InvoiceNo & "' " & _
                        "WHERE  [DELV_NO]='" & DeliveryNo & "'"

                CMD = New SqlCommand
                CMD.Connection = Conn
                CMD.CommandText = Query
                CMD.ExecuteNonQuery()


                DGV.CurrentRow.Cells(5).Value = InvoiceNo

                'Call LOAD_DATA()

            End If

        End If



    End Sub

    Private Sub tsb_PrintSalesReceipt_Click(sender As Object, e As EventArgs)

        Dim CUST As String = ""

        Query = "SELECT flkode,flnama,flala1,flwila FROM [MASCUST] " & _
                "WHERE  [flkode]='" & DGV.CurrentRow.Cells(2).Value.ToString & "'"

        DS = New DataSet
        DA = New SqlDataAdapter(Query, Conn)
        DA.Fill(DS, "CUSTOMER")

        CUST = DS.Tables("CUSTOMER").Rows(0)("flnama")
        CUST = CUST & vbCrLf & DS.Tables("CUSTOMER").Rows(0)("flala1")
        CUST = CUST & vbCrLf & DS.Tables("CUSTOMER").Rows(0)("flwila")



        Query = "SELECT  TRANSLSD.SO_NO, TRANSLSD.ITEM_NO, TRANSLSD.KDPROD, TRANSLSD.NMPROD, TRANSLSD.COLOR, TRANSLSD.QTY1, TRANSLSD.QTY2, TRANSLSD.UNIT, TRANSLSD.PRICE, " & _
                        "TRANSLSH.SO_NO, TRANSLSH.SO_DATE, TRANSLSH.DELV_NO, TRANSLSH.INV_NO, TRANSLSH.CUST_NO, TRANSLSH.CUST_NAME, TRANSLSH.PO_NO, TRANSLSH.PAY_TERMS, TRANSLSH.SLOC, TRANSLSH.TOTAL, TRANSLSH.TAX " & _
                "FROM    TRANSLSD INNER JOIN TRANSLSH ON TRANSLSD.SO_NO = TRANSLSH.SO_NO " & _
                "WHERE   TRANSLSD.SO_NO = '" & DGV.CurrentRow.Cells(0).Value.ToString & "'"


        Dim PrintControl As New ClassPrintReport

        PrintControl.CetakReport("RptSalesReceipt.rdlc", "DataSet4", Query, CUST, TAXRATE)

    End Sub

    Private Sub tsb_PrintSalesInvoice_Click(sender As Object, e As EventArgs) Handles tsb_PrintSalesInvoice.Click

        If Trim(DGV.CurrentRow.Cells(5).Value.ToString) = "" Then
            MessageBox.Show("Please process upload to SAP first !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If


        Dim CUST As String = ""
        Dim RecCount As Long = 0
        Dim POSTDATE As String = ""

        POSTDATE = DGV.CurrentRow.Cells(1).Value
        POSTDATE = Mid(POSTDATE, 7, 4) & "-" & Mid(POSTDATE, 4, 2) & "-" & Mid(POSTDATE, 1, 2)
        TAXRATE = TAX_RATE(DateValue(POSTDATE))


        Query = "SELECT flkode,flnama,flala1,flwila FROM [MASCUST] WHERE [flkode]='" & DGV.CurrentRow.Cells(2).Value.ToString & "'"

        DS = New DataSet
        DA = New SqlDataAdapter(Query, Conn)
        DA.Fill(DS, "CUSTOMER")

        CUST = DS.Tables("CUSTOMER").Rows(0)("flnama")
        CUST = CUST & vbCrLf & DS.Tables("CUSTOMER").Rows(0)("flala1")
        CUST = CUST & vbCrLf & DS.Tables("CUSTOMER").Rows(0)("flwila")


        Query = "SELECT TRANSLSD.SO_NO, TRANSLSD.ITEM_NO, TRANSLSD.KDPROD, TRANSLSD.NMPROD, TRANSLSD.COLOR, TRANSLSD.QTY1, TRANSLSD.QTY2, TRANSLSD.UNIT, TRANSLSD.PRICE," & _
                      " TRANSLSH.SO_NO, TRANSLSH.SO_DATE, TRANSLSH.DELV_NO, TRANSLSH.INV_NO, TRANSLSH.CUST_NO, TRANSLSH.CUST_NAME, TRANSLSH.PO_NO, TRANSLSH.PAY_TERMS," & _
                      " TRANSLSH.SLOC, TRANSLSH.TOTAL, TRANSLSH.TAX, TRANSLSH.REMARK, TRANSLSH.FREIGHT, TRANSLSH.DISCOUNT " & _
                  "FROM TRANSLSD INNER JOIN TRANSLSH ON TRANSLSD.SO_NO = TRANSLSH.SO_NO " & _
                 "WHERE TRANSLSD.SO_NO = '" & DGV.CurrentRow.Cells(0).Value.ToString & "' " & _
                 "ORDER BY CAST(TRANSLSD.ITEM_NO AS INT) "

        DS = New DataSet
        DA = New SqlDataAdapter(Query, Conn)
        DA.Fill(DS, "SO")

        RecCount = DS.Tables("SO").Rows.Count

        If (RecCount Mod 10) > 0 Then

            For n = 1 To (10 - (RecCount Mod 10))

                Query = "INSERT INTO [TRANSLSD] (so_no,item_no,kdprod,nmprod,color,qty1,qty2,unit,price) " & _
                        "VALUES ('" & DGV.CurrentRow.Cells(0).Value.ToString & "','" & Trim((RecCount + n).ToString) & "'," & _
                                "'', '', '', 0, 0, '', 0) "

                CMD = New SqlCommand
                CMD.Connection = Conn
                CMD.CommandText = Query
                CMD.ExecuteNonQuery()

            Next

            Query = "SELECT TRANSLSD.SO_NO, TRANSLSD.ITEM_NO, TRANSLSD.KDPROD, TRANSLSD.NMPROD, TRANSLSD.COLOR, TRANSLSD.QTY1, TRANSLSD.QTY2, TRANSLSD.UNIT, TRANSLSD.PRICE," & _
                          " TRANSLSH.SO_NO, TRANSLSH.SO_DATE, TRANSLSH.DELV_NO, TRANSLSH.INV_NO, TRANSLSH.CUST_NO, TRANSLSH.CUST_NAME, TRANSLSH.PO_NO, TRANSLSH.PAY_TERMS," & _
                          " TRANSLSH.SLOC, TRANSLSH.TOTAL, TRANSLSH.TAX, TRANSLSH.REMARK, TRANSLSH.FREIGHT, TRANSLSH.DISCOUNT " & _
                      "FROM TRANSLSD INNER JOIN TRANSLSH ON TRANSLSD.SO_NO = TRANSLSH.SO_NO " & _
                     "WHERE TRANSLSD.SO_NO = '" & DGV.CurrentRow.Cells(0).Value.ToString & "' " & _
                     "ORDER BY CAST(TRANSLSD.ITEM_NO AS INT) "

        End If


        Dim PrintControl As New ClassPrintReport

        PrintControl.CetakReport("RptSalesInvoice.rdlc", "DataSet4", Query, CUST, TAXRATE)


        Query = "DELETE FROM [TRANSLSD] WHERE [so_no]='" & DGV.CurrentRow.Cells(0).Value.ToString & "' AND [kdprod]='' "

        CMD = New SqlCommand
        CMD.Connection = Conn
        CMD.CommandText = Query
        CMD.ExecuteNonQuery()



    End Sub

    Private Sub txtFilter_TextChanged(sender As Object, e As EventArgs) Handles txtFilter.TextChanged
        Call LOAD_DATA()
    End Sub

    Private Sub cboFilter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboFilter.SelectedIndexChanged
        Select Case cboFilter.SelectedIndex
            Case Is = 0
                FilterBy = "INV_NO"
            Case Is = 1
                FilterBy = "CUST_NAME"
        End Select
    End Sub

    Private Sub tsbFilter_Click(sender As Object, e As EventArgs) Handles tsbFilter.Click
        FrmFilterSO.ShowDialog()
        If FilterOkay = True Then Call LOAD_DATA()
    End Sub

    Private Sub tsbCancelBilling_Click(sender As Object, e As EventArgs) Handles tsbCancelBilling.Click

        InvoiceNo = Trim(UCase(DGV.CurrentRow.Cells(5).Value.ToString))

        If InvoiceNo = "CANCEL" Then
            MessageBox.Show("This document already cancelled !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If InvoiceNo = "" Then
            MessageBox.Show("This document not yet billed !", "Info", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If MessageBox.Show("Cancel Billing No. " & InvoiceNo & "  ? ", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = vbNo Then
            Exit Sub
        End If

        Call BAPI_BILLINGDOC_CANCEL1()

        If SaveOkay Then

            Call Local_Connect()

            Query = "UPDATE [TRANSLSH] " & _
                    "SET    [UPLOAD]=0, [INV_NO]='CANCEL' " & _
                    "WHERE  [INV_NO]='" & InvoiceNo & "'"

            CMD = New SqlCommand
            CMD.Connection = Conn
            CMD.CommandText = Query
            CMD.ExecuteNonQuery()


            DGV.CurrentRow.Cells(5).Value = "CANCEL"




        End If

    End Sub

    Private Sub tsbPrintInvoice_Click(sender As Object, e As EventArgs) Handles tsbPrintInvoice.Click

    End Sub
End Class