Imports MySql.Data.MySqlClient

Public Class StudentRegistrationDataAccess
    Private connectionString As String = "Server=localhost;Database=new_activitydms;Uid=root;Pwd=;"

    ' Method to register a new user (student or teacher)
    Public Sub RegisterUser(userID As String, firstName As String, lastName As String, email As String, password As String, role As String)
        Using connection As New MySqlConnection(connectionString)
            connection.Open()
            ' Register a user (student or teacher)
            Dim command As New MySqlCommand("INSERT INTO users (UserID, FirstName, LastName, Email, Password, Role) VALUES (@userID, @firstName, @lastName, @Email, @Password, @Role)", connection)

            command.Parameters.AddWithValue("@userID", userID)
            command.Parameters.AddWithValue("@firstName", firstName)
            command.Parameters.AddWithValue("@lastName", lastName)
            command.Parameters.AddWithValue("@Email", email)
            command.Parameters.AddWithValue("@Password", password)
            command.Parameters.AddWithValue("@Role", role)

            command.ExecuteNonQuery()
        End Using
    End Sub

    ' Optional: Method to check if a user exists before registration
    Public Function UserExists(userID As String) As Boolean
        Dim exists As Boolean = False

        Using connection As New MySqlConnection(connectionString)
            connection.Open()
            ' Check if the user exists based on UserID
            Dim command As New MySqlCommand("SELECT COUNT(*) FROM users WHERE UserID = @userID", connection)
            command.Parameters.AddWithValue("@userID", userID)

            exists = Convert.ToInt32(command.ExecuteScalar()) > 0
        End Using

        Return exists
    End Function
End Class
