<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmEnrollment
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEnrollment))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblSelectCourse = New System.Windows.Forms.Label()
        Me.lblSelectSection = New System.Windows.Forms.Label()
        Me.txtFirstName = New System.Windows.Forms.TextBox()
        Me.txtLastName = New System.Windows.Forms.TextBox()
        Me.cmbCourses = New System.Windows.Forms.ComboBox()
        Me.cmbSections = New System.Windows.Forms.ComboBox()
        Me.btnEnroll = New System.Windows.Forms.Button()
        Me.btnEnrollInCourse = New System.Windows.Forms.Button()
        Me.btnReEnroll = New System.Windows.Forms.Button()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.btnRefresh = New System.Windows.Forms.Label()
        Me.txtUserID = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Gold
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Black", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(418, 149)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(377, 40)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Student Enrollment Form"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(100, 224)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(105, 24)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Students ID"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(99, 268)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(122, 25)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "First Name:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(117, 313)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(104, 24)
        Me.Label4.TabIndex = 1
        Me.Label4.Text = "Last Name:"
        '
        'lblSelectCourse
        '
        Me.lblSelectCourse.AutoSize = True
        Me.lblSelectCourse.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSelectCourse.Location = New System.Drawing.Point(583, 222)
        Me.lblSelectCourse.Name = "lblSelectCourse"
        Me.lblSelectCourse.Size = New System.Drawing.Size(133, 24)
        Me.lblSelectCourse.TabIndex = 2
        Me.lblSelectCourse.Text = "Select Course:"
        '
        'lblSelectSection
        '
        Me.lblSelectSection.AutoSize = True
        Me.lblSelectSection.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSelectSection.Location = New System.Drawing.Point(579, 273)
        Me.lblSelectSection.Name = "lblSelectSection"
        Me.lblSelectSection.Size = New System.Drawing.Size(135, 24)
        Me.lblSelectSection.TabIndex = 2
        Me.lblSelectSection.Text = "Select Section:"
        '
        'txtFirstName
        '
        Me.txtFirstName.Enabled = False
        Me.txtFirstName.Location = New System.Drawing.Point(227, 274)
        Me.txtFirstName.Name = "txtFirstName"
        Me.txtFirstName.Size = New System.Drawing.Size(217, 20)
        Me.txtFirstName.TabIndex = 4
        '
        'txtLastName
        '
        Me.txtLastName.Enabled = False
        Me.txtLastName.Location = New System.Drawing.Point(227, 317)
        Me.txtLastName.Name = "txtLastName"
        Me.txtLastName.Size = New System.Drawing.Size(217, 20)
        Me.txtLastName.TabIndex = 5
        '
        'cmbCourses
        '
        Me.cmbCourses.FormattingEnabled = True
        Me.cmbCourses.Location = New System.Drawing.Point(720, 222)
        Me.cmbCourses.Name = "cmbCourses"
        Me.cmbCourses.Size = New System.Drawing.Size(217, 21)
        Me.cmbCourses.TabIndex = 6
        '
        'cmbSections
        '
        Me.cmbSections.FormattingEnabled = True
        Me.cmbSections.Location = New System.Drawing.Point(720, 273)
        Me.cmbSections.Name = "cmbSections"
        Me.cmbSections.Size = New System.Drawing.Size(217, 21)
        Me.cmbSections.TabIndex = 7
        '
        'btnEnroll
        '
        Me.btnEnroll.BackColor = System.Drawing.Color.Coral
        Me.btnEnroll.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red
        Me.btnEnroll.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEnroll.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEnroll.ForeColor = System.Drawing.Color.White
        Me.btnEnroll.Image = Global.DMS1.My.Resources.Resources.icons8_load_from_file_321
        Me.btnEnroll.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnEnroll.Location = New System.Drawing.Point(252, 359)
        Me.btnEnroll.Name = "btnEnroll"
        Me.btnEnroll.Size = New System.Drawing.Size(151, 45)
        Me.btnEnroll.TabIndex = 8
        Me.btnEnroll.Text = "ENROLL"
        Me.btnEnroll.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnEnroll.UseVisualStyleBackColor = False
        '
        'btnEnrollInCourse
        '
        Me.btnEnrollInCourse.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        Me.btnEnrollInCourse.BackColor = System.Drawing.Color.Coral
        Me.btnEnrollInCourse.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEnrollInCourse.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEnrollInCourse.ForeColor = System.Drawing.Color.White
        Me.btnEnrollInCourse.Image = Global.DMS1.My.Resources.Resources.icons8_load_from_file_321
        Me.btnEnrollInCourse.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnEnrollInCourse.Location = New System.Drawing.Point(756, 307)
        Me.btnEnrollInCourse.Name = "btnEnrollInCourse"
        Me.btnEnrollInCourse.Size = New System.Drawing.Size(151, 41)
        Me.btnEnrollInCourse.TabIndex = 11
        Me.btnEnrollInCourse.Text = "ENROLL"
        Me.btnEnrollInCourse.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnEnrollInCourse.UseVisualStyleBackColor = False
        '
        'btnReEnroll
        '
        Me.btnReEnroll.BackColor = System.Drawing.Color.Coral
        Me.btnReEnroll.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnReEnroll.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnReEnroll.Font = New System.Drawing.Font("Segoe UI Black", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReEnroll.ForeColor = System.Drawing.Color.White
        Me.btnReEnroll.Image = Global.DMS1.My.Resources.Resources.icons8_sync_32
        Me.btnReEnroll.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnReEnroll.Location = New System.Drawing.Point(1018, 380)
        Me.btnReEnroll.Name = "btnReEnroll"
        Me.btnReEnroll.Size = New System.Drawing.Size(141, 40)
        Me.btnReEnroll.TabIndex = 12
        Me.btnReEnroll.Text = "Re-Enroll"
        Me.btnReEnroll.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnReEnroll.UseVisualStyleBackColor = False
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Century Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle5
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.DataGridView1.Location = New System.Drawing.Point(0, 426)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RowHeadersVisible = False
        Me.DataGridView1.Size = New System.Drawing.Size(1167, 264)
        Me.DataGridView1.TabIndex = 13
        '
        'btnRefresh
        '
        Me.btnRefresh.AutoSize = True
        Me.btnRefresh.BackColor = System.Drawing.Color.Coral
        Me.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefresh.Font = New System.Drawing.Font("Segoe UI Black", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRefresh.ForeColor = System.Drawing.Color.White
        Me.btnRefresh.Image = Global.DMS1.My.Resources.Resources.icons8_sync_32
        Me.btnRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnRefresh.Location = New System.Drawing.Point(4, 379)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(135, 30)
        Me.btnRefresh.TabIndex = 15
        Me.btnRefresh.Text = "     REFRESH"
        Me.btnRefresh.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtUserID
        '
        Me.txtUserID.Enabled = False
        Me.txtUserID.Location = New System.Drawing.Point(227, 225)
        Me.txtUserID.Name = "txtUserID"
        Me.txtUserID.Size = New System.Drawing.Size(217, 20)
        Me.txtUserID.TabIndex = 3
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.PictureBox2)
        Me.Panel1.Controls.Add(Me.PictureBox1)
        Me.Panel1.Location = New System.Drawing.Point(-3, -2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1366, 148)
        Me.Panel1.TabIndex = 16
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Century", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(487, 91)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(214, 20)
        Me.Label8.TabIndex = 16
        Me.Label8.Text = "Naga Extension Campus"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Century", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(360, 52)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(438, 33)
        Me.Label7.TabIndex = 15
        Me.Label7.Text = "Cebu Technological University"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Century", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(478, 22)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(234, 20)
        Me.Label9.TabIndex = 14
        Me.Label9.Text = "Republic of the Philippines"
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(1021, 5)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(137, 135)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox2.TabIndex = 7
        Me.PictureBox2.TabStop = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(12, 5)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(137, 135)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 6
        Me.PictureBox1.TabStop = False
        '
        'frmEnrollment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Teal
        Me.ClientSize = New System.Drawing.Size(1167, 690)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.btnRefresh)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.btnReEnroll)
        Me.Controls.Add(Me.btnEnrollInCourse)
        Me.Controls.Add(Me.btnEnroll)
        Me.Controls.Add(Me.cmbSections)
        Me.Controls.Add(Me.cmbCourses)
        Me.Controls.Add(Me.txtLastName)
        Me.Controls.Add(Me.txtFirstName)
        Me.Controls.Add(Me.txtUserID)
        Me.Controls.Add(Me.lblSelectSection)
        Me.Controls.Add(Me.lblSelectCourse)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmEnrollment"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Enrollment Form"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents lblSelectCourse As Label
    Friend WithEvents lblSelectSection As Label
    Friend WithEvents txtFirstName As TextBox
    Friend WithEvents txtLastName As TextBox
    Friend WithEvents cmbCourses As ComboBox
    Friend WithEvents cmbSections As ComboBox
    Friend WithEvents btnEnroll As Button
    Friend WithEvents btnEnrollInCourse As Button
    Friend WithEvents btnReEnroll As Button
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents btnRefresh As Label
    Friend WithEvents txtUserID As TextBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label8 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents PictureBox1 As PictureBox
End Class
