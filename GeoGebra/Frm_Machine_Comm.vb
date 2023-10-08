Imports System.ComponentModel

Public Class Frm_Machine_Comm

    Dim ComPortMainMachine As String

<<<<<<< HEAD
    Public enterKeyPressed As Boolean

=======
>>>>>>> f6d78bb321f8c4ecda94376d6249262d0340ce93
    Private Sub Btn_EStop_Click(sender As Object, e As EventArgs) Handles Btn_EStop.Click
        WsmbsControl1.WriteSingleCoil(1, 2096, 1) ' Set M48 ON
        Pause(100)
        WsmbsControl1.WriteSingleCoil(1, 2096, 0) ' Set M48 OFF
    End Sub

    Private Sub Frm_Machine_Comm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        WsmbsControl1.LicenseKey("22B8-91B6-7383-20CB-2422-8E7B")
        ComPortMainMachine = My.Settings.Mem_MachineComPort
        Btn_Refresh_MainMachineCOMS.Text = "Refresh / " & ComPortMainMachine
<<<<<<< HEAD

        'test
        TB_PosXAbs.Text = "10"
        TB_PosYAbs.Text = "10"

        TB_PosXInc.Text = "10"
        TB_PosYInc.Text = "10"
=======
>>>>>>> f6d78bb321f8c4ecda94376d6249262d0340ce93
    End Sub

    Private Sub Btn_ComConnect_Click(sender As Object, e As EventArgs) Handles Btn_ComConnect.Click
        Modbus_Connect()
    End Sub

    Sub Modbus_Connect()
        ' If CB_PCLink.CheckState = 1 And CB_Futuretech.CheckState = False Then
        If Me.Btn_ComConnect.BackColor <> Color.Aquamarine Then
            Dim Result As WSMBS.Result

            WsmbsControl1.Mode = WSMBS.Mode.ASCII
            WsmbsControl1.PortName = ComPortMainMachine
            WsmbsControl1.BaudRate = 9600
            WsmbsControl1.DataBits = 7
            WsmbsControl1.StopBits = 1
            WsmbsControl1.Parity = WSMBS.Parity.Even
            WsmbsControl1.ResponseTimeout = 1000   '1000ms
            Result = WsmbsControl1.Open()
            If Result <> WSMBS.Result.SUCCESS Then
                MessageBox.Show("Multitek MLT - 2 : " & WsmbsControl1.GetLastErrorString())
                Btn_ComConnect.Text = "Tester Disconnected"
                Btn_ComConnect.BackColor = Color.DarkGray
            Else
                Btn_ComConnect.Text = "Connected Multitek MLT - 2"
                Btn_ComConnect.BackColor = Color.Aquamarine
                'cbPortName.Enabled = False
                'Button_RefreshPortList.Enabled = False
                'OnloadTurretForce_Sync() '**************************CHECK********************
                'Frm_LiveMeasure.Show()
                Timer1.Start()
                'Force_Synchronize()
                'Lens_Synchronize()
            End If
            Exit Sub
        End If
        If Btn_ComConnect.BackColor = Color.Aquamarine Then

            Modbus_Disconnect()
            Btn_ComConnect.Text = "Tester Disconnected"
            cbPortName.Enabled = False
            Btn_Refresh_MainMachineCOMS.Enabled = False

        End If
        ' End If
    End Sub
    Sub Modbus_Disconnect()
        Timer1.Stop()
        Me.Btn_ComConnect.BackColor = Color.DarkGray
        Me.cbPortName.Enabled = True
        Me.Btn_Refresh_MainMachineCOMS.Enabled = True
        Me.WsmbsControl1.Close()
    End Sub

    Private Sub cbPortName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbPortName.SelectedIndexChanged
        ComPortMainMachine = cbPortName.Text
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim Result As WSMBS.Result
        Dim Registers As Short() = New Short(20) {}

        Result = WsmbsControl1.ReadHoldingRegisters(1, 4286, 20, Registers) 'Read D190 to D210 From PLC

        If Result = WSMBS.Result.SUCCESS Then

<<<<<<< HEAD
            TB_PosXInc.Text = Math.Round(((WsmbsControl1.RegistersToInt32(Registers(1), Registers(0))) / 1000), 3) ' D190 is Current X
            TB_PosYInc.Text = Math.Round((WsmbsControl1.RegistersToInt32(Registers(3), Registers(2)) / 1000), 3) 'D192 is Current Y
=======
            TB_PosX.Text = Math.Round(((WsmbsControl1.RegistersToInt32(Registers(1), Registers(0))) / 1000), 3) ' D190 is Current X
            TB_PosY.Text = Math.Round((WsmbsControl1.RegistersToInt32(Registers(3), Registers(2)) / 1000), 3) 'D192 is Current Y
>>>>>>> f6d78bb321f8c4ecda94376d6249262d0340ce93

        End If

    End Sub

    Private Sub Btn_Refresh_MainMachineCOMS_Click(sender As Object, e As EventArgs) Handles Btn_Refresh_MainMachineCOMS.Click
        cbPortName.Items.Clear()
        cbPortName.Items.AddRange(System.IO.Ports.SerialPort.GetPortNames())
    End Sub

    Private Sub Frm_Machine_Comm_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        My.Settings.Mem_MachineComPort = ComPortMainMachine
        Timer1.Stop()
    End Sub




#Region "XY SCROLL"
    Private Sub Btn_XSlowLeft_MouseWheel(sender As Object, e As MouseEventArgs) Handles Btn_XSlowLeft.MouseWheel
        If e.Delta > 0 Then
            WsmbsControl1.WriteSingleCoil(1, 2288, 1) ' Set M240 On
        Else
            WsmbsControl1.WriteSingleCoil(1, 2289, 1) 'Set M241 On
        End If
    End Sub
    Private Sub Btn_XSlowRight_MouseWheel(sender As Object, e As MouseEventArgs) Handles Btn_XSlowRight.MouseWheel
        If e.Delta > 0 Then
            WsmbsControl1.WriteSingleCoil(1, 2288, 1) ' Set M240 On
        Else
            WsmbsControl1.WriteSingleCoil(1, 2289, 1) 'Set M241 On
        End If
    End Sub
    Private Sub Btn_YSlowFront_MouseWheel(sender As Object, e As MouseEventArgs) Handles Btn_YSlowFront.MouseWheel
        If e.Delta < 0 Then
            WsmbsControl1.WriteSingleCoil(1, 2290, 1) 'Set M242 On
        Else
            WsmbsControl1.WriteSingleCoil(1, 2291, 1) ' Set M243 On
        End If
    End Sub
    Private Sub Btn_YSlowBack_MouseWheel(sender As Object, e As MouseEventArgs) Handles Btn_YSlowBack.MouseWheel
        If e.Delta < 0 Then
            WsmbsControl1.WriteSingleCoil(1, 2290, 1) 'Set M242 On
        Else
            WsmbsControl1.WriteSingleCoil(1, 2291, 1) ' Set M243 On
        End If
    End Sub
#End Region
#Region "XY Fast"
    Private Sub Btn_XFastLeft_MouseDown(sender As Object, e As MouseEventArgs) Handles Btn_XFastLeft.MouseDown
        Dim Result As WSMBS.Result
        Result = WsmbsControl1.WriteSingleCoil(1, 2294, 1) 'Set M246 On
        If (Result <> WSMBS.Result.SUCCESS) Then
            MessageBox.Show("Multitek MLT - 2 : " & WsmbsControl1.GetLastErrorString())
        End If
    End Sub

    Private Sub Btn_XFastLeft_MouseLeave(sender As Object, e As EventArgs) Handles Btn_XFastLeft.MouseLeave
        WsmbsControl1.WriteSingleCoil(1, 2294, 0) 'Set M246 OFF
    End Sub

    Private Sub Btn_XFastLeft_MouseUp(sender As Object, e As MouseEventArgs) Handles Btn_XFastLeft.MouseUp
        WsmbsControl1.WriteSingleCoil(1, 2294, 0) 'Set M246 OFF
    End Sub


    Private Sub Btn_XFastRight_MouseDown(sender As Object, e As MouseEventArgs) Handles Btn_XFastRight.MouseDown
        Dim Result As WSMBS.Result
        Result = WsmbsControl1.WriteSingleCoil(1, 2297, 1) ' Set M249 On
        If (Result <> WSMBS.Result.SUCCESS) Then
            MessageBox.Show("Multitek MLT - 2 : " & WsmbsControl1.GetLastErrorString())
        End If
    End Sub
    Private Sub Btn_XFastRight_MouseLeave(sender As Object, e As EventArgs) Handles Btn_XFastRight.MouseLeave
        WsmbsControl1.WriteSingleCoil(1, 2297, 0) ' Set M249 OFF
    End Sub

    Private Sub Btn_XFastRight_MouseUp(sender As Object, e As MouseEventArgs) Handles Btn_XFastRight.MouseUp
        WsmbsControl1.WriteSingleCoil(1, 2297, 0) ' Set M249 OFF
    End Sub

    Private Sub Btn_YFastFront_MouseDown(sender As Object, e As MouseEventArgs) Handles Btn_YFastFront.MouseDown
        Dim Result As WSMBS.Result
        Result = WsmbsControl1.WriteSingleCoil(1, 2299, 1) ' Set M251 O
        If (Result <> WSMBS.Result.SUCCESS) Then
            MessageBox.Show("Multitek MLT - 2 : " & WsmbsControl1.GetLastErrorString())
        End If
    End Sub

    Private Sub Btn_YFastFront_MouseUp(sender As Object, e As MouseEventArgs) Handles Btn_YFastFront.MouseUp
        WsmbsControl1.WriteSingleCoil(1, 2299, 0) ' Set M251 OFF
    End Sub

    Private Sub Btn_YFastFront_MouseLeave(sender As Object, e As EventArgs) Handles Btn_YFastFront.MouseLeave
        WsmbsControl1.WriteSingleCoil(1, 2299, 0) ' Set M251 OFF
    End Sub
    Private Sub Btn_YFastBack_MouseDown(sender As Object, e As MouseEventArgs) Handles Btn_YFastBack.MouseDown
        Dim Result As WSMBS.Result
        Result = WsmbsControl1.WriteSingleCoil(1, 2302, 1) 'Set M254 On
        If (Result <> WSMBS.Result.SUCCESS) Then
            MessageBox.Show("Multitek MLT - 2 : " & WsmbsControl1.GetLastErrorString())
        End If
    End Sub
    Private Sub Btn_YFastBack_MouseUp(sender As Object, e As MouseEventArgs) Handles Btn_YFastBack.MouseUp
        WsmbsControl1.WriteSingleCoil(1, 2302, 0) 'Set M254 OFF
    End Sub

    Private Sub Btn_YFastBack_MouseLeave(sender As Object, e As EventArgs) Handles Btn_YFastBack.MouseLeave
        WsmbsControl1.WriteSingleCoil(1, 2302, 0) 'Set M254 OFF
    End Sub
#End Region
#Region "XY Slow"

    Private Sub Btn_XSlowLeft_MouseDown(sender As Object, e As MouseEventArgs) Handles Btn_XSlowLeft.MouseDown
        Dim Result As WSMBS.Result
        Result = WsmbsControl1.WriteSingleCoil(1, 2295, 1) 'Set M247 On
        If (Result <> WSMBS.Result.SUCCESS) Then
            MessageBox.Show("Multitek MLT - 2 : " & WsmbsControl1.GetLastErrorString())
        End If
    End Sub
    Private Sub Btn_XSlowLeft_MouseUp(sender As Object, e As MouseEventArgs) Handles Btn_XSlowLeft.MouseUp
        WsmbsControl1.WriteSingleCoil(1, 2295, 0) 'Set M247 OFF
    End Sub

    Private Sub Btn_XSlowLeft_MouseLeave(sender As Object, e As EventArgs) Handles Btn_XSlowLeft.MouseLeave
        WsmbsControl1.WriteSingleCoil(1, 2295, 0) 'Set M247 OFF
    End Sub

    Private Sub Btn_XSlowRight_MouseDown(sender As Object, e As MouseEventArgs) Handles Btn_XSlowRight.MouseDown
        Dim Result As WSMBS.Result
        Result = WsmbsControl1.WriteSingleCoil(1, 2296, 1) ' Set M248 On
        If (Result <> WSMBS.Result.SUCCESS) Then
            MessageBox.Show("Multitek MLT - 2 : " & WsmbsControl1.GetLastErrorString())
        End If
    End Sub

    Private Sub Btn_XSlowRight_MouseUp(sender As Object, e As MouseEventArgs) Handles Btn_XSlowRight.MouseUp
        WsmbsControl1.WriteSingleCoil(1, 2296, 0) ' Set M248 OFF
    End Sub

    Private Sub Btn_XSlowRight_MouseLeave(sender As Object, e As EventArgs) Handles Btn_XSlowRight.MouseLeave
        WsmbsControl1.WriteSingleCoil(1, 2296, 0) ' Set M248 OFF
    End Sub

    Private Sub Btn_YSlowBack_MouseDown(sender As Object, e As MouseEventArgs) Handles Btn_YSlowBack.MouseDown
        Dim Result As WSMBS.Result
        Result = WsmbsControl1.WriteSingleCoil(1, 2301, 1) 'Set M253 On
        If (Result <> WSMBS.Result.SUCCESS) Then
            MessageBox.Show("Multitek MLT - 2 : " & WsmbsControl1.GetLastErrorString())
        End If
    End Sub

    Private Sub Btn_YSlowBack_MouseUp(sender As Object, e As MouseEventArgs) Handles Btn_YSlowBack.MouseUp
        WsmbsControl1.WriteSingleCoil(1, 2301, 0) 'Set M253 OFF
    End Sub

    Private Sub Btn_YSlowBack_MouseLeave(sender As Object, e As EventArgs) Handles Btn_YSlowBack.MouseLeave
        WsmbsControl1.WriteSingleCoil(1, 2301, 0) 'Set M253 OFF
    End Sub

    Private Sub Btn_YSlowFront_MouseDown(sender As Object, e As MouseEventArgs) Handles Btn_YSlowFront.MouseDown
        Dim Result As WSMBS.Result
        Result = WsmbsControl1.WriteSingleCoil(1, 2300, 1) 'Set M252 On
        If (Result <> WSMBS.Result.SUCCESS) Then
            MessageBox.Show("Multitek MLT - 2 : " & WsmbsControl1.GetLastErrorString())
        End If
    End Sub

    Private Sub Btn_YSlowFront_MouseUp(sender As Object, e As MouseEventArgs) Handles Btn_YSlowFront.MouseUp
        WsmbsControl1.WriteSingleCoil(1, 2300, 0) 'Set M252 OFF
    End Sub

    Private Sub Btn_YSlowFront_MouseLeave(sender As Object, e As EventArgs) Handles Btn_YSlowFront.MouseLeave
        WsmbsControl1.WriteSingleCoil(1, 2300, 0) 'Set M252 OFF
    End Sub

#End Region
#Region "Z Scroll"
    Private Sub Btn_FocusUPSlow_MouseWheel(sender As Object, e As MouseEventArgs) Handles Btn_FocusUPSlow.MouseWheel

        If e.Delta > 0 Then
            WsmbsControl1.WriteSingleCoil(1, 2304, 1) 'Set M260 OFF, One step Up
        Else
            WsmbsControl1.WriteSingleCoil(1, 2303, 1) 'Set M260 OFF, One step Down. 
        End If
    End Sub

    Private Sub Btn_FocusDownSlow_MouseWheel(sender As Object, e As MouseEventArgs) Handles Btn_FocusDownSlow.MouseWheel

        If e.Delta > 0 Then
            WsmbsControl1.WriteSingleCoil(1, 2304, 1) 'Set M260 OFF
        Else
            WsmbsControl1.WriteSingleCoil(1, 2303, 1) 'Set M260 OFF
        End If
    End Sub
#End Region
#Region "Z Slow"
    Private Sub Btn_FocusUPSlow_MouseDown(sender As Object, e As MouseEventArgs) Handles Btn_FocusUPSlow.MouseDown
        Dim Result As WSMBS.Result
        Result = WsmbsControl1.WriteSingleCoil(1, 2306, 1) 'Set M258 On
        If (Result <> WSMBS.Result.SUCCESS) Then
            MessageBox.Show("Multitek MLT - 2 : " & WsmbsControl1.GetLastErrorString())
        End If
    End Sub
    Private Sub Btn_FocusUPSlow_MouseUp(sender As Object, e As MouseEventArgs) Handles Btn_FocusUPSlow.MouseUp
        WsmbsControl1.WriteSingleCoil(1, 2306, 0) 'Set M258 OFF
    End Sub
    Private Sub Btn_FocusUPSlow_MouseLeave(sender As Object, e As EventArgs) Handles Btn_FocusUPSlow.MouseLeave
        WsmbsControl1.WriteSingleCoil(1, 2306, 0) 'Set M258 OFF
    End Sub

    Private Sub Btn_FocusDownSlow_MouseDown(sender As Object, e As MouseEventArgs) Handles Btn_FocusDownSlow.MouseDown
        Dim Result As WSMBS.Result
        Result = WsmbsControl1.WriteSingleCoil(1, 2305, 1) 'Set M257 On
        If (Result <> WSMBS.Result.SUCCESS) Then
            MessageBox.Show("Multitek MLT - 2 : " & WsmbsControl1.GetLastErrorString())
        End If
    End Sub
    Private Sub Btn_FocusDownSlow_MouseUp(sender As Object, e As MouseEventArgs) Handles Btn_FocusDownSlow.MouseUp
        WsmbsControl1.WriteSingleCoil(1, 2305, 0) 'Set M257 OFF
    End Sub
    Private Sub Btn_FocusDownSlow_MouseLeave(sender As Object, e As EventArgs) Handles Btn_FocusDownSlow.MouseLeave
        WsmbsControl1.WriteSingleCoil(1, 2305, 0) 'Set M257 OFF
    End Sub
#End Region
#Region "Z Fast"
    Private Sub Btn_FocusUpFast_MouseDown(sender As Object, e As MouseEventArgs) Handles Btn_FocusUpFast.MouseDown
        Dim Result As WSMBS.Result
        Result = WsmbsControl1.WriteSingleCoil(1, 2308, 1) 'Set M260 On
        If (Result <> WSMBS.Result.SUCCESS) Then
            MessageBox.Show("Multitek MLT - 2 : " & WsmbsControl1.GetLastErrorString())
        End If
    End Sub
    Private Sub Btn_FocusUpFast_MouseLeave(sender As Object, e As EventArgs) Handles Btn_FocusUpFast.MouseLeave
        WsmbsControl1.WriteSingleCoil(1, 2308, 0) 'Set M260 OFF
    End Sub
    Private Sub Btn_FocusUpFast_MouseUp(sender As Object, e As MouseEventArgs) Handles Btn_FocusUpFast.MouseUp
        WsmbsControl1.WriteSingleCoil(1, 2308, 0) 'Set M260 OFF
    End Sub

    Private Sub Btn_FocusDownFast_MouseDown(sender As Object, e As MouseEventArgs) Handles Btn_FocusDownFast.MouseDown
        Dim Result As WSMBS.Result
        Result = WsmbsControl1.WriteSingleCoil(1, 2307, 1) 'Set M259 On
        If (Result <> WSMBS.Result.SUCCESS) Then
            MessageBox.Show("Multitek MLT - 2 : " & WsmbsControl1.GetLastErrorString())
        End If
    End Sub
    Private Sub Btn_FocusDownFast_MouseLeave(sender As Object, e As EventArgs) Handles Btn_FocusDownFast.MouseLeave
        WsmbsControl1.WriteSingleCoil(1, 2307, 0) 'Set M259 OFF
    End Sub
    Private Sub Btn_FocusDownFast_MouseUp(sender As Object, e As MouseEventArgs) Handles Btn_FocusDownFast.MouseUp
        WsmbsControl1.WriteSingleCoil(1, 2307, 0) 'Set M259 OFF
    End Sub
#End Region
<<<<<<< HEAD
    Private Sub Btn_CurX_Click(sender As Object, e As EventArgs) Handles Btn_CurXInc.Click
=======
    Private Sub Btn_CurX_Click(sender As Object, e As EventArgs) Handles Btn_CurX.Click
>>>>>>> f6d78bb321f8c4ecda94376d6249262d0340ce93
        WsmbsControl1.WriteSingleCoil(1, 2048, 1) 'Set M0 On
        Pause(300)
        WsmbsControl1.WriteSingleCoil(1, 2048, 0) 'Set M0 Off
    End Sub

<<<<<<< HEAD
    Private Sub Btn_CurY_Click(sender As Object, e As EventArgs) Handles Btn_CurYInc.Click
=======
    Private Sub Btn_CurY_Click(sender As Object, e As EventArgs) Handles Btn_CurY.Click
>>>>>>> f6d78bb321f8c4ecda94376d6249262d0340ce93
        WsmbsControl1.WriteSingleCoil(1, 2049, 1) 'Set M0 On
        Pause(300)
        WsmbsControl1.WriteSingleCoil(1, 2049, 0) 'Set M0 Off
    End Sub
<<<<<<< HEAD

    Private Sub btn_enterPt_Click(sender As Object, e As EventArgs) Handles btn_enterPt.Click
        enterKeyPressed = True
    End Sub
=======
>>>>>>> f6d78bb321f8c4ecda94376d6249262d0340ce93
End Class
