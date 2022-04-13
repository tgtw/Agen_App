<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmGRtitipan
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
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.dtpDate = New System.Windows.Forms.DateTimePicker()
        Me.cboSLoc = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtNote = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.DGV = New System.Windows.Forms.DataGridView()
        Me.ColNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColDesc = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColColor = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColGrade = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColYard = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColMeter = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.DGV, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.SystemColors.HotTrack
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label16.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.White
        Me.Label16.Location = New System.Drawing.Point(0, 0)
        Me.Label16.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(1066, 41)
        Me.Label16.TabIndex = 379
        Me.Label16.Text = " Goods Receipt (Barang Titipan)"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.MediumBlue
        Me.Label4.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.White
        Me.Label4.Location = New System.Drawing.Point(715, 63)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(111, 23)
        Me.Label4.TabIndex = 384
        Me.Label4.Text = " Posting Date"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'dtpDate
        '
        Me.dtpDate.CalendarFont = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpDate.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDate.Location = New System.Drawing.Point(832, 62)
        Me.dtpDate.Name = "dtpDate"
        Me.dtpDate.Size = New System.Drawing.Size(114, 23)
        Me.dtpDate.TabIndex = 383
        '
        'cboSLoc
        '
        Me.cboSLoc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSLoc.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboSLoc.FormattingEnabled = True
        Me.cboSLoc.Location = New System.Drawing.Point(139, 63)
        Me.cboSLoc.Name = "cboSLoc"
        Me.cboSLoc.Size = New System.Drawing.Size(103, 24)
        Me.cboSLoc.TabIndex = 396
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.MediumBlue
        Me.Label2.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(22, 63)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(111, 23)
        Me.Label2.TabIndex = 395
        Me.Label2.Text = " To Location"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtNote
        '
        Me.txtNote.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtNote.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNote.Location = New System.Drawing.Point(365, 63)
        Me.txtNote.MaxLength = 50
        Me.txtNote.Name = "txtNote"
        Me.txtNote.Size = New System.Drawing.Size(311, 23)
        Me.txtNote.TabIndex = 398
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.MediumBlue
        Me.Label3.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(284, 64)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(75, 23)
        Me.Label3.TabIndex = 397
        Me.Label3.Text = " Notes"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'DGV
        '
        Me.DGV.BackgroundColor = System.Drawing.Color.White
        Me.DGV.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised
        Me.DGV.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        Me.DGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ColNo, Me.ColCode, Me.ColDesc, Me.ColColor, Me.ColGrade, Me.ColYard, Me.ColMeter})
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.White
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DGV.DefaultCellStyle = DataGridViewCellStyle8
        Me.DGV.EnableHeadersVisualStyles = False
        Me.DGV.GridColor = System.Drawing.SystemColors.ControlDarkDark
        Me.DGV.Location = New System.Drawing.Point(12, 127)
        Me.DGV.Name = "DGV"
        Me.DGV.RowHeadersVisible = False
        Me.DGV.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.DGV.Size = New System.Drawing.Size(866, 351)
        Me.DGV.TabIndex = 399
        '
        'ColNo
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ColNo.DefaultCellStyle = DataGridViewCellStyle1
        Me.ColNo.HeaderText = "NO"
        Me.ColNo.MaxInputLength = 2
        Me.ColNo.Name = "ColNo"
        Me.ColNo.ReadOnly = True
        Me.ColNo.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.ColNo.Width = 40
        '
        'ColCode
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.ColCode.DefaultCellStyle = DataGridViewCellStyle2
        Me.ColCode.HeaderText = "PRODUCT #"
        Me.ColCode.MaxInputLength = 18
        Me.ColCode.Name = "ColCode"
        Me.ColCode.ReadOnly = True
        Me.ColCode.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.ColCode.Width = 150
        '
        'ColDesc
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.White
        Me.ColDesc.DefaultCellStyle = DataGridViewCellStyle3
        Me.ColDesc.HeaderText = "DESCRIPTION"
        Me.ColDesc.MaxInputLength = 40
        Me.ColDesc.Name = "ColDesc"
        Me.ColDesc.ReadOnly = True
        Me.ColDesc.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.ColDesc.Width = 481
        '
        'ColColor
        '
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.White
        Me.ColColor.DefaultCellStyle = DataGridViewCellStyle4
        Me.ColColor.HeaderText = "COLOR"
        Me.ColColor.MaxInputLength = 15
        Me.ColColor.Name = "ColColor"
        Me.ColColor.ReadOnly = True
        Me.ColColor.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.ColColor.Width = 80
        '
        'ColGrade
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.NullValue = Nothing
        Me.ColGrade.DefaultCellStyle = DataGridViewCellStyle5
        Me.ColGrade.HeaderText = "GRADE"
        Me.ColGrade.MaxInputLength = 1
        Me.ColGrade.Name = "ColGrade"
        Me.ColGrade.ReadOnly = True
        Me.ColGrade.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.ColGrade.Width = 80
        '
        'ColYard
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle6.Format = "N0"
        DataGridViewCellStyle6.NullValue = Nothing
        Me.ColYard.DefaultCellStyle = DataGridViewCellStyle6
        Me.ColYard.HeaderText = "ROLL"
        Me.ColYard.MaxInputLength = 8
        Me.ColYard.Name = "ColYard"
        Me.ColYard.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.ColYard.Width = 90
        '
        'ColMeter
        '
        DataGridViewCellStyle7.Format = "N2"
        Me.ColMeter.DefaultCellStyle = DataGridViewCellStyle7
        Me.ColMeter.HeaderText = "YARD"
        Me.ColMeter.MaxInputLength = 8
        Me.ColMeter.Name = "ColMeter"
        Me.ColMeter.ReadOnly = True
        Me.ColMeter.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.ColMeter.Width = 55
        '
        'FrmGRtitipan
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1066, 526)
        Me.Controls.Add(Me.DGV)
        Me.Controls.Add(Me.txtNote)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cboSLoc)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.dtpDate)
        Me.Controls.Add(Me.Label16)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmGRtitipan"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Goods Receipt (Titipan)"
        CType(Me.DGV, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents dtpDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents cboSLoc As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtNote As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents DGV As System.Windows.Forms.DataGridView
    Friend WithEvents ColNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColDesc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColColor As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColGrade As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColYard As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColMeter As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
