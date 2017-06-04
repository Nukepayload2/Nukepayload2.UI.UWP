Imports System.Numerics
Imports Windows.UI
Imports Windows.UI.Composition
Imports Windows.UI.Xaml.Hosting

Public NotInheritable Class PointerPointLight
    Inherits XamlLight

    ' Register an attached property that enables apps to set a UIElement Or Brush as a target for this light type in markup.
    Public Shared ReadOnly IsTargetProperty As DependencyProperty = DependencyProperty.RegisterAttached(
            "IsTarget",
            GetType(Boolean),
            GetType(PointerPointLight),
            New PropertyMetadata(Nothing, New PropertyChangedCallback(AddressOf OnIsTargetChanged)
            )
        )

    Public Shared Sub SetIsTarget(target As DependencyObject, value As Boolean)
        target.SetValue(IsTargetProperty, value)
    End Sub

    Public Shared Function GetIsTarget(target As DependencyObject) As Boolean
        Return target.GetValue(IsTargetProperty)
    End Function

    Private Shared Sub SetLightProperty(s As DependencyObject, e As DependencyPropertyChangedEventArgs, setCallback As Action(Of PointLight, Object))
        Dim newValue = e.NewValue
        Dim light As PointerPointLight = s
        Dim compLight = TryCast(light.CompositionLight, PointLight)
        If compLight IsNot Nothing Then
            setCallback(compLight, newValue)
        End If
    End Sub

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
                           GetType(Color), GetType(PointerPointLight),
                           New PropertyMetadata(Colors.White,
                                                Sub(s, e) SetLightProperty(s, e, Sub(compLight, newValue) compLight.Color = newValue)))

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
                           GetType(Vector3), GetType(PointerPointLight),
                           New PropertyMetadata(New Vector3(30, 30, 50),
                                                Sub(s, e) SetLightProperty(s, e, Sub(compLight, newValue) compLight.Offset = newValue)))

    Public Property ConstantAttenuation As Single
        Get
            Return GetValue(ConstantAttenuationProperty)
        End Get
        Set
            SetValue(ConstantAttenuationProperty, Value)
        End Set
    End Property
    Public Shared ReadOnly ConstantAttenuationProperty As DependencyProperty =
                           DependencyProperty.Register(NameOf(ConstantAttenuation),
                           GetType(Single), GetType(PointerPointLight),
                           New PropertyMetadata(1.0F,
                                                Sub(s, e) SetLightProperty(s, e, Sub(compLight, newValue) compLight.ConstantAttenuation = newValue)))

    Public Property QuadraticAttenuation As Single
        Get
            Return GetValue(QuadraticAttenuationProperty)
        End Get
        Set
            SetValue(QuadraticAttenuationProperty, Value)
        End Set
    End Property
    Public Shared ReadOnly QuadraticAttenuationProperty As DependencyProperty =
                           DependencyProperty.Register(NameOf(QuadraticAttenuation),
                           GetType(Single), GetType(PointerPointLight),
                           New PropertyMetadata(1.0F,
                                                Sub(s, e) SetLightProperty(s, e, Sub(compLight, newValue) compLight.QuadraticAttenuation = newValue)))

    Public Property LinearAttenuation As Single
        Get
            Return GetValue(LinearAttenuationProperty)
        End Get
        Set
            SetValue(LinearAttenuationProperty, Value)
        End Set
    End Property
    Public Shared ReadOnly LinearAttenuationProperty As DependencyProperty =
                           DependencyProperty.Register(NameOf(LinearAttenuation),
                           GetType(Single), GetType(PointerPointLight),
                           New PropertyMetadata(0F,
                                                Sub(s, e) SetLightProperty(s, e, Sub(compLight, newValue) compLight.LinearAttenuation = newValue)))

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
                           GetType(Boolean), GetType(PointerPointLight),
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
                           GetType(Single), GetType(PointerPointLight),
                           New PropertyMetadata(48))

    ' Handle attached property changed to automatically target And untarget UIElements And Brushes.
    Public Shared Sub OnIsTargetChanged(obj As DependencyObject, e As DependencyPropertyChangedEventArgs)
        Dim isAdding = DirectCast(e.NewValue, Boolean)

        If isAdding Then
            If TypeOf obj Is UIElement Then
                AddTargetElement(GetIdStatic(), TryCast(obj, UIElement))
            ElseIf TypeOf obj Is Brush Then
                AddTargetBrush(GetIdStatic(), TryCast(obj, Brush))
            End If
        Else
            If TypeOf obj Is UIElement Then
                RemoveTargetElement(GetIdStatic(), TryCast(obj, UIElement))
            ElseIf TypeOf obj Is Brush Then
                RemoveTargetBrush(GetIdStatic(), TryCast(obj, Brush))
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
            Dim pointLight = compositor.CreatePointLight
            With pointLight
                .Color = Color
                .Targets.Add(visual)
            End With

            CompositionLight = pointLight
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
        ElementRoot = Nothing
    End Sub

    Protected Overrides Function GetId() As String
        Return GetIdStatic()
    End Function

    Private Shared Function GetIdStatic() As String
        ' This specifies the unique name of the light. In most cases you should use the type's FullName.
        Static FullName$ = GetType(PointerPointLight).FullName
        Return FullName
    End Function

    Private Sub ConnectedElement_PointerMoved(sender As Object, e As PointerRoutedEventArgs) Handles ElementRoot.PointerMoved
        If TrackPointer Then
            Dim pos = e.GetCurrentPoint(ConnectedElement).Position
            Offset = New Vector3(pos.X, pos.Y, DefaultDistance)
        End If
    End Sub
End Class