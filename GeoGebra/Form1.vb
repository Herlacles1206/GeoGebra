﻿Imports System.Runtime.InteropServices
Imports AForge.Video
Imports AForge.Video.DirectShow
Imports DocumentFormat.OpenXml.Bibliography
Imports Emgu.CV
Imports Emgu.CV.Flann
Imports Emgu.CV.[Structure]

Public Class MainForm
    Public oriImg As Mat = New Mat
    Public curImg As Mat = New Mat
    Public Shared zoomFactor As Double
    Public curMeasureType As Integer
    Public prevMeasureType As Integer
    Public distType As Integer
    Public mLBtnDown As Boolean
    Public mPtF As PointF = New PointF()
    Public mPt As Point = New Point()
    Public nullBrush As Brush = New SolidBrush(Color.White)
    Public Shared drawFont As Font = New Font("Arial", 10, FontStyle.Regular)
    Public Shared drawPen As Pen = New Pen(Color.Black, 1)
    Public Shared redPen As Pen = New Pen(Color.Red, 2)
    Public Shared drawBrush As Brush = New SolidBrush(Color.Black)
    Public Shared ptBrush As Brush = New SolidBrush(Color.Blue)
    Public Shared RedBrush As Brush = New SolidBrush(Color.Red)
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
    Public realWidth As Integer = 200000
    Public realHeight As Integer = 150000

    Public Shared eraseState As Boolean
    Public Shared expandState As Boolean
    Public Shared selectedObj As Integer
    Public Shared selectedLineID As Integer                             'In case of angle
    Public Shared selectedSegment As Integer
    Public Shared selectedPt As Integer
    Public Shared threshold As Integer = 5
    Public Shared Infinite As Integer = 999999999
    Public Shared selectState As Boolean
    Public Shared selectedObjSet(2) As Integer

    Public XsLinePoint As Integer                                      'X-coordinate of foot of perpendicular
    Public YsLinePoint As Integer                                      'Y-coordinate of foot of perpendicular
    Public PXs, PYs As Integer                                         'points used for drawing max, min lines
    Public FinalPXs, FinalPYs As Integer

    Public DotX, DotY, CDotX, CDotY As Integer                         'points used for dotted lines

    Public OutPointFlag As Boolean                                     'flag specifies whether the foot of perpendicular is in range of object or not
    Public COutPointFlag As Boolean                                    'flag specifies whether the foot of perpendicular is in range of curve&poly object or not
    Public PDotX As Integer                                            'X-coordinate of point which is used for drawing dotted line in case of polygen object
    Public PDotY As Integer                                            'Y-coordinate of point which is used for drawing dotted line in case of polygen object
    Public POutFlag As Boolean                                         'flag specifies whether the foot of perpendicular is in range of polygen object or not

    Public txtBox As TextBox = New TextBox()
    Public annoNum As Integer

    Public EdgeRegionDrawReady As Boolean
    Public FirstPtOfEdge As Point
    Public SecondPtOfEdge As Point
    Public C_CurveObj As CurveObj = New CurveObj()



    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If curMeasureType <> MeasureType.eraseObj Then
            eraseState = False
        End If
        If curMeasureType <> MeasureType.expandObj Then
            expandState = False
        End If
        If curMeasureType <> MeasureType.selectObj Then
            selectState = False
        End If
    End Sub

    Private Sub RefreshVariables()
        curObj.Refresh()
        curMeasureType = -1
        objList.Clear()
        pic_main.Refresh()
        dgv_pos.Rows.Clear()
        txt_x.Text = ""
        txt_y.Text = ""
        txt_counter.Text = ""

    End Sub
    Private Sub InitializeVariables()
        InitializeMeasureObj(curObj)
        RefreshVariables()
        ReSetSelectedIDs()
        ReSetSelectedObjSet()
        zoomFactor = 1.0
        annoNum = Infinite
    End Sub
    Private Sub InitializeComponents()
        pic_main.Controls.Add(txtBox)

        txtBox.Multiline = True
        txtBox.AutoSize = False
        txtBox.Visible = False
        txtBox.Font = drawFont
        AddHandler txtBox.TextChanged, New EventHandler(AddressOf ID_MY_TEXTBOX_TextChanged)

        oriImg = New Mat(New Size(pic_main.Width, pic_main.Height), CvEnum.DepthType.Cv8U, 3)
        oriImg.SetTo(New Bgr(255, 255, 255).MCvScalar)
        curImg = oriImg.Clone()
        pic_main.Invoke(New Action(Sub() pic_main.Image = oriImg.ToBitmap))

        Timer1.Interval = 30
        Timer1.Start()
    End Sub

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeComponents()
        InitializeVariables()
    End Sub

    Private Sub InitializeCurObj(mType As Integer)
        curObj.Refresh()
        curObj.mType = mType
        If mType = MeasureType.detectLine Then curObj.mType = MeasureType.lineFit
        If mType = MeasureType.detectCircle Then curObj.mType = MeasureType.circleFit
    End Sub

    Private Sub btn_point_Click(sender As Object, e As EventArgs) Handles btn_point.Click
        curMeasureType = MeasureType.point
        InitializeCurObj(curMeasureType)
        ReSetSelectedObjSet()
    End Sub
    Private Sub btn_line_Click(sender As Object, e As EventArgs) Handles btn_line.Click
        curMeasureType = MeasureType.line
        InitializeCurObj(curMeasureType)
        ReSetSelectedObjSet()
    End Sub
    Private Sub btn_angle_Click(sender As Object, e As EventArgs) Handles btn_angle.Click
        curMeasureType = MeasureType.angle
        InitializeCurObj(curMeasureType)
        If CheckAngleBetweenTwoLines(objList, curObj, distType, selectedObjSet) Then
            AppendObjToList()
            DrawToPic()
        End If

        ReSetSelectedObjSet()
    End Sub

    Private Sub btn_circle_center_Click(sender As Object, e As EventArgs) Handles btn_circle_center.Click
        curMeasureType = MeasureType.circleWithCenter
        InitializeCurObj(curMeasureType)
        ReSetSelectedObjSet()
    End Sub

    Private Sub btn_angle_fixed_Click(sender As Object, e As EventArgs) Handles btn_angle_fixed.Click
        curMeasureType = MeasureType.angleFixed
        InitializeCurObj(curMeasureType)
        ReSetSelectedObjSet()
    End Sub

    Private Sub btn_circle_fixed_Click(sender As Object, e As EventArgs) Handles btn_circle_fixed.Click
        curMeasureType = MeasureType.circleCenterRadius
        InitializeCurObj(curMeasureType)
        ReSetSelectedObjSet()
    End Sub

    Private Sub btn_fit_line_Click(sender As Object, e As EventArgs) Handles btn_fit_line.Click
        curMeasureType = MeasureType.lineFit
        InitializeCurObj(curMeasureType)
        ReSetSelectedObjSet()
    End Sub

    Private Sub btn_fit_circle_Click(sender As Object, e As EventArgs) Handles btn_fit_circle.Click
        curMeasureType = MeasureType.circleFit
        InitializeCurObj(curMeasureType)
        ReSetSelectedObjSet()
    End Sub

    Private Sub btn_fit_arc_Click(sender As Object, e As EventArgs) Handles btn_fit_arc.Click
        curMeasureType = MeasureType.arcFit
        InitializeCurObj(curMeasureType)
        ReSetSelectedObjSet()
    End Sub

    Private Sub btn_annotation_Click(sender As Object, e As EventArgs) Handles btn_annotation.Click
        curMeasureType = MeasureType.annotation
        InitializeCurObj(curMeasureType)
        ReSetSelectedObjSet()
    End Sub
    Private Sub GetMousePositions(X As Integer, Y As Integer)
        mPt.X = X : mPt.Y = Y
        mPtF.X = CDbl(X) / pic_main.Width
        mPtF.Y = CDbl(Y) / pic_main.Height
    End Sub

    Private Sub DisplayMousePositions()
        txt_x.Text = (mPtF.X * realWidth).ToString()
        txt_y.Text = (mPtF.Y * realHeight).ToString()
    End Sub
    Private Sub AppendObjToList()
        Dim curObjBackup As New measureObj
        InitializeMeasureObj(curObjBackup)
        CloneMeasureObj(curObj, curObjBackup)
        curObjBackup.objID = objList.Count
        objList.Add(curObjBackup)
        curObj.Refresh()
        curMeasureType = -1
    End Sub

    Private Const EM_GETLINECOUNT As Integer = &HBA
    <DllImport("user32", EntryPoint:="SendMessageA", CharSet:=CharSet.Ansi, SetLastError:=True, ExactSpelling:=True)>
    Private Shared Function SendMessage(ByVal hwnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
    End Function
    Private Sub ID_MY_TEXTBOX_TextChanged(sender As Object, e As EventArgs)
        Dim textBox = CType(sender, TextBox)
        Dim numberOfLines = SendMessage(textBox.Handle.ToInt32(), EM_GETLINECOUNT, 0, 0)
        textBox.Height = (textBox.Font.Height + 2) * numberOfLines
    End Sub
    Private Sub AddToSelectedObjIDs()
        If selectedObjSet(0) = Infinite And selectedObj <> Infinite Then selectedObjSet(0) = selectedObj : Return
        If selectedObjSet(1) = Infinite And selectedObj <> Infinite Then selectedObjSet(1) = selectedObj : curMeasureType = MeasureType.initState
    End Sub
    Private Sub pic_main_MouseDown(sender As Object, e As MouseEventArgs) Handles pic_main.MouseDown
        If e.Button = MouseButtons.Left Then
            mLBtnDown = True
            GetMousePositions(e.X, e.Y)

            If curMeasureType >= 0 Then
                Dim completed = UpdateObj(curObj, curMeasureType, mPtF)

                txt_counter.Text = curObj.ptCnt.ToString()
                If completed Then
                    CompareWithExisingObjs(curObj, objList)
                    AppendObjToList()
                End If

                If curMeasureType = MeasureType.eraseObj Then
                    EraseSelectedObj(objList)
                End If
                If curMeasureType = MeasureType.expandObj Then
                    ExpandSelectedObj(objList)
                End If
                If curMeasureType = MeasureType.selectObj Then
                    AddToSelectedObjIDs()
                End If
                If EdgeRegionDrawReady Then
                    FirstPtOfEdge = mPt
                End If
            End If
            If annoNum <> Infinite Then
                DisableTextBox(txtBox, objList, annoNum)
                annoNum = Infinite

            End If
            DrawObjList(pic_main, objList)
        End If
    End Sub

    Private Sub DrawToPic()
        Dim g As Graphics = pic_main.CreateGraphics()
        DrawObjList(pic_main, objList)
        DrawObj(g, pic_main, curObj, curMeasureType, mPtF)
        g.Dispose()
    End Sub

    Private Sub pic_main_MouseMove(sender As Object, e As MouseEventArgs) Handles pic_main.MouseMove
        GetMousePositions(e.X, e.Y)
        DisplayMousePositions()
        ReSetSelectedIDs()
        Dim found = GetSelectedIDs(objList, mPt)
        If found Then
            GetMousePositions(e.X, e.Y)
        End If

        If curMeasureType >= 0 And (curObj.ptCnt > 0 Or curMeasureType = MeasureType.eraseObj Or curMeasureType = MeasureType.expandObj Or curMeasureType = MeasureType.selectObj Or EdgeRegionDrawReady) Then
            DrawToPic()
        End If

        If EdgeRegionDrawReady And FirstPtOfEdge.X <> 0 And FirstPtOfEdge.Y <> 0 Then
            SecondPtOfEdge = mPt
            DrawRectangle(pic_main, FirstPtOfEdge, SecondPtOfEdge)
        End If
    End Sub

    Private Sub ReSetEdgeDrawState()
        EdgeRegionDrawReady = False
        FirstPtOfEdge.X = 0
        FirstPtOfEdge.Y = 0
        SecondPtOfEdge.X = 0
        SecondPtOfEdge.Y = 0
    End Sub
    Private Sub pic_main_MouseUp(sender As Object, e As MouseEventArgs) Handles pic_main.MouseUp
        mLBtnDown = False
        If EdgeRegionDrawReady And SecondPtOfEdge.X <> 0 And SecondPtOfEdge.Y <> 0 Then
            C_CurveObj = DetectCurve(curImg.ToBitmap(), FirstPtOfEdge, SecondPtOfEdge)
            ReSetEdgeDrawState()
            Dim form = New ConfirmDetection()
            Dim res = form.ShowDialog()
            If res = DialogResult.OK Then
                If C_CurveObj.CPointIndx >= 2 Then
                    If curMeasureType = MeasureType.detectLine Then
                        UpdateFitLineObjFromCurve(curObj, C_CurveObj)
                        CompleteFitLineObj(curObj)
                    Else
                        UpdateFitCircleObjFromCurve(curObj, C_CurveObj)
                        CompleteFitCircleObj(curObj)
                    End If
                    CompareWithExisingObjs(curObj, objList)
                    AppendObjToList()
                End If
            ElseIf res = DialogResult.Retry Then
                EdgeRegionDrawReady = True
            End If
            DrawToPic()
            C_CurveObj.Refresh()
        End If
    End Sub

    Private Sub pic_main_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles pic_main.MouseDoubleClick
        GetMousePositions(e.X, e.Y)
        If selectedObj <> Infinite Then
            If objList(selectedObj).mType = MeasureType.annotation Then
                EnableTextBox(txtBox, objList(selectedObj).annoObj)
                annoNum = selectedObj
            End If
        End If
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

    Private Sub btn_cancel_last_Click(sender As Object, e As EventArgs) Handles btn_cancel_last.Click
        If curObj.ptCnt > 0 Then
            RemoveOnePtFromFitObj(curObj)
            DrawToPic()
            txt_counter.Text = curObj.ptCnt.ToString()
            LoadPosDataToGridView(curObj)
        End If
    End Sub

    Private Sub btn_cancel_all_Click(sender As Object, e As EventArgs) Handles btn_cancel_all.Click
        RemoveAllPtFromFitObj(curObj)
        DrawToPic()
        txt_counter.Text = curObj.ptCnt.ToString()
        LoadPosDataToGridView(curObj)
    End Sub



    Private Sub btn_finish_Click(sender As Object, e As EventArgs) Handles btn_finish.Click
        If curObj.ptCnt < 2 Then
            MsgBox("Please select more than 2 points")
            Return
        End If
        If curMeasureType = MeasureType.lineFit Then
            curObj.fitLineObj.completed = True
            CompleteFitLineObj(curObj)
        ElseIf curMeasureType = MeasureType.circleFit Or curMeasureType = MeasureType.arcFit Then
            curObj.fitCirObj.completed = True
            CompleteFitCircleObj(curObj)
        End If
        AppendObjToList()
        DrawObjList(pic_main, objList)
    End Sub

    Private Sub btn_tool_clearall_Click(sender As Object, e As EventArgs) Handles btn_tool_clearall.Click
        RefreshVariables()
    End Sub

    Private Sub btn_tool_open_Click(sender As Object, e As EventArgs) Handles btn_tool_open.Click
        Dim filter = "All Files|*.*|JPEG Files|*.jpg|PNG Files|*.png|BMP Files|*.bmp"
        Dim title = "Open"

        oriImg = LoadImageFromFile(pic_main, filter, title)
        curImg = oriImg.Clone()
        pic_main.Invoke(New Action(Sub() pic_main.Image = oriImg.ToBitmap()))
    End Sub

    Private Sub ReSetSelectedIDs()
        selectedObj = Infinite : selectedSegment = Infinite : selectedPt = Infinite : selectedLineID = Infinite
    End Sub

    Private Sub ReSetSelectedObjSet()
        selectedObjSet(0) = Infinite
        selectedObjSet(1) = Infinite
    End Sub

    Private Sub btn_erase_Click(sender As Object, e As EventArgs) Handles btn_erase.Click
        curMeasureType = MeasureType.eraseObj
        eraseState = True
        curObj.Refresh()
        ReSetSelectedIDs()
    End Sub

    Private Sub btn_extand_Click(sender As Object, e As EventArgs) Handles btn_extand.Click
        curMeasureType = MeasureType.expandObj
        expandState = True
        curObj.Refresh()
        ReSetSelectedIDs()
    End Sub

    Private Sub GetDistType()
        If rad_min.Checked Then distType = DistanceType.min : Return
        If rad_max.Checked Then distType = DistanceType.max : Return
        If rad_center.Checked Then distType = DistanceType.center : Return
    End Sub
    Private Sub rad_min_CheckedChanged(sender As Object, e As EventArgs) Handles rad_min.CheckedChanged
        GetDistType()
    End Sub

    Private Sub rad_max_CheckedChanged(sender As Object, e As EventArgs) Handles rad_max.CheckedChanged
        GetDistType()
    End Sub

    Private Sub rad_center_CheckedChanged(sender As Object, e As EventArgs) Handles rad_center.CheckedChanged
        GetDistType()
    End Sub

    Private Sub zoom_Image()
        Dim zoomed = ZoomImage(zoomFactor, oriImg)
        curImg?.Dispose()
        curImg = zoomed.Clone
        pic_main.Image = zoomed.ToBitmap()
        zoomed.Dispose()
        DrawToPic()
    End Sub


    Private Sub btn_zoom_in_Click(sender As Object, e As EventArgs) Handles btn_zoom_in.Click
        If zoomFactor > 10 Then Return
        zoomFactor *= 1.1
        zoom_Image()
    End Sub

    Private Sub btn_zoom_out_Click(sender As Object, e As EventArgs) Handles btn_zoom_out.Click
        If zoomFactor < 0.1 Then Return
        zoomFactor /= 1.1
        zoom_Image()
    End Sub

    Private Sub btn_calc_dist_Click(sender As Object, e As EventArgs) Handles btn_calc_dist.Click
        curMeasureType = MeasureType.calcdist
        GetDistType()
        curObj.Refresh()
        curObj.mType = MeasureType.line
        CalcDistBetweenSelectedObjs(objList, curObj, distType, selectedObjSet)
        If curObj.ptCnt <> 0 Then AppendObjToList()
        DrawToPic()
        ReSetSelectedIDs()
    End Sub

    Private Sub btn_select_Click(sender As Object, e As EventArgs) Handles btn_select.Click
        curMeasureType = MeasureType.selectObj
        selectState = True
        curObj.Refresh()
        ReSetSelectedIDs()
        ReSetSelectedObjSet()
    End Sub

    Private Sub btn_detect_line_Click(sender As Object, e As EventArgs) Handles btn_detect_line.Click
        ReSetEdgeDrawState()
        EdgeRegionDrawReady = True
        curMeasureType = MeasureType.detectLine
        InitializeCurObj(curMeasureType)
        ReSetSelectedObjSet()
    End Sub
    Private Sub btn_detect_circle_Click(sender As Object, e As EventArgs) Handles btn_detect_circle.Click
        ReSetEdgeDrawState()
        EdgeRegionDrawReady = True
        curMeasureType = MeasureType.detectCircle
        InitializeCurObj(curMeasureType)
        ReSetSelectedObjSet()
    End Sub
End Class
