Imports System.ComponentModel.DataAnnotations
Imports System.Reflection
''' <summary>
''' <see cref="[Enum]"/>-&gt;<see cref="IEnumerable(Of String)"/>
''' </summary>
Public Class EnumItemsSourceConverter
    Implements IValueConverter

    Public Function Convert(value As Object, targetType As Type, parameter As Object, language As String) As Object Implements IValueConverter.Convert
        Return From field In DirectCast(value, [Enum]).GetType().GetTypeInfo().DeclaredFields
               Where field.IsLiteral
               Let attribs = field.GetCustomAttributes(Of DisplayAttribute)
               Select If(attribs.Any, attribs.First.GetName(), field.Name)
    End Function

    Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, language As String) As Object Implements IValueConverter.ConvertBack
        Throw New NotSupportedException
    End Function
End Class