' The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236
#Const STRICT_ARG_CHECK = True

''' <summary>
''' 验证是否指定的字符串可以使用 TryParse(<see cref="String"/>, 类型引用) 方式转换成目标类型。
''' </summary>
Public NotInheritable Class TypeValidatorTextBlock
    Inherits UserControl

    Public Property ParseToType As Type
        Get
            Return GetValue(ParseToTypeProperty)
        End Get
        Set
            SetValue(ParseToTypeProperty, Value)
        End Set
    End Property
    Public Shared ReadOnly ParseToTypeProperty As DependencyProperty =
                           DependencyProperty.Register(NameOf(ParseToType),
                           GetType(Type), GetType(TypeValidatorTextBlock),
                           New PropertyMetadata(Nothing, Sub(s, e) DirectCast(s, TypeValidatorTextBlock).ValidateType(e.NewValue)))

    Public Property Value As String
        Get
            Return GetValue(ValueProperty)
        End Get
        Set
            SetValue(ValueProperty, Value)
        End Set
    End Property
    Public Shared ReadOnly ValueProperty As DependencyProperty =
                           DependencyProperty.Register(NameOf(Value),
                           GetType(String), GetType(TypeValidatorTextBlock),
                           New PropertyMetadata(Nothing, Sub(s, e) DirectCast(s, TypeValidatorTextBlock).ValidateValue(e.NewValue)))

    Protected Sub ValidateType(tp As Type)
        Validate(Value, tp)
    End Sub

    Protected Sub ValidateValue(value As String)
        Validate(value, ParseToType)
    End Sub

    Protected Sub Validate(value As String, tp As Type)
#If STRICT_ARG_CHECK Then
        If tp Is Nothing Then
            Throw New ArgumentNullException(NameOf(tp))
        End If
#End If
        ErrorMessage = If(TypeValidations.IsConvertable(value, tp), String.Empty, $"无法将类型 '{tp.Name}' 转换为 'System.String'。")
    End Sub

    Public Property ErrorMessage As String
        Get
            Return GetValue(ErrorMessageProperty)
        End Get
        Protected Set(value As String)
            SetValue(ErrorMessageProperty, value)
        End Set
    End Property
    Public Shared ReadOnly ErrorMessageProperty As DependencyProperty =
                           DependencyProperty.Register(NameOf(ErrorMessage),
                           GetType(String), GetType(TypeValidatorTextBlock),
                           New PropertyMetadata(Nothing, Sub(s, e) DirectCast(s, TypeValidatorTextBlock).TxtErrorMessage.Text = e.NewValue))
End Class
