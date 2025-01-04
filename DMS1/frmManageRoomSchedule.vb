Imports MySql.Data.MySqlClient

Public Class frmManageRoomSchedule
    ' Updated connection string to point to new_activitydms database
    Private connectionString As String = "server=localhost;user id=root;password=;database=new_activitydms"

    ' Property to hold the UserID passed from the TeacherDashboard or StudentDashboard
    Public Property UserID As String

    ' Form Load event to populate room availability, rooms, and courses
    Private Sub frmManageRoomSchedule_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadRooms() ' Load room numbers into ComboBox
        LoadCourses() ' Load courses into ComboBox

        ' Set the txtUserID with the UserID passed from the TeacherDashboard or StudentDashboard
        txtUserID.Text = UserID

        ' Configure DateTimePickers to show only time
        dtpStartTime.Format = DateTimePickerFormat.Time
        dtpStartTime.ShowUpDown = True

        dtpEndTime.Format = DateTimePickerFormat.Time
        dtpEndTime.ShowUpDown = True

        ' Populate DayOfWeek ComboBox
        cmbDay.Items.AddRange(New String() {"Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"})
    End Sub

    ' Method to load room numbers into the ComboBox
    Private Sub LoadRooms()
        Dim query As String = "SELECT RoomNumber FROM room"
        Using con As New MySqlConnection(connectionString)
            Using cmd As New MySqlCommand(query, con)
                con.Open()
                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        ' Add RoomNumber to the ComboBox
                        cmbRoomNumber.Items.Add(reader("RoomNumber").ToString())
                    End While
                End Using
            End Using
        End Using
    End Sub



    ' Method to load courses into the ComboBox
    Private Sub LoadCourses()
        Dim query As String = "SELECT Course_number FROM course"
        Using con As New MySqlConnection(connectionString)
            Using cmd As New MySqlCommand(query, con)
                con.Open()
                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        ' Add only the Course_number to the ComboBox
                        cmbCourse.Items.Add(reader("Course_number").ToString())
                    End While
                End Using
            End Using
        End Using
    End Sub

    ' Method to check if the course exists
    Private Function CourseExists(courseNumber As String) As Boolean
        Dim query As String = "SELECT COUNT(*) FROM course WHERE Course_number = @CourseNumber"
        Using con As New MySqlConnection(connectionString)
            Using cmd As New MySqlCommand(query, con)
                cmd.Parameters.AddWithValue("@CourseNumber", courseNumber)
                con.Open()
                Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                Return count > 0 ' Return true if course exists
            End Using
        End Using
    End Function

    ' Method to insert room schedule
    Private Sub InsertRoomSchedule(userID As String, courseNumber As String, roomNumber As String, dayOfWeek As String, startTime As DateTime, endTime As DateTime)
        ' Check if the course exists before trying to insert
        If Not CourseExists(courseNumber) Then
            MessageBox.Show("The specified course does not exist.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        ' Use the correct column name in the SQL statement
        Dim query As String = "INSERT INTO roomschedule (UserID, Course_number, RoomNumber, DayOfWeek, StartTime, EndTime) " &
                          "VALUES (@UserID, @CourseNumber, @RoomNumber, @DayOfWeek, @StartTime, @EndTime)"

        Try
            Using con As New MySqlConnection(connectionString)
                Using cmd As New MySqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@UserID", userID)
                    cmd.Parameters.AddWithValue("@CourseNumber", courseNumber) ' Ensure this matches the column name in your table
                    cmd.Parameters.AddWithValue("@RoomNumber", roomNumber)
                    cmd.Parameters.AddWithValue("@DayOfWeek", dayOfWeek)
                    cmd.Parameters.AddWithValue("@StartTime", startTime)
                    cmd.Parameters.AddWithValue("@EndTime", endTime)

                    con.Open()
                    cmd.ExecuteNonQuery()
                    MessageBox.Show("Room schedule inserted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End Using
            End Using
        Catch ex As MySqlException
            MessageBox.Show("An error occurred while inserting the schedule: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show("An unexpected error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Button Click event to save the room schedule
    Private Sub btnSaveSchedule_Click(sender As Object, e As EventArgs) Handles btnSaveSchedule.Click
        Dim userID As String = txtUserID.Text.Trim()
        Dim courseNumber As String = cmbCourse.SelectedItem?.ToString() ' Directly use the selected course number
        Dim roomNumber As String = cmbRoomNumber.SelectedItem?.ToString() ' Use the ComboBox for RoomNumber
        Dim dayOfWeek As String = cmbDay.SelectedItem?.ToString()
        Dim startTime As DateTime = dtpStartTime.Value
        Dim endTime As DateTime = dtpEndTime.Value

        ' Validate input
        If String.IsNullOrEmpty(roomNumber) Or String.IsNullOrEmpty(userID) Or String.IsNullOrEmpty(courseNumber) Or String.IsNullOrEmpty(dayOfWeek) Then
            MessageBox.Show("All fields must be filled out.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        If endTime <= startTime Then
            MessageBox.Show("End time must be greater than start time.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        ' Check room availability before inserting
        If CheckRoomAvailability(roomNumber, dayOfWeek, startTime, endTime) Then
            InsertRoomSchedule(userID, courseNumber, roomNumber, dayOfWeek, startTime, endTime)

            ' Optionally, clear the input fields after insertion
            ClearForm()


            ' Show a success message
            MessageBox.Show("Schedule saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show("Room is already scheduled during this time. Please select another time or room.", "Room Unavailable", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    ' Method to clear the form after successful insert
    Private Sub ClearForm()
        txtUserID.Clear() ' Optionally clear this field if you want to reset it
        cmbRoomNumber.SelectedIndex = -1 ' Clear selected room number
        cmbDay.SelectedIndex = -1
        cmbCourse.SelectedIndex = -1 ' Clear selected course
        dtpStartTime.Value = DateTime.Now
        dtpEndTime.Value = DateTime.Now
    End Sub

    ' Method to check if the room is available during the specified time on the specified day
    Private Function CheckRoomAvailability(roomNumber As String, dayOfWeek As String, startTime As DateTime, endTime As DateTime) As Boolean
        Dim query As String = "SELECT COUNT(*) FROM roomschedule WHERE RoomNumber = @RoomNumber AND DayOfWeek = @DayOfWeek AND " &
                              "((StartTime < @EndTime AND EndTime > @StartTime))"

        Using con As New MySqlConnection(connectionString)
            Using cmd As New MySqlCommand(query, con)
                cmd.Parameters.AddWithValue("@RoomNumber", roomNumber)
                cmd.Parameters.AddWithValue("@DayOfWeek", dayOfWeek)
                cmd.Parameters.AddWithValue("@StartTime", startTime)
                cmd.Parameters.AddWithValue("@EndTime", endTime)

                con.Open()
                Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                Return count = 0 ' Return true if no overlapping schedules are found
            End Using
        End Using
    End Function
End Class
