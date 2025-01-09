<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class TeacherDashboard
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
        Dim DataGridViewCellStyle16 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle17 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle18 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TeacherDashboard))
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.dgSalarySubject = New System.Windows.Forms.DataGridView()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.dgHourlyData = New System.Windows.Forms.DataGridView()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblEmail = New System.Windows.Forms.Label()
        Me.btnPrintSalary = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.btnLogout = New System.Windows.Forms.Button()
        Me.btnSchedule = New System.Windows.Forms.Button()
        Me.btnTimeOut = New System.Windows.Forms.Button()
        Me.btnTimeIn = New System.Windows.Forms.Button()
        Me.btnDash = New System.Windows.Forms.Button()
        Me.btnInputGrades = New System.Windows.Forms.Button()
        Me.Panel2.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.dgSalarySubject, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.dgHourlyData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Teal
        Me.Panel2.Controls.Add(Me.GroupBox2)
        Me.Panel2.Controls.Add(Me.GroupBox1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(167, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1183, 729)
        Me.Panel2.TabIndex = 5
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.dgSalarySubject)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox2.Font = New System.Drawing.Font("Lucida Sans Typewriter", 11.25!)
        Me.GroupBox2.ForeColor = System.Drawing.Color.Black
        Me.GroupBox2.Location = New System.Drawing.Point(0, 363)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(1183, 366)
        Me.GroupBox2.TabIndex = 0
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Salary Subject"
        '
        'dgSalarySubject
        '
        Me.dgSalarySubject.AllowUserToAddRows = False
        Me.dgSalarySubject.AllowUserToDeleteRows = False
        Me.dgSalarySubject.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgSalarySubject.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgSalarySubject.BackgroundColor = System.Drawing.Color.White
        Me.dgSalarySubject.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        DataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle16.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle16.Font = New System.Drawing.Font("Lucida Sans Typewriter", 11.25!)
        DataGridViewCellStyle16.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle16.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgSalarySubject.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle16
        Me.dgSalarySubject.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgSalarySubject.Location = New System.Drawing.Point(6, 24)
        Me.dgSalarySubject.Name = "dgSalarySubject"
        Me.dgSalarySubject.ReadOnly = True
        Me.dgSalarySubject.Size = New System.Drawing.Size(1171, 336)
        Me.dgSalarySubject.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.dgHourlyData)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox1.Font = New System.Drawing.Font("Lucida Sans Typewriter", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.Black
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1183, 366)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Hourly Data"
        '
        'dgHourlyData
        '
        Me.dgHourlyData.AllowUserToAddRows = False
        Me.dgHourlyData.AllowUserToDeleteRows = False
        Me.dgHourlyData.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgHourlyData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgHourlyData.BackgroundColor = System.Drawing.Color.White
        Me.dgHourlyData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        DataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle17.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle17.Font = New System.Drawing.Font("Lucida Sans Typewriter", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle17.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        DataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgHourlyData.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle17
        Me.dgHourlyData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgHourlyData.GridColor = System.Drawing.Color.CadetBlue
        Me.dgHourlyData.Location = New System.Drawing.Point(6, 24)
        Me.dgHourlyData.Name = "dgHourlyData"
        Me.dgHourlyData.ReadOnly = True
        DataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle18.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        DataGridViewCellStyle18.Font = New System.Drawing.Font("Lucida Sans Typewriter", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle18.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle18.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle18.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgHourlyData.RowHeadersDefaultCellStyle = DataGridViewCellStyle18
        Me.dgHourlyData.Size = New System.Drawing.Size(1171, 336)
        Me.dgHourlyData.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.DimGray
        Me.Panel1.Controls.Add(Me.btnPrintSalary)
        Me.Panel1.Controls.Add(Me.lblEmail)
        Me.Panel1.Controls.Add(Me.PictureBox1)
        Me.Panel1.Controls.Add(Me.btnLogout)
        Me.Panel1.Controls.Add(Me.btnSchedule)
        Me.Panel1.Controls.Add(Me.btnTimeOut)
        Me.Panel1.Controls.Add(Me.btnTimeIn)
        Me.Panel1.Controls.Add(Me.btnDash)
        Me.Panel1.Controls.Add(Me.btnInputGrades)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(167, 729)
        Me.Panel1.TabIndex = 4
        '
        'lblEmail
        '
        Me.lblEmail.AutoSize = True
        Me.lblEmail.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEmail.Location = New System.Drawing.Point(3, 136)
        Me.lblEmail.Name = "lblEmail"
        Me.lblEmail.Size = New System.Drawing.Size(92, 15)
        Me.lblEmail.TabIndex = 11
        Me.lblEmail.Text = "[Teacher Email]"
        '
        'btnPrintSalary
        '
        Me.btnPrintSalary.BackColor = System.Drawing.Color.DarkTurquoise
        Me.btnPrintSalary.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnPrintSalary.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrintSalary.Font = New System.Drawing.Font("Berlin Sans FB", 12.0!)
        Me.btnPrintSalary.ForeColor = System.Drawing.Color.White
        Me.btnPrintSalary.Image = Global.DMS1.My.Resources.Resources.icons8_receipt_32
        Me.btnPrintSalary.Location = New System.Drawing.Point(3, 317)
        Me.btnPrintSalary.Name = "btnPrintSalary"
        Me.btnPrintSalary.Size = New System.Drawing.Size(160, 49)
        Me.btnPrintSalary.TabIndex = 12
        Me.btnPrintSalary.Text = "Print Salary"
        Me.btnPrintSalary.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnPrintSalary.UseVisualStyleBackColor = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(26, 12)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(116, 107)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 10
        Me.PictureBox1.TabStop = False
        '
        'btnLogout
        '
        Me.btnLogout.BackColor = System.Drawing.Color.Red
        Me.btnLogout.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnLogout.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray
        Me.btnLogout.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray
        Me.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLogout.Font = New System.Drawing.Font("Bell MT", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLogout.ForeColor = System.Drawing.Color.White
        Me.btnLogout.Image = Global.DMS1.My.Resources.Resources.icons8_log_out_32
        Me.btnLogout.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnLogout.Location = New System.Drawing.Point(14, 676)
        Me.btnLogout.Name = "btnLogout"
        Me.btnLogout.Size = New System.Drawing.Size(139, 46)
        Me.btnLogout.TabIndex = 9
        Me.btnLogout.Text = "Logout"
        Me.btnLogout.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnLogout.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnLogout.UseVisualStyleBackColor = False
        '
        'btnSchedule
        '
        Me.btnSchedule.BackColor = System.Drawing.Color.Coral
        Me.btnSchedule.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSchedule.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray
        Me.btnSchedule.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray
        Me.btnSchedule.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSchedule.Font = New System.Drawing.Font("Berlin Sans FB", 12.0!)
        Me.btnSchedule.ForeColor = System.Drawing.Color.White
        Me.btnSchedule.Image = Global.DMS1.My.Resources.Resources.icons8_schedule_32
        Me.btnSchedule.Location = New System.Drawing.Point(3, 265)
        Me.btnSchedule.Name = "btnSchedule"
        Me.btnSchedule.Size = New System.Drawing.Size(160, 46)
        Me.btnSchedule.TabIndex = 4
        Me.btnSchedule.Text = "View Schedule"
        Me.btnSchedule.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnSchedule.UseVisualStyleBackColor = False
        '
        'btnTimeOut
        '
        Me.btnTimeOut.BackColor = System.Drawing.Color.Crimson
        Me.btnTimeOut.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnTimeOut.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray
        Me.btnTimeOut.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray
        Me.btnTimeOut.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTimeOut.Font = New System.Drawing.Font("Bahnschrift SemiLight Condensed", 14.25!)
        Me.btnTimeOut.ForeColor = System.Drawing.Color.White
        Me.btnTimeOut.Image = Global.DMS1.My.Resources.Resources.icons8_check_out_32
        Me.btnTimeOut.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnTimeOut.Location = New System.Drawing.Point(28, 598)
        Me.btnTimeOut.Name = "btnTimeOut"
        Me.btnTimeOut.Size = New System.Drawing.Size(106, 72)
        Me.btnTimeOut.TabIndex = 3
        Me.btnTimeOut.Text = "Time Out"
        Me.btnTimeOut.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnTimeOut.UseVisualStyleBackColor = False
        '
        'btnTimeIn
        '
        Me.btnTimeIn.BackColor = System.Drawing.SystemColors.Highlight
        Me.btnTimeIn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnTimeIn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnTimeIn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray
        Me.btnTimeIn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray
        Me.btnTimeIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTimeIn.Font = New System.Drawing.Font("Bahnschrift SemiLight Condensed", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTimeIn.ForeColor = System.Drawing.Color.White
        Me.btnTimeIn.Image = Global.DMS1.My.Resources.Resources.icons8_check_in_32
        Me.btnTimeIn.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnTimeIn.Location = New System.Drawing.Point(28, 520)
        Me.btnTimeIn.Name = "btnTimeIn"
        Me.btnTimeIn.Size = New System.Drawing.Size(106, 72)
        Me.btnTimeIn.TabIndex = 3
        Me.btnTimeIn.Text = "Time In"
        Me.btnTimeIn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnTimeIn.UseVisualStyleBackColor = False
        '
        'btnDash
        '
        Me.btnDash.BackColor = System.Drawing.Color.Coral
        Me.btnDash.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnDash.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray
        Me.btnDash.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray
        Me.btnDash.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDash.Font = New System.Drawing.Font("Berlin Sans FB", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDash.ForeColor = System.Drawing.Color.White
        Me.btnDash.Image = Global.DMS1.My.Resources.Resources.icons8_index_32
        Me.btnDash.Location = New System.Drawing.Point(3, 161)
        Me.btnDash.Name = "btnDash"
        Me.btnDash.Size = New System.Drawing.Size(160, 46)
        Me.btnDash.TabIndex = 3
        Me.btnDash.Text = "Dashboard"
        Me.btnDash.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnDash.UseVisualStyleBackColor = False
        '
        'btnInputGrades
        '
        Me.btnInputGrades.BackColor = System.Drawing.Color.Coral
        Me.btnInputGrades.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnInputGrades.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray
        Me.btnInputGrades.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray
        Me.btnInputGrades.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnInputGrades.Font = New System.Drawing.Font("Berlin Sans FB", 12.0!)
        Me.btnInputGrades.ForeColor = System.Drawing.Color.White
        Me.btnInputGrades.Image = Global.DMS1.My.Resources.Resources.icons8_grade_32
        Me.btnInputGrades.Location = New System.Drawing.Point(3, 213)
        Me.btnInputGrades.Name = "btnInputGrades"
        Me.btnInputGrades.Size = New System.Drawing.Size(160, 46)
        Me.btnInputGrades.TabIndex = 3
        Me.btnInputGrades.Text = "Input Grades"
        Me.btnInputGrades.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnInputGrades.UseVisualStyleBackColor = False
        '
        'TeacherDashboard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1350, 729)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "TeacherDashboard"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Teacher Dashboard"
        Me.Panel2.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.dgSalarySubject, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.dgHourlyData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents lblEmail As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents btnLogout As Button
    Friend WithEvents btnSchedule As Button
    Friend WithEvents btnInputGrades As Button
    Friend WithEvents btnTimeOut As Button
    Friend WithEvents btnTimeIn As Button
    Friend WithEvents btnDash As Button
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents dgHourlyData As DataGridView
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents dgSalarySubject As DataGridView
    Friend WithEvents btnPrintSalary As Button
End Class
