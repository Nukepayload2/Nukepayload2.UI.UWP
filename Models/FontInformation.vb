Imports Windows.UI.Text

Public Class FontInformation
    Implements INotifyPropertyChanged

    Dim _FontSize As Single = 14
    Public Property FontSize As Single
        Get
            Return _FontSize
        End Get
        Set
            If Not _FontSize.Equals(Value) Then
                _FontSize = Value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(FontSize)))
            End If
        End Set
    End Property

    Dim _FontFamily As String = Media.FontFamily.XamlAutoFontFamily.Source
    Public Property FontFamily As String
        Get
            Return _FontFamily
        End Get
        Set
            If Not _FontFamily.Equals(Value) Then
                _FontFamily = Value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(FontFamily)))
            End If
        End Set
    End Property


    Dim _FontStretch As FontStretch = FontStretch.Normal
    Public Property FontStretch As FontStretch
        Get
            Return _FontStretch
        End Get
        Set
            If _FontStretch <> Value Then
                _FontStretch = Value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(FontStretch)))
            End If
        End Set
    End Property

    Dim _FontStyle As FontStyle
    Public Property FontStyle As FontStyle
        Get
            Return _FontStyle
        End Get
        Set
            If _FontStyle <> Value Then
                _FontStyle = Value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(FontStyle)))
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(IsItalic)))
            End If
        End Set
    End Property

    Dim _FontWeight As FontWeight? = FontWeights.Normal
    Public Property FontWeight As FontWeight?
        Get
            Return _FontWeight
        End Get
        Set
            If Value.HasValue Then
                If _FontWeight.Value.Weight <> Value.Value.Weight Then
                    _FontWeight = Value
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(FontWeight)))
                    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(IsBold)))
                End If
            End If
        End Set
    End Property

    Public Property IsColorFontEnabled As Boolean = True
    Public Property IsBold As Boolean
        Get
            Return FontWeight.Equals(FontWeights.Bold)
        End Get
        Set
            FontWeight = If(Value, FontWeights.Bold, FontWeights.Normal)
        End Set
    End Property

    Public Property IsItalic As Boolean
        Get
            Return FontStyle = Windows.UI.Text.FontStyle.Italic
        End Get
        Set(value As Boolean)
            FontStyle = If(value, FontStyle.Italic, FontStyle.Normal)
        End Set
    End Property

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
    ''' <summary>
    ''' 将当前字体属性转换为能用作文件名的字符串
    ''' </summary>
    Public Overrides Function ToString() As String
        Return FontFamily & If(IsBold, " Bold", "") & If(IsItalic, " Italic", "") & " " & FontSize
    End Function
End Class