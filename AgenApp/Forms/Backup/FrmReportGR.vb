Imports Microsoft.Reporting.WinForms

Public Class FrmReportGR

    Public MATDOC As String = ""

    Private Sub FrmReportGR_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim IPNUM As String = GetIniValue("LOCAL", "IPN", My.Application.Info.DirectoryPath & "\SysInfo.ini")
        Dim DBNAME As String = GetIniValue("LOCAL", "DBN", My.Application.Info.DirectoryPath & "\SysInfo.ini")

        DataTable1TableAdapter.Connection.ConnectionString = "Data Source=" & IPNUM & ";" & _
                                                             "Initial Catalog=" & DBNAME & ";" & _
                                                             "User=sa;Password=tki3c134"

        Dim Param1 As New ReportParameter("CompName", CONAME)
        Dim Param2 As New ReportParameter("CompAddress", COADDR)

        RV.LocalReport.SetParameters(Param1)
        RV.LocalReport.SetParameters(Param2)


 
        Try
            Me.DataTable1TableAdapter.Fill(Me.DataSet1.DataTable1, MATDOC)
            Me.RV.RefreshReport()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try

    End Sub


End Class