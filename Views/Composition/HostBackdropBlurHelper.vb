Imports System.Numerics
Imports Windows.ApplicationModel.Core
Imports Windows.UI
Imports Windows.UI.Composition
Imports Windows.UI.Xaml.Hosting

Public Class HostBackdropBlurHelper

    Public Shared Function CreateBackdropSpriteVisual(hostElement As Panel) As SpriteVisual
        Dim containerVisual = ElementCompositionPreview.GetElementVisual(hostElement)
        Dim compositor = Window.Current.Compositor
        Dim sprite = compositor.CreateSpriteVisual
        Dim brush = compositor.CreateHostBackdropBrush
        With sprite
            .Size = New Vector2(hostElement.ActualWidth, hostElement.ActualHeight)
            ElementCompositionPreview.SetElementChildVisual(hostElement, sprite)
            .Brush = compositor.CreateHostBackdropBrush
        End With
        Return sprite
    End Function

    Public Shared Sub SetTitleBarColor(themeColor As Color)
        Dim titleBar = CoreApplication.GetCurrentView().TitleBar
        titleBar.ExtendViewIntoTitleBar = True
        With ApplicationView.GetForCurrentView.TitleBar
            .BackgroundColor = themeColor
            .ButtonBackgroundColor = Colors.Transparent
            .InactiveBackgroundColor = Colors.Transparent
        End With
    End Sub

End Class
