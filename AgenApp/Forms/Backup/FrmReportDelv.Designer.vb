<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmReportDelv
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim ReportDataSource1 As Global.Microsoft.Reporting.WinForms.ReportDataSource = New Global.Microsoft.Reporting.WinForms.ReportDataSource()
        Me.RV = New Global.Microsoft.Reporting.WinForms.ReportViewer()
        Me.DataTable3BindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DataSet3 = New AgenApp.DataSet3()
        Me.DataTable3TableAdapter = New AgenApp.DataSet3TableAdapters.DataTable3TableAdapter()
        CType(Me.DataTable3BindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataSet3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RV
        '
        Me.RV.Dock = System.Windows.Forms.DockStyle.Fill
        ReportDataSource1.Name = "DataSet3"
        ReportDataSource1.Value = Me.DataTable3BindingSource
        Me.RV.LocalReport.DataSources.Add(ReportDataSource1)
        Me.RV.LocalReport.ReportEmbeddedResource = "AgenApp.RptDelvNote.rdlc"
        Me.RV.Location = New System.Drawing.Point(0, 0)
        Me.RV.Name = "RV"
        Me.RV.Size = New System.Drawing.Size(922, 650)
        Me.RV.TabIndex = 0
        '
        'DataTable3BindingSource
        '
        Me.DataTable3BindingSource.DataMember = "DataTable3"
        Me.DataTable3BindingSource.DataSource = Me.DataSet3
        '
        'DataSet3
        '
        Me.DataSet3.DataSetName = "DataSet3"
        Me.DataSet3.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'DataTable3TableAdapter
        '
        Me.DataTable3TableAdapter.ClearBeforeFill = True
        '
        'FrmReportDelv
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(922, 650)
        Me.Controls.Add(Me.RV)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "FrmReportDelv"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Delivery Note"
        CType(Me.DataTable3BindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataSet3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RV As Global.Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents DataTable3BindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DataSet3 As AgenApp.DataSet3
    Friend WithEvents DataTable3TableAdapter As AgenApp.DataSet3TableAdapters.DataTable3TableAdapter
End Class
