Public Class Roles_Form
    Private Sub ClearAll()
        TextBox1.Text = ""
        CheckBox1.Checked = True
        CheckBox2.Checked = False
        CheckBox3.Checked = False
        TextBox1.Select()
        Button2.Enabled = False

    End Sub

    Private Sub Roles_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ClearAll()
        Load_Roles()
        Dim width As Double = DataGridView1.Width / 5
        With DataGridView1
            .DataSource = dt_Roles
            .Columns(0).Width = width / 2
            .Columns(1).Width = width * 2
            .Columns(2).Width = width / 1.5
            .Columns(3).Width = width / 1.5
            .Columns(4).Width = width / 1.5
            .Columns(0).HeaderText = "Number"
            .Columns(1).HeaderText = "Name"
            .Columns(2).HeaderText = "Main"
            .Columns(3).HeaderText = "Statistics"
            .Columns(4).HeaderText = "Control"
        End With
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Insert_Role(TextBox1.Text, CheckBox1.Checked, CheckBox2.Checked, CheckBox3.Checked)
        Load_Roles()
        ClearAll()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Update_Role(TextBox1.Text, CheckBox1.Checked, CheckBox2.Checked, CheckBox3.Checked, DataGridView1.CurrentRow.Cells(0).Value)
        Load_Roles()
        Button3.PerformClick()

    End Sub



    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        TextBox1.Text = DataGridView1.CurrentRow.Cells(1).Value
        CheckBox1.Checked = DataGridView1.CurrentRow.Cells(2).Value
        CheckBox2.Checked = DataGridView1.CurrentRow.Cells(3).Value
        CheckBox3.Checked = DataGridView1.CurrentRow.Cells(4).Value

        Button1.Enabled = False
        Button2.Enabled = True
        Button3.Enabled = True

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ClearAll()
        Button1.Enabled = True
        Button2.Enabled = False
    End Sub
End Class