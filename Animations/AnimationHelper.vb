Imports Windows.UI.Xaml.Media.Animation

Public Module AnimationHelper
    ''' <summary>
    ''' 模拟实现 WPF 的 BeginAnimation，结合 NameOf 可方便移植
    ''' </summary>
    ''' <param name="ele">要应用时间线的元素</param>
    ''' <param name="path">元素要改变的属性名</param>
    ''' <param name="anim">相应的时间线</param>
    ''' <remarks></remarks>
    <Extension>
    Public Sub BeginAnimation(ele As DependencyObject, path As String, anim As Timeline)
        Dim sb As New Storyboard
        sb.Children.Add(anim)
        Storyboard.SetTarget(anim, ele)
        Storyboard.SetTargetProperty(anim, path)
        sb.Begin()
    End Sub
End Module
