' The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

Imports System.Reflection

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
                                                    Dim newValue = e.NewValue
                                                    this.CtlPropertiesView.Content = newValue
                                                    Dim newType = newValue.GetType
                                                    this.TblTypeName.Text = newType.Name
                                                    this.Constructors = newType.GetConstructors
                                                    this.ConstructorSelectedIndex = -1
                                                    this.EventList = Aggregate evt In newType.GetEvents
                                                                     Select New EventBinding(evt) Into ToArray
                                                End Sub))

    Public Property Constructors As ConstructorInfo()
        Get
            Return GetValue(ConstructorsProperty)
        End Get
        Friend Set(value As ConstructorInfo())
            SetValue(ConstructorsProperty, value)
        End Set
    End Property
    Public Shared ReadOnly ConstructorsProperty As DependencyProperty =
                           DependencyProperty.Register(NameOf(Constructors),
                           GetType(ConstructorInfo()), GetType(PropertyGrid),
                           New PropertyMetadata(Nothing))

    Public Property ConstructorSelectedIndex As Integer
        Get
            Return GetValue(ConstructorSelectedIndexProperty)
        End Get
        Friend Set
            SetValue(ConstructorSelectedIndexProperty, Value)
        End Set
    End Property
    Public Shared ReadOnly ConstructorSelectedIndexProperty As DependencyProperty =
                           DependencyProperty.Register(NameOf(ConstructorSelectedIndex),
                           GetType(Integer), GetType(PropertyGrid),
                           New PropertyMetadata(-1,
                                                Sub(s, e)
                                                    Dim this = DirectCast(s, PropertyGrid)
                                                    Dim newValue% = e.NewValue
                                                    If newValue >= 0 AndAlso this.Constructors.Length > newValue Then
                                                        this.CurrentConstructorSelection = New ConstructorSelection With {
                                                            .SelectedConstructor = this.Constructors(newValue)
                                                        }
                                                    End If
                                                End Sub))

    Public Property CurrentConstructorSelection As ConstructorSelection
        Get
            Return GetValue(CurrentConstructorSelectionProperty)
        End Get
        Friend Set
            SetValue(CurrentConstructorSelectionProperty, Value)
        End Set
    End Property
    Public Shared ReadOnly CurrentConstructorSelectionProperty As DependencyProperty =
                           DependencyProperty.Register(NameOf(CurrentConstructorSelection),
                           GetType(ConstructorSelection), GetType(PropertyGrid),
                           New PropertyMetadata(New ConstructorSelection))

    Public Property EventList As EventBinding()
        Get
            Return GetValue(EventListProperty)
        End Get
        Friend Set
            SetValue(EventListProperty, Value)
        End Set
    End Property
    Public Shared ReadOnly EventListProperty As DependencyProperty =
                           DependencyProperty.Register(NameOf(EventList),
                           GetType(EventBinding()), GetType(PropertyGrid),
                           New PropertyMetadata(Nothing))

End Class
