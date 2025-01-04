Imports MySql.Data.MySqlClient

Public Class ScheduleDataAccess
    Private dbConnectionString As String = "Server=localhost;Database=new_activitydms;Uid=root;Pwd=;"

    Public Sub AddSchedule(userID As Integer, courseNumber As String, dayOfWeek As String, timeSlot As String, roomNumber As String, section As String)
        ' Check for conflicts before adding the schedule
        If CheckScheduleConflict(dayOfWeek, timeSlot, roomNumber, section, courseNumber) Then
            MessageBox.Show("Conflict: A schedule already exists for this day, time, room, and section.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
        End If

        ' Proceed to add schedule if no conflict is found
        Using connection As New MySqlConnection(dbConnectionString)
            Dim query As String = "INSERT INTO schedule (TeacherID, CourseNumber, DayOfWeek, Time, RoomNumber, Section) " &
                              "VALUES (@teacherID, @courseNumber, @dayOfWeek, @timeSlot, @roomNumber, @section)"
            Using command As New MySqlCommand(query, connection)
                command.Parameters.AddWithValue("@teacherID", userID)
                command.Parameters.AddWithValue("@courseNumber", courseNumber)
                command.Parameters.AddWithValue("@dayOfWeek", dayOfWeek)
                command.Parameters.AddWithValue("@timeSlot", timeSlot)
                command.Parameters.AddWithValue("@roomNumber", roomNumber)
                command.Parameters.AddWithValue("@section", section)
                connection.Open()
                command.ExecuteNonQuery()
            End Using
        End Using
    End Sub


    ' Update an existing schedule for a user (either teacher or student)
    Public Sub UpdateSchedule(scheduleID As Integer, userID As Integer, courseNumber As String, dayOfWeek As String, timeSlot As String, roomNumber As String, section As String)
        Using connection As New MySqlConnection(dbConnectionString)
            ' Adjusted to use UserID for both teacher and student
            Dim query As String = "UPDATE schedule SET TeacherID = @teacherID, CourseNumber = @courseNumber, DayOfWeek = @dayOfWeek, Time = @timeSlot, " &
                                  "RoomNumber = @roomNumber, Section = @section WHERE ScheduleID = @scheduleID"
            Using command As New MySqlCommand(query, connection)
                command.Parameters.AddWithValue("@scheduleID", scheduleID)
                command.Parameters.AddWithValue("@teacherID", userID)
                command.Parameters.AddWithValue("@courseNumber", courseNumber)
                command.Parameters.AddWithValue("@dayOfWeek", dayOfWeek)
                command.Parameters.AddWithValue("@timeSlot", timeSlot)
                command.Parameters.AddWithValue("@roomNumber", roomNumber)
                command.Parameters.AddWithValue("@section", section)
                connection.Open()
                command.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    ' Delete a schedule for a user (either teacher or student)
    Public Sub DeleteSchedule(scheduleID As Integer)
        Using connection As New MySqlConnection(dbConnectionString)
            Dim query As String = "DELETE FROM schedule WHERE ScheduleID = @scheduleID"
            Using command As New MySqlCommand(query, connection)
                command.Parameters.AddWithValue("@scheduleID", scheduleID)
                connection.Open()
                command.ExecuteNonQuery()
            End Using
        End Using
    End Sub



    ' Check for conflicts based on Day, Time, and Room
    Public Function CheckScheduleConflict(day As String, timeSlot As String, roomNumber As String, section As String, courseNumber As String) As Boolean
        Using connection As New MySqlConnection(dbConnectionString)
            Dim query As String = "SELECT COUNT(*) FROM schedule WHERE DayOfWeek = @dayOfWeek AND Time = @timeSlot AND RoomNumber = @roomNumber AND Section = @section AND CourseNumber = @courseNumber"
            Using command As New MySqlCommand(query, connection)
                command.Parameters.AddWithValue("@dayOfWeek", day)
                command.Parameters.AddWithValue("@timeSlot", timeSlot)
                command.Parameters.AddWithValue("@roomNumber", roomNumber)
                command.Parameters.AddWithValue("@section", section)
                command.Parameters.AddWithValue("@courseNumber", courseNumber)

                connection.Open()
                Dim result = Convert.ToInt32(command.ExecuteScalar())
                Return result > 0 ' Returns True if conflict is found
            End Using
        End Using
    End Function


End Class
