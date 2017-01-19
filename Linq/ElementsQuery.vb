Namespace Linq

    Public Module ElementsQuery
        <Extension>
        Public Function GetElementsByNames(Of T As FrameworkElement)(parent As FrameworkElement, ParamArray names As String()) As IEnumerable(Of T)
            Return (From n In names Select parent.FindName(n)).OfType(Of T)
        End Function
    End Module
End Namespace
