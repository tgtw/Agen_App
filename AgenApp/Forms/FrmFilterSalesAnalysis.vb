Public Class FrmFilterSalesAnalysis

    Private Sub FrmFilterSalesAnalysis_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub btnFilter_Click(sender As Object, e As EventArgs) Handles btnFilter.Click

        If dtpDate1.Value > dtpDate2.Value Then
            MsgBox(" Invalid date ! ", MsgBoxStyle.Critical, "Warning")
            dtpDate2.Select()
            Exit Sub
        End If

        FilterOkay = True
        Me.Hide()

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        FilterOkay = False
        Me.Hide()
    End Sub
End Class