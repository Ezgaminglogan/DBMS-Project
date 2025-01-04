Imports MySql.Data.MySqlClient

Public Class frmSection
    Private Sub frmSection_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadSections()
    End Sub

    Private Sub LoadSections()
        Dim connectionString As String = "Server=localhost;Database=new_activitydms;Uid=root;Pwd=;"
        Using connection As New MySqlConnection(connectionString)
            Try
                connection.Open()
                Dim query As String = "SELECT Section_identifier, Capacity FROM Section"
                Using command As New MySqlCommand(query, connection)
                    Dim adapter As New MySqlDataAdapter(command)
                    Dim dt As New DataTable()
                    adapter.Fill(dt)

                    ' Bind the DataTable to the DataGridView
                    DataGridView1.DataSource = dt
                End Using
            Catch ex As Exception
                MessageBox.Show("Error loading sections: " & ex.Message)
            End Try
        End Using
    End Sub

    Private Sub btnAddSection_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim sectionId As String = txtSectionIdentifier.Text.Trim()
        Dim capacity As Integer

        If String.IsNullOrEmpty(sectionId) OrElse Not Integer.TryParse(txtCapacity.Text, capacity) Then
            MessageBox.Show("Please enter valid section identifier and capacity.")
            Return
        End If

        ' Check if the section already exists
        Dim connectionString As String = "Server=localhost;Database=new_activitydms;Uid=root;Pwd=;"
        Using connection As New MySqlConnection(connectionString)
            Try
                connection.Open()

                ' Check if the section identifier already exists in the database
                Dim checkQuery As String = "SELECT COUNT(*) FROM Section WHERE Section_identifier = @SectionId"
                Using checkCommand As New MySqlCommand(checkQuery, connection)
                    checkCommand.Parameters.AddWithValue("@SectionId", sectionId)
                    Dim sectionCount As Integer = Convert.ToInt32(checkCommand.ExecuteScalar())

                    If sectionCount > 0 Then
                        MessageBox.Show("This section identifier already exists.")
                        Return
                    End If
                End Using

                ' Insert new section
                Dim query As String = "INSERT INTO Section (Section_identifier, Capacity) VALUES (@SectionId, @Capacity)"
                Using command As New MySqlCommand(query, connection)
                    command.Parameters.AddWithValue("@SectionId", sectionId)
                    command.Parameters.AddWithValue("@Capacity", capacity)
                    command.ExecuteNonQuery()
                End Using

                MessageBox.Show("Section added successfully!")
                LoadSections() ' Refresh the DataGridView
            Catch ex As Exception
                MessageBox.Show("Error adding section: " & ex.Message)
            End Try
        End Using
    End Sub

    Private Sub btnEditSection_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        Dim selectedRow As DataGridViewRow = DataGridView1.CurrentRow
        If selectedRow IsNot Nothing Then
            Dim sectionId As String = selectedRow.Cells("Section_identifier").Value.ToString()
            Dim newCapacity As Integer

            If Not Integer.TryParse(txtCapacity.Text, newCapacity) Then
                MessageBox.Show("Please enter a valid capacity.")
                Return
            End If

            Dim connectionString As String = "Server=localhost;Database=new_activitydms;Uid=root;Pwd=;"
            Using connection As New MySqlConnection(connectionString)
                Try
                    connection.Open()
                    Dim query As String = "UPDATE Section SET Capacity = @Capacity WHERE Section_identifier = @SectionId"
                    Using command As New MySqlCommand(query, connection)
                        command.Parameters.AddWithValue("@SectionId", sectionId)
                        command.Parameters.AddWithValue("@Capacity", newCapacity)
                        command.ExecuteNonQuery()
                    End Using

                    MessageBox.Show("Section updated successfully!")
                    LoadSections() ' Refresh the DataGridView
                Catch ex As Exception
                    MessageBox.Show("Error updating section: " & ex.Message)
                End Try
            End Using
        Else
            MessageBox.Show("Please select a section to edit.")
        End If
    End Sub

    Private Sub btnDeleteSection_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim selectedRow As DataGridViewRow = DataGridView1.CurrentRow
        If selectedRow IsNot Nothing Then
            Dim sectionId As String = selectedRow.Cells("Section_identifier").Value.ToString()

            Dim result As DialogResult = MessageBox.Show("Are you sure you want to delete this section?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            If result = DialogResult.Yes Then
                Dim connectionString As String = "Server=localhost;Database=new_activitydms;Uid=root;Pwd=;"
                Using connection As New MySqlConnection(connectionString)
                    Try
                        connection.Open()
                        Dim query As String = "DELETE FROM Section WHERE Section_identifier = @SectionId"
                        Using command As New MySqlCommand(query, connection)
                            command.Parameters.AddWithValue("@SectionId", sectionId)
                            command.ExecuteNonQuery()
                        End Using

                        MessageBox.Show("Section deleted successfully!")
                        LoadSections() ' Refresh the DataGridView
                    Catch ex As Exception
                        MessageBox.Show("Error deleting section: " & ex.Message)
                    End Try
                End Using
            End If
        Else
            MessageBox.Show("Please select a section to delete.")
        End If
    End Sub



End Class
