<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSales
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmSales))
        Me.Button2 = New System.Windows.Forms.Button()
        Me.dgv = New System.Windows.Forms.DataGridView()
        Me.mkdbrg = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.mnabar = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.msatuan = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.mjumlah = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.mharga = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.mtot = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.txtKdCust = New System.Windows.Forms.TextBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.TextBox5 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnFindCust = New System.Windows.Forms.Button()
        Me.lblNmCust = New System.Windows.Forms.Label()
        Me.lblAlmCust1 = New System.Windows.Forms.Label()
        Me.lblAlmCust2 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(1195, 603)
        Me.Button2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(100, 28)
        Me.Button2.TabIndex = 0
        Me.Button2.Text = "Simpan"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'dgv
        '
        Me.dgv.BackgroundColor = System.Drawing.Color.White
        Me.dgv.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised
        Me.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.mkdbrg, Me.mnabar, Me.msatuan, Me.mjumlah, Me.mharga, Me.mtot})
        Me.dgv.EnableHeadersVisualStyles = False
        Me.dgv.GridColor = System.Drawing.SystemColors.ControlDarkDark
        Me.dgv.Location = New System.Drawing.Point(16, 277)
        Me.dgv.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.dgv.Name = "dgv"
        Me.dgv.Size = New System.Drawing.Size(1279, 196)
        Me.dgv.TabIndex = 1
        '
        'mkdbrg
        '
        Me.mkdbrg.HeaderText = "KODE BARANG"
        Me.mkdbrg.Name = "mkdbrg"
        '
        'mnabar
        '
        Me.mnabar.HeaderText = "NAMA BARANG"
        Me.mnabar.Name = "mnabar"
        Me.mnabar.Width = 200
        '
        'msatuan
        '
        Me.msatuan.HeaderText = "SATUAN"
        Me.msatuan.Name = "msatuan"
        Me.msatuan.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        '
        'mjumlah
        '
        Me.mjumlah.HeaderText = "KUANTITAS"
        Me.mjumlah.Name = "mjumlah"
        '
        'mharga
        '
        Me.mharga.HeaderText = "HARGA"
        Me.mharga.Name = "mharga"
        '
        'mtot
        '
        Me.mtot.HeaderText = "SUB TOTAL"
        Me.mtot.Name = "mtot"
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.Color.MediumBlue
        Me.Label25.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.ForeColor = System.Drawing.Color.White
        Me.Label25.Location = New System.Drawing.Point(64, 132)
        Me.Label25.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(141, 28)
        Me.Label25.TabIndex = 321
        Me.Label25.Text = " Pelanggan"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtKdCust
        '
        Me.txtKdCust.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtKdCust.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtKdCust.Location = New System.Drawing.Point(213, 132)
        Me.txtKdCust.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtKdCust.MaxLength = 15
        Me.txtKdCust.Name = "txtKdCust"
        Me.txtKdCust.Size = New System.Drawing.Size(193, 23)
        Me.txtKdCust.TabIndex = 322
        '
        'TextBox2
        '
        Me.TextBox2.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox2.Location = New System.Drawing.Point(484, 239)
        Me.TextBox2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(380, 23)
        Me.TextBox2.TabIndex = 330
        '
        'TextBox3
        '
        Me.TextBox3.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox3.Location = New System.Drawing.Point(484, 203)
        Me.TextBox3.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(380, 23)
        Me.TextBox3.TabIndex = 329
        '
        'TextBox4
        '
        Me.TextBox4.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox4.Location = New System.Drawing.Point(484, 167)
        Me.TextBox4.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New System.Drawing.Size(380, 23)
        Me.TextBox4.TabIndex = 328
        '
        'TextBox5
        '
        Me.TextBox5.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox5.Location = New System.Drawing.Point(633, 132)
        Me.TextBox5.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TextBox5.Name = "TextBox5"
        Me.TextBox5.Size = New System.Drawing.Size(231, 23)
        Me.TextBox5.TabIndex = 327
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.MediumBlue
        Me.Label1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(484, 132)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(141, 28)
        Me.Label1.TabIndex = 326
        Me.Label1.Text = " Kirim Ke"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnFindCust
        '
        Me.btnFindCust.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFindCust.ForeColor = System.Drawing.SystemColors.ButtonFace
        Me.btnFindCust.Image = CType(resources.GetObject("btnFindCust.Image"), System.Drawing.Image)
        Me.btnFindCust.Location = New System.Drawing.Point(416, 132)
        Me.btnFindCust.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnFindCust.Name = "btnFindCust"
        Me.btnFindCust.Size = New System.Drawing.Size(31, 28)
        Me.btnFindCust.TabIndex = 331
        Me.btnFindCust.UseVisualStyleBackColor = True
        '
        'lblNmCust
        '
        Me.lblNmCust.BackColor = System.Drawing.Color.White
        Me.lblNmCust.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblNmCust.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNmCust.ForeColor = System.Drawing.Color.Black
        Me.lblNmCust.Location = New System.Drawing.Point(64, 167)
        Me.lblNmCust.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblNmCust.Name = "lblNmCust"
        Me.lblNmCust.Size = New System.Drawing.Size(381, 28)
        Me.lblNmCust.TabIndex = 332
        Me.lblNmCust.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblAlmCust1
        '
        Me.lblAlmCust1.BackColor = System.Drawing.Color.White
        Me.lblAlmCust1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblAlmCust1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAlmCust1.ForeColor = System.Drawing.Color.Black
        Me.lblAlmCust1.Location = New System.Drawing.Point(64, 203)
        Me.lblAlmCust1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblAlmCust1.Name = "lblAlmCust1"
        Me.lblAlmCust1.Size = New System.Drawing.Size(381, 28)
        Me.lblAlmCust1.TabIndex = 333
        Me.lblAlmCust1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblAlmCust2
        '
        Me.lblAlmCust2.BackColor = System.Drawing.Color.White
        Me.lblAlmCust2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblAlmCust2.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAlmCust2.ForeColor = System.Drawing.Color.Black
        Me.lblAlmCust2.Location = New System.Drawing.Point(64, 239)
        Me.lblAlmCust2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblAlmCust2.Name = "lblAlmCust2"
        Me.lblAlmCust2.Size = New System.Drawing.Size(381, 28)
        Me.lblAlmCust2.TabIndex = 334
        Me.lblAlmCust2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.SystemColors.HotTrack
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label16.Font = New System.Drawing.Font("Cambria", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.White
        Me.Label16.Location = New System.Drawing.Point(0, 0)
        Me.Label16.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(1311, 41)
        Me.Label16.TabIndex = 378
        Me.Label16.Text = " Create Sales Invoice"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'FrmSales
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1311, 646)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.lblAlmCust2)
        Me.Controls.Add(Me.lblAlmCust1)
        Me.Controls.Add(Me.lblNmCust)
        Me.Controls.Add(Me.btnFindCust)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.TextBox3)
        Me.Controls.Add(Me.TextBox4)
        Me.Controls.Add(Me.TextBox5)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtKdCust)
        Me.Controls.Add(Me.Label25)
        Me.Controls.Add(Me.dgv)
        Me.Controls.Add(Me.Button2)
        Me.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmSales"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Sales Invoice"
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents dgv As System.Windows.Forms.DataGridView
    Friend WithEvents mkdbrg As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents mnabar As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents msatuan As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents mjumlah As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents mharga As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents mtot As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents txtKdCust As System.Windows.Forms.TextBox
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox4 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox5 As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnFindCust As System.Windows.Forms.Button
    Friend WithEvents lblNmCust As System.Windows.Forms.Label
    Friend WithEvents lblAlmCust1 As System.Windows.Forms.Label
    Friend WithEvents lblAlmCust2 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
End Class
