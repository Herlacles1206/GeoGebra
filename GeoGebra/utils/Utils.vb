Public Module Utils
    Function CloneFitLineObj(src As fitLineObj) As fitLineObj
        Dim temp As New fitLineObj()
        temp.m = src.m : temp.b = src.b
        temp.completed = src.completed
        For i = 0 To 20
            temp.ptPos(i) = src.ptPos(i)
        Next
        Return temp
    End Function

    Sub CloneFitLineObj(src As fitLineObj, ByRef dst As measureObj)
        dst.fitLineObj = src
    End Sub

    Function CloneFitCircleObj(src As fitCircleObj) As fitCircleObj
        Dim temp As New fitCircleObj()
        temp.completed = src.completed
        temp.circle = src.circle
        For i = 0 To 20
            temp.ptPos(i) = src.ptPos(i)
        Next
        Return temp
    End Function

    Sub CloneFitCircleObj(src As fitCircleObj, ByRef dst As measureObj)
        dst.fitCirObj = src
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
End Module
