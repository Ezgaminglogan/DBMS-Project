Imports System.Drawing.Printing
Imports System.Globalization
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
        CurrentUserID = 0 ' Reset the current user ID
        frmLogin1.Show() ' Show the login form
        Me.Hide() ' Hide the current dashboard form
    End Sub

    ' Event handler for Schedule button click
    Private Sub btnSchedule_Click(sender As Object, e As EventArgs) Handles btnSchedule.Click
        Dim viewScheduleForm As New frmViewSchedule(CurrentUserID) ' Pass the UserID to view schedule
        switchPanel(viewScheduleForm) ' Call your method to switch panels
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
            connection.Open()

            ' Get today's day of the week
            Dim today As String = DateTime.Now.DayOfWeek.ToString()

            ' Check if current time falls within any scheduled hours
            Dim scheduleQuery As String = "
        SELECT ScheduleID, Time 
        FROM schedule 
        WHERE TeacherID = @UserID 
          AND DayOfWeek = @Today 
          AND CURTIME() BETWEEN 
              STR_TO_DATE(SUBSTRING_INDEX(Time, '-', 1), '%l:%i %p') 
              AND STR_TO_DATE(SUBSTRING_INDEX(Time, '-', -1), '%l:%i %p')"
            Using scheduleCommand As New MySqlCommand(scheduleQuery, connection)
                scheduleCommand.Parameters.AddWithValue("@UserID", CurrentUserID)
                scheduleCommand.Parameters.AddWithValue("@Today", today)
                Using reader As MySqlDataReader = scheduleCommand.ExecuteReader()
                    If reader.Read() Then
                        Dim scheduleID As Integer = reader.GetInt32("ScheduleID")
                        Dim scheduleTime As String = reader.GetString("Time")
                        reader.Close()

                        ' Check if the teacher has already timed in for this schedule today
                        Dim checkTimeInQuery As String = "
                    SELECT log_id 
                    FROM teacherlogs 
                    WHERE teacher_ID = @UserID 
                      AND DATE(Time_In) = CURDATE() 
                      AND ScheduleID = @ScheduleID 
                      AND Time_Out IS NULL"
                        Using checkCommand As New MySqlCommand(checkTimeInQuery, connection)
                            checkCommand.Parameters.AddWithValue("@UserID", CurrentUserID)
                            checkCommand.Parameters.AddWithValue("@ScheduleID", scheduleID)
                            If checkCommand.ExecuteScalar() IsNot Nothing Then
                                MessageBox.Show("You have already timed in for this schedule today.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                Exit Sub
                            End If
                        End Using

                        ' Calculate late hours
                        Dim lateHours As Decimal = 0
                        Dim scheduledStartTime As DateTime

                        ' Fetch the scheduled start time as a string
                        Dim getScheduledStartTimeQuery As String = "
                        SELECT STR_TO_DATE(SUBSTRING_INDEX(Time, '-', 1), '%l:%i %p') AS StartTime 
                        FROM schedule 
                        WHERE ScheduleID = @ScheduleID"
                        Using getScheduledStartTimeCommand As New MySqlCommand(getScheduledStartTimeQuery, connection)
                            getScheduledStartTimeCommand.Parameters.AddWithValue("@ScheduleID", scheduleID)
                            Dim scheduledStartTimeString As String = getScheduledStartTimeCommand.ExecuteScalar()?.ToString()

                            ' Parse the scheduled start time string into a DateTime object
                            If Not String.IsNullOrEmpty(scheduledStartTimeString) Then
                                scheduledStartTime = DateTime.ParseExact(scheduledStartTimeString, "HH:mm:ss", CultureInfo.InvariantCulture)
                            Else
                                MessageBox.Show("Failed to retrieve scheduled start time.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                Exit Sub
                            End If
                        End Using

                        ' Compare the current time with the scheduled start time
                        If DateTime.Now > scheduledStartTime Then
                            lateHours = Math.Round(CDec((DateTime.Now - scheduledStartTime).TotalHours), 2)
                        End If

                        ' Insert the time in record with the ScheduleID and LateHours
                        Dim insertQuery As String = "
                    INSERT INTO teacherlogs (teacher_ID, Time_In, ScheduleID, LateHours) 
                    VALUES (@UserID, NOW(), @ScheduleID, @LateHours)"
                        Using insertCommand As New MySqlCommand(insertQuery, connection)
                            insertCommand.Parameters.AddWithValue("@UserID", CurrentUserID)
                            insertCommand.Parameters.AddWithValue("@ScheduleID", scheduleID)
                            insertCommand.Parameters.AddWithValue("@LateHours", lateHours)
                            insertCommand.ExecuteNonQuery()

                            MessageBox.Show($"Time In recorded successfully for schedule: {scheduleTime}. Late hours: {lateHours}.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End Using
                    Else
                        MessageBox.Show("You cannot time in outside of your scheduled hours.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End If
                End Using
            End Using
        End Using

        ' Reload data
        LoadDailyLogs()
        LoadSalarySubject()
    End Sub

    Private Sub btnTimeOut_Click(sender As Object, e As EventArgs) Handles btnTimeOut.Click
        Dim connectionString As String = "Server=localhost;Database=new_activitydms;Uid=root;Pwd=;"
        Using connection As New MySqlConnection(connectionString)
            connection.Open()

            ' Get today's day of the week
            Dim today As String = DateTime.Now.DayOfWeek.ToString()

            ' Check if current time falls within any scheduled hours
            Dim scheduleQuery As String = "
            SELECT ScheduleID, Time 
            FROM schedule 
            WHERE TeacherID = @UserID 
              AND DayOfWeek = @Today 
              AND CURTIME() BETWEEN 
                  STR_TO_DATE(SUBSTRING_INDEX(Time, '-', 1), '%l:%i %p') 
                  AND STR_TO_DATE(SUBSTRING_INDEX(Time, '-', -1), '%l:%i %p')"
            Using scheduleCommand As New MySqlCommand(scheduleQuery, connection)
                scheduleCommand.Parameters.AddWithValue("@UserID", CurrentUserID)
                scheduleCommand.Parameters.AddWithValue("@Today", today)
                Using reader As MySqlDataReader = scheduleCommand.ExecuteReader()
                    If reader.Read() Then
                        Dim scheduleID As Integer = reader.GetInt32("ScheduleID")
                        Dim scheduleTime As String = reader.GetString("Time")
                        reader.Close()

                        ' Check if timed in for this schedule today and retrieve Time_In
                        Dim checkTimeInQuery As String = "
                        SELECT log_id, Time_In 
                        FROM teacherlogs 
                        WHERE teacher_ID = @UserID 
                          AND DATE(Time_In) = CURDATE() 
                          AND ScheduleID = @ScheduleID 
                          AND Time_Out IS NULL"
                        Using checkTimeInCommand As New MySqlCommand(checkTimeInQuery, connection)
                            checkTimeInCommand.Parameters.AddWithValue("@UserID", CurrentUserID)
                            checkTimeInCommand.Parameters.AddWithValue("@ScheduleID", scheduleID)
                            Using reader2 As MySqlDataReader = checkTimeInCommand.ExecuteReader()
                                If reader2.Read() Then
                                    ' Retrieve log_id and Time_In
                                    Dim logId As Integer = reader2.GetInt32("log_id")
                                    Dim timeIn As DateTime = reader2.GetDateTime("Time_In")
                                    reader2.Close()

                                    ' Get the Qualifications from the users table to determine the hourly rate
                                    Dim getQualificationsQuery As String = "SELECT Qualifications FROM users WHERE UserID = @UserID"
                                    Dim qualifications As String = String.Empty
                                    Using qualificationsCommand As New MySqlCommand(getQualificationsQuery, connection)
                                        qualificationsCommand.Parameters.AddWithValue("@UserID", CurrentUserID)
                                        qualifications = qualificationsCommand.ExecuteScalar()?.ToString()
                                    End Using

                                    ' Set the hourly rate based on the qualifications
                                    Dim hourlyRate As Decimal
                                    Select Case qualifications
                                        Case "Part Timer"
                                            hourlyRate = 200D
                                        Case "Masteral"
                                            hourlyRate = 400D
                                        Case "PhD"
                                            hourlyRate = 600D
                                        Case "Doctorate"
                                            hourlyRate = 800D
                                        Case Else
                                            hourlyRate = 0.0D ' Handle unexpected or missing qualifications
                                    End Select

                                    ' Calculate Hours Worked (with decimal precision)
                                    Dim hoursWorked As Decimal = Math.Round(CDec(DateTime.Now.Subtract(timeIn).TotalHours), 2)

                                    ' Calculate Late Hours
                                    Dim lateHours As Decimal = 0
                                    Dim scheduledStartTime As DateTime
                                    Dim getScheduledStartTimeQuery As String = "
                                    SELECT STR_TO_DATE(SUBSTRING_INDEX(Time, '-', 1), '%l:%i %p') AS StartTime 
                                    FROM schedule 
                                    WHERE ScheduleID = @ScheduleID"
                                    Using getScheduledStartTimeCommand As New MySqlCommand(getScheduledStartTimeQuery, connection)
                                        getScheduledStartTimeCommand.Parameters.AddWithValue("@ScheduleID", scheduleID)
                                        scheduledStartTime = Convert.ToDateTime(getScheduledStartTimeCommand.ExecuteScalar())
                                    End Using

                                    If timeIn > scheduledStartTime Then
                                        lateHours = Math.Round(CDec((timeIn - scheduledStartTime).TotalHours), 2)
                                    End If

                                    ' Calculate Total Salary (with decimal precision and late hours deduction)
                                    Dim totalSalary As Decimal = Math.Round((hoursWorked - lateHours) * hourlyRate, 2)

                                    ' Record the time out and update the hours, late hours, and salary
                                    Dim updateQuery As String = "
                                UPDATE teacherlogs 
                                SET Time_Out = NOW(), 
                                    Hours = @Hours, 
                                    LateHours = @LateHours, 
                                    Salary = @Salary 
                                WHERE log_id = @LogID"
                                    Using updateCommand As New MySqlCommand(updateQuery, connection)
                                        updateCommand.Parameters.AddWithValue("@LogID", logId)
                                        updateCommand.Parameters.AddWithValue("@Hours", hoursWorked)
                                        updateCommand.Parameters.AddWithValue("@LateHours", lateHours)
                                        updateCommand.Parameters.AddWithValue("@Salary", totalSalary)
                                        updateCommand.ExecuteNonQuery()

                                        MessageBox.Show($"Time Out recorded successfully for schedule: {scheduleTime}. Salary calculated and updated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    End Using
                                Else
                                    MessageBox.Show("You need to time in for this schedule before you can time out.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                End If
                            End Using
                        End Using
                    Else
                        MessageBox.Show("You cannot time out outside of your scheduled hours.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End If
                End Using
            End Using
        End Using

        ' Reload data
        LoadDailyLogs()
        LoadSalarySubject()
    End Sub



    Private Sub LoadDailyLogs()
        Dim connectionString As String = "Server=localhost;Database=new_activitydms;Uid=root;Pwd=;"
        Using connection As New MySqlConnection(connectionString)
            Try
                connection.Open()

                ' Query to fetch logs, hours worked, late hours, and the qualification/salary rate
                Dim query As String = "
            SELECT 
                log_id AS 'Log ID',
                Time_In AS 'Time In',
                Time_Out AS 'Time Out',
                ScheduleID AS 'Schedule ID',
                TIMESTAMPDIFF(HOUR, Time_In, Time_Out) AS 'Hours Worked',
                LateHours AS 'Late Hours',
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

                    ' Calculate grand total salary, total hours worked, and total late hours
                    Dim grandTotalSalary As Decimal = 0
                    Dim totalHours As Decimal = 0
                    Dim totalLateHours As Decimal = 0
                    For Each row As DataRow In dataTable.Rows
                        If Not IsDBNull(row("Total Salary")) Then
                            grandTotalSalary += Convert.ToDecimal(row("Total Salary"))
                        End If

                        If Not IsDBNull(row("Hours Worked")) Then
                            totalHours += Convert.ToDecimal(row("Hours Worked"))
                        End If

                        If Not IsDBNull(row("Late Hours")) Then
                            totalLateHours += Convert.ToDecimal(row("Late Hours"))
                        End If
                    Next

                    ' Add a new row for the grand total
                    Dim totalRow As DataRow = dataTable.NewRow()
                    totalRow("Log ID") = DBNull.Value
                    totalRow("Time In") = DBNull.Value
                    totalRow("Time Out") = DBNull.Value
                    totalRow("Schedule ID") = DBNull.Value
                    totalRow("Hours Worked") = totalHours
                    totalRow("Late Hours") = totalLateHours
                    totalRow("Hourly Rate") = DBNull.Value
                    totalRow("Total Salary") = grandTotalSalary
                    dataTable.Rows.Add(totalRow)

                    ' Bind the DataTable to the DataGridView
                    dgHourlyData.DataSource = dataTable

                    ' Customize header text for DataGridView columns
                    dgHourlyData.Columns("Log ID").HeaderText = "Log Identifier"
                    dgHourlyData.Columns("Time In").HeaderText = "Check-In Time"
                    dgHourlyData.Columns("Time Out").HeaderText = "Check-Out Time"
                    dgHourlyData.Columns("Schedule ID").HeaderText = "Schedule ID"
                    dgHourlyData.Columns("Hours Worked").HeaderText = "Total Hours"
                    dgHourlyData.Columns("Late Hours").HeaderText = "Late Hours"
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

    Private Sub btnPrintSalary_Click(sender As Object, e As EventArgs) Handles btnPrintSalary.Click
        Dim connectionString As String = "Server=localhost;Database=new_activitydms;Uid=root;Pwd=;"
        Using connection As New MySqlConnection(connectionString)
            connection.Open()

            ' Fetch the total salary for the day
            Dim query As String = "SELECT SUM(Salary) AS TotalSalary FROM teacherlogs WHERE teacher_ID = @UserID AND DATE(Time_In) = CURDATE()"
            Using command As New MySqlCommand(query, connection)
                command.Parameters.AddWithValue("@UserID", CurrentUserID)
                Dim result As Object = command.ExecuteScalar()
                Dim totalSalary As Decimal = If(result Is DBNull.Value, 0D, Convert.ToDecimal(result))

                ' Calculate deductions as percentages
                Dim taxRate As Decimal = 0.1D ' Example: 10% tax
                Dim sssRate As Decimal = 0.035D ' Example: 3.5% SSS deduction
                Dim pagIbigRate As Decimal = 0.02D ' Example: 2% Pag-IBIG deduction
                Dim philHealthRate As Decimal = 0.015D ' Example: 1.5% PhilHealth deduction

                ' Calculate deductions
                Dim tax As Decimal = totalSalary * taxRate
                Dim sssDeduction As Decimal = totalSalary * sssRate
                Dim pagIbigDeduction As Decimal = totalSalary * pagIbigRate
                Dim philHealthDeduction As Decimal = totalSalary * philHealthRate

                ' Calculate total deductions
                Dim totalDeductions As Decimal = tax + sssDeduction + pagIbigDeduction + philHealthDeduction

                ' Ensure total deductions do not exceed total salary
                If totalDeductions > totalSalary Then
                    totalDeductions = totalSalary
                End If

                ' Calculate net salary
                Dim netSalary As Decimal = totalSalary - totalDeductions

                ' Fetch teacher details
                Dim teacherQuery As String = "SELECT FirstName, LastName FROM users WHERE UserID = @UserID"
                Using teacherCommand As New MySqlCommand(teacherQuery, connection)
                    teacherCommand.Parameters.AddWithValue("@UserID", CurrentUserID)
                    Using reader As MySqlDataReader = teacherCommand.ExecuteReader()
                        If reader.Read() Then
                            Dim firstName As String = reader.GetString("FirstName")
                            Dim lastName As String = reader.GetString("LastName")

                            ' Create a PrintDocument object
                            Dim printDoc As New PrintDocument()
                            AddHandler printDoc.PrintPage, Sub(s, ev)
                                                               ' Define fonts
                                                               Dim headerFont As New Font("Arial", 18, FontStyle.Bold)
                                                               Dim subHeaderFont As New Font("Arial", 14, FontStyle.Bold)
                                                               Dim contentFont As New Font("Arial", 12)
                                                               Dim footerFont As New Font("Arial", 10, FontStyle.Italic)

                                                               ' Define margins and starting positions
                                                               Dim leftMargin As Integer = 50
                                                               Dim topMargin As Integer = 50
                                                               Dim lineHeight As Integer = 30

                                                               ' Draw header
                                                               ev.Graphics.DrawString("SALARY RECEIPT", headerFont, Brushes.Black, leftMargin, topMargin)
                                                               topMargin += lineHeight + 10

                                                               ' Draw sub-header (organization name)
                                                               ev.Graphics.DrawString("XYZ Educational Institution", subHeaderFont, Brushes.Black, leftMargin, topMargin)
                                                               topMargin += lineHeight + 10

                                                               ' Draw teacher details
                                                               ev.Graphics.DrawString($"Teacher: {firstName} {lastName}", contentFont, Brushes.Black, leftMargin, topMargin)
                                                               topMargin += lineHeight
                                                               ev.Graphics.DrawString($"Date: {DateTime.Now.ToShortDateString()}", contentFont, Brushes.Black, leftMargin, topMargin)
                                                               topMargin += lineHeight + 10

                                                               ' Draw salary details with proper formatting
                                                               ev.Graphics.DrawString("Salary Details", subHeaderFont, Brushes.Black, leftMargin, topMargin)
                                                               topMargin += lineHeight
                                                               ev.Graphics.DrawString($"Total Salary: {totalSalary:C2}", contentFont, Brushes.Black, leftMargin, topMargin)
                                                               topMargin += lineHeight
                                                               ev.Graphics.DrawString($"Tax Deducted (10%): {tax:C2}", contentFont, Brushes.Black, leftMargin, topMargin)
                                                               topMargin += lineHeight
                                                               ev.Graphics.DrawString($"SSS Deduction (3.5%): {sssDeduction:C2}", contentFont, Brushes.Black, leftMargin, topMargin)
                                                               topMargin += lineHeight
                                                               ev.Graphics.DrawString($"Pag-IBIG Deduction (2%): {pagIbigDeduction:C2}", contentFont, Brushes.Black, leftMargin, topMargin)
                                                               topMargin += lineHeight
                                                               ev.Graphics.DrawString($"PhilHealth Deduction (1.5%): {philHealthDeduction:C2}", contentFont, Brushes.Black, leftMargin, topMargin)
                                                               topMargin += lineHeight + 10

                                                               ' Draw total deductions and net salary
                                                               ev.Graphics.DrawString($"Total Deductions: {totalDeductions:C2}", contentFont, Brushes.Black, leftMargin, topMargin)
                                                               topMargin += lineHeight
                                                               ev.Graphics.DrawString($"Net Salary: {netSalary:C2}", contentFont, Brushes.Black, leftMargin, topMargin)
                                                               topMargin += lineHeight + 10

                                                               ' Draw footer
                                                               ev.Graphics.DrawString("Thank you for your service!", footerFont, Brushes.Black, leftMargin, topMargin)
                                                           End Sub

                            ' Show the print dialog
                            Dim printDialog As New PrintDialog()
                            printDialog.Document = printDoc
                            If printDialog.ShowDialog() = DialogResult.OK Then
                                printDoc.Print() ' Print the document
                            End If
                        End If
                    End Using
                End Using
            End Using
        End Using
    End Sub

    ' Function to calculate SSS deduction
    Private Function CalculateSSSDeduction(totalSalary As Decimal) As Decimal
        ' Example SSS calculation (adjust based on actual SSS rules)
        If totalSalary <= 10000 Then
            Return 400
        ElseIf totalSalary <= 20000 Then
            Return 800
        Else
            Return 1200
        End If
    End Function

    ' Function to calculate Pag-IBIG deduction
    Private Function CalculatePagIbigDeduction(totalSalary As Decimal) As Decimal
        ' Example Pag-IBIG calculation (adjust based on actual Pag-IBIG rules)
        If totalSalary <= 5000 Then
            Return 100
        Else
            Return 200
        End If
    End Function

    ' Function to calculate PhilHealth deduction
    Private Function CalculatePhilHealthDeduction(totalSalary As Decimal) As Decimal
        ' Example PhilHealth calculation (adjust based on actual PhilHealth rules)
        If totalSalary <= 10000 Then
            Return 300
        ElseIf totalSalary <= 20000 Then
            Return 600
        Else
            Return 900
        End If
    End Function
    Private Function CalculatePhilippineTax(totalSalary As Decimal) As Decimal
        ' Philippine Tax Calculation (Simplified)
        If totalSalary <= 20833 Then
            Return 0
        ElseIf totalSalary <= 33333 Then
            Return (totalSalary - 20833) * 0.2D
        ElseIf totalSalary <= 66667 Then
            Return 2500 + (totalSalary - 33333) * 0.25D
        ElseIf totalSalary <= 166667 Then
            Return 10833 + (totalSalary - 66667) * 0.3D
        ElseIf totalSalary <= 666667 Then
            Return 40833.33 + (totalSalary - 166667) * 0.32D
        Else
            Return 200833.33 + (totalSalary - 666667) * 0.35D
        End If

    End Function
End Class
