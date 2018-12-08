Public Class Sale__Statistics_Form
    Private Sub Sale__Statistics_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Get_Items_Names()
        ComboBox1.DataSource = dt_Items_Names
        ComboBox1.DisplayMember = "item-name"
        ComboBox1.ValueMember = "item_id"


        Load_Sale_View_Statistics(DateTimePicker1.Value)
        With DataGridView1
            .DataSource = dt_Sale_View_Statistics

            .Columns(0).Width = 80
            .Columns(1).Width = 112
            .Columns(2).Width = 80
            .Columns(3).Width = 120
            .Columns(4).Width = 110
            .Columns(5).Width = 110
            .Columns(6).Width = 110
            .Columns(7).Width = 125
        End With

        Total_Count_Sale()


    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        Load_Sale_View_Statistics_Two_Dates(DateTimePicker1.Value, DateTimePicker2.Value)
        DataGridView1.DataSource = dt_Sale_View_Statistics
        Total_Count_Sale()
    End Sub

    Private Sub DateTimePicker2_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker2.ValueChanged
        Load_Sale_View_Statistics_Two_Dates(DateTimePicker1.Value, DateTimePicker2.Value)
        DataGridView1.DataSource = dt_Sale_View_Statistics
        Total_Count_Sale()
    End Sub


    Public Sub Total_Count_Sale()
        Dim Sum As Double = 0
        Dim Count As Integer = 0
        For i As Integer = 0 To DataGridView1.Rows.Count() - 1
            Sum += DataGridView1.Item("total", i).Value
            Count += DataGridView1.Item("item_qunitity", i).Value
        Next
        Label2.Text = Sum
        Label5.Text = Count
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If TextBox1.Text = "" Then
            TextBox1.Text = "Search By Name"
            TextBox1.ForeColor = Color.Gray
            TextBox1.SelectAll()

            DataGridView1.DataSource = dt_Sale_View_Statistics
        End If

        If TextBox1.Text <> "Search By Name" Then
            TextBox1.ForeColor = Color.Black
            Fiter_By_Customer_Name(TextBox1.Text)
            DataGridView1.DataSource = DV

        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Fiter_By_Item_Name(ComboBox1.Text)
        DataGridView1.DataSource = DV
        Total_Count_Sale()

    End Sub

    Private Sub TextBox1_MouseClick(sender As Object, e As MouseEventArgs) Handles TextBox1.MouseClick
        TextBox1.Text = ""
    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click

    End Sub
End Class