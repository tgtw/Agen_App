<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAdvGrid
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.DGV1 = New ADGV.AdvancedDataGridView()
        Me.chk1 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        CType(Me.DGV1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DGV1
        '
        Me.DGV1.AllowUserToAddRows = False
        Me.DGV1.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGV1.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DGV1.AutoGenerateContextFilters = True
        Me.DGV1.BackgroundColor = System.Drawing.Color.White
        Me.DGV1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.DGV1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        Me.DGV1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.chk1})
        Me.DGV1.DateWithTime = True
        Me.DGV1.GridColor = System.Drawing.Color.Black
        Me.DGV1.Location = New System.Drawing.Point(8, 8)
        Me.DGV1.Name = "DGV1"
        Me.DGV1.Size = New System.Drawing.Size(529, 192)
        Me.DGV1.TabIndex = 34
        Me.DGV1.TimeFilter = True
        '
        'chk1
        '
        Me.chk1.HeaderText = ""
        Me.chk1.MinimumWidth = 22
        Me.chk1.Name = "chk1"
        Me.chk1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic
        '
        'FrmAdvGrid
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(690, 262)
        Me.Controls.Add(Me.DGV1)
        Me.Name = "FrmAdvGrid"
        Me.Text = "FrmAdvGrid"
        CType(Me.DGV1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DGV1 As ADGV.AdvancedDataGridView
    Friend WithEvents chk1 As System.Windows.Forms.DataGridViewCheckBoxColumn
End Class
