Imports System.Data.SqlClient

Module Employees_Module

    Public dt_Employees As New DataTable
    Public Sub Load_Employees()
        dt_Employees.Clear()
        Dim cmd As New SqlCommand("SELECT * FROM Employees WHERE [emp_state]=1", connSQL_Server)
        connSQL_Server.Open()
        dt_Employees.Load(cmd.ExecuteReader)
        connSQL_Server.Close()
        cmd = Nothing
    End Sub

    Public Function Get_Max_Employee_Id()
        Dim Number As Integer = 0
        Try
            Dim cmd As New SqlCommand("Select Max(emp_id) From Employees", connSQL_Server)
            connSQL_Server.Open()
            Number = cmd.ExecuteScalar
            connSQL_Server.Close()
        Catch
            Number = 0
            connSQL_Server.Close()
        End Try
        Return Number
    End Function

    Public Sub Insert_Employee(ByVal emp_name As String, ByVal emp_phone As String, ByVal emp_state As Boolean)
        Dim cmd As New SqlCommand("insert into Employees ([emp_id], [emp_name], [emp_phone], [emp_state]) values(@emp_id,@emp_name,@emp_phone,@emp_state)", connSQL_Server)
        cmd.Parameters.Add("emp_id", SqlDbType.Int).Value = Get_Max_Employee_Id() + 1
        cmd.Parameters.Add("emp_name", SqlDbType.NVarChar).Value = emp_name
        cmd.Parameters.Add("emp_phone", SqlDbType.NVarChar).Value = emp_phone
        cmd.Parameters.Add("emp_state", SqlDbType.Bit).Value = emp_state
        connSQL_Server.Open()
        cmd.ExecuteNonQuery()
        connSQL_Server.Close()
        cmd = Nothing
    End Sub

    Public Sub Update_Employee(ByVal emp_name As String, ByVal emp_phone As String, ByVal emp_state As Boolean, ByVal emp_id As Integer)

        Dim cmd As New SqlCommand("UPDATE Employees SET [emp_name]=@emp_name, [emp_phone]=@emp_phone, [emp_state]=@emp_state WHERE [emp_id]=@emp_id", connSQL_Server)

        cmd.Parameters.Add("emp_id", SqlDbType.Int).Value = emp_id
        cmd.Parameters.Add("emp_name", SqlDbType.NVarChar).Value = emp_name
        cmd.Parameters.Add("emp_phone", SqlDbType.NVarChar).Value = emp_phone
        cmd.Parameters.Add("emp_state", SqlDbType.Bit).Value = emp_state

        connSQL_Server.Open()
        If cmd.ExecuteNonQuery() = 1 Then
            MsgBox("DATA UPDATE")
        Else
            MsgBox("DATA NOT UPDATE!!!!")
        End If
        connSQL_Server.Close()
        cmd = Nothing
    End Sub

    Public Sub Delete_Employee(ByVal emp_id As Integer)
        Dim cmd As New SqlCommand("DELETE FROM Employees WHERE [emp_id]=@emp_id ", connSQL_Server)
        cmd.Parameters.Add("emp_id ", SqlDbType.Int).Value = emp_id
        connSQL_Server.Open()
        cmd.ExecuteNonQuery()
        connSQL_Server.Close()
        cmd = Nothing
    End Sub


    Public DV_Filter_Employee_Name_OR_Phone As New DataView(dt_Employees)
    Public Sub Fiter_By_Employee_Name_OR_Phone(ByVal emp_name As String)
        Try
            DV_Filter_Employee_Name_OR_Phone.RowFilter = String.Format("emp_name like '%{0}%' or emp_phone like '%{0}%'", emp_name)
        Catch ex As Exception
            DV_Filter_Employee_Name_OR_Phone = dt_Employees.AsDataView
        End Try

    End Sub

End Module
