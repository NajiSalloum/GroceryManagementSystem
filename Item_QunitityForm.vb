Public Class Item_QunitityForm
    Private Sub NumericUpDown1_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown1.ValueChanged
        Sale_Form.DataGridView1.CurrentRow.Cells(4).Value = NumericUpDown1.Value
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If NumericUpDown1.Value = 0 Then
            Sale_Form.Button2.PerformClick()
            Close()
        Else
            Update_Sales_Item_Quntity_AND_Total(Sale_Form.DataGridView1.CurrentRow.Cells(0).Value, NumericUpDown1.Value, Sale_Form.DataGridView1.CurrentRow.Cells(5).Value)

            Load_Sales_By_Date_Bill(Sale_Form.DateTimePicker1.Value, Sale_Form.Label9.Text)
            Sale_Form.Label7.Text = Calculate_Bill()
            Close()
            Sale_Form.DataGridView1.CurrentRow.Cells(1).Selected = False
            Sale_Form.DataGridView1.Rows(Sale_Form.DataGridView1.Rows.Count - 1).Cells(4).Selected = True
            Sale_Form.Label5.Text = Count_All_Items_In_One_Sale()

        End If



    End Sub

    Private Sub Item_QunitityForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '  MsgBox(Sale_Form.DataGridView1.CurrentRow.Cells(4))

    End Sub
End Class