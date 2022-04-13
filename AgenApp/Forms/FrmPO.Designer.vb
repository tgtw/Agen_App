<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPO
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
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmPO))
        Me.txtKdSupl = New System.Windows.Forms.TextBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.DGV = New System.Windows.Forms.DataGridView()
        Me.ColNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColProduct = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColDesc = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColColor = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColQty = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColUnit = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.ColPrice = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColTotal = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column9 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dtpPODate = New System.Windows.Forms.DateTimePicker()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.dtpDelDate = New System.Windows.Forms.DateTimePicker()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cboCat = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtSales = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.cboDelStatus = New System.Windows.Forms.ComboBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.cboPoint = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtKirim = New System.Windows.Forms.TextBox()
        Me.cboKirim = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.cboPanjang = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cboBentuk = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.chkSulam = New System.Windows.Forms.CheckBox()
        Me.chkFace = New System.Windows.Forms.CheckBox()
        Me.chkSelvedge = New System.Windows.Forms.CheckBox()
        Me.txtMerek = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.cboCapPinggir = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.txtHanger = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.txtLembaran = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtKain = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtM1020 = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtAlbum = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.txtCatatan = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.cboTOP = New System.Windows.Forms.ComboBox()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnFindSupl = New System.Windows.Forms.Button()
        Me.btnSplitGrade = New System.Windows.Forms.Button()
        Me.lblTotal = New System.Windows.Forms.Label()
        Me.cboTax = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtNoUrut = New System.Windows.Forms.TextBox()
        CType(Me.DGV, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtKdSupl
        '
        Me.txtKdSupl.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtKdSupl.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtKdSupl.Location = New System.Drawing.Point(372, 57)
        Me.txtKdSupl.MaxLength = 50
        Me.txtKdSupl.Name = "txtKdSupl"
        Me.txtKdSupl.Size = New System.Drawing.Size(335, 23)
        Me.txtKdSupl.TabIndex = 337
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.Color.MediumBlue
        Me.Label25.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.ForeColor = System.Drawing.Color.White
        Me.Label25.Location = New System.Drawing.Point(277, 57)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(89, 23)
        Me.Label25.TabIndex = 336
        Me.Label25.Text = " VENDOR"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'DGV
        '
        Me.DGV.BackgroundColor = System.Drawing.Color.White
        Me.DGV.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.DGV.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised
        Me.DGV.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGV.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ColNo, Me.ColProduct, Me.ColDesc, Me.ColColor, Me.ColQty, Me.ColUnit, Me.ColPrice, Me.ColTotal, Me.Column9})
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
        Me.DGV.Location = New System.Drawing.Point(11, 285)
        Me.DGV.Name = "DGV"
        Me.DGV.RowHeadersVisible = False
        Me.DGV.Size = New System.Drawing.Size(1002, 227)
        Me.DGV.TabIndex = 335
        '
        'ColNo
        '
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.White
        Me.ColNo.DefaultCellStyle = DataGridViewCellStyle2
        Me.ColNo.HeaderText = "NO"
        Me.ColNo.MaxInputLength = 2
        Me.ColNo.Name = "ColNo"
        Me.ColNo.ReadOnly = True
        Me.ColNo.Width = 40
        '
        'ColProduct
        '
        Me.ColProduct.HeaderText = "ITEM CODE"
        Me.ColProduct.MaxInputLength = 20
        Me.ColProduct.Name = "ColProduct"
        Me.ColProduct.Width = 145
        '
        'ColDesc
        '
        Me.ColDesc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.White
        Me.ColDesc.DefaultCellStyle = DataGridViewCellStyle3
        Me.ColDesc.HeaderText = "ITEM DESCRIPTION"
        Me.ColDesc.MaxInputLength = 50
        Me.ColDesc.Name = "ColDesc"
        '
        'ColColor
        '
        Me.ColColor.HeaderText = "COLOR"
        Me.ColColor.MaxInputLength = 15
        Me.ColColor.Name = "ColColor"
        '
        'ColQty
        '
        DataGridViewCellStyle4.Format = "N2"
        DataGridViewCellStyle4.NullValue = Nothing
        Me.ColQty.DefaultCellStyle = DataGridViewCellStyle4
        Me.ColQty.HeaderText = "QUANTITY"
        Me.ColQty.MaxInputLength = 10
        Me.ColQty.Name = "ColQty"
        '
        'ColUnit
        '
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.White
        Me.ColUnit.DefaultCellStyle = DataGridViewCellStyle5
        Me.ColUnit.HeaderText = "UNIT"
        Me.ColUnit.Name = "ColUnit"
        Me.ColUnit.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.ColUnit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.ColUnit.Width = 50
        '
        'ColPrice
        '
        DataGridViewCellStyle6.Format = "N0"
        DataGridViewCellStyle6.NullValue = Nothing
        Me.ColPrice.DefaultCellStyle = DataGridViewCellStyle6
        Me.ColPrice.HeaderText = "NET PRICE"
        Me.ColPrice.MaxInputLength = 16
        Me.ColPrice.Name = "ColPrice"
        Me.ColPrice.Width = 110
        '
        'ColTotal
        '
        DataGridViewCellStyle7.BackColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle7.Format = "N0"
        DataGridViewCellStyle7.NullValue = Nothing
        Me.ColTotal.DefaultCellStyle = DataGridViewCellStyle7
        Me.ColTotal.HeaderText = "TOTAL"
        Me.ColTotal.MaxInputLength = 16
        Me.ColTotal.Name = "ColTotal"
        Me.ColTotal.ReadOnly = True
        Me.ColTotal.Width = 120
        '
        'Column9
        '
        Me.Column9.HeaderText = "DEL"
        Me.Column9.Name = "Column9"
        Me.Column9.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Column9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.Column9.ToolTipText = "Delete Item"
        Me.Column9.Width = 40
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.MediumBlue
        Me.Label3.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(774, 54)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(106, 23)
        Me.Label3.TabIndex = 344
        Me.Label3.Text = " ORDER DATE"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'dtpPODate
        '
        Me.dtpPODate.CalendarFont = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpPODate.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpPODate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpPODate.Location = New System.Drawing.Point(886, 54)
        Me.dtpPODate.Name = "dtpPODate"
        Me.dtpPODate.Size = New System.Drawing.Size(114, 23)
        Me.dtpPODate.TabIndex = 345
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.MediumBlue
        Me.Label7.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.White
        Me.Label7.Location = New System.Drawing.Point(725, 520)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(116, 23)
        Me.Label7.TabIndex = 352
        Me.Label7.Text = "GRAND TOTAL"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnExit)
        Me.GroupBox1.Controls.Add(Me.btnSave)
        Me.GroupBox1.Controls.Add(Me.btnCancel)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupBox1.Location = New System.Drawing.Point(0, 563)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1025, 58)
        Me.GroupBox1.TabIndex = 354
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
        Me.btnExit.Location = New System.Drawing.Point(902, 19)
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
        Me.btnSave.Location = New System.Drawing.Point(668, 19)
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
        Me.btnCancel.Location = New System.Drawing.Point(785, 19)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(111, 29)
        Me.btnCancel.TabIndex = 338
        Me.btnCancel.Text = " CANCEL"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'dtpDelDate
        '
        Me.dtpDelDate.CalendarFont = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpDelDate.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpDelDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDelDate.Location = New System.Drawing.Point(886, 87)
        Me.dtpDelDate.Name = "dtpDelDate"
        Me.dtpDelDate.Size = New System.Drawing.Size(114, 23)
        Me.dtpDelDate.TabIndex = 367
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.MediumBlue
        Me.Label4.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.White
        Me.Label4.Location = New System.Drawing.Point(774, 87)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(106, 23)
        Me.Label4.TabIndex = 366
        Me.Label4.Text = " DELV DATE"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboCat
        '
        Me.cboCat.BackColor = System.Drawing.SystemColors.Window
        Me.cboCat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCat.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCat.FormattingEnabled = True
        Me.cboCat.Items.AddRange(New Object() {"KAIN", "SERAGAM"})
        Me.cboCat.Location = New System.Drawing.Point(127, 56)
        Me.cboCat.Name = "cboCat"
        Me.cboCat.Size = New System.Drawing.Size(119, 24)
        Me.cboCat.TabIndex = 369
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.MediumBlue
        Me.Label5.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.White
        Me.Label5.Location = New System.Drawing.Point(26, 56)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(95, 24)
        Me.Label5.TabIndex = 368
        Me.Label5.Text = " CATEGORY"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtSales
        '
        Me.txtSales.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSales.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSales.Location = New System.Drawing.Point(127, 87)
        Me.txtSales.MaxLength = 15
        Me.txtSales.Name = "txtSales"
        Me.txtSales.Size = New System.Drawing.Size(119, 23)
        Me.txtSales.TabIndex = 371
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.MediumBlue
        Me.Label8.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.White
        Me.Label8.Location = New System.Drawing.Point(26, 87)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(95, 23)
        Me.Label8.TabIndex = 370
        Me.Label8.Text = " PIC/SALES"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.MediumBlue
        Me.Label14.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.White
        Me.Label14.Location = New System.Drawing.Point(509, 87)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(111, 24)
        Me.Label14.TabIndex = 372
        Me.Label14.Text = " DELV STATUS"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboDelStatus
        '
        Me.cboDelStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDelStatus.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDelStatus.FormattingEnabled = True
        Me.cboDelStatus.Items.AddRange(New Object() {"NORMAL", "URGENT"})
        Me.cboDelStatus.Location = New System.Drawing.Point(626, 87)
        Me.cboDelStatus.Name = "cboDelStatus"
        Me.cboDelStatus.Size = New System.Drawing.Size(112, 24)
        Me.cboDelStatus.TabIndex = 373
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(11, 160)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.Padding = New System.Drawing.Point(6, 6)
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1002, 119)
        Me.TabControl1.TabIndex = 376
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.TabPage1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TabPage1.Controls.Add(Me.cboPoint)
        Me.TabPage1.Controls.Add(Me.Label10)
        Me.TabPage1.Controls.Add(Me.txtKirim)
        Me.TabPage1.Controls.Add(Me.cboKirim)
        Me.TabPage1.Controls.Add(Me.Label11)
        Me.TabPage1.Controls.Add(Me.cboPanjang)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.cboBentuk)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabPage1.Location = New System.Drawing.Point(4, 31)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(994, 84)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = " METODE PACKING "
        '
        'cboPoint
        '
        Me.cboPoint.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPoint.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboPoint.FormattingEnabled = True
        Me.cboPoint.Items.AddRange(New Object() {"TERPISAH", "IKUT ECERAN KAIN"})
        Me.cboPoint.Location = New System.Drawing.Point(790, 14)
        Me.cboPoint.Name = "cboPoint"
        Me.cboPoint.Size = New System.Drawing.Size(153, 24)
        Me.cboPoint.TabIndex = 406
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.MediumBlue
        Me.Label10.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.White
        Me.Label10.Location = New System.Drawing.Point(694, 14)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(90, 23)
        Me.Label10.TabIndex = 405
        Me.Label10.Text = " POINT"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtKirim
        '
        Me.txtKirim.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtKirim.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtKirim.Location = New System.Drawing.Point(319, 46)
        Me.txtKirim.MaxLength = 15
        Me.txtKirim.Name = "txtKirim"
        Me.txtKirim.Size = New System.Drawing.Size(624, 23)
        Me.txtKirim.TabIndex = 404
        Me.txtKirim.Visible = False
        '
        'cboKirim
        '
        Me.cboKirim.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboKirim.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboKirim.FormattingEnabled = True
        Me.cboKirim.Items.AddRange(New Object() {"AGEN", "PEMBELI"})
        Me.cboKirim.Location = New System.Drawing.Point(119, 45)
        Me.cboKirim.Name = "cboKirim"
        Me.cboKirim.Size = New System.Drawing.Size(146, 24)
        Me.cboKirim.TabIndex = 403
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.MediumBlue
        Me.Label11.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.White
        Me.Label11.Location = New System.Drawing.Point(23, 45)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(90, 23)
        Me.Label11.TabIndex = 402
        Me.Label11.Text = " KIRIM KE"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboPanjang
        '
        Me.cboPanjang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPanjang.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboPanjang.FormattingEnabled = True
        Me.cboPanjang.Items.AddRange(New Object() {"STANDAR  (30 Yds)", "BESAR  (50 Yds)"})
        Me.cboPanjang.Location = New System.Drawing.Point(419, 15)
        Me.cboPanjang.Name = "cboPanjang"
        Me.cboPanjang.Size = New System.Drawing.Size(219, 24)
        Me.cboPanjang.TabIndex = 399
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.MediumBlue
        Me.Label2.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(319, 15)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(94, 23)
        Me.Label2.TabIndex = 398
        Me.Label2.Text = " PANJANG"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboBentuk
        '
        Me.cboBentuk.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboBentuk.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboBentuk.FormattingEnabled = True
        Me.cboBentuk.Items.AddRange(New Object() {"PIECE", "ROLL"})
        Me.cboBentuk.Location = New System.Drawing.Point(119, 15)
        Me.cboBentuk.Name = "cboBentuk"
        Me.cboBentuk.Size = New System.Drawing.Size(146, 24)
        Me.cboBentuk.TabIndex = 397
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.MediumBlue
        Me.Label1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(23, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(90, 23)
        Me.Label1.TabIndex = 396
        Me.Label1.Text = " BENTUK"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.TabPage2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TabPage2.Controls.Add(Me.chkSulam)
        Me.TabPage2.Controls.Add(Me.chkFace)
        Me.TabPage2.Controls.Add(Me.chkSelvedge)
        Me.TabPage2.Controls.Add(Me.txtMerek)
        Me.TabPage2.Controls.Add(Me.Label12)
        Me.TabPage2.Controls.Add(Me.cboCapPinggir)
        Me.TabPage2.Controls.Add(Me.Label6)
        Me.TabPage2.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabPage2.Location = New System.Drawing.Point(4, 31)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(994, 84)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = " CAP PINGGIR "
        '
        'chkSulam
        '
        Me.chkSulam.AutoSize = True
        Me.chkSulam.Location = New System.Drawing.Point(638, 17)
        Me.chkSulam.Name = "chkSulam"
        Me.chkSulam.Size = New System.Drawing.Size(77, 20)
        Me.chkSulam.TabIndex = 396
        Me.chkSulam.Text = " SULAM"
        Me.chkSulam.UseVisualStyleBackColor = True
        '
        'chkFace
        '
        Me.chkFace.AutoSize = True
        Me.chkFace.Location = New System.Drawing.Point(484, 18)
        Me.chkFace.Name = "chkFace"
        Me.chkFace.Size = New System.Drawing.Size(125, 20)
        Me.chkFace.TabIndex = 395
        Me.chkFace.Text = " FACE (Kepala)"
        Me.chkFace.UseVisualStyleBackColor = True
        '
        'chkSelvedge
        '
        Me.chkSelvedge.AutoSize = True
        Me.chkSelvedge.Location = New System.Drawing.Point(311, 18)
        Me.chkSelvedge.Name = "chkSelvedge"
        Me.chkSelvedge.Size = New System.Drawing.Size(158, 20)
        Me.chkSelvedge.TabIndex = 394
        Me.chkSelvedge.Text = " SELVEDGE (Pinggir)"
        Me.chkSelvedge.UseVisualStyleBackColor = True
        '
        'txtMerek
        '
        Me.txtMerek.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtMerek.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMerek.Location = New System.Drawing.Point(139, 46)
        Me.txtMerek.MaxLength = 25
        Me.txtMerek.Name = "txtMerek"
        Me.txtMerek.Size = New System.Drawing.Size(144, 23)
        Me.txtMerek.TabIndex = 392
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.MediumBlue
        Me.Label12.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.White
        Me.Label12.Location = New System.Drawing.Point(23, 46)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(110, 23)
        Me.Label12.TabIndex = 391
        Me.Label12.Text = " MEREK"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboCapPinggir
        '
        Me.cboCapPinggir.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCapPinggir.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCapPinggir.FormattingEnabled = True
        Me.cboCapPinggir.Items.AddRange(New Object() {"CAP", "TANPA CAP"})
        Me.cboCapPinggir.Location = New System.Drawing.Point(139, 17)
        Me.cboCapPinggir.Name = "cboCapPinggir"
        Me.cboCapPinggir.Size = New System.Drawing.Size(144, 24)
        Me.cboCapPinggir.TabIndex = 390
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.MediumBlue
        Me.Label6.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.White
        Me.Label6.Location = New System.Drawing.Point(23, 16)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(110, 23)
        Me.Label6.TabIndex = 389
        Me.Label6.Text = " CAP PINGGIR"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TabPage3
        '
        Me.TabPage3.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.TabPage3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TabPage3.Controls.Add(Me.txtHanger)
        Me.TabPage3.Controls.Add(Me.Label21)
        Me.TabPage3.Controls.Add(Me.txtLembaran)
        Me.TabPage3.Controls.Add(Me.Label18)
        Me.TabPage3.Controls.Add(Me.txtKain)
        Me.TabPage3.Controls.Add(Me.Label19)
        Me.TabPage3.Controls.Add(Me.txtM1020)
        Me.TabPage3.Controls.Add(Me.Label17)
        Me.TabPage3.Controls.Add(Me.txtAlbum)
        Me.TabPage3.Controls.Add(Me.Label13)
        Me.TabPage3.Location = New System.Drawing.Point(4, 31)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(994, 84)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = " SAMPLE "
        '
        'txtHanger
        '
        Me.txtHanger.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtHanger.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHanger.Location = New System.Drawing.Point(783, 14)
        Me.txtHanger.MaxLength = 15
        Me.txtHanger.Name = "txtHanger"
        Me.txtHanger.Size = New System.Drawing.Size(185, 23)
        Me.txtHanger.TabIndex = 401
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.MediumBlue
        Me.Label21.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.Color.White
        Me.Label21.Location = New System.Drawing.Point(669, 14)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(108, 23)
        Me.Label21.TabIndex = 400
        Me.Label21.Text = " HANGER"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtLembaran
        '
        Me.txtLembaran.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtLembaran.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLembaran.Location = New System.Drawing.Point(458, 43)
        Me.txtLembaran.MaxLength = 15
        Me.txtLembaran.Name = "txtLembaran"
        Me.txtLembaran.Size = New System.Drawing.Size(185, 23)
        Me.txtLembaran.TabIndex = 399
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.MediumBlue
        Me.Label18.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.White
        Me.Label18.Location = New System.Drawing.Point(344, 43)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(108, 23)
        Me.Label18.TabIndex = 398
        Me.Label18.Text = " LEMBARAN"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtKain
        '
        Me.txtKain.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtKain.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtKain.Location = New System.Drawing.Point(458, 14)
        Me.txtKain.MaxLength = 15
        Me.txtKain.Name = "txtKain"
        Me.txtKain.Size = New System.Drawing.Size(185, 23)
        Me.txtKain.TabIndex = 397
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.MediumBlue
        Me.Label19.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.Color.White
        Me.Label19.Location = New System.Drawing.Point(344, 14)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(108, 23)
        Me.Label19.TabIndex = 396
        Me.Label19.Text = " KAIN"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtM1020
        '
        Me.txtM1020.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtM1020.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtM1020.Location = New System.Drawing.Point(133, 44)
        Me.txtM1020.MaxLength = 15
        Me.txtM1020.Name = "txtM1020"
        Me.txtM1020.Size = New System.Drawing.Size(185, 23)
        Me.txtM1020.TabIndex = 395
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.MediumBlue
        Me.Label17.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.White
        Me.Label17.Location = New System.Drawing.Point(19, 44)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(108, 23)
        Me.Label17.TabIndex = 394
        Me.Label17.Text = " M 10/20"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtAlbum
        '
        Me.txtAlbum.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtAlbum.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAlbum.Location = New System.Drawing.Point(133, 15)
        Me.txtAlbum.MaxLength = 15
        Me.txtAlbum.Name = "txtAlbum"
        Me.txtAlbum.Size = New System.Drawing.Size(185, 23)
        Me.txtAlbum.TabIndex = 393
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.MediumBlue
        Me.Label13.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.White
        Me.Label13.Location = New System.Drawing.Point(19, 15)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(108, 23)
        Me.Label13.TabIndex = 391
        Me.Label13.Text = " ALBUM"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TabPage4
        '
        Me.TabPage4.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.TabPage4.Controls.Add(Me.txtCatatan)
        Me.TabPage4.Controls.Add(Me.Label15)
        Me.TabPage4.Location = New System.Drawing.Point(4, 31)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage4.Size = New System.Drawing.Size(994, 84)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = " CATATAN  "
        '
        'txtCatatan
        '
        Me.txtCatatan.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCatatan.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCatatan.Location = New System.Drawing.Point(119, 9)
        Me.txtCatatan.MaxLength = 200
        Me.txtCatatan.Multiline = True
        Me.txtCatatan.Name = "txtCatatan"
        Me.txtCatatan.Size = New System.Drawing.Size(855, 69)
        Me.txtCatatan.TabIndex = 385
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.MediumBlue
        Me.Label15.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.White
        Me.Label15.Location = New System.Drawing.Point(22, 9)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(91, 69)
        Me.Label15.TabIndex = 384
        Me.Label15.Text = " CATATAN"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.SystemColors.HotTrack
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label16.Font = New System.Drawing.Font("Cambria", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.White
        Me.Label16.Location = New System.Drawing.Point(0, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(1025, 37)
        Me.Label16.TabIndex = 377
        Me.Label16.Text = " Create Purchase Order"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.MediumBlue
        Me.Label20.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.Color.White
        Me.Label20.Location = New System.Drawing.Point(277, 87)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(89, 24)
        Me.Label20.TabIndex = 378
        Me.Label20.Text = " TERMS"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboTOP
        '
        Me.cboTOP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTOP.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboTOP.FormattingEnabled = True
        Me.cboTOP.Items.AddRange(New Object() {"KAIN", "SERAGAM"})
        Me.cboTOP.Location = New System.Drawing.Point(372, 87)
        Me.cboTOP.Name = "cboTOP"
        Me.cboTOP.Size = New System.Drawing.Size(104, 24)
        Me.cboTOP.TabIndex = 379
        '
        'btnDelete
        '
        Me.btnDelete.BackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(158, Byte), Integer))
        Me.btnDelete.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnDelete.Image = CType(resources.GetObject("btnDelete.Image"), System.Drawing.Image)
        Me.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDelete.Location = New System.Drawing.Point(150, 518)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(190, 27)
        Me.btnDelete.TabIndex = 355
        Me.btnDelete.Text = "   Delete Selected Items"
        Me.btnDelete.UseVisualStyleBackColor = False
        '
        'btnFindSupl
        '
        Me.btnFindSupl.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFindSupl.ForeColor = System.Drawing.SystemColors.ButtonFace
        Me.btnFindSupl.Image = CType(resources.GetObject("btnFindSupl.Image"), System.Drawing.Image)
        Me.btnFindSupl.Location = New System.Drawing.Point(713, 56)
        Me.btnFindSupl.Name = "btnFindSupl"
        Me.btnFindSupl.Size = New System.Drawing.Size(25, 25)
        Me.btnFindSupl.TabIndex = 338
        Me.btnFindSupl.UseVisualStyleBackColor = True
        '
        'btnSplitGrade
        '
        Me.btnSplitGrade.BackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(158, Byte), Integer))
        Me.btnSplitGrade.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSplitGrade.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnSplitGrade.Image = CType(resources.GetObject("btnSplitGrade.Image"), System.Drawing.Image)
        Me.btnSplitGrade.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSplitGrade.Location = New System.Drawing.Point(12, 518)
        Me.btnSplitGrade.Name = "btnSplitGrade"
        Me.btnSplitGrade.Size = New System.Drawing.Size(132, 27)
        Me.btnSplitGrade.TabIndex = 381
        Me.btnSplitGrade.Text = " Split Grade"
        Me.btnSplitGrade.UseVisualStyleBackColor = False
        '
        'lblTotal
        '
        Me.lblTotal.BackColor = System.Drawing.Color.White
        Me.lblTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTotal.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotal.Location = New System.Drawing.Point(848, 520)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New System.Drawing.Size(120, 23)
        Me.lblTotal.TabIndex = 382
        Me.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboTax
        '
        Me.cboTax.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboTax.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTax.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboTax.FormattingEnabled = True
        Me.cboTax.Items.AddRange(New Object() {"Tax Included", "Tax Excluded"})
        Me.cboTax.Location = New System.Drawing.Point(586, 521)
        Me.cboTax.Name = "cboTax"
        Me.cboTax.Size = New System.Drawing.Size(133, 24)
        Me.cboTax.TabIndex = 392
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.MediumBlue
        Me.Label9.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.White
        Me.Label9.Location = New System.Drawing.Point(26, 117)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(95, 23)
        Me.Label9.TabIndex = 393
        Me.Label9.Text = " NO URUT"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtNoUrut
        '
        Me.txtNoUrut.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtNoUrut.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNoUrut.Location = New System.Drawing.Point(127, 117)
        Me.txtNoUrut.MaxLength = 10
        Me.txtNoUrut.Name = "txtNoUrut"
        Me.txtNoUrut.Size = New System.Drawing.Size(119, 23)
        Me.txtNoUrut.TabIndex = 394
        '
        'FrmPO
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1025, 621)
        Me.Controls.Add(Me.txtNoUrut)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.cboTax)
        Me.Controls.Add(Me.lblTotal)
        Me.Controls.Add(Me.btnSplitGrade)
        Me.Controls.Add(Me.cboTOP)
        Me.Controls.Add(Me.Label20)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.cboDelStatus)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.txtSales)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.cboCat)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.dtpDelDate)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.dtpPODate)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.btnFindSupl)
        Me.Controls.Add(Me.txtKdSupl)
        Me.Controls.Add(Me.Label25)
        Me.Controls.Add(Me.DGV)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmPO"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Purchase Order"
        CType(Me.DGV, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        Me.TabPage4.ResumeLayout(False)
        Me.TabPage4.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnFindSupl As System.Windows.Forms.Button
    Friend WithEvents txtKdSupl As System.Windows.Forms.TextBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents DGV As System.Windows.Forms.DataGridView
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents dtpPODate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents dtpDelDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cboCat As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtSales As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents cboDelStatus As System.Windows.Forms.ComboBox
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents txtKirim As System.Windows.Forms.TextBox
    Friend WithEvents cboKirim As System.Windows.Forms.ComboBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents cboPanjang As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cboBentuk As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents txtMerek As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents cboCapPinggir As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents cboPoint As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents chkFace As System.Windows.Forms.CheckBox
    Friend WithEvents chkSelvedge As System.Windows.Forms.CheckBox
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents txtHanger As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents txtLembaran As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtKain As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents txtM1020 As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtAlbum As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents cboTOP As System.Windows.Forms.ComboBox
    Friend WithEvents txtCatatan As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents btnSplitGrade As System.Windows.Forms.Button
    Friend WithEvents lblTotal As System.Windows.Forms.Label
    Friend WithEvents cboTax As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtNoUrut As System.Windows.Forms.TextBox
    Friend WithEvents chkSulam As System.Windows.Forms.CheckBox
    Friend WithEvents ColNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColProduct As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColDesc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColColor As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColQty As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColUnit As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents ColPrice As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColTotal As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column9 As System.Windows.Forms.DataGridViewCheckBoxColumn
End Class
