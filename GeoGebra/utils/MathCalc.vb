Imports System.Net
Imports Emgu.CV.VideoCapture
Imports System.Security.Claims

Public Module MathCalc

    Public Function CalcAngleBetweenTwoLines(X1 As Integer, Y1 As Integer, X2 As Integer, Y2 As Integer, X3 As Integer, Y3 As Integer) As Double
        Dim dx21 As Double = X1 - X2
        Dim dy21 As Double = Y1 - Y2
        Dim dx31 As Double = X3 - X2
        Dim dy31 As Double = Y3 - Y2
        Dim m12 = Math.Sqrt(dx21 * dx21 + dy21 * dy21)
        Dim m13 = Math.Sqrt(dx31 * dx31 + dy31 * dy31)
        Dim val = (dx21 * dx31 + dy21 * dy31) / (m12 * m13)
        If val < -1 Then
            val = -1
        ElseIf val > 1 Then
            val = 1
        End If
        If Double.IsNaN(val) Then
            Return 0
        End If
        Dim theta = Math.Acos(val)
        theta = theta * 360 / Math.PI / 2

        theta = Math.Round(theta, 2)
        Return theta
    End Function

    Public Function CalcDistBetweenPoints(X1 As Double, Y1 As Double, X2 As Double, Y2 As Double) As Double
        Dim size As SizeF = New SizeF(X2 - X1, Y2 - Y1)
        Dim dist As Double = size.Width * size.Width + size.Height * size.Height
        dist = Math.Sqrt(dist)
        Return dist
    End Function

    Public Function CheckAngleDirection(X1 As Integer, Y1 As Integer, X2 As Integer, Y2 As Integer, X3 As Integer, Y3 As Integer) As Boolean
        Dim a = Y2 - Y1
        Dim b = X1 - X2
        Dim c = X2 * Y1 - Y2 * X1

        Dim val = a * X3 + b * Y3 + c
        If val > 0 Then
            Return True
        End If
        Return False
    End Function

    Public Function GetUnitVector(X1 As Integer, Y1 As Integer, X2 As Integer, Y2 As Integer) As SizeF
        Dim offset As SizeF = New SizeF(0, 0)

        Dim sum As Double = (X2 - X1) * (X2 - X1) + (Y2 - Y1) * (Y2 - Y1)
        sum = Math.Sqrt(sum)
        If sum <> 0 Then
            offset.Width = CSng((X2 - X1) / sum)
            offset.Height = CSng((Y2 - Y1) / sum)
        End If

        Return offset
    End Function

    Public Function CalcPositionInCircle(ByVal centerpt As Point, ByVal radius As Integer, ByVal angle As Integer) As Point
        Dim target_point As Point = New Point()
        Dim ang_radian = angle / 360 * Math.PI * 2
        Dim offset_x = Convert.ToInt32(radius * Math.Cos(ang_radian))
        Dim offset_y = Convert.ToInt32(radius * Math.Sin(ang_radian))
        target_point.X = centerpt.X + offset_x
        target_point.Y = centerpt.Y + offset_y
        Return target_point
    End Function

    Public Function ErrorSquared(ByVal points As List(Of PointF), ByVal m As Double, ByVal b As Double) As Double
        Dim total As Double = 0
        For Each pt As PointF In points
            Dim dy As Double = pt.Y - (m * pt.X + b)
            total += dy * dy
        Next pt
        Return total
    End Function
    Public Function FindLinearLeastSquaresFit(ByVal points As List(Of PointF), ByRef m As Double, ByRef b As Double) _
    As Double
        ' Perform the calculation.
        ' Find the values S1, Sx, Sy, Sxx, and Sxy.
        Dim S1 As Double = points.Count
        Dim Sx As Double = 0
        Dim Sy As Double = 0
        Dim Sxx As Double = 0
        Dim Sxy As Double = 0
        For Each pt As PointF In points
            Sx += pt.X
            Sy += pt.Y
            Sxx += pt.X * pt.X
            Sxy += pt.X * pt.Y
        Next pt

        ' Solve for m and b.
        m = (Sxy * S1 - Sx * Sy) / (Sxx * S1 - Sx * Sx)
        b = (Sxy * Sx - Sy * Sxx) / (Sx * Sx - S1 * Sxx)
        b /= MainForm.zoomFactor

        Return Math.Sqrt(ErrorSquared(points, m, b))
    End Function

    Public Function GetCenterPoint(points As List(Of PointF)) As PointF
        Dim X As Double = 0 : Dim Y As Double = 0
        For Each pt In points
            X += pt.X : Y += pt.Y
        Next
        X /= points.Count : Y /= points.Count
        Return New PointF(X, Y)
    End Function

    Public Function GetRadiusFromMultiPt(center As PointF, pts As List(Of PointF)) As Double
        Dim distSum As Double = 0
        Dim radius As Double = 0
        For Each pt In pts
            distSum += CalcDistBetweenPoints(center.X, center.Y, pt.X, pt.Y)
        Next
        If pts.Count > 0 Then radius = distSum / pts.Count

        Return radius
    End Function

    Public Function CalcAngleFromYaxis(X1, Y1, X2, Y2) As Double
        Dim theta = CalcAngleBetweenTwoLines(X1, Y1, X2, Y2, X2 + 10, Y2)
        Dim downFlag = CheckAngleDirection(X2 + 10, Y2, X2, Y2, X1, Y1)
        If Not downFlag Then theta = 360 - theta
        Return theta
    End Function

    Function GetDatingPtLineAndLine(m1, b1, m2, b2) As PointF
        Dim res As New PointF(0, 0)
        If m1 <> m2 Then
            res.X = (b2 - b1) / (m1 - m2)
            res.Y = m1 * res.X + b1
        End If
        Return res
    End Function

    Function GetDatingPtLineAndCircle(m, c, a, b, r) As List(Of PointF)
        Dim ptList As New List(Of PointF)
        Dim aprim = (1 + m ^ 2)
        Dim bprim = 2 * m * (c - b) - 2 * a
        Dim cprim = a ^ 2 + (c - b) ^ 2 - r ^ 2
        Dim delta = bprim ^ 2 - 4 * aprim * cprim

        If delta >= 0 Then

            Dim x1_e_intersection = (-bprim + Math.Sqrt(delta)) / (2 * aprim)
            Dim y1_e_intersection = m * x1_e_intersection + c
            ptList.Add(New PointF(x1_e_intersection, y1_e_intersection))
            If delta > 0 Then
                Dim x2_e_intersection = (-bprim - Math.Sqrt(delta)) / (2 * aprim)
                Dim y2_e_intersection = m * x2_e_intersection + c
                ptList.Add(New PointF(x2_e_intersection, y2_e_intersection))
            End If

        End If
        Return ptList
    End Function

    ' Find the points where the two circles intersect.
    Function GetDatingPtCircleAndCircle(
    ByVal cx0 As Single, ByVal cy0 As Single, ByVal radius0 _
        As Single,
    ByVal cx1 As Single, ByVal cy1 As Single, ByVal radius1 _
        As Single
    ) As List(Of PointF)
        Dim dx, dy As Single
        Dim dist, a, h, cx2, cy2 As Double
        Dim FindCircleCircleIntersections As Integer
        Dim res As New List(Of PointF)
        ' Find the distance between the centers.
        dx = cx0 - cx1
        dy = cy0 - cy1
        dist = Math.Sqrt(dx * dx + dy * dy)

        ' See how many solutions there are.
        If (dist > radius0 + radius1) Then
            ' No solutions, the circles are too far apart.
            FindCircleCircleIntersections = 0
        ElseIf (dist < Math.Abs(radius0 - radius1)) Then
            ' No solutions, one circle contains the other.
            FindCircleCircleIntersections = 0
        ElseIf ((dist = 0) And (radius0 = radius1)) Then
            ' No solutions, the circles coincide.
            FindCircleCircleIntersections = 0
        Else
            ' Find a and h.
            a = (radius0 * radius0 -
            radius1 * radius1 + dist * dist) / (2 * dist)
            h = Math.Sqrt(radius0 * radius0 - a * a)

            ' Find P2.
            cx2 = cx0 + a * (cx1 - cx0) / dist
            cy2 = cy0 + a * (cy1 - cy0) / dist

            ' Get the points P3.
            Dim intersectionx1 = CSng(cx2 + h * (cy1 - cy0) / dist)
            Dim intersectiony1 = CSng(cy2 - h * (cx1 - cx0) / dist)
            Dim intersectionx2 = CSng(cx2 - h * (cy1 - cy0) / dist)
            Dim intersectiony2 = CSng(cy2 + h * (cx1 - cx0) / dist)


            Res.Add(New PointF(intersectionx1, intersectiony1))
            ' See if we have 1 or 2 solutions.
            If (dist = radius0 + radius1) Then
                FindCircleCircleIntersections = 1
            Else
                res.Add(New PointF(intersectionx2, intersectiony2))
                FindCircleCircleIntersections = 2
            End If
        End If
        Return Res
    End Function

    Public Function Find_TwoPointDistance(X1 As Integer, Y1 As Integer, X2 As Integer, Y2 As Integer) As Integer
        Dim distance As Integer
        distance = Math.Round(Math.Sqrt((X1 - X2) * (X1 - X2) + (Y1 - Y2) * (Y1 - Y2)))
        Return distance
    End Function
    Public Function Find_BPointLineDistance(X1 As Integer, Y1 As Integer, X2 As Integer, Y2 As Integer, Xp As Integer, Yp As Integer) As Integer
        Dim a, b, a1, b1, Xs, Ys As Double
        Dim distance As Integer
        MainForm.OutPointFlag = False
        If Y2 <> Y1 Then
            If X2 <> X1 Then
                a = (Y2 - Y1) / (X2 - X1) : b = Y1 - a * X1
                a1 = -1 / a : b1 = Yp - a1 * Xp
                Xs = (b - b1) / (a1 - a) : Ys = a1 * Xs + b1
                distance = Math.Round(Math.Sqrt((Xp - Xs) * (Xp - Xs) + (Yp - Ys) * (Yp - Ys)))
            Else
                Xs = X1 : Ys = Yp
                distance = Math.Abs(Xs - Xp)
            End If

            If Y1 < Y2 And Ys < Y1 Then MainForm.OutPointFlag = True : MainForm.DotX = X1 : MainForm.DotY = Y1
            If Y2 < Y1 And Ys < Y2 Then MainForm.OutPointFlag = True : MainForm.DotX = X2 : MainForm.DotY = Y2
            If Y1 > Y2 And Ys > Y1 Then MainForm.OutPointFlag = True : MainForm.DotX = X1 : MainForm.DotY = Y1
            If Y2 > Y1 And Ys > Y2 Then MainForm.OutPointFlag = True : MainForm.DotX = X2 : MainForm.DotY = Y2
        Else
            If X1 < X2 And Xs < X1 Then MainForm.OutPointFlag = True : MainForm.DotX = X1 : MainForm.DotY = Y1
            If X2 < X1 And Xs < X2 Then MainForm.OutPointFlag = True : MainForm.DotX = X2 : MainForm.DotY = Y2
            If X1 > X2 And Xs > X1 Then MainForm.OutPointFlag = True : MainForm.DotX = X1 : MainForm.DotY = Y1
            If X2 > X1 And Xs > X2 Then MainForm.OutPointFlag = True : MainForm.DotX = X2 : MainForm.DotY = Y2

            Ys = Y1 : Xs = Xp
            distance = Math.Abs(Ys - Yp)
        End If

        MainForm.XsLinePoint = Xs : MainForm.YsLinePoint = Ys
        If MainForm.OutPointFlag = True Then distance = Find_TwoPointDistance(MainForm.DotX, MainForm.DotY, Xp, Yp) : MainForm.XsLinePoint = MainForm.DotX : MainForm.YsLinePoint = MainForm.DotY

        Return distance
    End Function
End Module
