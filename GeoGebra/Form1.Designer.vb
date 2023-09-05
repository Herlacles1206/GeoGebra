<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.rad_center = New System.Windows.Forms.RadioButton()
        Me.rad_max = New System.Windows.Forms.RadioButton()
        Me.rad_min = New System.Windows.Forms.RadioButton()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btn_finish = New System.Windows.Forms.Button()
        Me.btn_cancel_all = New System.Windows.Forms.Button()
        Me.btn_cancel_last = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.btn_tool_open = New System.Windows.Forms.Button()
        Me.btn_tool_clearall = New System.Windows.Forms.Button()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FILEToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OPENCAMERAToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CLOSECAMERAToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EXPORTREPORTToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.btn_detect_circle = New System.Windows.Forms.Button()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.btn_annotation = New System.Windows.Forms.Button()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.btn_detect_line = New System.Windows.Forms.Button()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.btn_select = New System.Windows.Forms.Button()
        Me.btn_calc_dist = New System.Windows.Forms.Button()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.btn_zoom_out = New System.Windows.Forms.Button()
        Me.btn_zoom_in = New System.Windows.Forms.Button()
        Me.btn_extand = New System.Windows.Forms.Button()
        Me.btn_erase = New System.Windows.Forms.Button()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.btn_fit_arc = New System.Windows.Forms.Button()
        Me.btn_fit_circle = New System.Windows.Forms.Button()
        Me.btn_fit_line = New System.Windows.Forms.Button()
        Me.CameraResolutionsCB = New System.Windows.Forms.ComboBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.btn_circle_fixed = New System.Windows.Forms.Button()
        Me.btn_angle_fixed = New System.Windows.Forms.Button()
        Me.btn_circle_center = New System.Windows.Forms.Button()
        Me.btn_angle = New System.Windows.Forms.Button()
        Me.btn_line = New System.Windows.Forms.Button()
        Me.btn_point = New System.Windows.Forms.Button()
        Me.btn_move = New System.Windows.Forms.Button()
        Me.lab_circle_center = New System.Windows.Forms.Label()
        Me.lab_angle = New System.Windows.Forms.Label()
        Me.lab_line = New System.Windows.Forms.Label()
        Me.lab_point = New System.Windows.Forms.Label()
        Me.lab_move = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.pic_main = New System.Windows.Forms.PictureBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txt_counter = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txt_y = New System.Windows.Forms.TextBox()
        Me.txt_x = New System.Windows.Forms.TextBox()
        Me.dgv_pos = New System.Windows.Forms.DataGridView()
        Me.No = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.X = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Y = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Panel1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.pic_main, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        CType(Me.dgv_pos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.Controls.Add(Me.GroupBox2)
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.Button6)
        Me.Panel1.Controls.Add(Me.Button5)
        Me.Panel1.Controls.Add(Me.Button4)
        Me.Panel1.Controls.Add(Me.Button3)
        Me.Panel1.Controls.Add(Me.btn_tool_open)
        Me.Panel1.Controls.Add(Me.btn_tool_clearall)
        Me.Panel1.Controls.Add(Me.MenuStrip1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1446, 83)
        Me.Panel1.TabIndex = 0
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.rad_center)
        Me.GroupBox2.Controls.Add(Me.rad_max)
        Me.GroupBox2.Controls.Add(Me.rad_min)
        Me.GroupBox2.Location = New System.Drawing.Point(1138, 19)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(204, 50)
        Me.GroupBox2.TabIndex = 12
        Me.GroupBox2.TabStop = False
        '
        'rad_center
        '
        Me.rad_center.AutoSize = True
        Me.rad_center.Location = New System.Drawing.Point(136, 22)
        Me.rad_center.Name = "rad_center"
        Me.rad_center.Size = New System.Drawing.Size(56, 17)
        Me.rad_center.TabIndex = 13
        Me.rad_center.TabStop = True
        Me.rad_center.Text = "Center"
        Me.rad_center.UseVisualStyleBackColor = True
        '
        'rad_max
        '
        Me.rad_max.AutoSize = True
        Me.rad_max.Location = New System.Drawing.Point(76, 22)
        Me.rad_max.Name = "rad_max"
        Me.rad_max.Size = New System.Drawing.Size(45, 17)
        Me.rad_max.TabIndex = 1
        Me.rad_max.TabStop = True
        Me.rad_max.Text = "Max"
        Me.rad_max.UseVisualStyleBackColor = True
        '
        'rad_min
        '
        Me.rad_min.AutoSize = True
        Me.rad_min.Location = New System.Drawing.Point(18, 22)
        Me.rad_min.Name = "rad_min"
        Me.rad_min.Size = New System.Drawing.Size(42, 17)
        Me.rad_min.TabIndex = 0
        Me.rad_min.TabStop = True
        Me.rad_min.Text = "Min"
        Me.rad_min.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.DimGray
        Me.Label10.Location = New System.Drawing.Point(777, 35)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(48, 16)
        Me.Label10.TabIndex = 9
        Me.Label10.Text = "Setting"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.DimGray
        Me.Label9.Location = New System.Drawing.Point(623, 35)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(84, 16)
        Me.Label9.TabIndex = 8
        Me.Label9.Text = "Print Preview"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.DimGray
        Me.Label8.Location = New System.Drawing.Point(476, 33)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(43, 16)
        Me.Label8.TabIndex = 7
        Me.Label8.Text = "Share"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.DimGray
        Me.Label7.Location = New System.Drawing.Point(326, 35)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(39, 16)
        Me.Label7.TabIndex = 7
        Me.Label7.Text = "Save"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.DimGray
        Me.Label6.Location = New System.Drawing.Point(184, 35)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(40, 16)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "Open"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btn_finish)
        Me.GroupBox1.Controls.Add(Me.btn_cancel_all)
        Me.GroupBox1.Controls.Add(Me.btn_cancel_last)
        Me.GroupBox1.Location = New System.Drawing.Point(881, 19)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(251, 50)
        Me.GroupBox1.TabIndex = 11
        Me.GroupBox1.TabStop = False
        '
        'btn_finish
        '
        Me.btn_finish.Location = New System.Drawing.Point(166, 19)
        Me.btn_finish.Name = "btn_finish"
        Me.btn_finish.Size = New System.Drawing.Size(75, 23)
        Me.btn_finish.TabIndex = 2
        Me.btn_finish.Text = "Finish"
        Me.btn_finish.UseVisualStyleBackColor = True
        '
        'btn_cancel_all
        '
        Me.btn_cancel_all.Location = New System.Drawing.Point(87, 19)
        Me.btn_cancel_all.Name = "btn_cancel_all"
        Me.btn_cancel_all.Size = New System.Drawing.Size(75, 23)
        Me.btn_cancel_all.TabIndex = 1
        Me.btn_cancel_all.Text = "Cancel All"
        Me.btn_cancel_all.UseVisualStyleBackColor = True
        '
        'btn_cancel_last
        '
        Me.btn_cancel_last.Location = New System.Drawing.Point(8, 19)
        Me.btn_cancel_last.Name = "btn_cancel_last"
        Me.btn_cancel_last.Size = New System.Drawing.Size(75, 23)
        Me.btn_cancel_last.TabIndex = 0
        Me.btn_cancel_last.Text = "Cancel Last"
        Me.btn_cancel_last.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.DimGray
        Me.Label5.Location = New System.Drawing.Point(64, 35)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(57, 16)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "Clear All"
        '
        'Button6
        '
        Me.Button6.BackColor = System.Drawing.Color.White
        Me.Button6.BackgroundImage = CType(resources.GetObject("Button6.BackgroundImage"), System.Drawing.Image)
        Me.Button6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button6.Location = New System.Drawing.Point(587, 26)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(30, 30)
        Me.Button6.TabIndex = 5
        Me.Button6.UseVisualStyleBackColor = False
        '
        'Button5
        '
        Me.Button5.BackColor = System.Drawing.Color.White
        Me.Button5.BackgroundImage = CType(resources.GetObject("Button5.BackgroundImage"), System.Drawing.Image)
        Me.Button5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button5.Location = New System.Drawing.Point(741, 26)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(30, 30)
        Me.Button5.TabIndex = 4
        Me.Button5.UseVisualStyleBackColor = False
        '
        'Button4
        '
        Me.Button4.BackColor = System.Drawing.Color.White
        Me.Button4.BackgroundImage = CType(resources.GetObject("Button4.BackgroundImage"), System.Drawing.Image)
        Me.Button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button4.Location = New System.Drawing.Point(440, 26)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(30, 30)
        Me.Button4.TabIndex = 3
        Me.Button4.UseVisualStyleBackColor = False
        '
        'Button3
        '
        Me.Button3.BackColor = System.Drawing.Color.White
        Me.Button3.BackgroundImage = CType(resources.GetObject("Button3.BackgroundImage"), System.Drawing.Image)
        Me.Button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button3.Location = New System.Drawing.Point(290, 26)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(30, 30)
        Me.Button3.TabIndex = 2
        Me.Button3.UseVisualStyleBackColor = False
        '
        'btn_tool_open
        '
        Me.btn_tool_open.BackColor = System.Drawing.Color.White
        Me.btn_tool_open.BackgroundImage = CType(resources.GetObject("btn_tool_open.BackgroundImage"), System.Drawing.Image)
        Me.btn_tool_open.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn_tool_open.Location = New System.Drawing.Point(148, 26)
        Me.btn_tool_open.Name = "btn_tool_open"
        Me.btn_tool_open.Size = New System.Drawing.Size(30, 30)
        Me.btn_tool_open.TabIndex = 1
        Me.btn_tool_open.UseVisualStyleBackColor = False
        '
        'btn_tool_clearall
        '
        Me.btn_tool_clearall.BackColor = System.Drawing.Color.White
        Me.btn_tool_clearall.BackgroundImage = CType(resources.GetObject("btn_tool_clearall.BackgroundImage"), System.Drawing.Image)
        Me.btn_tool_clearall.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn_tool_clearall.Location = New System.Drawing.Point(28, 26)
        Me.btn_tool_clearall.Name = "btn_tool_clearall"
        Me.btn_tool_clearall.Size = New System.Drawing.Size(30, 30)
        Me.btn_tool_clearall.TabIndex = 0
        Me.btn_tool_clearall.UseVisualStyleBackColor = False
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FILEToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1446, 24)
        Me.MenuStrip1.TabIndex = 10
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FILEToolStripMenuItem
        '
        Me.FILEToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OPENCAMERAToolStripMenuItem, Me.CLOSECAMERAToolStripMenuItem, Me.EXPORTREPORTToolStripMenuItem})
        Me.FILEToolStripMenuItem.Name = "FILEToolStripMenuItem"
        Me.FILEToolStripMenuItem.Size = New System.Drawing.Size(40, 20)
        Me.FILEToolStripMenuItem.Text = "FILE"
        '
        'OPENCAMERAToolStripMenuItem
        '
        Me.OPENCAMERAToolStripMenuItem.Name = "OPENCAMERAToolStripMenuItem"
        Me.OPENCAMERAToolStripMenuItem.Size = New System.Drawing.Size(160, 22)
        Me.OPENCAMERAToolStripMenuItem.Text = "OPEN CAMERA"
        '
        'CLOSECAMERAToolStripMenuItem
        '
        Me.CLOSECAMERAToolStripMenuItem.Name = "CLOSECAMERAToolStripMenuItem"
        Me.CLOSECAMERAToolStripMenuItem.Size = New System.Drawing.Size(160, 22)
        Me.CLOSECAMERAToolStripMenuItem.Text = "CLOSE CAMERA"
        '
        'EXPORTREPORTToolStripMenuItem
        '
        Me.EXPORTREPORTToolStripMenuItem.Name = "EXPORTREPORTToolStripMenuItem"
        Me.EXPORTREPORTToolStripMenuItem.Size = New System.Drawing.Size(160, 22)
        Me.EXPORTREPORTToolStripMenuItem.Text = "EXPORT REPORT"
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Panel2.AutoScroll = True
        Me.Panel2.BackColor = System.Drawing.Color.White
        Me.Panel2.Controls.Add(Me.Label27)
        Me.Panel2.Controls.Add(Me.btn_detect_circle)
        Me.Panel2.Controls.Add(Me.Label26)
        Me.Panel2.Controls.Add(Me.btn_annotation)
        Me.Panel2.Controls.Add(Me.Label25)
        Me.Panel2.Controls.Add(Me.btn_detect_line)
        Me.Panel2.Controls.Add(Me.Label24)
        Me.Panel2.Controls.Add(Me.Label23)
        Me.Panel2.Controls.Add(Me.btn_select)
        Me.Panel2.Controls.Add(Me.btn_calc_dist)
        Me.Panel2.Controls.Add(Me.Label22)
        Me.Panel2.Controls.Add(Me.Label21)
        Me.Panel2.Controls.Add(Me.Label20)
        Me.Panel2.Controls.Add(Me.Label19)
        Me.Panel2.Controls.Add(Me.btn_zoom_out)
        Me.Panel2.Controls.Add(Me.btn_zoom_in)
        Me.Panel2.Controls.Add(Me.btn_extand)
        Me.Panel2.Controls.Add(Me.btn_erase)
        Me.Panel2.Controls.Add(Me.Label18)
        Me.Panel2.Controls.Add(Me.btn_fit_arc)
        Me.Panel2.Controls.Add(Me.btn_fit_circle)
        Me.Panel2.Controls.Add(Me.btn_fit_line)
        Me.Panel2.Controls.Add(Me.CameraResolutionsCB)
        Me.Panel2.Controls.Add(Me.Label12)
        Me.Panel2.Controls.Add(Me.Label11)
        Me.Panel2.Controls.Add(Me.btn_circle_fixed)
        Me.Panel2.Controls.Add(Me.btn_angle_fixed)
        Me.Panel2.Controls.Add(Me.btn_circle_center)
        Me.Panel2.Controls.Add(Me.btn_angle)
        Me.Panel2.Controls.Add(Me.btn_line)
        Me.Panel2.Controls.Add(Me.btn_point)
        Me.Panel2.Controls.Add(Me.btn_move)
        Me.Panel2.Controls.Add(Me.lab_circle_center)
        Me.Panel2.Controls.Add(Me.lab_angle)
        Me.Panel2.Controls.Add(Me.lab_line)
        Me.Panel2.Controls.Add(Me.lab_point)
        Me.Panel2.Controls.Add(Me.lab_move)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Location = New System.Drawing.Point(1, 86)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(291, 972)
        Me.Panel2.TabIndex = 1
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.ForeColor = System.Drawing.Color.DimGray
        Me.Label27.Location = New System.Drawing.Point(207, 830)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(68, 13)
        Me.Label27.TabIndex = 46
        Me.Label27.Text = "Detect Circle"
        '
        'btn_detect_circle
        '
        Me.btn_detect_circle.BackColor = System.Drawing.Color.White
        Me.btn_detect_circle.BackgroundImage = Global.GeoGebra.My.Resources.Resources.mode_conic5
        Me.btn_detect_circle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn_detect_circle.Location = New System.Drawing.Point(222, 797)
        Me.btn_detect_circle.Name = "btn_detect_circle"
        Me.btn_detect_circle.Size = New System.Drawing.Size(30, 30)
        Me.btn_detect_circle.TabIndex = 45
        Me.btn_detect_circle.UseVisualStyleBackColor = False
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.ForeColor = System.Drawing.Color.DimGray
        Me.Label26.Location = New System.Drawing.Point(21, 830)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(58, 13)
        Me.Label26.TabIndex = 44
        Me.Label26.Text = "Annotation"
        '
        'btn_annotation
        '
        Me.btn_annotation.BackColor = System.Drawing.Color.White
        Me.btn_annotation.BackgroundImage = Global.GeoGebra.My.Resources.Resources.mode_text
        Me.btn_annotation.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn_annotation.Location = New System.Drawing.Point(35, 797)
        Me.btn_annotation.Name = "btn_annotation"
        Me.btn_annotation.Size = New System.Drawing.Size(30, 30)
        Me.btn_annotation.TabIndex = 43
        Me.btn_annotation.UseVisualStyleBackColor = False
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.ForeColor = System.Drawing.Color.DimGray
        Me.Label25.Location = New System.Drawing.Point(119, 830)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(62, 13)
        Me.Label25.TabIndex = 42
        Me.Label25.Text = "Detect Line"
        '
        'btn_detect_line
        '
        Me.btn_detect_line.BackColor = System.Drawing.Color.White
        Me.btn_detect_line.BackgroundImage = Global.GeoGebra.My.Resources.Resources.mode_fitline
        Me.btn_detect_line.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn_detect_line.Location = New System.Drawing.Point(130, 797)
        Me.btn_detect_line.Name = "btn_detect_line"
        Me.btn_detect_line.Size = New System.Drawing.Size(30, 30)
        Me.btn_detect_line.TabIndex = 41
        Me.btn_detect_line.UseVisualStyleBackColor = False
        '
        'Label24
        '
        Me.Label24.ForeColor = System.Drawing.Color.DimGray
        Me.Label24.Location = New System.Drawing.Point(207, 763)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(54, 29)
        Me.Label24.TabIndex = 40
        Me.Label24.Text = "Calculate Distance"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.ForeColor = System.Drawing.Color.DimGray
        Me.Label23.Location = New System.Drawing.Point(124, 763)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(37, 13)
        Me.Label23.TabIndex = 39
        Me.Label23.Text = "Select"
        '
        'btn_select
        '
        Me.btn_select.BackColor = System.Drawing.Color.White
        Me.btn_select.BackgroundImage = Global.GeoGebra.My.Resources.Resources.arrow_cursor_grabbing
        Me.btn_select.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn_select.Location = New System.Drawing.Point(130, 730)
        Me.btn_select.Name = "btn_select"
        Me.btn_select.Size = New System.Drawing.Size(30, 30)
        Me.btn_select.TabIndex = 38
        Me.btn_select.UseVisualStyleBackColor = False
        '
        'btn_calc_dist
        '
        Me.btn_calc_dist.BackgroundImage = Global.GeoGebra.My.Resources.Resources.mode_distance
        Me.btn_calc_dist.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn_calc_dist.Location = New System.Drawing.Point(222, 730)
        Me.btn_calc_dist.Name = "btn_calc_dist"
        Me.btn_calc_dist.Size = New System.Drawing.Size(30, 30)
        Me.btn_calc_dist.TabIndex = 37
        Me.btn_calc_dist.UseVisualStyleBackColor = True
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.ForeColor = System.Drawing.Color.DimGray
        Me.Label22.Location = New System.Drawing.Point(28, 763)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(54, 13)
        Me.Label22.TabIndex = 36
        Me.Label22.Text = "Zoom Out"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.ForeColor = System.Drawing.Color.DimGray
        Me.Label21.Location = New System.Drawing.Point(215, 697)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(46, 13)
        Me.Label21.TabIndex = 35
        Me.Label21.Text = "Zoom In"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.ForeColor = System.Drawing.Color.DimGray
        Me.Label20.Location = New System.Drawing.Point(124, 697)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(43, 13)
        Me.Label20.TabIndex = 34
        Me.Label20.Text = "Expand"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.ForeColor = System.Drawing.Color.DimGray
        Me.Label19.Location = New System.Drawing.Point(32, 697)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(34, 13)
        Me.Label19.TabIndex = 33
        Me.Label19.Text = "Erase"
        '
        'btn_zoom_out
        '
        Me.btn_zoom_out.BackgroundImage = Global.GeoGebra.My.Resources.Resources.mode_zoomout
        Me.btn_zoom_out.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn_zoom_out.Location = New System.Drawing.Point(36, 730)
        Me.btn_zoom_out.Name = "btn_zoom_out"
        Me.btn_zoom_out.Size = New System.Drawing.Size(30, 30)
        Me.btn_zoom_out.TabIndex = 32
        Me.btn_zoom_out.UseVisualStyleBackColor = True
        '
        'btn_zoom_in
        '
        Me.btn_zoom_in.BackgroundImage = Global.GeoGebra.My.Resources.Resources.mode_zoomin
        Me.btn_zoom_in.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn_zoom_in.Location = New System.Drawing.Point(222, 664)
        Me.btn_zoom_in.Name = "btn_zoom_in"
        Me.btn_zoom_in.Size = New System.Drawing.Size(30, 30)
        Me.btn_zoom_in.TabIndex = 31
        Me.btn_zoom_in.UseVisualStyleBackColor = True
        '
        'btn_extand
        '
        Me.btn_extand.BackgroundImage = Global.GeoGebra.My.Resources.Resources.mode_expand
        Me.btn_extand.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn_extand.Location = New System.Drawing.Point(130, 664)
        Me.btn_extand.Name = "btn_extand"
        Me.btn_extand.Size = New System.Drawing.Size(30, 30)
        Me.btn_extand.TabIndex = 30
        Me.btn_extand.UseVisualStyleBackColor = True
        '
        'btn_erase
        '
        Me.btn_erase.BackColor = System.Drawing.Color.White
        Me.btn_erase.BackgroundImage = Global.GeoGebra.My.Resources.Resources.mode_delete
        Me.btn_erase.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn_erase.Location = New System.Drawing.Point(36, 664)
        Me.btn_erase.Name = "btn_erase"
        Me.btn_erase.Size = New System.Drawing.Size(30, 30)
        Me.btn_erase.TabIndex = 29
        Me.btn_erase.UseVisualStyleBackColor = False
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.DimGray
        Me.Label18.Location = New System.Drawing.Point(12, 632)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(67, 20)
        Me.Label18.TabIndex = 28
        Me.Label18.Text = "Phase 3"
        '
        'btn_fit_arc
        '
        Me.btn_fit_arc.BackgroundImage = Global.GeoGebra.My.Resources.Resources.mode_circumcirclearc3
        Me.btn_fit_arc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn_fit_arc.Location = New System.Drawing.Point(224, 345)
        Me.btn_fit_arc.Name = "btn_fit_arc"
        Me.btn_fit_arc.Size = New System.Drawing.Size(30, 30)
        Me.btn_fit_arc.TabIndex = 27
        Me.btn_fit_arc.UseVisualStyleBackColor = True
        '
        'btn_fit_circle
        '
        Me.btn_fit_circle.BackgroundImage = Global.GeoGebra.My.Resources.Resources.mode_conic5
        Me.btn_fit_circle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn_fit_circle.Location = New System.Drawing.Point(128, 345)
        Me.btn_fit_circle.Name = "btn_fit_circle"
        Me.btn_fit_circle.Size = New System.Drawing.Size(30, 30)
        Me.btn_fit_circle.TabIndex = 26
        Me.btn_fit_circle.UseVisualStyleBackColor = True
        '
        'btn_fit_line
        '
        Me.btn_fit_line.BackgroundImage = Global.GeoGebra.My.Resources.Resources.mode_fitline
        Me.btn_fit_line.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn_fit_line.Location = New System.Drawing.Point(36, 345)
        Me.btn_fit_line.Name = "btn_fit_line"
        Me.btn_fit_line.Size = New System.Drawing.Size(30, 30)
        Me.btn_fit_line.TabIndex = 25
        Me.btn_fit_line.UseVisualStyleBackColor = True
        '
        'CameraResolutionsCB
        '
        Me.CameraResolutionsCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CameraResolutionsCB.FormattingEnabled = True
        Me.CameraResolutionsCB.Location = New System.Drawing.Point(36, 202)
        Me.CameraResolutionsCB.Name = "CameraResolutionsCB"
        Me.CameraResolutionsCB.Size = New System.Drawing.Size(121, 21)
        Me.CameraResolutionsCB.TabIndex = 24
        '
        'Label12
        '
        Me.Label12.ForeColor = System.Drawing.Color.DimGray
        Me.Label12.Location = New System.Drawing.Point(102, 522)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(75, 31)
        Me.Label12.TabIndex = 23
        Me.Label12.Text = "Circle: Center & Radius"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label11
        '
        Me.Label11.ForeColor = System.Drawing.Color.DimGray
        Me.Label11.Location = New System.Drawing.Point(24, 522)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(56, 31)
        Me.Label11.TabIndex = 22
        Me.Label11.Text = "Angle with Given size"
        '
        'btn_circle_fixed
        '
        Me.btn_circle_fixed.BackgroundImage = Global.GeoGebra.My.Resources.Resources.mode_circlepointradius1
        Me.btn_circle_fixed.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn_circle_fixed.Location = New System.Drawing.Point(128, 489)
        Me.btn_circle_fixed.Name = "btn_circle_fixed"
        Me.btn_circle_fixed.Size = New System.Drawing.Size(30, 30)
        Me.btn_circle_fixed.TabIndex = 21
        Me.btn_circle_fixed.UseVisualStyleBackColor = True
        '
        'btn_angle_fixed
        '
        Me.btn_angle_fixed.BackgroundImage = Global.GeoGebra.My.Resources.Resources.mode_anglefixed
        Me.btn_angle_fixed.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn_angle_fixed.Location = New System.Drawing.Point(36, 489)
        Me.btn_angle_fixed.Name = "btn_angle_fixed"
        Me.btn_angle_fixed.Size = New System.Drawing.Size(30, 30)
        Me.btn_angle_fixed.TabIndex = 20
        Me.btn_angle_fixed.UseVisualStyleBackColor = True
        '
        'btn_circle_center
        '
        Me.btn_circle_center.BackgroundImage = CType(resources.GetObject("btn_circle_center.BackgroundImage"), System.Drawing.Image)
        Me.btn_circle_center.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn_circle_center.Location = New System.Drawing.Point(130, 116)
        Me.btn_circle_center.Name = "btn_circle_center"
        Me.btn_circle_center.Size = New System.Drawing.Size(30, 30)
        Me.btn_circle_center.TabIndex = 19
        Me.ToolTip1.SetToolTip(Me.btn_circle_center, "Select center point, then point on circle")
        Me.btn_circle_center.UseVisualStyleBackColor = True
        '
        'btn_angle
        '
        Me.btn_angle.BackgroundImage = CType(resources.GetObject("btn_angle.BackgroundImage"), System.Drawing.Image)
        Me.btn_angle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn_angle.Location = New System.Drawing.Point(36, 116)
        Me.btn_angle.Name = "btn_angle"
        Me.btn_angle.Size = New System.Drawing.Size(30, 30)
        Me.btn_angle.TabIndex = 18
        Me.ToolTip1.SetToolTip(Me.btn_angle, "Select three points or two lines")
        Me.btn_angle.UseVisualStyleBackColor = True
        '
        'btn_line
        '
        Me.btn_line.BackgroundImage = CType(resources.GetObject("btn_line.BackgroundImage"), System.Drawing.Image)
        Me.btn_line.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn_line.Location = New System.Drawing.Point(224, 52)
        Me.btn_line.Name = "btn_line"
        Me.btn_line.Size = New System.Drawing.Size(30, 30)
        Me.btn_line.TabIndex = 17
        Me.ToolTip1.SetToolTip(Me.btn_line, "Select two points or positions")
        Me.btn_line.UseVisualStyleBackColor = True
        '
        'btn_point
        '
        Me.btn_point.BackgroundImage = CType(resources.GetObject("btn_point.BackgroundImage"), System.Drawing.Image)
        Me.btn_point.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn_point.Location = New System.Drawing.Point(130, 52)
        Me.btn_point.Name = "btn_point"
        Me.btn_point.Size = New System.Drawing.Size(30, 30)
        Me.btn_point.TabIndex = 16
        Me.ToolTip1.SetToolTip(Me.btn_point, "Select position or line, function or curve")
        Me.btn_point.UseVisualStyleBackColor = True
        '
        'btn_move
        '
        Me.btn_move.BackgroundImage = CType(resources.GetObject("btn_move.BackgroundImage"), System.Drawing.Image)
        Me.btn_move.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn_move.Location = New System.Drawing.Point(36, 52)
        Me.btn_move.Name = "btn_move"
        Me.btn_move.Size = New System.Drawing.Size(30, 30)
        Me.btn_move.TabIndex = 15
        Me.ToolTip1.SetToolTip(Me.btn_move, "Drag or select object")
        Me.btn_move.UseVisualStyleBackColor = True
        '
        'lab_circle_center
        '
        Me.lab_circle_center.ForeColor = System.Drawing.Color.DimGray
        Me.lab_circle_center.Location = New System.Drawing.Point(117, 149)
        Me.lab_circle_center.Name = "lab_circle_center"
        Me.lab_circle_center.Size = New System.Drawing.Size(60, 29)
        Me.lab_circle_center.TabIndex = 14
        Me.lab_circle_center.Text = "Circle with Center"
        Me.lab_circle_center.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lab_angle
        '
        Me.lab_angle.AutoSize = True
        Me.lab_angle.ForeColor = System.Drawing.Color.DimGray
        Me.lab_angle.Location = New System.Drawing.Point(34, 149)
        Me.lab_angle.Name = "lab_angle"
        Me.lab_angle.Size = New System.Drawing.Size(34, 13)
        Me.lab_angle.TabIndex = 13
        Me.lab_angle.Text = "Angle"
        '
        'lab_line
        '
        Me.lab_line.AutoSize = True
        Me.lab_line.ForeColor = System.Drawing.Color.DimGray
        Me.lab_line.Location = New System.Drawing.Point(227, 88)
        Me.lab_line.Name = "lab_line"
        Me.lab_line.Size = New System.Drawing.Size(27, 13)
        Me.lab_line.TabIndex = 12
        Me.lab_line.Text = "Line"
        '
        'lab_point
        '
        Me.lab_point.AutoSize = True
        Me.lab_point.ForeColor = System.Drawing.Color.DimGray
        Me.lab_point.Location = New System.Drawing.Point(127, 88)
        Me.lab_point.Name = "lab_point"
        Me.lab_point.Size = New System.Drawing.Size(31, 13)
        Me.lab_point.TabIndex = 11
        Me.lab_point.Text = "Point"
        '
        'lab_move
        '
        Me.lab_move.AutoSize = True
        Me.lab_move.ForeColor = System.Drawing.Color.DimGray
        Me.lab_move.Location = New System.Drawing.Point(34, 88)
        Me.lab_move.Name = "lab_move"
        Me.lab_move.Size = New System.Drawing.Size(34, 13)
        Me.lab_move.TabIndex = 10
        Me.lab_move.Text = "Move"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.DimGray
        Me.Label4.Location = New System.Drawing.Point(12, 462)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(71, 20)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Measure"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.DimGray
        Me.Label3.Location = New System.Drawing.Point(12, 313)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(63, 20)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Phase2"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.DimGray
        Me.Label2.Location = New System.Drawing.Point(12, 168)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 20)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Webcam"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.DimGray
        Me.Label1.Location = New System.Drawing.Point(12, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(90, 20)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Basic Tools"
        '
        'Panel3
        '
        Me.Panel3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel3.AutoScroll = True
        Me.Panel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel3.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Panel3.Controls.Add(Me.pic_main)
        Me.Panel3.Location = New System.Drawing.Point(290, 83)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(890, 975)
        Me.Panel3.TabIndex = 2
        '
        'pic_main
        '
        Me.pic_main.BackColor = System.Drawing.Color.White
        Me.pic_main.Location = New System.Drawing.Point(3, 3)
        Me.pic_main.Name = "pic_main"
        Me.pic_main.Size = New System.Drawing.Size(1024, 768)
        Me.pic_main.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.pic_main.TabIndex = 0
        Me.pic_main.TabStop = False
        '
        'Panel4
        '
        Me.Panel4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel4.BackColor = System.Drawing.Color.White
        Me.Panel4.Controls.Add(Me.Label17)
        Me.Panel4.Controls.Add(Me.Label16)
        Me.Panel4.Controls.Add(Me.Label15)
        Me.Panel4.Controls.Add(Me.txt_counter)
        Me.Panel4.Controls.Add(Me.Label14)
        Me.Panel4.Controls.Add(Me.Label13)
        Me.Panel4.Controls.Add(Me.txt_y)
        Me.Panel4.Controls.Add(Me.txt_x)
        Me.Panel4.Controls.Add(Me.dgv_pos)
        Me.Panel4.Location = New System.Drawing.Point(1186, 89)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(260, 691)
        Me.Panel4.TabIndex = 3
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.ForeColor = System.Drawing.Color.DimGray
        Me.Label17.Location = New System.Drawing.Point(218, 282)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(21, 13)
        Me.Label17.TabIndex = 19
        Me.Label17.Text = "um"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.ForeColor = System.Drawing.Color.DimGray
        Me.Label16.Location = New System.Drawing.Point(107, 282)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(21, 13)
        Me.Label16.TabIndex = 18
        Me.Label16.Text = "um"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.ForeColor = System.Drawing.Color.DimGray
        Me.Label15.Location = New System.Drawing.Point(6, 315)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(44, 13)
        Me.Label15.TabIndex = 17
        Me.Label15.Text = "Counter"
        '
        'txt_counter
        '
        Me.txt_counter.Location = New System.Drawing.Point(51, 312)
        Me.txt_counter.Name = "txt_counter"
        Me.txt_counter.Size = New System.Drawing.Size(69, 20)
        Me.txt_counter.TabIndex = 16
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.ForeColor = System.Drawing.Color.DimGray
        Me.Label14.Location = New System.Drawing.Point(139, 282)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(17, 13)
        Me.Label14.TabIndex = 15
        Me.Label14.Text = "Y:"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.ForeColor = System.Drawing.Color.DimGray
        Me.Label13.Location = New System.Drawing.Point(28, 282)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(17, 13)
        Me.Label13.TabIndex = 14
        Me.Label13.Text = "X:"
        '
        'txt_y
        '
        Me.txt_y.Location = New System.Drawing.Point(162, 278)
        Me.txt_y.Name = "txt_y"
        Me.txt_y.Size = New System.Drawing.Size(53, 20)
        Me.txt_y.TabIndex = 2
        '
        'txt_x
        '
        Me.txt_x.Location = New System.Drawing.Point(51, 278)
        Me.txt_x.Name = "txt_x"
        Me.txt_x.Size = New System.Drawing.Size(53, 20)
        Me.txt_x.TabIndex = 1
        '
        'dgv_pos
        '
        Me.dgv_pos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_pos.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.No, Me.X, Me.Y})
        Me.dgv_pos.Location = New System.Drawing.Point(6, 25)
        Me.dgv_pos.Name = "dgv_pos"
        Me.dgv_pos.Size = New System.Drawing.Size(249, 214)
        Me.dgv_pos.TabIndex = 0
        '
        'No
        '
        Me.No.HeaderText = "No"
        Me.No.Name = "No"
        '
        'X
        '
        Me.X.HeaderText = "X"
        Me.X.Name = "X"
        '
        'Y
        '
        Me.Y.HeaderText = "Y"
        Me.Y.Name = "Y"
        '
        'Timer1
        '
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(1446, 1061)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "MainForm"
        Me.Text = "GeoGebra"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.pic_main, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.dgv_pos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents pic_main As PictureBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents lab_circle_center As Label
    Friend WithEvents lab_angle As Label
    Friend WithEvents lab_line As Label
    Friend WithEvents lab_point As Label
    Friend WithEvents lab_move As Label
    Friend WithEvents btn_move As Button
    Friend WithEvents btn_circle_center As Button
    Friend WithEvents btn_angle As Button
    Friend WithEvents btn_line As Button
    Friend WithEvents btn_point As Button
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents btn_tool_clearall As Button
    Friend WithEvents btn_tool_open As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents Button5 As Button
    Friend WithEvents Button6 As Button
    Friend WithEvents Label5 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents btn_angle_fixed As Button
    Friend WithEvents btn_circle_fixed As Button
    Friend WithEvents Label12 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents FILEToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OPENCAMERAToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CLOSECAMERAToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents EXPORTREPORTToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CameraResolutionsCB As ComboBox
    Friend WithEvents btn_fit_line As Button
    Friend WithEvents btn_fit_arc As Button
    Friend WithEvents btn_fit_circle As Button
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Label15 As Label
    Friend WithEvents txt_counter As TextBox
    Friend WithEvents Label14 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents txt_y As TextBox
    Friend WithEvents txt_x As TextBox
    Friend WithEvents dgv_pos As DataGridView
    Friend WithEvents No As DataGridViewTextBoxColumn
    Friend WithEvents X As DataGridViewTextBoxColumn
    Friend WithEvents Y As DataGridViewTextBoxColumn
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents btn_finish As Button
    Friend WithEvents btn_cancel_all As Button
    Friend WithEvents btn_cancel_last As Button
    Friend WithEvents Label17 As Label
    Friend WithEvents Label16 As Label
    Friend WithEvents btn_erase As Button
    Friend WithEvents Label18 As Label
    Friend WithEvents btn_extand As Button
    Friend WithEvents btn_zoom_out As Button
    Friend WithEvents btn_zoom_in As Button
    Friend WithEvents Timer1 As Timer
    Friend WithEvents Label22 As Label
    Friend WithEvents Label21 As Label
    Friend WithEvents Label20 As Label
    Friend WithEvents Label19 As Label
    Friend WithEvents btn_calc_dist As Button
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents btn_select As Button
    Friend WithEvents Label24 As Label
    Friend WithEvents Label23 As Label
    Friend WithEvents rad_center As RadioButton
    Friend WithEvents rad_max As RadioButton
    Friend WithEvents rad_min As RadioButton
    Friend WithEvents btn_detect_line As Button
    Friend WithEvents Label25 As Label
    Friend WithEvents btn_annotation As Button
    Friend WithEvents Label26 As Label
    Friend WithEvents btn_detect_circle As Button
    Friend WithEvents Label27 As Label
End Class
