Imports System.Net

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
End Module
