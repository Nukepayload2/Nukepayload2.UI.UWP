''' <summary>
''' 验证字符串的类型
''' </summary>
Public Class StringValidator

    Shared ReadOnly _typeValidations As New Dictionary(Of Type, Func(Of String, Boolean)) From {
        {GetType(Long), Function(o) Long.TryParse(o, 0L)},
        {GetType(ULong), Function(o) ULong.TryParse(o, 0UL)},
        {GetType(Integer), Function(o) Integer.TryParse(o, 0)},
        {GetType(UInteger), Function(o) UInteger.TryParse(o, 0UI)},
        {GetType(Short), Function(o) Short.TryParse(o, 0S)},
        {GetType(UShort), Function(o) UShort.TryParse(o, 0US)},
        {GetType(SByte), Function(o) SByte.TryParse(o, New SByte)},
        {GetType(Byte), Function(o) UShort.TryParse(o, New Byte)},
        {GetType(Single), Function(o) Single.TryParse(o, 0F)}，
        {GetType(Double), Function(o) Double.TryParse(o, 0R)}，
        {GetType(Decimal), Function(o) Double.TryParse(o, 0D)},
        {GetType(TimeSpan), Function(o) TimeSpan.TryParse(o, New TimeSpan)},
        {GetType(Date), Function(o) Date.TryParse(o, New Date)},
        {GetType(DateTimeOffset), Function(o) DateTimeOffset.TryParse(o, New DateTimeOffset)},
        {GetType(String), Function(o) True},
        {GetType(Char), Function(o) o.Length = 1}
    }

    ''' <summary>
    ''' 验证指定的字符串是否能转换为指定的 数字，日期，时间间隔 或 字符 类型。如果不能转换，则返回异常提示。如果可以转换，返回空字符串。
    ''' </summary>
    ''' <param name="value"></param>
    ''' <param name="expectedType"></param>
    ''' <exception cref="NotSupportedException">尝试检查转换到非预期的类型。</exception>
    ''' <returns>异常提示</returns>
    Public Function Validate(value As String, expectedType As Type) As String
        If _typeValidations.ContainsKey(expectedType) Then
            Return If(_typeValidations(expectedType)(value), String.Empty, $"无法将输入的数据转换为 {expectedType.ToString}。请检查输入的格式。")
        End If
        Throw New NotSupportedException("预期类型不在可验证范围内。")
    End Function
End Class
