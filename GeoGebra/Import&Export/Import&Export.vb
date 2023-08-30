Imports ClosedXML.Excel
Imports System.IO
Imports Emgu.CV
Imports System.Runtime.CompilerServices
Imports Emgu.CV.CvEnum

Public Module Import_Export
    Public Function LoadImageFromFile(ByVal pictureBox As PictureBox, ByVal filter As String, ByVal fileDialogTitle As String) As Mat
        Dim openFileDialog As OpenFileDialog = New OpenFileDialog()
        openFileDialog.Filter = filter
        openFileDialog.Title = fileDialogTitle
        Dim img_name As String

        If openFileDialog.ShowDialog() = DialogResult.OK Then
            img_name = openFileDialog.FileName
        Else
            Return Nothing
        End If

        Dim img = CvInvoke.Imread(img_name, ImreadModes.AnyColor)

        Return img
    End Function
    Public Sub SaveReportToExcel(ByVal picturebox As PictureBox, ByVal filter As String, ByVal saveDialogTitle As String, ByVal obj_list As List(Of measureObj))
        Dim SaveFileDialog As SaveFileDialog = New SaveFileDialog()
        SaveFileDialog.Filter = filter
        SaveFileDialog.Title = saveDialogTitle

        Dim xlsx_savepath = ""
        If SaveFileDialog.ShowDialog() = DialogResult.OK Then
            xlsx_savepath = SaveFileDialog.FileName

            Dim NameSet = "ABCDHIJKLMNOPQRSTUVWXYZ"

            Using workbook As XLWorkbook = XLWorkbook.OpenFromTemplate("template.xlsx")
                Dim img = picturebox.Image
                If img IsNot Nothing Then
                    Dim result As Bitmap = New Bitmap(img.Width, img.Height)

                    Using graph = Graphics.FromImage(result)
                        graph.Clear(Color.White)
                        DrawObjList(graph, picturebox, obj_list)
                        graph.Flush()
                    End Using

                    Dim ms As MemoryStream = New MemoryStream()
                    result.Save(ms, Imaging.ImageFormat.Png)

                    Dim worksheet As IXLWorksheet
                    workbook.TryGetWorksheet("Sheet1", worksheet)


                    Dim image = worksheet.AddPicture(ms).MoveTo(worksheet.Cell("A14"), worksheet.Cell("K31")) 'the cast is only to be sure

                    workbook.SaveAs(xlsx_savepath)
                End If
            End Using
        End If

    End Sub

End Module
