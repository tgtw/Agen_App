Imports System.Data.SqlClient

Public Class FrmFindItem

    Private Sub FrmFindItem_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Call btnCancel_Click(sender, e)
        End If
    End Sub

    Private Sub FrmFindItem_Load(sender As Object, e As EventArgs) Handles MyBase.Load

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

        cboFilter.SelectedIndex = 0

        Query = "SELECT   kdprod,nmprod,nmwarna,unit,hbeli FROM [MASPROD] " & _
                "WHERE    [" & IIf(cboFilter.SelectedIndex = 0, "kdprod", "nmprod") & "]  LIKE '%" & txtFilter.Text & "%' " & _
                "ORDER BY [" & IIf(cboFilter.SelectedIndex = 0, "kdprod", "nmprod") & "]"

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

                .Columns(0).Width = 140
                .Columns(1).Width = 300
                .Columns(2).Width = 80
                .Columns(3).Width = 60
                .Columns(4).Width = 100

                .Columns(0).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
                .Columns(1).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
                .Columns(2).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
                .Columns(3).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
                .Columns(4).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight


                .Columns(0).HeaderText = "PRODUCT #"
                .Columns(1).HeaderText = "DESCRIPTION"
                .Columns(2).HeaderText = "COLOR"
                .Columns(3).HeaderText = "UNIT"
                .Columns(4).HeaderText = "NET PRICE"

                .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
                .ColumnHeadersHeight = 30

                .ColumnHeadersDefaultCellStyle.Font = New Font("Verdana", 9)

                .DefaultCellStyle.Font = New Font("Verdana", 9)

                .Columns(4).DefaultCellStyle.Format = "N0"

                .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

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

        Query = "SELECT   kdprod,nmprod,nmwarna,unit,hbeli FROM [MASPROD] " & _
                "WHERE    [" & IIf(cboFilter.SelectedIndex = 0, "kdprod", "nmprod") & "]  LIKE '%" & txtFilter.Text & "%' " & _
                "ORDER BY [" & IIf(cboFilter.SelectedIndex = 0, "kdprod", "nmprod") & "]"

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