Imports Windows.UI
Imports Windows.UI.Xaml.Shapes
Public Class Hexagon
    Inherits ContentControl

    Dim polygon As New Polygon With {
        .Points = New PointCollection() From {
            New Point(100, 0), New Point(300, 0), New Point(400, 200),
            New Point(300, 400), New Point(100, 400), New Point(0, 200)
        },
        .Name = "polygon"
    }

    Public Property Fill As Brush
        Get
            Return GetValue(FillProperty)
        End Get
        Set
            SetValue(FillProperty, Value)
        End Set
    End Property

    Public Shared ReadOnly FillProperty As DependencyProperty =
                           DependencyProperty.Register(NameOf(Fill),
                           GetType(Brush), GetType(Hexagon),
                           New PropertyMetadata(Nothing))

    Public ReadOnly Property GeometryTransform As Transform
        Get
            Return polygon.GeometryTransform
        End Get
    End Property

    Public Property Stretch As Stretch
        Get
            Return GetValue(StretchProperty)
        End Get
        Set
            SetValue(StretchProperty, Value)
        End Set
    End Property
    Public Shared ReadOnly StretchProperty As DependencyProperty =
                           DependencyProperty.Register(NameOf(Stretch),
                           GetType(Stretch), GetType(Hexagon),
                           New PropertyMetadata(Nothing))

    Public Property Stroke As Brush
        Get
            Return GetValue(StrokeProperty)
        End Get
        Set
            SetValue(StrokeProperty, Value)
        End Set
    End Property
    Public Shared ReadOnly StrokeProperty As DependencyProperty =
                           DependencyProperty.Register(NameOf(Stroke),
                           GetType(Brush), GetType(Hexagon),
                           New PropertyMetadata(Nothing))

    Public Property StrokeDashArray As DoubleCollection
        Get
            Return GetValue(StrokeDashArrayProperty)
        End Get
        Set
            SetValue(StrokeDashArrayProperty, Value)
        End Set
    End Property
    Public Shared ReadOnly StrokeDashArrayProperty As DependencyProperty =
                           DependencyProperty.Register(NameOf(StrokeDashArray),
                           GetType(DoubleCollection), GetType(Hexagon),
                           New PropertyMetadata(Nothing))

    Public Property StrokeDashCap As PenLineCap
        Get
            Return GetValue(StrokeDashCapProperty)
        End Get
        Set
            SetValue(StrokeDashCapProperty, Value)
        End Set
    End Property
    Public Shared ReadOnly StrokeDashCapProperty As DependencyProperty =
                           DependencyProperty.Register(NameOf(StrokeDashCap),
                           GetType(PenLineCap), GetType(Hexagon),
                           New PropertyMetadata(Nothing))

    Public Property StrokeDashOffset As Double
        Get
            Return GetValue(StrokeDashOffsetProperty)
        End Get
        Set
            SetValue(StrokeDashOffsetProperty, Value)
        End Set
    End Property
    Public Shared ReadOnly StrokeDashOffsetProperty As DependencyProperty =
                           DependencyProperty.Register(NameOf(StrokeDashOffset),
                           GetType(Double), GetType(Hexagon),
                           New PropertyMetadata(Nothing))

    Public Property StrokeEndLineCap As PenLineCap
        Get
            Return GetValue(StrokeEndLineCapProperty)
        End Get
        Set
            SetValue(StrokeEndLineCapProperty, Value)
        End Set
    End Property
    Public Shared ReadOnly StrokeEndLineCapProperty As DependencyProperty =
                           DependencyProperty.Register(NameOf(StrokeEndLineCap),
                           GetType(PenLineCap), GetType(Hexagon),
                           New PropertyMetadata(Nothing))

    Public Property StrokeLineJoin As PenLineJoin
        Get
            Return GetValue(StrokeLineJoinProperty)
        End Get
        Set
            SetValue(StrokeLineJoinProperty, Value)
        End Set
    End Property
    Public Shared ReadOnly StrokeLineJoinProperty As DependencyProperty =
                           DependencyProperty.Register(NameOf(StrokeLineJoin),
                           GetType(PenLineJoin), GetType(Hexagon),
                           New PropertyMetadata(Nothing))

    Public Property StrokeMiterLimit As Double
        Get
            Return GetValue(StrokeMiterLimitProperty)
        End Get
        Set
            SetValue(StrokeMiterLimitProperty, Value)
        End Set
    End Property
    Public Shared ReadOnly StrokeMiterLimitProperty As DependencyProperty =
                           DependencyProperty.Register(NameOf(StrokeMiterLimit),
                           GetType(Double), GetType(Hexagon),
                           New PropertyMetadata(Nothing))

    Public Property StrokeStartLineCap As PenLineCap
        Get
            Return GetValue(StrokeStartLineCapProperty)
        End Get
        Set
            SetValue(StrokeStartLineCapProperty, Value)
        End Set
    End Property
    Public Shared ReadOnly StrokeStartLineCapProperty As DependencyProperty =
                           DependencyProperty.Register(NameOf(StrokeStartLineCap),
                           GetType(PenLineCap), GetType(Hexagon),
                           New PropertyMetadata(Nothing))

    Public Property StrokeThickness As Double
        Get
            Return GetValue(StrokeThicknessProperty)
        End Get
        Set
            SetValue(StrokeThicknessProperty, Value)
        End Set
    End Property
    Public Shared ReadOnly StrokeThicknessProperty As DependencyProperty =
                           DependencyProperty.Register(NameOf(StrokeThickness),
                           GetType(Double), GetType(Hexagon),
                           New PropertyMetadata(Nothing))

    Sub New()
        Content = polygon
        With polygon
            .BindTwoWay(Shape.FillProperty, "Fill", Me)
            .BindTwoWay(Shape.StretchProperty, "Stretch", Me)
            .BindTwoWay(Shape.StrokeDashArrayProperty, "StrokeDashArray", Me)
            .BindTwoWay(Shape.StrokeDashCapProperty, "StrokeDashCap", Me)
            .BindTwoWay(Shape.StrokeDashOffsetProperty, "StrokeDashOffset", Me)
            .BindTwoWay(Shape.StrokeEndLineCapProperty, "StrokeEndLineCap", Me)
            .BindTwoWay(Shape.StrokeLineJoinProperty, "StrokeLineJoin", Me)
            .BindTwoWay(Shape.StrokeMiterLimitProperty, "StrokeMiterLimit", Me)
            .BindTwoWay(Shape.StrokeProperty, "Stroke", Me)
            .BindTwoWay(Shape.StrokeStartLineCapProperty, "StrokeStartLineCap", Me)
            .BindTwoWay(Shape.StrokeThicknessProperty, "StrokeThickness", Me)
        End With
    End Sub
    Private Sub Hexagon_SizeChanged(sender As Object, e As SizeChangedEventArgs) Handles Me.SizeChanged
        UpdatePolygon()
    End Sub

    Private Sub UpdatePolygon()
        Dim w = Math.Max(0, ActualWidth - polygon.StrokeThickness * 2)
        Dim h = Math.Max(0, ActualHeight - polygon.StrokeThickness * 2)
        With polygon
            .Points(0) = New Point(1 * w / 4, 0)
            .Points(1) = New Point(3 * w / 4, 0)
            .Points(2) = New Point(4 * w / 4, h / 2)
            .Points(3) = New Point(3 * w / 4, h)
            .Points(4) = New Point(1 * w / 4, h)
            .Points(5) = New Point(0, h / 2)
        End With
    End Sub
End Class