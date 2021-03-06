﻿' The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

Imports System.Numerics

Public NotInheritable Class Vector3Editor
    Inherits UserControl
    Public Property Value As Vector3
        Get
            Return GetValue(ValueProperty)
        End Get
        Set
            SetValue(ValueProperty, Value)
        End Set
    End Property
    Public Shared ReadOnly ValueProperty As DependencyProperty =
                           DependencyProperty.Register(NameOf(Value),
                           GetType(Vector3), GetType(Vector3Editor),
                           New PropertyMetadata(New Vector3,
                                                Sub(sender, e)
                                                    Dim editor = DirectCast(sender, Vector3Editor)
                                                    Dim valuePreview = editor.TblMatrix
                                                    Dim vec = DirectCast(e.NewValue, Vector3)
                                                    valuePreview.Text = vec.ToString
                                                    editor.ReloadValue()
                                                End Sub))

    Dim _isCanceling As Boolean
    Private Sub Flyout_Closing(sender As FlyoutBase, args As FlyoutBaseClosingEventArgs)
        TblError.Text = ""
        Try
            Value = New Vector3(TxtX.Text, TxtY.Text,
                                TxtZ.Text)
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
        TxtZ.Text = Value.Z
        _isCanceling = False
    End Sub

    Private Sub BtnSetZero_Click(sender As Object, e As RoutedEventArgs)
        Value = New Vector3
    End Sub
End Class
