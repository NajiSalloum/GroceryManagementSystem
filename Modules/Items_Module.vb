Imports System.Data.SqlClient

Module Items_Module
    Public dt_Items As New DataTable
    Public Sub Load_Items()
        dt_Items.Clear()
        Dim cmd As New SqlCommand("SELECT * From Items", connSQL_Server)
        connSQL_Server.Open()
        dt_Items.Load(cmd.ExecuteReader)
        connSQL_Server.Close()
        cmd = Nothing
    End Sub

    Public Sub Load_Items(ByVal item_barcode As String)
        dt_Items.Clear()
        Dim cmd As New SqlCommand("SELECT * FROM Items WHERE [item-barcode]=@item_barcode", connSQL_Server)
        cmd.Parameters.Add("item_barcode", SqlDbType.NVarChar).Value = item_barcode
        connSQL_Server.Open()
        dt_Items.Load(cmd.ExecuteReader)
        connSQL_Server.Close()
        cmd = Nothing
    End Sub


    Public DV_Filter_Item_Barcode As New DataView(dt_Items)
    Public Sub Filter_By_Item_Barcode(ByVal item_barcode As String)
        Try
            DV_Filter_Item_Barcode.RowFilter = String.Format("[item-barcode] like '%{0}%'", item_barcode)
        Catch ex As Exception
            DV_Filter_Item_Barcode = dt_Items.AsDataView
        End Try

    End Sub




    Public Function Max_Item_Id_From_Items()
        Dim Number As Integer
        Try
            Dim cmd As New SqlCommand("Select Max(item_id) From Items", connSQL_Server)
            connSQL_Server.Open()
            Number = cmd.ExecuteScalar
            connSQL_Server.Close()
        Catch
            Number = 0
            connSQL_Server.Close()
        End Try
        Return Number
    End Function

    Public Sub Insert_Items(ByVal item_name As String, ByVal item_price As Decimal, ByVal item_barcode As String)
        Dim cmd As New SqlCommand("insert into Items ([item_id], [item-name], [item-price], [item-barcode]) values(@item_id,@item_name,@item_price,@item_barcode)", connSQL_Server)
        cmd.Parameters.Add("item_id", SqlDbType.Int).Value = Max_Item_Id_From_Items() + 1
        cmd.Parameters.Add("item_name", SqlDbType.NVarChar).Value = item_name
        cmd.Parameters.Add("item_price", SqlDbType.Decimal).Value = item_price
        cmd.Parameters.Add("item_barcode", SqlDbType.NVarChar).Value = item_barcode
        connSQL_Server.Open()
        cmd.ExecuteNonQuery()
        connSQL_Server.Close()
        cmd = Nothing
    End Sub

    Public Sub Update_Items(ByVal item_name As String, ByVal item_price As Decimal, ByVal item_barcode As String, ByVal item_id As Integer)

        Dim cmd As New SqlCommand("UPDATE Items SET [item-name]=@item_name, [item-price]=@item_price, [item-barcode]=@item_barcode WHERE [item_id]=@item_id", connSQL_Server)
        cmd.Parameters.Add("item_id", SqlDbType.Int).Value = item_id
        cmd.Parameters.Add("item_name", SqlDbType.NVarChar).Value = item_name
        cmd.Parameters.Add("item_price", SqlDbType.Decimal).Value = item_price
        cmd.Parameters.Add("item_barcode", SqlDbType.NVarChar).Value = item_barcode

        connSQL_Server.Open()
        If cmd.ExecuteNonQuery() = 1 Then
            MsgBox("DATA UPDATE")
        Else
            MsgBox("DATA NOT UPDATE!!!!")
        End If
        connSQL_Server.Close()
        cmd = Nothing
    End Sub

    Public Sub Delete_Items(ByVal item_id As Integer)
        Dim cmd As New SqlCommand("DELETE FROM Items WHERE [item_id]=@item_id", connSQL_Server)
        cmd.Parameters.Add("item_id", SqlDbType.Int).Value = item_id
        connSQL_Server.Open()
        cmd.ExecuteNonQuery()
        connSQL_Server.Close()
        cmd = Nothing
    End Sub
End Module
