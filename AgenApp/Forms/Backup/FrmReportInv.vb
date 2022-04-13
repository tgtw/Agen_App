Imports Microsoft.Reporting.WinForms

Public Class FrmReportInv

    Public INV_NO As String = ""
    Public CUSTINFO As String = ""

    Private Sub FrmReportInv_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim IPNUM As String = GetIniValue("LOCAL", "IPN", My.Application.Info.DirectoryPath & "\SysInfo.ini")
        Dim DBNAME As String = GetIniValue("LOCAL", "DBN", My.Application.Info.DirectoryPath & "\SysInfo.ini")

        DataTable2TableAdapter.Connection.ConnectionString = "Data Source=" & IPNUM & ";" & _
                                                             "Initial Catalog=" & DBNAME & ";" & _
                                                             "User=sa;Password=tki3c134"

        Dim Param1 As New ReportParameter("CompName", CONAME)
        Dim Param2 As New ReportParameter("CompAddress", COADDR)
        Dim Param3 As New ReportParameter("Customer", CUSTINFO)

        RV.LocalReport.SetParameters(Param1)
        RV.LocalReport.SetParameters(Param2)
        RV.LocalReport.SetParameters(Param3)


        Me.DataTable2TableAdapter.Fill(Me.DataSet2.DataTable2, INV_NO)

        Me.RV.RefreshReport()


    End Sub
End Class