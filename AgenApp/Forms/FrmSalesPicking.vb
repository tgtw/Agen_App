Imports System.Data.SqlClient

Public Class FrmSalesPicking

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
            .RowHeadersWidth = 50
            .Columns(1).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(2).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns(1).HeaderText = Format(TYDS, "#,##0.00")
            .Columns(2).HeaderText = Format(TMTR, "#,##0.00")
        End With

        Call AutoNumberRowsForGridView(DGV)


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

            Call TOTAL()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        txtPickNo.Focus()


    End Sub

    Private Sub FrmSalesPicking_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Call LOCAL_CONNECT()

        Call HOST_CONNECT()

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

        'DGV.Columns(0).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

        DGV.Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        DGV.Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DGV.Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        DGV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        DGV.ColumnHeadersHeight = 30

        'dgvTotal.ColumnHeadersVisible = False
        'dgvTotal.Rows.Add()

        For n = 0 To 3
            dgvTotal.Columns(n).SortMode = DataGridViewColumnSortMode.NotSortable
        Next

        dgvTotal.Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgvTotal.Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        dgvTotal.BorderStyle = BorderStyle.FixedSingle
        dgvTotal.CellBorderStyle = DataGridViewCellBorderStyle.None

        txtPickNo.Select()

        Call Load_Data()



    End Sub

    Private Sub txtPickNo_GotFocus(sender As Object, e As EventArgs) Handles txtPickNo.GotFocus
        txtPickNo.BackColor = Color.FromArgb(254, 240, 158)
    End Sub

    Private Sub txtPickNo_KeyDown(sender As Object, e As KeyEventArgs) Handles txtPickNo.KeyDown

        If e.KeyCode = Keys.Enter Then

            If Trim(txtPickNo.Text) = "" Then
                txtPickNo.Select()
                Exit Sub
            End If

            Call btnScan_Click(txtPickNo, e)

        End If

    End Sub

    Private Sub txtPickNo_LostFocus(sender As Object, e As EventArgs) Handles txtPickNo.LostFocus
        txtPickNo.BackColor = Color.White
    End Sub


    Private Sub btnScan_Click(sender As Object, e As EventArgs) Handles btnScan.Click

        Dim i As Integer = 0
        Dim n As Integer = 0
        Dim NoBarcode As String = ""


        If Trim(txtPickNo.Text) = "" Then
            txtPickNo.Select()
            Exit Sub
        End If


        Me.Cursor = Cursors.WaitCursor


        'Query = "INSERT INTO SIASAT_" & PLANT & ".DBO." & TMPSALES & " (barcode, corak, warna, grade, yard, meter, unit, so, sloc, x, sj) " & _
        '          "(SELECT DISTINCT DP.Barcode, MK.CustDes, MK.WarnaCust, DP.Grade, DP.Panjang, DP.PanjangM, MK.Satuan, " & _
        '                           "SUBSTRING(DK.NoProd,1,9), '" & PLANT & "', 0, NoPick " & _
        '           "FROM TIPS.DBO.D_PICK DP INNER JOIN TIPS.DBO.D_KAINJADI DK ON DK.Barcode=DP.Barcode " & _
        '                                   "INNER JOIN TIPS.DBO.M_KAINJADI MK ON MK.NoProd=DK.NoProd " & _
        '           "WHERE [NoPick] LIKE '%" & Trim(txtPickNo.Text) & "')"

        'CMD = New SqlCommand
        'CMD.Connection = Conn
        'CMD.CommandText = Query
        'CMD.ExecuteNonQuery()


        Query = "SELECT DISTINCT DP.Barcode, MK.CustDes, MK.WarnaCust, DP.Grade, DP.Panjang, DP.PanjangM, MK.Satuan, DK.NoProd, DP.NoPick " & _
                "FROM TIPS.DBO.D_PICK DP INNER JOIN TIPS.DBO.D_KAINJADI DK ON DK.Barcode=DP.Barcode " & _
                                        "INNER JOIN TIPS.DBO.M_KAINJADI MK ON MK.NoProd=DK.NoProd " & _
                "WHERE [NoPick] LIKE '%" & Trim(txtPickNo.Text) & "'"

        DS = New DataSet
        DA = New SqlDataAdapter(Query, HostConn)
        DA.Fill(DS, "PICKING")

        For n = 0 To DS.Tables("PICKING").Rows.Count - 1

            Query = "INSERT INTO SIASAT_" & PLANT & ".DBO." & TMPSALES & " (barcode, corak, warna, grade, yard, meter, unit, so, sloc, x, sj) " & _
                    "VALUES ('" & DS.Tables("PICKING").Rows(n)("BARCODE") & "','" & DS.Tables("PICKING").Rows(n)("CUSTDES") & "'," & _
                            "'" & DS.Tables("PICKING").Rows(n)("WARNACUST") & "','" & DS.Tables("PICKING").Rows(n)("GRADE") & "'," & _
                            " " & Replace(DS.Tables("PICKING").Rows(n)("PANJANG"), ",", ".") & "," & _
                            " " & Replace(DS.Tables("PICKING").Rows(n)("PANJANGM"), ",", ".") & "," & _
                            "'" & IIf(Trim(DS.Tables("PICKING").Rows(n)("SATUAN")) = "YARD", "YD", "M") & "'," & _
                            "'" & Mid(DS.Tables("PICKING").Rows(n)("NOPROD"), 1, 9) & "','" & PLANT & "',0," & _
                            "'" & DS.Tables("PICKING").Rows(n)("NOPICK") & "')"

            CMD = New SqlCommand
            CMD.Connection = Conn
            CMD.CommandText = Query
            CMD.ExecuteNonQuery()

        Next



        '==================================================
        ' HAPUS BARCODE-2 YANG TIDAK ADA di TABEL STOCKBCD 
        '==================================================

        'Query = "DELETE FROM [" & TMPSALES & "] WHERE " & TMPSALES & ".barcode NOT IN (SELECT barcode FROM STOCKBCD) "

        'CMD = New SqlCommand
        'CMD.Connection = Conn
        'CMD.CommandText = Query
        'CMD.ExecuteNonQuery()



        '===============================================
        ' HAPUS DUPLIKASI DATA BERDASARKAN KODE BARCODE
        '===============================================

        Query = "WITH CTE AS(" & _
                     "SELECT [barcode], [corak], [warna], [grade], [partai], [yard], [meter], [lebar], [unit], [so], [ket], [sloc], [po], [sj], [tglsj], " & _
                             "RN = ROW_NUMBER() OVER (PARTITION BY barcode ORDER BY barcode) " & _
                     "FROM dbo." & TMPSALES & ") " & _
                "DELETE FROM CTE WHERE RN > 1 "

        CMD = New SqlCommand
        CMD.Connection = Conn
        CMD.CommandText = Query
        CMD.ExecuteNonQuery()


        '========================================
        ' UPDATE DATA BERDASARKAN TABEL STOCKBCD
        '========================================

        'Query = "UPDATE [" & TMPSALES & "] " & _
        '        "SET    corak=T2.corak, warna=T2.warna, grade=T2.grade, partai=T2.partai, yard=T2.yard, meter=T2.meter, " & _
        '               "lebar=T2.lebar, unit=T2.unit, so=T2.so, sloc=T2.sloc " & _
        '        "FROM   [" & TMPSALES & "] T1 INNER JOIN [STOCKBCD] T2 ON T1.barcode=T2.barcode "

        'CMD = New SqlCommand
        'CMD.Connection = Conn
        'CMD.CommandText = Query
        'CMD.ExecuteNonQuery()


        '=========================================
        ' UPDATE CORAK BERDASARKAN TABLE MASPROD
        '=========================================

        Dim CORAK As String = ""
        Dim WARNA As String = ""
        Dim GRADE As String = ""

        Query = "SELECT barcode, corak, warna, grade FROM [" & TMPSALES & "] "

        DS = New DataSet
        DA = New SqlDataAdapter(Query, Conn)
        DA.Fill(DS, "TMPSLS")

        For n = 0 To DS.Tables("TMPSLS").Rows.Count - 1

            CORAK = Trim(DS.Tables("TMPSLS").Rows(n)("CORAK"))
            WARNA = Trim(DS.Tables("TMPSLS").Rows(n)("WARNA"))
            GRADE = Trim(DS.Tables("TMPSLS").Rows(n)("GRADE"))

            If IsNumeric(CORAK) Then

                Query = "SELECT kdprod FROM [MASPROD] " & _
                        "WHERE [kdprod] LIKE '%" & CORAK & "%' AND [kdprod] LIKE '%" & WARNA & "%' AND [kdprod] LIKE '%" & GRADE & "%'"

                DS2 = New DataSet
                DA2 = New SqlDataAdapter(Query, Conn)
                DA2.Fill(DS2, "ITEMCODE")

                If DS2.Tables("ITEMCODE").Rows.Count = 1 Then
                    CORAK = DS2.Tables("ITEMCODE").Rows(0)("KDPROD")
                End If

            Else
                CORAK = Replace(DS.Tables("TMPSLS").Rows(n)("CORAK"), ".A.", "." & Trim(DS.Tables("TMPSLS").Rows(n)("GRADE")) & ".")
            End If


            Query = "UPDATE [" & TMPSALES & "] SET [corak]='" & CORAK & "' WHERE [barcode]='" & DS.Tables("TMPSLS").Rows(n)("BARCODE") & "'"

            CMD = New SqlCommand
            CMD.Connection = Conn
            CMD.CommandText = Query
            CMD.ExecuteNonQuery()

        Next


        Query = "UPDATE [" & TMPSALES & "] SET sloc='" & PLANT & "' WHERE sloc='' OR sloc Is Null "

        CMD = New SqlCommand
        CMD.Connection = Conn
        CMD.CommandText = Query
        CMD.ExecuteNonQuery()


        '====================
        ' TAMPILKAN KE GRID
        '====================

        Query = "SELECT sj, barcode, corak, warna, grade, yard, meter FROM [" & TMPSALES & "] ORDER BY sj, barcode "

        DS = New DataSet
        DA = New SqlDataAdapter(Query, Conn)
        DA.Fill(DS, "PICK")

        ByPass = True

        DGV.Rows.Clear()

        For n = 1 To DS.Tables("PICK").Rows.Count

            i = DGV.Rows.Add()

            DGV.Rows(i).Cells(0).Value = DS.Tables("PICK").Rows(n - 1)(0)   ' picking
            DGV.Rows(i).Cells(1).Value = DS.Tables("PICK").Rows(n - 1)(1)   ' Barcode
            DGV.Rows(i).Cells(2).Value = DS.Tables("PICK").Rows(n - 1)(2)   ' Corak
            DGV.Rows(i).Cells(3).Value = DS.Tables("PICK").Rows(n - 1)(3)   ' Warna
            DGV.Rows(i).Cells(4).Value = DS.Tables("PICK").Rows(n - 1)(4)   ' Grade
            DGV.Rows(i).Cells(5).Value = DS.Tables("PICK").Rows(n - 1)(5)   ' Yard
            DGV.Rows(i).Cells(6).Value = DS.Tables("PICK").Rows(n - 1)(6)   ' Meter

        Next



        'Query = "SELECT DISTINCT DP.Barcode,DP.Grade,DP.Panjang,DP.PanjangM,DP.PanjangX,DK.NoProd,DK.CorakSAP,MK.CustDes,MK.WarnaCust,MK.WarnaProd,MK.Satuan " & _
        '        "FROM  [D_PICK] DP INNER JOIN [D_KAINJADI] DK ON DK.Barcode=DP.Barcode INNER JOIN [M_KAINJADI] MK ON MK.NoProd=DK.NoProd " & _
        '        "WHERE [NoPick] LIKE '%" & txtPickNo.Text & "' ORDER BY DP.Barcode "

        'DS = New DataSet
        'DA = New SqlDataAdapter(Query, HostConn)
        'DA.Fill(DS, "PICK")

        'If DS.Tables("PICK").Rows.Count = 0 Then
        '    MsgBox(" Picking No [" & Trim(txtPickNo.Text) & "] no found ! ", MsgBoxStyle.Critical, "Info")
        '    txtPickNo.Text = ""
        '    txtPickNo.Select()
        '    Exit Sub
        'End If

        'ByPass = True

        'For n = 1 To DS.Tables("PICK").Rows.Count

        '    i = DGV.Rows.Add()

        '    DGV.Rows(i).Cells(0).Value = i + 1
        '    DGV.Rows(i).Cells(1).Value = DS.Tables("PICK").Rows(n - 1)(0)   ' Barcode
        '    DGV.Rows(i).Cells(2).Value = DS.Tables("PICK").Rows(n - 1)(7)   ' Corak
        '    DGV.Rows(i).Cells(3).Value = DS.Tables("PICK").Rows(n - 1)(8)   ' Warna
        '    DGV.Rows(i).Cells(4).Value = DS.Tables("PICK").Rows(n - 1)(1)   ' Grade
        '    DGV.Rows(i).Cells(5).Value = DS.Tables("PICK").Rows(n - 1)(2)   ' Yard
        '    DGV.Rows(i).Cells(6).Value = DS.Tables("PICK").Rows(n - 1)(3)   ' Meter

        '    'Query = "INSERT INTO [" & TMPSALES & "] (barcode, corak, warna, grade, yard, meter) " & _
        '    '        "VALUES ('" & DS.Tables("PICK").Rows(n - 1)(0) & "','" & DS.Tables("PICK").Rows(n - 1)(7) & "'," & _
        '    '                "'" & DS.Tables("PICK").Rows(n - 1)(8) & "','" & DS.Tables("PICK").Rows(n - 1)(1) & "'," & _
        '    '                " " & Replace(DS.Tables("PICK").Rows(n - 1)(2), ",", ".") & "," & Replace(DS.Tables("PICK").Rows(n - 1)(3), ",", ".") & ")"

        '    'CMD = New SqlCommand
        '    'CMD.Connection = Conn
        '    'CMD.CommandText = Query
        '    'CMD.ExecuteNonQuery()

        'Next

        '======================
        ' CEK DI STOCK BARCODE
        '======================

        'For n = 1 To DGV.Rows.Count

        '    'If n > DGV.Rows.Count Then
        '    '    Exit For
        '    'End If

        '    Query = "SELECT * FROM [STOCKBCD] WHERE [barcode]='" & DGV.Rows(n - 1).Cells(1).Value & "'"

        '    DS = New DataSet
        '    DA = New SqlDataAdapter(Query, Conn)
        '    DA.Fill(DS, "STOCKBAR")

        '    If DS.Tables("STOCKBAR").Rows.Count = 1 Then

        '        Query = "INSERT INTO [" & TMPSALES & "] SELECT * FROM [STOCKBCD] WHERE [barcode]='" & DGV.Rows(n - 1).Cells(1).Value & "'"

        '        CMD = New SqlCommand
        '        CMD.Connection = Conn
        '        CMD.CommandText = Query
        '        CMD.ExecuteNonQuery()

        '        DGV.Rows(n - 1).Cells(0).Value = n
        '        DGV.Rows(n - 1).Cells(1).Value = DS.Tables("STOCKBAR").Rows(0)("barcode")
        '        DGV.Rows(n - 1).Cells(2).Value = DS.Tables("STOCKBAR").Rows(0)("corak")
        '        DGV.Rows(n - 1).Cells(3).Value = DS.Tables("STOCKBAR").Rows(0)("warna")
        '        DGV.Rows(n - 1).Cells(4).Value = DS.Tables("STOCKBAR").Rows(0)("grade")
        '        DGV.Rows(n - 1).Cells(5).Value = DS.Tables("STOCKBAR").Rows(0)("yard")
        '        DGV.Rows(n - 1).Cells(6).Value = DS.Tables("STOCKBAR").Rows(0)("meter")

        '    Else
        '        DGV.Rows.RemoveAt(n - 1)
        '        n = n - 1
        '    End If
        'Next


        Me.Cursor = Cursors.Default

        ByPass = False

        txtPickNo.Text = ""

        txtPickNo.Select()

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

            If DGV.Rows(n).Cells(8).Value = True Then

                Query = "DELETE FROM [" & TMPSALES & "] WHERE [barcode] LIKE '%" & DGV.Rows(n).Cells(1).Value & "'"

                CMD = New SqlCommand
                CMD.Connection = Conn
                CMD.CommandText = Query
                CMD.ExecuteNonQuery()

                Me.DGV.Rows.RemoveAt(n)
                Ulang = Ulang - 1
                n = n - 1

            End If

        Next

        txtPickNo.Text = ""

        txtPickNo.Select()

        Call TOTAL()

    End Sub

 

    Private Sub DGV_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DGV.CellValueChanged

        If ByPass = True Then Exit Sub

        If e.RowIndex < 0 Then Exit Sub

        If e.ColumnIndex < 5 Or e.ColumnIndex > 6 Then Exit Sub


        Dim YDS As Decimal = 0
        Dim MTR As Decimal = 0

        If e.ColumnIndex = 5 Then       ' YARD

            YDS = DGV.Rows(e.RowIndex).Cells(5).Value
            MTR = Math.Round(YDS / 1.0936, 2)

        ElseIf e.ColumnIndex = 6 Then   ' METER

            MTR = DGV.Rows(e.RowIndex).Cells(6).Value
            YDS = Math.Round(MTR * 1.0936, 2)

        End If

        DGV.Rows(e.RowIndex).Cells(5).Value = YDS
        DGV.Rows(e.RowIndex).Cells(6).Value = MTR

        Call TOTAL()

    End Sub

    Private Sub DGV_Sorted(sender As Object, e As EventArgs) Handles DGV.Sorted
        Call TOTAL()
    End Sub
End Class
