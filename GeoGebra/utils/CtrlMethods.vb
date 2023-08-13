Imports System.Runtime.InteropServices

Public Module CtrlMethods
    Public Function UpdateLineObj(ByRef curObj As measureObj, mPtF As PointF)
        If curObj.ptCnt = 0 Then
            curObj.lineObj.stPt.pt.X = mPtF.X : curObj.lineObj.stPt.pt.Y = mPtF.Y
            curObj.lineObj.hasEnd = False : curObj.ptCnt += 1
            Return False
        Else
            curObj.lineObj.edPt.pt.X = mPtF.X : curObj.lineObj.edPt.pt.Y = mPtF.Y
            Dim X1 As Integer = curObj.lineObj.stPt.pt.X * 1000 - 10 : Dim Y1 As Integer = curObj.lineObj.stPt.pt.Y * 1000
            Dim X2 As Integer = X1 + 10 : Dim Y2 = Y1
            Dim X3 As Integer = curObj.lineObj.edPt.pt.X * 1000 - 10 : Dim Y3 As Integer = curObj.lineObj.edPt.pt.Y * 1000
            curObj.lineObj.angle = CalcAngleBetweenTwoLines(X1, Y1, X2, Y2, X3, Y3)
            curObj.ptCnt += 1
            Return True
        End If
    End Function

    Public Function UpdateCircleWithCenterObj(ByRef curObj As measureObj, mPtF As PointF)
        If curObj.ptCnt = 0 Then
            curObj.circleObj.centerPt.pt.X = mPtF.X : curObj.circleObj.centerPt.pt.Y = mPtF.Y : curObj.ptCnt += 1
            Return False
        Else
            curObj.circleObj.circlePt1.pt.X = mPtF.X : curObj.circleObj.circlePt1.pt.Y = mPtF.Y
            curObj.circleObj.radius = CalcDistBetweenPoints(curObj.circleObj.centerPt.pt.X, curObj.circleObj.centerPt.pt.Y, curObj.circleObj.circlePt1.pt.X, curObj.circleObj.circlePt1.pt.Y)
            curObj.ptCnt += 1
            Return True
        End If
    End Function

    Public Function UpdateAngleObj(ByRef curObj As measureObj, mPtF As PointF)
        If curObj.ptCnt = 0 Then
            curObj.angleObj.stPt.pt.X = mPtF.X : curObj.angleObj.stPt.pt.Y = mPtF.Y : curObj.ptCnt += 1
            Return False
        ElseIf curObj.ptCnt = 1 Then
            curObj.angleObj.midPt.pt.X = mPtF.X : curObj.angleObj.midPt.pt.Y = mPtF.Y : curObj.ptCnt += 1
            Return False
        Else
            curObj.angleObj.edPt.pt.X = mPtF.X : curObj.angleObj.edPt.pt.Y = mPtF.Y : curObj.ptCnt += 1
            Dim X1 As Integer = curObj.angleObj.stPt.pt.X * 1000 : Dim Y1 As Integer = curObj.angleObj.stPt.pt.Y * 1000
            Dim X2 As Integer = curObj.angleObj.midPt.pt.X * 1000 : Dim Y2 As Integer = curObj.angleObj.midPt.pt.Y * 1000
            Dim X3 As Integer = curObj.angleObj.edPt.pt.X * 1000 : Dim Y3 As Integer = curObj.angleObj.edPt.pt.Y * 1000
            curObj.angleObj.angle = CalcAngleBetweenTwoLines(X1, Y1, X2, Y2, X3, Y3)
            Return True
        End If
    End Function

    Public Function UpdatePointObj(ByRef curObj As measureObj, mPtF As PointF)
        curObj.ptObj.pt.X = mPtF.X : curObj.ptObj.pt.Y = mPtF.Y
        Return True
    End Function
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
        End Select
        Return completed
    End Function

    Public Sub DrawString(pic As PictureBox, ptF As PointF, str As String)
        Dim g As Graphics = pic.CreateGraphics()
        g.DrawString(str, MainForm.drawFont, MainForm.drawBrush, New Point(ptF.X * pic.Width + 10, ptF.Y * pic.Height + 10))
        g.Dispose()
    End Sub
    Public Sub DrawPoint(pic As PictureBox, PtF As PointF)
        Dim g As Graphics = pic.CreateGraphics()
        g.FillEllipse(MainForm.ptBrush, New Rectangle(PtF.X * pic.Width - 5, PtF.Y * pic.Height - 5, 10, 10))
        g.Dispose()
    End Sub

    Public Sub DrawLine(pic As PictureBox, X1 As Integer, Y1 As Integer, X2 As Integer, Y2 As Integer)
        Dim g As Graphics = pic.CreateGraphics()
        g.DrawLine(MainForm.drawPen, X1, Y1, X2, Y2)
        g.Dispose()
    End Sub
    Public Sub DrawLine(pic As PictureBox, pt1 As PointF, pt2 As PointF)
        DrawLine(pic, pt1.X * pic.Width, pt1.Y * pic.Height, pt2.X * pic.Width, pt2.Y * pic.Height)
    End Sub

    Public Sub DrawLongLine(pic As PictureBox, pt1 As PointF, pt2 As PointF)
        Dim g As Graphics = pic.CreateGraphics()
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
        g.Dispose()
    End Sub

    Public Sub DrawLineObj(pic As PictureBox, ByRef curObj As measureObj, mPtF As PointF)
        If curObj.ptCnt < 1 Then Return
        DrawPoint(pic, curObj.lineObj.stPt.pt)
        If curObj.ptCnt = 1 Then
            curObj.lineObj.edPt.pt.X = mPtF.X : curObj.lineObj.edPt.pt.Y = mPtF.Y
        Else
            DrawPoint(pic, curObj.lineObj.edPt.pt)
        End If
        If curObj.lineObj.hasEnd Then
            DrawLine(pic, curObj.lineObj.stPt.pt, curObj.lineObj.edPt.pt)
        Else
            DrawLongLine(pic, curObj.lineObj.stPt.pt, curObj.lineObj.edPt.pt)
        End If
    End Sub

    Public Sub DrawCircle(pic As PictureBox, pt1 As PointF, pt2 As PointF)
        Dim CenterX As Integer = pt1.X * pic.Width : Dim CenterY As Integer = pt1.Y * pic.Height
        Dim OnX As Integer = pt2.X * pic.Width : Dim OnY As Integer = pt2.Y * pic.Height
        Dim radius = Math.Sqrt(Math.Pow(CenterX - OnX, 2) + Math.Pow(CenterY - OnY, 2))
        Dim g As Graphics = pic.CreateGraphics()
        g.DrawEllipse(MainForm.drawPen, New Rectangle(CenterX - radius, CenterY - radius, radius * 2, radius * 2))
        g.Dispose()
    End Sub
    Public Sub DrawCircleWithCenterObj(pic As PictureBox, ByRef curObj As measureObj, mPtF As PointF)
        If curObj.ptCnt < 1 Then Return
        DrawPoint(pic, curObj.circleObj.centerPt.pt)
        If curObj.ptCnt = 1 Then
            curObj.circleObj.circlePt1.pt.X = mPtF.X : curObj.circleObj.circlePt1.pt.Y = mPtF.Y
        Else
            DrawPoint(pic, curObj.circleObj.circlePt1.pt)
        End If
        DrawCircle(pic, curObj.circleObj.centerPt.pt, curObj.circleObj.circlePt1.pt)
    End Sub

    Public Sub DrawAngle(pic As PictureBox, ByRef curObj As measureObj)
        Dim g As Graphics = pic.CreateGraphics()
        Dim X1 As Integer = curObj.angleObj.stPt.pt.X * 1000 : Dim Y1 As Integer = curObj.angleObj.stPt.pt.Y * 1000
        Dim X2 As Integer = curObj.angleObj.midPt.pt.X * 1000 : Dim Y2 As Integer = curObj.angleObj.midPt.pt.Y * 1000
        Dim X3 As Integer = curObj.angleObj.edPt.pt.X * 1000 : Dim Y3 As Integer = curObj.angleObj.edPt.pt.Y * 1000
        Dim angle = CalcAngleBetweenTwoLines(X1, Y1, X2, Y2, X3, Y3)
        Dim clockwise = CheckAngleDirection(X1, Y1, X2, Y2, X3, Y3)
        Dim theta = CalcAngleBetweenTwoLines(X1, Y1, X2, Y2, X2 + 10, Y2)
        Dim downFlag = CheckAngleDirection(X2 + 10, Y2, X2, Y2, X1, Y1)
        If Not downFlag Then theta = 360 - theta
        If Not clockwise Then angle = -1 * angle

        g.DrawArc(MainForm.drawPen, New Rectangle(curObj.angleObj.midPt.pt.X * pic.Width - 25, curObj.angleObj.midPt.pt.Y * pic.Height - 25, 50, 50), CSng(theta), CSng(angle))
        g.Dispose()
    End Sub
    Public Sub DrawAngleObj(pic As PictureBox, ByRef curObj As measureObj, mPtF As PointF)
        If curObj.ptCnt < 1 Then Return
        DrawPoint(pic, curObj.angleObj.stPt.pt)
        If curObj.ptCnt = 2 Then
            curObj.angleObj.edPt.pt.X = mPtF.X : curObj.angleObj.edPt.pt.Y = mPtF.Y
            Dim X1 As Integer = curObj.angleObj.stPt.pt.X * 1000 : Dim Y1 As Integer = curObj.angleObj.stPt.pt.Y * 1000
            Dim X2 As Integer = curObj.angleObj.midPt.pt.X * 1000 : Dim Y2 As Integer = curObj.angleObj.midPt.pt.Y * 1000
            Dim X3 As Integer = curObj.angleObj.edPt.pt.X * 1000 : Dim Y3 As Integer = curObj.angleObj.edPt.pt.Y * 1000
            curObj.angleObj.angle = CalcAngleBetweenTwoLines(X1, Y1, X2, Y2, X3, Y3)
        End If
        If curObj.ptCnt >= 2 Then
            DrawAngle(pic, curObj)
            Dim unitVec1 = GetUnitVector(curObj.angleObj.midPt.pt.X * pic.Width, curObj.angleObj.midPt.pt.Y * pic.Height, curObj.angleObj.stPt.pt.X * pic.Width, curObj.angleObj.stPt.pt.Y * pic.Height)
            Dim unitVec2 = GetUnitVector(curObj.angleObj.midPt.pt.X * pic.Width, curObj.angleObj.midPt.pt.Y * pic.Height, curObj.angleObj.edPt.pt.X * pic.Width, curObj.angleObj.edPt.pt.Y * pic.Height)
            DrawLine(pic, curObj.angleObj.midPt.pt.X * pic.Width, curObj.angleObj.midPt.pt.Y * pic.Height, curObj.angleObj.midPt.pt.X * pic.Width + unitVec1.Width * 25, curObj.angleObj.midPt.pt.Y * pic.Height + unitVec1.Height * 25)
            DrawLine(pic, curObj.angleObj.midPt.pt.X * pic.Width, curObj.angleObj.midPt.pt.Y * pic.Height, curObj.angleObj.midPt.pt.X * pic.Width + unitVec2.Width * 25, curObj.angleObj.midPt.pt.Y * pic.Height + unitVec2.Height * 25)
            DrawPoint(pic, curObj.angleObj.midPt.pt)
            DrawString(pic, curObj.angleObj.midPt.pt, curObj.angleObj.angle.ToString())
        End If
        If curObj.ptCnt >= 3 Then DrawPoint(pic, curObj.angleObj.edPt.pt)
    End Sub

    Public Sub DrawPointObj(pic As PictureBox, curObj As measureObj)
        DrawPoint(pic, curObj.ptObj.pt)
    End Sub
    Public Sub DrawObj(pic As PictureBox, ByRef curObj As measureObj, curMType As Integer, mPtF As PointF)
        Select Case curMType
            Case 1
                DrawPointObj(pic, curObj)
            Case 3
                DrawLineObj(pic, curObj, mPtF)
            Case 5
                DrawCircleWithCenterObj(pic, curObj, mPtF)
            Case 30
                DrawAngleObj(pic, curObj, mPtF)
        End Select
    End Sub

    Public Sub DrawObjList(pic As PictureBox, objList As List(Of measureObj))
        pic.Refresh()
        Dim tempPt As PointF = New PointF(0, 0)
        For Each obj In objList
            DrawObj(pic, obj, obj.mType, tempPt)
        Next
    End Sub
End Module
