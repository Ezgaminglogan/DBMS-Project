Imports MySql.Data.MySqlClient

Public Class StudentDataAccess
    ' Updated connection string for the new database 'new_activitydms'
    Private connectionString As String = "Server=localhost;Database=new_activitydms;Uid=root;Pwd=;"

    ' Method to retrieve full student details from the database
    Public Function GetStudentDetails(userID As String) As User
        Dim student As User = Nothing

        Using connection As New MySqlConnection(connectionString)
            connection.Open()
            ' Join Users and Student tables to get full details
            Dim command As New MySqlCommand("SELECT u.FirstName, u.LastName, u.Email, s.EnrollmentDate 
                                             FROM Users u
                                             JOIN Student s ON u.UserID = s.UserID
                                             WHERE u.UserID = @UserID", connection)
            command.Parameters.AddWithValue("@UserID", userID)

            Using reader As MySqlDataReader = command.ExecuteReader()
                If reader.Read() Then
                    student = New User(
                        reader("FirstName").ToString(),
                        reader("LastName").ToString(),
                        userID,
                        reader("Email").ToString(),
                        Convert.ToDateTime(reader("EnrollmentDate"))
                    )
                End If
            End Using
        End Using

        Return student
    End Function

    ' Method to check if a student exists (based on Users table)
    Public Function StudentExists(userID As String) As Boolean
        Dim exists As Boolean = False

        Using connection As New MySqlConnection(connectionString)
            connection.Open()
            ' Check existence in Users table
            Dim command As New MySqlCommand("SELECT COUNT(*) FROM Users WHERE UserID = @UserID", connection)
            command.Parameters.AddWithValue("@UserID", userID)

            exists = Convert.ToInt32(command.ExecuteScalar()) > 0
        End Using

        Return exists
    End Function

    ' Get all students as a list (fetching from Users and Student tables)
    Public Function GetAllStudents() As List(Of User)
        Dim students As New List(Of User)()

        Using connection As New MySqlConnection(connectionString)
            connection.Open()
            ' Join Users and Student tables to fetch all students
            Dim command As New MySqlCommand("SELECT u.UserID, u.First_name, u.Last_name, u.Email, s.EnrollmentDate 
                                             FROM Users u
                                             JOIN Student s ON u.UserID = s.UserID", connection)
            Dim reader As MySqlDataReader = command.ExecuteReader()

            While reader.Read()
                ' Create and add Student object to the list
                Dim student As New User(
                    reader("FirstName").ToString(),
                    reader("LastName").ToString(),
                    reader("UserID").ToString(),
                    reader("Email").ToString(),
                    Convert.ToDateTime(reader("EnrollmentDate"))
                )
                students.Add(student)
            End While
        End Using

        Return students
    End Function

    ' Method to check if a student is enrolled in any course
    Public Function IsStudentEnrolled(userID As String) As Boolean
        Dim enrolled As Boolean = False

        Using connection As New MySqlConnection(connectionString)
            connection.Open()
            ' Check if student is enrolled in any course
            Dim command As New MySqlCommand("SELECT COUNT(*) FROM Enrollment WHERE UserID = @UserID", connection)
            command.Parameters.AddWithValue("@UserID", userID)

            enrolled = Convert.ToInt32(command.ExecuteScalar()) > 0
        End Using

        Return enrolled
    End Function
End Class
