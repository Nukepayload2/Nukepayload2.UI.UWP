Imports System.Runtime.InteropServices.WindowsRuntime

' The Templated Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234235

Public NotInheritable Class GroupBox
    Inherits ContentControl

    Public Sub New()
        Me.DefaultStyleKey = GetType(GroupBox)
    End Sub


    Public Property Footer As Object
        Get
            Return GetValue(FooterProperty)
        End Get
        Set
            SetValue(FooterProperty, Value)
        End Set
    End Property
    Public Shared ReadOnly FooterProperty As DependencyProperty =
                           DependencyProperty.Register(NameOf(Footer),
                           GetType(Object), GetType(GroupBox),
                           New PropertyMetadata(Nothing))

    Public Property FooterTemplate As DataTemplate
        Get
            Return GetValue(FooterTemplateProperty)
        End Get
        Set
            SetValue(FooterTemplateProperty, Value)
        End Set
    End Property
    Public Shared ReadOnly FooterTemplateProperty As DependencyProperty =
                           DependencyProperty.Register(NameOf(FooterTemplate),
                           GetType(DataTemplate), GetType(GroupBox),
                           New PropertyMetadata(New DataTemplate))

End Class
