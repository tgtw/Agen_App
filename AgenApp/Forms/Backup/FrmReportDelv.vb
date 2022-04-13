Imports Microsoft.Reporting.WinForms

Public Class FrmReportDelv

    Public DELV_NO As String = ""
    Public SHIP_TO As String = ""

    Private Sub FrmReportDelv_Load(sender As Object, e As EventArgs) Handles MyBase.Load       

        Dim IPNUM As String = GetIniValue("LOCAL", "IPN", My.Application.Info.DirectoryPath & "\SysInfo.ini")
        Dim DBNAME As String = GetIniValue("LOCAL", "DBN", My.Application.Info.DirectoryPath & "\SysInfo.ini")

        DataTable3TableAdapter.Connection.ConnectionString = "Data Source=" & IPNUM & ";" & _
                                                             "Initial Catalog=" & DBNAME & ";" & _
                                                             "User=sa;Password=tki3c134"

        Dim Param1 As New ReportParameter("CompName", CONAME)
        Dim Param2 As New ReportParameter("CompAddress", COADDR)
        Dim Param3 As New ReportParameter("ShipTo", SHIP_TO)

        RV.LocalReport.SetParameters(Param1)
        RV.LocalReport.SetParameters(Param2)
        RV.LocalReport.SetParameters(Param3)

        Me.DataTable3TableAdapter.Fill(Me.DataSet3.DataTable3, DELV_NO)

        Me.RV.RefreshReport()

    End Sub
End Class