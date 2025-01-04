Imports MySql.Data.MySqlClient

Public Class frmLoadData
    Public Property UserID As String ' Property to hold the UserID instead of StudentNumber

    Private Sub frmLoadData_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Display the UserID to verify it's being passed correctly
        MessageBox.Show("UserID in frmLoadData: " & UserID)
        LoadStudyLoadData() ' Load data when the form is loaded
    End Sub

    Private Sub LoadStudyLoadData()
        Dim conn As MySqlConnection = DatabaseConnection.GetConnection()

        If conn.State <> ConnectionState.Open Then
            conn.Open()
        End If

        ' Simplified query without instructor details
        Dim query As String = "
    SELECT 
        e.EnrollmentID AS 'Enrollment ID',
        c.Course_number AS 'Course Number', 
        c.Course_name AS 'Course Name', 
        c.Credit_hours AS 'Credit Hours', 
        c.Department AS 'Department', 
        e.SectionIdentifier AS 'Section', 
        e.EnrollmentDate AS 'Enrollment Date'
    FROM enrollment e
    JOIN course c ON e.CourseNumber = c.Course_number
    WHERE e.UserID = @UserID;"

        Using cmd As New MySqlCommand(query, conn)
            cmd.Parameters.Add("@UserID", MySqlDbType.Int32).Value = UserID ' Ensure correct parameter type

            Dim adapter As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)

            If dt.Rows.Count = 0 Then
                MessageBox.Show("No study load found for this user.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                DataGridView1.DataSource = dt
                DataGridView1.Refresh()

                ' Optionally, set column headers explicitly if needed
                DataGridView1.Columns("Enrollment ID").HeaderText = "Enrollment ID"
                DataGridView1.Columns("Course Number").HeaderText = "Course Number"
                DataGridView1.Columns("Course Name").HeaderText = "Course Name"
                DataGridView1.Columns("Credit Hours").HeaderText = "Credit Hours"
                DataGridView1.Columns("Department").HeaderText = "Department"
                DataGridView1.Columns("Section").HeaderText = "Section"
                DataGridView1.Columns("Enrollment Date").HeaderText = "Enrollment Date"
            End If
        End Using

        DatabaseConnection.CloseConnection(conn)
    End Sub






End Class
