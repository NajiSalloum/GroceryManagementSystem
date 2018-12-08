Imports System.Data.SqlClient

Module Users_Module
    Public Sub Get_Roles()
        Dim R As DataRow = dt_Roles.NewRow
        R("role_id") = 0
        R("role_name") = "--Select role--"
        dt_Roles.Rows.InsertAt(R, 0)
        With User_Password_Form.ComboBox1
            .DataSource = dt_Roles
            .DisplayMember = "role_name"
            .ValueMember = "role_id"
            .SelectedIndex = 0
        End With


    End Sub


    Public Function Get_Role_Id_By_Role_Name(ByVal role_name As String)
        Dim role_id As Integer = 0
        Try
            Dim cmd As New SqlCommand("select role_id from Roles where role_name=@role_name", connSQL_Server)
            cmd.Parameters.Add("role_name", SqlDbType.NVarChar).Value = role_name
            connSQL_Server.Open()
            role_id = cmd.ExecuteScalar
            connSQL_Server.Close()
        Catch
            role_id = 0
            connSQL_Server.Close()
        End Try
        Return role_id
    End Function



    Public dt_Users As New DataTable
    Public Sub Load_Users()
        dt_Users.Clear()
        Dim cmd As New SqlCommand("Select A.up_id, B.emp_name, A.user_name, A.user_password, C.role_name   from User_Password as A, Employees as B, Roles as C where A.emp_id = B.emp_id and A.role_id = C.role_id", connSQL_Server)
        connSQL_Server.Open()
        dt_Users.Load(cmd.ExecuteReader)
        connSQL_Server.Close()
        cmd = Nothing
    End Sub

    Public Function Get_Max_UP_Id()
        Dim Number As Integer = 0
        Try
            Dim cmd As New SqlCommand("Select Max(up_id) From User_Password", connSQL_Server)
            connSQL_Server.Open()
            Number = cmd.ExecuteScalar
            connSQL_Server.Close()
        Catch
            Number = 0
            connSQL_Server.Close()
        End Try
        Return Number
    End Function

    Public Sub Insert_UP(ByVal emp_id As Integer, ByVal user_name As String, ByVal user_password As String, ByVal role_id As Integer)
        Dim cmd As New SqlCommand("insert into User_Password ([up_id], [emp_id], [user_name], [user_password], [role_id]) values(@up_id,@emp_id,@user_name,@user_password,@role_id)", connSQL_Server)
        cmd.Parameters.Add("up_id", SqlDbType.Int).Value = Get_Max_UP_Id() + 1
        cmd.Parameters.Add("emp_id", SqlDbType.Int).Value = emp_id
        cmd.Parameters.Add("user_name", SqlDbType.NVarChar).Value = user_name
        cmd.Parameters.Add("user_password", SqlDbType.NVarChar).Value = user_password
        cmd.Parameters.Add("role_id", SqlDbType.Int).Value = role_id
        connSQL_Server.Open()
        cmd.ExecuteNonQuery()
        connSQL_Server.Close()
        cmd = Nothing
    End Sub

    Public Sub Update_UP(ByVal user_name As String, ByVal user_password As String, ByVal role_id As Integer, ByVal up_id As Integer)

        Dim cmd As New SqlCommand("UPDATE User_Password SET [user_name]=@user_name, [user_password]=@user_password, [role_id]=@role_id WHERE [up_id]=@up_id", connSQL_Server)

        cmd.Parameters.Add("up_id", SqlDbType.Int).Value = up_id
        cmd.Parameters.Add("user_name", SqlDbType.NVarChar).Value = user_name
        cmd.Parameters.Add("user_password", SqlDbType.NVarChar).Value = user_password
        cmd.Parameters.Add("role_id", SqlDbType.Int).Value = role_id

        connSQL_Server.Open()
        If cmd.ExecuteNonQuery() = 1 Then
            MsgBox("DATA UPDATE")
        Else
            MsgBox("DATA NOT UPDATE!!!!")
        End If
        connSQL_Server.Close()
        cmd = Nothing
    End Sub


    Public dt_Login As New DataTable
    Public Sub Load_Login(ByVal user_name As String, ByVal user_password As String)
        dt_Login.Clear()
        Dim cmd As New SqlCommand("select A.emp_id, B.role_main , B.role_statistics, B.role_control  from User_Password as A, Roles As B Where A.role_id = B.role_id and A.user_name=@user_name and A.user_password=@user_password", connSQL_Server)
        cmd.Parameters.Add("user_name", SqlDbType.NVarChar).Value = user_name
        cmd.Parameters.Add("user_password", SqlDbType.NVarChar).Value = user_password

        connSQL_Server.Open()
        dt_Login.Load(cmd.ExecuteReader)
        connSQL_Server.Close()
        cmd = Nothing
    End Sub


End Module
