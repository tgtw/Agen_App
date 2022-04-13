Imports System.Data.SqlClient

Public Class FrmCustomer

    Dim Indeks As Integer = 0
    Dim SaveOkay As Boolean = False
    Dim SearchTerm As String = ""

    Private Sub UPDATE_CUSTOMER()

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

        Indeks = 0

        oFuncCtrl = CreateObject("SAP.Functions")
        oFuncCtrl.Connection = SAPConn

        objRfcFunc = oFuncCtrl.Add("RFC_CALL_TRANSACTION")

        objRfcFunc.exports("TRANCODE") = "XD02"
        objRfcFunc.exports("UPDMODE") = "S"

        BDC_TABLE = objRfcFunc.Tables("BDCTABLE")

        ADD_BDCDATA(BDC_TABLE, "SAPMF02D", "0101", "X", "", "")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "BDC_CURSOR", "RF02D-D0320")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "BDC_OKCODE", "/00")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "RF02D-KUNNR", Format(Val(CUSTNUM), "0000000000"))
        ADD_BDCDATA(BDC_TABLE, "", "", "", "RF02D-BUKRS", PLANT)
        ADD_BDCDATA(BDC_TABLE, "", "", "", "RF02D-VKORG", PLANT)
        ADD_BDCDATA(BDC_TABLE, "", "", "", "RF02D-VTWEG", "DS")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "RF02D-SPART", "AG")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "RF02D-D0110", "X")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "RF02D-D0215", "X")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "RF02D-D0320", "X")

        ADD_BDCDATA(BDC_TABLE, "SAPMF02D", "0110", "X", "", "")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "BDC_CURSOR", "KNA1-ANRED")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "BDC_OKCODE", "=VW")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "KNA1-ANRED", "Company")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "KNA1-NAME1", txtName.Text)
        ADD_BDCDATA(BDC_TABLE, "", "", "", "KNA1-SORTL", SearchTerm)
        ADD_BDCDATA(BDC_TABLE, "", "", "", "KNA1-STRAS", txtAddr1.Text)
        ADD_BDCDATA(BDC_TABLE, "", "", "", "KNA1-ORT01", txtCity.Text)
        ADD_BDCDATA(BDC_TABLE, "", "", "", "KNA1-PSTLZ", txtPostal.Text)
        ADD_BDCDATA(BDC_TABLE, "", "", "", "KNA1-LAND1", "ID")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "KNA1-SPRAS", "EN")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "KNA1-TELF1", txtPhone.Text)
        ADD_BDCDATA(BDC_TABLE, "", "", "", "KNA1-TELFX", txtFax.Text)

        ADD_BDCDATA(BDC_TABLE, "SAPMF02D", "0215", "X", "", "")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "BDC_CURSOR", "KNB1-ZTERM")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "BDC_OKCODE", "=VW")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "KNB1-ZTERM", cboTOP.Text)

        ADD_BDCDATA(BDC_TABLE, "SAPMF02D", "0320", "X", "", "")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "BDC_CURSOR", "KNVV-PERFK")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "BDC_OKCODE", "=UPDA")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "KNVV-INCO1", "FOB")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "KNVV-INCO2", "JAKARTA")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "KNVV-ZTERM", cboTOP.Text)
        ADD_BDCDATA(BDC_TABLE, "", "", "", "KNVV-KTGRD", "01")


        If objRfcFunc.Call = True Then

            oMESS = objRfcFunc.Imports("MESSG")
            Pesan = oMESS.Value("MSGTX")

            If InStr(Pesan, "have been made") > 0 Then
                SaveOkay = True
                'MsgBox(Pesan, MsgBoxStyle.Information, "Info")
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

    Private Sub EXTEND_CUSTOMER()

        'Call LogToSAP(False)

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

        Indeks = 0


        oFuncCtrl = CreateObject("SAP.Functions")
        oFuncCtrl.Connection = SAPConn

        objRfcFunc = oFuncCtrl.Add("RFC_CALL_TRANSACTION")

        objRfcFunc.exports("TRANCODE") = "XD02"
        objRfcFunc.exports("UPDMODE") = "S"

        BDC_TABLE = objRfcFunc.Tables("BDCTABLE")

        ADD_BDCDATA(BDC_TABLE, "SAPMF02D", "0101", "X", "", "")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "BDC_CURSOR", "RF02D-D0324")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "BDC_OKCODE", "/00")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "RF02D-KUNNR", CUSTNUM)
        ADD_BDCDATA(BDC_TABLE, "", "", "", "RF02D-BUKRS", PLANT)
        ADD_BDCDATA(BDC_TABLE, "", "", "", "RF02D-VKORG", PLANT)
        ADD_BDCDATA(BDC_TABLE, "", "", "", "RF02D-VTWEG", "DS")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "RF02D-SPART", "AG")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "RF02D-D0324", "X")

        ADD_BDCDATA(BDC_TABLE, "SAPMF02D", "0324", "X", "", "")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "BDC_CURSOR", "RF02D-KTONR(04)")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "BDC_OKCODE", "=ENTR")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "KNVP-PARVW(01)", "SP")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "KNVP-PARVW(02)", "SH")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "KNVP-PARVW(03)", "BP")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "KNVP-PARVW(04)", "PY")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "RF02D-KTONR(01)", CUSTNUM)
        ADD_BDCDATA(BDC_TABLE, "", "", "", "RF02D-KTONR(02)", CUSTNUM)
        ADD_BDCDATA(BDC_TABLE, "", "", "", "RF02D-KTONR(03)", CUSTNUM)
        ADD_BDCDATA(BDC_TABLE, "", "", "", "RF02D-KTONR(04)", CUSTNUM)

        ADD_BDCDATA(BDC_TABLE, "SAPMF02D", "0324", "X", "", "")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "BDC_CURSOR", "KNVP-PARVW(01)")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "BDC_OKCODE", "=UPDA")


        If objRfcFunc.Call = True Then

            oMESS = objRfcFunc.Imports("MESSG")
            Pesan = oMESS.Value("MSGTX")

            If InStr(Pesan, "have been made") > 0 Then
                SaveOkay = True
                'MsgBox(Pesan, MsgBoxStyle.Information, "Info")
            Else
                MsgBox(Pesan, MsgBoxStyle.Critical, "Error")
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

    Private Sub CREATE_CUSTOMER()

        'Call LogToSAP(False)

        'If SAPConStatus = False Then Exit Sub

        SaveOkay = False
        Indeks = 0
        CUSTNUM = ""

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

        objRfcFunc.exports("TRANCODE") = "XD01"
        objRfcFunc.exports("UPDMODE") = "S"

        BDC_TABLE = objRfcFunc.Tables("BDCTABLE")

        ADD_BDCDATA(BDC_TABLE, "SAPMF02D", "0100", "X", "", "")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "BDC_CURSOR", "RF02D-KTOKD")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "BDC_OKCODE", "/00")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "RF02D-BUKRS", PLANT)                ' Company code
        ADD_BDCDATA(BDC_TABLE, "", "", "", "RF02D-VKORG", PLANT)                ' Sales organization
        ADD_BDCDATA(BDC_TABLE, "", "", "", "RF02D-VTWEG", "DS")                 ' Distribution channel
        ADD_BDCDATA(BDC_TABLE, "", "", "", "RF02D-SPART", "AG")                 ' Division
        ADD_BDCDATA(BDC_TABLE, "", "", "", "RF02D-KTOKD", "TR3P")               ' Account group

        ADD_BDCDATA(BDC_TABLE, "SAPMF02D", "0110", "X", "", "")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "BDC_CURSOR", "KNA1-TELFX")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "BDC_OKCODE", "/00")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "KNA1-ANRED", "Company")             ' Title
        ADD_BDCDATA(BDC_TABLE, "", "", "", "KNA1-NAME1", txtName.Text)          ' Name
        ADD_BDCDATA(BDC_TABLE, "", "", "", "KNA1-SORTL", SearchTerm)            ' Search term
        ADD_BDCDATA(BDC_TABLE, "", "", "", "KNA1-STRAS", txtAddr1.Text)         ' Street
        ADD_BDCDATA(BDC_TABLE, "", "", "", "KNA1-ORT01", txtCity.Text)          ' City
        ADD_BDCDATA(BDC_TABLE, "", "", "", "KNA1-PSTLZ", txtPostal.Text)        ' Postal code
        ADD_BDCDATA(BDC_TABLE, "", "", "", "KNA1-LAND1", "id")                  ' Country
        ADD_BDCDATA(BDC_TABLE, "", "", "", "KNA1-SPRAS", "EN")                  ' Language key
        ADD_BDCDATA(BDC_TABLE, "", "", "", "KNA1-TELF1", txtPhone.Text)         ' Telephone
        ADD_BDCDATA(BDC_TABLE, "", "", "", "KNA1-TELFX", txtFax.Text)           ' Fax

        ADD_BDCDATA(BDC_TABLE, "SAPMF02D", "0120", "X", "", "")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "BDC_CURSOR", "KNA1-LZONE")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "BDC_OKCODE", "/00")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "KNA1-LZONE", "0000000001")          ' Transport zone

        ADD_BDCDATA(BDC_TABLE, "SAPMF02D", "0125", "X", "", "")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "BDC_CURSOR", "KNA1-NIELS")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "BDC_OKCODE", "/00")

        ADD_BDCDATA(BDC_TABLE, "SAPMF02D", "0130", "X", "", "")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "BDC_CURSOR", "KNBK-BANKS(01)")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "BDC_OKCODE", "=ENTR")

        ADD_BDCDATA(BDC_TABLE, "SAPMF02D", "0340", "X", "", "")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "BDC_CURSOR", "RF02D-KUNNR")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "BDC_OKCODE", "=ENTR")

        ADD_BDCDATA(BDC_TABLE, "SAPMF02D", "0370", "X", "", "")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "BDC_CURSOR", "RF02D-KUNNR")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "BDC_OKCODE", "=ENTR")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "KNA1-CIVVE", "X")

        ADD_BDCDATA(BDC_TABLE, "SAPMF02D", "0360", "X", "", "")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "BDC_CURSOR", "KNVK-NAMEV(01)")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "BDC_OKCODE", "=ENTR")

        ADD_BDCDATA(BDC_TABLE, "SAPMF02D", "0210", "X", "", "")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "BDC_CURSOR", "KNB1-FDGRV")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "BDC_OKCODE", "/00")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "KNB1-AKONT", "1221000")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "KNB1-FDGRV", "E3")

        ADD_BDCDATA(BDC_TABLE, "SAPMF02D", "0215", "X", "", "")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "BDC_CURSOR", "KNB1-ZTERM")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "BDC_OKCODE", "/00")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "KNB1-ZTERM", cboTOP.Text)

        ADD_BDCDATA(BDC_TABLE, "SAPMF02D", "0220", "X", "", "")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "BDC_CURSOR", "KNB5-MAHNA")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "BDC_OKCODE", "/00")

        ADD_BDCDATA(BDC_TABLE, "SAPMF02D", "0230", "X", "", "")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "BDC_CURSOR", "KNB1-VRSNR")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "BDC_OKCODE", "/00")

        ADD_BDCDATA(BDC_TABLE, "SAPMF02D", "0310", "X", "", "")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "BDC_CURSOR", "KNVV-VERSG")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "BDC_OKCODE", "/00")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "KNVV-AWAHR", "100")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "KNVV-KDGRP", "03")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "KNVV-WAERS", "IDR")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "KNVV-KALKS", "G")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "KNVV-VERSG", "1")

        ADD_BDCDATA(BDC_TABLE, "SAPMF02D", "0315", "X", "", "")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "BDC_CURSOR", "KNVV-VWERK")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "BDC_OKCODE", "/00")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "KNVV-LPRIO", "02")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "KNVV-KZAZU", "X")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "KNVV-VSBED", "01")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "KNVV-VWERK", PLANT)
        ADD_BDCDATA(BDC_TABLE, "", "", "", "KNVV-ANTLF", "9")

        ADD_BDCDATA(BDC_TABLE, "SAPMF02D", "0320", "X", "", "")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "BDC_CURSOR", "KNVV-KTGRD")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "BDC_OKCODE", "/00")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "KNVV-INCO1", "FOB")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "KNVV-INCO2", "JAKARTA")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "KNVV-ZTERM", cboTOP.Text)
        ADD_BDCDATA(BDC_TABLE, "", "", "", "KNVV-KTGRD", "01")

        ADD_BDCDATA(BDC_TABLE, "SAPMF02D", "1350", "X", "", "")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "BDC_CURSOR", "KNVI-TAXKD(01)")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "BDC_OKCODE", "=ENTR")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "KNVI-TAXKD(01)", "1")

        ADD_BDCDATA(BDC_TABLE, "SAPMF02D", "1350", "X", "", "")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "BDC_CURSOR", "RF02D-KUNNR")
        ADD_BDCDATA(BDC_TABLE, "", "", "", "BDC_OKCODE", "=UPDA")


        If objRfcFunc.Call = True Then

            oMESS = objRfcFunc.Imports("MESSG")
            Pesan = oMESS.Value("MSGTX")

            MsgBox(Pesan, MsgBoxStyle.Information, "Info")

            If InStr(Pesan, "has been created") > 0 Then
                CUSTNUM = Strings.Mid(Pesan, 10, 10)
                SaveOkay = True

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

    Private Sub ADD_BDCDATA(BdcTable As Object, program As String, dynpro As String, dynbegin As String, fnam As String, fval As String)
        Indeks = Indeks + 1
        BdcTable.Rows.Add()
        BdcTable.Value(Indeks, "PROGRAM") = program ' Program Name
        BdcTable.Value(Indeks, "DYNPRO") = dynpro ' Dynpro Number
        BdcTable.Value(Indeks, "DYNBEGIN") = dynbegin ' X if a screen
        BdcTable.Value(Indeks, "FNAM") = fnam ' Field Name
        BdcTable.Value(Indeks, "FVAL") = fval ' Field Value
    End Sub

    Private Sub FrmCustomer_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Call Local_Connect()

        Query = "SELECT * FROM [PAYTERMS] ORDER BY [recno] "

        DS = New DataSet
        DA = New SqlDataAdapter(Query, Conn)
        DA.Fill(DS, "TOP")

        cboTOP.Items.Clear()

        For n = 0 To DS.Tables("TOP").Rows.Count - 1
            cboTOP.Items.Add(DS.Tables("TOP").Rows(n)("ZTERM"))
        Next

        cboTOP.SelectedIndex = 0


        If CUSTNUM = "" Then

            Me.Text = "Create Customer Master"

            txtName.Select()

        Else

            Me.Text = "Edit Customer Master"

            Query = "SELECT * FROM [MASCUST] WHERE [flkode]='" & CUSTNUM & "'"

            DS = New DataSet
            DA = New SqlDataAdapter(Query, Conn)
            DA.Fill(DS, "CUST")

            If DS.Tables("CUST").Rows.Count = 1 Then

                lblCode.Text = DS.Tables("CUST").Rows(0)("flkode")
                txtName.Text = DS.Tables("CUST").Rows(0)("flnama")
                txtAddr1.Text = "" & DS.Tables("CUST").Rows(0)("flala1")
                txtAddr2.Text = "" & DS.Tables("CUST").Rows(0)("flala2")
                txtCity.Text = DS.Tables("CUST").Rows(0)("flwila")
                txtPostal.Text = "" & DS.Tables("CUST").Rows(0)("flpos")
                txtPhone.Text = "" & DS.Tables("CUST").Rows(0)("flphon")
                txtFax.Text = "" & DS.Tables("CUST").Rows(0)("flfax")


                If Not IsDBNull(DS.Tables("CUST").Rows(0)("flterm")) Then
                    cboTOP.Text = DS.Tables("CUST").Rows(0)("flterm")
                End If

                txtName.Select()

            End If

        End If


    End Sub

    Private Sub txtName_GotFocus(sender As Object, e As EventArgs) Handles txtName.GotFocus
        txtName.BackColor = Color.FromArgb(254, 240, 158)
    End Sub

    Private Sub txtName_KeyDown(sender As Object, e As KeyEventArgs) Handles txtName.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtAddr1.Select()
        End If
    End Sub

    Private Sub txtName_LostFocus(sender As Object, e As EventArgs) Handles txtName.LostFocus

        txtName.BackColor = Color.White

        SearchTerm = ""

        For n = 1 To Len(txtName.Text)
            If Trim(Mid(txtName.Text, n, 1)) <> "" Then
                SearchTerm = SearchTerm & Mid(txtName.Text, n, 1)
            End If
        Next

        SearchTerm = Trim(Mid("TR-" & SearchTerm, 1, 10))

    End Sub

    Private Sub txtAddr1_GotFocus(sender As Object, e As EventArgs) Handles txtAddr1.GotFocus
        txtAddr1.BackColor = Color.FromArgb(254, 240, 158)
    End Sub

    Private Sub txtAddr1_KeyDown(sender As Object, e As KeyEventArgs) Handles txtAddr1.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtAddr2.Select()
        End If
    End Sub

    Private Sub txtAddr1_LostFocus(sender As Object, e As EventArgs) Handles txtAddr1.LostFocus
        txtAddr1.BackColor = Color.White
    End Sub

    Private Sub txtAddr2_GotFocus(sender As Object, e As EventArgs) Handles txtAddr2.GotFocus
        txtAddr2.BackColor = Color.FromArgb(254, 240, 158)
    End Sub

    Private Sub txtAddr2_KeyDown(sender As Object, e As KeyEventArgs) Handles txtAddr2.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtCity.Select()
        End If
    End Sub

    Private Sub txtAddr2_LostFocus(sender As Object, e As EventArgs) Handles txtAddr2.LostFocus
        txtAddr2.BackColor = Color.White
    End Sub

    Private Sub txtCity_GotFocus(sender As Object, e As EventArgs) Handles txtCity.GotFocus
        txtCity.BackColor = Color.FromArgb(254, 240, 158)
    End Sub

    Private Sub txtCity_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCity.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtPostal.Select()
        End If
    End Sub

    Private Sub txtCity_LostFocus(sender As Object, e As EventArgs) Handles txtCity.LostFocus
        txtCity.BackColor = Color.White
    End Sub

    Private Sub txtPhone_GotFocus(sender As Object, e As EventArgs) Handles txtPhone.GotFocus
        txtPhone.BackColor = Color.FromArgb(254, 240, 158)
    End Sub

    Private Sub txtPhone_KeyDown(sender As Object, e As KeyEventArgs) Handles txtPhone.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtFax.Select()
        End If
    End Sub

    Private Sub txtPhone_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPhone.KeyPress
        If Not (Char.IsDigit(e.KeyChar) Or "-()".Contains(e.KeyChar) Or e.KeyChar = Chr(Keys.Back)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtPhone_LostFocus(sender As Object, e As EventArgs) Handles txtPhone.LostFocus
        txtPhone.BackColor = Color.White
    End Sub

    Private Sub cboTOP_KeyDown(sender As Object, e As KeyEventArgs) Handles cboTOP.KeyDown
        If e.KeyCode = Keys.Enter Then
            btnSave.Select()
        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        CUSTNUM = ""
        Me.Dispose()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        If Trim(txtName.Text) = "" Then
            MsgBox("Please enter [Customer Name] ! ", vbCritical, "Warning")
            txtName.Select()
            Exit Sub
        End If

        If Trim(txtCity.Text) = "" Then
            MsgBox("Please enter [City] ! ", vbCritical, "Warning")
            txtCity.Select()
            Exit Sub
        End If


        If MsgBox(" Save Data  ? ", vbQuestion + vbYesNo + vbDefaultButton2, "Save Data") = vbNo Then
            Exit Sub
        End If



        If CUSTNUM = "" Then

            Call CREATE_CUSTOMER()

            If SaveOkay = True Then

                Call EXTEND_CUSTOMER()

                If SaveOkay = True Then

                    CUSTNUM = Trim(Str(Val(CUSTNUM)))

                    lblCode.Text = CUSTNUM

                    Try
                        Call Local_Connect()

                        Query = "INSERT INTO [MASCUST] (flkode,flnama,flala1,flala2,flwila,flphon,flfax,flterm,flpos) " & _
                                "VALUES ('" & lblCode.Text & "','" & txtName.Text & "','" & txtAddr1.Text & "'," & _
                                        "'" & txtAddr2.Text & "','" & txtCity.Text & "','" & txtPhone.Text & "'," & _
                                        "'" & txtFax.Text & "','" & cboTOP.Text & "','" & txtPostal.Text & "')"

                        CMD = New SqlCommand
                        CMD.Connection = Conn
                        CMD.CommandText = Query
                        CMD.ExecuteNonQuery()


                        Me.Dispose()

                    Catch ex As Exception
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try

                End If

            End If

        Else

            Call UPDATE_CUSTOMER()

            If SaveOkay Then

                Try
                    Call Local_Connect()

                    Query = "UPDATE [MASCUST] " & _
                               "SET [flnama]='" & txtName.Text & "', [flala1]='" & txtAddr1.Text & "'," & _
                                  " [flala2]='" & txtAddr2.Text & "', [flwila]='" & txtCity.Text & "'," & _
                                  " [flphon]='" & txtPhone.Text & "', [flfax]='" & txtFax.Text & "'," & _
                                  " [flpos]='" & txtPostal.Text & "', [flterm]='" & cboTOP.Text & "' " & _
                             "WHERE [flkode]='" & CUSTNUM & "'"

                    CMD = New SqlCommand
                    CMD.Connection = Conn
                    CMD.CommandText = Query
                    CMD.ExecuteNonQuery()


                    Me.Dispose()

                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try




            End If

            Me.Dispose()

        End If




    End Sub

    Private Sub txtPostal_GotFocus(sender As Object, e As EventArgs) Handles txtPostal.GotFocus
        txtPostal.BackColor = Color.FromArgb(254, 240, 158)
    End Sub

    Private Sub txtPostal_KeyDown(sender As Object, e As KeyEventArgs) Handles txtPostal.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtPhone.Select()
        End If
    End Sub

    Private Sub txtPostal_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPostal.KeyPress
        If Not Char.IsNumber(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

   
    Private Sub txtPostal_LostFocus(sender As Object, e As EventArgs) Handles txtPostal.LostFocus
        txtPostal.BackColor = Color.White
    End Sub

    Private Sub txtFax_GotFocus(sender As Object, e As EventArgs) Handles txtFax.GotFocus
        txtFax.BackColor = Color.FromArgb(254, 240, 158)
    End Sub

    Private Sub txtFax_KeyDown(sender As Object, e As KeyEventArgs) Handles txtFax.KeyDown
        If e.KeyCode = Keys.Enter Then
            cboTOP.Select()
        End If
    End Sub

    Private Sub txtFax_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtFax.KeyPress
        If Not (Char.IsDigit(e.KeyChar) Or "-()".Contains(e.KeyChar) Or e.KeyChar = Chr(Keys.Back)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtFax_LostFocus(sender As Object, e As EventArgs) Handles txtFax.LostFocus
        txtFax.BackColor = Color.White
    End Sub

    Private Sub txtName_TextChanged(sender As Object, e As EventArgs) Handles txtName.TextChanged

    End Sub
End Class