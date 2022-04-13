Imports System.Data.SqlClient
Imports Microsoft.Reporting.WinForms


Public Class FrmSalesReceiptList

    Dim Indeks As Integer = 0
    Dim SaveOkay As Boolean = False
    Dim DeliveryNo As String = ""
    Dim InvoiceDate As String = ""
    Dim InvoiceNo As String = ""
    Dim FilterBy As String = ""


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

        oSUCCESS.Rows.Add()
        oSUCCESS.Value(1, "REF_DOC") = DeliveryNo
        oSUCCESS.Value(1, "BILL_DOC") = InvoiceNo


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
        ADD_BDCDATA(BDC_TABLE, "", "0000", "", "BDC_OKCODE", "/00")
        ADD_BDCDATA(BDC_TABLE, "", "0000", "", "BDC_CURSOR", "LIKP-WADAT_IST")
        ADD_BDCDATA(BDC_TABLE, "", "0000", "", "LIKP-WADAT_IST", InvoiceDate)

        ADD_BDCDATA(BDC_TABLE, "SAPMV50A", "1000", "X", "", "")
        ADD_BDCDATA(BDC_TABLE, "", "0000", "", "BDC_OKCODE", "=WABU_T")


        If objRfcFunc.Call = True Then

            oMESS = objRfcFunc.Imports("MESSG")
            Pesan = oMESS.Value("MSGTX")

            If InStr(Pesan, "saved") > 0 Then
                SaveOkay = True
                'MDIMain.StatusStrip1.Items(0).Text = Pesan
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
                DGV.Rows(n).Cells(5).Value = DS.Tables("SALES").Rows(n)("PO_NO")
                DGV.Rows(n).Cells(6).Value = DS.Tables("SALES").Rows(n)("PAY_TERMS")
                DGV.Rows(n).Cells(7).Value = DS.Tables("SALES").Rows(n)("SLOC")
                DGV.Rows(n).Cells(8).Value = DS.Tables("SALES").Rows(n)("TAX")
                DGV.Rows(n).Cells(9).Value = DPP
                DGV.Rows(n).Cells(10).Value = PPN
                DGV.Rows(n).Cells(11).Value = TOT

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
            .dtpDate2.Value = Now   ' TglAkhirBulan(DateValue(CurYearPeriode & "-" & CurMonPeriode & "-01"))
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

        DGV.Columns(9).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
        DGV.Columns(10).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
        DGV.Columns(11).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight

        DGV.Columns(9).HeaderText = "SUB TOTAL" & vbCrLf & "(DPP)"
        DGV.Columns(10).HeaderText = "TAX" & vbCrLf & "(PPN)"
        DGV.Columns(11).HeaderText = "TOTAL"

        FilterBy = "CUST_NAME"

        cboFilter.SelectedIndex = 1

        Call LOAD_DATA()


    End Sub

    Private Sub tsbExit_Click(sender As Object, e As EventArgs) Handles tsbExit.Click
        Me.Close()
    End Sub

    Private Sub FrmSalesList_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        'On Error Resume Next
        'txtFilter.Left = Me.Width - txtFilter.Width - 35
        'cboFilter.Left = txtFilter.Left - cboFilter.Width - 5
        'On Error GoTo 0
    End Sub

    Private Sub tsbPrintDelivery_Click(sender As Object, e As EventArgs) Handles tsbPrintDelivery.Click

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


        Query = "SELECT  TRANSLSD.SO_NO, TRANSLSD.ITEM_NO, TRANSLSD.KDPROD, TRANSLSD.NMPROD, TRANSLSD.COLOR, TRANSLSD.QTY1, TRANSLSD.QTY2, TRANSLSD.UNIT, TRANSLSD.PRICE," & _
                       " TRANSLSH.SO_NO, TRANSLSH.SO_DATE, TRANSLSH.DELV_NO, TRANSLSH.INV_NO, TRANSLSH.CUST_NO, TRANSLSH.CUST_NAME, TRANSLSH.PO_NO, TRANSLSH.PAY_TERMS," & _
                       " TRANSLSH.SLOC, TRANSLSH.TOTAL, TRANSLSH.TAX " & _
                "FROM    TRANSLSD INNER JOIN TRANSLSH ON TRANSLSD.SO_NO = TRANSLSH.SO_NO " & _
                "WHERE   TRANSLSH.DELV_NO = '" & DGV.CurrentRow.Cells(4).Value.ToString & "' " & _
                "ORDER BY CAST(TRANSLSD.ITEM_NO AS INT) "

        DS = New DataSet
        DA = New SqlDataAdapter(Query, Conn)
        DA.Fill(DS, "SO")

        RecCount = DS.Tables("SO").Rows.Count

        Dim ItemNo As Integer = Val(DS.Tables("SO").Rows(RecCount - 1)("item_no"))

        If (RecCount Mod 10) > 0 Then

            For n = 1 To (10 - (RecCount Mod 10))

                Query = "INSERT INTO [TRANSLSD] (so_no,item_no,kdprod,nmprod,color,qty1,qty2,unit,price) " & _
                        "VALUES ('" & DGV.CurrentRow.Cells(0).Value.ToString & "','" & Trim((ItemNo + n).ToString) & "'," & _
                                "'', '', '', 0, 0, '', 0) "

                CMD = New SqlCommand
                CMD.Connection = Conn
                CMD.CommandText = Query
                CMD.ExecuteNonQuery()

            Next

            Query = "SELECT  TRANSLSD.SO_NO, TRANSLSD.ITEM_NO, TRANSLSD.KDPROD, TRANSLSD.NMPROD, TRANSLSD.COLOR, TRANSLSD.QTY1, TRANSLSD.QTY2, TRANSLSD.UNIT, TRANSLSD.PRICE," & _
                       " TRANSLSH.SO_NO, TRANSLSH.SO_DATE, TRANSLSH.DELV_NO, TRANSLSH.INV_NO, TRANSLSH.CUST_NO, TRANSLSH.CUST_NAME, TRANSLSH.PO_NO, TRANSLSH.PAY_TERMS," & _
                       " TRANSLSH.SLOC, TRANSLSH.TOTAL, TRANSLSH.TAX " & _
                "FROM    TRANSLSD INNER JOIN TRANSLSH ON TRANSLSD.SO_NO = TRANSLSH.SO_NO " & _
                "WHERE   TRANSLSH.DELV_NO = '" & DGV.CurrentRow.Cells(4).Value.ToString & "' " & _
                "ORDER BY CAST(TRANSLSD.ITEM_NO AS INT) "

        End If


        Dim PrintControl As New ClassPrintReport

        PrintControl.CetakReport("RptDelvNote.rdlc", "DataSet3", Query, CUST, TAXRATE)


        Query = "DELETE FROM [TRANSLSD] WHERE [so_no]='" & DGV.CurrentRow.Cells(0).Value.ToString & "' AND [kdprod]='' "

        CMD = New SqlCommand
        CMD.Connection = Conn
        CMD.CommandText = Query
        CMD.ExecuteNonQuery()


    End Sub

    Private Sub tsbPrintSalesReceipt_Click(sender As Object, e As EventArgs) Handles tsbPrintSalesReceipt.Click

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

        Dim ItemNo As Integer = Val(DS.Tables("SO").Rows(RecCount - 1)("item_no"))

        If (RecCount Mod 10) > 0 Then

            For n = 1 To (10 - (RecCount Mod 10))

                Query = "INSERT INTO [TRANSLSD] (so_no,item_no,kdprod,nmprod,color,qty1,qty2,unit,price) " & _
                        "VALUES ('" & DGV.CurrentRow.Cells(0).Value.ToString & "','" & Trim((ItemNo + n).ToString) & "'," & _
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

        PrintControl.CetakReport("RptSalesReceipt.rdlc", "DataSet4", Query, CUST, TAXRATE)
        

        Query = "DELETE FROM [TRANSLSD] WHERE [so_no]='" & DGV.CurrentRow.Cells(0).Value.ToString & "' AND [kdprod]='' "

        CMD = New SqlCommand
        CMD.Connection = Conn
        CMD.CommandText = Query
        CMD.ExecuteNonQuery()



    End Sub

    Private Sub tsbNew_Click(sender As Object, e As EventArgs) Handles tsbNew.Click

        DOC_NO = ""

        FrmSalesReceipt.ShowDialog()


        If DOC_NO <> "" Then

            Call LOAD_DATA()

            DGV.CurrentCell = DGV(0, DGV.Rows.Count - 1)

        End If

    End Sub

    Private Sub tsbEdit_Click(sender As Object, e As EventArgs) Handles tsbEdit.Click

        DOC_NO = DGV.CurrentRow.Cells(0).Value.ToString

        FrmSalesReceipt.ShowDialog()
        
        If SaveStatus = True Then



        End If


    End Sub

    Private Sub tsbRefresh_Click(sender As Object, e As EventArgs) Handles tsbRefresh.Click
        txtFilter.Text = ""
        Call LOAD_DATA()
    End Sub

    

    Private Sub tsbDelete_Click(sender As Object, e As EventArgs) Handles tsbDelete.Click

        Dim SO_NO = Trim(DGV.CurrentRow.Cells(0).Value.ToString)
        Dim SJ_NO = Trim(DGV.CurrentRow.Cells(4).Value.ToString)


        If SJ_NO = "CANCEL" Then
            MessageBox.Show("This document already cancelled !", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If MessageBox.Show("Are you really want to delete " & vbCrLf & _
                           "Sales Order No. " & SO_NO & "  ? ", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = vbNo Then
            Exit Sub
        End If


        Call Local_Connect()

        If ConnectStatus = False Then Exit Sub


        Query = "UPDATE [TRANSLSH] " & _
                   "SET [DELV_NO]='CANCEL', [INV_NO]='CANCEL', [TOTAL]=0 " & _
                 "WHERE [SO_NO]='" & SO_NO & "'"

        CMD = New SqlCommand
        CMD.Connection = Conn
        CMD.CommandText = Query
        CMD.ExecuteNonQuery()


        Query = "UPDATE [TRANSLSD] SET [QTY1]=0, [QTY2]=0 WHERE [SO_NO]='" & SO_NO & "'"

        CMD = New SqlCommand
        CMD.Connection = Conn
        CMD.CommandText = Query
        CMD.ExecuteNonQuery()


        '=============================================
        ' JIKA JUAL POTONGAN UPDATE STOCK DI STOCKBCD
        '=============================================

        Query = "SELECT barcode,corak,warna,grade,partai,lebar,yard,meter,unit,so,sloc,potong FROM [STOCKCARD] " & _
                 "WHERE [SO]='" & SO_NO & "'"

        DS = New DataSet
        DA = New SqlDataAdapter(Query, Conn)
        DA.Fill(DS, "STOCKCARD")

        For n = 0 To DS.Tables("STOCKCARD").Rows.Count - 1

            If DS.Tables("STOCKCARD").Rows(n)("POTONG") = 1 Then

                Query = "UPDATE [STOCKBCD] " & _
                           "SET [YARD] = [YARD] + " & Replace(DS.Tables("STOCKCARD").Rows(n)("YARD"), ",", ".") & ", " & _
                              " [METER] = [METER] + " & Replace(DS.Tables("STOCKCARD").Rows(n)("METER"), ",", ".") & " " & _
                         "WHERE [BARCODE]='" & DS.Tables("STOCKCARD").Rows(n)("BARCODE") & "'"

            Else

                Query = "INSERT INTO [STOCKBCD] (barcode,corak,warna,grade,partai,lebar,yard,meter,unit,so,sloc) " & _
                        "SELECT barcode,corak,warna,grade,partai,lebar,yard,meter,unit,so,sloc FROM [STOCKCARD] " & _
                         "WHERE [barcode]='" & DS.Tables("STOCKCARD").Rows(n)("BARCODE") & "' AND [so]='" & SO_NO & "'"


            End If

            CMD = New SqlCommand
            CMD.Connection = Conn
            CMD.CommandText = Query
            CMD.ExecuteNonQuery()

        Next


        Query = "DELETE FROM [STOCKCARD] WHERE [SO]='" & SO_NO & "'"

        CMD = New SqlCommand
        CMD.Connection = Conn
        CMD.CommandText = Query
        CMD.ExecuteNonQuery()



        DGV.CurrentRow.Cells(4).Value = "CANCEL"
        DGV.CurrentRow.Cells(9).Value = 0
        DGV.CurrentRow.Cells(10).Value = 0
        DGV.CurrentRow.Cells(11).Value = 0


    End Sub

   
    Private Sub txtFilter_TextChanged(sender As Object, e As EventArgs) Handles txtFilter.TextChanged
        Call LOAD_DATA()
    End Sub

    Private Sub cboFilter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboFilter.SelectedIndexChanged
        Select Case cboFilter.SelectedIndex
            Case Is = 0
                FilterBy = "SO_NO"
            Case Is = 1
                FilterBy = "CUST_NAME"
        End Select
    End Sub

    Private Sub tsbFilter_Click(sender As Object, e As EventArgs) Handles tsbFilter.Click
        FrmFilterSO.ShowDialog()
        If FilterOkay = True Then Call LOAD_DATA()
    End Sub

    Private Sub tsbPrint_Click(sender As Object, e As EventArgs) Handles tsbPrint.Click

    End Sub

    Private Sub tsbPrintSalesReceipt2_Click(sender As Object, e As EventArgs) Handles tsbPrintSalesReceipt2.Click

        Dim CORAK As String = ""
        Dim MEREK As String = ""
        Dim TEMPSLSD As String = ""
        Dim NO As Integer = 1
        Dim CUST As String = ""
        Dim RecCount As Integer = 0
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


        '=====================
        '  CREATE TEMP TABLE
        '=====================
        'Ulang:
        '        TEMPSLSD = "TMPINV" & Format(NO, "00")

        '        Try

        '            Query = "SELECT * FROM [" & TEMPSLSD & "] "

        '            DS = New DataSet
        '            DA = New SqlDataAdapter(Query, Conn)
        '            DA.Fill(DS, "TMPSLSD")

        '            If DS.Tables("TMPSLSD").Rows.Count > 0 Then
        '                NO = NO + 1
        '                GoTo Ulang
        '            End If

        '        Catch ex As Exception

        '            Query = "SELECT * INTO [" & TEMPSLSD & "] FROM [TRANSLSD] WHERE 1=2 "

        '            CMD = New SqlCommand
        '            CMD.Connection = Conn
        '            CMD.CommandText = Query
        '            CMD.ExecuteNonQuery()

        '            GoTo Ulang

        '        End Try


        Query = "DELETE FROM [TMPINV] "

        CMD = New SqlCommand
        CMD.Connection = Conn
        CMD.CommandText = Query
        CMD.ExecuteNonQuery()


        Query = "SELECT kdprod, nmprod, qty1, qty2, unit, price FROM [TRANSLSD] " & _
                 "WHERE [SO_NO] = '" & DGV.CurrentRow.Cells(0).Value.ToString & "' " & _
                 "ORDER BY CAST(ITEM_NO AS INT) "

        DS = New DataSet
        DA = New SqlDataAdapter(Query, Conn)
        DA.Fill(DS, "SLSD")

        For n = 1 To DS.Tables("SLSD").Rows.Count

            CORAK = DS.Tables("SLSD").Rows(n - 1)("KDPROD")
            CORAK = Trim(Mid(CORAK, 3, 6))

            If Mid(CORAK, 6, 1) = "." Then
                CORAK = "0" & Mid(CORAK, 1, 5)
            End If

            MEREK = DS.Tables("SLSD").Rows(n - 1)("NMPROD")
            MEREK = Trim(Mid(MEREK, 1, InStr(MEREK, ",") - 1))

            If InStr(MEREK, "#") > 0 Then
                MEREK = Trim(Mid(MEREK, 1, InStr(MEREK, "#") - 1))
            End If

            Query = "INSERT INTO [TMPINV] (so_no,item_no,kdprod,nmprod,qty1,qty2,unit,price) " & _
                    "VALUES ('" & DGV.CurrentRow.Cells(0).Value.ToString & "'," & n & ",'" & CORAK & "'," & _
                            "'" & MEREK & "'," & DS.Tables("SLSD").Rows(n - 1)("QTY1") & "," & _
                            " " & Replace(DS.Tables("SLSD").Rows(n - 1)("QTY2"), ",", ".") & "," & _
                            "'" & DS.Tables("SLSD").Rows(n - 1)("UNIT") & "'," & DS.Tables("SLSD").Rows(n - 1)("PRICE") & ")"

            CMD = New SqlCommand
            CMD.Connection = Conn
            CMD.CommandText = Query
            CMD.ExecuteNonQuery()

        Next

        ' GROUPING

        Dim ItemNo As Integer = 100

        Query = "SELECT kdprod,nmprod,unit,so_no,price,SUM(qty1) AS pcs,SUM(qty2) AS yds FROM [TMPINV] " & _
                "GROUP BY kdprod,nmprod,unit,so_no,price "

        DS = New DataSet
        DA = New SqlDataAdapter(Query, Conn)
        DA.Fill(DS, "SLSD")

        For n = 1 To DS.Tables("SLSD").Rows.Count

            ItemNo = ItemNo + 1

            Query = "INSERT INTO [TMPINV] (so_no,item_no,kdprod,nmprod,qty1,qty2,unit,price) " & _
                    "VALUES ('" & DS.Tables("SLSD").Rows(n - 1)("SO_NO") & "','" & ItemNo & "'," & _
                            "'" & DS.Tables("SLSD").Rows(n - 1)("KDPROD") & "'," & _
                            "'" & DS.Tables("SLSD").Rows(n - 1)("NMPROD") & "'," & _
                            " " & DS.Tables("SLSD").Rows(n - 1)("PCS") & "," & _
                            " " & Replace(DS.Tables("SLSD").Rows(n - 1)("YDS"), ",", ".") & "," & _
                            "'" & DS.Tables("SLSD").Rows(n - 1)("UNIT") & "'," & _
                            " " & DS.Tables("SLSD").Rows(n - 1)("PRICE") & ")"

            CMD = New SqlCommand
            CMD.Connection = Conn
            CMD.CommandText = Query
            CMD.ExecuteNonQuery()

        Next


        Query = "DELETE FROM [TMPINV] WHERE [ITEM_NO] < 100 "

        CMD = New SqlCommand
        CMD.Connection = Conn
        CMD.CommandText = Query
        CMD.ExecuteNonQuery()


        Query = "SELECT TMPINV.SO_NO, TMPINV.ITEM_NO, TMPINV.KDPROD, TMPINV.NMPROD, TMPINV.QTY1, TMPINV.QTY2, TMPINV.UNIT, TMPINV.PRICE, " & _
                       "TRANSLSH.SO_DATE, TRANSLSH.DELV_NO, TRANSLSH.INV_NO, TRANSLSH.CUST_NO, TRANSLSH.CUST_NAME, TRANSLSH.PO_NO, " & _
                       "TRANSLSH.PAY_TERMS, TRANSLSH.TAX, TRANSLSH.TOTAL, TRANSLSH.REMARK " & _
                  "FROM [TMPINV] INNER JOIN [TRANSLSH] ON TMPINV.SO_NO = TRANSLSH.SO_NO " & _
                 "ORDER BY TMPINV.ITEM_NO "

        DS = New DataSet
        DA = New SqlDataAdapter(Query, Conn)
        DA.Fill(DS, "SLS")

        RecCount = DS.Tables("SLS").Rows.Count

        If (RecCount Mod 10) > 0 Then

            For n = 1 To (10 - (RecCount Mod 10))

                ItemNo = ItemNo + 1

                Query = "INSERT INTO [TMPINV] (so_no,item_no,kdprod,nmprod,color,qty1,qty2,unit,price) " & _
                        "VALUES ('" & DGV.CurrentRow.Cells(0).Value.ToString & "'," & ItemNo & ",'', '', '', 0, 0, '', 0) "

                CMD = New SqlCommand
                CMD.Connection = Conn
                CMD.CommandText = Query
                CMD.ExecuteNonQuery()

            Next

            '                      "WHERE TMPINV.SO_NO = '" & DGV.CurrentRow.Cells(0).Value.ToString & "' " & _

            Query = "SELECT TMPINV.SO_NO, TMPINV.ITEM_NO, TMPINV.KDPROD, TMPINV.NMPROD, TMPINV.QTY1, TMPINV.QTY2, TMPINV.UNIT, TMPINV.PRICE, " & _
                           "TRANSLSH.SO_DATE, TRANSLSH.DELV_NO, TRANSLSH.INV_NO, TRANSLSH.CUST_NO, TRANSLSH.CUST_NAME, TRANSLSH.PO_NO, " & _
                           "TRANSLSH.PAY_TERMS, TRANSLSH.TAX, TRANSLSH.TOTAL, TRANSLSH.REMARK " & _
                      "FROM [TMPINV] INNER JOIN [TRANSLSH] ON TMPINV.SO_NO = TRANSLSH.SO_NO " & _
                     "ORDER BY TMPINV.ITEM_NO "

        End If


        Dim PrintControl As New ClassPrintReport

        PrintControl.CetakReport("RptSalesReceipt2.rdlc", "DataSet8", Query, CUST, TAXRATE)



        'Query = "DROP TABLE [" & TEMPSLSD & "] "

        'CMD = New SqlCommand
        'CMD.Connection = Conn
        'CMD.CommandText = Query
        'CMD.ExecuteNonQuery()


    End Sub
End Class