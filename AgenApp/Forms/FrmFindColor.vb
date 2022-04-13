Imports System.Data.SqlClient

Public Class FrmFindColor

    Private Sub Load_Data()

        '// Clear all row

        Do Until DGV.Rows.Count = 0
            DGV.Rows.RemoveAt(0)
        Loop

        Try

            Query = "SELECT   kdwarna,nmwarna FROM [MASCOL] " & _
                    "WHERE    [" & IIf(cboFilter.SelectedIndex = 0, "kdwarna", "nmwarna") & "]  LIKE '%" & txtFilter.Text & "%' " & _
                    "ORDER BY [" & IIf(cboFilter.SelectedIndex = 0, "kdwarna", "nmwarna") & "]"

            DS = New DataSet
            DA = New SqlDataAdapter(Query, Conn)
            DA.Fill(DS, "COLOR")

            For n = 1 To DS.Tables("COLOR").Rows.Count

                DGV.Rows.Add()
                DGV.Rows(n - 1).Cells(0).Value = DS.Tables("COLOR").Rows(n - 1)("kdwarna")
                DGV.Rows(n - 1).Cells(1).Value = DS.Tables("COLOR").Rows(n - 1)("nmwarna")

            Next

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            Conn.Close()
            DA.Dispose()
        End Try

    End Sub

    Private Sub FrmFindColor_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        If ConnectStatus = False Then Me.Close()
    End Sub

    Private Sub FrmFindColor_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Call Local_Connect()


        '=====================
        ' DataGridView Design
        '=====================

        DGV.BorderStyle = BorderStyle.Fixed3D
        DGV.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249)
        DGV.CellBorderStyle = DataGridViewCellBorderStyle.Single
        DGV.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise
        DGV.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke
        DGV.BackgroundColor = Color.White

        DGV.EnableHeadersVisualStyles = False
        DGV.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        DGV.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72)
        DGV.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        'DGV.RowHeadersVisible = False

        DGV.Columns(0).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DGV.Columns(1).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

        DGV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        DGV.ColumnHeadersHeight = 30

        DGV.ColumnHeadersDefaultCellStyle.Font = New Font("Verdana", 9)

        DGV.DefaultCellStyle.Font = New Font("Verdana", 10)

        cboFilter.SelectedIndex = 0

        txtFilter.Select()
        
        Call Load_Data()

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        CARI = ""
        Me.Close()
    End Sub

    Private Sub txtFilter_GotFocus(sender As Object, e As EventArgs) Handles txtFilter.GotFocus
        txtFilter.BackColor = Color.FromArgb(254, 240, 158)
    End Sub

    Private Sub txtFilter_KeyDown(sender As Object, e As KeyEventArgs) Handles txtFilter.KeyDown
        If e.KeyCode = Keys.Down Then
            DGV.Select()
        End If
    End Sub

    Private Sub txtFilter_LostFocus(sender As Object, e As EventArgs) Handles txtFilter.LostFocus
        txtFilter.BackColor = Color.White
    End Sub

    Private Sub txtFilter_TextChanged(sender As Object, e As EventArgs) Handles txtFilter.TextChanged

        Call Load_Data()

    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click

        Dim n As Integer

        n = DGV.Rows.Add()

        DGV.Select()
        DGV.CurrentCell = DGV(0, n)

        DGV.ReadOnly = False

    End Sub

    Private Sub DGV_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DGV.CellEndEdit

        Dim Col As Integer = e.ColumnIndex
        Dim Row As Integer = e.RowIndex

        Select Case Col

            Case Is = 0

                If IsNothing(DGV.Rows(Row).Cells(Col).Value) Then

                Else
                    Query = "SELECT * FROM [MASCOL] " & _
                            "WHERE [kdwarna]='" & DGV.Rows(Row).Cells(Col).Value & "'"

                    DS = New DataSet
                    DA = New SqlDataAdapter(Query, Conn)
                    DA.Fill(DS, "COLOR")

                    If DS.Tables("COLOR").Rows.Count <> 0 Then
                        MessageBox.Show("Color Code [" & DGV.Rows(Row).Cells(Col).Value & "] already exist !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        DGV.Rows(Row).Cells(0).Value = ""
                        DGV.Select()
                        Exit Sub
                    End If
                End If

                

            Case Is = 1

                If Trim(DGV.Rows(Row).Cells(0).Value) <> "" And Trim(DGV.Rows(Row).Cells(1).Value) <> "" Then

                    Try

                        Call Local_Connect()

                        Query = "INSERT INTO [MASCOL] (kdwarna,nmwarna) " & _
                                "VALUES ('" & DGV.Rows(Row).Cells(0).Value & "','" & DGV.Rows(Row).Cells(1).Value & "')"

                        CMD = New SqlCommand
                        CMD.Connection = Conn
                        CMD.CommandText = Query
                        CMD.ExecuteNonQuery()

                    Catch ex As Exception
                        Me.Cursor = Cursors.Default
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try

                End If

        End Select
    End Sub

    Private Sub DGV_CellLeave(sender As Object, e As DataGridViewCellEventArgs) Handles DGV.CellLeave

        Dim Col As Integer = e.ColumnIndex
        Dim Row As Integer = e.RowIndex

        'If DGV.Rows(Row).Cells(0).Value = "" Or DGV.Rows(Row).Cells(1).Value = "" Then

        '    Try
        '        Call Local_Connect()

        '        If Not IsNothing(DGV.Rows(Row).Cells(0).Value) Then

        '            Query = "DELETE FROM [MASCOL] WHERE [kdwarna]='" & DGV.Rows(Row).Cells(0).Value & "'"

        '            CMD = New SqlCommand
        '            CMD.Connection = Conn
        '            CMD.CommandText = Query
        '            CMD.ExecuteNonQuery()

        '            DGV.Rows.RemoveAt(Row)

        '        End If



        '    Catch ex As Exception
        '        Me.Cursor = Cursors.Default
        '        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    End Try
        'End If

    End Sub

    Private Sub DGV_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles DGV.EditingControlShowing

        '// uppercase textbox

        If TypeOf e.Control Is TextBox Then
            DirectCast(e.Control, TextBox).CharacterCasing = CharacterCasing.Upper
        End If

    End Sub

   
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click

        Dim COLNUM As String = ""
        Dim COLNAM As String = ""

        COLNUM = DGV.CurrentRow.Cells(0).Value
        COLNAM = DGV.CurrentRow.Cells(1).Value

        Query = "SELECT * FROM [MASPROD] WHERE [kdwarna]='" & COLNUM & "'"

        DS = New DataSet
        DA = New SqlDataAdapter(Query, Conn)
        DA.Fill(DS, "COLOR")

        If DS.Tables("COLOR").Rows.Count <> 0 Then
            Exit Sub
        End If

        If MessageBox.Show("Are you sure want to delete this record ?" & vbCrLf & vbCrLf & _
                           "Color Code" & Chr(9) & ":  " & COLNUM & vbCrLf & _
                           "Color Name" & Chr(9) & ":  " & COLNAM, "Confirmation", _
                           MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = vbNo Then

            Exit Sub

        End If

        Call Local_Connect()

        Query = "DELETE FROM [MASCOL] WHERE [kdwarna]='" & COLNUM & "'"

        CMD = New SqlCommand
        CMD.Connection = Conn
        CMD.CommandText = Query
        CMD.ExecuteNonQuery()


        Call Load_Data()


    End Sub

    Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click
        CARI = DGV.CurrentRow.Cells(0).Value.ToString
        Me.Dispose()
    End Sub

    Private Sub DGV_KeyDown(sender As Object, e As KeyEventArgs) Handles DGV.KeyDown
        If e.KeyCode = Keys.Enter Then
            Call btnSelect_Click(sender, e)
        End If
    End Sub
End Class