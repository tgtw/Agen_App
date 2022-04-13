Imports System.Data.SqlClient


Public Class FrmCustomerList

    Public Sub CommandPass(ByVal xKey As String)
        On Error GoTo ErrMsg
        Select Case xKey

            Case Is = "New"
                FrmCustomer.ShowDialog()

            Case Is = "Edit"

        End Select

        Exit Sub

ErrMsg:
        MsgBox(Err.Description, MsgBoxStyle.Critical, "Error")

    End Sub


    Private Sub Load_Data(sql As String, dtg As DataGridView)

        Try
            CMD = New SqlCommand
            DA = New SqlDataAdapter
            DT = New DataTable

            CMD.Connection = Conn
            CMD.CommandText = sql

            DA.SelectCommand = CMD
            DA.Fill(DT)

            With dtg

                .DataSource = DT

                .Columns(0).HeaderText = "CUST CODE"
                .Columns(1).HeaderText = "CUST NAME"
                .Columns(2).HeaderText = "ADDRESS"
                .Columns(3).HeaderText = "CITY"
                .Columns(4).HeaderText = "PHONE"
                .Columns(5).HeaderText = "TERM"

                .Columns(0).Width = 100
                .Columns(1).Width = 250
                .Columns(2).Width = 250
                .Columns(3).Width = 220
                .Columns(4).Width = 180
                .Columns(5).Width = 80          

                .Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

                .Columns(0).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
                .Columns(1).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
                .Columns(2).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
                .Columns(3).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
                .Columns(4).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
                .Columns(5).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft

                .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
                .ColumnHeadersHeight = 30

                .ColumnHeadersDefaultCellStyle.Font = New Font("Verdana", 9)

                .DefaultCellStyle.Font = New Font("Verdana", 9)

            End With

            '=====================
            ' DataGridView Design
            '=====================

            DGV.BorderStyle = BorderStyle.None
            DGV.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249)
            DGV.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
            DGV.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise
            DGV.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke
            DGV.BackgroundColor = Color.White

            DGV.EnableHeadersVisualStyles = False
            DGV.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
            DGV.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72)
            DGV.ColumnHeadersDefaultCellStyle.ForeColor = Color.White

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            'Conn.Close()
            'DA.Dispose()
        End Try

    End Sub

    Private Sub FrmCustomerList_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Me.Top = 0
        Me.Left = 0
        Me.Width = MDIMain.Width - 20
        Me.Height = MDIMain.Height - 90
    End Sub

    Private Sub FrmCustomerList_Leave(sender As Object, e As EventArgs) Handles Me.Leave
        MDIMain.ToolStrip.Visible = True
    End Sub


    Private Sub FrmCustomerList_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Me.WindowState = FormWindowState.Maximized

        Call Local_Connect()

        If ConnectStatus = False Then Exit Sub

        Query = "SELECT   flkode,flnama,flala1,flwila,flphon,flterm FROM [MASCUST] " & _
                "WHERE    [" & IIf(cboFilter.SelectedIndex = 0, "FLKODE", "FLNAMA") & "] LIKE '%" & Trim(txtFilter.Text) & "%' " & _
                "ORDER BY [" & IIf(cboFilter.SelectedIndex = 0, "FLKODE", "FLNAMA") & "] "

        Load_Data(Query, DGV)

        cboFilter.SelectedIndex = 1

    End Sub

    Private Sub txtFilter_GotFocus(sender As Object, e As EventArgs)
        txtFilter.BackColor = Color.FromArgb(254, 240, 158)
    End Sub

    Private Sub txtFilter_LostFocus(sender As Object, e As EventArgs)
        txtFilter.BackColor = Color.White
    End Sub

    Private Sub tsbNew_Click(sender As Object, e As EventArgs) Handles tsbNew.Click

        CUSTNUM = ""

        FrmCustomer.ShowDialog()


        Query = "SELECT   flkode,flnama,flala1,flwila,flphon,flterm FROM [MASCUST] " & _
               "WHERE    [" & IIf(cboFilter.SelectedIndex = 0, "FLKODE", "FLNAMA") & "] LIKE '%" & Trim(txtFilter.Text) & "%' " & _
               "ORDER BY [" & IIf(cboFilter.SelectedIndex = 0, "FLKODE", "FLNAMA") & "] "

        Load_Data(Query, DGV)

        If CUSTNUM <> "" Then

            For i = 0 To DGV.Rows.Count - 1
                If DGV.Rows(i).Cells(0).Value = CUSTNUM Then
                    DGV.CurrentCell = DGV(1, i)
                    Exit For
                End If
            Next
        End If

    End Sub

    Private Sub tsbEdit_Click(sender As Object, e As EventArgs) Handles tsbEdit.Click

        CUSTNUM = DGV.CurrentRow.Cells(0).Value.ToString

        FrmCustomer.ShowDialog()

        Query = "SELECT   flkode,flnama,flala1,flwila,flphon,flterm FROM [MASCUST] " & _
               "WHERE    [" & IIf(cboFilter.SelectedIndex = 0, "FLKODE", "FLNAMA") & "] LIKE '%" & Trim(txtFilter.Text) & "%' " & _
               "ORDER BY [" & IIf(cboFilter.SelectedIndex = 0, "FLKODE", "FLNAMA") & "] "

        Load_Data(Query, DGV)

        If CUSTNUM <> "" Then

            For i = 0 To DGV.Rows.Count - 1
                If DGV.Rows(i).Cells(0).Value = CUSTNUM Then
                    DGV.CurrentCell = DGV(1, i)
                    Exit For
                End If
            Next
        End If


    End Sub

    Private Sub tsbDelete_Click(sender As Object, e As EventArgs) Handles tsbDelete.Click

        'Try
        '    If MessageBox.Show("Do you really want to delete this record ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Yes Then

        '        Dim KdItem As String = DGV.CurrentRow.Cells(0).Value.ToString

        '        Call LogToSQLServer()

        '        Query = "DELETE FROM [MASPROD] WHERE [kdprod]='" & KdItem & "'"

        '        CMD = New SqlCommand
        '        CMD.Connection = Conn
        '        CMD.CommandText = Query
        '        CMD.ExecuteNonQuery()


        '        Query = "SELECT kdprod,nmprod,corak,warna,unit,hbeli,hjual FROM [MASPROD] ORDER BY [kdprod] "

        '        Load_Data(Query, DGV)

        '    End If

        'Catch ex As Exception
        '    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End Try

    End Sub

    Private Sub tsbExit_Click(sender As Object, e As EventArgs) Handles tsbExit.Click
        Me.Close()
    End Sub

    Private Sub txtFilter_TextChanged(sender As Object, e As EventArgs) Handles txtFilter.TextChanged

        Query = "SELECT   flkode,flnama,flala1,flwila,flphon,flterm FROM [MASCUST] " & _
                "WHERE    [" & IIf(cboFilter.SelectedIndex = 0, "FLKODE", "FLNAMA") & "] LIKE '%" & Trim(txtFilter.Text) & "%' " & _
                "ORDER BY [" & IIf(cboFilter.SelectedIndex = 0, "FLKODE", "FLNAMA") & "] "

        Load_Data(Query, DGV)

    End Sub

    Private Sub FrmCustomerList_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        On Error Resume Next
        txtFilter.Left = Me.Width - txtFilter.Width - 35
        cboFilter.Left = txtFilter.Left - cboFilter.Width - 5
        On Error GoTo 0
    End Sub
End Class