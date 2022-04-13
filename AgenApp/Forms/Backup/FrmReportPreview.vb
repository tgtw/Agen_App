Public Class FrmReportPreview

    Private Sub FrmPrintPreview_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'DataSet5.DataTable5' table. You can move, or remove it, as needed.
        Me.DataTable5TableAdapter.Fill(Me.DataSet5.DataTable5)

        Me.RptViewer.RefreshReport()

    End Sub
End Class