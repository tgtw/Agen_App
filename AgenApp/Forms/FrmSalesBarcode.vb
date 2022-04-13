Imports System.Data.SqlClient

Public Class FrmSalesBarcode

    Public TMPSALES As String = ""
    Dim ByPass As Boolean = False

    Private Sub TOTAL()

        Dim TYDS As Double = 0
        Dim TMTR As Double = 0
 

        For n = 0 To DGV.Rows.Count - 1
            TYDS = TYDS + DGV.Rows(n).Cells(5).Value
            TMTR = TMTR + DGV.Rows(n).Cells(6).Value
        Next

        With dgvTotal
            .Rows(0).Cells(5).Value = Format(TYDS, "#,##0.00")
            .Rows(0).Cells(6).Value = Format(TMTR, "#,##0.00")
        End With        

    End Sub

    Private Sub Load_Data()

        DGV.Rows.Clear()


        Try

            Query = "SELECT  barcode,corak,warna,grade,yard,meter FROM [" & TMPSALES & "] " 

            DS = New DataSet
            DA = New SqlDataAdapter(Query, Conn)
            DA.Fill(DS, "TMPSLSBC")

            Dim n As Integer = 0

            ByPass = True

            For n = 0 To DS.Tables("TMPSLSBC").Rows.Count - 1

                DGV.Rows.Add()
                DGV.Rows(n).Cells(0).Value = n + 1
                DGV.Rows(n).Cells(1).Value = DS.Tables("TMPSLSBC").Rows(n)("barcode")
                DGV.Rows(n).Cells(2).Value = DS.Tables("TMPSLSBC").Rows(n)("corak")
                DGV.Rows(n).Cells(3).Value = DS.Tables("TMPSLSBC").Rows(n)("warna")
                DGV.Rows(n).Cells(4).Value = DS.Tables("TMPSLSBC").Rows(n)("grade")
                DGV.Rows(n).Cells(5).Value = DS.Tables("TMPSLSBC").Rows(n)("yard")
                DGV.Rows(n).Cells(6).Value = DS.Tables("TMPSLSBC").Rows(n)("meter")

            Next

            ByPass = False

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Call TOTAL()

        txtBarcode.Focus()


    End Sub

    Private Sub FrmSalesBarcode_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Call LOCAL_CONNECT()


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

        'DGV.Columns(0).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

        DGV.Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        DGV.Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DGV.Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        DGV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        DGV.ColumnHeadersHeight = 30

        dgvTotal.ColumnHeadersVisible = False
        dgvTotal.Rows.Add()

        dgvTotal.Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgvTotal.Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        dgvTotal.BorderStyle = BorderStyle.FixedSingle
        dgvTotal.CellBorderStyle = DataGridViewCellBorderStyle.None

        txtBarcode.Select()

        Call Load_Data()



    End Sub

    Private Sub txtBarcode_GotFocus(sender As Object, e As EventArgs) Handles txtBarcode.GotFocus
        txtBarcode.BackColor = Color.FromArgb(254, 240, 158)
    End Sub

    Private Sub txtBarcode_KeyDown(sender As Object, e As KeyEventArgs) Handles txtBarcode.KeyDown

        txtBarcode.Text = Trim(txtBarcode.Text)

        If e.KeyCode = Keys.Enter Then

            If txtBarcode.Text = "" Then
                txtBarcode.Select()
                Exit Sub
            End If

            Call btnScan_Click(txtBarcode, e)

        End If

    End Sub

    Private Sub txtBarcode_LostFocus(sender As Object, e As EventArgs) Handles txtBarcode.LostFocus
        txtBarcode.BackColor = Color.White
        txtBarcode.Text = Trim(txtBarcode.Text)
    End Sub


    Private Sub btnScan_Click(sender As Object, e As EventArgs) Handles btnScan.Click

        Dim i As Integer = 0
        Dim n As Integer = 0
        Dim Ada As Boolean = False


        If txtBarcode.Text = "" Then
            txtBarcode.Select()
            Exit Sub
        End If


        Query = "SELECT * FROM [STOCKBCD] WHERE [barcode]='" & txtBarcode.Text & "'"

        DS = New DataSet
        DA = New SqlDataAdapter(Query, Conn)
        DA.Fill(DS, "STOCKBAR")

        If DS.Tables("STOCKBAR").Rows.Count <> 1 Then
            MessageBox.Show("No barcode found in stock !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtBarcode.Text = ""
            txtBarcode.Select()
            Exit Sub
        End If


        '// insert into temporary table

        Ada = False

        For i = 0 To DGV.Rows.Count - 1
            If DGV.Rows(i).Cells(1).Value = txtBarcode.Text Then
                Ada = True
                Exit For
            End If
        Next

        If Ada = True Then
            DGV.CurrentCell = DGV(1, i)
        Else

            Query = "INSERT INTO [" & TMPSALES & "] SELECT * FROM [STOCKBCD] WHERE [barcode]='" & txtBarcode.Text & "'"

            CMD = New SqlCommand
            CMD.Connection = Conn
            CMD.CommandText = Query
            CMD.ExecuteNonQuery()

            ByPass = True

            n = DGV.Rows.Add()
            DGV.Rows(n).Cells(0).Value = n + 1
            DGV.Rows(n).Cells(1).Value = DS.Tables("STOCKBAR").Rows(0)("barcode")
            DGV.Rows(n).Cells(2).Value = DS.Tables("STOCKBAR").Rows(0)("corak")
            DGV.Rows(n).Cells(3).Value = DS.Tables("STOCKBAR").Rows(0)("warna")
            DGV.Rows(n).Cells(4).Value = DS.Tables("STOCKBAR").Rows(0)("grade")
            DGV.Rows(n).Cells(5).Value = DS.Tables("STOCKBAR").Rows(0)("yard")
            DGV.Rows(n).Cells(6).Value = DS.Tables("STOCKBAR").Rows(0)("meter")
            DGV.CurrentCell = DGV(1, n)

            ByPass = False

        End If


        txtBarcode.Text = ""

        txtBarcode.Select()

        Call TOTAL()


    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click

        Query = "DELETE FROM [" & TMPSALES & "] "

        CMD = New SqlCommand
        CMD.Connection = Conn
        CMD.CommandText = Query
        CMD.ExecuteNonQuery()

        Me.Dispose()

    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        If DGV.Rows.Count = 0 Then Exit Sub

        For n = 0 To DGV.Rows.Count - 1

            Query = "UPDATE [" & TMPSALES & "] " & _
                       "SET [yard]=" & Replace(Math.Round(DGV.Rows(n).Cells(5).Value, 2), ",", ".") & "," & _
                          " [meter]=" & Replace(Math.Round(DGV.Rows(n).Cells(6).Value, 2), ",", ".") & "," & _
                          " [potong]=" & IIf(DGV.Rows(n).Cells(7).Value = True, 1, 0) & " " & _
                     "WHERE [barcode]='" & DGV.Rows(n).Cells(1).Value & "'"

            CMD = New SqlCommand
            CMD.Connection = Conn
            CMD.CommandText = Query
            CMD.ExecuteNonQuery()

        Next

        Me.Dispose()

    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click

        Dim Ulang As Integer = DGV.Rows.Count - 1

        For n = 0 To Ulang
            If n > Ulang Then Exit For
            If DGV.Rows(n).Cells(7).Value = True Then
                Me.DGV.Rows.RemoveAt(n)
                Ulang = Ulang - 1
                n = n - 1
            End If
        Next

    End Sub

 

    Private Sub DGV_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DGV.CellValueChanged

        If ByPass = True Then Exit Sub

        If e.RowIndex < 0 Then Exit Sub

        If e.ColumnIndex < 5 Or e.ColumnIndex > 6 Then Exit Sub


        Dim YDS As Decimal = 0
        Dim MTR As Decimal = 0

        If e.ColumnIndex = 5 Then       ' YARD

            YDS = DGV.Rows(e.RowIndex).Cells(5).Value
            MTR = YDS / 1.0936

        ElseIf e.ColumnIndex = 6 Then   ' METER

            MTR = DGV.Rows(e.RowIndex).Cells(6).Value
            YDS = MTR * 1.0936

        End If

        DGV.Rows(e.RowIndex).Cells(5).Value = YDS
        DGV.Rows(e.RowIndex).Cells(6).Value = MTR

        Call TOTAL()

    End Sub

    Private Sub txtBarcode_QueryAccessibilityHelp(sender As Object, e As QueryAccessibilityHelpEventArgs) Handles txtBarcode.QueryAccessibilityHelp

    End Sub

    Private Sub txtBarcode_TabIndexChanged(sender As Object, e As EventArgs) Handles txtBarcode.TabIndexChanged

    End Sub


    
    Private Sub txtBarcode_TextChanged(sender As Object, e As EventArgs) Handles txtBarcode.TextChanged

    End Sub
End Class
