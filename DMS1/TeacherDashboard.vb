Imports MySql.Data.MySqlClient

Public Class TeacherDashboard
    ' This property will be set when the teacher logs in
    Public Property CurrentUserID As Integer

    Private Sub TeacherDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Load the teacher's email when the dashboard loads
        LoadTeacherEmail()
        LoadDailyLogs()
        LoadSalarySubject()
    End Sub

    ' Method to load teacher's email from the 'users' table
    Private Sub LoadTeacherEmail()
        Dim connectionString As String = "Server=localhost;Database=new_activitydms;Uid=root;Pwd=;"
        Using connection As New MySqlConnection(connectionString)
            Try
                connection.Open()
                ' Query the 'users' table for the teacher's email using UserID
                Dim query As String = "SELECT Email FROM users WHERE UserID = @UserID AND Role = 'Teacher'"
                Using command As New MySqlCommand(query, connection)
                    command.Parameters.AddWithValue("@UserID", CurrentUserID)

                    Dim email As Object = command.ExecuteScalar()
                    If email IsNot Nothing Then
                        lblEmail.Text = email.ToString() ' Set the label text to the email
                    Else
                        lblEmail.Text = "Email not found" ' Optional: Indicate email not found
                    End If
                End Using
            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message) ' Display any error that occurs
            End Try
        End Using
    End Sub

    ' Event handler for Input Grades button click
    Private Sub btnInputGrades_Click(sender As Object, e As EventArgs) Handles btnInputGrades.Click
        switchPanel(New frmInputGrades()) ' Assuming frmInputGrades does not require a teacher ID
    End Sub

    ' Event handler for Logout button click
    Private Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
        frmLogin1.Show() ' Show the login form
        Me.Hide() ' Hide the current dashboard form
    End Sub

    ' Event handler for Schedule button click
    Private Sub btnSchedule_Click(sender As Object, e As EventArgs) Handles btnSchedule.Click
        Dim viewScheduleForm As New frmViewSchedule(CurrentUserID) ' Pass the UserID to view schedule
        switchPanel(viewScheduleForm) ' Call your method to switch panels
    End Sub

    ' Event handler for Room Management button click
    Private Sub btnRoom_Click(sender As Object, e As EventArgs)
        switchPanel(New frmManageRoomSchedule()) ' Assuming frmManageRoomSchedule does not require a teacher ID
    End Sub

    ' Method to switch between forms dynamically inside Panel2
    Private Sub switchPanel(panel As Form)
        ' Clear the existing controls in Panel2
        Panel2.Controls.Clear()

        ' Set the new form to fill the Panel2
        panel.TopLevel = False
        panel.FormBorderStyle = FormBorderStyle.None
        panel.Dock = DockStyle.Fill

        ' Add the new form to the Panel2
        Panel2.Controls.Add(panel)
        Panel2.Tag = panel ' Optional: store the current panel in the tag

        ' Show the new form
        panel.Show()
    End Sub

    Private Sub btnTimeIn_Click(sender As Object, e As EventArgs) Handles btnTimeIn.Click
        Dim connectionString As String = "Server=localhost;Database=new_activitydms;Uid=root;Pwd=;"
        Using connection As New MySqlConnection(connectionString)
            Try
                connection.Open()

                Dim today As String = DateTime.Now.DayOfWeek.ToString()

                ' Check if already timed in
                Dim checkTimeInQuery As String = "SELECT Time_In FROM teacherlogs WHERE teacher_ID = @UserID AND DATE(Time_In) = CURDATE()"
                Using checkCommand As New MySqlCommand(checkTimeInQuery, connection)
                    checkCommand.Parameters.AddWithValue("@UserID", CurrentUserID)
                    If checkCommand.ExecuteScalar() IsNot Nothing Then
                        MessageBox.Show("You have already timed in today.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End If
                End Using

                ' Check if current time falls within scheduled hours
                Dim scheduleQuery As String = "
                    SELECT ScheduleID 
                    FROM schedule 
                    WHERE TeacherID = @UserID 
                      AND DayOfWeek = @Today 
                      AND CURTIME() BETWEEN 
                          STR_TO_DATE(SUBSTRING_INDEX(Time, '-', 1), '%l:%i %p') 
                          AND STR_TO_DATE(SUBSTRING_INDEX(Time, '-', -1), '%l:%i %p')"
                Using scheduleCommand As New MySqlCommand(scheduleQuery, connection)
                    scheduleCommand.Parameters.AddWithValue("@UserID", CurrentUserID)
                    scheduleCommand.Parameters.AddWithValue("@Today", today)
                    If scheduleCommand.ExecuteScalar() Is Nothing Then
                        MessageBox.Show("You cannot time in outside of your scheduled hours.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End If
                End Using

                ' Record the time in
                Dim insertQuery As String = "INSERT INTO teacherlogs (teacher_ID, Time_In) VALUES (@UserID, NOW())"
                Using insertCommand As New MySqlCommand(insertQuery, connection)
                    insertCommand.Parameters.AddWithValue("@UserID", CurrentUserID)
                    insertCommand.ExecuteNonQuery()
                    MessageBox.Show("Time In recorded successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End Using
            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message)
            End Try

        End Using
    End Sub

    Private Sub btnTimeOut_Click(sender As Object, e As EventArgs) Handles btnTimeOut.Click
        Dim connectionString As String = "Server=localhost;Database=new_activitydms;Uid=root;Pwd=;"
        Using connection As New MySqlConnection(connectionString)
            Try
                connection.Open()

                ' Check if already timed out
                Dim checkTimeOutQuery As String = "SELECT Time_Out FROM teacherlogs WHERE teacher_ID = @UserID AND DATE(Time_In) = CURDATE() AND Time_Out IS NOT NULL"
                Using checkCommand As New MySqlCommand(checkTimeOutQuery, connection)
                    checkCommand.Parameters.AddWithValue("@UserID", CurrentUserID)
                    If checkCommand.ExecuteScalar() IsNot Nothing Then
                        MessageBox.Show("You have already timed out today.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End If
                End Using

                ' Check if timed in for today
                Dim checkTimeInQuery As String = "SELECT log_id, Time_In FROM teacherlogs WHERE teacher_ID = @UserID AND DATE(Time_In) = CURDATE() AND Time_Out IS NULL"
                Using checkTimeInCommand As New MySqlCommand(checkTimeInQuery, connection)
                    checkTimeInCommand.Parameters.AddWithValue("@UserID", CurrentUserID)
                    Dim logId As Object = checkTimeInCommand.ExecuteScalar()
                    If logId Is Nothing Then
                        MessageBox.Show("You need to time in before you can time out.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End If

                    ' Get Time_In for calculation
                    Dim timeIn As DateTime = Convert.ToDateTime(checkTimeInCommand.ExecuteScalar())

                    ' Get the Qualifications from the users table to determine the hourly rate
                    Dim getQualificationsQuery As String = "SELECT Qualifications FROM users WHERE UserID = @UserID"
                    Dim qualifications As String = String.Empty
                    Using qualificationsCommand As New MySqlCommand(getQualificationsQuery, connection)
                        qualificationsCommand.Parameters.AddWithValue("@UserID", CurrentUserID)
                        qualifications = qualificationsCommand.ExecuteScalar()?.ToString()
                    End Using

                    ' Set the hourly rate based on the qualifications
                    Dim hourlyRate As Integer = 0
                    Select Case qualifications
                        Case "Part Timer"
                            hourlyRate = 200
                        Case "Masteral"
                            hourlyRate = 400
                        Case "PhD"
                            hourlyRate = 600
                        Case "Doctorate"
                            hourlyRate = 800
                    End Select

                    ' Calculate Hours Worked
                    Dim hoursWorked As Integer = Convert.ToInt32(DateTime.Now.Subtract(timeIn).TotalHours)

                    ' Calculate Total Salary
                    Dim totalSalary As Decimal = hoursWorked * hourlyRate

                    ' Record the time out and update the hours and salary
                    Dim updateQuery As String = "
                    UPDATE teacherlogs 
                    SET Time_Out = NOW(), 
                        Hours = @Hours, 
                        Salary = @Salary 
                    WHERE log_id = @LogID"
                    Using updateCommand As New MySqlCommand(updateQuery, connection)
                        updateCommand.Parameters.AddWithValue("@LogID", logId)
                        updateCommand.Parameters.AddWithValue("@Hours", hoursWorked)
                        updateCommand.Parameters.AddWithValue("@Salary", totalSalary)
                        updateCommand.ExecuteNonQuery()

                        MessageBox.Show("Time Out recorded successfully. Salary calculated and updated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End Using
                End Using
            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message)
            End Try
        End Using
    End Sub


    Private Sub LoadDailyLogs()
        Dim connectionString As String = "Server=localhost;Database=new_activitydms;Uid=root;Pwd=;"
        Using connection As New MySqlConnection(connectionString)
            Try
                connection.Open()

                ' Query to fetch logs, hours worked, and the qualification/salary rate
                Dim query As String = "
            SELECT 
                log_id AS 'Log ID',
                Time_In AS 'Time In',
                Time_Out AS 'Time Out',
                TIMESTAMPDIFF(HOUR, Time_In, Time_Out) AS 'Hours Worked',
                (CASE
                    WHEN qualifications = 'Part Timer' THEN 200
                    WHEN qualifications = 'Masteral' THEN 400
                    WHEN qualifications = 'PhD' THEN 600
                    WHEN qualifications = 'Doctorate' THEN 800
                    ELSE 0
                END) AS 'Hourly Rate',
                (TIMESTAMPDIFF(HOUR, Time_In, Time_Out) * 
                 CASE
                    WHEN qualifications = 'Part Timer' THEN 200
                    WHEN qualifications = 'Masteral' THEN 400
                    WHEN qualifications = 'PhD' THEN 600
                    WHEN qualifications = 'Doctorate' THEN 800
                    ELSE 0
                 END) AS 'Total Salary'
            FROM teacherlogs
            INNER JOIN users ON teacherlogs.teacher_ID = users.UserID
            WHERE teacherlogs.teacher_ID = @UserID AND DATE(Time_In) = CURDATE()"

                Using command As New MySqlCommand(query, connection)
                    command.Parameters.AddWithValue("@UserID", CurrentUserID)

                    Dim adapter As New MySqlDataAdapter(command)
                    Dim dataTable As New DataTable()
                    adapter.Fill(dataTable)

                    ' Calculate grand total salary and total hours worked
                    Dim grandTotalSalary As Integer = 0
                    Dim totalHours As Integer = 0
                    For Each row As DataRow In dataTable.Rows
                        If Not IsDBNull(row("Total Salary")) Then
                            grandTotalSalary += Convert.ToInt32(row("Total Salary"))
                        End If

                        If Not IsDBNull(row("Hours Worked")) Then
                            totalHours += Convert.ToInt32(row("Hours Worked"))
                        End If
                    Next

                    ' Add a new row for the grand total
                    Dim totalRow As DataRow = dataTable.NewRow()
                    totalRow("Log ID") = DBNull.Value
                    totalRow("Time In") = DBNull.Value
                    totalRow("Time Out") = DBNull.Value
                    totalRow("Hours Worked") = totalHours
                    totalRow("Hourly Rate") = DBNull.Value
                    totalRow("Total Salary") = grandTotalSalary
                    dataTable.Rows.Add(totalRow)

                    ' Bind the DataTable to the DataGridView
                    dgHourlyData.DataSource = dataTable

                    ' Customize header text for DataGridView columns
                    dgHourlyData.Columns("Log ID").HeaderText = "Log Identifier"
                    dgHourlyData.Columns("Time In").HeaderText = "Check-In Time"
                    dgHourlyData.Columns("Time Out").HeaderText = "Check-Out Time"
                    dgHourlyData.Columns("Hours Worked").HeaderText = "Total Hours"
                    dgHourlyData.Columns("Hourly Rate").HeaderText = "Hourly Rate"
                    dgHourlyData.Columns("Total Salary").HeaderText = "Total Salary"
                End Using
            Catch ex As Exception
                MessageBox.Show("Error loading logs: " & ex.Message)
            End Try
        End Using
    End Sub

    Public Sub LoadSalarySubject()
        Dim connectionString As String = "Server=localhost;Database=new_activitydms;Uid=root;Pwd=;"
        Using connection As New MySqlConnection(connectionString)
            Try
                connection.Open()

                ' SQL query to get the total salary for each subject
                Dim query As String = "
            SELECT 
                c.Course_name AS 'Subject',
                SUM(tl.Salary) AS 'Total Salary'
            FROM teacherlogs tl
            INNER JOIN schedule s ON tl.teacher_ID = s.TeacherID
            INNER JOIN course c ON s.CourseNumber = c.Course_number
            GROUP BY c.Course_name"

                Using command As New MySqlCommand(query, connection)
                    Dim adapter As New MySqlDataAdapter(command)
                    Dim dataTable As New DataTable()
                    adapter.Fill(dataTable)

                    ' Bind the DataTable to a DataGridView to display the results
                    dgSalarySubject.DataSource = dataTable

                    ' Customize header text for DataGridView columns
                    dgSalarySubject.Columns("Subject").HeaderText = "Course"
                    dgSalarySubject.Columns("Total Salary").HeaderText = "Total Salary"
                End Using
            Catch ex As Exception
                MessageBox.Show("Error loading salary for subjects: " & ex.Message)
            End Try
        End Using
    End Sub

    Private Sub btnDash_Click(sender As Object, e As EventArgs) Handles btnDash.Click
        Panel2.Controls.Clear()
        Panel2.Refresh()
        Panel2.Controls.Add(GroupBox1)
        Panel2.Controls.Add(GroupBox2)
    End Sub
End Class
