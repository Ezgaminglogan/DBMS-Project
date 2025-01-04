Imports MySql.Data.MySqlClient

Public Class frmManageSubjects
    ' Database connection string
    Private Const ConnectionString As String = "server=localhost;user=root;password=;database=new_activitydms"

    Private Sub frmManageSubjects_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadDepartments()
        LoadCourses()
        LoadYears() ' Load years into the combo box
    End Sub

    Private Sub LoadDepartments()
        cmbDepartment.Items.Clear() ' Clear existing items
        ' Add IT/Programming related departments
        cmbDepartment.Items.Add("College of Technology")
        cmbDepartment.Items.Add("College of Education")
        cmbDepartment.Items.Add("College of Arts")
        cmbDepartment.Items.Add("College of Engineering")
        cmbDepartment.Items.Add("College of Medicine")
        cmbDepartment.Items.Add("College of Communication")
    End Sub

    Private Sub LoadYears()
        cmbYear.Items.Clear() ' Clear existing items
        ' Add year options
        cmbYear.Items.Add("1st Year")
        cmbYear.Items.Add("2nd Year")
        cmbYear.Items.Add("3rd Year")
        cmbYear.Items.Add("4th Year")
        cmbYear.SelectedIndex = 0 ' Select the first year by default
    End Sub

    Private Sub LoadCourses()
        Dim query As String = "SELECT Course_number, Course_name, Credit_hours, Department, Year FROM Course"
        Using conn As New MySqlConnection(ConnectionString)
            Dim adapter As New MySqlDataAdapter(query, conn)
            Dim table As New DataTable()
            adapter.Fill(table)
            dgvCourses.DataSource = table
        End Using
    End Sub

    Private Sub btnAddCourse_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If ValidateInputs() Then
            Dim query As String = "INSERT INTO Course (Course_number, Course_name, Credit_hours, Department, Year) " &
                                  "VALUES (@CourseNumber, @CourseName, @CreditHours, @Department, @Year)"
            ExecuteDatabaseQuery(query)
            LoadCourses()
            MessageBox.Show("Course added successfully!")
        End If
    End Sub

    Private Sub btnEditCourse_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        If ValidateInputs() Then
            Dim query As String = "UPDATE Course SET Course_name = @CourseName, Credit_hours = @CreditHours, " &
                                  "Department = @Department, Year = @Year WHERE Course_number = @CourseNumber"
            ExecuteDatabaseQuery(query)
            LoadCourses()
            MessageBox.Show("Course updated successfully!")
        End If
    End Sub

    Private Sub btnDeleteCourse_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If String.IsNullOrEmpty(txtCourseNumber.Text) Then
            MessageBox.Show("Please select a course to delete.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim query As String = "DELETE FROM Course WHERE Course_number = @CourseNumber"
        ExecuteDatabaseQuery(query)
        LoadCourses()
        MessageBox.Show("Course deleted successfully!")
    End Sub

    Private Sub dgvCourses_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvCourses.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = dgvCourses.Rows(e.RowIndex)
            txtCourseNumber.Text = row.Cells("Course_number").Value.ToString()
            txtCourseName.Text = row.Cells("Course_name").Value.ToString()
            numCreditHours.Value = CInt(row.Cells("Credit_hours").Value)
            cmbDepartment.SelectedItem = row.Cells("Department").Value.ToString()
            cmbYear.SelectedItem = row.Cells("Year").Value.ToString() ' Set the selected year
        End If
    End Sub

    Private Sub ExecuteDatabaseQuery(query As String)
        Try
            Using conn As New MySqlConnection(ConnectionString)
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@CourseNumber", txtCourseNumber.Text)
                    cmd.Parameters.AddWithValue("@CourseName", txtCourseName.Text)
                    cmd.Parameters.AddWithValue("@CreditHours", numCreditHours.Value)
                    cmd.Parameters.AddWithValue("@Department", cmbDepartment.SelectedItem.ToString())
                    cmd.Parameters.AddWithValue("@Year", cmbYear.SelectedItem.ToString())

                    conn.Open()
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("An error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function ValidateInputs() As Boolean
        If String.IsNullOrWhiteSpace(txtCourseNumber.Text) OrElse
           String.IsNullOrWhiteSpace(txtCourseName.Text) OrElse
           cmbDepartment.SelectedIndex = -1 OrElse
           cmbYear.SelectedIndex = -1 Then
            MessageBox.Show("Please fill in all fields.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If
        Return True
    End Function
End Class
