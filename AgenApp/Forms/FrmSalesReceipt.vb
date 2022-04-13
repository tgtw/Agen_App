Imports System.Data.SqlClient


Public Class FrmSalesReceipt

    Dim TEMPSALES As String = ""
    Dim SaveOkay As Boolean = False
    Dim SalesOrderNo As String
    Dim DeliveryNo As String = ""
    Dim InvoiceNo As String = ""
    Dim TOTSALES As Double = 0
    Dim NO As Integer = 1
    Dim Indeks As Integer = 0
    Dim KDCUST As String = ""
    Dim NMCUST As String = ""
    Dim INTCOMP As Integer = 0


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
                    lblSONo.Text = InvoiceNo
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
        ADD_BDCDATA(BDC_TABLE, "", "0000", "", "LIKP-WADAT_IST", Format(dtpSODate.Value, "dd.MM.yyyy"))

        ADD_BDCDATA(BDC_TABLE, "SAPMV50A", "1000", "X", "", "")
        ADD_BDCDATA(BDC_TABLE, "", "0000", "", "BDC_OKCODE", "=WABU_T")


        If objRfcFunc.Call = True Then

            oMESS = objRfcFunc.Imports("MESSG")
            Pesan = oMESS.Value("MSGTX")

            If InStr(Pesan, "saved") > 0 Then
                SaveOkay = True
                MDIMain.StatusStrip1.Items(0).Text = Pesan
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

    Private Sub GET_SLOC()

        'Call LogToSAP(True)

        'If SAPConStatus = False Then Exit Sub

        Dim objRfcFunc, objQueryTab As Object
        Dim objOptTab, objFldTab, objDatTab As Object
        Dim objDatRec As Object
        Dim SAP_RFC As Object

        Dim vSLOC As String


        SAP_RFC = CreateObject("SAP.Functions")
        SAP_RFC.Connection = SAPConn

        objRfcFunc = SAP_RFC.Add("RFC_READ_TABLE")

        objQueryTab = objRfcFunc.Exports("QUERY_TABLE")
        objQueryTab.Value = "T001L"  ' Storage locations

        objOptTab = objRfcFunc.Tables("OPTIONS")
        objFldTab = objRfcFunc.Tables("FIELDS")
        objDatTab = objRfcFunc.Tables("DATA")

        objOptTab.FreeTable()
        objOptTab.Rows.Add()
        objOptTab(objOptTab.rowcount, "TEXT") = "WERKS = '" + PLANT + "' "

        objFldTab.FreeTable()
        objFldTab.Rows.Add()
        objFldTab(objFldTab.rowcount, "FIELDNAME") = "LGORT"    ' SLoc
        objFldTab.Rows.Add()
        objFldTab(objFldTab.rowcount, "FIELDNAME") = "LGOBE"    ' Desc


        If objRfcFunc.Call = False Then
            MsgBox(objRfcFunc.Exception)
            Exit Sub
        End If

        If objDatTab.RowCount > 0 Then

            Dim i As Integer = 0

            cboSLoc.Items.Clear()

            For Each objDatRec In objDatTab.Rows
                i = i + 1
                vSLOC = Mid(objDatTab.Rows(i)("WA"), objFldTab(1, "OFFSET") + 1, objFldTab(1, "LENGTH"))
                cboSLoc.Items.Add(vSLOC)
            Next

        End If

        objFldTab.Rows.RemoveAll()
        objOptTab.Rows.RemoveAll()
        objDatTab.Rows.RemoveAll()

        objRfcFunc = Nothing
        objQueryTab = Nothing
        objOptTab = Nothing
        objFldTab = Nothing
        objDatTab = Nothing
        SAP_RFC = Nothing


    End Sub

    Private Sub GET_DELIVERY_NO()

        'Call LogToSAP(True)

        'If SAPConStatus = False Then Exit Sub

        Dim objRfcFunc, objQueryTab As Object
        Dim objOptTab, objFldTab, objDatTab As Object
        Dim objDatRec As Object
        Dim SAP_RFC As Object

        SAP_RFC = CreateObject("SAP.Functions")
        SAP_RFC.Connection = SAPConn

        objRfcFunc = SAP_RFC.Add("RFC_READ_TABLE")

        objQueryTab = objRfcFunc.Exports("QUERY_TABLE")
        objQueryTab.Value = "VBFA"  ' Sales Document Flow

        objOptTab = objRfcFunc.Tables("OPTIONS")
        objFldTab = objRfcFunc.Tables("FIELDS")
        objDatTab = objRfcFunc.Tables("DATA")

        objOptTab.FreeTable()
        objOptTab.Rows.Add()
        objOptTab(objOptTab.rowcount, "TEXT") = "VBELV = '" + SalesOrderNo + "' AND "       ' Preceding doc.
        objOptTab.Rows.Add()
        objOptTab(objOptTab.rowcount, "TEXT") = "BWART = '601' AND "                        ' Movement type
        objOptTab.Rows.Add()
        objOptTab(objOptTab.rowcount, "TEXT") = "MANDT = '777' "                            ' Client

        objFldTab.FreeTable()
        objFldTab.Rows.Add()
        objFldTab(objFldTab.rowcount, "FIELDNAME") = "VBELN"                                ' Follow on doc


        If objRfcFunc.Call = False Then
            MsgBox(objRfcFunc.Exception)
            Exit Sub
        End If

        If objDatTab.rowcount > 0 Then

            Dim i As Integer = 0

            For Each objDatRec In objDatTab.Rows
                i = i + 1
                DeliveryNo = Mid(objDatTab.Rows(i)("WA"), objFldTab(1, "OFFSET") + 1, objFldTab(1, "LENGTH"))
            Next

        End If

        lblDelvNo.Text = DeliveryNo

        objFldTab.Rows.RemoveAll()
        objOptTab.Rows.RemoveAll()
        objDatTab.Rows.RemoveAll()

        objRfcFunc = Nothing
        objQueryTab = Nothing
        objOptTab = Nothing
        objFldTab = Nothing
        objDatTab = Nothing
        SAP_RFC = Nothing

    End Sub

    Private Sub BAPI_SALESORDER_CREATEFROMDAT2()

        'Call LogToSAP(True)

        'If SAPConStatus = False Then Exit Sub

        SaveOkay = False

        Dim oFuncCtrl As Object
        Dim oTheFunc As Object
        Dim FuncCommit As Object

        Dim oHEADER_IN As Object
        Dim oHEADER_INX As Object
        Dim oPARTNERS As Object
        Dim oRETURN As Object
        Dim oITEMS_IN As Object
        Dim oITEMS_INX As Object
        Dim oSCHEDULES_IN As Object
        Dim oSCHEDULES_INX As Object
        Dim oCONDITIONS_IN As Object
        'Dim oSALESDOC As Object

        Dim NETPRICE As Decimal = 0
        Dim PERPRICE As Integer = 0
        Dim retMess As String = ""


        oFuncCtrl = CreateObject("SAP.Functions")
        oFuncCtrl.Connection = SAPConn

        oTheFunc = oFuncCtrl.Add("BAPI_SALESORDER_CREATEFROMDAT2")

        oHEADER_IN = oTheFunc.Exports.Item("ORDER_HEADER_IN")
        oHEADER_INX = oTheFunc.Exports.Item("ORDER_HEADER_INX")
        oPARTNERS = oTheFunc.tables.Item("ORDER_PARTNERS")
        oRETURN = oTheFunc.tables.Item("RETURN")
        oITEMS_IN = oTheFunc.tables.Item("ORDER_ITEMS_IN")
        oITEMS_INX = oTheFunc.tables.Item("ORDER_ITEMS_INX")
        oSCHEDULES_IN = oTheFunc.tables.Item("ORDER_SCHEDULES_IN")
        oSCHEDULES_INX = oTheFunc.tables.Item("ORDER_SCHEDULES_INX")
        oCONDITIONS_IN = oTheFunc.tables.Item("ORDER_CONDITIONS_IN")


        '===================
        ' IMPORT PARAMETERS 
        '===================

        oHEADER_IN.Value("DOC_TYPE") = "ZAGN"
        oHEADER_IN.Value("SALES_ORG") = PLANT
        oHEADER_IN.Value("DISTR_CHAN") = "DS"
        oHEADER_IN.Value("DIVISION") = "AG"
        oHEADER_IN.Value("REQ_DATE_H") = Format(dtpSODate.Value, "yyyyMMdd")            ' requestion date
        oHEADER_IN.Value("PMNTTRMS") = cboTOP.Text
        oHEADER_IN.Value("PRICE_DATE") = Format(dtpSODate.Value, "yyyyMMdd")            ' pricing date
        oHEADER_IN.Value("PURCH_NO_C") = txtPO.Text
        oHEADER_IN.Value("DOC_DATE") = Format(dtpSODate.Value, "yyyyMMdd")

        oHEADER_INX.VALUE("UPDATEFLAG") = "I"
        oHEADER_INX.Value("DOC_TYPE") = "X"
        oHEADER_INX.Value("SALES_ORG") = "X"
        oHEADER_INX.Value("DISTR_CHAN") = "X"
        oHEADER_INX.Value("DIVISION") = "X"
        oHEADER_INX.Value("REQ_DATE_H") = "X"
        oHEADER_INX.Value("PMNTTRMS") = "X"
        oHEADER_INX.Value("PRICE_DATE") = "X"
        oHEADER_INX.Value("PURCH_NO_C") = "X"
        oHEADER_INX.Value("DOC_DATE") = "X"


        '========
        ' TABLES
        '========

        oPARTNERS.Rows.Add()
        oPARTNERS.Value(1, "PARTN_ROLE") = "AG"     'for sold to
        oPARTNERS.Value(1, "PARTN_NUMB") = Format(Val(txtKdCust.Text), "0000000000")

        oPARTNERS.Rows.Add()
        oPARTNERS.Value(2, "PARTN_ROLE") = "WE"     'for ship to
        oPARTNERS.Value(2, "PARTN_NUMB") = Format(Val(txtKdCust.Text), "0000000000")


        For n = 1 To DGV.Rows.Count

            If IsNothing(DGV.Rows(n - 1).Cells(1).Value) Then
                Exit For
            End If

            oITEMS_IN.Rows.Add()
            oITEMS_IN.Value(n, "ITM_NUMBER") = Format(n * 10, "000000")
            oITEMS_IN.Value(n, "MATERIAL") = UCase(DGV.Rows(n - 1).Cells(1).Value)
            oITEMS_IN.Value(n, "PLANT") = PLANT
            oITEMS_IN.Value(n, "STORE_LOC") = cboSLoc.Text
            oITEMS_IN.Value(n, "ITEM_CATEG") = "ZAGE"
            oITEMS_IN.Value(n, "SALES_UNIT") = IIf(Trim(UCase(DGV.Rows(n - 1).Cells(6).Value)) = "PC", "ST", UCase(DGV.Rows(n - 1).Cells(6).Value))

            oITEMS_INX.Rows.Add()
            oITEMS_INX.Value(n, "ITM_NUMBER") = Format(n * 10, "000000")
            oITEMS_INX.Value(n, "UPDATEFLAG") = "I"
            oITEMS_INX.Value(n, "MATERIAL") = "X"
            oITEMS_INX.Value(n, "PLANT") = "X"
            oITEMS_INX.Value(n, "STORE_LOC") = "X"
            oITEMS_INX.Value(n, "ITEM_CATEG") = "X"
            oITEMS_INX.Value(n, "SALES_UNIT") = "X"

            oSCHEDULES_IN.Rows.Add()
            oSCHEDULES_IN.Value(n, "ITM_NUMBER") = Format(n * 10, "000000")
            oSCHEDULES_IN.Value(n, "SCHED_LINE") = Format(n, "0000")
            oSCHEDULES_IN.Value(n, "REQ_DATE") = Format(dtpSODate.Value, "yyyyMMdd")
            oSCHEDULES_IN.Value(n, "REQ_QTY") = DGV.Rows(n - 1).Cells(5).Value

            oSCHEDULES_INX.Rows.Add()
            oSCHEDULES_INX.Value(n, "ITM_NUMBER") = Format(n * 10, "000000")
            oSCHEDULES_INX.Value(n, "SCHED_LINE") = Format(n, "0000")
            oSCHEDULES_INX.Value(n, "UPDATEFLAG") = "X"
            oSCHEDULES_INX.Value(n, "REQ_DATE") = "X"
            oSCHEDULES_INX.Value(n, "REQ_QTY") = "X"



            NETPRICE = DGV.Rows(n - 1).Cells(7).Value
            PERPRICE = 1000

            If Trim(cboTax.Text) = "Tax Included" Then
                NETPRICE = Math.Round((NETPRICE * 100 / (100 + TAXRATE)) * 1000, 0)
                NETPRICE = NETPRICE / 1000
                PERPRICE = 1000
            End If


            oCONDITIONS_IN.Rows.Add()
            oCONDITIONS_IN.Value(n, "ITM_NUMBER") = Format(n * 10, "000000")
            oCONDITIONS_IN.Value(n, "COND_TYPE") = "ZGSP"
            oCONDITIONS_IN.Value(n, "COND_VALUE") = NETPRICE
            oCONDITIONS_IN.Value(n, "COND_P_UNT") = PERPRICE

        Next n




        If oTheFunc.Call Then

            For n = 1 To oRETURN.RowCount

                retMess = oRETURN.Value(n, "MESSAGE")

                'MsgBox(retMess)

                If InStr(retMess, "successfully") = 0 And InStr(retMess, "has been saved") = 0 Then
                    MsgBox(retMess)
                End If

                If InStr(retMess, "AGEN-Sales Order") > 0 And InStr(retMess, "has been saved") > 0 Then
                    SalesOrderNo = Mid(retMess, 21, 10)
                    lblSONo.Text = SalesOrderNo
                    MsgBox(retMess, MsgBoxStyle.Information, "Info")
                    SaveOkay = True
                    Exit For
                End If

            Next n


            'oSALESDOC = oTheFunc.Imports.Item("SALESDOCUMENT")
            'SalesOrderNo = oSALESDOC.Value


            If Trim(SalesOrderNo) <> "" Then

                FuncCommit = oFuncCtrl.Add("BAPI_TRANSACTION_COMMIT")

                If FuncCommit.Call Then
                    SaveOkay = True
                    'lblSONo.Text = SalesOrderNo
                End If

            End If

        End If


        

    End Sub

    Sub Reset()

        txtKdCust.Text = ""
        lblNmCust.Text = ""
        txtPO.Text = ""
        cboTOP.SelectedIndex = -1
        lblSONo.Text = ""
        dtpSODate.Value = Format(Now, "dd/MM/yyyy")
        lblTotal.Text = ""
        lblSONo.Text = ""
        lblDelvNo.Text = ""

        Do Until DGV.Rows.Count = 1
            DGV.Rows.RemoveAt(0)
        Loop

        txtKdCust.Select()


    End Sub

    Private Sub BATAL()

        Call Local_Connect()

        Query = "DELETE FROM [" & TEMPSALES & "] "

        CMD = New SqlCommand
        CMD.Connection = Conn
        CMD.CommandText = Query
        CMD.ExecuteNonQuery()

        DGV.Rows.Clear()

        txtKdCust.Text = ""
        lblNmCust.Text = ""
        txtPO.Text = ""
        cboSLoc.SelectedIndex = 0
        cboTOP.SelectedIndex = -1
        lblSONo.Text = ""
        dtpSODate.Value = Now
        lblDelvNo.Text = ""
        cboTax.SelectedIndex = 0
        lblSubTotal.Text = ""
        txtDiscount.Text = ""
        txtFreight.Text = ""
        lblTotal.Text = ""

        txtKdCust.Select()

        TAXRATE = TAX_RATE(dtpSODate.Value)

    End Sub

    Private Sub GRANDTOTAL()

        Dim n As Integer = 0

        TOTSALES = 0

        For n = 0 To DGV.Rows.Count - 1

            TOTSALES = TOTSALES + DGV.Rows(n).Cells(8).Value    '//  TOTAL

            'If InStr(DGV.Rows(n).Cells(7).Value, ".") = 0 And InStr(DGV.Rows(n).Cells(7).Value, ",") = 0 Then
            '    DGV.Rows(n).Cells(7).Value = Format(DGV.Rows(n).Cells(7).Value, "#,##0.00")
            'End If

        Next

        lblSubTotal.Text = Format(TOTSALES, "#,##0")

        lblTotal.Text = Format(TOTSALES - VALNUM(txtDiscount.Text) + VALNUM(txtFreight.Text), "#,##0")


    End Sub

    Private Sub FrmSalesReceipt_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing

    End Sub

    Private Sub FrmSales_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Me.WindowState = FormWindowState.Maximized

        Call LOCAL_CONNECT()

        Call GET_PERIOD()

        Call GET_SLOC()


        Query = "SELECT * FROM [PAYTERMS] ORDER BY [recno] "

        DS = New DataSet
        DA = New SqlDataAdapter(Query, Conn)
        DA.Fill(DS, "TOP")

        cboTOP.Items.Clear()

        For n = 0 To DS.Tables("TOP").Rows.Count - 1
            cboTOP.Items.Add(DS.Tables("TOP").Rows(n)("ZTERM"))
        Next


        '// DESIGN DATAGRIDVIEW

        DGV.BorderStyle = BorderStyle.Fixed3D
        DGV.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249)
        'DGV.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
        DGV.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise
        DGV.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke
        DGV.BackgroundColor = Color.White

        DGV.EnableHeadersVisualStyles = False
        DGV.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        DGV.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72)
        DGV.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        DGV.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72)
        'DGV.RowHeadersDefaultCellStyle.ForeColor = Color.FromArgb(20, 25, 72)
        DGV.RowHeadersVisible = False

        DGV.Columns(0).HeaderText = "NO"
        DGV.Columns(1).HeaderText = "PRODUCT #"
        DGV.Columns(2).HeaderText = "DESCRIPTION"
        DGV.Columns(3).HeaderText = "COLOR"
        DGV.Columns(4).HeaderText = "PACK"
        DGV.Columns(5).HeaderText = "QTTY"
        DGV.Columns(6).HeaderText = "UNIT"
        DGV.Columns(7).HeaderText = "PRICE"
        DGV.Columns(8).HeaderText = "TOTAL"

        DGV.Columns(0).Width = 40
        DGV.Columns(1).Width = 150
        DGV.Columns(2).Width = 290
        DGV.Columns(3).Width = 80
        DGV.Columns(4).Width = 80
        DGV.Columns(5).Width = 90
        DGV.Columns(6).Width = 50
        DGV.Columns(7).Width = 90
        DGV.Columns(8).Width = 110

        DGV.Columns(0).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DGV.Columns(1).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DGV.Columns(2).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DGV.Columns(3).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DGV.Columns(4).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DGV.Columns(5).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DGV.Columns(6).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DGV.Columns(7).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DGV.Columns(8).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

        DGV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        DGV.ColumnHeadersHeight = 30

        DGV.Columns(4).DefaultCellStyle.Format = "N0"
        DGV.Columns(5).DefaultCellStyle.Format = "N2"
        DGV.Columns(7).DefaultCellStyle.Format = "N2"
        DGV.Columns(8).DefaultCellStyle.Format = "N0"

        DGV.Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        DGV.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DGV.Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DGV.Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DGV.Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        With ColUnit
            .Items.Clear()
            .Items.Add("YD")
            .Items.Add("M")
            .Items.Add("PC")
            .Items.Add("SET")
        End With

        Me.cboSLoc.SelectedIndex = 0
        Me.cboTax.SelectedIndex = 0

        Me.txtKdCust.Select()   ' Object Focus


        '=====================
        '  CREATE TEMP TABLE
        '=====================
Ulang:
        TEMPSALES = "TMPSLS" & Format(NO, "00")

        Try

            Query = "SELECT * FROM [" & TEMPSALES & "] "

            DS = New DataSet
            DA = New SqlDataAdapter(Query, Conn)
            DA.Fill(DS, "TMPSLS")

            If DS.Tables("TMPSLS").Rows.Count > 0 Then
                NO = NO + 1
                GoTo Ulang
            End If

        Catch ex As Exception

            Query = "SELECT * INTO [" & TEMPSALES & "] FROM [STOCKBCD] WHERE 1=2 "

            CMD = New SqlCommand
            CMD.Connection = Conn
            CMD.CommandText = Query
            CMD.ExecuteNonQuery()

            GoTo Ulang

        End Try


        Call BATAL()


    End Sub

    Private Sub txtKdCust_GotFocus(sender As Object, e As EventArgs) Handles txtKdCust.GotFocus
        Me.txtKdCust.BackColor = Color.FromArgb(254, 240, 158)
    End Sub

    Private Sub txtKdCust_KeyDown(sender As Object, e As KeyEventArgs) Handles txtKdCust.KeyDown
        If e.KeyCode = Keys.Enter Then
            Call btnFindCust_Click(txtKdCust, e)
        End If
    End Sub

    Private Sub txtKdCust_LostFocus(sender As Object, e As EventArgs) Handles txtKdCust.LostFocus
        Me.txtKdCust.BackColor = Color.White
    End Sub

    Private Sub btnFindCust_Click(sender As Object, e As EventArgs) Handles btnFindCust.Click

Ulang:

        Query = "SELECT flkode,flnama,flala1,flwila,flphon,intercomp FROM [MASCUST] WHERE [flkode]='" & Me.txtKdCust.Text & "'"

        DS = New DataSet
        DA = New SqlDataAdapter(Query, Conn)
        DA.Fill(DS, "CUSTOMER")

        If DS.Tables("CUSTOMER").Rows.Count <> 1 Then

            FrmFindCust.txtFilter.Text = txtKdCust.Text
            FrmFindCust.ShowDialog()

            txtKdCust.Select()
            txtKdCust.Text = CARI

            If CARI = "" Then
                Exit Sub
            End If

            GoTo Ulang

            Exit Sub
        End If

        KDCUST = DS.Tables("CUSTOMER").Rows(0)("flkode")
        NMCUST = DS.Tables("CUSTOMER").Rows(0)("flnama")
        INTCOMP = DS.Tables("CUSTOMER").Rows(0)("intercomp")


        lblNmCust.Text = DS.Tables("CUSTOMER").Rows(0)("flnama")
        lblNmCust.Text = lblNmCust.Text & vbCrLf & DS.Tables("CUSTOMER").Rows(0)("flala1")
        lblNmCust.Text = lblNmCust.Text & vbCrLf & DS.Tables("CUSTOMER").Rows(0)("flwila")

        txtPO.Select()


    End Sub


    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click

        If DGV.Rows.Count > 1 Then
            If MessageBox.Show("Do you really want to cancel this transaction ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.No Then
                Exit Sub
            End If

        End If

        Call BATAL()

        If SaveOkay = True Then
            DOC_NO = SalesOrderNo
        Else
            DOC_NO = ""
        End If

        Me.Dispose()

    End Sub

    Private Sub txtPO_GotFocus(sender As Object, e As EventArgs) Handles txtPO.GotFocus
        txtPO.BackColor = Color.FromArgb(254, 240, 158)
    End Sub

    Private Sub txtPO_KeyDown(sender As Object, e As KeyEventArgs) Handles txtPO.KeyDown
        If e.KeyCode = Keys.Enter Then
            cboSLoc.Select()
        End If
    End Sub

    Private Sub txtPO_LostFocus(sender As Object, e As EventArgs) Handles txtPO.LostFocus
        txtPO.BackColor = Color.White
        If Trim(txtPO.Text) = "" Then
            txtPO.Text = "-"
        End If
    End Sub

    Private Sub cboTOP_KeyDown(sender As Object, e As KeyEventArgs) Handles cboTOP.KeyDown
        If e.KeyCode = Keys.Enter Then
            dtpSODate.Select()
        End If
    End Sub

    Private Sub DGV_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DGV.CellEndEdit

        If e.ColumnIndex = 1 Then   '// KODE BARANG

Ulang:
            Query = "SELECT kdprod,nmprod,kdwarna,nmwarna,unit,hbeli,hjual FROM [MASPROD] " & _
                    "WHERE  [kdprod]='" & DGV.Rows(e.RowIndex).Cells(1).Value & "'"

            DS = New DataSet
            DA = New SqlDataAdapter(Query, Conn)
            DA.Fill(DS, "PRODUK")

            If DS.Tables("PRODUK").Rows.Count <> 1 Then

                With FrmFindItem
                    .txtFilter.Text = DGV.Rows(e.RowIndex).Cells(1).Value
                    .ShowDialog()
                End With

                If CARI = "" Then
                    SendKeys.Send("{up}")
                    DGV.Rows.RemoveAt(e.RowIndex)
                    Exit Sub
                Else
                    DGV.Rows(e.RowIndex).Cells(1).Value = CARI
                    GoTo Ulang
                End If

            Else

                DGV.Rows(e.RowIndex).Cells(0).Value = e.RowIndex + 1
                DGV.Rows(e.RowIndex).Cells(1).Value = DS.Tables("PRODUK").Rows(0)("kdprod")
                DGV.Rows(e.RowIndex).Cells(2).Value = DS.Tables("PRODUK").Rows(0)("nmprod")
                DGV.Rows(e.RowIndex).Cells(3).Value = DS.Tables("PRODUK").Rows(0)("nmwarna")
                DGV.Rows(e.RowIndex).Cells(6).Value = UCase(DS.Tables("PRODUK").Rows(0)("unit"))
                DGV.Rows(e.RowIndex).Cells(7).Value = DS.Tables("PRODUK").Rows(0)("hjual")

                DGV.Rows(e.RowIndex).Cells(4).ReadOnly = False
                DGV.Rows(e.RowIndex).Cells(5).ReadOnly = False
                DGV.Rows(e.RowIndex).Cells(7).ReadOnly = False


                DGV.CurrentCell = DGV(4, e.RowIndex)

                SendKeys.Send("{up}")

            End If


        ElseIf e.ColumnIndex = 4 Then       ' roll/piece

            'DGV.CurrentCell = DGV(5, e.RowIndex)    '
            SendKeys.Send("{up}")


        ElseIf e.ColumnIndex = 5 Then       ' yard

            DGV.Rows(e.RowIndex).Cells(5).Value = DGV.Rows(e.RowIndex).Cells(5).Value * 1
            DGV.Rows(e.RowIndex).Cells(8).Value = DGV.Rows(e.RowIndex).Cells(5).Value * DGV.Rows(e.RowIndex).Cells(7).Value

            'DGV.CurrentCell = DGV(7, e.RowIndex)    '
            SendKeys.Send("{up}")


        ElseIf e.ColumnIndex = 7 Then       ' sales price

            DGV.Rows(e.RowIndex).Cells(7).Value = DGV.Rows(e.RowIndex).Cells(7).Value * 1
            DGV.Rows(e.RowIndex).Cells(8).Value = DGV.Rows(e.RowIndex).Cells(5).Value * DGV.Rows(e.RowIndex).Cells(7).Value

            'DGV.CurrentCell = DGV(0, e.RowIndex)    ' Fokus di kolom Kode
            SendKeys.Send("{up}")

        End If


        Call GRANDTOTAL()


    End Sub


    Private Sub DGV_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles DGV.EditingControlShowing

        Dim Row As Integer = DGV.CurrentCell.RowIndex
        Dim Col As Integer = DGV.CurrentCell.ColumnIndex


        '// uppercase textbox

        If TypeOf e.Control Is TextBox Then
            DirectCast(e.Control, TextBox).CharacterCasing = CharacterCasing.Upper
        End If

        '// numeric textbox

        If (Col >= 4 And Col <= 5) Or Col = 7 Then
            AddHandler CType(e.Control, TextBox).KeyPress, AddressOf TextBoxNum_keyPress
        End If


    End Sub

    Private Sub TextBoxNum_keyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs)

        Dim Col As Integer = DGV.CurrentCell.ColumnIndex
        Dim Row As Integer = DGV.CurrentCell.RowIndex

        If (Col >= 4 And Col <= 5) Or Col = 7 Then

            'If Row >= 0 Then
            '    If Val(dgv.Rows(Row - 1).Cells(1).Value) = 0 Then
            '        e.Handled = True
            '        Exit Sub
            '    End If
            'End If

            If Not (Char.IsDigit(CChar(CStr(e.KeyChar))) Or e.KeyChar = Chr(8)) And Not e.KeyChar = "," Then
                e.Handled = True
            End If


        End If

    End Sub

    Private Sub DGV_KeyDown(sender As Object, e As KeyEventArgs) Handles DGV.KeyDown

        Dim Row As Integer = DGV.CurrentCell.RowIndex
        Dim Col As Integer = DGV.CurrentCell.ColumnIndex

        If e.KeyCode = Keys.Enter Then

            'e.Handled = True

            If Col = 1 Then

                If Trim(DGV.Rows(Row).Cells(Col).Value) = "" Then

                    FrmFindItem.ShowDialog()

                    If CARI <> "" Then

                        DGV.Rows(Row).Cells(1).Value = CARI

                        Query = "SELECT kdprod,nmprod,kdwarna,nmwarna,unit,hbeli,hjual FROM [MASPROD] " & _
                                 "WHERE  [kdprod]='" & DGV.Rows(Row).Cells(1).Value & "'"

                        DS = New DataSet
                        DA = New SqlDataAdapter(Query, Conn)
                        DA.Fill(DS, "PRODUK")

                        DGV.Rows(Row).Cells(0).Value = Row + 1
                        DGV.Rows(Row).Cells(1).Value = DS.Tables("PRODUK").Rows(0)("kdprod")
                        DGV.Rows(Row).Cells(2).Value = DS.Tables("PRODUK").Rows(0)("nmprod")
                        DGV.Rows(Row).Cells(3).Value = DS.Tables("PRODUK").Rows(0)("nmwarna")
                        DGV.Rows(Row).Cells(6).Value = UCase(DS.Tables("PRODUK").Rows(0)("unit"))
                        DGV.Rows(Row).Cells(7).Value = DS.Tables("PRODUK").Rows(0)("hjual")

                        DGV.Rows(Row).Cells(4).ReadOnly = False
                        DGV.Rows(Row).Cells(5).ReadOnly = False
                        DGV.Rows(Row).Cells(7).ReadOnly = False

                        DGV.CurrentCell = DGV(4, Row)


                    End If

                End If


            ElseIf Col = 4 Then



            ElseIf Col = 5 Then

                If Val(DGV.Rows(Row).Cells(Col).Value) > 0 Then
                    DGV.Rows(Row).Cells(8).Value = DGV.Rows(Row).Cells(5).Value * DGV.Rows(Row).Cells(7).Value
                    DGV.CurrentCell = DGV(1, DGV.Rows.Count - 1)
                End If


            ElseIf Col = 7 Then

                If Val(DGV.Rows(Row).Cells(Col).Value) > 0 Then
                    DGV.Rows(Row).Cells(8).Value = DGV.Rows(Row).Cells(5).Value * DGV.Rows(Row).Cells(7).Value
                    DGV.CurrentCell = DGV(1, DGV.Rows.Count - 1)

                End If


            End If

            Call GRANDTOTAL()

        End If

    End Sub


    Private Sub btnBarcode_Click(sender As Object, e As EventArgs) Handles btnBarcode.Click

        Dim i As Integer
        Dim z As Integer

        With FrmSalesBarcode
            .TMPSALES = TEMPSALES
            .ShowDialog()
        End With

  
        Try

            Query = "SELECT SLS.corak,MAT.nmprod,MAT.nmwarna,MAT.unit,MAT.hjual,COUNT(SLS.corak) AS pack,SUM(SLS.yard) AS yds " & _
                      "FROM [" & TEMPSALES & "] SLS INNER JOIN [MASPROD] MAT ON MAT.kdprod=SLS.corak " & _
                     "GROUP BY SLS.corak,MAT.nmprod,MAT.nmwarna,MAT.unit,MAT.hjual "

            DS = New DataSet
            DA = New SqlDataAdapter(Query, Conn)
            DA.Fill(DS, "TMPSALES")

            For n = 1 To DS.Tables("TMPSALES").Rows.Count

                '// if already exist

                For i = 1 To DGV.Rows.Count
                    If DGV.Rows(i - 1).Cells(1).Value = DS.Tables("TMPSALES").Rows(n - 1)("corak") Then
                        Exit For
                    End If
                Next

                If i > DGV.Rows.Count Then
                    z = DGV.Rows.Add()
                Else
                    z = i - 1
                End If

                DGV.Rows(z).Cells(1).Value = DS.Tables("TMPSALES").Rows(n - 1)(0)   ' product
                DGV.Rows(z).Cells(2).Value = DS.Tables("TMPSALES").Rows(n - 1)(1)   ' description
                DGV.Rows(z).Cells(3).Value = DS.Tables("TMPSALES").Rows(n - 1)(2)   ' color
                DGV.Rows(z).Cells(4).Value = DS.Tables("TMPSALES").Rows(n - 1)(5)   ' qty in packs
                DGV.Rows(z).Cells(5).Value = DS.Tables("TMPSALES").Rows(n - 1)(6)   ' qty in yards
                DGV.Rows(z).Cells(6).Value = DS.Tables("TMPSALES").Rows(n - 1)(3)   ' unit
                DGV.Rows(z).Cells(7).Value = DS.Tables("TMPSALES").Rows(n - 1)(4)   ' price
                DGV.Rows(z).Cells(8).Value = DS.Tables("TMPSALES").Rows(n - 1)(6) * DS.Tables("TMPSALES").Rows(n - 1)(4)

            Next

            For n = 1 To DGV.Rows.Count
                If Trim(DGV.Rows(n - 1).Cells(2).Value) = "" Then Exit For
                DGV.Rows(n - 1).Cells(0).Value = n
            Next

            DGV.Focus()
            DGV.CurrentCell = DGV(7, 0)


            '========================
            ' JIKA ADA JUAL POTONGAN
            '========================

            Query = "SELECT corak,potong FROM [" & TEMPSALES & "] WHERE [potong]=1 "

            DS = New DataSet
            DA = New SqlDataAdapter(Query, Conn)
            DA.Fill(DS, "TMPSALES")

            For n = 1 To DS.Tables("TMPSALES").Rows.Count

                For i = 1 To DGV.Rows.Count - 1

                    If DGV.Rows(i - 1).Cells(1).Value = "" Then Exit For

                    If DGV.Rows(i - 1).Cells(1).Value = DS.Tables("TMPSALES").Rows(n - 1)("corak") Then

                        DGV.Rows(i - 1).Cells(4).Value = DGV.Rows(i - 1).Cells(4).Value - 1

                    End If

                Next

            Next


            Call GRANDTOTAL()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try



    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click

        Try
            If MessageBox.Show("Cancel this transaction ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Yes Then
                Call BATAL()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        Call GRANDTOTAL()

        If Trim(lblNmCust.Text) = "" Then
            MessageBox.Show("Please enter customer ! ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtKdCust.Select()
            Exit Sub
        End If

        If Trim(txtPO.Text) = "" Then
            'MessageBox.Show("Please enter Purchase Order ! ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtPO.Text = "-"
            'Exit Sub
        End If

        If Trim(cboTOP.Text) = "" Then
            MessageBox.Show("Please select Payment Terms ! ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cboTOP.Select()
            Exit Sub
        End If

        If Trim(cboSLoc.Text) = "" Then
            MessageBox.Show("Please select Location ! ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cboSLoc.Select()
            Exit Sub
        End If


        For n = 1 To DGV.Rows.Count

            If Trim(DGV.Rows(n - 1).Cells(2).Value) = "" Then
                Exit For
            End If

            'If DGV.Rows(n - 1).Cells(4).Value = 0 Then
            '    MessageBox.Show("Please enter quantity ! ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            '    DGV.Select()
            '    DGV.CurrentCell = DGV(4, n - 1)
            '    Exit Sub
            'End If

            If DGV.Rows(n - 1).Cells(5).Value = 0 Then
                MessageBox.Show("Please enter quantity ! ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                DGV.Select()
                DGV.CurrentCell = DGV(5, n - 1)
                Exit Sub
            End If

            If DGV.Rows(n - 1).Cells(7).Value = 0 Then
                MessageBox.Show("Please enter sales price ! ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                DGV.Select()
                DGV.CurrentCell = DGV(7, n - 1)
                Exit Sub
            End If

        Next


        '// check if make selling without barcode scanning

        Query = "SELECT * FROM [" & TEMPSALES & "]"

        DS = New DataSet
        DA = New SqlDataAdapter(Query, Conn)
        DA.Fill(DS, "SALESBAR")

        If DS.Tables("SALESBAR").Rows.Count = 0 Then

            'MessageBox.Show("Please input using barcode !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)

            If MessageBox.Show("Are you really want to save without barcode ?", "Confirm", _
                               MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = vbNo Then

                btnBarcode.Select()
                Exit Sub

            End If

        End If



        '// check posting period

        Dim CurrPeriod As Long = Val(CurYearPeriode & CurMonPeriode)
        Dim PrevPeriod As Long = Val(PrevYearPeriode & PrevMonPeriode)
        Dim PostDate As Long = Val(Mid(Format(dtpSODate.Value, "yyyyMMdd"), 1, 6))

        'If PostDate < PrevPeriod Or PostDate > CurrPeriod Then
        '    MsgBox("Posting only possible in periods " & PrevMonPeriode & "-" & PrevYearPeriode & " and " & CurMonPeriode & "-" & CurYearPeriode & " ! ", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Warning")
        '    dtpSODate.Focus()
        '    Exit Sub
        'End If


        If MessageBox.Show("Save this transaction ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = vbNo Then
            Exit Sub
        End If


        Me.Cursor = Cursors.WaitCursor

        TAXRATE = TAX_RATE(dtpSODate.Value)

        Call BAPI_SALESORDER_CREATEFROMDAT2()

        If SaveOkay And Trim(SalesOrderNo) <> "" Then

            DeliveryNo = ""

            Do Until Trim(DeliveryNo) <> ""
                Call GET_DELIVERY_NO()
            Loop

            'Call CHANGE_ACTUAL_GI()

            'Call BAPI_BILLINGDOC_CREATEMULTIPLE()


            Call Local_Connect()

            If ConnectStatus = False Then
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            If INTCOMP = 1 Then
                Call Host_Connect()
            End If


            '=====================
            ' UPDATE SALES HEADER
            '=====================

            Query = "INSERT INTO [TRANSLSH] (SO_NO, SO_DATE, DELV_NO, CUST_NO, CUST_NAME, PO_NO, PAY_TERMS, SLOC, TOTAL, TAX, REMARK, DISCOUNT, FREIGHT) " & _
                    "VALUES ('" & SalesOrderNo & "','" & DATECHAR(dtpSODate.Value) & "','" & DeliveryNo & "'," & _
                            "'" & KDCUST & "','" & PETIK(NMCUST) & "','" & txtPO.Text & "','" & cboTOP.Text & "'," & _
                            "'" & cboSLoc.Text & "'," & Math.Round(TOTSALES, 0) & ",'" & Mid(cboTax.Text, 5) & "'," & _
                            "'" & txtNotes.Text & "'," & VALNUM(txtDiscount.Text) & "," & VALNUM(txtFreight.Text) & ")"

            CMD = New SqlCommand
            CMD.Connection = Conn
            CMD.CommandText = Query
            CMD.ExecuteNonQuery()


            '=====================
            ' UPDATE SALES DETAIL
            '=====================

            For n = 0 To DGV.Rows.Count - 1

                If Trim(DGV.Rows(n).Cells(2).Value) = "" Then Exit For


                Query = "UPDATE [MASPROD] " & _
                        "SET    [HJUAL]=" & Replace(DGV.Rows(n).Cells(7).Value, ",", ".") & "," & _
                              " [STOCK1] = [STOCK1] - " & DGV.Rows(n).Cells(4).Value & "," & _
                              " [STOCK2] = [STOCK2] - " & Replace(DGV.Rows(n).Cells(5).Value, ",", ".") & " " & _
                        "WHERE  [KDPROD]='" & DGV.Rows(n).Cells(1).Value & "'"

                CMD = New SqlCommand
                CMD.Connection = Conn
                CMD.CommandText = Query
                CMD.ExecuteNonQuery()


                Query = "INSERT INTO [TRANSLSD] (SO_NO, ITEM_NO, KDPROD, NMPROD, COLOR, QTY1, QTY2, UNIT, PRICE) " & _
                        "VALUES ('" & lblSONo.Text & "','" & DGV.Rows(n).Cells(0).Value & "'," & _
                                "'" & DGV.Rows(n).Cells(1).Value & "','" & PETIK(DGV.Rows(n).Cells(2).Value) & "'," & _
                                "'" & DGV.Rows(n).Cells(3).Value & "'," & DGV.Rows(n).Cells(4).Value & "," & _
                                " " & DESIMAL(DGV.Rows(n).Cells(5).Value) & ",'" & DGV.Rows(n).Cells(6).Value & "'," & _
                                " " & DESIMAL(DGV.Rows(n).Cells(7).Value) & ")"

                CMD = New SqlCommand
                CMD.Connection = Conn
                CMD.CommandText = Query
                CMD.ExecuteNonQuery()

            Next


            '===================
            '  UPDATE BAR CODE
            '===================

            Dim SAT As String = "YD"
            Dim LOK As String = PLANT


            Query = "SELECT * FROM [" & TEMPSALES & "] "    ' WHERE [corak]='" & DGV.Rows(n).Cells(1).Value & "'"

            DS = New DataSet
            DA = New SqlDataAdapter(Query, Conn)
            DA.Fill(DS, "SLSBCD")

            For i = 0 To DS.Tables("SLSBCD").Rows.Count - 1

                '=======================
                ' INTER COMPANY PROCESS
                '=======================

                If INTCOMP = 1 Then

                    Query = "INSERT INTO [AG_TRANBCD] (agen,barcode,partai,corak,warna,yard,meter,grade,satuan,lebar,nomor_sj,tgl_sj) " & _
                            "VALUES ('" & PETIK(NMCUST) & "','" & DS.Tables("SLSBCD").Rows(i)("BARCODE") & "'," & _
                                    "'" & DS.Tables("SLSBCD").Rows(i)("PARTAI") & "','" & DS.Tables("SLSBCD").Rows(i)("CORAK") & "'," & _
                                    "'" & DS.Tables("SLSBCD").Rows(i)("WARNA") & "'," & Replace(DS.Tables("SLSBCD").Rows(i)("YARD"), ",", ".") & "," & _
                                    " " & Replace(DS.Tables("SLSBCD").Rows(i)("METER"), ",", ".") & ",'" & DS.Tables("SLSBCD").Rows(i)("GRADE") & "'," & _
                                    "'" & DS.Tables("SLSBCD").Rows(i)("UNIT") & "','" & DS.Tables("SLSBCD").Rows(i)("LEBAR") & "'," & _
                                    "'" & lblDelvNo.Text & "','" & DATECHAR(dtpSODate.Value) & "')"

                    CMD = New SqlCommand
                    CMD.Connection = HostConn
                    CMD.CommandText = Query
                    CMD.ExecuteNonQuery()

                End If


                '============
                ' STOCK CARD 
                '============

                If IsDBNull(DS.Tables("SLSBCD").Rows(i)("UNIT")) = True Then
                    SAT = "YD"
                Else
                    SAT = Trim(DS.Tables("SLSBCD").Rows(i)("UNIT"))
                    If SAT = "YARD" Or SAT = "" Then
                        SAT = "YD"
                    ElseIf SAT = "METER" Then
                        SAT = "M"
                    End If
                End If


                If IsDBNull(DS.Tables("SLSBCD").Rows(i)("SLOC")) = True Then
                    LOK = PLANT
                Else
                    If Trim(DS.Tables("SLSBCD").Rows(i)("SLOC")) = "" Then
                        LOK = PLANT
                    End If
                End If


                Query = "INSERT INTO [STOCKCARD] (postdate,so,barcode,corak,warna,grade,partai,lebar,yard,meter,unit,sloc,potong) " & _
                        "VALUES ('" & DATECHAR(dtpSODate.Value) & "','" & SalesOrderNo & "'," & _
                                "'" & DS.Tables("SLSBCD").Rows(i)("BARCODE") & "','" & DS.Tables("SLSBCD").Rows(i)("CORAK") & "'," & _
                                "'" & DS.Tables("SLSBCD").Rows(i)("WARNA") & "','" & DS.Tables("SLSBCD").Rows(i)("GRADE") & "'," & _
                                "'" & DS.Tables("SLSBCD").Rows(i)("PARTAI") & "','" & DS.Tables("SLSBCD").Rows(i)("LEBAR") & "'," & _
                                " " & Replace(DS.Tables("SLSBCD").Rows(i)("YARD"), ",", ".") & "," & _
                                " " & Replace(DS.Tables("SLSBCD").Rows(i)("METER"), ",", ".") & "," & _
                                "'" & SAT & "','" & LOK & "'," & DS.Tables("SLSBCD").Rows(i)("POTONG") & ")"

                CMD = New SqlCommand
                CMD.Connection = Conn
                CMD.CommandText = Query
                CMD.ExecuteNonQuery()


                If DS.Tables("SLSBCD").Rows(i)("POTONG") = 1 Then

                    Dim YDS As Double = DS.Tables("SLSBCD").Rows(i)("YARD")
                    Dim MTR As Double = DS.Tables("SLSBCD").Rows(i)("METER")

                    Query = "UPDATE [STOCKBCD] " & _
                               "SET [YARD] = [YARD] - " & Replace(YDS, ",", ".") & "," & _
                                  " [METER] = [METER] - " & Replace(MTR, ",", ".") & " " & _
                             "WHERE [BARCODE] = '" & DS.Tables("SLSBCD").Rows(i)("BARCODE") & "'"
                Else

                    Query = "DELETE FROM [STOCKBCD] WHERE [BARCODE]='" & DS.Tables("SLSBCD").Rows(i)("BARCODE") & "'"

                End If

                CMD = New SqlCommand
                CMD.Connection = Conn
                CMD.CommandText = Query
                CMD.ExecuteNonQuery()

            Next



            '=====================
            ' PRINT SALES RECEIPT 
            '=====================

            If MessageBox.Show("Print Sales Receipt No. " & lblSONo.Text & "  ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = vbYes Then

                Query = "SELECT TRANSLSD.SO_NO, TRANSLSD.ITEM_NO, TRANSLSD.KDPROD, TRANSLSD.NMPROD, TRANSLSD.COLOR, TRANSLSD.QTY1, TRANSLSD.QTY2, TRANSLSD.UNIT, TRANSLSD.PRICE, " & _
                              " TRANSLSH.SO_NO, TRANSLSH.SO_DATE, TRANSLSH.DELV_NO, TRANSLSH.INV_NO, TRANSLSH.CUST_NO, TRANSLSH.CUST_NAME, TRANSLSH.PO_NO, TRANSLSH.PAY_TERMS, TRANSLSH.SLOC, TRANSLSH.TOTAL, TRANSLSH.TAX " & _
                          "FROM TRANSLSD INNER JOIN TRANSLSH ON TRANSLSD.SO_NO = TRANSLSH.SO_NO " & _
                         "WHERE TRANSLSD.SO_NO = '" & lblSONo.Text & "' " & _
                         "ORDER BY CAST(TRANSLSD.ITEM_NO AS INT) "

                DS = New DataSet
                DA = New SqlDataAdapter(Query, Conn)
                DA.Fill(DS, "SO")

                Dim RecCount As Long = DS.Tables("SO").Rows.Count

                Dim ItemNo As Integer = Val(DS.Tables("SO").Rows(RecCount - 1)("item_no"))

                If (RecCount Mod 10) > 0 Then

                    For n = 1 To (10 - (RecCount Mod 10))

                        Query = "INSERT INTO [TRANSLSD] (so_no,item_no,kdprod,nmprod,color,qty1,qty2,unit,price) " & _
                                "VALUES ('" & SalesOrderNo & "','" & Trim((ItemNo + n).ToString) & "','', '', '', 0, 0, '', 0) "

                        CMD = New SqlCommand
                        CMD.Connection = Conn
                        CMD.CommandText = Query
                        CMD.ExecuteNonQuery()

                    Next

                    Query = "SELECT TRANSLSD.SO_NO, TRANSLSD.ITEM_NO, TRANSLSD.KDPROD, TRANSLSD.NMPROD, TRANSLSD.COLOR, TRANSLSD.QTY1, TRANSLSD.QTY2, TRANSLSD.UNIT, TRANSLSD.PRICE," & _
                                  " TRANSLSH.SO_NO, TRANSLSH.SO_DATE, TRANSLSH.DELV_NO, TRANSLSH.INV_NO, TRANSLSH.CUST_NO, TRANSLSH.CUST_NAME, TRANSLSH.PO_NO, TRANSLSH.PAY_TERMS, TRANSLSH.SLOC, TRANSLSH.TOTAL, TRANSLSH.TAX " & _
                              "FROM TRANSLSD INNER JOIN TRANSLSH ON TRANSLSD.SO_NO = TRANSLSH.SO_NO " & _
                             "WHERE TRANSLSD.SO_NO = '" & lblSONo.Text & "' " & _
                             "ORDER BY CAST(TRANSLSD.ITEM_NO AS INT) "

                End If


                Dim PrintControl As New ClassPrintReport

                PrintControl.CetakReport("RptSalesReceipt.rdlc", "DataSet4", Query, lblNmCust.Text, TAXRATE)


                Query = "DELETE FROM [TRANSLSD] WHERE [so_no]='" & lblSONo.Text & "' AND [kdprod]='' "

                CMD = New SqlCommand
                CMD.Connection = Conn
                CMD.CommandText = Query
                CMD.ExecuteNonQuery()

            End If


            '=====================
            ' PRINT DELIVERY NOTE
            '=====================

            If MessageBox.Show("Print Delivery No. " & DeliveryNo & "  ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = vbYes Then

                Query = "SELECT TRANSLSD.SO_NO, TRANSLSD.ITEM_NO, TRANSLSD.KDPROD, TRANSLSD.NMPROD, TRANSLSD.COLOR, TRANSLSD.QTY1, TRANSLSD.QTY2, TRANSLSD.UNIT, TRANSLSD.PRICE," & _
                              " TRANSLSH.SO_NO, TRANSLSH.SO_DATE, TRANSLSH.DELV_NO, TRANSLSH.INV_NO, TRANSLSH.CUST_NO, TRANSLSH.CUST_NAME, TRANSLSH.PO_NO, TRANSLSH.PAY_TERMS, TRANSLSH.SLOC, TRANSLSH.TOTAL, TRANSLSH.TAX " & _
                          "FROM TRANSLSD INNER JOIN TRANSLSH ON TRANSLSD.SO_NO = TRANSLSH.SO_NO " & _
                         "WHERE TRANSLSH.DELV_NO = '" & DeliveryNo & "' " & _
                         "ORDER BY CAST(TRANSLSD.ITEM_NO AS INT) "

                DS = New DataSet
                DA = New SqlDataAdapter(Query, Conn)
                DA.Fill(DS, "SO")

                Dim RecCount As Long = DS.Tables("SO").Rows.Count

                Dim ItemNo As Integer = Val(DS.Tables("SO").Rows(RecCount - 1)("item_no"))


                If (RecCount Mod 10) > 0 Then

                    For n = 1 To (10 - (RecCount Mod 10))

                        Query = "INSERT INTO [TRANSLSD] (so_no,item_no,kdprod,nmprod,color,qty1,qty2,unit,price) " & _
                                "VALUES ('" & SalesOrderNo & "','" & Trim((ItemNo + n).ToString) & "','', '', '', 0, 0, '', 0) "

                        CMD = New SqlCommand
                        CMD.Connection = Conn
                        CMD.CommandText = Query
                        CMD.ExecuteNonQuery()

                    Next

                    Query = "SELECT TRANSLSD.SO_NO, TRANSLSD.ITEM_NO, TRANSLSD.KDPROD, TRANSLSD.NMPROD, TRANSLSD.COLOR, TRANSLSD.QTY1, TRANSLSD.QTY2, TRANSLSD.UNIT, TRANSLSD.PRICE, " & _
                                  " TRANSLSH.SO_NO, TRANSLSH.SO_DATE, TRANSLSH.DELV_NO, TRANSLSH.INV_NO, TRANSLSH.CUST_NO, TRANSLSH.CUST_NAME, TRANSLSH.PO_NO, TRANSLSH.PAY_TERMS, TRANSLSH.SLOC, TRANSLSH.TOTAL, TRANSLSH.TAX " & _
                              "FROM TRANSLSD INNER JOIN TRANSLSH ON TRANSLSD.SO_NO = TRANSLSH.SO_NO " & _
                             "WHERE TRANSLSH.DELV_NO = '" & DeliveryNo & "' " & _
                             "ORDER BY CAST(TRANSLSD.ITEM_NO AS INT) "

                End If


                Dim PrintControl As New ClassPrintReport

                PrintControl.CetakReport("RptDelvNote.rdlc", "DataSet3", Query, lblNmCust.Text, TAXRATE)


                Query = "DELETE FROM [TRANSLSD] WHERE [so_no]='" & lblSONo.Text & "' AND [kdprod]='' "

                CMD = New SqlCommand
                CMD.Connection = Conn
                CMD.CommandText = Query
                CMD.ExecuteNonQuery()

            End If



            '// RESET SALES TEMP

            'Query = "DROP TABLE [" & TEMPSALES & "] "

            'CMD = New SqlCommand
            'CMD.Connection = Conn
            'CMD.CommandText = Query
            'CMD.ExecuteNonQuery()


            Me.Cursor = Cursors.Default

            Call BATAL()

            Me.Dispose()


            'Catch ex As Exception
            '    Me.Cursor = Cursors.Default
            '    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            'End Try


        Else
            Me.Cursor = Cursors.Default
            MessageBox.Show("Could not save the document to SAP ! ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If




    End Sub

    Private Sub cboSLoc_KeyDown(sender As Object, e As KeyEventArgs) Handles cboSLoc.KeyDown
        If e.KeyCode = Keys.Enter Then
            cboTOP.Select()
        End If
    End Sub

    Private Sub dtpSODate_KeyDown(sender As Object, e As KeyEventArgs) Handles dtpSODate.KeyDown
        If e.KeyCode = Keys.Enter Then
            DGV.Select()
            DGV.CurrentCell = DGV(1, 0)
        End If        
    End Sub

    Private Sub txtNotes_GotFocus(sender As Object, e As EventArgs) Handles txtNotes.GotFocus
        txtNotes.BackColor = Color.FromArgb(254, 240, 158)
    End Sub

    Private Sub txtNotes_LostFocus(sender As Object, e As EventArgs) Handles txtNotes.LostFocus
        txtNotes.BackColor = Color.White
    End Sub

    Private Sub txtDiscount_GotFocus(sender As Object, e As EventArgs) Handles txtDiscount.GotFocus
        txtDiscount.BackColor = Color.FromArgb(254, 240, 158)
        txtDiscount.Text = Replace(Replace(txtDiscount.Text, ",", ""), ".", "")
    End Sub

    Private Sub txtDiscount_KeyDown(sender As Object, e As KeyEventArgs) Handles txtDiscount.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtFreight.Select()
        End If
    End Sub

    Private Sub txtDiscount_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtDiscount.KeyPress
        If Not Char.IsNumber(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtDiscount_LostFocus(sender As Object, e As EventArgs) Handles txtDiscount.LostFocus
        txtDiscount.BackColor = Color.White
        txtDiscount.Text = Format(Val(txtDiscount.Text), "#,##0")
        Call GRANDTOTAL()
    End Sub

    Private Sub txtFreight_GotFocus(sender As Object, e As EventArgs) Handles txtFreight.GotFocus
        txtFreight.BackColor = Color.FromArgb(254, 240, 158)
        txtFreight.Text = Replace(Replace(txtFreight.Text, ",", ""), ".", "")
    End Sub

    Private Sub txtFreight_KeyDown(sender As Object, e As KeyEventArgs) Handles txtFreight.KeyDown
        If e.KeyCode = Keys.Enter Then
            btnSave.Select()
        End If
    End Sub

    Private Sub txtFreight_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtFreight.KeyPress
        If Not Char.IsNumber(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtFreight_LostFocus(sender As Object, e As EventArgs) Handles txtFreight.LostFocus
        txtFreight.BackColor = Color.White
        txtFreight.Text = Format(Val(txtFreight.Text), "#,##0")
        Call GRANDTOTAL()
    End Sub

    Private Sub btnPickingList_Click(sender As Object, e As EventArgs) Handles btnPickingList.Click

        Dim i As Integer
        Dim z As Integer

        With FrmSalesPicking
            .TMPSALES = TEMPSALES
            .ShowDialog()
        End With


        Try
            Query = "SELECT SLS.corak,MAT.nmprod,MAT.nmwarna,MAT.unit,MAT.hjual,COUNT(SLS.corak) AS pack,SUM(SLS.yard) AS yds " & _
                    "FROM     [" & TEMPSALES & "] SLS INNER JOIN [MASPROD] MAT ON MAT.kdprod=SLS.corak " & _
                    "GROUP BY SLS.corak,MAT.nmprod,MAT.nmwarna,MAT.unit,MAT.hjual "

            DS = New DataSet
            DA = New SqlDataAdapter(Query, Conn)
            DA.Fill(DS, "TMPSALES")

            For n = 1 To DS.Tables("TMPSALES").Rows.Count

                '// if already exist

                For i = 1 To DGV.Rows.Count
                    If DGV.Rows(i - 1).Cells(1).Value = DS.Tables("TMPSALES").Rows(n - 1)("corak") Then
                        Exit For
                    End If
                Next

                If i > DGV.Rows.Count Then
                    z = DGV.Rows.Add()
                Else
                    z = i - 1
                End If

                DGV.Rows(z).Cells(1).Value = DS.Tables("TMPSALES").Rows(n - 1)(0)   ' product
                DGV.Rows(z).Cells(2).Value = DS.Tables("TMPSALES").Rows(n - 1)(1)   ' description
                DGV.Rows(z).Cells(3).Value = DS.Tables("TMPSALES").Rows(n - 1)(2)   ' color
                DGV.Rows(z).Cells(4).Value = DS.Tables("TMPSALES").Rows(n - 1)(5)   ' qty in packs
                DGV.Rows(z).Cells(5).Value = DS.Tables("TMPSALES").Rows(n - 1)(6)   ' qty in yards
                DGV.Rows(z).Cells(6).Value = DS.Tables("TMPSALES").Rows(n - 1)(3)   ' unit
                DGV.Rows(z).Cells(7).Value = DS.Tables("TMPSALES").Rows(n - 1)(4)   ' price
                DGV.Rows(z).Cells(8).Value = DS.Tables("TMPSALES").Rows(n - 1)(6) * DS.Tables("TMPSALES").Rows(n - 1)(4)

            Next

            For n = 1 To DGV.Rows.Count
                If Trim(DGV.Rows(n - 1).Cells(2).Value) = "" Then Exit For
                DGV.Rows(n - 1).Cells(0).Value = n
            Next


            DGV.Focus()
            DGV.CurrentCell = DGV(7, 0)


            '========================
            ' JIKA ADA JUAL POTONGAN
            '========================

            Query = "SELECT corak,potong FROM [" & TEMPSALES & "] WHERE [potong]=1 "

            DS = New DataSet
            DA = New SqlDataAdapter(Query, Conn)
            DA.Fill(DS, "TMPSALES")

            For n = 1 To DS.Tables("TMPSALES").Rows.Count

                For i = 1 To DGV.Rows.Count - 1

                    If DGV.Rows(i - 1).Cells(1).Value = "" Then Exit For

                    If DGV.Rows(i - 1).Cells(1).Value = DS.Tables("TMPSALES").Rows(n - 1)("corak") Then

                        DGV.Rows(i - 1).Cells(4).Value = DGV.Rows(i - 1).Cells(4).Value - 1

                    End If

                Next

            Next


            Call GRANDTOTAL()


        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click

        Dim Ulang As Integer = DGV.Rows.Count - 1

        For n = 0 To Ulang
            If n > Ulang Then Exit For
            If DGV.Rows(n).Cells(9).Value = True Then
                Me.DGV.Rows.RemoveAt(n)
                Ulang = Ulang - 1
                n = n - 1
            End If
        Next

        Call GRANDTOTAL()

    End Sub

    Private Sub dtpSODate_ValueChanged(sender As Object, e As EventArgs) Handles dtpSODate.ValueChanged
        TAXRATE = TAX_RATE(dtpSODate.Value)
    End Sub

End Class