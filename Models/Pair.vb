Public Class Pair(Of T1, T2)
    Public Sub New(item1 As T1, item2 As T2)
        Me.Item1 = item1
        Me.Item2 = item2
    End Sub
    Sub New()

    End Sub
    Public Property Item1 As T1
    Public Property Item2 As T2

End Class
