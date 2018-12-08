Public Class Sale_Form


    Private Sub Sale_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Load_Items()
        With DataGridView2
            .DataSource = dt_Items
            .Columns(0).HeaderText = "Id"
            .Columns(1).HeaderText = "Item Name"
            .Columns(2).HeaderText = "Item Price"
            .Columns(3).HeaderText = "Item Barcode"
            .Columns(0).Width = 20
            .Columns(1).Width = 100
            .Columns(2).Width = 80
            .Columns(3).Width = 100
        End With

        Label9.Text = Max_Sale_Bill_No_From_Sales(DateTimePicker1.Value) + 1
        Load_Sales_By_Date_Bill(DateTimePicker1.Value, Label9.Text)
        With DataGridView1
            .DataSource = dt_Sales_by_date_bill
            .Columns(0).HeaderText = "Sale Number"
            .Columns(1).HeaderText = "Sale Date"
            .Columns(2).HeaderText = "Bil Number"
            .Columns(3).HeaderText = "Item Name"
            .Columns(4).HeaderText = "Item Quintity"
            .Columns(5).HeaderText = "Item Price"
            .Columns(6).HeaderText = "Total Price"
            .Columns(7).HeaderText = "Customer Number"
            .Columns(1).Width = 95
            .Columns(2).Width = 95
            .Columns(3).Width = 95
            .Columns(4).Width = 95
            .Columns(5).Width = 95
            .Columns(6).Width = 95
            .Columns(0).Visible = False
            .Columns(7).Visible = False
        End With



    End Sub



    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

        If TextBox1.Text = "" Then
            TextBox1.Text = "Search By Barcode"
            TextBox1.ForeColor = Color.Gray
            TextBox1.SelectAll()
            Load_Items()
            DataGridView2.DataSource = dt_Items
        End If

        If TextBox1.Text <> "Search By Barcode" Then
            TextBox1.ForeColor = Color.Black
            Filter_By_Item_Barcode(TextBox1.Text)
            DataGridView2.DataSource = DV_Filter_Item_Barcode

        End If


    End Sub

    Private Sub TextBox1_MouseClick(sender As Object, e As MouseEventArgs) Handles TextBox1.MouseClick
        TextBox1.SelectAll()
    End Sub



    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Customer_Form.MdiParent = MainForm
        Customer_Form.Show()
        Customer_Form.Button1.Visible = False
        Customer_Form.Button2.Visible = False
        Customer_Form.Button3.Visible = False

        dt_Sales_by_date_bill.Clear()
    End Sub

    Private Sub DataGridView2_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView2.DoubleClick
        If Label2.Text <> "0" Then
            If DataGridView1.Rows.Count() >= 0 Then
                Button3.Enabled = True
                Button4.Enabled = True

            End If

            If Count_Sales_By_Bill_No_AND_Sale_Date_AND_Item_Id(Label9.Text, DateTimePicker1.Value, DataGridView2.CurrentRow.Cells(0).Value) > 0 Then
                    Increase_Sales_Item_Quntity_AND_Total(Label9.Text, DateTimePicker1.Value, DataGridView2.CurrentRow.Cells(0).Value, DataGridView2.CurrentRow.Cells(2).Value)
                    Load_Sales_By_Date_Bill(DateTimePicker1.Value, Label9.Text)
                    DataGridView1.CurrentRow.Cells(1).Selected = False
                    DataGridView1.Rows(DataGridView1.Rows.Count - 1).Cells(4).Selected = True
                    Label5.Text = Count_All_Items_In_One_Sale()
                    Label7.Text = Calculate_Bill()
                Else
                    Insert_Sales(DateTimePicker1.Value, Label9.Text, DataGridView2.CurrentRow.Cells(0).Value, 1, DataGridView2.CurrentRow.Cells(2).Value, 1 * DataGridView2.CurrentRow.Cells(2).Value, Label2.Text)
                    Load_Sales_By_Date_Bill(DateTimePicker1.Value, Label9.Text)
                    DataGridView1.CurrentRow.Cells(1).Selected = False
                    DataGridView1.Rows(DataGridView1.Rows.Count - 1).Cells(4).Selected = True

                    Label5.Text = Count_All_Items_In_One_Sale()
                    Label7.Text = Calculate_Bill()

                End If


            Else
                Button1.PerformClick()
        End If

    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.ColumnIndex = 4 Then
            Item_QunitityForm.Show()

        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Delete_Sales(DataGridView1.CurrentRow.Cells(0).Value)
        Load_Sales_By_Date_Bill(DateTimePicker1.Value, Label9.Text)
        Label5.Text = Count_All_Items_In_One_Sale()
        Label5.Text = Count_All_Items_In_One_Sale()
        Label7.Text = Calculate_Bill()

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        'Load_Sale_View(Label9.Text, DateTimePicker1.Value)
        'Dim report As New CrystalReport1
        'report.SetDataSource(dt_Sale_View)

        'Dim regDate As Date = Date.Now()
        'Dim strDate As String = regDate.ToString("ddMMMyyyy")
        'Dim Report_Name As String = "raport" + Label9.Text + "_" + Label4.Text + "_" + strDate
        'report.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, "C:\Users\data\Documents\Visual Studio 2017\Projects\Free_Sale\Free_Sale\Bill\" + Report_Name + ".PDF")
        Label9.Text += 1
        Load_Sales_By_Date_Bill(DateTimePicker1.Value, Label9.Text)
        Button3.Enabled = False
        Button4.Enabled = False


    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Load_Sale_View(Label9.Text, DateTimePicker1.Value)
        Dim report As New CrystalReport1
        report.SetDataSource(dt_Sale_View)
        Report_View_Form.CrystalReportViewer1.ReportSource = report
        Report_View_Form.CrystalReportViewer1.Zoom(100%)
        'report.PrintToPrinter(2, False, 0, 0)
        'Report_View_Form.CrystalReportViewer1.PrintReport()
        Report_View_Form.Show()

        'Dim regDate As Date = Date.Now()
        'Dim strDate As String = regDate.ToString("ddMMMyyyy")
        'Dim Report_Name As String = "raport" + Label9.Text + "_" + Label4.Text + "_" + strDate
        'report.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, "C:\Users\data\Documents\Visual Studio 2017\Projects\Free_Sale\Free_Sale\Bill\" + Report_Name + ".PDF")
        'Label9.Text += 1
        'Load_Sales_By_Date_Bill(DateTimePicker1.Value, Label9.Text)
        'Button3.Enabled = False

    End Sub
End Class