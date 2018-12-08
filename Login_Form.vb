Public Class Login_Form
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Load_Login(TextBox1.Text, TextBox2.Text)
        If dt_Login.Rows.Count > 0 Then
            If dt_Login.Rows(0).Item(1) = True Then
                MainForm.MainToolStripMenuItem.Enabled = True
            Else
                MainForm.MainToolStripMenuItem.Enabled = False
            End If
            If dt_Login.Rows(0).Item(2) = True Then
                MainForm.StatisticsToolStripMenuItem.Enabled = True
            Else
                MainForm.StatisticsToolStripMenuItem.Enabled = False
            End If
            If dt_Login.Rows(0).Item(3) = True Then
                MainForm.ManagToolStripMenuItem.Enabled = True
            Else
                MainForm.ManagToolStripMenuItem.Enabled = False
            End If
            MainForm.Show()
            Close()
        Else

            TextBox2.Text = ""
            TextBox1.SelectAll()
        End If
    End Sub
End Class