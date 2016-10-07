'*********************************************************
'
' Copyright (c) Microsoft. All rights reserved.
' This code is licensed under the MIT License (MIT).
' THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, 
' INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
' FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. 
' IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, 
' DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, 
' TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH 
' THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
'
'*********************************************************

Imports Microsoft.Graphics.Canvas.Effects
Imports Windows.UI
Imports Windows.UI.Composition
Imports Windows.UI.Xaml.Hosting
Public Class BackDrop
    Inherits Control
    Private m_compositor As Compositor
    Private m_blurVisual As SpriteVisual
    Private m_blurBrush As CompositionBrush
    Private m_rootVisual As Visual

    Private m_setUpExpressions As Boolean
    Private m_noiseBrush As CompositionSurfaceBrush

    Public Sub New()
        m_rootVisual = ElementCompositionPreview.GetElementVisual(TryCast(Me, UIElement))
        Compositor = m_rootVisual.Compositor

        m_blurVisual = Compositor.CreateSpriteVisual()

        m_noiseBrush = Compositor.CreateSurfaceBrush()

        Dim brush As CompositionEffectBrush = BuildBlurBrush()
        brush.SetSourceParameter("source", m_compositor.CreateBackdropBrush())
        m_blurBrush = brush
        m_blurVisual.Brush = m_blurBrush

        BlurAmount = 9
        TintColor = Colors.Transparent
        ElementCompositionPreview.SetElementChildVisual(TryCast(Me, UIElement), m_blurVisual)

        AddHandler Me.Loading, AddressOf OnLoading
        AddHandler Me.Unloaded, AddressOf OnUnloaded
    End Sub

    Public Const BlurAmountProperty As String = NameOf(BlurAmount)
    Public Const TintColorProperty As String = NameOf(TintColor)

    Public Property BlurAmount() As Double
        Get
            Dim value As Single = 0
            m_rootVisual.Properties.TryGetScalar(BlurAmountProperty, value)
            Return value
        End Get
        Set
            If Not m_setUpExpressions Then
                m_blurBrush.Properties.InsertScalar("Blur.BlurAmount", CSng(Value))
            End If
            m_rootVisual.Properties.InsertScalar(BlurAmountProperty, CSng(Value))
        End Set
    End Property

    Public Property TintColor() As Color
        Get
            Dim value As Color
            m_rootVisual.Properties.TryGetColor("TintColor", value)
            Return value
        End Get
        Set
            If Not m_setUpExpressions Then
                m_blurBrush.Properties.InsertColor("Color.Color", Value)
            End If
            m_rootVisual.Properties.InsertColor(TintColorProperty, Value)
        End Set
    End Property

    Public Property Compositor() As Compositor
        Get
            Return m_compositor
        End Get

        Private Set
            m_compositor = Value
        End Set
    End Property

    Private Async Sub OnLoading(sender As FrameworkElement, args As Object)
        AddHandler Me.SizeChanged, AddressOf OnSizeChanged
        OnSizeChanged(Me, Nothing)

        m_noiseBrush.Surface = Await SurfaceLoader.LoadFromUri(New Uri("ms-appx:///Assets/Noise.jpg"))
        m_noiseBrush.Stretch = CompositionStretch.UniformToFill
    End Sub

    Private Sub OnUnloaded(sender As Object, e As RoutedEventArgs)
        RemoveHandler Me.SizeChanged, AddressOf OnSizeChanged
    End Sub


    Private Sub OnSizeChanged(sender As Object, e As Windows.UI.Xaml.SizeChangedEventArgs)
        If m_blurVisual IsNot Nothing Then
            m_blurVisual.Size = New System.Numerics.Vector2(CSng(Me.ActualWidth), CSng(Me.ActualHeight))
        End If
    End Sub

    Private Sub SetUpPropertySetExpressions()
        m_setUpExpressions = True

        Dim exprAnimation = Compositor.CreateExpressionAnimation()
        exprAnimation.Expression = "sourceProperties.{BlurAmountProperty}"
        exprAnimation.SetReferenceParameter("sourceProperties", m_rootVisual.Properties)

        m_blurBrush.Properties.StartAnimation("Blur.BlurAmount", exprAnimation)

        exprAnimation.Expression = "sourceProperties.{TintColorProperty}"

        m_blurBrush.Properties.StartAnimation("Color.Color", exprAnimation)
    End Sub


    Private Function BuildBlurBrush() As CompositionEffectBrush
        Dim blurEffect As New GaussianBlurEffect() With {
            .Name = "Blur",
            .BlurAmount = 0F,
            .BorderMode = EffectBorderMode.Hard,
            .Optimization = EffectOptimization.Balanced,
            .Source = New CompositionEffectSourceParameter("source")
        }

        Dim blendEffect As New BlendEffect() With {
            .Background = blurEffect,
            .Foreground = New ColorSourceEffect() With {
                .Name = "Color",
                .Color = Color.FromArgb(64, 255, 255, 255)
            },
            .Mode = BlendEffectMode.SoftLight
        }

        Dim saturationEffect As New SaturationEffect() With {
            .Source = blendEffect,
            .Saturation = 1.75F
        }

        Dim finalEffect As New BlendEffect() With {
            .Foreground = New CompositionEffectSourceParameter("NoiseImage"),
            .Background = saturationEffect,
            .Mode = BlendEffectMode.Screen
        }

        Dim factory = Compositor.CreateEffectFactory(finalEffect, {"Blur.BlurAmount", "Color.Color"})

        Dim brush As CompositionEffectBrush = factory.CreateBrush()
        brush.SetSourceParameter("NoiseImage", m_noiseBrush)
        Return brush
    End Function

    Public ReadOnly Property VisualProperties() As CompositionPropertySet
        Get
            If Not m_setUpExpressions Then
                SetUpPropertySetExpressions()
            End If
            Return m_rootVisual.Properties
        End Get
    End Property


End Class