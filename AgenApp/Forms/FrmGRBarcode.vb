Imports System.Data.SqlClient


Public Class FrmGRBarcode


    Public TMPGRBCD As String = ""

    Private Sub Load_Data()

         DGV.Rows.Clear()

        Try

            Query = "SELECT   barcode,corak,warna,grade,partai,lebar,yard,meter FROM [" & TMPGRBCD & "] " & _
                    "WHERE    [po]='" & lblPO.Text & "'"


            DS = New DataSet
            DA = New SqlDataAdapter(Query, Conn)
            DA.Fill(DS, "TEMPRECV")

            Dim n As Integer = 0

            For n = 0 To DS.Tables("TEMPRECV").Rows.Count - 1

                DGV.Rows.Add()
                DGV.Rows(n).Cells(0).Value = n + 1
                DGV.Rows(n).Cells(1).Value = DS.Tables("TEMPRECV").Rows(n)("barcode")
                DGV.Rows(n).Cells(2).Value = DS.Tables("TEMPRECV").Rows(n)("corak")
                DGV.Rows(n).Cells(3).Value = DS.Tables("TEMPRECV").Rows(n)("warna")
                DGV.Rows(n).Cells(4).Value = DS.Tables("TEMPRECV").Rows(n)("grade")
                DGV.Rows(n).Cells(5).Value = DS.Tables("TEMPRECV").Rows(n)("partai")
                DGV.Rows(n).Cells(6).Value = DS.Tables("TEMPRECV").Rows(n)("lebar")
                DGV.Rows(n).Cells(7).Value = DS.Tables("TEMPRECV").Rows(n)("yard")
                DGV.Rows(n).Cells(8).Value = DS.Tables("TEMPRECV").Rows(n)("meter")

            Next

        Catch ex As Exception
            MsgBox(ex.Message)
            'Finally
            '    Conn.Close()
            '    DA.Dispose()
        End Try

        txtBarcode.Focus()


    End Sub

    Private Sub FrmGRBarcode_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        If ConnectStatus = False Then Me.Close()
        txtBarcode.Select()
    End Sub

    Private Sub FrmGRBarcode_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Call Local_Connect()

        Call Host_Connect()

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


        DGV.Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        DGV.Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DGV.Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DGV.Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        DGV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        DGV.ColumnHeadersHeight = 30

        Call Load_Data()


    End Sub

    Private Sub txtBarcode_GotFocus(sender As Object, e As EventArgs) Handles txtBarcode.GotFocus
        txtBarcode.BackColor = Color.FromArgb(254, 240, 158)
    End Sub

    Private Sub txtBarcode_KeyDown(sender As Object, e As KeyEventArgs) Handles txtBarcode.KeyDown
        If e.KeyCode = Keys.Enter Then
            If Trim(txtBarcode.Text) <> "" Then
                Call btnFind_Click(txtBarcode, e)
            End If
        End If
    End Sub

    Private Sub txtBarcode_LostFocus(sender As Object, e As EventArgs) Handles txtBarcode.LostFocus
        txtBarcode.BackColor = Color.White
        txtBarcode.Text = Trim(txtBarcode.Text)
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click

        Query = "DELETE FROM [" & TMPGRBCD & "] "

        CMD = New SqlCommand
        CMD.Connection = Conn
        CMD.CommandText = Query
        CMD.ExecuteNonQuery()

        Me.Close()

    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        If DGV.Rows.Count = 0 Then Exit Sub

        Me.Dispose()

    End Sub

    Private Sub txtBarcode_TextChanged(sender As Object, e As EventArgs) Handles txtBarcode.TextChanged

    End Sub

    Private Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click

        If Trim(txtBarcode.Text) = "" Then
            txtBarcode.Select()
            Exit Sub
        End If


        '// check barcode if already exist

        Query = "SELECT * FROM [STOCKBCD] WHERE [PO]='" & lblPO.Text & "' AND [BARCODE] LIKE '%" & txtBarcode.Text & "'"

        DS = New DataSet
        DA = New SqlDataAdapter(Query, Conn)
        DA.Fill(DS, "STOCKBAR")

        If DS.Tables("STOCKBAR").Rows.Count <> 0 Then
            MessageBox.Show("Barcode no [" & DS.Tables("STOCKBAR").Rows(0)("BARCODE") & "] already exist in stock !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtBarcode.Text = ""
            txtBarcode.Select()
            Exit Sub
        End If


        '// check reference data

        Query = "SELECT * FROM [AG_TRANBCD] WHERE [barcode]='" & txtBarcode.Text & "'"      ' [nopo]='" & lblPO.Text & "' AND

        DS = New DataSet
        DA = New SqlDataAdapter(Query, HostConn)
        DA.Fill(DS, "BARCODE")

        If DS.Tables("BARCODE").Rows.Count <> 1 Then
            txtBarcode.Text = ""
            txtBarcode.Select()
            Exit Sub
        End If


        Dim i As Integer = 0
        Dim n As Integer = 0
        Dim Ada As Boolean = False
        Dim GUDANG As String = ""
        Dim GRD_X As Decimal = 0


        '// insert into temporary table

        Ada = False

        For i = 0 To DGV.Rows.Count - 1
            If DGV.Rows(i).Cells(1).Value = txtBarcode.Text Then
                Ada = True
                Exit For
            End If
        Next


        If Ada = False Then

            If IsDBNull(DS.Tables("BARCODE").Rows(0)("sloc")) Then
                GUDANG = SLOC
            End If

            If IsDBNull(DS.Tables("BARCODE").Rows(0)("x")) Then
                GRD_X = 0
            Else
                GRD_X = DS.Tables("BARCODE").Rows(0)("x")
            End If

            Query = "INSERT INTO [" & TMPGRBCD & "] (barcode,corak,warna,grade,partai,lebar,yard,meter,po,so,sj,ket,sloc,x) " & _
                    "VALUES ('" & DS.Tables("BARCODE").Rows(0)("barcode") & "'," & _
                            "'" & DS.Tables("BARCODE").Rows(0)("corak") & "'," & _
                            "'" & DS.Tables("BARCODE").Rows(0)("warna") & "'," & _
                            "'" & DS.Tables("BARCODE").Rows(0)("grade") & "'," & _
                            "'" & DS.Tables("BARCODE").Rows(0)("partai") & "'," & _
                            "'" & DS.Tables("BARCODE").Rows(0)("lebar") & "'," & _
                             "" & Replace(DS.Tables("BARCODE").Rows(0)("yard"), ",", ".") & "," & _
                             "" & Replace(DS.Tables("BARCODE").Rows(0)("meter"), ",", ".") & "," & _
                            "'" & DS.Tables("BARCODE").Rows(0)("nopo") & "'," & _
                            "'" & DS.Tables("BARCODE").Rows(0)("noso") & "'," & _
                            "'" & DS.Tables("BARCODE").Rows(0)("nomor_sj") & "'," & _
                            "'" & DS.Tables("BARCODE").Rows(0)("keterangan") & "','" & GUDANG & "'," & _
                            " " & Replace(GRD_X, ",", ".") & ")"

            CMD = New SqlCommand
            CMD.Connection = Conn
            CMD.CommandText = Query
            CMD.ExecuteNonQuery()


            n = DGV.Rows.Add()
            DGV.Rows(n).Cells(0).Value = n + 1
            DGV.Rows(n).Cells(1).Value = DS.Tables("BARCODE").Rows(0)("barcode")
            DGV.Rows(n).Cells(2).Value = DS.Tables("BARCODE").Rows(0)("corak")
            DGV.Rows(n).Cells(3).Value = DS.Tables("BARCODE").Rows(0)("warna")
            DGV.Rows(n).Cells(4).Value = DS.Tables("BARCODE").Rows(0)("grade")
            DGV.Rows(n).Cells(5).Value = DS.Tables("BARCODE").Rows(0)("partai")
            DGV.Rows(n).Cells(6).Value = DS.Tables("BARCODE").Rows(0)("lebar")
            DGV.Rows(n).Cells(7).Value = DS.Tables("BARCODE").Rows(0)("yard")
            DGV.Rows(n).Cells(8).Value = DS.Tables("BARCODE").Rows(0)("meter")
            DGV.CurrentCell = DGV(1, n)
        Else
            DGV.CurrentCell = DGV(1, i)
        End If

        txtBarcode.Text = ""
        txtBarcode.Select()


    End Sub

End Class