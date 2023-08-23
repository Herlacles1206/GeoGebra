<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AngleFixedUI
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txt_angle = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.radio_counterclock = New System.Windows.Forms.RadioButton()
        Me.radio_clock = New System.Windows.Forms.RadioButton()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(21, 26)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(34, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Angle"
        '
        'txt_angle
        '
        Me.txt_angle.Location = New System.Drawing.Point(140, 23)
        Me.txt_angle.Name = "txt_angle"
        Me.txt_angle.Size = New System.Drawing.Size(100, 20)
        Me.txt_angle.TabIndex = 1
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.radio_clock)
        Me.GroupBox1.Controls.Add(Me.radio_counterclock)
        Me.GroupBox1.Location = New System.Drawing.Point(24, 47)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(216, 46)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        '
        'radio_counterclock
        '
        Me.radio_counterclock.AutoSize = True
        Me.radio_counterclock.Location = New System.Drawing.Point(16, 19)
        Me.radio_counterclock.Name = "radio_counterclock"
        Me.radio_counterclock.Size = New System.Drawing.Size(109, 17)
        Me.radio_counterclock.TabIndex = 0
        Me.radio_counterclock.TabStop = True
        Me.radio_counterclock.Text = "Counterclockwise"
        Me.radio_counterclock.UseVisualStyleBackColor = True
        '
        'radio_clock
        '
        Me.radio_clock.AutoSize = True
        Me.radio_clock.Location = New System.Drawing.Point(136, 19)
        Me.radio_clock.Name = "radio_clock"
        Me.radio_clock.Size = New System.Drawing.Size(73, 17)
        Me.radio_clock.TabIndex = 1
        Me.radio_clock.TabStop = True
        Me.radio_clock.Text = "Clockwise"
        Me.radio_clock.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Button1.Location = New System.Drawing.Point(87, 99)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 3
        Me.Button1.Text = "OK"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button2.Location = New System.Drawing.Point(165, 99)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 4
        Me.Button2.Text = "Cancel"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'AngleFixedUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(263, 128)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.txt_angle)
        Me.Controls.Add(Me.Label1)
        Me.Name = "AngleFixedUI"
        Me.Text = "Angle with Given size"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents txt_angle As TextBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents radio_clock As RadioButton
    Friend WithEvents radio_counterclock As RadioButton
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
End Class
