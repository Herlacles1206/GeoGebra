Imports AForge.Video
Imports AForge.Video.DirectShow


Public Class MainForm
    Public zoomFactor As Double
    Public curMeasureType As Integer
    Public prevMeasureType As Integer
    Public mLBtnDown As Boolean
    Public mPtF As PointF = New PointF()
    Public mPt As Point = New Point()
    Public nullBrush As Brush = New SolidBrush(Color.White)
    Public Shared drawFont As Font = New Font("Arial", 10, FontStyle.Regular)
    Public Shared drawPen As Pen = New Pen(Color.Black, 1)
    Public Shared drawBrush As Brush = New SolidBrush(Color.Black)
    Public Shared ptBrush As Brush = New SolidBrush(Color.Blue)
    Public Shared ptNameSet As String() = {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
                                           "AA", "AB", "AC", "AD", "AE", "AF", "AG", "AH", "AI", "AJ", "AK", "AL", "AM", "AN", "AO", "AP", "AQ", "AR", "AS", "AT", "AU", "AV", "AW", "AX", "AY", "AZ",
                                           "BA", "BB", "BC", "BD", "BE", "BF", "BG", "BH", "BI", "BJ", "BK", "BL", "BM", "BN", "BO", "BP", "BQ", "BR", "BS", "BT", "BU", "BV", "BW", "BX", "BY", "BZ",
                                           "CA", "CB", "CC", "CD", "CE", "CF", "CG", "CH", "CI", "CJ", "CK", "CL", "CM", "CN", "CO", "CP", "CQ", "CR", "CS", "CT", "CU", "CV", "CW", "CX", "CY", "CZ",
                                           "DA", "DB", "DC", "DD", "DE", "DF", "DG", "DH", "DI", "DJ", "DK", "DL", "DM", "DN", "DO", "DP", "DQ", "DR", "DS", "DT", "DU", "DV", "DW", "DX", "DY", "DZ"}

    Public curObj As measureObj
    Public objList As List(Of measureObj) = New List(Of measureObj)
    'member variable for webcam
    Public videoDevices As FilterInfoCollection                        'usable video devices
    Public videoDevice As VideoCaptureDevice                           'video device currently used 
    Public snapshotCapabilities As VideoCapabilities()
    Public ReadOnly listCamera As ArrayList = New ArrayList()
    Public Shared needSnapshot As Boolean = False
    Public newImage As Bitmap = Nothing                                'used for capturing frame of webcam
    Public ReadOnly _devicename As String = "MultitekHDCam"            'device name
    Public ReadOnly photoList As New System.Windows.Forms.ImageList    'list of captured images
    Public file_counter As Integer = 0                                 'the count of captured images
    Public camera_state As Boolean = False                             'the state of camera is opened or not
    Public imagepath As String = ""                                     'path of folder storing captured images
    Public flag As Boolean = False                                     'flag for live image


    Private Sub InitializeComponents()
        pic_main.Invoke(New Action(Sub() pic_main.Image = New Bitmap(pic_main.Width, pic_main.Height)))
    End Sub

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeComponents()
    End Sub

    Private Sub InitializeCurObj(mType As Integer)
        curObj.Refresh()
        curObj.mType = mType
    End Sub

    Private Sub btn_point_Click(sender As Object, e As EventArgs) Handles btn_point.Click
        curMeasureType = MeasureType.point
        InitializeCurObj(curMeasureType)
    End Sub
    Private Sub btn_line_Click(sender As Object, e As EventArgs) Handles btn_line.Click
        curMeasureType = MeasureType.line
        InitializeCurObj(curMeasureType)
    End Sub

    Private Sub btn_angle_Click(sender As Object, e As EventArgs) Handles btn_angle.Click
        curMeasureType = MeasureType.angle
        InitializeCurObj(curMeasureType)
    End Sub

    Private Sub btn_circle_center_Click(sender As Object, e As EventArgs) Handles btn_circle_center.Click
        curMeasureType = MeasureType.circleWithCenter
        InitializeCurObj(curMeasureType)
    End Sub

    Private Sub btn_angle_fixed_Click(sender As Object, e As EventArgs) Handles btn_angle_fixed.Click
        curMeasureType = MeasureType.angleFixed
        InitializeCurObj(curMeasureType)
    End Sub

    Private Sub btn_circle_fixed_Click(sender As Object, e As EventArgs) Handles btn_circle_fixed.Click
        curMeasureType = MeasureType.circleCenterRadius
        InitializeCurObj(curMeasureType)
    End Sub
    Private Sub GetMousePositions(X As Integer, Y As Integer)
        mPt.X = X : mPt.Y = Y
        mPtF.X = CDbl(X) / pic_main.Width
        mPtF.Y = CDbl(Y) / pic_main.Height
    End Sub
    Private Sub AppendObjToList()
        objList.Add(curObj)
        curObj.Refresh()
        curMeasureType = -1
    End Sub
    Private Sub pic_main_MouseDown(sender As Object, e As MouseEventArgs) Handles pic_main.MouseDown
        If e.Button = MouseButtons.Left Then
            mLBtnDown = True
            GetMousePositions(e.X, e.Y)

            If curMeasureType >= 0 Then
                Dim completed = UpdateObj(curObj, curMeasureType, mPtF)
                If completed Then
                    AppendObjToList()
                End If
                DrawObjList(pic_main, objList)
            End If

        End If
    End Sub

    Private Sub pic_main_MouseMove(sender As Object, e As MouseEventArgs) Handles pic_main.MouseMove
        GetMousePositions(e.X, e.Y)
        If curMeasureType >= 0 And curObj.ptCnt > 0 Then
            Dim g As Graphics = pic_main.CreateGraphics()
            DrawObjList(pic_main, objList)
            DrawObj(g, pic_main, curObj, curMeasureType, mPtF)
            g.Dispose()
        End If
    End Sub

    Private Sub pic_main_MouseUp(sender As Object, e As MouseEventArgs) Handles pic_main.MouseUp
        mLBtnDown = False
    End Sub

#Region "Webcam"

    Public Sub Device_NewFrame(sender As Object, eventArgs As AForge.Video.NewFrameEventArgs)
        On Error Resume Next

        Me.Invoke(Sub()
                      newImage = DirectCast(eventArgs.Frame.Clone(), Bitmap)

                      If flag = False Then
                          pic_main.Image = newImage.Clone()
                      End If
                      newImage?.Dispose()
                  End Sub)

    End Sub

    Private Sub OpenCamera()
        Dim cameraInt As Int32 = CheckPerticularCamera(videoDevices, _devicename)
        If (cameraInt < 0) Then
            MessageBox.Show("Compatible Camera not found..")
            Exit Sub
        End If

        videoDevices = New FilterInfoCollection(FilterCategory.VideoInputDevice)
        videoDevice = New VideoCaptureDevice(videoDevices(Convert.ToInt32(cameraInt)).MonikerString)
        If Not My.Settings.camresindex.Equals("") Then
            videoDevice.VideoResolution = videoDevice.VideoCapabilities(Convert.ToInt32(My.Settings.camresindex))
        End If
        AddHandler videoDevice.NewFrame, New NewFrameEventHandler(AddressOf Device_NewFrame)
        videoDevice.Start()
        camera_state = True
    End Sub

    'close camera
    Private Sub CloseCamera()

        If videoDevice Is Nothing Then
        ElseIf videoDevice.IsRunning Then
            videoDevice.SignalToStop()
            RemoveHandler videoDevice.NewFrame, New NewFrameEventHandler(AddressOf Device_NewFrame)
            videoDevice.Source = Nothing
        End If
        camera_state = False
    End Sub
    Private Sub OPENCAMERAToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OPENCAMERAToolStripMenuItem.Click
        Try
            OpenCamera()
            SelectResolution(videoDevice, CameraResolutionsCB)
            If Not My.Settings.camresindex.Equals("") Then
                CameraResolutionsCB.SelectedIndex = My.Settings.camresindex + 1
            End If

        Catch excpt As Exception
            MessageBox.Show(excpt.Message)
        End Try
    End Sub

    Private Sub CLOSECAMERAToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CLOSECAMERAToolStripMenuItem.Click
        Try
            CloseCamera()
            pic_main.Image = Nothing

        Catch excpt As Exception
            MessageBox.Show(excpt.Message)
        End Try
    End Sub

    Private Sub CameraResolutionsCB_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CameraResolutionsCB.SelectedIndexChanged
        If CameraResolutionsCB.SelectedIndex > 0 Then
            My.Settings.camresindex = CameraResolutionsCB.SelectedIndex - 1
            My.Settings.Save()
            CloseCamera()
            Threading.Thread.Sleep(500)
            OpenCamera()
        End If
    End Sub
#End Region
    Private Sub EXPORTREPORTToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EXPORTREPORTToolStripMenuItem.Click
        Dim filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*"
        Dim title = "Save"
        SaveReportToExcel(pic_main, filter, title, objList)
    End Sub


End Class
