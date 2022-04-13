Imports System.Data.SqlClient
Imports Microsoft.Reporting.WinForms

Public Class ClassPrintReport

    Public Sub CetakReport(ByVal ReportFile As String, ByVal DataSource As String, ByVal strSQL As String, ByVal PARAINFO As String, ByVal PPN As Double)

        Dim Param1 As ReportParameter
        Dim Param2 As ReportParameter
        Dim Param3 As ReportParameter
        Dim Param4 As ReportParameter

        Param1 = New ReportParameter("Parameter1", CONAME)
        Param2 = New ReportParameter("Parameter2", COADDR)
        Param3 = New ReportParameter("Parameter3", PARAINFO)
        Param4 = New ReportParameter("ParamTaxRate", PPN)

        Call LOCAL_CONNECT()

        Dim CMD As New SqlCommand(strSQL, Conn)
        Dim DR = CMD.ExecuteReader()
        Dim DT As New DataTable

        DT.Load(DR)
        'DR.Close()

        Dim FormCetak = New FrmReportPreview

        FormCetak.RptViewer.LocalReport.DataSources.Add(New ReportDataSource(DataSource, DT))
        FormCetak.RptViewer.LocalReport.ReportPath = Application.StartupPath & "\Reports\" & ReportFile

        FormCetak.RptViewer.LocalReport.SetParameters(Param1)
        FormCetak.RptViewer.LocalReport.SetParameters(Param2)
        FormCetak.RptViewer.LocalReport.SetParameters(Param3)
        FormCetak.RptViewer.LocalReport.SetParameters(Param4)

        FormCetak.RptViewer.SetDisplayMode(DisplayMode.PrintLayout)
        FormCetak.RptViewer.ZoomMode = ZoomMode.Percent
        FormCetak.RptViewer.ZoomPercent = 100
        FormCetak.RptViewer.PrinterSettings.DefaultPageSettings.PaperSize = New Printing.PaperSize("Custom", 850, 1100)
        FormCetak.RptViewer.PrinterSettings.DefaultPageSettings.Landscape = False
        FormCetak.RptViewer.RefreshReport()

        FormCetak.ShowDialog()

        DR.Close()

    End Sub


End Class


