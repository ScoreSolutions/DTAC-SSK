<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmScreenSaver
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmScreenSaver))
        Me.VDO = New AxWMPLib.AxWindowsMediaPlayer()
        Me.TimerSetTime = New System.Windows.Forms.Timer(Me.components)
        Me.lblCurrentTime = New System.Windows.Forms.Label()
        Me.lblCurrentFile = New System.Windows.Forms.Label()
        Me.TimerCheckBillerLogoFile = New System.Windows.Forms.Timer(Me.components)
        CType(Me.VDO, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'VDO
        '
        Me.VDO.Dock = System.Windows.Forms.DockStyle.Fill
        Me.VDO.Enabled = True
        Me.VDO.Location = New System.Drawing.Point(0, 0)
        Me.VDO.Name = "VDO"
        Me.VDO.OcxState = CType(resources.GetObject("VDO.OcxState"), System.Windows.Forms.AxHost.State)
        Me.VDO.Size = New System.Drawing.Size(589, 518)
        Me.VDO.TabIndex = 0
        '
        'TimerSetTime
        '
        '
        'lblCurrentTime
        '
        Me.lblCurrentTime.AutoSize = True
        Me.lblCurrentTime.Font = New System.Drawing.Font("Microsoft Sans Serif", 26.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblCurrentTime.Location = New System.Drawing.Point(41, -3)
        Me.lblCurrentTime.Name = "lblCurrentTime"
        Me.lblCurrentTime.Size = New System.Drawing.Size(36, 39)
        Me.lblCurrentTime.TabIndex = 3
        Me.lblCurrentTime.Text = "0"
        Me.lblCurrentTime.Visible = False
        '
        'lblCurrentFile
        '
        Me.lblCurrentFile.AutoSize = True
        Me.lblCurrentFile.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblCurrentFile.Location = New System.Drawing.Point(174, 9)
        Me.lblCurrentFile.Name = "lblCurrentFile"
        Me.lblCurrentFile.Size = New System.Drawing.Size(0, 16)
        Me.lblCurrentFile.TabIndex = 4
        Me.lblCurrentFile.Visible = False
        '
        'TimerCheckBillerLogoFile
        '
        Me.TimerCheckBillerLogoFile.Enabled = True
        Me.TimerCheckBillerLogoFile.Interval = 1000
        '
        'frmScreenSaver
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(589, 518)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblCurrentFile)
        Me.Controls.Add(Me.lblCurrentTime)
        Me.Controls.Add(Me.VDO)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmScreenSaver"
        Me.Text = "frmVDO"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.VDO, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents VDO As AxWMPLib.AxWindowsMediaPlayer
    Friend WithEvents TimerSetTime As System.Windows.Forms.Timer
    Public WithEvents lblCurrentTime As System.Windows.Forms.Label
    Public WithEvents lblCurrentFile As System.Windows.Forms.Label
    Friend WithEvents TimerCheckBillerLogoFile As System.Windows.Forms.Timer
End Class
