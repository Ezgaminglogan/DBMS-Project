Imports MySql.Data.MySqlClient

Public Class frmManageStudents
    ' Load the student data when the form loads
    Private Sub frmManageStudents_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadStudents()
    End Sub

    ' Method to load all students into the DataGridView
    Private Sub LoadStudents()
        Dim connectionString As String = "server=localhost;user id=root;password=;database=new_activitydms"
        Using con As New MySqlConnection(connectionString)
            con.Open()
            ' Fetch the necessary columns from the users table (now using UserID instead of StudentNumber)
            Dim query As String = "SELECT UserID, FirstName, LastName, Email, Role FROM users WHERE Role = 'Student'"
            Dim adapter As New MySqlDataAdapter(query, con)
            Dim table As New DataTable()
            adapter.Fill(table)
            DataGridView1.DataSource = table
        End Using
    End Sub

    ' Add a new student when the Add button is clicked
    Private Sub btnAddStudent_Click(sender As Object, e As EventArgs) Handles btnAddStudent.Click
        Dim userId As String = txtStudentNumber.Text ' Still using the textbox to get UserID as a placeholder for the old StudentNumber
        Dim firstName As String = txtFirstName.Text
        Dim lastName As String = txtLastName.Text
        Dim email As String = txtEmail.Text
        Dim password As String = txtPassword.Text

        ' Validate inputs
        If String.IsNullOrWhiteSpace(userId) OrElse String.IsNullOrWhiteSpace(firstName) OrElse String.IsNullOrWhiteSpace(lastName) OrElse String.IsNullOrWhiteSpace(email) OrElse String.IsNullOrWhiteSpace(password) Then
            MessageBox.Show("Please fill in all fields.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim connectionString As String = "server=localhost;user id=root;password=;database=new_activitydms"
        Using con As New MySqlConnection(connectionString)
            con.Open()
            ' Insert a new user into the users table with UserID as the unique key for a student
            Dim query As String = "INSERT INTO users (UserID, FirstName, LastName, Email, Password, Role) VALUES (@UserID, @FirstName, @LastName, @Email, @Password, 'Student')"
            Using cmd As New MySqlCommand(query, con)
                cmd.Parameters.AddWithValue("@UserID", userId)
                cmd.Parameters.AddWithValue("@FirstName", firstName)
                cmd.Parameters.AddWithValue("@LastName", lastName)
                cmd.Parameters.AddWithValue("@Email", email)
                cmd.Parameters.AddWithValue("@Password", password)
                cmd.ExecuteNonQuery()
            End Using
        End Using

        ' Refresh DataGridView after adding the student
        LoadStudents()
        MessageBox.Show("Student added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

        ' Clear input fields
        ClearInputFields()
    End Sub

    ' Edit the selected student when the Edit button is clicked
    Private Sub btnEditStudent_Click(sender As Object, e As EventArgs) Handles btnEditStudent.Click
        Dim userId As String = txtStudentNumber.Text ' Updated to use UserID
        Dim firstName As String = txtFirstName.Text
        Dim lastName As String = txtLastName.Text
        Dim email As String = txtEmail.Text
        Dim password As String = txtPassword.Text ' Optional: To handle password changes

        If String.IsNullOrWhiteSpace(userId) Then
            MessageBox.Show("Please select a student to edit.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim connectionString As String = "server=localhost;user id=root;password=;database=new_activitydms"
        Using con As New MySqlConnection(connectionString)
            con.Open()
            ' Update all fields including password (if changed)
            Dim query As String = "UPDATE users SET FirstName = @FirstName, LastName = @LastName, Email = @Email, Password = @Password WHERE UserID = @UserID"
            Using cmd As New MySqlCommand(query, con)
                cmd.Parameters.AddWithValue("@UserID", userId)
                cmd.Parameters.AddWithValue("@FirstName", firstName)
                cmd.Parameters.AddWithValue("@LastName", lastName)
                cmd.Parameters.AddWithValue("@Email", email)
                cmd.Parameters.AddWithValue("@Password", password)
                cmd.ExecuteNonQuery()
            End Using
        End Using

        ' Refresh DataGridView after editing the student
        LoadStudents()
        MessageBox.Show("Student updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

        ' Clear input fields
        ClearInputFields()
    End Sub

    ' Remove the selected student when the Remove button is clicked
    Private Sub btnRemoveStudent_Click(sender As Object, e As EventArgs) Handles btnRemoveStudent.Click
        Dim userId As String = txtStudentNumber.Text ' Updated to use UserID

        If String.IsNullOrWhiteSpace(userId) Then
            MessageBox.Show("Please select a student to remove.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim result As DialogResult = MessageBox.Show("Are you sure you want to delete this student?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
        If result = DialogResult.Yes Then
            Dim connectionString As String = "server=localhost;user id=root;password=;database=new_activitydms"
            Using con As New MySqlConnection(connectionString)
                con.Open()
                Dim query As String = "DELETE FROM users WHERE UserID = @UserID AND Role = 'Student'"
                Using cmd As New MySqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@UserID", userId)
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            ' Refresh DataGridView after deleting the student
            LoadStudents()
            MessageBox.Show("Student removed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' Clear input fields
            ClearInputFields()
        End If
    End Sub

    ' Populate the textboxes when a student is selected in the DataGridView
    Private Sub dgvStudents_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex >= 0 Then
            Dim selectedRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
            txtStudentNumber.Text = selectedRow.Cells("UserID").Value.ToString() ' Updated to UserID
            txtFirstName.Text = selectedRow.Cells("FirstName").Value.ToString()
            txtLastName.Text = selectedRow.Cells("LastName").Value.ToString()
            txtEmail.Text = selectedRow.Cells("Email").Value.ToString()
            txtPassword.Text = selectedRow.Cells("Password").Value.ToString() ' Display the password
        End If
    End Sub

    ' Clear the input fields after actions
    Private Sub ClearInputFields()
        txtStudentNumber.Clear() ' Use UserID now, but still called "StudentNumber" for compatibility
        txtFirstName.Clear()
        txtLastName.Clear()
        txtEmail.Clear()
        txtPassword.Clear() ' Keep this clear as password shouldn't be shown
    End Sub
End Class
