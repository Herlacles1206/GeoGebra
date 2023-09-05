Imports Emgu.CV.OCR

Public Module Utils

    Sub InitializeLineObj(ByRef curObj As lineObj)
        ReDim curObj.midPts(10)
        ReDim curObj.drawFlag(11)
    End Sub
    Sub InitializeMeasureObj(ByRef curObj As measureObj)
        'Intialize lineObj
        ReDim curObj.lineObj.midPts(10)
        ReDim curObj.lineObj.drawFlag(11)
        'Initialzie circleObj
        ReDim curObj.circleObj.midPts(10)
        ReDim curObj.circleObj.drawFlag(11)
        'Intialize fitlineObj
        ReDim curObj.fitLineObj.lineObj.midPts(10)
        ReDim curObj.fitLineObj.lineObj.drawFlag(11)
        ReDim curObj.fitLineObj.refPts(20)
        'Initialzie fitcircleObj
        ReDim curObj.fitCirObj.circleObj.midPts(10)
        ReDim curObj.fitCirObj.circleObj.drawFlag(11)
        ReDim curObj.fitCirObj.refPts(20)
        'Initialize angleObj
        ReDim curObj.angleObj.lineObj_1.midPts(10)
        ReDim curObj.angleObj.lineObj_1.drawFlag(11)
        ReDim curObj.angleObj.lineObj_2.midPts(10)
        ReDim curObj.angleObj.lineObj_2.drawFlag(11)
    End Sub

    Sub ClonePointObj(src As pointObj, ByRef dst As pointObj)
        dst.pt.X = src.pt.X : dst.pt.Y = src.pt.Y
        dst.name = src.name
    End Sub

    Sub CloneLineObj(src As lineObj, ByRef dst As lineObj)
        dst.b = src.b : dst.m = src.m : dst.ptCnt = src.ptCnt : dst.dist = src.dist
        For i = 0 To 10
            ClonePointObj(src.midPts(i), dst.midPts(i))
        Next
        For i = 0 To 11
            dst.drawFlag(i) = src.drawFlag(i)
        Next
    End Sub

    Sub CloneCircleObj(src As circleObj, ByRef dst As circleObj)
        dst.radius = src.radius : dst.ptCnt = src.ptCnt : dst.reversed = src.reversed
        ClonePointObj(src.centerPt, dst.centerPt)
        For i = 0 To 10
            ClonePointObj(src.midPts(i), dst.midPts(i))
        Next
        For i = 0 To 11
            dst.drawFlag(i) = src.drawFlag(i)
        Next
    End Sub

    Sub CloneAngleObj(src As angleObj, ByRef dst As angleObj)
        dst.angle = src.angle : dst.startAngle = src.startAngle : dst.sweepAngle = src.sweepAngle : dst.clockwise = src.clockwise : dst.fixed = src.fixed : dst.ptCnt = src.ptCnt
        ClonePointObj(src.midPt, dst.midPt)
        CloneLineObj(src.lineObj_1, dst.lineObj_1)
        CloneLineObj(src.lineObj_2, dst.lineObj_2)
    End Sub

    Sub CloneFitLineObj(src As fitLineObj, ByRef dst As fitLineObj)
        dst.completed = src.completed : dst.ptCnt = src.ptCnt
        For i = 0 To 20
            ClonePointObj(src.refPts(i), dst.refPts(i))
        Next
        CloneLineObj(src.lineObj, dst.lineObj)
    End Sub

    Sub CloneFitCircleObj(src As fitCircleObj, ByRef dst As fitCircleObj)
        dst.completed = src.completed : dst.ptCnt = src.ptCnt
        For i = 0 To 20
            ClonePointObj(src.refPts(i), dst.refPts(i))
        Next
        CloneCircleObj(src.circleObj, dst.circleObj)
    End Sub

    Sub CloneAnnoObj(src As AnnoObject, ByRef dst As AnnoObject)
        ClonePointObj(src.EndPt, dst.EndPt)
        ClonePointObj(src.Stpt, dst.Stpt)
        dst.annotation = src.annotation
        dst.size.Width = src.size.Width
        dst.size.Height = src.size.Height
        dst.ptCnt = src.ptCnt
    End Sub
    Sub CloneMeasureObj(src As measureObj, ByRef dst As measureObj)
        dst.mType = src.mType : dst.objID = src.objID : dst.ptCnt = src.ptCnt : dst.ptLimit = src.ptLimit
        ClonePointObj(src.ptObj, dst.ptObj)
        CloneLineObj(src.lineObj, dst.lineObj)
        CloneAngleObj(src.angleObj, dst.angleObj)
        CloneCircleObj(src.circleObj, dst.circleObj)
        CloneFitLineObj(src.fitLineObj, dst.fitLineObj)
        CloneFitCircleObj(src.fitCirObj, dst.fitCirObj)
        CloneAnnoObj(src.annoObj, dst.annoObj)
    End Sub
    Function CheckIfTargetAngleInInternal(angles As List(Of Double), st As Double, ed As Double) As Integer
        Dim sum As Integer = 0
        For Each angle In angles
            If angle > st And angle < ed Then
                sum += 1
            End If
        Next
        Return sum
    End Function
    Sub GetOptPtsForArc(angles As List(Of Double), ByRef st As Integer, ByRef ed As Integer, ByRef sweep As Integer)
        Dim tempSt, tempEd, angle, cnt As Integer
        Dim minAngle As Integer = 99999
        Dim flag As Boolean = False
        For i = 0 To angles.Count - 1
            For j = 0 To angles.Count - 1
                If i = j Then Continue For
                tempSt = i : tempEd = j : flag = False : cnt = 0
                If angles(i) > angles(j) Then
                    cnt = CheckIfTargetAngleInInternal(angles, angles(i), 360)
                    cnt += CheckIfTargetAngleInInternal(angles, 0, angles(j))
                Else
                    cnt = CheckIfTargetAngleInInternal(angles, angles(i), angles(j))
                End If
                If cnt < angles.Count - 2 Then flag = True
                If flag = False Then
                    angle = (angles(j) - angles(i) + 360) Mod 360
                    If angle < minAngle Then
                        minAngle = angle : st = i : ed = j
                    End If
                End If
            Next
        Next
        sweep = minAngle
    End Sub

    Function ReSetLineObj(ByRef Obj As lineObj, pt As PointF) As Integer
        If Obj.ptCnt >= 10 Then Return 0
        For i = 0 To Obj.ptCnt - 2
            If pt.X > Obj.midPts(i).pt.X And pt.X < Obj.midPts(i + 1).pt.X Then
                For j = 9 To i + 1 Step -1
                    ClonePointObj(Obj.midPts(j), Obj.midPts(j + 1))
                    Obj.drawFlag(j) = Obj.drawFlag(j - 1)
                Next
                Obj.midPts(i + 1).pt.X = pt.X : Obj.midPts(i + 1).pt.Y = pt.Y : Obj.ptCnt += 1
                Return 1
            End If
        Next
        Return 0
    End Function

    Sub ArrangePtsOfLine(ByRef Obj As lineObj)
        For i = 0 To Obj.ptCnt - 2
            For j = i + 1 To Obj.ptCnt - 1
                If Obj.midPts(i).pt.X > Obj.midPts(j).pt.X Then
                    Dim temp As New PointF(Obj.midPts(i).pt.X, Obj.midPts(i).pt.Y)
                    Obj.midPts(i).pt.X = Obj.midPts(j).pt.X : Obj.midPts(i).pt.Y = Obj.midPts(j).pt.Y
                    Obj.midPts(j).pt.X = temp.X : Obj.midPts(j).pt.Y = temp.Y
                End If
            Next
        Next
    End Sub

    Function ReSetCircleObj(ByRef Obj As circleObj, pt As PointF, mType As MeasureType) As Integer
        If Obj.ptCnt >= 10 Then Return 0
        Dim angleList As New List(Of Double)
        Dim X = Obj.centerPt.pt.X * MainForm.pic_main.Width : Dim Y = Obj.centerPt.pt.Y * MainForm.pic_main.Height

        angleList.Add(0)
        For i = 0 To Obj.ptCnt - 1
            Dim X2 = Obj.midPts(i).pt.X * MainForm.pic_main.Width : Dim Y2 = Obj.midPts(i).pt.Y * MainForm.pic_main.Height
            Dim theta = CalcAngleFromYaxis(X2, Y2, X, Y)
            angleList.Add(theta)
        Next
        Dim X3 = pt.X * MainForm.pic_main.Width : Dim Y3 = pt.Y * MainForm.pic_main.Height
        Dim theta3 = CalcAngleFromYaxis(X3, Y3, X, Y)
        If Obj.ptCnt >= 2 Then
            'If Not Obj.reversed Then
            For i = 0 To Obj.ptCnt - 1
                If theta3 > angleList(i) And theta3 < angleList(i + 1) Then
                    Dim temp = Obj.drawFlag(Obj.ptCnt - 1)
                    For j = 9 To i Step -1
                        ClonePointObj(Obj.midPts(j), Obj.midPts(j + 1))
                        If j - 1 >= 0 Then
                            Obj.drawFlag(j) = Obj.drawFlag(j - 1)
                        Else
                            Obj.drawFlag(j) = temp
                        End If

                    Next
                    Obj.midPts(i).pt.X = pt.X : Obj.midPts(i).pt.Y = pt.Y : Obj.ptCnt += 1
                    Return 1
                End If
            Next
            If theta3 > angleList(Obj.ptCnt) Then
                Obj.midPts(Obj.ptCnt).pt.X = pt.X : Obj.midPts(Obj.ptCnt).pt.Y = pt.Y : Obj.drawFlag(Obj.ptCnt) = Obj.drawFlag(Obj.ptCnt - 1) : Obj.ptCnt += 1
                Return 1
            End If
            'Else
            '    For i = 0 To Obj.ptCnt - 2
            '        If theta3 > angleList(i) And theta3 < angleList(i + 1) Then
            '            For j = 9 To i Step -1
            '                ClonePointObj(Obj.midPts(j), Obj.midPts(j + 1))
            '                Obj.drawFlag(j + 1) = Obj.drawFlag(j)
            '            Next
            '            Obj.midPts(i).pt.X = pt.X : Obj.midPts(i).pt.Y = pt.Y : Obj.ptCnt += 1
            '            Return 1
            '        End If
            '    Next
            '    If theta3 > angleList(Obj.ptCnt - 1) And theta3 < angleList(0) Then
            '        Obj.midPts(Obj.ptCnt).pt.X = pt.X : Obj.midPts(Obj.ptCnt).pt.Y = pt.Y : Obj.drawFlag(Obj.ptCnt) = Obj.drawFlag(Obj.ptCnt - 1) : Obj.ptCnt += 1
            '        Return 1
            '    End If
            'End If

        ElseIf Obj.ptCnt = 1 Then
            Obj.midPts(Obj.ptCnt).pt.X = pt.X : Obj.midPts(Obj.ptCnt).pt.Y = pt.Y : Obj.drawFlag(Obj.ptCnt) = Obj.drawFlag(Obj.ptCnt - 1) : Obj.ptCnt += 1
            ArrangePtsOfCircle(Obj)
            Return 1
        Else
            Obj.midPts(Obj.ptCnt).pt.X = pt.X : Obj.midPts(Obj.ptCnt).pt.Y = pt.Y : Obj.drawFlag(Obj.ptCnt) = True : Obj.ptCnt += 1 : Return 1
        End If

        Return 0
    End Function

    Function ArrangePtsOfCircle(ByRef Obj As circleObj) As Boolean
        Dim angleList As New List(Of Double)
        If Obj.ptCnt < 2 Then Return False
        Dim X = Obj.centerPt.pt.X * MainForm.pic_main.Width : Dim Y = Obj.centerPt.pt.Y * MainForm.pic_main.Height
        For i = 0 To Obj.ptCnt - 1
            Dim X2 = Obj.midPts(i).pt.X * MainForm.pic_main.Width : Dim Y2 = Obj.midPts(i).pt.Y * MainForm.pic_main.Height
            Dim theta = CalcAngleFromYaxis(X2, Y2, X, Y)
            angleList.Add(theta)
        Next
        For i = 0 To Obj.ptCnt - 2
            For j = i + 1 To Obj.ptCnt - 1
                If angleList(i) > angleList(j) Then
                    Dim temp As New PointF(Obj.midPts(i).pt.X, Obj.midPts(i).pt.Y)
                    Obj.midPts(i).pt.X = Obj.midPts(j).pt.X : Obj.midPts(i).pt.Y = Obj.midPts(j).pt.Y
                    Obj.midPts(j).pt.X = temp.X : Obj.midPts(j).pt.Y = temp.Y
                    Obj.reversed = True
                    Return True
                End If
            Next
        Next
        Return False
    End Function

    Sub GetConstsOfLine(ByRef curObj As lineObj)
        Dim ptList As List(Of PointF) = New List(Of PointF)
        For i = 0 To curObj.ptCnt - 1
            Dim X = curObj.midPts(i).pt.X * MainForm.pic_main.Width
            Dim Y = curObj.midPts(i).pt.Y * MainForm.pic_main.Height
            ptList.Add(New PointF(X, Y))
        Next
        FindLinearLeastSquaresFit(ptList, curObj.m, curObj.b)
    End Sub

    Public Sub CalcMinBetweenPointAndLine(ByVal Obj1 As pointObj, ByVal Obj2 As lineObj, ByRef curObj As measureObj)
        Dim width = MainForm.pic_main.Width : Dim height = MainForm.pic_main.Height
        Dim dLinePoint As Integer
        Dim FirstPointofLine = New Point(Obj2.midPts(0).pt.X * width, Obj2.midPts(0).pt.Y * height)
        Dim SecndPointOfLine = New Point(Obj2.midPts(1).pt.X * width, Obj2.midPts(1).pt.Y * height)
        Dim PointPoint = New Point(Obj1.pt.X * width, Obj1.pt.Y * height)
        dLinePoint = Find_BPointLineDistance(FirstPointofLine.X, FirstPointofLine.Y, SecndPointOfLine.X, SecndPointOfLine.Y, PointPoint.X, PointPoint.Y)

        ClonePointObj(Obj1, curObj.lineObj.midPts(0))
        curObj.lineObj.midPts(1).pt = New PointF(MainForm.XsLinePoint / CSng(width), MainForm.YsLinePoint / CSng(height))
        curObj.lineObj.drawFlag(0) = True
        curObj.lineObj.dist = dLinePoint
        curObj.ptCnt = 2 : curObj.lineObj.ptCnt = 2
        ArrangePtsOfLine(curObj.lineObj)
        GetConstsOfLine(curObj.lineObj)
    End Sub

    Public Sub CalcBetweenPointAndCircle(obj1 As pointObj, obj2 As circleObj, ByRef curObj As measureObj, distType As Integer)
        Dim width = MainForm.pic_main.Width : Dim height = MainForm.pic_main.Height
        Dim pointPoint = New Point(obj1.pt.X * width, obj1.pt.Y * height)
        Dim centerPoint = New Point(obj2.centerPt.pt.X * width, obj2.centerPt.pt.Y * height)
        Dim radius = obj2.radius * MainForm.zoomFactor

        ClonePointObj(obj1, curObj.lineObj.midPts(0))
        curObj.lineObj.midPts(1).pt.X = obj2.centerPt.pt.X : curObj.lineObj.midPts(1).pt.Y = obj2.centerPt.pt.Y
        curObj.ptCnt = 2 : curObj.lineObj.ptCnt = 2 : curObj.lineObj.drawFlag(0) = True
        GetConstsOfLine(curObj.lineObj)

        If distType = DistanceType.center Then
            curObj.lineObj.dist = Find_TwoPointDistance(pointPoint.X, pointPoint.Y, centerPoint.X, centerPoint.Y) / MainForm.zoomFactor
            ArrangePtsOfLine(curObj.lineObj)
            GetConstsOfLine(curObj.lineObj)
            Return
        End If

        Dim m = curObj.lineObj.m : Dim c = curObj.lineObj.b * MainForm.zoomFactor
        Dim a = centerPoint.X : Dim b = centerPoint.Y : Dim r = radius
        Dim datingPtList = GetDatingPtLineAndCircle(m, c, a, b, r)

        Dim minDist As Integer = MainForm.Infinite : Dim maxDist As Integer = 0
        Dim MinCirclePt, MaxCirclePt As New Point()
        For Each datingPt In datingPtList
            Dim dist = Find_TwoPointDistance(pointPoint.X, pointPoint.Y, datingPt.X, datingPt.Y)
            If dist < minDist Then minDist = dist : MinCirclePt.X = datingPt.X : MinCirclePt.Y = datingPt.Y
            If dist > maxDist Then maxDist = dist : MaxCirclePt.X = datingPt.X : MaxCirclePt.Y = datingPt.Y
        Next

        If distType = DistanceType.min Then
            curObj.lineObj.dist = minDist / MainForm.zoomFactor
            curObj.lineObj.midPts(1).pt.X = MinCirclePt.X / width : curObj.lineObj.midPts(1).pt.Y = MinCirclePt.Y / height
            ArrangePtsOfLine(curObj.lineObj)
            GetConstsOfLine(curObj.lineObj)
            Return
        Else
            curObj.lineObj.dist = maxDist / MainForm.zoomFactor
            curObj.lineObj.midPts(1).pt.X = MaxCirclePt.X / width : curObj.lineObj.midPts(1).pt.Y = MaxCirclePt.Y / height
            ArrangePtsOfLine(curObj.lineObj)
            GetConstsOfLine(curObj.lineObj)
            Return
        End If
    End Sub

    Sub CalcMinBetweenLineAndLine(obj1 As lineObj, obj2 As lineObj, ByRef curObj As measureObj)
        Dim width = MainForm.pic_main.Width : Dim height = MainForm.pic_main.Height
        Dim X1_1 = obj1.midPts(0).pt.X * width : Dim Y1_1 = obj1.midPts(0).pt.Y * height
        Dim X1_2 = obj1.midPts(obj1.ptCnt - 1).pt.X * width : Dim Y1_2 = obj1.midPts(obj1.ptCnt - 1).pt.Y * height

        Dim X2_1 = obj2.midPts(0).pt.X * width : Dim Y2_1 = obj2.midPts(0).pt.Y * height
        Dim X2_2 = obj2.midPts(obj2.ptCnt - 1).pt.X * width : Dim Y2_2 = obj2.midPts(obj2.ptCnt - 1).pt.Y * height

        Dim minDist As Integer = MainForm.Infinite
        Dim Xp_1, Yp_1, Xp_2, Yp_2 As Integer

        Dim dist = Find_BPointLineDistance(X2_1, Y2_1, X2_2, Y2_2, X1_1, Y1_1)
        If dist < minDist Then minDist = dist : Xp_1 = X1_1 : Yp_1 = Y1_1 : Xp_2 = MainForm.XsLinePoint : Yp_2 = MainForm.YsLinePoint

        dist = Find_BPointLineDistance(X2_1, Y2_1, X2_2, Y2_2, X1_2, Y1_2)
        If dist < minDist Then minDist = dist : Xp_1 = X1_2 : Yp_1 = Y1_2 : Xp_2 = MainForm.XsLinePoint : Yp_2 = MainForm.YsLinePoint

        dist = Find_BPointLineDistance(X1_1, Y1_1, X1_2, Y1_2, X2_1, Y2_1)
        If dist < minDist Then minDist = dist : Xp_1 = X2_1 : Yp_1 = Y2_1 : Xp_2 = MainForm.XsLinePoint : Yp_2 = MainForm.YsLinePoint

        dist = Find_BPointLineDistance(X1_1, Y1_1, X1_2, Y1_2, X2_2, Y2_2)
        If dist < minDist Then minDist = dist : Xp_1 = X2_2 : Yp_1 = Y2_2 : Xp_2 = MainForm.XsLinePoint : Yp_2 = MainForm.YsLinePoint

        curObj.lineObj.midPts(0).pt = New PointF(Xp_1 / CSng(width), Yp_1 / CSng(height))
        curObj.lineObj.midPts(1).pt = New PointF(Xp_2 / CSng(width), Yp_2 / CSng(height))
        curObj.lineObj.drawFlag(0) = True
        curObj.lineObj.dist = minDist
        curObj.ptCnt = 2 : curObj.lineObj.ptCnt = 2
        ArrangePtsOfLine(curObj.lineObj)
        GetConstsOfLine(curObj.lineObj)
    End Sub

    Sub CalcMaxBetweenLineAndLine(obj1 As lineObj, obj2 As lineObj, ByRef curObj As measureObj)
        Dim width = MainForm.pic_main.Width : Dim height = MainForm.pic_main.Height
        Dim X1_1 = obj1.midPts(0).pt.X * width : Dim Y1_1 = obj1.midPts(0).pt.Y * height
        Dim X1_2 = obj1.midPts(obj1.ptCnt - 1).pt.X * width : Dim Y1_2 = obj1.midPts(obj1.ptCnt - 1).pt.Y * height

        Dim X2_1 = obj2.midPts(0).pt.X * width : Dim Y2_1 = obj2.midPts(0).pt.Y * height
        Dim X2_2 = obj2.midPts(obj2.ptCnt - 1).pt.X * width : Dim Y2_2 = obj2.midPts(obj2.ptCnt - 1).pt.Y * height

        Dim maxDist As Integer = 0
        Dim Xp_1, Yp_1, Xp_2, Yp_2 As Integer

        Dim dist = Find_TwoPointDistance(X1_1, Y1_1, X2_1, Y2_1)
        If dist > maxDist Then maxDist = dist : Xp_1 = X1_1 : Yp_1 = Y1_1 : Xp_2 = X2_1 : Yp_2 = Y2_1

        dist = Find_TwoPointDistance(X1_1, Y1_1, X2_2, Y2_2)
        If dist > maxDist Then maxDist = dist : Xp_1 = X1_1 : Yp_1 = Y1_1 : Xp_2 = X2_2 : Yp_2 = Y2_2

        dist = Find_TwoPointDistance(X1_2, Y1_2, X2_1, Y2_1)
        If dist > maxDist Then maxDist = dist : Xp_1 = X1_2 : Yp_1 = Y1_2 : Xp_2 = X2_1 : Yp_2 = Y2_1

        dist = Find_TwoPointDistance(X1_2, Y1_2, X2_2, Y2_2)
        If dist > maxDist Then maxDist = dist : Xp_1 = X1_2 : Yp_1 = Y1_2 : Xp_2 = X2_2 : Yp_2 = Y2_2

        curObj.lineObj.midPts(0).pt = New PointF(Xp_1 / CSng(width), Yp_1 / CSng(height))
        curObj.lineObj.midPts(1).pt = New PointF(Xp_2 / CSng(width), Yp_2 / CSng(height))
        curObj.lineObj.drawFlag(0) = True
        curObj.lineObj.dist = maxDist
        curObj.ptCnt = 2 : curObj.lineObj.ptCnt = 2
        ArrangePtsOfLine(curObj.lineObj)
        GetConstsOfLine(curObj.lineObj)
    End Sub

End Module
