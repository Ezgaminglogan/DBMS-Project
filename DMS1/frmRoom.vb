Imports MySql.Data.MySqlClient
Public Class frmRoom

    Dim conn As MySqlConnection = DatabaseConnection.GetConnection

    Private Sub tbRoomNumber_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tbRoomNumber.KeyPress
        ' Check if the pressed key is not a number and is not a control (like backspace)
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True ' Cancel the key press
        End If
    End Sub

    Private Sub frmRoom_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        LoadRoom()
    End Sub

    Private Sub btnCreate_Click(sender As Object, e As EventArgs) Handles btnCreate.Click
        ' Ensure the room number textbox is not empty
        If String.IsNullOrWhiteSpace(tbRoomNumber.Text) Then
            MessageBox.Show("Please enter a valid room number.", "Room", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            ' Ensure the database connection is open
            If conn.State <> ConnectionState.Open Then
                conn.Open()

                ' SQL query to insert room number
                Dim query = "INSERT INTO `room`(RoomNumber) VALUES(@RoomNumber)"

                Using cmd As New MySqlCommand(query, conn)
                    ' Add parameter for the room number
                    cmd.Parameters.AddWithValue("@RoomNumber", tbRoomNumber.Text)

                    ' Execute the query and check how many rows were affected
                    Dim row As Integer = cmd.ExecuteNonQuery()

                    ' Check if the insert was successful
                    If row > 0 Then
                        MessageBox.Show("Room Inserted Successfully", "Room", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else
                        MessageBox.Show("Error: Something went wrong.", "Room", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                End Using
            Else
                MessageBox.Show("Database connection is not open.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            ' Handle any exceptions (e.g., database connection issues)
            MessageBox.Show("Error inserting room: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ' Ensure to refresh the room list after inserting the new room
            LoadRoom()
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        ' Ensure the room number textbox is not empty
        If String.IsNullOrWhiteSpace(tbRoomNumber.Text) Then
            MessageBox.Show("Please enter a valid room number to delete.", "Room", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Ask for confirmation before deleting the room
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to delete this room?", "Delete Room", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
        If result = DialogResult.No Then
            Return ' If the user cancels, do nothing
        End If

        Try
            ' Ensure the database connection is open
            If conn.State <> ConnectionState.Open Then
                conn.Open()
            End If

            ' SQL query to delete room based on RoomNumber
            Dim query As String = "DELETE FROM `room` WHERE RoomNumber = @RoomNumber"

            Using cmd As New MySqlCommand(query, conn)
                ' Add parameter for the room number
                cmd.Parameters.AddWithValue("@RoomNumber", tbRoomNumber.Text)

                ' Execute the query and check how many rows were affected
                Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

                ' Check if the delete was successful
                If rowsAffected > 0 Then
                    MessageBox.Show("Room deleted successfully.", "Room", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    MessageBox.Show("Room not found or could not be deleted.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End Using
        Catch ex As Exception
            ' Handle any exceptions (e.g., database connection issues)
            MessageBox.Show("Error deleting room: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ' Ensure to refresh the room list after deleting the room
            LoadRoom()
        End Try
    End Sub



    Public Sub LoadRoom()
        Try
            If conn IsNot Nothing AndAlso conn.State = ConnectionState.Open Then
                Dim querys = "SELECT * FROM `room`"
                Using cmd As New MySqlCommand(querys, conn)
                    Using adapter As New MySqlDataAdapter(cmd)
                        Dim dt As New DataTable
                        adapter.Fill(dt)
                        dgRoom.DataSource = dt
                        dgRoom.Columns("RoomNumber").HeaderText = "Room Number"
                    End Using
                End Using
            End If
        Catch ex As Exception
            MessageBox.Show("Error loading rooms: " & ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            DatabaseConnection.CloseConnection(conn)
        End Try
    End Sub
End Class