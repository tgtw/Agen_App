Imports System.Data.SqlClient

Public Class FrmAddBarcode

    Dim NO As Integer = 1
    Dim TEMPFILE As String = ""
    Dim YDS As Double = 0
    Dim MTR As Double = 0
    Dim MODE As String = "ADD"

    Private Sub Load_Data()

        DGV.Rows.Clear()

        Try

            Query = "SELECT * FROM [" & TEMPFILE & "] "

            DS = New DataSet
            DA = New SqlDataAdapter(Query, Conn)
            DA.Fill(DS, "ADDBCD")

            Dim n As Integer = 0

            For n = 0 To DS.Tables("ADDBCD").Rows.Count - 1

                DGV.Rows.Add()
                DGV.Rows(n).Cells(0).Value = DS.Tables("ADDBCD").Rows(n)("barcode")
                DGV.Rows(n).Cells(1).Value = DS.Tables("ADDBCD").Rows(n)("corak")
                DGV.Rows(n).Cells(2).Value = DS.Tables("ADDBCD").Rows(n)("grade")
                DGV.Rows(n).Cells(3).Value = DS.Tables("ADDBCD").Rows(n)("warna")
                DGV.Rows(n).Cells(4).Value = DS.Tables("ADDBCD").Rows(n)("SO")
                DGV.Rows(n).Cells(5).Value = DS.Tables("ADDBCD").Rows(n)("partai")
                DGV.Rows(n).Cells(6).Value = DS.Tables("ADDBCD").Rows(n)("lebar")
                DGV.Rows(n).Cells(7).Value = DS.Tables("ADDBCD").Rows(n)("yard")
                DGV.Rows(n).Cells(8).Value = DS.Tables("ADDBCD").Rows(n)("meter")
                DGV.Rows(n).Cells(9).Value = DS.Tables("ADDBCD").Rows(n)("ket")
            Next

        Catch ex As Exception
            MsgBox(ex.Message)
            'Finally
            '    Conn.Close()
            '    DA.Dispose()
        End Try

        txtBarcode.Focus()


    End Sub

    Private Sub FrmAddBarcode_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed

        Call LOCAL_CONNECT()

        Query = "DELETE FROM [" & TEMPFILE & "] "

        CMD = New SqlCommand
        CMD.Connection = Conn
        CMD.CommandText = Query
        CMD.ExecuteNonQuery()

    End Sub

    Private Sub FrmAddBarcode_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        '=================
        'TEST GITHUB
        '================



        Call LOCAL_CONNECT()

        Call HOST_CONNECT()

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
        DGV.RowHeadersVisible = False

        For n = 0 To 8
            DGV.Columns(n).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        Next


        DGV.Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DGV.Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        DGV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        DGV.ColumnHeadersHeight = 30


        Call ClearObject()




        '=====================
        '  CREATE TEMP TABLE
        '=====================

Ulang:
        TEMPFILE = "TMPBCD" & Format(NO, "00")

        Try

            Query = "SELECT * FROM [" & TEMPFILE & "] "

            DS = New DataSet
            DA = New SqlDataAdapter(Query, Conn)
            DA.Fill(DS, "ADDBCD")

            If DS.Tables("ADDBCD").Rows.Count > 0 Then
                NO = NO + 1
                GoTo Ulang
            End If

        Catch ex As Exception

            Query = "SELECT * INTO [" & TEMPFILE & "] FROM [STOCKBCD] WHERE 1=2 "

            CMD = New SqlCommand
            CMD.Connection = Conn
            CMD.CommandText = Query
            CMD.ExecuteNonQuery()

            GoTo Ulang

        End Try


        txtBarcode.Focus()


    End Sub

    Private Sub txtBarcode_GotFocus(sender As Object, e As EventArgs) Handles txtBarcode.GotFocus
        txtBarcode.BackColor = Color.FromArgb(254, 240, 158)
    End Sub

    Private Sub txtBarcode_KeyDown(sender As Object, e As KeyEventArgs) Handles txtBarcode.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtProduct.Select()
        End If
    End Sub

    Private Sub txtBarcode_LostFocus(sender As Object, e As EventArgs) Handles txtBarcode.LostFocus

        '=========================
        '  CHECK BARCODE IN STOCK
        '=========================

        txtBarcode.BackColor = Color.White

        If Trim(txtBarcode.Text) = "" Then
            txtBarcode.Focus()
            Exit Sub
        End If

        Query = "SELECT * FROM [STOCKBCD] WHERE [Barcode] LIKE '%" & txtBarcode.Text & "%'"

        DS = New DataSet
        DA = New SqlDataAdapter(Query, Conn)
        DA.Fill(DS, "BARCODE")

        If DS.Tables("BARCODE").Rows.Count = 1 Then
            txtBarcode.Text = DS.Tables("BARCODE").Rows(0)("Barcode")
            MessageBox.Show("Barcode No [" & txtBarcode.Text & "] already exist in stock ! ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtBarcode.Text = ""
            txtBarcode.Focus()
            Exit Sub
        End If

        '==================================
        ' CHECK BARCODE IN REFERENCE TABLE
        '==================================

        Query = "SELECT * FROM [AG_TRANBCD] WHERE [Barcode] LIKE '%" & txtBarcode.Text & "%'"

        DS = New DataSet
        DA = New SqlDataAdapter(Query, HostConn)
        DA.Fill(DS, "BARCODE")

        If DS.Tables("BARCODE").Rows.Count = 1 Then

            txtBarcode.Text = DS.Tables("BARCODE").Rows(0)("Barcode")
            txtProduct.Text = DS.Tables("BARCODE").Rows(0)("Corak")
            txtColor.Text = DS.Tables("BARCODE").Rows(0)("Warna")
            txtBatch.Text = DS.Tables("BARCODE").Rows(0)("Partai")
            txtWidth.Text = DS.Tables("BARCODE").Rows(0)("Lebar")
            txtGrade.Text = DS.Tables("BARCODE").Rows(0)("Grade")
            txtSO.Text = DS.Tables("BARCODE").Rows(0)("NoSO")
            txtQty.Text = DS.Tables("BARCODE").Rows(0)("Yard")
            cboUnit.Text = "YD"
            txtRemark.Text = DS.Tables("BARCODE").Rows(0)("Keterangan")
            txtProduct.Focus()


        End If




    End Sub

    Private Sub txtProduct_GotFocus(sender As Object, e As EventArgs) Handles txtProduct.GotFocus
        txtProduct.BackColor = Color.FromArgb(254, 240, 158)
    End Sub

    Private Sub txtProduct_KeyDown(sender As Object, e As KeyEventArgs) Handles txtProduct.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtColor.Select()
        End If
    End Sub

    Private Sub txtProduct_LostFocus(sender As Object, e As EventArgs) Handles txtProduct.LostFocus

        txtProduct.BackColor = Color.White

        If Trim(txtProduct.Text) = "" Then
            txtProduct.Focus()
            Exit Sub
        End If

        '====================
        ' CHECK PRODUCT CODE
        '====================

        txtProduct.Text = Trim(txtProduct.Text)

Ulang:

        Query = "SELECT * FROM [MASPROD] WHERE [kdprod]='" & txtProduct.Text & "'"

        DS = New DataSet
        DA = New SqlDataAdapter(Query, Conn)
        DA.Fill(DS, "PRODUCT")

        If DS.Tables("PRODUCT").Rows.Count <> 1 Then

            'MessageBox.Show("Product No [" & Trim(txtProduct.Text) & "] does not exist ! ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error)
            'txtProduct.Text = ""
            'txtProduct.Focus()
            'Exit Sub

            With FrmFindItem
                .txtFilter.Text = txtProduct.Text
                .ShowDialog()
            End With

            If CARI = "" Then
                txtProduct.Text = ""
                txtProduct.Focus()
                Exit Sub
            Else
                txtProduct.Text = CARI
                GoTo Ulang
            End If

        End If

        txtProduct.Text = DS.Tables("PRODUCT").Rows(0)("KDPROD")
        txtColor.Text = DS.Tables("PRODUCT").Rows(0)("NMWARNA")
        txtGrade.Text = IIf(InStr(txtProduct.Text, ".A.") > 0, "A", IIf(InStr(txtProduct.Text, ".B.") > 0, "B", IIf(InStr(txtProduct.Text, ".S.") > 0, "S", "X")))




    End Sub

    Private Sub txtProduct_TextChanged(sender As Object, e As EventArgs) Handles txtProduct.TextChanged

    End Sub

    Private Sub txtColor_GotFocus(sender As Object, e As EventArgs) Handles txtColor.GotFocus
        txtColor.BackColor = Color.FromArgb(254, 240, 158)
    End Sub

    Private Sub txtColor_KeyDown(sender As Object, e As KeyEventArgs) Handles txtColor.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtBatch.Select()
        End If
    End Sub

    Private Sub txtColor_LostFocus(sender As Object, e As EventArgs) Handles txtColor.LostFocus
        txtColor.BackColor = Color.White
    End Sub

    Private Sub txtColor_TextChanged(sender As Object, e As EventArgs) Handles txtColor.TextChanged

    End Sub

    Private Sub txtBatch_GotFocus(sender As Object, e As EventArgs) Handles txtBatch.GotFocus
        txtBatch.BackColor = Color.FromArgb(254, 240, 158)
    End Sub

    Private Sub txtBatch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtBatch.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtWidth.Select()
        End If
    End Sub

    Private Sub txtBatch_LostFocus(sender As Object, e As EventArgs) Handles txtBatch.LostFocus
        txtBatch.BackColor = Color.White
    End Sub

    Private Sub txtBatch_TextChanged(sender As Object, e As EventArgs) Handles txtBatch.TextChanged

    End Sub

    Private Sub txtWidth_GotFocus(sender As Object, e As EventArgs) Handles txtWidth.GotFocus
        txtWidth.BackColor = Color.FromArgb(254, 240, 158)
    End Sub

    Private Sub txtWidth_KeyDown(sender As Object, e As KeyEventArgs) Handles txtWidth.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtGrade.Select()
        End If
    End Sub

    Private Sub txtWidth_LostFocus(sender As Object, e As EventArgs) Handles txtWidth.LostFocus
        txtWidth.BackColor = Color.White
    End Sub

    Private Sub txtWidth_TextChanged(sender As Object, e As EventArgs) Handles txtWidth.TextChanged

    End Sub

    Private Sub txtGrade_GotFocus(sender As Object, e As EventArgs) Handles txtGrade.GotFocus
        txtGrade.BackColor = Color.FromArgb(254, 240, 158)
    End Sub

    Private Sub txtGrade_KeyDown(sender As Object, e As KeyEventArgs) Handles txtGrade.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtSO.Select()
        End If
    End Sub

    Private Sub txtGrade_LostFocus(sender As Object, e As EventArgs) Handles txtGrade.LostFocus
        txtGrade.BackColor = Color.White
    End Sub

    Private Sub txtGrade_TextChanged(sender As Object, e As EventArgs) Handles txtGrade.TextChanged

    End Sub

    Private Sub txtSO_GotFocus(sender As Object, e As EventArgs) Handles txtSO.GotFocus
        txtSO.BackColor = Color.FromArgb(254, 240, 158)
    End Sub

    Private Sub txtSO_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSO.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtQty.Select()
        End If
    End Sub

    Private Sub txtSO_LostFocus(sender As Object, e As EventArgs) Handles txtSO.LostFocus
        txtSO.BackColor = Color.White
    End Sub

    Private Sub txtSO_TextChanged(sender As Object, e As EventArgs) Handles txtSO.TextChanged

    End Sub

    Private Sub txtQty_GotFocus(sender As Object, e As EventArgs) Handles txtQty.GotFocus
        txtQty.BackColor = Color.FromArgb(254, 240, 158)
    End Sub

    Private Sub txtQty_KeyDown(sender As Object, e As KeyEventArgs) Handles txtQty.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtRemark.Focus()
        End If
    End Sub

    Private Sub txtQty_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtQty.KeyPress
        If InStr("1234567890,.", (e.KeyChar)) = 0 AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtQty_LostFocus(sender As Object, e As EventArgs) Handles txtQty.LostFocus
        txtQty.BackColor = Color.White
    End Sub

    Private Sub cboUnit_KeyDown(sender As Object, e As KeyEventArgs) Handles cboUnit.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtRemark.Select()
        End If
    End Sub


    Private Sub cboUnit_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboUnit.SelectedIndexChanged

    End Sub

    Private Sub txtRemark_GotFocus(sender As Object, e As EventArgs) Handles txtRemark.GotFocus
        txtRemark.BackColor = Color.FromArgb(254, 240, 158)
    End Sub

    Private Sub txtRemark_KeyDown(sender As Object, e As KeyEventArgs) Handles txtRemark.KeyDown
        If e.KeyCode = Keys.Enter Then
            btnAdd.Select()
        End If
    End Sub

    Private Sub txtRemark_LostFocus(sender As Object, e As EventArgs) Handles txtRemark.LostFocus
        txtRemark.BackColor = Color.White
    End Sub

    Private Sub txtRemark_TextChanged(sender As Object, e As EventArgs) Handles txtRemark.TextChanged

    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click

        If Trim(txtBarcode.Text) = "" Then
            MessageBox.Show("Please enter [Barcode No] ! ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtBarcode.Select()
            Exit Sub
        End If

        If Trim(txtProduct.Text) = "" Then
            MessageBox.Show("Please enter [Product No] ! ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtProduct.Select()
            Exit Sub
        End If

        If Trim(txtColor.Text) = "" Then
            MessageBox.Show("Please enter [Color No] ! ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtColor.Select()
            Exit Sub
        End If

        If Trim(txtGrade.Text) = "" Then
            MessageBox.Show("Please enter [Grade] ! ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtGrade.Select()
            Exit Sub
        End If

        If Val(Replace(txtQty.Text, ",", ".")) = 0 Then
            MessageBox.Show("Please enter [Qty] ! ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtQty.Select()
            Exit Sub
        End If

        If cboUnit.Text = "YD" Then
            YDS = Val(Replace(txtQty.Text, ",", "."))
            MTR = Math.Round(YDS / 1.0936, 2)
        Else
            MTR = Val(txtQty.Text)
            YDS = Math.Round(MTR * 1.0936, 2)
        End If


        For i = 0 To DGV.Rows.Count - 1
            If DGV.Rows(i).Cells(0).Value = txtBarcode.Text Then
                MessageBox.Show("Barcode No [" & txtBarcode.Text & "] already exist ! ", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtBarcode.Focus()
                Exit Sub
            End If
        Next


        Call LOCAL_CONNECT()


        Query = "INSERT INTO [" & TEMPFILE & "] (barcode,corak,warna,grade,partai,yard,meter,lebar,unit,SO,ket,sloc,X) " & _
                "VALUES ('" & txtBarcode.Text & "','" & txtProduct.Text & "','" & txtColor.Text & "','" & txtGrade.Text & "'," & _
                        "'" & txtBatch.Text & "'," & Replace(YDS, ",", ".") & "," & Replace(MTR, ",", ".") & ",'" & txtWidth.Text & "'," & _
                        "'YD','" & txtSO.Text & "','" & txtRemark.Text & "','" & PLANT & "',0)"

        CMD = New SqlCommand
        CMD.Connection = Conn
        CMD.CommandText = Query
        CMD.ExecuteNonQuery()

        Call Load_Data()

        txtBarcode.Focus()

        'Call ClearObject()

    

    End Sub

    Private Sub ClearObject()
        txtBarcode.Text = ""
        txtProduct.Text = ""
        txtColor.Text = ""
        txtGrade.Text = ""
        txtBatch.Text = ""
        txtSO.Text = ""
        txtWidth.Text = ""
        txtRemark.Text = ""
        txtQty.Text = ""
        cboUnit.Text = "YD"
        MODE = "ADD"
        txtBarcode.Focus()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        If DGV.RowCount = 0 Then
            Exit Sub
        End If

        If MessageBox.Show("Save new barcode ? ", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = vbNo Then
            Exit Sub
        End If

        Call LOCAL_CONNECT()

        Query = "INSERT INTO [STOCKBCD] SELECT * FROM [" & TEMPFILE & "]"

        CMD = New SqlCommand
        CMD.Connection = Conn
        CMD.CommandText = Query
        CMD.ExecuteNonQuery()

        Me.Dispose()


    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Dispose()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click

        Dim BarcodeNo As String = Trim(DGV.CurrentRow.Cells(0).Value)

        If BarcodeNo = "" Then Exit Sub

        If MessageBox.Show("Delete Barcode No [" & BarcodeNo & "]   ?  ", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = vbNo Then
            Exit Sub
        End If


        Call LOCAL_CONNECT()

        Query = "DELETE FROM [" & TEMPFILE & "] WHERE [Barcode]='" & BarcodeNo & "'"

        CMD = New SqlCommand
        CMD.Connection = Conn
        CMD.CommandText = Query
        CMD.ExecuteNonQuery()

        Call Load_Data()



    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click

        If btnEdit.Text = "Edit" Then

            If DGV.RowCount = 0 Then Exit Sub

            MODE = "EDIT"

            txtBarcode.Text = DGV.CurrentRow.Cells(0).Value
            txtProduct.Text = DGV.CurrentRow.Cells(1).Value
            txtGrade.Text = DGV.CurrentRow.Cells(2).Value
            txtColor.Text = DGV.CurrentRow.Cells(3).Value
            txtSO.Text = DGV.CurrentRow.Cells(4).Value
            txtBatch.Text = DGV.CurrentRow.Cells(5).Value
            txtWidth.Text = DGV.CurrentRow.Cells(6).Value
            txtQty.Text = DGV.CurrentRow.Cells(7).Value
            cboUnit.Text = "YD"
            txtRemark.Text = DGV.CurrentRow.Cells(9).Value

            txtBarcode.Enabled = False

            btnAdd.Enabled = False
            btnEdit.Text = "Save"
            btnDelete.Text = "Cancel"



        ElseIf btnEdit.Text = "Save" Then

            If cboUnit.Text = "YD" Then
                YDS = Val(Replace(txtQty.Text, ",", "."))
                MTR = Math.Round(YDS / 1.0936, 2)
            Else
                MTR = Val(txtQty.Text)
                YDS = Math.Round(MTR * 1.0936, 2)
            End If

            Call LOCAL_CONNECT()

            Query = "UPDATE [" & TEMPFILE & "] " & _
                       "SET [Corak]='" & txtProduct.Text & "', [Warna]='" & txtColor.Text & "', [Grade]='" & txtGrade.Text & "'," & _
                          " [Partai]='" & txtBatch.Text & "', [SO]='" & txtSO.Text & "', [WIdth]='" & txtWidth.Text & "'," & _
                          " [Ket]='" & txtRemark.Text & "', [Yard]=" & Replace(YDS, ",", ".") & ", [Meter]=" & Replace(MTR, ",", ".") & " " & _
                     "WHERE [Barcode]='" & txtBarcode.Text & "'"


            CMD = New SqlCommand
            CMD.Connection = Conn
            CMD.CommandText = Query
            CMD.ExecuteNonQuery()

            btnAdd.Enabled = True
            btnEdit.Text = "Edit"
            btnDelete.Text = "Delete"

            Call Load_Data()


        End If




    End Sub
End Class