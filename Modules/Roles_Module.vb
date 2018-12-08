Imports System.Data.SqlClient

Module Roles_Module
    Public dt_Roles As New DataTable
    Public Sub Load_Roles()
        dt_Roles.Clear()
        Dim cmd As New SqlCommand("SELECT * FROM Roles", connSQL_Server)
        connSQL_Server.Open()
        dt_Roles.Load(cmd.ExecuteReader)
        connSQL_Server.Close()
        cmd = Nothing
    End Sub


    Public Function Get_Max_Role_Id()
        Dim Number As Integer = 0
        Try
            Dim cmd As New SqlCommand("Select Max(role_id) From Roles", connSQL_Server)
            connSQL_Server.Open()
            Number = cmd.ExecuteScalar
            connSQL_Server.Close()
        Catch
            Number = 0
            connSQL_Server.Close()
        End Try
        Return Number
    End Function

    Public Sub Insert_Role(ByVal role_name As String, ByVal role_main As Boolean, ByVal role_statistics As Boolean, ByVal role_control As Boolean)
        Dim cmd As New SqlCommand("insert into Roles ([role_id], [role_name], [role_main], [role_statistics], [role_control]) values(@role_id,@role_name,@role_main,@role_statistics, @role_control)", connSQL_Server)
        cmd.Parameters.Add("role_id", SqlDbType.Int).Value = Get_Max_Role_Id() + 1
        cmd.Parameters.Add("role_name", SqlDbType.NVarChar).Value = role_name
        cmd.Parameters.Add("role_main", SqlDbType.Bit).Value = role_main
        cmd.Parameters.Add("role_statistics", SqlDbType.Bit).Value = role_statistics
        cmd.Parameters.Add("role_control", SqlDbType.Bit).Value = role_control
        connSQL_Server.Open()
        cmd.ExecuteNonQuery()
        connSQL_Server.Close()
        cmd = Nothing
    End Sub


    Public Sub Update_Role(ByVal role_name As String, ByVal role_main As Boolean, ByVal role_statistics As Boolean, ByVal role_control As Boolean, ByVal role_id As Integer)

        Dim cmd As New SqlCommand("UPDATE Roles SET [role_name]=@role_name, [role_main]=@role_main, [role_statistics]=@role_statistics, [role_control]=@role_control WHERE [role_id]=@role_id", connSQL_Server)

        cmd.Parameters.Add("role_id", SqlDbType.Int).Value = role_id
        cmd.Parameters.Add("role_name", SqlDbType.NVarChar).Value = role_name
        cmd.Parameters.Add("role_main", SqlDbType.Bit).Value = role_main
        cmd.Parameters.Add("role_statistics", SqlDbType.Bit).Value = role_statistics
        cmd.Parameters.Add("role_control", SqlDbType.Bit).Value = role_control

        connSQL_Server.Open()
        If cmd.ExecuteNonQuery() = 1 Then
            MsgBox("DATA UPDATE")
        Else
            MsgBox("DATA NOT UPDATE!!!!")
        End If
        connSQL_Server.Close()
        cmd = Nothing
    End Sub
End Module
