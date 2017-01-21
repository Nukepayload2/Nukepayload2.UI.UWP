' The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

Imports System.Numerics

Public NotInheritable Class Vector2Editor
    Inherits UserControl
    Public Property Value As Vector2
        Get
            Return GetValue(ValueProperty)
        End Get
        Set
            SetValue(ValueProperty, Value)
        End Set
    End Property
    Public Shared ReadOnly ValueProperty As DependencyProperty =
                           DependencyProperty.Register(NameOf(Value),
                           GetType(Vector2), GetType(Vector2Editor),
                           New PropertyMetadata(New Vector2,
                                                Sub(sender, e)
                                                    Dim editor = DirectCast(sender, Vector2Editor)
                                                    Dim valuePreview = editor.TblMatrix
                                                    Dim vec = DirectCast(e.NewValue, Vector2)
                                                    valuePreview.Text = $"<{vec.X},{vec.Y}>"
                                                    editor.ReloadValue()
                                                End Sub))

    Dim _isCanceling As Boolean
    Private Sub Flyout_Closing(sender As FlyoutBase, args As FlyoutBaseClosingEventArgs)
        TblError.Text = ""
        Try
            Value = New Vector2(TxtX.Text, TxtY.Text)
            _isCanceling = False
        Catch ex As Exception
            If _isCanceling Then
                _isCanceling = False
            Else
                TblError.Text = ex.Message + vbCrLf + "再次尝试关闭将取消更改"
                _isCanceling = True
                args.Cancel = True
            End If
        End Try
    End Sub

    Private Sub Flyout_Opening(sender As Object, e As Object)
        ReloadValue()
    End Sub

    Private Sub ReloadValue()
        TblError.Text = ""
        TxtX.Text = Value.X
        TxtY.Text = Value.Y
        _isCanceling = False
    End Sub

    Private Sub BtnSetZero_Click(sender As Object, e As RoutedEventArgs)
        Value = New Vector2
    End Sub
End Class
