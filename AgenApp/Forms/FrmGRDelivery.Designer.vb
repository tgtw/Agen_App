<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmGRDelivery
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
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmGRDelivery))
        Me.Label8 = New System.Windows.Forms.Label()
        Me.lblPO = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtDelvNo = New System.Windows.Forms.TextBox()
        Me.DGV = New System.Windows.Forms.DataGridView()
        Me.ColNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColDelvNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColBarcode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColMaterial = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColGrade = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColColor = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColBatch = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColWitdh = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColYard = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColMeter = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColX = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column10 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.btnFindDelv = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        CType(Me.DGV, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.MediumBlue
        Me.Label8.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.White
        Me.Label8.Location = New System.Drawing.Point(20, 18)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(120, 23)
        Me.Label8.TabIndex = 380
        Me.Label8.Text = " PURCH. ORDER"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblPO
        '
        Me.lblPO.BackColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(225, Byte), Integer), CType(CType(225, Byte), Integer))
        Me.lblPO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPO.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPO.Location = New System.Drawing.Point(146, 18)
        Me.lblPO.Name = "lblPO"
        Me.lblPO.Size = New System.Drawing.Size(92, 23)
        Me.lblPO.TabIndex = 381
        Me.lblPO.Text = "110000028"
        Me.lblPO.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.MediumBlue
        Me.Label1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(274, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(117, 23)
        Me.Label1.TabIndex = 382
        Me.Label1.Text = " DELIVERY NO"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtDelvNo
        '
        Me.txtDelvNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtDelvNo.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDelvNo.Location = New System.Drawing.Point(397, 18)
        Me.txtDelvNo.MaxLength = 20
        Me.txtDelvNo.Name = "txtDelvNo"
        Me.txtDelvNo.Size = New System.Drawing.Size(105, 23)
        Me.txtDelvNo.TabIndex = 383
        '
        'DGV
        '
        Me.DGV.AllowUserToAddRows = False
        Me.DGV.AllowUserToDeleteRows = False
        Me.DGV.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Orange
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGV.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ColNo, Me.ColDelvNo, Me.ColBarcode, Me.ColMaterial, Me.ColGrade, Me.ColColor, Me.ColBatch, Me.ColWitdh, Me.ColYard, Me.ColMeter, Me.ColX, Me.Column10})
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle7.Padding = New System.Windows.Forms.Padding(0, 1, 0, 1)
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.Orange
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DGV.DefaultCellStyle = DataGridViewCellStyle7
        Me.DGV.Location = New System.Drawing.Point(12, 63)
        Me.DGV.MultiSelect = False
        Me.DGV.Name = "DGV"
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.Orange
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGV.RowHeadersDefaultCellStyle = DataGridViewCellStyle8
        Me.DGV.RowHeadersVisible = False
        DataGridViewCellStyle9.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.Orange
        DataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.Black
        Me.DGV.RowsDefaultCellStyle = DataGridViewCellStyle9
        Me.DGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DGV.Size = New System.Drawing.Size(965, 495)
        Me.DGV.TabIndex = 384
        '
        'ColNo
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.ColNo.DefaultCellStyle = DataGridViewCellStyle2
        Me.ColNo.HeaderText = "NO"
        Me.ColNo.Name = "ColNo"
        Me.ColNo.ReadOnly = True
        Me.ColNo.Width = 45
        '
        'ColDelvNo
        '
        Me.ColDelvNo.HeaderText = "DELIVERY"
        Me.ColDelvNo.MaxInputLength = 10
        Me.ColDelvNo.Name = "ColDelvNo"
        Me.ColDelvNo.ReadOnly = True
        '
        'ColBarcode
        '
        Me.ColBarcode.HeaderText = "BARCODE"
        Me.ColBarcode.MaxInputLength = 20
        Me.ColBarcode.Name = "ColBarcode"
        Me.ColBarcode.ReadOnly = True
        Me.ColBarcode.Width = 150
        '
        'ColMaterial
        '
        Me.ColMaterial.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.ColMaterial.HeaderText = "PRODUCT NO"
        Me.ColMaterial.MaxInputLength = 20
        Me.ColMaterial.Name = "ColMaterial"
        Me.ColMaterial.ReadOnly = True
        '
        'ColGrade
        '
        Me.ColGrade.HeaderText = "GRD"
        Me.ColGrade.MaxInputLength = 3
        Me.ColGrade.Name = "ColGrade"
        Me.ColGrade.ReadOnly = True
        Me.ColGrade.Width = 50
        '
        'ColColor
        '
        Me.ColColor.HeaderText = "COLOR"
        Me.ColColor.MaxInputLength = 10
        Me.ColColor.Name = "ColColor"
        Me.ColColor.ReadOnly = True
        Me.ColColor.Width = 70
        '
        'ColBatch
        '
        Me.ColBatch.HeaderText = "BATCH"
        Me.ColBatch.MaxInputLength = 7
        Me.ColBatch.Name = "ColBatch"
        Me.ColBatch.ReadOnly = True
        Me.ColBatch.Width = 60
        '
        'ColWitdh
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle3.Format = "N0"
        DataGridViewCellStyle3.NullValue = Nothing
        Me.ColWitdh.DefaultCellStyle = DataGridViewCellStyle3
        Me.ColWitdh.HeaderText = "WITDH"
        Me.ColWitdh.MaxInputLength = 12
        Me.ColWitdh.Name = "ColWitdh"
        Me.ColWitdh.ReadOnly = True
        Me.ColWitdh.Width = 70
        '
        'ColYard
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle4.Format = "N2"
        DataGridViewCellStyle4.NullValue = Nothing
        Me.ColYard.DefaultCellStyle = DataGridViewCellStyle4
        Me.ColYard.HeaderText = "YARD"
        Me.ColYard.MaxInputLength = 12
        Me.ColYard.Name = "ColYard"
        Me.ColYard.ReadOnly = True
        Me.ColYard.Width = 70
        '
        'ColMeter
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle5.Format = "N2"
        DataGridViewCellStyle5.NullValue = Nothing
        Me.ColMeter.DefaultCellStyle = DataGridViewCellStyle5
        Me.ColMeter.HeaderText = "METER"
        Me.ColMeter.MaxInputLength = 12
        Me.ColMeter.Name = "ColMeter"
        Me.ColMeter.ReadOnly = True
        Me.ColMeter.Width = 70
        '
        'ColX
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.ColX.DefaultCellStyle = DataGridViewCellStyle6
        Me.ColX.HeaderText = " X"
        Me.ColX.Name = "ColX"
        Me.ColX.ReadOnly = True
        Me.ColX.Width = 50
        '
        'Column10
        '
        Me.Column10.HeaderText = ""
        Me.Column10.Name = "Column10"
        Me.Column10.Width = 30
        '
        'btnFindDelv
        '
        Me.btnFindDelv.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFindDelv.ForeColor = System.Drawing.SystemColors.ButtonFace
        Me.btnFindDelv.Image = CType(resources.GetObject("btnFindDelv.Image"), System.Drawing.Image)
        Me.btnFindDelv.Location = New System.Drawing.Point(508, 16)
        Me.btnFindDelv.Name = "btnFindDelv"
        Me.btnFindDelv.Size = New System.Drawing.Size(25, 25)
        Me.btnFindDelv.TabIndex = 387
        Me.btnFindDelv.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(158, Byte), Integer))
        Me.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnSave.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnSave.Image = CType(resources.GetObject("btnSave.Image"), System.Drawing.Image)
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSave.Location = New System.Drawing.Point(745, 577)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(113, 29)
        Me.btnSave.TabIndex = 386
        Me.btnSave.Text = " SAVE"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.BackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(158, Byte), Integer))
        Me.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnCancel.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnCancel.Image = CType(resources.GetObject("btnCancel.Image"), System.Drawing.Image)
        Me.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCancel.Location = New System.Drawing.Point(864, 577)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(113, 29)
        Me.btnCancel.TabIndex = 385
        Me.btnCancel.Text = " CANCEL"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.BackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(158, Byte), Integer))
        Me.btnDelete.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnDelete.Image = CType(resources.GetObject("btnDelete.Image"), System.Drawing.Image)
        Me.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDelete.Location = New System.Drawing.Point(12, 577)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(190, 27)
        Me.btnDelete.TabIndex = 392
        Me.btnDelete.Text = "   Delete Selected Items"
        Me.btnDelete.UseVisualStyleBackColor = False
        '
        'FrmGRDelivery
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(991, 616)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.btnFindDelv)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.DGV)
        Me.Controls.Add(Me.txtDelvNo)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblPO)
        Me.Controls.Add(Me.Label8)
        Me.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmGRDelivery"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Goods Receipt Refer to Delivery"
        CType(Me.DGV, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents lblPO As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtDelvNo As System.Windows.Forms.TextBox
    Friend WithEvents DGV As System.Windows.Forms.DataGridView
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnFindDelv As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents ColNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColDelvNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColBarcode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColMaterial As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColGrade As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColColor As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColBatch As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColWitdh As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColYard As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColMeter As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColX As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column10 As System.Windows.Forms.DataGridViewCheckBoxColumn
End Class
