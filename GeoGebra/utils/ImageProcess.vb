Imports Emgu.CV
Imports Emgu.CV.Structure

Public Module ImageProcess

    Public Function GetMatFromSDImage(ByVal image As Image) As Mat
        If image Is Nothing Then
            Return Nothing
        End If
        Dim stride = 0
        Dim bmp As Bitmap = New Bitmap(image)

        Dim rect As Rectangle = New Rectangle(0, 0, bmp.Width, bmp.Height)
        Dim bmpData = bmp.LockBits(rect, Imaging.ImageLockMode.ReadWrite, bmp.PixelFormat)

        Dim pf = bmp.PixelFormat
        If pf = Imaging.PixelFormat.Format32bppArgb Then
            stride = bmp.Width * 4
        Else
            stride = bmp.Width * 3
        End If

        Dim cvImage As Emgu.CV.Image(Of Bgra, Byte) = New Emgu.CV.Image(Of Bgra, Byte)(bmp.Width, bmp.Height, stride, bmpData.Scan0)

        bmp.UnlockBits(bmpData)

        Return cvImage.Mat
    End Function

    Public Function DetectCurve(ByVal scr As Image, ByVal FirstPtOfEdge As Point, ByVal SecondPtOfEdge As Point) As CurveObj
        Dim bmpImage As Bitmap = New Bitmap(scr)
        Dim grayImage As Emgu.CV.Image(Of Gray, Byte) = bmpImage.ToImage(Of Gray, Byte)()
        Dim colorImage As Emgu.CV.Image(Of Bgr, Byte) = bmpImage.ToImage(Of Bgr, Byte)()
        bmpImage.Dispose()

        Dim C_Curve As CurveObj() = New CurveObj(1000) {}
        For i = 0 To 1000
            C_Curve(i) = New CurveObj()
        Next
        Dim Width = scr.Width
        Dim Height = scr.Height
        Dim CropWidth = SecondPtOfEdge.X - FirstPtOfEdge.X
        Dim CropHeight = SecondPtOfEdge.Y - FirstPtOfEdge.Y
        Dim Region As Rectangle = New Rectangle(FirstPtOfEdge.X, FirstPtOfEdge.Y, CropWidth, CropHeight)
        If Region.Width <= 0 Or Region.Height <= 0 Then Return C_Curve(0)
        grayImage.ROI = Region

        Dim cropped = grayImage.Copy()
        Dim tempimg As Emgu.CV.Image(Of Emgu.CV.Structure.Gray, Byte) = cropped
        CvInvoke.MedianBlur(cropped, cropped, 3)
        'Emgu.CV.CvInvoke.Imshow("original image", cropped)
        CvInvoke.Canny(cropped, tempimg, 100, 200)

        Dim Edge = tempimg.Data

        Dim totalCnt As Integer = 0
        Dim nX2, nY2, nX3, nY3, nStackNum, tempX, tempY As Integer
        Dim iContinue As Boolean
        'get connected elements
        For x = 0 To CropWidth - 1
            For y = 0 To CropHeight - 1
                Dim OriX = FirstPtOfEdge.X + x
                Dim OriY = FirstPtOfEdge.Y + y

                If Edge(y, x, 0) <> 0 Then
                    Dim Pos = New PointF(OriX / CDbl(Width), OriY / CDbl(Height))
                    C_Curve(totalCnt).CurvePoint(C_Curve(totalCnt).CPointIndx) = Pos
                    C_Curve(totalCnt).CPointIndx += 1
                    Edge(y, x, 0) = 0
                    nX3 = x
                    nY3 = y
                    While (1)
                        nX2 = nX3 - 1
                        nY2 = nY3 - 1
                        iContinue = True
                        nStackNum = 0
                        If nX2 < 0 Or nX2 > CropWidth - 1 Or nY2 < 0 Or nY2 > CropHeight - 1 Then
                            iContinue = False
                        Else
                            If Edge(nY2, nX2, 0) = 0 Then
                                iContinue = False
                            End If
                        End If
                        If iContinue Then
                            Pos = New PointF((FirstPtOfEdge.X + nX2) / CDbl(Width), (FirstPtOfEdge.Y + nY2) / CDbl(Height))
                            C_Curve(totalCnt).CurvePoint(C_Curve(totalCnt).CPointIndx) = Pos
                            C_Curve(totalCnt).CPointIndx += 1
                            Edge(nY2, nX2, 0) = 0
                            nStackNum += 1
                            tempX = nX2 : tempY = nY2
                        End If

                        nX2 = nX3
                        nY2 = nY3 - 1
                        iContinue = True
                        If nX2 < 0 Or nX2 > CropWidth - 1 Or nY2 < 0 Or nY2 > CropHeight - 1 Then
                            iContinue = False
                        Else
                            If Edge(nY2, nX2, 0) = 0 Then
                                iContinue = False
                            End If
                        End If
                        If iContinue Then
                            Pos = New PointF((FirstPtOfEdge.X + nX2) / CDbl(Width), (FirstPtOfEdge.Y + nY2) / CDbl(Height))
                            C_Curve(totalCnt).CurvePoint(C_Curve(totalCnt).CPointIndx) = Pos
                            C_Curve(totalCnt).CPointIndx += 1
                            Edge(nY2, nX2, 0) = 0
                            nStackNum += 1
                            tempX = nX2 : tempY = nY2
                        End If

                        nX2 = nX3 + 1
                        nY2 = nY3 - 1
                        iContinue = True
                        If nX2 < 0 Or nX2 > CropWidth - 1 Or nY2 < 0 Or nY2 > CropHeight - 1 Then
                            iContinue = False
                        Else
                            If Edge(nY2, nX2, 0) = 0 Then
                                iContinue = False
                            End If
                        End If
                        If iContinue Then
                            Pos = New PointF((FirstPtOfEdge.X + nX2) / CDbl(Width), (FirstPtOfEdge.Y + nY2) / CDbl(Height))
                            C_Curve(totalCnt).CurvePoint(C_Curve(totalCnt).CPointIndx) = Pos
                            C_Curve(totalCnt).CPointIndx += 1
                            Edge(nY2, nX2, 0) = 0
                            nStackNum += 1
                            tempX = nX2 : tempY = nY2
                        End If

                        nX2 = nX3 - 1
                        nY2 = nY3
                        iContinue = True
                        If nX2 < 0 Or nX2 > CropWidth - 1 Or nY2 < 0 Or nY2 > CropHeight - 1 Then
                            iContinue = False
                        Else
                            If Edge(nY2, nX2, 0) = 0 Then
                                iContinue = False
                            End If
                        End If
                        If iContinue Then
                            Pos = New PointF((FirstPtOfEdge.X + nX2) / CDbl(Width), (FirstPtOfEdge.Y + nY2) / CDbl(Height))
                            C_Curve(totalCnt).CurvePoint(C_Curve(totalCnt).CPointIndx) = Pos
                            C_Curve(totalCnt).CPointIndx += 1
                            Edge(nY2, nX2, 0) = 0
                            nStackNum += 1
                            tempX = nX2 : tempY = nY2
                        End If

                        nX2 = nX3 + 1
                        nY2 = nY3
                        iContinue = True
                        If nX2 < 0 Or nX2 > CropWidth - 1 Or nY2 < 0 Or nY2 > CropHeight - 1 Then
                            iContinue = False
                        Else
                            If Edge(nY2, nX2, 0) = 0 Then
                                iContinue = False
                            End If
                        End If
                        If iContinue Then
                            Pos = New PointF((FirstPtOfEdge.X + nX2) / CDbl(Width), (FirstPtOfEdge.Y + nY2) / CDbl(Height))
                            C_Curve(totalCnt).CurvePoint(C_Curve(totalCnt).CPointIndx) = Pos
                            C_Curve(totalCnt).CPointIndx += 1
                            Edge(nY2, nX2, 0) = 0
                            nStackNum += 1
                            tempX = nX2 : tempY = nY2
                        End If

                        nX2 = nX3 - 1
                        nY2 = nY3 + 1
                        iContinue = True
                        If nX2 < 0 Or nX2 > CropWidth - 1 Or nY2 < 0 Or nY2 > CropHeight - 1 Then
                            iContinue = False
                        Else
                            If Edge(nY2, nX2, 0) = 0 Then
                                iContinue = False
                            End If
                        End If
                        If iContinue Then
                            Pos = New PointF((FirstPtOfEdge.X + nX2) / CDbl(Width), (FirstPtOfEdge.Y + nY2) / CDbl(Height))
                            C_Curve(totalCnt).CurvePoint(C_Curve(totalCnt).CPointIndx) = Pos
                            C_Curve(totalCnt).CPointIndx += 1
                            Edge(nY2, nX2, 0) = 0
                            nStackNum += 1
                            tempX = nX2 : tempY = nY2
                        End If

                        nX2 = nX3
                        nY2 = nY3 + 1
                        iContinue = True
                        If nX2 < 0 Or nX2 > CropWidth - 1 Or nY2 < 0 Or nY2 > CropHeight - 1 Then
                            iContinue = False
                        Else
                            If Edge(nY2, nX2, 0) = 0 Then
                                iContinue = False
                            End If
                        End If
                        If iContinue Then
                            Pos = New PointF((FirstPtOfEdge.X + nX2) / CDbl(Width), (FirstPtOfEdge.Y + nY2) / CDbl(Height))
                            C_Curve(totalCnt).CurvePoint(C_Curve(totalCnt).CPointIndx) = Pos
                            C_Curve(totalCnt).CPointIndx += 1
                            Edge(nY2, nX2, 0) = 0
                            nStackNum += 1
                            tempX = nX2 : tempY = nY2
                        End If

                        nX2 = nX3 + 1
                        nY2 = nY3 + 1
                        iContinue = True
                        If nX2 < 0 Or nX2 > CropWidth - 1 Or nY2 < 0 Or nY2 > CropHeight - 1 Then
                            iContinue = False
                        Else
                            If Edge(nY2, nX2, 0) = 0 Then
                                iContinue = False
                            End If
                        End If
                        If iContinue Then
                            Pos = New PointF((FirstPtOfEdge.X + nX2) / CDbl(Width), (FirstPtOfEdge.Y + nY2) / CDbl(Height))
                            C_Curve(totalCnt).CurvePoint(C_Curve(totalCnt).CPointIndx) = Pos
                            C_Curve(totalCnt).CPointIndx += 1
                            Edge(nY2, nX2, 0) = 0
                            nStackNum += 1
                            tempX = nX2 : tempY = nY2
                        End If

                        If nStackNum < 1 Then
                            Exit While
                        End If
                        nX3 = tempX : nY3 = tempY
                    End While
                    totalCnt += 1
                    If totalCnt > 1000 Then
                        Exit For
                    End If
                End If
            Next
        Next

        'get the tallest curve
        Dim maxCnt, maxCount As Integer
        maxCnt = 0 : maxCount = 0
        For i = 0 To totalCnt - 1
            If C_Curve(i).CPointIndx > maxCount Then
                maxCnt = i
                maxCount = C_Curve(i).CPointIndx
            End If
        Next
        C_Curve(maxCnt).CPointIndx -= 1
        Return C_Curve(maxCnt)

    End Function
End Module
