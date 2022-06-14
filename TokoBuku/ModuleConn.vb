Imports MySql.Data.MySqlClient
Module ModuleConn
    Public conn As MySqlConnection
    Public da As MySqlDataAdapter
    Public ds As DataSet
    Public mySet As String
    Public cmd As MySqlCommand
    Public rd As MySqlDataReader
    Sub connection()
        Try
            Dim mySet As String = "server=localhost; userid=root; password=; database=tokobuku_db; convert zero datetime=true;"
            conn = New MySqlConnection(mySet)
            If conn.State = ConnectionState.Closed Then
                conn.Open()
                'MsgBox("OK")
            End If
        Catch ex As Exception
            MsgBox("error : " + ex.Message)
        End Try
    End Sub
End Module
