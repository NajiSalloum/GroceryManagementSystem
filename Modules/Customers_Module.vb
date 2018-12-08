Imports System.Data.SqlClient

Module Customers_Module
    Public dt_Customers As New DataTable
    Public Sub Load_Customers()
        dt_Customers.Clear()
        Dim cmd As New SqlCommand("SELECT * FROM [Free_Sales].[dbo].[Customers]", connSQL_Server)
        connSQL_Server.Open()
        dt_Customers.Load(cmd.ExecuteReader)
        connSQL_Server.Close()
        cmd = Nothing
    End Sub

    Public Function Is_Customer_Exist(ByVal customer_id As Integer)
        Dim Is_Exist As Boolean = False
        Dim Count As Integer = 0
        Try
            Dim cmd As New SqlCommand("SELECT count(*) FROM Customers WHERE [customer_id]=@customer_id", connSQL_Server)
            cmd.Parameters.Add("customer_id", SqlDbType.Int).Value = customer_id
            connSQL_Server.Open()
            Count = cmd.ExecuteScalar
            connSQL_Server.Close()
        Catch
            Count = 0
            connSQL_Server.Close()
        End Try
        If Count = 0 Then
            Is_Exist = False
        Else
            Is_Exist = True
        End If
        Return Count
    End Function
    Public Sub Insert_Customers(ByVal customer_id As Integer, ByVal customer_name As String, ByVal customer_mob As String, ByVal customer_address As String)
        Dim cmd As New SqlCommand("insert into Customers ([customer_id], [customer_name], [customer_mob], [customer_address]) values(@customer_id,@customer_name,@customer_mob,@customer_address)", connSQL_Server)
        cmd.Parameters.Add("customer_id", SqlDbType.Int).Value = customer_id
        cmd.Parameters.Add("customer_name", SqlDbType.NVarChar).Value = customer_name
        cmd.Parameters.Add("customer_mob", SqlDbType.Decimal).Value = customer_mob
        cmd.Parameters.Add("customer_address", SqlDbType.NVarChar).Value = customer_address
        connSQL_Server.Open()
        cmd.ExecuteNonQuery()
        connSQL_Server.Close()
        cmd = Nothing
    End Sub

    Public Sub Update_Customers(ByVal customer_name As String, ByVal customer_mob As String, ByVal customer_address As String, ByVal customer_id As Integer)

        Dim cmd As New SqlCommand("UPDATE Customers SET [customer_name]=@customer_name, [customer_mob]=@customer_mob, [customer_address]=@customer_address WHERE [customer_id]=@customer_id", connSQL_Server)
        cmd.Parameters.Add("customer_id", SqlDbType.Int).Value = customer_id
        cmd.Parameters.Add("customer_name", SqlDbType.NVarChar).Value = customer_name
        cmd.Parameters.Add("customer_mob", SqlDbType.NVarChar).Value = customer_mob
        cmd.Parameters.Add("customer_address", SqlDbType.NVarChar).Value = customer_address

        connSQL_Server.Open()
        cmd.ExecuteNonQuery()
        connSQL_Server.Close()
        cmd = Nothing
    End Sub

    Public Sub Delete_Customrt(ByVal customer_id As Integer)
        Dim cmd As New SqlCommand("DELETE FROM Customers WHERE [customer_id]=@customer_id", connSQL_Server)
        cmd.Parameters.Add("customer_id", SqlDbType.Int).Value = customer_id

        connSQL_Server.Open()
        cmd.ExecuteNonQuery()
        connSQL_Server.Close()
        cmd = Nothing
    End Sub

End Module
