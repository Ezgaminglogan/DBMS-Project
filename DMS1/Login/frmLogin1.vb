Imports MySql.Data.MySqlClient

Public Class frmLogin1

    ' Main login button click handler (Unified Login)
    Private Sub btnLOGIN_Click_1(sender As Object, e As EventArgs) Handles btnLOGIN.Click
        Dim usernameOrEmail As String = txtUsernameOrEmail.Text.Trim()
        Dim password As String = txtPassword.Text.Trim()

        If String.IsNullOrWhiteSpace(usernameOrEmail) OrElse String.IsNullOrWhiteSpace(password) Then
            MessageBox.Show("Please enter your username/email and password.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' MySQL database connection string (XAMPP default settings)
        Dim connectionString As String = "server=localhost;user id=root;password=;database=new_activitydms"

        Using con As New MySqlConnection(connectionString)

            con.Open()

            ' SQL query to check if the user exists in the 'users' table
            Dim query As String = "SELECT UserID, Role FROM users WHERE Email = @Email AND Password = @Password"
            Using cmd As New MySqlCommand(query, con)
                cmd.Parameters.AddWithValue("@Email", usernameOrEmail)
                cmd.Parameters.AddWithValue("@Password", password)

                Dim reader As MySqlDataReader = cmd.ExecuteReader()
                If reader.HasRows Then
                    reader.Read() ' Read the first row

                    ' Get the UserID
                    Dim userID = reader.GetInt32("UserID")

                    Dim role As String = reader.GetString("Role")

                    ' Handle student login
                    If role = "Student" Then
                        Dim studentDashboard As New StudentDashboard()
                        studentDashboard.CurrentUserID = userID ' Pass UserID directly
                        studentDashboard.Show()
                        Me.Hide()
                        MessageBox.Show("Login successful! Welcome, Student.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return
                    End If

                    ' Handle teacher login
                    If role = "Teacher" Then
                        Dim teacherDashboard As New TeacherDashboard()
                        teacherDashboard.CurrentUserID = userID ' Pass UserID for teacher as well
                        teacherDashboard.Show()
                        Me.Hide()
                        MessageBox.Show($"Login successful! Welcome, Teacher.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return
                    End If

                    ' Handle admin login
                    If role = "Admin" Then
                        Dim adminForm As New frmAdmin()
                        adminForm.Show()
                        Me.Hide()
                        MessageBox.Show("Login successful! Welcome, Admin.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return
                    End If

                Else
                    MessageBox.Show("Invalid email or password!", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End Using
        End Using
    End Sub

    Private Sub txtPassword_KeyDown(sender As Object, e As KeyEventArgs) Handles txtPassword.KeyDown
        If e.KeyCode = Keys.Enter Then
            btnLOGIN_Click_1(sender, e)
        End If
    End Sub
End Class
