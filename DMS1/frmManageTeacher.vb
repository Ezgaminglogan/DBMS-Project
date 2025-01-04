Imports MySql.Data.MySqlClient

Public Class frmManageTeacher
    ' Load the teacher data when the form loads
    Private Sub frmManageTeachers_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadTeachers()
    End Sub

    ' Method to load all teachers into the DataGridView
    Private Sub LoadTeachers()
        Dim connectionString As String = "server=localhost;user id=root;password=;database=new_activitydms"
        Using con As New MySqlConnection(connectionString)
            con.Open()
            ' Modified query to include Password
            Dim query As String = "SELECT UserID, FirstName, LastName, Email, Password FROM users WHERE Role = 'Teacher'"
            Dim adapter As New MySqlDataAdapter(query, con)
            Dim table As New DataTable()
            adapter.Fill(table)
            dgvTeachers.DataSource = table
        End Using
    End Sub

    ' Add a new teacher when the Add button is clicked
    Private Sub btnAddTeacher_Click(sender As Object, e As EventArgs) Handles btnAddTeacher.Click
        Dim firstName As String = txtFirstName.Text
        Dim lastName As String = txtLastName.Text
        Dim email As String = txtEmail.Text
        Dim password As String = txtPassword.Text

        ' Validate inputs
        If String.IsNullOrWhiteSpace(firstName) OrElse String.IsNullOrWhiteSpace(lastName) OrElse String.IsNullOrWhiteSpace(email) OrElse String.IsNullOrWhiteSpace(password) Then
            MessageBox.Show("Please fill in all fields.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim connectionString As String = "server=localhost;user id=root;password=;database=new_activitydms"
        Using con As New MySqlConnection(connectionString)
            con.Open()

            ' Get the next available UserID
            Dim queryGetMaxID As String = "SELECT MAX(UserID) FROM users"
            Dim cmdMaxID As New MySqlCommand(queryGetMaxID, con)
            Dim maxID As Object = cmdMaxID.ExecuteScalar()
            Dim newUserID As Integer = If(maxID Is DBNull.Value, 1, Convert.ToInt32(maxID) + 1)

            ' Insert new teacher with generated UserID
            Dim query As String = "INSERT INTO `users` (UserID, Role, FirstName, LastName, Email, Password , Qualifications) VALUES (@UserID, 'Teacher', @FirstName, @LastName, @Email, @Password , @Qualifications)"

            Using cmd As New MySqlCommand(query, con)
                cmd.Parameters.AddWithValue("@UserID", newUserID)
                cmd.Parameters.AddWithValue("@FirstName", firstName)
                cmd.Parameters.AddWithValue("@LastName", lastName)
                cmd.Parameters.AddWithValue("@Email", email)
                cmd.Parameters.AddWithValue("@Password", password)
                cmd.Parameters.AddWithValue("@Qualifications", cbQual.SelectedItem.ToString())
                cmd.ExecuteNonQuery()
            End Using
        End Using

        ' Refresh DataGridView after adding the teacher
        LoadTeachers()
        MessageBox.Show("Teacher added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

        ' Clear input fields
        ClearInputFields()
    End Sub

    ' Edit the selected teacher when the Edit button is clicked
    Private Sub btnEditTeacher_Click(sender As Object, e As EventArgs) Handles btnEditTeacher.Click
        ' Retrieve UserID from the selected row in DataGridView, not from txtTeacherID
        If dgvTeachers.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select a teacher to edit.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim userID As Integer = Convert.ToInt32(dgvTeachers.SelectedRows(0).Cells("UserID").Value)
        Dim firstName As String = txtFirstName.Text
        Dim lastName As String = txtLastName.Text
        Dim email As String = txtEmail.Text
        Dim password As String = txtPassword.Text

        If String.IsNullOrWhiteSpace(firstName) OrElse String.IsNullOrWhiteSpace(lastName) OrElse String.IsNullOrWhiteSpace(email) OrElse String.IsNullOrWhiteSpace(password) Then
            MessageBox.Show("Please fill in all fields.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim connectionString As String = "server=localhost;user id=root;password=;database=new_activitydms"
        Using con As New MySqlConnection(connectionString)
            con.Open()
            Dim query As String = "UPDATE users SET FirstName = @FirstName, LastName = @LastName, Email = @Email, Password = @Password WHERE UserID = @UserID AND Role = 'Teacher'"
            Using cmd As New MySqlCommand(query, con)
                cmd.Parameters.AddWithValue("@UserID", userID)
                cmd.Parameters.AddWithValue("@FirstName", firstName)
                cmd.Parameters.AddWithValue("@LastName", lastName)
                cmd.Parameters.AddWithValue("@Email", email)
                cmd.Parameters.AddWithValue("@Password", password)
                cmd.ExecuteNonQuery()
            End Using
        End Using

        ' Refresh DataGridView after editing the teacher
        LoadTeachers()
        MessageBox.Show("Teacher updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

        ' Clear input fields
        ClearInputFields()
    End Sub

    ' Remove the selected teacher when the Remove button is clicked
    Private Sub btnRemoveTeacher_Click(sender As Object, e As EventArgs) Handles btnRemoveTeacher.Click
        ' Check if any teacher is selected in the DataGridView
        If dgvTeachers.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select a teacher to remove.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Retrieve UserID from the selected row in DataGridView
        Dim userID As Integer = Convert.ToInt32(dgvTeachers.SelectedRows(0).Cells("UserID").Value)

        ' Confirm the deletion of the teacher
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to delete this teacher?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
        If result = DialogResult.Yes Then
            ' SQL Query to delete teacher based on UserID
            Dim connectionString As String = "server=localhost;user id=root;password=;database=new_activitydms"
            Using con As New MySqlConnection(connectionString)
                con.Open()
                Dim query As String = "DELETE FROM users WHERE UserID = @UserID AND Role = 'Teacher'"
                Using cmd As New MySqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@UserID", userID)
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            ' Refresh DataGridView after deleting the teacher
            LoadTeachers()
            MessageBox.Show("Teacher removed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' Clear input fields
            ClearInputFields()
        End If
    End Sub

    ' Populate the textboxes when a teacher is selected in the DataGridView
    Private Sub dgvTeachers_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvTeachers.CellClick
        If e.RowIndex >= 0 Then
            Dim selectedRow As DataGridViewRow = dgvTeachers.Rows(e.RowIndex)
            txtFirstName.Text = selectedRow.Cells("FirstName").Value.ToString()
            txtLastName.Text = selectedRow.Cells("LastName").Value.ToString()
            txtEmail.Text = selectedRow.Cells("Email").Value.ToString()
            txtPassword.Text = selectedRow.Cells("Password").Value.ToString() ' Ensure this is valid
        End If
    End Sub

    ' Clear the input fields after actions
    Private Sub ClearInputFields()
        txtFirstName.Clear()
        txtLastName.Clear()
        txtEmail.Clear()
        txtPassword.Clear()
    End Sub
End Class
