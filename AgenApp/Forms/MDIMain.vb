Imports System.Windows.Forms
Imports System.Data.SqlClient
Imports System.Globalization

Public Class MDIMain

    Private Sub MDIMain_Activated(sender As Object, e As EventArgs) Handles Me.Activated

    End Sub

    Private Sub MDIMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Application.CurrentCulture = New CultureInfo("ID-ID")

        Dim mayor As Integer = 0
        Dim minor As Integer = 0
        Dim osInfo As System.OperatingSystem = System.Environment.OSVersion

        mayor = osInfo.Version.Major.ToString
        minor = osInfo.Version.Minor.ToString

        If mayor >= 6 Then '>= 6.2 windows 8 ke atas
            If minor >= 2 Then
                Application.CurrentCulture = New CultureInfo("EN-ID")
            Else
                Application.CurrentCulture = New CultureInfo("ID-ID")
            End If
        Else
            Application.CurrentCulture = New CultureInfo("ID-ID")
        End If


        Me.statuslbl1.Text = ""
        Me.statuslbl2.Text = ""
        Me.statuslbl3.Text = ""
        Me.statuslbl4.Text = ""

        Call LogToSAP(False)

        If SAPConStatus = False Then
            Me.Close()
        End If


        Select Case Mid(USNAM, 1, 2)
            Case Is = "A1"
                DBNAME = "PMKA"
            Case Is = "A2"
                DBNAME = "SACN"
            Case Is = "A3"
                DBNAME = "SAVL"
            Case Is = "A4"
                DBNAME = "CAKK"
            Case Is = "A5"
                DBNAME = "PBMI"
            Case Is = "A6"
                DBNAME = "TBMS"
            Case Else

Ulang:
                Dim str As String = InputBox(" Please Enter Company Code : ", "Company Code")

                If Trim(str) = "" Then
                    Me.Close()
                End If

                DBNAME = UCase(Trim(str))

                If InStr("PMKA/SACN/SAVL/CAKK/PBMI/TBMS", DBNAME) = 0 Then
                    GoTo Ulang
                End If

        End Select



        Call LOCAL_CONNECT()

        If ConnectStatus = True Then

            Query = "SELECT * FROM [SAPCFG] "

            DS = New DataSet
            DA = New SqlDataAdapter(Query, Conn)
            DA.Fill(DS, "SAPCFG")

            If DS.Tables("SAPCFG").Rows.Count > 0 Then
                COCODE = DS.Tables("SAPCFG").Rows(0)("COMP_CODE")
                CONAME = DS.Tables("SAPCFG").Rows(0)("COMP_NAME")
                COADDR = DS.Tables("SAPCFG").Rows(0)("COMP_ADDR")
                PLANT = DS.Tables("SAPCFG").Rows(0)("PLANT")
                PGROUP = DS.Tables("SAPCFG").Rows(0)("PUR_GROUP")
                DOCTYPE = DS.Tables("SAPCFG").Rows(0)("DOC_TYPE")
                SLOC = PLANT
            End If

        End If


        'Me.statuslbl4.Text = IPNUM
        Me.statuslbl3.Text = USNAM
        Me.statuslbl2.Text = PLANT




    End Sub

    Private Sub mnuMasterProduct_Click(sender As Object, e As EventArgs) Handles mnuMasterProduct.Click
        ToolStrip.Visible = False
        With FrmProductList
            .MdiParent = Me
            .Show()
        End With
    End Sub

    Private Sub mnuTransPurchasePO_Click(sender As Object, e As EventArgs) Handles mnuTransPurchasePO.Click
        ToolStrip.Visible = False
        With FrmPOList
            .MdiParent = Me
            .Show()
        End With
    End Sub

    Private Sub mnuTransPurchaseGR_Click(sender As Object, e As EventArgs) Handles mnuTransPurchaseGR.Click
        ToolStrip.Visible = False
        With FrmGRList
            .MdiParent = Me
            .Show()
        End With
    End Sub

    Private Sub mnuTransSalesReceipt_Click(sender As Object, e As EventArgs) Handles mnuTransSalesReceipt.Click
        ToolStrip.Visible = False
        With FrmSalesReceiptList
            .MdiParent = Me
            .Show()
        End With
    End Sub

    Private Sub tsbPurchaseOrder_Click(sender As Object, e As EventArgs) Handles tsbPurchaseOrder.Click
        ToolStrip.Visible = False
        With FrmPOList
            .MdiParent = Me
            .Show()
        End With
    End Sub


    Private Sub tsbGoodsReceipt_Click(sender As Object, e As EventArgs) Handles tsbGoodsReceipt.Click
        ToolStrip.Visible = False
        With FrmGRList
            .MdiParent = Me
            .Show()
        End With
    End Sub

    Private Sub tsbSalesReceipt_Click(sender As Object, e As EventArgs) Handles tsbSalesReceipt.Click
        ToolStrip.Visible = False
        With FrmSalesReceiptList
            .MdiParent = Me
            .Show()
        End With
    End Sub

    Private Sub tsbExit_Click(sender As Object, e As EventArgs) Handles tsbExit.Click
        Me.Close()
    End Sub

    Private Sub mnuTransSalesInvoice_Click(sender As Object, e As EventArgs) Handles mnuTransSalesInvoice.Click
        ToolStrip.Visible = False
        With FrmSalesInvoiceList
            .MdiParent = Me
            .Show()
        End With
    End Sub

    Private Sub tsbSalesInvoice_Click(sender As Object, e As EventArgs) Handles tsbSalesInvoice.Click
        ToolStrip.Visible = False
        With FrmSalesInvoiceList
            .MdiParent = Me
            .Show()
        End With
    End Sub

    Private Sub tsbProduct_Click(sender As Object, e As EventArgs) Handles tsbProduct.Click
        ToolStrip.Visible = False
        With FrmProductList
            .MdiParent = Me
            .Show()
        End With
    End Sub

    Private Sub mnuRptStock_Click(sender As Object, e As EventArgs) Handles mnuRptStock.Click
        ToolStrip.Visible = False
        With FrmStockList
            .MdiParent = Me
            .Show()
        End With
    End Sub


    Private Sub SrappingToolStripMenuItem_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub tsbStockReport_Click(sender As Object, e As EventArgs) Handles tsbStockReport.Click
        ToolStrip.Visible = False
        With FrmStockList
            .MdiParent = Me
            .Show()
        End With
    End Sub

    Private Sub mnuMasterCustomer_Click(sender As Object, e As EventArgs) Handles mnuMasterCustomer.Click
        ToolStrip.Visible = False
        With FrmCustomerList
            .MdiParent = Me
            .Show()
        End With
    End Sub

    Private Sub tsbCustomer_Click(sender As Object, e As EventArgs) Handles tsbCustomer.Click
        ToolStrip.Visible = False
        With FrmCustomerList
            .MdiParent = Me
            .Show()
        End With
    End Sub


    Private Sub mnuRptSales_Click(sender As Object, e As EventArgs) Handles mnuRptSales.Click
        ToolStrip.Visible = False
        With FrmReportSales
            .MdiParent = Me
            .Show()
        End With
    End Sub

    Private Sub MDIMain_Shown(sender As Object, e As EventArgs) Handles Me.Shown

    End Sub

 
    Private Sub mnuRptStockCard_Click(sender As Object, e As EventArgs) Handles mnuRptStockCard.Click
        ToolStrip.Visible = False
        With FrmStockCard
            .MdiParent = Me
            .Show()
        End With
    End Sub

    Private Sub mnuRptSalesAnalysis_Click(sender As Object, e As EventArgs) Handles mnuRptSalesAnalysis.Click
        ToolStrip.Visible = False
        With FrmSalesAnalysis
            .MdiParent = Me
            .Show()
        End With
    End Sub

    Private Sub mnuGoodsIssueSample_Click(sender As Object, e As EventArgs) Handles mnuGoodsIssueSample.Click
        ToolStrip.Visible = False
        With FrmGoodsIssue
            .MdiParent = Me
            .Show()
        End With
    End Sub

    Private Sub mnuRptPurchaseDetail_Click(sender As Object, e As EventArgs) Handles mnuRptPurchaseDetail.Click
        ToolStrip.Visible = False
        With FrmReportPurchase
            .MdiParent = Me
            .Show()
        End With
    End Sub
End Class
