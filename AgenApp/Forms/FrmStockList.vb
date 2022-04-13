Imports System.Data.SqlClient
Imports System.Drawing.Printing


Public Class FrmStockList

    Dim TEMPSTOCK As String = ""
    Dim NO As Integer = 0


    Private Sub EXPORT_EXCEL()

        Dim sfd As New SaveFileDialog

        sfd.Filter = "Microsoft Excel File (*.xls)|*.xls"

        If Me.DGVH.Rows.Count > 0 Then

            If sfd.ShowDialog = Windows.Forms.DialogResult.OK Then

                'Try

                Dim dt As New DataTable

                'For Each Col As DataGridViewColumn In Me.DGVH.Columns
                '    dt.Columns.Add(Col.HeaderText, Col.CellType)
                'Next

                dt.Columns.Add("Product No", GetType(String))
                dt.Columns.Add("Product Name", GetType(String))
                dt.Columns.Add("Color", GetType(String))
                dt.Columns.Add("Pack", GetType(Integer))
                dt.Columns.Add("Yard", GetType(Double))

                Dim Count As Integer = 0

                For Each Row As DataGridViewRow In Me.DGVH.Rows
                    If Count < Me.DGVH.Rows.Count - 1 Then
                        dt.Rows.Add()
                        For Each Cell As DataGridViewCell In Row.Cells
                            dt.Rows(dt.Rows.Count - 1)(Cell.ColumnIndex) = Cell.Value.ToString()
                        Next
                    End If
                    Count += 1
                Next

                Dim wr As New IO.StreamWriter(sfd.FileName)

                For i As Integer = 0 To dt.Columns.Count - 1
                    wr.Write(dt.Columns(i).ToString().ToUpper() & vbTab)
                Next

                wr.WriteLine()

                For i As Integer = 0 To (dt.Rows.Count) - 1
                    For j As Integer = 0 To dt.Columns.Count - 1
                        If dt.Rows(i)(j) IsNot Nothing Then
                            wr.Write(Convert.ToString(dt.Rows(i)(j)) & vbTab)
                        Else
                            wr.Write(vbTab)
                        End If
                    Next
                    wr.WriteLine()
                Next

                wr.Close()

                MsgBox("Data berhasil diexport ke excel!", MsgBoxStyle.Information, "Information")


                'Catch ex As Exception
                '    Throw ex
                'End Try

            End If

        End If


    End Sub

    Private Sub LOAD_HEADER_DATA()

        If TEMPSTOCK = "" Then Exit Sub


        Dim TOTPCS As Long = 0
        Dim TOTYDS As Double = 0
        Dim n As Long = 0
        Dim FLDNAME() As String = {"KDPROD", "NMPROD"}


        DGVH.Rows.Clear()
        DGVD.Rows.Clear()


        Query = "UPDATE [" & TEMPSTOCK & "] SET [stock1]=0, [stock2]=0 "

        CMD = New SqlCommand
        CMD.Connection = Conn
        CMD.CommandText = Query
        CMD.ExecuteNonQuery()




        If cboSLoc.SelectedIndex = 0 Then

            Query = "UPDATE [" & TEMPSTOCK & "] SET [stock1]=BCD.pack, [stock2]=BCD.yds " & _
                    "FROM   (SELECT corak,COUNT(barcode) AS pack,SUM(yard) AS yds FROM [STOCKBCD] "

            If cboFilter.Text = "BARCODE NO" Then
                Query = Query & "WHERE [barcode] LIKE '%" & Trim(txtFilter.Text) & "%' "

            ElseIf cboFilter.Text = "DELIVERY NO" Then
                Query = Query & "WHERE [sj] LIKE '%" & Trim(txtFilter.Text) & "%' "
            End If

            Query = Query & "GROUP BY corak) BCD WHERE  BCD.corak = " & TEMPSTOCK & ".kdprod "


        Else

            Query = "UPDATE [" & TEMPSTOCK & "] SET [stock1]=BCD.pack, [stock2]=BCD.yds " & _
                    "FROM   (SELECT corak,COUNT(barcode) AS pack,SUM(yard) AS yds FROM [STOCKBCD] " & _
                            "WHERE  [sloc]='" & cboSLoc.Text & "' "

            If cboFilter.Text = "BARCODE NO" Then
                Query = Query & "AND [barcode] LIKE '%" & Trim(txtFilter.Text) & "%' "

            ElseIf cboFilter.Text = "DELIVERY NO" Then
                Query = Query & "AND [sj] LIKE '%" & Trim(txtFilter.Text) & "%' "
            End If

            Query = Query & "GROUP BY corak) BCD WHERE  BCD.corak = " & TEMPSTOCK & ".kdprod "

        End If



        CMD = New SqlCommand
        CMD.Connection = Conn
        CMD.CommandText = Query
        CMD.ExecuteNonQuery()


        Try
            TOTPCS = 0 : TOTYDS = 0

            If cboFilter.SelectedIndex < 2 Then

                Query = "SELECT   kdprod,nmprod,nmwarna,stock1,stock2 FROM [" & TEMPSTOCK & "] " & _
                        "WHERE    [" & FLDNAME(cboFilter.SelectedIndex) & "] LIKE '%" & Trim(txtFilter.Text) & "%' " & _
                          "AND    [stock2] > 0 " & _
                        "ORDER BY [" & FLDNAME(cboFilter.SelectedIndex) & "] "
            Else
                Query = "SELECT   kdprod,nmprod,nmwarna,stock1,stock2 FROM [" & TEMPSTOCK & "] " & _
                        "WHERE    [stock2] > 0 " & _
                        "ORDER BY [kdprod] "


            End If


            DS = New DataSet
            DA = New SqlDataAdapter(Query, Conn)
            DA.Fill(DS, "STOCK")

            For n = 1 To DS.Tables("STOCK").Rows.Count

                DGVH.Rows.Add()
                DGVH.Rows(n - 1).Cells(0).Value = DS.Tables("STOCK").Rows(n - 1)("KDPROD")
                DGVH.Rows(n - 1).Cells(1).Value = DS.Tables("STOCK").Rows(n - 1)("NMPROD")
                DGVH.Rows(n - 1).Cells(2).Value = DS.Tables("STOCK").Rows(n - 1)("NMWARNA")
                DGVH.Rows(n - 1).Cells(3).Value = DS.Tables("STOCK").Rows(n - 1)("STOCK1")
                DGVH.Rows(n - 1).Cells(4).Value = DS.Tables("STOCK").Rows(n - 1)("STOCK2")

                TOTPCS = TOTPCS + DS.Tables("STOCK").Rows(n - 1)("STOCK1")
                TOTYDS = TOTYDS + DS.Tables("STOCK").Rows(n - 1)("STOCK2")

            Next

            If DGVH.Rows.Count > 0 Then

                Call AutoNumberRowsForGridView(DGVH)

                DGVH.Rows.Add()
                DGVH.Rows(n - 1).Cells(3).Value = TOTPCS
                DGVH.Rows(n - 1).Cells(4).Value = TOTYDS

                DGVH.Rows(n - 1).DefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72)
                DGVH.Rows(n - 1).DefaultCellStyle.ForeColor = Color.White

                Call LOAD_DETAIL_DATA()

            End If


        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            'Conn.Close()
            'DA.Dispose()
        End Try


    End Sub

    Private Sub LOAD_DETAIL_DATA()

        Dim TOTMTR As Double = 0
        Dim TOTYDS As Double = 0
        Dim n As Long = 0


        DGVD.Rows.Clear()

        Try
            If cboSLoc.SelectedIndex = 0 Then
                Query = "SELECT barcode, corak, partai, yard, meter FROM [STOCKBCD] " & _
                        "WHERE  [corak]='" & DGVH.CurrentRow.Cells(0).Value & "' "
            Else
                Query = "SELECT barcode, corak, partai, yard, meter FROM [STOCKBCD] " & _
                        "WHERE  [corak]='" & DGVH.CurrentRow.Cells(0).Value & "' AND [sloc]='" & cboSLoc.Text & "' "
            End If

            If cboFilter.Text = "BARCODE NO" Then
                Query = Query & "AND [barcode] LIKE '%" & Trim(txtFilter.Text) & "%' "

            ElseIf cboFilter.Text = "DELIVERY NO" Then
                Query = Query & "AND [sj] LIKE '%" & Trim(txtFilter.Text) & "%' "
            End If


            DS = New DataSet
            DA = New SqlDataAdapter(Query, Conn)
            DA.Fill(DS, "BARCODE")

            For n = 1 To DS.Tables("BARCODE").Rows.Count

                DGVD.Rows.Add()
                DGVD.Rows(n - 1).Cells(0).Value = DS.Tables("BARCODE").Rows(n - 1)("barcode")
                DGVD.Rows(n - 1).Cells(1).Value = DS.Tables("BARCODE").Rows(n - 1)("corak")
                DGVD.Rows(n - 1).Cells(2).Value = DS.Tables("BARCODE").Rows(n - 1)("partai")
                DGVD.Rows(n - 1).Cells(3).Value = DS.Tables("BARCODE").Rows(n - 1)("yard")
                DGVD.Rows(n - 1).Cells(4).Value = DS.Tables("BARCODE").Rows(n - 1)("meter")

                TOTYDS = TOTYDS + DS.Tables("BARCODE").Rows(n - 1)("yard")
                TOTMTR = TOTMTR + DS.Tables("BARCODE").Rows(n - 1)("meter")

            Next

            If DGVD.Rows.Count > 0 Then

                Call AutoNumberRowsForGridView(DGVD)

                DGVD.Rows.Add()
                DGVD.Rows(n - 1).Cells(3).Value = TOTYDS
                DGVD.Rows(n - 1).Cells(4).Value = TOTMTR

                DGVD.Rows(n - 1).DefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72)
                DGVD.Rows(n - 1).DefaultCellStyle.ForeColor = Color.White

            End If


        Catch ex As Exception
            MsgBox(ex.Message)
            'Finally
            '    Conn.Close()
            '    DA.Dispose()
        End Try


    End Sub

    Private Sub FrmStockList_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing

        Query = "DROP TABLE [" & TEMPSTOCK & "] "

        CMD = New SqlCommand
        CMD.Connection = Conn
        CMD.CommandText = Query
        CMD.ExecuteNonQuery()

        MDIMain.ToolStrip.Visible = True

    End Sub

    Private Sub FrmStockList_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Call Local_Connect()

        If ConnectStatus = False Then Exit Sub


        With cboSLoc
            .Items.Clear()
            .Items.Add("<ALL>")
            .Items.Add(PLANT)
            .Items.Add("TRIS")
            .SelectedIndex = 0
        End With

        '=====================
        ' DataGridView Design
        '=====================


        With DGVH
            .BorderStyle = BorderStyle.Fixed3D
            .AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249)
            .CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
            .DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise
            .DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke
            .BackgroundColor = Color.White

            .EnableHeadersVisualStyles = False
            .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
            .ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72)
            .ColumnHeadersDefaultCellStyle.ForeColor = Color.White
            .RowHeadersWidth = 80

            '.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72)
            '.RowHeadersDefaultCellStyle.ForeColor = Color.White

            .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
            .ColumnHeadersHeight = 30

            .Columns(0).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns(1).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns(2).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns(3).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(4).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns(3).DefaultCellStyle.Format = "N0"
            .Columns(4).DefaultCellStyle.Format = "N2"
        End With


        With DGVD
            .BorderStyle = BorderStyle.Fixed3D
            .AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249)
            .CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
            .DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise
            .DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke
            .BackgroundColor = Color.White

            .EnableHeadersVisualStyles = False
            .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
            .ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72)
            .ColumnHeadersDefaultCellStyle.ForeColor = Color.White
            .RowHeadersVisible = True


            .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
            .ColumnHeadersHeight = 30

            .Columns(0).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns(1).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns(2).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Columns(3).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(4).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            .Columns(3).DefaultCellStyle.Format = "N2"
            .Columns(4).DefaultCellStyle.Format = "N2"
        End With

        cboSLoc.SelectedIndex = 0
        cboFilter.SelectedIndex = 0


        '=====================
        '  CREATE TEMP TABLE
        '=====================
Ulang:
        NO = NO + 1
        TEMPSTOCK = "TMPST" & Format(NO, "00")

        Try
            Query = "SELECT * FROM [" & TEMPSTOCK & "] "

            DS = New DataSet
            DA = New SqlDataAdapter(Query, Conn)
            DA.Fill(DS, "TMPST")

            If DS.Tables("TMPST").Rows.Count > 0 Then
                GoTo Ulang
            End If

        Catch ex As Exception

            Query = "SELECT * INTO [" & TEMPSTOCK & "] FROM [MASPROD] "

            CMD = New SqlCommand
            CMD.Connection = Conn
            CMD.CommandText = Query
            CMD.ExecuteNonQuery()

        End Try


        Call LOAD_HEADER_DATA()


    End Sub

    Private Sub txtFilter_GotFocus(sender As Object, e As EventArgs) Handles txtFilter.GotFocus
        txtFilter.BackColor = Color.FromArgb(254, 240, 158)
    End Sub

    Private Sub txtFilter_KeyDown(sender As Object, e As KeyEventArgs) Handles txtFilter.KeyDown
        If e.KeyCode = Keys.Enter Then
            LOAD_HEADER_DATA()
        End If
    End Sub

    Private Sub txtFilter_LostFocus(sender As Object, e As EventArgs) Handles txtFilter.LostFocus
        txtFilter.BackColor = Color.White
    End Sub

    Private Sub tsbExit_Click(sender As Object, e As EventArgs) Handles tsbExit.Click

        Call Local_Connect()

        Query = "DELETE FROM [" & TEMPSTOCK & "] "

        CMD = New SqlCommand
        CMD.Connection = Conn
        CMD.CommandText = Query
        CMD.ExecuteNonQuery()

        Me.Close()

    End Sub

    Private Sub DGVH_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGVH.CellClick
        Call LOAD_DETAIL_DATA()
    End Sub


    Private Sub Btn_Export_Click(sender As Object, e As EventArgs)
        'If DGVH.RowCount > 0 Then
        '    DGVH.SelectAll()
        '    Clipboard.SetDataObject(DGVH.GetClipboardContent())
        'End If
    End Sub

    Private Sub tsbExport_Click(sender As Object, e As EventArgs) Handles tsbExport.Click
        Call EXPORT_EXCEL()
    End Sub

    Private Sub txtFilter_TextChanged(sender As Object, e As EventArgs) Handles txtFilter.TextChanged
        Call LOAD_HEADER_DATA()
    End Sub

    Private Sub btnLoad_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub cboSLoc_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSLoc.SelectedIndexChanged
        DGVH.Rows.Clear()
        DGVD.Rows.Clear()
        Call LOAD_HEADER_DATA()
    End Sub

    Private Sub tsbPrintBarcode_Click(sender As Object, e As EventArgs) Handles tsbPrintBarcode.Click


        Query = "SELECT  BARCODE, CORAK, WARNA, GRADE, PARTAI, YARD, METER, LEBAR, UNIT, SO, KET " & _
                "FROM    STOCKBCD " & _
                "WHERE   BARCODE = '" & DGVD.CurrentRow.Cells(0).Value.ToString & "'"

        DS = New DataSet
        DA = New SqlDataAdapter(Query, Conn)
        DA.Fill(DS, "BARCODE")

        If DS.Tables("BARCODE").Rows.Count = 1 Then

            Dim PrintControl As New ClassPrintReport

            PrintControl.CetakReport("RptBarcode.rdlc", "DataSet7", Query, "BARCODE", TAXRATE)


            'If MsgBox("Print Label Barcode No. [" & DS.Tables("BARCODE").Rows(0)("BARCODE") & "]  ? ", _
            '          MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Print") = vbNo Then
            '    Exit Sub
            'End If


            'Dim vBarcode As String = DS.Tables("BARCODE").Rows(0)("BARCODE")
            'Dim vCorak As String = DS.Tables("BARCODE").Rows(0)("CORAK")
            'Dim vWarna As String = DS.Tables("BARCODE").Rows(0)("WARNA")
            'Dim vGrade As String = DS.Tables("BARCODE").Rows(0)("GRADE")
            'Dim vBatch As String = DS.Tables("BARCODE").Rows(0)("PARTAI")

            'Dim vLebar As String = IIf(IsDBNull(DS.Tables("BARCODE").Rows(0)("LEBAR")), "", DS.Tables("BARCODE").Rows(0)("LEBAR"))
            'Dim vSO As String = IIf(IsDBNull(DS.Tables("BARCODE").Rows(0)("SO")), "", DS.Tables("BARCODE").Rows(0)("SO"))
            'Dim vKet As String = IIf(IsDBNull(DS.Tables("BARCODE").Rows(0)("KET")), "", DS.Tables("BARCODE").Rows(0)("KET"))

            'Dim vYard As Decimal = DS.Tables("BARCODE").Rows(0)("YARD")
            'Dim vMeter As Decimal = DS.Tables("BARCODE").Rows(0)("METER")

            'Printer.NewPrint()

            'Printer.Print("^XA")

            'Printer.Print("^FO,28^FS^FT20,40^AJN,22,23^FD" + vBarcode + "^FS")

            'Printer.Print("^BY1,3,72^FT20,120^BCN,,N,N^FD" + vBarcode + "^FS")

            'Printer.Print("^FO,24^FS^FT10,160^AJN,23,24^FDSO^FS")
            'Printer.Print("^FO,24^FS^FT105,160^AJN,23,24^FD:^FS")
            'Printer.Print("^FO,31^FS^FT120,160^AJN,23,24^FD" + vSO + "^FS")

            'Printer.Print("^FO,24^FS^FT10,195^AJN,23,24^FDDesign^FS")
            'Printer.Print("^FO,24^FS^FT105,195^AJN,20,20^FD:^FS")
            'Printer.Print("^FO,31^FS^FT120,195^AJN,23,24^FD" + vCorak + "^FS")

            'Printer.Print("^FO,24^FS^FT10,230^AJN,23,24^FDColor^FS")
            'Printer.Print("^FO,24^FS^FT105,230^AJN,20,20^FD:^FS")
            'Printer.Print("^FO,31^FS^FT120,230^AJN,23,24^FD" + vWarna + "^FS")

            'Printer.Print("^FO,24^FS^FT10,265^AJN,23,24^FDBatch^FS")
            'Printer.Print("^FO,24^FS^FT105,265^AJN,20,20^FD:^FS")
            'Printer.Print("^FO,31^FS^FT120,265^AJN,23,24^FD" + vBatch + "^FS")

            'Printer.Print("^FO,24^FS^FT220,265^AJN,23,24^FDGrade^FS")
            'Printer.Print("^FO,24^FS^FT300,265^AJN,20,20^FD:^FS")
            'Printer.Print("^FO,31^FS^FT315,265^AJN,23,24^FD" + vGrade + "^FS")

            'Printer.Print("^FO,24^FS^FT10,300^AJN,23,24^FDLength^FS")
            'Printer.Print("^FO,24^FS^FT105,300^AJN,20,20^FD:^FS")
            'Printer.Print("^FO,31^FS^FT120,300^AJN,23,24^FD" + LTrim(Format(vYard, "#,##0.00")) + "^FS")
            'Printer.Print("^FO,24^FS^FT200,300^AJN,20,20^FD(YD)^FS")
            'Printer.Print("^FO,31^FS^FT270,300^AJN,23,24^FD" + LTrim(Format(vMeter, "#,##0.00")) + "^FS")
            'Printer.Print("^FO,24^FS^FT350,300^AJN,20,20^FD(M)^FS")

            'Printer.Print("^FO,24^FS^FT10,335^AJN,23,24^FDWidth^FS")
            'Printer.Print("^FO,24^FS^FT105,335^AJN,20,20^FD:^FS")
            'Printer.Print("^FO,31^FS^FT120,335^AJN,23,24^FD" + RTrim(vLebar) + "^FS")

            'Printer.Print("^FO,24^FS^FT10,370^AJN,23,24^FDRemark^FS")
            'Printer.Print("^FO,24^FS^FT105,370^AJN,20,20^FD:^FS")
            'Printer.Print("^FO,31^FS^FT120,370^AJN,23,24^FD" + vKet + "^FS")

            'Printer.Print("^XZ")

            'Printer.DoPrint()

        End If


    End Sub

  
    Private Sub tsbAddBarcode_Click(sender As Object, e As EventArgs) Handles tsbAddBarcode.Click
        With FrmAddBarcode
            .ShowDialog()
        End With
    End Sub
End Class