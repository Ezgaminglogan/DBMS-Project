Imports MySql.Data.MySqlClient

Public Class StudentDashboard
    ' This property will be set when the user (either student or teacher) logs in
    Public Property CurrentUserID As Integer

    Private Sub StudentDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Refresh user data when the form loads
        RefreshData()
    End Sub

    ' Method to refresh user data
    Public Sub RefreshData()
        LoadUserEmail() ' Refresh the email label
        ' You can add more calls to other refresh methods if needed
    End Sub

    ' Method to load user's email from the 'users' table
    Private Sub LoadUserEmail()
        Dim connectionString As String = "Server=localhost;Database=new_activitydms;Uid=root;Pwd=;"
        Using connection As New MySqlConnection(connectionString)
            Try
                connection.Open()

                ' Query the 'users' table for the user's email (for both student and teacher)
                Dim query As String = "SELECT Email , UserID FROM users WHERE UserID = @UserID"
                Using command As New MySqlCommand(query, connection)
                    command.Parameters.AddWithValue("@UserID", CurrentUserID)

                    ' Execute the query and get the result
                    Dim email As Object = command.ExecuteScalar()
                    If email IsNot Nothing Then
                        lblEmail.Text = email.ToString() ' Set the label text to the email
                        lblID.Text = CurrentUserID.ToString()
                    Else
                        MessageBox.Show("No email and ID found for UserID: " & CurrentUserID)
                        lblEmail.Text = "Email not found"
                        lblID.Text = "ID not Found!"
                    End If
                End Using
            Catch ex As Exception
                ' Handle any errors and show them in a MessageBox
                MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

    ' Method to switch between forms dynamically inside Panel2
    Sub switchPanel(ByVal panel As Form)
        Panel2.Controls.Clear()
        panel.TopLevel = False
        Panel2.Controls.Add(panel)
        panel.Show()
    End Sub

    ' Event for Enrollment form button click (this could be for student enrollment)
    Private Sub btnEForm_Click(sender As Object, e As EventArgs) Handles btnEForm.Click
        frmEnrollment.CurrentUserID = CurrentUserID
        switchPanel(frmEnrollment)
    End Sub

    ' Event for Course details button click (this could be for both students and teachers)
    Private Sub btnCourse_Click(sender As Object, e As EventArgs)
        switchPanel(frmCourseDetails)
    End Sub

    ' Event for Study button click (load study-related data form)
    Private Sub btnStudy_Click(sender As Object, e As EventArgs) Handles btnStudy.Click
        ' Create a new instance of frmLoadData
        Dim loadDataForm As New frmLoadData()

        ' Pass the current user ID (Student or Teacher) to frmLoadData
        loadDataForm.UserID = CurrentUserID

        ' Use switchPanel to display frmLoadData
        switchPanel(loadDataForm)
    End Sub

    ' Event for Logout button click
    Private Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
        frmLogin1.Show() ' Show login form
        Me.Close() ' Close the current form
    End Sub

End Class
