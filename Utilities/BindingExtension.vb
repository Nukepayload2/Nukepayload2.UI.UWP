Module BindingExtension
    <Extension>
    Public Sub BindTwoWay(this As FrameworkElement, dp As DependencyProperty, path As String, source As Object)
        this.SetBinding(dp, New Binding With {.Path = New PropertyPath(path), .Mode = BindingMode.TwoWay, .Source = source})
    End Sub
End Module