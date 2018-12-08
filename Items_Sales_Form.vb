Public Class Items_Sales_Form
    Private Sub Items_Sales_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Load_Items_Sales(DateTimePicker1.Value)

        With DataGridView1
            .DataSource = dt_Items_Sales

            .Columns(0).Width = 130
            .Columns(1).Width = 240
            .Columns(2).Width = 240
            .Columns(3).Width = 240

        End With

    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        Load_Items_Sales_Two_Dates(DateTimePicker1.Value, DateTimePicker2.Value)
        DataGridView1.DataSource = dt_Items_Sales
    End Sub

    Private Sub DateTimePicker2_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker2.ValueChanged
        Load_Items_Sales_Two_Dates(DateTimePicker1.Value, DateTimePicker2.Value)
        DataGridView1.DataSource = dt_Items_Sales
    End Sub
End Class