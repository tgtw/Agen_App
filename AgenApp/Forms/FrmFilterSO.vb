Public Class FrmFilterSO

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        FilterOKay = False
        Me.Hide()
    End Sub

    Private Sub btnFilter_Click(sender As Object, e As EventArgs) Handles btnFilter.Click
        FilterOKay = True
        Me.Hide()
    End Sub

    Private Sub FrmFilterSO_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class