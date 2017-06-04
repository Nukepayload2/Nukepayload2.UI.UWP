Imports Windows.UI
Imports Windows.UI.Composition
Imports Windows.UI.Xaml.Hosting

Public Class HostBlurBackground
    Inherits DependencyObject

    Shared s_hostBackdrop As Panel
    Shared s_sprites As New Dictionary(Of Panel, SpriteVisual)

    Public Shared Function GetIsEnabled(element As DependencyObject) As Boolean
        If element Is Nothing Then
            Throw New ArgumentNullException("element")
        End If
        Return element.GetValue(IsEnabledProperty)
    End Function

    Public Shared Sub SetIsEnabled(element As DependencyObject, value As Boolean)
        If element Is Nothing Then
            Throw New ArgumentNullException("element")
        End If
        element.SetValue(IsEnabledProperty, value)
    End Sub

    Public Shared ReadOnly IsEnabledProperty As _
                           DependencyProperty = DependencyProperty.RegisterAttached("IsEnabled",
                           GetType(Boolean), GetType(Panel),
                           New PropertyMetadata(False,
                                                Sub(s, e)
                                                    s_hostBackdrop = s
                                                    If Not s_sprites.ContainsKey(s_hostBackdrop) Then
                                                        s_sprites.Add(s_hostBackdrop, HostBackdropBlurHelper.CreateBackdropSpriteVisual(s_hostBackdrop))
                                                        AddHandler s_hostBackdrop.SizeChanged,
                                                            Sub(sender, evt)
                                                                s_hostBackdrop = sender
                                                                Dim sprite As SpriteVisual
                                                                If s_sprites.TryGetValue(s_hostBackdrop, sprite) AndAlso sprite IsNot Nothing Then
                                                                    sprite.Size = New Numerics.Vector2(s_hostBackdrop.ActualWidth, s_hostBackdrop.ActualHeight)
                                                                End If
                                                            End Sub
                                                    End If
                                                    If Not e.NewValue Then
                                                        ElementCompositionPreview.SetElementChildVisual(s_hostBackdrop, Nothing)
                                                    End If
                                                End Sub))

    Public Shared Function GetThemeColor(ByVal element As DependencyObject) As Color
        If element Is Nothing Then
            Throw New ArgumentNullException("element")
        End If

        Return element.GetValue(ThemeColorProperty)
    End Function

    Public Shared Sub SetThemeColor(ByVal element As DependencyObject, ByVal value As Color)
        If element Is Nothing Then
            Throw New ArgumentNullException("element")
        End If

        element.SetValue(ThemeColorProperty, value)
    End Sub

    Public Shared ReadOnly ThemeColorProperty As _
                           DependencyProperty = DependencyProperty.RegisterAttached("ThemeColor",
                           GetType(Color), GetType(Panel),
                           New PropertyMetadata(Nothing,
                                                Sub(s, e)
                                                    Dim color = If(e.NewValue, Colors.Transparent)
                                                    HostBackdropBlurHelper.SetTitleBarColor(color)
                                                    DirectCast(s, Panel).Background = New SolidColorBrush(color)
                                                End Sub))

End Class
