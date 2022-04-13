Imports System.Data.SqlClient

Public Class FrmGoodsIssue

    Dim SaveOkay As Boolean = False
    Dim MATDOC As String = ""

    Private Sub FrmGoodsIssue_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        MDIMain.ToolStrip.Visible = True
    End Sub

    Private Sub FrmGoodsIssue_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Call LOCAL_CONNECT()

        Call GET_PERIOD()

        dtpDate.Value = Format(Now, "dd.MM.yyyy")


        '=====================
        ' DataGridView Design
        '=====================

        DGV.BorderStyle = BorderStyle.FixedSingle
        DGV.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249)
        DGV.CellBorderStyle = DataGridViewCellBorderStyle.Single
        DGV.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise
        DGV.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke
        DGV.BackgroundColor = Color.White

        DGV.EnableHeadersVisualStyles = False
        DGV.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        DGV.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72)
        DGV.ColumnHeadersDefaultCellStyle.ForeColor = Color.White

        DGV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        DGV.ColumnHeadersHeight = 30

        'DGV.RowHeadersVisible = False

        DGV.Columns(4).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight

        DGV.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        With ColUnit
            .Items.Clear()
            .Items.Add("YD")
            .Items.Add("M")
        End With


        DGV.Select()
        DGV.CurrentCell = DGV(0, 0)

 

    End Sub

    Private Sub btnScan_Click(sender As Object, e As EventArgs)

        'Dim i As Integer = 0
        'Dim n As Integer = 0

        'Dim Ada As Boolean = False

        'If txtBarcode.Text = "" Then
        '    txtBarcode.Select()
        '    Exit Sub
        'End If


        'Query = "SELECT * FROM [STOCKBCD] WHERE [barcode]='" & txtBarcode.Text & "'"

        'DS = New DataSet
        'DA = New SqlDataAdapter(Query, Conn)
        'DA.Fill(DS, "STOCKBAR")

        'If DS.Tables("STOCKBAR").Rows.Count <> 1 Then
        '    MessageBox.Show("Barcode not found  !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    txtBarcode.Text = ""
        '    txtBarcode.Select()
        '    Exit Sub
        'End If

        'Ada = False

        'For i = 0 To DGV.Rows.Count - 1
        '    If DGV.Rows(i).Cells(0).Value = txtBarcode.Text Then
        '        Ada = True
        '        Exit For
        '    End If
        'Next

        'If Ada = True Then
        '    DGV.CurrentCell = DGV(0, i)
        'Else

        '    'Query = "INSERT INTO [" & TMPSALES & "] SELECT * FROM [STOCKBCD] WHERE [barcode]='" & txtBarcode.Text & "'"

        '    'CMD = New SqlCommand
        '    'CMD.Connection = Conn
        '    'CMD.CommandText = Query
        '    'CMD.ExecuteNonQuery()

        '    ByPass = True

        '    n = DGV.Rows.Add()
        '    DGV.Rows(n).Cells(0).Value = DS.Tables("STOCKBAR").Rows(0)("barcode")
        '    DGV.Rows(n).Cells(1).Value = DS.Tables("STOCKBAR").Rows(0)("corak")
        '    DGV.Rows(n).Cells(2).Value = DS.Tables("STOCKBAR").Rows(0)("warna")
        '    DGV.Rows(n).Cells(3).Value = DS.Tables("STOCKBAR").Rows(0)("grade")
        '    DGV.Rows(n).Cells(4).Value = DS.Tables("STOCKBAR").Rows(0)("yard")
        '    DGV.Rows(n).Cells(5).Value = DS.Tables("STOCKBAR").Rows(0)("meter")
        '    DGV.CurrentCell = DGV(1, n)

        '    ByPass = False

        'End If

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click

        Try
            If MessageBox.Show("Do you really want to cancel this transaction ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Yes Then

                txtNote.Text = "Untuk Sample"
                txtAkun.Text = "6110500"
                dtpDate.Value = Format(Now, "dd/MM/yyyy")

                DGV.Rows.Clear()
                DGV.Select()
                DGV.CurrentCell = DGV(0, 0)

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try


    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        If Trim(txtAkun.Text) = "" Then
            MessageBox.Show("Please select G/L Account ! ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtAkun.Select()
            Exit Sub
        End If

        '// check posting period

        Dim CurrPeriod As Long = Val(CurYearPeriode & CurMonPeriode)
        Dim PrevPeriod As Long = Val(PrevYearPeriode & PrevMonPeriode)
        Dim PostDate As Long = Val(Mid(Format(dtpDate.Value, "yyyyMMdd"), 1, 6))

        If PostDate < PrevPeriod Or PostDate > CurrPeriod Then
            MsgBox("Posting only possible in periods " & PrevMonPeriode & "-" & PrevYearPeriode & " and " & CurMonPeriode & "-" & CurYearPeriode & " ! ", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Warning")
            dtpDate.Focus()
            Exit Sub
        End If


        For n = 0 To DGV.Rows.Count - 1

            If Trim(DGV.Rows(n).Cells(1).Value) = "" Then
                Exit For
            End If

            If DGV.Rows(n).Cells(4).Value = 0 Then
                MessageBox.Show("Please enter Quantity ! ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                DGV.Select()
                DGV.CurrentCell = DGV(4, n)
                Exit Sub
            End If

        Next

        If MessageBox.Show("Save this transaction ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If


        Call BAPI_GOODSMVT_CREATE()

        If SaveOkay = False Then
            Exit Sub
        End If


        Dim YDS As Double = 0
        Dim MTR As Double = 0

        For n = 0 To DGV.Rows.Count - 1

            If DGV.Rows(n).Cells(4).Value > 0 Then

                If DGV.Rows(n).Cells(5).Value = "YD" Then
                    YDS = DGV.Rows(n).Cells(4).Value
                    MTR = Math.Round(YDS / 1.0936, 2)
                Else
                    MTR = DGV.Rows(n).Cells(4).Value
                    YDS = Math.Round(MTR * 1.0936, 2)
                End If


                Query = "UPDATE [STOCKBCD] " & _
                           "SET [yard]  = [yard]  - " & Replace(YDS, ",", ".") & "," & _
                              " [meter] = [meter] - " & Replace(MTR, ",", ".") & " " & _
                         "WHERE [Barcode] = '" & DGV.Rows(n).Cells(0).Value & "'"

                CMD = New SqlCommand
                CMD.Connection = Conn
                CMD.CommandText = Query
                CMD.ExecuteNonQuery()


                Query = "INSERT INTO [STOCKCARD] (postdate,barcode,corak,warna,grade,yard,meter,unit,sloc,movetype,matdoc) " & _
                        "VALUES ('" & DATECHAR(dtpDate.Value) & "','" & DGV.Rows(n).Cells(0).Value & "'," & _
                                "'" & DGV.Rows(n).Cells(1).Value & "','" & DGV.Rows(n).Cells(2).Value & "'," & _
                                "'" & DGV.Rows(n).Cells(3).Value & "'," & Replace(YDS, ",", ".") & "," & Replace(MTR, ",", ".") & "," & _
                                "'" & DGV.Rows(n).Cells(5).Value & "','" & SLOC & "','201','" & MATDOC & "')"

                CMD = New SqlCommand
                CMD.Connection = Conn
                CMD.CommandText = Query
                CMD.ExecuteNonQuery()


                Query = "INSERT INTO [TRANGID] (docno,barcode,corak,warna,grade,yard,meter,recno) " & _
                        "VALUES ('" & MATDOC & "','" & DGV.Rows(n).Cells(0).Value & "','" & DGV.Rows(n).Cells(1).Value & "'," & _
                                "'" & DGV.Rows(n).Cells(2).Value & "','" & DGV.Rows(n).Cells(3).Value & "'," & _
                                " " & Replace(YDS, ",", ".") & "," & Replace(MTR, ",", ".") & "," & (n + 1) & ")"

                CMD = New SqlCommand
                CMD.Connection = Conn
                CMD.CommandText = Query
                CMD.ExecuteNonQuery()

            End If


        Next

        Query = "INSERT INTO [TRANGIH] (docno, docdate, notes) " & _
                "VALUES ('" & MATDOC & "','" & DATECHAR(dtpDate.Value) & "','" & txtNote.Text & "')"

        CMD = New SqlCommand
        CMD.Connection = Conn
        CMD.CommandText = Query
        CMD.ExecuteNonQuery()


        Query = "DELETE FROM [STOCKBCD] WHERE [Yard] <= 0 "

        CMD = New SqlCommand
        CMD.Connection = Conn
        CMD.CommandText = Query
        CMD.ExecuteNonQuery()


        txtNote.Text = "Untuk Sample"
        txtAkun.Text = "6110500"
        dtpDate.Value = Format(Now, "dd/MM/yyyy")

        DGV.Rows.Clear()
        DGV.Select()
        DGV.CurrentCell = DGV(0, 0)



    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click

        If Trim(DGV.CurrentRow.Cells(1).Value.ToString) = "" Then
            Exit Sub
        End If

        If MsgBox("Do you want to delete line item no. " & Trim(DGV.CurrentRow.ToString) & "  ? ", _
           MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Delete") = MsgBoxResult.Ok Then

            DGV.Rows.RemoveAt(Val(DGV.CurrentRow.ToString))
            DGV.CurrentCell = DGV(0, DGV.RowCount - 1)

        End If





        'Dim Ulang As Integer = DGV.Rows.Count - 1

        'For n = 0 To Ulang
        '    If n > Ulang Then Exit For
        '    If DGV.Rows(n).Cells(7).Value = True Then
        '        Me.DGV.Rows.RemoveAt(n)
        '        Ulang = Ulang - 1
        '        n = n - 1
        '    End If
        'Next

    End Sub

    Private Sub DGV_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DGV.CellEndEdit

        If e.ColumnIndex = 0 Then   '// BARCODE

            Dim BarcodeNo As String = Trim(DGV.Rows(e.RowIndex).Cells(0).Value)

            If BarcodeNo = "" Then
                DGV.CurrentCell = DGV(0, e.RowIndex)
                Exit Sub
            End If

            Query = "SELECT * FROM [STOCKBCD] WHERE [barcode] LIKE '%" & BarcodeNo & "%'"

            DS = New DataSet
            DA = New SqlDataAdapter(Query, Conn)
            DA.Fill(DS, "STOCKBAR")

            If DS.Tables("STOCKBAR").Rows.Count <> 1 Then
                MessageBox.Show("Barcode not found  !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                DGV.Rows(e.RowIndex).Cells(0).Value = ""
                DGV.CurrentCell = DGV(0, e.RowIndex)
                Exit Sub
            End If

            Dim Ada As Boolean = False

            For i = 0 To DGV.Rows.Count - 1
                If DGV.Rows(i).Cells(0).Value = BarcodeNo And i <> e.RowIndex Then
                    Ada = True
                    Exit For
                End If
            Next

            If Ada = True Then
                MessageBox.Show("Barcode already exist  !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                DGV.Rows.RemoveAt(e.RowIndex)
                DGV.CurrentCell = DGV(0, DGV.RowCount - 1)
                Exit Sub
            End If

            DGV.Rows(e.RowIndex).Cells(0).Value = DS.Tables("STOCKBAR").Rows(0)("BARCODE")
            DGV.Rows(e.RowIndex).Cells(1).Value = DS.Tables("STOCKBAR").Rows(0)("CORAK")
            DGV.Rows(e.RowIndex).Cells(2).Value = DS.Tables("STOCKBAR").Rows(0)("WARNA")
            DGV.Rows(e.RowIndex).Cells(3).Value = DS.Tables("STOCKBAR").Rows(0)("GRADE")
            DGV.Rows(e.RowIndex).Cells(4).Value = DS.Tables("STOCKBAR").Rows(0)("YARD")
            DGV.Rows(e.RowIndex).Cells(5).Value = "YD"

            DGV.Rows(e.RowIndex).Cells(4).ReadOnly = False
            DGV.Rows(e.RowIndex).Cells(5).ReadOnly = False

            DGV.CurrentCell = DGV(0, e.RowIndex)

            Call AutoNumberRowsForGridView(DGV)

            'SendKeys.Send("{up}")

        End If

    End Sub

    Private Sub DGV_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DGV.CellValueChanged

        If e.RowIndex < 0 Then Exit Sub

        If e.ColumnIndex = 4 Then
            DGV.Rows(e.RowIndex).Cells(4).Value = DGV.Rows(e.RowIndex).Cells(4).Value * 1
        End If

    End Sub

    Private Sub DGV_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles DGV.EditingControlShowing

        Dim Row As Integer = DGV.CurrentCell.RowIndex
        Dim Col As Integer = DGV.CurrentCell.ColumnIndex

        '// uppercase textbox

        If TypeOf e.Control Is TextBox Then
            DirectCast(e.Control, TextBox).CharacterCasing = CharacterCasing.Upper
        End If

        '// numeric textbox

        If Col = 4 Then
            AddHandler CType(e.Control, TextBox).KeyPress, AddressOf TextBoxNum_keyPress
        End If

    End Sub

    Private Sub TextBoxNum_keyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs)

        Dim Col As Integer = DGV.CurrentCell.ColumnIndex
        Dim Row As Integer = DGV.CurrentCell.RowIndex

        If Col = 4 Then

            If Not (Char.IsDigit(CChar(CStr(e.KeyChar))) Or e.KeyChar = Chr(8)) And Not e.KeyChar = "," Then
                e.Handled = True
            End If

        End If

    End Sub


    Private Sub BAPI_GOODSMVT_CREATE()

        'Call LogToSAP(True)

        'If SAPConStatus = False Then Exit Sub

        SaveOkay = False

        Dim oFuncCtrl As Object
        Dim oTheFunc As Object
        Dim oFuncCommit As Object

        Dim oHEADER As Object
        Dim oCODE As Object
        Dim oITEM As Object
        Dim oHEADRET As Object


        oFuncCtrl = CreateObject("SAP.Functions")
        oFuncCtrl.Connection = SAPConn

        oTheFunc = oFuncCtrl.Add("BAPI_GOODSMVT_CREATE")

        '===================
        ' IMPORT PARAMETERS 
        '===================

        oHEADER = oTheFunc.Exports.Item("GOODSMVT_HEADER")
        oHEADER.Value("PSTNG_DATE") = Format(dtpDate.Value, "yyyyMMdd")
        oHEADER.Value("DOC_DATE") = Format(dtpDate.Value, "yyyyMMdd")
        oHEADER.Value("HEADER_TXT") = Trim(txtNote.Text)

        oCODE = oTheFunc.Exports.Item("GOODSMVT_CODE")
        oCODE.Value("GM_CODE") = "03"


        '========
        ' TABLES
        '========

        oITEM = oTheFunc.tables.Item("GOODSMVT_ITEM")

        Dim i As Integer = 0

        For n = 0 To DGV.Rows.Count - 1

            If DGV.Rows(n).Cells(4).Value > 0 Then

                oITEM.Rows.Add()
                i = i + 1

                oITEM.Value(i, "MATERIAL") = DGV.Rows(n).Cells(1).Value
                oITEM.Value(i, "PLANT") = PLANT
                oITEM.Value(i, "STGE_LOC") = SLOC
                oITEM.Value(i, "MOVE_TYPE") = "201"
                oITEM.Value(i, "ENTRY_QNT") = DGV.Rows(n).Cells(4).Value
                oITEM.Value(i, "ENTRY_UOM") = DGV.Rows(n).Cells(5).Value
                oITEM.Value(i, "COSTCENTER") = PLANT
                oITEM.Value(i, "MVT_IND") = " "
                oITEM.Value(i, "GL_ACCOUNT") = Format(Val(txtAkun.Text), "0000000000")

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

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub DGV_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGV.CellContentClick

    End Sub
End Class
