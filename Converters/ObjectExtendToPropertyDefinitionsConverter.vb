﻿''' <summary>
''' 将对象转换为可编辑的属性集合
''' </summary>
Public Class ObjectExtendToPropertyDefinitionsConverter
    Implements IValueConverter

    Public Function Convert(value As Object, targetType As Type, parameter As Object, language As String) As Object Implements IValueConverter.Convert
        Return ObjectDeconstructor.GetPropertyDefinitions(value)
    End Function

    Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, language As String) As Object Implements IValueConverter.ConvertBack
        Return TryCast(value, IEnumerable(Of PropertyDefinition))?.FirstOrDefault?.Owner
    End Function
End Class
