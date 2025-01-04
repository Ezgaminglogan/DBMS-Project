Imports MySql.Data.MySqlClient

Public Class frmEnrollment
    Private Const MaxEnrollment As Integer = 30
    Public Shared CurrentUserID As String ' Changed from StudentNumber to UserID

    Private Sub frmEnrollment_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblSelectCourse.Visible = False
        cmbCourses.Visible = False
        lblSelectSection.Visible = False
        cmbSections.Visible = False
        btnEnrollInCourse.Visible = False

        DataGridView1.DataSource = Nothing
        DataGridView1.Rows.Clear()


        PopulateID(CurrentUserID)


    End Sub

    Private Sub LoadEnrollmentData(userID As String)
        DataGridView1.DataSource = Nothing
        DataGridView1.Rows.Clear()

        Dim conn As MySqlConnection = DatabaseConnection.GetConnection()
        Try
            ' Updated query to use UserID instead of StudentNumber
            Dim query As String = "
            SELECT 
                e.EnrollmentID, 
                e.UserID, 
                e.CourseNumber, 
                e.SectionIdentifier, 
                e.EnrollmentDate 
            FROM enrollment e 
            INNER JOIN users u ON e.UserID = u.UserID 
            WHERE u.UserID = @UserID"

            Dim cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@UserID", userID)

            Dim adapter As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)

            If dt.Rows.Count > 0 Then
                ' Bind data to DataGridView
                DataGridView1.DataSource = dt

                ' Set custom HeaderText for each column
                DataGridView1.Columns("EnrollmentID").HeaderText = "Enrollment ID"
                DataGridView1.Columns("UserID").HeaderText = "Student ID"
                DataGridView1.Columns("CourseNumber").HeaderText = "Course Number"
                DataGridView1.Columns("SectionIdentifier").HeaderText = "Section ID"
                DataGridView1.Columns("EnrollmentDate").HeaderText = "Enrollment Date"
            Else
                ' Handle case when there is no data
                MessageBox.Show("No enrollment records found for the user.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MessageBox.Show("Error loading data: " & ex.Message)
        Finally
            DatabaseConnection.CloseConnection(conn)
        End Try
    End Sub




    Private Sub btnEnroll_Click(sender As Object, e As EventArgs) Handles btnEnroll.Click
        Dim userID As String = txtUserID.Text.Trim() ' Changed from StudentNumber to UserID
        Dim firstName As String = txtFirstName.Text.Trim()
        Dim lastName As String = txtLastName.Text.Trim()

        ' Validate input fields specific to the enrollment
        If String.IsNullOrWhiteSpace(userID) OrElse
            String.IsNullOrWhiteSpace(firstName) OrElse
            String.IsNullOrWhiteSpace(lastName) Then
            MessageBox.Show("Please fill in all fields.")
            Return
        End If

        Dim userDataAccess As New UserDataAccess()

        ' Check if user exists
        If Not userDataAccess.UserExists(userID) Then ' Changed to use UserID
            MessageBox.Show("Student does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        ' Load this user's enrollment data into the DataGridView
        LoadEnrollmentData(userID)

        ' If user exists, you can now allow them to enroll
        ShowCourseAndSectionSelection() ' Move this here so it's after validation
    End Sub

    Private Sub ShowCourseAndSectionSelection()
        ' Populate courses and sections
        PopulateCourses()
        PopulateSections()

        ' Show the course and section selection UI
        lblSelectCourse.Visible = True
        cmbCourses.Visible = True
        lblSelectSection.Visible = True
        cmbSections.Visible = True
        btnEnrollInCourse.Visible = True ' Show the button after enrollment
    End Sub

    Private Sub PopulateCourses()
        Dim courseDataAccess As New CourseDataAccess()
        Dim courses As List(Of String) = courseDataAccess.GetAllCourses()

        cmbCourses.Items.Clear()
        For Each course As String In courses
            cmbCourses.Items.Add(course)
        Next
    End Sub

    Private Sub PopulateSections()
        Dim sectionDataAccess As New SectionDataAccess()
        Dim sections As List(Of String) = sectionDataAccess.GetAllSections()

        cmbSections.Items.Clear()
        For Each section As String In sections
            cmbSections.Items.Add(section)
        Next

        ' Add event handler for section selection
        AddHandler cmbSections.SelectedIndexChanged, AddressOf cmbSections_SelectedIndexChanged
    End Sub

    Private Sub cmbSections_SelectedIndexChanged(sender As Object, e As EventArgs)
        If cmbSections.SelectedItem IsNot Nothing Then
            Dim selectedSection As String = cmbSections.SelectedItem.ToString()

            ' Fetch the section's current enrollment count and capacity
            Dim sectionDataAccess As New SectionDataAccess()
            Dim currentEnrollmentCount As Integer = sectionDataAccess.GetCurrentEnrollmentCount(selectedSection)
            Dim sectionCapacity As Integer = sectionDataAccess.GetSectionCapacity(selectedSection)
            Dim availableSlots As Integer = sectionCapacity - currentEnrollmentCount

            ' Prompt a message with the available slots information
            MessageBox.Show($"The section '{selectedSection}' has {availableSlots} available slots out of {sectionCapacity}.",
                        "Room Capacity Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub btnEnrollInCourse_Click(sender As Object, e As EventArgs) Handles btnEnrollInCourse.Click
        Dim userID As String = txtUserID.Text.Trim() ' Changed from StudentNumber to UserID
        Dim selectedCourse As String = cmbCourses.SelectedItem?.ToString()
        Dim selectedSection As String = cmbSections.SelectedItem?.ToString()

        ' Validate selections specific to the course enrollment
        If String.IsNullOrWhiteSpace(selectedCourse) OrElse String.IsNullOrWhiteSpace(selectedSection) Then
            MessageBox.Show("Please fill all fields.")
            Return
        End If

        Dim enrollmentDataAccess As New EnrollmentDataAccess()

        ' Check if the user is already enrolled in the course and section
        If enrollmentDataAccess.IsUserAlreadyEnrolled(userID, selectedCourse, selectedSection) Then ' Changed from IsStudentAlreadyEnrolled to IsUserAlreadyEnrolled
            MessageBox.Show("You are already enrolled in this course and section.", "Enrollment Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Check if the user can enroll (prerequisite check)
        If Not enrollmentDataAccess.IsValidEnrollment(userID, selectedCourse) Then ' Changed from IsValidEnrollment(studentNumber) to IsValidEnrollment(userID)
            MessageBox.Show("You have failed a prerequisite course for this enrollment.")
            Return
        End If

        ' Check if the selected section has space for more students
        Dim sectionDataAccess As New SectionDataAccess()
        Dim currentEnrollmentCount As Integer = sectionDataAccess.GetCurrentEnrollmentCount(selectedSection)
        Dim sectionCapacity As Integer = sectionDataAccess.GetSectionCapacity(selectedSection)

        If currentEnrollmentCount < sectionCapacity Then
            ' Proceed with enrollment
            enrollmentDataAccess.EnrollUserInCourse(userID, selectedCourse, selectedSection)
            MessageBox.Show("Successfully enrolled!", "Enrollment Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' Refresh the DataGridView to show the new enrollment for the user
            LoadEnrollmentData(userID)
        Else
            MessageBox.Show("Cannot enroll in this section as it is full.", "Enrollment Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub btnReEnroll_Click(sender As Object, e As EventArgs) Handles btnReEnroll.Click
        Dim reEnrollForm As New frmReEnroll()
        reEnrollForm.Show()
        Me.Hide()
    End Sub

    Private Sub txtUserID_TextChanged(sender As Object, e As EventArgs) Handles txtUserID.TextChanged

    End Sub

    Public Sub PopulateID(ByVal StudentID As Integer)
        ' Connection string to the database
        Dim connectionString As String = "Server=localhost;Database=new_activitydms;Uid=root;Pwd=;"
        Using connection As New MySqlConnection(connectionString)
            Try
                connection.Open()

                ' Query to fetch UserID, FirstName, and LastName based on StudentID
                Dim query As String = "SELECT UserID, FirstName, LastName FROM users WHERE UserID = @StudentID"
                Using command As New MySqlCommand(query, connection)
                    ' Add parameter for StudentID
                    command.Parameters.AddWithValue("@StudentID", StudentID)

                    ' Execute the query and read the data
                    Using reader As MySqlDataReader = command.ExecuteReader()
                        If reader.Read() Then
                            ' Populate the text boxes with the retrieved values
                            txtUserID.Text = reader("UserID").ToString()
                            txtFirstName.Text = reader("FirstName").ToString()
                            txtLastName.Text = reader("LastName").ToString()
                        Else
                            ' Handle case where no matching user is found
                            MessageBox.Show("No student found with the given ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            txtUserID.Clear()
                            txtFirstName.Clear()
                            txtLastName.Clear()
                        End If
                    End Using
                End Using
            Catch ex As Exception
                ' Handle any errors that occur
                MessageBox.Show("Error: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub


End Class
