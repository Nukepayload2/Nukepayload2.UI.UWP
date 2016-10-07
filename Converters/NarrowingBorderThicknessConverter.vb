Public Class NarrowingBorderThicknessConverter
    Implements IValueConverter

    Public Function Convert(value As Object, targetType As Type, parameter As Object, language As String) As Object Implements IValueConverter.Convert
        Dim th = DirectCast(value, Thickness)
        Return th.Left
    End Function

    Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, language As String) As Object Implements IValueConverter.ConvertBack
        Dim th = DirectCast(value, Double)
        Return New Thickness(th)
    End Function
End Class