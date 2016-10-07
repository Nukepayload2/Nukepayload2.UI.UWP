' The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

Imports System.Numerics
Imports Microsoft.Graphics.Canvas.Brushes
Imports Microsoft.Graphics.Canvas.UI
Imports Microsoft.Graphics.Canvas.UI.Xaml
Imports Windows.UI

Public NotInheritable Class ColorPickerControl
    Inherits UserControl
    Public Event PickedColorChanged As RoutedEventHandler
    Public Property PickedColor As Color
        Get
            Return GetValue(PickedColorProperty)
        End Get
        Set
            SetValue(PickedColorProperty, Value)
            RaiseEvent PickedColorChanged(Me, New RoutedEventArgs())
        End Set
    End Property
    Public Shared ReadOnly PickedColorProperty As DependencyProperty =
                           DependencyProperty.Register(NameOf(PickedColor),
                           GetType(Color), GetType(ColorPickerControl),
                           New PropertyMetadata(Nothing))

    Private Sub ColorPickerControl_Unloaded(sender As Object, e As RoutedEventArgs) Handles Me.Unloaded
        CtlColorPane.RemoveFromVisualTree()
        CtlColorPane = Nothing
    End Sub
    Dim gradientBrush As CanvasLinearGradientBrush
    Dim opacityMask As CanvasLinearGradientBrush
    Private Sub CtlColorPane_CreateResources(sender As CanvasControl, args As CanvasCreateResourcesEventArgs) Handles CtlColorPane.CreateResources
        gradientBrush = New CanvasLinearGradientBrush(sender, {
                            New CanvasGradientStop() With {.Color = Colors.Red, .Position = 0.0F},
                            New CanvasGradientStop() With {.Color = Colors.Yellow, .Position = 0.167F},
                            New CanvasGradientStop() With {.Color = Colors.Lime, .Position = 0.333F},
                            New CanvasGradientStop() With {.Color = Colors.Cyan, .Position = 0.5F},
                            New CanvasGradientStop() With {.Color = Colors.Blue, .Position = 0.667F},
                            New CanvasGradientStop() With {.Color = Colors.Magenta, .Position = 0.833F},
                            New CanvasGradientStop() With {.Color = Colors.Red, .Position = 1.0F}
                            }) With {
                                .EndPoint = New Vector2(ActualWidth, 0),
                                .StartPoint = New Vector2(0, 0)}
        opacityMask = New CanvasLinearGradientBrush(sender, {
                            New CanvasGradientStop() With {.Color = Colors.Black, .Position = 0.0F},
                            New CanvasGradientStop() With {.Color = Colors.Transparent, .Position = 1.0F}
                            }) With {
                                .EndPoint = New Vector2(0, ActualHeight),
                                .StartPoint = New Vector2(0, 0)}

    End Sub

    Private Sub CtlColorPane_Draw(sender As CanvasControl, args As CanvasDrawEventArgs) Handles CtlColorPane.Draw
        args.DrawingSession.FillRectangle(New Rect(0, 0, ActualWidth, ActualHeight), gradientBrush, opacityMask)
    End Sub

    Private Async Sub CtlColorPane_PointerPressed(sender As Object, e As PointerRoutedEventArgs) Handles CtlColorPane.PointerPressed
        Dim pos = e.GetCurrentPoint(CtlColorPane).Position
        PickedColor = Await CtlColorPane.PickPremultipledColor(pos.X, pos.Y)
    End Sub
End Class