Public Class frmAdmin
    Private Sub btnManageSchedule_Click(sender As Object, e As EventArgs) Handles btnManageSchedule.Click
        switchPanel(frmManageSchedule)
        'Dim manageScheduleForm As New frmManageSchedule()
        'manageScheduleForm.Show()
    End Sub

    Sub switchPanel(ByVal panel As Form)
        panelDash.Controls.Clear()
        panel.TopLevel = False
        panelDash.Controls.Add(panel)
        panel.Show()
    End Sub

    Private Sub TabPage1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub btnStudent_Click(sender As Object, e As EventArgs) Handles btnStudent.Click

        switchPanel(frmManageStudents)
        'Dim manageStudentsForm As New frmManageStudents()
        'manageStudentsForm.ShowDialog()
    End Sub

    Private Sub btnTeachers_Click(sender As Object, e As EventArgs) Handles btnTeachers.Click
        switchPanel(frmManageTeacher)
        'Dim manageTeachersForm As New frmManageTeacher()
        'manageTeachersForm.ShowDialog()
    End Sub

    Private Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
        frmLogin1.Show()
        Me.Close()
    End Sub

    Private Sub btnSchedule_Click(sender As Object, e As EventArgs) Handles btnSchedule.Click
        switchPanel(frmManageSubjects)
    End Sub

    Private Sub btnSection_Click(sender As Object, e As EventArgs) Handles btnSection.Click
        switchPanel(frmSection)
    End Sub

    Private Sub btnRoomManage_Click(sender As Object, e As EventArgs) Handles btnRoomManage.Click
        frmRoom.ShowDialog()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        switchPanel(frmAdminMTSalary)
    End Sub
End Class