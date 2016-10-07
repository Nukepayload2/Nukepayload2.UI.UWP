Imports Windows.UI.Text

Public Class FontStretchBackToStringConverter
    Implements IValueConverter

    Public Function Convert(value As Object, targetType As Type, parameter As Object, language As String) As Object Implements IValueConverter.Convert
        Return value.ToString
    End Function

    Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, language As String) As Object Implements IValueConverter.ConvertBack
        If value Is Nothing Then Return Nothing
        Return [Enum].Parse(GetType(FontStretch), value)
    End Function
End Class
