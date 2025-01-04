<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmAdmin
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAdmin))
        Me.btnLogout = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.btnSchedule = New System.Windows.Forms.Button()
        Me.btnRoomManage = New System.Windows.Forms.Button()
        Me.btnSection = New System.Windows.Forms.Button()
        Me.btnManageSchedule = New System.Windows.Forms.Button()
        Me.btnTeachers = New System.Windows.Forms.Button()
        Me.btnStudent = New System.Windows.Forms.Button()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.panelDash = New System.Windows.Forms.Panel()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnLogout
        '
        Me.btnLogout.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnLogout.FlatAppearance.BorderSize = 0
        Me.btnLogout.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray
        Me.btnLogout.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray
        Me.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLogout.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btnLogout.ForeColor = System.Drawing.Color.White
        Me.btnLogout.Location = New System.Drawing.Point(1, 641)
        Me.btnLogout.Name = "btnLogout"
        Me.btnLogout.Size = New System.Drawing.Size(166, 46)
        Me.btnLogout.TabIndex = 9
        Me.btnLogout.Text = "Logout"
        Me.btnLogout.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnLogout.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(36, Byte), Integer), CType(CType(46, Byte), Integer))
        Me.Panel1.Controls.Add(Me.PictureBox1)
        Me.Panel1.Controls.Add(Me.btnLogout)
        Me.Panel1.Controls.Add(Me.btnSchedule)
        Me.Panel1.Controls.Add(Me.btnRoomManage)
        Me.Panel1.Controls.Add(Me.btnSection)
        Me.Panel1.Controls.Add(Me.btnManageSchedule)
        Me.Panel1.Controls.Add(Me.Button1)
        Me.Panel1.Controls.Add(Me.btnTeachers)
        Me.Panel1.Controls.Add(Me.btnStudent)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(167, 690)
        Me.Panel1.TabIndex = 4
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(33, 12)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(100, 97)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'btnSchedule
        '
        Me.btnSchedule.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSchedule.FlatAppearance.BorderSize = 0
        Me.btnSchedule.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray
        Me.btnSchedule.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray
        Me.btnSchedule.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSchedule.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btnSchedule.ForeColor = System.Drawing.Color.White
        Me.btnSchedule.Location = New System.Drawing.Point(3, 268)
        Me.btnSchedule.Name = "btnSchedule"
        Me.btnSchedule.Size = New System.Drawing.Size(166, 46)
        Me.btnSchedule.TabIndex = 7
        Me.btnSchedule.Text = "Manage Subjects"
        Me.btnSchedule.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnSchedule.UseVisualStyleBackColor = False
        '
        'btnRoomManage
        '
        Me.btnRoomManage.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnRoomManage.FlatAppearance.BorderSize = 0
        Me.btnRoomManage.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray
        Me.btnRoomManage.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray
        Me.btnRoomManage.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRoomManage.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btnRoomManage.ForeColor = System.Drawing.Color.White
        Me.btnRoomManage.Location = New System.Drawing.Point(3, 372)
        Me.btnRoomManage.Name = "btnRoomManage"
        Me.btnRoomManage.Size = New System.Drawing.Size(166, 46)
        Me.btnRoomManage.TabIndex = 6
        Me.btnRoomManage.Text = "Manage Room"
        Me.btnRoomManage.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnRoomManage.UseVisualStyleBackColor = False
        '
        'btnSection
        '
        Me.btnSection.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSection.FlatAppearance.BorderSize = 0
        Me.btnSection.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray
        Me.btnSection.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray
        Me.btnSection.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSection.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btnSection.ForeColor = System.Drawing.Color.White
        Me.btnSection.Location = New System.Drawing.Point(0, 320)
        Me.btnSection.Name = "btnSection"
        Me.btnSection.Size = New System.Drawing.Size(166, 46)
        Me.btnSection.TabIndex = 6
        Me.btnSection.Text = "Manage Sections"
        Me.btnSection.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnSection.UseVisualStyleBackColor = False
        '
        'btnManageSchedule
        '
        Me.btnManageSchedule.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnManageSchedule.FlatAppearance.BorderSize = 0
        Me.btnManageSchedule.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray
        Me.btnManageSchedule.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray
        Me.btnManageSchedule.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnManageSchedule.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btnManageSchedule.ForeColor = System.Drawing.Color.White
        Me.btnManageSchedule.Location = New System.Drawing.Point(3, 216)
        Me.btnManageSchedule.Name = "btnManageSchedule"
        Me.btnManageSchedule.Size = New System.Drawing.Size(166, 46)
        Me.btnManageSchedule.TabIndex = 5
        Me.btnManageSchedule.Text = "Manage Schedule"
        Me.btnManageSchedule.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnManageSchedule.UseVisualStyleBackColor = False
        '
        'btnTeachers
        '
        Me.btnTeachers.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnTeachers.FlatAppearance.BorderSize = 0
        Me.btnTeachers.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray
        Me.btnTeachers.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray
        Me.btnTeachers.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTeachers.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btnTeachers.ForeColor = System.Drawing.Color.White
        Me.btnTeachers.Location = New System.Drawing.Point(0, 164)
        Me.btnTeachers.Name = "btnTeachers"
        Me.btnTeachers.Size = New System.Drawing.Size(166, 46)
        Me.btnTeachers.TabIndex = 4
        Me.btnTeachers.Text = "Manage Teachers"
        Me.btnTeachers.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnTeachers.UseVisualStyleBackColor = False
        '
        'btnStudent
        '
        Me.btnStudent.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnStudent.FlatAppearance.BorderSize = 0
        Me.btnStudent.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray
        Me.btnStudent.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray
        Me.btnStudent.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnStudent.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btnStudent.ForeColor = System.Drawing.Color.White
        Me.btnStudent.Location = New System.Drawing.Point(0, 112)
        Me.btnStudent.Name = "btnStudent"
        Me.btnStudent.Size = New System.Drawing.Size(166, 46)
        Me.btnStudent.TabIndex = 3
        Me.btnStudent.Text = "Manage Students"
        Me.btnStudent.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnStudent.UseVisualStyleBackColor = False
        '
        'Panel2
        '
        Me.Panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel2.BackColor = System.Drawing.Color.Teal
        Me.Panel2.Controls.Add(Me.panelDash)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1334, 690)
        Me.Panel2.TabIndex = 5
        '
        'panelDash
        '
        Me.panelDash.Dock = System.Windows.Forms.DockStyle.Right
        Me.panelDash.Location = New System.Drawing.Point(167, 0)
        Me.panelDash.Name = "panelDash"
        Me.panelDash.Size = New System.Drawing.Size(1167, 690)
        Me.panelDash.TabIndex = 0
        '
        'Button1
        '
        Me.Button1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button1.FlatAppearance.BorderSize = 0
        Me.Button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray
        Me.Button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Location = New System.Drawing.Point(3, 424)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(161, 46)
        Me.Button1.TabIndex = 4
        Me.Button1.Text = "Manage Teachers Salary"
        Me.Button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button1.UseVisualStyleBackColor = False
        '
        'frmAdmin
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.ClientSize = New System.Drawing.Size(1334, 690)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmAdmin"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Admin Dashboard"
        Me.Panel1.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents btnLogout As Button
    Friend WithEvents Panel1 As Panel
    Friend WithEvents btnSchedule As Button
    Friend WithEvents btnSection As Button
    Friend WithEvents btnManageSchedule As Button
    Friend WithEvents btnTeachers As Button
    Friend WithEvents btnStudent As Button
    Friend WithEvents Panel2 As Panel
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents panelDash As Panel
    Friend WithEvents btnRoomManage As Button
    Friend WithEvents Button1 As Button
End Class
