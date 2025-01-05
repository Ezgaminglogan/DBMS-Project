Imports MySql.Data.MySqlClient

Public Class frmViewSchedule
    Private userID As Integer ' Store the logged-in user's ID (either teacher or student)

    ' Constructor to initialize the user's ID (can be a teacher or student)
    Public Sub New(loggedInUserID As Integer)
        InitializeComponent()
        userID = loggedInUserID ' Get the user's ID (could be TeacherID or StudentID)
        LoadSchedule() ' Load the schedule on form creation
    End Sub

    Private Sub frmViewSchedule_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Display the user's name
        lblWelcome.Text = "Welcome, " & GetUserName(userID)
    End Sub

    ' Fetch the user's name based on their ID from the 'users' table
    Private Function GetUserName(userID As Integer) As String
        Dim name As String = ""
        Using connection As New MySqlConnection("Server=localhost;Database=new_activitydms;Uid=root;Pwd=;")
            ' Query to fetch name using UserID (whether it's a teacher or student)
            Dim query As String = "SELECT FirstName, LastName FROM users WHERE UserID = @UserID"
            Using command As New MySqlCommand(query, connection)
                command.Parameters.AddWithValue("@UserID", userID)
                connection.Open()
                Using reader As MySqlDataReader = command.ExecuteReader()
                    If reader.Read() Then
                        name = reader("FirstName").ToString() & " " & reader("LastName").ToString()
                    End If
                End Using
            End Using
        End Using
        Return name
    End Function

    ' Load the schedule for the logged-in user (either teacher or student) into the DataGridView
    Private Sub LoadSchedule()
        Dim query As String = "SELECT ScheduleID, TeacherID, CourseNumber, DayOfWeek, Time, RoomNumber , Section FROM schedule WHERE TeacherID = @UserID"

        Using connection As New MySqlConnection("Server=localhost;Database=new_activitydms;Uid=root;Pwd=;")
            Using command As New MySqlCommand(query, connection)
                command.Parameters.AddWithValue("@UserID", userID)
                Using adapter As New MySqlDataAdapter(command)
                    Dim dt As New DataTable()

                    Try
                        connection.Open()
                        adapter.Fill(dt)

                        ' Ensure columns are defined programmatically if not set in the designer
                        If DataGridView1.Columns.Count = 0 Then
                            DataGridView1.Columns.Add("ScheduleID", "Schedule ID")
                            DataGridView1.Columns.Add("TeacherID", "Teacher ID")
                            DataGridView1.Columns.Add("CourseNumber", "Course Number")
                            DataGridView1.Columns.Add("DayOfWeek", "Day Of Week")
                            DataGridView1.Columns.Add("Time", "Time")
                            DataGridView1.Columns.Add("RoomNumber", "Room Number")
                            DataGridView1.Columns.Add("Section", "Section ID")
                        End If

                        ' Check if the DataTable has rows
                        If dt.Rows.Count > 0 Then
                            PopulateScheduleGrid(dt)
                        Else
                            MessageBox.Show("No schedule data found for this user.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    Catch ex As Exception
                        MessageBox.Show("Error loading schedule: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End Using
            End Using
        End Using
    End Sub

    ' Populate the DataGridView with the schedule data
    Private Sub PopulateScheduleGrid(dt As DataTable)
        DataGridView1.Rows.Clear() ' Clear existing rows

        ' Loop through each row in the DataTable and add it to the DataGridView
        For Each row As DataRow In dt.Rows
            ' Extract data from the row
            Dim scheduleID As Integer = Convert.ToInt32(row("ScheduleID"))
            Dim userID As Integer = Convert.ToInt32(row("TeacherID"))
            Dim courseNumber As String = row("CourseNumber").ToString()
            Dim dayOfWeek As String = row("DayOfWeek").ToString()
            Dim time As String = row("Time").ToString()
            Dim roomNumber As String = If(row("RoomNumber") IsNot DBNull.Value, row("RoomNumber").ToString(), String.Empty)
            Dim Section As String = If(row("Section") IsNot DBNull.Value, row("Section").ToString(), String.Empty)
            ' Add a new row to the DataGridView
            DataGridView1.Rows.Add(scheduleID, userID, courseNumber, dayOfWeek, time, roomNumber, Section)
        Next
    End Sub

    ' Load schedule when button is clicked (if needed)
    Private Sub btnLoadSchedule_Click(sender As Object, e As EventArgs)
        LoadSchedule() ' Reload the schedule if needed
    End Sub

    ' Placeholder for export to PDF functionality
    Private Sub btnExportToPDF_Click(sender As Object, e As EventArgs)
        ' Implement PDF export logic
    End Sub

    ' Placeholder for print functionality
    Private Sub btnPrint_Click(sender As Object, e As EventArgs)
        ' Implement print logic
    End Sub

    ' Close the form
    Private Sub btnClose_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

End Class
