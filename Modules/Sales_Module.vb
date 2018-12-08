Imports System.Data.SqlClient

Module Sales_Module
    Public customer_id As Integer
    Public customer_name As String
    Public dt_Sales As New DataTable
    Public Sub Load_Sales()
        dt_Sales.Clear()
        Dim cmd As New SqlCommand("SELECT * FROM Sales", connSQL_Server)
        connSQL_Server.Open()
        dt_Sales.Load(cmd.ExecuteReader)
        connSQL_Server.Close()
        cmd = Nothing
    End Sub

    Public dt_Sales_by_date_bill As New DataTable
    Public Sub Load_Sales_By_Date_Bill(ByVal sale_date As Date, ByVal sale_bill_no As Integer)
        dt_Sales_by_date_bill.Clear()
        Try
            Dim cmd As New SqlCommand("SELECT sale_no, sale_date, sale_bill_no, Items.[item-name] , item_qunitity, Sales.item_price, total, customer_id FROM Sales, Items WHERE Sales.item_id = Items.item_id and Sales.sale_date=@sale_date And Sales.sale_bill_no=@sale_bill_no", connSQL_Server)
            cmd.Parameters.Add("sale_date", SqlDbType.Date).Value = sale_date
            cmd.Parameters.Add("sale_bill_no", SqlDbType.Int).Value = sale_bill_no
            connSQL_Server.Open()
            dt_Sales_by_date_bill.Load(cmd.ExecuteReader)
            connSQL_Server.Close()
            cmd = Nothing
        Catch
            connSQL_Server.Close()
        End Try

    End Sub

    Public Function Max_Sale_Bill_No_From_Sales(ByVal sale_date As Date)
        Dim Number As Integer
        Try
            Dim cmd As New SqlCommand("SELECT Max(sale_bill_no) FROM Sales WHERE [sale_date]=@sale_date", connSQL_Server)
            cmd.Parameters.Add("sale_date", SqlDbType.Date).Value = sale_date
            connSQL_Server.Open()
            Number = cmd.ExecuteScalar
            connSQL_Server.Close()
        Catch
            Number = 0
            connSQL_Server.Close()
        End Try
        Return Number
    End Function

    Public Function Max_Sale_No_From_Sales()
        Dim Number As Integer
        Try
            Dim cmd As New SqlCommand("SELECT Max(sale_no) FROM Sales", connSQL_Server)
            connSQL_Server.Open()
            Number = cmd.ExecuteScalar
            connSQL_Server.Close()
        Catch
            Number = 0
            connSQL_Server.Close()
        End Try
        Return Number
    End Function

    Public Sub Insert_Sales(ByVal sale_date As Date, ByVal sale_bill_no As Integer, ByVal item_id As Integer, ByVal item_qunitity As Double, ByVal item_price As Double, ByVal total As Double, ByVal customer_id As Integer)
        Dim cmd As New SqlCommand("insert into sales ([sale_no], [sale_date], [sale_bill_no], [item_id], [item_qunitity], [item_price], [total], [customer_id]) values(@sale_no,@sale_date,@sale_bill_no,@item_id,@item_qunitity,@item_price,@total,@customer_id)", connSQL_Server)
        cmd.Parameters.Add("sale_no", SqlDbType.Int).Value = Max_Sale_No_From_Sales() + 1
        cmd.Parameters.Add("sale_date", SqlDbType.Date).Value = sale_date
        cmd.Parameters.Add("sale_bill_no", SqlDbType.Decimal).Value = sale_bill_no
        cmd.Parameters.Add("item_id", SqlDbType.Int).Value = item_id
        cmd.Parameters.Add("item_qunitity", SqlDbType.Int).Value = item_qunitity
        cmd.Parameters.Add("item_price", SqlDbType.Decimal).Value = item_price
        cmd.Parameters.Add("total", SqlDbType.Decimal).Value = total
        cmd.Parameters.Add("customer_id", SqlDbType.Int).Value = customer_id
        connSQL_Server.Open()
        cmd.ExecuteNonQuery()
        connSQL_Server.Close()
        cmd = Nothing

    End Sub
    Public Sub Update_Sales_Item_Quntity_AND_Total(ByVal sale_no As Integer, ByVal item_qunitity As Double, ByVal item_price As Double)
        Dim NewTotal As Double = item_qunitity * item_price
        Dim cmd As New SqlCommand("UPDATE Sales SET [item_qunitity]=@item_qunitity, [total]='" & NewTotal & "' WHERE [sale_no]=@sale_no", connSQL_Server)
        cmd.Parameters.Add("sale_no", SqlDbType.Int).Value = sale_no
        cmd.Parameters.Add("item_qunitity", SqlDbType.Int).Value = item_qunitity

        connSQL_Server.Open()
        cmd.ExecuteNonQuery()
        connSQL_Server.Close()
        cmd = Nothing

    End Sub
    Public Function Get_Item_Qunitity_By_Sale_Bill_No_AND_Sale_Date_AND_Item_Id(ByVal sale_bill_no As Integer, ByVal sale_date As Date, ByVal item_id As Integer)
        Dim Count As Integer
        Try
            Dim cmd As New SqlCommand("SELECT [item_qunitity] FROM [Free_Sales].[dbo].[Sales] where [sale_bill_no]=@sale_bill_no and [sale_date]=@sale_date and [item_id]=@item_id", connSQL_Server)
            cmd.Parameters.Add("sale_bill_no", SqlDbType.Int).Value = sale_bill_no
            cmd.Parameters.Add("sale_date", SqlDbType.Date).Value = sale_date
            cmd.Parameters.Add("item_id", SqlDbType.Int).Value = item_id
            connSQL_Server.Open()
            Count = cmd.ExecuteScalar
            connSQL_Server.Close()
        Catch
            Count = 0
            connSQL_Server.Close()
        End Try
        Return Count
    End Function

    Public Sub Increase_Sales_Item_Quntity_AND_Total(ByVal sale_bill_no As Integer, ByVal sale_date As Date, ByVal item_id As Integer, ByVal item_price As Double)
        Dim Count As Integer
        Count = Get_Item_Qunitity_By_Sale_Bill_No_AND_Sale_Date_AND_Item_Id(sale_bill_no, sale_date, item_id)
        Dim NewTotal As Double = (Count + 1) * item_price

        Dim cmd As New SqlCommand("UPDATE Sales SET [item_qunitity]='" & Count + 1 & "', [total]='" & NewTotal & "' WHERE [sale_bill_no]=@sale_bill_no AND [sale_date]=@sale_date AND [item_id]=@item_id", connSQL_Server)
        cmd.Parameters.Add("sale_bill_no", SqlDbType.Int).Value = sale_bill_no
        cmd.Parameters.Add("sale_date", SqlDbType.Date).Value = sale_date
        cmd.Parameters.Add("item_id", SqlDbType.Int).Value = item_id

        connSQL_Server.Open()
        cmd.ExecuteNonQuery()
        connSQL_Server.Close()
        cmd = Nothing

    End Sub


    Public Sub Delete_Sales(ByVal sale_no As Integer)
        Dim cmd As New SqlCommand("DELETE FROM Sales WHERE [sale_no]=@sale_no", connSQL_Server)
        cmd.Parameters.Add("sale_no", SqlDbType.Int).Value = sale_no


        connSQL_Server.Open()
        cmd.ExecuteNonQuery()
        connSQL_Server.Close()
        cmd = Nothing

    End Sub

    Public Function Count_Sales_By_Bill_No_AND_Sale_Date_AND_Item_Id(ByVal sale_bill_no As Integer, ByVal sale_date As Date, ByVal item_id As Integer)
        Dim Count As Integer
        Try
            Dim cmd As New SqlCommand("SELECT Count(*) FROM Sales WHERE [sale_bill_no]=@sale_bill_no and [sale_date]=@sale_date and [item_id]=@item_id", connSQL_Server)
            cmd.Parameters.Add("sale_bill_no", SqlDbType.Int).Value = sale_bill_no
            cmd.Parameters.Add("sale_date", SqlDbType.Date).Value = sale_date
            cmd.Parameters.Add("item_id", SqlDbType.Int).Value = item_id
            connSQL_Server.Open()
            Count = cmd.ExecuteScalar
            connSQL_Server.Close()
        Catch
            Count = 0
            connSQL_Server.Close()
        End Try
        Return Count
    End Function

    Public Function Count_All_Items_In_One_Sale()
        Dim Count As Integer
        Count = Sale_Form.DataGridView1.Rows.Count()
        Return Count
    End Function

    Public Function Calculate_Bill()
        Dim Sum As Double = 0
        For i As Integer = 0 To Sale_Form.DataGridView1.Rows.Count - 1
            Sum += Sale_Form.DataGridView1.Rows(i).Cells("total").Value
        Next
        Return Sum
    End Function

    Public dt_Sale_View As New DataTable
    Public Sub Load_Sale_View(sale_bill_no As Integer, sale_date As Date)
        dt_Sale_View.Clear()
        Try

            Dim cmd As New SqlCommand("SELECT sale_date, sale_bill_no, item_qunitity, item_price, total, customer_name, [item-name], customer_mob, customer_address FROM Sale_View WHERE [sale_bill_no]=@sale_bill_no AND [sale_date]=@sale_date", connSQL_Server)
            cmd.Parameters.Add("sale_bill_no", SqlDbType.Int).Value = sale_bill_no
            cmd.Parameters.Add("sale_date", SqlDbType.Date).Value = sale_date
            connSQL_Server.Open()
            dt_Sale_View.Load(cmd.ExecuteReader)
            connSQL_Server.Close()
            cmd = Nothing

        Catch
            connSQL_Server.Close()
        End Try

    End Sub

    Public dt_Sale_View_Statistics As New DataTable
    Public Sub Load_Sale_View_Statistics(sale_date As Date)
        dt_Sale_View_Statistics.Clear()
        Try

            Dim cmd As New SqlCommand("SELECT sale_no, sale_date, sale_bill_no, [item-name], item_price, item_qunitity, total, customer_name FROM Sale_Statistics_View WHERE [sale_date]=@sale_date", connSQL_Server)
            cmd.Parameters.Add("sale_date", SqlDbType.Date).Value = sale_date
            connSQL_Server.Open()
            dt_Sale_View_Statistics.Load(cmd.ExecuteReader)
            connSQL_Server.Close()
            cmd = Nothing

        Catch
            connSQL_Server.Close()
        End Try

    End Sub

    Public Sub Load_Sale_View_Statistics_Two_Dates(ByVal sale_date1 As Date, ByVal sale_date2 As Date)


        dt_Sale_View_Statistics.Clear()
        If Sale__Statistics_Form.DateTimePicker1.Value > Sale__Statistics_Form.DateTimePicker2.Value Then
            Try

                Dim cmd As New SqlCommand("SELECT sale_no, sale_date, sale_bill_no, [item-name], item_price, item_qunitity, total, customer_name FROM Sale_Statistics_View WHERE [sale_date]>=@sale_date2 and [sale_date]<=@sale_date1 ", connSQL_Server)
                cmd.Parameters.Add("sale_date1", SqlDbType.Date).Value = sale_date1
                cmd.Parameters.Add("sale_date2", SqlDbType.Date).Value = sale_date2
                connSQL_Server.Open()
                dt_Sale_View_Statistics.Load(cmd.ExecuteReader)
                connSQL_Server.Close()
                cmd = Nothing

            Catch
                connSQL_Server.Close()
            End Try
        Else

            Dim cmd As New SqlCommand("SELECT sale_no, sale_date, sale_bill_no, [item-name], item_price, item_qunitity, total, customer_name FROM Sale_Statistics_View WHERE [sale_date]>=@sale_date1 and [sale_date]<=@sale_date2 ", connSQL_Server)
            cmd.Parameters.Add("sale_date1", SqlDbType.Date).Value = sale_date1
            cmd.Parameters.Add("sale_date2", SqlDbType.Date).Value = sale_date2
            connSQL_Server.Open()
            dt_Sale_View_Statistics.Load(cmd.ExecuteReader)
            connSQL_Server.Close()
            cmd = Nothing


        End If
    End Sub



    Public dt_Items_Sales As New DataTable
    Public Sub Load_Items_Sales(sale_date As Date)
        dt_Items_Sales.Clear()
        Try

            Dim cmd As New SqlCommand("select Sales.item_id, Items.[item-name], SUM(item_qunitity) as Qunitity_All, SUM(total) As Tolat_All from Sales, Items Where Sales.item_id = Items .item_id and Sales.sale_date=@sale_date group by Sales.item_id, Items.[item-name]", connSQL_Server)
            cmd.Parameters.Add("sale_date", SqlDbType.Date).Value = sale_date
            connSQL_Server.Open()
            dt_Items_Sales.Load(cmd.ExecuteReader)
            connSQL_Server.Close()
            cmd = Nothing

        Catch
            connSQL_Server.Close()
        End Try

    End Sub
    Public Sub Load_Items_Sales_Two_Dates(ByVal sale_date1 As Date, ByVal sale_date2 As Date)
        dt_Items_Sales.Clear()
        If Items_Sales_Form.DateTimePicker1.Value > Items_Sales_Form.DateTimePicker2.Value Then
            Try
                'and [sale_date]>=@sale_date2 and [sale_date]<=@sale_date1 
                Dim cmd As New SqlCommand("select Sales.item_id, Items.[item-name], SUM(item_qunitity) as Qunitity_All, SUM(total) As Tolat_All from Sales, Items Where Sales.item_id = Items .item_id and [sale_date]>=@sale_date2 and [sale_date]<=@sale_date1 group by Sales.item_id, Items.[item-name]", connSQL_Server)
                cmd.Parameters.Add("sale_date1", SqlDbType.Date).Value = sale_date1
                cmd.Parameters.Add("sale_date2", SqlDbType.Date).Value = sale_date2
                connSQL_Server.Open()
                dt_Items_Sales.Load(cmd.ExecuteReader)
                connSQL_Server.Close()
                cmd = Nothing

            Catch
                connSQL_Server.Close()
            End Try
        Else

            Dim cmd As New SqlCommand("select Sales.item_id, Items.[item-name], SUM(item_qunitity) as Qunitity_All, SUM(total) As Tolat_All from Sales, Items Where Sales.item_id = Items .item_id and [sale_date]>=@sale_date1 and [sale_date]<=@sale_date2 group by Sales.item_id, Items.[item-name] ", connSQL_Server)
            cmd.Parameters.Add("sale_date1", SqlDbType.Date).Value = sale_date1
            cmd.Parameters.Add("sale_date2", SqlDbType.Date).Value = sale_date2
            connSQL_Server.Open()
            dt_Items_Sales.Load(cmd.ExecuteReader)
            connSQL_Server.Close()
            cmd = Nothing


        End If
    End Sub




    Public DV As New DataView(dt_Sale_View_Statistics)
    Public Sub Fiter_By_Customer_Name(ByVal customer_name As String)
        Try
            DV.RowFilter = String.Format("customer_name like '%{0}%'", customer_name)
        Catch ex As Exception
            DV = dt_Sale_View_Statistics.AsDataView
        End Try

    End Sub

    Public Sub Fiter_By_Item_Name(ByVal item_name As String)

        If item_name = "--Select item--" Then
            DV = dt_Sale_View_Statistics.AsDataView
        Else
            Try
                DV.RowFilter = String.Format("[item-name] like '%{0}%'", item_name)
            Catch ex As Exception
                DV = dt_Sale_View_Statistics.AsDataView


            End Try
        End If




    End Sub


    Public dt_Items_Names As New DataTable
    Public Sub Get_Items_Names()
        dt_Items_Names.Clear()
        Try

            Dim cmd As New SqlCommand("SELECT [item_id], [item-name] FROM Items", connSQL_Server)

            connSQL_Server.Open()
            dt_Items_Names.Load(cmd.ExecuteReader)
            connSQL_Server.Close()
            cmd = Nothing
            Dim R As DataRow = dt_Items_Names.NewRow
            R("item_id") = 0
            R("item-name") = "--Select item--"
            dt_Items_Names.Rows.InsertAt(R, 0)


        Catch
            connSQL_Server.Close()
        End Try

    End Sub


End Module
