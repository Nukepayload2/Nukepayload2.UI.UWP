Imports System.Reflection

Public Class EnumValuesConverter
    Implements IValueConverter

    Public Function Convert(value As Object, targetType As Type, parameter As Object, language As String) As Object Implements IValueConverter.Convert
        Return From f In value.GetType.GetTypeInfo.DeclaredFields Where f.IsLiteral Select [Enum].GetName(f.DeclaringType, f.GetValue(value))
    End Function

    Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, language As String) As Object Implements IValueConverter.ConvertBack
        Return [Enum].Parse(targetType, value.ToString)
    End Function
End Class