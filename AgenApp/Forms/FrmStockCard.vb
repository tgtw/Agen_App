Imports System.Data.SqlClient

Public Class FrmStockCard

    Private Sub FrmStockCard_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        MDIMain.ToolStrip.Visible = True
    End Sub

    Private Sub FrmStockCard_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub FrmStockCard_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Call Local_Connect()

        '=====================
        ' DataGridView Design
        '=====================

        DGV.BorderStyle = BorderStyle.Fixed3D
        DGV.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249)
        DGV.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
        DGV.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise
        DGV.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke
        DGV.BackgroundColor = Color.White

        DGV.EnableHeadersVisualStyles = False
        DGV.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        DGV.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72)
        DGV.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        DGV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        DGV.ColumnHeadersHeight = 30
        DGV.ColumnHeadersDefaultCellStyle.Font = New Font("Verdana", 9)
        DGV.DefaultCellStyle.Font = New Font("Verdana", 9)
        'DGV.RowHeadersVisible = False

        DGV.ColumnCount = 12

        DGV.Columns(0).HeaderText = "Posting Date"
        DGV.Columns(1).HeaderText = "PO No"
        DGV.Columns(2).HeaderText = "SO No"
        DGV.Columns(3).HeaderText = "Barcode"
        DGV.Columns(4).HeaderText = "Design"
        DGV.Columns(5).HeaderText = "Color"
        DGV.Columns(6).HeaderText = "Grade"
        DGV.Columns(7).HeaderText = "Batch"
        DGV.Columns(8).HeaderText = "Yard"
        DGV.Columns(9).HeaderText = "Meter"
        DGV.Columns(10).HeaderText = "Unit"
        DGV.Columns(11).HeaderText = "SLoc"

        DGV.Columns(0).Width = 120
        DGV.Columns(1).Width = 100
        DGV.Columns(2).Width = 100
        DGV.Columns(3).Width = 200
        DGV.Columns(4).Width = 250
        DGV.Columns(5).Width = 100
        DGV.Columns(6).Width = 60
        DGV.Columns(7).Width = 80
        DGV.Columns(8).Width = 80
        DGV.Columns(9).Width = 80
        DGV.Columns(10).Width = 70
        DGV.Columns(11).Width = 70

        DGV.Columns(4).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

        DGV.Columns(8).DefaultCellStyle.Format = "N2"
        DGV.Columns(8).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
        DGV.Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        DGV.Columns(9).DefaultCellStyle.Format = "N2"
        DGV.Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DGV.Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        'Call AutoNumberRowsForGridView(DGV)

        cboFilter.SelectedIndex = 0


    End Sub

    Private Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click

        If Trim(txtFilter.Text) = "" Then
            MsgBox(" Please fill filter text !  ", MsgBoxStyle.Critical, "Warning")
            txtFilter.Select()
            Exit Sub
        End If


        Dim i As Long = 0
        Dim Satuan As String = ""


        Query = "SELECT * FROM [STOCKCARD] WHERE "

        Select Case Trim(UCase(cboFilter.Text))
            Case Is = "BARCODE NO"
                Query = Query & "[barcode] LIKE '%" & Trim(txtFilter.Text) & "%' "
            Case Is = "PRODUCT NO"
                Query = Query & "[corak] LIKE '%" & Trim(txtFilter.Text) & "%' "
            Case Is = "PO NO"
                Query = Query & "[po] LIKE '%" & Trim(txtFilter.Text) & "%' "
            Case Is = "SO NO"
                Query = Query & "[so] LIKE '%" & Trim(txtFilter.Text) & "%' "
        End Select

        Query = Query & "ORDER BY [corak], [postdate], [barcode] "

        DS = New DataSet
        DA = New SqlDataAdapter(Query, Conn)
        DA.Fill(DS, "MUTASI")


        DGV.Rows.Clear()

        For n = 1 To DS.Tables("MUTASI").Rows.Count

            i = DGV.Rows.Add()

            Satuan = Trim(DS.Tables("MUTASI").Rows(n - 1)("unit"))

            If Satuan = "" Or Satuan = "YARD" Then
                Satuan = "YD"
            Else
                Satuan = "M"
            End If

            DGV.Rows(i).Cells(0).Value = Format(DS.Tables("MUTASI").Rows(n - 1)("postdate"), "dd.MM.yyyy")
            DGV.Rows(i).Cells(1).Value = DS.Tables("MUTASI").Rows(n - 1)("PO")
            DGV.Rows(i).Cells(2).Value = DS.Tables("MUTASI").Rows(n - 1)("SO")
            DGV.Rows(i).Cells(3).Value = DS.Tables("MUTASI").Rows(n - 1)("barcode")
            DGV.Rows(i).Cells(4).Value = DS.Tables("MUTASI").Rows(n - 1)("corak")
            DGV.Rows(i).Cells(5).Value = DS.Tables("MUTASI").Rows(n - 1)("warna")
            DGV.Rows(i).Cells(6).Value = DS.Tables("MUTASI").Rows(n - 1)("grade")
            DGV.Rows(i).Cells(7).Value = DS.Tables("MUTASI").Rows(n - 1)("partai")
            DGV.Rows(i).Cells(8).Value = DS.Tables("MUTASI").Rows(n - 1)("yard")
            DGV.Rows(i).Cells(9).Value = DS.Tables("MUTASI").Rows(n - 1)("meter")
            DGV.Rows(i).Cells(10).Value = Satuan
            DGV.Rows(i).Cells(11).Value = DS.Tables("MUTASI").Rows(n - 1)("sloc")

        Next



    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class