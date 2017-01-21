' The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

Imports Windows.UI

Public NotInheritable Class ColorEditor
    Inherits UserControl

    Public Property Color As Color
        Get
            Return GetValue(ColorProperty)
        End Get
        Set
            SetValue(ColorProperty, Value)
        End Set
    End Property
    Public Shared ReadOnly ColorProperty As DependencyProperty =
                           DependencyProperty.Register(NameOf(Color),
                           GetType(Color), GetType(ColorEditor),
                           New PropertyMetadata(Colors.Transparent,
                                                Sub(s, e)
                                                    Dim this = DirectCast(s, ColorEditor)
                                                    this.RectPreview.Fill = New SolidColorBrush(e.NewValue)
                                                End Sub))

    Private Async Sub BtnPickColor_ClickAsync(sender As Object, e As RoutedEventArgs) Handles BtnPickColor.Click
        Dim colorDlg As New ColorPickerDialog(Me.Color)
        Dim color = Await colorDlg.ShowAsync()
        If color.HasValue Then
            Me.Color = color.Value
        End If
    End Sub
End Class
