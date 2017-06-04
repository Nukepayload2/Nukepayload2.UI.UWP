Imports System.Reflection
''' <summary>
''' 表示事件绑定方法
''' </summary>
Public Class EventBinding
    Implements INotifyPropertyChanged

    Dim _MethodName As String

    Sub New([event] As EventInfo)
        Me.Event = [event]
    End Sub
    ''' <summary>
    ''' 绑定到事件的方法
    ''' </summary>
    Public Property MethodName As String
        Get
            Return _MethodName
        End Get
        Set(value As String)
            _MethodName = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(MethodName)))
        End Set
    End Property
    ''' <summary>
    ''' 要绑定方法的事件
    ''' </summary>
    Public ReadOnly Property [Event] As EventInfo

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
End Class
