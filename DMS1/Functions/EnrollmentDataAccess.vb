Imports MySql.Data.MySqlClient

Public Class EnrollmentDataAccess
    ' Updated connection string to point to new_activitydms
    Private connectionString As String = "Server=localhost;Database=new_activitydms;Uid=root;Pwd=;"
    Private sectionDataAccess As New SectionDataAccess() ' Access section data

    ' Check if the user can enroll in the specified course
    Public Function IsValidEnrollment(userID As Integer, courseNumber As String) As Boolean
        Using connection As New MySqlConnection(connectionString)
            ' Query to check if the user has failed the specified course or its prerequisites
            Dim query As String = "
        SELECT COUNT(*) 
        FROM prerequisite p
        LEFT JOIN gradereport gr ON p.PrerequisiteCourse_number = gr.CourseNumber
        WHERE 
            gr.UserID = @UserID 
            AND gr.Grade = 'F' 
            AND (p.Course_number = @CourseNumber OR p.PrerequisiteCourse_number = @CourseNumber)"

            Dim command As New MySqlCommand(query, connection)
            command.Parameters.AddWithValue("@UserID", userID)
            command.Parameters.AddWithValue("@CourseNumber", courseNumber)

            connection.Open()
            Dim count As Integer = Convert.ToInt32(command.ExecuteScalar())
            Return count = 0 ' Return true if there are no failed prerequisites or the course itself
        End Using
    End Function


    ' Get the current enrollment count for a specific section
    Public Function GetSectionEnrollmentCount(sectionIdentifier As String) As Integer
        Using connection As New MySqlConnection(connectionString)
            Dim query As String = "SELECT COUNT(*) FROM enrollment WHERE SectionIdentifier = @SectionIdentifier"
            Dim command As New MySqlCommand(query, connection)
            command.Parameters.AddWithValue("@SectionIdentifier", sectionIdentifier)

            connection.Open()
            Dim result = command.ExecuteScalar()
            Return If(result IsNot DBNull.Value, Convert.ToInt32(result), 0)
        End Using
    End Function

    ' Check if a user can enroll in a specific section based on the maximum enrollment allowed
    Public Function CanEnrollInSection(sectionIdentifier As String) As Boolean
        Dim currentCount As Integer = GetSectionEnrollmentCount(sectionIdentifier)
        Dim maxCapacity As Integer = sectionDataAccess.GetSectionCapacity(sectionIdentifier)

        Return currentCount < maxCapacity
    End Function

    ' Check if the user is already enrolled in the specified course and section
    Public Function IsUserAlreadyEnrolled(userID As Integer, courseNumber As String, sectionIdentifier As String) As Boolean
        Using connection As New MySqlConnection(connectionString)
            Dim query As String = "SELECT COUNT(*) FROM enrollment WHERE UserID = @UserID AND CourseNumber = @CourseNumber AND SectionIdentifier = @SectionIdentifier"
            Dim command As New MySqlCommand(query, connection)
            command.Parameters.AddWithValue("@UserID", userID)
            command.Parameters.AddWithValue("@CourseNumber", courseNumber)
            command.Parameters.AddWithValue("@SectionIdentifier", sectionIdentifier)

            connection.Open()
            Dim result = command.ExecuteScalar()
            Return If(result IsNot DBNull.Value, Convert.ToInt32(result), 0) > 0 ' Return true if the user is already enrolled
        End Using
    End Function

    ' Enroll the user in the specified course and section
    Public Sub EnrollUserInCourse(userID As Integer, courseNumber As String, sectionIdentifier As String)
        Using conn As MySqlConnection = DatabaseConnection.GetConnection()

            ' Check if the user is already enrolled in this course and section
            If IsUserAlreadyEnrolled(userID, courseNumber, sectionIdentifier) Then
                ' Show a message indicating the user is already enrolled
                MessageBox.Show("You are already enrolled in this course and section.")
            Else
                ' Proceed with enrollment if not already enrolled
                Dim command As MySqlCommand
                Dim insertQuery As String = "INSERT INTO Enrollment (UserID, CourseNumber, SectionIdentifier, EnrollmentDate) VALUES (@userID, @courseNumber, @sectionIdentifier, @enrollmentDate)"
                command = New MySqlCommand(insertQuery, conn)
                command.Parameters.AddWithValue("@userID", userID)
                command.Parameters.AddWithValue("@courseNumber", courseNumber)
                command.Parameters.AddWithValue("@sectionIdentifier", sectionIdentifier)
                command.Parameters.AddWithValue("@enrollmentDate", DateTime.Now)
                command.ExecuteNonQuery()

                ' Notify that the user has been enrolled
                MessageBox.Show("You have successfully enrolled in the course.")
            End If
        End Using
    End Sub

    ' Method to check if the user exists in the system (check by UserID)
    Public Function IsUserExists(userID As Integer) As Boolean
        Using connection As New MySqlConnection(connectionString)
            Dim query As String = "SELECT COUNT(*) FROM users WHERE UserID = @UserID"
            Dim command As New MySqlCommand(query, connection)
            command.Parameters.AddWithValue("@UserID", userID)

            connection.Open()
            Dim result = command.ExecuteScalar()
            Return If(result IsNot DBNull.Value, Convert.ToInt32(result), 0) > 0 ' Return true if the user exists
        End Using
    End Function

    ' Method to check if the user has failed a course
    Public Function HasFailedCourse(userID As Integer, courseNumber As String) As Boolean
        Dim grade As String = CheckGrade(userID, courseNumber)
        Return grade = "F" ' Return True if the user has failed
    End Function

    ' Method to add a new user (for both students and teachers)
    Public Sub AddUser(userID As Integer, firstName As String, lastName As String, role As String)
        Try
            Using connection As New MySqlConnection(connectionString)
                Dim query As String = "INSERT INTO users (UserID, FirstName, LastName, Role) VALUES (@UserID, @FirstName, @LastName, @Role)"
                Dim command As New MySqlCommand(query, connection)
                command.Parameters.AddWithValue("@UserID", userID)
                command.Parameters.AddWithValue("@FirstName", firstName)
                command.Parameters.AddWithValue("@LastName", lastName)
                command.Parameters.AddWithValue("@Role", role)

                connection.Open()
                command.ExecuteNonQuery()
                MessageBox.Show("User added successfully!")
            End Using
        Catch ex As Exception
            MessageBox.Show("Error adding user: " & ex.Message)
        End Try
    End Sub

    ' Check if a course exists in the database
    Public Function CourseExists(courseNumber As String) As Boolean
        Using connection As New MySqlConnection(connectionString)
            Dim query As String = "SELECT COUNT(*) FROM course WHERE Course_number = @CourseNumber"
            Dim command As New MySqlCommand(query, connection)
            command.Parameters.AddWithValue("@CourseNumber", courseNumber)

            connection.Open()
            Dim result = command.ExecuteScalar()
            Return If(result IsNot DBNull.Value, Convert.ToInt32(result), 0) > 0 ' Return true if the course exists
        End Using
    End Function

    ' Method to check if the user has passed a prerequisite
    Public Function HasPassedPrerequisite(userID As Integer, prerequisiteCourse As String) As Boolean
        Dim grade As String = CheckGrade(userID, prerequisiteCourse)
        Return grade <> "F" AndAlso grade <> "No grade found"
    End Function

    ' Method to retrieve the user's grade for a specific course
    Public Function CheckGrade(userID As Integer, courseNumber As String) As String
        Dim grade As String = String.Empty

        Using connection As New MySqlConnection(connectionString)
            Dim query As String = "SELECT Grade FROM gradereport WHERE UserID = @UserID AND CourseNumber = @CourseNumber"
            Dim command As New MySqlCommand(query, connection)
            command.Parameters.AddWithValue("@UserID", userID)
            command.Parameters.AddWithValue("@CourseNumber", courseNumber)

            connection.Open()
            Dim result = command.ExecuteScalar()

            If result IsNot Nothing AndAlso result IsNot DBNull.Value Then
                grade = result.ToString()
            End If
        End Using

        Return grade
    End Function
End Class
