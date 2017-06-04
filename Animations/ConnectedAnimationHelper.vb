Public Module ConnectedAnimationHelper
    ''' <summary>
    ''' 将控件保留 Opacity 复制一份，并且用 <see cref="TranslateTransform"/> 将控件的位置移动到与目标控件相同的位置。这适合用户控件和非容器控件。
    ''' </summary>
    ''' <param name="target">目标控件</param>
    ''' <param name="layoutRoot">代表控件坐标原点的控件</param>
    <Extension>
    Public Function CloneControl(target As FrameworkElement, layoutRoot As UIElement) As FrameworkElement
        Dim itemCoordinate = target.TransformToVisual(layoutRoot).TransformPoint(New Point)
        Dim clone As FrameworkElement = Activator.CreateInstance(target.GetType)
        With clone
            .RenderTransform = New TranslateTransform With {
                .X = itemCoordinate.X, .Y = itemCoordinate.Y
            }
            .Width = target.ActualWidth
            .Height = target.ActualHeight
            .HorizontalAlignment = HorizontalAlignment.Left
            .VerticalAlignment = VerticalAlignment.Top
            .Opacity = target.Opacity
        End With
        Return clone
    End Function

End Module
