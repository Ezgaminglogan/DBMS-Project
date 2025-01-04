Imports MySql.Data.MySqlClient

Public Class UserDataAccess

    ' Method to check if the student exists in the database
    Public Function UserExists(userID As String) As Boolean
        Dim conn As MySqlConnection = Nothing
        Try
            conn = DatabaseConnection.GetConnection()
            Dim query As String = "SELECT COUNT(*) FROM users WHERE UserID = @UserID"
            Dim cmd As New MySqlCommand(query, conn)
            cmd.Parameters.Add("@UserID", MySqlDbType.VarChar).Value = userID

            Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
            Return count > 0 ' Returns true if the student exists
        Catch ex As Exception
            ' Log error in production instead of MessageBox
            MessageBox.Show("Error checking user existence: " & ex.Message)
            Return False
        Finally
            If conn IsNot Nothing Then
                DatabaseConnection.CloseConnection(conn)
            End If
        End Try
    End Function

    ' Method to get user details by student number
    Public Function GetUserDetails(userID As String) As User
        Dim conn As MySqlConnection = Nothing
        Try
            conn = DatabaseConnection.GetConnection()
            Dim query As String = "SELECT FirstName, LastName FROM users WHERE UserID = @UserID"
            Dim cmd As New MySqlCommand(query, conn)
            cmd.Parameters.Add("@UserID", MySqlDbType.VarChar).Value = userID

            Using reader As MySqlDataReader = cmd.ExecuteReader()
                If reader.Read() Then
                    Return New User() With {
                        .UserID = userID,
                        .FirstName = reader("FirstName").ToString(),
                        .LastName = reader("LastName").ToString()
                    }
                Else
                    Return Nothing ' User not found
                End If
            End Using
        Catch ex As Exception
            ' Log error in production instead of MessageBox
            MessageBox.Show("Error retrieving user details: " & ex.Message)
            Return Nothing
        Finally
            If conn IsNot Nothing Then
                DatabaseConnection.CloseConnection(conn)
            End If
        End Try
    End Function

    ' Method to get all users (students)
    Public Function GetAllUsers() As List(Of User)
        Dim conn As MySqlConnection = DatabaseConnection.GetConnection()
        Dim users As New List(Of User)()

        If conn Is Nothing Then
            MessageBox.Show("Failed to establish connection.")
            Return users
        End If

        Try
            Dim query As String = "SELECT UserID, FirstName, LastName FROM users WHERE Role = 'Student'"
            Dim cmd As New MySqlCommand(query, conn)

            Using reader As MySqlDataReader = cmd.ExecuteReader()
                While reader.Read()
                    Dim user As New User With {
                        .UserID = reader("UserID").ToString(),
                        .FirstName = reader("FirstName").ToString(),
                        .LastName = reader("LastName").ToString()
                    }
                    users.Add(user)
                End While
            End Using
        Catch ex As Exception
            MessageBox.Show("Error retrieving users: " & ex.Message)
        Finally
            DatabaseConnection.CloseConnection(conn)
        End Try

        Return users
    End Function
End Class
