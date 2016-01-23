<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmChangeLanguage
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmChangeLanguage))
        Me.lblScreenSaverCurrentFile = New System.Windows.Forms.Label()
        Me.lblScreenSaverCurrentTime = New System.Windows.Forms.Label()
        Me.TimerScreenSaver = New System.Windows.Forms.Timer(Me.components)
        Me.lblExit = New System.Windows.Forms.Label()
        Me.pbThai = New System.Windows.Forms.PictureBox()
        Me.pbEng = New System.Windows.Forms.PictureBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.TimerCheckAlarm = New System.Windows.Forms.Timer(Me.components)
        CType(Me.pbThai, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbEng, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblScreenSaverCurrentFile
        '
        Me.lblScreenSaverCurrentFile.AutoSize = True
        Me.lblScreenSaverCurrentFile.Location = New System.Drawing.Point(285, 173)
        Me.lblScreenSaverCurrentFile.Name = "lblScreenSaverCurrentFile"
        Me.lblScreenSaverCurrentFile.Size = New System.Drawing.Size(0, 13)
        Me.lblScreenSaverCurrentFile.TabIndex = 5
        Me.lblScreenSaverCurrentFile.Visible = False
        '
        'lblScreenSaverCurrentTime
        '
        Me.lblScreenSaverCurrentTime.AutoSize = True
        Me.lblScreenSaverCurrentTime.Location = New System.Drawing.Point(285, 149)
        Me.lblScreenSaverCurrentTime.Name = "lblScreenSaverCurrentTime"
        Me.lblScreenSaverCurrentTime.Size = New System.Drawing.Size(0, 13)
        Me.lblScreenSaverCurrentTime.TabIndex = 4
        Me.lblScreenSaverCurrentTime.Visible = False
        '
        'TimerScreenSaver
        '
        Me.TimerScreenSaver.Enabled = True
        Me.TimerScreenSaver.Interval = 1000
        '
        'lblExit
        '
        Me.lblExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblExit.AutoSize = True
        Me.lblExit.BackColor = System.Drawing.Color.White
        Me.lblExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblExit.ForeColor = System.Drawing.Color.White
        Me.lblExit.Location = New System.Drawing.Point(994, 6)
        Me.lblExit.Name = "lblExit"
        Me.lblExit.Size = New System.Drawing.Size(24, 24)
        Me.lblExit.TabIndex = 10
        Me.lblExit.Text = "E"
        '
        'pbThai
        '
        Me.pbThai.BackColor = System.Drawing.Color.Transparent
        Me.pbThai.Location = New System.Drawing.Point(643, 567)
        Me.pbThai.Name = "pbThai"
        Me.pbThai.Size = New System.Drawing.Size(140, 140)
        Me.pbThai.TabIndex = 11
        Me.pbThai.TabStop = False
        '
        'pbEng
        '
        Me.pbEng.BackColor = System.Drawing.Color.Transparent
        Me.pbEng.Location = New System.Drawing.Point(816, 567)
        Me.pbEng.Name = "pbEng"
        Me.pbEng.Size = New System.Drawing.Size(139, 140)
        Me.pbEng.TabIndex = 12
        Me.pbEng.TabStop = False
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(288, 615)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 13
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        Me.Button1.Visible = False
        '
        'TimerCheckAlarm
        '
        Me.TimerCheckAlarm.Enabled = True
        Me.TimerCheckAlarm.Interval = 1000
        '
        'frmChangeLanguage
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1024, 768)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.pbEng)
        Me.Controls.Add(Me.pbThai)
        Me.Controls.Add(Me.lblExit)
        Me.Controls.Add(Me.lblScreenSaverCurrentFile)
        Me.Controls.Add(Me.lblScreenSaverCurrentTime)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmChangeLanguage"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "กรุณาเลือกภาษา"
        CType(Me.pbThai, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbEng, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblScreenSaverCurrentFile As System.Windows.Forms.Label
    Friend WithEvents lblScreenSaverCurrentTime As System.Windows.Forms.Label
    Friend WithEvents TimerScreenSaver As System.Windows.Forms.Timer
    Friend WithEvents lblExit As System.Windows.Forms.Label
    Friend WithEvents pbThai As System.Windows.Forms.PictureBox
    Friend WithEvents pbEng As System.Windows.Forms.PictureBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents TimerCheckAlarm As System.Windows.Forms.Timer
End Class
