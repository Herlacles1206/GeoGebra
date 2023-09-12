
'define variables 

Public Enum DistanceType
    min = 0
    max = 1
    center = 2
End Enum
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
    circle_3 = 64
    arc_3 = 65
    'Polygens

    'Conics

    'Transform


    'Others
    eraseObj = 100
    expandObj = 101
    zoomIn = 102
    zoomOut = 103
    calcdist = 104
    selectObj = 105
    annotation = 106
    detectLine = 107
    detectCircle = 108
    viewMove = 109
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
    Public midPts() As pointObj
    Public drawFlag() As Boolean
    Public ptCnt As Integer
    Public m As Double
    Public b As Double
    Dim dist As Double
    Public Sub Refresh()
        For i = 0 To 10
            midPts(i).Refresh()
        Next
        For i = 0 To 11
            drawFlag(i) = False
        Next
        m = 0 : b = 0 : ptCnt = 0 : dist = 0
    End Sub

    Public Sub GetLength()
        Dim X1 = midPts(0).pt.X * MainForm.realWidth : Dim Y1 = midPts(0).pt.Y * MainForm.realHeight
        Dim X2 = midPts(ptCnt - 1).pt.X * MainForm.realWidth : Dim Y2 = midPts(ptCnt - 1).pt.Y * MainForm.realHeight
        dist = Find_TwoPointDistance(X1, Y1, X2, Y2)
    End Sub
End Structure

Public Structure angleObj
    Public lineObj_1 As lineObj
    Public lineObj_2 As lineObj
    Public midPt As pointObj
    Public angle As Integer
    Public clockwise As Boolean
    Public fixed As Boolean
    Public startAngle As Double
    Public sweepAngle As Double
    Public ptCnt As Integer

    Public Sub Refresh()
        lineObj_1.Refresh() : midPt.Refresh() : lineObj_2.Refresh()
        angle = 0 : ptCnt = 0
        clockwise = False : fixed = False
        startAngle = 0 : sweepAngle = 0
    End Sub
End Structure

Public Structure circleObj
    Public centerPt As pointObj
    Public midPts() As pointObj
    Public drawFlag() As Boolean
    Public ptCnt As Integer
    Public radius As Single
    Public reversed As Boolean

    Public Sub Refresh()
        centerPt.Refresh()
        For i = 0 To 10
            midPts(i).Refresh()
        Next
        For i = 0 To 11
            drawFlag(i) = False
        Next
        radius = 0 : ptCnt = 0 : reversed = False
    End Sub
End Structure

Public Structure fitLineObj
    Public ptCnt As Integer
    Public refPts() As pointObj
    Public lineObj As lineObj
    Public completed As Boolean
    Public Sub Refresh()
        completed = False : ptCnt = 0
        For i = 0 To 20
            refPts(i).Refresh()
        Next
        lineObj.Refresh()
    End Sub
End Structure

Public Structure fitCircleObj
    Public ptCnt As Integer
    Public refPts() As pointObj
    Public circleObj As circleObj
    Public completed As Boolean
    Public Sub Refresh()
        completed = False : ptCnt = 0
        For i = 0 To 20
            refPts(i).Refresh()
        Next
        circleObj.Refresh()
    End Sub
End Structure

Public Structure AnnoObject
    Public Stpt As pointObj       'point used for drawing line
    Public EndPt As pointObj
    Public size As SizeF             'size of anno object
    Public annotation As String         'optional, used for annotation
    Public ptCnt As Integer

    Public Sub Refresh()
        Stpt.Refresh()
        EndPt.Refresh()
        size.Width = 0 : size.Height = 0 : ptCnt = 0
        annotation = "annotation"
    End Sub
End Structure
Public Structure measureObj
    Public mType As Integer
    Public objID As Integer
    Public ptLimit As Integer
    Public ptCnt As Integer
    Public name As String
    Public parameter As String
    Public spec As String
    Public judgement As String
    Public description As String

    Public ptObj As pointObj
    Public lineObj As lineObj
    Public angleObj As angleObj
    Public circleObj As circleObj
    Public fitLineObj As fitLineObj
    Public fitCirObj As fitCircleObj
    Public annoObj As AnnoObject

    Public Sub Refresh()
        mType = MeasureType.initState
        objID = 0
        ptCnt = 0
        ptLimit = 0
        name = ""
        parameter = ""
        spec = ""
        judgement = ""
        description = ""

        ptObj.Refresh()
        lineObj.Refresh()
        angleObj.Refresh()
        circleObj.Refresh()
        fitLineObj.Refresh()
        fitCirObj.Refresh()
        annoObj.Refresh()
    End Sub
End Structure

Public Class CurveObj
    Public CurvePoint(1000) As PointF
    Public CDrawPos As PointF
    Public CPointIndx As Integer

    Public Sub Refresh()
        CDrawPos.X = 0
        CDrawPos.Y = 0
        CPointIndx = 0
        For i = 0 To 1000
            CurvePoint(i).X = 0
            CurvePoint(i).Y = 0
        Next
    End Sub
End Class