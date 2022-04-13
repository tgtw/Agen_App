Public Class FrmReportSales

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        FilterOKay = False
        Me.Hide()
    End Sub

    Private Sub btnFilter_Click(sender As Object, e As EventArgs) Handles btnFilter.Click
        FilterOKay = True
        Me.Hide()
    End Sub
End Class