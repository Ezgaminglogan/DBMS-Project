Imports MySql.Data.MySqlClient

Public Class frmAdminMTSalary

    Dim conn As MySqlConnection = DatabaseConnection.GetConnection()

    Private Sub frmAdminMTSalary_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadAVG()
    End Sub

    Public Sub LoadAVG()
        Dim Query = "SELECT AVG(Salary) FROM `teacherlogs`"
        Using cmd As New MySqlCommand(Query, conn)
            Dim reader As MySqlDataReader = cmd.ExecuteReader()
            If reader.Read() Then
                If Not IsDBNull(reader(0)) Then
                    ' Check if the value is a valid number
                    Dim avgSalary As Decimal = Convert.ToDecimal(reader(0))
                    lblAVG.Text = avgSalary.ToString("C")
                Else
                    lblAVG.Text = 0
                End If
            End If
            conn.Close()
        End Using
    End Sub


    Private Sub cbTable_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbTable.SelectedIndexChanged
        Select Case cbTable.SelectedItem.ToString()
            Case "Clear Table"
                dgTeachers.DataSource = Nothing
            Case "Get Teachers By Date"
                LoadTeacherByDate(dtpTeachers)
            Case "Teacher Work Hours"
                LoadTeacherWorkHours(tbTeacherID)
            Case "Most Hours Worked"
                LoadMostWorkedTeacher()
            Case "Delete Teacher with Lowest Salary"
                DeleteTeacherWithLowestSalary()
        End Select
    End Sub

    Private Sub dtpTeachers_ValueChanged(sender As Object, e As EventArgs) Handles dtpTeachers.ValueChanged
        ' You can add logic here if needed when date changes
    End Sub

    Private Sub tbTeacherID_TextChanged(sender As Object, e As EventArgs) Handles tbTeacherID.TextChanged
        ' You can add logic here if needed when teacher ID changes
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        DeleteTeacherWithLowestSalary()
    End Sub

    ' Function to retrieve the teacher who worked the most hours
    Public Sub LoadMostWorkedTeacher()
        Dim query As String = "SELECT teacher_ID, SUM(Hours) AS TotalHours FROM teacherlogs GROUP BY teacher_ID ORDER BY TotalHours DESC LIMIT 1"
        Using cmd As New MySqlCommand(query, conn)
            conn.Open()
            Dim reader As MySqlDataReader = cmd.ExecuteReader()
            If reader.Read() Then
                Dim teacherID = reader("teacher_ID").ToString()
                Dim totalHours = reader("TotalHours").ToString()
                ' Display in DataGridView
                Dim dt As New DataTable()
                dt.Columns.Add("Teacher ID")
                dt.Columns.Add("Total Hours")
                dt.Rows.Add(teacherID, totalHours)
                dgTeachers.DataSource = dt
            End If
            conn.Close()
        End Using
    End Sub

    ' Function to delete the teacher with the lowest total salary
    Public Sub DeleteTeacherWithLowestSalary()
        Dim query As String = "SELECT teacher_ID FROM teacherlogs GROUP BY teacher_ID ORDER BY SUM(Salary) ASC LIMIT 1"
        Using cmd As New MySqlCommand(query, conn)
            conn.Open()
            Dim reader As MySqlDataReader = cmd.ExecuteReader()
            If reader.Read() Then
                Dim teacherID = reader("teacher_ID").ToString()
                reader.Close()
                ' Delete the teacher logs first
                Dim deleteQuery As String = "DELETE FROM teacherlogs WHERE teacher_ID = @teacherID"
                Using deleteCmd As New MySqlCommand(deleteQuery, conn)
                    deleteCmd.Parameters.AddWithValue("@teacherID", teacherID)
                    deleteCmd.ExecuteNonQuery()
                End Using
                ' Delete the teacher from the users table
                Dim deleteUserQuery As String = "DELETE FROM users WHERE UserID = @teacherID"
                Using deleteCmd As New MySqlCommand(deleteUserQuery, conn)
                    deleteCmd.Parameters.AddWithValue("@teacherID", teacherID)
                    deleteCmd.ExecuteNonQuery()
                End Using
                MessageBox.Show("Teacher with ID " & teacherID & " and the lowest salary has been deleted.")
                ' Refresh DataGridView after deletion
                LoadTeacherWorkHours(tbTeacherID)
            End If
            conn.Close()
        End Using
    End Sub

    ' Function to list all teachers who worked on a specific date
    Public Sub LoadTeacherByDate(ByVal dtpTeachers As DateTimePicker)
        Dim query As String = "SELECT u.FirstName, u.LastName, t.Hours, t.Time_In, t.Time_Out FROM teacherlogs t JOIN users u ON t.teacher_ID = u.UserID WHERE DATE(t.Time_In) = @selectedDate"
        Using cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@selectedDate", dtpTeachers.Value.Date)
            conn.Open()
            Dim reader As MySqlDataReader = cmd.ExecuteReader()
            Dim dt As New DataTable()
            dt.Columns.Add("First Name")
            dt.Columns.Add("Last Name")
            dt.Columns.Add("Hours Worked")
            dt.Columns.Add("Time In")
            dt.Columns.Add("Time Out")

            While reader.Read()
                dt.Rows.Add(reader("FirstName"), reader("LastName"), reader("Hours"), reader("Time_In"), reader("Time_Out"))
            End While
            dgTeachers.DataSource = dt
            conn.Close()
        End Using
    End Sub

    ' Function to list teacher work hours by teacher ID
    Public Sub LoadTeacherWorkHours(ByVal tbTeacherID As TextBox)
        Dim query As String = "SELECT u.FirstName, u.LastName, SUM(t.Hours) AS TotalHours, SUM(t.Salary) AS TotalSalary FROM teacherlogs t JOIN users u ON t.teacher_ID = u.UserID WHERE t.teacher_ID = @teacherID GROUP BY t.teacher_ID"
        Using cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@teacherID", tbTeacherID.Text)
            conn.Open()
            Dim reader As MySqlDataReader = cmd.ExecuteReader()
            Dim dt As New DataTable()
            dt.Columns.Add("Teacher Name")
            dt.Columns.Add("Total Hours")
            dt.Columns.Add("Total Salary")

            If reader.Read() Then
                Dim teacherName = reader("FirstName") & " " & reader("LastName")
                Dim totalHours = reader("TotalHours").ToString()
                Dim totalSalary As String
                If IsDBNull(reader("TotalSalary")) Then
                    totalSalary = "0"
                Else
                    totalSalary = Convert.ToDecimal(reader("TotalSalary")).ToString("C")
                End If
                dt.Rows.Add(teacherName, totalHours, totalSalary)
            End If
            dgTeachers.DataSource = dt
            conn.Close()
        End Using
    End Sub
End Class
