Imports System.Data.SqlClient

Public Class FrmGRNewBarcode

    Public TMPGRBCD As String = ""

    Private Sub Load_Data()

        DGV.Rows.Clear()

        Try

            Query = "SELECT barcode,corak,warna,grade,lebar,yard,meter,ket FROM [" & TMPGRBCD & "] "

            DS = New DataSet
            DA = New SqlDataAdapter(Query, Conn)
            DA.Fill(DS, "TEMPRECV")

            Dim n As Integer = 0

            For n = 0 To DS.Tables("TEMPRECV").Rows.Count - 1

                DGV.Rows.Add()
                DGV.Rows(n).Cells(0).Value = DS.Tables("TEMPRECV").Rows(n)("barcode")
                DGV.Rows(n).Cells(1).Value = DS.Tables("TEMPRECV").Rows(n)("corak")
                DGV.Rows(n).Cells(2).Value = DS.Tables("TEMPRECV").Rows(n)("warna")
                DGV.Rows(n).Cells(3).Value = DS.Tables("TEMPRECV").Rows(n)("grade")
                DGV.Rows(n).Cells(4).Value = DS.Tables("TEMPRECV").Rows(n)("yard")
                DGV.Rows(n).Cells(5).Value = DS.Tables("TEMPRECV").Rows(n)("meter")
                DGV.Rows(n).Cells(6).Value = DS.Tables("TEMPRECV").Rows(n)("lebar")
                DGV.Rows(n).Cells(7).Value = DS.Tables("TEMPRECV").Rows(n)("ket")

            Next

            Call AutoNumberRowsForGridView(DGV)


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        txtProduct.Focus()


    End Sub

    Private Sub FrmGRNewBarcode_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Call Local_Connect()

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
        'DGV.RowHeadersVisible = False

        For n = 0 To 7
            DGV.Columns(n).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        Next


        DGV.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DGV.Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        DGV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        DGV.ColumnHeadersHeight = 30


        txtProduct.Text = ""
        txtQty.Text = ""
        txtRemark.Text = ""
        cboUnit.SelectedIndex = 0

        Call Load_Data()


    End Sub

    Private Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click

Ulang:

        Query = "SELECT * FROM [MASPROD] WHERE [kdprod]='" & txtProduct.Text & "'"

        DS = New DataSet
        DA = New SqlDataAdapter(Query, Conn)
        DA.Fill(DS, "PRODUK")

        If DS.Tables("PRODUK").Rows.Count <> 1 Then

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

        txtProduct.Text = DS.Tables("PRODUK").Rows(0)("KDPROD")
        lblColor.Text = DS.Tables("PRODUK").Rows(0)("NMWARNA")
        lblGrade.Text = IIf(InStr(txtProduct.Text, ".A.") > 0, "A", IIf(InStr(txtProduct.Text, ".B.") > 0, "B", IIf(InStr(txtProduct.Text, ".S.") > 0, "S", "X")))

        txtQty.Focus()



    End Sub

    Private Sub txtQty_GotFocus(sender As Object, e As EventArgs) Handles txtQty.GotFocus
        txtQty.BackColor = Color.FromArgb(254, 240, 158)
    End Sub

    Private Sub txtQty_KeyDown(sender As Object, e As KeyEventArgs) Handles txtQty.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtWidth.Focus()
        End If
    End Sub

    Private Sub txtQty_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtQty.KeyPress
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso InStr("1234567890,.", e.KeyChar) = 0 Then
            MessageBox.Show("Please enter numbers only !")
            e.Handled = True
        End If
    End Sub

    Private Sub txtQty_LostFocus(sender As Object, e As EventArgs) Handles txtQty.LostFocus
        txtQty.BackColor = Color.White
    End Sub

    Private Sub txtProduct_GotFocus(sender As Object, e As EventArgs) Handles txtProduct.GotFocus
        txtProduct.BackColor = Color.FromArgb(254, 240, 158)
    End Sub

    Private Sub txtProduct_KeyDown(sender As Object, e As KeyEventArgs) Handles txtProduct.KeyDown
        If e.KeyCode = Keys.Enter Then
            Call btnFind_Click(txtProduct.Text, e)
        End If
    End Sub

    Private Sub txtProduct_LostFocus(sender As Object, e As EventArgs) Handles txtProduct.LostFocus
        txtProduct.BackColor = Color.White
    End Sub

    Private Sub txtRemark_GotFocus(sender As Object, e As EventArgs) Handles txtRemark.GotFocus
        txtRemark.BackColor = Color.FromArgb(254, 240, 158)
    End Sub

    Private Sub txtRemark_KeyDown(sender As Object, e As KeyEventArgs) Handles txtRemark.KeyDown
        If e.KeyCode = Keys.Enter Then
            btnAdd.Focus()
        End If
    End Sub

    Private Sub txtRemark_LostFocus(sender As Object, e As EventArgs) Handles txtRemark.LostFocus
        txtRemark.BackColor = Color.White
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click

        Call Local_Connect()

        If txtProduct.Text = "" Then
            txtProduct.Focus()
            Exit Sub
        End If

        If Val(txtQty.Text) = 0 Then
            txtQty.Text = ""
            txtQty.Focus()
            Exit Sub
        End If


        Dim QtyYd As Decimal = 0
        Dim QtyMtr As Decimal = 0
        Dim n As Integer = 0


        If cboUnit.Text = "Yard" Then
            QtyYd = Val(Replace(txtQty.Text, ",", "."))
            QtyMtr = Math.Round(QtyYd / 1.0936, 2)
        Else
            QtyMtr = Val(Replace(txtQty.Text, ",", "."))
            QtyYd = Math.Round(QtyMtr * 1.0936, 2)
        End If


        Query = "INSERT INTO [" & TMPGRBCD & "] (barcode,corak,warna,grade,lebar,yard,meter,ket,x) " & _
                    "VALUES ('" & Format(Now, "ddMMyy") & Mid(PLANT, 1, 3) & Format(Now, "hhmmss") & "'," & _
                            "'" & txtProduct.Text & "','" & lblColor.Text & "','" & lblGrade.Text & "','" & txtWidth.Text & "'," & _
                            " " & Replace(QtyYd, ",", ".") & "," & Replace(QtyMtr, ",", ".") & ",'" & txtRemark.Text & "',0)"

        CMD = New SqlCommand
        CMD.Connection = Conn
        CMD.CommandText = Query
        CMD.ExecuteNonQuery()


        'n = DGV.Rows.Add()
        'DGV.Rows(n).Cells(0).Value = Format(Now, "ddMMyy") & Mid(PLANT, 1, 3) & Format(Now, "hhmmss")
        'DGV.Rows(n).Cells(1).Value = txtProduct.Text
        'DGV.Rows(n).Cells(2).Value = lblColor.Text
        'DGV.Rows(n).Cells(3).Value = lblGrade.Text
        'DGV.Rows(n).Cells(4).Value = QtyYd
        'DGV.Rows(n).Cells(5).Value = QtyMtr
        'DGV.Rows(n).Cells(6).Value = txtWidth.Text
        'DGV.Rows(n).Cells(7).Value = txtRemark.Text

        txtProduct.Text = ""
        lblColor.Text = ""
        lblGrade.Text = ""
        txtQty.Text = ""
        txtWidth.Text = ""
        txtRemark.Text = ""

        txtProduct.Focus()

        Call Load_Data()


    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub txtProduct_TextChanged(sender As Object, e As EventArgs) Handles txtProduct.TextChanged

    End Sub

    Private Sub txtQty_TextChanged(sender As Object, e As EventArgs) Handles txtQty.TextChanged

    End Sub

    Private Sub txtWidth_GotFocus(sender As Object, e As EventArgs) Handles txtWidth.GotFocus
        txtWidth.BackColor = Color.FromArgb(254, 240, 158)
    End Sub

    Private Sub txtWidth_KeyDown(sender As Object, e As KeyEventArgs) Handles txtWidth.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtRemark.Focus()
        End If
    End Sub

    Private Sub txtWidth_LostFocus(sender As Object, e As EventArgs) Handles txtWidth.LostFocus
        txtWidth.BackColor = Color.White
    End Sub

    Private Sub txtWidth_TextChanged(sender As Object, e As EventArgs) Handles txtWidth.TextChanged

    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        If DGV.Rows.Count = 0 Then Exit Sub

        Me.Dispose()

    End Sub
End Class