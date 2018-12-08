Imports System.Data.SqlClient

Public Class Item_Form
    Dim Add_Edit As Boolean = True
    Dim item_id As Integer
    Private Sub Item_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Load_Items()
        DataGridView1.DataSource = dt_Items
        DataGridView1.Columns(0).HeaderText = "Item Number"
        DataGridView1.Columns(1).HeaderText = "Item Name"
        DataGridView1.Columns(2).HeaderText = "Item Price"
        DataGridView1.Columns(3).HeaderText = "Item Code"
        Dim width As Double = DataGridView1.Width / 4
        DataGridView1.Columns(0).Width = width
        DataGridView1.Columns(1).Width = width
        DataGridView1.Columns(2).Width = width
        DataGridView1.Columns(3).Width = width
        TextBox1.Enabled = False
        TextBox2.Enabled = False
        TextBox3.Enabled = False

    End Sub

    Public Sub Clear_All_Text()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox1.Enabled = True
        TextBox2.Enabled = True
        TextBox3.Enabled = True
        TextBox1.Select()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Clear_All_Text()
        Add_Edit = True

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        If Add_Edit = True Then
            If TextBox1.Text <> "" And TextBox2.Text <> "" And TextBox3.Text <> "" Then
                Insert_Items(TextBox1.Text, TextBox2.Text, TextBox3.Text)
            Else
                MsgBox("Enter The Data!!!!!")
            End If

        Else
            If TextBox1.Text <> "" And TextBox2.Text <> "" And TextBox3.Text <> "" Then
                Update_Items(TextBox1.Text, TextBox2.Text, TextBox3.Text, item_id)
            Else
                MsgBox("Enter The Data!!!!!")
            End If
        End If


        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox1.Enabled = False
        TextBox2.Enabled = False
        TextBox3.Enabled = False

        Load_Items()


    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Add_Edit = False
        TextBox1.Enabled = True
        TextBox2.Enabled = True
        TextBox3.Enabled = True
        item_id = DataGridView1.CurrentRow.Cells(0).Value
        TextBox1.Text = DataGridView1.CurrentRow.Cells(1).Value
        TextBox2.Text = DataGridView1.CurrentRow.Cells(2).Value
        TextBox3.Text = DataGridView1.CurrentRow.Cells(3).Value



    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        item_id = DataGridView1.CurrentRow.Cells(0).Value
        Delete_Items(item_id)
        Load_Items()
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged

    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        Add_Edit = False
        TextBox1.Enabled = True
        TextBox2.Enabled = True
        TextBox3.Enabled = True
        item_id = DataGridView1.CurrentRow.Cells(0).Value
        TextBox1.Text = DataGridView1.CurrentRow.Cells(1).Value
        TextBox2.Text = DataGridView1.CurrentRow.Cells(2).Value
        TextBox3.Text = DataGridView1.CurrentRow.Cells(3).Value
    End Sub
End Class