Imports MySql.Data.MySqlClient

Public Class DatabaseConnection
    ' Connection string to connect to the new_activitydms MySQL database
    Public Shared ConnectionString As String = "Server=localhost;Database=new_activitydms;Uid=root;Pwd=;"

    ' Method to create and return a MySqlConnection
    Public Shared Function GetConnection() As MySqlConnection
        Dim conn As New MySqlConnection(ConnectionString)
        Try
            conn.Open() ' Ensure the connection is opened before it's returned
        Catch ex As Exception
            MessageBox.Show("Error opening database connection: " & ex.Message)
            Return Nothing
        End Try
        Return conn
    End Function

    ' Method to close the connection
    Public Shared Sub CloseConnection(conn As MySqlConnection)
        If conn IsNot Nothing AndAlso conn.State = ConnectionState.Open Then
            conn.Close() ' Close the connection after operation is complete
        End If
    End Sub
End Class
