Imports System.Data.SqlClient

Public Class FrmPO

    Public PO_NO As String = ""
    Public EditOrDisplay As String = ""
    Dim SaveOkay As Boolean = False
    Dim POnumber As String = ""
    Dim TotalPO As Double = 0
    Dim KDSUPL As String = ""
    Dim NMSUPL As String = ""


    Private Sub BAPI_PO_CREATE1()

        'Call LogToSAP(True)

        'If SAPConStatus = False Then Exit Sub

        SaveOkay = False

        Dim oFuncCtrl As Object
        Dim oTheFunc As Object
        Dim FuncCommit As Object

        Dim oPOHEADER As Object
        Dim oPOHEADERX As Object
        Dim oPOITEM As Object
        Dim oPOITEMX As Object
        Dim oPOSCHEDULE As Object
        Dim oPOSCHEDULEX As Object
        Dim oPOCOND As Object
        Dim oPOCONDX As Object
        Dim oPOACCOUNT As Object
        Dim oPOACCOUNTX As Object
        Dim oRETURN As Object

        Dim retMess As String

        Dim NETPRICE As String = ""
        Dim PRICEUNIT As String = "1"


        oFuncCtrl = CreateObject("SAP.Functions")
        oFuncCtrl.Connection = SAPConn

        oTheFunc = oFuncCtrl.Add("BAPI_PO_CREATE1")

        '===================
        ' IMPORT PARAMETERS 
        '===================

        oPOHEADER = oTheFunc.Exports.Item("POHEADER")
        oPOHEADER.Value("COMP_CODE") = PLANT
        oPOHEADER.Value("DOC_TYPE") = DOCTYPE
        oPOHEADER.Value("VENDOR") = Format(Val(KDSUPL), "0000000000")   ' "0003000011"
        oPOHEADER.Value("PMNTTRMS") = cboTOP.Text
        oPOHEADER.Value("PURCH_ORG") = PLANT
        oPOHEADER.Value("PUR_GROUP") = PGROUP
        oPOHEADER.Value("CURRENCY") = "IDR"
        oPOHEADER.Value("DOC_DATE") = Format(dtpPODate.Value, "yyyyMMdd")
        oPOHEADER.Value("CREAT_DATE") = Format(CDate(Date.Now), "yyyyMMdd")

        oPOHEADERX = oTheFunc.Exports.Item("POHEADERX")
        oPOHEADERX.Value("COMP_CODE") = "X"
        oPOHEADERX.Value("DOC_TYPE") = "X"
        oPOHEADERX.Value("VENDOR") = "X"
        oPOHEADERX.Value("PMNTTRMS") = "X"
        oPOHEADERX.Value("PURCH_ORG") = "X"
        oPOHEADERX.Value("PUR_GROUP") = "X"
        oPOHEADERX.Value("CURRENCY") = "X"
        oPOHEADERX.Value("DOC_DATE") = "X"
        oPOHEADERX.Value("CREAT_DATE") = "X"

        '========
        ' TABLES
        '========

        oPOITEM = oTheFunc.tables.Item("POITEM")
        oPOITEMX = oTheFunc.tables.Item("POITEMX")
        oPOSCHEDULE = oTheFunc.tables.Item("POSCHEDULE")
        oPOSCHEDULEX = oTheFunc.tables.Item("POSCHEDULEX")
        oPOCOND = oTheFunc.tables.Item("POCOND")
        oPOCONDX = oTheFunc.tables.Item("POCONDX")
        oPOACCOUNT = oTheFunc.tables.Item("POACCOUNT")
        oPOACCOUNTX = oTheFunc.tables.Item("POACCOUNTX")
        oRETURN = oTheFunc.tables.Item("RETURN")


        For n = 1 To DGV.Rows.Count

            If IsNothing(DGV.Rows(n - 1).Cells(1).Value) Then
                Exit For
            End If


            NETPRICE = Trim(Str(DGV.Rows(n - 1).Cells(6).Value))
            PRICEUNIT = "1"

            If Trim(cboTax.Text) = "Tax Included" Then
                NETPRICE = Trim(Str(Math.Round((Val(NETPRICE) * 100 / (100 + TAXRATE)) * 1000, 0)))
                PRICEUNIT = Trim(Str(Val(PRICEUNIT) * 1000))
            End If

            If InStr(NETPRICE, ".") > 0 Then
                Dim Ke As Integer = Len(NETPRICE)
                Do Until Mid(NETPRICE, Ke, 1) = "."
                    PRICEUNIT = PRICEUNIT & "0"
                    Ke = Ke - 1
                Loop
            End If

            NETPRICE = Replace(NETPRICE, ".", "")


            oPOITEM.Rows.Add()
            oPOITEM.Value(n, "PO_ITEM") = Format(n, "00000")
            oPOITEM.Value(n, "MATERIAL") = DGV.Rows(n - 1).Cells(1).Value
            oPOITEM.Value(n, "PLANT") = PLANT
            oPOITEM.Value(n, "STGE_LOC") = SLOC
            oPOITEM.Value(n, "QUANTITY") = DGV.Rows(n - 1).Cells(4).Value
            oPOITEM.Value(n, "PO_UNIT") = DGV.Rows(n - 1).Cells(5).Value
            oPOITEM.Value(n, "NET_PRICE") = Val(NETPRICE)
            oPOITEM.Value(n, "PRICE_UNIT") = Val(PRICEUNIT)
            oPOITEM.Value(n, "UNLIMITED_DLV") = "X"

            oPOITEMX.Rows.Add()
            oPOITEMX.Value(n, "PO_ITEM") = Format(n, "00000")
            oPOITEMX.Value(n, "PO_ITEMX") = "X"
            oPOITEMX.Value(n, "MATERIAL") = "X"
            oPOITEMX.Value(n, "PLANT") = "X"
            oPOITEMX.Value(n, "STGE_LOC") = "X"
            oPOITEMX.Value(n, "QUANTITY") = "X"
            oPOITEMX.Value(n, "PO_UNIT") = "X"
            oPOITEMX.Value(n, "NET_PRICE") = "X"
            oPOITEMX.Value(n, "PRICE_UNIT") = "X"
            oPOITEMX.Value(n, "UNLIMITED_DLV") = "X"


            oPOSCHEDULE.Rows.Add()
            oPOSCHEDULE.Value(n, "PO_ITEM") = Format(n, "00000")
            oPOSCHEDULE.Value(n, "SCHED_LINE") = "1"
            oPOSCHEDULE.Value(n, "QUANTITY") = DGV.Rows(n - 1).Cells(4).Value
            oPOSCHEDULE.Value(n, "DELIVERY_DATE") = Format(dtpDelDate.Value, "yyyyMMdd")

            oPOSCHEDULEX.Rows.Add()
            oPOSCHEDULEX.Value(n, "PO_ITEM") = Format(n, "00000")
            oPOSCHEDULEX.Value(n, "SCHED_LINE") = "1"
            oPOSCHEDULEX.Value(n, "PO_ITEMX") = "X"
            oPOSCHEDULEX.Value(n, "SCHED_LINEX") = "X"
            oPOSCHEDULEX.Value(n, "QUANTITY") = "X"
            oPOSCHEDULEX.Value(n, "DELIVERY_DATE") = "X"


            oPOCOND.Rows.Add()
            oPOCOND.Value(n, "ITM_NUMBER") = Format(n, "00000")
            oPOCOND.Value(n, "COND_ST_NO") = "1"
            oPOCOND.Value(n, "COND_COUNT") = "1"
            oPOCOND.Value(n, "COND_TYPE") = "PB00"
            oPOCOND.Value(n, "COND_VALUE") = Val(NETPRICE)                      ' DGV.Rows(n - 1).Cells(6).Value     ' NET PRICE
            oPOCOND.Value(n, "CURRENCY") = "IDR"
            oPOCOND.Value(n, "COND_UNIT") = DGV.Rows(n - 1).Cells(5).Value      ' UNIT
            oPOCOND.Value(n, "CHANGE_ID") = "I"
            oPOCOND.Value(n, "COND_P_UNT") = Val(PRICEUNIT)                     ' "1"

            oPOCONDX.Rows.Add()
            oPOCONDX.Value(n, "ITM_NUMBER") = Format(n, "00000")
            oPOCONDX.Value(n, "ITM_NUMBERX") = "X"
            oPOCONDX.Value(n, "COND_ST_NO") = "1"
            oPOCONDX.Value(n, "COND_ST_NOX") = "1"
            oPOCONDX.Value(n, "COND_COUNT") = "X"
            oPOCONDX.Value(n, "COND_TYPE") = "X"
            oPOCONDX.Value(n, "COND_VALUE") = "X"
            oPOCONDX.Value(n, "CURRENCY") = "X"
            oPOCONDX.Value(n, "COND_UNIT") = "X"
            oPOCONDX.Value(n, "COND_P_UNT") = "X"


            oPOACCOUNT.Rows.Add()
            oPOACCOUNT.Value(n, "PO_ITEM") = Format(n, "00000")
            oPOACCOUNT.Value(n, "SERIAL_NO") = "01"
            oPOACCOUNT.Value(n, "CREAT_DATE") = Format(CDate(Date.Now), "yyyyMMdd")
            oPOACCOUNT.Value(n, "QUANTITY") = DGV.Rows(n - 1).Cells(4).Value

            oPOACCOUNTX.Rows.Add()
            oPOACCOUNTX.Value(n, "PO_ITEM") = Format(n, "00000")
            oPOACCOUNTX.Value(n, "PO_ITEMX") = "X"
            oPOACCOUNTX.Value(n, "SERIAL_NO") = "01"
            oPOACCOUNTX.Value(n, "SERIAL_NOX") = "X"
            oPOACCOUNTX.Value(n, "CREAT_DATE") = "X"
            oPOACCOUNTX.Value(n, "QUANTITY") = "X"

        Next n




        If oTheFunc.Call Then

            FuncCommit = oFuncCtrl.Add("BAPI_TRANSACTION_COMMIT")

            If FuncCommit.Call Then

                For n = 1 To oRETURN.RowCount

                    retMess = oRETURN.Value(n, "MESSAGE")

                    'MsgBox(retMess)

                    If InStr(retMess, "created") > 0 Then
                        MsgBox(retMess, MsgBoxStyle.Information, "Info")
                        POnumber = Trim(Strings.Right(retMess, 10))
                        SaveOkay = True
                        Exit For
                    End If

                Next n

            End If

        Else


        End If


    End Sub

    Private Sub GRANDTOTAL()

        Dim n As Integer = 0

        TotalPO = 0

        For n = 0 To DGV.Rows.Count - 1
            TotalPO = TotalPO + DGV.Rows(n).Cells(7).Value
        Next

        lblTotal.Text = Format(TotalPO, "#,##0")

    End Sub

    Private Sub Reset()

        txtKdSupl.Text = ""
        txtSales.Text = ""
        dtpPODate.Value = Format(Now, "dd/MM/yyyy")
        dtpDelDate.Value = Format(Now, "dd/MM/yyyy")
        cboCat.SelectedIndex = 0
        cboDelStatus.SelectedIndex = 0
        cboTOP.Text = "ZT75"

        cboBentuk.SelectedIndex = -1
        cboPanjang.SelectedIndex = -1
        cboPoint.SelectedIndex = -1
        cboKirim.SelectedIndex = -1
        txtKirim.Text = ""

        cboCapPinggir.SelectedIndex = -1
        chkFace.Checked = False
        chkSelvedge.Checked = False
        chkSulam.Checked = False
        chkFace.Enabled = False
        chkSelvedge.Enabled = False
        chkSulam.Enabled = False
        txtMerek.Text = ""

        txtAlbum.Text = ""
        txtM1020.Text = ""
        txtKain.Text = ""
        txtLembaran.Text = ""
        txtHanger.Text = ""

        txtCatatan.Text = ""
        lblTotal.Text = ""
        cboTax.SelectedIndex = 1

        DGV.Rows.Clear()

        DGV.CurrentCell = DGV(1, 0)

        txtKdSupl.Enabled = True
        btnFindSupl.Enabled = True

        txtKdSupl.Select()

        btnSave.Enabled = True
        btnCancel.Enabled = True
        btnExit.Enabled = True

        TAXRATE = TAX_RATE(dtpPODate.Value)


    End Sub

    Private Sub txtKdSupl_GotFocus(sender As Object, e As EventArgs) Handles txtKdSupl.GotFocus
        txtKdSupl.BackColor = Color.FromArgb(254, 240, 158)
    End Sub

    Private Sub txtKdSupl_KeyDown(sender As Object, e As KeyEventArgs) Handles txtKdSupl.KeyDown
        If e.KeyCode = Keys.Enter Then
            Call btnFindSupl_Click(txtKdSupl, e)
        End If
    End Sub

    Private Sub txtKdSupl_LostFocus(sender As Object, e As EventArgs) Handles txtKdSupl.LostFocus
        txtKdSupl.BackColor = Color.White
    End Sub

    Private Sub btnFindSupl_Click(sender As Object, e As EventArgs) Handles btnFindSupl.Click

Ulang:

        Query = "SELECT flkode,flnama,flala1,flala2,flwila FROM [MASVEND] " & _
                "WHERE " & IIf(IsNumeric(txtKdSupl.Text), "[flkode]", "[flnama]") & "='" & Me.txtKdSupl.Text & "'"

        DS = New DataSet
        DA = New SqlDataAdapter(Query, Conn)
        DA.Fill(DS, "VENDOR")

        If DS.Tables("VENDOR").Rows.Count <> 1 Then

            FrmFindVend.txtFilter.Text = txtKdSupl.Text
            FrmFindVend.ShowDialog()

            txtKdSupl.Select()
            txtKdSupl.Text = CARI

            If CARI = "" Then
                Exit Sub
            End If

            GoTo Ulang

            Exit Sub
        End If

        KDSUPL = DS.Tables("VENDOR").Rows(0)("flkode")
        NMSUPL = DS.Tables("VENDOR").Rows(0)("flnama")

        txtKdSupl.Text = KDSUPL & " " & NMSUPL

        dtpPODate.Select()


    End Sub

    Private Sub FrmPO_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Me.WindowState = FormWindowState.Maximized

        Call Local_Connect()

        Query = "SELECT * FROM [PAYTERMS] "

        DS = New DataSet
        DA = New SqlDataAdapter(Query, Conn)
        DA.Fill(DS, "TOP")

        cboTOP.Items.Clear()

        For n = 0 To DS.Tables("TOP").Rows.Count - 1
            cboTOP.Items.Add(DS.Tables("TOP").Rows(n)("ZTERM"))
        Next


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
        DGV.Columns(4).HeaderText = "QUANTITY"
        DGV.Columns(5).HeaderText = "UNIT"
        DGV.Columns(6).HeaderText = "PRICE"
        DGV.Columns(7).HeaderText = "TOTAL"

        DGV.Columns(0).Width = 40
        DGV.Columns(1).Width = 145
        DGV.Columns(2).Width = 290
        DGV.Columns(3).Width = 100
        DGV.Columns(4).Width = 100
        DGV.Columns(5).Width = 50
        DGV.Columns(6).Width = 100
        DGV.Columns(7).Width = 110
        DGV.Columns(8).Width = 40

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
        DGV.ColumnHeadersHeight = 30

        DGV.Columns(4).DefaultCellStyle.Format = "N2"
        DGV.Columns(6).DefaultCellStyle.Format = "N2"
        DGV.Columns(7).DefaultCellStyle.Format = "N0"

        DGV.Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        DGV.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DGV.Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DGV.Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        With ColUnit.Items
            .Clear()
            .Add("YD")
            .Add("M")
            .Add("PC")
            .Add("SET")
        End With

        Call Reset()

        btnSave.Enabled = True
        btnCancel.Enabled = True

        For n = 0 To 8
            DGV.Columns(n).ReadOnly = False
        Next


        If PO_NO <> "" Then

            Query = "SELECT * FROM [TRANPOH] WHERE [NoPO]='" & PO_NO & "'"

            DS = New DataSet
            DA = New SqlDataAdapter(Query, Conn)
            DA.Fill(DS, "PO_H")

            If DS.Tables("PO_H").Rows.Count = 1 Then

                KDSUPL = Trim(DS.Tables("PO_H").Rows(0)("kdsupl"))
                NMSUPL = Trim(DS.Tables("PO_H").Rows(0)("nmsupl"))

                txtKdSupl.Text = KDSUPL & " " & NMSUPL

                cboCat.Text = DS.Tables("PO_H").Rows(0)("kategori")
                txtSales.Text = DS.Tables("PO_H").Rows(0)("nmsls")
                txtNoUrut.Text = "" & DS.Tables("PO_H").Rows(0)("nourut")
                cboTOP.Text = DS.Tables("PO_H").Rows(0)("terms")
                cboDelStatus.Text = DS.Tables("PO_H").Rows(0)("delstatus")
                dtpPODate.Value = DS.Tables("PO_H").Rows(0)("tglpo")
                dtpDelDate.Value = DS.Tables("PO_H").Rows(0)("tgldel")

                cboBentuk.Text = DS.Tables("PO_H").Rows(0)("bentuk")
                cboPanjang.Text = DS.Tables("PO_H").Rows(0)("panjang")
                cboPoint.Text = DS.Tables("PO_H").Rows(0)("point")
                cboKirim.Text = DS.Tables("PO_H").Rows(0)("kirimke")
                txtKirim.Text = DS.Tables("PO_H").Rows(0)("pembeli")

                cboCat.Text = DS.Tables("PO_H").Rows(0)("cappinggir")
                chkSelvedge.Checked = IIf(DS.Tables("PO_H").Rows(0)("selvedge") = 1, True, False)
                chkFace.Checked = IIf(DS.Tables("PO_H").Rows(0)("face") = 1, True, False)
                chkSulam.Checked = IIf(DS.Tables("PO_H").Rows(0)("sulam") = 1, True, False)
                txtMerek.Text = DS.Tables("PO_H").Rows(0)("merek")

                txtAlbum.Text = DS.Tables("PO_H").Rows(0)("album")
                txtKain.Text = DS.Tables("PO_H").Rows(0)("kain")
                txtHanger.Text = DS.Tables("PO_H").Rows(0)("hanger")
                txtM1020.Text = DS.Tables("PO_H").Rows(0)("m1020")
                txtLembaran.Text = DS.Tables("PO_H").Rows(0)("lembaran")

                txtCatatan.Text = DS.Tables("PO_H").Rows(0)("catatan")


                cboTax.SelectedIndex = IIf(Trim(DS.Tables("PO_H").Rows(0)("tax")) = "Included", 0, 1)


                Query = "SELECT * FROM [TRANPOD] WHERE [NoPO]='" & PO_NO & "' ORDER BY [ItemNo] "

                DS = New DataSet
                DA = New SqlDataAdapter(Query, Conn)
                DA.Fill(DS, "PO_D")

                For n = 0 To DS.Tables("PO_D").Rows.Count - 1

                    n = DGV.Rows.Add()
                    DGV.Rows(n).Cells(0).Value = Val(DS.Tables("PO_D").Rows(n)("itemno"))
                    DGV.Rows(n).Cells(1).Value = DS.Tables("PO_D").Rows(n)("kdprod")
                    DGV.Rows(n).Cells(2).Value = DS.Tables("PO_D").Rows(n)("nmprod")
                    DGV.Rows(n).Cells(3).Value = DS.Tables("PO_D").Rows(n)("nmwarna")
                    DGV.Rows(n).Cells(4).Value = DS.Tables("PO_D").Rows(n)("qty")
                    DGV.Rows(n).Cells(5).Value = DS.Tables("PO_D").Rows(n)("unit")
                    DGV.Rows(n).Cells(6).Value = DS.Tables("PO_D").Rows(n)("price")
                    DGV.Rows(n).Cells(7).Value = DS.Tables("PO_D").Rows(n)("qty") * DS.Tables("PO_D").Rows(n)("price")

                Next

                For n = 0 To 8
                    DGV.Columns(n).ReadOnly = True
                Next

                Call GRANDTOTAL()

                txtKdSupl.Enabled = False
                btnFindSupl.Enabled = False

            End If

            cboBentuk.Enabled = IIf(EditOrDisplay = "Edit", True, False)
            cboPanjang.Enabled = IIf(EditOrDisplay = "Edit", True, False)
            cboPoint.Enabled = IIf(EditOrDisplay = "Edit", True, False)
            cboKirim.Enabled = IIf(EditOrDisplay = "Edit", True, False)
            txtKirim.Enabled = IIf(EditOrDisplay = "Edit", True, False)
            cboCapPinggir.Enabled = IIf(EditOrDisplay = "Edit", True, False)
            chkSelvedge.Enabled = IIf(EditOrDisplay = "Edit", True, False)
            chkFace.Enabled = IIf(EditOrDisplay = "Edit", True, False)
            chkSulam.Enabled = IIf(EditOrDisplay = "Edit", True, False)
            txtMerek.Enabled = IIf(EditOrDisplay = "Edit", True, False)
            txtAlbum.Enabled = IIf(EditOrDisplay = "Edit", True, False)
            txtM1020.Enabled = IIf(EditOrDisplay = "Edit", True, False)
            txtKain.Enabled = IIf(EditOrDisplay = "Edit", True, False)
            txtLembaran.Enabled = IIf(EditOrDisplay = "Edit", True, False)
            txtHanger.Enabled = IIf(EditOrDisplay = "Edit", True, False)
            txtCatatan.Enabled = IIf(EditOrDisplay = "Edit", True, False)
            cboTax.Enabled = IIf(EditOrDisplay = "Edit", True, False)


            btnSave.Enabled = IIf(EditOrDisplay = "Edit", True, False)
            btnCancel.Enabled = IIf(EditOrDisplay = "Edit", True, False)
            btnSplitGrade.Enabled = IIf(EditOrDisplay = "Edit", True, False)
            btnDelete.Enabled = IIf(EditOrDisplay = "Edit", True, False)

        End If


    End Sub

    Private Sub dtpPODate_KeyDown(sender As Object, e As KeyEventArgs) Handles dtpPODate.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtSales.Select()
        End If
    End Sub

    Private Sub dgv_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DGV.CellEndEdit

        If e.ColumnIndex = 0 Then       ' NO


        ElseIf e.ColumnIndex = 1 And PO_NO = "" Then   ' ITEM CODE
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
                    DGV.Rows.Clear()
                    Exit Sub
                Else
                    DGV.Rows(e.RowIndex).Cells(1).Value = CARI
                    SendKeys.Send("{up}")
                    GoTo Ulang
                End If

            Else

                'If Mid(DS.Tables("PRODUK").Rows(0)("kdprod"), 1, 2) = "F." And InStr(DS.Tables("PRODUK").Rows(0)("kdprod"), ".A.") = 0 Then
                '    DGV.Rows.RemoveAt(e.RowIndex)
                '    Exit Sub
                'End If

                DGV.Rows(e.RowIndex).Cells(0).Value = e.RowIndex + 1
                DGV.Rows(e.RowIndex).Cells(1).Value = DS.Tables("PRODUK").Rows(0)("kdprod")
                DGV.Rows(e.RowIndex).Cells(2).Value = DS.Tables("PRODUK").Rows(0)("nmprod")
                DGV.Rows(e.RowIndex).Cells(3).Value = DS.Tables("PRODUK").Rows(0)("nmwarna")
                DGV.Rows(e.RowIndex).Cells(5).Value = DS.Tables("PRODUK").Rows(0)("unit")
                DGV.Rows(e.RowIndex).Cells(6).Value = DS.Tables("PRODUK").Rows(0)("hbeli")

                DGV.Rows(e.RowIndex).Cells(4).ReadOnly = False
                DGV.Rows(e.RowIndex).Cells(6).ReadOnly = False

                DGV.CurrentCell = DGV(4, e.RowIndex)    ' Fokus di kolom Quantity

                'SendKeys.Send("{up}")

            End If


        ElseIf e.ColumnIndex = 4 Then   ' QUANTITY

            DGV.Rows(e.RowIndex).Cells(4).Value = DGV.Rows(e.RowIndex).Cells(4).Value * 1
            DGV.Rows(e.RowIndex).Cells(7).Value = DGV.Rows(e.RowIndex).Cells(4).Value * DGV.Rows(e.RowIndex).Cells(6).Value

            'DGV.CurrentCell = DGV(6, e.RowIndex)        ' Fokus di kolom Harga

            SendKeys.Send("{up}")


        ElseIf e.ColumnIndex = 6 Then   ' HARGA

            DGV.Rows(e.RowIndex).Cells(6).Value = DGV.Rows(e.RowIndex).Cells(6).Value * 1
            DGV.Rows(e.RowIndex).Cells(7).Value = DGV.Rows(e.RowIndex).Cells(4).Value * DGV.Rows(e.RowIndex).Cells(6).Value

            'DGV.CurrentCell = DGV(1, e.RowIndex)

            SendKeys.Send("{up}")


        End If

        Call GRANDTOTAL()


    End Sub


    Private Sub dgv_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles DGV.EditingControlShowing

        Dim Row As Integer = DGV.CurrentCell.RowIndex
        Dim Col As Integer = DGV.CurrentCell.ColumnIndex


        '// uppercase textbox

        If TypeOf e.Control Is TextBox Then
            DirectCast(e.Control, TextBox).CharacterCasing = CharacterCasing.Upper
        End If

        '// numeric textbox

        If Col = 4 Or Col = 6 Then
            AddHandler CType(e.Control, TextBox).KeyPress, AddressOf TextBoxNum_keyPress
        End If


    End Sub

    Private Sub TextBoxNum_keyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs)

        Dim Col As Integer = DGV.CurrentCell.ColumnIndex
        Dim Row As Integer = DGV.CurrentCell.RowIndex

        If Col = 4 Or Col = 6 Then

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

    Private Sub dgv_KeyDown(sender As Object, e As KeyEventArgs) Handles DGV.KeyDown

        Dim Row As Integer = DGV.CurrentCell.RowIndex
        Dim Col As Integer = DGV.CurrentCell.ColumnIndex

        If e.KeyCode = Keys.Enter Then

            'e.Handled = True

            If Col = 1 And PO_NO = "" Then

                If Trim(DGV.Rows(Row).Cells(Col).Value) = "" Then

                    FrmFindItem.ShowDialog()

                    If CARI <> "" Then

                        'If Mid(CARI, 1, 2) = "F." And Mid(CARI, 9, 3) <> ".A." Then
                        '    Exit Sub
                        'End If

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
                        DGV.Rows(Row).Cells(5).Value = UCase(DS.Tables("PRODUK").Rows(0)("unit"))
                        DGV.Rows(Row).Cells(6).Value = DS.Tables("PRODUK").Rows(0)("hbeli")

                        DGV.Rows(Row).Cells(4).ReadOnly = False
                        DGV.Rows(Row).Cells(6).ReadOnly = False

                        DGV.CurrentCell = DGV(4, Row)    ' Fokus di kolom Quantity


                    End If

                End If


            ElseIf Col = 4 Then

                If Val(DGV.Rows(Row).Cells(Col).Value) > 0 Then
                    DGV.Rows(Row).Cells(7).Value = DGV.Rows(Row).Cells(4).Value * DGV.Rows(Row).Cells(6).Value
                    'DGV.CurrentCell = DGV(1, DGV.Rows.Count - 1)
                End If


            ElseIf Col = 6 Then

                If Val(DGV.Rows(Row).Cells(Col).Value) > 0 Then
                    DGV.Rows(Row).Cells(7).Value = DGV.Rows(Row).Cells(4).Value * DGV.Rows(Row).Cells(6).Value
                    'DGV.CurrentCell = DGV(1, DGV.Rows.Count - 1)
                End If


            End If

            Call GRANDTOTAL()

        End If

    End Sub

    Private Sub dtpDelDate_KeyDown(sender As Object, e As KeyEventArgs) Handles dtpDelDate.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtNoUrut.Select()
        End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        Call GRANDTOTAL()

        If Trim(txtKdSupl.Text) = "" Then
            MessageBox.Show("Please enter vendor ! ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtKdSupl.Select()
            Exit Sub
        End If

        If Trim(txtSales.Text) = "" Then
            MessageBox.Show("Please enter PIC sales ! ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtSales.Select()
            Exit Sub
        End If

        If TotalPO = 0 Then
            MessageBox.Show("Please enter item ! ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            DGV.Select()
            DGV.CurrentCell = DGV(1, DGV.Rows.Count - 1)
            Exit Sub
        End If

        If (cboTOP.Text) = "" Then
            MessageBox.Show("Please select [Payment Terms] ! ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cboTOP.Select()
            Exit Sub
        End If


        If InStr(txtKdSupl.Text, "TRISULA TEXTILE INDUSTRIES") > 0 Then

            If (cboBentuk.Text) = "" Then
                MessageBox.Show("Please select [Bentuk] ! ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                TabControl1.SelectedIndex = 0
                cboBentuk.Select()
                Exit Sub
            End If

            If (cboPanjang.Text) = "" Then
                MessageBox.Show("Please select [Panjang] ! ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                TabControl1.SelectedIndex = 0
                cboPanjang.Select()
                Exit Sub
            End If

            If (cboPoint.Text) = "" Then
                MessageBox.Show("Please select [Point] ! ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                TabControl1.SelectedIndex = 0
                cboPoint.Select()
                Exit Sub
            End If

            If (cboKirim.Text) = "" Then
                MessageBox.Show("Please select [Kirim Ke] ! ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                TabControl1.SelectedIndex = 0
                cboKirim.Select()
                Exit Sub
            End If

            If (cboCapPinggir.Text) = "" Then
                MessageBox.Show("Please select [Cap Pinggir] ! ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                TabControl1.SelectedIndex = 1
                cboCapPinggir.Select()
                Exit Sub
            End If

            If (txtMerek.Text) = "" Then
                MessageBox.Show("Please enter [Merek] ! ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                TabControl1.SelectedIndex = 1
                txtMerek.Select()
                Exit Sub
            End If

            If Trim(txtAlbum.Text) = "" Then
                MessageBox.Show("Please enter [ALBUM] ! ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                TabControl1.SelectedIndex = 2
                txtAlbum.Select()
                Exit Sub
            End If

            If Trim(txtM1020.Text) = "" Then
                MessageBox.Show("Please enter [M1020] ! ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                TabControl1.SelectedIndex = 2
                txtM1020.Select()
                Exit Sub
            End If

            If Trim(txtKain.Text) = "" Then
                MessageBox.Show("Please enter [KAIN] ! ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                TabControl1.SelectedIndex = 2
                txtKain.Select()
                Exit Sub
            End If

            If Trim(txtLembaran.Text) = "" Then
                MessageBox.Show("Please enter [LEMBARAN] ! ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                TabControl1.SelectedIndex = 2
                txtLembaran.Select()
                Exit Sub
            End If

            If Trim(txtHanger.Text) = "" Then
                MessageBox.Show("Please enter [HANGER] ! ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                TabControl1.SelectedIndex = 2
                txtHanger.Select()
                Exit Sub
            End If

        End If



        For n = 0 To DGV.Rows.Count - 1

            If Trim(DGV.Rows(n).Cells(2).Value) <> "" Then

                If DGV.Rows(n).Cells(4).Value = 0 Then

                    MessageBox.Show("Please enter [QUANTITY] ! ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    DGV.Select()
                    DGV.CurrentCell = DGV(4, 0)
                    Exit Sub

                End If

                If DGV.Rows(n).Cells(6).Value = 0 Then

                    MessageBox.Show("Please enter [NET PRICE] ! ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    DGV.Select()
                    DGV.CurrentCell = DGV(6, 0)
                    Exit Sub

                End If

            End If

        Next


        If MessageBox.Show("Save this transaction ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If


        Me.Cursor = Cursors.WaitCursor

        TAXRATE = TAX_RATE(dtpPODate.Value)

        Call BAPI_PO_CREATE1()


        If SaveOkay Then

            Try

                '===============
                ' SAVE TO LOCAL
                '===============

                Call LOCAL_CONNECT()

                If ConnectStatus = False Then
                    Me.Cursor = Cursors.Default
                    Exit Sub
                End If


                Query = "INSERT INTO [TRANPOH] (nopo,tglpo,agen,kdsupl,nmsupl,kategori,nmsls,delstatus,tgldel,bentuk,panjang,point,kirimke," & _
                                               "pembeli,cappinggir,selvedge,face,merek,album,m1020,kain,lembaran,hanger,catatan,total,terms,tax) " & _
                        "VALUES ('" & POnumber & "','" & DATECHAR(dtpPODate.Value) & "','" & PLANT & "','" & KDSUPL & "'," & _
                                "'" & PETIK(NMSUPL) & "','" & cboCat.Text & "','" & txtSales.Text & "','" & cboDelStatus.Text & "'," & _
                                "'" & DATECHAR(dtpDelDate.Value) & "','" & cboBentuk.Text & "','" & cboPanjang.Text & "'," & _
                                "'" & cboPoint.Text & "','" & cboKirim.Text & "','" & txtKirim.Text & "','" & cboCapPinggir.Text & "'," & _
                                " " & IIf(chkSelvedge.Checked, 1, 0) & "," & IIf(chkFace.Checked, 1, 0) & ",'" & txtMerek.Text & "'," & _
                                "'" & txtAlbum.Text & "','" & txtM1020.Text & "','" & txtKain.Text & "','" & txtLembaran.Text & "'," & _
                                "'" & txtHanger.Text & "','" & PETIK(txtCatatan.Text) & "'," & Math.Round(TotalPO, 0) & "," & _
                                "'" & cboTOP.Text & "','" & Replace(cboTax.Text, "Tax ", "") & "')"


                CMD = New SqlCommand
                CMD.Connection = Conn
                CMD.CommandText = Query
                CMD.ExecuteNonQuery()


                For n = 0 To DGV.Rows.Count - 1

                    If IsNothing(DGV.Rows(n).Cells(1).Value) Then
                        Exit For
                    End If


                    '=======================
                    ' UPDATE PRODUCT MASTER
                    '=======================

                    Query = "UPDATE [MASPROD] " & _
                            "SET    [NMPROD]='" & PETIK(DGV.Rows(n).Cells(2).Value) & "'," & _
                                  " [HBELI]=" & DESIMAL(DGV.Rows(n).Cells(6).Value) & " " & _
                            "WHERE  [KDPROD]='" & DGV.Rows(n).Cells(1).Value & "'"

                    CMD = New SqlCommand
                    CMD.Connection = Conn
                    CMD.CommandText = Query
                    CMD.ExecuteNonQuery()




                    Query = "INSERT INTO [TRANPOD] (nopo,itemno,kdprod,nmprod,nmwarna,qty,unit,price) " & _
                            "VALUES ('" & POnumber & "','" & Format(DGV.Rows(n).Cells(0).Value, "00000") & "'," & _
                                    "'" & DGV.Rows(n).Cells(1).Value & "','" & PETIK(DGV.Rows(n).Cells(2).Value) & "'," & _
                                    "'" & DGV.Rows(n).Cells(3).Value & "'," & DESIMAL(DGV.Rows(n).Cells(4).Value) & "," & _
                                    "'" & DGV.Rows(n).Cells(5).Value & "'," & DESIMAL(DGV.Rows(n).Cells(6).Value) & ")"

                    CMD = New SqlCommand
                    CMD.Connection = Conn
                    CMD.CommandText = Query
                    CMD.ExecuteNonQuery()

                Next


                '==============
                ' SAVE TO HOST
                '==============

                Call HOST_CONNECT()

                If ConnectStatus = False Then
                    Me.Cursor = Cursors.Default
                    Exit Sub
                End If

                Query = "INSERT INTO [AG_TRANPOH] (nopo,tglpo,agen,kdsupl,nmsupl,kategori,nmsls,delstatus,tgldel,bentuk,panjang,point,kirimke," & _
                                                  "pembeli,cappinggir,selvedge,face,merek,album,m1020,kain,lembaran,hanger,catatan,total,terms) " & _
                        "VALUES ('" & POnumber & "','" & DATECHAR(dtpPODate.Value) & "','" & PLANT & "','" & KDSUPL & "'," & _
                                "'" & PETIK(NMSUPL) & "','" & cboCat.Text & "','" & txtSales.Text & "','" & cboDelStatus.Text & "'," & _
                                "'" & DATECHAR(dtpDelDate.Value) & "','" & cboBentuk.Text & "','" & cboPanjang.Text & "'," & _
                                "'" & cboPoint.Text & "','" & cboKirim.Text & "','" & txtKirim.Text & "','" & cboCapPinggir.Text & "'," & _
                                " " & IIf(chkSelvedge.Checked, 1, 0) & "," & IIf(chkFace.Checked, 1, 0) & ",'" & txtMerek.Text & "'," & _
                                "'" & txtAlbum.Text & "','" & txtM1020.Text & "','" & txtKain.Text & "','" & txtLembaran.Text & "'," & _
                                "'" & txtHanger.Text & "','" & PETIK(txtCatatan.Text) & "'," & Math.Round(TotalPO, 0) & ",'" & cboTOP.Text & "')"

                CMD = New SqlCommand
                CMD.Connection = HostConn
                CMD.CommandText = Query
                CMD.ExecuteNonQuery()


                For n = 0 To DGV.Rows.Count - 1

                    If IsNothing(DGV.Rows(n).Cells(1).Value) Then
                        Exit For
                    End If

                    Query = "INSERT INTO [AG_TRANPOD] (nopo,itemno,kdprod,nmprod,nmwarna,qty,unit,price) " & _
                            "VALUES ('" & POnumber & "','" & Format(DGV.Rows(n).Cells(0).Value, "00000") & "'," & _
                                    "'" & DGV.Rows(n).Cells(1).Value & "','" & PETIK(DGV.Rows(n).Cells(2).Value) & "'," & _
                                    "'" & DGV.Rows(n).Cells(3).Value & "'," & Replace(DGV.Rows(n).Cells(4).Value, ",", ".") & "," & _
                                    "'" & DGV.Rows(n).Cells(5).Value & "'," & Replace(DGV.Rows(n).Cells(6).Value, ",", ".") & ")"

                    CMD = New SqlCommand
                    CMD.Connection = HostConn
                    CMD.CommandText = Query
                    CMD.ExecuteNonQuery()

                Next

                SaveStatus = True

                Me.Cursor = Cursors.Default

                Me.Dispose()


            Catch ex As Exception
                Me.Cursor = Cursors.Default
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

        Else
            Me.Cursor = Cursors.Default
            MessageBox.Show("Could not save the document to SAP ! ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If



    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            If MessageBox.Show("Cancel this transaction ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Yes Then
                Call Reset()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click

        Dim n As Integer = 0

        For n = 0 To DGV.Rows.Count - 1
            If DGV.Rows(n).Cells(8).Value = True Then   ' kolom checkbox
                Exit For
            End If
        Next

        If n > DGV.Rows.Count - 1 Then
            Exit Sub
        End If

        If MessageBox.Show("Do you really want to delete selected items ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        Dim Ulang As Integer = DGV.Rows.Count - 1

        For n = 0 To Ulang
            If n > Ulang Then Exit For
            If DGV.Rows(n).Cells(8).Value = True Then
                Me.DGV.Rows.RemoveAt(n)
                Ulang = Ulang - 1
                n = n - 1
            End If
        Next

        For n = 1 To Ulang
            DGV.Rows(n - 1).Cells(0).Value = n
        Next

        DGV.CurrentCell = DGV(1, DGV.Rows.Count - 1)

    End Sub

    Private Sub cboKirim_KeyDown(sender As Object, e As KeyEventArgs) Handles cboKirim.KeyDown
        If e.KeyCode = Keys.Enter Then
            If cboKirim.Text = "AGEN" Then
                TabControl1.SelectedIndex = 1
                cboCapPinggir.Select()
            Else
                txtKirim.Select()
            End If
        End If
    End Sub

    Private Sub cboKirim_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboKirim.SelectedIndexChanged

        If Trim(cboKirim.Text) = "AGEN" Then
            txtKirim.Clear()
            txtKirim.Visible = False
        Else
            txtKirim.Visible = True
            txtKirim.Clear()
            txtKirim.Select()
        End If

    End Sub

    Private Sub cboCat_KeyDown(sender As Object, e As KeyEventArgs) Handles cboCat.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtKdSupl.Select()
        End If
    End Sub

    Private Sub txtSales_GotFocus(sender As Object, e As EventArgs) Handles txtSales.GotFocus
        txtSales.BackColor = Color.FromArgb(254, 240, 158)
    End Sub

    Private Sub txtSales_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSales.KeyDown
        If e.KeyCode = Keys.Enter Then
            cboTOP.Select()
        End If
    End Sub

    Private Sub txtSales_LostFocus(sender As Object, e As EventArgs) Handles txtSales.LostFocus
        txtSales.BackColor = Color.White
    End Sub

    Private Sub cboDelStatus_KeyDown(sender As Object, e As KeyEventArgs) Handles cboDelStatus.KeyDown
        If e.KeyCode = Keys.Enter Then
            dtpDelDate.Select()
        End If
    End Sub

    Private Sub txtKirim_GotFocus(sender As Object, e As EventArgs) Handles txtKirim.GotFocus
        txtKirim.BackColor = Color.FromArgb(254, 240, 158)
    End Sub

    Private Sub txtKirim_KeyDown(sender As Object, e As KeyEventArgs) Handles txtKirim.KeyDown
        If e.KeyCode = Keys.Enter Then
            TabControl1.SelectedIndex = 1
            cboCapPinggir.Select()
        End If
    End Sub

    Private Sub txtKirim_LostFocus(sender As Object, e As EventArgs) Handles txtKirim.LostFocus
        txtKirim.BackColor = Color.White
    End Sub

    Private Sub txtMerek_GotFocus(sender As Object, e As EventArgs) Handles txtMerek.GotFocus
        txtMerek.BackColor = Color.FromArgb(254, 240, 158)
    End Sub

    Private Sub txtMerek_KeyDown(sender As Object, e As KeyEventArgs) Handles txtMerek.KeyDown
        If e.KeyCode = Keys.Enter Then
            TabControl1.SelectedIndex = 2
            txtAlbum.Select()
        End If
    End Sub

    Private Sub txtMerek_LostFocus(sender As Object, e As EventArgs) Handles txtMerek.LostFocus
        txtMerek.BackColor = Color.White
    End Sub

    Private Sub txtCatatan_GotFocus(sender As Object, e As EventArgs) Handles txtCatatan.GotFocus
        txtCatatan.BackColor = Color.FromArgb(254, 240, 158)
    End Sub

    Private Sub txtCatatan_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCatatan.KeyDown
        'If e.KeyCode = Keys.Enter Then
        '    DGV.Select()
        '    DGV.CurrentCell = DGV(1, 0)
        'End If
    End Sub

    Private Sub txtCatatan_LostFocus(sender As Object, e As EventArgs) Handles txtCatatan.LostFocus
        txtCatatan.BackColor = Color.White
    End Sub

    Private Sub cboBentuk_KeyDown(sender As Object, e As KeyEventArgs) Handles cboBentuk.KeyDown
        If e.KeyCode = Keys.Enter Then
            cboPanjang.Select()
        End If
    End Sub

    Private Sub cboPanjang_KeyDown(sender As Object, e As KeyEventArgs) Handles cboPanjang.KeyDown
        If e.KeyCode = Keys.Enter Then
            cboPoint.Select()
        End If
    End Sub

    Private Sub cboCapPinggir_KeyDown(sender As Object, e As KeyEventArgs) Handles cboCapPinggir.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtMerek.Select()
        End If
    End Sub

    Private Sub txtAlbum_GotFocus(sender As Object, e As EventArgs) Handles txtAlbum.GotFocus
        txtAlbum.BackColor = Color.FromArgb(254, 240, 158)
    End Sub

    Private Sub txtAlbum_KeyDown(sender As Object, e As KeyEventArgs) Handles txtAlbum.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtM1020.Select()
        End If

    End Sub

    Private Sub txtAlbum_LostFocus(sender As Object, e As EventArgs) Handles txtAlbum.LostFocus
        txtAlbum.BackColor = Color.White
    End Sub

    Private Sub txtKain_GotFocus(sender As Object, e As EventArgs) Handles txtKain.GotFocus
        txtKain.BackColor = Color.FromArgb(254, 240, 158)
    End Sub

    Private Sub txtKain_KeyDown(sender As Object, e As KeyEventArgs) Handles txtKain.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtLembaran.Select()
        End If
    End Sub

    Private Sub txtKain_LostFocus(sender As Object, e As EventArgs) Handles txtKain.LostFocus
        txtKain.BackColor = Color.White
    End Sub

    Private Sub txtM1020_GotFocus(sender As Object, e As EventArgs) Handles txtM1020.GotFocus
        txtM1020.BackColor = Color.FromArgb(254, 240, 158)
    End Sub

    Private Sub txtM1020_KeyDown(sender As Object, e As KeyEventArgs) Handles txtM1020.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtKain.Select()
        End If
    End Sub

    Private Sub txtM1020_LostFocus(sender As Object, e As EventArgs) Handles txtM1020.LostFocus
        txtM1020.BackColor = Color.White
    End Sub

    Private Sub txtLembaran_GotFocus(sender As Object, e As EventArgs) Handles txtLembaran.GotFocus
        txtLembaran.BackColor = Color.FromArgb(254, 240, 158)
    End Sub

    Private Sub txtLembaran_KeyDown(sender As Object, e As KeyEventArgs) Handles txtLembaran.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtHanger.Select()
        End If
    End Sub

    Private Sub txtLembaran_LostFocus(sender As Object, e As EventArgs) Handles txtLembaran.LostFocus
        txtLembaran.BackColor = Color.White
    End Sub

    Private Sub txtHanger_GotFocus(sender As Object, e As EventArgs) Handles txtHanger.GotFocus
        txtHanger.BackColor = Color.FromArgb(254, 240, 158)
    End Sub

    Private Sub txtHanger_KeyDown(sender As Object, e As KeyEventArgs) Handles txtHanger.KeyDown
        If e.KeyCode = Keys.Enter Then
            TabControl1.SelectedIndex = 3
            txtCatatan.Select()
        End If
    End Sub

    Private Sub txtHanger_LostFocus(sender As Object, e As EventArgs) Handles txtHanger.LostFocus
        txtHanger.BackColor = Color.White
    End Sub

    
    Private Sub cboCapPinggir_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCapPinggir.SelectedIndexChanged

        chkFace.Checked = False
        chkSelvedge.Checked = False
        chkSulam.Checked = False

        chkSelvedge.Enabled = IIf(cboCapPinggir.SelectedIndex = 0, True, False)
        chkFace.Enabled = IIf(cboCapPinggir.SelectedIndex = 0, True, False)
        chkSulam.Enabled = IIf(cboCapPinggir.SelectedIndex = 0, True, False)

    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        SaveStatus = False
        Me.Close()
    End Sub

    Private Sub cboTOP_KeyDown(sender As Object, e As KeyEventArgs) Handles cboTOP.KeyDown
        If e.KeyCode = Keys.Enter Then
            cboDelStatus.Select()
        End If
    End Sub

    Private Sub cboPoint_KeyDown(sender As Object, e As KeyEventArgs) Handles cboPoint.KeyDown
        If e.KeyCode = Keys.Enter Then
            cboKirim.Select()
        End If
    End Sub

  
    Private Sub btnSplitGrade_Click(sender As Object, e As EventArgs) Handles btnSplitGrade.Click

        '// TAMBAH GRADE B/S/X

        Dim KDBRG As String = ""
        Dim NMBRG As String = ""
        Dim WARNA As String = ""
        Dim QTTY As Long = 0
        Dim UNIT As String = ""
        Dim HARGA As Single = 0
        Dim i As Integer = 0


        KDBRG = DGV.CurrentRow.Cells(1).Value

        If InStr(KDBRG, ".A.") = 0 Then Exit Sub


        NMBRG = DGV.CurrentRow.Cells(2).Value
        WARNA = DGV.CurrentRow.Cells(3).Value
        QTTY = DGV.CurrentRow.Cells(4).Value
        UNIT = DGV.CurrentRow.Cells(5).Value
        HARGA = DGV.CurrentRow.Cells(6).Value

        i = DGV.CurrentRow.Index

        i = i + 1
        DGV.Rows.Insert(i, 1)
        DGV.Rows(i).Cells(1).Value = Replace(KDBRG, ".A.", ".B.")
        DGV.Rows(i).Cells(2).Value = NMBRG
        DGV.Rows(i).Cells(3).Value = WARNA
        DGV.Rows(i).Cells(4).Value = QTTY * 7 / 100
        DGV.Rows(i).Cells(5).Value = UNIT
        DGV.Rows(i).Cells(6).Value = HARGA * 85 / 100

        i = i + 1
        DGV.Rows.Insert(i, 1)
        DGV.Rows(i).Cells(1).Value = Replace(KDBRG, ".A.", ".S.")
        DGV.Rows(i).Cells(2).Value = NMBRG
        DGV.Rows(i).Cells(3).Value = WARNA
        DGV.Rows(i).Cells(4).Value = QTTY * 7 / 100
        DGV.Rows(i).Cells(5).Value = UNIT
        DGV.Rows(i).Cells(6).Value = HARGA * 85 / 100

        i = i + 1
        DGV.Rows.Insert(i, 1)
        DGV.Rows(i).Cells(1).Value = Replace(KDBRG, ".A.", ".X.")
        DGV.Rows(i).Cells(2).Value = NMBRG
        DGV.Rows(i).Cells(3).Value = WARNA
        DGV.Rows(i).Cells(4).Value = QTTY * 10 / 100
        DGV.Rows(i).Cells(5).Value = UNIT
        DGV.Rows(i).Cells(6).Value = HARGA * 85 / 100

 
        For n = 1 To DGV.Rows.Count
            If DGV.Rows(n - 1).Cells(2).Value = "" Then Exit For
            DGV.Rows(n - 1).Cells(0).Value = n
            DGV.Rows(n - 1).Cells(7).Value = DGV.Rows(n - 1).Cells(4).Value * DGV.Rows(n - 1).Cells(6).Value
        Next


        Call GRANDTOTAL()



    End Sub

    Private Sub txtAlbum_Leave(sender As Object, e As EventArgs) Handles txtAlbum.Leave
        If Trim(txtAlbum.Text) = "" Then
            txtAlbum.Text = "-"
        End If
    End Sub

    Private Sub txtM1020_Leave(sender As Object, e As EventArgs) Handles txtM1020.Leave
        If Trim(txtM1020.Text) = "" Then
            txtM1020.Text = "-"
        End If
    End Sub

    Private Sub txtKain_Leave(sender As Object, e As EventArgs) Handles txtKain.Leave
        If Trim(txtKain.Text) = "" Then
            txtKain.Text = "-"
        End If
    End Sub

    Private Sub txtLembaran_Leave(sender As Object, e As EventArgs) Handles txtLembaran.Leave
        If Trim(txtLembaran.Text) = "" Then
            txtLembaran.Text = "-"
        End If
    End Sub

    Private Sub txtHanger_Leave(sender As Object, e As EventArgs) Handles txtHanger.Leave
        If Trim(txtHanger.Text) = "" Then
            txtHanger.Text = "-"
        End If
    End Sub

    Private Sub txtNoUrut_GotFocus(sender As Object, e As EventArgs) Handles txtNoUrut.GotFocus
        txtNoUrut.BackColor = Color.FromArgb(254, 240, 158)
    End Sub

    Private Sub txtNoUrut_KeyDown(sender As Object, e As KeyEventArgs) Handles txtNoUrut.KeyDown
        If e.KeyCode = Keys.Enter Then
            TabControl1.SelectedIndex = 0
            cboBentuk.Select()
        End If
    End Sub

    Private Sub txtNoUrut_LostFocus(sender As Object, e As EventArgs) Handles txtNoUrut.LostFocus
        txtNoUrut.BackColor = Color.White
    End Sub

    Private Sub DGV_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGV.CellContentClick

    End Sub

    Private Sub dtpPODate_ValueChanged(sender As Object, e As EventArgs) Handles dtpPODate.ValueChanged
        TAXRATE = TAX_RATE(dtpPODate.Value)
    End Sub
End Class