Imports MySql.Data.MySqlClient

Public Class frmCourseDetails
    Public Property CurrentStudentNumber As String
    Private Sub frmCourseDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadCourseData()
    End Sub

    Private Sub LoadCourseData()
        Dim table As New DataTable()

        ' Connection string (make sure to replace with your actual connection details)
        Dim connectionString As String = "Server=localhost;Database=new_activitydms;User ID=root;Password=;"

        Using conn As New MySqlConnection(connectionString)
            Try
                conn.Open() ' Open the connection

                ' Update the SQL query to use Instructor from the Course table and include the Year
                Using cmd As New MySqlCommand("SELECT c.Course_number, c.Course_name, c.Credit_hours, c.Department, c.Year, c.Instructor, p.PrerequisiteCourse_number 
                                          FROM Course c 
                                          LEFT JOIN Prerequisite p ON c.Course_number = p.Course_number", conn)
                    Using adapter As New MySqlDataAdapter(cmd)
                        adapter.Fill(table) ' Fill the DataTable
                    End Using
                End Using

                ' Clear existing data and bind new data to DataGridView
                DataGridView1.DataSource = Nothing ' Clear existing data
                DataGridView1.DataSource = table ' Bind DataTable to DataGridView

            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message)
            Finally
                ' Ensure the connection is closed
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
            End Try
        End Using
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) 
        ' Get the instance of StudentDashboard
        Dim studentDashboard As StudentDashboard = CType(Application.OpenForms("StudentDashboard"), StudentDashboard)

        If studentDashboard IsNot Nothing Then
            studentDashboard.RefreshData() ' Refresh the data on the StudentDashboard
        Else
            ' If the StudentDashboard is not open, create a new instance
            studentDashboard = New StudentDashboard()
            studentDashboard.CurrentUserID = Me.CurrentStudentNumber ' Use the property from frmCourseDetails
            studentDashboard.Show()
        End If

        Me.Close() ' Close the current form
    End Sub


End Class
