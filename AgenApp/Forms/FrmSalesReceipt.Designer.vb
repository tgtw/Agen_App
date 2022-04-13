<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSalesReceipt
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmSalesReceipt))
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.txtKdCust = New System.Windows.Forms.TextBox()
        Me.lblNmCust = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.btnFindCust = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.dtpSODate = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblSONo = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboTOP = New System.Windows.Forms.ComboBox()
        Me.txtPO = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.DGV = New System.Windows.Forms.DataGridView()
        Me.ColNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColProd = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColDesc = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColColor = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColPack = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColQty = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColUnit = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.ColPrice = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColTotal = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColDelete = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnPickingList = New System.Windows.Forms.Button()
        Me.btnBarcode = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cboTax = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cboSLoc = New System.Windows.Forms.ComboBox()
        Me.lblDelvNo = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblTotal = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtNotes = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.lblSubTotal = New System.Windows.Forms.Label()
        Me.txtDiscount = New System.Windows.Forms.TextBox()
        Me.txtFreight = New System.Windows.Forms.TextBox()
        Me.btnDelete = New System.Windows.Forms.Button()
        CType(Me.DGV, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.Color.MediumBlue
        Me.Label25.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.ForeColor = System.Drawing.Color.White
        Me.Label25.Location = New System.Drawing.Point(17, 58)
        Me.Label25.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(127, 23)
        Me.Label25.TabIndex = 321
        Me.Label25.Text = " CUSTOMER"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtKdCust
        '
        Me.txtKdCust.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtKdCust.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtKdCust.Location = New System.Drawing.Point(149, 58)
        Me.txtKdCust.Margin = New System.Windows.Forms.Padding(4)
        Me.txtKdCust.MaxLength = 15
        Me.txtKdCust.Name = "txtKdCust"
        Me.txtKdCust.Size = New System.Drawing.Size(169, 23)
        Me.txtKdCust.TabIndex = 322
        '
        'lblNmCust
        '
        Me.lblNmCust.BackColor = System.Drawing.Color.White
        Me.lblNmCust.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblNmCust.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNmCust.ForeColor = System.Drawing.Color.Black
        Me.lblNmCust.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblNmCust.Location = New System.Drawing.Point(16, 88)
        Me.lblNmCust.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblNmCust.Name = "lblNmCust"
        Me.lblNmCust.Size = New System.Drawing.Size(330, 53)
        Me.lblNmCust.TabIndex = 332
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.SystemColors.HotTrack
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label16.Font = New System.Drawing.Font("Cambria", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.White
        Me.Label16.Location = New System.Drawing.Point(0, 0)
        Me.Label16.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(1059, 41)
        Me.Label16.TabIndex = 378
        Me.Label16.Text = " Create Sales Receipt"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnFindCust
        '
        Me.btnFindCust.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFindCust.ForeColor = System.Drawing.SystemColors.ButtonFace
        Me.btnFindCust.Image = CType(resources.GetObject("btnFindCust.Image"), System.Drawing.Image)
        Me.btnFindCust.Location = New System.Drawing.Point(323, 58)
        Me.btnFindCust.Margin = New System.Windows.Forms.Padding(4)
        Me.btnFindCust.Name = "btnFindCust"
        Me.btnFindCust.Size = New System.Drawing.Size(23, 23)
        Me.btnFindCust.TabIndex = 331
        Me.btnFindCust.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.MediumBlue
        Me.Label4.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.White
        Me.Label4.Location = New System.Drawing.Point(752, 87)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(111, 23)
        Me.Label4.TabIndex = 381
        Me.Label4.Text = " SALES NO"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'dtpSODate
        '
        Me.dtpSODate.CalendarFont = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpSODate.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpSODate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpSODate.Location = New System.Drawing.Point(869, 59)
        Me.dtpSODate.Name = "dtpSODate"
        Me.dtpSODate.Size = New System.Drawing.Size(114, 23)
        Me.dtpSODate.TabIndex = 380
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.MediumBlue
        Me.Label3.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(752, 58)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(111, 23)
        Me.Label3.TabIndex = 379
        Me.Label3.Text = " SALES DATE"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblSONo
        '
        Me.lblSONo.BackColor = System.Drawing.Color.White
        Me.lblSONo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSONo.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSONo.Location = New System.Drawing.Point(869, 89)
        Me.lblSONo.Name = "lblSONo"
        Me.lblSONo.Size = New System.Drawing.Size(114, 22)
        Me.lblSONo.TabIndex = 382
        Me.lblSONo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.MediumBlue
        Me.Label1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(430, 118)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(105, 23)
        Me.Label1.TabIndex = 383
        Me.Label1.Text = " PAY. TERMS"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboTOP
        '
        Me.cboTOP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTOP.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboTOP.FormattingEnabled = True
        Me.cboTOP.Items.AddRange(New Object() {"KAIN", "SERAGAM"})
        Me.cboTOP.Location = New System.Drawing.Point(541, 118)
        Me.cboTOP.Name = "cboTOP"
        Me.cboTOP.Size = New System.Drawing.Size(122, 24)
        Me.cboTOP.TabIndex = 384
        '
        'txtPO
        '
        Me.txtPO.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPO.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPO.Location = New System.Drawing.Point(541, 59)
        Me.txtPO.MaxLength = 10
        Me.txtPO.Name = "txtPO"
        Me.txtPO.Size = New System.Drawing.Size(122, 23)
        Me.txtPO.TabIndex = 386
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.MediumBlue
        Me.Label8.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.White
        Me.Label8.Location = New System.Drawing.Point(430, 59)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(105, 23)
        Me.Label8.TabIndex = 385
        Me.Label8.Text = " REF. NO"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'DGV
        '
        Me.DGV.BackgroundColor = System.Drawing.Color.White
        Me.DGV.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.DGV.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised
        Me.DGV.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        Me.DGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ColNo, Me.ColProd, Me.ColDesc, Me.ColColor, Me.ColPack, Me.ColQty, Me.ColUnit, Me.ColPrice, Me.ColTotal, Me.ColDelete})
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle10.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.White
        DataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DGV.DefaultCellStyle = DataGridViewCellStyle10
        Me.DGV.EnableHeadersVisualStyles = False
        Me.DGV.GridColor = System.Drawing.SystemColors.ControlDarkDark
        Me.DGV.Location = New System.Drawing.Point(12, 162)
        Me.DGV.Name = "DGV"
        Me.DGV.RowHeadersVisible = False
        Me.DGV.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.DGV.Size = New System.Drawing.Size(1033, 272)
        Me.DGV.TabIndex = 387
        '
        'ColNo
        '
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ColNo.DefaultCellStyle = DataGridViewCellStyle1
        Me.ColNo.HeaderText = "NO"
        Me.ColNo.MaxInputLength = 2
        Me.ColNo.Name = "ColNo"
        Me.ColNo.ReadOnly = True
        Me.ColNo.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.ColNo.Width = 40
        '
        'ColProd
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.ColProd.DefaultCellStyle = DataGridViewCellStyle2
        Me.ColProd.HeaderText = "PRODUCT #"
        Me.ColProd.MaxInputLength = 18
        Me.ColProd.Name = "ColProd"
        Me.ColProd.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.ColProd.Width = 170
        '
        'ColDesc
        '
        Me.ColDesc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.White
        Me.ColDesc.DefaultCellStyle = DataGridViewCellStyle3
        Me.ColDesc.HeaderText = "DESCRIPTION"
        Me.ColDesc.MaxInputLength = 50
        Me.ColDesc.Name = "ColDesc"
        Me.ColDesc.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'ColColor
        '
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.White
        Me.ColColor.DefaultCellStyle = DataGridViewCellStyle4
        Me.ColColor.HeaderText = "COLOR"
        Me.ColColor.MaxInputLength = 15
        Me.ColColor.Name = "ColColor"
        Me.ColColor.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.ColColor.Width = 80
        '
        'ColPack
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle5.Format = "N0"
        DataGridViewCellStyle5.NullValue = Nothing
        Me.ColPack.DefaultCellStyle = DataGridViewCellStyle5
        Me.ColPack.HeaderText = "PACK"
        Me.ColPack.MaxInputLength = 10
        Me.ColPack.Name = "ColPack"
        Me.ColPack.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.ColPack.Width = 80
        '
        'ColQty
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle6.Format = "N2"
        DataGridViewCellStyle6.NullValue = Nothing
        Me.ColQty.DefaultCellStyle = DataGridViewCellStyle6
        Me.ColQty.HeaderText = "QUANTITY"
        Me.ColQty.MaxInputLength = 12
        Me.ColQty.Name = "ColQty"
        Me.ColQty.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.ColQty.Width = 90
        '
        'ColUnit
        '
        DataGridViewCellStyle7.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ColUnit.DefaultCellStyle = DataGridViewCellStyle7
        Me.ColUnit.HeaderText = "UNIT"
        Me.ColUnit.Name = "ColUnit"
        Me.ColUnit.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.ColUnit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.ColUnit.Width = 55
        '
        'ColPrice
        '
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle8.Format = "N2"
        DataGridViewCellStyle8.NullValue = Nothing
        Me.ColPrice.DefaultCellStyle = DataGridViewCellStyle8
        Me.ColPrice.HeaderText = "PRICE"
        Me.ColPrice.MaxInputLength = 12
        Me.ColPrice.Name = "ColPrice"
        Me.ColPrice.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.ColPrice.Width = 90
        '
        'ColTotal
        '
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle9.BackColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle9.Format = "N0"
        DataGridViewCellStyle9.NullValue = Nothing
        Me.ColTotal.DefaultCellStyle = DataGridViewCellStyle9
        Me.ColTotal.HeaderText = "TOTAL"
        Me.ColTotal.MaxInputLength = 16
        Me.ColTotal.Name = "ColTotal"
        Me.ColTotal.ReadOnly = True
        Me.ColTotal.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.ColTotal.Width = 110
        '
        'ColDelete
        '
        Me.ColDelete.HeaderText = "DEL"
        Me.ColDelete.Name = "ColDelete"
        Me.ColDelete.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.ColDelete.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.ColDelete.ToolTipText = "Delete Item"
        Me.ColDelete.Width = 35
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnExit)
        Me.GroupBox1.Controls.Add(Me.btnSave)
        Me.GroupBox1.Controls.Add(Me.btnCancel)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupBox1.Location = New System.Drawing.Point(0, 566)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1059, 57)
        Me.GroupBox1.TabIndex = 388
        Me.GroupBox1.TabStop = False
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.BackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(158, Byte), Integer))
        Me.btnExit.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnExit.Image = CType(resources.GetObject("btnExit.Image"), System.Drawing.Image)
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.Location = New System.Drawing.Point(935, 17)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(111, 29)
        Me.btnExit.TabIndex = 392
        Me.btnExit.Text = "  EXIT"
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(158, Byte), Integer))
        Me.btnSave.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnSave.Image = CType(resources.GetObject("btnSave.Image"), System.Drawing.Image)
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSave.Location = New System.Drawing.Point(701, 17)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(111, 29)
        Me.btnSave.TabIndex = 339
        Me.btnSave.Text = " SAVE"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.BackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(158, Byte), Integer))
        Me.btnCancel.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnCancel.Image = CType(resources.GetObject("btnCancel.Image"), System.Drawing.Image)
        Me.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCancel.Location = New System.Drawing.Point(818, 17)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(111, 29)
        Me.btnCancel.TabIndex = 338
        Me.btnCancel.Text = " CANCEL"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'btnPickingList
        '
        Me.btnPickingList.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPickingList.BackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(158, Byte), Integer))
        Me.btnPickingList.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPickingList.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnPickingList.Image = CType(resources.GetObject("btnPickingList.Image"), System.Drawing.Image)
        Me.btnPickingList.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPickingList.Location = New System.Drawing.Point(187, 445)
        Me.btnPickingList.Name = "btnPickingList"
        Me.btnPickingList.Size = New System.Drawing.Size(177, 29)
        Me.btnPickingList.TabIndex = 394
        Me.btnPickingList.Text = "Refer to Picking List "
        Me.btnPickingList.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnPickingList.UseVisualStyleBackColor = False
        '
        'btnBarcode
        '
        Me.btnBarcode.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnBarcode.BackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(158, Byte), Integer))
        Me.btnBarcode.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.btnBarcode.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnBarcode.Image = CType(resources.GetObject("btnBarcode.Image"), System.Drawing.Image)
        Me.btnBarcode.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnBarcode.Location = New System.Drawing.Point(16, 445)
        Me.btnBarcode.Name = "btnBarcode"
        Me.btnBarcode.Size = New System.Drawing.Size(165, 29)
        Me.btnBarcode.TabIndex = 393
        Me.btnBarcode.Text = "Refer to Barcode "
        Me.btnBarcode.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnBarcode.UseVisualStyleBackColor = False
        '
        'Label7
        '
        Me.Label7.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label7.BackColor = System.Drawing.Color.MediumBlue
        Me.Label7.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.White
        Me.Label7.Location = New System.Drawing.Point(769, 444)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(127, 23)
        Me.Label7.TabIndex = 389
        Me.Label7.Text = " SUB TOTAL"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboTax
        '
        Me.cboTax.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboTax.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTax.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboTax.FormattingEnabled = True
        Me.cboTax.Items.AddRange(New Object() {"Tax Included", "Tax Excluded"})
        Me.cboTax.Location = New System.Drawing.Point(623, 445)
        Me.cboTax.Name = "cboTax"
        Me.cboTax.Size = New System.Drawing.Size(133, 24)
        Me.cboTax.TabIndex = 391
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.MediumBlue
        Me.Label2.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(430, 89)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(105, 23)
        Me.Label2.TabIndex = 393
        Me.Label2.Text = " LOCATION"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboSLoc
        '
        Me.cboSLoc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSLoc.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboSLoc.FormattingEnabled = True
        Me.cboSLoc.Location = New System.Drawing.Point(541, 89)
        Me.cboSLoc.Name = "cboSLoc"
        Me.cboSLoc.Size = New System.Drawing.Size(122, 24)
        Me.cboSLoc.TabIndex = 394
        '
        'lblDelvNo
        '
        Me.lblDelvNo.BackColor = System.Drawing.Color.White
        Me.lblDelvNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDelvNo.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDelvNo.Location = New System.Drawing.Point(869, 118)
        Me.lblDelvNo.Name = "lblDelvNo"
        Me.lblDelvNo.Size = New System.Drawing.Size(114, 22)
        Me.lblDelvNo.TabIndex = 396
        Me.lblDelvNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.MediumBlue
        Me.Label6.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.White
        Me.Label6.Location = New System.Drawing.Point(752, 117)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(111, 23)
        Me.Label6.TabIndex = 395
        Me.Label6.Text = " DELIVERY NO"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTotal
        '
        Me.lblTotal.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTotal.BackColor = System.Drawing.Color.White
        Me.lblTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTotal.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotal.Location = New System.Drawing.Point(902, 525)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New System.Drawing.Size(114, 22)
        Me.lblTotal.TabIndex = 397
        Me.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.MediumBlue
        Me.Label5.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.White
        Me.Label5.Location = New System.Drawing.Point(17, 491)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(444, 23)
        Me.Label5.TabIndex = 398
        Me.Label5.Text = " NOTES"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtNotes
        '
        Me.txtNotes.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtNotes.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNotes.Location = New System.Drawing.Point(16, 517)
        Me.txtNotes.MaxLength = 100
        Me.txtNotes.Multiline = True
        Me.txtNotes.Name = "txtNotes"
        Me.txtNotes.Size = New System.Drawing.Size(445, 43)
        Me.txtNotes.TabIndex = 399
        '
        'Label9
        '
        Me.Label9.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label9.BackColor = System.Drawing.Color.MediumBlue
        Me.Label9.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.White
        Me.Label9.Location = New System.Drawing.Point(769, 471)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(127, 23)
        Me.Label9.TabIndex = 400
        Me.Label9.Text = " DISCOUNT"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label10
        '
        Me.Label10.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label10.BackColor = System.Drawing.Color.MediumBlue
        Me.Label10.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.White
        Me.Label10.Location = New System.Drawing.Point(769, 498)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(127, 23)
        Me.Label10.TabIndex = 401
        Me.Label10.Text = " FREIGHT COST"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label11
        '
        Me.Label11.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label11.BackColor = System.Drawing.Color.MediumBlue
        Me.Label11.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.White
        Me.Label11.Location = New System.Drawing.Point(769, 525)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(127, 23)
        Me.Label11.TabIndex = 402
        Me.Label11.Text = " GRAND TOTAL"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblSubTotal
        '
        Me.lblSubTotal.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblSubTotal.BackColor = System.Drawing.Color.White
        Me.lblSubTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSubTotal.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSubTotal.Location = New System.Drawing.Point(902, 444)
        Me.lblSubTotal.Name = "lblSubTotal"
        Me.lblSubTotal.Size = New System.Drawing.Size(114, 22)
        Me.lblSubTotal.TabIndex = 403
        Me.lblSubTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtDiscount
        '
        Me.txtDiscount.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDiscount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtDiscount.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDiscount.Location = New System.Drawing.Point(902, 471)
        Me.txtDiscount.MaxLength = 10
        Me.txtDiscount.Name = "txtDiscount"
        Me.txtDiscount.Size = New System.Drawing.Size(114, 23)
        Me.txtDiscount.TabIndex = 404
        Me.txtDiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtFreight
        '
        Me.txtFreight.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtFreight.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtFreight.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFreight.Location = New System.Drawing.Point(902, 498)
        Me.txtFreight.MaxLength = 10
        Me.txtFreight.Name = "txtFreight"
        Me.txtFreight.Size = New System.Drawing.Size(114, 23)
        Me.txtFreight.TabIndex = 405
        Me.txtFreight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.BackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(158, Byte), Integer))
        Me.btnDelete.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnDelete.Image = CType(resources.GetObject("btnDelete.Image"), System.Drawing.Image)
        Me.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDelete.Location = New System.Drawing.Point(370, 447)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(196, 27)
        Me.btnDelete.TabIndex = 406
        Me.btnDelete.Text = "   Delete Selected Items"
        Me.btnDelete.UseVisualStyleBackColor = False
        '
        'FrmSalesReceipt
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1059, 623)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.btnPickingList)
        Me.Controls.Add(Me.txtFreight)
        Me.Controls.Add(Me.btnBarcode)
        Me.Controls.Add(Me.txtDiscount)
        Me.Controls.Add(Me.lblSubTotal)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.txtNotes)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.lblTotal)
        Me.Controls.Add(Me.lblDelvNo)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.cboSLoc)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cboTax)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.DGV)
        Me.Controls.Add(Me.txtPO)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.cboTOP)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblSONo)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.dtpSODate)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.lblNmCust)
        Me.Controls.Add(Me.btnFindCust)
        Me.Controls.Add(Me.txtKdCust)
        Me.Controls.Add(Me.Label25)
        Me.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmSalesReceipt"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Sales Receipt"
        CType(Me.DGV, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents txtKdCust As System.Windows.Forms.TextBox
    Friend WithEvents btnFindCust As System.Windows.Forms.Button
    Friend WithEvents lblNmCust As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents dtpSODate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblSONo As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cboTOP As System.Windows.Forms.ComboBox
    Friend WithEvents txtPO As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents DGV As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cboTax As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cboSLoc As System.Windows.Forms.ComboBox
    Friend WithEvents lblDelvNo As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lblTotal As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtNotes As System.Windows.Forms.TextBox
    Friend WithEvents btnBarcode As System.Windows.Forms.Button
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents lblSubTotal As System.Windows.Forms.Label
    Friend WithEvents txtDiscount As System.Windows.Forms.TextBox
    Friend WithEvents txtFreight As System.Windows.Forms.TextBox
    Friend WithEvents btnPickingList As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents ColNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColProd As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColDesc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColColor As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColPack As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColQty As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColUnit As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents ColPrice As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColTotal As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColDelete As System.Windows.Forms.DataGridViewCheckBoxColumn
End Class
