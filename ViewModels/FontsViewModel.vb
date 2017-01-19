Friend Class FontsViewModel
    Implements INotifyPropertyChanged

    Dim _FontInformation As New FontInformation
    Public Property FontInformation As FontInformation
        Get
            Return _FontInformation
        End Get
        Set
            _FontInformation = Value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(FontInformation)))
        End Set
    End Property

    Public ReadOnly Property FontWeights As String()
    Public ReadOnly Property FontFamilies As String()
    Public ReadOnly Property FontSizes As Single()
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Sub New()
        FontWeights = FontDefaults.FontWeights.Keys.ToArray
        FontFamilies = Microsoft.Graphics.Canvas.Text.CanvasTextFormat.GetSystemFontFamilies()
        FontSizes = (Iterator Function()
                         For f = 6.0F To 72.0F Step 1.0F
                             Yield f
                         Next
                     End Function).Invoke.ToArray
    End Sub

End Class