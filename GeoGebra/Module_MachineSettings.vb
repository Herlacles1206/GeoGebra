Module Module_MachineSettings



    Public Function Pause(ByVal Duration As Integer)
        Threading.Thread.Sleep(Duration)
        Return Nothing
    End Function

End Module
