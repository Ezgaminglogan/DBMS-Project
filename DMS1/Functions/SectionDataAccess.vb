Imports MySql.Data.MySqlClient
Imports System.IO

Public Class SectionDataAccess
    ' Updated connection string for the new database
    Private connectionString As String = "Server=localhost;Database=new_activitydms;Uid=root;Pwd=;"

    ' Method to get all sections
    Public Function GetAllSections() As List(Of String)
        Dim sections As New List(Of String)()

        Using connection As New MySqlConnection(connectionString)
            Dim command As New MySqlCommand("SELECT Section_identifier FROM Section", connection)

            Try
                connection.Open()
                Using reader As MySqlDataReader = command.ExecuteReader()
                    While reader.Read()
                        sections.Add(reader("Section_identifier").ToString())
                    End While
                End Using
            Catch ex As MySqlException
                MessageBox.Show("Database error: " & ex.Message)
                LogError(ex)
            Catch ex As Exception
                MessageBox.Show("Unexpected error: " & ex.Message)
                LogError(ex)
            End Try
        End Using

        Return sections
    End Function

    ' Method to get the capacity of a section
    Public Function GetSectionCapacity(sectionIdentifier As String) As Integer
        Dim capacity As Integer = 0

        Using connection As New MySqlConnection(connectionString)
            Dim command As New MySqlCommand("SELECT Capacity FROM Section WHERE Section_identifier = @SectionIdentifier", connection)
            command.Parameters.AddWithValue("@SectionIdentifier", sectionIdentifier)

            Try
                connection.Open()
                Dim result = command.ExecuteScalar()
                If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                    capacity = Convert.ToInt32(result)
                Else
                    capacity = 0 ' Default to 0 if NULL or invalid
                End If
            Catch ex As MySqlException
                MessageBox.Show("Database error: " & ex.Message)
                LogError(ex)
            Catch ex As Exception
                MessageBox.Show("Unexpected error: " & ex.Message)
                LogError(ex)
            End Try
        End Using

        Return capacity
    End Function

    ' Method to get the current enrollment count for a specific section
    Public Function GetCurrentEnrollmentCount(sectionIdentifier As String) As Integer
        Dim count As Integer = 0

        Using connection As New MySqlConnection(connectionString)
            Dim command As New MySqlCommand("SELECT COUNT(*) FROM Enrollment WHERE SectionIdentifier = @SectionIdentifier", connection)
            command.Parameters.AddWithValue("@SectionIdentifier", sectionIdentifier)

            Try
                connection.Open()
                count = Convert.ToInt32(command.ExecuteScalar())
            Catch ex As MySqlException
                MessageBox.Show("Database error: " & ex.Message)
                LogError(ex)
            Catch ex As Exception
                MessageBox.Show("Unexpected error: " & ex.Message)
                LogError(ex)
            End Try
        End Using

        Return count
    End Function

    ' Method to update section capacity
    Public Sub UpdateSectionCapacity(sectionIdentifier As String, newCapacity As Integer)
        Using connection As New MySqlConnection(connectionString)
            Dim command As New MySqlCommand("UPDATE Section SET Capacity = @NewCapacity WHERE Section_identifier = @SectionIdentifier", connection)
            command.Parameters.AddWithValue("@NewCapacity", newCapacity)
            command.Parameters.AddWithValue("@SectionIdentifier", sectionIdentifier)

            Try
                connection.Open()
                command.ExecuteNonQuery()
            Catch ex As MySqlException
                MessageBox.Show("Database error: " & ex.Message)
                LogError(ex)
            Catch ex As Exception
                MessageBox.Show("Unexpected error: " & ex.Message)
                LogError(ex)
            End Try
        End Using
    End Sub

    ' Method to check if enrollment can occur in a section
    Public Function CanEnrollInSection(sectionIdentifier As String, maxCapacity As Integer) As Boolean
        Dim currentCount As Integer = GetCurrentEnrollmentCount(sectionIdentifier)

        ' Debugging output to check the values
        MessageBox.Show("Current Enrollment Count: " & currentCount.ToString())
        MessageBox.Show("Max Capacity: " & maxCapacity.ToString())

        Return currentCount < maxCapacity
    End Function

    ' Log error details to a file for better traceability
    Private Sub LogError(ex As Exception)
        ' Change the file path if required, especially for deployment environments
        Dim filePath As String = "C:\error_log.txt"

        ' Ensure the log file exists or create it
        If Not File.Exists(filePath) Then
            File.Create(filePath).Dispose()
        End If

        ' Log the error message and stack trace
        Using writer As New StreamWriter(filePath, True)
            writer.WriteLine($"{DateTime.Now}: {ex.Message}")
            writer.WriteLine(ex.StackTrace)
            writer.WriteLine("--------------------------------------------------")
        End Using
    End Sub
End Class
