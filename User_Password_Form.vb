Public Class User_Password_Form
    Private Sub ClearAll()
        TextBox1.Text = ""
        TextBox2.Text = ""
        ComboBox1.SelectedIndex = 0
    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Close()
    End Sub

    Private Sub User_Password_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Load_Roles()
        Get_Roles()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Button1.Text = "ADD" Then
            Insert_UP(Label3.Text, TextBox1.Text, TextBox2.Text, ComboBox1.SelectedValue)

        Else
            Update_UP(TextBox1.Text, TextBox2.Text, ComboBox1.SelectedValue, User_Form.DataGridView2.CurrentRow.Cells(0).Value)

        End If



        ClearAll()
        Close()
        Load_Users()
        User_Form.DataGridView2.DataSource = dt_Users
    End Sub
End Class