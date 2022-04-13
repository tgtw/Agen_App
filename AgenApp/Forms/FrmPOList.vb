Imports System.Data.SqlClient

Public Class FrmPOList

    Private Sub Load_Data()


        Query = "SELECT nopo,tglpo,nmsupl,nmsls,delstatus,tgldel,terms,tax,total FROM [TRANPOH] " & _
                 "WHERE [tglpo] BETWEEN '" & Format(FrmFilterPO.dtpDate1.Value, "yyyy-MM-dd") & "' AND " & _
                                       "'" & Format(FrmFilterPO.dtpDate2.Value, "yyyy-MM-dd") & "' "

        If FrmFilterPO.txtNo1.Text <> "" Then
            Query = Query & "AND [noPO]>='" & FrmFilterPO.txtNo1.Text & "' "
        End If

        If FrmFilterPO.txtNo2.Text <> "" Then
            Query = Query & "AND [noPO]<='" & FrmFilterPO.txtNo2.Text & "' "
        End If

        Query = Query & "ORDER BY [noPO] "


        Try
            CMD = New SqlCommand
            DA = New SqlDataAdapter
            DT = New DataTable

            CMD.Connection = Conn
            CMD.CommandText = Query

            DA.SelectCommand = CMD
            DA.Fill(DT)

            With DGV

                .DataSource = DT

                '.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
                '.ColumnHeadersHeight = 30

                '.ColumnHeadersDefaultCellStyle.Font = New Font("Verdana", 9)

                .Columns(0).HeaderText = "PO No"
                .Columns(1).HeaderText = "PO Date"
                .Columns(2).HeaderText = "Vendor"
                .Columns(3).HeaderText = "Sales"
                .Columns(4).HeaderText = "Delivery Status"
                .Columns(5).HeaderText = "Delivery Date"
                .Columns(6).HeaderText = "Terms"
                .Columns(7).HeaderText = "Tax"
                .Columns(8).HeaderText = "Total"

                .Columns(0).Width = 100
                .Columns(1).Width = 110
                .Columns(2).Width = 250
                .Columns(3).Width = 100
                .Columns(4).Width = 100
                .Columns(5).Width = 110
                .Columns(6).Width = 100
                .Columns(7).Width = 100
                .Columns(7).Width = 100

                .Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

                .Columns(8).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight

                .Columns(8).DefaultCellStyle.Format = "N0"

                .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                '.DefaultCellStyle.Font = New Font("Verdana", 9)


                'Call AutoNumberRowsForGridView(DGV)

            End With

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            'Conn.Close()
            'DA.Dispose()
        End Try

    End Sub

    Private Sub FrmPOList_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        MDIMain.ToolStrip.Visible = True
    End Sub

    Private Sub FrmPOList_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Call Local_Connect()

        If ConnectStatus = False Then Exit Sub

        Call GET_PERIOD()

        Dim FromDate As Date = DateValue(CurYearPeriode & "-" & CurMonPeriode & "-01")


        With FrmFilterPO
            .Show()
            .txtNo1.Text = ""
            .txtNo2.Text = ""
            .dtpDate1.Value = FromDate
            .dtpDate2.Value = Now   ' TglAkhirBulan(FromDate)
            .txtVendor.Text = ""
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
        'DGV.RowHeadersVisible = False



        Call Load_Data()


    End Sub

    Private Sub tsbExit_Click(sender As Object, e As EventArgs) Handles tsbExit.Click
        FrmFilterPO.Close()
        Me.Close()
    End Sub

    Private Sub tsbNew_Click(sender As Object, e As EventArgs) Handles tsbNew.Click

        With FrmPO
            .PO_NO = ""
            .ShowDialog()
        End With

        If SaveStatus = True Then
            Load_Data()
        End If

    End Sub

    Private Sub tsbEdit_Click(sender As Object, e As EventArgs) Handles tsbEdit.Click

        With FrmPO
            .PO_NO = DGV.CurrentRow.Cells(0).Value
            .EditOrDisplay = Trim(tsbEdit.Text)
            .ShowDialog()
        End With

        If SaveStatus = True Then
            Load_Data()
        End If

    End Sub
 
    Private Sub tsbFilter_Click(sender As Object, e As EventArgs) Handles tsbFilter.Click
        FrmFilterPO.ShowDialog()
        If FilterOkay = True Then Call Load_Data()
    End Sub


    Private Sub tsbRefresh_Click(sender As Object, e As EventArgs) Handles tsbRefresh.Click
        Call Load_Data()
    End Sub

    Private Sub tsbPrintPO_Click(sender As Object, e As EventArgs) Handles tsbPrintPO.Click

        Dim SLOCINFO As String = ""

        Query = "SELECT TRANPOH.NOPO, TRANPOH.TGLPO, TRANPOH.AGEN, TRANPOH.KDSUPL, TRANPOH.NMSUPL, TRANPOH.KATEGORI, TRANPOH.NMSLS," & _
                       "TRANPOH.DELSTATUS, TRANPOH.TGLDEL, TRANPOH.BENTUK, TRANPOH.PANJANG, TRANPOH.POINT, TRANPOH.KIRIMKE, TRANPOH.PEMBELI," & _
                       "TRANPOH.CAPPINGGIR, TRANPOH.SELVEDGE, TRANPOH.FACE, TRANPOH.MEREK, TRANPOH.ALBUM, TRANPOH.M1020, TRANPOH.KAIN," & _
                       "TRANPOH.LEMBARAN, TRANPOH.HANGER, TRANPOH.CATATAN, TRANPOH.TOTAL, TRANPOH.TERMS, TRANPOH.NOURUT, TRANPOH.SULAM," & _
                       "TRANPOH.TAX, TRANPOD.ITEMNO, TRANPOD.KDPROD, TRANPOD.NMPROD, TRANPOD.NMWARNA, TRANPOD.LOT, TRANPOD.QTY," & _
                       "TRANPOD.UNIT, TRANPOD.PRICE " & _
                "FROM   TRANPOH INNER JOIN TRANPOD ON TRANPOH.NOPO = TRANPOD.NOPO " & _
                "WHERE  TRANPOH.NOPO ='" & DGV.CurrentRow.Cells(0).Value.ToString & "' " & _
                "AND    TRANPOD.KDPROD LIKE '%.A.%'"

        DS = New DataSet
        DA = New SqlDataAdapter(Query, Conn)
        DA.Fill(DS, "PO")

        SLOCINFO = DS.Tables("PO").Rows(0)("AGEN")

        Dim PrintControl As New ClassPrintReport

        PrintControl.CetakReport("RptPO.rdlc", "DataSet6", Query, SLOCINFO, 0)

    End Sub

    Private Sub tsbDisplay_Click(sender As Object, e As EventArgs) Handles tsbDisplay.Click
        With FrmPO
            .PO_NO = DGV.CurrentRow.Cells(0).Value
            .EditOrDisplay = Trim(tsbDisplay.Text)
            .ShowDialog()
        End With
    End Sub
End Class