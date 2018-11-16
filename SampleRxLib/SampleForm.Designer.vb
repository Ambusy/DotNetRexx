<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SampleForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.RexxRc = New System.Windows.Forms.TextBox()
        Me.RexxParm = New System.Windows.Forms.TextBox()
        Me.SelFileName = New System.Windows.Forms.Button()
        Me.CompExecRexx = New System.Windows.Forms.Button()
        Me.RexxFileName = New System.Windows.Forms.TextBox()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.ExecRexx = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.CDebug = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'RexxRc
        '
        Me.RexxRc.Location = New System.Drawing.Point(271, 212)
        Me.RexxRc.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.RexxRc.Name = "RexxRc"
        Me.RexxRc.Size = New System.Drawing.Size(72, 22)
        Me.RexxRc.TabIndex = 9
        Me.RexxRc.Visible = False
        '
        'RexxParm
        '
        Me.RexxParm.Location = New System.Drawing.Point(149, 64)
        Me.RexxParm.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.RexxParm.Name = "RexxParm"
        Me.RexxParm.Size = New System.Drawing.Size(740, 22)
        Me.RexxParm.TabIndex = 8
        '
        'SelFileName
        '
        Me.SelFileName.Location = New System.Drawing.Point(899, 15)
        Me.SelFileName.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.SelFileName.Name = "SelFileName"
        Me.SelFileName.Size = New System.Drawing.Size(88, 28)
        Me.SelFileName.TabIndex = 7
        Me.SelFileName.Text = "Browse..."
        Me.SelFileName.UseVisualStyleBackColor = True
        '
        'CompExecRexx
        '
        Me.CompExecRexx.Location = New System.Drawing.Point(16, 177)
        Me.CompExecRexx.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.CompExecRexx.Name = "CompExecRexx"
        Me.CompExecRexx.Size = New System.Drawing.Size(247, 46)
        Me.CompExecRexx.TabIndex = 6
        Me.CompExecRexx.Text = "Compile and run Rexx"
        Me.CompExecRexx.UseVisualStyleBackColor = True
        Me.CompExecRexx.Visible = False
        '
        'RexxFileName
        '
        Me.RexxFileName.Location = New System.Drawing.Point(16, 15)
        Me.RexxFileName.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.RexxFileName.Name = "RexxFileName"
        Me.RexxFileName.Size = New System.Drawing.Size(873, 22)
        Me.RexxFileName.TabIndex = 5
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'ExecRexx
        '
        Me.ExecRexx.Location = New System.Drawing.Point(16, 230)
        Me.ExecRexx.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ExecRexx.Name = "ExecRexx"
        Me.ExecRexx.Size = New System.Drawing.Size(247, 46)
        Me.ExecRexx.TabIndex = 10
        Me.ExecRexx.Text = "Run Rexx again"
        Me.ExecRexx.UseVisualStyleBackColor = True
        Me.ExecRexx.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 68)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(129, 17)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "Parameter for Rexx"
        '
        'CDebug
        '
        Me.CDebug.AutoSize = True
        Me.CDebug.Location = New System.Drawing.Point(905, 69)
        Me.CDebug.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.CDebug.Name = "CDebug"
        Me.CDebug.Size = New System.Drawing.Size(72, 21)
        Me.CDebug.TabIndex = 12
        Me.CDebug.Text = "Debug"
        Me.CDebug.UseVisualStyleBackColor = True
        '
        'SampleForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1000, 327)
        Me.Controls.Add(Me.CDebug)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ExecRexx)
        Me.Controls.Add(Me.RexxRc)
        Me.Controls.Add(Me.RexxParm)
        Me.Controls.Add(Me.SelFileName)
        Me.Controls.Add(Me.CompExecRexx)
        Me.Controls.Add(Me.RexxFileName)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "SampleForm"
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RexxRc As System.Windows.Forms.TextBox
    Friend WithEvents RexxParm As System.Windows.Forms.TextBox
    Friend WithEvents SelFileName As System.Windows.Forms.Button
    Friend WithEvents CompExecRexx As System.Windows.Forms.Button
    Friend WithEvents RexxFileName As System.Windows.Forms.TextBox
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents ExecRexx As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents CDebug As System.Windows.Forms.CheckBox

End Class
