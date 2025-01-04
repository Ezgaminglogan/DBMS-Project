Imports MySql.Data.MySqlClient

Public Class User
    Public Property FirstName As String
    Public Property LastName As String
    Public Property UserID As Integer
    Public Property Email As String
    Public Property EnrollmentDate As DateTime ' For students, or hire date for teachers (if applicable)

    ' Constructor
    Public Sub New(ByVal firstName As String, ByVal lastName As String, ByVal userID As Integer, ByVal email As String, ByVal enrollmentDate As DateTime)
        Me.FirstName = firstName
        Me.LastName = lastName
        Me.UserID = userID
        Me.Email = email
        Me.EnrollmentDate = enrollmentDate
    End Sub

    Public Sub New()
    End Sub

    ' Method to validate the user ID (e.g., ensure it is a positive integer)
    Public Function IsValidUserID() As Boolean
        ' Example validation: Ensure the UserID is a positive integer
        Return UserID > 0
    End Function

    ' Method to validate the email format
    Public Function IsValidEmail() As Boolean
        ' Simple email validation
        Return Email.Contains("@") AndAlso Email.Contains(".")
    End Function

    ' Static method to get user data (for both student and teacher)
    Public Shared Function GetUserByID(userID As Integer) As User
        ' Connect to the database to retrieve user data based on UserID
        ' Assuming there is a "users" table where UserID, FirstName, LastName, Email, and EnrollmentDate (or hire date for teachers) are stored

        Using connection As New MySqlConnection("Server=localhost;Database=new_activitydms;Uid=root;Pwd=;")
            Dim query As String = "SELECT FirstName, LastName, Email, EnrollmentDate FROM users WHERE UserID = @UserID"
            Using command As New MySqlCommand(query, connection)
                command.Parameters.AddWithValue("@UserID", userID)

                Try
                    connection.Open()
                    Using reader As MySqlDataReader = command.ExecuteReader()
                        If reader.Read() Then
                            ' Assuming that the table has a column to differentiate between students and teachers, or they are handled in the same table
                            Dim firstName As String = reader("FirstName").ToString()
                            Dim lastName As String = reader("LastName").ToString()
                            Dim email As String = reader("Email").ToString()
                            Dim enrollmentDate As DateTime = Convert.ToDateTime(reader("EnrollmentDate"))

                            ' Return a new User object with the data
                            Return New User(firstName, lastName, userID, email, enrollmentDate)
                        End If
                    End Using
                Catch ex As Exception
                    Throw New Exception("Error retrieving user data: " & ex.Message)
                End Try
            End Using
        End Using

        ' Return Nothing if no user was found
        Return Nothing
    End Function
End Class
