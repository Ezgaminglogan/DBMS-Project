Imports MySql.Data.MySqlClient

Public Class frmReEnroll

    ' Define database connection variables
    Private connectionString As String = "Server=localhost; Database=new_activitydms; Uid=root; Pwd=;"
    Private connection As New MySqlConnection(connectionString)

    ' Load event when the form is initialized
    Private Sub frmReEnroll_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Auto-populate First Name and Last Name fields when UserID changes
        AddHandler txtUserID.TextChanged, AddressOf txtUserID_TextChanged
        PopulateCourses() ' Populate courses
        PopulateSections() ' Populate sections
    End Sub

    ' Populate available courses from the database with only Course_number displayed
    Private Sub PopulateCourses()
        Dim query As String = "SELECT Course_number, Course_name FROM Course"
        Dim command As New MySqlCommand(query, connection)

        Try
            connection.Open()
            Dim reader As MySqlDataReader = command.ExecuteReader()

            cmbCourses.Items.Clear()
            While reader.Read()
                ' Add only the Course_number to the ComboBox display
                Dim courseNumber As String = reader("Course_number").ToString()
                cmbCourses.Items.Add(courseNumber)
            End While
        Catch ex As Exception
            MessageBox.Show("Error loading courses: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            connection.Close()
        End Try
    End Sub

    ' Populate all available sections from the Section table
    Private Sub PopulateSections()
        Dim query As String = "SELECT Section_identifier FROM Section"
        Dim command As New MySqlCommand(query, connection)

        Try
            connection.Open()
            Dim reader As MySqlDataReader = command.ExecuteReader()

            cmbSections.Items.Clear()
            While reader.Read()
                ' Add section identifiers to the ComboBox
                Dim sectionIdentifier As String = reader("Section_identifier").ToString()
                cmbSections.Items.Add(sectionIdentifier)
            End While
        Catch ex As Exception
            MessageBox.Show("Error loading sections: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            connection.Close()
        End Try
    End Sub

    ' Fetch student details based on UserID from the database
    Private Sub txtUserID_TextChanged(sender As Object, e As EventArgs)
        Dim userID As String = txtUserID.Text.Trim()

        ' Check if UserID is not empty
        If Not String.IsNullOrEmpty(userID) Then
            Try
                Dim studentData As User = GetStudentDataFromDatabase(userID)

                If studentData IsNot Nothing Then
                    txtFirstName.Text = studentData.FirstName
                    txtLastName.Text = studentData.LastName
                Else
                    txtFirstName.Clear()
                    txtLastName.Clear()
                End If
            Catch ex As Exception
                MessageBox.Show("Error retrieving student data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Else
            txtFirstName.Clear()
            txtLastName.Clear()
        End If
    End Sub

    ' Method to fetch student data (First Name, Last Name) from the database using UserID
    Private Function GetStudentDataFromDatabase(userID As String) As User
        ' SQL query to get student details by UserID
        Dim query As String = "SELECT First_name, Last_name, Email, EnrollmentDate FROM users WHERE UserID = @UserID"
        Dim command As New MySqlCommand(query, connection)
        command.Parameters.AddWithValue("@UserID", userID)

        Try
            connection.Open()
            Dim reader As MySqlDataReader = command.ExecuteReader()

            If reader.Read() Then
                ' Fetch all the necessary values from the database and pass them to the Student constructor
                Dim firstName As String = reader("First_name").ToString()
                Dim lastName As String = reader("Last_name").ToString()
                Dim email As String = reader("Email").ToString()
                Dim enrollmentDate As DateTime = Convert.ToDateTime(reader("EnrollmentDate"))

                ' Create and return a new Student object
                Return New User(firstName, lastName, userID, email, enrollmentDate)
            End If
        Catch ex As Exception
            MessageBox.Show("Error fetching student data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            connection.Close()
        End Try

        Return Nothing
    End Function

    ' Handle the re-enrollment process
    Private Sub btnReEnroll_Click(sender As Object, e As EventArgs) Handles btnReEnroll.Click
        Dim userID As String = txtUserID.Text ' Get the user ID from your input field
        Dim selectedCourse As String = cmbCourses.SelectedItem.ToString() ' Get the selected course number from the dropdown

        ' Check if a course is selected
        If String.IsNullOrEmpty(selectedCourse) Then
            MessageBox.Show("Please select a course before re-enrolling.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Check if the student has previously failed the selected course
        If HasFailedCourse(userID, selectedCourse) Then
            MessageBox.Show("You have previously failed this course. You can still re-enroll.")
        End If

        ' Student can proceed with re-enrollment
        Dim selectedSection As String = cmbSections.SelectedItem.ToString() ' Get the selected section number from the dropdown

        ' Call the appropriate enrollment method based on how you are passing the course number
        EnrollStudentInCourse(userID, selectedCourse, selectedSection) ' Use this if passing the course number directly
        ' EnrollStudentInCourseByName(userID, selectedCourseName, selectedSection) ' Use this if using course names

        MessageBox.Show("You have successfully re-enrolled in the course.")
    End Sub

    ' Enroll the student in the selected course and section
    Private Sub EnrollStudentInCourse(userID As String, courseNumber As String, sectionName As String)
        ' Ensure courseNumber is not empty
        If String.IsNullOrEmpty(courseNumber) Then
            MessageBox.Show("Course number cannot be null.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim query As String = "INSERT INTO Enrollment (UserID, CourseNumber, SectionIdentifier, EnrollmentDate) VALUES (@UserID, @CourseNumber, @SectionName, NOW())"
        Dim command As New MySqlCommand(query, connection)
        command.Parameters.AddWithValue("@UserID", userID)
        command.Parameters.AddWithValue("@CourseNumber", courseNumber) ' Make sure this is set correctly
        command.Parameters.AddWithValue("@SectionName", sectionName)

        Try
            connection.Open()
            command.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show("Error enrolling student in course: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            connection.Close()
        End Try
    End Sub

    ' Check if the student has failed the specified course
    Private Function HasFailedCourse(userID As String, courseNumber As String) As Boolean
        ' Query to check if the student has failed the specific course they want to re-enroll in
        Dim query As String = "
    SELECT COUNT(*) 
    FROM Grade_Report 
    WHERE UserID = @UserID 
    AND CourseNumber = @CourseNumber 
    AND Grade = 'F'"

        Using connection As New MySqlConnection(connectionString)
            Dim command As New MySqlCommand(query, connection)
            command.Parameters.AddWithValue("@UserID", userID)
            command.Parameters.AddWithValue("@CourseNumber", courseNumber)

            Try
                connection.Open()
                Dim count As Integer = Convert.ToInt32(command.ExecuteScalar())

                ' Return true if the count is greater than 0 (i.e., student has failed)
                Return count > 0
            Catch ex As Exception
                MessageBox.Show("Error checking re-enrollment eligibility: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using

        ' Return false by default if an error occurs
        Return False
    End Function

    ' Check if the student is eligible to re-enroll in the course
    Private Function CanReEnroll(userID As String, courseNumber As String) As Boolean
        ' ** Query to check if the student has failed the specific course they want to re-enroll in **
        Dim query As String = "
        SELECT COUNT(*) 
        FROM Grade_Report 
        WHERE UserID = @UserID 
        AND CourseNumber = @CourseNumber 
        AND Grade = 'F'"

        Using connection As New MySqlConnection(connectionString)
            Dim command As New MySqlCommand(query, connection)
            command.Parameters.AddWithValue("@UserID", userID)
            command.Parameters.AddWithValue("@CourseNumber", courseNumber)

            Try
                connection.Open()
                Dim count As Integer = Convert.ToInt32(command.ExecuteScalar())

                ' Debug message
                MessageBox.Show($"Debug: User {userID}, Course {courseNumber}, Failed Count: {count}", "Debug", MessageBoxButtons.OK, MessageBoxIcon.Information)

                ' Allow re-enrollment if the student has failed the course (i.e., count > 0)
                Return count > 0
            Catch ex As Exception
                MessageBox.Show("Error checking re-enrollment eligibility: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using

        ' Return false by default if an error occurs
        Return False
    End Function

    ' Enroll the student in the selected course and section by course name
    Private Sub EnrollStudentInCourseByName(userID As String, courseName As String, sectionName As String)
        Dim query As String = "INSERT INTO Enrollment (UserID, CourseNumber, SectionIdentifier, EnrollmentDate) VALUES (@UserID, (SELECT Course_number FROM Course WHERE Course_name = @CourseName), @SectionName, NOW())"
        Dim command As New MySqlCommand(query, connection)
        command.Parameters.AddWithValue("@UserID", userID)
        command.Parameters.AddWithValue("@CourseName", courseName)
        command.Parameters.AddWithValue("@SectionName", sectionName)

        Try
            connection.Open()
            command.ExecuteNonQuery()
        Catch ex As Exception
            Throw New Exception("Error enrolling student in course: " & ex.Message)
        Finally
            connection.Close()
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
        frmEnrollment.Show()
    End Sub
End Class
