Public Class DateTimeToOffsetConverter
    Implements IValueConverter

    Public Function Convert(value As Object, targetType As Type, parameter As Object, language As String) As Object Implements IValueConverter.Convert
        Return New DateTimeOffset(DirectCast(value, Date))
    End Function

    Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, language As String) As Object Implements IValueConverter.ConvertBack
        Return DirectCast(value, DateTimeOffset).Date
    End Function
End Class
