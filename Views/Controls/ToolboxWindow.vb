Imports System.Runtime.InteropServices.WindowsRuntime

' The Templated Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234235

Public NotInheritable Class ToolboxWindow
    Inherits ContentControl

    Dim ncHandler As New NonClientAeraHandler(Me)

    Public Event Closing As EventHandler(Of CancelEventArgs)
    Public Event Closed As EventHandler

    Sub New()
        Me.DefaultStyleKey = GetType(ToolboxWindow)
        CloseCommand = New SimpleCommand(
            Sub()
                Dim cancel As New CancelEventArgs
                RaiseEvent Closing(Me, cancel)
                If Not cancel.Cancel Then
                    RaiseEvent Closed(Me, EventArgs.Empty)
                    Debug.WriteLine("正在关闭")
                End If
            End Sub)
    End Sub

    Public Property Header As Object
        Get
            Return GetValue(HeaderProperty)
        End Get
        Set
            SetValue(HeaderProperty, Value)
        End Set
    End Property
    Public Shared ReadOnly HeaderProperty As DependencyProperty =
                           DependencyProperty.Register(NameOf(Header),
                           GetType(Object), GetType(ToolboxWindow),
                           New PropertyMetadata(Nothing))

    Public Property CloseCommand As ICommand
        Get
            Return GetValue(CloseCommandProperty)
        End Get
        Set
            SetValue(CloseCommandProperty, Value)
        End Set
    End Property
    Public Shared ReadOnly CloseCommandProperty As DependencyProperty =
                           DependencyProperty.Register(NameOf(CloseCommand),
                           GetType(ICommand), GetType(ToolboxWindow),
                           New PropertyMetadata(Nothing))
    Public Sub DragMove()
        ncHandler.DragMove()
    End Sub
End Class