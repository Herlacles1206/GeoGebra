Imports System.CodeDom
Imports System.ComponentModel
Imports System.Runtime.InteropServices
Imports System.Security.Cryptography
Imports DocumentFormat.OpenXml.Drawing.Wordprocessing
Imports DocumentFormat.OpenXml.Office2019.Drawing.Animation
Imports DocumentFormat.OpenXml.Spreadsheet
Imports DocumentFormat.OpenXml.Vml.Office
Imports Emgu.CV
Imports GeometRi


Public Module CtrlMethods
    Public Function UpdateLineObj(ByRef curObj As measureObj, mPtF As PointF)
        If curObj.ptCnt = 0 Then
            curObj.lineObj.midPts(0).pt.X = mPtF.X : curObj.lineObj.midPts(0).pt.Y = mPtF.Y
            curObj.ptCnt += 1 : curObj.lineObj.ptCnt += 1
            curObj.lineObj.drawFlag(0) = True
            Return False
        Else
            curObj.lineObj.midPts(1).pt.X = mPtF.X : curObj.lineObj.midPts(1).pt.Y = mPtF.Y
            curObj.ptCnt += 1 : curObj.lineObj.ptCnt += 1
            ArrangePtsOfLine(curObj.lineObj)

            GetConstsOfLine(curObj.lineObj)
            Return True
        End If
    End Function

    Public Sub UpdateFitLineObj(ByRef curObj As measureObj, mPtF As PointF)
        curObj.fitLineObj.refPts(curObj.ptCnt).pt = mPtF
        curObj.ptCnt += 1 : curObj.fitLineObj.ptCnt += 1
    End Sub

    Public Sub UpdateFitLineObjFromCurve(ByRef curObj As measureObj, curveObj As CurveObj)
        curObj.fitLineObj.refPts(0).pt = curveObj.CurvePoint(0)
        curObj.fitLineObj.refPts(1).pt = curveObj.CurvePoint(curveObj.CPointIndx / 2)
        curObj.fitLineObj.refPts(2).pt = curveObj.CurvePoint(curveObj.CPointIndx)
        curObj.ptCnt = 3 : curObj.fitLineObj.ptCnt = 3 : curObj.fitLineObj.completed = True
    End Sub
    Sub RemoveOnePtFromFitObj(ByRef curObj As measureObj)
        If curObj.ptCnt <= 0 Then Return
        curObj.ptCnt -= 1
        If curObj.mType = MeasureType.lineFit Then
            curObj.fitLineObj.ptCnt -= 1
        ElseIf curObj.mType = MeasureType.circleFit Or curObj.mType = MeasureType.arcFit Then
            curObj.fitCirObj.ptCnt -= 1
        End If
    End Sub

    Sub RemoveAllPtFromFitObj(ByRef curObj As measureObj)
        curObj.ptCnt = 0
        curObj.fitLineObj.ptCnt = 0
        curObj.fitCirObj.ptCnt = 0
    End Sub
    Public Sub CompleteFitLineObj(ByRef curObj As measureObj, X_L As Integer, Y_T As Integer, X_R As Integer, Y_B As Integer)
        Dim ptList As List(Of PointF) = New List(Of PointF)
        For i = 0 To curObj.ptCnt - 1
            Dim X = curObj.fitLineObj.refPts(i).pt.X * MainForm.pic_main.Width
            Dim Y = curObj.fitLineObj.refPts(i).pt.Y * MainForm.pic_main.Height
            ptList.Add(New PointF(X, Y))
        Next

        FindLinearLeastSquaresFit(ptList, curObj.fitLineObj.lineObj.m, curObj.fitLineObj.lineObj.b)
        GetEndPtsOfFitLine(curObj.fitLineObj, X_L, Y_T, X_R, Y_B)
    End Sub
    Public Function UpdateCircleWithCenterObj(ByRef curObj As measureObj, mPtF As PointF)
        If curObj.ptCnt = 0 Then
            curObj.circleObj.centerPt.pt.X = mPtF.X : curObj.circleObj.centerPt.pt.Y = mPtF.Y : curObj.ptCnt += 1
            curObj.circleObj.drawFlag(0) = True
            Return False
        Else
            curObj.circleObj.midPts(0).pt.X = mPtF.X : curObj.circleObj.midPts(0).pt.Y = mPtF.Y
            'need to modify
            Dim X1 = curObj.circleObj.centerPt.pt.X * MainForm.pic_main.Width : Dim Y1 = curObj.circleObj.centerPt.pt.Y * MainForm.pic_main.Height
            Dim X2 = curObj.circleObj.midPts(0).pt.X * MainForm.pic_main.Width : Dim Y2 = curObj.circleObj.midPts(0).pt.Y * MainForm.pic_main.Height
            curObj.circleObj.radius = CalcDistBetweenPoints(X1, Y1, X2, Y2) / MainForm.zoomFactor
            curObj.ptCnt += 1 : curObj.circleObj.ptCnt += 1

            Return True
        End If
    End Function

    Public Sub UpdateFitCircleObj(ByRef curObj As measureObj, mPtF As PointF)
        curObj.fitCirObj.refPts(curObj.ptCnt).pt = mPtF
        curObj.ptCnt += 1 : curObj.fitCirObj.ptCnt += 1
    End Sub

    Public Sub UpdateFitCircleObjFromCurve(ByRef curObj As measureObj, curveObj As CurveObj)
        curObj.fitCirObj.refPts(0).pt = curveObj.CurvePoint(0)
        curObj.fitCirObj.refPts(1).pt = curveObj.CurvePoint(curveObj.CPointIndx / 2)
        curObj.fitCirObj.refPts(2).pt = curveObj.CurvePoint(curveObj.CPointIndx)
        curObj.ptCnt = 3 : curObj.fitCirObj.ptCnt = 3 : curObj.fitCirObj.completed = True
    End Sub
    Public Sub CompleteFitCircleObj(ByRef curObj As measureObj)
        Dim ptList As List(Of PointF) = New List(Of PointF)
        For i = 0 To curObj.ptCnt - 1
            Dim X = curObj.fitCirObj.refPts(i).pt.X * MainForm.pic_main.Width
            Dim Y = curObj.fitCirObj.refPts(i).pt.Y * MainForm.pic_main.Height
            ptList.Add(New PointF(X, Y))
        Next
        Dim CenterPt = GetCenterPoint(ptList)
        curObj.fitCirObj.circleObj.radius = GetRadiusFromMultiPt(CenterPt, ptList) / MainForm.zoomFactor
        curObj.fitCirObj.circleObj.centerPt.pt = New PointF(CenterPt.X / MainForm.pic_main.Width, CenterPt.Y / MainForm.pic_main.Height)
        curObj.fitCirObj.circleObj.drawFlag(0) = True
        If curObj.mType = MeasureType.arcFit Then
            Dim angleList As List(Of Double) = New List(Of Double)
            For i = 0 To curObj.ptCnt - 1
                Dim angle = CalcAngleBetweenTwoLines(ptList(i).X, ptList(i).Y, CenterPt.X, CenterPt.Y, CenterPt.X + 10, CenterPt.Y)
                Dim downFlag = CheckAngleDirection(CenterPt.X + 10, CenterPt.Y, CenterPt.X, CenterPt.Y, ptList(i).X, ptList(i).Y)
                If Not downFlag Then angle = 360 - angle
                angleList.Add(angle)
            Next
            Dim st, ed, sweep As Integer
            GetOptPtsForArc(angleList, st, ed, sweep)
            Dim pt1 = CalcPositionInCircle(New Point(CenterPt.X, CenterPt.Y), curObj.fitCirObj.circleObj.radius * MainForm.zoomFactor, angleList(st))
            curObj.fitCirObj.circleObj.midPts(0).pt = New PointF(pt1.X / MainForm.pic_main.Width, pt1.Y / MainForm.pic_main.Height)
            Dim pt2 = CalcPositionInCircle(New Point(CenterPt.X, CenterPt.Y), curObj.fitCirObj.circleObj.radius * MainForm.zoomFactor, angleList(st) + sweep)
            curObj.fitCirObj.circleObj.midPts(1).pt = New PointF(pt2.X / MainForm.pic_main.Width, pt2.Y / MainForm.pic_main.Height)

            'curObj.fitCirObj.circleObj.midPts(0).pt = curObj.fitCirObj.refPts(st).pt
            'curObj.fitCirObj.circleObj.midPts(1).pt = curObj.fitCirObj.refPts(ed).pt
            curObj.fitCirObj.circleObj.ptCnt = 2
            If ArrangePtsOfCircle(curObj.fitCirObj.circleObj) Then curObj.fitCirObj.circleObj.drawFlag(0) = False : curObj.fitCirObj.circleObj.drawFlag(1) = True
        End If
    End Sub
    Public Function UpdateCircleCenterRadius(ByRef curObj As measureObj, mPtF As PointF)
        If curObj.ptCnt = 0 Then
            curObj.circleObj.centerPt.pt.X = mPtF.X : curObj.circleObj.centerPt.pt.Y = mPtF.Y : curObj.ptCnt += 1
            Dim form As CircleCenterRadius = New CircleCenterRadius()
            If form.ShowDialog() = DialogResult.OK Then
                curObj.circleObj.radius = CInt(form.txt_radius.Text)
                curObj.circleObj.drawFlag(0) = True
            End If
        End If
        Return True
    End Function
    Public Function UpdateAngleObj(ByRef curObj As measureObj, mPtF As PointF)
        If curObj.ptCnt = 0 Then
            curObj.angleObj.lineObj_1.midPts(1).pt.X = mPtF.X : curObj.angleObj.lineObj_1.midPts(1).pt.Y = mPtF.Y : curObj.ptCnt += 1 : curObj.angleObj.ptCnt += 1
            Return False
        ElseIf curObj.ptCnt = 1 Then
            curObj.angleObj.midPt.pt.X = mPtF.X : curObj.angleObj.midPt.pt.Y = mPtF.Y : curObj.ptCnt += 1 : curObj.angleObj.ptCnt += 1 : curObj.angleObj.lineObj_1.ptCnt += 1
            Return False
        Else
            curObj.angleObj.lineObj_2.midPts(1).pt.X = mPtF.X : curObj.angleObj.lineObj_2.midPts(1).pt.Y = mPtF.Y : curObj.ptCnt += 1 : curObj.angleObj.ptCnt += 1 : curObj.angleObj.lineObj_2.ptCnt += 1
            Dim X1 As Integer = curObj.angleObj.lineObj_1.midPts(1).pt.X * MainForm.pic_main.Width : Dim Y1 As Integer = curObj.angleObj.lineObj_1.midPts(1).pt.Y * MainForm.pic_main.Height
            Dim X2 As Integer = curObj.angleObj.midPt.pt.X * MainForm.pic_main.Width : Dim Y2 As Integer = curObj.angleObj.midPt.pt.Y * MainForm.pic_main.Height
            Dim X3 As Integer = curObj.angleObj.lineObj_2.midPts(1).pt.X * MainForm.pic_main.Width : Dim Y3 As Integer = curObj.angleObj.lineObj_2.midPts(1).pt.Y * MainForm.pic_main.Height
            curObj.angleObj.angle = CalcAngleBetweenTwoLines(X1, Y1, X2, Y2, X3, Y3)
            ClonePointObj(curObj.angleObj.midPt, curObj.angleObj.lineObj_1.midPts(0))
            curObj.angleObj.lineObj_1.ptCnt += 1 : curObj.angleObj.lineObj_1.drawFlag(0) = True
            ArrangePtsOfLine(curObj.angleObj.lineObj_1)
            GetConstsOfLine(curObj.angleObj.lineObj_1)
            ClonePointObj(curObj.angleObj.midPt, curObj.angleObj.lineObj_2.midPts(0))
            curObj.angleObj.lineObj_2.ptCnt += 1 : curObj.angleObj.lineObj_2.drawFlag(0) = True
            ArrangePtsOfLine(curObj.angleObj.lineObj_2)
            GetConstsOfLine(curObj.angleObj.lineObj_2)
            Return True
        End If
    End Function

    Public Function UpdateAngleFixedObj(ByRef curObj As measureObj, mPtF As PointF)
        If curObj.ptCnt = 0 Then
            curObj.angleObj.lineObj_1.midPts(1).pt.X = mPtF.X : curObj.angleObj.lineObj_1.midPts(1).pt.Y = mPtF.Y : curObj.ptCnt += 1 : curObj.angleObj.ptCnt += 1 : curObj.angleObj.lineObj_1.ptCnt += 1
            curObj.angleObj.fixed = True
            Return False
        Else curObj.ptCnt = 1
            curObj.angleObj.midPt.pt.X = mPtF.X : curObj.angleObj.midPt.pt.Y = mPtF.Y : curObj.ptCnt += 1 : curObj.angleObj.ptCnt += 1
            Dim form As AngleFixedUI = New AngleFixedUI()
            If form.ShowDialog() = DialogResult.OK Then
                curObj.angleObj.angle = CInt(form.txt_angle.Text) : Dim angle = curObj.angleObj.angle : curObj.angleObj.clockwise = form.clockwise
                Dim X1 As Integer = curObj.angleObj.lineObj_1.midPts(1).pt.X * MainForm.pic_main.Width : Dim Y1 As Integer = curObj.angleObj.lineObj_1.midPts(1).pt.Y * MainForm.pic_main.Height
                Dim X2 As Integer = curObj.angleObj.midPt.pt.X * MainForm.pic_main.Width : Dim Y2 As Integer = curObj.angleObj.midPt.pt.Y * MainForm.pic_main.Height
                Dim radius = CalcDistBetweenPoints(X1, Y1, X2, Y2)
                Dim theta = CalcAngleBetweenTwoLines(X1, Y1, X2, Y2, X2 + 10, Y2)
                Dim downFlag = CheckAngleDirection(X2 + 10, Y2, X2, Y2, X1, Y1)
                If Not downFlag Then theta = 360 - theta
                If Not curObj.angleObj.clockwise Then angle = -1 * angle
                Dim edPt = CalcPositionInCircle(New Point(X2, Y2), radius, angle + theta)
                curObj.angleObj.lineObj_2.midPts(1).pt.X = edPt.X / CSng(MainForm.pic_main.Width) : curObj.angleObj.lineObj_2.midPts(1).pt.Y = edPt.Y / CSng(MainForm.pic_main.Height)
                curObj.angleObj.lineObj_1.midPts(0).pt.X = curObj.angleObj.midPt.pt.X : curObj.angleObj.lineObj_1.midPts(0).pt.Y = curObj.angleObj.midPt.pt.Y : curObj.angleObj.lineObj_1.drawFlag(0) = True : curObj.angleObj.lineObj_1.ptCnt += 1
                curObj.angleObj.lineObj_2.midPts(0).pt.X = curObj.angleObj.midPt.pt.X : curObj.angleObj.lineObj_2.midPts(0).pt.Y = curObj.angleObj.midPt.pt.Y : curObj.angleObj.lineObj_2.drawFlag(0) = True : curObj.angleObj.lineObj_2.ptCnt = 2
                ArrangePtsOfLine(curObj.angleObj.lineObj_1)
                ArrangePtsOfLine(curObj.angleObj.lineObj_2)
                GetConstsOfLine(curObj.angleObj.lineObj_1)
                GetConstsOfLine(curObj.angleObj.lineObj_2)
                Return True
            End If
            curObj.Refresh()
            Return False
        End If
    End Function
    Public Function UpdatePointObj(ByRef curObj As measureObj, mPtF As PointF) As Boolean
        curObj.ptObj.pt.X = mPtF.X : curObj.ptObj.pt.Y = mPtF.Y
        Return True
    End Function

    Public Function UpdateAnnoObj(ByRef curObj As measureObj, mPtF As PointF) As Boolean
        If curObj.ptCnt = 0 Then
            curObj.annoObj.Stpt.pt.X = mPtF.X : curObj.annoObj.Stpt.pt.Y = mPtF.Y : curObj.ptCnt += 1 : curObj.annoObj.ptCnt += 1 : Return False
        Else
            curObj.annoObj.EndPt.pt.X = mPtF.X : curObj.annoObj.EndPt.pt.Y = mPtF.Y : curObj.ptCnt += 1 : curObj.annoObj.ptCnt += 1 : Return True
        End If
    End Function

    Public Function UpdateCircle_3Obj(ByRef curObj As measureObj, mPtF As PointF) As Boolean
        If curObj.ptCnt < 2 Then
            curObj.circleObj.midPts(curObj.ptCnt).pt.X = mPtF.X : curObj.circleObj.midPts(curObj.ptCnt).pt.Y = mPtF.Y
            curObj.circleObj.drawFlag(0) = True
            curObj.ptCnt += 1 : curObj.circleObj.ptCnt += 1
            Return False
        ElseIf curObj.ptCnt = 2 Then
            curObj.ptCnt += 1
            If GetRadiusAndCenter(curObj.circleObj, mPtF) Then
                If ArrangePtsOfCircle(curObj.circleObj) Then curObj.circleObj.drawFlag(0) = False : curObj.circleObj.drawFlag(1) = True
                ReSetCircle_3Obj(curObj.circleObj, mPtF, curObj.mType)
                Return True
            Else
                curObj.Refresh()
                Return False
            End If

        End If
    End Function

    Public Sub UpdateCircle_3ObjFromCurve(ByRef curObj As measureObj, curveObj As CurveObj)
        curObj.circleObj.midPts(0).pt = curveObj.CurvePoint(0)
        curObj.circleObj.midPts(1).pt = curveObj.CurvePoint(curveObj.CPointIndx / 2)
        Dim tempPt = curveObj.CurvePoint(curveObj.CPointIndx)
        curObj.ptCnt = 2 : curObj.circleObj.ptCnt = 2
        UpdateCircle_3Obj(curObj, tempPt)
    End Sub
    Public Function UpdateObj(ByRef curObj As measureObj, curMType As Integer, mPtF As PointF)
        Dim completed As Boolean
        Select Case curMType
            Case 1
                completed = UpdatePointObj(curObj, mPtF)
            Case 3
                completed = UpdateLineObj(curObj, mPtF)
            Case 5
                completed = UpdateCircleWithCenterObj(curObj, mPtF)
            Case 30
                completed = UpdateAngleObj(curObj, mPtF)
            Case 31
                completed = UpdateAngleFixedObj(curObj, mPtF)
            Case 61
                completed = UpdateCircleCenterRadius(curObj, mPtF)
            Case 50
                UpdateFitLineObj(curObj, mPtF)

            Case 62, 63
                UpdateFitCircleObj(curObj, mPtF)

            Case 64, 65
                completed = UpdateCircle_3Obj(curObj, mPtF)
            Case 106
                completed = UpdateAnnoObj(curObj, mPtF)
        End Select
        Return completed
    End Function

    Public Sub CompareLineAndLineObj(ByRef Obj1 As measureObj, ByRef Obj2 As measureObj)
        Dim datingPt = GetDatingPtLineAndLine(Obj1.lineObj.m, Obj1.lineObj.b * MainForm.zoomFactor, Obj2.lineObj.m, Obj2.lineObj.b * MainForm.zoomFactor)
        datingPt.X /= MainForm.pic_main.Width : datingPt.Y /= MainForm.pic_main.Height
        Dim Obj1_backUp, Obj2_backup As New measureObj
        InitializeMeasureObj(Obj1_backUp)
        InitializeMeasureObj(Obj2_backup)
        CloneMeasureObj(Obj1, Obj1_backUp)
        CloneMeasureObj(Obj2, Obj2_backup)

        Dim a1 = ReSetLineObj(Obj1.lineObj, datingPt)
        Dim a2 = ReSetLineObj(Obj2.lineObj, datingPt)
        If a1 <> a2 Then
            CloneMeasureObj(Obj1_backUp, Obj1)
            CloneMeasureObj(Obj2_backup, Obj2)
        End If
    End Sub

    Public Sub CompareLineAndCircleObj(ByRef Obj1 As measureObj, ByRef Obj2 As measureObj) 'Obj1 is lineObj, Obj2 is circleObj
        Dim m = Obj1.lineObj.m : Dim c = Obj1.lineObj.b * MainForm.zoomFactor
        Dim a = Obj2.circleObj.centerPt.pt.X * MainForm.pic_main.Width
        Dim b = Obj2.circleObj.centerPt.pt.Y * MainForm.pic_main.Height
        Dim r = Obj2.circleObj.radius * MainForm.zoomFactor
        Dim datingPtList = GetDatingPtLineAndCircle(m, c, a, b, r)

        Dim Obj1_backUp, Obj2_backup As New measureObj
        InitializeMeasureObj(Obj1_backUp)
        InitializeMeasureObj(Obj2_backup)
        CloneMeasureObj(Obj1, Obj1_backUp)
        CloneMeasureObj(Obj2, Obj2_backup)

        For Each datingPt In datingPtList
            datingPt.X /= MainForm.pic_main.Width : datingPt.Y /= MainForm.pic_main.Height
            If ReSetLineObj(Obj1_backUp.lineObj, datingPt) = ReSetCircleObj(Obj2_backup.circleObj, datingPt, Obj2_backup.mType) Then
                ReSetLineObj(Obj1.lineObj, datingPt)
                ReSetCircleObj(Obj2.circleObj, datingPt, Obj2.mType)
            End If
        Next
    End Sub

    Public Sub CompareLineAndFitCircleObj(ByRef Obj1 As measureObj, ByRef Obj2 As measureObj) 'Obj1 is lineObj, Obj2 is FitcircleObj
        Dim m = Obj1.lineObj.m : Dim c = Obj1.lineObj.b * MainForm.zoomFactor
        Dim a = Obj2.fitCirObj.circleObj.centerPt.pt.X * MainForm.pic_main.Width
        Dim b = Obj2.fitCirObj.circleObj.centerPt.pt.Y * MainForm.pic_main.Height
        Dim r = Obj2.fitCirObj.circleObj.radius * MainForm.zoomFactor
        Dim datingPtList = GetDatingPtLineAndCircle(m, c, a, b, r)

        Dim Obj1_backUp, Obj2_backup As New measureObj
        InitializeMeasureObj(Obj1_backUp)
        InitializeMeasureObj(Obj2_backup)
        CloneMeasureObj(Obj1, Obj1_backUp)
        CloneMeasureObj(Obj2, Obj2_backup)

        For Each datingPt In datingPtList
            datingPt.X /= MainForm.pic_main.Width : datingPt.Y /= MainForm.pic_main.Height
            If ReSetLineObj(Obj1_backUp.lineObj, datingPt) = ReSetCircleObj(Obj2_backup.fitCirObj.circleObj, datingPt, Obj2_backup.mType) Then
                ReSetLineObj(Obj1.lineObj, datingPt)
                ReSetCircleObj(Obj2.fitCirObj.circleObj, datingPt, Obj2.mType)
            End If
        Next
    End Sub

    Public Sub CompareLineAndAngleObj(ByRef Obj1 As measureObj, ByRef Obj2 As measureObj) ' OBj1 is lineObj, Obj2 is angleObj
        Dim datingPt = GetDatingPtLineAndLine(Obj1.lineObj.m, Obj1.lineObj.b * MainForm.zoomFactor, Obj2.angleObj.lineObj_1.m, Obj2.angleObj.lineObj_1.b * MainForm.zoomFactor)
        datingPt.X /= MainForm.pic_main.Width : datingPt.Y /= MainForm.pic_main.Height

        Dim Obj1_backUp, Obj2_backup As New lineObj
        InitializeLineObj(Obj1_backUp)
        InitializeLineObj(Obj2_backup)
        CloneLineObj(Obj1.lineObj, Obj1_backUp)
        CloneLineObj(Obj2.angleObj.lineObj_1, Obj2_backup)

        If ReSetLineObj(Obj1.lineObj, datingPt) <> ReSetLineObj(Obj2.angleObj.lineObj_1, datingPt) Then
            CloneLineObj(Obj1_backUp, Obj1.lineObj)
            CloneLineObj(Obj2_backup, Obj2.angleObj.lineObj_1)
        End If

        datingPt = GetDatingPtLineAndLine(Obj1.lineObj.m, Obj1.lineObj.b * MainForm.zoomFactor, Obj2.angleObj.lineObj_2.m, Obj2.angleObj.lineObj_2.b * MainForm.zoomFactor)
        datingPt.X /= MainForm.pic_main.Width : datingPt.Y /= MainForm.pic_main.Height
        CloneLineObj(Obj2.angleObj.lineObj_2, Obj2_backup)

        If ReSetLineObj(Obj1.lineObj, datingPt) <> ReSetLineObj(Obj2.angleObj.lineObj_2, datingPt) Then
            CloneLineObj(Obj1_backUp, Obj1.lineObj)
            CloneLineObj(Obj2_backup, Obj2.angleObj.lineObj_2)
        End If
    End Sub

    Public Sub CompareAngleAndAngleObj(ByRef Obj1 As measureObj, ByRef Obj2 As measureObj)
        Dim datingPt = GetDatingPtLineAndLine(Obj1.angleObj.lineObj_1.m, Obj1.angleObj.lineObj_1.b * MainForm.zoomFactor, Obj2.angleObj.lineObj_1.m, Obj2.angleObj.lineObj_1.b * MainForm.zoomFactor)
        datingPt.X /= MainForm.pic_main.Width : datingPt.Y /= MainForm.pic_main.Height
        Dim Obj1_backUp, Obj2_backup As New lineObj
        InitializeLineObj(Obj1_backUp)
        InitializeLineObj(Obj2_backup)
        CloneLineObj(Obj1.angleObj.lineObj_1, Obj1_backUp)
        CloneLineObj(Obj2.angleObj.lineObj_1, Obj2_backup)

        If ReSetLineObj(Obj1.angleObj.lineObj_1, datingPt) <> ReSetLineObj(Obj2.angleObj.lineObj_1, datingPt) Then
            CloneLineObj(Obj1_backUp, Obj1.angleObj.lineObj_1)
            CloneLineObj(Obj2_backup, Obj2.angleObj.lineObj_1)
        End If

        datingPt = GetDatingPtLineAndLine(Obj1.angleObj.lineObj_1.m, Obj1.angleObj.lineObj_1.b * MainForm.zoomFactor, Obj2.angleObj.lineObj_2.m, Obj2.angleObj.lineObj_2.b * MainForm.zoomFactor)
        datingPt.X /= MainForm.pic_main.Width : datingPt.Y /= MainForm.pic_main.Height

        CloneLineObj(Obj1.angleObj.lineObj_1, Obj1_backUp)
        CloneLineObj(Obj2.angleObj.lineObj_2, Obj2_backup)

        If ReSetLineObj(Obj1.angleObj.lineObj_1, datingPt) <> ReSetLineObj(Obj2.angleObj.lineObj_2, datingPt) Then
            CloneLineObj(Obj1_backUp, Obj1.angleObj.lineObj_1)
            CloneLineObj(Obj2_backup, Obj2.angleObj.lineObj_2)
        End If

        datingPt = GetDatingPtLineAndLine(Obj1.angleObj.lineObj_2.m, Obj1.angleObj.lineObj_2.b * MainForm.zoomFactor, Obj2.angleObj.lineObj_1.m, Obj2.angleObj.lineObj_1.b * MainForm.zoomFactor)
        datingPt.X /= MainForm.pic_main.Width : datingPt.Y /= MainForm.pic_main.Height

        CloneLineObj(Obj1.angleObj.lineObj_2, Obj1_backUp)
        CloneLineObj(Obj2.angleObj.lineObj_1, Obj2_backup)

        If ReSetLineObj(Obj1.angleObj.lineObj_2, datingPt) <> ReSetLineObj(Obj2.angleObj.lineObj_1, datingPt) Then
            CloneLineObj(Obj1_backUp, Obj1.angleObj.lineObj_2)
            CloneLineObj(Obj2_backup, Obj2.angleObj.lineObj_1)
        End If

        datingPt = GetDatingPtLineAndLine(Obj1.angleObj.lineObj_2.m, Obj1.angleObj.lineObj_2.b * MainForm.zoomFactor, Obj2.angleObj.lineObj_2.m, Obj2.angleObj.lineObj_2.b * MainForm.zoomFactor)
        datingPt.X /= MainForm.pic_main.Width : datingPt.Y /= MainForm.pic_main.Height

        CloneLineObj(Obj1.angleObj.lineObj_2, Obj1_backUp)
        CloneLineObj(Obj2.angleObj.lineObj_2, Obj2_backup)

        If ReSetLineObj(Obj1.angleObj.lineObj_2, datingPt) <> ReSetLineObj(Obj2.angleObj.lineObj_2, datingPt) Then
            CloneLineObj(Obj1_backUp, Obj1.angleObj.lineObj_2)
            CloneLineObj(Obj2_backup, Obj2.angleObj.lineObj_2)
        End If
    End Sub
    Public Sub CompareCircleAndCircleObj(ByRef Obj1 As measureObj, ByRef Obj2 As measureObj)
        Dim a1 = Obj1.circleObj.centerPt.pt.X * MainForm.pic_main.Width
        Dim b1 = Obj1.circleObj.centerPt.pt.Y * MainForm.pic_main.Height
        Dim r1 = Obj1.circleObj.radius * MainForm.zoomFactor
        Dim a2 = Obj2.circleObj.centerPt.pt.X * MainForm.pic_main.Width
        Dim b2 = Obj2.circleObj.centerPt.pt.Y * MainForm.pic_main.Height
        Dim r2 = Obj2.circleObj.radius * MainForm.zoomFactor

        Dim Obj1_backUp, Obj2_backup As New measureObj
        InitializeMeasureObj(Obj1_backUp)
        InitializeMeasureObj(Obj2_backup)
        CloneMeasureObj(Obj1, Obj1_backUp)
        CloneMeasureObj(Obj2, Obj2_backup)

        Dim datingPtList = GetDatingPtCircleAndCircle(a1, b1, r1, a2, b2, r2)
        For Each datingPt In datingPtList
            datingPt.X /= MainForm.pic_main.Width : datingPt.Y /= MainForm.pic_main.Height
            If ReSetCircleObj(Obj1_backUp.circleObj, datingPt, Obj1_backUp.mType) = ReSetCircleObj(Obj2_backup.circleObj, datingPt, Obj2_backup.mType) Then
                ReSetCircleObj(Obj1.circleObj, datingPt, Obj1.mType)
                ReSetCircleObj(Obj2.circleObj, datingPt, Obj2.mType)
            End If
        Next
    End Sub

    Public Sub CompareCircleAndFitCircleObj(ByRef Obj1 As measureObj, ByRef Obj2 As measureObj) ' Obj1 is circle, Obj2 is fit circle
        Dim a1 = Obj1.circleObj.centerPt.pt.X * MainForm.pic_main.Width
        Dim b1 = Obj1.circleObj.centerPt.pt.Y * MainForm.pic_main.Height
        Dim r1 = Obj1.circleObj.radius * MainForm.zoomFactor
        Dim a2 = Obj2.fitCirObj.circleObj.centerPt.pt.X * MainForm.pic_main.Width
        Dim b2 = Obj2.fitCirObj.circleObj.centerPt.pt.Y * MainForm.pic_main.Height
        Dim r2 = Obj2.fitCirObj.circleObj.radius * MainForm.zoomFactor

        Dim Obj1_backUp, Obj2_backup As New measureObj
        InitializeMeasureObj(Obj1_backUp)
        InitializeMeasureObj(Obj2_backup)
        CloneMeasureObj(Obj1, Obj1_backUp)
        CloneMeasureObj(Obj2, Obj2_backup)

        Dim datingPtList = GetDatingPtCircleAndCircle(a1, b1, r1, a2, b2, r2)
        For Each datingPt In datingPtList
            datingPt.X /= MainForm.pic_main.Width : datingPt.Y /= MainForm.pic_main.Height
            If ReSetCircleObj(Obj1_backUp.circleObj, datingPt, Obj1_backUp.mType) = ReSetCircleObj(Obj2_backup.fitCirObj.circleObj, datingPt, Obj2_backup.mType) Then
                ReSetCircleObj(Obj1.circleObj, datingPt, Obj1.mType)
                ReSetCircleObj(Obj2.fitCirObj.circleObj, datingPt, Obj2.mType)
            End If
        Next
    End Sub

    Public Sub CompareFitCircleAndFitCircleObj(ByRef Obj1 As measureObj, ByRef Obj2 As measureObj)
        Dim a1 = Obj1.fitCirObj.circleObj.centerPt.pt.X * MainForm.pic_main.Width
        Dim b1 = Obj1.fitCirObj.circleObj.centerPt.pt.Y * MainForm.pic_main.Height
        Dim r1 = Obj1.fitCirObj.circleObj.radius * MainForm.zoomFactor
        Dim a2 = Obj2.fitCirObj.circleObj.centerPt.pt.X * MainForm.pic_main.Width
        Dim b2 = Obj2.fitCirObj.circleObj.centerPt.pt.Y * MainForm.pic_main.Height
        Dim r2 = Obj2.fitCirObj.circleObj.radius * MainForm.zoomFactor

        Dim Obj1_backUp, Obj2_backup As New measureObj
        InitializeMeasureObj(Obj1_backUp)
        InitializeMeasureObj(Obj2_backup)
        CloneMeasureObj(Obj1, Obj1_backUp)
        CloneMeasureObj(Obj2, Obj2_backup)

        Dim datingPtList = GetDatingPtCircleAndCircle(a1, b1, r1, a2, b2, r2)
        For Each datingPt In datingPtList
            datingPt.X /= MainForm.pic_main.Width : datingPt.Y /= MainForm.pic_main.Height
            If ReSetCircleObj(Obj1_backUp.fitCirObj.circleObj, datingPt, Obj1_backUp.mType) = ReSetCircleObj(Obj2_backup.fitCirObj.circleObj, datingPt, Obj2_backup.mType) Then
                ReSetCircleObj(Obj1.fitCirObj.circleObj, datingPt, Obj1.mType)
                ReSetCircleObj(Obj2.fitCirObj.circleObj, datingPt, Obj2.mType)
            End If
        Next
    End Sub

    Public Sub CompareCircleAndAngleObj(ByRef Obj1 As measureObj, ByRef Obj2 As measureObj) 'Obj1 is circleOBj, Obj2 is angleObj
        Dim a = Obj1.circleObj.centerPt.pt.X * MainForm.pic_main.Width
        Dim b = Obj1.circleObj.centerPt.pt.Y * MainForm.pic_main.Height
        Dim r = Obj1.circleObj.radius * MainForm.zoomFactor

        Dim m1 = Obj2.angleObj.lineObj_1.m : Dim c1 = Obj2.angleObj.lineObj_1.b * MainForm.zoomFactor
        Dim m2 = Obj2.angleObj.lineObj_2.m : Dim c2 = Obj2.angleObj.lineObj_2.b * MainForm.zoomFactor

        Dim datingPtList1 = GetDatingPtLineAndCircle(m1, c1, a, b, r)
        Dim Obj1_backUp As New measureObj
        Dim Obj2_backUp As New lineObj
        InitializeMeasureObj(Obj1_backUp)
        InitializeLineObj(Obj2_backUp)
        CloneMeasureObj(Obj1, Obj1_backUp)
        CloneLineObj(Obj2.angleObj.lineObj_1, Obj2_backUp)

        For Each datingPt In datingPtList1
            datingPt.X /= MainForm.pic_main.Width : datingPt.Y /= MainForm.pic_main.Height
            If ReSetCircleObj(Obj1_backUp.circleObj, datingPt, Obj1_backUp.mType) = ReSetLineObj(Obj2_backUp, datingPt) Then
                ReSetCircleObj(Obj1.circleObj, datingPt, Obj1.mType)
                ReSetLineObj(Obj2.angleObj.lineObj_1, datingPt)
            End If
        Next

        CloneLineObj(Obj2.angleObj.lineObj_2, Obj2_backUp)
        Dim datingPtList2 = GetDatingPtLineAndCircle(m2, c2, a, b, r)
        For Each datingPt In datingPtList2
            datingPt.X /= MainForm.pic_main.Width : datingPt.Y /= MainForm.pic_main.Height
            If ReSetCircleObj(Obj1_backUp.circleObj, datingPt, Obj1_backUp.mType) = ReSetLineObj(Obj2_backUp, datingPt) Then
                ReSetCircleObj(Obj1.circleObj, datingPt, Obj1.mType)
                ReSetLineObj(Obj2.angleObj.lineObj_2, datingPt)
            End If
        Next
    End Sub

    Public Sub CompareFitCircleAndAngleObj(ByRef Obj1 As measureObj, ByRef Obj2 As measureObj) 'Obj1 is fit circle, Obj2 is angle
        Dim a = Obj1.fitCirObj.circleObj.centerPt.pt.X * MainForm.pic_main.Width
        Dim b = Obj1.fitCirObj.circleObj.centerPt.pt.Y * MainForm.pic_main.Height
        Dim r = Obj1.fitCirObj.circleObj.radius * MainForm.zoomFactor

        Dim m1 = Obj2.angleObj.lineObj_1.m : Dim c1 = Obj2.angleObj.lineObj_1.b * MainForm.zoomFactor
        Dim m2 = Obj2.angleObj.lineObj_2.m : Dim c2 = Obj2.angleObj.lineObj_2.b * MainForm.zoomFactor
        Dim Obj1_backUp As New measureObj
        Dim Obj2_backUp As New lineObj
        InitializeMeasureObj(Obj1_backUp)
        InitializeLineObj(Obj2_backUp)
        CloneMeasureObj(Obj1, Obj1_backUp)
        CloneLineObj(Obj2.angleObj.lineObj_1, Obj2_backUp)

        Dim datingPtList1 = GetDatingPtLineAndCircle(m1, c1, a, b, r)
        For Each datingPt In datingPtList1
            datingPt.X /= MainForm.pic_main.Width : datingPt.Y /= MainForm.pic_main.Height
            If ReSetCircleObj(Obj1_backUp.fitCirObj.circleObj, datingPt, Obj1_backUp.mType) = ReSetLineObj(Obj2_backUp, datingPt) Then
                ReSetCircleObj(Obj1.fitCirObj.circleObj, datingPt, Obj1.mType)
                ReSetLineObj(Obj2.angleObj.lineObj_1, datingPt)
            End If
        Next

        CloneLineObj(Obj2.angleObj.lineObj_2, Obj2_backUp)
        Dim datingPtList2 = GetDatingPtLineAndCircle(m2, c2, a, b, r)
        For Each datingPt In datingPtList2
            datingPt.X /= MainForm.pic_main.Width : datingPt.Y /= MainForm.pic_main.Height
            If ReSetCircleObj(Obj1_backUp.fitCirObj.circleObj, datingPt, Obj1_backUp.mType) = ReSetLineObj(Obj2_backUp, datingPt) Then
                ReSetCircleObj(Obj1.fitCirObj.circleObj, datingPt, Obj1.mType)
                ReSetLineObj(Obj2.angleObj.lineObj_2, datingPt)
            End If
        Next
    End Sub
    Public Sub CompareWithExisingObjs(ByRef curObj As measureObj, objList As List(Of measureObj))
        Dim curMtype = curObj.mType
        For i = 0 To objList.Count - 1
            Dim Obj = objList(i)
            Dim tarMtype = Obj.mType
            Select Case curMtype
                Case 3, 50
                    Select Case tarMtype
                        Case 3, 50 'line and line
                            CompareLineAndLineObj(curObj, Obj)
                        Case 5, 61, 64 'line and circle
                            CompareLineAndCircleObj(curObj, Obj)
                        Case 62, 63 ' line and fit circle
                            CompareLineAndFitCircleObj(curObj, Obj)
                        Case 30, 31 'line and angle
                            CompareLineAndAngleObj(curObj, Obj)
                    End Select

                Case 5, 61, 64
                    Select Case tarMtype
                        Case 3, 50 'circle and line
                            CompareLineAndCircleObj(Obj, curObj)
                        Case 5, 61, 64 'circle and circle
                            CompareCircleAndCircleObj(curObj, Obj)
                        Case 62, 63 ' circle and fit circle
                            CompareCircleAndFitCircleObj(curObj, Obj)
                        Case 30, 31 'circle and angle
                            CompareCircleAndAngleObj(curObj, Obj)
                    End Select
                Case 62, 63
                    Select Case tarMtype
                        Case 3, 50 'fit circle and line
                            CompareLineAndFitCircleObj(Obj, curObj)
                        Case 5, 61, 64 'fit circle and circle
                            CompareCircleAndFitCircleObj(Obj, curObj)
                        Case 62, 63 'fit circle and fit circle
                            CompareFitCircleAndFitCircleObj(Obj, curObj)
                        Case 30, 31 'fit circle and angle
                            CompareFitCircleAndFitCircleObj(curObj, Obj)
                    End Select
                Case 30, 31
                    Select Case tarMtype
                        Case 3, 50 'angle and line
                            CompareLineAndAngleObj(Obj, curObj)
                        Case 5, 61, 64 'angle and circle
                            CompareCircleAndAngleObj(Obj, curObj)
                        Case 62, 63 'angle and fit circle
                            CompareFitCircleAndAngleObj(Obj, curObj)
                        Case 30, 31 'angle and angle
                            CompareAngleAndAngleObj(curObj, Obj)
                    End Select
            End Select
            objList(i) = Obj
        Next
    End Sub
    Public Sub DrawString(g As Graphics, pic As PictureBox, ptF As PointF, str As String)
        g.DrawString(str, MainForm.drawFont, MainForm.drawBrush, New Point(ptF.X * pic.Width + 10, ptF.Y * pic.Height + 10))
    End Sub
    Public Sub DrawPoint(g As Graphics, pic As PictureBox, PtF As PointF, Optional redFlag As Boolean = False)
        If redFlag Then
            g.FillEllipse(MainForm.RedBrush, New Rectangle(PtF.X * pic.Width - 5, PtF.Y * pic.Height - 5, 10, 10))
        Else
            g.FillEllipse(MainForm.ptBrush, New Rectangle(PtF.X * pic.Width - 5, PtF.Y * pic.Height - 5, 10, 10))
        End If
    End Sub

    Public Sub DrawLine(g As Graphics, pic As PictureBox, X1 As Integer, Y1 As Integer, X2 As Integer, Y2 As Integer, Optional redflag As Boolean = False)
        If redflag Then
            g.DrawLine(MainForm.redPen, X1, Y1, X2, Y2)
        Else
            g.DrawLine(MainForm.drawPen, X1, Y1, X2, Y2)
        End If
    End Sub
    Public Sub DrawLine(g As Graphics, pic As PictureBox, pt1 As PointF, pt2 As PointF, Optional redflag As Boolean = False)
        DrawLine(g, pic, pt1.X * pic.Width, pt1.Y * pic.Height, pt2.X * pic.Width, pt2.Y * pic.Height, redflag)
    End Sub

    Public Sub DrawLongLine(g As Graphics, pic As PictureBox, pt1 As PointF, pt2 As PointF)
        Dim X1 As Integer = pt1.X * pic.Width : Dim Y1 As Integer = pt1.Y * pic.Height
        Dim X2 As Integer = pt2.X * pic.Width : Dim Y2 As Integer = pt2.Y * pic.Height
        Dim dx = X2 - X1 : Dim dy = Y2 - Y1
        Dim val1, val2 As Double
        If dx = 0 And dy = 0 Then Return
        If dx = 0 Then
            val1 = Y1 / dy
            val2 = (pic.Height - Y2) / dy

        ElseIf dy = 0 Then
            val1 = X1 / dx
            val2 = (pic.Width - X2) / dx
        Else
            val1 = Math.Min(X1 / dx, Y1 / dy)
            val2 = Math.Min((pic.Width - X2) / dx, (pic.Height - Y2) / dy)
        End If
        Dim X_LeftTop As Integer = X1 - val1 * dx : Dim Y_LeftTop As Integer = Y1 - val1 * dy
        Dim X_RightBottom As Integer = X2 + val2 * dx : Dim Y_RightBottom As Integer = Y2 + val2 * dy

        g.DrawLine(MainForm.drawPen, X_LeftTop, Y_LeftTop, X_RightBottom, Y_RightBottom)
    End Sub

    Public Sub DrawLineObj(g As Graphics, pic As PictureBox, ByRef curObj As lineObj, mPtF As PointF, objID As Integer, Optional redFlag As Boolean = False)
        If curObj.ptCnt < 1 Then Return
        DrawPoint(g, pic, curObj.midPts(0).pt)
        If curObj.ptCnt = 1 Then
            curObj.midPts(1).pt.X = mPtF.X : curObj.midPts(1).pt.Y = mPtF.Y
        End If

        Dim ptCnt As Integer = curObj.ptCnt
        If ptCnt = 1 Then ptCnt = 2
        For i = 1 To ptCnt - 1
            DrawPoint(g, pic, curObj.midPts(i).pt)
            If curObj.drawFlag(i - 1) Then
                DrawLine(g, pic, curObj.midPts(i - 1).pt, curObj.midPts(i).pt)
            End If
            If MainForm.eraseState Or MainForm.expandState Then
                If redFlag And objID = MainForm.selectedObj And i - 1 = MainForm.selectedSegment Then
                    DrawLine(g, pic, curObj.midPts(i - 1).pt, curObj.midPts(i).pt, True)
                    Continue For
                End If
            End If
            If MainForm.selectState Then
                If objID = MainForm.selectedObj Then
                    DrawLine(g, pic, curObj.midPts(i - 1).pt, curObj.midPts(i).pt, True)
                End If
            End If
            If objID = MainForm.selectedObjSet(0) Or objID = MainForm.selectedObjSet(1) Then
                DrawLine(g, pic, curObj.midPts(i - 1).pt, curObj.midPts(i).pt, True)
            End If
        Next
    End Sub

    Public Sub DrawLongLine(g As Graphics, pic As PictureBox, m As Double, b As Double)
        Dim X1 As Integer = 0
        Dim Y1 As Integer = X1 * m + b
        Dim X2 As Integer = pic.Width
        Dim Y2 As Integer = X2 * m + b
        g.DrawLine(MainForm.drawPen, X1, Y1, X2, Y2)
    End Sub

    Public Sub DrawFitLineObj(g As Graphics, pic As PictureBox, curObj As fitLineObj, objID As Integer)
        For i = 0 To curObj.ptCnt - 1
            DrawPoint(g, pic, curObj.refPts(i).pt)
        Next
        If curObj.completed Then
            'DrawLongLine(g, pic, curObj.lineObj.m, curObj.lineObj.b * MainForm.zoomFactor)
            Dim mPtF As New PointF(0, 0)
            DrawLineObj(g, pic, curObj.lineObj, mPtF, objID)
        End If
    End Sub

    Public Sub DrawCircle(g As Graphics, pic As PictureBox, pt1 As PointF, pt2 As PointF)
        Dim CenterX As Integer = pt1.X * pic.Width : Dim CenterY As Integer = pt1.Y * pic.Height
        Dim OnX As Integer = pt2.X * pic.Width : Dim OnY As Integer = pt2.Y * pic.Height
        Dim radius = Math.Sqrt(Math.Pow(CenterX - OnX, 2) + Math.Pow(CenterY - OnY, 2))
        g.DrawEllipse(MainForm.drawPen, New Rectangle(CenterX - radius, CenterY - radius, radius * 2, radius * 2))
    End Sub

    Public Sub DrawCircle(g As Graphics, pic As PictureBox, pt As PointF, radius As Integer)
        Dim CenterX As Integer = pt.X * pic.Width : Dim CenterY As Integer = pt.Y * pic.Height
        g.DrawEllipse(MainForm.drawPen, New Rectangle(CenterX - radius, CenterY - radius, radius * 2, radius * 2))
    End Sub
    Public Sub DrawCircleWithCenterObj(g As Graphics, pic As PictureBox, ByRef curObj As circleObj, mPtF As PointF, objId As Integer)
        DrawPoint(g, pic, curObj.centerPt.pt)
        If curObj.ptCnt = 0 Then
            curObj.midPts(0).pt.X = mPtF.X : curObj.midPts(0).pt.Y = mPtF.Y
        End If

        For i = 0 To curObj.ptCnt - 1
            DrawPoint(g, pic, curObj.midPts(i).pt)
        Next
        DrawArc(g, pic, curObj, objId)
    End Sub

    Public Sub DrawCircleCenterRadius(g As Graphics, pic As PictureBox, curObj As circleObj, mPtF As PointF, objId As Integer)
        DrawPoint(g, pic, curObj.centerPt.pt)
        For i = 0 To curObj.ptCnt - 1
            DrawPoint(g, pic, curObj.midPts(i).pt)
        Next
        DrawArc(g, pic, curObj, objId)
    End Sub

    Public Sub DrawFitCircleObj(g As Graphics, pic As PictureBox, curObj As fitCircleObj, objID As Integer)
        For i = 0 To curObj.ptCnt - 1
            DrawPoint(g, pic, curObj.refPts(i).pt)
        Next
        If curObj.completed Then
            DrawPoint(g, pic, curObj.circleObj.centerPt.pt)
            For i = 0 To curObj.circleObj.ptCnt - 1
                DrawPoint(g, pic, curObj.circleObj.midPts(i).pt)
            Next
            DrawArc(g, pic, curObj.circleObj, objID)
        End If
    End Sub

    Public Sub DrawArc(g As Graphics, pic As PictureBox, curObj As circleObj, objID As Integer)
        Dim X2 As Integer = curObj.centerPt.pt.X * pic.Width : Dim Y2 As Integer = curObj.centerPt.pt.Y * pic.Height
        DrawPoint(g, pic, curObj.centerPt.pt)
        Dim ptCnt = curObj.ptCnt
        Dim X1, Y1, X3, Y3, theta, sweep As New Single
        Dim radius = curObj.radius * MainForm.zoomFactor
        If ptCnt >= 2 Then
            For i = 0 To ptCnt - 2
                X1 = curObj.midPts(i).pt.X * pic.Width : Y1 = curObj.midPts(i).pt.Y * pic.Height
                X3 = curObj.midPts(i + 1).pt.X * pic.Width : Y3 = curObj.midPts(i + 1).pt.Y * pic.Height
                theta = CalcAngleFromYaxis(X1, Y1, X2, Y2)
                sweep = CalcAngleFromYaxis(X3, Y3, X2, Y2)
                If curObj.drawFlag(i) = True Then
                    If radius <> 0 Then g.DrawArc(MainForm.drawPen, New Rectangle(X2 - radius, Y2 - radius, radius * 2, radius * 2), CSng(theta), CSng((sweep - theta + 360) Mod 360))
                End If
                If MainForm.eraseState Or MainForm.expandState Then
                    If objID = MainForm.selectedObj And i = MainForm.selectedSegment Then
                        If radius <> 0 Then g.DrawArc(MainForm.redPen, New Rectangle(X2 - radius, Y2 - radius, radius * 2, radius * 2), CSng(theta), CSng((sweep - theta + 360) Mod 360))
                        Continue For
                    End If
                End If
            Next
            X1 = curObj.midPts(ptCnt - 1).pt.X * pic.Width : Y1 = curObj.midPts(ptCnt - 1).pt.Y * pic.Height
            X3 = curObj.midPts(0).pt.X * pic.Width : Y3 = curObj.midPts(0).pt.Y * pic.Height
            theta = CalcAngleFromYaxis(X1, Y1, X2, Y2)
            sweep = CalcAngleFromYaxis(X3, Y3, X2, Y2)
            If curObj.drawFlag(ptCnt - 1) Then
                If radius <> 0 Then g.DrawArc(MainForm.drawPen, New Rectangle(X2 - radius, Y2 - radius, radius * 2, radius * 2), CSng(theta), CSng((sweep - theta + 360) Mod 360))
            End If
            If MainForm.eraseState Or MainForm.expandState Then
                If objID = MainForm.selectedObj And ptCnt - 1 = MainForm.selectedSegment Then
                    If radius <> 0 Then g.DrawArc(MainForm.redPen, New Rectangle(X2 - radius, Y2 - radius, radius * 2, radius * 2), CSng(theta), CSng((sweep - theta + 360) Mod 360))
                    Return
                End If
            End If
        Else
            X3 = curObj.midPts(0).pt.X * pic.Width : Y3 = curObj.midPts(0).pt.Y * pic.Height
            If X3 <> 0 Or Y3 <> 0 Then
                radius = Find_TwoPointDistance(X2, Y2, X3, Y3)
            End If
            If curObj.drawFlag(0) Then
                g.DrawEllipse(MainForm.drawPen, New Rectangle(X2 - radius, Y2 - radius, radius * 2, radius * 2))
            End If
            radius = curObj.radius * MainForm.zoomFactor
            If MainForm.eraseState Or MainForm.expandState Then
                If objID = MainForm.selectedObj And 0 = MainForm.selectedSegment Then
                    g.DrawEllipse(MainForm.redPen, New Rectangle(X2 - radius, Y2 - radius, radius * 2, radius * 2))
                    Return
                End If
            End If
        End If
        If MainForm.selectState Then
            If objID = MainForm.selectedObj Then
                g.DrawEllipse(MainForm.redPen, New Rectangle(X2 - radius, Y2 - radius, radius * 2, radius * 2))
                Return
            End If
        End If
        If objID = MainForm.selectedObjSet(0) Or objID = MainForm.selectedObjSet(1) Then
            g.DrawEllipse(MainForm.redPen, New Rectangle(X2 - radius, Y2 - radius, radius * 2, radius * 2))
            Return
        End If
    End Sub

    Public Sub DrawAngle(g As Graphics, pic As PictureBox, ByRef curObj As angleObj)
        Dim X2 As Integer = curObj.midPt.pt.X * pic.Width : Dim Y2 As Integer = curObj.midPt.pt.Y * pic.Height
        Dim X1 As Integer = curObj.lineObj_1.midPts(1).pt.X * pic.Width : Dim Y1 As Integer = curObj.lineObj_1.midPts(1).pt.Y * pic.Height
        Dim X3 As Integer = curObj.lineObj_2.midPts(1).pt.X * pic.Width : Dim Y3 As Integer = curObj.lineObj_2.midPts(1).pt.Y * pic.Height

        If X1 = X2 And Y1 = Y2 Then
            X1 = curObj.lineObj_1.midPts(0).pt.X * pic.Width : Y1 = curObj.lineObj_1.midPts(0).pt.Y * pic.Height
        End If
        If X3 = X2 And Y3 = Y2 Then
            X3 = curObj.lineObj_2.midPts(0).pt.X * pic.Width : Y3 = curObj.lineObj_2.midPts(0).pt.Y * pic.Height
        End If
        Dim angle = CalcAngleBetweenTwoLines(X1, Y1, X2, Y2, X3, Y3)
        Dim clockwise = CheckAngleDirection(X1, Y1, X2, Y2, X3, Y3)
        Dim theta = CalcAngleBetweenTwoLines(X1, Y1, X2, Y2, X2 + 10, Y2)
        Dim downFlag = CheckAngleDirection(X2 + 10, Y2, X2, Y2, X1, Y1)
        If Not downFlag Then theta = 360 - theta
        If Not clockwise Then angle = -1 * angle

        g.DrawArc(MainForm.drawPen, New Rectangle(X2 - 25, Y2 - 25, 50, 50), CSng(theta), CSng(angle))
    End Sub

    Public Sub DrawAngleFixed(g As Graphics, pic As PictureBox, curObj As angleObj)
        Dim X1 As Integer = curObj.lineObj_1.midPts(1).pt.X * pic.Width : Dim Y1 As Integer = curObj.lineObj_1.midPts(1).pt.Y * pic.Height
        Dim X2 As Integer = curObj.midPt.pt.X * pic.Width : Dim Y2 As Integer = curObj.midPt.pt.Y * pic.Height
        Dim angle = curObj.angle
        Dim clockwise = curObj.clockwise
        Dim theta = CalcAngleBetweenTwoLines(X1, Y1, X2, Y2, X2 + 10, Y2)
        Dim downFlag = CheckAngleDirection(X2 + 10, Y2, X2, Y2, X1, Y1)
        If Not downFlag Then theta = 360 - theta
        If Not clockwise Then angle = -1 * angle

        g.DrawArc(MainForm.drawPen, New Rectangle(X2 - 25, Y2 - 25, 50, 50), CSng(theta), CSng(angle))
    End Sub
    Public Sub DrawAngleObj(g As Graphics, pic As PictureBox, ByRef curObj As angleObj, mPtF As PointF, objID As Integer)
        If curObj.ptCnt < 1 Then Return
        If curObj.ptCnt = 1 Then DrawPoint(g, pic, curObj.lineObj_1.midPts(1).pt) : Return

        If curObj.ptCnt = 2 Then
            Dim X1 As Integer = curObj.lineObj_1.midPts(1).pt.X * pic.Width : Dim Y1 As Integer = curObj.lineObj_1.midPts(1).pt.Y * pic.Height
            Dim X2 As Integer = curObj.midPt.pt.X * pic.Width : Dim Y2 As Integer = curObj.midPt.pt.Y * pic.Height
            curObj.lineObj_2.midPts(1).pt.X = mPtF.X : curObj.lineObj_2.midPts(1).pt.Y = mPtF.Y
            Dim X3 As Integer = curObj.lineObj_2.midPts(1).pt.X * pic.Width : Dim Y3 As Integer = curObj.lineObj_2.midPts(1).pt.Y * pic.Height
            curObj.angle = CalcAngleBetweenTwoLines(X1, Y1, X2, Y2, X3, Y3)
            DrawLine(g, pic, X2, Y2, X1, Y1)
            DrawLine(g, pic, X2, Y2, X3, Y3)
            DrawPoint(g, pic, curObj.lineObj_1.midPts(1).pt)
        End If
        If curObj.ptCnt >= 2 Then
            DrawAngle(g, pic, curObj)
            DrawPoint(g, pic, curObj.midPt.pt)
            DrawString(g, pic, curObj.midPt.pt, curObj.angle.ToString())
        End If
        If curObj.ptCnt >= 3 Then
            If MainForm.selectedLineID = 1 Then
                DrawLineObj(g, pic, curObj.lineObj_1, mPtF, objID, True)
            Else
                DrawLineObj(g, pic, curObj.lineObj_1, mPtF, objID)
            End If
            If MainForm.selectedLineID = 2 Then
                DrawLineObj(g, pic, curObj.lineObj_2, mPtF, objID, True)
            Else
                DrawLineObj(g, pic, curObj.lineObj_2, mPtF, objID)
            End If
        End If
    End Sub

    Public Sub DrawAngleFixedObj(g As Graphics, pic As PictureBox, ByRef curObj As angleObj, mPtF As PointF, objId As Integer)
        If curObj.ptCnt < 1 Then Return
        DrawPoint(g, pic, curObj.lineObj_1.midPts(1).pt)
        If curObj.ptCnt >= 2 Then
            DrawAngleFixed(g, pic, curObj)
            If MainForm.selectedLineID = 1 Then
                DrawLineObj(g, pic, curObj.lineObj_1, mPtF, objId, True)
            Else
                DrawLineObj(g, pic, curObj.lineObj_1, mPtF, objId)
            End If
            If MainForm.selectedLineID = 2 Then
                DrawLineObj(g, pic, curObj.lineObj_2, mPtF, objId, True)
            Else
                DrawLineObj(g, pic, curObj.lineObj_2, mPtF, objId)
            End If
            DrawString(g, pic, curObj.midPt.pt, curObj.angle.ToString())
        End If
    End Sub
    Public Sub DrawPointObj(g As Graphics, pic As PictureBox, curObj As pointObj, Optional objID As Integer = 9999)
        If MainForm.selectState And objID = MainForm.selectedObj Or (objID = MainForm.selectedObjSet(0) Or objID = MainForm.selectedObjSet(1)) Then
            DrawPoint(g, pic, curObj.pt, True)
        Else
            DrawPoint(g, pic, curObj.pt)
        End If
    End Sub

    Public Sub DrawAnnoObj(g As Graphics, pic As PictureBox, ByRef curObj As AnnoObject, mPtF As PointF)
        If curObj.ptCnt < 1 Then Return
        If curObj.ptCnt = 1 Then
            curObj.EndPt.pt.X = mPtF.X : curObj.EndPt.pt.Y = mPtF.Y
            curObj.size = g.MeasureString(curObj.annotation, MainForm.drawFont)
        End If
        Dim textsize As RectangleF = New RectangleF()
        textsize.X = curObj.EndPt.pt.X * MainForm.pic_main.Width
        textsize.Y = curObj.EndPt.pt.Y * MainForm.pic_main.Height
        textsize.Width = curObj.size.Width
        textsize.Height = curObj.size.Height
        g.DrawString(curObj.annotation, MainForm.drawFont, MainForm.drawBrush, textsize)

        Dim StPt As New Point(curObj.Stpt.pt.X * MainForm.pic_main.Width, curObj.Stpt.pt.Y * MainForm.pic_main.Height)
        g.DrawLine(MainForm.drawPen, StPt, New Point(textsize.X, textsize.Y))
    End Sub

    Public Sub DrawCircle_3Obj(g As Graphics, pic As PictureBox, ByRef curObj As circleObj, mPtF As PointF, id As Integer, type As Integer)
        If curObj.ptCnt < 1 Then Return
        For i = 0 To curObj.ptCnt - 1
            DrawPoint(g, pic, curObj.midPts(i).pt)
        Next
        Dim tempObj As New circleObj
        InitializeCircleObj(tempObj)
        CloneCircleObj(curObj, tempObj)
        If curObj.ptCnt = 2 Then
            If GetRadiusAndCenter(tempObj, mPtF) Then
                If ArrangePtsOfCircle(tempObj) Then
                    tempObj.drawFlag(0) = False
                    tempObj.drawFlag(1) = True
                End If
                ReSetCircle_3Obj(tempObj, mPtF, type)
            Else
                Dim A = New Point(tempObj.midPts(0).pt.X * MainForm.pic_main.Width, tempObj.midPts(0).pt.Y * MainForm.pic_main.Height)
                Dim B = New Point(tempObj.midPts(1).pt.X * MainForm.pic_main.Width, tempObj.midPts(1).pt.Y * MainForm.pic_main.Height)
                Dim C = New Point(mPtF.X * MainForm.pic_main.Width, mPtF.Y * MainForm.pic_main.Height)
                DrawLine(g, pic, A.X, A.Y, B.X, B.Y)
                DrawLine(g, pic, B.X, B.Y, C.X, C.Y)
                Return
            End If
        End If
        If tempObj.ptCnt >= 2 Then
            DrawArc(g, pic, tempObj, id)
        End If
    End Sub
    Public Sub DrawObj(g As Graphics, pic As PictureBox, ByRef curObj As measureObj, curMType As Integer, mPtF As PointF)
        Select Case curMType
            Case 1
                DrawPointObj(g, pic, curObj.ptObj, curObj.objID)
            Case 3
                DrawLineObj(g, pic, curObj.lineObj, mPtF, curObj.objID, True)
            Case 5
                If curObj.ptCnt >= 0 Then DrawCircleWithCenterObj(g, pic, curObj.circleObj, mPtF, curObj.objID)
            Case 30
                DrawAngleObj(g, pic, curObj.angleObj, mPtF, curObj.objID)
            Case 31
                DrawAngleFixedObj(g, pic, curObj.angleObj, mPtF, curObj.objID)
            Case 50
                DrawFitLineObj(g, pic, curObj.fitLineObj, curObj.objID)
            Case 61
                DrawCircleCenterRadius(g, pic, curObj.circleObj, mPtF, curObj.objID)
            Case 62, 63
                DrawFitCircleObj(g, pic, curObj.fitCirObj, curObj.objID)
            Case 64, 65
                DrawCircle_3Obj(g, pic, curObj.circleObj, mPtF, curObj.objID, curMType)
            Case 106
                DrawAnnoObj(g, pic, curObj.annoObj, mPtF)
        End Select
    End Sub

    Public Sub DrawObjList(g As Graphics, pic As PictureBox, objList As List(Of measureObj))
        Dim tempPt As PointF = New PointF(0, 0)
        For Each obj In objList
            DrawObj(g, pic, obj, obj.mType, tempPt)
        Next
    End Sub
    Public Sub DrawObjList(pic As PictureBox, objList As List(Of measureObj))
        pic.Refresh()
        Dim g As Graphics = pic.CreateGraphics()
        DrawObjList(g, pic, objList)
        g.Dispose()
    End Sub

    Public Sub LoadPosDataToGridView(obj As measureObj)
        MainForm.dgv_pos.Rows.Clear()
        Dim str_item = New String(2) {}
        Dim W = MainForm.pic_main.Width : Dim H = MainForm.pic_main.Height
        For i = 0 To obj.ptCnt - 1
            str_item(0) = (i + 1).ToString()
            If obj.mType = MeasureType.lineFit Then
                str_item(1) = CInt(obj.fitLineObj.refPts(i).pt.X * W)
                str_item(2) = CInt(obj.fitLineObj.refPts(i).pt.Y * H)
            ElseIf obj.mType = MeasureType.circleFit Or obj.mType = MeasureType.arcFit Then
                str_item(1) = CInt(obj.fitCirObj.refPts(i).pt.X * W)
                str_item(2) = CInt(obj.fitCirObj.refPts(i).pt.Y * H)
            End If

            MainForm.dgv_pos.Rows.Add(str_item)
        Next
    End Sub

    Public Sub LoadPosDataToGridView(mPt As Point)
        Dim str_item = New String(2) {}
        Dim cnt = MainForm.dgv_pos.RowCount
        str_item(0) = cnt.ToString()
        str_item(1) = mPt.X.ToString()
        str_item(2) = mPt.Y.ToString()

        MainForm.dgv_pos.Rows.Add(str_item)
    End Sub

    Public Sub DrawRectangle(graph As Graphics, ByVal pictureBox As PictureBox, ByVal FirstPtOfEdge As Point, ByVal SecondPtOfEdge As Point)
        graph.DrawRectangle(MainForm.drawPen, New Rectangle(FirstPtOfEdge.X, FirstPtOfEdge.Y, SecondPtOfEdge.X - FirstPtOfEdge.X, SecondPtOfEdge.Y - FirstPtOfEdge.Y))
    End Sub
    Public Sub DrawRectangle(ByVal pictureBox As PictureBox, ByVal FirstPtOfEdge As Point, ByVal SecondPtOfEdge As Point)
        Dim graph As Graphics = pictureBox.CreateGraphics()
        DrawRectangle(graph, pictureBox, FirstPtOfEdge, SecondPtOfEdge)
        graph.Dispose()
    End Sub
    Public Function CheckIfLineInPos(obj As lineObj, mPt As Point) As Boolean
        For i = 0 To obj.ptCnt - 2
            Dim startPt As New Point(obj.midPts(i).pt.X * MainForm.pic_main.Width, obj.midPts(i).pt.Y * MainForm.pic_main.Height)
            Dim endPt As New Point(obj.midPts(i + 1).pt.X * MainForm.pic_main.Width, obj.midPts(i + 1).pt.Y * MainForm.pic_main.Height)
            Dim dist1 = Find_BPointLineDistance(startPt.X, startPt.Y, endPt.X, endPt.Y, mPt.X, mPt.Y)
            Dim dist2 = Find_TwoPointDistance(startPt.X, startPt.Y, mPt.X, mPt.Y)
            Dim dist3 = Find_TwoPointDistance(endPt.X, endPt.Y, mPt.X, mPt.Y)
            If dist1 < MainForm.threshold Then
                If dist2 < MainForm.threshold Then MainForm.selectedSegment = i : MainForm.selectedPt = i : Return True
                If dist3 < MainForm.threshold Then MainForm.selectedSegment = i : MainForm.selectedPt = i + 1 : Return True
                If MainForm.OutPointFlag = False Then MainForm.selectedSegment = i : MainForm.selectedPt = MainForm.Infinite : Return True
            End If
        Next
        Return False
    End Function

    Public Function CheckIfCircleInPos(obj As circleObj, mPt As Point) As Boolean
        Dim centerPt = New Point(obj.centerPt.pt.X * MainForm.pic_main.Width, obj.centerPt.pt.Y * MainForm.pic_main.Height)
        Dim m_angle = CalcAngleFromYaxis(mPt.X, mPt.Y, centerPt.X, centerPt.Y)
        Dim angleList As New List(Of Double)
        Dim midPts(10) As Point
        Dim dist1, dist2, dist3 As Double
        Dim radius = obj.radius * MainForm.zoomFactor
        For i = 0 To obj.ptCnt - 1
            midPts(i).X = obj.midPts(i).pt.X * MainForm.pic_main.Width : midPts(i).Y = obj.midPts(i).pt.Y * MainForm.pic_main.Height
            Dim angle = CalcAngleFromYaxis(midPts(i).X, midPts(i).Y, centerPt.X, centerPt.Y)
            angleList.Add(angle)
        Next
        For i = 0 To obj.ptCnt - 2
            If m_angle > angleList(i) And m_angle < angleList(i + 1) Then
                dist1 = Find_TwoPointDistance(mPt.X, mPt.Y, centerPt.X, centerPt.Y)
                dist2 = Find_TwoPointDistance(midPts(i).X, midPts(i).Y, mPt.X, mPt.Y)
                dist3 = Find_TwoPointDistance(midPts(i + 1).X, midPts(i + 1).Y, mPt.X, mPt.Y)
                If Math.Abs(dist1 - radius) < MainForm.threshold Then
                    MainForm.selectedSegment = i
                    If dist2 < MainForm.threshold Then MainForm.selectedPt = i
                    If dist3 < MainForm.threshold Then MainForm.selectedPt = i + 1
                    Return True
                End If
            End If
        Next
        dist1 = Find_TwoPointDistance(mPt.X, mPt.Y, centerPt.X, centerPt.Y)
        If obj.ptCnt = 0 Then
            If Math.Abs(dist1 - radius) < MainForm.threshold Then
                MainForm.selectedSegment = 0
                Return True
            End If
            Return False
        Else
            dist2 = Find_TwoPointDistance(midPts(0).X, midPts(0).Y, mPt.X, mPt.Y)
            dist3 = Find_TwoPointDistance(midPts(obj.ptCnt - 1).X, midPts(obj.ptCnt - 1).Y, mPt.X, mPt.Y)
            If Math.Abs(dist1 - radius) < MainForm.threshold Then
                MainForm.selectedSegment = obj.ptCnt - 1
                If dist2 < MainForm.threshold Then MainForm.selectedPt = 0
                If dist3 < MainForm.threshold Then MainForm.selectedPt = obj.ptCnt - 1
                Return True
            End If
            Return False
        End If

    End Function

    Public Function CheckIfAngleInPos(obj As angleObj, mPt As Point) As Boolean
        Dim found = CheckIfLineInPos(obj.lineObj_1, mPt)
        If found Then MainForm.selectedLineID = 1 : Return True
        found = CheckIfLineInPos(obj.lineObj_2, mPt)
        If found Then MainForm.selectedLineID = 2 : Return True
        Return False
    End Function

    Public Function CheckIfPtInPos(obj As pointObj, mPt As Point) As Boolean
        Dim X As Integer = obj.pt.X * MainForm.pic_main.Width : Dim Y As Integer = obj.pt.Y * MainForm.pic_main.Height
        Dim dist = Find_TwoPointDistance(X, Y, mPt.X, mPt.Y)
        If dist < 10 Then Return True
        Return False
    End Function

    Public Function CheckIfAnnoInPos(obj As AnnoObject, mPt As Point) As Boolean
        Dim X1 = obj.EndPt.pt.X * MainForm.pic_main.Width : Dim Y1 = obj.EndPt.pt.Y * MainForm.pic_main.Height
        Dim X2 = X1 + obj.size.Width : Dim Y2 = Y1 + obj.size.Height
        If mPt.X > X1 And mPt.X < X2 And mPt.Y > Y1 And mPt.Y < Y2 Then
            Return True
        End If
        Return False
    End Function
    Public Function GetSelectedIDs(objList As List(Of measureObj), mPt As Point) As Boolean
        Dim found As Boolean
        For Each obj In objList
            Select Case obj.mType
                Case 1
                    found = CheckIfPtInPos(obj.ptObj, mPt)
                    If found Then MainForm.selectedObj = obj.objID
                Case 3, 50
                    found = CheckIfLineInPos(obj.lineObj, mPt)
                    If found Then MainForm.selectedObj = obj.objID
                Case 5, 61, 64, 65
                    found = CheckIfCircleInPos(obj.circleObj, mPt)
                    If found Then MainForm.selectedObj = obj.objID
                Case 62, 63
                    found = CheckIfCircleInPos(obj.fitCirObj.circleObj, mPt)
                    If found Then MainForm.selectedObj = obj.objID
                Case 30, 31
                    found = CheckIfAngleInPos(obj.angleObj, mPt)
                    If found Then MainForm.selectedObj = obj.objID
                Case 106
                    found = CheckIfAnnoInPos(obj.annoObj, mPt)
                    If found Then MainForm.selectedObj = obj.objID
            End Select
            If found Then Return True
        Next
        Return False
    End Function

    Public Sub EraseLineObj(ByRef obj As lineObj, id As Integer)
        If id <= 10 Then obj.drawFlag(id) = False
    End Sub

    Public Sub EraseCircleObj(ByRef obj As circleObj, id As Integer)
        If id <= 10 Then obj.drawFlag(id) = False
    End Sub

    Public Sub EraseAngleObj(ByRef obj As angleObj, lineId As Integer, segId As Integer)
        If lineId = 1 Then
            EraseLineObj(obj.lineObj_1, segId)
        Else
            EraseLineObj(obj.lineObj_2, segId)
        End If
    End Sub
    Public Sub EraseSelectedObj(objList As List(Of measureObj))
        If MainForm.selectedObj = MainForm.Infinite Then Return
        Dim Obj = objList(MainForm.selectedObj)
        Select Case Obj.mType
            Case 3, 50  'have to set logic for fit line
                EraseLineObj(Obj.lineObj, MainForm.selectedSegment)
            Case 5, 61
                EraseCircleObj(Obj.circleObj, MainForm.selectedSegment)
            Case 62, 63
                EraseCircleObj(Obj.fitCirObj.circleObj, MainForm.selectedSegment)
            Case 30, 31
                EraseAngleObj(Obj.angleObj, MainForm.selectedLineID, MainForm.selectedSegment)
        End Select
        objList(MainForm.selectedObj) = Obj
    End Sub

    Public Sub ExpandLineObj(ByRef obj As lineObj, id As Integer)
        If id <= 10 Then obj.drawFlag(id) = True
    End Sub

    Public Sub ExpandCircleObj(ByRef obj As circleObj, id As Integer)
        If id <= 10 Then obj.drawFlag(id) = True
    End Sub

    Public Sub ExpandAngleObj(ByRef obj As angleObj, lineId As Integer, segId As Integer)
        If lineId = 1 Then
            ExpandLineObj(obj.lineObj_1, segId)
        Else
            ExpandLineObj(obj.lineObj_2, segId)
        End If
    End Sub
    Public Sub ExpandSelectedObj(objList As List(Of measureObj))
        If MainForm.selectedObj = MainForm.Infinite Then Return
        Dim Obj = objList(MainForm.selectedObj)
        Select Case Obj.mType
            Case 3, 50  'have to set logic for fit line
                ExpandLineObj(Obj.lineObj, MainForm.selectedSegment)
            Case 5, 61
                ExpandCircleObj(Obj.circleObj, MainForm.selectedSegment)
            Case 62, 63
                ExpandCircleObj(Obj.fitCirObj.circleObj, MainForm.selectedSegment)
            Case 30, 31
                ExpandAngleObj(Obj.angleObj, MainForm.selectedLineID, MainForm.selectedSegment)
        End Select
        objList(MainForm.selectedObj) = Obj
    End Sub

    Public Function ZoomImage(ByVal zoomFactor As Double, ByVal ori As Mat) As Mat
        Dim ori_img = ori.Clone()
        Dim s As Size = New Size(Convert.ToInt32(ori.Width * zoomFactor), Convert.ToInt32(ori.Height * zoomFactor))
        Dim dst_img As Mat = New Mat()
        CvInvoke.Resize(ori_img, dst_img, s)
        ori_img.Dispose()
        Return dst_img
    End Function

    Public Sub CalcDistBetweenPtandLine(obj1 As pointObj, obj2 As lineObj, ByRef curObj As measureObj, distType As Integer)
        CalcMinBetweenPointAndLine(obj1, obj2, curObj)
    End Sub

    Public Sub CalcDistBetweenPtandCircle(obj1 As pointObj, obj2 As circleObj, ByRef curObj As measureObj, distType As Integer)
        CalcBetweenPointAndCircle(obj1, obj2, curObj, distType)
    End Sub

    Public Sub CalcDistBetweenLineAndLine(obj1 As lineObj, obj2 As lineObj, ByRef curObj As measureObj, distType As Integer)
        Select Case distType
            Case DistanceType.min
                CalcMinBetweenLineAndLine(obj1, obj2, curObj)
            Case DistanceType.max
                CalcMaxBetweenLineAndLine(obj1, obj2, curObj)
        End Select
    End Sub

    Public Sub CalcDistBetweenLineAndCircle(obj1 As lineObj, obj2 As circleObj, ByRef curObj As measureObj, distType As Integer)
        Dim width = MainForm.pic_main.Width : Dim height = MainForm.pic_main.Height
        Dim X1 = obj1.midPts(0).pt.X * width : Dim Y1 = obj1.midPts(0).pt.Y * height
        Dim X2 = obj1.midPts(obj1.ptCnt - 1).pt.X * width : Dim Y2 = obj1.midPts(obj1.ptCnt - 1).pt.Y * height
        Dim Xp = obj2.centerPt.pt.X * width : Dim Yp = obj2.centerPt.pt.Y * height
        Find_BPointLineDistance(X1, Y1, X2, Y2, Xp, Yp)
        Dim ptObj = New pointObj()
        ptObj.pt = New PointF(MainForm.XsLinePoint / CDbl(width), MainForm.YsLinePoint / CDbl(height))

        CalcBetweenPointAndCircle(ptObj, obj2, curObj, distType)
    End Sub
    Public Function CalcDistBetweenSelectedObjs(objList As List(Of measureObj), ByRef curObj As measureObj, distType As Integer, objIDs() As Integer) As Boolean
        If objIDs(0) = MainForm.Infinite Or objIDs(1) = MainForm.Infinite Then Return False
        Dim obj1 = objList(objIDs(0)) : Dim obj2 = objList(objIDs(1))
        Select Case obj1.mType
            Case 1  'point
                Select Case obj2.mType
                    Case 1
                    Case 3
                        CalcDistBetweenPtandLine(obj1.ptObj, obj2.lineObj, curObj, distType)
                    Case 5, 61
                        CalcDistBetweenPtandCircle(obj1.ptObj, obj2.circleObj, curObj, distType)
                    Case 62
                        CalcDistBetweenPtandCircle(obj1.ptObj, obj2.fitCirObj.circleObj, curObj, distType)
                End Select
            Case 3  'line
                Select Case obj2.mType
                    Case 1
                        CalcDistBetweenPtandLine(obj2.ptObj, obj1.lineObj, curObj, distType)
                    Case 3
                        CalcDistBetweenLineAndLine(obj1.lineObj, obj2.lineObj, curObj, distType)
                    Case 5, 61
                        CalcDistBetweenLineAndCircle(obj1.lineObj, obj2.circleObj, curObj, distType)
                    Case 62
                        CalcDistBetweenLineAndCircle(obj1.lineObj, obj2.fitCirObj.circleObj, curObj, distType)
                End Select
            Case 5, 61  'circle
                Select Case obj2.mType
                    Case 1
                        CalcDistBetweenPtandCircle(obj2.ptObj, obj1.circleObj, curObj, distType)
                    Case 3
                        CalcDistBetweenLineAndCircle(obj2.lineObj, obj1.circleObj, curObj, distType)
                    Case 5, 61
                    Case 62

                End Select
            Case 62     'fit circle
                Select Case obj2.mType
                    Case 1
                        CalcDistBetweenPtandCircle(obj2.ptObj, obj1.fitCirObj.circleObj, curObj, distType)
                    Case 3
                        CalcDistBetweenLineAndCircle(obj2.lineObj, obj1.fitCirObj.circleObj, curObj, distType)
                    Case 5, 61
                    Case 62

                End Select

        End Select
        Return True
    End Function

    Function CheckAngleBetweenTwoLines(objList As List(Of measureObj), ByRef curObj As measureObj, distType As Integer, objIDs As Integer()) As Boolean
        If objIDs(0) = MainForm.Infinite Or objIDs(1) = MainForm.Infinite Then Return False
        Dim obj1 = objList(objIDs(0)) : Dim obj2 = objList(objIDs(1))
        If obj1.mType <> MeasureType.line Or obj2.mType <> MeasureType.line Then Return False

        Dim width = MainForm.pic_main.Width : Dim height = MainForm.pic_main.Height
        Dim datingPt = GetDatingPtLineAndLine(obj1.lineObj.m, obj1.lineObj.b * MainForm.zoomFactor, obj2.lineObj.m, obj2.lineObj.b * MainForm.zoomFactor)
        Dim X1 = obj1.lineObj.midPts(0).pt.X * width : Dim Y1 = obj1.lineObj.midPts(0).pt.Y * height
        Dim X3 = obj2.lineObj.midPts(0).pt.X * width : Dim Y3 = obj2.lineObj.midPts(0).pt.Y * height
        Dim angle = CalcAngleBetweenTwoLines(X1, Y1, datingPt.X, datingPt.Y, X3, Y3)

        curObj.mType = MeasureType.angle
        curObj.ptCnt = 3 : curObj.angleObj.angle = angle : curObj.angleObj.ptCnt = 3
        curObj.angleObj.midPt.pt = New PointF(datingPt.X / width, datingPt.Y / height)
        curObj.angleObj.lineObj_1.midPts(0).pt = New PointF(datingPt.X / width, datingPt.Y / height)
        curObj.angleObj.lineObj_2.midPts(0).pt = New PointF(datingPt.X / width, datingPt.Y / height)

        ClonePointObj(obj1.lineObj.midPts(0), curObj.angleObj.lineObj_1.midPts(1))
        ClonePointObj(obj2.lineObj.midPts(0), curObj.angleObj.lineObj_2.midPts(1))
        curObj.angleObj.lineObj_1.ptCnt = 2 : curObj.angleObj.lineObj_2.ptCnt = 2
        curObj.angleObj.lineObj_1.drawFlag(0) = True : curObj.angleObj.lineObj_2.drawFlag(0) = True

        Select Case distType
            Case DistanceType.min
                If angle < 90 Then
                    ArrangePtsOfLine(curObj.angleObj.lineObj_1) : ArrangePtsOfLine(curObj.angleObj.lineObj_2)
                    Dim angle1 = CalcAngleFromYaxis(X1, Y1, datingPt.X, datingPt.Y)
                    Dim angle2 = CalcAngleFromYaxis(X3, Y3, datingPt.X, datingPt.Y)
                    curObj.angleObj.startAngle = Math.Min(angle1, angle2) : curObj.angleObj.sweepAngle = angle
                    Return True
                Else
                    If curObj.angleObj.midPt.pt.X > obj1.lineObj.midPts(0).pt.X And curObj.angleObj.midPt.pt.X < obj1.lineObj.midPts(obj1.ptCnt - 1).pt.X Then
                        ClonePointObj(obj1.lineObj.midPts(obj1.ptCnt - 1), curObj.angleObj.lineObj_1.midPts(1))
                    ElseIf curObj.angleObj.midPt.pt.X > obj2.lineObj.midPts(0).pt.X And curObj.angleObj.midPt.pt.X < obj2.lineObj.midPts(obj2.ptCnt - 1).pt.X Then
                        ClonePointObj(obj2.lineObj.midPts(obj2.ptCnt - 1), curObj.angleObj.lineObj_2.midPts(1))
                    Else
                        Dim offSet As SizeF = New SizeF(curObj.angleObj.midPt.pt.X - obj2.lineObj.midPts(0).pt.X, curObj.angleObj.midPt.pt.Y - obj2.lineObj.midPts(0).pt.Y)
                        curObj.angleObj.lineObj_2.midPts(1).pt.X = curObj.angleObj.midPt.pt.X + offSet.Width
                        curObj.angleObj.lineObj_2.midPts(1).pt.Y = curObj.angleObj.midPt.pt.Y + offSet.Height
                    End If
                    ArrangePtsOfLine(curObj.angleObj.lineObj_1) : ArrangePtsOfLine(curObj.angleObj.lineObj_2)
                    curObj.angleObj.angle = 180 - angle
                    X1 = curObj.angleObj.lineObj_1.midPts(1).pt.X * width : Y1 = curObj.angleObj.lineObj_1.midPts(1).pt.Y * height
                    X3 = curObj.angleObj.lineObj_2.midPts(1).pt.X * width : Y3 = curObj.angleObj.lineObj_2.midPts(1).pt.Y * height
                    Dim angle1 = CalcAngleFromYaxis(X1, Y1, datingPt.X, datingPt.Y)
                    Dim angle2 = CalcAngleFromYaxis(X3, Y3, datingPt.X, datingPt.Y)
                    curObj.angleObj.startAngle = Math.Min(angle1, angle2) : curObj.angleObj.sweepAngle = 180 - angle
                    Return True
                End If
            Case DistanceType.max
                If angle < 90 Then
                    If curObj.angleObj.midPt.pt.X > obj1.lineObj.midPts(0).pt.X And curObj.angleObj.midPt.pt.X < obj1.lineObj.midPts(obj1.ptCnt - 1).pt.X Then
                        ClonePointObj(obj1.lineObj.midPts(obj1.ptCnt - 1), curObj.angleObj.lineObj_1.midPts(1))
                    ElseIf curObj.angleObj.midPt.pt.X > obj2.lineObj.midPts(0).pt.X And curObj.angleObj.midPt.pt.X < obj2.lineObj.midPts(obj2.ptCnt - 1).pt.X Then
                        ClonePointObj(obj2.lineObj.midPts(obj2.ptCnt - 1), curObj.angleObj.lineObj_2.midPts(1))
                    Else
                        Dim offSet As SizeF = New SizeF(curObj.angleObj.midPt.pt.X - obj2.lineObj.midPts(0).pt.X, curObj.angleObj.midPt.pt.Y - obj2.lineObj.midPts(0).pt.Y)
                        curObj.angleObj.lineObj_2.midPts(1).pt.X = curObj.angleObj.midPt.pt.X + offSet.Width
                        curObj.angleObj.lineObj_2.midPts(1).pt.Y = curObj.angleObj.midPt.pt.Y + offSet.Height
                    End If
                    ArrangePtsOfLine(curObj.angleObj.lineObj_1) : ArrangePtsOfLine(curObj.angleObj.lineObj_2)
                    curObj.angleObj.angle = 180 - angle
                    X1 = curObj.angleObj.lineObj_1.midPts(1).pt.X * width : Y1 = curObj.angleObj.lineObj_1.midPts(1).pt.Y * height
                    X3 = curObj.angleObj.lineObj_2.midPts(1).pt.X * width : Y3 = curObj.angleObj.lineObj_2.midPts(1).pt.Y * height
                    Dim angle1 = CalcAngleFromYaxis(X1, Y1, datingPt.X, datingPt.Y)
                    Dim angle2 = CalcAngleFromYaxis(X3, Y3, datingPt.X, datingPt.Y)
                    curObj.angleObj.startAngle = Math.Min(angle1, angle2) : curObj.angleObj.sweepAngle = 180 - angle
                    Return True
                Else
                    ArrangePtsOfLine(curObj.angleObj.lineObj_1) : ArrangePtsOfLine(curObj.angleObj.lineObj_2)
                    Dim angle1 = CalcAngleFromYaxis(X1, Y1, datingPt.X, datingPt.Y)
                    Dim angle2 = CalcAngleFromYaxis(X3, Y3, datingPt.X, datingPt.Y)
                    curObj.angleObj.startAngle = Math.Min(angle1, angle2) : curObj.angleObj.sweepAngle = angle
                    Return True
                End If
        End Select
        Return False
    End Function

    Public Sub EnableTextBox(ByVal textbox As TextBox, ByVal obj As AnnoObject)
        textbox.Text = obj.annotation
        Dim pos_image As Point = New Point(obj.EndPt.pt.X * MainForm.pic_main.Width, obj.EndPt.pt.Y * MainForm.pic_main.Height)

        textbox.Location = pos_image
        Dim rt_size = obj.size
        textbox.Visible = True
        textbox.Size = New Size(rt_size.Width, rt_size.Height)
    End Sub

    Public Sub DisableTextBox(txtBox As TextBox, ByRef objList As List(Of measureObj), annoNum As Integer)
        txtBox.Visible = False
        Dim obj = objList(annoNum)
        obj.annoObj.annotation = txtBox.Text
        obj.annoObj.size = txtBox.Size
        objList(annoNum) = obj
    End Sub

    Public Sub MovePointObj(ByRef obj As pointObj, dx As Double, dy As Double)
        obj.pt.X += dx : obj.pt.Y += dy
    End Sub

    Public Sub MoveLineObj(ByRef obj As lineObj, dx As Double, dy As Double)
        For i = 0 To obj.ptCnt - 1
            obj.midPts(i).pt.X += dx
            obj.midPts(i).pt.Y += dy
        Next
    End Sub

    Public Sub MovePtOfLineObj(ByRef obj As lineObj, dx As Double, dy As Double)
        If MainForm.selectedPt < obj.ptCnt Then
            obj.midPts(MainForm.selectedPt).pt.X += dx
            obj.midPts(MainForm.selectedPt).pt.Y += dy
        End If
    End Sub

    Public Sub MoveAngleObj(ByRef obj As angleObj, dx As Double, dy As Double)
        MoveLineObj(obj.lineObj_1, dx, dy)
        MoveLineObj(obj.lineObj_2, dx, dy)
        obj.midPt.pt.X += dx : obj.midPt.pt.Y += dy
    End Sub

    Public Sub MovePtOfAngleObj(ByRef obj As angleObj, dx As Double, dy As Double)
        If MainForm.selectedLineID = 1 Then
            If obj.lineObj_1.midPts(MainForm.selectedPt).pt = obj.midPt.pt Then
                For i = 0 To obj.lineObj_2.ptCnt - 1
                    If obj.lineObj_2.midPts(i).pt = obj.lineObj_1.midPts(MainForm.selectedPt).pt Then
                        obj.lineObj_2.midPts(i).pt.X += dx
                        obj.lineObj_2.midPts(i).pt.Y += dy
                    End If
                Next
                obj.midPt.pt.X += dx : obj.midPt.pt.Y += dy
            End If
            MovePtOfLineObj(obj.lineObj_1, dx, dy)
        ElseIf MainForm.selectedLineID = 2 Then
            If obj.lineObj_2.midPts(MainForm.selectedPt).pt = obj.midPt.pt Then
                For i = 0 To obj.lineObj_1.ptCnt - 1
                    If obj.lineObj_1.midPts(i).pt = obj.lineObj_2.midPts(MainForm.selectedPt).pt Then
                        obj.lineObj_1.midPts(i).pt.X += dx
                        obj.lineObj_1.midPts(i).pt.Y += dy
                    End If
                Next
                obj.midPt.pt.X += dx : obj.midPt.pt.Y += dy
            End If
            MovePtOfLineObj(obj.lineObj_2, dx, dy)
        End If
        Dim X2 = obj.midPt.pt.X * MainForm.pic_main.Width : Dim Y2 = obj.midPt.pt.Y * MainForm.pic_main.Height
        Dim X1, Y1, X3, Y3 As Integer
        For i = 0 To obj.lineObj_1.ptCnt - 1
            If obj.lineObj_1.midPts(i).pt <> obj.midPt.pt Then
                X1 = obj.lineObj_1.midPts(i).pt.X * MainForm.pic_main.Width
                Y1 = obj.lineObj_1.midPts(i).pt.Y * MainForm.pic_main.Height
                Exit For
            End If
        Next
        For i = 0 To obj.lineObj_2.ptCnt - 1
            If obj.lineObj_2.midPts(i).pt <> obj.midPt.pt Then
                X3 = obj.lineObj_2.midPts(i).pt.X * MainForm.pic_main.Width
                Y3 = obj.lineObj_2.midPts(i).pt.Y * MainForm.pic_main.Height
                Exit For
            End If
        Next
        obj.angle = CalcAngleBetweenTwoLines(X1, Y1, X2, Y2, X3, Y3)
    End Sub
    Public Sub MoveCircleObj(ByRef obj As circleObj, dx As Double, dy As Double)
        obj.centerPt.pt.X += dx : obj.centerPt.pt.Y += dy
        For i = 0 To obj.ptCnt - 1
            obj.midPts(i).pt.X += dx
            obj.midPts(i).pt.Y += dy
        Next
    End Sub

    Public Sub MovePtOfCircleObj(ByRef obj As circleObj, dx As Double, dy As Double, mType As Integer)
        If MainForm.selectedPt < obj.ptCnt Then
            obj.midPts(MainForm.selectedPt).pt.X += dx
            obj.midPts(MainForm.selectedPt).pt.Y += dy
        End If
        GetRadiusAndCenter(obj, mType)
    End Sub
    Public Sub MoveFitCircleObj(ByRef obj As fitCircleObj, dx As Double, dy As Double)
        For i = 0 To obj.ptCnt - 1
            obj.refPts(i).pt.X += dx
            obj.refPts(i).pt.Y += dy
        Next
        MoveCircleObj(obj.circleObj, dx, dy)
    End Sub

    Public Sub MoveAnnotation(ByRef obj As AnnoObject, dx As Double, dy As Double)
        'obj.Stpt.pt.X += dx : obj.Stpt.pt.Y += dy
        obj.EndPt.pt.X += dx : obj.EndPt.pt.Y += dy
    End Sub
    Public Sub MoveSelectedObj(objList As List(Of measureObj), dx As Integer, dy As Integer)
        Dim flag = True
        If MainForm.selectedObj = MainForm.Infinite Then Return
        Dim fdx = dx / MainForm.pic_main.Width : Dim fdy = dy / MainForm.pic_main.Height
        Dim obj = objList(MainForm.selectedObj)
        If MainForm.selectedPt <> MainForm.Infinite Then
            flag = False
        End If
        Select Case obj.mType
            Case MeasureType.point
                MovePointObj(obj.ptObj, fdx, fdy)
            Case MeasureType.line
                If flag Then
                    MoveLineObj(obj.lineObj, fdx, fdy)
                Else
                    MovePtOfLineObj(obj.lineObj, fdx, fdy)
                End If
            Case MeasureType.angle ', MeasureType.angleFixed
                If flag Then
                    MoveAngleObj(obj.angleObj, fdx, fdy)
                Else
                    MovePtOfAngleObj(obj.angleObj, fdx, fdy)
                End If
            Case MeasureType.circleWithCenter, MeasureType.arc_3, MeasureType.circle_3 ', MeasureType.circleCenterRadius
                If flag Then
                    MoveCircleObj(obj.circleObj, fdx, fdy)
                Else
                    MovePtOfCircleObj(obj.circleObj, fdx, fdy, obj.mType)
                End If
            Case MeasureType.arcFit, MeasureType.circleFit
                If flag Then
                    MoveFitCircleObj(obj.fitCirObj, fdx, fdy)
                Else
                    MovePtOfCircleObj(obj.fitCirObj.circleObj, fdx, fdy, obj.mType)
                End If
            Case MeasureType.annotation
                MoveAnnotation(obj.annoObj, fdx, fdy)
        End Select
        objList(MainForm.selectedObj) = obj
    End Sub

    Public Sub ReSetMovedObj(objList As List(Of measureObj))
        If MainForm.selectedObj = MainForm.Infinite Then Return
        Dim obj = objList(MainForm.selectedObj)
        Select Case obj.mType
            Case MeasureType.line
                ArrangePtsOfLine(obj.lineObj)
                GetConstsOfLine(obj.lineObj)
            Case MeasureType.angle ', MeasureType.angleFixed
                ArrangePtsOfLine(obj.angleObj.lineObj_1)
                GetConstsOfLine(obj.angleObj.lineObj_1)
                ArrangePtsOfLine(obj.angleObj.lineObj_2)
                GetConstsOfLine(obj.angleObj.lineObj_2)
            Case MeasureType.circleWithCenter, MeasureType.arc_3, MeasureType.circle_3 ', MeasureType.circleCenterRadius
                ArrangePtsOfCircle(obj.circleObj)
            Case MeasureType.arcFit, MeasureType.circleFit
                ArrangePtsOfCircle(obj.fitCirObj.circleObj)
        End Select
        objList(MainForm.selectedObj) = obj
    End Sub

    Public Sub DrawCrossHair(pic As PictureBox)
        Dim g As Graphics = pic.CreateGraphics()
        Dim W = pic.Width : Dim H = pic.Height
        Dim X1 As Integer = W / 2 : Dim X2 = X1
        g.DrawLine(MainForm.drawPen, X1, 0, X2, H)
        Dim Y1 As Integer = H / 2 : Dim Y2 = Y1
        g.DrawLine(MainForm.drawPen, 0, Y1, W, Y2)
        g.Dispose()
    End Sub

    Public Sub DrawIncPos(pic As PictureBox)
        Dim g As Graphics = pic.CreateGraphics()
        g.DrawEllipse(MainForm.redPen, New Rectangle(MainForm.PosIncX - 2, MainForm.PosIncY - 2, 4, 4))
        g.Dispose()
    End Sub
    Public Sub GetName(ByRef obj As measureObj)
        Select Case obj.mType
            Case MeasureType.point
                obj.name = "Pt" & obj.objID
            Case MeasureType.line
                obj.name = "L" & obj.objID
            Case MeasureType.lineFit
                obj.name = "FL" & obj.objID
            Case MeasureType.circleCenterRadius, MeasureType.circleWithCenter, MeasureType.circle_3
                obj.name = "C" & obj.objID
            Case MeasureType.arc_3
                obj.name = "Ar" & obj.objID
            Case MeasureType.circleFit
                obj.name = "FC" & obj.objID
            Case MeasureType.arcFit
                obj.name = "FAr" & obj.objID
            Case MeasureType.angle, MeasureType.angleFixed
                obj.name = "AG" & obj.objID
            Case MeasureType.annotation
                obj.name = "A" & obj.objID
        End Select
    End Sub
    Public Function SetComboItemContent(name As String, ByVal name_list As List(Of String)) As DataGridViewComboBoxCell
        Dim cell As DataGridViewComboBoxCell = New DataGridViewComboBoxCell()
        Dim str_array As String() = New String(name_list.Count - 1) {}
        Dim cnt As Integer = 0
        If name <> "" Then
            str_array(cnt) = name
            cnt += 1
        End If

        For i = 0 To str_array.Length() - 1
            If name <> name_list(i) Then
                str_array(cnt) = name_list(i)
                cnt += 1
            End If
        Next
        cell.Items.AddRange(str_array)
        cell.Value = cell.Items(0)
        cell.ReadOnly = False
        Return cell
    End Function
    Public Sub LoadObjectList(ByVal listView As DataGridView, ByVal object_list As List(Of measureObj), ByVal CF As Double, ByVal digit As Integer, ByVal unit As String, ByVal name_list As List(Of String))
        listView.Rows.Clear()
        If object_list.Count > 0 Then
            Dim i = 0
            Dim length As Double

            For Each item In object_list
                Dim str_item = New String(6) {}
                str_item(0) = (i + 1).ToString()
                str_item(1) = item.name
                str_item(2) = ""
                str_item(3) = item.parameter
                str_item(4) = item.spec
                str_item(5) = ""
                str_item(6) = item.judgement

                Select Case item.mType
                    Case MeasureType.point

                    Case MeasureType.line
                        Dim X1 = item.lineObj.midPts(0).pt.X * MainForm.pic_main.Width : Dim Y1 = item.lineObj.midPts(0).pt.Y * MainForm.pic_main.Height
                        Dim X2 = item.lineObj.midPts(item.lineObj.ptCnt - 1).pt.X * MainForm.pic_main.Width : Dim Y2 = item.lineObj.midPts(item.lineObj.ptCnt - 1).pt.Y * MainForm.pic_main.Height
                        length = CalcDistBetweenPoints(X1, Y1, X2, Y2)
                        length = GetDecimalNumber(length, digit, CF)
                        str_item(5) = length.ToString() & " " & unit

                    Case MeasureType.circleCenterRadius, MeasureType.circleWithCenter, MeasureType.circle_3, MeasureType.arc_3
                        length = item.circleObj.radius
                        length = GetDecimalNumber(length, digit, CF)
                        str_item(5) = length.ToString() & " " & unit

                    Case MeasureType.arcFit, MeasureType.circleFit
                        length = item.fitCirObj.circleObj.radius
                        length = GetDecimalNumber(length, digit, CF)
                        str_item(5) = length.ToString() & " " & unit

                    Case MeasureType.angle, MeasureType.angleFixed
                        str_item(5) = item.angleObj.angle.ToString() & " degree"

                End Select

                listView.Rows.Add(str_item)
                listView.Rows(i).Cells(2) = SetComboItemContent(item.description, name_list)
                i += 1
            Next
        End If

    End Sub
End Module
