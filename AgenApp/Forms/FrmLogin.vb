Imports System.Data.SqlClient

Public Class FrmLogin

    Private Sub FrmLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load


    End Sub


    Private Sub txtUser_Click(sender As Object, e As EventArgs) Handles txtUser.Click
        If Trim(txtUser.Text) = "Username" Then
            txtUser.Text = ""
            txtUser.ForeColor = Color.Black
        End If
    End Sub

    Private Sub txtUser_Leave(sender As Object, e As EventArgs) Handles txtUser.Leave
        If Trim(txtUser.Text) = "" Then
            txtUser.Text = " Username"
            txtUser.ForeColor = Color.Gray
        End If
    End Sub

    Private Sub txtPass_Click(sender As Object, e As EventArgs) Handles txtPass.Click
        If Trim(txtPass.Text) = "Password" Then
            txtPass.Text = ""
            txtPass.ForeColor = Color.Black
            txtPass.PasswordChar = "*"
        End If
    End Sub


    Private Sub txtPass_Leave(sender As Object, e As EventArgs) Handles txtPass.Leave
        If Trim(txtPass.Text) = "" Then
            txtPass.Text = " Password"
            txtPass.ForeColor = Color.Gray
            txtPass.PasswordChar = ""
        End If
    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click

       


    End Sub

    Private Sub SqlConnection(p1 As Object)

    End Sub

  
End Class