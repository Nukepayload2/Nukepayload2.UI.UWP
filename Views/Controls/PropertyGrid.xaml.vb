' The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

Public NotInheritable Class PropertyGrid
    Inherits UserControl

    Public Property Target As Object
        Get
            Return GetValue(TargetProperty)
        End Get
        Set
            SetValue(TargetProperty, Value)
        End Set
    End Property
    Public Shared ReadOnly TargetProperty As DependencyProperty =
                           DependencyProperty.Register(NameOf(Target),
                           GetType(Object), GetType(PropertyGrid),
                           New PropertyMetadata(Nothing,
                                                Sub(s, e)
                                                    Dim this = DirectCast(s, PropertyGrid)
                                                    this.CtlPropertiesView.Content = e.NewValue
                                                End Sub))

End Class
