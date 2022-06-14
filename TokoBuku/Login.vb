Public Class Login

    Private Sub ButtonLogin_Click(sender As Object, e As EventArgs) Handles ButtonLogin.Click
        Try
            connection()
            cmd = New MySql.Data.MySqlClient.MySqlCommand("select * from tb_user where username = '" & TextBoxUsername.Text & "' AND
                                                                                    password = '" & TextBoxPassword.Text & "'", conn)
            rd = cmd.ExecuteReader
            rd.Read()
            If rd.HasRows Then
                Me.Hide()
                Class1.savedusername = TextBoxUsername.Text
                Class1.savedKodeAdmin = rd!id
                Main.Show()

            Else
                MsgBox("data tidak ditemukan")
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class
