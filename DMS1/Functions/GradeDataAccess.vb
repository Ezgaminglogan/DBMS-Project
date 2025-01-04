Imports MySql.Data.MySqlClient

Public Class GradeDataAccess
    ' Insert or update the grade for a user (either student or teacher) in a specific course
    Public Sub InsertGrade(userID As Integer, courseNumber As String, grade As String)
        ' Validate grade
        Dim validGrades As HashSet(Of String) = New HashSet(Of String)({"A", "B", "C", "D", "F"})
        If Not validGrades.Contains(grade.ToUpper()) Then
            Throw New ArgumentException("Invalid grade. Valid grades are A, B, C, D, and F.")
        End If

        ' Insert or update grade in the Grade_Report table
        Dim query As String = "
            INSERT INTO gradereport (UserID, CourseNumber, Grade) 
            VALUES (@UserID, @CourseNumber, @Grade) 
            ON DUPLICATE KEY UPDATE Grade = @Grade"

        Using con As New MySqlConnection(DatabaseConnection.ConnectionString)
            Using cmd As New MySqlCommand(query, con)
                cmd.Parameters.AddWithValue("@UserID", userID)
                cmd.Parameters.AddWithValue("@CourseNumber", courseNumber)
                cmd.Parameters.AddWithValue("@Grade", grade.ToUpper()) ' Store grade as uppercase

                Try
                    con.Open()
                    cmd.ExecuteNonQuery()
                Catch ex As MySqlException
                    Throw New Exception("Database error occurred while inserting grade: " & ex.Message)
                Finally
                    con.Close()
                End Try
            End Using
        End Using
    End Sub

    ' Check the grade of a user (student or teacher) for a specific course
    Public Function CheckGrade(userID As Integer, courseNumber As String) As String
        Using connection As New MySqlConnection(DatabaseConnection.ConnectionString)
            Dim query As String = "SELECT Grade FROM gradereport WHERE UserID = @UserID AND CourseNumber = @CourseNumber"
            Dim command As New MySqlCommand(query, connection)
            command.Parameters.AddWithValue("@UserID", userID)
            command.Parameters.AddWithValue("@CourseNumber", courseNumber)

            Try
                connection.Open()
                Dim result As Object = command.ExecuteScalar()
                If result IsNot Nothing Then
                    Return result.ToString()
                Else
                    Return "No grade found"
                End If
            Catch ex As MySqlException
                Throw New Exception("Database error occurred while checking grade: " & ex.Message)
            Finally
                connection.Close()
            End Try
        End Using
    End Function

    ' Check if the user has passed the prerequisite course
    Public Function HasPassedPrerequisite(userID As Integer, prerequisiteCourse As String) As Boolean
        Dim grade As String = CheckGrade(userID, prerequisiteCourse)
        ' Return True if the grade is not "F" and the grade exists (not "No grade found")
        Return grade <> "F" AndAlso grade <> "No grade found"
    End Function
End Class
