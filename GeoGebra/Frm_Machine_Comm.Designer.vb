<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Machine_Comm
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
        Me.GB_ZAxis = New System.Windows.Forms.GroupBox()
        Me.Btn_FocusDownFast = New System.Windows.Forms.Button()
        Me.Btn_FocusDownSlow = New System.Windows.Forms.Button()
        Me.Btn_FocusUpFast = New System.Windows.Forms.Button()
        Me.Btn_FocusUPSlow = New System.Windows.Forms.Button()
        Me.Pnl_MotorisedStage = New System.Windows.Forms.Panel()
        Me.Btn_GoTo_Incremental = New System.Windows.Forms.Button()
        Me.TB_GoTo_Y = New System.Windows.Forms.TextBox()
        Me.Btn_YFastBack = New System.Windows.Forms.Button()
        Me.TB_GoTo_X = New System.Windows.Forms.TextBox()
        Me.Btn_XFastLeft = New System.Windows.Forms.Button()
        Me.Btn_YFastFront = New System.Windows.Forms.Button()
        Me.Btn_XFastRight = New System.Windows.Forms.Button()
        Me.Btn_XSlowRight = New System.Windows.Forms.Button()
        Me.Btn_YSlowBack = New System.Windows.Forms.Button()
        Me.Btn_YSlowFront = New System.Windows.Forms.Button()
        Me.Btn_XSlowLeft = New System.Windows.Forms.Button()
        Me.Btn_EStop = New System.Windows.Forms.Button()
        Me.Btn_ComConnect = New System.Windows.Forms.Button()
        Me.GB_MachineConnect = New System.Windows.Forms.GroupBox()
        Me.cbPortName = New System.Windows.Forms.ComboBox()
        Me.Btn_Refresh_MainMachineCOMS = New System.Windows.Forms.Button()
        Me.WsmbsControl1 = New WSMBS.WSMBSControl(Me.components)
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.TB_PosY = New System.Windows.Forms.TextBox()
        Me.TB_PosX = New System.Windows.Forms.TextBox()
        Me.Btn_CurX = New System.Windows.Forms.Button()
        Me.Btn_CurY = New System.Windows.Forms.Button()
        Me.GB_ZAxis.SuspendLayout()
        Me.Pnl_MotorisedStage.SuspendLayout()
        Me.GB_MachineConnect.SuspendLayout()
        Me.SuspendLayout()
        '
        'GB_ZAxis
        '
        Me.GB_ZAxis.AutoSize = True
        Me.GB_ZAxis.BackColor = System.Drawing.SystemColors.ControlDark
        Me.GB_ZAxis.Controls.Add(Me.Btn_FocusDownFast)
        Me.GB_ZAxis.Controls.Add(Me.Btn_FocusDownSlow)
        Me.GB_ZAxis.Controls.Add(Me.Btn_FocusUpFast)
        Me.GB_ZAxis.Controls.Add(Me.Btn_FocusUPSlow)
        Me.GB_ZAxis.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.GB_ZAxis.Location = New System.Drawing.Point(12, 12)
        Me.GB_ZAxis.Name = "GB_ZAxis"
        Me.GB_ZAxis.Size = New System.Drawing.Size(100, 292)
        Me.GB_ZAxis.TabIndex = 554
        Me.GB_ZAxis.TabStop = False
        Me.GB_ZAxis.Text = "Focus Control"
        '
        'Btn_FocusDownFast
        '
        Me.Btn_FocusDownFast.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_FocusDownFast.ForeColor = System.Drawing.Color.IndianRed
        Me.Btn_FocusDownFast.Location = New System.Drawing.Point(9, 221)
        Me.Btn_FocusDownFast.Name = "Btn_FocusDownFast"
        Me.Btn_FocusDownFast.Size = New System.Drawing.Size(81, 52)
        Me.Btn_FocusDownFast.TabIndex = 547
        Me.Btn_FocusDownFast.Text = "▼▼"
        Me.Btn_FocusDownFast.UseVisualStyleBackColor = True
        '
        'Btn_FocusDownSlow
        '
        Me.Btn_FocusDownSlow.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_FocusDownSlow.ForeColor = System.Drawing.Color.RosyBrown
        Me.Btn_FocusDownSlow.Location = New System.Drawing.Point(9, 153)
        Me.Btn_FocusDownSlow.Name = "Btn_FocusDownSlow"
        Me.Btn_FocusDownSlow.Size = New System.Drawing.Size(81, 52)
        Me.Btn_FocusDownSlow.TabIndex = 546
        Me.Btn_FocusDownSlow.Text = "▼"
        Me.Btn_FocusDownSlow.UseVisualStyleBackColor = True
        '
        'Btn_FocusUpFast
        '
        Me.Btn_FocusUpFast.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_FocusUpFast.ForeColor = System.Drawing.Color.IndianRed
        Me.Btn_FocusUpFast.Location = New System.Drawing.Point(9, 17)
        Me.Btn_FocusUpFast.Name = "Btn_FocusUpFast"
        Me.Btn_FocusUpFast.Size = New System.Drawing.Size(81, 52)
        Me.Btn_FocusUpFast.TabIndex = 545
        Me.Btn_FocusUpFast.Text = "▲▲"
        Me.Btn_FocusUpFast.UseVisualStyleBackColor = True
        '
        'Btn_FocusUPSlow
        '
        Me.Btn_FocusUPSlow.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_FocusUPSlow.ForeColor = System.Drawing.Color.RosyBrown
        Me.Btn_FocusUPSlow.Location = New System.Drawing.Point(9, 85)
        Me.Btn_FocusUPSlow.Name = "Btn_FocusUPSlow"
        Me.Btn_FocusUPSlow.Size = New System.Drawing.Size(81, 52)
        Me.Btn_FocusUPSlow.TabIndex = 544
        Me.Btn_FocusUPSlow.Text = "▲"
        Me.Btn_FocusUPSlow.UseVisualStyleBackColor = True
        '
        'Pnl_MotorisedStage
        '
        Me.Pnl_MotorisedStage.Controls.Add(Me.Btn_GoTo_Incremental)
        Me.Pnl_MotorisedStage.Controls.Add(Me.TB_GoTo_Y)
        Me.Pnl_MotorisedStage.Controls.Add(Me.Btn_YFastBack)
        Me.Pnl_MotorisedStage.Controls.Add(Me.TB_GoTo_X)
        Me.Pnl_MotorisedStage.Controls.Add(Me.Btn_XFastLeft)
        Me.Pnl_MotorisedStage.Controls.Add(Me.Btn_YFastFront)
        Me.Pnl_MotorisedStage.Controls.Add(Me.Btn_XFastRight)
        Me.Pnl_MotorisedStage.Controls.Add(Me.Btn_XSlowRight)
        Me.Pnl_MotorisedStage.Controls.Add(Me.Btn_YSlowBack)
        Me.Pnl_MotorisedStage.Controls.Add(Me.Btn_YSlowFront)
        Me.Pnl_MotorisedStage.Controls.Add(Me.Btn_XSlowLeft)
        Me.Pnl_MotorisedStage.Location = New System.Drawing.Point(118, 12)
        Me.Pnl_MotorisedStage.Name = "Pnl_MotorisedStage"
        Me.Pnl_MotorisedStage.Size = New System.Drawing.Size(237, 259)
        Me.Pnl_MotorisedStage.TabIndex = 555
        '
        'Btn_GoTo_Incremental
        '
        Me.Btn_GoTo_Incremental.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_GoTo_Incremental.Location = New System.Drawing.Point(161, 231)
        Me.Btn_GoTo_Incremental.Name = "Btn_GoTo_Incremental"
        Me.Btn_GoTo_Incremental.Size = New System.Drawing.Size(66, 22)
        Me.Btn_GoTo_Incremental.TabIndex = 567
        Me.Btn_GoTo_Incremental.Text = "Go To ►╢"
        Me.Btn_GoTo_Incremental.UseVisualStyleBackColor = True
        '
        'TB_GoTo_Y
        '
        Me.TB_GoTo_Y.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TB_GoTo_Y.Location = New System.Drawing.Point(88, 231)
        Me.TB_GoTo_Y.Name = "TB_GoTo_Y"
        Me.TB_GoTo_Y.Size = New System.Drawing.Size(64, 22)
        Me.TB_GoTo_Y.TabIndex = 566
        Me.TB_GoTo_Y.Text = "0.000"
        Me.TB_GoTo_Y.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Btn_YFastBack
        '
        Me.Btn_YFastBack.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_YFastBack.Location = New System.Drawing.Point(96, 6)
        Me.Btn_YFastBack.Name = "Btn_YFastBack"
        Me.Btn_YFastBack.Size = New System.Drawing.Size(36, 44)
        Me.Btn_YFastBack.TabIndex = 564
        Me.Btn_YFastBack.Text = "▲" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "▲"
        Me.Btn_YFastBack.UseVisualStyleBackColor = True
        '
        'TB_GoTo_X
        '
        Me.TB_GoTo_X.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TB_GoTo_X.Location = New System.Drawing.Point(14, 231)
        Me.TB_GoTo_X.Name = "TB_GoTo_X"
        Me.TB_GoTo_X.Size = New System.Drawing.Size(64, 22)
        Me.TB_GoTo_X.TabIndex = 565
        Me.TB_GoTo_X.Text = "0.000"
        Me.TB_GoTo_X.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Btn_XFastLeft
        '
        Me.Btn_XFastLeft.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_XFastLeft.Location = New System.Drawing.Point(3, 93)
        Me.Btn_XFastLeft.Name = "Btn_XFastLeft"
        Me.Btn_XFastLeft.Size = New System.Drawing.Size(48, 34)
        Me.Btn_XFastLeft.TabIndex = 563
        Me.Btn_XFastLeft.Text = "◄◄"
        Me.Btn_XFastLeft.UseVisualStyleBackColor = True
        '
        'Btn_YFastFront
        '
        Me.Btn_YFastFront.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_YFastFront.Location = New System.Drawing.Point(96, 174)
        Me.Btn_YFastFront.Name = "Btn_YFastFront"
        Me.Btn_YFastFront.Size = New System.Drawing.Size(36, 44)
        Me.Btn_YFastFront.TabIndex = 562
        Me.Btn_YFastFront.Text = "▼" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "▼"
        Me.Btn_YFastFront.UseVisualStyleBackColor = True
        '
        'Btn_XFastRight
        '
        Me.Btn_XFastRight.Font = New System.Drawing.Font("Arial Narrow", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_XFastRight.Location = New System.Drawing.Point(181, 93)
        Me.Btn_XFastRight.Name = "Btn_XFastRight"
        Me.Btn_XFastRight.Size = New System.Drawing.Size(49, 34)
        Me.Btn_XFastRight.TabIndex = 561
        Me.Btn_XFastRight.Text = "►►"
        Me.Btn_XFastRight.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Btn_XFastRight.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage
        Me.Btn_XFastRight.UseVisualStyleBackColor = True
        '
        'Btn_XSlowRight
        '
        Me.Btn_XSlowRight.Font = New System.Drawing.Font("Arial Narrow", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_XSlowRight.Location = New System.Drawing.Point(135, 93)
        Me.Btn_XSlowRight.Name = "Btn_XSlowRight"
        Me.Btn_XSlowRight.Size = New System.Drawing.Size(42, 34)
        Me.Btn_XSlowRight.TabIndex = 560
        Me.Btn_XSlowRight.Text = "►╢"
        Me.Btn_XSlowRight.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Btn_XSlowRight.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage
        Me.Btn_XSlowRight.UseVisualStyleBackColor = True
        '
        'Btn_YSlowBack
        '
        Me.Btn_YSlowBack.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_YSlowBack.Location = New System.Drawing.Point(96, 52)
        Me.Btn_YSlowBack.Name = "Btn_YSlowBack"
        Me.Btn_YSlowBack.Size = New System.Drawing.Size(36, 39)
        Me.Btn_YSlowBack.TabIndex = 559
        Me.Btn_YSlowBack.Text = "╤" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "▲"
        Me.Btn_YSlowBack.UseVisualStyleBackColor = True
        '
        'Btn_YSlowFront
        '
        Me.Btn_YSlowFront.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_YSlowFront.Location = New System.Drawing.Point(96, 131)
        Me.Btn_YSlowFront.Name = "Btn_YSlowFront"
        Me.Btn_YSlowFront.Size = New System.Drawing.Size(36, 40)
        Me.Btn_YSlowFront.TabIndex = 558
        Me.Btn_YSlowFront.Text = "▼" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "╧"
        Me.Btn_YSlowFront.UseVisualStyleBackColor = True
        '
        'Btn_XSlowLeft
        '
        Me.Btn_XSlowLeft.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_XSlowLeft.Location = New System.Drawing.Point(52, 93)
        Me.Btn_XSlowLeft.Name = "Btn_XSlowLeft"
        Me.Btn_XSlowLeft.Size = New System.Drawing.Size(42, 34)
        Me.Btn_XSlowLeft.TabIndex = 557
        Me.Btn_XSlowLeft.Text = "╟◄"
        Me.Btn_XSlowLeft.UseVisualStyleBackColor = True
        '
        'Btn_EStop
        '
        Me.Btn_EStop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Btn_EStop.Location = New System.Drawing.Point(12, 431)
        Me.Btn_EStop.Name = "Btn_EStop"
        Me.Btn_EStop.Size = New System.Drawing.Size(75, 52)
        Me.Btn_EStop.TabIndex = 570
        Me.Btn_EStop.Text = "E Stop"
        Me.Btn_EStop.UseVisualStyleBackColor = True
        '
        'Btn_ComConnect
        '
        Me.Btn_ComConnect.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_ComConnect.Location = New System.Drawing.Point(64, 648)
        Me.Btn_ComConnect.Name = "Btn_ComConnect"
        Me.Btn_ComConnect.Size = New System.Drawing.Size(132, 52)
        Me.Btn_ComConnect.TabIndex = 569
        Me.Btn_ComConnect.Text = "Connect Tester"
        Me.Btn_ComConnect.UseVisualStyleBackColor = True
        '
        'GB_MachineConnect
        '
        Me.GB_MachineConnect.Controls.Add(Me.cbPortName)
        Me.GB_MachineConnect.Controls.Add(Me.Btn_Refresh_MainMachineCOMS)
        Me.GB_MachineConnect.Location = New System.Drawing.Point(64, 509)
        Me.GB_MachineConnect.Name = "GB_MachineConnect"
        Me.GB_MachineConnect.Size = New System.Drawing.Size(139, 123)
        Me.GB_MachineConnect.TabIndex = 572
        Me.GB_MachineConnect.TabStop = False
        Me.GB_MachineConnect.Text = "Multitek MLT - 2"
        '
        'cbPortName
        '
        Me.cbPortName.FormattingEnabled = True
        Me.cbPortName.Location = New System.Drawing.Point(9, 19)
        Me.cbPortName.Name = "cbPortName"
        Me.cbPortName.Size = New System.Drawing.Size(122, 21)
        Me.cbPortName.TabIndex = 509
        '
        'Btn_Refresh_MainMachineCOMS
        '
        Me.Btn_Refresh_MainMachineCOMS.Location = New System.Drawing.Point(9, 43)
        Me.Btn_Refresh_MainMachineCOMS.Margin = New System.Windows.Forms.Padding(0)
        Me.Btn_Refresh_MainMachineCOMS.Name = "Btn_Refresh_MainMachineCOMS"
        Me.Btn_Refresh_MainMachineCOMS.Size = New System.Drawing.Size(122, 26)
        Me.Btn_Refresh_MainMachineCOMS.TabIndex = 510
        Me.Btn_Refresh_MainMachineCOMS.Text = "Refresh"
        Me.Btn_Refresh_MainMachineCOMS.UseVisualStyleBackColor = True
        '
        'WsmbsControl1
        '
        Me.WsmbsControl1.BaudRate = 9600
        Me.WsmbsControl1.DataBits = 7
        Me.WsmbsControl1.DTREnable = False
        Me.WsmbsControl1.Mode = WSMBS.Mode.ASCII
        Me.WsmbsControl1.Parity = WSMBS.Parity.Even
        Me.WsmbsControl1.PortName = "COM3"
        Me.WsmbsControl1.RemoveEcho = False
        Me.WsmbsControl1.ResponseTimeout = 1000
        Me.WsmbsControl1.RTSEnable = False
        Me.WsmbsControl1.StopBits = 1
        '
        'Timer1
        '
        Me.Timer1.Interval = 400
        '
        'TB_PosY
        '
        Me.TB_PosY.Location = New System.Drawing.Point(315, 377)
        Me.TB_PosY.Name = "TB_PosY"
        Me.TB_PosY.Size = New System.Drawing.Size(58, 20)
        Me.TB_PosY.TabIndex = 597
        '
        'TB_PosX
        '
        Me.TB_PosX.Location = New System.Drawing.Point(152, 377)
        Me.TB_PosX.Name = "TB_PosX"
        Me.TB_PosX.Size = New System.Drawing.Size(58, 20)
        Me.TB_PosX.TabIndex = 596
        '
        'Btn_CurX
        '
        Me.Btn_CurX.Location = New System.Drawing.Point(110, 375)
        Me.Btn_CurX.Name = "Btn_CurX"
        Me.Btn_CurX.Size = New System.Drawing.Size(36, 23)
        Me.Btn_CurX.TabIndex = 598
        Me.Btn_CurX.Text = "X"
        Me.Btn_CurX.UseVisualStyleBackColor = True
        '
        'Btn_CurY
        '
        Me.Btn_CurY.Location = New System.Drawing.Point(259, 377)
        Me.Btn_CurY.Name = "Btn_CurY"
        Me.Btn_CurY.Size = New System.Drawing.Size(36, 23)
        Me.Btn_CurY.TabIndex = 599
        Me.Btn_CurY.Text = "Y"
        Me.Btn_CurY.UseVisualStyleBackColor = True
        '
        'Frm_Machine_Comm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(394, 921)
        Me.Controls.Add(Me.Btn_CurY)
        Me.Controls.Add(Me.Btn_CurX)
        Me.Controls.Add(Me.TB_PosY)
        Me.Controls.Add(Me.TB_PosX)
        Me.Controls.Add(Me.GB_MachineConnect)
        Me.Controls.Add(Me.Btn_EStop)
        Me.Controls.Add(Me.Btn_ComConnect)
        Me.Controls.Add(Me.Pnl_MotorisedStage)
        Me.Controls.Add(Me.GB_ZAxis)
        Me.Location = New System.Drawing.Point(1500, 0)
        Me.Name = "Frm_Machine_Comm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Multitek PT 300 E Control Panel"
        Me.TopMost = True
        Me.GB_ZAxis.ResumeLayout(False)
        Me.Pnl_MotorisedStage.ResumeLayout(False)
        Me.Pnl_MotorisedStage.PerformLayout()
        Me.GB_MachineConnect.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GB_ZAxis As GroupBox
    Friend WithEvents Btn_FocusDownFast As Button
    Friend WithEvents Btn_FocusDownSlow As Button
    Friend WithEvents Btn_FocusUpFast As Button
    Friend WithEvents Btn_FocusUPSlow As Button
    Friend WithEvents Pnl_MotorisedStage As Panel
    Friend WithEvents Btn_GoTo_Incremental As Button
    Friend WithEvents TB_GoTo_Y As TextBox
    Friend WithEvents Btn_YFastBack As Button
    Friend WithEvents TB_GoTo_X As TextBox
    Friend WithEvents Btn_XFastLeft As Button
    Friend WithEvents Btn_YFastFront As Button
    Friend WithEvents Btn_XFastRight As Button
    Friend WithEvents Btn_XSlowRight As Button
    Friend WithEvents Btn_YSlowBack As Button
    Friend WithEvents Btn_YSlowFront As Button
    Friend WithEvents Btn_XSlowLeft As Button
    Friend WithEvents Btn_EStop As Button
    Friend WithEvents Btn_ComConnect As Button
    Friend WithEvents GB_MachineConnect As GroupBox
    Friend WithEvents cbPortName As ComboBox
    Friend WithEvents Btn_Refresh_MainMachineCOMS As Button
    Friend WithEvents WsmbsControl1 As WSMBS.WSMBSControl
    Friend WithEvents Timer1 As Timer
    Friend WithEvents TB_PosY As TextBox
    Friend WithEvents TB_PosX As TextBox
    Friend WithEvents Btn_CurX As Button
    Friend WithEvents Btn_CurY As Button
End Class
