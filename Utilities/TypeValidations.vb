Imports System.Reflection

Public Class TypeValidations

    Public Shared Function IsConvertable(content As String, tp As Type) As Boolean
        Return TryParseDynamic(content, tp, Nothing)
    End Function

    Public Shared Function TryParseDynamic(stringValue As String, tp As Type, ByRef convertedValue As Object) As Boolean
        Dim targetType = tp.GetTypeInfo
        If targetType.Equals(GetType(String)) Then
            convertedValue = stringValue
            Return True
        End If

        Dim nullableType = targetType.IsGenericType AndAlso targetType.GetGenericTypeDefinition().Equals(GetType(Nullable(Of )))
        If nullableType Then
            If String.IsNullOrEmpty(stringValue) Then
                convertedValue = Nothing
                Return True
            End If
            tp = Nullable.GetUnderlyingType(tp)
            targetType = tp.GetTypeInfo
        End If

        Dim argTypes = {GetType(String), tp.MakeByRefType()}
        Dim tryParseMethodInfo = tp.GetRuntimeMethod("TryParse", argTypes)
        If tryParseMethodInfo IsNot Nothing Then
            Dim args As Object() = {stringValue, Nothing}
            Dim successfulParse = DirectCast(tryParseMethodInfo.Invoke(Nothing, args), Boolean)
            If Not successfulParse Then
                Return False
            End If
            convertedValue = If(nullableType, Activator.CreateInstance(GetType(Nullable(Of )).MakeGenericType(tp), args(1)), args(1))
            Return True
        Else
            Try
                Convert.ChangeType(stringValue, tp)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End If
    End Function
End Class
