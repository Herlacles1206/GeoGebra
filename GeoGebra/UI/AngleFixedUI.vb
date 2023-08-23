Public Class AngleFixedUI
    Public angle As Integer
    Public clockwise As Boolean

    Private Sub CheckIfClockwise()
        If radio_counterclock.Checked Then
            clockwise = False
        Else
            clockwise = True
        End If
    End Sub
    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles radio_counterclock.CheckedChanged
        CheckIfClockwise()
    End Sub

    Private Sub radio_clock_CheckedChanged(sender As Object, e As EventArgs) Handles radio_clock.CheckedChanged
        CheckIfClockwise()
    End Sub
End Class