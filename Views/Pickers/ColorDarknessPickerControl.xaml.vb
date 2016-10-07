' The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236
Imports System.Numerics
Imports Microsoft.Graphics.Canvas.Brushes
Imports Microsoft.Graphics.Canvas.UI
Imports Microsoft.Graphics.Canvas.UI.Xaml
Imports Windows.UI

Public NotInheritable Class ColorDarknessPickerControl
    Inherits UserControl
    Public Property InputColor As Color
        Get
            Return GetValue(InputColorProperty)
        End Get
        Set
            SetValue(InputColorProperty, Value)
        End Set
    End Property
    Public Shared ReadOnly InputColorProperty As DependencyProperty =
                           DependencyProperty.Register(NameOf(InputColor),
                           GetType(Color), GetType(ColorDarknessPickerControl),
                           New PropertyMetadata(Nothing))
    Public Property PickedColor As Color
        Get
            Return GetValue(PickedColorProperty)
        End Get
        Set
            SetValue(PickedColorProperty, Value)
            RaiseEvent PickedColorChanged(Me, New RoutedEventArgs)
        End Set
    End Property
    Public Shared ReadOnly PickedColorProperty As DependencyProperty =
                           DependencyProperty.Register(NameOf(PickedColor),
                           GetType(Color), GetType(ColorDarknessPickerControl),
                           New PropertyMetadata(Nothing))

    Dim gradientBrush As CanvasLinearGradientBrush
    Private Sub CtlColorPane_CreateResources(sender As CanvasControl, args As CanvasCreateResourcesEventArgs) Handles CtlColorPane.CreateResources
        CreateBrush(sender)
    End Sub

    Private Sub CreateBrush(sender As CanvasControl)
        gradientBrush = New CanvasLinearGradientBrush(sender, {
                           New CanvasGradientStop() With {.Color = Colors.Black, .Position = 0.0F},
                           New CanvasGradientStop() With {.Color = InputColor, .Position = 0.5F},
                           New CanvasGradientStop() With {.Color = Colors.White, .Position = 1.0F}
                           }) With {
                               .EndPoint = New Vector2(ActualWidth, 0),
                               .StartPoint = New Vector2(0, 0)}
    End Sub

    Public Sub Refresh()
        CtlColorPane.Invalidate()
    End Sub
    Private Sub UserControl_Unloaded(sender As Object, e As RoutedEventArgs)
        CtlColorPane.RemoveFromVisualTree()
        CtlColorPane = Nothing
    End Sub

    Private Sub CtlColorPane_Draw(sender As CanvasControl, args As CanvasDrawEventArgs) Handles CtlColorPane.Draw
        CreateBrush(sender)
        args.DrawingSession.FillRectangle(New Rect(0, 0, ActualWidth, ActualHeight), gradientBrush)
    End Sub
    Public Event PickedColorChanged As RoutedEventHandler
    Private Async Sub CtlColorPane_PointerPressed(sender As Object, e As PointerRoutedEventArgs) Handles CtlColorPane.PointerPressed
        Dim pos = e.GetCurrentPoint(CtlColorPane).Position
        PickedColor = Await CtlColorPane.PickPremultipledColor(pos.X, pos.Y)
    End Sub
End Class