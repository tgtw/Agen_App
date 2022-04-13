Imports System.Data.SqlClient


Public Class FrmGRDelivery

    Public TMPFILEGR As String = ""

    Private Sub Load_Data()

        Dim n As Integer = 0

        DGV.Rows.Clear()

        Try

            Query = "SELECT  sj,barcode,corak,warna,grade,partai,lebar,yard,meter,x FROM [" & TMPFILEGR & "] " & _
                    "WHERE   [po]='" & lblPO.Text & "'"

            DS = New DataSet
            DA = New SqlDataAdapter(Query, Conn)
            DA.Fill(DS, "TEMPRECV")

            For n = 0 To DS.Tables("TEMPRECV").Rows.Count - 1

                DGV.Rows.Add()
                DGV.Rows(n).Cells(0).Value = n + 1
                DGV.Rows(n).Cells(1).Value = DS.Tables("TEMPRECV").Rows(n)("sj")
                DGV.Rows(n).Cells(2).Value = DS.Tables("TEMPRECV").Rows(n)("barcode")
                DGV.Rows(n).Cells(3).Value = DS.Tables("TEMPRECV").Rows(n)("corak")
                DGV.Rows(n).Cells(4).Value = DS.Tables("TEMPRECV").Rows(n)("grade")
                DGV.Rows(n).Cells(5).Value = DS.Tables("TEMPRECV").Rows(n)("warna")
                DGV.Rows(n).Cells(6).Value = DS.Tables("TEMPRECV").Rows(n)("partai")
                DGV.Rows(n).Cells(7).Value = DS.Tables("TEMPRECV").Rows(n)("lebar")
                DGV.Rows(n).Cells(8).Value = DS.Tables("TEMPRECV").Rows(n)("yard")
                DGV.Rows(n).Cells(9).Value = DS.Tables("TEMPRECV").Rows(n)("meter")
                DGV.Rows(n).Cells(10).Value = DS.Tables("TEMPRECV").Rows(n)("x")

            Next

        Catch ex As Exception
            MsgBox(ex.Message)

        Finally
            '    Conn.Close()
            '    DA.Dispose()
        End Try

        txtDelvNo.Focus()


    End Sub

    Private Sub FrmGRDelivery_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        If ConnectStatus = False Then Me.Close()
        txtDelvNo.Select()
    End Sub

    Private Sub FrmGRDelivery_Load(sender As Object, e As EventArgs) Handles MyBase.Load

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
        DGV.Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight    ' yard
        DGV.Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight    ' meter
        DGV.Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight   ' X

        DGV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        DGV.ColumnHeadersHeight = 30

        DGV.Columns(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill


        Call Load_Data()


    End Sub

    Private Sub txtDelvNo_GotFocus(sender As Object, e As EventArgs) Handles txtDelvNo.GotFocus
        txtDelvNo.BackColor = Color.FromArgb(254, 240, 158)
    End Sub

    Private Sub txtDelvNo_KeyDown(sender As Object, e As KeyEventArgs) Handles txtDelvNo.KeyDown
        If e.KeyCode = Keys.Enter Then
            If Trim(txtDelvNo.Text) <> "" Then
                Call btnFindDelv_Click(txtDelvNo, e)
            End If
        End If
    End Sub

    Private Sub txtDelvNo_LostFocus(sender As Object, e As EventArgs) Handles txtDelvNo.LostFocus
        txtDelvNo.BackColor = Color.White
        txtDelvNo.Text = Trim(txtDelvNo.Text)
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click

        Query = "DELETE FROM [" & TMPFILEGR & "] "

        CMD = New SqlCommand
        CMD.Connection = Conn
        CMD.CommandText = Query
        CMD.ExecuteNonQuery()

        Me.Close()

    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        If DGV.Rows.Count = 0 Then Exit Sub


        'Query = "SELECT  barcode,corak,warna,grade,partai,lebar,yard,meter,sj,tglsj FROM [" & TMPFILEGR & "] " & _
        '         "WHERE  [po]='" & lblPO.Text & "'"

        'DS = New DataSet
        'DA = New SqlDataAdapter(Query, Conn)
        'DA.Fill(DS, "TEMPRECV")

        'If DS.Tables("TEMPRECV").Rows.Count > 0 Then
        '    With FrmGR
        '        .lblDelv.Text = DS.Tables("TEMPRECV").Rows(0)("SJ")
        '        .dtpPostDate.Value = DS.Tables("TEMPRECV").Rows(0)("TGLSJ")
        '    End With
        'End If

        Me.Dispose()

    End Sub

    Private Sub btnFindDelv_Click(sender As Object, e As EventArgs) Handles btnFindDelv.Click

        '=======================
        ' CARI NO PL DARI NO SJ
        '=======================

        'Query = "SELECT NoPick FROM [M_PICK] WHERE [NoSJ] LIKE '%" & txtDelvNo.Text & "'"

        'DS = New DataSet
        'DA = New SqlDataAdapter(Query, HostConn)
        'DA.Fill(DS, "PICKING")

        'If DS.Tables("PICKING").Rows.Count = 0 Then
        '    txtDelvNo.Text = ""
        '    txtDelvNo.Select()
        '    Exit Sub
        'End If

        'PICK_NO = Trim(DS.Tables("PICKING").Rows(n)("NoPick"))

        'If PICK_NO = "" Then
        '    txtDelvNo.Text = ""
        '    txtDelvNo.Select()
        '    Exit Sub
        'End If

        '===================
        ' CARI DATA PICKING
        '===================

        'Query = "SELECT DISTINCT DP.NoPick,DP.Barcode,DP.Panjang,DP.PanjangM,DP.Grade,DP.PanjangX, DK.NoSO,DK.Party," & _
        '                "MK.CustDes,MK.WarnaCust,MK.Satuan " & _
        '        "FROM   TIPS.DBO.D_PICK DP INNER JOIN TIPS.DBO.D_KAINJADI DK ON DK.Barcode=DP.Barcode " & _
        '                                  "INNER JOIN TIPS.DBO.M_KAINJADI MK ON MK.NoProd=DK.NoProd " & _
        '        "WHERE [NoPick] LIKE '%" & PICK_NO & "'"

        'DS = New DataSet
        'DA = New SqlDataAdapter(Query, HostConn)
        'DA.Fill(DS, "PICKING")

        'If DS.Tables("PICKING").Rows.Count = 0 Then
        '    txtDelvNo.Text = ""
        '    txtDelvNo.Select()
        '    Exit Sub
        'End If



        Dim ada As Boolean = False
        Dim n As Integer = 0
        Dim i As Integer
        Dim GUDANG As String = ""
        Dim GRD_X As Decimal = 0
        Dim PICK_NO As String = ""
        Dim YDS As Decimal = 0
        Dim MTR As Decimal = 0

        DGV.Rows.Clear()


        Query = "SELECT * FROM [AG_TRANBCD] WHERE [Nomor_SJ] LIKE '%" & txtDelvNo.Text & "'"    ' AND [NoPO]='" & lblPO.Text & "'"

        DS = New DataSet
        DA = New SqlDataAdapter(Query, HostConn)
        DA.Fill(DS, "BARCODE")

        If DS.Tables("BARCODE").Rows.Count = 0 Then
            txtDelvNo.Text = ""
            txtDelvNo.Select()
            Exit Sub
        End If


        For n = 0 To DS.Tables("BARCODE").Rows.Count - 1

            '===========================
            ' CEK BARCODE DI DATA STOCK 
            '===========================

            Query = "SELECT barcode FROM [STOCKBCD] WHERE [barcode]='" & DS.Tables("BARCODE").Rows(n)("barcode") & "'"

            DS2 = New DataSet
            DA2 = New SqlDataAdapter(Query, Conn)
            DA2.Fill(DS2, "STOCKBAR")

            If DS2.Tables("STOCKBAR").Rows.Count = 0 Then

                '==============================
                ' CEK BARCODE DI DATAGRIDVIEW
                '==============================

                ada = False

                For i = 0 To DGV.Rows.Count - 1
                    If Trim(DGV.Rows(i).Cells(1).Value) = Trim(DS.Tables("BARCODE").Rows(n)("barcode")) Then
                        ada = True
                        Exit For
                    End If
                Next


                If ada = False Then

                    GUDANG = IIf(IsNothing(DS.Tables("BARCODE").Rows(n)("sloc")), SLOC, DS.Tables("BARCODE").Rows(n)("sloc"))

                    If IsDBNull(DS.Tables("BARCODE").Rows(n)("X")) Then
                        GRD_X = 0
                    Else
                        GRD_X = DS.Tables("BARCODE").Rows(n)("X")
                    End If

                    YDS = DS.Tables("BARCODE").Rows(n)("yard")
                    MTR = DS.Tables("BARCODE").Rows(n)("meter")

                    If Trim(DS.Tables("BARCODE").Rows(n)("satuan")) = "METER" Then
                        GRD_X = GRD_X * 1.0936
                    End If

                    Query = "INSERT INTO [" & TMPFILEGR & "] (po,sloc,barcode,corak,warna,grade,partai,lebar,yard,meter,so,sj,ket,tglsj,x,unit) " & _
                            "VALUES ('" & lblPO.Text & "','" & GUDANG & "'," & _
                                    "'" & DS.Tables("BARCODE").Rows(n)("barcode") & "'," & _
                                    "'" & DS.Tables("BARCODE").Rows(n)("corak") & "'," & _
                                    "'" & DS.Tables("BARCODE").Rows(n)("warna") & "'," & _
                                    "'" & DS.Tables("BARCODE").Rows(n)("grade") & "'," & _
                                    "'" & DS.Tables("BARCODE").Rows(n)("partai") & "'," & _
                                    "'" & DS.Tables("BARCODE").Rows(n)("lebar") & "'," & _
                                    " " & Replace(YDS, ",", ".") & "," & Replace(MTR, ",", ".") & "," & _
                                    "'" & Trim(DS.Tables("BARCODE").Rows(n)("noso")) & "'," & _
                                    "'" & DS.Tables("BARCODE").Rows(n)("nomor_sj") & "'," & _
                                    "'" & DS.Tables("BARCODE").Rows(n)("keterangan") & "'," & _
                                    "'" & DATECHAR(DS.Tables("BARCODE").Rows(n)("tgl_sj")) & "'," & _
                                    " " & Replace(GRD_X, ",", ".") & "," & _
                                    "'" & DS.Tables("BARCODE").Rows(n)("satuan") & "')"

                    CMD = New SqlCommand
                    CMD.Connection = Conn
                    CMD.CommandText = Query
                    CMD.ExecuteNonQuery()

                End If

            Else
                MsgBox("Barcode No. [" & DS.Tables("BARCODE").Rows(n)("barcode") & "] already exist in stock ! ", MsgBoxStyle.Critical, "Warning")
            End If

        Next

        Call Load_Data()


        txtDelvNo.Text = ""

        btnSave.Select()


    End Sub

 
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click

        Dim Ulang As Integer = DGV.Rows.Count - 1

        For n = 0 To Ulang

            If n > Ulang Then Exit For

            If DGV.Rows(n).Cells(11).Value = True Then

                Query = "DELETE FROM [" & TMPFILEGR & "] WHERE [barcode] LIKE '%" & DGV.Rows(n).Cells(2).Value & "'"

                CMD = New SqlCommand
                CMD.Connection = Conn
                CMD.CommandText = Query
                CMD.ExecuteNonQuery()

                Me.DGV.Rows.RemoveAt(n)
                Ulang = Ulang - 1
                n = n - 1

            End If

        Next

        Call Load_Data()

    End Sub
End Class