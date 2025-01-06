Imports MySql.Data.MySqlClient

Public Class frmManageSchedule
    Private scheduleDataAccess As New ScheduleDataAccess()

    ' Class to represent ComboBox items with Text and Value properties
    Public Class ComboBoxItem
        Public Property Text As String
        Public Property Value As Object

        Public Sub New(text As String, value As Object)
            Me.Text = text
            Me.Value = value
        End Sub

        Public Overrides Function ToString() As String
            Return Text
        End Function
    End Class

    Private Sub frmManageSchedule_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadUsers()  ' Updated to load both teachers and students
        LoadCourses()
        LoadDays() ' Load days into the ComboBox
        LoadSchedules()
        LoadRooms()
        LoadSections()
    End Sub

    Private Sub LoadSections()
        ' Clear existing items before loading new data
        cmbSections.Items.Clear()

        Dim connectionString As String = "Server=localhost;Database=new_activitydms;Uid=root;Pwd=;"
        Using connection As New MySqlConnection(connectionString)
            Try
                connection.Open()
                ' Query to select section identifiers from the section table
                Dim query As String = "SELECT Section_identifier FROM section"
                Using command As New MySqlCommand(query, connection)
                    Using reader As MySqlDataReader = command.ExecuteReader()
                        While reader.Read()
                            cmbSections.Items.Add(New ComboBoxItem(reader("Section_identifier").ToString(), reader("Section_identifier"))) ' Add section to ComboBox
                        End While
                    End Using
                End Using
            Catch ex As Exception
                MessageBox.Show("Error loading sections: " & ex.Message)
            End Try
        End Using
    End Sub


    Private Sub LoadRooms()
        Dim connectionString As String = "Server=localhost;Database=new_activitydms;Uid=root;Pwd=;"
        Using connection As New MySqlConnection(connectionString)
            Try
                connection.Open()
                ' Query for RoomNumber from the rooms table
                Dim query As String = "SELECT RoomNumber FROM room"
                Using command As New MySqlCommand(query, connection)
                    Using reader As MySqlDataReader = command.ExecuteReader()
                        While reader.Read()
                            cmbRooms.Items.Add(New ComboBoxItem(reader("RoomNumber").ToString(), reader("RoomNumber"))) ' Use RoomNumber as value
                        End While
                    End Using
                End Using
            Catch ex As Exception
                MessageBox.Show("Error loading rooms: " & ex.Message)
            End Try
        End Using
    End Sub

    ' Load users (teachers and students) into the ComboBox
    Private Sub LoadUsers()
        Dim connectionString As String = "Server=localhost;Database=new_activitydms;Uid=root;Pwd=;"
        Using connection As New MySqlConnection(connectionString)
            Try
                connection.Open()
                ' Query for users, including both teachers and students (assuming Role field exists in users table)
                Dim query As String = "SELECT UserID, CONCAT(FirstName, ' ', LastName) AS FullName FROM users WHERE Role IN ('Teacher')"
                Using command As New MySqlCommand(query, connection)
                    Using reader As MySqlDataReader = command.ExecuteReader()
                        While reader.Read()
                            cmbUsers.Items.Add(New ComboBoxItem(reader("FullName").ToString(), reader("UserID"))) ' Use UserID from users table as value
                        End While
                    End Using
                End Using
            Catch ex As Exception
                MessageBox.Show("Error loading users: " & ex.Message)
            End Try
        End Using
    End Sub

    Private Sub LoadCourses()
        ' Clear existing items before loading new data
        cmbCourses.Items.Clear()

        Dim connectionString As String = "Server=localhost;Database=new_activitydms;Uid=root;Pwd=;"
        Using connection As New MySqlConnection(connectionString)
            Try
                connection.Open()
                Dim query As String = "SELECT Course_number, Course_name FROM course"
                Using command As New MySqlCommand(query, connection)
                    Using reader As MySqlDataReader = command.ExecuteReader()
                        While reader.Read()
                            cmbCourses.Items.Add(New ComboBoxItem(reader("Course_name").ToString(), reader("Course_number")))
                        End While
                    End Using
                End Using
            Catch ex As Exception
                MessageBox.Show("Error loading courses: " & ex.Message)
            End Try
        End Using
    End Sub

    Private Sub LoadDays()
        ' Load days of the week into cmbDays
        Dim days As String() = {"Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"}
        cmbDays.Items.AddRange(days)
    End Sub

    Private Sub LoadSchedules()
        ' Updated query to use UserID for both Teacher and Student
        Dim query As String = "SELECT s.ScheduleID, s.TeacherID, u.FirstName, u.LastName, s.CourseNumber, s.DayOfWeek, s.Time, s.RoomNumber, s.Section " &
                          "FROM schedule s " &
                          "JOIN users u ON s.TeacherID = u.UserID"
        Dim dt As New DataTable()

        Using connection As New MySqlConnection("Server=localhost;Database=new_activitydms;Uid=root;Pwd=;")
            Using command As New MySqlCommand(query, connection)
                Using adapter As New MySqlDataAdapter(command)
                    connection.Open()
                    adapter.Fill(dt)
                End Using
            End Using
        End Using

        ' Bind the DataTable to the DataGridView
        DataGridView1.DataSource = dt
    End Sub
    ' Add schedule button click
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If cmbUsers.SelectedItem IsNot Nothing AndAlso
       cmbCourses.SelectedItem IsNot Nothing AndAlso
       Not String.IsNullOrEmpty(txtTime.Text) AndAlso
       cmbDays.SelectedItem IsNot Nothing AndAlso
       cmbRooms.SelectedItem IsNot Nothing AndAlso
       cmbSections.SelectedItem IsNot Nothing Then

            Dim selectedUser As Integer = CType(cmbUsers.SelectedItem, ComboBoxItem).Value
            Dim selectedCourse As String = CType(cmbCourses.SelectedItem, ComboBoxItem).Value
            Dim day As String = cmbDays.SelectedItem.ToString()
            Dim time As String = txtTime.Text
            Dim selectedRoom As String = cmbRooms.SelectedItem.ToString()
            Dim selectedSection As String = CType(cmbSections.SelectedItem, ComboBoxItem).Value

            ' Check for conflicts
            If scheduleDataAccess.CheckScheduleConflict(day, time, selectedRoom, selectedSection, selectedCourse) Then
                MessageBox.Show("Conflict detected: Cannot add schedule due to an overlap.", "Conflict", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Else
                ' Add schedule if no conflict
                scheduleDataAccess.AddSchedule(selectedUser, selectedCourse, day, time, selectedRoom, selectedSection)
                LoadSchedules()
                MessageBox.Show("Schedule added successfully.")
            End If
        Else
            MessageBox.Show("Please fill out all required fields.")
        End If
    End Sub


    ' Update schedule button click
    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        If DataGridView1.SelectedRows.Count > 0 Then
            ' Get the ScheduleID
            Dim scheduleID As Integer = Convert.ToInt32(DataGridView1.SelectedRows(0).Cells("ScheduleID").Value)

            ' Get UserID from ComboBox (this will be either teacher or student)
            Dim selectedUser As Integer = CType(cmbUsers.SelectedItem, ComboBoxItem).Value
            Dim selectedCourse As String = CType(cmbCourses.SelectedItem, ComboBoxItem).Value
            Dim day As String = cmbDays.SelectedItem.ToString()
            Dim time As String = txtTime.Text
            Dim selectedRoom As String = cmbRooms.SelectedItem.ToString()
            Dim selectedSection As String = CType(cmbSections.SelectedItem, ComboBoxItem).Value ' Get the selected section

            ' Call UpdateSchedule method with the updated UserID
            scheduleDataAccess.UpdateSchedule(scheduleID, selectedUser, selectedCourse, day, time, selectedRoom, selectedSection)
            LoadSchedules()
            MessageBox.Show("Schedule updated successfully.")
        Else
            MessageBox.Show("Please select a schedule to update.")
        End If
    End Sub

    ' Delete schedule button click
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If DataGridView1.SelectedRows.Count > 0 Then
            Dim scheduleID As Integer = Convert.ToInt32(DataGridView1.SelectedRows(0).Cells("ScheduleID").Value)

            scheduleDataAccess.DeleteSchedule(scheduleID)
            LoadSchedules()
            MessageBox.Show("Schedule deleted successfully.")
        Else
            MessageBox.Show("Please select a schedule to delete.")
        End If
    End Sub

    ' Handle row selection in DataGridView
    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If DataGridView1.SelectedRows.Count > 0 Then
            Dim selectedRow As DataGridViewRow = DataGridView1.SelectedRows(0)
            cmbUsers.SelectedItem = cmbUsers.Items.Cast(Of ComboBoxItem)().FirstOrDefault(Function(x) x.Value = selectedRow.Cells("TeacherID").Value) ' Use UserID here
            cmbCourses.SelectedItem = cmbCourses.Items.Cast(Of ComboBoxItem)().FirstOrDefault(Function(x) x.Value = selectedRow.Cells("CourseNumber").Value)
            cmbDays.SelectedItem = selectedRow.Cells("DayOfWeek").Value.ToString()
            cmbRooms.SelectedItem = selectedRow.Cells("RoomNumber").Value.ToString()
            cmbSections.SelectedItem = cmbSections.Items.Cast(Of ComboBoxItem)().FirstOrDefault(Function(x) x.Value = selectedRow.Cells("Section").Value) ' Set selected section
            txtTime.Text = selectedRow.Cells("Time").Value.ToString()
        End If
    End Sub

End Class
