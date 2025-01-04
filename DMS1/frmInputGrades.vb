Imports Microsoft.VisualBasic.ApplicationServices

Public Class frmInputGrades
    Private Sub frmInputGrades_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PopulateStudents()
        PopulateCourses()
    End Sub

    Private Sub PopulateStudents()
        ' Fetch students from the database and add them to cmbStudents
        Dim userDataAccess As New UserDataAccess()
        Dim users As List(Of User) = userDataAccess.GetAllUsers() ' Returns a list of User objects

        MessageBox.Show($"Fetched {users.Count} students.", "Debug", MessageBoxButtons.OK, MessageBoxIcon.Information)

        cmbStudents.Items.Clear()

        For Each user As User In users
            ' Create a UserItem with FullName (concatenated) and Student_number and add it to cmbStudents
            Dim fullName As String = user.FirstName & " " & user.LastName
            Dim userItem As New UserItem(fullName, user.UserID)
            cmbStudents.Items.Add(userItem)
        Next
    End Sub

    Private Sub PopulateCourses()
        ' Fetch courses from the database and add them to cmbCourses
        Dim courseDataAccess As New CourseDataAccess()
        Dim courses As List(Of String) = courseDataAccess.GetAllCourses()

        MessageBox.Show($"Fetched {courses.Count} courses.", "Debug", MessageBoxButtons.OK, MessageBoxIcon.Information)

        cmbCourses.Items.Clear()
        For Each course As String In courses
            cmbCourses.Items.Add(course)
        Next
    End Sub

    Private Sub btnSubmitGrade_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        ' Get selected student and course
        Dim selectedUserItem As UserItem = TryCast(cmbStudents.SelectedItem, UserItem)
        Dim selectedCourse As String = cmbCourses.SelectedItem?.ToString()
        Dim grade As String = txtGrade.Text.Trim() ' Assuming there's a textbox for grade input

        ' Validation
        If selectedUserItem Is Nothing OrElse String.IsNullOrEmpty(selectedCourse) OrElse String.IsNullOrEmpty(grade) Then
            MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        ' Validate grade value (e.g., A, B, C, D, F)
        Dim validGrades As HashSet(Of String) = New HashSet(Of String)({"A", "B", "C", "D", "F"})
        If Not validGrades.Contains(grade.ToUpper()) Then
            MessageBox.Show("Please enter a valid grade (A, B, C, D, F).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        ' Get the StudentNumber from the selected user item
        Dim userID As String = selectedUserItem.UserID

        ' Insert the grade into the database
        Dim gradeDataAccess As New GradeDataAccess()
        Try
            gradeDataAccess.InsertGrade(userID, selectedCourse, grade.ToUpper()) ' Store grade as uppercase
            MessageBox.Show("Grade submitted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("An error occurred while submitting the grade: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub
End Class