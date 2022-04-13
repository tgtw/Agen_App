Public Class FrmReportPreview

    Private Sub FrmReportPreview_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.RptViewer.RefreshReport()
    End Sub
End Class