Public Class MainForm
    Public zoomFactor As Double
    Public curMeasureType As Integer
    Public prevMeasureType As Integer
    Public mLBtnDown As Boolean
    Public mPtF As PointF = New PointF()
    Public mPt As Point = New Point()
    Public nullBrush As Brush = New SolidBrush(Color.White)
    Public Shared drawFont As Font = New Font("Arial", 10, FontStyle.Regular)
    Public Shared drawPen As Pen = New Pen(Color.Black, 1)
    Public Shared drawBrush As Brush = New SolidBrush(Color.Black)
    Public Shared ptBrush As Brush = New SolidBrush(Color.Blue)
    Public Shared ptNameSet As String() = {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
                                           "AA", "AB", "AC", "AD", "AE", "AF", "AG", "AH", "AI", "AJ", "AK", "AL", "AM", "AN", "AO", "AP", "AQ", "AR", "AS", "AT", "AU", "AV", "AW", "AX", "AY", "AZ",
                                           "BA", "BB", "BC", "BD", "BE", "BF", "BG", "BH", "BI", "BJ", "BK", "BL", "BM", "BN", "BO", "BP", "BQ", "BR", "BS", "BT", "BU", "BV", "BW", "BX", "BY", "BZ",
                                           "CA", "CB", "CC", "CD", "CE", "CF", "CG", "CH", "CI", "CJ", "CK", "CL", "CM", "CN", "CO", "CP", "CQ", "CR", "CS", "CT", "CU", "CV", "CW", "CX", "CY", "CZ",
                                           "DA", "DB", "DC", "DD", "DE", "DF", "DG", "DH", "DI", "DJ", "DK", "DL", "DM", "DN", "DO", "DP", "DQ", "DR", "DS", "DT", "DU", "DV", "DW", "DX", "DY", "DZ"}

    Public curObj As measureObj
    Public objList As List(Of measureObj) = New List(Of measureObj)

    Private Sub InitializeComponents()
        pic_main.Invoke(New Action(Sub() pic_main.Image = New Bitmap(pic_main.Width, pic_main.Height)))
    End Sub

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeComponents()
    End Sub

    Private Sub InitializeCurObj(mType As Integer)
        curObj.Refresh()
        curObj.mType = mType
    End Sub

    Private Sub btn_point_Click(sender As Object, e As EventArgs) Handles btn_point.Click
        curMeasureType = MeasureType.point
        InitializeCurObj(curMeasureType)
    End Sub
    Private Sub btn_line_Click(sender As Object, e As EventArgs) Handles btn_line.Click
        curMeasureType = MeasureType.line
        InitializeCurObj(curMeasureType)
    End Sub

    Private Sub btn_angle_Click(sender As Object, e As EventArgs) Handles btn_angle.Click
        curMeasureType = MeasureType.angle
        InitializeCurObj(curMeasureType)
    End Sub

    Private Sub btn_circle_center_Click(sender As Object, e As EventArgs) Handles btn_circle_center.Click
        curMeasureType = MeasureType.circleWithCenter
        InitializeCurObj(curMeasureType)
    End Sub

    Private Sub GetMousePositions(X As Integer, Y As Integer)
        mPt.X = X : mPt.Y = Y
        mPtF.X = CDbl(X) / pic_main.Width
        mPtF.Y = CDbl(Y) / pic_main.Height
    End Sub
    Private Sub AppendObjToList()
        objList.Add(curObj)
        curObj.Refresh()
        curMeasureType = -1
    End Sub
    Private Sub pic_main_MouseDown(sender As Object, e As MouseEventArgs) Handles pic_main.MouseDown
        If e.Button = MouseButtons.Left Then
            mLBtnDown = True
            GetMousePositions(e.X, e.Y)

            If curMeasureType >= 0 Then
                Dim completed = UpdateObj(curObj, curMeasureType, mPtF)
                If completed Then
                    AppendObjToList()
                End If
                DrawObjList(pic_main, objList)
            End If

        End If
    End Sub

    Private Sub pic_main_MouseMove(sender As Object, e As MouseEventArgs) Handles pic_main.MouseMove
        GetMousePositions(e.X, e.Y)
        If curMeasureType >= 0 And curObj.ptCnt > 0 Then
            DrawObjList(pic_main, objList)
            DrawObj(pic_main, curObj, curMeasureType, mPtF)
        End If
    End Sub

    Private Sub pic_main_MouseUp(sender As Object, e As MouseEventArgs) Handles pic_main.MouseUp
        mLBtnDown = False
    End Sub


End Class
