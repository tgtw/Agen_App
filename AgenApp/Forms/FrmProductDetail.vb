Imports System.Data.SqlClient

Public Class FrmProductDetail

    Public KodeItem As String
    Dim SaveOkay As Boolean = False
    Dim MATNUM As String = ""


    Private Sub BAPI_MATERIAL_SAVEDATA(ByRef MATNUM As String)

        'Call LogToSAP(True)

        'If SAPConStatus = False Then Exit Sub

        Dim VAL_CLASS As String = ""
        Dim ACC_ASSIGN As String = ""

        If cboGrup.Text = "FABRIC" Then
            VAL_CLASS = "7930"
            ACC_ASSIGN = "04"

        ElseIf cboGrup.Text = "GREIGE" Then
            VAL_CLASS = "7930"
            ACC_ASSIGN = "04"

        Else
            VAL_CLASS = ""
        End If


        Dim oFuncCtrl As Object
        Dim oTheFunc As Object
        Dim FuncCommit As Object

        Dim oHEADDATA As Object
        Dim oCLIENTDATA As Object
        Dim oCLIENTDATAX As Object
        Dim oPLANTDATA As Object
        Dim oPLANTDATAX As Object
        Dim oSLOCDATA As Object
        Dim oSLOCDATAX As Object
        Dim oVALUEDATA As Object
        Dim oVALUEDATAX As Object
        Dim oSALESDATA As Object
        Dim oSALESDATAX As Object

        Dim MATDESC As Object
        Dim TAXCLASS As Object
        Dim retMess As Object


        Dim returnFunc As Boolean = False


        oFuncCtrl = CreateObject("SAP.Functions")
        oFuncCtrl.Connection = SAPConn

        oTheFunc = oFuncCtrl.Add("BAPI_MATERIAL_SAVEDATA")


        '===================
        ' IMPORT PARAMETERS 
        '===================

        oHEADDATA = oTheFunc.Exports.Item("HEADDATA")

        oHEADDATA.Value("MATERIAL") = MATNUM
        oHEADDATA.Value("IND_SECTOR") = "T"      ' Industry Sectors
        oHEADDATA.Value("MATL_TYPE") = "FERX"    ' Finish Product Textile
        oHEADDATA.Value("BASIC_VIEW") = "X"
        oHEADDATA.Value("SALES_VIEW") = "X"
        oHEADDATA.Value("PURCHASE_VIEW") = "X"
        oHEADDATA.Value("STORAGE_VIEW") = "X"
        oHEADDATA.Value("ACCOUNT_VIEW") = "X"


        oCLIENTDATA = oTheFunc.Exports.Item("CLIENTDATA")

        oCLIENTDATA.Value("MATL_GROUP") = cboGrup.Text
        oCLIENTDATA.Value("BASE_UOM") = cboUnit.Text
        oCLIENTDATA.Value("BASE_UOM_ISO") = cboUnit.Text
        oCLIENTDATA.Value("ITEM_CAT") = "ZAGN"
        oCLIENTDATA.Value("TRANS_GRP") = "0003"
        'CLIENTDATA.Value("BATCH_MGMT") = "X"


        oCLIENTDATAX = oTheFunc.Exports.Item("CLIENTDATAX")

        oCLIENTDATAX.Value("MATL_GROUP") = "X"
        oCLIENTDATAX.Value("BASE_UOM") = "X"
        oCLIENTDATAX.Value("BASE_UOM_ISO") = "X"
        oCLIENTDATAX.Value("ITEM_CAT") = "X"
        oCLIENTDATAX.Value("TRANS_GRP") = "0003"
        'oCLIENTDATAX.Value("BATCH_MGMT") = "X"


        oPLANTDATA = oTheFunc.Exports.Item("PLANTDATA")

        oPLANTDATA.Value("PLANT") = PLANT
        oPLANTDATA.Value("PUR_GROUP") = PGROUP
        oPLANTDATA.Value("AVAILCHECK") = "02"
        oPLANTDATA.Value("LOADINGGRP") = "0001"
        oPLANTDATA.Value("PROFIT_CTR") = PLANT


        oPLANTDATAX = oTheFunc.Exports.Item("PLANTDATAX")

        oPLANTDATAX.Value("PLANT") = PLANT
        oPLANTDATAX.Value("PUR_GROUP") = "X"
        oPLANTDATAX.Value("AVAILCHECK") = "X"
        oPLANTDATAX.Value("LOADINGGRP") = "X"
        oPLANTDATA.Value("PROFIT_CTR") = "X"


        oSLOCDATA = oTheFunc.Exports.Item("STORAGELOCATIONDATA")

        oSLOCDATA.Value("PLANT") = PLANT
        oSLOCDATA.Value("STGE_LOC") = SLOC


        oSLOCDATAX = oTheFunc.Exports.Item("STORAGELOCATIONDATAX")

        oSLOCDATAX.Value("PLANT") = PLANT
        oSLOCDATAX.Value("STGE_LOC") = SLOC


        oVALUEDATA = oTheFunc.Exports.Item("VALUATIONDATA")

        oVALUEDATA.Value("VAL_AREA") = PLANT
        oVALUEDATA.Value("PRICE_CTRL") = "V"
        oVALUEDATA.Value("PRICE_UNIT") = "1"
        oVALUEDATA.Value("VAL_CLASS") = VAL_CLASS


        oVALUEDATAX = oTheFunc.Exports.Item("VALUATIONDATAX")

        oVALUEDATAX.Value("VAL_AREA") = PLANT
        oVALUEDATAX.Value("PRICE_CTRL") = "X"
        oVALUEDATAX.Value("PRICE_UNIT") = "X"
        oVALUEDATAX.Value("VAL_CLASS") = "X"


        oSALESDATA = oTheFunc.Exports.Item("SALESDATA")

        oSALESDATA.Value("SALES_ORG") = PLANT
        oSALESDATA.Value("DISTR_CHAN") = "DS"
        oSALESDATA.Value("MATL_STATS") = "1"
        oSALESDATA.Value("ITEM_CAT") = "ZAGN"
        oSALESDATA.Value("MAT_PR_GRP") = "01"
        oSALESDATA.Value("ACCT_ASSGT") = "04"



        oSALESDATAX = oTheFunc.Exports.Item("SALESDATAX")

        oSALESDATAX.Value("SALES_ORG") = PLANT
        oSALESDATAX.Value("DISTR_CHAN") = "DS"
        oSALESDATAX.Value("MATL_STATS") = "X"
        oSALESDATAX.Value("ITEM_CAT") = "X"
        oSALESDATAX.Value("MAT_PR_GRP") = "X"
        oSALESDATAX.Value("ACCT_ASSGT") = "X"


        '========
        ' TABLES
        '========

        MATDESC = oTheFunc.tables.Item("MATERIALDESCRIPTION")
        MATDESC.Rows.Add()
        MATDESC.Value(1, "LANGU") = "EN"
        MATDESC.Value(1, "MATL_DESC") = txtNama.Text

        TAXCLASS = oTheFunc.tables.Item("TAXCLASSIFICATIONS")
        TAXCLASS.Rows.Add()
        TAXCLASS.Value(1, "DEPCOUNTRY") = "ID"
        TAXCLASS.Value(1, "TAX_TYPE_1") = "MWST"
        TAXCLASS.Value(1, "TAXCLASS_1") = "1"


        FuncCommit = oFuncCtrl.Add("BAPI_TRANSACTION_COMMIT")

        If oTheFunc.Call Then

            If FuncCommit.Call Then

                retMess = oTheFunc.tables.Item("RETURNMESSAGES")

                For n = 1 To retMess.RowCount

                    'MsgBox(retMess.Value(n, "MESSAGE"))

                    If InStr(retMess.Value(n, "MESSAGE"), "created") > 0 Then
                        MsgBox(retMess.Value(n, "MESSAGE"))
                        SaveOkay = True
                        Exit For
                    End If

                Next n

            End If

        End If


    End Sub

    Private Sub GetKodeProduk()

        txtKode.Text = ""

        If Trim(txtCorak.Text) = "" Then Exit Sub

        If Trim(txtWarna.Text) = "" Then Exit Sub

        If chkExtNum.Checked = True Then Exit Sub


        Dim KodeProduk As String = ""

        KodeProduk = Mid(cboGrup.Text, 1, 1)

        KodeProduk = KodeProduk & "." & Trim(txtCorak.Text)

        KodeProduk = KodeProduk & "." & "A"

        KodeProduk = KodeProduk & "." & Trim(txtWarna.Text)

        txtKode.Text = Trim(Strings.Mid(KodeProduk, 1, 18))



    End Sub

    Private Sub GetNamaProduk()

        If Trim(txtMerk.Text) <> "" Then
            txtNama.Text = Trim(txtMerk.Text) & ", "
        End If

        If Trim(txtCorak.Text) <> "" Then
            txtNama.Text = txtNama.Text & Trim(txtCorak.Text) & ", "
        End If

        If Trim(lblWarna.Text) <> "" Then
            txtNama.Text = txtNama.Text & Trim(txtWarna.Text)
        End If

    End Sub

    Private Sub FrmProdukDetail_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Call Local_Connect()

        If ConnectStatus = False Then Me.Close()

        cboGrup.SelectedIndex = 0
        cboUnit.SelectedIndex = 0

        CARI = ""

        If KodeItem = "" Then

            Me.Text = "Create Product Master"

            txtMerk.Select()

            chkExtNum.Checked = False
            txtKode.ReadOnly = True
            txtKode.BackColor = Color.WhiteSmoke

        Else

            Me.Text = "Edit Product Master"

            Query = "SELECT * FROM [MASPROD] WHERE [kdprod]='" & KodeItem & "'"

            DS = New DataSet
            DA = New SqlDataAdapter(Query, Conn)
            DA.Fill(DS, "PRODUK")

            If DS.Tables("PRODUK").Rows.Count = 1 Then

                txtKode.Text = DS.Tables("PRODUK").Rows(0)("kdprod")
                txtNama.Text = DS.Tables("PRODUK").Rows(0)("nmprod")
                cboGrup.Text = IIf(Mid(txtKode.Text, 1, 1) = "F", "FABRIC", "GREIGE")
                txtCorak.Text = Mid(txtKode.Text, 3, 6)
                txtWarna.Text = DS.Tables("PRODUK").Rows(0)("kdwarna")

                cboUnit.Text = DS.Tables("PRODUK").Rows(0)("unit")
                txtHBeli.Text = Format(DS.Tables("PRODUK").Rows(0)("hbeli"), "#,##0")
                txtHJual.Text = Format(DS.Tables("PRODUK").Rows(0)("hjual"), "#,##0")

                txtNama.Select()

            End If

        End If

    End Sub

    Private Sub txtNama_GotFocus(sender As Object, e As EventArgs) Handles txtNama.GotFocus
        'txtNama.BackColor = Color.FromArgb(254, 240, 158)
    End Sub

    Private Sub txtNama_KeyDown(sender As Object, e As KeyEventArgs) Handles txtNama.KeyDown
        'If e.KeyCode = Keys.Enter Then
        '    cboGrup.Select()
        'End If
    End Sub

    Private Sub txtNama_LostFocus(sender As Object, e As EventArgs) Handles txtNama.LostFocus
        'txtNama.BackColor = Color.White
    End Sub

    Private Sub cboGrup_KeyDown(sender As Object, e As KeyEventArgs) Handles cboGrup.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtCorak.Select()
        End If
    End Sub

    Private Sub txtCorak_GotFocus(sender As Object, e As EventArgs) Handles txtCorak.GotFocus
        txtCorak.BackColor = Color.FromArgb(254, 240, 158)
    End Sub

    Private Sub txtCorak_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCorak.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtWarna.Select()
        End If
    End Sub

    Private Sub txtCorak_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCorak.KeyPress
        If Not Char.IsNumber(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtCorak_LostFocus(sender As Object, e As EventArgs) Handles txtCorak.LostFocus
        txtCorak.BackColor = Color.White
        If Trim(txtCorak.Text) <> "" Then
            txtCorak.Text = Format(Val(txtCorak.Text), "000000")
        End If
    End Sub

    Private Sub txtWarna_GotFocus(sender As Object, e As EventArgs) Handles txtWarna.GotFocus
        txtWarna.BackColor = Color.FromArgb(254, 240, 158)
    End Sub

    Private Sub txtWarna_KeyDown(sender As Object, e As KeyEventArgs) Handles txtWarna.KeyDown
        If e.KeyCode = Keys.Enter Then
            If Trim(txtWarna.Text) <> "" Then
                Call btnFindColor_Click(txtWarna.Text, e)
            End If
            cboUnit.Select()            
        End If
    End Sub

    Private Sub txtWarna_LostFocus(sender As Object, e As EventArgs) Handles txtWarna.LostFocus
        txtWarna.BackColor = Color.White
        'Call btnFindColor_Click(txtWarna.Text, e)
        Call GetNamaProduk()
    End Sub

    Private Sub txtWarna_TextChanged(sender As Object, e As EventArgs) Handles txtWarna.TextChanged

        If chkExtNum.Checked = False And Trim(txtWarna.Text) <> "" Then
            Call GetKodeProduk()
        End If

    End Sub

    Private Sub cboUnit_KeyDown(sender As Object, e As KeyEventArgs) Handles cboUnit.KeyDown
        If e.KeyCode = Keys.Enter Then            
            txtHBeli.Select()
        End If
    End Sub

    Private Sub txtHBeli_GotFocus(sender As Object, e As EventArgs) Handles txtHBeli.GotFocus
        txtHBeli.BackColor = Color.FromArgb(254, 240, 158)
        txtHBeli.Text = Replace(Replace(txtHBeli.Text, ",", ""), ".", "")
    End Sub

    Private Sub txtHBeli_KeyDown(sender As Object, e As KeyEventArgs) Handles txtHBeli.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtHJual.Select()
        End If
    End Sub

    Private Sub txtHBeli_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtHBeli.KeyPress
        If Not Char.IsNumber(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtHBeli_LostFocus(sender As Object, e As EventArgs) Handles txtHBeli.LostFocus
        txtHBeli.BackColor = Color.White
        txtHBeli.Text = Format(Val(txtHBeli.Text), "#,##0")
    End Sub


    Private Sub txtHJual_GotFocus(sender As Object, e As EventArgs) Handles txtHJual.GotFocus
        txtHJual.BackColor = Color.FromArgb(254, 240, 158)
        txtHJual.Text = Replace(Replace(txtHJual.Text, ",", ""), ".", "")
    End Sub

    Private Sub txtHJual_KeyDown(sender As Object, e As KeyEventArgs) Handles txtHJual.KeyDown
        If e.KeyCode = Keys.Enter Then
            btnSave.Select()
        End If
    End Sub

    Private Sub txtHJual_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtHJual.KeyPress
        If Not Char.IsNumber(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtHJual_LostFocus(sender As Object, e As EventArgs) Handles txtHJual.LostFocus
        txtHJual.BackColor = Color.White
        txtHJual.Text = Format(Val(txtHJual.Text), "#,##0")
    End Sub

    Private Sub cboGrup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboGrup.SelectedIndexChanged
        If chkExtNum.Checked = False Then
            Call GetKodeProduk()
        End If
    End Sub

    Private Sub txtCorak_TextAlignChanged(sender As Object, e As EventArgs) Handles txtCorak.TextAlignChanged

    End Sub

    Private Sub txtCorak_TextChanged(sender As Object, e As EventArgs) Handles txtCorak.TextChanged
        If chkExtNum.Checked = False Then
            Call GetKodeProduk()
        End If
        Call GetNamaProduk()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Dispose()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        txtKode.Text = Trim(txtKode.Text)
        txtNama.Text = Trim(txtNama.Text)
        txtWarna.Text = Trim(txtWarna.Text)

        If chkExtNum.Checked = False Then
            Call GetKodeProduk()
            Call GetNamaProduk()
        End If

        If txtMerk.Text = "" Then
            MsgBox(" Please entry [Merk] or [Brand] ! ", vbCritical, "Warning")
            txtMerk.Select()
            Exit Sub
        End If

        'If Trim(cboGrup.Text) = "" Then
        '    MsgBox(" [Grup Produk] harus di pilih ! ", vbCritical, "Warning")
        '    cboGrup.Select()
        '    Exit Sub
        'End If

        If txtCorak.Text = "" Then
            MsgBox(" [Corak] harus di isi ! ", vbCritical, "Warning")
            txtCorak.Select()
            Exit Sub
        End If

        If txtWarna.Text = "" Then
            MsgBox(" [Warna] harus di isi ! ", vbCritical, "Warning")
            txtWarna.Select()
            Exit Sub
        End If

        If Trim(cboUnit.Text) = "" Then
            MsgBox(" [Satuan] harus di pilih ! ", vbCritical, "Warning")
            cboUnit.Select()
            Exit Sub
        End If

        If Trim(txtHBeli.Text) = "" Then
            txtHBeli.Text = 0
        End If

        If Trim(txtHJual.Text) = "" Then
            txtHJual.Text = 0
        End If

        Call GetNamaProduk()


        If MsgBox(" Save Data  ? ", vbQuestion + vbYesNo + vbDefaultButton2, "Save") = vbNo Then
            Exit Sub
        End If



        Dim HBELI As Single = Val(Replace(Replace(txtHBeli.Text, ".", ""), ",", "."))
        Dim HJUAL As Single = Val(Replace(Replace(txtHJual.Text, ".", ""), ",", "."))


        If KodeItem = "" Then

            '// CEK KODE PRODUK JIKA SUDAH ADA

            Query = "SELECT * FROM [MASPROD] WHERE [kdprod]='" & txtKode.Text & "'"

            DS = New DataSet
            DA = New SqlDataAdapter(Query, Conn)
            DA.Fill(DS, "PRODUK")

            If DS.Tables("PRODUK").Rows.Count > 0 Then
                MessageBox.Show("Product no. [" & txtKode.Text & "] is already registered !", "Error", MessageBoxButtons.OK, MessageBoxIcon.[Error])
                txtCorak.Select()
                Return
            End If

            SaveOkay = False



            Call BAPI_MATERIAL_SAVEDATA(txtKode.Text)

            If SaveOkay = False Then
                MessageBox.Show("Error save to SAP ! ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            Else

                Me.Cursor = Cursors.WaitCursor

                If cboGrup.Text = "FABRIC" Then

                    Call BAPI_MATERIAL_SAVEDATA(Replace(txtKode.Text, ".A.", ".B."))

                    Call BAPI_MATERIAL_SAVEDATA(Replace(txtKode.Text, ".A.", ".S."))

                    Call BAPI_MATERIAL_SAVEDATA(Replace(txtKode.Text, ".A.", ".X."))

                End If

            End If


            Me.Cursor = Cursors.Default

            If SaveOkay Then

                Try

                    Call Local_Connect()

                    Query = "INSERT INTO [MASPROD] (kdprod,nmprod,corak,kdwarna,nmwarna,grup,unit,hbeli,hjual) " & _
                            "VALUES ('" & txtKode.Text & "','" & txtNama.Text & "','" & txtCorak.Text & "'," & _
                                    "'" & txtWarna.Text & "','" & lblWarna.Text & "','" & cboGrup.Text & "'," & _
                                    "'" & cboUnit.Text & "'," & HBELI & "," & HJUAL & ")"

                    CMD = New SqlCommand
                    CMD.Connection = Conn
                    CMD.CommandText = Query
                    CMD.ExecuteNonQuery()

                    If cboGrup.Text = "FABRIC" Then

                        '// GRADE B

                        MATNUM = Replace(txtKode.Text, ".A.", ".B.")

                        Query = "INSERT INTO [MASPROD] (kdprod,nmprod,corak,kdwarna,nmwarna,grup,unit,hbeli,hjual) " & _
                            "VALUES ('" & MATNUM & "','" & txtNama.Text & "','" & txtCorak.Text & "'," & _
                                    "'" & txtWarna.Text & "','" & lblWarna.Text & "','" & cboGrup.Text & "'," & _
                                    "'" & cboUnit.Text & "'," & HBELI & "," & HJUAL & ")"

                        CMD = New SqlCommand
                        CMD.Connection = Conn
                        CMD.CommandText = Query
                        CMD.ExecuteNonQuery()

                        '// GRADE S

                        MATNUM = Replace(txtKode.Text, ".A.", ".S.")

                        Query = "INSERT INTO [MASPROD] (kdprod,nmprod,corak,kdwarna,nmwarna,grup,unit,hbeli,hjual) " & _
                            "VALUES ('" & MATNUM & "','" & txtNama.Text & "','" & txtCorak.Text & "'," & _
                                    "'" & txtWarna.Text & "','" & lblWarna.Text & "','" & cboGrup.Text & "'," & _
                                    "'" & cboUnit.Text & "'," & HBELI & "," & HJUAL & ")"

                        CMD = New SqlCommand
                        CMD.Connection = Conn
                        CMD.CommandText = Query
                        CMD.ExecuteNonQuery()

                        '// GRADE X

                        MATNUM = Replace(txtKode.Text, ".A.", ".X.")

                        Query = "INSERT INTO [MASPROD] (kdprod,nmprod,corak,kdwarna,nmwarna,grup,unit,hbeli,hjual) " & _
                            "VALUES ('" & MATNUM & "','" & txtNama.Text & "','" & txtCorak.Text & "'," & _
                                    "'" & txtWarna.Text & "','" & lblWarna.Text & "','" & cboGrup.Text & "'," & _
                                    "'" & cboUnit.Text & "'," & HBELI & "," & HJUAL & ")"

                        CMD = New SqlCommand
                        CMD.Connection = Conn
                        CMD.CommandText = Query
                        CMD.ExecuteNonQuery()

                    End If

                    Me.Dispose()



                    'txtKode.Text = ""
                    'txtNama.Text = ""
                    'txtCorak.Text = ""
                    'txtWarna.Text = ""
                    'txtHBeli.Text = ""
                    'txtHJual.Text = ""

                    'txtKode.Select()

                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try

            End If

        Else

            Try

                Query = "UPDATE [MASPROD] " & _
                           "SET [nmprod]='" & txtNama.Text & "', [warna]='" & txtWarna.Text & "'," & _
                              " [unit]='" & cboUnit.Text & "', [hbeli]=" & HBELI & ", [hjual]=" & HJUAL & " " & _
                         "WHERE [kdprod]='" & KodeItem & "'"

                CMD = New SqlCommand
                CMD.Connection = Conn
                CMD.CommandText = Query
                CMD.ExecuteNonQuery()

            Catch ex As Exception
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

        End If

    End Sub


    Private Sub txtKode_GotFocus(sender As Object, e As EventArgs) Handles txtKode.GotFocus
        txtKode.BackColor = Color.FromArgb(254, 240, 158)
    End Sub

    Private Sub txtKode_KeyDown(sender As Object, e As KeyEventArgs) Handles txtKode.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtNama.Select()
        End If
    End Sub

    Private Sub txtKode_LostFocus(sender As Object, e As EventArgs) Handles txtKode.LostFocus
        txtKode.BackColor = Color.White
    End Sub

    Private Sub btnFindColor_Click(sender As Object, e As EventArgs) Handles btnFindColor.Click

Ulang:

        Query = "SELECT * FROM [MASCOL] WHERE [kdwarna]='" & txtWarna.Text & "'"

        DS = New DataSet
        DA = New SqlDataAdapter(Query, Conn)
        DA.Fill(DS, "COLOR")

        If DS.Tables("COLOR").Rows.Count <> 1 Then

            If FrmFindColor.IsHandleCreated Then
                Exit Sub
            End If

            FrmFindColor.ShowDialog()

            txtWarna.Select()
            txtWarna.Text = CARI

            If CARI = "" Then
                Exit Sub
            End If

            GoTo Ulang

        End If

        txtWarna.Text = DS.Tables("COLOR").Rows(0)("kdwarna")
        lblWarna.Text = DS.Tables("COLOR").Rows(0)("nmwarna")

        'txtNama.Text = cboGrup.Text & " DESIGN # " & txtCorak.Text & " COLOR # " & txtWarna.Text

    End Sub

    Private Sub chkExtNum_CheckedChanged(sender As Object, e As EventArgs) Handles chkExtNum.CheckedChanged

        If chkExtNum.Checked = True Then
            txtKode.ReadOnly = False
            txtKode.BackColor = Color.White
        Else
            txtKode.ReadOnly = True
            txtKode.BackColor = Color.WhiteSmoke

            Call GetKodeProduk()
        End If

    End Sub

   
    Private Sub txtMerk_GotFocus(sender As Object, e As EventArgs) Handles txtMerk.GotFocus
        txtMerk.BackColor = Color.FromArgb(254, 240, 158)
    End Sub

    Private Sub txtMerk_KeyDown(sender As Object, e As KeyEventArgs) Handles txtMerk.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtCorak.Select()
        End If
    End Sub

    Private Sub txtMerk_LostFocus(sender As Object, e As EventArgs) Handles txtMerk.LostFocus
        txtMerk.BackColor = Color.White
    End Sub

    Private Sub txtMerk_TextChanged(sender As Object, e As EventArgs) Handles txtMerk.TextChanged
        Call GetNamaProduk()
    End Sub

    Private Sub txtHBeli_TextChanged(sender As Object, e As EventArgs) Handles txtHBeli.TextChanged

    End Sub
End Class