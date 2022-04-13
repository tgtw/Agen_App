Imports System.Data.SqlClient


Public Class FrmSales

    Private Sub FrmPenjualan_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Call LogToSQLServer()

        'Me.WindowState = FormWindowState.Maximized

        dgv.Columns(0).Width = 120
        dgv.Columns(1).Width = 170

        dgv.Columns(0).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgv.Columns(1).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgv.Columns(2).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgv.Columns(3).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgv.Columns(4).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgv.Columns(5).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

        dgv.Columns(0).HeaderText = "KODE PRODUK"
        dgv.Columns(1).HeaderText = "NAMA PRODUK"
        dgv.Columns(2).HeaderText = "SATUAN"

        dgv.Columns(3).DefaultCellStyle.Format = "##,0"
        dgv.Columns(4).DefaultCellStyle.Format = "##,0"
        dgv.Columns(5).DefaultCellStyle.Format = "##,0"

        dgv.Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgv.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgv.Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight


        Me.txtKdCust.Select()   ' Object Focus

    End Sub


    Private Sub dgv_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv.CellEndEdit

        If e.ColumnIndex = 0 Then   '// KODE BARANG

            Query = "SELECT kdprod,nmprod,warna,unit,hbeli,hjual FROM [MASPROD] " & _
                    "WHERE  [kdprod]='" & dgv.Rows(e.RowIndex).Cells(0).Value & "'"

            DS = New DataSet
            DA = New SqlDataAdapter(Query, Conn)
            DA.Fill(DS, "PRODUK")

            If DS.Tables("PRODUK").Rows.Count <> 1 Then

                MsgBox(" Kode Produk [ " & dgv.Rows(e.RowIndex).Cells(0).Value & " ] tidak ditemukan ! ", MsgBoxStyle.Critical, "Warning")
                SendKeys.Send("{up}")

                dgv.Rows.Clear()

                Exit Sub
            Else
                dgv.Rows(e.RowIndex).Cells(1).Value = UCase(DS.Tables("PRODUK").Rows(0)("nmprod"))
                dgv.Rows(e.RowIndex).Cells(2).Value = UCase(DS.Tables("PRODUK").Rows(0)("unit"))
                dgv.Rows(e.RowIndex).Cells(4).Value = UCase(DS.Tables("PRODUK").Rows(0)("hjual"))

                dgv.CurrentCell = dgv(3, e.RowIndex)    ' Fokus di kolom Quantity
                SendKeys.Send("{up}")

            End If



        ElseIf e.ColumnIndex = 3 Then   '// QUANTITY

            dgv.Rows(e.RowIndex).Cells(5).Value = dgv.Rows(e.RowIndex).Cells(3).Value * dgv.Rows(e.RowIndex).Cells(4).Value

            dgv.CurrentCell = dgv(4, e.RowIndex)    ' Fokus di kolom Harga
            SendKeys.Send("{up}")


        ElseIf e.ColumnIndex = 4 Then   '// HARGA

            dgv.Rows(e.RowIndex).Cells(5).Value = dgv.Rows(e.RowIndex).Cells(3).Value * dgv.Rows(e.RowIndex).Cells(4).Value

            dgv.CurrentCell = dgv(0, e.RowIndex)    ' Fokus di kolom Kode


        End If

    End Sub

    '//  Change cell to upper case
    Private Sub dgv_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles dgv.EditingControlShowing
        If TypeOf e.Control Is TextBox Then
            DirectCast(e.Control, TextBox).CharacterCasing = CharacterCasing.Upper
        End If
    End Sub



    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click

        Try

            For i As Integer = 0 To (dgv.Rows.Count - 2)

                'vkdbrg = dgv.Rows(i).Cells(0).Value
                'vnabar = dgv.Rows(i).Cells(1).Value
                'vsat = dgv.Rows(i).Cells(2).Value
                'vjumlah = dgv.Rows(i).Cells(3).Value
                'vharga = dgv.Rows(i).Cells(4).Value
                'vtot = dgv.Rows(i).Cells(5).Value






            Next

        Catch ex As Exception
            Beep()
            MsgBox(ex.Message, MsgBoxStyle.Information, "Informasi")
            CMD.Dispose()
        End Try
    End Sub

    Private Sub txtKdCust_GotFocus(sender As Object, e As EventArgs) Handles txtKdCust.GotFocus
        Me.txtKdCust.BackColor = Color.Yellow
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

        Query = "SELECT flkode,flnama,flcomp,flala1,flala2,flphon,flfaks FROM [MASCUST] " & _
                "WHERE  [flkode]='" & Me.txtKdCust.Text & "'"

        DS = New DataSet
        DA = New SqlDataAdapter(Query, Conn)
        DA.Fill(DS, "CUSTOMER")

        If DS.Tables("CUSTOMER").Rows.Count <> 1 Then
            'MsgBox("Kode Customer [" & txtKdCust.Text & "] tidak ditemukan ! ", MsgBoxStyle.Critical, "Warning")

            FrmFindCust.ShowDialog()

            txtKdCust.Select()
            txtKdCust.Text = CARI

            If CARI = "" Then
                Exit Sub
            End If

            GoTo Ulang

            Exit Sub
        End If

        lblNmCust.Text = DS.Tables("CUSTOMER").Rows(0)("flnama")
        lblAlmCust1.Text = DS.Tables("CUSTOMER").Rows(0)("flala1")
        lblAlmCust2.Text = DS.Tables("CUSTOMER").Rows(0)("flala2")


    End Sub


    Private Sub txtKdCust_TextChanged(sender As Object, e As EventArgs) Handles txtKdCust.TextChanged

    End Sub

    Private Sub dgv_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv.CellContentClick

    End Sub
End Class