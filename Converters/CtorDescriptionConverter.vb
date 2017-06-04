Imports Nukepayload2.CodeAnalysis

Public Class CtorDescriptionConverter
    Implements IValueConverter

    Public Function Convert(value As Object, targetType As Type, parameter As Object, language As String) As Object Implements IValueConverter.Convert
        Return "Sub New(" + New VBObjectViewer().AnalyzeConstructor(value) + ")"
    End Function

    Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, language As String) As Object Implements IValueConverter.ConvertBack
        Throw New NotImplementedException()
    End Function
End Class
