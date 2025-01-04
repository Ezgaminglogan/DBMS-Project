Imports MySql.Data.MySqlClient

Public Class TeacherRegistrationDataAccess
    Private connectionString As String = "Server=localhost;Database=new_activitydms;Uid=root;Pwd=;"

    ' Method to register a new teacher
    Public Sub RegisterTeacher(firstName As String, lastName As String, email As String, password As String)
        Using connection As New MySqlConnection(connectionString)
            connection.Open()
            Dim command As New MySqlCommand("INSERT INTO users (FirstName, LastName, Email, Password, Role) VALUES (@firstName, @lastName, @Email, @Password, 'Teacher')", connection)

            command.Parameters.AddWithValue("@firstName", firstName)
            command.Parameters.AddWithValue("@lastName", lastName)
            command.Parameters.AddWithValue("@Email", email)
            command.Parameters.AddWithValue("@Password", password)

            command.ExecuteNonQuery()
        End Using
    End Sub

    ' Method to check if a teacher exists before registration
    Public Function TeacherExists(email As String) As Boolean
        Dim exists As Boolean = False

        Using connection As New MySqlConnection(connectionString)
            connection.Open()
            Dim command As New MySqlCommand("SELECT COUNT(*) FROM users WHERE Email = @Email AND Role = 'Teacher'", connection)
            command.Parameters.AddWithValue("@Email", email)

            exists = Convert.ToInt32(command.ExecuteScalar()) > 0
        End Using

        Return exists
    End Function

    ' Optional: Method to check if the email already exists
    Public Function EmailExists(email As String) As Boolean
        Using connection As New MySqlConnection(connectionString)
            connection.Open()
            Dim command As New MySqlCommand("SELECT COUNT(*) FROM users WHERE Email = @Email", connection)
            command.Parameters.AddWithValue("@Email", email)

            Return Convert.ToInt32(command.ExecuteScalar()) > 0
        End Using
    End Function
End Class
