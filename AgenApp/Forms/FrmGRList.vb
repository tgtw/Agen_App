Imports System.Data.SqlClient

Public Class FrmGRList

    Dim MATDOC As String = ""
    Dim SaveOkay As Boolean = False

    Private Sub BAPI_GOODSMVT_CREATE()

        Call LogToSAP(True)

        If SAPConStatus = False Then Exit Sub

 
    End Sub

    Private Sub Load_Data()

        Try

            Query = "SELECT grno,grdate,pono,podate,kdsupl,nmsupl,delvno,notes FROM [TRANGRH] " & _
                     "WHERE [grdate] BETWEEN '" & Format(FrmFilterGR.dtpGRdate1.Value, "yyyy-MM-dd") & "' " & _
                                        "AND '" & Format(FrmFilterGR.dtpGRdate2.Value, "yyyy-MM-dd") & "' "

            If FrmFilterGR.txtGRNo1.Text <> "" Then
                Query = Query & "AND [grno] >= '" & FrmFilterGR.txtGRNo1.Text & "' "
            End If

            If FrmFilterGR.txtGRNo2.Text <> "" Then
                Query = Query & "AND [grno] <= '" & FrmFilterGR.txtGRNo2.Text & "' "
            End If

            If FrmFilterGR.txtPO1.Text <> "" Then
                Query = Query & "AND [pono] >= '" & FrmFilterGR.txtPO1.Text & "' "
            End If

            If FrmFilterGR.txtPO2.Text <> "" Then
                Query = Query & "AND [pono] <= '" & FrmFilterGR.txtPO2.Text & "' "
            End If

            If FrmFilterGR.txtDelv1.Text <> "" Then
                Query = Query & "AND [delvno] >= '" & FrmFilterGR.txtDelv1.Text & "' "
            End If

            If FrmFilterGR.txtDelv2.Text <> "" Then
                Query = Query & "AND [delvno] <= '" & FrmFilterGR.txtDelv2.Text & "' "
            End If

            Query = Query & "ORDER BY [grno] "



            CMD = New SqlCommand
            DA = New SqlDataAdapter
            DT = New DataTable

            CMD.Connection = Conn
            CMD.CommandText = Query

            DA.SelectCommand = CMD
            DA.Fill(DT)

            With DGV

                .DataSource = DT

                .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
                .ColumnHeadersHeight = 40

                '.ColumnHeadersDefaultCellStyle.Font = New Font("Verdana", 9)

                .Columns(0).HeaderText = "GR NO"
                .Columns(1).HeaderText = "GR DATE"
                .Columns(2).HeaderText = "PO NO"
                .Columns(3).HeaderText = "PO DATE"
                .Columns(4).HeaderText = "VENDOR" & vbCrLf & "CODE"
                .Columns(5).HeaderText = "VENDOR" & vbCrLf & "NAME"
                .Columns(6).HeaderText = "DELIVERY" & vbCrLf & "NO"
                .Columns(7).HeaderText = "NOTES"

                .Columns(0).Width = 100
                .Columns(1).Width = 110
                .Columns(2).Width = 100
                .Columns(3).Width = 110
                .Columns(4).Width = 80
                .Columns(5).Width = 250
                .Columns(6).Width = 100
                .Columns(7).Width = 300

                .Columns(5).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

            End With

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            Conn.Close()
            DA.Dispose()
        End Try

    End Sub

    Private Sub FrmGRList_Activated(sender As Object, e As EventArgs) Handles Me.Activated

        Call UPLOAD_GR_MANUAL()

    End Sub

    Private Sub FrmGRList_Leave(sender As Object, e As EventArgs) Handles Me.Leave
        MDIMain.ToolStrip.Visible = True
    End Sub

    Private Sub FrmGRList_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Call LOCAL_CONNECT()

        If ConnectStatus = False Then Exit Sub

        Call GET_PERIOD()

        Dim FromDate As Date = DateValue(CurYearPeriode & "-" & CurMonPeriode & "-01")

        With FrmFilterGR
            .Show()
            .txtGRNo1.Text = ""
            .txtGRNo2.Text = ""
            .txtPO1.Text = ""
            .txtPO2.Text = ""
            .dtpGRdate1.Value = FromDate
            .dtpGRdate2.Value = Now   ' TglAkhirBulan(FromDate)
            .txtDelv1.Text = ""
            .txtDelv2.Text = ""
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
        Me.Close()
    End Sub

    Private Sub tsbNew_Click(sender As Object, e As EventArgs) Handles tsbNew.Click

        FrmGR.ShowDialog()

        Call Load_Data()

    End Sub

    Private Sub tsbPrintGRN_Click(sender As Object, e As EventArgs) Handles tsbPrintGRN.Click

        Dim SLOCINFO As String = ""

        Query = "SELECT  TRANGRH.GRNO, TRANGRH.GRDATE, TRANGRH.PONO, TRANGRH.PODATE, TRANGRH.NMSUPL, TRANGRH.DELVNO, TRANGRH.NOTES," & _
                "        TRANGRD.ITEMNO, TRANGRD.KDPROD, TRANGRD.NMPROD, TRANGRD.QTYRECV1, TRANGRD.QTYRECV2, TRANGRD.UNIT, TRANGRD.SLOC " & _
                "FROM    TRANGRD INNER JOIN TRANGRH ON TRANGRD.GRNO = TRANGRH.GRNO " & _
                "WHERE   TRANGRH.GRNO = '" & DGV.CurrentRow.Cells(0).Value.ToString & "'"

        DS = New DataSet
        DA = New SqlDataAdapter(Query, Conn)
        DA.Fill(DS, "GR")

        SLOCINFO = DS.Tables("GR").Rows(0)("SLOC")
        SLOCINFO = SLOCINFO & "-" & SLOC_INFO(SLOCINFO)

        Dim PrintControl As New ClassPrintReport

        PrintControl.CetakReport("RptGRNote.rdlc", "DataSet1", Query, SLOCINFO, 0)

    End Sub

    Private Sub tsbFilter_Click(sender As Object, e As EventArgs) Handles tsbFilter.Click
        FrmFilterGR.ShowDialog()
        If FilterOkay = True Then
            Call Load_Data()
        End If
    End Sub

    Private Sub tsbDisplay_Click(sender As Object, e As EventArgs) Handles tsbDisplay.Click
        With FrmGR
            .GR_NO = DGV.CurrentRow.Cells(0).Value
            .EditOrDisplay = Trim(tsbDisplay.Text)
            .ShowDialog()
        End With
    End Sub

    Private Sub tsbDelete_Click(sender As Object, e As EventArgs) Handles tsbDelete.Click

        Dim GR_NO As String = Trim(DGV.CurrentRow.Cells(0).Value)
        Dim PO_NO As String = Trim(DGV.CurrentRow.Cells(2).Value)
        Dim SJ_NO As String = Trim(DGV.CurrentRow.Cells(6).Value)
        Dim STATUS As String = Trim(DGV.CurrentRow.Cells(7).Value)

        If STATUS = "CANCEL" Then
            MessageBox.Show("This document already cancelled !", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If MessageBox.Show("Are you really want to delete " & vbCrLf & _
                           "Goods Receipt No. " & GR_NO & "  ? ", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = vbNo Then
            Exit Sub
        End If


        Call LOCAL_CONNECT()

        '==========================================================================
        ' Update table [TRANPOD], kolom [RECEIVED] di kurangi QTY GR yg di cancel
        '==========================================================================

        Query = "SELECT * FROM [TRANGRD] WHERE [GRNO]='" & GR_NO & "'"

        DS = New DataSet
        DA = New SqlDataAdapter(Query, Conn)
        DA.Fill(DS, "GRD")

        For n = 0 To DS.Tables("GRD").Rows.Count - 1

            Query = "UPDATE [TRANPOD] " & _
                       "SET [RECEIVED]=[RECEIVED]-" & Replace(DS.Tables("GRD").Rows(n)("QTYRECV2"), ",", ".") & " " & _
                     "WHERE [NOPO]='" & PO_NO & "' AND [KDPROD]='" & DS.Tables("GRD").Rows(n)("KDPROD") & "'"

            CMD = New SqlCommand
            CMD.Connection = Conn
            CMD.CommandText = Query
            CMD.ExecuteNonQuery()

        Next


        '================================================================
        ' Update table [TRANGRH], kolom [NOTES] di set status 'CANCEL' 
        '================================================================

        Query = "UPDATE [TRANGRH] SET [NOTES]='CANCEL' WHERE [GRNO]='" & GR_NO & "'"

        CMD = New SqlCommand
        CMD.Connection = Conn
        CMD.CommandText = Query
        CMD.ExecuteNonQuery()


        '=====================================================================
        ' Update table [TRANGRD], kolom [QTYRECV1], [QTYRECV2] di set jadi 0 
        '=====================================================================

        Query = "UPDATE [TRANGRD] SET [QTYRECV1]=0, [QTYRECV2]=0 WHERE [GRNO]='" & GR_NO & "'"

        CMD = New SqlCommand
        CMD.Connection = Conn
        CMD.CommandText = Query
        CMD.ExecuteNonQuery()


        '==========================================
        ' Hapus data barcode di table [STOCKBCD]   
        '==========================================

        Query = "DELETE FROM [STOCKBCD] WHERE [GR]='" & GR_NO & "'"

        CMD = New SqlCommand
        CMD.Connection = Conn
        CMD.CommandText = Query
        CMD.ExecuteNonQuery()


        '==========================================
        ' Hapus data barcode di table [STOCKCARD]   
        '==========================================

        Query = "DELETE FROM [STOCKCARD] WHERE [GR]='" & GR_NO & "'"

        CMD = New SqlCommand
        CMD.Connection = Conn
        CMD.CommandText = Query
        CMD.ExecuteNonQuery()


        DGV.CurrentRow.Cells(7).Value = "CANCEL"



    End Sub

    Private Sub tsbEdit_Click(sender As Object, e As EventArgs) Handles tsbEdit.Click

    End Sub

    Private Sub UPLOAD_GR_MANUAL()

        Dim CurrPeriod As Long = Val(CurYearPeriode & CurMonPeriode)
        Dim PrevPeriod As Long = Val(PrevYearPeriode & PrevMonPeriode)

        Dim PostDate As Long = Val(Format(Now, "yyyyMM"))
        Dim DOCNUM As String = ""


        Query = "SELECT * FROM [TRANGRH] WHERE [GRNO] LIKE 'M%' "

        DS = New DataSet
        DA = New SqlDataAdapter(Query, Conn)
        DA.Fill(DS, "GRH")

        If DS.Tables("GRH").Rows.Count = 0 Then
            Exit Sub
        End If

        MessageBox.Show(" Upload Manual Goods Receipt to SAP ! ", "Automatic Process", MessageBoxButtons.OK, MessageBoxIcon.Information)


        For n = 0 To DS.Tables("GRH").Rows.Count - 1

            DOCNUM = DS.Tables("GRH").Rows(n)("GRNO")

            PostDate = Val(Format(DS.Tables("GRH").Rows(n)("GRDATE"), "yyyyMM"))

            If PostDate = CurrPeriod Then

                '==================
                '  POSTING TO SAP
                '==================

                SaveOkay = False

                MATDOC = ""


                Dim oFuncCtrl As Object
                Dim oTheFunc As Object
                Dim oFuncCommit As Object

                Dim oHEADER As Object
                Dim oCODE As Object
                Dim oITEM As Object
                Dim oHEADRET As Object


                oFuncCtrl = CreateObject("SAP.Functions")
                oFuncCtrl.Connection = SAPConn

                oTheFunc = oFuncCtrl.Add("BAPI_GOODSMVT_CREATE")

                '===================
                ' IMPORT PARAMETERS 
                '===================

                oHEADER = oTheFunc.Exports.Item("GOODSMVT_HEADER")
                oHEADER.Value("PSTNG_DATE") = Format(DS.Tables("GRH").Rows(n)("GRDATE"), "yyyyMMdd")
                oHEADER.Value("DOC_DATE") = Format(DS.Tables("GRH").Rows(n)("GRDATE"), "yyyyMMdd")
                oHEADER.Value("REF_DOC_NO") = Trim(DS.Tables("GRH").Rows(n)("NOTES"))

                oCODE = oTheFunc.Exports.Item("GOODSMVT_CODE")
                oCODE.Value("GM_CODE") = "01"


                '========
                ' TABLES
                '========

                oITEM = oTheFunc.tables.Item("GOODSMVT_ITEM")


                Query = "SELECT * FROM [TRANGRD] WHERE [GRNO]='" & DOCNUM & "'"

                DS2 = New DataSet
                DA2 = New SqlDataAdapter(Query, Conn)
                DA2.Fill(DS2, "GRD")

                Dim i As Integer = 0

                For x = 0 To DS2.Tables("GRD").Rows.Count - 1

                    i = i + 1

                    oITEM.Rows.Add()
                    oITEM.Value(i, "MATERIAL") = DS2.Tables("GRD").Rows(x)("KDPROD")
                    oITEM.Value(i, "PLANT") = PLANT
                    oITEM.Value(i, "STGE_LOC") = SLOC
                    oITEM.Value(i, "MOVE_TYPE") = "101"
                    oITEM.Value(i, "VENDOR") = Format(Val(DS.Tables("GRH").Rows(n)("KDSUPL")), "0000000000")
                    oITEM.Value(i, "ENTRY_QNT") = DS2.Tables("GRD").Rows(x)("QTYRECV2")
                    oITEM.Value(i, "ENTRY_UOM") = DS2.Tables("GRD").Rows(x)("UNIT")
                    oITEM.Value(i, "PO_NUMBER") = Format(Val(DS.Tables("GRH").Rows(n)("PONO")), "0000000000")
                    oITEM.Value(i, "PO_ITEM") = DS2.Tables("GRD").Rows(x)("ITEMNO")
                    oITEM.Value(i, "ITEM_TEXT") = DS.Tables("GRH").Rows(n)("DELVNO")
                    oITEM.Value(i, "MVT_IND") = "B"

                Next x


                If oTheFunc.Call Then

                    oHEADRET = oTheFunc.Imports.Item("GOODSMVT_HEADRET")

                    MATDOC = oHEADRET.Value("MAT_DOC")

                    If Trim(MATDOC) <> "" Then

                        oFuncCommit = oFuncCtrl.Add("BAPI_TRANSACTION_COMMIT")

                        If oFuncCommit.Call Then

                            MessageBox.Show("Material document no. " & MATDOC & " created !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)

                            SaveOkay = True

                        End If

                    End If

                End If


                oFuncCtrl = Nothing
                oTheFunc = Nothing
                oFuncCommit = Nothing

                oHEADER = Nothing
                oCODE = Nothing
                oITEM = Nothing
                oHEADER = Nothing


                If SaveOkay = True Then

                    Call LOCAL_CONNECT()

                    Query = "UPDATE [TRANGRH] SET [GRNO]='" & MATDOC & "', [NOTES]='" & DOCNUM & "' WHERE [GRNO]='" & DOCNUM & "'"

                    CMD = New SqlCommand
                    CMD.Connection = Conn
                    CMD.CommandText = Query
                    CMD.ExecuteNonQuery()


                    Query = "UPDATE [TRANGRD] SET [GRNO]='" & MATDOC & "' WHERE [GRNO]='" & DOCNUM & "'"

                    CMD = New SqlCommand
                    CMD.Connection = Conn
                    CMD.CommandText = Query
                    CMD.ExecuteNonQuery()

                End If


            End If

        Next n

        Call Load_Data()


    End Sub

End Class