Imports System.Data.SqlClient

Public Class FrmReportPurchase

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        MDIMain.ToolStrip.Visible = True
        Me.Dispose()
    End Sub

    Private Sub btnPreview_Click(sender As Object, e As EventArgs) Handles btnPreview.Click

        TAXRATE = TAX_RATE(dtpDate1.Value)

        Call LOCAL_CONNECT()

        Query = "UPDATE [TRANGRD] SET PONO=GRH.PONO, GRDATE=GRH.GRDATE " & _
                  "FROM (SELECT GRNO,GRDATE,PONO FROM [TRANGRH]) GRH " & _
                 "WHERE GRH.GRNO = TRANGRD.GRNO "

        CMD = New SqlCommand
        CMD.Connection = Conn
        CMD.CommandText = Query
        CMD.ExecuteNonQuery()


        Query = "UPDATE [TRANGRD] SET PRICE=POD.PRICE " & _
                  "FROM (SELECT NOPO,KDPROD,PRICE FROM [TRANPOD]) POD " & _
                 "WHERE POD.NOPO = TRANGRD.PONO AND POD.KDPROD = TRANGRD.KDPROD "

        CMD = New SqlCommand
        CMD.Connection = Conn
        CMD.CommandText = Query
        CMD.ExecuteNonQuery()


        Query = "UPDATE [TRANGRD] SET TAX=POH.TAX " & _
                  "FROM (SELECT NOPO,TAX FROM [TRANPOH]) POH " & _
                 "WHERE POH.NOPO = TRANGRD.PONO "

        CMD = New SqlCommand
        CMD.Connection = Conn
        CMD.CommandText = Query
        CMD.ExecuteNonQuery()


        Query = "UPDATE [TRANGRD] SET TAX='Included' WHERE TAX IS NULL "

        CMD = New SqlCommand
        CMD.Connection = Conn
        CMD.CommandText = Query
        CMD.ExecuteNonQuery()


        Query = "UPDATE [TRANGRD] " & _
                   "SET [DPP]=CASE WHEN RTRIM(TAX)='Included' THEN (PRICE*QTYRECV2*100/(100+" & TAXRATE & ")) ELSE (PRICE*QTYRECV2) END " & _
                 "WHERE [GRDATE] BETWEEN '" & Format(dtpDate1.Value, "yyyy-MM-dd") & "' AND '" & Format(dtpDate2.Value, "yyyy-MM-dd") & "'"

        CMD = New SqlCommand
        CMD.Connection = Conn
        CMD.CommandText = Query
        CMD.ExecuteNonQuery()


        Query = "UPDATE [TRANGRD] " & _
                   "SET [PPN]=(CASE WHEN RTRIM(TAX)='Included' THEN (PRICE*QTYRECV2*100/(100+" & TAXRATE & ")) ELSE (PRICE*QTYRECV2) END) * " & TAXRATE & "/100 " & _
                 "WHERE [GRDATE] BETWEEN '" & Format(dtpDate1.Value, "yyyy-MM-dd") & "' AND '" & Format(dtpDate2.Value, "yyyy-MM-dd") & "'"

        CMD = New SqlCommand
        CMD.Connection = Conn
        CMD.CommandText = Query
        CMD.ExecuteNonQuery()


        Query = "UPDATE [TRANGRD] SET [JUMLAH]=(DPP+PPN) "

        CMD = New SqlCommand
        CMD.Connection = Conn
        CMD.CommandText = Query
        CMD.ExecuteNonQuery()




        Query = "SELECT  TRANGRH.GRNO, TRANGRH.GRDATE, TRANGRH.PONO, TRANGRH.PODATE, TRANGRH.KDSUPL, TRANGRH.NMSUPL, TRANGRH.DELVNO, " & _
                        "TRANGRH.NOTES, TRANGRD.ITEMNO, TRANGRD.KDPROD, TRANGRD.NMPROD, TRANGRD.PRICE, TRANGRD.QTYRECV1, TRANGRD.QTYRECV2, " & _
                        "TRANGRD.UNIT, TRANGRD.TAX, TRANGRD.SLOC, TRANGRD.DPP, TRANGRD.PPN, TRANGRD.JUMLAH " & _
                  "FROM  TRANGRH INNER JOIN TRANGRD ON TRANGRD.GRNO = TRANGRH.GRNO " & _
                 "WHERE  TRANGRH.GRDATE BETWEEN '" & Format(dtpDate1.Value, "yyyy-MM-dd") & "' AND '" & Format(dtpDate2.Value, "yyyy-MM-dd") & "' " & _
                   "AND  TRANGRH.NMSUPL LIKE '%" & txtVendor.Text & "%' "

        If Trim(txtNo1.Text) <> "" Then
            Query = Query & "AND TRANGRH.GRNO >= '" & Trim(txtNo1.Text) & "' "
        End If

        If Trim(txtNo2.Text) <> "" Then
            Query = Query & "AND TRANGRH.GRNO <= '" & Trim(txtNo2.Text) & "' "
        End If

        Query = Query & "ORDER BY TRANGRH.GRNO, CAST(TRANGRD.ITEMNO AS INT) "



        Dim PRD As String = Format(dtpDate1.Value, "dd.MM.yyyy") & " - " & Format(dtpDate2.Value, "dd.MM.yyyy")

        Dim PrintControl As New ClassPrintReport

        PrintControl.CetakReport("RptPurchaseDetail.rdlc", "DataSet9", Query, PRD, TAXRATE)


    End Sub

    Private Sub FrmReportPurchase_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Call GET_PERIOD()

        Dim FromDate As Date = DateValue(CurYearPeriode & "-" & CurMonPeriode & "-01")

        txtNo1.Text = ""
        txtNo2.Text = ""
        dtpDate1.Value = FromDate
        dtpDate2.Value = TglAkhirBulan(FromDate)
        txtVendor.Text = ""

 

    End Sub

    Private Sub txtVendor_TextChanged(sender As Object, e As EventArgs) Handles txtVendor.TextChanged

    End Sub
End Class