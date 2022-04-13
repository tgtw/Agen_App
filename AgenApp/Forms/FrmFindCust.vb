Imports System.Data.SqlClient

Public Class FrmFindCust

    Private Sub FrmFindCust_Load(sender As Object, e As EventArgs) Handles MyBase.Load

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



        Query = "SELECT   flkode,flnama,flala1,flala2 FROM [MASCUST] " & _
                "WHERE    [" & IIf(cboFilter.SelectedIndex = 0, "flkode", "flnama") & "]  LIKE '%" & txtFilter.Text & "%' " & _
                "ORDER BY [" & IIf(cboFilter.SelectedIndex = 0, "flkode", "flnama") & "]"

        Load_Data(Query, dgv)

        cboFilter.SelectedIndex = 1     ' Nama Customer

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

                .Columns(0).Width = 110
                .Columns(1).Width = 200
                .Columns(2).Width = 280
                .Columns(3).Width = 100

                '.Columns(0).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                '.Columns(1).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                '.Columns(2).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                '.Columns(3).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

                .Columns(0).HeaderText = "CUST. CODE"
                .Columns(1).HeaderText = "CUSTOMER NAME "
                .Columns(2).HeaderText = "ADDREASS"
                .Columns(3).HeaderText = "CITY"

                .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
                .ColumnHeadersHeight = 30

                .ColumnHeadersDefaultCellStyle.Font = New Font("Verdana", 9)

                .DefaultCellStyle.Font = New Font("Verdana", 9)

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

        Query = "SELECT   flkode,flnama,flala1,flala2 FROM [MASCUST] " & _
                "WHERE    [" & IIf(cboFilter.SelectedIndex = 0, "flkode", "flnama") & "]  LIKE '%" & txtFilter.Text & "%' " & _
                "ORDER BY [" & IIf(cboFilter.SelectedIndex = 0, "flkode", "flnama") & "]"

        Load_Data(Query, dgv)

    End Sub

    Private Sub dgv_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv.CellContentClick

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