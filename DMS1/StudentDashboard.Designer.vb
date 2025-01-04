<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class StudentDashboard
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(StudentDashboard))
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.btnEForm = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblEmail = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.btnLogout = New System.Windows.Forms.Button()
        Me.btnStudy = New System.Windows.Forms.Button()
        Me.lblID = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Teal
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(167, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1183, 729)
        Me.Panel2.TabIndex = 5
        '
        'btnEForm
        '
        Me.btnEForm.BackColor = System.Drawing.Color.Coral
        Me.btnEForm.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnEForm.FlatAppearance.BorderSize = 0
        Me.btnEForm.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray
        Me.btnEForm.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray
        Me.btnEForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEForm.Font = New System.Drawing.Font("Segoe UI Black", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEForm.ForeColor = System.Drawing.Color.Black
        Me.btnEForm.Location = New System.Drawing.Point(9, 250)
        Me.btnEForm.Name = "btnEForm"
        Me.btnEForm.Size = New System.Drawing.Size(149, 52)
        Me.btnEForm.TabIndex = 1
        Me.btnEForm.Text = "Enrollment Form"
        Me.btnEForm.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnEForm.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.DimGray
        Me.Panel1.Controls.Add(Me.lblID)
        Me.Panel1.Controls.Add(Me.lblEmail)
        Me.Panel1.Controls.Add(Me.PictureBox1)
        Me.Panel1.Controls.Add(Me.btnLogout)
        Me.Panel1.Controls.Add(Me.btnStudy)
        Me.Panel1.Controls.Add(Me.btnEForm)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(167, 729)
        Me.Panel1.TabIndex = 4
        '
        'lblEmail
        '
        Me.lblEmail.AutoSize = True
        Me.lblEmail.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEmail.Location = New System.Drawing.Point(12, 147)
        Me.lblEmail.Name = "lblEmail"
        Me.lblEmail.Size = New System.Drawing.Size(126, 16)
        Me.lblEmail.TabIndex = 11
        Me.lblEmail.Text = "[Student's Name]"
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(30, 12)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(111, 103)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 10
        Me.PictureBox1.TabStop = False
        '
        'btnLogout
        '
        Me.btnLogout.BackColor = System.Drawing.Color.Coral
        Me.btnLogout.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnLogout.FlatAppearance.BorderSize = 0
        Me.btnLogout.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray
        Me.btnLogout.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray
        Me.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLogout.Font = New System.Drawing.Font("Segoe UI Black", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLogout.ForeColor = System.Drawing.Color.Black
        Me.btnLogout.Location = New System.Drawing.Point(9, 680)
        Me.btnLogout.Name = "btnLogout"
        Me.btnLogout.Size = New System.Drawing.Size(149, 46)
        Me.btnLogout.TabIndex = 9
        Me.btnLogout.Text = "Logout"
        Me.btnLogout.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnLogout.UseVisualStyleBackColor = False
        '
        'btnStudy
        '
        Me.btnStudy.BackColor = System.Drawing.Color.Coral
        Me.btnStudy.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnStudy.FlatAppearance.BorderSize = 0
        Me.btnStudy.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray
        Me.btnStudy.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray
        Me.btnStudy.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnStudy.Font = New System.Drawing.Font("Segoe UI Black", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnStudy.ForeColor = System.Drawing.Color.Black
        Me.btnStudy.Location = New System.Drawing.Point(9, 308)
        Me.btnStudy.Name = "btnStudy"
        Me.btnStudy.Size = New System.Drawing.Size(149, 46)
        Me.btnStudy.TabIndex = 7
        Me.btnStudy.Text = "Generate Study Load"
        Me.btnStudy.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnStudy.UseVisualStyleBackColor = False
        '
        'lblID
        '
        Me.lblID.AutoSize = True
        Me.lblID.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblID.Location = New System.Drawing.Point(12, 172)
        Me.lblID.Name = "lblID"
        Me.lblID.Size = New System.Drawing.Size(100, 16)
        Me.lblID.TabIndex = 11
        Me.lblID.Text = "[Student's ID]"
        '
        'StudentDashboard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1350, 729)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "StudentDashboard"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Student Dashboard"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel2 As Panel
    Friend WithEvents btnEForm As Button
    Friend WithEvents Panel1 As Panel
    Friend WithEvents btnLogout As Button
    Friend WithEvents btnStudy As Button
    Friend WithEvents lblEmail As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents lblID As Label
End Class
