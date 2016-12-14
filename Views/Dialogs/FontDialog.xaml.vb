' “内容对话框”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上进行了说明

Public NotInheritable Class FontDialog

    Private Sub Rectangle_PointerPressed(sender As Object, e As PointerRoutedEventArgs)
        DragMove()
    End Sub

    Private Sub BtnClose_Click(sender As Object, e As RoutedEventArgs)
        Hide()
    End Sub

    Public Property FontInformation As FontInformation
        Get
            Return TryCast(DataContext, FontsViewModel)?.FontInformation
        End Get
        Set
            Dim vm = TryCast(DataContext, FontsViewModel)
            If vm IsNot Nothing Then
                vm.FontInformation = Value
            End If
        End Set
    End Property

    Private Sub BtnOk_Click(sender As Object, e As RoutedEventArgs)
        Hide()
    End Sub

    Public Overloads Async Function ShowAsync() As Task(Of FontInformation)
        Await MyBase.ShowAsync()
        Return FontInformation
    End Function
End Class
