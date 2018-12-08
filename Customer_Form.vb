Public Class Customer_Form
    Private Sub Customer_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Load_Customers()
        DataGridView1.DataSource = dt_Customers
        DataGridView1.Columns(0).ReadOnly = True
        DataGridView1.Columns(0).HeaderText = "Customer Number"
        DataGridView1.Columns(1).HeaderText = "Customer Name"
        DataGridView1.Columns(2).HeaderText = "Customer Mobile"
        DataGridView1.Columns(3).HeaderText = "Customer Address"
        Dim width As Double = DataGridView1.Width / 4
        DataGridView1.Columns(0).Width = width
        DataGridView1.Columns(1).Width = width
        DataGridView1.Columns(2).Width = width
        DataGridView1.Columns(3).Width = width

    End Sub
    Public Function GetMax()
        Dim MaxNumber As Integer
        For i As Integer = 0 To DataGridView1.RowCount - 2
            If MaxNumber < DataGridView1.Rows(i).Cells(0).Value Then
                MaxNumber = DataGridView1.Rows(i).Cells(0).Value
            End If
        Next
        Return MaxNumber
    End Function

    Private Sub DataGridView1_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellEnter
        If IsDBNull(DataGridView1.CurrentRow.Cells(0).Value) Then
            DataGridView1.CurrentRow.Cells(0).Value = GetMax() + 1

        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        For i As Integer = 0 To DataGridView1.Rows().Count - 2
            With DataGridView1.Rows(i)
                If Is_Customer_Exist(.Cells(0).Value) = 1 Then
                    Update_Customers(.Cells(1).Value, .Cells(2).Value, .Cells(3).Value, .Cells(0).Value)

                Else

                    Insert_Customers(.Cells(0).Value, .Cells(1).Value, .Cells(2).Value, .Cells(3).Value)



                End If
            End With
        Next
        Load_Customers()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Delete_Customrt(DataGridView1.CurrentRow.Cells(0).Value)
        MsgBox("Is Deleted!!!")
        Load_Customers()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Sales_Module.customer_id = DataGridView1.CurrentRow.Cells(0).Value
        Sales_Module.customer_name = DataGridView1.CurrentRow.Cells(1).Value
        Sale_Form.Label2.Text = customer_id
        Sale_Form.Label4.Text = customer_name
        Close()
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If Button1.Visible = False Then
            DataGridView1.AllowUserToAddRows = False
            Sales_Module.customer_id = DataGridView1.CurrentRow.Cells(0).Value
            Sales_Module.customer_name = DataGridView1.CurrentRow.Cells(1).Value
            Sale_Form.Label2.Text = customer_id
            Sale_Form.Label4.Text = customer_name


            Close()
        End If
    End Sub
End Class