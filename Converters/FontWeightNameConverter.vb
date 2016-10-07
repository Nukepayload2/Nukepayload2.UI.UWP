Public Class FontWeightNameConverter
    Implements IValueConverter

    Public Function Convert(value As Object, targetType As Type, parameter As Object, language As String) As Object Implements IValueConverter.Convert
        Return FontDefaults.FontWeightsInv(value)
    End Function

    Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, language As String) As Object Implements IValueConverter.ConvertBack
        Return FontDefaults.FontWeights(DirectCast(value, String))
    End Function
End Class