Imports MySql.Data.MySqlClient ' Ensure the MySQL .NET connector is available

Public Class PrerequisiteDataAccess
    ' Connection string for your new_activitydms database
    Private connectionString As String = "server=localhost;userid=root;password=;database=new_activitydms;"

    ' Retrieve the prerequisite course number for a specific course
    Public Function GetPrerequisite(courseNumber As String) As String
        Dim prerequisite As String = Nothing ' Return Nothing if no prerequisite exists

        Try
            Using connection As New MySqlConnection(connectionString)
                connection.Open()

                ' Query to fetch the prerequisite course number
                Dim query As String = "SELECT PrerequisiteCourse_number FROM Prerequisites WHERE Course_number = @CourseNumber"
                Dim command As New MySqlCommand(query, connection)
                command.Parameters.AddWithValue("@CourseNumber", courseNumber)

                ' Execute the query and read the result
                Using reader As MySqlDataReader = command.ExecuteReader()
                    If reader.Read() Then
                        prerequisite = reader("PrerequisiteCourse_number").ToString()
                    End If
                End Using
            End Using
        Catch ex As MySqlException
            ' Log or handle the SQL exception
            Console.WriteLine("SQL Error: " & ex.Message)
        Catch ex As Exception
            ' Log or handle the general exception
            Console.WriteLine("Error: " & ex.Message)
        End Try

        Return prerequisite
    End Function
End Class
