Imports MySql.Data.MySqlClient

Public Class CourseDataAccess
    ' Updated connection string to point to the new_activitydms database
    Private connectionString As String = "Server=localhost;Database=new_activitydms;Uid=root;Pwd=;"

    ' Function to fetch all course numbers
    Public Function GetAllCourses() As List(Of String)
        Dim courses As New List(Of String)()

        ' Establishing a connection using the new database
        Using connection As New MySqlConnection(connectionString)
            Dim command As New MySqlCommand("SELECT Course_number FROM course", connection)

            ' Open the connection and execute the command
            connection.Open()

            Using reader As MySqlDataReader = command.ExecuteReader()
                ' Read each course number and add it to the list
                While reader.Read()
                    courses.Add(reader("Course_number").ToString())
                End While
            End Using
        End Using

        ' Return the list of course numbers
        Return courses
    End Function

    ' Function to get course details by course number (as an example of adding more functionality)
    Public Function GetCourseDetails(courseNumber As String) As String
        Dim courseDetails As String = String.Empty

        Using connection As New MySqlConnection(connectionString)
            Dim command As New MySqlCommand("SELECT Course_name, Credit_hours, Department FROM course WHERE Course_number = @courseNumber", connection)
            command.Parameters.AddWithValue("@courseNumber", courseNumber)

            connection.Open()

            Using reader As MySqlDataReader = command.ExecuteReader()
                If reader.Read() Then
                    ' Constructing a course details string
                    courseDetails = $"Name: {reader("Course_name")}, Credit Hours: {reader("Credit_hours")}, Department: {reader("Department")}"
                End If
            End Using
        End Using

        Return courseDetails
    End Function

    ' Function to add a new course (as an example)
    Public Sub AddCourse(courseNumber As String, courseName As String, creditHours As Integer, department As String)
        Using connection As New MySqlConnection(connectionString)
            Dim command As New MySqlCommand("INSERT INTO course (Course_number, Course_name, Credit_hours, Department) VALUES (@courseNumber, @courseName, @creditHours, @department)", connection)
            command.Parameters.AddWithValue("@courseNumber", courseNumber)
            command.Parameters.AddWithValue("@courseName", courseName)
            command.Parameters.AddWithValue("@creditHours", creditHours)
            command.Parameters.AddWithValue("@department", department)

            connection.Open()
            command.ExecuteNonQuery()
        End Using
    End Sub
End Class
