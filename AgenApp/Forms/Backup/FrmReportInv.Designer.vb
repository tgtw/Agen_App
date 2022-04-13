<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmReportInv
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
        Dim ReportDataSource2 As Global.Microsoft.Reporting.WinForms.ReportDataSource = New Global.Microsoft.Reporting.WinForms.ReportDataSource()
        Me.DataTable2BindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DataSet2 = New AgenApp.DataSet2()
        Me.RV = New Global.Microsoft.Reporting.WinForms.ReportViewer()
        Me.DataTable2TableAdapter = New AgenApp.DataSet2TableAdapters.DataTable2TableAdapter()
        CType(Me.DataTable2BindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataSet2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataTable2BindingSource
        '
        Me.DataTable2BindingSource.DataMember = "DataTable2"
        Me.DataTable2BindingSource.DataSource = Me.DataSet2
        '
        'DataSet2
        '
        Me.DataSet2.DataSetName = "DataSet2"
        Me.DataSet2.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'RV
        '
        Me.RV.Dock = System.Windows.Forms.DockStyle.Fill
        ReportDataSource2.Name = "DataSet2"
        ReportDataSource2.Value = Me.DataTable2BindingSource
        Me.RV.LocalReport.DataSources.Add(ReportDataSource2)
        Me.RV.LocalReport.ReportEmbeddedResource = "AgenApp.RptSalesInv.rdlc"
        Me.RV.Location = New System.Drawing.Point(0, 0)
        Me.RV.Name = "RV"
        Me.RV.Size = New System.Drawing.Size(994, 652)
        Me.RV.TabIndex = 0
        '
        'DataTable2TableAdapter
        '
        Me.DataTable2TableAdapter.ClearBeforeFill = True
        '
        'FrmReportInv
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(994, 652)
        Me.Controls.Add(Me.RV)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "FrmReportInv"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Sales Invoice Note"
        CType(Me.DataTable2BindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataSet2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RV As Global.Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents DataTable2BindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DataSet2 As AgenApp.DataSet2
    Friend WithEvents DataTable2TableAdapter As AgenApp.DataSet2TableAdapters.DataTable2TableAdapter
End Class
