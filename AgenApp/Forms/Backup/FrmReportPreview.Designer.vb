<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmReportPreview
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
        Dim ReportDataSource1 As Microsoft.Reporting.WinForms.ReportDataSource = New Microsoft.Reporting.WinForms.ReportDataSource()
        Me.RptViewer = New Microsoft.Reporting.WinForms.ReportViewer()
        Me.DataSet5 = New AgenApp.DataSet5()
        Me.DataTable5BindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DataTable5TableAdapter = New AgenApp.DataSet5TableAdapters.DataTable5TableAdapter()
        CType(Me.DataSet5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataTable5BindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RptViewer
        '
        Me.RptViewer.Dock = System.Windows.Forms.DockStyle.Fill
        ReportDataSource1.Name = "DataSet5"
        ReportDataSource1.Value = Me.DataTable5BindingSource
        Me.RptViewer.LocalReport.DataSources.Add(ReportDataSource1)
        Me.RptViewer.LocalReport.ReportEmbeddedResource = "AgenApp.RptSalesDetailList.rdlc"
        Me.RptViewer.Location = New System.Drawing.Point(0, 0)
        Me.RptViewer.Name = "RptViewer"
        Me.RptViewer.Size = New System.Drawing.Size(988, 699)
        Me.RptViewer.TabIndex = 0
        '
        'DataSet5
        '
        Me.DataSet5.DataSetName = "DataSet5"
        Me.DataSet5.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'DataTable5BindingSource
        '
        Me.DataTable5BindingSource.DataMember = "DataTable5"
        Me.DataTable5BindingSource.DataSource = Me.DataSet5
        '
        'DataTable5TableAdapter
        '
        Me.DataTable5TableAdapter.ClearBeforeFill = True
        '
        'FrmReportPreview
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(988, 699)
        Me.Controls.Add(Me.RptViewer)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "FrmReportPreview"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Report Preview"
        CType(Me.DataSet5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataTable5BindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RptViewer As Global.Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents DataTable5BindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DataSet5 As AgenApp.DataSet5
    Friend WithEvents DataTable5TableAdapter As AgenApp.DataSet5TableAdapters.DataTable5TableAdapter
End Class
