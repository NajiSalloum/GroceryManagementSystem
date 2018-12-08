Public Class MainForm
    Public Sub Close_All()
        Item_Form.Close()
        Customer_Form.Close()
        Sale_Form.Close()
    End Sub
    Private Sub CategoriesToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Close_All()
        Item_Form.MdiParent = Me
        Item_Form.Show()
    End Sub

    Private Sub CustomersToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Close_All()
        Customer_Form.MdiParent = Me
        Customer_Form.Show()
    End Sub

    Private Sub SalesToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Close_All()
        Sale_Form.MdiParent = Me
        Sale_Form.Show()
    End Sub

    Private Sub StatisticsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StatisticsToolStripMenuItem.Click

    End Sub

    Private Sub ItemToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ItemToolStripMenuItem.Click
        Close_All()
        Sale_Form.MdiParent = Me
        Sale__Statistics_Form.Show()
    End Sub

    Private Sub ItemsSalesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ItemsSalesToolStripMenuItem.Click
        Close_All()
        Items_Sales_Form.MdiParent = Me
        Items_Sales_Form.Show()
    End Sub

    Private Sub AddEmplToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddEmplToolStripMenuItem.Click
        Close_All()
        Employees_Form.MdiParent = Me
        Employees_Form.Show()
    End Sub

    Private Sub ItemsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ItemsToolStripMenuItem.Click
        Close_All()
        Item_Form.MdiParent = Me
        Item_Form.Show()
    End Sub

    Private Sub CustomersToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles CustomersToolStripMenuItem1.Click
        Close_All()
        Customer_Form.MdiParent = Me
        Customer_Form.Show()
    End Sub

    Private Sub SalesToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles SalesToolStripMenuItem1.Click
        Close_All()
        Sale_Form.MdiParent = Me
        Sale_Form.Show()
    End Sub

    Private Sub RolesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RolesToolStripMenuItem.Click
        Close_All()
        Roles_Form.MdiParent = Me
        Roles_Form.Show()
    End Sub

    Private Sub UsersToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UsersToolStripMenuItem.Click
        Close_All()
        User_Form.MdiParent = Me
        User_Form.Show()
    End Sub
End Class
