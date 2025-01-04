Public Class StudentItem
    Public Property FullName As String
    Public Property UserID As String

    ' Constructor to initialize the FullName and StudentNumber properties
    Public Sub New(fullName As String, userID As String)
        Me.FullName = fullName
        Me.UserID = userID
    End Sub

    ' Override ToString to display the FullName in the ComboBox
    Public Overrides Function ToString() As String
        Return FullName
    End Function
End Class
