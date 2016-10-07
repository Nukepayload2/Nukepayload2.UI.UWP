Public Class NamedProperty(Of T)
    Sub New(name As String, data As T)
        Me.Name = name
        Me.Data = data
    End Sub

    Public Property Name As String
    Public Property Data As T

End Class
