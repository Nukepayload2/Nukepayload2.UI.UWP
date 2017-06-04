Imports System.Numerics
Imports Windows.UI
Imports Windows.UI.Composition
Imports Windows.UI.Xaml.Hosting

Public Class PointerSpotLight
    Inherits XamlLight

    ' Register an attached property that enables apps to set a UIElement Or Brush as a target for this light type in markup.
    Public Shared ReadOnly IsTargetProperty As DependencyProperty = DependencyProperty.RegisterAttached(
            "IsTarget",
            GetType(Boolean),
            GetType(PointerSpotLight),
            New PropertyMetadata(Nothing, New PropertyChangedCallback(AddressOf OnIsTargetChanged)
            )
        )

    Public Shared Sub SetIsTarget(target As DependencyObject, value As Boolean)
        target.SetValue(IsTargetProperty, value)
    End Sub

    Public Shared Function GetIsTarget(target As DependencyObject) As Boolean
        Return DirectCast(target.GetValue(IsTargetProperty), Boolean)
    End Function

    Private Shared Sub SetLightProperty(s As DependencyObject, e As DependencyPropertyChangedEventArgs, setCallback As Action(Of SpotLight, Object))
        Dim newValue = e.NewValue
        Dim light As PointerSpotLight = s
        Dim compLight = TryCast(light.CompositionLight, SpotLight)
        If compLight IsNot Nothing Then
            setCallback(compLight, newValue)
        End If
    End Sub

    Public Property Offset As Vector3
        Get
            Return GetValue(OffsetProperty)
        End Get
        Set
            SetValue(OffsetProperty, Value)
        End Set
    End Property
    Public Shared ReadOnly OffsetProperty As DependencyProperty =
                           DependencyProperty.Register(NameOf(Offset),
                           GetType(Vector3), GetType(PointerSpotLight),
                           New PropertyMetadata(New Vector3(900, 900, 10),
                                                Sub(s, e) SetLightProperty(s, e, Sub(compLight, newValue) compLight.Offset = newValue)))

    Public Property OuterConeAngleInDegrees As Double
        Get
            Return GetValue(OuterConeAngleInDegreesProperty)
        End Get
        Set
            SetValue(OuterConeAngleInDegreesProperty, Value)
        End Set
    End Property
    Public Shared ReadOnly OuterConeAngleInDegreesProperty As DependencyProperty =
                           DependencyProperty.Register(NameOf(OuterConeAngleInDegrees),
                           GetType(Double), GetType(PointerSpotLight),
                           New PropertyMetadata(42.0,
                                                Sub(s, e) SetLightProperty(s, e, Sub(compLight, newValue) compLight.OuterConeAngleInDegrees = CSng(newValue))))

    Public Property InnerConeAngleInDegrees As Single
        Get
            Return GetValue(InnerConeAngleInDegreesProperty)
        End Get
        Set
            SetValue(InnerConeAngleInDegreesProperty, Value)
        End Set
    End Property
    Public Shared ReadOnly InnerConeAngleInDegreesProperty As DependencyProperty =
                           DependencyProperty.Register(NameOf(InnerConeAngleInDegrees),
                           GetType(Single), GetType(PointerSpotLight),
                           New PropertyMetadata(4.0F,
                                                Sub(s, e) SetLightProperty(s, e, Sub(compLight, newValue) compLight.InnerConeAngleInDegrees = newValue)))

    Public Property InnerConeColor As Color
        Get
            Return GetValue(InnerConeColorProperty)
        End Get
        Set
            SetValue(InnerConeColorProperty, Value)
        End Set
    End Property
    Public Shared ReadOnly InnerConeColorProperty As DependencyProperty =
                           DependencyProperty.Register(NameOf(InnerConeColor),
                           GetType(Color), GetType(PointerSpotLight),
                           New PropertyMetadata(Colors.WhiteSmoke,
                                                Sub(s, e) SetLightProperty(s, e, Sub(compLight, newValue) compLight.InnerConeColor = newValue)))

    Public Property OuterConeColor As Color
        Get
            Return GetValue(OuterConeColorProperty)
        End Get
        Set
            SetValue(OuterConeColorProperty, Value)
        End Set
    End Property
    Public Shared ReadOnly OuterConeColorProperty As DependencyProperty =
                           DependencyProperty.Register(NameOf(OuterConeColor),
                           GetType(Color), GetType(PointerSpotLight),
                           New PropertyMetadata(Colors.White,
                                                Sub(s, e) SetLightProperty(s, e, Sub(compLight, newValue) compLight.OuterConeColor = newValue)))

    Public Property TrackPointer As Boolean
        Get
            Return GetValue(TrackPointerProperty)
        End Get
        Set
            SetValue(TrackPointerProperty, Value)
        End Set
    End Property
    Public Shared ReadOnly TrackPointerProperty As DependencyProperty =
                           DependencyProperty.Register(NameOf(TrackPointer),
                           GetType(Boolean), GetType(PointerSpotLight),
                           New PropertyMetadata(True))

    Public Property DefaultDistance As Single
        Get
            Return GetValue(DefaultDistanceProperty)
        End Get
        Set
            SetValue(DefaultDistanceProperty, Value)
        End Set
    End Property
    Public Shared ReadOnly DefaultDistanceProperty As DependencyProperty =
                           DependencyProperty.Register(NameOf(DefaultDistance),
                           GetType(Single), GetType(PointerSpotLight),
                           New PropertyMetadata(100))


    ' Handle attached property changed to automatically target And untarget UIElements And Brushes.
    Public Shared Sub OnIsTargetChanged(obj As DependencyObject, e As DependencyPropertyChangedEventArgs)
        Dim isAdding = DirectCast(e.NewValue, Boolean)

        If isAdding Then
            If TypeOf obj Is UIElement Then
                XamlLight.AddTargetElement(GetIdStatic(), TryCast(obj, UIElement))
            ElseIf TypeOf obj Is Brush Then
                XamlLight.AddTargetBrush(GetIdStatic(), TryCast(obj, Brush))
            End If
        Else
            If TypeOf obj Is UIElement Then
                XamlLight.RemoveTargetElement(GetIdStatic(), TryCast(obj, UIElement))
            ElseIf TypeOf obj Is Brush Then
                XamlLight.RemoveTargetBrush(GetIdStatic(), TryCast(obj, Brush))
            End If
        End If
    End Sub

    WithEvents ConnectedElement As UIElement
    WithEvents ElementRoot As Frame

    Protected Overrides Sub OnConnected(newElement As UIElement)
        If CompositionLight Is Nothing Then
            ' OnConnected Is called when the first target UIElement Is shown on the screen. This enables delaying composition object creation until it's actually necessary.
            Dim visual = ElementCompositionPreview.GetElementVisual(newElement)
            Dim compositor = visual.Compositor
            Dim spotLight = compositor.CreateSpotLight()
            With spotLight
                .InnerConeColor = InnerConeColor
                .OuterConeColor = OuterConeColor
                .InnerConeAngleInDegrees = InnerConeAngleInDegrees
                .OuterConeAngleInDegrees = OuterConeAngleInDegrees
                .Targets.Add(visual)
            End With
            CompositionLight = spotLight
        End If
        ConnectedElement = newElement
        Dim root = newElement
        Do Until TypeOf root Is Frame
            root = VisualTreeHelper.GetParent(root)
        Loop
        ElementRoot = root
    End Sub

    Protected Overrides Sub OnDisconnected(oldElement As UIElement)
        ' OnDisconnected Is called when there are no more target UIElements on the screen. The CompositionLight should be disposed when no longer required.
        If CompositionLight IsNot Nothing Then
            'CompositionLight.Dispose()
            CompositionLight = Nothing
        End If
        ConnectedElement = Nothing
    End Sub

    Protected Overrides Function GetId() As String
        Return GetIdStatic()
    End Function

    Private Shared Function GetIdStatic() As String
        ' This specifies the unique name of the light. In most cases you should use the type's FullName.
        Return GetType(PointerSpotLight).FullName
    End Function

    Private Sub ConnectedElement_PointerMoved(sender As Object, e As PointerRoutedEventArgs) Handles ElementRoot.PointerMoved
        If TrackPointer Then
            Dim pos = e.GetCurrentPoint(ConnectedElement).Position
            Offset = New Vector3(pos.X, pos.Y, DefaultDistance)
        End If
    End Sub
End Class
