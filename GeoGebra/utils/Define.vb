
'define variables 

Public Enum MeasureType
    initState = -1
    'Basic Tools
    move = 0
    point = 1
    segment = 2
    line = 3
    polygen = 4
    circleWithCenter = 5
    'Edit
    selectObjects = 10
    showHideLabel = 11
    showHideObject = 12
    delete = 13
    moveGraphicsView = 14
    copyVisualStyle = 15
    'Construct
    midpoint = 20
    perpendicularLine = 21
    perpendicularBiseCtor = 22
    parallelLine = 23
    angleBiseCtor = 24
    tangents = 25
    locus = 26
    'Measure
    angle = 30
    angleFixed = 31
    distance = 32
    area = 33
    slider = 34
    slope = 35
    'Point
    intersect = 40
    pointOnObject = 41
    attachPoint = 42
    extremum = 43
    roots = 44
    'Lines
    lineFit = 50

    'Circles
    circleCenterRadius = 61
    circleFit = 62
    arcFit = 63
    'Polygens

    'Conics

    'Transform

    'Media

    'Others


End Enum


Public Structure pointObj
    Public pt As PointF
    Public name As String

    Public Sub Refresh()
        pt.X = 0.0
        pt.Y = 0.0
        name = ""
    End Sub
End Structure
Public Structure lineObj
    Public stPt As pointObj
    Public edPt As pointObj
    Public midPt As pointObj
    Public angle As Integer
    Public hasEnd As Boolean

    Public Sub Refresh()
        stPt.Refresh() : midPt.Refresh() : edPt.Refresh()
        angle = 0
        hasEnd = False
    End Sub
End Structure

Public Structure angleObj
    Public stPt As pointObj
    Public edPt As pointObj
    Public midPt As pointObj
    Public angle As Integer
    Public clockwise As Boolean
    Public fixed As Boolean
    Public startAngle As Double
    Public sweepAngle As Double

    Public Sub Refresh()
        stPt.Refresh() : midPt.Refresh() : edPt.Refresh()
        angle = 0
        clockwise = False : fixed = False
        startAngle = 0 : sweepAngle = 0
    End Sub
End Structure

Public Structure circleObj
    Public centerPt As pointObj
    Public circlePt1 As pointObj
    Public circlePt2 As pointObj
    Public circlePt3 As pointObj
    Public extraPt As pointObj
    Public radius As Single
    Public startAngle As Integer
    Public sweepAngle As Integer
    Public filled As Boolean

    Public Sub Refresh()
        centerPt.Refresh() : circlePt1.Refresh() : circlePt2.Refresh() : circlePt3.Refresh() : extraPt.Refresh()
        radius = 0 : startAngle = 0 : sweepAngle = 0
        filled = False
    End Sub
End Structure

Public Class fitLineObj
    Public ptPos(20) As pointObj
    Public m As Double
    Public b As Double
    Public completed As Boolean
    Public Sub Refresh()
        completed = False
        For i = 0 To 20
            ptPos(i).Refresh()
        Next
        m = 0.0 : b = 0.0
    End Sub
End Class

Public Class fitCircleObj
    Public ptPos(20) As pointObj
    Public circle As circleObj
    Public completed As Boolean
    Public Sub Refresh()
        completed = False
        For i = 0 To 20
            ptPos(i).Refresh()
        Next
        circle.Refresh()
    End Sub
End Class

Public Structure measureObj
    Public mType As Integer
    Public objID As Integer
    Public ptLimit As Integer
    Public ptCnt As Integer

    Public ptObj As pointObj
    Public lineObj As lineObj
    Public angleObj As angleObj
    Public circleObj As circleObj
    Public fitLineObj As fitLineObj
    Public fitCirObj As fitCircleObj

    Public Sub Refresh()
        mType = MeasureType.initState
        objID = 0
        ptCnt = 0
        ptLimit = 0

        ptObj.Refresh()
        lineObj.Refresh()
        angleObj.Refresh()
        circleObj.Refresh()
        fitLineObj.Refresh()
        fitCirObj.Refresh()
    End Sub
End Structure