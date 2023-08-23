
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

    'Circles
    circleCenterRadius = 61
    'Polygens

    'Conics

    'Transform

    'Media

    'Others


End Enum


Public Structure pointObj
    Public pt As PointF
    Public name As String
End Structure
Public Structure lineObj
    Public stPt As pointObj
    Public edPt As pointObj
    Public midPt As pointObj
    Public angle As Integer
    Public hasEnd As Boolean
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
End Structure

Public Structure measureObj
    Public mType As Integer
    Public objID As Integer
    Public ptLimit As Integer
    Public ptCnt As Integer

    Public ptObj As pointObj
    Public lineObj As lineObj
    Public angleObj As angleObj
    Public circleObj As circleObj

    Public Sub Refresh()
        mType = MeasureType.initState
        objID = 0
        ptCnt = 0
        ptLimit = 0

    End Sub
End Structure