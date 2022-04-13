Public Class FrmCompany

    Private Sub FrmCompany_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        With cboCompany
            .Items.Clear()
            .Items.Add("PMKA")
            .Items.Add("SACN")
            .Items.Add("SAVL")
            .Items.Add("CAKK")
            .SelectedIndex = 0
        End With

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        DBNAME = ""
        Me.Dispose()
    End Sub

    Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click
        DBNAME = cboCompany.Text
        Me.Dispose()
    End Sub
End Class