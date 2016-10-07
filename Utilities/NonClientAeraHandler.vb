''' <summary>
''' 让一个控件的行为变得像窗体, 能够拖动和改变大小
''' </summary>
Public Class NonClientAeraHandler

    Dim curPos As Point
    Dim dragging As Boolean
    Dim captured As Boolean
    Dim nonClientAction As ResizeDirection

    WithEvents AttachedElement As FrameworkElement
    Public Property ResizeThickness As New Thickness(10)
    Public Property AngleWidth As Integer = 20
    Public Property Resizeable As Boolean = False

    Sub New(attachedElement As FrameworkElement, resizeThickness As Thickness, angleWidth As Integer, resizeable As Boolean)
        MyClass.New(attachedElement)
        Me.ResizeThickness = resizeThickness
        Me.AngleWidth = angleWidth
        Me.Resizeable = resizeable
    End Sub

    Public Sub New(attachedElement As FrameworkElement)
        Me.AttachedElement = attachedElement
    End Sub

    Protected Overridable Function FilterAction(pt As Point) As ResizeDirection
        Const Top = 0
        Const Left = 0
        With AttachedElement
            If pt.Y - Top <= AngleWidth AndAlso pt.X - Left <= AngleWidth Then
                Return ResizeDirection.Left Or ResizeDirection.Top
            ElseIf .ActualHeight + Top - pt.Y <= AngleWidth AndAlso pt.X - Left <= AngleWidth Then
                Return ResizeDirection.Left Or ResizeDirection.Bottom
            ElseIf pt.Y - Top <= AngleWidth AndAlso .ActualWidth + Left - pt.X <= AngleWidth Then
                Return ResizeDirection.Right Or ResizeDirection.Top
            ElseIf .ActualWidth + Left - pt.X <= AngleWidth AndAlso .ActualHeight + Top - pt.Y <= AngleWidth Then
                Return ResizeDirection.Right Or ResizeDirection.Bottom
            ElseIf pt.X - Left <= ResizeThickness.Left Then
                Return ResizeDirection.Left
            ElseIf .ActualWidth + Left - pt.X <= ResizeThickness.Right Then
                Return ResizeDirection.Right
            ElseIf pt.Y - Top <= ResizeThickness.Top Then
                Return ResizeDirection.Top
            ElseIf .ActualHeight + Top - pt.Y <= ResizeThickness.Bottom Then
                Return ResizeDirection.Bottom
            Else
                Return ResizeDirection.Truncate
            End If
        End With
    End Function

    Public Sub OnMoved(pt As Point)
        If captured Then
            Dim marg = AttachedElement.Margin
            Dim ofsx = pt.X - curPos.X
            Dim ofsy = pt.Y - curPos.Y
            If nonClientAction <> ResizeDirection.Truncate Then
                If nonClientAction.HasFlag(ResizeDirection.Left) Then marg.Left += ofsx
                If nonClientAction.HasFlag(ResizeDirection.Right) Then marg.Right -= ofsx
                If nonClientAction.HasFlag(ResizeDirection.Top) Then marg.Top += ofsy
                If nonClientAction.HasFlag(ResizeDirection.Bottom) Then marg.Bottom -= ofsy
            ElseIf dragging Then
                marg.Left += ofsx
                marg.Right -= ofsx
                marg.Top += ofsy
                marg.Bottom -= ofsy
            End If
            AttachedElement.Margin = marg
        End If
    End Sub

    Protected Overridable Sub OnReleased(ptr As Pointer)
        If captured Then
            nonClientAction = ResizeDirection.None
            AttachedElement.ReleasePointerCapture(ptr)
            captured = False
            dragging = False
        End If
    End Sub

    Protected Overridable Sub OnPressed(ptr As Pointer)
        nonClientAction = If(Resizeable, FilterAction(curPos), ResizeDirection.Truncate)
        AttachedElement.CapturePointer(ptr)
        captured = True
    End Sub

    Public Sub DragMove()
        dragging = True
    End Sub

    Private Sub AttachedElement_PointerPressed(sender As Object, e As PointerRoutedEventArgs) Handles AttachedElement.PointerPressed
        curPos = e.GetCurrentPoint(AttachedElement).Position
        OnPressed(e.Pointer)
    End Sub

    Private Sub AttachedElement_PointerReleased(sender As Object, e As PointerRoutedEventArgs) Handles AttachedElement.PointerReleased
        OnReleased(e.Pointer)
    End Sub

    Private Sub AttachedElement_PointerMoved(sender As Object, e As PointerRoutedEventArgs) Handles AttachedElement.PointerMoved
        OnMoved(e.GetCurrentPoint(AttachedElement).Position)
    End Sub
End Class