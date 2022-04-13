Imports System.Data.SqlClient

Public Class FrmFindPO

    Private Sub FrmFindPO_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Call btnCancel_Click(sender, e)
        End If
    End Sub

    Private Sub FrmFindPO_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Call Local_Connect()

        '=====================
        ' DataGridView Design
        '=====================

        dgv.BorderStyle = BorderStyle.Fixed3D
        dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249)
        dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
        dgv.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise
        dgv.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke
        dgv.BackgroundColor = Color.White

        dgv.EnableHeadersVisualStyles = False
        dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72)
        dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        'dgv.RowHeadersVisible = False



        Query = "SELECT   nopo,VAL(itemno),kdprod,nmprod,nmwarna,price,qty,received,(qty-received) AS remaining FROM [TRANPOD] " & _
                "WHERE    (qty-received) > 0 " & _
                  "AND    [" & IIf(cboFilter.SelectedIndex = 0, "nopo", "kdprod") & "]  LIKE '%" & txtFilter.Text & "%' " & _
                "ORDER BY [" & IIf(cboFilter.SelectedIndex = 0, "nopo", "kdprod") & "]"

        Load_Data(Query, dgv)

        cboFilter.SelectedIndex = 0

        txtFilter.Select()

    End Sub


    Private Sub Load_Data(sql As String, dtg As DataGridView)

        Try
            CMD = New SqlCommand
            DA = New SqlDataAdapter
            DT = New DataTable

            CMD.Connection = Conn
            CMD.CommandText = sql

            DA.SelectCommand = CMD
            DA.Fill(DT)

            With dtg

                .DataSource = DT

                .Columns(0).Width = 100
                .Columns(1).Width = 50
                .Columns(2).Width = 140
                .Columns(3).Width = 250
                .Columns(4).Width = 90
                .Columns(5).Width = 90
                .Columns(6).Width = 90
                .Columns(7).Width = 90
                .Columns(8).Width = 90

                .Columns(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

                .Columns(0).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
                .Columns(1).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
                .Columns(2).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
                .Columns(3).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
                .Columns(4).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
                .Columns(5).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(6).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(7).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(8).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight

                .Columns(0).HeaderText = "PO #"
                .Columns(1).HeaderText = "NO"
                .Columns(2).HeaderText = "PRODUCT #"
                .Columns(3).HeaderText = "DESCRIPTION"
                .Columns(4).HeaderText = "COLOR"
                .Columns(5).HeaderText = "PRICE"
                .Columns(6).HeaderText = "ORDER QTY"
                .Columns(7).HeaderText = "RECEIVED QTY"
                .Columns(8).HeaderText = "REMAINING QTY"


                .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
                .ColumnHeadersHeight = 50

                .ColumnHeadersDefaultCellStyle.Font = New Font("Verdana", 9)

                .DefaultCellStyle.Font = New Font("Verdana", 9)

                .Columns(5).DefaultCellStyle.Format = "N2"
                .Columns(6).DefaultCellStyle.Format = "N2"
                .Columns(7).DefaultCellStyle.Format = "N2"
                .Columns(8).DefaultCellStyle.Format = "N2"

                .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            End With

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            Conn.Close()
            DA.Dispose()
        End Try

    End Sub


    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        CARI = ""
        Me.Dispose()
    End Sub

    Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click
        CARI = dgv.CurrentRow.Cells(0).Value.ToString
        Me.Dispose()
    End Sub

    Private Sub txtFilter_GotFocus(sender As Object, e As EventArgs) Handles txtFilter.GotFocus
        txtFilter.BackColor = Color.FromArgb(254, 240, 158)
    End Sub

    Private Sub txtFilter_KeyDown(sender As Object, e As KeyEventArgs) Handles txtFilter.KeyDown
        If e.KeyCode = Keys.Down Then
            dgv.Select()
        End If
    End Sub

    Private Sub txtFilter_LostFocus(sender As Object, e As EventArgs) Handles txtFilter.LostFocus
        txtFilter.BackColor = Color.White
    End Sub

    Private Sub txtFilter_TextChanged(sender As Object, e As EventArgs) Handles txtFilter.TextChanged

        Query = "SELECT   nopo,itemno,kdprod,nmprod,nmwarna,price,qty,received,(qty-received) AS remaining FROM [TRANPOD] " & _
                "WHERE    (qty-received) > 0 " & _
                  "AND    [" & IIf(cboFilter.SelectedIndex = 0, "nopo", "kdprod") & "]  LIKE '%" & txtFilter.Text & "%' " & _
                "ORDER BY [" & IIf(cboFilter.SelectedIndex = 0, "nopo", "kdprod") & "]"

        Load_Data(Query, dgv)

    End Sub

    Private Sub dgv_DoubleClick(sender As Object, e As EventArgs) Handles dgv.DoubleClick
        Call btnSelect_Click(sender, e)
    End Sub

    Private Sub dgv_KeyDown(sender As Object, e As KeyEventArgs) Handles dgv.KeyDown
        If e.KeyCode = Keys.Enter Then
            Call btnSelect_Click(sender, e)
        End If
    End Sub
End Class