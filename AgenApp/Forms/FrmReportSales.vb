Imports System.Data.SqlClient

Public Class FrmReportSales

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        MDIMain.ToolStrip.Visible = True
        Me.Dispose()
    End Sub

    Private Sub btnPreview_Click(sender As Object, e As EventArgs) Handles btnPreview.Click

        TAXRATE = TAX_RATE(dtpDate1.Value)

        Call LOCAL_CONNECT()


        Query = "DELETE FROM [TRANSLSD] WHERE [QTY2]=0 "

        CMD = New SqlCommand
        CMD.Connection = Conn
        CMD.CommandText = Query
        CMD.ExecuteNonQuery()


        Query = "UPDATE [TRANSLSD] SET [SO_DATE]=SLSH.SO_DATE " & _
                  "FROM (SELECT SO_NO,SO_DATE FROM [TRANSLSH]) SLSH " & _
                 "WHERE SLSH.SO_NO = TRANSLSD.SO_NO "

        CMD = New SqlCommand
        CMD.Connection = Conn
        CMD.CommandText = Query
        CMD.ExecuteNonQuery()


        Query = "UPDATE [TRANSLSD] SET [TAX]=SLSH.TAX " & _
                  "FROM (SELECT SO_NO,TAX FROM [TRANSLSH]) SLSH " & _
                 "WHERE SLSH.SO_NO = TRANSLSD.SO_NO " & _
                   "AND TRANSLSD.SO_DATE BETWEEN '" & Format(dtpDate1.Value, "yyyy-MM-dd") & "' AND '" & Format(dtpDate2.Value, "yyyy-MM-dd") & "'"

        CMD = New SqlCommand
        CMD.Connection = Conn
        CMD.CommandText = Query
        CMD.ExecuteNonQuery()


        Query = "UPDATE [TRANSLSD] SET [TAX]='Included' WHERE [TAX] IS NULL "

        CMD = New SqlCommand
        CMD.Connection = Conn
        CMD.CommandText = Query
        CMD.ExecuteNonQuery()


        Query = "UPDATE [TRANSLSD] " & _
                   "SET [DPP]=CASE WHEN RTRIM(TAX)='Included' THEN (QTY2*PRICE*100/(100+" & TAXRATE & ")) ELSE (QTY2*PRICE) END " & _
                 "WHERE [SO_DATE] BETWEEN '" & Format(dtpDate1.Value, "yyyy-MM-dd") & "' AND '" & Format(dtpDate2.Value, "yyyy-MM-dd") & "'"

        CMD = New SqlCommand
        CMD.Connection = Conn
        CMD.CommandText = Query
        CMD.ExecuteNonQuery()


        Query = "UPDATE [TRANSLSD] " & _
                   "SET [PPN]=(CASE WHEN RTRIM(TAX)='Included' THEN (QTY2*PRICE*100/(100+" & TAXRATE & ")) ELSE (QTY2*PRICE) END) * " & TAXRATE & "/100 " & _
                 "WHERE [SO_DATE] BETWEEN '" & Format(dtpDate1.Value, "yyyy-MM-dd") & "' AND '" & Format(dtpDate2.Value, "yyyy-MM-dd") & "'"

        CMD = New SqlCommand
        CMD.Connection = Conn
        CMD.CommandText = Query
        CMD.ExecuteNonQuery()


        Query = "UPDATE [TRANSLSD] SET [JUMLAH]=[DPP]+[PPN] "

        CMD = New SqlCommand
        CMD.Connection = Conn
        CMD.CommandText = Query
        CMD.ExecuteNonQuery()

   

        Query = "SELECT   TRANSLSH.SO_NO, TRANSLSH.SO_DATE, TRANSLSH.CUST_NO, TRANSLSH.CUST_NAME, TRANSLSH.DELV_NO, TRANSLSH.INV_NO, " & _
                         "TRANSLSD.ITEM_NO, TRANSLSD.KDPROD, TRANSLSD.NMPROD, TRANSLSD.QTY2, TRANSLSD.UNIT, TRANSLSD.PRICE, " & _
                         "ROUND(TRANSLSD.QTY2 * TRANSLSD.PRICE * 100 / (100+" & TAXRATE & "), 2) AS DPP, ROUND(TRANSLSD.QTY2 * TRANSLSD.PRICE / 11, 2) AS PPN, " & _
                         "(TRANSLSD.QTY2 * TRANSLSD.PRICE) AS JUMLAH " & _
                "FROM     [TRANSLSH] INNER JOIN [TRANSLSD] ON TRANSLSH.SO_NO = TRANSLSD.SO_NO " & _
                "WHERE    TRANSLSH.SO_DATE BETWEEN '" & Format(dtpDate1.Value, "yyyy-MM-dd") & "' AND " & _
                                                  "'" & Format(dtpDate2.Value, "yyyy-MM-dd") & "' AND " & _
                         "TRANSLSH.CUST_NAME LIKE '%" & txtCust.Text & "%' "

        If Trim(txtNo1.Text) <> "" Then
            Query = Query & "AND TRANSLSH.SO_NO >= '" & Trim(txtNo1.Text) & "' "
        End If

        If Trim(txtNo2.Text) <> "" Then
            Query = Query & "AND TRANSLSH.SO_NO <= '" & Trim(txtNo2.Text) & "' "
        End If


        Query = Query & "ORDER BY TRANSLSH.SO_NO, CAST(TRANSLSD.ITEM_NO AS INT) "


        Dim PRD As String = Format(dtpDate1.Value, "dd.MM.yyyy") & " - " & Format(dtpDate2.Value, "dd.MM.yyyy")

        Dim PrintControl As New ClassPrintReport

        PrintControl.CetakReport("RptSalesDetail.rdlc", "DataSet5", Query, PRD, TAXRATE)




    End Sub

    Private Sub FrmReportSales_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Call GET_PERIOD()

        Dim FromDate As Date = DateValue(CurYearPeriode & "-" & CurMonPeriode & "-01")

        txtNo1.Text = ""
        txtNo2.Text = ""
        dtpDate1.Value = FromDate
        dtpDate2.Value = TglAkhirBulan(FromDate)
        txtCust.Text = ""

    End Sub

End Class