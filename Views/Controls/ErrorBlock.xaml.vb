' The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

Public NotInheritable Class ErrorBlock
    Inherits UserControl

    Public Property ErrorMessage As String
        Get
            Return GetValue(ErrorMessageProperty)
        End Get
        Set
            SetValue(ErrorMessageProperty, Value)
        End Set
    End Property
    Public Shared ReadOnly ErrorMessageProperty As DependencyProperty =
                           DependencyProperty.Register(NameOf(ErrorMessage),
                           GetType(String), GetType(ErrorBlock),
                           New PropertyMetadata(Nothing,
                               Sub(s, e)
                                   Dim this = DirectCast(s, ErrorBlock)
                                   If String.IsNullOrEmpty(e.NewValue) Then
                                       this.Show(e.NewValue)
                                   Else
                                       this.Hide()
                                   End If
                               End Sub))

    Public Sub Show(errMessage$)
        TblErrorText.Text = errMessage
        Visibility = Visibility.Visible
    End Sub

    Public Sub Hide()
        Visibility = Visibility.Collapsed
    End Sub

    Private Sub BtnOk_Click(sender As Object, e As RoutedEventArgs)
        Hide()
    End Sub
End Class