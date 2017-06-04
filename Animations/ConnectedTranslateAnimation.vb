Imports Windows.UI.Xaml.Media.Animation
''' <summary>
''' 表示适合 用户控件 和 非容器控件 的平移型连接的动画。
''' </summary>
Public Class ConnectedTranslateAnimation

    Dim mainPageElement, subPageElement As FrameworkElement
    Dim lastOpacity As Double
    Dim mainPageCoordinate, subPageCoordinate As Point

    ''' <summary>
    ''' 准备某个控件在连接的动画之前
    ''' </summary>
    ''' <param name="target">目标控件</param>
    ''' <param name="layoutRoot">代表控件坐标原点的控件</param>
    Public Sub PrepareForward(target As FrameworkElement, layoutRoot As UIElement)
        lastOpacity = target.Opacity
        subPageElement = CloneControl(target, layoutRoot)
        Dim translate = DirectCast(subPageElement.RenderTransform, TranslateTransform)
        mainPageCoordinate = New Point(translate.X, translate.Y)
        target.Opacity = 0
        mainPageElement = target
    End Sub
    ''' <summary>
    ''' 开始一个移动图标的连接动画
    ''' </summary>
    ''' <param name="layoutRoot">整个界面的容器</param>
    ''' <param name="targetContainer">目标的容器</param>
    ''' <param name="duration">持续时间</param>
    ''' <param name="configureAnimation">修改动画属性的回调。第一个是 X 动画，另一个是 Y 动画。</param>
    Public Sub BeginForward(layoutRoot As Panel, targetContainer As ContentControl, duration As TimeSpan,
                            Optional configureAnimation As Action(Of DoubleAnimation, DoubleAnimation) = Nothing)
        layoutRoot.Children.Add(subPageElement)
        subPageCoordinate = targetContainer.TransformToVisual(layoutRoot).TransformPoint(New Point)
        Dim animX As New DoubleAnimation With {
            .From = mainPageCoordinate.X, .To = subPageCoordinate.X, .Duration = duration
        }
        Dim animY As New DoubleAnimation With {
            .From = mainPageCoordinate.Y, .To = subPageCoordinate.Y, .Duration = duration
        }
        configureAnimation?(animX, animY)
        AddHandler animX.Completed,
            Sub()
                layoutRoot.Children.Remove(subPageElement)
                subPageElement.RenderTransform = New TranslateTransform
                targetContainer.Content = subPageElement
            End Sub
        Dim translate = DirectCast(subPageElement.RenderTransform, TranslateTransform)
        translate.BeginAnimation(NameOf(translate.X), animX)
        translate.BeginAnimation(NameOf(translate.Y), animY)
    End Sub

    ''' <summary>
    ''' 准备某个控件在连接的动画之前
    ''' </summary>
    Public Sub PrepareBack(targetContainer As ContentControl)
        targetContainer.Content = Nothing
    End Sub
    ''' <summary>
    ''' 反向播放连接的动画
    ''' </summary>
    ''' <param name="layoutRoot">布局根元素</param>
    ''' <param name="duration">持续时间</param>
    ''' <param name="configureAnimation">修改动画属性的回调。第一个是 X 动画，另一个是 Y 动画。</param>
    Public Sub BeginBack(layoutRoot As Panel, duration As TimeSpan,
                         Optional configureAnimation As Action(Of DoubleAnimation, DoubleAnimation) = Nothing)
        mainPageCoordinate = mainPageElement.TransformToVisual(layoutRoot).TransformPoint(New Point)
        layoutRoot.Children.Add(subPageElement)
        Dim animX As New DoubleAnimation With {
            .From = subPageCoordinate.X, .To = mainPageCoordinate.X, .Duration = duration
        }
        Dim animY As New DoubleAnimation With {
            .From = subPageCoordinate.Y, .To = mainPageCoordinate.Y, .Duration = duration
        }
        configureAnimation?(animX, animY)
        AddHandler animX.Completed,
                   Sub()
                       layoutRoot.Children.Remove(subPageElement)
                       mainPageElement.Opacity = lastOpacity
                   End Sub
        Dim translate = DirectCast(subPageElement.RenderTransform, TranslateTransform)
        translate.BeginAnimation(NameOf(translate.X), animX)
        translate.BeginAnimation(NameOf(translate.Y), animY)
    End Sub
End Class
