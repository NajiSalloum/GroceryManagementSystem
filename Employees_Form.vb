Public Class Employees_Form
    Private Sub ClearAll()
        txtName.Text = ""
        txtPhone.Text = ""
        txtName.Select()
    End Sub


    Private Sub Employees_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ClearAll()
        Load_Employees()
        Dim width As Double = DataGridView1.Width / 4
        With DataGridView1
            .DataSource = dt_Employees
            .Columns(0).Width = width
            .Columns(1).Width = width
            .Columns(2).Width = width
            .Columns(3).Width = width
            .Columns(0).HeaderText = "Number"
            .Columns(1).HeaderText = "Name"
            .Columns(2).HeaderText = "Phone"
            .Columns(3).HeaderText = "State"
        End With


    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Insert_Employee(txtName.Text, txtPhone.Text, True)
        Load_Employees()
        ClearAll()
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        txtName.Text = DataGridView1.CurrentRow.Cells(1).Value
        txtPhone.Text = DataGridView1.CurrentRow.Cells(2).Value

        Button1.Enabled = False
        Button2.Enabled = True
        Button3.Enabled = True

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        ClearAll()
        Button1.Enabled = True
        Button2.Enabled = False
        Button3.Enabled = False
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Update_Employee(txtName.Text, txtPhone.Text, True, DataGridView1.CurrentRow.Cells(0).Value)
        Load_Employees()
        Button4.PerformClick()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ' Delete_Employee(DataGridView1.CurrentRow.Cells(0).Value)
        Update_Employee(DataGridView1.CurrentRow.Cells(1).Value, DataGridView1.CurrentRow.Cells(2).Value, False, DataGridView1.CurrentRow.Cells(0).Value)
        Load_Employees()
        Button4.PerformClick()
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If TextBox1.Text = "" Then
            TextBox1.Text = "Search By Name OR Phone"
            TextBox1.ForeColor = Color.Gray
            TextBox1.SelectAll()

            DataGridView1.DataSource = dt_Employees
        End If

        If TextBox1.Text <> "Search By Name OR Phone" Then
            TextBox1.ForeColor = Color.Black
            Fiter_By_Employee_Name_OR_Phone(TextBox1.Text)
            DataGridView1.DataSource = DV_Filter_Employee_Name_OR_Phone

        End If
    End Sub

    Private Sub TextBox1_MouseClick(sender As Object, e As MouseEventArgs) Handles TextBox1.MouseClick
        TextBox1.Text = ""
    End Sub
End Class