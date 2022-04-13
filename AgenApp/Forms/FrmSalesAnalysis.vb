Imports System.Data.SqlClient

Public Class FrmSalesAnalysis

    Dim FromDate As Date

    Private Sub FrmSalesAnalysis_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        MDIMain.ToolStrip.Visible = True
    End Sub

    Private Sub FrmSalesAnalysis_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub FrmSalesDetailList_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Call Local_Connect()

        If ConnectStatus = False Then Exit Sub

        Call GET_PERIOD()

        FromDate = DateValue(CurYearPeriode & "-" & CurMonPeriode & "-01")

        With FrmFilterSalesAnalysis
            .Show()
            .dtpDate1.Value = FromDate
            .dtpDate2.Value = Now
            .txtSoldTo.Text = ""
            .txtProduct.Text = ""
            .cboStatus.SelectedIndex = 0
            .Hide()
        End With


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

        DGV.ColumnCount = 13

        DGV.Columns(0).HeaderText = "Sales Date"
        DGV.Columns(1).HeaderText = "Sales No"
        DGV.Columns(2).HeaderText = "Delivery No"
        DGV.Columns(3).HeaderText = "Billing No"
        DGV.Columns(4).HeaderText = "Sold To"
        DGV.Columns(5).HeaderText = "Material"
        DGV.Columns(6).HeaderText = "         Roll"
        DGV.Columns(7).HeaderText = "     Quantity"
        DGV.Columns(8).HeaderText = "Unit"
        DGV.Columns(9).HeaderText = "    Price"
        DGV.Columns(10).HeaderText = "         DPP Value"
        DGV.Columns(11).HeaderText = "                Tax"
        DGV.Columns(12).HeaderText = "                 Total"

        DGV.Columns(0).Width = 90
        DGV.Columns(1).Width = 100
        DGV.Columns(2).Width = 100
        DGV.Columns(3).Width = 100
        DGV.Columns(4).Width = 260
        DGV.Columns(5).Width = 150
        DGV.Columns(6).Width = 70
        DGV.Columns(7).Width = 100
        DGV.Columns(8).Width = 50
        DGV.Columns(9).Width = 60
        DGV.Columns(10).Width = 110
        DGV.Columns(11).Width = 100
        DGV.Columns(12).Width = 110

        'DGV.Columns(4).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

        DGV.Columns(6).DefaultCellStyle.Format = "N0"
        DGV.Columns(6).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
        DGV.Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        DGV.Columns(7).DefaultCellStyle.Format = "N2"
        DGV.Columns(7).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
        DGV.Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        For n = 9 To 12
            DGV.Columns(n).DefaultCellStyle.Format = "N0"
            DGV.Columns(n).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DGV.Columns(n).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        Next

        Call LOAD_DATA()


    End Sub

    Private Sub LOAD_DATA()

        Dim i As Long = 0
        Dim Satuan As String = ""
        Dim DPP As Double = 0
        Dim PPN As Double = 0
        Dim TOT_PC As Long = 0
        Dim TOT_YD As Decimal = 0
        Dim TOT_DPP As Double = 0
        Dim TOT_PPN As Double = 0


        Query = "SELECT TRANSLSH.SO_DATE, TRANSLSH.SO_NO, TRANSLSH.DELV_NO, TRANSLSH.INV_NO, TRANSLSH.CUST_NAME, TRANSLSH.TAX," & _
                      " TRANSLSD.KDPROD, TRANSLSD.QTY1, TRANSLSD.QTY2, TRANSLSD.UNIT, TRANSLSD.PRICE " & _
                  "FROM TRANSLSH INNER JOIN TRANSLSD ON TRANSLSH.SO_NO = TRANSLSD.SO_NO " & _
                 "WHERE TRANSLSH.SO_DATE BETWEEN '" & Format(FrmFilterSalesAnalysis.dtpDate1.Value, "yyyy-MM-dd") & "' AND " & _
                                                "'" & Format(FrmFilterSalesAnalysis.dtpDate2.Value, "yyyy-MM-dd") & "' " & _
                   "AND TRANSLSH.CUST_NAME LIKE '%" & FrmFilterSalesAnalysis.txtSoldTo.Text & "%' " & _
                   "AND TRANSLSD.KDPROD LIKE '%" & FrmFilterSalesAnalysis.txtProduct.Text & "%' AND TRANSLSD.QTY2 > 0 "

        If FrmFilterSalesAnalysis.cboStatus.SelectedIndex > 0 Then
            If FrmFilterSalesAnalysis.cboStatus.SelectedIndex = 1 Then
                Query = Query & "AND TRANSLSH.INV_NO IS NULL "
            Else
                Query = Query & "AND TRANSLSH.INV_NO IS NOT NULL AND TRANSLSH.INV_NO <> 'CANCEL' "
            End If
        End If

        Query = Query & "ORDER BY TRANSLSH.SO_DATE, TRANSLSH.SO_NO, CAST(TRANSLSD.ITEM_NO AS INT) "


        DS = New DataSet
        DA = New SqlDataAdapter(Query, Conn)
        DA.Fill(DS, "SALES")


        DGV.Rows.Clear()

        For n = 1 To DS.Tables("SALES").Rows.Count

            i = DGV.Rows.Add()

            Satuan = Trim(DS.Tables("SALES").Rows(n - 1)("UNIT"))

            If Satuan = "" Or Satuan = "YD" Or Satuan = "YARD" Then
                Satuan = "YD"
            Else
                Satuan = "M"
            End If


            TAXRATE = TAX_RATE(DS.Tables("SALES").Rows(n - 1)("SO_DATE"))

            DPP = DS.Tables("SALES").Rows(n - 1)("QTY2") * DS.Tables("SALES").Rows(n - 1)("PRICE")

            If DS.Tables("SALES").Rows(n - 1)("TAX") = "Included" Then
                DPP = Math.Round(DPP * 100 / (100 + TAXRATE), 0)
            End If

            PPN = Math.Round(DPP * TAXRATE / 100, 0)


            DGV.Rows(i).Cells(0).Value = Format(DS.Tables("SALES").Rows(n - 1)("SO_DATE"), "dd.MM.yyyy")
            DGV.Rows(i).Cells(1).Value = DS.Tables("SALES").Rows(n - 1)("SO_NO")
            DGV.Rows(i).Cells(2).Value = DS.Tables("SALES").Rows(n - 1)("DELV_NO")
            DGV.Rows(i).Cells(3).Value = DS.Tables("SALES").Rows(n - 1)("INV_NO")
            DGV.Rows(i).Cells(4).Value = DS.Tables("SALES").Rows(n - 1)("CUST_NAME")
            DGV.Rows(i).Cells(5).Value = DS.Tables("SALES").Rows(n - 1)("KDPROD")
            DGV.Rows(i).Cells(6).Value = DS.Tables("SALES").Rows(n - 1)("QTY1")
            DGV.Rows(i).Cells(7).Value = DS.Tables("SALES").Rows(n - 1)("QTY2")
            DGV.Rows(i).Cells(8).Value = Satuan
            DGV.Rows(i).Cells(9).Value = DS.Tables("SALES").Rows(n - 1)("PRICE")
            DGV.Rows(i).Cells(10).Value = DPP
            DGV.Rows(i).Cells(11).Value = PPN
            DGV.Rows(i).Cells(12).Value = DPP + PPN

            TOT_PC = TOT_PC + DS.Tables("SALES").Rows(n - 1)("QTY1")
            TOT_YD = TOT_YD + DS.Tables("SALES").Rows(n - 1)("QTY2")
            TOT_DPP = TOT_DPP + DPP
            TOT_PPN = TOT_PPN + PPN

        Next

        Call AutoNumberRowsForGridView(DGV)

        i = DGV.Rows.Add()

        DGV.Rows(i).Cells(6).Value = TOT_PC
        DGV.Rows(i).Cells(7).Value = TOT_YD
        DGV.Rows(i).Cells(10).Value = TOT_DPP
        DGV.Rows(i).Cells(11).Value = TOT_PPN
        DGV.Rows(i).Cells(12).Value = TOT_DPP + TOT_PPN

        DGV.Rows(i).DefaultCellStyle.BackColor = Color.Yellow




    End Sub

    Private Sub tsbExit_Click(sender As Object, e As EventArgs) Handles tsbExit.Click
        Me.Close()
    End Sub

    Private Sub tsbFilter_Click(sender As Object, e As EventArgs) Handles tsbFilter.Click
        FrmFilterSalesAnalysis.ShowDialog()
        If FilterOkay = True Then Call LOAD_DATA()
    End Sub

    Private Sub tsbRefresh_Click(sender As Object, e As EventArgs) Handles tsbRefresh.Click

        With FrmFilterSalesAnalysis
            .dtpDate1.Value = FromDate
            .dtpDate2.Value = Now
            .txtSoldTo.Text = ""
            .txtProduct.Text = ""
            .cboStatus.SelectedIndex = 0
        End With

        Call LOAD_DATA()

    End Sub
End Class