' The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

Imports System.Numerics

Public NotInheritable Class Matrix4x4Editor
    Inherits UserControl
    Public Property Value As Matrix4x4
        Get
            Return GetValue(ValueProperty)
        End Get
        Set
            SetValue(ValueProperty, Value)
        End Set
    End Property
    Public Shared ReadOnly ValueProperty As DependencyProperty =
                           DependencyProperty.Register(NameOf(Value),
                           GetType(Matrix4x4), GetType(Matrix4x4Editor),
                           New PropertyMetadata(New Matrix4x4,
                                                Sub(sender, e)
                                                    Dim editor = DirectCast(sender, Matrix4x4Editor).TblMatrix
                                                    Dim mat = DirectCast(e.NewValue, Matrix4x4)
                                                    editor.Text = $"[{mat.M11},{mat.M12},{mat.M13},{mat.M14};{mat.M21},{mat.M22},{mat.M23},{mat.M24};{mat.M31},{mat.M32},{mat.M33},{mat.M34};{mat.M41},{mat.M42},{mat.M43},{mat.M44}]"
                                                End Sub))

    Dim _isCanceling As Boolean
    Private Sub Flyout_Closing(sender As FlyoutBase, args As FlyoutBaseClosingEventArgs)
        TblError.Text = ""
        Try
            Value = New Matrix4x4(TxtM11.Text, TxtM12.Text, TxtM13.Text, TxtM14.Text,
                                  TxtM21.Text, TxtM22.Text, TxtM23.Text, TxtM24.Text,
                                  TxtM31.Text, TxtM32.Text, TxtM33.Text, TxtM34.Text,
                                  TxtM41.Text, TxtM42.Text, TxtM43.Text, TxtM44.Text)
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
        TblError.Text = ""
        TxtM11.Text = Value.M11
        TxtM12.Text = Value.M12
        TxtM13.Text = Value.M13
        TxtM14.Text = Value.M14
        TxtM21.Text = Value.M21
        TxtM22.Text = Value.M22
        TxtM23.Text = Value.M23
        TxtM24.Text = Value.M24
        TxtM31.Text = Value.M31
        TxtM32.Text = Value.M32
        TxtM33.Text = Value.M33
        TxtM34.Text = Value.M34
        TxtM41.Text = Value.M41
        TxtM42.Text = Value.M42
        TxtM43.Text = Value.M43
        TxtM44.Text = Value.M44
        _isCanceling = False
    End Sub

    Private Sub BtnSetIdentify_Click(sender As Object, e As RoutedEventArgs)
        Value = Matrix4x4.Identity
    End Sub

    Private Sub BtnSetZero_Click(sender As Object, e As RoutedEventArgs)
        Value = New Matrix4x4
    End Sub
End Class
