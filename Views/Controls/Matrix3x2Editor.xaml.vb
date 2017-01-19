' The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236
Imports System.Numerics

Public NotInheritable Class Matrix3x2Editor
    Inherits UserControl

    Public Property Value As Matrix3x2
        Get
            Return GetValue(ValueProperty)
        End Get
        Set
            SetValue(ValueProperty, Value)
        End Set
    End Property
    Public Shared ReadOnly ValueProperty As DependencyProperty =
                           DependencyProperty.Register(NameOf(Value),
                           GetType(Matrix3x2), GetType(Matrix3x2Editor),
                           New PropertyMetadata(New Matrix3x2,
                                                Sub(sender, e)
                                                    Dim editor = DirectCast(sender, Matrix3x2Editor)
                                                    Dim valuePreview = editor.TblMatrix
                                                    Dim mat = DirectCast(e.NewValue, Matrix3x2)
                                                    valuePreview.Text = $"[{mat.M11},{mat.M12};{mat.M21},{mat.M22};{mat.M31},{mat.M32}]"
                                                    editor.ReloadValue()
                                                End Sub))

    Dim _isCanceling As Boolean
    Private Sub Flyout_Closing(sender As FlyoutBase, args As FlyoutBaseClosingEventArgs)
        TblError.Text = ""
        Try
            Value = New Matrix3x2(TxtM11.Text, TxtM12.Text,
                                  TxtM21.Text, TxtM22.Text,
                                  TxtM31.Text, TxtM32.Text)
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
        TxtM11.Text = Value.M11
        TxtM12.Text = Value.M12
        TxtM21.Text = Value.M21
        TxtM22.Text = Value.M22
        TxtM31.Text = Value.M31
        TxtM32.Text = Value.M32
        _isCanceling = False
    End Sub

    Private Sub BtnSetZero_Click(sender As Object, e As RoutedEventArgs)
        Value = New Matrix3x2
    End Sub
End Class