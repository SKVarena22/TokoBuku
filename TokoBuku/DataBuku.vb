Public Class DataBuku
    Private Sub DataBuku_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Main.Show()
    End Sub

    Private Sub DataBuku_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call connection()
        showData()
        kondisiAwal()
        nomorOtomatis()
    End Sub

    Sub nomorOtomatis()
        Call connection()
        cmd = New MySql.Data.MySqlClient.MySqlCommand("Select * from tb_buku where kode_buku in (select max(kode_buku) from tb_buku)", conn)
        Dim urutanKode As String
        Dim hitung As Long
        rd = cmd.ExecuteReader
        rd.Read()
        If Not rd.HasRows Then
            urutanKode = "BK" + "001"
        Else
            hitung = Microsoft.VisualBasic.Right(rd.GetString(0), 3) + 1
            urutanKode = "BK" + Microsoft.VisualBasic.Right("000" & hitung, 3)
        End If
        TextBoxKodeBuku.Text = urutanKode
        rd.Close()

    End Sub

    Sub kondisiAwal()
        TextBoxKodeBuku.Enabled = False
        TextBoxJudul.Enabled = False
        TextBoxPenulis.Enabled = False
        TextBoxPenerbit.Enabled = False
        TextBoxStok.Enabled = False
        TextBoxHarga.Enabled = False


        TextBoxJudul.Text = ""
        TextBoxPenulis.Text = ""
        TextBoxPenerbit.Text = ""
        TextBoxStok.Text = ""
        TextBoxHarga.Text = ""

        ButtonBatal.Enabled = True
        ButtonSimpan.Enabled = True

        ButtonTambah.Enabled = True
        ButtonEdit.Enabled = False
        ButtonHapus.Enabled = False
    End Sub
    Sub showData()
        Dim data As Integer
        da = New MySql.Data.MySqlClient.MySqlDataAdapter("Select tb_buku.kode_buku, tb_buku.judul_buku, tb_buku.penulis, tb_buku.penerbit, tb_buku.stok, tb_buku.harga, tb_user.username 
                                                          From tb_buku 
                                                          INNER Join tb_user ON (tb_user.id = tb_buku.user_id)", conn)
        Dim dt As New DataTable()
        data = da.Fill(dt)
        If (data > 0) Then
            DataGridView1.DataSource = dt
            DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            DataGridView1.Columns(0).HeaderText = "Kode Buku"
            DataGridView1.Columns(1).HeaderText = "Judul Buku"
            DataGridView1.Columns(2).HeaderText = "Penulis"
            DataGridView1.Columns(3).HeaderText = "Penerbit"
            DataGridView1.Columns(4).HeaderText = "Stok"
            DataGridView1.Columns(5).HeaderText = "Harga"
            DataGridView1.Columns(6).HeaderText = "Admin"

        Else
            DataGridView1.DataSource = Nothing
        End If
    End Sub

    Private Sub ButtonCari_Click(sender As Object, e As EventArgs) Handles ButtonCari.Click
        Dim data As Integer
        da = New MySql.Data.MySqlClient.MySqlDataAdapter("SELECT tb_buku.kode_buku, tb_buku.judul_buku, tb_buku.penulis, tb_buku.penerbit, tb_buku.stok, tb_buku.harga, tb_user.username
                                                          FROM tb_buku 
                                                          INNER Join tb_user ON (tb_user.id = tb_buku.user_id)
                                                          WHERE
                                                          tb_buku.kode_buku LIKE '%" & TextBoxCari.Text & "%' OR
                                                          tb_buku.judul_buku LIKE '%" & TextBoxCari.Text & "%' OR
                                                          tb_buku.penulis LIKE '%" & TextBoxCari.Text & "%' OR
                                                          tb_buku.penerbit LIKE '%" & TextBoxCari.Text & "%' OR
                                                          tb_buku.stok LIKE '%" & TextBoxCari.Text & "%' OR
                                                          tb_buku.harga LIKE '%" & TextBoxCari.Text & "%' OR
                                                          tb_user.username LIKE '%" & TextBoxCari.Text & "%'", conn)

        Dim dt As New DataTable()
        data = da.Fill(dt)

        If (data > 0) Then
            DataGridView1.DataSource = dt
            DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            DataGridView1.Columns(0).HeaderText = "Kode Buku"
            DataGridView1.Columns(1).HeaderText = "Judul Buku"
            DataGridView1.Columns(2).HeaderText = "Penulis"
            DataGridView1.Columns(3).HeaderText = "Penerbit"
            DataGridView1.Columns(4).HeaderText = "Stok"
            DataGridView1.Columns(5).HeaderText = "Harga"
            DataGridView1.Columns(6).HeaderText = "Admin"

        Else
            DataGridView1.DataSource = Nothing
        End If
    End Sub

    Private Sub ButtonTambah_Click(sender As Object, e As EventArgs) Handles ButtonTambah.Click

        TextBoxJudul.Enabled = True
        TextBoxPenulis.Enabled = True
        TextBoxPenerbit.Enabled = True
        TextBoxStok.Enabled = True
        TextBoxHarga.Enabled = True


        TextBoxJudul.Text = ""
        TextBoxPenulis.Text = ""
        TextBoxPenerbit.Text = ""
        TextBoxStok.Text = ""
        TextBoxHarga.Text = ""

        ButtonBatal.Enabled = True
        ButtonSimpan.Enabled = True

        ButtonTambah.Enabled = False
        ButtonEdit.Enabled = False
        ButtonHapus.Enabled = False
    End Sub

    Private Sub ButtonSimpan_Click(sender As Object, e As EventArgs) Handles ButtonSimpan.Click

        Try
            cmd = New MySql.Data.MySqlClient.MySqlCommand("INSERT INTO tb_buku (tb_buku.kode_buku, tb_buku.judul_buku, tb_buku.penulis, tb_buku.penerbit, tb_buku.stok, tb_buku.harga, tb_buku.user_id)
                                                           VALUES 
                                                           (
                                                            '" & TextBoxKodeBuku.Text & "',
                                                            '" & TextBoxJudul.Text & "',
                                                            '" & TextBoxPenulis.Text & "',
                                                            '" & TextBoxPenerbit.Text & "',
                                                            '" & TextBoxStok.Text & "',
                                                            '" & TextBoxHarga.Text & "',
                                                            '" & Class1.savedKodeAdmin & "'
                                                           )", conn)
            cmd.ExecuteNonQuery()
            MsgBox("data berhasil disimpan")
            nomorOtomatis()
            showData()
            kondisiAwal()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        TextBoxKodeBuku.Enabled = False
        TextBoxJudul.Enabled = True
        TextBoxPenulis.Enabled = True
        TextBoxPenerbit.Enabled = True
        TextBoxStok.Enabled = True
        TextBoxHarga.Enabled = True

        ButtonBatal.Enabled = True
        ButtonSimpan.Enabled = False

        ButtonTambah.Enabled = False
        ButtonEdit.Enabled = True
        ButtonHapus.Enabled = True
        Dim i As Integer
        i = DataGridView1.CurrentRow.Index
        With DataGridView1.Rows.Item(i)
            TextBoxKodeBuku.Text = .Cells(0).Value
            TextBoxJudul.Text = .Cells(1).Value
            TextBoxPenulis.Text = .Cells(2).Value
            TextBoxPenerbit.Text = .Cells(3).Value
            TextBoxStok.Text = .Cells(4).Value
            TextBoxHarga.Text = .Cells(5).Value

        End With
    End Sub

    Private Sub ButtonEdit_Click(sender As Object, e As EventArgs) Handles ButtonEdit.Click
        Try
            cmd = New MySql.Data.MySqlClient.MySqlCommand("UPDATE tb_buku
                                                           SET
                                                            judul_buku = '" & TextBoxJudul.Text & "',
                                                            penulis = '" & TextBoxPenulis.Text & "',
                                                            penerbit = '" & TextBoxPenerbit.Text & "',
                                                            stok = '" & TextBoxStok.Text & "',
                                                            harga = '" & TextBoxHarga.Text & "',
                                                            user_id = '" & Class1.savedKodeAdmin & "'
                                                           WHERE 
                                                            kode_buku = '" & TextBoxKodeBuku.Text & "'", conn)


            cmd.ExecuteNonQuery()
            MsgBox("data berhasil diupdate")
            nomorOtomatis()
            showData()
            kondisiAwal()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ButtonHapus_Click(sender As Object, e As EventArgs) Handles ButtonHapus.Click
        Try
            cmd = New MySql.Data.MySqlClient.MySqlCommand("DELETE FROM `tokobuku_db`.`tb_buku` WHERE  `kode_buku`= '" & TextBoxKodeBuku.Text & "'", conn)

            cmd.ExecuteNonQuery()
            MsgBox("data berhasil dihapus")
            nomorOtomatis()
            showData()
            kondisiAwal()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ButtonBatal_Click(sender As Object, e As EventArgs) Handles ButtonBatal.Click
        kondisiAwal()
    End Sub
End Class