Public Class User_Form
    Private Sub User_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Load_Employees()
        Dim emp_width As Double = DataGridView1.Width / 4
        With DataGridView1
            .DataSource = dt_Employees
            .Columns(0).Width = emp_width / 2
            .Columns(1).Width = emp_width
            .Columns(2).Width = emp_width
            .Columns(3).Width = emp_width
            .Columns(0).HeaderText = "Number"
            .Columns(1).HeaderText = "Name"
            .Columns(2).HeaderText = "Phone"
            .Columns(3).HeaderText = "State"
        End With

        Load_Users()
        Dim user_width As Double = DataGridView2.Width / 5
        With DataGridView2
            .DataSource = dt_Users
            .Columns(0).Width = user_width / 2
            .Columns(1).Width = user_width + 20
            .Columns(2).Width = user_width
            .Columns(3).Width = user_width
            .Columns(4).Width = user_width
            .Columns(0).HeaderText = "Number"
            .Columns(1).HeaderText = "Employee Name"
            .Columns(2).HeaderText = "Username"
            .Columns(3).HeaderText = "Password"
            .Columns(4).HeaderText = "Role"
        End With

        DataGridView1.CurrentRow.Selected = True
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick

        User_Password_Form.Label3.Text = DataGridView1.CurrentRow.Cells(0).Value
        User_Password_Form.Label4.Text = DataGridView1.CurrentRow.Cells(1).Value
        User_Password_Form.Show()
    End Sub

    Private Sub DataGridView2_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView2.DoubleClick

        Dim Role_Id_Index As Integer = Get_Role_Id_By_Role_Name(DataGridView2.CurrentRow.Cells(4).Value)
        With User_Password_Form
            .Label3.Text = DataGridView2.CurrentRow.Cells(0).Value
            .Label4.Text = DataGridView2.CurrentRow.Cells(1).Value
            .TextBox1.Text = DataGridView2.CurrentRow.Cells(2).Value
            .TextBox2.Text = DataGridView2.CurrentRow.Cells(3).Value

            .Show()
            .ComboBox1.SelectedValue = Role_Id_Index
            .Button1.Text = "UPDATE"
        End With

    End Sub
End Class